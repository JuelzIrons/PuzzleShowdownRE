using System;
using System.Collections.Generic;
using System.Globalization;
using RenPy.Ast;
using RenPy.Python;

namespace RenPy.Runtime
{
    /// <summary>A named transform defined by a `transform` statement.</summary>
    public sealed class AtlTransform : IPyCallable
    {
        public readonly TransformNode Node;
        /// <summary>Arguments bound when the transform is used as `at foo(1, 2)`.</summary>
        public PyDict BoundArgs;

        public AtlTransform(TransformNode node) { Node = node; }

        public object Call(object[] args, PyDict kwargs)
        {
            var rv = new AtlTransform(Node) { BoundArgs = new PyDict() };

            if (Node.Parameters != null)
            {
                for (int i = 0; i < args.Length && i < Node.Parameters.Parameters.Count; i++)
                    rv.BoundArgs.Set(Node.Parameters.Parameters[i].Name, args[i]);
            }

            if (kwargs != null)
                foreach (var kv in kwargs) rv.BoundArgs.Set(kv.Key, kv.Value);

            return rv;
        }

        public override string ToString() { return "<transform " + Node.VarName + ">"; }
    }

    /// <summary>
    /// A transform built inline by Ren'Py's Transform() / anonymous ATL, holding
    /// plain property values.
    /// </summary>
    public sealed class InlineTransform : IPyCallable
    {
        public readonly PyDict Properties;

        public InlineTransform(PyDict properties) { Properties = properties ?? new PyDict(); }

        public object Call(object[] args, PyDict kwargs) { return this; }

        public override string ToString() { return "<Transform>"; }
    }

    /// <summary>A scene transition value, e.g. `dissolve` or `Dissolve(0.5)`.</summary>
    public sealed class Transition : IPyCallable
    {
        public readonly string Name;
        public float Duration;

        public Transition(string name, float duration) { Name = name; Duration = duration; }

        public object Call(object[] args, PyDict kwargs)
        {
            float duration = args.Length > 0 ? (float)Py.ToDouble(args[0]) : Duration;
            return new Transition(Name, duration);
        }

        public override string ToString() { return "<" + Name + ">"; }
    }

    /// <summary>
    /// Applies ATL blocks to a <see cref="TransformState"/>.
    ///
    /// This resolves the block to its final property values rather than animating
    /// through them; the host is given the end state and interpolates. That covers
    /// the positioning and fade work ATL is overwhelmingly used for.
    /// </summary>
    public static class AtlRuntime
    {
        /// <summary>Applies whatever an `at` clause evaluated to.</summary>
        public static void Apply(RenPyEngine engine, TransformState state, object value)
        {
            if (value == null) return;

            var list = value as PyList;
            if (list != null)
            {
                foreach (var item in list.Items) Apply(engine, state, item);
                return;
            }

            var tuple = value as PyTuple;
            if (tuple != null)
            {
                foreach (var item in tuple.Items) Apply(engine, state, item);
                return;
            }

            var named = value as AtlTransform;
            if (named != null)
            {
                ApplyBlock(engine, state, named.Node.Atl, named.BoundArgs);
                return;
            }

            var inline = value as InlineTransform;
            if (inline != null)
            {
                foreach (var kv in inline.Properties)
                    SetProperty(state, PyOps.ToStr(kv.Key), kv.Value);
                return;
            }

            var position = value as Position;
            if (position != null) { position.ApplyTo(state); return; }
        }

        public static void ApplyBlock(RenPyEngine engine, TransformState state, RawBlock block, PyDict args = null)
        {
            if (block == null) return;

            foreach (var statement in block.Statements)
                ApplyStatement(engine, state, statement, args);
        }

        /// <summary>
        /// Builds the animation an ATL block describes, starting from
        /// <paramref name="initial"/>.
        ///
        /// Statements with no duration mutate the current state in place; statements
        /// with a duration become timed steps. That is what turns
        /// `alpha 1.0` / `linear 2.0 alpha 0.0` into a two-second fade rather than an
        /// instant jump to transparent.
        /// </summary>
        public static AtlTimeline BuildTimeline(RenPyEngine engine, RawBlock block,
                                                TransformState initial, PyDict args = null)
        {
            var timeline = new AtlTimeline { Initial = initial != null ? initial.Clone() : new TransformState() };
            if (block == null) return timeline;

            var current = timeline.Initial.Clone();
            Collect(engine, block, timeline, ref current, args);

            return timeline;
        }

        static void Collect(RenPyEngine engine, RawStatement statement, AtlTimeline timeline,
                            ref TransformState current, PyDict args)
        {
            var block = statement as RawBlock;
            if (block != null)
            {
                foreach (var inner in block.Statements) Collect(engine, inner, timeline, ref current, args);
                return;
            }

            var multi = statement as RawMultipurpose;
            if (multi != null)
            {
                var target = current.Clone();

                foreach (var expr in multi.Expressions)
                {
                    if (string.IsNullOrEmpty(expr.Key)) continue;
                    Apply(engine, target, EvalWithArgs(engine, expr.Key, args));
                }

                foreach (var property in multi.Properties)
                    SetProperty(target, property.Key, EvalWithArgs(engine, property.Value, args));

                float duration = Duration(engine, multi, args);

                if (duration > 0f)
                {
                    timeline.Steps.Add(new AtlStep
                    {
                        Duration = duration,
                        Warper = string.IsNullOrEmpty(multi.Warper) ? "linear" : multi.Warper,
                        Target = target,
                    });
                }
                else if (timeline.Steps.Count == 0)
                {
                    // Before any timed step, an instant statement is the start state.
                    timeline.Initial = target.Clone();
                }
                else
                {
                    timeline.Steps.Add(new AtlStep { Duration = 0f, Warper = "instant", Target = target });
                }

                current = target;
                return;
            }

            var repeat = statement as RawRepeat;
            if (repeat != null)
            {
                if (string.IsNullOrEmpty(repeat.Repeats)) timeline.Repeat = 0;   // forever
                else
                {
                    object value = EvalWithArgs(engine, repeat.Repeats, args);
                    try { timeline.Repeat = value == null ? 0 : (int)Py.ToInt(value); }
                    catch (PyError) { timeline.Repeat = 0; }
                }
                return;
            }

            var time = statement as RawTime;
            if (time != null) return;   // absolute timing is not modelled

            var parallel = statement as RawParallel;
            if (parallel != null)
            {
                // Parallel tracks would need independent clocks; the first track
                // carries the motion in practice.
                if (parallel.Blocks.Count > 0) Collect(engine, parallel.Blocks[0], timeline, ref current, args);
                return;
            }

            var on = statement as RawOn;
            if (on != null)
            {
                RawBlock handler;
                if (on.Handlers.TryGetValue("start", out handler) || on.Handlers.TryGetValue("show", out handler))
                    Collect(engine, handler, timeline, ref current, args);
                return;
            }

            var choice = statement as RawChoice;
            if (choice != null)
            {
                if (choice.Choices.Count > 0 && choice.Choices[0].Value != null)
                    Collect(engine, choice.Choices[0].Value, timeline, ref current, args);
                return;
            }

            var child = statement as RawChild;
            if (child != null)
            {
                foreach (var b in child.Children) Collect(engine, b, timeline, ref current, args);
            }
        }

        static float Duration(RenPyEngine engine, RawMultipurpose multi, PyDict args)
        {
            if (string.IsNullOrEmpty(multi.Duration)) return 0f;

            object value = EvalWithArgs(engine, multi.Duration, args);
            if (value == null) return 0f;

            try { return (float)Py.ToDouble(value); }
            catch (PyError) { return 0f; }
        }

        static void ApplyStatement(RenPyEngine engine, TransformState state, RawStatement statement, PyDict args)
        {
            var multi = statement as RawMultipurpose;
            if (multi != null)
            {
                // Bare expressions name transforms or displayables to compose in.
                foreach (var expr in multi.Expressions)
                {
                    if (string.IsNullOrEmpty(expr.Key)) continue;
                    object value = EvalWithArgs(engine, expr.Key, args);
                    Apply(engine, state, value);
                }

                foreach (var property in multi.Properties)
                    SetProperty(state, property.Key, EvalWithArgs(engine, property.Value, args));

                return;
            }

            var parallel = statement as RawParallel;
            if (parallel != null)
            {
                foreach (var b in parallel.Blocks) ApplyBlock(engine, state, b, args);
                return;
            }

            var child = statement as RawChild;
            if (child != null)
            {
                foreach (var b in child.Children) ApplyBlock(engine, state, b, args);
                return;
            }

            var choice = statement as RawChoice;
            if (choice != null)
            {
                // Without animation timing, the first choice is as good as any.
                if (choice.Choices.Count > 0) ApplyBlock(engine, state, choice.Choices[0].Value, args);
                return;
            }

            var on = statement as RawOn;
            if (on != null)
            {
                // The "start" handler is what a freshly shown image runs.
                RawBlock start;
                if (on.Handlers.TryGetValue("start", out start)) ApplyBlock(engine, state, start, args);
                else if (on.Handlers.TryGetValue("show", out start)) ApplyBlock(engine, state, start, args);
                return;
            }

            var nested = statement as RawBlock;
            if (nested != null) { ApplyBlock(engine, state, nested, args); return; }

            // repeat/time/event/function carry animation semantics with no static effect.
        }

        static object EvalWithArgs(RenPyEngine engine, string expression, PyDict args)
        {
            if (string.IsNullOrEmpty(expression)) return null;

            if (args == null || args.Length == 0) return engine.SafeEval(expression);

            // Transform parameters shadow the store while the block is applied.
            var store = engine.Interp.GetStore("store");
            var saved = new PyDict();
            var added = new List<object>();

            foreach (var kv in args)
            {
                object existing;
                if (store.TryGet(kv.Key, out existing)) saved.Set(kv.Key, existing);
                else added.Add(kv.Key);
                store.Set(kv.Key, kv.Value);
            }

            try
            {
                return engine.SafeEval(expression);
            }
            finally
            {
                foreach (var kv in saved) store.Set(kv.Key, kv.Value);
                foreach (var key in added) store.Remove(key);
            }
        }

        /// <summary>Writes one ATL/transform property into the state.</summary>
        public static void SetProperty(TransformState state, string name, object value)
        {
            if (name == null) return;

            switch (name)
            {
                case "xpos": state.XPos = ToFloat(value); return;
                case "ypos": state.YPos = ToFloat(value); return;
                case "pos": SetPair(value, ref state.XPos, ref state.YPos); return;

                case "xanchor": state.XAnchor = ToFloat(value); return;
                case "yanchor": state.YAnchor = ToFloat(value); return;
                case "anchor": SetPair(value, ref state.XAnchor, ref state.YAnchor); return;

                case "xalign": state.XAlign = ToFloat(value); state.HasAlign = true; return;
                case "yalign": state.YAlign = ToFloat(value); state.HasAlign = true; return;
                case "align":
                    SetPair(value, ref state.XAlign, ref state.YAlign);
                    state.HasAlign = true;
                    return;

                case "xcenter":
                    state.XPos = ToFloat(value);
                    state.XAnchor = 0.5f;
                    return;
                case "ycenter":
                    state.YPos = ToFloat(value);
                    state.YAnchor = 0.5f;
                    return;

                case "zoom": state.Zoom = ToFloat(value); return;
                case "xzoom": state.XZoom = ToFloat(value); return;
                case "yzoom": state.YZoom = ToFloat(value); return;

                case "rotate": state.Rotate = ToFloat(value); return;
                case "alpha": state.Alpha = ToFloat(value); return;

                // Offsets fold into position.
                case "xoffset": state.XPos += ToFloat(value); return;
                case "yoffset": state.YPos += ToFloat(value); return;
                case "offset":
                {
                    float x = 0, y = 0;
                    SetPair(value, ref x, ref y);
                    state.XPos += x;
                    state.YPos += y;
                    return;
                }
            }
        }

        static void SetPair(object value, ref float x, ref float y)
        {
            var tuple = value as PyTuple;
            if (tuple != null && tuple.Items.Length >= 2)
            {
                x = ToFloat(tuple.Items[0]);
                y = ToFloat(tuple.Items[1]);
                return;
            }

            var list = value as PyList;
            if (list != null && list.Items.Count >= 2)
            {
                x = ToFloat(list.Items[0]);
                y = ToFloat(list.Items[1]);
                return;
            }

            float single = ToFloat(value);
            x = single;
            y = single;
        }

        static float ToFloat(object value)
        {
            if (value == null) return 0f;
            try { return (float)Py.ToDouble(value); }
            catch (PyError) { return 0f; }
        }
    }

    /// <summary>Ren'Py's Position()/left/right/center anchors.</summary>
    public sealed class Position : IPyCallable
    {
        public readonly PyDict Properties;

        public Position(PyDict properties) { Properties = properties ?? new PyDict(); }

        public object Call(object[] args, PyDict kwargs) { return new Position(kwargs); }

        public void ApplyTo(TransformState state)
        {
            foreach (var kv in Properties)
                AtlRuntime.SetProperty(state, PyOps.ToStr(kv.Key), kv.Value);
        }

        public static Position Align(float x, float y)
        {
            var d = new PyDict();
            d.Set("xalign", (double)x);
            d.Set("yalign", (double)y);
            return new Position(d);
        }
    }
}
