using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RenPy.Python
{
    /// <summary>
    /// Methods on the built-in types (str, list, dict, set). Resolved on demand by
    /// <see cref="PyOps.TryGetAttr"/>, which keeps the value model free of per-instance
    /// method tables.
    /// </summary>
    public static class PyBuiltinMethods
    {
        public static object Lookup(object self, string name)
        {
            if (self is string) return StrMethod((string)self, name);
            if (self is PyList) return ListMethod((PyList)self, name);
            if (self is PyDict) return DictMethod((PyDict)self, name);
            if (self is PySet) return SetMethod((PySet)self, name);
            if (self is PyTuple) return TupleMethod((PyTuple)self, name);
            return null;
        }

        static PyNative N(string name, Func<object[], PyDict, object> fn) { return new PyNative(name, fn); }

        static object Arg(object[] a, int i, object dflt = null) { return i < a.Length ? a[i] : dflt; }

        static object Kw(PyDict kwargs, string name, object dflt)
        {
            if (kwargs == null) return dflt;
            object v;
            return kwargs.TryGet(name, out v) ? v : dflt;
        }

        // ------------------------------------------------------------ str

        static object StrMethod(string s, string name)
        {
            switch (name)
            {
                case "format": return N(name, (a, k) => Format(s, a, k));
                case "format_map": return N(name, (a, k) => Format(s, Py.EmptyArgs, (PyDict)a[0]));

                case "join": return N(name, (a, k) =>
                {
                    var sb = new StringBuilder();
                    bool first = true;
                    foreach (var o in PyOps.Iterate(a[0]))
                    {
                        if (!first) sb.Append(s);
                        first = false;
                        sb.Append(PyOps.ToStr(o));
                    }
                    return sb.ToString();
                });

                case "split": return N(name, (a, k) => Split(s, Arg(a, 0), (int)Py.ToInt(Arg(a, 1, Kw(k, "maxsplit", -1L))), false));
                case "rsplit": return N(name, (a, k) => Split(s, Arg(a, 0), (int)Py.ToInt(Arg(a, 1, Kw(k, "maxsplit", -1L))), true));

                case "splitlines": return N(name, (a, k) =>
                {
                    bool keepends = Py.Truthy(Arg(a, 0, Kw(k, "keepends", false)));
                    var rv = new PyList();
                    int start = 0;
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (s[i] != '\n' && s[i] != '\r') continue;
                        int end = i;
                        int next = i + 1;
                        if (s[i] == '\r' && next < s.Length && s[next] == '\n') next++;
                        rv.Items.Add(keepends ? s.Substring(start, next - start) : s.Substring(start, end - start));
                        start = next;
                        i = next - 1;
                    }
                    if (start < s.Length) rv.Items.Add(s.Substring(start));
                    return rv;
                });

                case "strip": return N(name, (a, k) => Trim(s, Arg(a, 0), true, true));
                case "lstrip": return N(name, (a, k) => Trim(s, Arg(a, 0), true, false));
                case "rstrip": return N(name, (a, k) => Trim(s, Arg(a, 0), false, true));

                case "replace": return N(name, (a, k) =>
                {
                    string oldStr = PyOps.ToStr(a[0]), newStr = PyOps.ToStr(a[1]);
                    int count = a.Length > 2 ? (int)Py.ToInt(a[2]) : -1;
                    if (oldStr.Length == 0) return s;
                    if (count < 0) return s.Replace(oldStr, newStr);
                    var sb = new StringBuilder();
                    int pos = 0;
                    while (count > 0)
                    {
                        int idx = s.IndexOf(oldStr, pos, StringComparison.Ordinal);
                        if (idx < 0) break;
                        sb.Append(s, pos, idx - pos).Append(newStr);
                        pos = idx + oldStr.Length;
                        count--;
                    }
                    sb.Append(s, pos, s.Length - pos);
                    return sb.ToString();
                });

                case "startswith": return N(name, (a, k) => MatchAffix(s, a, true));
                case "endswith": return N(name, (a, k) => MatchAffix(s, a, false));

                case "upper": return N(name, (a, k) => s.ToUpperInvariant());
                case "lower": return N(name, (a, k) => s.ToLowerInvariant());
                case "title": return N(name, (a, k) => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLowerInvariant()));
                case "capitalize": return N(name, (a, k) => s.Length == 0 ? s : char.ToUpperInvariant(s[0]) + s.Substring(1).ToLowerInvariant());
                case "swapcase": return N(name, (a, k) =>
                {
                    var sb = new StringBuilder(s.Length);
                    foreach (char c in s) sb.Append(char.IsUpper(c) ? char.ToLowerInvariant(c) : char.ToUpperInvariant(c));
                    return sb.ToString();
                });

                case "find": return N(name, (a, k) => (long)IndexOfIn(s, a, false));
                case "rfind": return N(name, (a, k) => (long)IndexOfIn(s, a, true));
                case "index": return N(name, (a, k) =>
                {
                    int r = IndexOfIn(s, a, false);
                    if (r < 0) throw new PyError("ValueError", "substring not found");
                    return (long)r;
                });
                case "rindex": return N(name, (a, k) =>
                {
                    int r = IndexOfIn(s, a, true);
                    if (r < 0) throw new PyError("ValueError", "substring not found");
                    return (long)r;
                });

                case "count": return N(name, (a, k) =>
                {
                    string needle = PyOps.ToStr(a[0]);
                    if (needle.Length == 0) return (long)(s.Length + 1);
                    long n = 0;
                    int pos = 0;
                    while (true)
                    {
                        int idx = s.IndexOf(needle, pos, StringComparison.Ordinal);
                        if (idx < 0) break;
                        n++;
                        pos = idx + needle.Length;
                    }
                    return n;
                });

                case "encode": return N(name, (a, k) => Encoding.UTF8.GetBytes(s));

                case "isdigit": return N(name, (a, k) => s.Length > 0 && All(s, char.IsDigit));
                case "isalpha": return N(name, (a, k) => s.Length > 0 && All(s, char.IsLetter));
                case "isalnum": return N(name, (a, k) => s.Length > 0 && All(s, char.IsLetterOrDigit));
                case "isspace": return N(name, (a, k) => s.Length > 0 && All(s, char.IsWhiteSpace));
                case "isupper": return N(name, (a, k) => s.Length > 0 && !All(s, c => !char.IsUpper(c)) && All(s, c => !char.IsLower(c)));
                case "islower": return N(name, (a, k) => s.Length > 0 && !All(s, c => !char.IsLower(c)) && All(s, c => !char.IsUpper(c)));

                case "ljust": return N(name, (a, k) => s.PadRight((int)Py.ToInt(a[0]), PadChar(a)));
                case "rjust": return N(name, (a, k) => s.PadLeft((int)Py.ToInt(a[0]), PadChar(a)));
                case "zfill": return N(name, (a, k) =>
                {
                    int w = (int)Py.ToInt(a[0]);
                    if (s.Length >= w) return s;
                    if (s.Length > 0 && (s[0] == '-' || s[0] == '+'))
                        return s[0] + s.Substring(1).PadLeft(w - 1, '0');
                    return s.PadLeft(w, '0');
                });
                case "center": return N(name, (a, k) =>
                {
                    int w = (int)Py.ToInt(a[0]);
                    if (s.Length >= w) return s;
                    char pad = PadChar(a);
                    int left = (w - s.Length) / 2;
                    return new string(pad, left) + s + new string(pad, w - s.Length - left);
                });

                case "partition": return N(name, (a, k) => Partition(s, PyOps.ToStr(a[0]), false));
                case "rpartition": return N(name, (a, k) => Partition(s, PyOps.ToStr(a[0]), true));

                default: return null;
            }
        }

        static char PadChar(object[] a) { return a.Length > 1 ? PyOps.ToStr(a[1])[0] : ' '; }

        static bool All(string s, Func<char, bool> pred)
        {
            foreach (char c in s) if (!pred(c)) return false;
            return true;
        }

        static object MatchAffix(string s, object[] a, bool prefix)
        {
            var candidates = new List<string>();
            var t = a[0] as PyTuple;
            if (t != null) { foreach (var o in t.Items) candidates.Add(PyOps.ToStr(o)); }
            else candidates.Add(PyOps.ToStr(a[0]));

            string target = s;
            if (a.Length > 1)
            {
                int start = (int)Py.ToInt(a[1]);
                if (start < 0) start += s.Length;
                start = Math.Max(0, Math.Min(s.Length, start));
                int end = s.Length;
                if (a.Length > 2)
                {
                    end = (int)Py.ToInt(a[2]);
                    if (end < 0) end += s.Length;
                    end = Math.Max(start, Math.Min(s.Length, end));
                }
                target = s.Substring(start, end - start);
            }

            foreach (var c in candidates)
            {
                if (prefix ? target.StartsWith(c, StringComparison.Ordinal)
                           : target.EndsWith(c, StringComparison.Ordinal)) return true;
            }
            return false;
        }

        static int IndexOfIn(string s, object[] a, bool last)
        {
            string needle = PyOps.ToStr(a[0]);
            int start = 0, end = s.Length;
            if (a.Length > 1 && a[1] != null) { start = (int)Py.ToInt(a[1]); if (start < 0) start += s.Length; start = Math.Max(0, Math.Min(s.Length, start)); }
            if (a.Length > 2 && a[2] != null) { end = (int)Py.ToInt(a[2]); if (end < 0) end += s.Length; end = Math.Max(start, Math.Min(s.Length, end)); }
            string region = s.Substring(start, end - start);
            int idx = last ? region.LastIndexOf(needle, StringComparison.Ordinal)
                           : region.IndexOf(needle, StringComparison.Ordinal);
            return idx < 0 ? -1 : idx + start;
        }

        static PyTuple Partition(string s, string sep, bool fromRight)
        {
            int idx = fromRight ? s.LastIndexOf(sep, StringComparison.Ordinal) : s.IndexOf(sep, StringComparison.Ordinal);
            if (idx < 0)
                return fromRight ? new PyTuple(new object[] { "", "", s }) : new PyTuple(new object[] { s, "", "" });
            return new PyTuple(new object[] { s.Substring(0, idx), sep, s.Substring(idx + sep.Length) });
        }

        static object Trim(string s, object chars, bool left, bool right)
        {
            if (chars == null)
            {
                if (left && right) return s.Trim();
                return left ? s.TrimStart() : s.TrimEnd();
            }
            char[] set = PyOps.ToStr(chars).ToCharArray();
            if (left && right) return s.Trim(set);
            return left ? s.TrimStart(set) : s.TrimEnd(set);
        }

        static PyList Split(string s, object sep, int maxsplit, bool fromRight)
        {
            var rv = new PyList();

            if (sep == null)
            {
                // Whitespace split collapses runs and ignores leading/trailing space.
                var parts = new List<string>();
                int i = 0;
                while (i < s.Length)
                {
                    while (i < s.Length && char.IsWhiteSpace(s[i])) i++;
                    if (i >= s.Length) break;
                    int start = i;
                    while (i < s.Length && !char.IsWhiteSpace(s[i])) i++;
                    parts.Add(s.Substring(start, i - start));
                }
                if (maxsplit >= 0 && parts.Count > maxsplit + 1)
                {
                    // Re-join the overflow; rare, but keeps semantics right.
                    var head = parts.GetRange(0, maxsplit);
                    head.Add(string.Join(" ", parts.GetRange(maxsplit, parts.Count - maxsplit).ToArray()));
                    parts = head;
                }
                foreach (var p in parts) rv.Items.Add(p);
                return rv;
            }

            string sepStr = PyOps.ToStr(sep);
            if (sepStr.Length == 0) throw new PyError("ValueError", "empty separator");

            var pieces = new List<string>();
            int pos = 0;
            while (true)
            {
                int idx = s.IndexOf(sepStr, pos, StringComparison.Ordinal);
                if (idx < 0) break;
                pieces.Add(s.Substring(pos, idx - pos));
                pos = idx + sepStr.Length;
            }
            pieces.Add(s.Substring(pos));

            if (maxsplit >= 0 && pieces.Count > maxsplit + 1)
            {
                if (fromRight)
                {
                    int keep = pieces.Count - maxsplit;
                    var merged = new List<string> { string.Join(sepStr, pieces.GetRange(0, keep).ToArray()) };
                    merged.AddRange(pieces.GetRange(keep, pieces.Count - keep));
                    pieces = merged;
                }
                else
                {
                    var head = pieces.GetRange(0, maxsplit);
                    head.Add(string.Join(sepStr, pieces.GetRange(maxsplit, pieces.Count - maxsplit).ToArray()));
                    pieces = head;
                }
            }

            foreach (var p in pieces) rv.Items.Add(p);
            return rv;
        }

        // ------------------------------------------------------------ str.format

        /// <summary>
        /// Implements `str.format` field substitution. Also used for f-string spec
        /// handling by the interpreter.
        /// </summary>
        public static string Format(string fmt, object[] args, PyDict kwargs)
        {
            var sb = new StringBuilder(fmt.Length + 16);
            int autoIndex = 0;
            int i = 0;

            while (i < fmt.Length)
            {
                char c = fmt[i];

                if (c == '{')
                {
                    if (i + 1 < fmt.Length && fmt[i + 1] == '{') { sb.Append('{'); i += 2; continue; }

                    int close = FindClosingBrace(fmt, i);
                    if (close < 0) throw new PyError("ValueError", "unmatched '{' in format string");

                    string field = fmt.Substring(i + 1, close - i - 1);
                    sb.Append(RenderField(field, args, kwargs, ref autoIndex));
                    i = close + 1;
                    continue;
                }

                if (c == '}')
                {
                    if (i + 1 < fmt.Length && fmt[i + 1] == '}') { sb.Append('}'); i += 2; continue; }
                    throw new PyError("ValueError", "single '}' encountered in format string");
                }

                sb.Append(c);
                i++;
            }

            return sb.ToString();
        }

        static int FindClosingBrace(string s, int open)
        {
            int depth = 0;
            for (int i = open; i < s.Length; i++)
            {
                if (s[i] == '{') depth++;
                else if (s[i] == '}') { depth--; if (depth == 0) return i; }
            }
            return -1;
        }

        static string RenderField(string field, object[] args, PyDict kwargs, ref int autoIndex)
        {
            // Split off the conversion (!r/!s) and format spec (:...), outside of any [].
            string spec = null, conversion = null;
            int depth = 0;
            for (int i = 0; i < field.Length; i++)
            {
                char c = field[i];
                if (c == '[') depth++;
                else if (c == ']') depth--;
                else if (depth == 0 && c == ':') { spec = field.Substring(i + 1); field = field.Substring(0, i); break; }
                else if (depth == 0 && c == '!' && i + 1 < field.Length && field[i + 1] != '=')
                {
                    conversion = field.Substring(i + 1);
                    field = field.Substring(0, i);
                    break;
                }
            }

            if (conversion == null && spec != null)
            {
                int bang = field.LastIndexOf('!');
                if (bang >= 0) { conversion = field.Substring(bang + 1); field = field.Substring(0, bang); }
            }

            object value = ResolveFieldName(field, args, kwargs, ref autoIndex);

            if (conversion == "r") return Pad(PyOps.Repr(value), spec);
            if (conversion == "s") return Pad(PyOps.ToStr(value), spec);

            // Nested replacement inside the spec, e.g. {0:{1}}
            if (spec != null && spec.IndexOf('{') >= 0)
                spec = Format(spec, args, kwargs);

            return PyFormat.FormatSpec(value, spec);
        }

        static string Pad(string text, string spec)
        {
            return string.IsNullOrEmpty(spec) ? text : PyFormat.FormatSpec(text, spec);
        }

        static object ResolveFieldName(string field, object[] args, PyDict kwargs, ref int autoIndex)
        {
            // Leading name or index, then a chain of .attr and [key] accessors.
            int i = 0;
            while (i < field.Length && field[i] != '.' && field[i] != '[') i++;
            string head = field.Substring(0, i);

            object value;
            if (head.Length == 0)
            {
                if (autoIndex >= args.Length) throw new PyError("IndexError", "Replacement index out of range");
                value = args[autoIndex++];
            }
            else if (IsAllDigits(head))
            {
                int idx = int.Parse(head, CultureInfo.InvariantCulture);
                if (idx >= args.Length) throw new PyError("IndexError", "Replacement index out of range");
                value = args[idx];
            }
            else
            {
                if (kwargs == null || !kwargs.TryGet(head, out value))
                    throw new PyError("KeyError", PyOps.Repr(head));
            }

            while (i < field.Length)
            {
                if (field[i] == '.')
                {
                    int start = ++i;
                    while (i < field.Length && field[i] != '.' && field[i] != '[') i++;
                    value = PyOps.GetAttr(value, field.Substring(start, i - start));
                }
                else if (field[i] == '[')
                {
                    int close = field.IndexOf(']', i);
                    if (close < 0) throw new PyError("ValueError", "unmatched '[' in format field");
                    string key = field.Substring(i + 1, close - i - 1);
                    value = PyOps.GetItem(value, IsAllDigits(key) ? (object)long.Parse(key, CultureInfo.InvariantCulture) : key);
                    i = close + 1;
                }
                else break;
            }

            return value;
        }

        static bool IsAllDigits(string s)
        {
            if (s.Length == 0) return false;
            foreach (char c in s) if (!char.IsDigit(c)) return false;
            return true;
        }

        // ------------------------------------------------------------ list

        static object ListMethod(PyList l, string name)
        {
            switch (name)
            {
                case "append": return N(name, (a, k) => { l.Items.Add(a[0]); return null; });
                case "extend": return N(name, (a, k) => { l.Items.AddRange(PyOps.Iterate(a[0])); return null; });
                case "insert": return N(name, (a, k) =>
                {
                    int idx = (int)Py.ToInt(a[0]);
                    if (idx < 0) idx += l.Items.Count;
                    idx = Math.Max(0, Math.Min(l.Items.Count, idx));
                    l.Items.Insert(idx, a[1]);
                    return null;
                });
                case "remove": return N(name, (a, k) =>
                {
                    for (int i = 0; i < l.Items.Count; i++)
                        if (PyOps.Eq(l.Items[i], a[0])) { l.Items.RemoveAt(i); return null; }
                    throw new PyError("ValueError", "list.remove(x): x not in list");
                });
                case "pop": return N(name, (a, k) =>
                {
                    if (l.Items.Count == 0) throw new PyError("IndexError", "pop from empty list");
                    int idx = a.Length > 0 ? (int)Py.ToInt(a[0]) : l.Items.Count - 1;
                    if (idx < 0) idx += l.Items.Count;
                    if (idx < 0 || idx >= l.Items.Count) throw new PyError("IndexError", "pop index out of range");
                    object v = l.Items[idx];
                    l.Items.RemoveAt(idx);
                    return v;
                });
                case "clear": return N(name, (a, k) => { l.Items.Clear(); return null; });
                case "copy": return N(name, (a, k) => new PyList(new List<object>(l.Items)));
                case "reverse": return N(name, (a, k) => { l.Items.Reverse(); return null; });
                case "index": return N(name, (a, k) =>
                {
                    for (int i = 0; i < l.Items.Count; i++)
                        if (PyOps.Eq(l.Items[i], a[0])) return (long)i;
                    throw new PyError("ValueError", PyOps.Repr(a[0]) + " is not in list");
                });
                case "count": return N(name, (a, k) =>
                {
                    long n = 0;
                    foreach (var o in l.Items) if (PyOps.Eq(o, a[0])) n++;
                    return n;
                });
                case "sort": return N(name, (a, k) =>
                {
                    object key = Kw(k, "key", null);
                    bool reverse = Py.Truthy(Kw(k, "reverse", false));
                    SortInPlace(l.Items, key, reverse);
                    return null;
                });
                default: return null;
            }
        }

        /// <summary>Stable sort matching Python's, with optional key function.</summary>
        public static void SortInPlace(List<object> items, object key, bool reverse)
        {
            var decorated = new List<KeyValuePair<object, object>>(items.Count);
            for (int i = 0; i < items.Count; i++)
            {
                object k = key == null ? items[i] : PyOps.CallObject(key, new object[] { items[i] }, null);
                decorated.Add(new KeyValuePair<object, object>(k, items[i]));
            }

            // List.Sort is unstable, so merge sort by index to keep equal elements in order.
            var indices = new int[decorated.Count];
            for (int i = 0; i < indices.Length; i++) indices[i] = i;

            var arr = decorated.ToArray();
            Array.Sort(indices, (x, y) =>
            {
                int c = PyOps.Compare(arr[x].Key, arr[y].Key);
                if (c != 0) return reverse ? -c : c;
                return x.CompareTo(y);
            });

            items.Clear();
            for (int i = 0; i < indices.Length; i++) items.Add(arr[indices[i]].Value);
        }

        // ------------------------------------------------------------ tuple

        static object TupleMethod(PyTuple t, string name)
        {
            switch (name)
            {
                case "index": return N(name, (a, k) =>
                {
                    for (int i = 0; i < t.Items.Length; i++) if (PyOps.Eq(t.Items[i], a[0])) return (long)i;
                    throw new PyError("ValueError", "tuple.index(x): x not in tuple");
                });
                case "count": return N(name, (a, k) =>
                {
                    long n = 0;
                    foreach (var o in t.Items) if (PyOps.Eq(o, a[0])) n++;
                    return n;
                });
                default: return null;
            }
        }

        // ------------------------------------------------------------ dict

        static object DictMethod(PyDict d, string name)
        {
            switch (name)
            {
                case "get": return N(name, (a, k) => d.Get(a[0], Arg(a, 1)));
                case "keys": return N(name, (a, k) => new PyList(d.Keys));
                case "values": return N(name, (a, k) => new PyList(d.Values));
                case "items": return N(name, (a, k) =>
                {
                    var rv = new PyList();
                    foreach (var kv in d) rv.Items.Add(new PyTuple(new[] { kv.Key, kv.Value }));
                    return rv;
                });
                case "has_key": return N(name, (a, k) => d.Contains(a[0]));
                case "setdefault": return N(name, (a, k) =>
                {
                    object v;
                    if (d.TryGet(a[0], out v)) return v;
                    v = Arg(a, 1);
                    d.Set(a[0], v);
                    return v;
                });
                case "pop": return N(name, (a, k) =>
                {
                    object v;
                    if (d.TryGet(a[0], out v)) { d.Remove(a[0]); return v; }
                    if (a.Length > 1) return a[1];
                    throw new PyError("KeyError", PyOps.Repr(a[0]));
                });
                case "popitem": return N(name, (a, k) =>
                {
                    if (d.Length == 0) throw new PyError("KeyError", "popitem(): dictionary is empty");
                    object key = d.Order[d.Order.Count - 1];
                    object v = d.Get(key, null);
                    d.Remove(key);
                    return new PyTuple(new[] { key, v });
                });
                case "update": return N(name, (a, k) =>
                {
                    if (a.Length > 0 && a[0] != null)
                    {
                        var other = a[0] as PyDict;
                        if (other != null) { foreach (var kv in other) d.Set(kv.Key, kv.Value); }
                        else
                        {
                            foreach (var pair in PyOps.Iterate(a[0]))
                            {
                                var t = pair as PyTuple;
                                if (t == null || t.Items.Length != 2)
                                    throw new PyError("ValueError", "dictionary update sequence element has length != 2");
                                d.Set(t.Items[0], t.Items[1]);
                            }
                        }
                    }
                    if (k != null) foreach (var kv in k) d.Set(kv.Key, kv.Value);
                    return null;
                });
                case "clear": return N(name, (a, k) => { d.Clear(); return null; });
                case "copy": return N(name, (a, k) =>
                {
                    var rv = new PyDict();
                    foreach (var kv in d) rv.Set(kv.Key, kv.Value);
                    return rv;
                });
                default: return null;
            }
        }

        // ------------------------------------------------------------ set

        static object SetMethod(PySet s, string name)
        {
            switch (name)
            {
                case "add": return N(name, (a, k) => { s.Items.Add(a[0]); return null; });
                case "discard": return N(name, (a, k) => { s.Items.Remove(a[0]); return null; });
                case "remove": return N(name, (a, k) =>
                {
                    if (!s.Items.Remove(a[0])) throw new PyError("KeyError", PyOps.Repr(a[0]));
                    return null;
                });
                case "clear": return N(name, (a, k) => { s.Items.Clear(); return null; });
                case "copy": return N(name, (a, k) => new PySet(s.Items));
                case "add_all":
                case "update": return N(name, (a, k) =>
                {
                    foreach (var arg in a) foreach (var o in PyOps.Iterate(arg)) s.Items.Add(o);
                    return null;
                });
                case "union": return N(name, (a, k) =>
                {
                    var rv = new PySet(s.Items);
                    foreach (var arg in a) foreach (var o in PyOps.Iterate(arg)) rv.Items.Add(o);
                    return rv;
                });
                case "intersection": return N(name, (a, k) =>
                {
                    var rv = new PySet(s.Items);
                    foreach (var arg in a)
                    {
                        var other = new PySet(PyOps.Iterate(arg));
                        rv.Items.IntersectWith(other.Items);
                    }
                    return rv;
                });
                case "difference": return N(name, (a, k) =>
                {
                    var rv = new PySet(s.Items);
                    foreach (var arg in a) foreach (var o in PyOps.Iterate(arg)) rv.Items.Remove(o);
                    return rv;
                });
                case "issubset": return N(name, (a, k) => s.Items.IsSubsetOf(new PySet(PyOps.Iterate(a[0])).Items));
                case "issuperset": return N(name, (a, k) => s.Items.IsSupersetOf(new PySet(PyOps.Iterate(a[0])).Items));
                case "pop": return N(name, (a, k) =>
                {
                    foreach (var o in s.Items) { s.Items.Remove(o); return o; }
                    throw new PyError("KeyError", "pop from an empty set");
                });
                default: return null;
            }
        }
    }
}
