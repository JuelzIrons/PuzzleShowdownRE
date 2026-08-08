using System;
using System.Collections.Generic;

namespace RenPy.Runtime
{
    /// <summary>A displayable currently shown on a layer.</summary>
    public sealed class ShownImage
    {
        public string Tag;
        public string Layer;
        /// <summary>Full image name, e.g. "eileen happy".</summary>
        public string Name;
        /// <summary>Resolved asset path, when the image maps directly to a file.</summary>
        public string AssetPath;
        /// <summary>
        /// Set when the image is not a plain file — a solid colour, a movie, or
        /// generated text defined by an `image` statement.
        /// </summary>
        public Displayable Displayable;
        public int ZOrder;
        /// <summary>Transform state produced by ATL and `at` clauses.</summary>
        public TransformState Transform = new TransformState();
        /// <summary>Timed animation from the statement's ATL block, if it has one.</summary>
        public AtlTimeline Timeline;
        /// <summary>Set when the image is an animation rather than a single frame.</summary>
        public List<string> Frames;
        public float FrameDelay = 0.05f;
    }

    /// <summary>The subset of Ren'Py transform properties the player animates.</summary>
    public sealed class TransformState
    {
        public float XPos, YPos;
        public float XAnchor, YAnchor;
        public float XAlign = -1f, YAlign = -1f;
        public float XZoom = 1f, YZoom = 1f, Zoom = 1f;
        public float Rotate;
        public float Alpha = 1f;
        public bool HasAlign;

        public TransformState Clone() { return (TransformState)MemberwiseClone(); }
    }

    /// <summary>How a character's dialogue should be presented.</summary>
    public sealed class SpeakerStyle
    {
        public string Name;
        public string NameColor;
        public string WhatColor;
        /// <summary>Ren'Py `kind` for narration/NVL variants.</summary>
        public string Kind;
        public string WhoFont;
        public string WhatFont;
    }

    /// <summary>
    /// Everything the engine needs from its environment. Implemented once for Unity
    /// and once for headless testing.
    ///
    /// The engine runs on its own thread and calls these synchronously; blocking
    /// methods return only when the corresponding interaction has finished.
    /// </summary>
    public interface IRenPyHost
    {
        // ------------------------------------------------------------ assets

        /// <summary>True when a game-relative path (e.g. "images/bg.png") exists.</summary>
        bool FileExists(string path);

        byte[] ReadFile(string path);

        /// <summary>All game-relative file paths, used to resolve image names.</summary>
        IEnumerable<string> ListFiles();

        // ------------------------------------------------------------ display

        /// <summary>Shows or replaces the image occupying a tag on a layer.</summary>
        void Show(ShownImage image);

        void Hide(string layer, string tag);

        void ClearLayer(string layer);

        /// <summary>Applies a transition to a layer's pending scene change.</summary>
        void Transition(string layer, string name, float duration);

        /// <summary>Pushes updated transform state for an already-shown image.</summary>
        void UpdateTransform(string layer, string tag, TransformState state);

        // ------------------------------------------------------------ interaction

        /// <summary>
        /// Displays a line and blocks until the player advances. `autoAdvance` is the
        /// delay requested by a {p=...} tag, or a negative value to wait for input.
        /// </summary>
        void Say(SpeakerStyle who, string what, float autoAdvance, bool noWait);

        /// <summary>Presents choices and blocks until one is selected.</summary>
        int Menu(string caption, List<string> choices);

        /// <summary>Blocks for a fixed time, or until the player clicks if `hard` is false.</summary>
        void Pause(float seconds, bool hard);

        /// <summary>Plays a fullscreen video and blocks until it ends or is skipped.</summary>
        void MovieCutscene(string path);

        // ------------------------------------------------------------ audio

        void PlayAudio(string channel, AudioSpec spec, float fadeIn, bool loop);
        void QueueAudio(string channel, AudioSpec spec);
        void StopAudio(string channel, float fadeOut);
        void SetAudioPaused(string channel, bool paused);
        void SetAudioVolume(string channel, float volume);

        /// <summary>
        /// Playback position on a channel in seconds, or a negative value when
        /// nothing is playing. Games use this to resynchronise voice tracks, so it
        /// must report the real position within the file.
        /// </summary>
        float GetAudioPosition(string channel);

        // ------------------------------------------------------------ screens

        /// <summary>The set of screens to draw has changed.</summary>
        void ScreensChanged(List<ShownScreen> screens);

        /// <summary>
        /// Blocks until the player activates something on a screen. Returns false
        /// when the host is shutting down.
        /// </summary>
        bool WaitForScreenAction();

        // ------------------------------------------------------------ misc

        /// <summary>Seconds since the game started.</summary>
        float Time { get; }

        void Log(string message);

        /// <summary>True when the engine should stop, e.g. the player quit.</summary>
        bool ShouldStop { get; }
    }
}
