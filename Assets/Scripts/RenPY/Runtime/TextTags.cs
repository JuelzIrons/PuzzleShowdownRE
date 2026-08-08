using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RenPy.Runtime
{
    /// <summary>
    /// The result of stripping Ren'Py control tags out of a line of dialogue.
    /// </summary>
    public sealed class ParsedText
    {
        /// <summary>Display text, with styling tags translated to TextMeshPro markup.</summary>
        public string Text = "";
        /// <summary>Seconds to wait before auto-advancing; negative means wait for input.</summary>
        public float AutoAdvance = -1f;
        /// <summary>True when {nw} asked to continue without waiting for a click.</summary>
        public bool NoWait;
        /// <summary>Characters-per-second override from {cps=...}.</summary>
        public float Cps = -1f;
        /// <summary>True when the line should not be added to history.</summary>
        public bool NoHistory;
    }

    /// <summary>
    /// Translates Ren'Py's text tags. Styling tags become TextMeshPro markup so the
    /// UI layer can render them directly; control tags ({p}, {w}, {nw}) are lifted
    /// out and returned to the engine.
    /// </summary>
    public static class TextTags
    {
        public static ParsedText Parse(string source)
        {
            var rv = new ParsedText();
            if (string.IsNullOrEmpty(source)) return rv;

            var sb = new StringBuilder(source.Length);
            // Tracks styling tags we opened, so {/b} closes the right markup.
            var open = new List<string>();

            int i = 0;
            while (i < source.Length)
            {
                char c = source[i];

                if (c == '{')
                {
                    // "{{" is a literal brace.
                    if (i + 1 < source.Length && source[i + 1] == '{') { sb.Append('{'); i += 2; continue; }

                    int close = source.IndexOf('}', i);
                    if (close < 0) { sb.Append(c); i++; continue; }

                    string body = source.Substring(i + 1, close - i - 1);
                    i = close + 1;

                    HandleTag(body, sb, open, rv);
                    continue;
                }

                if (c == '[')
                {
                    // Ren'Py interpolation is resolved before we get here; "[[" is literal.
                    if (i + 1 < source.Length && source[i + 1] == '[') { sb.Append('['); i += 2; continue; }
                    sb.Append(c);
                    i++;
                    continue;
                }

                sb.Append(c);
                i++;
            }

            // Close anything the line left open so markup does not leak.
            for (int j = open.Count - 1; j >= 0; j--) sb.Append("</").Append(open[j]).Append('>');

            rv.Text = sb.ToString();
            return rv;
        }

        static void HandleTag(string body, StringBuilder sb, List<string> open, ParsedText rv)
        {
            bool closing = body.StartsWith("/", StringComparison.Ordinal);
            if (closing) body = body.Substring(1);

            string name = body;
            string value = null;
            int eq = body.IndexOf('=');
            if (eq >= 0)
            {
                name = body.Substring(0, eq);
                value = body.Substring(eq + 1);
            }

            name = name.Trim().ToLowerInvariant();

            if (closing)
            {
                string markup = CloseMarkup(name);
                if (markup != null)
                {
                    sb.Append(markup);
                    int idx = open.LastIndexOf(MarkupName(name));
                    if (idx >= 0) open.RemoveAt(idx);
                }
                return;
            }

            switch (name)
            {
                // ---------------------------------------------------- control tags
                case "p":
                    // {p} is a paragraph break that also waits.
                    sb.Append('\n');
                    rv.AutoAdvance = ParseFloat(value, -1f);
                    return;

                case "w":
                    rv.AutoAdvance = ParseFloat(value, -1f);
                    return;

                case "nw":
                    rv.NoWait = true;
                    return;

                case "cps":
                    rv.Cps = ParseFloat(value, -1f);
                    return;

                case "nh":
                    rv.NoHistory = true;
                    return;

                case "fast":
                case "done":
                case "clear":
                    return;

                case "wait":
                    rv.AutoAdvance = ParseFloat(value, -1f);
                    return;

                // ---------------------------------------------------- styling tags
                case "b": sb.Append("<b>"); open.Add("b"); return;
                case "i": sb.Append("<i>"); open.Add("i"); return;
                case "u": sb.Append("<u>"); open.Add("u"); return;
                case "s": sb.Append("<s>"); open.Add("s"); return;

                case "color":
                    sb.Append("<color=").Append(NormalizeColor(value)).Append('>');
                    open.Add("color");
                    return;

                case "outlinecolor":
                    return;

                case "size":
                {
                    // A leading +/- makes the size relative; TMP understands both forms.
                    string v = (value ?? "").Trim();
                    sb.Append("<size=").Append(v.Length == 0 ? "100%" : v).Append('>');
                    open.Add("size");
                    return;
                }

                case "font":
                    // Font swaps need an asset the UI layer resolves; markup alone cannot.
                    return;

                case "alpha":
                {
                    float a = ParseFloat(value, 1f);
                    int alpha = (int)Math.Round(Math.Max(0f, Math.Min(1f, a)) * 255f);
                    sb.Append("<alpha=#").Append(alpha.ToString("X2")).Append('>');
                    open.Add("alpha");
                    return;
                }

                case "k":
                case "kerning":
                    sb.Append("<cspace=").Append((value ?? "0").Trim()).Append('>');
                    open.Add("cspace");
                    return;

                case "space":
                    sb.Append("<space=").Append((value ?? "0").Trim()).Append('>');
                    return;

                case "vspace":
                    sb.Append('\n');
                    return;

                case "plain":
                case "noalt":
                case "alt":
                    return;

                default:
                    // Unknown tags are dropped rather than shown as literal text.
                    return;
            }
        }

        static string MarkupName(string tag)
        {
            switch (tag)
            {
                case "k":
                case "kerning": return "cspace";
                default: return tag;
            }
        }

        static string CloseMarkup(string name)
        {
            switch (name)
            {
                case "b": return "</b>";
                case "i": return "</i>";
                case "u": return "</u>";
                case "s": return "</s>";
                case "color": return "</color>";
                case "size": return "</size>";
                case "alpha": return "</alpha>";
                case "k":
                case "kerning": return "</cspace>";
                default: return null;
            }
        }

        static string NormalizeColor(string value)
        {
            if (string.IsNullOrEmpty(value)) return "#ffffff";
            value = value.Trim();
            if (!value.StartsWith("#", StringComparison.Ordinal)) value = "#" + value;

            // Ren'Py accepts #rgb and #rgba shorthand; TMP wants the long form.
            string digits = value.Substring(1);
            if (digits.Length == 3 || digits.Length == 4)
            {
                var sb = new StringBuilder("#");
                foreach (char c in digits) sb.Append(c).Append(c);
                return sb.ToString();
            }

            return value;
        }

        static float ParseFloat(string value, float fallback)
        {
            if (string.IsNullOrEmpty(value)) return fallback;
            float rv;
            return float.TryParse(value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out rv)
                ? rv : fallback;
        }

        /// <summary>
        /// Substitutes Ren'Py's square-bracket interpolation, e.g. "[player_name]".
        /// </summary>
        public static string Interpolate(string source, Func<string, string> resolve)
        {
            if (string.IsNullOrEmpty(source) || source.IndexOf('[') < 0) return source;

            var sb = new StringBuilder(source.Length);
            int i = 0;

            while (i < source.Length)
            {
                char c = source[i];

                if (c == '[')
                {
                    if (i + 1 < source.Length && source[i + 1] == '[') { sb.Append("[["); i += 2; continue; }

                    int close = source.IndexOf(']', i);
                    if (close < 0) { sb.Append(c); i++; continue; }

                    string expr = source.Substring(i + 1, close - i - 1);
                    i = close + 1;

                    // A trailing !r/!t/!q conversion flag selects the formatting.
                    int bang = expr.LastIndexOf('!');
                    if (bang > 0) expr = expr.Substring(0, bang);

                    // A format spec after ':' is applied by the caller's resolver.
                    sb.Append(resolve(expr.Trim()));
                    continue;
                }

                sb.Append(c);
                i++;
            }

            return sb.ToString();
        }
    }
}
