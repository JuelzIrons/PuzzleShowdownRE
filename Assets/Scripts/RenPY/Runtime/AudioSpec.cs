using System;
using System.Globalization;

namespace RenPy.Runtime
{
    /// <summary>
    /// A Ren'Py audio filename with its optional playback clause, e.g.
    /// `&lt;from 1.67 to 4.2 loop 0&gt;audio/scene.mp3`.
    /// </summary>
    public sealed class AudioSpec
    {
        public string Path;
        /// <summary>Start offset in seconds.</summary>
        public float From;
        /// <summary>End offset in seconds, or negative to play to the end.</summary>
        public float To = -1f;
        /// <summary>Loop restart point in seconds, or negative for no loop clause.</summary>
        public float LoopPoint = -1f;
        public bool HasLoopClause;

        public static AudioSpec Parse(string raw)
        {
            var rv = new AudioSpec();
            if (string.IsNullOrEmpty(raw)) return rv;

            raw = raw.Trim();

            if (raw.Length > 0 && raw[0] == '<')
            {
                int close = raw.IndexOf('>');
                if (close > 0)
                {
                    ParseClause(raw.Substring(1, close - 1), rv);
                    raw = raw.Substring(close + 1);
                }
            }

            rv.Path = raw;
            return rv;
        }

        static void ParseClause(string clause, AudioSpec spec)
        {
            var words = clause.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                switch (words[i].ToLowerInvariant())
                {
                    case "from":
                        spec.From = Number(words, ++i);
                        break;
                    case "to":
                        spec.To = Number(words, ++i);
                        break;
                    case "loop":
                        spec.HasLoopClause = true;
                        spec.LoopPoint = Number(words, ++i);
                        break;
                    case "noloop":
                        spec.HasLoopClause = false;
                        spec.LoopPoint = -1f;
                        break;
                    case "silence":
                        // A silence clause plays nothing for the given duration.
                        spec.Path = null;
                        spec.To = Number(words, ++i);
                        break;
                }
            }
        }

        static float Number(string[] words, int index)
        {
            if (index >= words.Length) return 0f;
            float rv;
            return float.TryParse(words[index], NumberStyles.Float, CultureInfo.InvariantCulture, out rv) ? rv : 0f;
        }

        public override string ToString()
        {
            return Path + (From > 0f ? " @" + From.ToString("0.###", CultureInfo.InvariantCulture) : "");
        }
    }
}
