using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using RenPy.Python;
using RenPy.Runtime;
using Debug = UnityEngine.Debug;

namespace RenPy.Unity
{
    /// <summary>
    /// Bridges the engine to Unity.
    ///
    /// The engine runs on a background thread and calls these methods synchronously.
    /// Anything touching Unity is queued onto the main thread; the calls that
    /// represent a player interaction additionally block the engine thread until the
    /// main thread reports the interaction finished.
    /// </summary>
    public class UnityRenPyHost : IRenPyHost
    {
        readonly RenPyPlayer player;
        readonly RenPyFileSystem files;
        readonly Stopwatch clock = Stopwatch.StartNew();

        /// <summary>Work queued for the main thread.</summary>
        readonly Queue<Action> pending = new Queue<Action>();

        /// <summary>Signalled by the main thread when the current interaction ends.</summary>
        readonly ManualResetEventSlim interactionDone = new ManualResetEventSlim(false);
        int interactionResult;

        volatile bool stopping;

        // Distinct from `stopping`: an abort unwinds the script thread but leaves the
        // host usable, so the player can return to the title and start again.
        volatile bool aborting;

        public UnityRenPyHost(RenPyPlayer player, RenPyFileSystem files)
        {
            this.player = player;
            this.files = files;
        }

        public void Stop()
        {
            stopping = true;
            // Release the engine thread wherever it is parked.
            interactionDone.Set();
            screenAction.Set();
        }

        // ---------------------------------------------------------------- dispatch

        /// <summary>Queues work for the main thread and returns immediately.</summary>
        void Dispatch(Action action)
        {
            if (stopping) return;
            lock (pending) pending.Enqueue(action);
        }

        /// <summary>Runs queued work. Called once per frame from the player.</summary>
        public void Pump()
        {
            while (true)
            {
                Action action;
                lock (pending)
                {
                    if (pending.Count == 0) return;
                    action = pending.Dequeue();
                }

                try { action(); }
                catch (Exception e) { Debug.LogException(e); }
            }
        }

        /// <summary>
        /// Starts an interaction on the main thread and parks the engine thread until
        /// the player finishes it.
        /// </summary>
        int AwaitInteraction(Action begin)
        {
            if (stopping) return -1;

            interactionDone.Reset();
            interactionResult = -1;

            Dispatch(begin);
            interactionDone.Wait();

            return interactionResult;
        }

        /// <summary>Called from the main thread to release a parked engine thread.</summary>
        public void CompleteInteraction(int result)
        {
            interactionResult = result;
            interactionDone.Set();
        }

        // ---------------------------------------------------------------- assets

        public bool FileExists(string path) { return files.Exists(path); }
        public byte[] ReadFile(string path) { return files.Read(path); }
        public IEnumerable<string> ListFiles() { return files.Files; }

        // ---------------------------------------------------------------- display

        public void Show(ShownImage image)
        {
            // The engine mutates its own copy after this returns, so snapshot the
            // transform the UI will read.
            var snapshot = new ShownImage
            {
                Tag = image.Tag,
                Layer = image.Layer,
                Name = image.Name,
                AssetPath = image.AssetPath,
                Displayable = image.Displayable,
                ZOrder = image.ZOrder,
                Transform = image.Transform.Clone(),
                Timeline = image.Timeline,
                Frames = image.Frames,
                FrameDelay = image.FrameDelay,
            };

            Dispatch(() =>
            {
                player.UI.RecordZ(snapshot);
                player.UI.Show(snapshot);
            });
        }

        public void Hide(string layer, string tag) { Dispatch(() => player.UI.Hide(layer, tag)); }

        public void ClearLayer(string layer) { Dispatch(() => player.UI.ClearLayer(layer)); }

        public void UpdateTransform(string layer, string tag, TransformState state) { }

        public void Transition(string layer, string name, float duration)
        {
            if (duration <= 0f) return;

            // A transition is a timed interaction: hold the script while it plays.
            AwaitInteraction(() => player.BeginTransition(name, duration));
        }

        // ---------------------------------------------------------------- interaction

        public void Say(SpeakerStyle who, string what, float autoAdvance, bool noWait)
        {
            AwaitInteraction(() => player.BeginSay(who, what, autoAdvance, noWait));
        }

        public int Menu(string caption, List<string> choices)
        {
            int picked = AwaitInteraction(() => player.BeginMenu(caption, choices));
            return picked < 0 ? 0 : picked;
        }

        public void Pause(float seconds, bool hard)
        {
            // renpy.pause(0) is a yield point, not a wait.
            if (seconds == 0f) return;
            AwaitInteraction(() => player.BeginPause(seconds, hard));
        }

        public void MovieCutscene(string path)
        {
            AwaitInteraction(() => player.BeginMovie(path));
        }

        // ---------------------------------------------------------------- audio

        public void PlayAudio(string channel, AudioSpec spec, float fadeIn, bool loop)
        {
            if (spec == null || string.IsNullOrEmpty(spec.Path)) return;
            Dispatch(() => player.Audio.Play(channel, spec, fadeIn, loop));
        }

        public void QueueAudio(string channel, AudioSpec spec)
        {
            if (spec == null || string.IsNullOrEmpty(spec.Path)) return;
            Dispatch(() => player.Audio.Queue(channel, spec));
        }

        public void StopAudio(string channel, float fadeOut)
        {
            Dispatch(() => player.Audio.Stop(channel, fadeOut));
        }

        public void SetAudioPaused(string channel, bool paused)
        {
            Dispatch(() => player.Audio.SetPaused(channel, paused));
        }

        public void SetAudioVolume(string channel, float volume)
        {
            Dispatch(() => player.Audio.SetVolume(channel, volume));
        }

        // Audio positions are read from the engine thread but only knowable on the
        // main thread, so they are snapshotted once per frame.
        readonly Dictionary<string, float> positions = new Dictionary<string, float>();
        readonly Dictionary<string, float> positionScratch = new Dictionary<string, float>();

        /// <summary>Called each frame on the main thread.</summary>
        public void SamplePositions(RenPyAudio audio)
        {
            positionScratch.Clear();
            audio.SnapshotPositions(positionScratch);

            lock (positions)
            {
                positions.Clear();
                foreach (var kv in positionScratch) positions[kv.Key] = kv.Value;
            }
        }

        public float GetAudioPosition(string channel)
        {
            if (channel == null) return -1f;
            lock (positions)
            {
                float value;
                return positions.TryGetValue(channel, out value) ? value : -1f;
            }
        }

        // ---------------------------------------------------------------- screens

        public void ScreensChanged(List<ShownScreen> screens)
        {
            // Snapshot the list; the engine mutates its own copy.
            var copy = new List<ShownScreen>(screens);
            Dispatch(() => player.RebuildScreens(copy));
        }

        readonly ManualResetEventSlim screenAction = new ManualResetEventSlim(false);

        /// <summary>Called from the main thread when a screen widget is activated.</summary>
        public void NotifyScreenAction() { screenAction.Set(); }

        public bool WaitForScreenAction()
        {
            if (stopping) return false;

            screenAction.Reset();
            screenAction.Wait();

            return !stopping;
        }

        // ---------------------------------------------------------------- misc

        public float Time { get { return (float)clock.Elapsed.TotalSeconds; } }

        public void Log(string message)
        {
            if (player.VerboseLogging) Debug.Log("[RenPy] " + message);
        }

        public bool ShouldStop { get { return stopping || aborting; } }

        /// <summary>Unwinds the running script without shutting the host down.</summary>
        public void RequestAbort()
        {
            aborting = true;
            interactionDone.Set();
            screenAction.Set();
        }

        /// <summary>Clears the abort so a new script run can begin.</summary>
        public void ClearAbort()
        {
            aborting = false;
            interactionDone.Reset();
            screenAction.Reset();
        }
    }
}
