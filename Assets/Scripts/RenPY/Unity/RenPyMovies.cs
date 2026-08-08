using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

namespace RenPy.Unity
{
    /// <summary>
    /// Video shown as part of the scene, via Ren'Py's `Movie` displayable.
    ///
    /// A game can `show` a movie the same way it shows a sprite, so each movie tag
    /// gets its own VideoPlayer rendering into a RenderTexture that the layer's
    /// RawImage samples. Players are pooled per tag and released when the tag is
    /// hidden or its layer is cleared.
    /// </summary>
    public class RenPyMovies
    {
        sealed class Movie
        {
            public VideoPlayer Player;
            public RenderTexture Target;
            public string Path;
        }

        readonly MonoBehaviour owner;
        readonly RenPyFileSystem files;
        readonly Transform root;

        readonly Dictionary<string, Movie> movies = new Dictionary<string, Movie>();

        /// <summary>Ceiling on simultaneous videos; decoding several at once is costly.</summary>
        public int MaxConcurrent = 3;

        public RenPyMovies(MonoBehaviour owner, RenPyFileSystem files)
        {
            this.owner = owner;
            this.files = files;

            var go = new GameObject("RenPy Movies");
            go.transform.SetParent(owner.transform, false);
            root = go.transform;
        }

        /// <summary>
        /// Returns the texture a movie is rendering into, starting playback the first
        /// time a given key asks for it. Null when the file is missing.
        /// </summary>
        public Texture Resolve(string key, string relativePath, bool loop)
        {
            if (string.IsNullOrEmpty(relativePath)) return null;

            Movie movie;
            if (movies.TryGetValue(key, out movie))
            {
                // Same tag, different clip: restart rather than leaking a player.
                if (movie.Path == relativePath) return movie.Target;
                Release(key);
            }

            string absolute = files.Absolute(relativePath);
            if (absolute == null || !File.Exists(absolute))
            {
                Debug.LogWarning("[RenPy] movie not found: " + relativePath);
                return null;
            }

            if (movies.Count >= MaxConcurrent) ReleaseOldest();

            var go = new GameObject("movie " + key);
            go.transform.SetParent(root, false);

            movie = new Movie
            {
                Path = relativePath,
                Target = new RenderTexture(1920, 1080, 0),
                Player = go.AddComponent<VideoPlayer>(),
            };

            movie.Player.playOnAwake = false;
            movie.Player.renderMode = VideoRenderMode.RenderTexture;
            movie.Player.targetTexture = movie.Target;
            movie.Player.source = VideoSource.Url;
            movie.Player.url = "file://" + absolute;
            movie.Player.isLooping = loop;
            // Ren'Py's Movie(play=...) plays the file on an audio channel, so the
            // soundtrack baked into the video is meant to be heard.
            movie.Player.audioOutputMode = VideoAudioOutputMode.Direct;
            movie.Player.Play();

            movies[key] = movie;
            order.Add(key);

            return movie.Target;
        }

        readonly List<string> order = new List<string>();

        public void Release(string key)
        {
            Movie movie;
            if (!movies.TryGetValue(key, out movie)) return;

            movies.Remove(key);
            order.Remove(key);

            if (movie.Player != null)
            {
                movie.Player.Stop();
                UnityEngine.Object.Destroy(movie.Player.gameObject);
            }
            if (movie.Target != null) movie.Target.Release();
        }

        /// <summary>Releases every movie whose key belongs to a layer.</summary>
        public void ReleaseLayer(string layer)
        {
            string prefix = layer + "/";
            var doomed = new List<string>();
            foreach (var key in movies.Keys)
                if (key.StartsWith(prefix, StringComparison.Ordinal)) doomed.Add(key);

            foreach (var key in doomed) Release(key);
        }

        void ReleaseOldest()
        {
            if (order.Count == 0) return;
            Release(order[0]);
        }

        public void ReleaseAll()
        {
            foreach (var key in new List<string>(movies.Keys)) Release(key);
        }
    }
}
