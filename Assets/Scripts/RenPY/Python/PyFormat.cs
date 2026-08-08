using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RenPy.Python
{
    /// <summary>
    /// String formatting: the `%` operator and `str.format` / f-string field syntax.
    /// Ren'Py scripts use both heavily, including inside dialogue interpolation.
    /// </summary>
    public static class PyFormat
    {
        // ------------------------------------------------------------ "fmt" % args

        public static string Percent(string fmt, object args)
        {
            var mapping = args as PyDict;
            IList<object> positional = null;
            int argIndex = 0;

            if (mapping == null)
            {
                var t = args as PyTuple;
                if (t != null) positional = t.Items;
                else positional = new object[] { args };
            }

            var sb = new StringBuilder(fmt.Length + 16);
            int i = 0;

            while (i < fmt.Length)
            {
                char c = fmt[i];
                if (c != '%') { sb.Append(c); i++; continue; }

                i++;
                if (i >= fmt.Length) { sb.Append('%'); break; }
                if (fmt[i] == '%') { sb.Append('%'); i++; continue; }

                // %(name)s
                object value = null;
                bool haveValue = false;
                if (fmt[i] == '(')
                {
                    int close = fmt.IndexOf(')', i);
                    if (close < 0) throw new PyError("ValueError", "incomplete format key");
                    string key = fmt.Substring(i + 1, close - i - 1);
                    if (mapping == null) throw new PyError("TypeError", "format requires a mapping");
                    if (!mapping.TryGet(key, out value)) throw new PyError("KeyError", PyOps.Repr(key));
                    haveValue = true;
                    i = close + 1;
                }

                // flags
                bool leftAlign = false, plusSign = false, spaceSign = false, zeroPad = false, altForm = false;
                while (i < fmt.Length)
                {
                    char f = fmt[i];
                    if (f == '-') leftAlign = true;
                    else if (f == '+') plusSign = true;
                    else if (f == ' ') spaceSign = true;
                    else if (f == '0') zeroPad = true;
                    else if (f == '#') altForm = true;
                    else break;
                    i++;
                }

                // width
                int width = 0;
                if (i < fmt.Length && fmt[i] == '*')
                {
                    width = (int)Py.ToInt(NextArg(positional, ref argIndex));
                    i++;
                }
                else
                {
                    while (i < fmt.Length && char.IsDigit(fmt[i])) { width = width * 10 + (fmt[i] - '0'); i++; }
                }

                // precision
                int precision = -1;
                if (i < fmt.Length && fmt[i] == '.')
                {
                    i++;
                    precision = 0;
                    if (i < fmt.Length && fmt[i] == '*')
                    {
                        precision = (int)Py.ToInt(NextArg(positional, ref argIndex));
                        i++;
                    }
                    else
                    {
                        while (i < fmt.Length && char.IsDigit(fmt[i])) { precision = precision * 10 + (fmt[i] - '0'); i++; }
                    }
                }

                // length modifiers, ignored
                while (i < fmt.Length && (fmt[i] == 'l' || fmt[i] == 'h' || fmt[i] == 'L')) i++;

                if (i >= fmt.Length) throw new PyError("ValueError", "incomplete format");
                char conv = fmt[i++];

                if (!haveValue) value = NextArg(positional, ref argIndex);

                string text = ConvertOne(conv, value, precision, altForm, plusSign, spaceSign);
                sb.Append(Pad(text, width, leftAlign, zeroPad && !leftAlign && IsNumeric(conv)));
            }

            return sb.ToString();
        }

        static bool IsNumeric(char conv)
        {
            return conv == 'd' || conv == 'i' || conv == 'u' || conv == 'f' || conv == 'F' ||
                   conv == 'e' || conv == 'E' || conv == 'g' || conv == 'G' ||
                   conv == 'x' || conv == 'X' || conv == 'o';
        }

        static object NextArg(IList<object> positional, ref int index)
        {
            if (positional == null || index >= positional.Count)
                throw new PyError("TypeError", "not enough arguments for format string");
            return positional[index++];
        }

        static string ConvertOne(char conv, object value, int precision, bool altForm, bool plusSign, bool spaceSign)
        {
            string text;
            switch (conv)
            {
                case 's':
                    text = PyOps.ToStr(value);
                    if (precision >= 0 && text.Length > precision) text = text.Substring(0, precision);
                    return text;

                case 'r':
                    text = PyOps.Repr(value);
                    if (precision >= 0 && text.Length > precision) text = text.Substring(0, precision);
                    return text;

                case 'd':
                case 'i':
                case 'u':
                    return SignPrefix(Py.ToDouble(value) < 0, plusSign, spaceSign) +
                           Math.Abs(Py.ToInt(value)).ToString(CultureInfo.InvariantCulture);

                case 'f':
                case 'F':
                {
                    double d = Py.ToDouble(value);
                    int p = precision < 0 ? 6 : precision;
                    return SignPrefix(d < 0, plusSign, spaceSign) +
                           Math.Abs(d).ToString("F" + p, CultureInfo.InvariantCulture);
                }

                case 'e':
                case 'E':
                {
                    double d = Py.ToDouble(value);
                    int p = precision < 0 ? 6 : precision;
                    text = Math.Abs(d).ToString((conv == 'e' ? "e" : "E") + p, CultureInfo.InvariantCulture);
                    text = FixExponent(text);
                    return SignPrefix(d < 0, plusSign, spaceSign) + text;
                }

                case 'g':
                case 'G':
                {
                    double d = Py.ToDouble(value);
                    int p = precision < 0 ? 6 : (precision == 0 ? 1 : precision);
                    text = Math.Abs(d).ToString((conv == 'g' ? "G" : "G") + p, CultureInfo.InvariantCulture);
                    if (conv == 'g') text = text.ToLowerInvariant();
                    text = FixExponent(text);
                    return SignPrefix(d < 0, plusSign, spaceSign) + text;
                }

                case 'x':
                    text = Math.Abs(Py.ToInt(value)).ToString("x", CultureInfo.InvariantCulture);
                    return SignPrefix(Py.ToInt(value) < 0, plusSign, spaceSign) + (altForm ? "0x" : "") + text;

                case 'X':
                    text = Math.Abs(Py.ToInt(value)).ToString("X", CultureInfo.InvariantCulture);
                    return SignPrefix(Py.ToInt(value) < 0, plusSign, spaceSign) + (altForm ? "0X" : "") + text;

                case 'o':
                    text = Convert.ToString(Math.Abs(Py.ToInt(value)), 8);
                    return SignPrefix(Py.ToInt(value) < 0, plusSign, spaceSign) + (altForm ? "0o" : "") + text;

                case 'c':
                    if (value is string) return (string)value;
                    return ((char)Py.ToInt(value)).ToString();

                default:
                    throw new PyError("ValueError", "unsupported format character '" + conv + "'");
            }
        }

        static string SignPrefix(bool negative, bool plusSign, bool spaceSign)
        {
            if (negative) return "-";
            if (plusSign) return "+";
            if (spaceSign) return " ";
            return "";
        }

        /// <summary>.NET writes three exponent digits where Python writes two.</summary>
        static string FixExponent(string s)
        {
            int e = s.IndexOfAny(new[] { 'e', 'E' });
            if (e < 0) return s;
            int p = e + 1;
            if (p >= s.Length) return s;
            char sign = '+';
            if (s[p] == '+' || s[p] == '-') { sign = s[p]; p++; }
            string digits = s.Substring(p).TrimStart('0');
            if (digits.Length < 2) digits = digits.PadLeft(2, '0');
            return s.Substring(0, e + 1) + sign + digits;
        }

        static string Pad(string text, int width, bool leftAlign, bool zeroPad)
        {
            if (text.Length >= width) return text;
            if (leftAlign) return text.PadRight(width);
            if (zeroPad)
            {
                // Zero padding goes after any sign.
                if (text.Length > 0 && (text[0] == '-' || text[0] == '+' || text[0] == ' '))
                    return text[0] + text.Substring(1).PadLeft(width - 1, '0');
                return text.PadLeft(width, '0');
            }
            return text.PadLeft(width);
        }

        // ------------------------------------------------------------ format spec

        /// <summary>
        /// Applies a `str.format` / f-string format spec such as "&gt;10.2f" to a value.
        /// </summary>
        public static string FormatSpec(object value, string spec)
        {
            if (string.IsNullOrEmpty(spec)) return PyOps.ToStr(value);

            int i = 0;
            char fill = ' ';
            char align = '\0';

            if (spec.Length >= 2 && IsAlign(spec[1])) { fill = spec[0]; align = spec[1]; i = 2; }
            else if (spec.Length >= 1 && IsAlign(spec[0])) { align = spec[0]; i = 1; }

            char sign = '\0';
            if (i < spec.Length && (spec[i] == '+' || spec[i] == '-' || spec[i] == ' ')) sign = spec[i++];

            bool altForm = false;
            if (i < spec.Length && spec[i] == '#') { altForm = true; i++; }

            bool zeroPad = false;
            if (i < spec.Length && spec[i] == '0') { zeroPad = true; i++; if (align == '\0') align = '='; }

            int width = 0;
            while (i < spec.Length && char.IsDigit(spec[i])) { width = width * 10 + (spec[i] - '0'); i++; }

            bool comma = false;
            if (i < spec.Length && spec[i] == ',') { comma = true; i++; }

            int precision = -1;
            if (i < spec.Length && spec[i] == '.')
            {
                i++;
                precision = 0;
                while (i < spec.Length && char.IsDigit(spec[i])) { precision = precision * 10 + (spec[i] - '0'); i++; }
            }

            char type = i < spec.Length ? spec[i] : '\0';

            string text;
            if (type == '\0')
            {
                text = PyOps.ToStr(value);
                if (Py.IsNumber(value) && precision >= 0) text = ConvertOne('f', value, precision, altForm, sign == '+', sign == ' ');
            }
            else if (type == 's')
            {
                text = PyOps.ToStr(value);
                if (precision >= 0 && text.Length > precision) text = text.Substring(0, precision);
            }
            else if (type == '%')
            {
                double d = Py.ToDouble(value) * 100.0;
                text = ConvertOne('f', d, precision < 0 ? 6 : precision, altForm, sign == '+', sign == ' ') + "%";
            }
            else if (type == 'n')
            {
                text = ConvertOne('g', value, precision, altForm, sign == '+', sign == ' ');
            }
            else
            {
                text = ConvertOne(type, value, precision, altForm, sign == '+', sign == ' ');
            }

            if (comma) text = InsertThousands(text);

            if (text.Length >= width) return text;

            if (align == '\0') align = Py.IsNumber(value) && !(value is bool) ? '>' : '<';

            int padCount = width - text.Length;
            switch (align)
            {
                case '<': return text + new string(fill, padCount);
                case '>': return new string(fill, padCount) + text;
                case '^':
                {
                    int left = padCount / 2;
                    return new string(fill, left) + text + new string(fill, padCount - left);
                }
                case '=':
                {
                    char padChar = zeroPad ? '0' : fill;
                    if (text.Length > 0 && (text[0] == '-' || text[0] == '+' || text[0] == ' '))
                        return text[0] + new string(padChar, padCount) + text.Substring(1);
                    return new string(padChar, padCount) + text;
                }
                default: return text;
            }
        }

        static bool IsAlign(char c) { return c == '<' || c == '>' || c == '^' || c == '='; }

        static string InsertThousands(string text)
        {
            int start = 0;
            if (text.Length > 0 && (text[0] == '-' || text[0] == '+' || text[0] == ' ')) start = 1;
            int dot = text.IndexOf('.');
            int end = dot < 0 ? text.Length : dot;

            var sb = new StringBuilder();
            sb.Append(text, 0, start);
            int digits = end - start;
            for (int i = 0; i < digits; i++)
            {
                if (i > 0 && (digits - i) % 3 == 0) sb.Append(',');
                sb.Append(text[start + i]);
            }
            sb.Append(text, end, text.Length - end);
            return sb.ToString();
        }
    }
}
