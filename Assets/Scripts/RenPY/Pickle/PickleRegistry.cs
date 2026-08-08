using System;
using System.Collections.Generic;
using System.Text;
using RenPy.Python;

namespace RenPy.Pickle
{
    /// <summary>
    /// Resolves pickled "module.name" references. Python builtins map to real
    /// behaviour; every Ren'Py class becomes a cached stub <see cref="PyType"/> so
    /// instances keep their fields and the AST builder can read them by name.
    /// </summary>
    public static class PickleRegistry
    {
        static readonly Dictionary<string, PyType> stubs = new Dictionary<string, PyType>();
        static readonly Dictionary<string, PyNative> builtins = new Dictionary<string, PyNative>();

        static PickleRegistry()
        {
            RegisterBuiltins();
        }

        static void RegisterBuiltins()
        {
            // Python 2 spells the module __builtin__; Python 3 spells it builtins.
            foreach (string mod in new[] { "__builtin__", "builtins" })
            {
                Add(mod, "set", (a, k) => a.Length > 0 ? new PySet(PyOps.Iterate(a[0])) : new PySet());
                Add(mod, "frozenset", (a, k) => a.Length > 0 ? new PySet(PyOps.Iterate(a[0]), true) : new PySet(true));
                Add(mod, "list", (a, k) => a.Length > 0 ? new PyList(PyOps.Iterate(a[0])) : new PyList());
                Add(mod, "dict", (a, k) => CopyDict(a));
                Add(mod, "tuple", (a, k) => a.Length > 0 ? new PyTuple(new List<object>(PyOps.Iterate(a[0]))) : PyTuple.Empty);
                Add(mod, "object", (a, k) => new PyInstance(GetStub("builtins", "object")));
                Add(mod, "str", (a, k) => a.Length > 0 ? PyOps.ToStr(a[0]) : "");
                Add(mod, "unicode", (a, k) => a.Length > 0 ? PyOps.ToStr(a[0]) : "");
                Add(mod, "int", (a, k) => a.Length > 0 ? Py.ToInt(a[0]) : 0L);
                Add(mod, "float", (a, k) => a.Length > 0 ? Py.ToDouble(a[0]) : 0.0);
                Add(mod, "bool", (a, k) => a.Length > 0 && Py.Truthy(a[0]));
                Add(mod, "bytearray", (a, k) => a.Length > 0 ? ToBytes(a[0]) : new byte[0]);
                Add(mod, "bytes", (a, k) => a.Length > 0 ? ToBytes(a[0]) : new byte[0]);
                Add(mod, "getattr", (a, k) => PyOps.GetAttr(a[0], PyOps.ToStr(a[1])));
            }

            // collections.OrderedDict and defaultdict both behave close enough to dict
            // for the data Ren'Py pickles.
            Add("collections", "OrderedDict", (a, k) => CopyDict(a));
            Add("collections", "defaultdict", (a, k) => CopyDict(a.Length > 1 ? new[] { a[1] } : Py.EmptyArgs));

            // Ren'Py's PyExpr subclasses str and carries the expression text as its
            // first constructor argument; downstream code only ever wants that text.
            foreach (string mod in new[] { "renpy.ast", "renpy.astsupport" })
                Add(mod, "PyExpr", (a, k) => a.Length > 0 ? PyOps.ToStr(a[0]) : "");

            // The Revertable* containers exist so Ren'Py can roll back state. They
            // behave as plain containers; rollback is handled by the runtime instead.
            foreach (string mod in new[] { "renpy.revertable", "renpy.python" })
            {
                Add(mod, "RevertableDict", (a, k) => CopyDict(a));
                Add(mod, "RevertableList", (a, k) => a.Length > 0 ? new PyList(PyOps.Iterate(a[0])) : new PyList());
                Add(mod, "RevertableSet", (a, k) => a.Length > 0 ? new PySet(PyOps.Iterate(a[0])) : new PySet());
            }

            // _codecs.encode shows up when Python 3 pickles Python 2 bytes.
            Add("_codecs", "encode", (a, k) =>
            {
                string s = PyOps.ToStr(a[0]);
                var bytes = new byte[s.Length];
                for (int i = 0; i < s.Length; i++) bytes[i] = (byte)s[i];
                return bytes;
            });
        }

        static PyDict CopyDict(object[] a)
        {
            var rv = new PyDict();
            if (a.Length > 0)
            {
                var src = a[0] as PyDict;
                if (src != null) foreach (var kv in src) rv.Set(kv.Key, kv.Value);
            }
            return rv;
        }

        static byte[] ToBytes(object o)
        {
            var b = o as byte[];
            if (b != null) return b;
            string s = PyOps.ToStr(o);
            var rv = new byte[s.Length];
            for (int i = 0; i < s.Length; i++) rv[i] = (byte)s[i];
            return rv;
        }

        static void Add(string module, string name, Func<object[], PyDict, object> fn)
        {
            builtins[module + "." + name] = new PyNative(module + "." + name, fn);
        }

        /// <summary>The fallback resolver used when no game-specific one matches.</summary>
        public static object Default(string module, string name)
        {
            PyNative native;
            if (builtins.TryGetValue(module + "." + name, out native)) return native;
            return GetStub(module, name);
        }

        /// <summary>Gets (or creates) the cached stub class for a pickled type.</summary>
        public static PyType GetStub(string module, string name)
        {
            string qual = module + "." + name;
            PyType t;
            if (stubs.TryGetValue(qual, out t)) return t;

            t = new PyType(name) { QualName = qual, IsStub = true };
            stubs[qual] = t;
            return t;
        }

        /// <summary>True when the instance is of the named pickled class.</summary>
        public static bool Is(object o, string qualName)
        {
            var inst = o as PyInstance;
            return inst != null && inst.Type != null && inst.Type.QualName == qualName;
        }

        /// <summary>The pickled class name of an instance, or null.</summary>
        public static string QualNameOf(object o)
        {
            var inst = o as PyInstance;
            return inst != null && inst.Type != null ? inst.Type.QualName : null;
        }
    }
}
