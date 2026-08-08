using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RenPy.Python
{
    /// <summary>A lazy `range`, so large ranges do not allocate a list.</summary>
    public sealed class PyRange : IPySized, IEnumerable<object>
    {
        public readonly long Start, Stop, Step;

        public PyRange(long start, long stop, long step)
        {
            if (step == 0) throw new PyError("ValueError", "range() arg 3 must not be zero");
            Start = start; Stop = stop; Step = step;
        }

        public int Length
        {
            get
            {
                long n = Step > 0 ? (Stop - Start + Step - 1) / Step : (Stop - Start + Step + 1) / Step;
                return (int)Math.Max(0, n);
            }
        }

        public IEnumerator<object> GetEnumerator()
        {
            if (Step > 0) { for (long i = Start; i < Stop; i += Step) yield return i; }
            else { for (long i = Start; i > Stop; i += Step) yield return i; }
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public override string ToString()
        {
            return "range(" + Start + ", " + Stop + (Step == 1 ? "" : ", " + Step) + ")";
        }
    }

    /// <summary>Installs the Python builtins the interpreter exposes to game code.</summary>
    public static class PyBuiltins
    {
        public static void Install(PyInterp interp)
        {
            var b = interp.Builtins;

            void Add(string name, Func<object[], PyDict, object> fn) => b.Set(name, new PyNative(name, fn));

            b.Set("None", null);
            b.Set("True", true);
            b.Set("False", false);

            // ---------------------------------------------------------- conversion

            Add("len", (a, k) => (long)PyOps.Len(a[0]));
            Add("str", (a, k) => a.Length == 0 ? "" : PyOps.ToStr(a[0]));
            Add("unicode", (a, k) => a.Length == 0 ? "" : PyOps.ToStr(a[0]));
            Add("repr", (a, k) => PyOps.Repr(a[0]));
            Add("bool", (a, k) => a.Length != 0 && Py.Truthy(a[0]));
            Add("float", (a, k) => a.Length == 0 ? 0.0 : Py.ToDouble(a[0]));

            Add("int", (a, k) =>
            {
                if (a.Length == 0) return 0L;
                if (a.Length > 1 && a[0] is string)
                    return Convert.ToInt64(((string)a[0]).Trim(), (int)Py.ToInt(a[1]));
                if (a[0] is string)
                {
                    string s = ((string)a[0]).Trim();
                    long parsed;
                    if (long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed)) return parsed;
                    // Python's int() accepts a float-looking string only via float().
                    double d;
                    if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out d)) return (long)d;
                    throw new PyError("ValueError", "invalid literal for int(): '" + s + "'");
                }
                return Py.ToInt(a[0]);
            });

            Add("list", (a, k) => a.Length == 0 ? new PyList() : new PyList(PyOps.Iterate(a[0])));
            Add("tuple", (a, k) => a.Length == 0 ? PyTuple.Empty : new PyTuple(new List<object>(PyOps.Iterate(a[0]))));
            Add("set", (a, k) => a.Length == 0 ? new PySet() : new PySet(PyOps.Iterate(a[0])));
            Add("frozenset", (a, k) => a.Length == 0 ? new PySet(true) : new PySet(PyOps.Iterate(a[0]), true));

            Add("dict", (a, k) =>
            {
                var rv = new PyDict();
                if (a.Length > 0 && a[0] != null)
                {
                    var src = a[0] as PyDict;
                    if (src != null) foreach (var kv in src) rv.Set(kv.Key, kv.Value);
                    else
                        foreach (var pair in PyOps.Iterate(a[0]))
                        {
                            var t = pair as PyTuple;
                            if (t != null && t.Items.Length == 2) rv.Set(t.Items[0], t.Items[1]);
                        }
                }
                if (k != null) foreach (var kv in k) rv.Set(kv.Key, kv.Value);
                return rv;
            });

            // ---------------------------------------------------------- numeric

            Add("abs", (a, k) =>
            {
                if (Py.IsInt(a[0])) return Math.Abs(Py.ToInt(a[0]));
                return Math.Abs(Py.ToDouble(a[0]));
            });

            Add("round", (a, k) =>
            {
                double value = Py.ToDouble(a[0]);
                int digits = a.Length > 1 ? (int)Py.ToInt(a[1]) : 0;
                // Python rounds half to even.
                double rounded = Math.Round(value, Math.Max(0, digits), MidpointRounding.ToEven);
                return a.Length > 1 ? (object)rounded : (object)(long)rounded;
            });

            Add("min", (a, k) => MinMax(a, k, true));
            Add("max", (a, k) => MinMax(a, k, false));

            Add("sum", (a, k) =>
            {
                object total = a.Length > 1 ? a[1] : (object)0L;
                foreach (var o in PyOps.Iterate(a[0])) total = PyOps.Add(total, o);
                return total;
            });

            Add("pow", (a, k) => PyOps.Pow(a[0], a[1]));

            Add("divmod", (a, k) => new PyTuple(new[] { PyOps.FloorDiv(a[0], a[1]), PyOps.Mod(a[0], a[1]) }));

            Add("hex", (a, k) =>
            {
                long v = Py.ToInt(a[0]);
                return v < 0 ? "-0x" + (-v).ToString("x") : "0x" + v.ToString("x");
            });
            Add("oct", (a, k) =>
            {
                long v = Py.ToInt(a[0]);
                return (v < 0 ? "-0o" : "0o") + Convert.ToString(Math.Abs(v), 8);
            });
            Add("bin", (a, k) =>
            {
                long v = Py.ToInt(a[0]);
                return (v < 0 ? "-0b" : "0b") + Convert.ToString(Math.Abs(v), 2);
            });

            Add("ord", (a, k) =>
            {
                string s = PyOps.ToStr(a[0]);
                if (s.Length == 0) throw new PyError("TypeError", "ord() expected a character");
                return (long)s[0];
            });
            Add("chr", (a, k) => ((char)Py.ToInt(a[0])).ToString());
            Add("unichr", (a, k) => ((char)Py.ToInt(a[0])).ToString());

            // ---------------------------------------------------------- sequences

            Add("range", (a, k) =>
            {
                if (a.Length == 1) return new PyRange(0, Py.ToInt(a[0]), 1);
                if (a.Length == 2) return new PyRange(Py.ToInt(a[0]), Py.ToInt(a[1]), 1);
                return new PyRange(Py.ToInt(a[0]), Py.ToInt(a[1]), Py.ToInt(a[2]));
            });
            b.Set("xrange", b.Get("range", null));

            Add("sorted", (a, k) =>
            {
                var items = new List<object>(PyOps.Iterate(a[0]));
                object key = Kw(k, "key", a.Length > 1 ? a[1] : null);
                bool reverse = Py.Truthy(Kw(k, "reverse", false));
                PyBuiltinMethods.SortInPlace(items, key, reverse);
                return new PyList(items);
            });

            Add("reversed", (a, k) =>
            {
                var items = new List<object>(PyOps.Iterate(a[0]));
                items.Reverse();
                return new PyList(items);
            });

            Add("enumerate", (a, k) =>
            {
                long start = a.Length > 1 ? Py.ToInt(a[1]) : Py.ToInt(Kw(k, "start", 0L));
                var rv = new PyList();
                foreach (var o in PyOps.Iterate(a[0]))
                    rv.Items.Add(new PyTuple(new object[] { start++, o }));
                return rv;
            });

            Add("zip", (a, k) =>
            {
                var lists = new List<List<object>>();
                foreach (var arg in a) lists.Add(new List<object>(PyOps.Iterate(arg)));

                int n = int.MaxValue;
                foreach (var l in lists) n = Math.Min(n, l.Count);
                if (lists.Count == 0) n = 0;

                var rv = new PyList();
                for (int i = 0; i < n; i++)
                {
                    var row = new object[lists.Count];
                    for (int j = 0; j < lists.Count; j++) row[j] = lists[j][i];
                    rv.Items.Add(new PyTuple(row));
                }
                return rv;
            });

            Add("map", (a, k) =>
            {
                var rv = new PyList();
                if (a.Length == 2)
                {
                    foreach (var o in PyOps.Iterate(a[1]))
                        rv.Items.Add(a[0] == null ? o : PyOps.CallObject(a[0], new[] { o }, null));
                    return rv;
                }

                var lists = new List<List<object>>();
                for (int i = 1; i < a.Length; i++) lists.Add(new List<object>(PyOps.Iterate(a[i])));
                int n = int.MaxValue;
                foreach (var l in lists) n = Math.Min(n, l.Count);
                for (int i = 0; i < n; i++)
                {
                    var row = new object[lists.Count];
                    for (int j = 0; j < lists.Count; j++) row[j] = lists[j][i];
                    rv.Items.Add(PyOps.CallObject(a[0], row, null));
                }
                return rv;
            });

            Add("filter", (a, k) =>
            {
                var rv = new PyList();
                foreach (var o in PyOps.Iterate(a[1]))
                {
                    bool keep = a[0] == null ? Py.Truthy(o) : Py.Truthy(PyOps.CallObject(a[0], new[] { o }, null));
                    if (keep) rv.Items.Add(o);
                }
                return rv;
            });

            Add("any", (a, k) =>
            {
                foreach (var o in PyOps.Iterate(a[0])) if (Py.Truthy(o)) return true;
                return false;
            });

            Add("all", (a, k) =>
            {
                foreach (var o in PyOps.Iterate(a[0])) if (!Py.Truthy(o)) return false;
                return true;
            });

            Add("iter", (a, k) => new PyList(PyOps.Iterate(a[0])));

            Add("next", (a, k) =>
            {
                foreach (var o in PyOps.Iterate(a[0])) return o;
                if (a.Length > 1) return a[1];
                throw new PyError("StopIteration", "");
            });

            // ---------------------------------------------------------- reflection

            Add("isinstance", (a, k) => IsInstance(a[0], a[1]));
            Add("issubclass", (a, k) =>
            {
                var sub = a[0] as PyType;
                var super = a[1] as PyType;
                return sub != null && super != null && sub.IsSubclassOf(super);
            });

            Add("type", (a, k) =>
            {
                var inst = a[0] as PyInstance;
                if (inst != null && inst.Type != null) return inst.Type;
                return Py.TypeName(a[0]);
            });

            Add("getattr", (a, k) =>
            {
                object value;
                if (PyOps.TryGetAttr(a[0], PyOps.ToStr(a[1]), out value)) return value;
                if (a.Length > 2) return a[2];
                throw new PyError("AttributeError",
                    "'" + Py.TypeName(a[0]) + "' object has no attribute '" + PyOps.ToStr(a[1]) + "'");
            });

            Add("setattr", (a, k) => { PyOps.SetAttr(a[0], PyOps.ToStr(a[1]), a[2]); return null; });

            Add("hasattr", (a, k) =>
            {
                object value;
                try { return PyOps.TryGetAttr(a[0], PyOps.ToStr(a[1]), out value); }
                catch (PyError) { return false; }
            });

            Add("delattr", (a, k) =>
            {
                var inst = a[0] as PyInstance;
                if (inst != null) inst.Dict.Remove(PyOps.ToStr(a[1]));
                return null;
            });

            Add("callable", (a, k) => a[0] is IPyCallable || a[0] is PyType);

            Add("hash", (a, k) => (long)PyKeyComparer.Instance.GetHashCode(a[0]));
            Add("id", (a, k) => (long)System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(a[0]));

            Add("format", (a, k) => PyFormat.FormatSpec(a[0], a.Length > 1 ? PyOps.ToStr(a[1]) : null));

            Add("print", (a, k) =>
            {
                var sb = new StringBuilder();
                for (int i = 0; i < a.Length; i++)
                {
                    if (i > 0) sb.Append(' ');
                    sb.Append(PyOps.ToStr(a[i]));
                }
                if (interp.Warn != null) interp.Warn(sb.ToString());
                return null;
            });

            // Ren'Py's translation marker; at runtime it is the identity.
            Add("_", (a, k) => a.Length > 0 ? a[0] : null);
            Add("__", (a, k) => a.Length > 0 ? a[0] : null);
            Add("_p", (a, k) => a.Length > 0 ? Dedent(PyOps.ToStr(a[0])) : null);

            InstallExceptions(b);
        }

        static object Kw(PyDict kwargs, string name, object dflt)
        {
            if (kwargs == null) return dflt;
            object v;
            return kwargs.TryGet(name, out v) ? v : dflt;
        }

        static object MinMax(object[] a, PyDict k, bool wantMin)
        {
            object key = Kw(k, "key", null);
            IEnumerable<object> source = a.Length == 1 ? PyOps.Iterate(a[0]) : a;

            object best = null, bestKey = null;
            bool first = true;

            foreach (var o in source)
            {
                object ok = key == null ? o : PyOps.CallObject(key, new[] { o }, null);
                if (first)
                {
                    best = o; bestKey = ok; first = false;
                    continue;
                }
                int c = PyOps.Compare(ok, bestKey);
                if (wantMin ? c < 0 : c > 0) { best = o; bestKey = ok; }
            }

            if (first)
            {
                object dflt = Kw(k, "default", null);
                if (dflt != null) return dflt;
                throw new PyError("ValueError", (wantMin ? "min" : "max") + "() arg is an empty sequence");
            }

            return best;
        }

        static bool IsInstance(object value, object type)
        {
            var tuple = type as PyTuple;
            if (tuple != null)
            {
                foreach (var t in tuple.Items) if (IsInstance(value, t)) return true;
                return false;
            }

            var pyType = type as PyType;
            if (pyType != null)
            {
                var inst = value as PyInstance;
                if (inst == null || inst.Type == null) return false;
                return inst.Type.IsSubclassOf(pyType);
            }

            // Builtin types are identified by the native constructor's name.
            var native = type as PyNative;
            if (native != null)
            {
                string name = native.Name;
                int dot = name.LastIndexOf('.');
                if (dot >= 0) name = name.Substring(dot + 1);
                return MatchesBuiltinType(value, name);
            }

            var exceptionType = type as PyExceptionType;
            if (exceptionType != null)
            {
                var inst = value as PyExceptionInstance;
                return inst != null && exceptionType.Matches(inst.Error.PyType);
            }

            return false;
        }

        static bool MatchesBuiltinType(object value, string name)
        {
            switch (name)
            {
                case "int": return Py.IsInt(value) && !(value is bool);
                case "long": return Py.IsInt(value) && !(value is bool);
                case "float": return Py.IsFloat(value);
                case "bool": return value is bool;
                case "str":
                case "unicode":
                case "basestring": return value is string;
                case "bytes": return value is byte[];
                case "list": return value is PyList;
                case "tuple": return value is PyTuple;
                case "dict": return value is PyDict;
                case "set":
                case "frozenset": return value is PySet;
                case "object": return true;
                default: return false;
            }
        }

        /// <summary>Removes the common leading indentation, as Ren'Py's _p() does.</summary>
        static string Dedent(string text)
        {
            var lines = text.Replace("\r\n", "\n").Split('\n');
            int common = int.MaxValue;

            foreach (var line in lines)
            {
                if (line.Trim().Length == 0) continue;
                int indent = 0;
                while (indent < line.Length && line[indent] == ' ') indent++;
                common = Math.Min(common, indent);
            }
            if (common == int.MaxValue) common = 0;

            var sb = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                sb.Append(line.Length >= common ? line.Substring(common) : line.TrimStart());
                if (i < lines.Length - 1) sb.Append('\n');
            }
            return sb.ToString().Trim();
        }

        static void InstallExceptions(PyDict b)
        {
            var baseException = new PyExceptionType("BaseException");
            var exception = new PyExceptionType("Exception", baseException);

            b.Set("BaseException", baseException);
            b.Set("Exception", exception);

            var arithmetic = Define(b, "ArithmeticError", exception);
            var lookup = Define(b, "LookupError", exception);
            var os = Define(b, "OSError", exception);
            var value = Define(b, "ValueError", exception);
            var runtime = Define(b, "RuntimeError", exception);

            Define(b, "TypeError", exception);
            Define(b, "NameError", exception);
            Define(b, "AttributeError", exception);
            Define(b, "AssertionError", exception);
            Define(b, "StopIteration", exception);
            Define(b, "ImportError", exception);
            Define(b, "SystemError", exception);
            Define(b, "MemoryError", exception);
            Define(b, "SystemExit", baseException);
            Define(b, "KeyboardInterrupt", baseException);
            Define(b, "NotImplementedError", runtime);
            Define(b, "RecursionError", runtime);
            Define(b, "ZeroDivisionError", arithmetic);
            Define(b, "OverflowError", arithmetic);
            Define(b, "FloatingPointError", arithmetic);
            Define(b, "IndexError", lookup);
            Define(b, "KeyError", lookup);
            Define(b, "IOError", os);
            Define(b, "FileNotFoundError", os);
            Define(b, "UnicodeError", value);
            Define(b, "UnicodeDecodeError", value);
            Define(b, "UnicodeEncodeError", value);
        }

        static PyExceptionType Define(PyDict b, string name, PyExceptionType parent)
        {
            var t = new PyExceptionType(name, parent);
            b.Set(name, t);
            return t;
        }
    }
}
