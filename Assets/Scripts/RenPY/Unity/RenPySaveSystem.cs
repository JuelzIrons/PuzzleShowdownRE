using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;
using RenPy.Python;
using RenPy.Runtime;

namespace RenPy.Unity
{
    /// <summary>Everything a slot needs to draw itself without loading the save.</summary>
    public sealed class SaveSlotInfo
    {
        public int Slot;
        public bool Occupied;
        public string SavedAt = "";
        public string LabelHint = "";
        public string Caption = "";
    }

    /// <summary>
    /// Reads and writes save slots.
    ///
    /// Slots live as files under persistentDataPath rather than in PlayerPrefs:
    /// a save carries the whole store and scene, which is far larger than PlayerPrefs
    /// is meant to hold. Persistent data (the endings gallery) stays in PlayerPrefs.
    /// </summary>
    public static class RenPySaveSystem
    {
        public const int SlotCount = 9;

        static string Root(string gameId)
        {
            string safe = string.IsNullOrEmpty(gameId) ? "game" : gameId.Replace('/', '_').Replace('\\', '_');
            return Path.Combine(Application.persistentDataPath, "renpy-saves", safe);
        }

        static string SlotPath(string gameId, int slot)
        {
            return Path.Combine(Root(gameId), "slot" + slot.ToString(CultureInfo.InvariantCulture) + ".txt");
        }

        // ---------------------------------------------------------------- listing

        public static List<SaveSlotInfo> List(string gameId)
        {
            var rv = new List<SaveSlotInfo>();

            for (int i = 1; i <= SlotCount; i++)
            {
                var info = new SaveSlotInfo { Slot = i };
                string path = SlotPath(gameId, i);

                if (File.Exists(path))
                {
                    try
                    {
                        // The header is the first three lines, so a listing never
                        // has to parse the whole save.
                        using (var reader = new StreamReader(path))
                        {
                            info.SavedAt = reader.ReadLine() ?? "";
                            info.LabelHint = reader.ReadLine() ?? "";
                            info.Caption = reader.ReadLine() ?? "";
                        }
                        info.Occupied = true;
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning("[RenPy] could not read save slot " + i + ": " + e.Message);
                    }
                }

                rv.Add(info);
            }

            return rv;
        }

        public static bool Exists(string gameId, int slot) { return File.Exists(SlotPath(gameId, slot)); }

        public static void Delete(string gameId, int slot)
        {
            string path = SlotPath(gameId, slot);
            if (File.Exists(path)) File.Delete(path);
        }

        /// <summary>The most recently written slot, or -1 when there are none.</summary>
        public static int MostRecent(string gameId)
        {
            int best = -1;
            DateTime bestTime = DateTime.MinValue;

            for (int i = 1; i <= SlotCount; i++)
            {
                string path = SlotPath(gameId, i);
                if (!File.Exists(path)) continue;

                DateTime written = File.GetLastWriteTimeUtc(path);
                if (written <= bestTime) continue;

                bestTime = written;
                best = i;
            }

            return best;
        }

        // ---------------------------------------------------------------- writing

        public static bool Save(string gameId, int slot, RenPySaveState state)
        {
            if (state == null) return false;

            try
            {
                Directory.CreateDirectory(Root(gameId));

                var sb = new StringBuilder();

                // Header: three display lines, newline separated.
                sb.AppendLine(Clean(state.SavedAt));
                sb.AppendLine(Clean(state.LabelHint));
                sb.AppendLine(Clean(Trim(state.Caption, 90)));

                sb.AppendLine("node " + (state.NodeId ?? ""));

                foreach (var id in state.CallStack) sb.AppendLine("call " + id);

                foreach (var image in state.Scene)
                {
                    sb.AppendLine("image " + image.Layer + "" + image.Tag + "" +
                                  image.Name + "" + image.ZOrder.ToString(CultureInfo.InvariantCulture));
                }

                foreach (var kv in state.Store)
                {
                    string encoded;
                    if (!ValueCodec.Encode(kv.Value, out encoded)) continue;
                    sb.AppendLine("var " + PyOps.ToStr(kv.Key) + "" + encoded);
                }

                File.WriteAllText(SlotPath(gameId, slot), sb.ToString());
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("[RenPy] save to slot " + slot + " failed: " + e.Message);
                return false;
            }
        }

        static string Clean(string s)
        {
            return string.IsNullOrEmpty(s) ? "" : s.Replace("\n", " ").Replace("\r", " ");
        }

        static string Trim(string s, int max)
        {
            if (string.IsNullOrEmpty(s)) return "";
            // Strip rich-text markup so a slot caption reads cleanly.
            var sb = new StringBuilder();
            bool inTag = false;
            foreach (char c in s)
            {
                if (c == '<') { inTag = true; continue; }
                if (c == '>') { inTag = false; continue; }
                if (!inTag) sb.Append(c);
            }
            string text = sb.ToString().Trim();
            return text.Length > max ? text.Substring(0, max) + "..." : text;
        }

        // ---------------------------------------------------------------- reading

        public static RenPySaveState Load(string gameId, int slot)
        {
            string path = SlotPath(gameId, slot);
            if (!File.Exists(path)) return null;

            try
            {
                var lines = File.ReadAllLines(path);
                if (lines.Length < 4) return null;

                var state = new RenPySaveState
                {
                    SavedAt = lines[0],
                    LabelHint = lines[1],
                    Caption = lines[2],
                };

                for (int i = 3; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.Length == 0) continue;

                    int space = line.IndexOf(' ');
                    if (space < 0) continue;

                    string kind = line.Substring(0, space);
                    string body = line.Substring(space + 1);

                    switch (kind)
                    {
                        case "node":
                            state.NodeId = body;
                            break;

                        case "call":
                            state.CallStack.Add(body);
                            break;

                        case "image":
                        {
                            var parts = body.Split('');
                            if (parts.Length < 4) break;
                            int z;
                            int.TryParse(parts[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out z);
                            state.Scene.Add(new RenPySaveState.SavedImage
                            {
                                Layer = parts[0],
                                Tag = parts[1],
                                Name = parts[2],
                                ZOrder = z,
                            });
                            break;
                        }

                        case "var":
                        {
                            int sep = body.IndexOf('');
                            if (sep < 0) break;

                            object value;
                            if (ValueCodec.Decode(body.Substring(sep + 1), out value))
                                state.Store.Set(body.Substring(0, sep), value);
                            break;
                        }
                    }
                }

                return state;
            }
            catch (Exception e)
            {
                Debug.LogError("[RenPy] load of slot " + slot + " failed: " + e.Message);
                return null;
            }
        }
    }
}
