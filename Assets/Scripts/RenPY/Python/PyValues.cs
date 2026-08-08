using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RenPy.Python
{
    /// <summary>
    /// The runtime value model shared by the unpickler and the interpreter.
    ///
    /// Mapping to Python: None -> null, int -> long, float -> double, bool -> bool,
    /// str -> string, bytes -> byte[]. Everything else is one of the classes here.
    /// Everything is a boxed <see cref="object"/> so the unpickler can hand its output
    /// straight to the interpreter without a translation pass.
    /// </summary>
    public static class Py
    {
        public static readonly object[] EmptyArgs = new object[0];

        public static bool IsInt(object o) { return o is long || o is int; }
        public static bool IsFloat(object o) { return o is double || o is float; }
        public static bool IsNumber(object o) { return IsInt(o) || IsFloat(o) || o is bool; }

        public static long ToInt(object o)
        {
            if (o is long) return (long)o;
            if (o is int) return (int)o;
            if (o is bool) return ((bool)o) ? 1L : 0L;
            if (o is double) return (long)(double)o;
            if (o is float) return (long)(float)o;
            if (o is string) return long.Parse((string)o, CultureInfo.InvariantCulture);
            if (o == null) throw new PyError("TypeError", "int() argument must not be None");
            throw new PyError("TypeError", "cannot convert " + TypeName(o) + " to int");
        }

        public static double ToDouble(object o)
        {
            if (o is double) return (double)o;
            if (o is float) return (float)o;
            if (o is long) return (long)o;
            if (o is int) return (int)o;
            if (o is bool) return ((bool)o) ? 1.0 : 0.0;
            if (o is string) return double.Parse((string)o, CultureInfo.InvariantCulture);
            if (o == null) throw new PyError("TypeError", "float() argument must not be None");
            throw new PyError("TypeError", "cannot convert " + TypeName(o) + " to float");
        }

        /// <summary>Python truthiness.</summary>
        public static bool Truthy(object o)
        {
            if (o == null) return false;
            if (o is bool) return (bool)o;
            if (o is long) return (long)o != 0;
            if (o is int) return (int)o != 0;
            if (o is double) return (double)o != 0.0;
            if (o is float) return (float)o != 0f;
            if (o is string) return ((string)o).Length != 0;
            if (o is byte[]) return ((byte[])o).Length != 0;
            var sized = o as IPySized;
            if (sized != null) return sized.Length != 0;
            var inst = o as PyInstance;
            if (inst != null)
            {
                object m;
                if (inst.TryGetAttr("__bool__", out m) || inst.TryGetAttr("__nonzero__", out m))
                    return Truthy(PyOps.CallObject(m, EmptyArgs, null));
                if (inst.TryGetAttr("__len__", out m))
                    return ToInt(PyOps.CallObject(m, EmptyArgs, null)) != 0;
            }
            return true;
        }

        public static string TypeName(object o)
        {
            if (o == null) return "NoneType";
            if (o is bool) return "bool";
            if (o is long || o is int) return "int";
            if (o is double || o is float) return "float";
            if (o is string) return "str";
            if (o is byte[]) return "bytes";
            if (o is PyList) return "list";
            if (o is PyTuple) return "tuple";
            if (o is PyDict) return "dict";
            if (o is PySet) return "set";
            if (o is PyType) return "type";
            if (o is PyInstance) return ((PyInstance)o).Type != null ? ((PyInstance)o).Type.Name : "instance";
            if (o is IPyBindable) return "function";
            if (o is PyBoundMethod) return "method";
            if (o is PyNative) return "builtin_function_or_method";
            return o.GetType().Name;
        }
    }

    /// <summary>Anything with a Python len().</summary>
    public interface IPySized { int Length { get; } }

    /// <summary>Anything Python can call.</summary>
    public interface IPyCallable
    {
        object Call(object[] args, PyDict kwargs);
    }

    /// <summary>
    /// A callable that binds to an instance when looked up on its class, the way a
    /// Python function becomes a method. Implemented by the interpreter's PyFunction.
    /// </summary>
    public interface IPyBindable : IPyCallable { }

    /// <summary>Objects that expose Python attributes.</summary>
    public interface IPyAttrs
    {
        bool TryGetAttr(string name, out object value);
        void SetAttr(string name, object value);
    }

    // ---------------------------------------------------------------- exceptions

    /// <summary>A Python-level exception crossing the C# stack.</summary>
    public class PyError : Exception
    {
        public string PyType;
        public object Value;

        public PyError(string type, string message) : base(type + ": " + message)
        {
            PyType = type;
            Value = message;
        }

        public PyError(string type, string message, object value) : base(type + ": " + message)
        {
            PyType = type;
            Value = value;
        }
    }

    // ---------------------------------------------------------------- containers

    public sealed class PyTuple : IPySized, IEnumerable<object>
    {
        public static readonly PyTuple Empty = new PyTuple(Py.EmptyArgs);

        public readonly object[] Items;

        public PyTuple(object[] items) { Items = items ?? Py.EmptyArgs; }
        public PyTuple(List<object> items) { Items = items.ToArray(); }

        public int Length { get { return Items.Length; } }
        public object this[int i] { get { return Items[i]; } }

        public IEnumerator<object> GetEnumerator() { return ((IEnumerable<object>)Items).GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return Items.GetEnumerator(); }

        public override string ToString() { return PyOps.Repr(this); }
    }

    public sealed class PyList : IPySized, IEnumerable<object>
    {
        public readonly List<object> Items;

        public PyList() { Items = new List<object>(); }
        public PyList(List<object> items) { Items = items; }
        public PyList(IEnumerable<object> items) { Items = new List<object>(items); }

        public int Length { get { return Items.Count; } }
        public object this[int i] { get { return Items[i]; } set { Items[i] = value; } }

        public IEnumerator<object> GetEnumerator() { return Items.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return Items.GetEnumerator(); }

        public override string ToString() { return PyOps.Repr(this); }
    }

    /// <summary>
    /// Hashing/equality that follows Python rules: 1 == 1.0 == True hash alike, and
    /// tuples hash structurally so they work as dict keys.
    /// </summary>
    public sealed class PyKeyComparer : IEqualityComparer<object>
    {
        public static readonly PyKeyComparer Instance = new PyKeyComparer();

        public new bool Equals(object a, object b) { return PyOps.Eq(a, b); }

        public int GetHashCode(object o)
        {
            if (o == null) return 0;
            if (o is bool) return ((bool)o) ? 1 : 0;
            if (Py.IsNumber(o))
            {
                double d = Py.ToDouble(o);
                // Integral floats must hash like the equivalent int.
                if (d == Math.Floor(d) && !double.IsInfinity(d)) return ((long)d).GetHashCode();
                return d.GetHashCode();
            }
            if (o is string) return o.GetHashCode();
            var t = o as PyTuple;
            if (t != null)
            {
                int h = 17;
                for (int i = 0; i < t.Items.Length; i++)
                    h = unchecked(h * 31 + GetHashCode(t.Items[i]));
                return h;
            }
            return o.GetHashCode();
        }
    }

    public sealed class PyDict : IPySized, IEnumerable<KeyValuePair<object, object>>
    {
        public readonly Dictionary<object, object> Map;
        // Python 3.7+ dicts keep insertion order, and Ren'Py code relies on it.
        public readonly List<object> Order;

        public PyDict()
        {
            Map = new Dictionary<object, object>(PyKeyComparer.Instance);
            Order = new List<object>();
        }

        public int Length { get { return Map.Count; } }

        public bool TryGet(object key, out object value) { return Map.TryGetValue(key, out value); }
        public bool Contains(object key) { return Map.ContainsKey(key); }

        public object Get(object key, object dflt)
        {
            object v;
            return Map.TryGetValue(key, out v) ? v : dflt;
        }

        public void Set(object key, object value)
        {
            if (!Map.ContainsKey(key)) Order.Add(key);
            Map[key] = value;
        }

        public bool Remove(object key)
        {
            if (!Map.Remove(key)) return false;
            for (int i = 0; i < Order.Count; i++)
            {
                if (PyOps.Eq(Order[i], key)) { Order.RemoveAt(i); break; }
            }
            return true;
        }

        public void Clear() { Map.Clear(); Order.Clear(); }

        /// <summary>Keys in insertion order.</summary>
        public List<object> Keys { get { return new List<object>(Order); } }

        public List<object> Values
        {
            get
            {
                var rv = new List<object>(Order.Count);
                for (int i = 0; i < Order.Count; i++) rv.Add(Map[Order[i]]);
                return rv;
            }
        }

        public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        {
            for (int i = 0; i < Order.Count; i++)
                yield return new KeyValuePair<object, object>(Order[i], Map[Order[i]]);
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        /// <summary>Convenience for the many places Ren'Py uses string-keyed dicts.</summary>
        public object GetStr(string key, object dflt)
        {
            object v;
            return Map.TryGetValue(key, out v) ? v : dflt;
        }

        public override string ToString() { return PyOps.Repr(this); }
    }

    public sealed class PySet : IPySized, IEnumerable<object>
    {
        public readonly HashSet<object> Items;
        public readonly bool Frozen;

        public PySet() { Items = new HashSet<object>(PyKeyComparer.Instance); }
        public PySet(bool frozen) : this() { Frozen = frozen; }
        public PySet(IEnumerable<object> src, bool frozen = false) : this(frozen)
        {
            foreach (var o in src) Items.Add(o);
        }

        public int Length { get { return Items.Count; } }

        public IEnumerator<object> GetEnumerator() { return Items.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return Items.GetEnumerator(); }

        public override string ToString() { return PyOps.Repr(this); }
    }

    /// <summary>A slice object, produced by `a[x:y:z]`.</summary>
    public sealed class PySlice
    {
        public readonly object Start, Stop, Step;
        public PySlice(object start, object stop, object step) { Start = start; Stop = stop; Step = step; }
    }

    // ---------------------------------------------------------------- callables

    /// <summary>A C#-implemented function exposed to Python (the renpy.* API, builtins).</summary>
    public sealed class PyNative : IPyCallable
    {
        public readonly string Name;
        public readonly Func<object[], PyDict, object> Fn;

        public PyNative(string name, Func<object[], PyDict, object> fn) { Name = name; Fn = fn; }

        public object Call(object[] args, PyDict kwargs) { return Fn(args, kwargs); }
        public override string ToString() { return "<built-in function " + Name + ">"; }
    }

    /// <summary>A method bound to an instance; calling it prepends `self`.</summary>
    public sealed class PyBoundMethod : IPyCallable
    {
        public readonly object Self;
        public readonly object Function;

        public PyBoundMethod(object self, object function) { Self = self; Function = function; }

        public object Call(object[] args, PyDict kwargs)
        {
            var full = new object[args.Length + 1];
            full[0] = Self;
            Array.Copy(args, 0, full, 1, args.Length);
            return PyOps.CallObject(Function, full, kwargs);
        }

        public override string ToString() { return "<bound method>"; }
    }

    // ---------------------------------------------------------------- classes

    /// <summary>A Python class. Calling it constructs a <see cref="PyInstance"/>.</summary>
    public sealed class PyType : IPyCallable, IPyAttrs
    {
        public string Name;
        public List<PyType> Bases = new List<PyType>();
        public PyDict Dict = new PyDict();
        /// <summary>Set for classes we stub out for unpickling but never really implement.</summary>
        public bool IsStub;
        /// <summary>Fully qualified "module.Name", used by the unpickler registry.</summary>
        public string QualName;

        public PyType(string name) { Name = name; QualName = name; }

        public bool TryGetAttr(string name, out object value)
        {
            if (Dict.TryGet(name, out value)) return true;
            for (int i = 0; i < Bases.Count; i++)
                if (Bases[i].TryGetAttr(name, out value)) return true;
            value = null;
            return false;
        }

        public void SetAttr(string name, object value) { Dict.Set(name, value); }

        public bool IsSubclassOf(PyType other)
        {
            if (ReferenceEquals(this, other)) return true;
            for (int i = 0; i < Bases.Count; i++)
                if (Bases[i].IsSubclassOf(other)) return true;
            return false;
        }

        public object Call(object[] args, PyDict kwargs)
        {
            var inst = new PyInstance(this);
            object init;
            if (TryGetAttr("__init__", out init))
            {
                var full = new object[args.Length + 1];
                full[0] = inst;
                Array.Copy(args, 0, full, 1, args.Length);
                PyOps.CallObject(init, full, kwargs);
            }
            return inst;
        }

        public override string ToString() { return "<class '" + Name + "'>"; }
    }

    /// <summary>
    /// An instance of a Python class. Also the landing place for unpickled objects
    /// whose class we only stub — the state dict is preserved verbatim so the AST
    /// builder can read fields off it.
    /// </summary>
    public class PyInstance : IPyAttrs
    {
        public PyType Type;
        public readonly PyDict Dict = new PyDict();

        public PyInstance(PyType type) { Type = type; }

        public virtual bool TryGetAttr(string name, out object value)
        {
            if (Dict.TryGet(name, out value)) return true;
            if (Type != null && Type.TryGetAttr(name, out value))
            {
                // Functions found on the class bind to this instance.
                if (value is IPyBindable || value is PyNative) value = new PyBoundMethod(this, value);
                return true;
            }
            value = null;
            return false;
        }

        public virtual void SetAttr(string name, object value) { Dict.Set(name, value); }

        /// <summary>Field access for unpickled objects, which store plain state.</summary>
        public object Field(string name, object dflt = null) { return Dict.GetStr(name, dflt); }

        public string StrField(string name, string dflt = null)
        {
            object v = Dict.GetStr(name, null);
            return v as string ?? dflt;
        }

        public int IntField(string name, int dflt = 0)
        {
            object v = Dict.GetStr(name, null);
            return v == null ? dflt : (int)Py.ToInt(v);
        }

        public override string ToString()
        {
            object repr;
            if (TryGetAttr("__repr__", out repr) && repr is PyBoundMethod)
                return PyOps.ToStr(((PyBoundMethod)repr).Call(Py.EmptyArgs, null));
            return "<" + (Type != null ? Type.Name : "object") + " object>";
        }
    }
}
