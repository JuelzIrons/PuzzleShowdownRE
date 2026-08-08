using System;
using System.Collections.Generic;
using System.Globalization;
using RenPy.Ast;
using RenPy.Python;

namespace RenPy.Runtime
{
    /// <summary>A displayable value: an image, solid colour, text, or composite.</summary>
    public class Displayable : IPyCallable, IPyAttrs
    {
        public string Kind = "image";
        /// <summary>Asset path for image displayables.</summary>
        public string Path;
        public string Text;
        public string Color;
        public readonly PyDict Properties = new PyDict();
        public readonly List<object> Children = new List<object>();

        public object Call(object[] args, PyDict kwargs) { return this; }

        public bool TryGetAttr(string name, out object value)
        {
            if (name == "path") { value = Path; return true; }
            if (name == "text") { value = Text; return true; }
            return Properties.TryGet(name, out value);
        }

        public void SetAttr(string name, object value) { Properties.Set(name, value); }

        public override string ToString() { return "<" + Kind + (Path != null ? " " + Path : "") + ">"; }
    }

    /// <summary>
    /// A Ren'Py Character. Calling it speaks a line, which is how `e "hello"` and
    /// direct calls from Python both work.
    /// </summary>
    public class RenPyCharacter : IPyCallable, IPyAttrs
    {
        public string Name;
        public readonly PyDict Properties = new PyDict();
        public RenPyEngine Engine;
        /// <summary>Ren'Py `callback`, invoked around each line.</summary>
        public object Callback;
        /// <summary>Image tag this character drives via `show` side images.</summary>
        public string ImageTag;

        public object Call(object[] args, PyDict kwargs)
        {
            string what = args.Length > 0 ? PyOps.ToStr(args[0]) : "";
            Engine.Api.SayAs(this, what);
            return null;
        }

        public bool TryGetAttr(string name, out object value)
        {
            switch (name)
            {
                case "name": value = Name; return true;
                case "image_tag": value = ImageTag; return true;
                case "callback": value = Callback; return true;
            }
            return Properties.TryGet(name, out value);
        }

        public void SetAttr(string name, object value)
        {
            if (name == "name") { Name = PyOps.ToStr(value); return; }
            if (name == "callback") { Callback = value; return; }
            Properties.Set(name, value);
        }

        public SpeakerStyle ToStyle()
        {
            return new SpeakerStyle
            {
                Name = Name,
                NameColor = StringProp("who_color"),
                WhatColor = StringProp("what_color"),
                WhoFont = StringProp("who_font"),
                WhatFont = StringProp("what_font"),
                Kind = StringProp("kind"),
            };
        }

        string StringProp(string key)
        {
            object v;
            return Properties.TryGet(key, out v) && v != null ? PyOps.ToStr(v) : null;
        }

        public override string ToString() { return "<Character " + Name + ">"; }
    }

    /// <summary>
    /// Installs the Ren'Py runtime API into the interpreter: the `renpy` module,
    /// the `config`/`gui`/`persistent`/`preferences` stores, displayable and
    /// transition constructors, and the built-in position names.
    /// </summary>
    public class RenPyApi
    {
        readonly RenPyEngine engine;
        IRenPyHost Host { get { return engine.Host; } }
        PyInterp Interp { get { return engine.Interp; } }

        readonly PyDict renpyModule = new PyDict();

        public RenPyApi(RenPyEngine engine) { this.engine = engine; }

        // ---------------------------------------------------------------- install

        public void Install()
        {
            var store = Interp.GetStore("store");

            InstallRenpyModule();
            InstallStores();
            InstallDisplayables(store);
            InstallPositions(store);
            InstallTransitions(store);
            InstallWarpers(store);

            store.Set("Character", new PyNative("Character", MakeCharacter));
            store.Set("DynamicCharacter", new PyNative("DynamicCharacter", MakeCharacter));

            // Ren'Py exposes the store itself so `store.x` works from anywhere.
            store.Set("store", new PyNamespace("store", store));

            Interp.Builtins.Set("renpy", new PyNamespace("renpy", renpyModule));
            Interp.Modules["renpy"] = new PyNamespace("renpy", renpyModule);
        }

        void Add(PyDict target, string name, Func<object[], PyDict, object> fn)
        {
            target.Set(name, new PyNative(name, fn));
        }

        static object Arg(object[] a, int i, object dflt = null) { return i < a.Length ? a[i] : dflt; }

        static object Kw(PyDict k, string name, object dflt)
        {
            if (k == null) return dflt;
            object v;
            return k.TryGet(name, out v) ? v : dflt;
        }

        static float Flt(object o, float dflt = 0f)
        {
            if (o == null) return dflt;
            try { return (float)Py.ToDouble(o); }
            catch (PyError) { return dflt; }
        }

        static string Str(object o) { return o == null ? null : PyOps.ToStr(o); }

        // ---------------------------------------------------------------- renpy.*

        void InstallRenpyModule()
        {
            var r = renpyModule;

            // ------------------------------------------------ dialogue and flow

            Add(r, "say", (a, k) => { Say(Arg(a, 0), Str(Arg(a, 1))); return null; });

            Add(r, "pause", (a, k) =>
            {
                float seconds = Flt(Arg(a, 0), -1f);
                bool hard = Py.Truthy(Kw(k, "hard", false));
                Host.Pause(seconds, hard);
                return false;
            });

            Add(r, "jump", (a, k) => { throw new JumpSignal(Str(Arg(a, 0))); });
            Add(r, "call", (a, k) => { throw new CallSignal(Str(Arg(a, 0))); });
            Add(r, "return_statement", (a, k) => { throw new ReturnSignal(Arg(a, 0)); });
            Add(r, "quit", (a, k) => { throw new QuitSignal(); });
            Add(r, "full_restart", (a, k) => { throw new JumpSignal("start"); });

            // Rollback bookkeeping has no effect here, but scripts call it constantly.
            Add(r, "checkpoint", (a, k) => Arg(a, 0));
            Add(r, "block_rollback", (a, k) => null);
            Add(r, "retain_after_load", (a, k) => null);
            Add(r, "fix_rollback", (a, k) => null);
            Add(r, "rollback", (a, k) => null);

            Add(r, "has_label", (a, k) => engine.Script.FindLabel(Str(Arg(a, 0))) != null);
            Add(r, "seen_label", (a, k) => false);
            Add(r, "get_all_labels", (a, k) => new PyList(new List<object>(engine.Script.Labels.Keys)));

            // ------------------------------------------------ scene management

            Add(r, "show", (a, k) => { ShowByName(Str(Arg(a, 0)), k); return null; });

            Add(r, "hide", (a, k) =>
            {
                string tag = RenPyScript.Normalize(Str(Arg(a, 0)));
                string layer = Str(Kw(k, "layer", null)) ?? engine.DefaultLayer(tag);
                engine.Scene.Hide(layer, tag);
                Host.Hide(layer, tag);
                return null;
            });

            Add(r, "scene", (a, k) =>
            {
                string layer = Str(Arg(a, 0)) ?? "master";
                engine.Scene.ClearLayer(layer);
                Host.ClearLayer(layer);
                return null;
            });

            Add(r, "transition", (a, k) =>
            {
                var t = Arg(a, 0) as Transition;
                string layer = Str(Kw(k, "layer", null)) ?? "master";
                Host.Transition(layer, t != null ? t.Name : Str(Arg(a, 0)), t != null ? t.Duration : 0.5f);
                return null;
            });

            Add(r, "with_statement", (a, k) =>
            {
                var t = Arg(a, 0) as Transition;
                if (t != null) Host.Transition("master", t.Name, t.Duration);
                return null;
            });

            Add(r, "image", (a, k) =>
            {
                string name = NameOf(Arg(a, 0));
                string path = DisplayableToPath(Arg(a, 1));
                if (name != null && path != null) engine.Script.ImageFiles[RenPyScript.Normalize(name)] = path;
                return null;
            });

            Add(r, "get_showing_tags", (a, k) =>
            {
                string layer = Str(Arg(a, 0)) ?? "master";
                return new PySet(new List<object>(engine.Scene.Layer(layer).Keys));
            });

            Add(r, "showing", (a, k) =>
            {
                string tag = RenPyScript.Normalize(Str(Arg(a, 0)));
                return engine.Scene.LayerForTag(tag) != null;
            });

            // ------------------------------------------------ screens

            Add(r, "show_screen", (a, k) => { engine.Screens.Show(Str(Arg(a, 0)), k); return null; });
            Add(r, "hide_screen", (a, k) => { engine.Screens.Hide(Str(Arg(a, 0))); return null; });
            Add(r, "call_screen", (a, k) => { engine.Screens.Show(Str(Arg(a, 0)), k); return null; });
            Add(r, "get_screen", (a, k) => engine.Screens.IsShowing(Str(Arg(a, 0))) ? Str(Arg(a, 0)) : null);
            Add(r, "has_screen", (a, k) => engine.Script.FindScreen(Str(Arg(a, 0))) != null);
            Add(r, "start_predict_screen", (a, k) => null);
            Add(r, "stop_predict_screen", (a, k) => null);
            Add(r, "restart_interaction", (a, k) => null);
            Add(r, "end_interaction", (a, k) => null);

            // ------------------------------------------------ media

            Add(r, "movie_cutscene", (a, k) =>
            {
                Host.MovieCutscene(Str(Arg(a, 0)));
                return true;
            });

            Add(r, "play", (a, k) =>
            {
                string channel = Str(Kw(k, "channel", "sound"));
                Host.PlayAudio(channel, AudioSpec.Parse(Str(Arg(a, 0))), Flt(Kw(k, "fadein", 0f)), false);
                return null;
            });

            Add(r, "sound_playing", (a, k) => false);

            Add(r, "notify", (a, k) => { Host.Log("[notify] " + Str(Arg(a, 0))); return null; });

            // ------------------------------------------------ environment

            Add(r, "variant", (a, k) =>
            {
                // The player targets one configuration; report the touch/mobile variants
                // so games pick their phone layouts.
                string name = Str(Arg(a, 0));
                var wanted = Arg(a, 0) as PyList;
                if (wanted != null)
                {
                    foreach (var v in wanted.Items) if (IsVariant(Str(v))) return true;
                    return false;
                }
                return IsVariant(name);
            });

            Add(r, "get_screen_size", (a, k) => new PyTuple(new object[] { 1920L, 1080L }));
            Add(r, "version", (a, k) => "7.5.0");
            Add(r, "version_tuple", (a, k) => new PyTuple(new object[] { 7L, 5L, 0L }));
            Add(r, "android", (a, k) => false);

            r.Set("random", MakeRandomModule());

            Add(r, "input", (a, k) => "");
            Add(r, "invoke_in_new_context", (a, k) =>
                a.Length > 0 ? PyOps.CallObject(a[0], Slice(a, 1), k) : null);

            Add(r, "save", (a, k) => null);
            Add(r, "load", (a, k) => null);
            Add(r, "list_saved_games", (a, k) => new PyList());
            Add(r, "can_load", (a, k) => false);
            Add(r, "take_screenshot", (a, k) => null);

            Add(r, "log", (a, k) => { Host.Log("[renpy.log] " + Str(Arg(a, 0))); return null; });
            Add(r, "error", (a, k) => { Host.Log("[renpy.error] " + Str(Arg(a, 0))); return null; });

            Add(r, "get_game_runtime", (a, k) => (double)Host.Time);
            Add(r, "display_menu", (a, k) => null);

            // ------------------------------------------------ submodules

            renpyModule.Set("music", new PyNamespace("renpy.music", MakeAudioModule()));
            renpyModule.Set("sound", new PyNamespace("renpy.sound", MakeAudioModule()));
            renpyModule.Set("audio", new PyNamespace("renpy.audio", MakeAudioModule()));
            renpyModule.Set("loader", new PyNamespace("renpy.loader", MakeLoaderModule()));
            renpyModule.Set("store", new PyNamespace("store", Interp.GetStore("store")));
            renpyModule.Set("exports", new PyNamespace("renpy.exports", renpyModule));

            var config = new PyDict();
            renpyModule.Set("config", new PyNamespace("renpy.config", config));
        }

        static object[] Slice(object[] a, int from)
        {
            if (from >= a.Length) return Py.EmptyArgs;
            var rv = new object[a.Length - from];
            Array.Copy(a, from, rv, 0, rv.Length);
            return rv;
        }

        static bool IsVariant(string name)
        {
            switch (name)
            {
                case "pc":
                case "large":
                case "medium":
                    return true;
                default:
                    return false;
            }
        }

        object MakeRandomModule()
        {
            var d = new PyDict();
            var rng = new Random();

            Add(d, "random", (a, k) => rng.NextDouble());
            Add(d, "randint", (a, k) => (long)rng.Next((int)Py.ToInt(a[0]), (int)Py.ToInt(a[1]) + 1));
            Add(d, "choice", (a, k) =>
            {
                var items = new List<object>(PyOps.Iterate(a[0]));
                return items.Count == 0 ? null : items[rng.Next(items.Count)];
            });
            Add(d, "shuffle", (a, k) =>
            {
                var list = a[0] as PyList;
                if (list == null) return null;
                for (int i = list.Items.Count - 1; i > 0; i--)
                {
                    int j = rng.Next(i + 1);
                    var tmp = list.Items[i];
                    list.Items[i] = list.Items[j];
                    list.Items[j] = tmp;
                }
                return null;
            });
            Add(d, "seed", (a, k) => null);

            return new PyNamespace("renpy.random", d);
        }

        PyDict MakeAudioModule()
        {
            var d = new PyDict();

            Add(d, "play", (a, k) =>
            {
                string channel = Str(Kw(k, "channel", null)) ?? Str(Arg(a, 1)) ?? "music";
                bool loop = Py.Truthy(Kw(k, "loop", channel == "music" || channel == "ambient"));
                var spec = AudioSpec.Parse(FirstPath(Arg(a, 0)));
                NotePlaying(channel, spec.Path);
                Host.PlayAudio(channel, spec, Flt(Kw(k, "fadein", 0f)), loop);
                return null;
            });

            Add(d, "queue", (a, k) =>
            {
                string channel = Str(Kw(k, "channel", null)) ?? Str(Arg(a, 1)) ?? "music";
                Host.QueueAudio(channel, AudioSpec.Parse(FirstPath(Arg(a, 0))));
                return null;
            });

            Add(d, "stop", (a, k) =>
            {
                string channel = Str(Kw(k, "channel", null)) ?? Str(Arg(a, 0)) ?? "music";
                Host.StopAudio(channel, Flt(Kw(k, "fadeout", 0f)));
                return null;
            });

            Add(d, "set_pause", (a, k) =>
            {
                string channel = Str(Kw(k, "channel", null)) ?? Str(Arg(a, 1)) ?? "music";
                Host.SetAudioPaused(channel, Py.Truthy(Arg(a, 0)));
                return null;
            });

            Add(d, "set_volume", (a, k) =>
            {
                string channel = Str(Kw(k, "channel", null)) ?? Str(Arg(a, 1)) ?? "music";
                Host.SetAudioVolume(channel, Flt(Arg(a, 0), 1f));
                return null;
            });

            // Games resynchronise voice-over against this; returning None
            // unconditionally makes them restart the track on every line.
            Add(d, "get_pos", (a, k) =>
            {
                string channel = Str(Kw(k, "channel", null)) ?? Str(Arg(a, 0)) ?? "music";
                float pos = Host.GetAudioPosition(channel);
                return pos < 0f ? null : (object)(double)pos;
            });

            Add(d, "is_playing", (a, k) =>
            {
                string channel = Str(Kw(k, "channel", null)) ?? Str(Arg(a, 0)) ?? "music";
                return Host.GetAudioPosition(channel) >= 0f;
            });

            Add(d, "get_playing", (a, k) =>
            {
                string channel = Str(Kw(k, "channel", null)) ?? Str(Arg(a, 0)) ?? "music";
                return Host.GetAudioPosition(channel) >= 0f ? PlayingPath(channel) : null;
            });
            Add(d, "set_queue_empty_callback", (a, k) => null);
            Add(d, "register_channel", (a, k) => null);
            Add(d, "set_pan", (a, k) => null);
            Add(d, "set_mixer", (a, k) => null);

            return d;
        }

        readonly Dictionary<string, string> playingPaths = new Dictionary<string, string>();

        /// <summary>Remembers what each channel was last asked to play.</summary>
        public void NotePlaying(string channel, string path)
        {
            if (channel != null) playingPaths[channel] = path;
        }

        string PlayingPath(string channel)
        {
            string path;
            return playingPaths.TryGetValue(channel, out path) ? path : null;
        }

        /// <summary>Ren'Py accepts a path or a list of paths for audio.</summary>
        static string FirstPath(object value)
        {
            var list = value as PyList;
            if (list != null && list.Items.Count > 0) return PyOps.ToStr(list.Items[0]);

            var tuple = value as PyTuple;
            if (tuple != null && tuple.Items.Length > 0) return PyOps.ToStr(tuple.Items[0]);

            return value == null ? null : PyOps.ToStr(value);
        }

        PyDict MakeLoaderModule()
        {
            var d = new PyDict();
            Add(d, "loadable", (a, k) => Host.FileExists(Str(Arg(a, 0))));
            Add(d, "load", (a, k) => Host.ReadFile(Str(Arg(a, 0))));
            Add(d, "listdir", (a, k) => new PyList(new List<object>()));
            return d;
        }

        // ---------------------------------------------------------------- stores

        void InstallStores()
        {
            var store = Interp.GetStore("store");

            var config = Interp.GetStore("store.config");
            var gui = Interp.GetStore("store.gui");
            var persistent = Interp.GetStore("persistent");
            var preferences = Interp.GetStore("store.preferences");

            SeedConfig(config);
            SeedPreferences(preferences);

            // `gui.init(w, h)` is called by every generated gui.rpy.
            Add(gui, "init", (a, k) => null);

            // `build` configures distribution packaging and has no runtime effect,
            // but options.rpy calls into it during init.
            var build = new PyDict();
            foreach (var name in new[] { "classify", "documentation", "archive", "package", "executable" })
                Add(build, name, (a, k) => null);
            store.Set("build", new PyNamespace("build", build));

            store.Set("config", new PyNamespace("config", config));
            store.Set("gui", new PyNamespace("gui", gui));
            store.Set("persistent", new PersistentNamespace(persistent));
            store.Set("preferences", new PyNamespace("preferences", preferences));
            store.Set("_preferences", new PyNamespace("preferences", preferences));

            Interp.Modules["store"] = new PyNamespace("store", store);
            Interp.Modules["store.config"] = new PyNamespace("config", config);
            Interp.Modules["store.gui"] = new PyNamespace("gui", gui);

            // Standard library modules games commonly touch in init blocks.
            Interp.Modules["time"] = MakeTimeModule();
            Interp.Modules["math"] = MakeMathModule();
            Interp.Modules["random"] = MakeRandomModule();
            Interp.Modules["os"] = new PyNamespace("os", new PyDict());
            Interp.Modules["sys"] = new PyNamespace("sys", new PyDict());
        }

        void SeedConfig(PyDict config)
        {
            config.Set("name", "");
            config.Set("version", "");
            config.Set("screen_width", 1920L);
            config.Set("screen_height", 1080L);
            config.Set("developer", false);
            config.Set("rollback_enabled", true);
            config.Set("hard_rollback_limit", 100L);
            config.Set("default_text_cps", 0L);
            config.Set("auto_choice_delay", null);
            config.Set("keymap", new PyDict());
            config.Set("overlay_screens", new PyList());
            config.Set("after_load_callbacks", new PyList());
            config.Set("character_id_prefixes", new PyList());
            config.Set("start_callbacks", new PyList());
            config.Set("interact_callbacks", new PyList());
            config.Set("python_callbacks", new PyList());
            config.Set("enter_transition", null);
            config.Set("exit_transition", null);
            config.Set("window", "auto");
            config.Set("say_attribute_transition", null);
            config.Set("main_menu_music", null);
            config.Set("has_voice", true);
            config.Set("layers", new PyList(new List<object> { "master", "transient", "screens", "overlay" }));
        }

        void SeedPreferences(PyDict preferences)
        {
            preferences.Set("text_cps", 0L);
            preferences.Set("afm_time", 0L);
            preferences.Set("show_empty_window", true);
            preferences.Set("music_volume", 1.0);
            preferences.Set("sfx_volume", 1.0);
            preferences.Set("voice_volume", 1.0);
            preferences.Set("skip_unseen", false);
            preferences.Set("fullscreen", true);
        }

        object MakeTimeModule()
        {
            var d = new PyDict();
            Add(d, "time", (a, k) => (double)Host.Time);
            Add(d, "clock", (a, k) => (double)Host.Time);
            Add(d, "sleep", (a, k) => { Host.Pause(Flt(Arg(a, 0)), true); return null; });
            return new PyNamespace("time", d);
        }

        object MakeMathModule()
        {
            var d = new PyDict();
            d.Set("pi", Math.PI);
            d.Set("e", Math.E);
            Add(d, "floor", (a, k) => (long)Math.Floor(Py.ToDouble(a[0])));
            Add(d, "ceil", (a, k) => (long)Math.Ceiling(Py.ToDouble(a[0])));
            Add(d, "sqrt", (a, k) => Math.Sqrt(Py.ToDouble(a[0])));
            Add(d, "sin", (a, k) => Math.Sin(Py.ToDouble(a[0])));
            Add(d, "cos", (a, k) => Math.Cos(Py.ToDouble(a[0])));
            Add(d, "tan", (a, k) => Math.Tan(Py.ToDouble(a[0])));
            Add(d, "atan2", (a, k) => Math.Atan2(Py.ToDouble(a[0]), Py.ToDouble(a[1])));
            Add(d, "pow", (a, k) => Math.Pow(Py.ToDouble(a[0]), Py.ToDouble(a[1])));
            Add(d, "log", (a, k) => Math.Log(Py.ToDouble(a[0])));
            Add(d, "fabs", (a, k) => Math.Abs(Py.ToDouble(a[0])));
            return new PyNamespace("math", d);
        }

        // ---------------------------------------------------------------- values

        void InstallDisplayables(PyDict store)
        {
            Add(store, "Image", (a, k) => new Displayable { Kind = "image", Path = Str(Arg(a, 0)) });

            Add(store, "Solid", (a, k) => new Displayable { Kind = "solid", Color = ColorValue.Normalize(Arg(a, 0)) });

            Add(store, "Text", (a, k) =>
            {
                var d = new Displayable { Kind = "text", Text = Str(Arg(a, 0)) };
                if (k != null) foreach (var kv in k) d.Properties.Set(kv.Key, kv.Value);
                return d;
            });

            Add(store, "Null", (a, k) => new Displayable { Kind = "null" });

            Add(store, "Frame", (a, k) => new Displayable { Kind = "frame", Path = DisplayableToPath(Arg(a, 0)) });

            Add(store, "Composite", (a, k) =>
            {
                var d = new Displayable { Kind = "composite" };
                for (int i = 1; i < a.Length; i++) d.Children.Add(a[i]);
                return d;
            });

            Add(store, "Fixed", (a, k) =>
            {
                var d = new Displayable { Kind = "fixed" };
                foreach (var child in a) d.Children.Add(child);
                return d;
            });

            Add(store, "LiveComposite", (a, k) => new Displayable { Kind = "composite" });

            Add(store, "Animation", (a, k) =>
            {
                var d = new Displayable { Kind = "animation" };
                foreach (var child in a) d.Children.Add(child);
                return d;
            });

            Add(store, "Transform", (a, k) =>
            {
                var props = new PyDict();
                if (k != null) foreach (var kv in k) props.Set(kv.Key, kv.Value);
                var t = new InlineTransform(props);
                // Transform(child, **props) also wraps a displayable.
                return t;
            });

            Add(store, "Movie", (a, k) =>
            {
                var d = new Displayable { Kind = "movie" };
                d.Path = Str(Kw(k, "play", null)) ?? Str(Kw(k, "filename", null)) ?? Str(Arg(a, 0));
                if (k != null) foreach (var kv in k) d.Properties.Set(kv.Key, kv.Value);
                return d;
            });

            Add(store, "AlphaMask", (a, k) => Arg(a, 0));
            Add(store, "At", (a, k) => Arg(a, 0));
            Add(store, "DynamicDisplayable", (a, k) => new Displayable { Kind = "dynamic" });
            Add(store, "ConditionSwitch", (a, k) => new Displayable { Kind = "condition" });
            Add(store, "ImageReference", (a, k) => new Displayable { Kind = "image", Path = Str(Arg(a, 0)) });

            Add(store, "Borders", (a, k) =>
            {
                var d = new Displayable { Kind = "borders" };
                for (int i = 0; i < a.Length; i++) d.Properties.Set("b" + i, a[i]);
                return d;
            });

            Add(store, "Color", (a, k) =>
            {
                // Color() also accepts rgb=/hls= keywords; the positional form covers
                // everything the games seen so far use.
                object source = Arg(a, 0) ?? Kw(k, "rgb", null) ?? Kw(k, "color", null);
                return new Displayable { Kind = "color", Color = ColorValue.Normalize(source) };
            });

            InstallActions(store);

            // Bar/scroll values. The screen layer reads them as opaque handles.
            foreach (var name in new[]
            {
                "YScrollValue", "XScrollValue", "StaticValue", "AnimatedValue",
                "VolumeValue", "ScreenVariableValue", "FieldValue", "DictValue",
                "VariableValue", "MixerValue", "AudioPositionValue",
            })
            {
                string valueName = name;
                Add(store, valueName, (a, k) => new Displayable { Kind = "value", Text = valueName });
            }

            Add(store, "Style", (a, k) => new PyDict());
            store.Set("style", new PyNamespace("style", new PyDict()));
        }

        /// <summary>
        /// Screen actions. Each returns a callable that the screen system invokes on
        /// the engine thread when the player activates a widget.
        /// </summary>
        void InstallActions(PyDict store)
        {
            PyNative Action(string name, Action<object[], PyDict> body)
            {
                return new PyNative(name, (bound, boundKw) =>
                    new PyNative(name + "()", (a2, k2) => { body(bound, boundKw); return null; }));
            }

            store.Set("Start", Action("Start", (a, k) =>
            {
                throw new StartGameSignal(Str(Arg(a, 0)));
            }));

            store.Set("MainMenu", Action("MainMenu", (a, k) => { throw new MainMenuSignal(); }));
            store.Set("Quit", Action("Quit", (a, k) => { throw new QuitSignal(); }));
            store.Set("Return", Action("Return", (a, k) => { throw new ReturnSignal(Arg(a, 0)); }));
            store.Set("Jump", Action("Jump", (a, k) => { throw new JumpSignal(Str(Arg(a, 0))); }));

            store.Set("Show", Action("Show", (a, k) => engine.Screens.Show(Str(Arg(a, 0)), k)));
            store.Set("Hide", Action("Hide", (a, k) => engine.Screens.Hide(Str(Arg(a, 0)))));
            store.Set("ShowMenu", Action("ShowMenu", (a, k) => engine.Screens.Show(Str(Arg(a, 0)), k)));
            store.Set("ShowTransient", Action("ShowTransient", (a, k) => engine.Screens.Show(Str(Arg(a, 0)), k)));
            store.Set("HideInterface", Action("HideInterface", (a, k) => { }));
            store.Set("NullAction", Action("NullAction", (a, k) => { }));

            store.Set("Play", Action("Play", (a, k) =>
            {
                string channel = Str(Arg(a, 0)) ?? "sound";
                Host.PlayAudio(channel, AudioSpec.Parse(Str(Arg(a, 1))), 0f, false);
            }));

            store.Set("Stop", Action("Stop", (a, k) =>
            {
                Host.StopAudio(Str(Arg(a, 0)) ?? "sound", Flt(Kw(k, "fadeout", 0f)));
            }));

            store.Set("Function", Action("Function", (a, k) =>
            {
                if (a.Length == 0 || a[0] == null) return;
                PyOps.CallObject(a[0], Slice(a, 1), k);
            }));

            store.Set("SetField", Action("SetField", (a, k) =>
            {
                if (a.Length < 3) return;
                PyOps.SetAttr(a[0], Str(a[1]), a[2]);
            }));

            store.Set("ToggleField", Action("ToggleField", (a, k) =>
            {
                if (a.Length < 2) return;
                object current;
                PyOps.TryGetAttr(a[0], Str(a[1]), out current);
                PyOps.SetAttr(a[0], Str(a[1]), !Py.Truthy(current));
            }));

            store.Set("SetVariable", Action("SetVariable", (a, k) =>
            {
                if (a.Length < 2) return;
                Interp.GetStore("store").Set(Str(a[0]), a[1]);
            }));

            store.Set("ToggleVariable", Action("ToggleVariable", (a, k) =>
            {
                if (a.Length < 1) return;
                var target = Interp.GetStore("store");
                target.Set(Str(a[0]), !Py.Truthy(target.Get(Str(a[0]), false)));
            }));

            // If(condition, true=..., false=...) picks between action lists.
            store.Set("If", new PyNative("If", (bound, boundKw) =>
                new PyNative("If()", (a2, k2) =>
                {
                    bool taken = bound.Length > 0 && Py.Truthy(bound[0]);
                    object branch = Kw(boundKw, taken ? "true" : "false", null);
                    if (branch == null && bound.Length > (taken ? 1 : 2))
                        branch = bound[taken ? 1 : 2];
                    engine.Screens.Invoke(branch);
                    return null;
                })));

            store.Set("SetScreenVariable", Action("SetScreenVariable", (a, k) => { }));
            store.Set("Preference", Action("Preference", (a, k) => { }));
            store.Set("FileSave", Action("FileSave", (a, k) => { }));
            store.Set("FileLoad", Action("FileLoad", (a, k) => { }));
            store.Set("FilePage", Action("FilePage", (a, k) => { }));
            store.Set("QuickSave", Action("QuickSave", (a, k) => { }));
            store.Set("QuickLoad", Action("QuickLoad", (a, k) => { }));
            store.Set("Rollback", Action("Rollback", (a, k) => { }));
            store.Set("Skip", Action("Skip", (a, k) => { }));
            store.Set("Help", Action("Help", (a, k) => { }));

            // `With(None)` inside an action list is a transition, not a behaviour.
            Add(store, "With", (a, k) => new PyNative("With()", (a2, k2) => null));
        }

        void InstallPositions(PyDict store)
        {
            store.Set("left", Position.Align(0f, 1f));
            store.Set("right", Position.Align(1f, 1f));
            store.Set("center", Position.Align(0.5f, 1f));
            store.Set("truecenter", Position.Align(0.5f, 0.5f));
            store.Set("top", Position.Align(0.5f, 0f));
            store.Set("bottom", Position.Align(0.5f, 1f));
            store.Set("topleft", Position.Align(0f, 0f));
            store.Set("topright", Position.Align(1f, 0f));
            store.Set("offscreenleft", Position.Align(-0.5f, 1f));
            store.Set("offscreenright", Position.Align(1.5f, 1f));

            Add(store, "Position", (a, k) => new Position(k));
            Add(store, "Align", (a, k) => Position.Align(Flt(Arg(a, 0), 0.5f), Flt(Arg(a, 1), 1f)));
        }

        void InstallTransitions(PyDict store)
        {
            // Bare transition names.
            foreach (var name in new[]
            {
                "dissolve", "fade", "pixellate", "move", "moveinleft", "moveinright",
                "moveintop", "moveinbottom", "moveoutleft", "moveoutright", "moveouttop",
                "moveoutbottom", "wipeleft", "wiperight", "wipeup", "wipedown",
                "slideleft", "slideright", "slideup", "slidedown", "irisin", "irisout",
                "vpunch", "hpunch", "zoomin", "zoomout", "blinds", "squares",
            })
            {
                store.Set(name, new Transition(name, 0.5f));
            }

            store.Set("None", null);

            // Constructors.
            foreach (var name in new[]
            {
                "Dissolve", "Fade", "Pixellate", "ImageDissolve", "AlphaDissolve",
                "MoveTransition", "CropMove", "PushMove", "Wipe", "MoveIn", "MoveOut",
                "ZoomInOut", "Shuffle",
            })
            {
                string transitionName = name.ToLowerInvariant();
                Add(store, name, (a, k) => new Transition(transitionName, Flt(Arg(a, 0), 0.5f)));
            }

            Add(store, "Fade", (a, k) =>
            {
                // Fade(out_time, hold_time, in_time)
                float total = Flt(Arg(a, 0), 0.25f) + Flt(Arg(a, 1), 0f) + Flt(Arg(a, 2), 0.25f);
                return new Transition("fade", total);
            });

            // `with Pause(t)` holds the screen for a moment rather than blending.
            Add(store, "Pause", (a, k) => new Transition("pause", Flt(Arg(a, 0), 0.5f)));

            Add(store, "With", (a, k) => Arg(a, 0));
            Add(store, "MultipleTransition", (a, k) => new Transition("dissolve", 0.5f));
        }

        void InstallWarpers(PyDict store)
        {
            // ATL warpers are referenced by name; the host does the easing.
            foreach (var name in new[]
            {
                "linear", "ease", "easein", "easeout", "ease_back", "easein_back",
                "easeout_back", "ease_bounce", "easein_bounce", "easeout_bounce",
                "ease_circ", "easein_circ", "easeout_circ", "ease_cubic", "easein_cubic",
                "easeout_cubic", "ease_elastic", "easein_elastic", "easeout_elastic",
                "ease_expo", "easein_expo", "easeout_expo", "ease_quad", "easein_quad",
                "easeout_quad", "ease_quart", "easein_quart", "easeout_quart",
                "ease_quint", "easein_quint", "easeout_quint", "ease_sine",
                "easein_sine", "easeout_sine", "instant", "pause",
            })
            {
                if (!store.Contains(name)) store.Set(name, name);
            }
        }

        // ---------------------------------------------------------------- character

        object MakeCharacter(object[] args, PyDict kwargs)
        {
            var character = new RenPyCharacter { Engine = engine };

            if (args.Length > 0 && args[0] != null) character.Name = PyOps.ToStr(args[0]);

            // Character(name, kind=other) inherits the other character's properties.
            if (kwargs != null)
            {
                object kind;
                if (kwargs.TryGet("kind", out kind))
                {
                    var parent = kind as RenPyCharacter;
                    if (parent != null)
                        foreach (var kv in parent.Properties) character.Properties.Set(kv.Key, kv.Value);
                }

                foreach (var kv in kwargs)
                {
                    string key = PyOps.ToStr(kv.Key);
                    if (key == "kind") continue;
                    if (key == "callback") { character.Callback = kv.Value; continue; }
                    if (key == "image") { character.ImageTag = PyOps.ToStr(kv.Value); continue; }
                    character.Properties.Set(key, kv.Value);
                }
            }

            if (character.ImageTag == null && character.Name != null)
                character.ImageTag = RenPyScript.Normalize(character.Name);

            return character;
        }

        // ---------------------------------------------------------------- say

        public void Say(object who, string what)
        {
            var character = who as RenPyCharacter;
            if (character != null) { SayAs(character, what); return; }

            if (who == null || who is bool)
            {
                SayAs(null, what);
                return;
            }

            // A plain string speaker is shown as an ad-hoc name.
            SayAs(new RenPyCharacter { Name = PyOps.ToStr(who), Engine = engine }, what);
        }

        public void SayAs(RenPyCharacter character, string what)
        {
            if (character != null && character.Callback != null)
            {
                try
                {
                    var kw = new PyDict();
                    kw.Set("interact", true);
                    PyOps.CallObject(character.Callback, new object[] { "begin" }, kw);
                }
                catch (JumpSignal) { throw; }
                catch (ReturnSignal) { throw; }
                catch (Exception e) { Host.Log("[character callback] " + e.Message); }
            }

            var parsed = TextTags.Parse(InterpolateText(what));

            var style = character != null ? character.ToStyle() : new SpeakerStyle();
            engine.LastSaid = parsed.Text;
            Host.Say(style, parsed.Text, parsed.AutoAdvance, parsed.NoWait);
        }

        /// <summary>Resolves Ren'Py's [name] interpolation against the store.</summary>
        public string InterpolateText(string source)
        {
            return TextTags.Interpolate(source, expression =>
            {
                try
                {
                    return PyOps.ToStr(Interp.Eval(expression, "store", "<interpolate>"));
                }
                catch (Exception)
                {
                    return "[" + expression + "]";
                }
            });
        }

        // ---------------------------------------------------------------- helpers

        void ShowByName(string name, PyDict kwargs)
        {
            if (string.IsNullOrEmpty(name)) return;

            string tag = RenPyScript.Normalize(name.Split(' ')[0]);
            string layer = Str(Kw(kwargs, "layer", null)) ?? engine.DefaultLayer(tag);

            var image = new ShownImage
            {
                Tag = tag,
                Layer = layer,
                Name = name,
                AssetPath = engine.Script.ResolveImageFile(name),
            };

            object at = Kw(kwargs, "at_list", null) ?? Kw(kwargs, "at", null);
            if (at != null) AtlRuntime.Apply(engine, image.Transform, at);

            engine.Scene.Show(image);
            Host.Show(image);
        }

        static string NameOf(object value)
        {
            var tuple = value as PyTuple;
            if (tuple != null)
            {
                var parts = new List<string>();
                foreach (var item in tuple.Items) parts.Add(PyOps.ToStr(item));
                return string.Join(" ", parts.ToArray());
            }
            return value == null ? null : PyOps.ToStr(value);
        }

        /// <summary>Extracts a usable asset path from a displayable value.</summary>
        public string DisplayableToPath(object value)
        {
            if (value == null) return null;

            var s = value as string;
            if (s != null)
            {
                if (Host.FileExists(s)) return s;
                return engine.Script.ResolveImageFile(s);
            }

            var displayable = value as Displayable;
            if (displayable != null)
            {
                if (displayable.Path != null) return DisplayableToPath(displayable.Path);
                foreach (var child in displayable.Children)
                {
                    string path = DisplayableToPath(child);
                    if (path != null) return path;
                }
            }

            return null;
        }
    }

    /// <summary>
    /// The persistent store. Reading an unknown name yields None rather than raising,
    /// which is what Ren'Py scripts rely on for first-run defaults.
    /// </summary>
    public sealed class PersistentNamespace : IPyAttrs
    {
        public readonly PyDict Dict;

        public PersistentNamespace(PyDict dict) { Dict = dict; }

        public bool TryGetAttr(string name, out object value)
        {
            if (Dict.TryGet(name, out value)) return true;
            value = null;
            return true;
        }

        public void SetAttr(string name, object value) { Dict.Set(name, value); }

        public override string ToString() { return "<persistent>"; }
    }
}
