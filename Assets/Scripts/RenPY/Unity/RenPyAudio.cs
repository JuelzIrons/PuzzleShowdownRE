using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using RenPy.Runtime;

namespace RenPy.Unity
{
    /// <summary>
    /// Ren'Py's channel-based audio, mapped onto AudioSources. Channels are created
    /// on demand, so a game can invent its own ("vo", "ambient", ...) freely.
    /// </summary>
    public class RenPyAudio
    {
        sealed class Channel
        {
            public AudioSource Source;
            public string Name;
            public float Volume = 1f;
            /// <summary>Running fade, if any.</summary>
            public Coroutine Fade;
            public AudioSpec Queued;
            /// <summary>Bumped by every play/stop so stale loads can bow out.</summary>
            public int Generation;
        }

        readonly MonoBehaviour owner;
        readonly RenPyFileSystem files;
        readonly Transform root;

        readonly Dictionary<string, Channel> channels = new Dictionary<string, Channel>();
        readonly Dictionary<string, AudioClip> clipCache = new Dictionary<string, AudioClip>();

        /// <summary>Channels that loop unless a statement says otherwise.</summary>
        static readonly HashSet<string> LoopingByDefault = new HashSet<string> { "music", "ambient" };

        public int MaxCachedClips = 24;
        readonly List<string> cacheOrder = new List<string>();

        public RenPyAudio(MonoBehaviour owner, RenPyFileSystem files)
        {
            this.owner = owner;
            this.files = files;

            var go = new GameObject("RenPy Audio");
            go.transform.SetParent(owner.transform, false);
            root = go.transform;
        }

        Channel Get(string name)
        {
            if (string.IsNullOrEmpty(name)) name = "sound";

            Channel channel;
            if (channels.TryGetValue(name, out channel)) return channel;

            var go = new GameObject("channel " + name);
            go.transform.SetParent(root, false);

            channel = new Channel
            {
                Name = name,
                Source = go.AddComponent<AudioSource>(),
            };
            channel.Source.playOnAwake = false;
            channel.Source.loop = LoopingByDefault.Contains(name);

            channels[name] = channel;
            return channel;
        }

        // ---------------------------------------------------------------- playback

        public void Play(string channelName, AudioSpec spec, float fadeIn, bool loop)
        {
            var channel = Get(channelName);
            // Supersede anything already loading for this channel.
            channel.Generation++;
            owner.StartCoroutine(PlayRoutine(channel, spec, fadeIn, loop, channel.Generation));
        }

        IEnumerator PlayRoutine(Channel channel, AudioSpec spec, float fadeIn, bool loop, int generation)
        {
            AudioClip clip = null;
            yield return LoadClip(spec.Path, result => clip = result);

            if (clip == null) yield break;

            // Decoding is asynchronous, so a stop (or a newer play) may have happened
            // while this one was loading. Without this guard the audio restarts after
            // it was told to stop, and clicking through dialogue stacks up playbacks.
            if (channel.Generation != generation) yield break;

            channel.Source.clip = clip;
            channel.Source.loop = loop;

            // `<from N>` starts partway in, which this game uses for voice sync.
            float start = Mathf.Clamp(spec.From, 0f, Mathf.Max(0f, clip.length - 0.05f));
            channel.Source.time = start;

            if (fadeIn > 0f)
            {
                channel.Source.volume = 0f;
                channel.Source.Play();
                StartFade(channel, channel.Volume, fadeIn, false);
            }
            else
            {
                channel.Source.volume = channel.Volume;
                channel.Source.Play();
            }

            // Seeking a streamed clip before Play is unreliable — the source can start
            // from zero instead, which desynchronises voice-over. Re-seek once playing.
            if (start > 0f && Mathf.Abs(channel.Source.time - start) > 0.05f)
                channel.Source.time = start;

            // `to N` stops early.
            if (spec.To > 0f && spec.To > spec.From)
                owner.StartCoroutine(StopAt(channel, clip, spec.To - spec.From));
        }

        IEnumerator StopAt(Channel channel, AudioClip clip, float after)
        {
            yield return new WaitForSeconds(after);
            if (channel.Source != null && channel.Source.clip == clip) channel.Source.Stop();
        }

        public void Queue(string channelName, AudioSpec spec)
        {
            var channel = Get(channelName);
            channel.Queued = spec;
            owner.StartCoroutine(QueueRoutine(channel));
        }

        IEnumerator QueueRoutine(Channel channel)
        {
            while (channel.Source != null && channel.Source.isPlaying) yield return null;

            var spec = channel.Queued;
            channel.Queued = null;
            if (spec != null)
            {
                channel.Generation++;
                yield return PlayRoutine(channel, spec, 0f, channel.Source.loop, channel.Generation);
            }
        }

        public void Stop(string channelName, float fadeOut)
        {
            Channel channel;
            if (!channels.TryGetValue(channelName ?? "sound", out channel)) return;

            channel.Queued = null;
            // Cancel any load still in flight, or it will start playing after this.
            channel.Generation++;

            if (fadeOut > 0f && channel.Source.isPlaying) StartFade(channel, 0f, fadeOut, true);
            else channel.Source.Stop();
        }

        public void SetPaused(string channelName, bool paused)
        {
            Channel channel;
            if (!channels.TryGetValue(channelName ?? "sound", out channel)) return;

            if (paused) channel.Source.Pause();
            else channel.Source.UnPause();
        }

        public void SetVolume(string channelName, float volume)
        {
            var channel = Get(channelName);
            channel.Volume = Mathf.Clamp01(volume);
            if (channel.Fade == null) channel.Source.volume = channel.Volume;
        }

        /// <summary>
        /// Position within the file on a channel, or -1 when idle. Ren'Py games
        /// compare this against their own markers to resynchronise voice tracks.
        /// </summary>
        public float Position(string channelName)
        {
            Channel channel;
            if (!channels.TryGetValue(channelName ?? "sound", out channel)) return -1f;
            if (channel.Source == null || !channel.Source.isPlaying) return -1f;
            return channel.Source.time;
        }

        /// <summary>Copies every channel position out for the engine thread to read.</summary>
        public void SnapshotPositions(Dictionary<string, float> into)
        {
            foreach (var kv in channels)
            {
                var source = kv.Value.Source;
                into[kv.Key] = (source != null && source.isPlaying) ? source.time : -1f;
            }
        }

        public void StopAll()
        {
            foreach (var channel in channels.Values)
            {
                channel.Generation++;
                channel.Queued = null;
                if (channel.Source != null) channel.Source.Stop();
            }
        }

        /// <summary>Stops a channel immediately, cancelling anything mid-load.</summary>
        public void StopNow(string channelName)
        {
            Channel channel;
            if (!channels.TryGetValue(channelName ?? "sound", out channel)) return;

            channel.Generation++;
            channel.Queued = null;

            if (channel.Fade != null) { owner.StopCoroutine(channel.Fade); channel.Fade = null; }
            if (channel.Source != null) channel.Source.Stop();
        }

        void StartFade(Channel channel, float target, float duration, bool stopAtEnd)
        {
            if (channel.Fade != null) owner.StopCoroutine(channel.Fade);
            channel.Fade = owner.StartCoroutine(FadeRoutine(channel, target, duration, stopAtEnd));
        }

        IEnumerator FadeRoutine(Channel channel, float target, float duration, bool stopAtEnd)
        {
            float start = channel.Source.volume;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                channel.Source.volume = Mathf.Lerp(start, target, Mathf.Clamp01(elapsed / duration));
                yield return null;
            }

            channel.Source.volume = target;
            if (stopAtEnd) channel.Source.Stop();
            channel.Fade = null;
        }

        // ---------------------------------------------------------------- loading

        IEnumerator LoadClip(string relativePath, Action<AudioClip> onDone)
        {
            AudioClip cached;
            if (clipCache.TryGetValue(relativePath, out cached) && cached != null)
            {
                Touch(relativePath);
                onDone(cached);
                yield break;
            }

            string absolute = files.Absolute(relativePath);
            if (absolute == null || !File.Exists(absolute))
            {
                Debug.LogWarning("[RenPy] audio not found: " + relativePath);
                onDone(null);
                yield break;
            }

            AudioType type = TypeFor(absolute);

            using (var www = UnityWebRequestMultimedia.GetAudioClip("file://" + absolute, type))
            {
                // Streaming keeps long voice tracks off the heap.
                ((DownloadHandlerAudioClip)www.downloadHandler).streamAudio = true;

                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogWarning("[RenPy] could not decode audio " + relativePath + ": " + www.error);
                    onDone(null);
                    yield break;
                }

                var clip = DownloadHandlerAudioClip.GetContent(www);
                if (clip == null) { onDone(null); yield break; }

                clip.name = relativePath;
                Remember(relativePath, clip);
                onDone(clip);
            }
        }

        static AudioType TypeFor(string path)
        {
            string ext = Path.GetExtension(path).ToLowerInvariant();
            switch (ext)
            {
                case ".mp3": return AudioType.MPEG;
                case ".ogg": return AudioType.OGGVORBIS;
                case ".wav": return AudioType.WAV;
                case ".aiff":
                case ".aif": return AudioType.AIFF;
                default: return AudioType.UNKNOWN;
            }
        }

        void Remember(string key, AudioClip clip)
        {
            clipCache[key] = clip;
            cacheOrder.Add(key);

            while (cacheOrder.Count > MaxCachedClips)
            {
                string oldest = cacheOrder[0];
                cacheOrder.RemoveAt(0);

                AudioClip victim;
                if (!clipCache.TryGetValue(oldest, out victim)) continue;

                // Never evict something still playing.
                bool inUse = false;
                foreach (var channel in channels.Values)
                    if (channel.Source != null && channel.Source.clip == victim) { inUse = true; break; }

                if (inUse) { cacheOrder.Add(oldest); continue; }

                clipCache.Remove(oldest);
                if (victim != null) UnityEngine.Object.Destroy(victim);
            }
        }

        void Touch(string key)
        {
            cacheOrder.Remove(key);
            cacheOrder.Add(key);
        }
    }
}
