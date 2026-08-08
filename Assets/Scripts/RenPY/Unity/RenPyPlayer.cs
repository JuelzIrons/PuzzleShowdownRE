using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using RenPy.Runtime;

namespace RenPy.Unity
{
    /// <summary>
    /// Plays a Ren'Py game inside Unity.
    ///
    /// Drop this component into any scene, point <see cref="GamePath"/> at an
    /// unzipped Ren'Py game under StreamingAssets, and press play. Nothing else in
    /// the scene is required — the canvas, audio channels and video surface are all
    /// created at runtime.
    ///
    /// The script itself runs on a background thread so Ren'Py's synchronous
    /// semantics survive intact; everything touching Unity is marshalled back here.
    /// </summary>
    [AddComponentMenu("Ren'Py/Ren'Py Player")]
    public class RenPyPlayer : MonoBehaviour
    {
        [Header("Game")]
        [Tooltip("Folder under StreamingAssets holding the unzipped Ren'Py game " +
                 "(the folder that contains 'game'). A '<name>.zip' beside it is also accepted.")]
        public string GamePath = "RenPyGame";

        [Tooltip("Label to begin at. Ren'Py games normally start at 'start'.")]
        public string StartLabel = "start";

        [Tooltip("Begin playing as soon as the scene loads.")]
        public bool AutoStart = true;

        [Tooltip("Follow Ren'Py's boot sequence: splashscreen, then the main menu. " +
                 "Turn off to jump straight to Start Label.")]
        public bool BootThroughMenu = true;

        [Tooltip("Use the hand-built title screen when the loaded game is recognised " +
                 "(currently Class of '09: Flip Side). Falls back to the generic screen " +
                 "renderer for every other game.")]
        public bool UseCustomTitleScreen = true;

        [Tooltip("Scale of the title logo. The source art is 5500x3000, far wider than the " +
                 "screen, so it is drawn well below native size.")]
        [Range(0.02f, 1f)]
        public float TitleLogoScale = 0.125f;

        [Header("Presentation")]
        [Tooltip("Characters per second for the typewriter effect. 0 shows lines instantly.")]
        public float TextSpeed = 45f;

        [Tooltip("Seconds a line stays up when the script asked to auto-advance and gave no time.")]
        public float DefaultAutoAdvance = 2f;

        [Tooltip("Let a click skip a timed wait.")]
        public bool ClickSkipsWaits = true;

        [Tooltip("Also let a click skip pauses the script marked 'hard'. Ren'Py itself " +
                 "blocks these (the 48s intro is one), so this trades fidelity for patience.")]
        public bool ClickSkipsHardPauses;

        [Tooltip("Channels silenced when a line is skipped, comma separated. Ren'Py stops " +
                 "its 'voice' channel at each interaction; games often use their own name too.")]
        public string VoiceChannels = "voice,vo";

        [Tooltip("Never draw the full-screen black curtain some games put on the " +
                 "screens layer (screens/black). Shorthand for adding it to Suppressed Images.")]
        public bool BlockScreensBlack;

        [Tooltip("Images never drawn, as 'layer/tag' entries separated by commas " +
                 "(e.g. screens/black). An escape hatch for artwork that misbehaves.")]
        public string SuppressedImages = "";

        [Header("Diagnostics")]
        [Tooltip("Log script warnings, unresolved images and Python errors to the console.")]
        public bool VerboseLogging;

        [Tooltip("Canvas sorting order. Raise it above any UI the host scene already draws.")]
        public int SortingOrder = 32000;

        /// <summary>Raised once the script has been loaded and init has run.</summary>
        public event Action Started;

        /// <summary>Raised when the script returns or the player is stopped.</summary>
        public event Action Finished;

        public RenPyUI UI { get; private set; }
        public RenPyAudio Audio { get; private set; }
        public RenPyEngine Engine { get; private set; }
        public RenPyFileSystem Files { get; private set; }
        public RenPyMovies Movies { get; private set; }
        public RenPyScreenView ScreenView { get; private set; }
        public FlipSideTitleScreen TitleScreen { get; private set; }
        public IRenPyPauseMenu GameMenu { get; private set; }

        UnityRenPyHost host;
        Thread scriptThread;

        // ------------------------------------------------------------ interaction

        enum Waiting { None, Say, Menu, Pause, Movie, Transition }

        Waiting waiting = Waiting.None;
        float waitDeadline;
        bool waitSkippable;

        // Typewriter state
        string pendingLine = "";
        float revealed;
        bool lineComplete = true;

        // Video
        VideoPlayer video;
        RawImage videoSurface;
        RenderTexture videoTexture;

        readonly Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();
        readonly List<string> textureOrder = new List<string>();

        [Tooltip("How many decoded images to keep in memory.")]
        public int MaxCachedTextures = 90;

        string loadingMessage = "";
        bool loading = true;

        // ---------------------------------------------------------------- lifecycle

        void Awake()
        {
            EnsureEventSystem();
            EnsureCameraAndListener();

            Files = new RenPyFileSystem(GamePath);
            Audio = new RenPyAudio(this, Files);
            Movies = new RenPyMovies(this, Files);

            UI = new RenPyUI(transform, LoadTexture, ResolveMovie, Movies.Release, SortingOrder);
            ApplySuppression();

            ScreenView = new RenPyScreenView(UI.Root, LoadTexture, QueueScreenAction, PlayUiSound,
                                             ResolveMovie, key => Movies.Release(key));
            // The real menu is built once the game is known, in Boot().
            UI.SetAdvanceHandler(OnAdvanceClicked);

            BuildVideoSurface();
        }

        void Start()
        {
            if (AutoStart) StartCoroutine(Boot());
        }

        /// <summary>Loads the game and begins playing. Safe to call manually when AutoStart is off.</summary>
        public IEnumerator Boot()
        {
            loading = true;
            loadingMessage = "Loading game...";
            UI.ShowDialogue(null, loadingMessage);

            yield return Files.Prepare();

            if (!Files.Ready)
            {
                loading = false;
                string message = Files.Error ?? "Could not load the game.";
                Debug.LogError("[RenPy] " + message);
                UI.ShowDialogue(null, message);
                yield break;
            }

            host = new UnityRenPyHost(this, Files);
            Engine = new RenPyEngine(host);

            var scripts = Files.ReadScripts();
            if (scripts.Count == 0)
            {
                loading = false;
                UI.ShowDialogue(null, "No .rpyc script files found in " + Files.GamePath);
                yield break;
            }

            loadingMessage = "Parsing script...";
            UI.ShowDialogue(null, loadingMessage);
            yield return null;

            // Loading and init are heavy; run them off the main thread so the frame
            // keeps ticking and the loading text stays visible.
            bool ready = false;
            Exception failure = null;

            // Restore saved persistent data before init, so `default` statements and
            // menu conditions see the player's real progress.
            RenPyPersistence.Load(Engine.Persistent, GamePath);

            var loader = new Thread(() =>
            {
                try
                {
                    Engine.LoadScripts(scripts);
                    Engine.RunInit();
                }
                catch (Exception e) { failure = e; }
                finally { ready = true; }
            });
            loader.IsBackground = true;
            loader.Start();

            while (!ready)
            {
                host.Pump();
                yield return null;
            }

            if (failure != null)
            {
                loading = false;
                Debug.LogException(failure);
                UI.ShowDialogue(null, "Failed to start: " + failure.Message);
                yield break;
            }

            loading = false;
            UI.HideDialogue();

            ApplyGameResolution();

            Debug.Log("[RenPy] loaded " + Engine.Script.Labels.Count + " labels, " +
                      Engine.Script.ImageFiles.Count + " images from " + Files.GamePath);

            if (Started != null) Started();

            // A game-specific menu uses that game's own art; anything else gets the
            // plain one, so saving is always reachable.
            GameMenu = (UseCustomTitleScreen && FlipSidePauseMenu.Matches(Files))
                ? (IRenPyPauseMenu)new FlipSidePauseMenu(this, UI.Root)
                : new RenPyGameMenu(this, UI.Root);
            GameMenu.SetOpenButtonVisible(false);

            if (UseCustomTitleScreen && FlipSideTitleScreen.Matches(Files))
            {
                // The bespoke title screen owns the menu; the engine only runs once a
                // choice is made, so no script thread starts yet.
                TitleScreen = new FlipSideTitleScreen(this, UI.Root);
                TitleScreen.OnNewGame = () => { pendingLoad = null; StartScriptThread(); };
                TitleScreen.OnLoadSlot = slot => LoadFromSlot(slot);
                TitleScreen.Show();
                yield break;
            }

            StartScriptThread();
        }

        void StartScriptThread()
        {
            if (scriptThread != null && scriptThread.IsAlive) return;

            // A menu click's sound would otherwise keep playing over the opening scene.
            Audio.StopNow("sound");

            if (host != null) host.ClearAbort();
            if (GameMenu != null) { GameMenu.Close(); GameMenu.SetOpenButtonVisible(true); }

            scriptThread = new Thread(RunScript) { IsBackground = true, Name = "RenPy Script" };
            scriptThread.Start();
        }

        void RunScript()
        {
            try
            {
                var resume = pendingLoad;
                pendingLoad = null;

                if (resume != null) { Engine.RestoreAndRun(resume); }
                else if (TitleScreen != null) { Engine.Run(StartLabel); }
                else if (BootThroughMenu) { Engine.Boot(StartLabel); }
                else { Engine.Run(StartLabel); }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                // Returning to the title is how Ren'Py ends a route.
                if (TitleScreen != null && !host.ShouldStop) ReturnToTitleRequested = true;
            }
        }

        /// <summary>Set by the script thread; acted on next frame by the main thread.</summary>
        volatile bool ReturnToTitleRequested;

        void OnDestroy() { Shutdown(); }
        void OnApplicationQuit() { Shutdown(); }

        void OnApplicationPause(bool paused)
        {
            // Mobile can kill the process without another callback, so persist here.
            if (paused) SavePersistent();
        }

        /// <summary>Writes the persistent store to PlayerPrefs.</summary>
        public void SavePersistent()
        {
            if (Engine == null) return;

            try { RenPyPersistence.Save(Engine.Persistent, GamePath); }
            catch (Exception e) { Debug.LogWarning("[RenPy] could not save persistent data: " + e.Message); }
        }

        void Shutdown()
        {
            if (host != null) host.Stop();

            if (scriptThread != null && scriptThread.IsAlive)
            {
                // The engine checks ShouldStop between statements; give it a moment.
                if (!scriptThread.Join(500)) scriptThread.Interrupt();
                scriptThread = null;
            }

            SavePersistent();

            if (Audio != null) Audio.StopAll();
            if (Movies != null) Movies.ReleaseAll();
            if (videoTexture != null) { videoTexture.Release(); videoTexture = null; }

            if (Finished != null) Finished();
        }

        // ---------------------------------------------------------------- update

        void Update()
        {
            if (host == null) return;

            if (BlockScreensBlack != lastBlockScreensBlack || SuppressedImages != lastSuppressedImages)
                ApplySuppression();

            if (GameMenu != null) GameMenu.Tick();

            if (ReturnToTitleRequested)
            {
                ReturnToTitleRequested = false;

                if (pendingResume != null)
                {
                    var resume = pendingResume;
                    pendingResume = null;
                    ResumeFromSave(resume);
                }
                else ReturnToTitle();
            }

            host.SamplePositions(Audio);
            host.Pump();
            UI.Tick(Time.unscaledDeltaTime);
            TickTypewriter();
            TickWait();
        }

        void TickTypewriter()
        {
            if (waiting != Waiting.Say || lineComplete) return;

            if (TextSpeed <= 0f)
            {
                CompleteLine();
                return;
            }

            revealed += Time.unscaledDeltaTime * TextSpeed;

            int total = VisibleLength(pendingLine);
            if (revealed >= total) { CompleteLine(); return; }

            UI.SetVisibleCharacters((int)revealed);
        }

        void CompleteLine()
        {
            lineComplete = true;
            UI.SetVisibleCharacters(int.MaxValue);
        }

        /// <summary>Counts characters excluding rich-text markup.</summary>
        static int VisibleLength(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;

            int count = 0;
            bool inTag = false;
            foreach (char c in text)
            {
                if (c == '<') { inTag = true; continue; }
                if (c == '>') { inTag = false; continue; }
                if (!inTag) count++;
            }
            return count;
        }

        void TickWait()
        {
            if (waiting == Waiting.None || waiting == Waiting.Menu) return;

            if (waiting == Waiting.Transition)
            {
                float remaining = waitDeadline - Time.unscaledTime;
                float duration = Mathf.Max(0.0001f, transitionDuration);
                float t = 1f - Mathf.Clamp01(remaining / duration);
                UI.SetTransitionProgress(transitionName, t);

                if (remaining <= 0f)
                {
                    UI.SetTransitionProgress(transitionName, 1f);
                    Finish(0);
                }
                return;
            }

            if (waiting == Waiting.Movie)
            {
                if (video == null || !video.isPlaying) Finish(0);
                return;
            }

            if (waitDeadline > 0f && Time.unscaledTime >= waitDeadline) Finish(0);
        }

        // ---------------------------------------------------------------- input

        void OnAdvanceClicked()
        {
            switch (waiting)
            {
                case Waiting.Say:
                    // First click completes the typewriter and cuts the line's audio;
                    // a second click moves on.
                    if (!lineComplete)
                    {
                        CompleteLine();
                        StopVoice();
                        return;
                    }
                    StopVoice();
                    Finish(0);
                    return;

                case Waiting.Pause:
                    if (waitSkippable) Finish(0);
                    return;

                case Waiting.Movie:
                    if (video != null) video.Stop();
                    Finish(0);
                    return;

                case Waiting.Transition:
                    if (ClickSkipsWaits)
                    {
                        UI.SetTransitionProgress(transitionName, 1f);
                        Finish(0);
                    }
                    return;
            }
        }

        /// <summary>Silences the channels carrying spoken dialogue.</summary>
        void StopVoice()
        {
            if (Audio == null || string.IsNullOrEmpty(VoiceChannels)) return;

            foreach (var name in VoiceChannels.Split(','))
            {
                string channel = name.Trim();
                if (channel.Length > 0) Audio.StopNow(channel);
            }
        }

        /// <summary>Releases the engine thread and clears the interaction state.</summary>
        void Finish(int result)
        {
            if (waiting == Waiting.None) return;
            waiting = Waiting.None;
            waitDeadline = 0f;
            host.CompleteInteraction(result);
        }

        // ---------------------------------------------------------------- interactions

        internal void BeginSay(SpeakerStyle who, string what, float autoAdvance, bool noWait)
        {
            UI.DiscardGhosts();
            UI.ClearChoices();
            UI.ShowDialogue(who, what);

            pendingLine = what ?? "";
            revealed = 0f;
            lineComplete = TextSpeed <= 0f;
            UI.SetVisibleCharacters(lineComplete ? int.MaxValue : 0);

            waiting = Waiting.Say;
            waitSkippable = true;

            if (autoAdvance >= 0f)
            {
                // A timed line still types out; the timer starts now, as Ren'Py does.
                waitDeadline = Time.unscaledTime + Mathf.Max(0.01f, autoAdvance);
            }
            else if (noWait)
            {
                // {nw} with no pause continues immediately once the text is shown.
                waitDeadline = Time.unscaledTime + 0.01f;
            }
            else
            {
                waitDeadline = 0f;
            }
        }

        internal void BeginMenu(string caption, List<string> choices)
        {
            UI.DiscardGhosts();
            waiting = Waiting.Menu;
            waitDeadline = 0f;
            UI.ShowChoices(caption, choices, index =>
            {
                UI.ClearChoices();
                Finish(index);
            });
        }

        internal void BeginPause(float seconds, bool hard)
        {
            UI.DiscardGhosts();
            waiting = Waiting.Pause;
            waitSkippable = ClickSkipsWaits && (!hard || ClickSkipsHardPauses);
            waitDeadline = seconds < 0f ? 0f : Time.unscaledTime + seconds;

            // A negative duration means "wait for the player".
            if (seconds < 0f) waitSkippable = true;
        }

        string transitionName = "dissolve";
        float transitionDuration = 0.5f;

        internal void BeginTransition(string name, float duration)
        {
            transitionName = name ?? "dissolve";
            transitionDuration = Mathf.Max(0.01f, duration);
            waiting = Waiting.Transition;
            waitDeadline = Time.unscaledTime + transitionDuration;
        }

        internal void BeginMovie(string path)
        {
            string absolute = Files.Absolute(path);

            if (absolute == null || !File.Exists(absolute))
            {
                Debug.LogWarning("[RenPy] movie not found: " + path);
                Finish(0);
                return;
            }

            waiting = Waiting.Movie;
            waitDeadline = 0f;

            videoSurface.gameObject.SetActive(true);
            video.url = "file://" + absolute;
            video.Play();
        }

        // ---------------------------------------------------------------- screens

        public bool IsScreenShowing(string name)
        {
            return ScreenView != null && ScreenView.IsShowing(name);
        }

        /// <summary>
        /// Pushes the suppression settings into the UI. Combines the explicit list
        /// with the screens/black shorthand.
        /// </summary>
        void ApplySuppression()
        {
            string combined = SuppressedImages ?? "";

            if (BlockScreensBlack)
                combined = combined.Length > 0 ? combined + ",screens/black" : "screens/black";

            UI.SetSuppressed(combined);

            lastBlockScreensBlack = BlockScreensBlack;
            lastSuppressedImages = SuppressedImages;
        }

        bool lastBlockScreensBlack;
        string lastSuppressedImages;

        /// <summary>
        /// Ren'Py art is authored against config.screen_width/height. Scale it to our
        /// 1920x1080 design space so games built at 1280x720 are not shown at a third
        /// of the screen.
        /// </summary>
        void ApplyGameResolution()
        {
            var config = Engine.Interp.GetStore("store.config");

            float width = ToFloat(config.GetStr("screen_width", null), RenPyUI.DesignWidth);
            float height = ToFloat(config.GetStr("screen_height", null), RenPyUI.DesignHeight);

            if (width <= 0f || height <= 0f) return;

            // Uniform scale keeps the aspect ratio the artist worked in.
            UI.ImageScale = Mathf.Min(RenPyUI.DesignWidth / width, RenPyUI.DesignHeight / height);

            if (Mathf.Abs(UI.ImageScale - 1f) > 0.001f)
                Debug.Log("[RenPy] game authored at " + width + "x" + height +
                          "; scaling art by " + UI.ImageScale.ToString("0.###"));
        }

        static float ToFloat(object value, float dflt)
        {
            if (value == null) return dflt;
            try { return (float)RenPy.Python.Py.ToDouble(value); }
            catch (Exception) { return dflt; }
        }

        /// <summary>Loads a texture through the player's cache.</summary>
        public Texture2D LoadTextureFor(string relativePath) { return LoadTexture(relativePath); }

        /// <summary>Plays a UI sound effect.</summary>
        public void PlayInterfaceSound(string path) { PlayUiSound(path); }

        /// <summary>Ends the session.</summary>
        public void QuitGame()
        {
            SavePersistent();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        // ---------------------------------------------------------------- save/load

        /// <summary>Writes the running game into a slot.</summary>
        public bool SaveToSlot(int slot)
        {
            if (Engine == null || !Engine.InGame)
            {
                Debug.LogWarning("[RenPy] nothing to save; the game is not running.");
                return false;
            }

            // The engine thread is parked on an interaction, so its state is settled.
            var state = Engine.CaptureState();
            bool ok = RenPySaveSystem.Save(GamePath, slot, state);

            if (ok) SavePersistent();
            return ok;
        }

        /// <summary>Loads a slot and resumes the game from it.</summary>
        public bool LoadFromSlot(int slot)
        {
            var state = RenPySaveSystem.Load(GamePath, slot);
            if (state == null)
            {
                Debug.LogWarning("[RenPy] save slot " + slot + " is empty or unreadable.");
                return false;
            }

            pendingLoad = state;
            StartScriptThread();
            return true;
        }

        RenPySaveState pendingLoad;

        /// <summary>Loads a slot while the game is already running.</summary>
        public void LoadFromSlotInGame(int slot)
        {
            var state = RenPySaveSystem.Load(GamePath, slot);
            if (state == null)
            {
                Debug.LogWarning("[RenPy] save slot " + slot + " is empty or unreadable.");
                return;
            }

            // Unwind the running script first, then resume from the save.
            pendingResume = state;
            AbortRunningScript();
        }

        /// <summary>Stops the script and shows the title screen again.</summary>
        public void AbortToTitle()
        {
            pendingResume = null;
            AbortRunningScript();
        }

        RenPySaveState pendingResume;

        void AbortRunningScript()
        {
            if (host == null) return;

            if (scriptThread == null || !scriptThread.IsAlive)
            {
                // Nothing running; act on the request immediately.
                ReturnToTitleRequested = true;
                return;
            }

            host.RequestAbort();
        }

        /// <summary>Queues a screen action for the engine thread and wakes it.</summary>
        void QueueScreenAction(object action)
        {
            if (Engine == null || host == null) return;
            Engine.Screens.Enqueue(action);
            host.NotifyScreenAction();

            // A screen click also settles whatever interaction is waiting.
            if (waiting != Waiting.None && waiting != Waiting.Menu) Finish(0);
        }

        /// <summary>Restarts the script thread from a loaded save.</summary>
        void ResumeFromSave(RenPySaveState state)
        {
            scriptThread = null;

            Audio.StopAll();
            if (ScreenView != null) ScreenView.ReleaseMovies();
            Movies.ReleaseAll();
            UI.HideDialogue();
            UI.ClearChoices();
            UI.DiscardGhosts();

            pendingLoad = state;
            StartScriptThread();
        }

        /// <summary>Clears the stage and brings the title screen back up.</summary>
        public void ReturnToTitle()
        {
            if (TitleScreen == null) return;

            scriptThread = null;

            Audio.StopAll();
            if (ScreenView != null) ScreenView.ReleaseMovies();
            Movies.ReleaseAll();
            UI.HideDialogue();
            UI.ClearChoices();
            UI.DiscardGhosts();

            foreach (var layer in new[] { "master", "transient", "screens", "overlay" })
                UI.ClearLayer(layer);

            SavePersistent();

            if (GameMenu != null) { GameMenu.Close(); GameMenu.SetOpenButtonVisible(false); }

            TitleScreen.Show();
        }

        void PlayUiSound(string path)
        {
            if (!string.IsNullOrEmpty(path)) Audio.Play("sound", AudioSpec.Parse(path), 0f, false);
        }

        internal void RebuildScreens(List<ShownScreen> screens)
        {
            if (ScreenView != null) ScreenView.Rebuild(screens);
        }

        /// <summary>Starts (or reuses) the video backing a `show`n Movie displayable.</summary>
        Texture ResolveMovie(string key, string path, bool loop)
        {
            return Movies.Resolve(key, path, loop);
        }

        // ---------------------------------------------------------------- textures

        Texture2D LoadTexture(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return null;

            Texture2D cached;
            if (textureCache.TryGetValue(relativePath, out cached) && cached != null)
            {
                textureOrder.Remove(relativePath);
                textureOrder.Add(relativePath);
                return cached;
            }

            byte[] data = Files.Read(relativePath);
            if (data == null)
            {
                if (VerboseLogging) Debug.LogWarning("[RenPy] image not found: " + relativePath);
                return null;
            }

            var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            if (!texture.LoadImage(data, false))
            {
                Destroy(texture);
                Debug.LogWarning("[RenPy] could not decode image: " + relativePath);
                return null;
            }

            texture.name = relativePath;
            texture.wrapMode = TextureWrapMode.Clamp;

            textureCache[relativePath] = texture;
            textureOrder.Add(relativePath);
            TrimTextureCache();

            return texture;
        }

        void TrimTextureCache()
        {
            while (textureOrder.Count > MaxCachedTextures)
            {
                string oldest = textureOrder[0];
                textureOrder.RemoveAt(0);

                Texture2D victim;
                if (!textureCache.TryGetValue(oldest, out victim)) continue;

                // Keep anything still on screen.
                if (UI.IsTextureInUse(victim)) { textureOrder.Add(oldest); continue; }

                textureCache.Remove(oldest);
                if (victim != null) Destroy(victim);
            }
        }

        // ---------------------------------------------------------------- setup

        void BuildVideoSurface()
        {
            var holder = new GameObject("Video", typeof(RectTransform));
            holder.transform.SetParent(UI.Root, false);

            var rect = (RectTransform)holder.transform;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            videoSurface = holder.AddComponent<RawImage>();
            videoSurface.color = Color.white;
            videoSurface.raycastTarget = false;

            videoTexture = new RenderTexture(1920, 1080, 0);
            videoSurface.texture = videoTexture;

            video = gameObject.AddComponent<VideoPlayer>();
            video.playOnAwake = false;
            video.renderMode = VideoRenderMode.RenderTexture;
            video.targetTexture = videoTexture;
            video.audioOutputMode = VideoAudioOutputMode.Direct;
            video.source = VideoSource.Url;
            video.loopPointReached += _ => OnMovieEnded();

            holder.SetActive(false);
        }

        void OnMovieEnded()
        {
            videoSurface.gameObject.SetActive(false);
            if (waiting == Waiting.Movie) Finish(0);
        }

        /// <summary>
        /// uGUI needs an EventSystem for clicks. Create one if the scene has none,
        /// matching whichever input backend the project is configured for.
        /// </summary>
        static void EnsureEventSystem()
        {
            if (EventSystem.current != null) return;
            if (Find<EventSystem>() != null) return;

            var go = new GameObject("EventSystem", typeof(EventSystem));

#if ENABLE_INPUT_SYSTEM
            go.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
#elif ENABLE_LEGACY_INPUT_MANAGER
            go.AddComponent<StandaloneInputModule>();
#endif
        }

        /// <summary>
        /// An empty scene has neither a camera nor an audio listener. The overlay
        /// canvas draws without a camera, but Unity reports "No cameras rendering",
        /// and — the part that actually matters — AudioSources are silent unless a
        /// listener exists somewhere in the scene.
        /// </summary>
        static void EnsureCameraAndListener()
        {
            var camera = Camera.main ?? Find<Camera>();

            if (camera == null)
            {
                var go = new GameObject("RenPy Camera");
                camera = go.AddComponent<Camera>();
                camera.clearFlags = CameraClearFlags.SolidColor;
                camera.backgroundColor = Color.black;
                camera.orthographic = true;
            }

            if (Find<AudioListener>() == null)
                camera.gameObject.AddComponent<AudioListener>();
        }

        static T Find<T>() where T : UnityEngine.Object
        {
#if UNITY_2023_1_OR_NEWER
            return FindFirstObjectByType<T>(FindObjectsInactive.Include);
#else
            return FindObjectOfType<T>();
#endif
        }
    }
}
