using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using RenPy.Python;

namespace RenPy.Runtime
{
    /// <summary>
    /// Encodes Python values as flat strings so they can be written to PlayerPrefs
    /// or a save file and come back as the same type.
    ///
    /// Each value carries a one-character type tag, so an int does not return as a
    /// string and a list does not return as its repr. Values that cannot round-trip
    /// (class instances, displayables) are refused rather than silently mangled.
    /// </summary>
    public static class ValueCodec
    {
        // ---------------------------------------------------------------- encoding

        public static bool Encode(object value, out string encoded)
        {
            encoded = null;

            if (value == null) { encoded = "n"; return true; }

            if (value is bool) { encoded = "b" + ((bool)value ? "1" : "0"); return true; }

            if (Py.IsInt(value))
            {
                encoded = "i" + Py.ToInt(value).ToString(CultureInfo.InvariantCulture);
                return true;
            }

            if (Py.IsFloat(value))
            {
                encoded = "f" + Py.ToDouble(value).ToString("R", CultureInfo.InvariantCulture);
                return true;
            }

            var text = value as string;
            if (text != null) { encoded = "s" + text; return true; }

            var list = value as PyList;
            if (list != null) return EncodeSequence('l', list.Items, out encoded);

            var tuple = value as PyTuple;
            if (tuple != null) return EncodeSequence('t', tuple.Items, out encoded);

            var set = value as PySet;
            if (set != null) return EncodeSequence('e', new List<object>(set.Items), out encoded);

            var dict = value as PyDict;
            if (dict != null)
            {
                var flat = new List<object>();
                foreach (var kv in dict) { flat.Add(kv.Key); flat.Add(kv.Value); }
                return EncodeSequence('d', flat, out encoded);
            }

            // Anything else (class instances, displayables) is not portable.
            return false;
        }

        public static bool EncodeSequence(char tag, IList<object> items, out string encoded)
        {
            var sb = new StringBuilder();
            sb.Append(tag);

            for (int i = 0; i < items.Count; i++)
            {
                string part;
                // A single unencodable element drops the whole collection rather than
                // silently storing a shorter one.
                if (!Encode(items[i], out part)) { encoded = null; return false; }

                if (i > 0) sb.Append('');
                sb.Append(Escape(part));
            }

            encoded = sb.ToString();
            return true;
        }

        public static bool Decode(string encoded, out object value)
        {
            value = null;
            if (string.IsNullOrEmpty(encoded)) return false;

            char tag = encoded[0];
            string body = encoded.Substring(1);

            switch (tag)
            {
                case 'n': value = null; return true;
                case 'b': value = body == "1"; return true;
                case 's': value = body; return true;

                case 'i':
                {
                    long parsed;
                    if (!long.TryParse(body, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed)) return false;
                    value = parsed;
                    return true;
                }

                case 'f':
                {
                    double parsed;
                    if (!double.TryParse(body, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed)) return false;
                    value = parsed;
                    return true;
                }

                case 'l':
                case 't':
                case 'e':
                case 'd':
                {
                    var items = new List<object>();
                    if (body.Length > 0)
                    {
                        foreach (var part in body.Split(''))
                        {
                            object item;
                            if (!Decode(Unescape(part), out item)) return false;
                            items.Add(item);
                        }
                    }

                    if (tag == 'l') value = new PyList(items);
                    else if (tag == 't') value = new PyTuple(items.ToArray());
                    else if (tag == 'e') value = new PySet(items);
                    else
                    {
                        var dict = new PyDict();
                        for (int i = 0; i + 1 < items.Count; i += 2) dict.Set(items[i], items[i + 1]);
                        value = dict;
                    }
                    return true;
                }

                default: return false;
            }
        }

        // Nested collections reuse the separators, so they are escaped one level deep.
        static string Escape(string s) { return s.Replace("", "0").Replace("", "1"); }
        static string Unescape(string s) { return s.Replace("1", "").Replace("0", ""); }
    }
}
