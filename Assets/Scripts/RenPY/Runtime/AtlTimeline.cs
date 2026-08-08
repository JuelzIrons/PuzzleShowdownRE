using System;
using System.Collections.Generic;

namespace RenPy.Runtime
{
    /// <summary>
    /// One timed leg of an ATL block: interpolate to <see cref="Target"/> over
    /// <see cref="Duration"/> seconds, shaped by <see cref="Warper"/>.
    /// </summary>
    public sealed class AtlStep
    {
        public float Duration;
        public string Warper = "linear";
        public TransformState Target;
    }

    /// <summary>
    /// The animation an ATL block describes: a starting state plus the steps that
    /// follow it.
    ///
    /// Resolving ATL to its final values only is what makes a `linear 2.0 alpha 0.0`
    /// fade snap instantly — and a full-screen black curtain that never fades is
    /// indistinguishable from a broken renderer. The timeline keeps the intermediate
    /// states so the host can play them out.
    /// </summary>
    public sealed class AtlTimeline
    {
        public TransformState Initial = new TransformState();
        public readonly List<AtlStep> Steps = new List<AtlStep>();
        /// <summary>Repeat count; 0 means loop forever, 1 means play once.</summary>
        public int Repeat = 1;

        public bool HasAnimation { get { return Steps.Count > 0; } }
    }

    /// <summary>Ren'Py's easing functions, applied to a normalised 0..1 time.</summary>
    public static class Warpers
    {
        public static float Apply(string name, float t)
        {
            t = t < 0f ? 0f : (t > 1f ? 1f : t);

            switch (name)
            {
                case null:
                case "":
                case "linear": return t;

                case "instant": return 1f;
                case "pause": return t >= 1f ? 1f : 0f;

                case "ease": return EaseInOut(t, 2f);
                case "easein":
                    // Ren'Py's easein decelerates into the target.
                    return 1f - Pow(1f - t, 2f);
                case "easeout": return Pow(t, 2f);

                case "ease_quad": return EaseInOut(t, 2f);
                case "easein_quad": return 1f - Pow(1f - t, 2f);
                case "easeout_quad": return Pow(t, 2f);

                case "ease_cubic": return EaseInOut(t, 3f);
                case "easein_cubic": return 1f - Pow(1f - t, 3f);
                case "easeout_cubic": return Pow(t, 3f);

                case "ease_quart": return EaseInOut(t, 4f);
                case "easein_quart": return 1f - Pow(1f - t, 4f);
                case "easeout_quart": return Pow(t, 4f);

                case "ease_quint": return EaseInOut(t, 5f);
                case "easein_quint": return 1f - Pow(1f - t, 5f);
                case "easeout_quint": return Pow(t, 5f);

                case "ease_sine": return 0.5f * (1f - (float)Math.Cos(Math.PI * t));
                case "easein_sine": return (float)Math.Sin(t * Math.PI * 0.5);
                case "easeout_sine": return 1f - (float)Math.Cos(t * Math.PI * 0.5);

                case "ease_circ": return t < 0.5f
                    ? 0.5f * (1f - (float)Math.Sqrt(1f - 4f * t * t))
                    : 0.5f * ((float)Math.Sqrt(1f - (2f * t - 2f) * (2f * t - 2f)) + 1f);
                case "easein_circ": return (float)Math.Sqrt(1f - (t - 1f) * (t - 1f));
                case "easeout_circ": return 1f - (float)Math.Sqrt(1f - t * t);

                case "ease_expo": return EaseInOut(t, 3f);
                case "easein_expo": return t == 0f ? 0f : (float)Math.Pow(2f, 10f * (t - 1f));
                case "easeout_expo": return t == 1f ? 1f : 1f - (float)Math.Pow(2f, -10f * t);

                default: return t;
            }
        }

        static float Pow(float value, float power) { return (float)Math.Pow(value, power); }

        static float EaseInOut(float t, float power)
        {
            return t < 0.5f
                ? 0.5f * Pow(2f * t, power)
                : 1f - 0.5f * Pow(2f * (1f - t), power);
        }
    }

    /// <summary>Blends two transform states.</summary>
    public static class TransformBlend
    {
        public static TransformState Lerp(TransformState a, TransformState b, float t)
        {
            var rv = b.Clone();

            rv.XPos = Mix(a.XPos, b.XPos, t);
            rv.YPos = Mix(a.YPos, b.YPos, t);
            rv.XAnchor = Mix(a.XAnchor, b.XAnchor, t);
            rv.YAnchor = Mix(a.YAnchor, b.YAnchor, t);
            rv.XZoom = Mix(a.XZoom, b.XZoom, t);
            rv.YZoom = Mix(a.YZoom, b.YZoom, t);
            rv.Zoom = Mix(a.Zoom, b.Zoom, t);
            rv.Rotate = Mix(a.Rotate, b.Rotate, t);
            rv.Alpha = Mix(a.Alpha, b.Alpha, t);

            // Alignment only interpolates when both ends actually use it.
            if (a.HasAlign && b.HasAlign)
            {
                rv.XAlign = Mix(a.XAlign, b.XAlign, t);
                rv.YAlign = Mix(a.YAlign, b.YAlign, t);
                rv.HasAlign = true;
            }

            return rv;
        }

        static float Mix(float a, float b, float t) { return a + (b - a) * t; }
    }
}
