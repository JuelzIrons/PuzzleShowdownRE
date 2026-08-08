using System;
using System.Globalization;
using System.Text;
using RenPy.Python;

namespace RenPy.Runtime
{
    /// <summary>
    /// Normalises the several shapes Ren'Py accepts for a colour into "#rrggbbaa".
    ///
    /// Games write colours as hex strings ("#fff", "#ff0000"), as RGB/RGBA tuples of
    /// bytes ((0, 0, 0, 255)) or of floats ((0.0, 0.0, 0.0, 1.0)), or as a packed
    /// integer. Stringifying a tuple and hoping a HTML colour parser copes produces a
    /// silent wrong colour, so it is converted here instead.
    /// </summary>
    public static class ColorValue
    {
        /// <summary>Returns "#rrggbbaa", or null when the value is not a colour.</summary>
        public static string Normalize(object value)
        {
            if (value == null) return null;

            var text = value as string;
            if (text != null) return FromString(text);

            var tuple = value as PyTuple;
            if (tuple != null) return FromComponents(tuple.Items, tuple.Items.Length);

            var list = value as PyList;
            if (list != null) return FromComponents(list.Items, list.Items.Count);

            // A Color() displayable carries its own normalised value.
            var displayable = value as Displayable;
            if (displayable != null && displayable.Color != null) return displayable.Color;

            if (Py.IsInt(value))
            {
                long packed = Py.ToInt(value);
                return "#" + (packed & 0xFFFFFF).ToString("x6", CultureInfo.InvariantCulture) + "ff";
            }

            return null;
        }

        static string FromString(string text)
        {
            text = text.Trim();
            if (text.Length == 0) return null;

            if (text[0] != '#') text = "#" + text;

            string digits = text.Substring(1);

            // Expand #rgb and #rgba shorthand.
            if (digits.Length == 3 || digits.Length == 4)
            {
                var sb = new StringBuilder("#");
                foreach (char c in digits) sb.Append(c).Append(c);
                digits = sb.ToString().Substring(1);
            }

            if (digits.Length == 6) digits += "ff";
            if (digits.Length != 8) return null;

            foreach (char c in digits)
                if (!Uri.IsHexDigit(c)) return null;

            return "#" + digits.ToLowerInvariant();
        }

        static string FromComponents(System.Collections.Generic.IList<object> items, int count)
        {
            if (count < 3) return null;

            // Floats in 0..1 and bytes in 0..255 are both common; tell them apart by
            // looking for a fractional component.
            bool fractional = false;
            for (int i = 0; i < count && i < 4; i++)
            {
                if (!Py.IsFloat(items[i])) continue;
                double d = Py.ToDouble(items[i]);
                if (d != Math.Floor(d)) { fractional = true; break; }
            }

            var sb = new StringBuilder("#");
            for (int i = 0; i < 4; i++)
            {
                double raw = i < count ? SafeDouble(items[i], i == 3 ? (fractional ? 1.0 : 255.0) : 0.0)
                                       : (fractional ? 1.0 : 255.0);

                double scaled = fractional ? raw * 255.0 : raw;
                int component = (int)Math.Round(Math.Max(0.0, Math.Min(255.0, scaled)));
                sb.Append(component.ToString("x2", CultureInfo.InvariantCulture));
            }

            return sb.ToString();
        }

        static double SafeDouble(object value, double fallback)
        {
            try { return Py.ToDouble(value); }
            catch (PyError) { return fallback; }
        }
    }
}
