using System;
using System.Collections.Generic;
using System.Globalization;
using RenPy.Ast;
using RenPy.Python;

namespace RenPy.Runtime
{
    /// <summary>Raised by renpy.jump() to transfer control, as Ren'Py itself does.</summary>
    public class JumpSignal : Exception
    {
        public readonly string Label;
        public JumpSignal(string label) : base("jump to " + label) { Label = label; }
    }

    /// <summary>Raised by renpy.call().</summary>
    public class CallSignal : Exception
    {
        public readonly string Label;
        public CallSignal(string label) : base("call " + label) { Label = label; }
    }

    /// <summary>Raised by renpy.return_statement().</summary>
    public class ReturnSignal : Exception
    {
        public readonly object Value;
        public ReturnSignal(object value = null) : base("return") { Value = value; }
    }

    /// <summary>Raised when the player quits, to unwind the script thread.</summary>
    public class QuitSignal : Exception { }

    /// <summary>
    /// Runs a loaded Ren'Py script. Executes synchronously on its own thread: calls
    /// into <see cref="IRenPyHost"/> that need player input simply block, which keeps
    /// the interpreter and script semantics straightforward.
    /// </summary>
    public class RenPyEngine
    {
        public readonly RenPyScript Script = new RenPyScript();
        public readonly PyInterp Interp = new PyInterp();
        public readonly SceneState Scene = new SceneState();
        public readonly IRenPyHost Host;

        /// <summary>The `store` namespace, where all game variables live.</summary>
        public PyDict Store { get { return Interp.GetStore("store"); } }
        public PyDict Persistent { get { return Interp.GetStore("persistent"); } }

        public RenPyApi Api { get; private set; }
        public ScreenManager Screens { get; private set; }

        Node current;
        readonly List<Node> callStack = new List<Node>();

        /// <summary>Set while a `menu` is choosing, to suppress nested interaction.</summary>
        public object LastReturnValue;

        public RenPyEngine(IRenPyHost host)
        {
            Host = host;
            Interp.Warn = message => Host.Log("[python] " + message);
            Screens = new ScreenManager(this);
            Screens.Changed = screens => Host.ScreensChanged(screens);
            Api = new RenPyApi(this);
            Api.Install();
        }

        // ---------------------------------------------------------------- loading

        public void LoadScripts(IEnumerable<KeyValuePair<string, byte[]>> files)
        {
            foreach (var file in files) Script.AddRpyc(file.Key, file.Value);
            Script.ScanImages(Host.ListFiles());

            foreach (var warning in Script.Warnings) Host.Log("[script] " + warning);
        }

        /// <summary>
        /// Runs the init blocks in priority order, as Ren'Py does before the game
        /// starts. `python early` blocks run first.
        /// </summary>
        public void RunInit()
        {
            var inits = new List<InitNode>(Script.Inits);
            // A stable sort keeps same-priority blocks in file order, which matters
            // because later blocks routinely depend on earlier ones.
            StableSortByPriority(inits);

            foreach (var init in inits)
            {
                if (init.Block.Count == 0) continue;
                RunInitChain(init.Block[0]);
            }
        }

        /// <summary>
        /// Runs an init block's chain. A failing statement is logged and skipped so
        /// one bad definition cannot take down the whole game.
        /// </summary>
        void RunInitChain(Node node)
        {
            var seen = new HashSet<Node>();
            while (node != null && seen.Add(node))
            {
                Node executing = node;
                node = node.Next;

                try
                {
                    ExecuteNode(executing, true);
                }
                catch (QuitSignal) { throw; }
                catch (Exception e)
                {
                    Host.Log("[init] error at " + executing + ": " + e.Message);
                }
            }
        }

        static void StableSortByPriority(List<InitNode> inits)
        {
            var indices = new int[inits.Count];
            for (int i = 0; i < indices.Length; i++) indices[i] = i;

            var source = inits.ToArray();
            Array.Sort(indices, (a, b) =>
            {
                int c = source[a].Priority.CompareTo(source[b].Priority);
                return c != 0 ? c : a.CompareTo(b);
            });

            inits.Clear();
            for (int i = 0; i < indices.Length; i++) inits.Add(source[indices[i]]);
        }

        // ---------------------------------------------------------------- running

        /// <summary>Starts execution at a label and runs until the script returns.</summary>
        public void Run(string startLabel = "start")
        {
            var label = Script.FindLabel(startLabel);
            if (label == null)
            {
                Host.Log("[engine] no label '" + startLabel + "'");
                return;
            }

            current = label.Next;
            Loop();
        }

        void Loop()
        {
            while (current != null)
            {
                if (Host.ShouldStop) return;

                Node node = current;

                try
                {
                    // Buttons clicked on an overlay screen run here, between
                    // statements, where touching the interpreter is safe.
                    if (Screens.HasPending) Screens.RunPending();

                    ExecuteNode(node, false);
                    // A statement that did not redirect control falls through.
                    if (ReferenceEquals(current, node)) current = node.Next;
                }
                catch (JumpSignal jump)
                {
                    if (!GoTo(jump.Label)) return;
                }
                catch (CallSignal call)
                {
                    callStack.Add(node.Next);
                    if (!GoTo(call.Label)) return;
                }
                catch (ReturnSignal ret)
                {
                    LastReturnValue = ret.Value;
                    if (!PopCall()) return;
                }
                catch (QuitSignal)
                {
                    return;
                }
                catch (PyError error)
                {
                    Host.Log("[script] " + error.Message + "\n   at " + node);
                    current = node.Next;
                }
                catch (Exception e)
                {
                    Host.Log("[script] " + e.GetType().Name + ": " + e.Message + "\n   at " + node);
                    current = node.Next;
                }
            }
        }

        bool GoTo(string labelName)
        {
            var label = Script.FindLabel(labelName);
            if (label == null)
            {
                Host.Log("[engine] jump to unknown label '" + labelName + "'");
                current = null;
                return false;
            }

            current = label.Next;
            return true;
        }

        bool PopCall()
        {
            if (callStack.Count == 0)
            {
                current = null;
                return false;
            }

            current = callStack[callStack.Count - 1];
            callStack.RemoveAt(callStack.Count - 1);
            return true;
        }

        /// <summary>
        /// Runs Ren'Py's boot sequence: the splashscreen, then the main menu, then
        /// the game. Falls straight through to `start` for games with no menu.
        /// </summary>
        public void Boot(string startLabel = "start")
        {
            RunLabel("splashscreen");

            bool hasMenu = Script.FindScreen("main_menu") != null || Script.FindLabel("main_menu") != null;

            while (!Host.ShouldStop)
            {
                try
                {
                    if (!hasMenu) { Run(startLabel); return; }

                    // A false return means the host will never deliver another
                    // action, so re-showing the menu would spin forever.
                    if (!ShowMainMenu()) return;
                }
                catch (StartGameSignal start)
                {
                    Screens.HideAll();
                    Run(string.IsNullOrEmpty(start.Label) ? startLabel : start.Label);
                }
                catch (MainMenuSignal)
                {
                    Screens.HideAll();
                }
                catch (JumpSignal jump)
                {
                    Screens.HideAll();
                    if (!GoTo(jump.Label)) return;
                    Loop();
                }
                catch (QuitSignal) { return; }
            }
        }

        /// <summary>
        /// Displays the main menu and waits for the player to choose. Returns false
        /// when the host stopped delivering actions, meaning boot should end.
        /// </summary>
        bool ShowMainMenu()
        {
            // A game may override the menu with a label instead of a screen.
            var label = Script.FindLabel("main_menu");
            if (label != null) { Run("main_menu"); return true; }

            Screens.Show("main_menu", null);

            while (!Host.ShouldStop)
            {
                if (!Host.WaitForScreenAction()) return false;
                Screens.RunPending();
            }

            return false;
        }

        /// <summary>Runs a label to completion if it exists, ignoring it otherwise.</summary>
        public void RunLabel(string name)
        {
            var label = Script.FindLabel(name);
            if (label == null) return;

            current = label.Next;
            Loop();
        }

        // ---------------------------------------------------------------- save/load

        /// <summary>The statement about to run. Null when nothing is executing.</summary>
        public Node CurrentNode { get { return current; } }

        /// <summary>Last line spoken, kept so a save slot can show where you were.</summary>
        public string LastSaid { get; internal set; }

        /// <summary>True while the game (rather than the menu) is running.</summary>
        public bool InGame { get { return current != null; } }

        /// <summary>Snapshots the running game.</summary>
        public RenPySaveState CaptureState()
        {
            var state = new RenPySaveState
            {
                NodeId = RenPyScript.IdOf(current),
                LabelHint = NearestLabel(),
                Caption = LastSaid,
                SavedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            };

            foreach (var node in callStack) state.CallStack.Add(RenPyScript.IdOf(node));

            // Only game state travels; init rebuilds the rest.
            foreach (var kv in Store)
            {
                string name = PyOps.ToStr(kv.Key);
                if (name.StartsWith("_", StringComparison.Ordinal)) continue;
                if (!IsSaveable(kv.Value)) continue;
                state.Store.Set(name, kv.Value);
            }

            foreach (var layer in Scene.LayerOrder)
            {
                foreach (var image in Scene.Sorted(layer))
                {
                    state.Scene.Add(new RenPySaveState.SavedImage
                    {
                        Layer = image.Layer,
                        Tag = image.Tag,
                        Name = image.Name,
                        ZOrder = image.ZOrder,
                    });
                }
            }

            return state;
        }

        /// <summary>Values that survive a round trip; anything else is init-owned.</summary>
        static bool IsSaveable(object value)
        {
            if (value == null || value is bool || value is string || value is byte[]) return true;
            if (Py.IsNumber(value)) return true;

            var list = value as PyList;
            if (list != null)
            {
                foreach (var item in list.Items) if (!IsSaveable(item)) return false;
                return true;
            }

            var tuple = value as PyTuple;
            if (tuple != null)
            {
                foreach (var item in tuple.Items) if (!IsSaveable(item)) return false;
                return true;
            }

            var set = value as PySet;
            if (set != null)
            {
                foreach (var item in set.Items) if (!IsSaveable(item)) return false;
                return true;
            }

            var dict = value as PyDict;
            if (dict != null)
            {
                foreach (var kv in dict)
                    if (!IsSaveable(kv.Key) || !IsSaveable(kv.Value)) return false;
                return true;
            }

            return false;
        }

        string NearestLabel()
        {
            if (current == null) return null;

            // Walk the label table for the closest one in the same file above us.
            LabelNode best = null;
            foreach (var kv in Script.Labels)
            {
                var label = kv.Value;
                if (label.Filename != current.Filename) continue;
                if (label.LineNumber > current.LineNumber) continue;
                if (best == null || label.LineNumber > best.LineNumber) best = label;
            }

            return best != null ? best.LabelName : null;
        }

        /// <summary>
        /// Restores a snapshot and resumes. Overwrites only the saved names, so
        /// Characters and functions created during init stay intact.
        /// </summary>
        public void RestoreAndRun(RenPySaveState state)
        {
            if (state == null) return;

            foreach (var kv in state.Store) Store.Set(kv.Key, kv.Value);

            callStack.Clear();
            foreach (var id in state.CallStack)
            {
                var node = Script.FindNode(id);
                if (node != null) callStack.Add(node);
            }

            Scene.Clear();
            Host.ClearLayer("master");
            Host.ClearLayer("screens");

            foreach (var saved in state.Scene)
            {
                string path;
                Displayable displayable;
                var image = new ShownImage
                {
                    Layer = saved.Layer,
                    Tag = saved.Tag,
                    Name = saved.Name,
                    ZOrder = saved.ZOrder,
                };
                ResolveImageName(saved.Name, out path, out displayable, image.Transform);
                image.AssetPath = path;
                image.Displayable = displayable;

                Scene.Show(image);
                Host.Show(image);
            }

            current = Script.FindNode(state.NodeId);

            if (current == null)
            {
                Host.Log("[save] could not find the saved statement; starting the label instead");
                if (!string.IsNullOrEmpty(state.LabelHint)) { Run(state.LabelHint); return; }
                return;
            }

            Loop();
        }

        /// <summary>Used by renpy.jump() from Python.</summary>
        public void RequestJump(string label) { throw new JumpSignal(label); }

        // ---------------------------------------------------------------- statements

        void ExecuteNode(Node node, bool initPass)
        {
            var python = node as PythonNode;
            if (python != null) { RunCode(python.Code, python.Store); return; }

            var early = node as EarlyPythonNode;
            if (early != null) { RunCode(early.Code, early.Store); return; }

            var define = node as DefineNode;
            if (define != null) { ExecuteDefine(define); return; }

            var dflt = node as DefaultNode;
            if (dflt != null)
            {
                var store = Interp.GetStore(dflt.Store);
                // `default` only assigns when the name is not already set.
                if (!store.Contains(dflt.VarName))
                    store.Set(dflt.VarName, EvalCode(dflt.Code, dflt.Store));
                return;
            }

            var say = node as SayNode;
            if (say != null) { ExecuteSay(say); return; }

            var show = node as ShowNode;
            if (show != null) { ExecuteShow(show.Spec, show.Atl, null); return; }

            var scene = node as SceneNode;
            if (scene != null)
            {
                string layer = scene.Layer ?? (scene.Spec != null ? scene.Spec.Layer : null) ?? "master";
                Scene.ClearLayer(layer);
                Host.ClearLayer(layer);
                if (scene.Spec != null) ExecuteShow(scene.Spec, scene.Atl, layer);
                return;
            }

            var hide = node as HideNode;
            if (hide != null)
            {
                if (hide.Spec == null) return;
                string layer = hide.Spec.Layer ?? "master";
                string tag = hide.Spec.EffectiveTag;
                Scene.Hide(layer, tag);
                Host.Hide(layer, tag);
                return;
            }

            var with = node as WithNode;
            if (with != null) { ExecuteWith(with); return; }

            var jump = node as JumpNode;
            if (jump != null)
            {
                string target = jump.IsExpression ? PyOps.ToStr(Eval(jump.Target)) : jump.Target;
                throw new JumpSignal(target);
            }

            var call = node as CallNode;
            if (call != null)
            {
                string target = call.IsExpression ? PyOps.ToStr(Eval(call.Label)) : call.Label;
                throw new CallSignal(target);
            }

            var ret = node as ReturnNode;
            if (ret != null)
                throw new ReturnSignal(ret.Expression == null ? null : Eval(ret.Expression));

            var ifNode = node as IfNode;
            if (ifNode != null) { ExecuteIf(ifNode); return; }

            var whileNode = node as WhileNode;
            if (whileNode != null) { ExecuteWhile(whileNode); return; }

            var menu = node as MenuNode;
            if (menu != null) { ExecuteMenu(menu); return; }

            var user = node as UserStatementNode;
            if (user != null) { UserStatements.Execute(this, user); return; }

            var image = node as ImageNode;
            if (image != null) { Script.ImageStatements[image.FullName] = image; return; }

            var transform = node as TransformNode;
            if (transform != null)
            {
                // Transforms are looked up by name when applied, so registering is enough.
                Interp.GetStore(transform.Store).Set(transform.VarName, new AtlTransform(transform));
                return;
            }

            var screen = node as ScreenNode;
            if (screen != null)
            {
                if (screen.Screen != null && screen.Screen.ScreenName != null)
                    Script.Screens[screen.Screen.ScreenName] = screen.Screen;
                return;
            }

            // A label falls straight into its body, which chaining already points at.
            if (node is LabelNode) return;

            var init = node as InitNode;
            if (init != null)
            {
                // Init blocks run in the init pass; reaching one during play is a no-op.
                if (initPass && init.Block.Count > 0) RunInitChain(init.Block[0]);
                return;
            }

            // Styles, translations and pass statements have no runtime effect here.
        }

        void ExecuteDefine(DefineNode define)
        {
            var store = Interp.GetStore(define.Store);
            object value = EvalCode(define.Code, define.Store);

            if (define.Operator == "=" || string.IsNullOrEmpty(define.Operator))
            {
                store.Set(define.VarName, value);
                return;
            }

            // `define x += value` and friends.
            object current;
            if (!store.TryGet(define.VarName, out current)) current = null;
            string op = define.Operator.TrimEnd('=');
            store.Set(define.VarName, PyInterp.ApplyBinary(op, current, value));
        }

        void ExecuteIf(IfNode node)
        {
            foreach (var entry in node.Entries)
            {
                if (!Py.Truthy(Eval(entry.Condition))) continue;
                if (entry.Block.Count > 0) current = entry.Block[0];
                return;
            }
        }

        void ExecuteWhile(WhileNode node)
        {
            // The body is chained back to this node, so entering it re-tests the
            // condition on each pass and falling through exits the loop.
            if (Py.Truthy(Eval(node.Condition)) && node.Block.Count > 0)
                current = node.Block[0];
        }

        void ExecuteMenu(MenuNode node)
        {
            var choices = new List<string>();
            var blocks = new List<List<Node>>();
            string caption = null;

            foreach (var item in node.Items)
            {
                string label = Api.InterpolateText(item.Label);

                if (item.Block == null)
                {
                    // A caption item carries the menu prompt rather than a choice.
                    caption = label;
                    continue;
                }

                if (!Py.Truthy(Eval(item.Condition))) continue;

                choices.Add(label);
                blocks.Add(item.Block);
            }

            if (choices.Count == 0) return;

            int picked = Host.Menu(caption, choices);
            if (picked < 0 || picked >= blocks.Count) return;

            var block = blocks[picked];
            if (block.Count > 0) current = block[0];
        }

        void ExecuteWith(WithNode node)
        {
            if (string.IsNullOrEmpty(node.Expr) || node.Expr == "None") return;

            object value = Eval(node.Expr);
            var transition = value as Transition;

            string name = transition != null ? transition.Name : PyOps.ToStr(value);
            float duration = transition != null ? transition.Duration : 0.5f;

            Host.Transition("master", name, duration);
        }

        void ExecuteSay(SayNode node)
        {
            object who = string.IsNullOrEmpty(node.Who) ? null : Eval(node.Who);
            Api.Say(who, node.What);
        }

        void ExecuteShow(ImSpec spec, RawBlock atl, string forcedLayer)
        {
            if (spec == null) return;

            string name = spec.Expression != null
                ? PyOps.ToStr(Eval(spec.Expression))
                : spec.FullName;

            string tag = spec.EffectiveTag;
            string layer = forcedLayer ?? spec.Layer ?? DefaultLayer(tag);

            var image = new ShownImage
            {
                Tag = RenPyScript.Normalize(tag),
                Layer = layer,
                Name = name,
                AssetPath = Script.ResolveImageFile(name),
                ZOrder = ParseZOrder(spec.ZOrder),
            };

            // `at` clauses apply named transforms in order.
            foreach (var atExpr in spec.AtList)
            {
                object t = SafeEval(atExpr);
                AtlRuntime.Apply(this, image.Transform, t);
            }

            if (atl != null)
            {
                // Keep the timed steps so fades and moves actually play out.
                image.Timeline = AtlRuntime.BuildTimeline(this, atl, image.Transform);
                image.Transform = image.Timeline.Initial.Clone();
            }

            if (image.AssetPath == null) ResolveDefinedImage(image, name);

            if (image.AssetPath == null && image.Displayable == null)
                Host.Log("[image] cannot resolve '" + name + "'");

            Scene.Show(image);
            Host.Show(image);
        }

        /// <summary>
        /// Resolves a name declared by an `image` statement, which may evaluate to a
        /// file path, a colour, a movie, or another displayable.
        /// </summary>
        void ResolveDefinedImage(ShownImage image, string name)
        {
            string path;
            Displayable displayable;
            ResolveImageName(name, out path, out displayable, image.Transform);

            image.AssetPath = path;
            if (displayable != null) image.Displayable = displayable;
        }

        /// <summary>
        /// Resolves an image name to a file or a displayable, checking loose files,
        /// `image` statements and store variables in that order. Shared by `show` and
        /// the screen system, which both need `add "main_menu_bg"` to find a Movie.
        /// </summary>
        public bool ResolveImageName(string name, out string path, out Displayable displayable,
                                     TransformState transform = null)
        {
            path = null;
            displayable = null;
            if (string.IsNullOrEmpty(name)) return false;

            path = Script.ResolveImageFile(name);
            if (path != null) return true;

            object value = null;

            ImageNode defined;
            if (Script.ImageStatements.TryGetValue(name, out defined))
            {
                if (defined.Code != null) value = SafeEval(defined.Code.Source);

                // `image foo: <atl>` defines the image entirely through a transform.
                if (value == null && defined.Atl != null && transform != null)
                    AtlRuntime.ApplyBlock(this, transform, defined.Atl);
            }

            // The name may also be a store variable holding a displayable.
            if (value == null)
            {
                object stored;
                if (Store.TryGet(name, out stored)) value = stored;
            }

            if (value == null) return false;

            // A movie is played, not decoded, so it must not become a file path.
            var candidate = value as Displayable;
            if (candidate != null && candidate.Kind == "movie")
            {
                displayable = candidate;
                return true;
            }

            path = Api.DisplayableToPath(value);
            if (path != null) return true;

            if (candidate != null) { displayable = candidate; return true; }

            // A bare colour string such as "#000" is a solid.
            var text = value as string;
            if (text != null && text.Length > 0 && text[0] == '#')
            {
                displayable = new Displayable { Kind = "solid", Color = text };
                return true;
            }

            return false;
        }

        static int ParseZOrder(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            int rv;
            return int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out rv) ? rv : 0;
        }

        /// <summary>
        /// The layer a bare `show` targets. Ren'Py's default_layer() consults
        /// config.tag_layer and falls back to config.default_tag_layer -- it does
        /// NOT remember where the tag currently is. Making it sticky sends an image
        /// to whatever layer that tag last used, which puts backdrops above the scene.
        /// </summary>
        public string DefaultLayer(string tag)
        {
            var config = Interp.GetStore("store.config");

            string fallback = config.GetStr("default_tag_layer", null) as string ?? "master";
            if (string.IsNullOrEmpty(tag)) return fallback;

            var tagLayer = config.GetStr("tag_layer", null) as PyDict;
            if (tagLayer == null) return fallback;

            object layer;
            return tagLayer.TryGet(tag, out layer) && layer != null ? PyOps.ToStr(layer) : fallback;
        }

        // ---------------------------------------------------------------- python

        public void RunCode(PyCodeRef code, string storeName)
        {
            if (code == null || string.IsNullOrEmpty(code.Source)) return;

            if (code.Mode == "eval") { EvalCode(code, storeName); return; }

            Interp.Exec(code.Source, storeName ?? "store", code.Filename);
        }

        public object EvalCode(PyCodeRef code, string storeName)
        {
            if (code == null || string.IsNullOrEmpty(code.Source)) return null;

            if (code.Mode == "eval")
                return Interp.Eval(code.Source, storeName ?? "store", code.Filename);

            Interp.Exec(code.Source, storeName ?? "store", code.Filename);
            return null;
        }

        public object Eval(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return null;
            return Interp.Eval(expression, "store", "<expr>");
        }

        /// <summary>Evaluates an expression, logging rather than throwing on failure.</summary>
        public object SafeEval(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return null;
            try
            {
                return Interp.Eval(expression, "store", "<expr>");
            }
            catch (Exception e)
            {
                Host.Log("[expr] " + expression + " -> " + e.Message);
                return null;
            }
        }
    }
}
