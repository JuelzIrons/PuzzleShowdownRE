using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RenPy.Python
{
    /// <summary>
    /// The Python operator semantics: arithmetic, comparison, indexing, iteration,
    /// attribute access, calling, and string conversion. Kept free of interpreter
    /// state so the unpickler and the AST layer can use it too.
    /// </summary>
    public static class PyOps
    {
        // ------------------------------------------------------------ calling

        public static object CallObject(object fn, object[] args, PyDict kwargs)
        {
            var callable = fn as IPyCallable;
            if (callable != null) return callable.Call(args ?? Py.EmptyArgs, kwargs);

            var inst = fn as PyInstance;
            if (inst != null)
            {
                object call;
                if (inst.TryGetAttr("__call__", out call))
                    return CallObject(call, args ?? Py.EmptyArgs, kwargs);
            }

            throw new PyError("TypeError", "'" + Py.TypeName(fn) + "' object is not callable");
        }

        // ------------------------------------------------------------ attributes

        public static object GetAttr(object o, string name)
        {
            object v;
            if (TryGetAttr(o, name, out v)) return v;
            throw new PyError("AttributeError", "'" + Py.TypeName(o) + "' object has no attribute '" + name + "'");
        }

        public static bool TryGetAttr(object o, string name, out object value)
        {
            var attrs = o as IPyAttrs;
            if (attrs != null) return attrs.TryGetAttr(name, out value);

            // Methods on the built-in types are resolved as bound natives.
            value = PyBuiltinMethods.Lookup(o, name);
            return value != null;
        }

        public static void SetAttr(object o, string name, object value)
        {
            var attrs = o as IPyAttrs;
            if (attrs == null)
                throw new PyError("AttributeError", "'" + Py.TypeName(o) + "' object has no attribute '" + name + "'");
            attrs.SetAttr(name, value);
        }

        // ------------------------------------------------------------ equality

        public static bool Eq(object a, object b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a == null || b == null) return false;

            if (a is string && b is string) return (string)a == (string)b;

            if (Py.IsNumber(a) && Py.IsNumber(b))
            {
                if (Py.IsInt(a) && Py.IsInt(b)) return Py.ToInt(a) == Py.ToInt(b);
                return Py.ToDouble(a) == Py.ToDouble(b);
            }

            var ta = a as PyTuple; var tb = b as PyTuple;
            if (ta != null || tb != null)
            {
                if (ta == null || tb == null) return false;
                if (ta.Items.Length != tb.Items.Length) return false;
                for (int i = 0; i < ta.Items.Length; i++)
                    if (!Eq(ta.Items[i], tb.Items[i])) return false;
                return true;
            }

            var la = a as PyList; var lb = b as PyList;
            if (la != null || lb != null)
            {
                if (la == null || lb == null) return false;
                if (la.Items.Count != lb.Items.Count) return false;
                for (int i = 0; i < la.Items.Count; i++)
                    if (!Eq(la.Items[i], lb.Items[i])) return false;
                return true;
            }

            var da = a as PyDict; var db = b as PyDict;
            if (da != null || db != null)
            {
                if (da == null || db == null) return false;
                if (da.Length != db.Length) return false;
                foreach (var kv in da)
                {
                    object other;
                    if (!db.TryGet(kv.Key, out other)) return false;
                    if (!Eq(kv.Value, other)) return false;
                }
                return true;
            }

            var sa = a as PySet; var sb = b as PySet;
            if (sa != null && sb != null) return sa.Items.SetEquals(sb.Items);

            var ba = a as byte[]; var bb = b as byte[];
            if (ba != null && bb != null)
            {
                if (ba.Length != bb.Length) return false;
                for (int i = 0; i < ba.Length; i++) if (ba[i] != bb[i]) return false;
                return true;
            }

            var ia = a as PyInstance;
            if (ia != null)
            {
                object m;
                if (ia.TryGetAttr("__eq__", out m))
                {
                    object r = CallObject(m, new object[] { b }, null);
                    if (r != null) return Py.Truthy(r);
                }
            }

            return a.Equals(b);
        }

        /// <summary>Three-way comparison for &lt; &lt;= &gt; &gt;= and sorting.</summary>
        public static int Compare(object a, object b)
        {
            if (Py.IsNumber(a) && Py.IsNumber(b))
            {
                if (Py.IsInt(a) && Py.IsInt(b)) return Py.ToInt(a).CompareTo(Py.ToInt(b));
                return Py.ToDouble(a).CompareTo(Py.ToDouble(b));
            }

            if (a is string && b is string)
                return string.CompareOrdinal((string)a, (string)b);

            var la = AsSequence(a); var lb = AsSequence(b);
            if (la != null && lb != null)
            {
                int n = Math.Min(la.Count, lb.Count);
                for (int i = 0; i < n; i++)
                {
                    int c = Compare(la[i], lb[i]);
                    if (c != 0) return c;
                }
                return la.Count.CompareTo(lb.Count);
            }

            var ia = a as PyInstance;
            if (ia != null)
            {
                object m;
                if (ia.TryGetAttr("__lt__", out m))
                {
                    if (Py.Truthy(CallObject(m, new object[] { b }, null))) return -1;
                    return Eq(a, b) ? 0 : 1;
                }
            }

            if (Eq(a, b)) return 0;
            throw new PyError("TypeError",
                "'<' not supported between instances of '" + Py.TypeName(a) + "' and '" + Py.TypeName(b) + "'");
        }

        static IList<object> AsSequence(object o)
        {
            var l = o as PyList; if (l != null) return l.Items;
            var t = o as PyTuple; if (t != null) return t.Items;
            return null;
        }

        // ------------------------------------------------------------ arithmetic

        public static object Add(object a, object b)
        {
            if (a is string && b is string) return (string)a + (string)b;

            if (Py.IsNumber(a) && Py.IsNumber(b))
            {
                if (Py.IsInt(a) && Py.IsInt(b) && !(a is bool && b is bool)) return Py.ToInt(a) + Py.ToInt(b);
                if (Py.IsInt(a) && Py.IsInt(b)) return Py.ToInt(a) + Py.ToInt(b);
                return Py.ToDouble(a) + Py.ToDouble(b);
            }

            var la = a as PyList; var lb = b as PyList;
            if (la != null && lb != null)
            {
                var rv = new List<object>(la.Items);
                rv.AddRange(lb.Items);
                return new PyList(rv);
            }

            var ta = a as PyTuple; var tb = b as PyTuple;
            if (ta != null && tb != null)
            {
                var rv = new object[ta.Items.Length + tb.Items.Length];
                Array.Copy(ta.Items, rv, ta.Items.Length);
                Array.Copy(tb.Items, 0, rv, ta.Items.Length, tb.Items.Length);
                return new PyTuple(rv);
            }

            object dunder;
            if (TryDunder(a, "__add__", b, out dunder)) return dunder;
            if (TryDunder(b, "__radd__", a, out dunder)) return dunder;

            // Ren'Py scripts concatenate freely; be forgiving when one side is text.
            if (a is string || b is string) return ToStr(a) + ToStr(b);

            throw new PyError("TypeError",
                "unsupported operand type(s) for +: '" + Py.TypeName(a) + "' and '" + Py.TypeName(b) + "'");
        }

        public static object Sub(object a, object b)
        {
            var sa = a as PySet; var sb = b as PySet;
            if (sa != null && sb != null)
            {
                var rv = new PySet();
                foreach (var o in sa.Items) if (!sb.Items.Contains(o)) rv.Items.Add(o);
                return rv;
            }
            object dunder;
            if (TryDunder(a, "__sub__", b, out dunder)) return dunder;
            if (Py.IsInt(a) && Py.IsInt(b)) return Py.ToInt(a) - Py.ToInt(b);
            return Py.ToDouble(a) - Py.ToDouble(b);
        }

        public static object Mul(object a, object b)
        {
            // str/list/tuple repetition
            if (a is string && Py.IsInt(b)) return Repeat((string)a, (int)Py.ToInt(b));
            if (b is string && Py.IsInt(a)) return Repeat((string)b, (int)Py.ToInt(a));

            var la = a as PyList;
            if (la != null && Py.IsInt(b)) return RepeatList(la, (int)Py.ToInt(b));
            var lb = b as PyList;
            if (lb != null && Py.IsInt(a)) return RepeatList(lb, (int)Py.ToInt(a));

            object dunder;
            if (TryDunder(a, "__mul__", b, out dunder)) return dunder;

            if (Py.IsInt(a) && Py.IsInt(b)) return Py.ToInt(a) * Py.ToInt(b);
            return Py.ToDouble(a) * Py.ToDouble(b);
        }

        static string Repeat(string s, int n)
        {
            if (n <= 0) return "";
            var sb = new StringBuilder(s.Length * n);
            for (int i = 0; i < n; i++) sb.Append(s);
            return sb.ToString();
        }

        static PyList RepeatList(PyList l, int n)
        {
            var rv = new List<object>();
            for (int i = 0; i < n; i++) rv.AddRange(l.Items);
            return new PyList(rv);
        }

        public static object Div(object a, object b)
        {
            object dunder;
            if (TryDunder(a, "__truediv__", b, out dunder)) return dunder;
            double d = Py.ToDouble(b);
            if (d == 0.0) throw new PyError("ZeroDivisionError", "division by zero");
            return Py.ToDouble(a) / d;
        }

        public static object FloorDiv(object a, object b)
        {
            if (Py.IsInt(a) && Py.IsInt(b))
            {
                long x = Py.ToInt(a), y = Py.ToInt(b);
                if (y == 0) throw new PyError("ZeroDivisionError", "integer division or modulo by zero");
                // Python floor-divides toward negative infinity.
                long q = x / y;
                if ((x % y != 0) && ((x < 0) != (y < 0))) q--;
                return q;
            }
            double db = Py.ToDouble(b);
            if (db == 0.0) throw new PyError("ZeroDivisionError", "float floor division by zero");
            return Math.Floor(Py.ToDouble(a) / db);
        }

        public static object Mod(object a, object b)
        {
            // `"fmt" % args` is pervasive in Ren'Py scripts.
            if (a is string) return PyFormat.Percent((string)a, b);

            if (Py.IsInt(a) && Py.IsInt(b))
            {
                long x = Py.ToInt(a), y = Py.ToInt(b);
                if (y == 0) throw new PyError("ZeroDivisionError", "integer division or modulo by zero");
                long r = x % y;
                if (r != 0 && ((r < 0) != (y < 0))) r += y;
                return r;
            }

            object dunder;
            if (TryDunder(a, "__mod__", b, out dunder)) return dunder;

            double dx = Py.ToDouble(a), dy = Py.ToDouble(b);
            if (dy == 0.0) throw new PyError("ZeroDivisionError", "float modulo by zero");
            double dr = dx % dy;
            if (dr != 0 && ((dr < 0) != (dy < 0))) dr += dy;
            return dr;
        }

        public static object Pow(object a, object b)
        {
            if (Py.IsInt(a) && Py.IsInt(b) && Py.ToInt(b) >= 0)
            {
                long result = 1, base_ = Py.ToInt(a), exp = Py.ToInt(b);
                while (exp > 0)
                {
                    if ((exp & 1) != 0) result *= base_;
                    base_ *= base_;
                    exp >>= 1;
                }
                return result;
            }
            return Math.Pow(Py.ToDouble(a), Py.ToDouble(b));
        }

        public static object Neg(object a)
        {
            if (Py.IsInt(a)) return -Py.ToInt(a);
            if (Py.IsNumber(a)) return -Py.ToDouble(a);
            object dunder;
            if (TryDunder(a, "__neg__", null, out dunder)) return dunder;
            throw new PyError("TypeError", "bad operand type for unary -: '" + Py.TypeName(a) + "'");
        }

        public static object BitAnd(object a, object b)
        {
            var sa = a as PySet; var sb = b as PySet;
            if (sa != null && sb != null)
            {
                var rv = new PySet();
                foreach (var o in sa.Items) if (sb.Items.Contains(o)) rv.Items.Add(o);
                return rv;
            }
            return Py.ToInt(a) & Py.ToInt(b);
        }

        public static object BitOr(object a, object b)
        {
            var sa = a as PySet; var sb = b as PySet;
            if (sa != null && sb != null)
            {
                var rv = new PySet(sa.Items);
                foreach (var o in sb.Items) rv.Items.Add(o);
                return rv;
            }
            var da = a as PyDict; var db = b as PyDict;
            if (da != null && db != null)
            {
                var rv = new PyDict();
                foreach (var kv in da) rv.Set(kv.Key, kv.Value);
                foreach (var kv in db) rv.Set(kv.Key, kv.Value);
                return rv;
            }
            return Py.ToInt(a) | Py.ToInt(b);
        }

        public static object BitXor(object a, object b) { return Py.ToInt(a) ^ Py.ToInt(b); }
        public static object LShift(object a, object b) { return Py.ToInt(a) << (int)Py.ToInt(b); }
        public static object RShift(object a, object b) { return Py.ToInt(a) >> (int)Py.ToInt(b); }
        public static object Invert(object a) { return ~Py.ToInt(a); }

        static bool TryDunder(object o, string name, object arg, out object result)
        {
            result = null;
            var inst = o as PyInstance;
            if (inst == null) return false;
            object m;
            if (!inst.TryGetAttr(name, out m)) return false;
            result = CallObject(m, arg == null && name.EndsWith("neg__") ? Py.EmptyArgs : new object[] { arg }, null);
            return true;
        }

        // ------------------------------------------------------------ containment

        public static bool Contains(object container, object item)
        {
            if (container is string)
            {
                string s = (string)container;
                string needle = item as string;
                if (needle == null) throw new PyError("TypeError", "'in <string>' requires string as left operand");
                return needle.Length == 0 || s.Contains(needle);
            }

            var d = container as PyDict;
            if (d != null) return d.Contains(item);

            var st = container as PySet;
            if (st != null) return st.Items.Contains(item);

            var seq = AsSequence(container);
            if (seq != null)
            {
                for (int i = 0; i < seq.Count; i++) if (Eq(seq[i], item)) return true;
                return false;
            }

            var inst = container as PyInstance;
            if (inst != null)
            {
                object m;
                if (inst.TryGetAttr("__contains__", out m))
                    return Py.Truthy(CallObject(m, new object[] { item }, null));
            }

            foreach (var o in Iterate(container)) if (Eq(o, item)) return true;
            return false;
        }

        // ------------------------------------------------------------ indexing

        public static object GetItem(object o, object key)
        {
            var d = o as PyDict;
            if (d != null)
            {
                object v;
                if (d.TryGet(key, out v)) return v;
                var inst0 = o as PyInstance;
                throw new PyError("KeyError", Repr(key));
            }

            var slice = key as PySlice;

            if (o is string)
            {
                string s = (string)o;
                if (slice != null) return SliceString(s, slice);
                int i = NormIndex(Py.ToInt(key), s.Length, "string index out of range");
                return s[i].ToString();
            }

            var list = o as PyList;
            if (list != null)
            {
                if (slice != null) return new PyList(SliceList(list.Items, slice));
                return list.Items[NormIndex(Py.ToInt(key), list.Items.Count, "list index out of range")];
            }

            var tup = o as PyTuple;
            if (tup != null)
            {
                if (slice != null) return new PyTuple(SliceList(tup.Items, slice).ToArray());
                return tup.Items[NormIndex(Py.ToInt(key), tup.Items.Length, "tuple index out of range")];
            }

            var inst = o as PyInstance;
            if (inst != null)
            {
                object m;
                if (inst.TryGetAttr("__getitem__", out m)) return CallObject(m, new object[] { key }, null);
            }

            throw new PyError("TypeError", "'" + Py.TypeName(o) + "' object is not subscriptable");
        }

        public static void SetItem(object o, object key, object value)
        {
            var d = o as PyDict;
            if (d != null) { d.Set(key, value); return; }

            var list = o as PyList;
            if (list != null)
            {
                var slice = key as PySlice;
                if (slice != null)
                {
                    int start, stop, step, count;
                    ResolveSlice(slice, list.Items.Count, out start, out stop, out step, out count);
                    if (step != 1) throw new PyError("ValueError", "extended slice assignment is not supported");
                    var repl = new List<object>(Iterate(value));
                    list.Items.RemoveRange(start, count);
                    list.Items.InsertRange(start, repl);
                    return;
                }
                list.Items[NormIndex(Py.ToInt(key), list.Items.Count, "list assignment index out of range")] = value;
                return;
            }

            var inst = o as PyInstance;
            if (inst != null)
            {
                object m;
                if (inst.TryGetAttr("__setitem__", out m)) { CallObject(m, new object[] { key, value }, null); return; }
            }

            throw new PyError("TypeError", "'" + Py.TypeName(o) + "' object does not support item assignment");
        }

        public static void DelItem(object o, object key)
        {
            var d = o as PyDict;
            if (d != null)
            {
                if (!d.Remove(key)) throw new PyError("KeyError", Repr(key));
                return;
            }
            var list = o as PyList;
            if (list != null)
            {
                list.Items.RemoveAt(NormIndex(Py.ToInt(key), list.Items.Count, "list index out of range"));
                return;
            }
            var inst = o as PyInstance;
            if (inst != null)
            {
                object m;
                if (inst.TryGetAttr("__delitem__", out m)) { CallObject(m, new object[] { key }, null); return; }
            }
            throw new PyError("TypeError", "'" + Py.TypeName(o) + "' object does not support item deletion");
        }

        static int NormIndex(long i, int len, string message)
        {
            if (i < 0) i += len;
            if (i < 0 || i >= len) throw new PyError("IndexError", message);
            return (int)i;
        }

        public static void ResolveSlice(PySlice s, int len, out int start, out int stop, out int step, out int count)
        {
            step = s.Step == null ? 1 : (int)Py.ToInt(s.Step);
            if (step == 0) throw new PyError("ValueError", "slice step cannot be zero");

            if (step > 0)
            {
                start = s.Start == null ? 0 : ClampIndex(Py.ToInt(s.Start), len);
                stop = s.Stop == null ? len : ClampIndex(Py.ToInt(s.Stop), len);
                count = Math.Max(0, (stop - start + step - 1) / step);
            }
            else
            {
                start = s.Start == null ? len - 1 : ClampIndexRev(Py.ToInt(s.Start), len);
                stop = s.Stop == null ? -1 : ClampIndexRev(Py.ToInt(s.Stop), len);
                count = Math.Max(0, (stop - start + step + 1) / step);
            }
        }

        static int ClampIndex(long i, int len)
        {
            if (i < 0) i += len;
            if (i < 0) return 0;
            if (i > len) return len;
            return (int)i;
        }

        static int ClampIndexRev(long i, int len)
        {
            if (i < 0) i += len;
            if (i < -1) return -1;
            if (i > len - 1) return len - 1;
            return (int)i;
        }

        static string SliceString(string s, PySlice slice)
        {
            int start, stop, step, count;
            ResolveSlice(slice, s.Length, out start, out stop, out step, out count);
            var sb = new StringBuilder(count);
            for (int i = 0, idx = start; i < count; i++, idx += step) sb.Append(s[idx]);
            return sb.ToString();
        }

        static List<object> SliceList(IList<object> items, PySlice slice)
        {
            int start, stop, step, count;
            ResolveSlice(slice, items.Count, out start, out stop, out step, out count);
            var rv = new List<object>(count);
            for (int i = 0, idx = start; i < count; i++, idx += step) rv.Add(items[idx]);
            return rv;
        }

        // ------------------------------------------------------------ iteration

        public static IEnumerable<object> Iterate(object o)
        {
            if (o == null) throw new PyError("TypeError", "'NoneType' object is not iterable");

            if (o is string)
            {
                string s = (string)o;
                for (int i = 0; i < s.Length; i++) yield return s[i].ToString();
                yield break;
            }

            var list = o as PyList;
            if (list != null)
            {
                // Snapshot so mutation during iteration cannot invalidate the enumerator.
                var copy = list.Items.ToArray();
                for (int i = 0; i < copy.Length; i++) yield return copy[i];
                yield break;
            }

            var tup = o as PyTuple;
            if (tup != null)
            {
                for (int i = 0; i < tup.Items.Length; i++) yield return tup.Items[i];
                yield break;
            }

            var d = o as PyDict;
            if (d != null)
            {
                var keys = d.Order.ToArray();
                for (int i = 0; i < keys.Length; i++) yield return keys[i];
                yield break;
            }

            var st = o as PySet;
            if (st != null)
            {
                foreach (var x in new List<object>(st.Items)) yield return x;
                yield break;
            }

            var enumerable = o as IEnumerable<object>;
            if (enumerable != null)
            {
                foreach (var x in enumerable) yield return x;
                yield break;
            }

            var inst = o as PyInstance;
            if (inst != null)
            {
                object iterFn;
                if (inst.TryGetAttr("__iter__", out iterFn))
                {
                    object it = CallObject(iterFn, Py.EmptyArgs, null);
                    if (!ReferenceEquals(it, o))
                    {
                        foreach (var x in Iterate(it)) yield return x;
                        yield break;
                    }
                    object next;
                    if (TryGetAttr(it, "__next__", out next) || TryGetAttr(it, "next", out next))
                    {
                        while (true)
                        {
                            object v;
                            try { v = CallObject(next, Py.EmptyArgs, null); }
                            catch (PyError e) when (e.PyType == "StopIteration") { yield break; }
                            yield return v;
                        }
                    }
                }

                object getitem;
                if (inst.TryGetAttr("__getitem__", out getitem))
                {
                    for (long i = 0; ; i++)
                    {
                        object v;
                        try { v = CallObject(getitem, new object[] { i }, null); }
                        catch (PyError e) when (e.PyType == "IndexError" || e.PyType == "StopIteration") { yield break; }
                        yield return v;
                    }
                }
            }

            throw new PyError("TypeError", "'" + Py.TypeName(o) + "' object is not iterable");
        }

        public static int Len(object o)
        {
            if (o == null) throw new PyError("TypeError", "object of type 'NoneType' has no len()");
            if (o is string) return ((string)o).Length;
            if (o is byte[]) return ((byte[])o).Length;
            var sized = o as IPySized;
            if (sized != null) return sized.Length;
            var inst = o as PyInstance;
            if (inst != null)
            {
                object m;
                if (inst.TryGetAttr("__len__", out m)) return (int)Py.ToInt(CallObject(m, Py.EmptyArgs, null));
            }
            throw new PyError("TypeError", "object of type '" + Py.TypeName(o) + "' has no len()");
        }

        // ------------------------------------------------------------ str / repr

        public static string ToStr(object o)
        {
            if (o == null) return "None";
            if (o is bool) return ((bool)o) ? "True" : "False";
            if (o is string) return (string)o;
            if (o is long || o is int) return Py.ToInt(o).ToString(CultureInfo.InvariantCulture);
            if (o is double || o is float) return FormatFloat(Py.ToDouble(o));
            if (o is byte[]) return "b" + Quote(Encoding.UTF8.GetString((byte[])o));

            var inst = o as PyInstance;
            if (inst != null)
            {
                object m;
                if (inst.TryGetAttr("__str__", out m)) return ToStr(CallObject(m, Py.EmptyArgs, null));
                if (inst.TryGetAttr("__repr__", out m)) return ToStr(CallObject(m, Py.EmptyArgs, null));
                return "<" + (inst.Type != null ? inst.Type.Name : "object") + " object>";
            }

            if (o is PyList || o is PyTuple || o is PyDict || o is PySet) return Repr(o);
            return o.ToString();
        }

        /// <summary>Python's float repr: integral values still show a trailing ".0".</summary>
        public static string FormatFloat(double d)
        {
            if (double.IsNaN(d)) return "nan";
            if (double.IsPositiveInfinity(d)) return "inf";
            if (double.IsNegativeInfinity(d)) return "-inf";
            string s = d.ToString("R", CultureInfo.InvariantCulture);
            if (s.IndexOf('.') < 0 && s.IndexOf('e') < 0 && s.IndexOf('E') < 0 &&
                s.IndexOf("inf", StringComparison.Ordinal) < 0 && s.IndexOf("nan", StringComparison.Ordinal) < 0)
                s += ".0";
            return s;
        }

        public static string Repr(object o)
        {
            if (o == null) return "None";
            if (o is bool) return ((bool)o) ? "True" : "False";
            if (o is string) return Quote((string)o);
            if (o is long || o is int) return Py.ToInt(o).ToString(CultureInfo.InvariantCulture);
            if (o is double || o is float) return FormatFloat(Py.ToDouble(o));

            var t = o as PyTuple;
            if (t != null)
            {
                if (t.Items.Length == 0) return "()";
                var sb = new StringBuilder("(");
                for (int i = 0; i < t.Items.Length; i++)
                {
                    if (i > 0) sb.Append(", ");
                    sb.Append(Repr(t.Items[i]));
                }
                if (t.Items.Length == 1) sb.Append(',');
                return sb.Append(')').ToString();
            }

            var l = o as PyList;
            if (l != null)
            {
                var sb = new StringBuilder("[");
                for (int i = 0; i < l.Items.Count; i++)
                {
                    if (i > 0) sb.Append(", ");
                    sb.Append(Repr(l.Items[i]));
                }
                return sb.Append(']').ToString();
            }

            var d = o as PyDict;
            if (d != null)
            {
                var sb = new StringBuilder("{");
                bool first = true;
                foreach (var kv in d)
                {
                    if (!first) sb.Append(", ");
                    first = false;
                    sb.Append(Repr(kv.Key)).Append(": ").Append(Repr(kv.Value));
                }
                return sb.Append('}').ToString();
            }

            var s2 = o as PySet;
            if (s2 != null)
            {
                if (s2.Length == 0) return "set()";
                var sb = new StringBuilder("{");
                bool first = true;
                foreach (var x in s2.Items)
                {
                    if (!first) sb.Append(", ");
                    first = false;
                    sb.Append(Repr(x));
                }
                return sb.Append('}').ToString();
            }

            var inst = o as PyInstance;
            if (inst != null)
            {
                object m;
                if (inst.TryGetAttr("__repr__", out m)) return ToStr(CallObject(m, Py.EmptyArgs, null));
            }

            return ToStr(o);
        }

        public static string Quote(string s)
        {
            bool hasSingle = s.IndexOf('\'') >= 0;
            bool hasDouble = s.IndexOf('"') >= 0;
            char q = (hasSingle && !hasDouble) ? '"' : '\'';

            var sb = new StringBuilder(s.Length + 2);
            sb.Append(q);
            foreach (char c in s)
            {
                if (c == q || c == '\\') sb.Append('\\').Append(c);
                else if (c == '\n') sb.Append("\\n");
                else if (c == '\r') sb.Append("\\r");
                else if (c == '\t') sb.Append("\\t");
                else sb.Append(c);
            }
            return sb.Append(q).ToString();
        }
    }
}
