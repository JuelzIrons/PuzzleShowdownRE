using System;
using System.Collections.Generic;
using RenPy.Ast;
using RenPy.Python;

namespace RenPy.Runtime
{
    /// <summary>Raised by the Start() screen action to begin the game.</summary>
    public class StartGameSignal : Exception
    {
        public readonly string Label;
        public StartGameSignal(string label) { Label = label; }
    }

    /// <summary>Raised by MainMenu() to return to the title screen.</summary>
    public class MainMenuSignal : Exception { }

    /// <summary>
    /// Tracks which screens are showing and re-evaluates them when state changes.
    ///
    /// Screen actions are queued rather than run where they are clicked: they can
    /// jump, start the game or call Python, none of which is safe from Unity's main
    /// thread while the engine owns the interpreter.
    /// </summary>
    public class ScreenManager
    {
        readonly RenPyEngine engine;
        readonly ScreenEvaluator evaluator;

        readonly List<ShownScreen> shown = new List<ShownScreen>();
        readonly Queue<object> pendingActions = new Queue<object>();

        /// <summary>Raised on the engine thread after the screen set changes.</summary>
        public Action<List<ShownScreen>> Changed;

        public ScreenManager(RenPyEngine engine)
        {
            this.engine = engine;
            evaluator = new ScreenEvaluator(engine);
        }

        public List<ShownScreen> Shown { get { return shown; } }

        // ---------------------------------------------------------------- show/hide

        public void Show(string name, PyDict arguments)
        {
            if (string.IsNullOrEmpty(name)) return;

            var definition = engine.Script.FindScreen(name);
            if (definition == null)
            {
                engine.Host.Log("[screen] unknown screen '" + name + "'");
                return;
            }

            string tag = definition.Tag ?? name;

            // Showing a screen replaces whatever already occupies its tag.
            bool replaced = shown.RemoveAll(s => s.Tag == tag) > 0;

            var screen = new ShownScreen
            {
                Name = name,
                Tag = tag,
                ZOrder = EvalInt(definition.ZOrder, 0),
                Modal = definition.Modal != null && Py.Truthy(engine.SafeEval(definition.Modal)),
            };

            if (arguments != null)
                foreach (var kv in arguments) screen.Scope.Set(kv.Key, kv.Value);

            shown.Add(screen);
            shown.Sort((a, b) => a.ZOrder.CompareTo(b.ZOrder));

            Refresh();

            // `on "show"` is how screens pull in their backdrop and start their music.
            // Without firing it a menu comes up with none of its scenery.
            FireEvent(screen, replaced ? "replace" : "show");
        }

        public void Hide(string name)
        {
            if (string.IsNullOrEmpty(name)) return;

            int removed = shown.RemoveAll(s => s.Name == name || s.Tag == name);
            if (removed > 0) Refresh();
        }

        public void HideAll()
        {
            if (shown.Count == 0) return;
            shown.Clear();
            Refresh();
        }

        public bool IsShowing(string name)
        {
            foreach (var screen in shown)
                if (screen.Name == name || screen.Tag == name) return true;
            return false;
        }

        /// <summary>Re-evaluates every shown screen and notifies the host.</summary>
        public void Refresh()
        {
            foreach (var screen in shown)
            {
                var definition = engine.Script.FindScreen(screen.Name);
                if (definition == null) continue;

                try
                {
                    screen.Root = evaluator.Evaluate(definition, screen.Scope);
                }
                catch (Exception e)
                {
                    engine.Host.Log("[screen] failed to build '" + screen.Name + "': " + e.Message);
                    screen.Root = new ScreenWidget { Kind = "screen" };
                }
            }

            if (Changed != null) Changed(shown);
        }

        int EvalInt(string expression, int dflt)
        {
            if (string.IsNullOrEmpty(expression)) return dflt;
            object value = engine.SafeEval(expression);
            if (value == null) return dflt;
            try { return (int)Py.ToInt(value); }
            catch (PyError) { return dflt; }
        }

        // ---------------------------------------------------------------- events

        /// <summary>Depth guard: an `on show` handler may show further screens.</summary>
        int eventDepth;

        /// <summary>Runs a screen's `on &lt;event&gt;` handlers.</summary>
        public void FireEvent(ShownScreen screen, string eventName)
        {
            if (screen == null || screen.Root == null || eventDepth > 4) return;

            var actions = new List<object>();
            CollectEvent(screen.Root, eventName, actions);
            if (actions.Count == 0) return;

            eventDepth++;
            try
            {
                foreach (var action in actions) Invoke(action);
            }
            finally { eventDepth--; }

            Refresh();
        }

        static void CollectEvent(ScreenWidget widget, string eventName, List<object> into)
        {
            if (widget.Kind == "on" && widget.Action != null &&
                widget.Positional.Count > 0 && widget.Positional[0] != null &&
                PyOps.ToStr(widget.Positional[0]) == eventName)
            {
                into.Add(widget.Action);
            }

            foreach (var child in widget.Children) CollectEvent(child, eventName, into);
        }

        // ---------------------------------------------------------------- actions

        /// <summary>Queues an action clicked on the host thread.</summary>
        public void Enqueue(object action)
        {
            if (action == null) return;
            lock (pendingActions) pendingActions.Enqueue(action);
        }

        public bool HasPending
        {
            get { lock (pendingActions) return pendingActions.Count > 0; }
        }

        /// <summary>
        /// Runs every queued action on the engine thread. Control-flow signals
        /// propagate so Start/Jump/MainMenu behave as they do in Ren'Py.
        /// </summary>
        public void RunPending()
        {
            while (true)
            {
                object action;
                lock (pendingActions)
                {
                    if (pendingActions.Count == 0) break;
                    action = pendingActions.Dequeue();
                }

                Invoke(action);
            }

            Refresh();
        }

        /// <summary>Invokes an action, or each action in a list.</summary>
        public void Invoke(object action)
        {
            if (action == null) return;

            var list = action as PyList;
            if (list != null)
            {
                foreach (var item in new List<object>(list.Items)) Invoke(item);
                return;
            }

            var tuple = action as PyTuple;
            if (tuple != null)
            {
                foreach (var item in tuple.Items) Invoke(item);
                return;
            }

            var callable = action as IPyCallable;
            if (callable == null) return;

            // Control-flow signals must escape; anything else is contained so one bad
            // button cannot take the menu down.
            try
            {
                callable.Call(Py.EmptyArgs, null);
            }
            catch (StartGameSignal) { throw; }
            catch (MainMenuSignal) { throw; }
            catch (JumpSignal) { throw; }
            catch (CallSignal) { throw; }
            catch (ReturnSignal) { throw; }
            catch (QuitSignal) { throw; }
            catch (Exception e)
            {
                engine.Host.Log("[action] " + e.Message);
            }
        }
    }
}
