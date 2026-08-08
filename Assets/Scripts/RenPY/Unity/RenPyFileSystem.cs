using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.Networking;

namespace RenPy.Unity
{
    /// <summary>
    /// Locates a Ren'Py game under StreamingAssets and makes it readable with plain
    /// file IO.
    ///
    /// On desktop and in the editor the folder is used in place. On Android
    /// StreamingAssets lives inside the APK, where directories cannot be enumerated
    /// and files cannot be opened directly, so the game is installed once into
    /// persistentDataPath — from a .zip if one is present, otherwise by copying the
    /// files listed in a build-time manifest.
    /// </summary>
    public class RenPyFileSystem
    {
        /// <summary>Name of the manifest an editor build writes next to the game folder.</summary>
        public const string ManifestName = "renpy_manifest.txt";

        /// <summary>Absolute path of the directory holding `game/`.</summary>
        public string RootPath { get; private set; }

        /// <summary>Absolute path of the `game/` directory itself.</summary>
        public string GamePath { get; private set; }

        /// <summary>Game-relative paths of every file, e.g. "images/bg.png".</summary>
        public readonly List<string> Files = new List<string>();

        public bool Ready { get; private set; }
        public string Error { get; private set; }
        public float Progress { get; private set; }
        public string Status { get; private set; } = "";

        readonly string requestedPath;

        public RenPyFileSystem(string streamingAssetsRelativePath)
        {
            requestedPath = (streamingAssetsRelativePath ?? "").Trim().Trim('/');
        }

        // ---------------------------------------------------------------- install

        /// <summary>
        /// Prepares the game for reading. Runs as a coroutine because the Android
        /// path has to stream files out of the APK.
        /// </summary>
        public IEnumerator Prepare()
        {
            Status = "Locating game";

            string streamingRoot = Application.streamingAssetsPath;
            string directCandidate = CombineUrl(streamingRoot, requestedPath);

            // Desktop and editor: read the folder where it sits.
            if (!IsCompressedStreamingAssets())
            {
                string found = Directory.Exists(directCandidate)
                    ? directCandidate
                    : AutoDetect(streamingRoot);

                if (found == null)
                {
                    Error = "No Ren'Py game found at StreamingAssets/" + requestedPath +
                            ". Put the unzipped game folder (the one containing 'game') there.";
                    yield break;
                }

                if (found != directCandidate)
                    Debug.Log("[RenPy] '" + requestedPath + "' not found; using " + found + " instead.");

                if (!Adopt(found)) yield break;
                IndexFromDisk();
                Ready = true;
                Progress = 1f;
                Status = "Ready";
                yield break;
            }

            // Everything below installs into persistentDataPath first.
            string installRoot = Path.Combine(Application.persistentDataPath, "renpy", SafeName(requestedPath));

            if (IsInstalled(installRoot))
            {
                if (!Adopt(installRoot)) yield break;
                IndexFromDisk();
                Ready = true;
                Progress = 1f;
                Status = "Ready";
                yield break;
            }

            // Prefer a zip: one read, and it enumerates itself.
            string zipUrl = CombineUrl(streamingRoot, requestedPath + ".zip");
            bool zipExists = false;
            yield return CheckExists(zipUrl, result => zipExists = result);

            if (zipExists)
            {
                yield return InstallFromZip(zipUrl, installRoot);
            }
            else
            {
                yield return InstallFromManifest(streamingRoot, installRoot);
            }

            if (Error != null) yield break;

            if (!Adopt(installRoot)) yield break;
            IndexFromDisk();
            Ready = true;
            Progress = 1f;
            Status = "Ready";
        }

        /// <summary>
        /// Finds the only Ren'Py game under StreamingAssets, so a mistyped or default
        /// Game Path still works when there is no ambiguity.
        /// </summary>
        static string AutoDetect(string streamingRoot)
        {
            if (!Directory.Exists(streamingRoot)) return null;

            string only = null;

            foreach (var candidate in SafeDirectories(streamingRoot))
            {
                if (!Directory.Exists(Path.Combine(candidate, "game"))) continue;
                if (only != null) return null;   // ambiguous: make the user choose
                only = candidate;
            }

            return only;
        }

        /// <summary>StreamingAssets is inside the APK/JAR on Android only.</summary>
        static bool IsCompressedStreamingAssets()
        {
            return Application.streamingAssetsPath.Contains("://");
        }

        static string SafeName(string path)
        {
            if (string.IsNullOrEmpty(path)) return "game";
            var chars = path.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
                if (chars[i] == '/' || chars[i] == '\\' || chars[i] == ':') chars[i] = '_';
            return new string(chars);
        }

        static bool IsInstalled(string root)
        {
            return Directory.Exists(root) && File.Exists(Path.Combine(root, ".installed"));
        }

        /// <summary>Points RootPath/GamePath at an installed copy.</summary>
        bool Adopt(string root)
        {
            string game = Path.Combine(root, "game");

            if (!Directory.Exists(game))
            {
                // The chosen folder may itself be the `game` directory, or the game
                // may sit one level down inside a wrapper folder.
                if (File.Exists(Path.Combine(root, "script.rpyc")) ||
                    Directory.Exists(Path.Combine(root, "images")))
                {
                    RootPath = Directory.GetParent(root)?.FullName ?? root;
                    GamePath = root;
                    return true;
                }

                foreach (var candidate in SafeDirectories(root))
                {
                    if (!Directory.Exists(Path.Combine(candidate, "game"))) continue;
                    RootPath = candidate;
                    GamePath = Path.Combine(candidate, "game");
                    return true;
                }

                Error = "No 'game' directory found under " + root;
                return false;
            }

            RootPath = root;
            GamePath = game;
            return true;
        }

        static IEnumerable<string> SafeDirectories(string root)
        {
            string[] rv;
            try { rv = Directory.GetDirectories(root); }
            catch (Exception) { yield break; }
            foreach (var d in rv) yield return d;
        }

        void IndexFromDisk()
        {
            Files.Clear();
            if (GamePath == null) return;

            int prefix = GamePath.Length + 1;
            foreach (var path in Directory.EnumerateFiles(GamePath, "*", SearchOption.AllDirectories))
            {
                if (path.Length <= prefix) continue;
                Files.Add(path.Substring(prefix).Replace('\\', '/'));
            }
        }

        // ---------------------------------------------------------------- zip

        IEnumerator InstallFromZip(string zipUrl, string installRoot)
        {
            Status = "Downloading archive";

            byte[] data = null;
            yield return ReadUrl(zipUrl, bytes => data = bytes);

            if (data == null)
            {
                Error = "Could not read " + zipUrl;
                yield break;
            }

            Status = "Extracting";
            Directory.CreateDirectory(installRoot);

            // Extraction is CPU-bound; yielding every so often keeps the frame alive.
            using (var stream = new MemoryStream(data))
            using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                int total = archive.Entries.Count;
                int done = 0;

                foreach (var entry in archive.Entries)
                {
                    done++;
                    Progress = total == 0 ? 1f : (float)done / total;

                    if (string.IsNullOrEmpty(entry.Name)) continue; // directory entry

                    string target = Path.Combine(installRoot, entry.FullName.Replace('\\', '/'));
                    string dir = Path.GetDirectoryName(target);

                    // Refuse entries that would escape the install directory.
                    if (dir == null || !IsInside(installRoot, target))
                    {
                        Debug.LogWarning("[RenPy] skipping unsafe zip entry: " + entry.FullName);
                        continue;
                    }

                    Directory.CreateDirectory(dir);

                    using (var source = entry.Open())
                    using (var output = File.Create(target))
                        source.CopyTo(output);

                    if ((done & 31) == 0) yield return null;
                }
            }

            File.WriteAllText(Path.Combine(installRoot, ".installed"), DateTime.UtcNow.ToString("o"));
        }

        static bool IsInside(string root, string candidate)
        {
            string fullRoot = Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            string fullCandidate = Path.GetFullPath(candidate);
            return fullCandidate.StartsWith(fullRoot, StringComparison.Ordinal);
        }

        // ---------------------------------------------------------------- manifest

        IEnumerator InstallFromManifest(string streamingRoot, string installRoot)
        {
            string manifestUrl = CombineUrl(streamingRoot, CombineUrl(requestedPath, ManifestName));

            byte[] manifestData = null;
            yield return ReadUrl(manifestUrl, bytes => manifestData = bytes);

            if (manifestData == null)
            {
                Error =
                    "No game found. Put the unzipped Ren'Py game at StreamingAssets/" + requestedPath +
                    " and run 'Tools > Ren'Py > Generate Manifest' (needed for Android), " +
                    "or place a zip at StreamingAssets/" + requestedPath + ".zip";
                yield break;
            }

            var entries = new List<string>();
            foreach (var line in System.Text.Encoding.UTF8.GetString(manifestData).Split('\n'))
            {
                string trimmed = line.Trim();
                if (trimmed.Length > 0 && !trimmed.StartsWith("#", StringComparison.Ordinal)) entries.Add(trimmed);
            }

            Status = "Installing";
            Directory.CreateDirectory(installRoot);

            for (int i = 0; i < entries.Count; i++)
            {
                Progress = entries.Count == 0 ? 1f : (float)i / entries.Count;

                string relative = entries[i];
                string target = Path.Combine(installRoot, relative.Replace('/', Path.DirectorySeparatorChar));

                if (!IsInside(installRoot, target)) continue;

                Directory.CreateDirectory(Path.GetDirectoryName(target));

                byte[] data = null;
                yield return ReadUrl(CombineUrl(streamingRoot, CombineUrl(requestedPath, relative)), bytes => data = bytes);

                if (data == null)
                {
                    Debug.LogWarning("[RenPy] missing file listed in manifest: " + relative);
                    continue;
                }

                File.WriteAllBytes(target, data);
            }

            File.WriteAllText(Path.Combine(installRoot, ".installed"), DateTime.UtcNow.ToString("o"));
        }

        // ---------------------------------------------------------------- reading

        static string CombineUrl(string a, string b)
        {
            if (string.IsNullOrEmpty(b)) return a;
            if (string.IsNullOrEmpty(a)) return b;
            return a.TrimEnd('/') + "/" + b.TrimStart('/');
        }

        static IEnumerator ReadUrl(string url, Action<byte[]> onDone)
        {
            // A plain path still needs the file:// scheme for UnityWebRequest.
            string request = url.Contains("://") ? url : "file://" + url;

            using (var www = UnityWebRequest.Get(request))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    onDone(null);
                    yield break;
                }

                onDone(www.downloadHandler.data);
            }
        }

        static IEnumerator CheckExists(string url, Action<bool> onDone)
        {
            if (!url.Contains("://"))
            {
                onDone(File.Exists(url));
                yield break;
            }

            using (var www = UnityWebRequest.Head(url))
            {
                yield return www.SendWebRequest();
                onDone(www.result == UnityWebRequest.Result.Success);
            }
        }

        // ---------------------------------------------------------------- access

        /// <summary>Resolves a game-relative path to an absolute one.</summary>
        public string Absolute(string relative)
        {
            if (string.IsNullOrEmpty(relative) || GamePath == null) return null;
            return Path.Combine(GamePath, relative.Replace('/', Path.DirectorySeparatorChar));
        }

        public bool Exists(string relative)
        {
            string absolute = Absolute(relative);
            return absolute != null && File.Exists(absolute);
        }

        public byte[] Read(string relative)
        {
            string absolute = Absolute(relative);
            return absolute != null && File.Exists(absolute) ? File.ReadAllBytes(absolute) : null;
        }

        /// <summary>The .rpyc files that make up the script, in load order.</summary>
        public List<KeyValuePair<string, byte[]>> ReadScripts()
        {
            var rv = new List<KeyValuePair<string, byte[]>>();
            if (GamePath == null) return rv;

            var names = new List<string>();
            foreach (var relative in Files)
                if (relative.EndsWith(".rpyc", StringComparison.OrdinalIgnoreCase)) names.Add(relative);

            // Ren'Py loads options and gui before screens and the script proper, and
            // init priorities sort out the rest.
            names.Sort((a, b) => LoadRank(a).CompareTo(LoadRank(b)));

            foreach (var name in names)
            {
                byte[] data = Read(name);
                if (data != null) rv.Add(new KeyValuePair<string, byte[]>(name, data));
            }

            return rv;
        }

        static int LoadRank(string path)
        {
            string file = Path.GetFileName(path).ToLowerInvariant();
            switch (file)
            {
                case "options.rpyc": return 0;
                case "gui.rpyc": return 1;
                case "screens.rpyc": return 2;
                case "script.rpyc": return 4;
                default: return 3;
            }
        }
    }
}
