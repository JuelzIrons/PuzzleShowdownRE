using System;
using System.Collections.Generic;
using RenPy.Ast;
using RenPy.Python;

namespace RenPy.Runtime
{
    /// <summary>
    /// Evaluates Ren'Py screen language into a <see cref="ScreenWidget"/> tree.
    ///
    /// Screens are re-evaluated whenever their state changes, so this walks the SL
    /// AST against a live scope: `for` loops iterate, `if` branches pick, `python`
    /// blocks run, and `use` splices another screen in.
    /// </summary>
    public class ScreenEvaluator
    {
        readonly RenPyEngine engine;

        /// <summary>Guards against a screen that uses itself.</summary>
        int depth;
        const int MaxDepth = 12;

        public ScreenEvaluator(RenPyEngine engine) { this.engine = engine; }

        /// <summary>Evaluates a screen by name into a widget tree.</summary>
        public ScreenWidget Evaluate(SLScreen screen, PyDict arguments)
        {
            var root = new ScreenWidget { Kind = "screen" };
            if (screen == null) return root;

            // Screens get their own scope so widget-local variables cannot leak.
            var scope = new PyDict();
            if (arguments != null)
                foreach (var kv in arguments) scope.Set(kv.Key, kv.Value);

            BindParameters(screen.Parameters, arguments, scope);

            var frame = ScopeFrame(scope);

            ApplyKeywords(screen.Keyword, root, frame);
            root.StylePrefix = root.Str("style_prefix") ?? root.Str("style");

            foreach (var child in screen.Children) EvaluateNode(child, root, frame, scope);

            return root;
        }

        /// <summary>
        /// Screen variables live in their own dict but still read the store, which is
        /// exactly what a nested frame gives us.
        /// </summary>
        PyFrame ScopeFrame(PyDict scope)
        {
            var store = engine.Interp.GetStore("store");
            return new PyFrame(scope, store);
        }

        void BindParameters(ParameterInfo parameters, PyDict arguments, PyDict scope)
        {
            if (parameters == null) return;

            foreach (var p in parameters.Parameters)
            {
                if (scope.Contains(p.Name)) continue;

                if (arguments != null && arguments.Contains(p.Name))
                {
                    scope.Set(p.Name, arguments.GetStr(p.Name, null));
                    continue;
                }

                if (p.Default != null) scope.Set(p.Name, SafeEval(p.Default, scope));
                else scope.Set(p.Name, null);
            }
        }

        // ---------------------------------------------------------------- nodes

        void EvaluateNode(SLNode node, ScreenWidget parent, PyFrame frame, PyDict scope)
        {
            if (node == null) return;

            var python = node as SLPython;
            if (python != null)
            {
                RunPython(python.Code, frame);
                return;
            }

            var dflt = node as SLDefault;
            if (dflt != null)
            {
                if (!scope.Contains(dflt.Variable))
                    scope.Set(dflt.Variable, SafeEval(dflt.Expression, scope));
                return;
            }

            var ifNode = node as SLIf;
            if (ifNode != null)
            {
                foreach (var entry in ifNode.Entries)
                {
                    // A null condition is the else branch.
                    bool taken = entry.Condition == null || Py.Truthy(SafeEval(entry.Condition, scope));
                    if (!taken) continue;

                    if (entry.Block != null) EvaluateBlockChildren(entry.Block, parent, frame, scope);
                    break;
                }
                return;
            }

            var forNode = node as SLFor;
            if (forNode != null)
            {
                object iterable = SafeEval(forNode.Expression, scope);
                if (iterable == null) return;

                foreach (var item in SafeIterate(iterable))
                {
                    BindLoopVariable(forNode.Variable, item, scope);
                    foreach (var child in forNode.Children) EvaluateNode(child, parent, frame, scope);
                }
                return;
            }

            var use = node as SLUse;
            if (use != null) { EvaluateUse(use, parent, frame, scope); return; }

            var customUse = node as SLCustomUse;
            if (customUse != null)
            {
                var target = customUse.Ast ?? engine.Script.FindScreen(customUse.Target);
                SpliceScreen(target, parent, null);
                return;
            }

            if (node is SLTransclude || node is SLPass) return;

            var displayable = node as SLDisplayable;
            if (displayable != null) { EvaluateDisplayable(displayable, parent, frame, scope); return; }

            var block = node as SLBlock;
            if (block != null) EvaluateBlockChildren(block, parent, frame, scope);
        }

        void EvaluateBlockChildren(SLBlock block, ScreenWidget parent, PyFrame frame, PyDict scope)
        {
            // A bare block contributes its keywords to the parent and its children inline.
            ApplyKeywords(block.Keyword, parent, frame);
            foreach (var child in block.Children) EvaluateNode(child, parent, frame, scope);
        }

        void EvaluateUse(SLUse use, ScreenWidget parent, PyFrame frame, PyDict scope)
        {
            var target = use.Ast ?? engine.Script.FindScreen(use.Target);
            if (target == null)
            {
                engine.Host.Log("[screen] use of unknown screen '" + use.Target + "'");
                return;
            }

            PyDict arguments = null;
            if (use.Args != null)
            {
                arguments = new PyDict();
                foreach (var arg in use.Args.Arguments)
                {
                    if (arg.Key == null) continue;
                    arguments.Set(arg.Key, SafeEval(arg.Value, scope));
                }
            }

            SpliceScreen(target, parent, arguments);
        }

        void SpliceScreen(SLScreen target, ScreenWidget parent, PyDict arguments)
        {
            if (target == null || depth >= MaxDepth) return;

            depth++;
            try
            {
                var sub = Evaluate(target, arguments);
                // `use` inlines the other screen's widgets rather than nesting a screen.
                foreach (var child in sub.Children) parent.Children.Add(child);
            }
            finally { depth--; }
        }

        void EvaluateDisplayable(SLDisplayable node, ScreenWidget parent, PyFrame frame, PyDict scope)
        {
            string kind = KindOf(node);

            var widget = new ScreenWidget { Kind = kind, StylePrefix = parent.StylePrefix };

            foreach (var expression in node.Positional)
                widget.Positional.Add(SafeEval(expression, scope));

            foreach (var pair in node.DefaultKeywords)
                if (pair.Value != null) widget.Properties.Set(pair.Key, SafeEval(pair.Value, scope));

            ApplyKeywords(node.Keyword, widget, frame);

            Finish(widget, node, scope);

            // `on` and `key` are behaviours attached to the enclosing screen, not boxes.
            if (kind == "on" || kind == "key" || kind == "timer")
            {
                parent.Children.Add(widget);
                return;
            }

            foreach (var child in node.Children) EvaluateNode(child, widget, frame, scope);

            parent.Children.Add(widget);
        }

        /// <summary>Pulls the well-known properties into their dedicated fields.</summary>
        void Finish(ScreenWidget widget, SLDisplayable node, PyDict scope)
        {
            widget.Action = widget.Get("action");
            widget.Hovered = widget.Get("hovered");
            widget.Unhovered = widget.Get("unhovered");

            widget.ActivateSound = widget.Str("activate_sound");
            widget.HoverSound = widget.Str("hover_sound");

            widget.Idle = PathOf(widget.Get("idle"));
            widget.Hover = PathOf(widget.Get("hover"));
            widget.Selected = PathOf(widget.Get("selected_idle"));
            widget.Insensitive = PathOf(widget.Get("insensitive"));

            string prefix = widget.Str("style_prefix");
            if (prefix != null) widget.StylePrefix = prefix;

            // Text for text/label/textbutton comes from the first positional argument.
            if (widget.Positional.Count > 0 && widget.Positional[0] != null)
            {
                if (widget.Kind == "text" || widget.Kind == "label" || widget.Kind == "textbutton")
                    widget.Text = engine.Api.InterpolateText(PyOps.ToStr(widget.Positional[0]));
            }

            // `add "path"` names a file, an `image` statement, or a displayable.
            if (widget.Kind == "add" && widget.Positional.Count > 0)
                ResolveInto(widget, widget.Positional[0]);

            ApplyAt(widget);
        }

        void ApplyAt(ScreenWidget widget)
        {
            object at = widget.Get("at");
            if (at != null) AtlRuntime.Apply(engine, widget.Transform, at);

            // A named transform carries an animation the host can play.
            var named = at as AtlTransform;
            if (named != null && named.Node != null && named.Node.Atl != null)
                widget.Timeline = AtlRuntime.BuildTimeline(engine, named.Node.Atl, widget.Transform, named.BoundArgs);

            var list = at as PyList;
            if (list != null)
            {
                foreach (var item in list.Items)
                {
                    var t = item as AtlTransform;
                    if (t == null || t.Node == null || t.Node.Atl == null) continue;
                    widget.Timeline = AtlRuntime.BuildTimeline(engine, t.Node.Atl, widget.Transform, t.BoundArgs);
                    break;
                }
            }

            // Explicit positioning overrides whatever the transform set.
            if (widget.Has("xpos")) widget.Transform.XPos = widget.Float("xpos", 0f);
            if (widget.Has("ypos")) widget.Transform.YPos = widget.Float("ypos", 0f);
            if (widget.Has("xalign")) { widget.Transform.XAlign = widget.Float("xalign", 0f); widget.Transform.HasAlign = true; }
            if (widget.Has("yalign")) { widget.Transform.YAlign = widget.Float("yalign", 0f); widget.Transform.HasAlign = true; }
            if (widget.Has("xanchor")) widget.Transform.XAnchor = widget.Float("xanchor", 0f);
            if (widget.Has("yanchor")) widget.Transform.YAnchor = widget.Float("yanchor", 0f);
            if (widget.Has("zoom")) widget.Transform.Zoom = widget.Float("zoom", 1f);
            if (widget.Has("alpha")) widget.Transform.Alpha = widget.Float("alpha", 1f);
            if (widget.Has("rotate")) widget.Transform.Rotate = widget.Float("rotate", 0f);
        }

        string PathOf(object value)
        {
            if (value == null) return null;

            string direct = engine.Api.DisplayableToPath(value);
            if (direct != null) return direct;

            // Button art may name an `image` statement rather than a file.
            var text = value as string;
            if (text == null) return null;

            string path;
            Displayable displayable;
            engine.ResolveImageName(text, out path, out displayable);
            return path;
        }

        /// <summary>Resolves an `add` argument into either a path or a displayable.</summary>
        void ResolveInto(ScreenWidget widget, object value)
        {
            if (value == null) return;

            var displayable = value as Displayable;
            if (displayable == null)
            {
                string direct = engine.Api.DisplayableToPath(value);
                if (direct != null) { widget.Idle = direct; return; }

                var text = value as string;
                if (text == null) return;

                string path;
                engine.ResolveImageName(text, out path, out displayable);
                if (path != null) { widget.Idle = path; return; }
            }

            if (displayable == null) return;

            widget.Displayable = displayable;

            // A movie can still show its start_image if the video will not play.
            object start;
            if (displayable.Properties.TryGet("start_image", out start))
                widget.Idle = engine.Api.DisplayableToPath(start);
        }

        void ApplyKeywords(List<KeyValuePair<string, string>> keywords, ScreenWidget widget, PyFrame frame)
        {
            if (keywords == null) return;

            foreach (var pair in keywords)
            {
                if (pair.Key == null) continue;
                object value = pair.Value == null ? true : SafeEvalIn(pair.Value, frame);
                widget.Properties.Set(pair.Key, value);
            }
        }

        /// <summary>Maps an SL displayable onto a widget kind the host understands.</summary>
        static string KindOf(SLDisplayable node)
        {
            if (!string.IsNullOrEmpty(node.StatementName)) return node.StatementName;

            switch (node.DisplayableName)
            {
                case "renpy.sl2.sldisplayables.sl2add": return "add";
                case "renpy.text.text.Text": return "text";
                case "renpy.display.layout.Window": return "frame";
                case "renpy.display.layout.Grid": return "grid";
                case "renpy.display.layout.Null": return "null";
                case "renpy.ui._imagebutton": return "imagebutton";
                case "renpy.ui._textbutton": return "textbutton";
                case "renpy.ui._label": return "label";
                case "renpy.ui._key": return "key";
                case "renpy.display.behavior.Button": return "button";
                case "renpy.display.behavior.Input": return "input";
                case "renpy.display.behavior.OnEvent": return "on";
                case "renpy.display.behavior.Timer": return "timer";
                case "renpy.display.behavior.DismissBehavior": return "dismiss";
                case "renpy.sl2.sldisplayables.sl2viewport": return "viewport";
                case "renpy.sl2.sldisplayables.sl2vpgrid": return "vpgrid";
                case "renpy.sl2.sldisplayables.sl2bar": return "bar";
                case "renpy.sl2.sldisplayables.sl2vbar": return "vbar";
                case "renpy.display.layout.MultiBox": return "fixed";
                default: return "fixed";
            }
        }

        // ---------------------------------------------------------------- helpers

        void BindLoopVariable(string variable, object item, PyDict scope)
        {
            if (string.IsNullOrEmpty(variable)) return;

            // Ren'Py compiles tuple targets to a comma-joined name.
            if (variable.IndexOf(',') < 0) { scope.Set(variable, item); return; }

            var names = variable.Split(',');
            var values = new List<object>(SafeIterate(item));

            for (int i = 0; i < names.Length; i++)
            {
                string name = names[i].Trim();
                if (name.Length == 0) continue;
                scope.Set(name, i < values.Count ? values[i] : null);
            }
        }

        IEnumerable<object> SafeIterate(object value)
        {
            List<object> rv;
            try { rv = new List<object>(PyOps.Iterate(value)); }
            catch (PyError e)
            {
                engine.Host.Log("[screen] cannot iterate: " + e.Message);
                rv = new List<object>();
            }
            return rv;
        }

        object SafeEval(string expression, PyDict scope)
        {
            if (string.IsNullOrEmpty(expression)) return null;
            return SafeEvalIn(expression, ScopeFrame(scope));
        }

        object SafeEvalIn(string expression, PyFrame frame)
        {
            if (string.IsNullOrEmpty(expression)) return null;

            try
            {
                var compiled = engine.Interp.CompileExpression(expression, "<screen>");
                return engine.Interp.Eval(compiled, frame);
            }
            catch (PyError error) when (error.PyType == "NameError" && IsBareWord(expression))
            {
                // Style properties take bare words: `box_layout fixed`, `xfill vertical`.
                // Ren'Py resolves those as strings rather than variables.
                return expression.Trim();
            }
            catch (Exception e)
            {
                engine.Host.Log("[screen] " + expression + " -> " + e.Message);
                return null;
            }
        }

        static bool IsBareWord(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return false;

            string text = expression.Trim();
            if (text.Length == 0) return false;
            if (!char.IsLetter(text[0]) && text[0] != '_') return false;

            foreach (char c in text)
                if (!char.IsLetterOrDigit(c) && c != '_') return false;

            return true;
        }

        void RunPython(PyCodeRef code, PyFrame frame)
        {
            if (code == null || string.IsNullOrEmpty(code.Source)) return;

            try
            {
                var module = engine.Interp.CompileModule(code.Source, code.Filename);
                engine.Interp.ExecBlock(module.Body, frame);
            }
            catch (Exception e)
            {
                engine.Host.Log("[screen python] " + e.Message);
            }
        }
    }
}
