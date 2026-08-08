using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using RenPy.Ast;
using RenPy.Python;

namespace RenPy.Runtime
{
    /// <summary>
    /// Executes Ren'Py's built-in "user statements" — the ones defined in
    /// renpy/common/000statements.rpy rather than the core grammar: play, queue,
    /// stop, pause, window, voice, and show/hide screen.
    ///
    /// These are stored in the .rpyc as their source line, so they are re-parsed here.
    /// </summary>
    public static class UserStatements
    {
        public static void Execute(RenPyEngine engine, UserStatementNode node)
        {
            var words = Tokenize(node.Line);
            if (words.Count == 0) return;

            switch (words[0])
            {
                case "play": Play(engine, words, false); return;
                case "queue": Play(engine, words, true); return;
                case "stop": Stop(engine, words); return;
                case "pause": Pause(engine, words); return;
                case "show": ShowHideScreen(engine, words, true); return;
                case "hide": ShowHideScreen(engine, words, false); return;
                case "voice": Voice(engine, words); return;
                case "window": return;   // window show/hide only affects the textbox
                case "nvl": return;
                default:
                    engine.Host.Log("[statement] unhandled: " + node.Line);
                    return;
            }
        }

        // ------------------------------------------------------------ handlers

        /// <summary>`play &lt;channel&gt; &lt;file&gt; [fadein n] [fadeout n] [loop|noloop] [volume n]`</summary>
        static void Play(RenPyEngine engine, List<string> words, bool queue)
        {
            if (words.Count < 3) return;

            string channel = words[1];
            string file = Unquote(Resolve(engine, words[2]));
            if (string.IsNullOrEmpty(file)) return;

            float fadeIn = 0f;
            float volume = -1f;
            bool loop = channel == "music" || channel == "ambient";

            for (int i = 3; i < words.Count; i++)
            {
                switch (words[i])
                {
                    case "fadein": fadeIn = ParseFloat(NextWord(words, ref i)); break;
                    case "fadeout": ParseFloat(NextWord(words, ref i)); break;
                    case "volume": volume = ParseFloat(NextWord(words, ref i)); break;
                    case "loop": loop = true; break;
                    case "noloop": loop = false; break;
                    case "from": NextWord(words, ref i); break;
                    case "if_changed": break;
                }
            }

            if (volume >= 0f) engine.Host.SetAudioVolume(channel, volume);

            var spec = AudioSpec.Parse(file);
            if (queue) engine.Host.QueueAudio(channel, spec);
            else engine.Host.PlayAudio(channel, spec, fadeIn, loop);
        }

        /// <summary>`stop &lt;channel&gt; [fadeout n]`</summary>
        static void Stop(RenPyEngine engine, List<string> words)
        {
            if (words.Count < 2) return;

            string channel = words[1];
            float fadeOut = 0f;

            for (int i = 2; i < words.Count; i++)
                if (words[i] == "fadeout") fadeOut = ParseFloat(NextWord(words, ref i));

            engine.Host.StopAudio(channel, fadeOut);
        }

        /// <summary>`pause [expression] [hard]`</summary>
        static void Pause(RenPyEngine engine, List<string> words)
        {
            float seconds = -1f;
            bool hard = false;

            var parts = new List<string>();
            for (int i = 1; i < words.Count; i++)
            {
                if (words[i] == "hard") { hard = true; continue; }
                parts.Add(words[i]);
            }

            if (parts.Count > 0)
            {
                string expression = string.Join(" ", parts.ToArray());
                object value = engine.SafeEval(expression);
                if (value != null)
                {
                    try { seconds = (float)Py.ToDouble(value); }
                    catch (PyError) { seconds = -1f; }
                }
            }

            engine.Host.Pause(seconds, hard);
        }

        /// <summary>`show screen name(args)` / `hide screen name`</summary>
        static void ShowHideScreen(RenPyEngine engine, List<string> words, bool show)
        {
            if (words.Count < 3 || words[1] != "screen") return;

            string name = words[2];

            // Strip any call arguments; the screen system reads them from the store.
            int paren = name.IndexOf('(');
            if (paren > 0) name = name.Substring(0, paren);

            if (show) engine.Screens.Show(name, null);
            else engine.Screens.Hide(name);
        }

        /// <summary>`voice "file"`</summary>
        static void Voice(RenPyEngine engine, List<string> words)
        {
            if (words.Count < 2) return;
            string file = Unquote(Resolve(engine, words[1]));
            if (!string.IsNullOrEmpty(file)) engine.Host.PlayAudio("voice", AudioSpec.Parse(file), 0f, false);
        }

        // ------------------------------------------------------------ helpers

        static string NextWord(List<string> words, ref int i)
        {
            i++;
            return i < words.Count ? words[i] : null;
        }

        static float ParseFloat(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0f;
            float rv;
            return float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out rv) ? rv : 0f;
        }

        static string Unquote(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            if (text.Length >= 2 && (text[0] == '"' || text[0] == '\'') && text[text.Length - 1] == text[0])
                return text.Substring(1, text.Length - 2);
            return text;
        }

        /// <summary>A bare word may be a variable holding the real path.</summary>
        static string Resolve(RenPyEngine engine, string word)
        {
            if (string.IsNullOrEmpty(word)) return word;
            if (word[0] == '"' || word[0] == '\'') return word;

            object value = engine.SafeEval(word);
            return value == null ? word : PyOps.ToStr(value);
        }

        /// <summary>Splits a statement line, keeping quoted strings and (...) groups whole.</summary>
        static List<string> Tokenize(string line)
        {
            var rv = new List<string>();
            if (string.IsNullOrEmpty(line)) return rv;

            int i = 0;
            while (i < line.Length)
            {
                while (i < line.Length && char.IsWhiteSpace(line[i])) i++;
                if (i >= line.Length) break;

                char c = line[i];
                var sb = new StringBuilder();

                if (c == '"' || c == '\'')
                {
                    char quote = c;
                    sb.Append(quote);
                    i++;
                    while (i < line.Length && line[i] != quote)
                    {
                        if (line[i] == '\\' && i + 1 < line.Length) { sb.Append(line[i]); i++; }
                        sb.Append(line[i]);
                        i++;
                    }
                    if (i < line.Length) { sb.Append(quote); i++; }
                    rv.Add(sb.ToString());
                    continue;
                }

                if (c == '(')
                {
                    int depth = 0;
                    while (i < line.Length)
                    {
                        if (line[i] == '(') depth++;
                        else if (line[i] == ')') depth--;
                        sb.Append(line[i]);
                        i++;
                        if (depth == 0) break;
                    }
                    // Drop the enclosing parentheses so the contents can be evaluated.
                    string inner = sb.ToString();
                    if (inner.Length >= 2) inner = inner.Substring(1, inner.Length - 2);
                    rv.Add(inner);
                    continue;
                }

                while (i < line.Length && !char.IsWhiteSpace(line[i])) { sb.Append(line[i]); i++; }
                rv.Add(sb.ToString());
            }

            return rv;
        }
    }
}
