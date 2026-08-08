using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using RenPy.Python;

namespace RenPy.Pickle
{
    /// <summary>
    /// A Python unpickler covering protocols 0-4, producing values in the shared
    /// <see cref="RenPy.Py"/> object model. No reflection or code generation, so it
    /// runs under IL2CPP.
    ///
    /// Classes are resolved through <see cref="ClassResolver"/>; anything unknown
    /// becomes a <see cref="PyInstance"/> of a stub <see cref="PyType"/> whose state
    /// dict preserves the pickled fields verbatim.
    /// </summary>
    public class PickleReader
    {
        // Protocol 0 (text) opcodes
        const byte MARK = (byte)'(';
        const byte STOP = (byte)'.';
        const byte POP = (byte)'0';
        const byte POP_MARK = (byte)'1';
        const byte DUP = (byte)'2';
        const byte FLOAT = (byte)'F';
        const byte INT = (byte)'I';
        const byte BININT = (byte)'J';
        const byte BININT1 = (byte)'K';
        const byte LONG = (byte)'L';
        const byte BININT2 = (byte)'M';
        const byte NONE = (byte)'N';
        const byte PERSID = (byte)'P';
        const byte BINPERSID = (byte)'Q';
        const byte REDUCE = (byte)'R';
        const byte STRING = (byte)'S';
        const byte BINSTRING = (byte)'T';
        const byte SHORT_BINSTRING = (byte)'U';
        const byte UNICODE = (byte)'V';
        const byte BINUNICODE = (byte)'X';
        const byte APPEND = (byte)'a';
        const byte BUILD = (byte)'b';
        const byte GLOBAL = (byte)'c';
        const byte DICT = (byte)'d';
        const byte EMPTY_DICT = (byte)'}';
        const byte APPENDS = (byte)'e';
        const byte GET = (byte)'g';
        const byte BINGET = (byte)'h';
        const byte INST = (byte)'i';
        const byte LONG_BINGET = (byte)'j';
        const byte LIST = (byte)'l';
        const byte EMPTY_LIST = (byte)']';
        const byte OBJ = (byte)'o';
        const byte PUT = (byte)'p';
        const byte BINPUT = (byte)'q';
        const byte LONG_BINPUT = (byte)'r';
        const byte SETITEM = (byte)'s';
        const byte TUPLE = (byte)'t';
        const byte EMPTY_TUPLE = (byte)')';
        const byte SETITEMS = (byte)'u';
        const byte BINFLOAT = (byte)'G';

        // Protocol 2
        const byte PROTO = 0x80;
        const byte NEWOBJ = 0x81;
        const byte EXT1 = 0x82;
        const byte EXT2 = 0x83;
        const byte EXT4 = 0x84;
        const byte TUPLE1 = 0x85;
        const byte TUPLE2 = 0x86;
        const byte TUPLE3 = 0x87;
        const byte NEWTRUE = 0x88;
        const byte NEWFALSE = 0x89;
        const byte LONG1 = 0x8a;
        const byte LONG4 = 0x8b;

        // Protocol 3
        const byte BINBYTES = (byte)'B';
        const byte SHORT_BINBYTES = (byte)'C';

        // Protocol 4
        const byte SHORT_BINUNICODE = 0x8c;
        const byte BINUNICODE8 = 0x8d;
        const byte BINBYTES8 = 0x8e;
        const byte EMPTY_SET = 0x8f;
        const byte FROZENSET = 0x90;
        const byte ADDITEMS = 0x91;
        const byte NEWOBJ_EX = 0x92;
        const byte STACK_GLOBAL = 0x93;
        const byte MEMOIZE = 0x94;
        const byte FRAME = 0x95;

        /// <summary>Resolves "module.name" to a callable or class. May return null.</summary>
        public Func<string, string, object> ClassResolver;

        readonly byte[] data;
        int pos;

        readonly List<object> stack = new List<object>();
        readonly List<int> marks = new List<int>();
        readonly Dictionary<int, object> memo = new Dictionary<int, object>();

        public PickleReader(byte[] bytes) { data = bytes; }

        public static object Load(byte[] bytes, Func<string, string, object> resolver)
        {
            var r = new PickleReader(bytes) { ClassResolver = resolver };
            return r.Run();
        }

        // ------------------------------------------------------------ stack helpers

        void Push(object o) { stack.Add(o); }

        object Pop()
        {
            int i = stack.Count - 1;
            if (i < 0) throw new PickleException("unpickling stack underflow");
            object o = stack[i];
            stack.RemoveAt(i);
            return o;
        }

        object Peek()
        {
            if (stack.Count == 0) throw new PickleException("unpickling stack underflow");
            return stack[stack.Count - 1];
        }

        int PopMark()
        {
            if (marks.Count == 0) throw new PickleException("could not find MARK");
            int m = marks[marks.Count - 1];
            marks.RemoveAt(marks.Count - 1);
            return m;
        }

        /// <summary>Removes and returns everything above the topmost mark.</summary>
        List<object> PopSince()
        {
            int m = PopMark();
            var rv = stack.GetRange(m, stack.Count - m);
            stack.RemoveRange(m, stack.Count - m);
            return rv;
        }

        // ------------------------------------------------------------ byte readers

        byte ReadByte()
        {
            if (pos >= data.Length) throw new PickleException("unexpected end of pickle stream");
            return data[pos++];
        }

        int ReadInt32()
        {
            int v = data[pos] | (data[pos + 1] << 8) | (data[pos + 2] << 16) | (data[pos + 3] << 24);
            pos += 4;
            return v;
        }

        long ReadInt64()
        {
            long v = BitConverter.ToInt64(data, pos);
            pos += 8;
            return v;
        }

        ushort ReadUInt16()
        {
            ushort v = (ushort)(data[pos] | (data[pos + 1] << 8));
            pos += 2;
            return v;
        }

        /// <summary>Reads a newline-terminated ASCII token (protocol 0).</summary>
        string ReadLine()
        {
            int start = pos;
            while (pos < data.Length && data[pos] != (byte)'\n') pos++;
            string s = Encoding.ASCII.GetString(data, start, pos - start);
            if (pos < data.Length) pos++;
            return s.TrimEnd('\r');
        }

        byte[] ReadBytes(int n)
        {
            if (pos + n > data.Length) throw new PickleException("unexpected end of pickle stream");
            var rv = new byte[n];
            Buffer.BlockCopy(data, pos, rv, 0, n);
            pos += n;
            return rv;
        }

        /// <summary>
        /// Decodes a protocol-0/1/2 `str`. Under Python 2 these are bytes; Ren'Py uses
        /// them for identifiers and file names, so UTF-8 with a Latin-1 fallback is right.
        /// </summary>
        static string DecodeStr(byte[] bytes)
        {
            try
            {
                return new UTF8Encoding(false, true).GetString(bytes);
            }
            catch (ArgumentException)
            {
                var sb = new StringBuilder(bytes.Length);
                foreach (byte b in bytes) sb.Append((char)b);
                return sb.ToString();
            }
        }

        // ------------------------------------------------------------ main loop

        public object Run()
        {
            while (true)
            {
                byte op = ReadByte();

                switch (op)
                {
                    case PROTO: ReadByte(); break;
                    case FRAME: pos += 8; break;

                    case STOP: return Pop();

                    case MARK: marks.Add(stack.Count); break;
                    case POP:
                        if (marks.Count > 0 && marks[marks.Count - 1] == stack.Count) marks.RemoveAt(marks.Count - 1);
                        else Pop();
                        break;
                    case POP_MARK: PopSince(); break;
                    case DUP: Push(Peek()); break;

                    case NONE: Push(null); break;
                    case NEWTRUE: Push(true); break;
                    case NEWFALSE: Push(false); break;

                    case BININT: Push((long)ReadInt32()); break;
                    case BININT1: Push((long)ReadByte()); break;
                    case BININT2: Push((long)ReadUInt16()); break;

                    case INT:
                    {
                        string s = ReadLine();
                        // Protocol 0 encodes bools as I01/I00.
                        if (s == "01") Push(true);
                        else if (s == "00") Push(false);
                        else Push(long.Parse(s, CultureInfo.InvariantCulture));
                        break;
                    }

                    case LONG:
                    {
                        string s = ReadLine().TrimEnd('L');
                        Push(long.Parse(s, CultureInfo.InvariantCulture));
                        break;
                    }

                    case LONG1: Push(DecodeLong(ReadBytes(ReadByte()))); break;
                    case LONG4: Push(DecodeLong(ReadBytes(ReadInt32()))); break;

                    case FLOAT: Push(double.Parse(ReadLine(), CultureInfo.InvariantCulture)); break;
                    case BINFLOAT:
                    {
                        // Big-endian IEEE 754.
                        var b = ReadBytes(8);
                        Array.Reverse(b);
                        Push(BitConverter.ToDouble(b, 0));
                        break;
                    }

                    case STRING: Push(DecodeQuoted(ReadLine())); break;
                    case BINSTRING: Push(DecodeStr(ReadBytes(ReadInt32()))); break;
                    case SHORT_BINSTRING: Push(DecodeStr(ReadBytes(ReadByte()))); break;

                    case UNICODE: Push(DecodeRawUnicode(ReadLine())); break;
                    case BINUNICODE: Push(Encoding.UTF8.GetString(ReadBytes(ReadInt32()))); break;
                    case SHORT_BINUNICODE: Push(Encoding.UTF8.GetString(ReadBytes(ReadByte()))); break;
                    case BINUNICODE8: Push(Encoding.UTF8.GetString(ReadBytes((int)ReadInt64()))); break;

                    case BINBYTES: Push(ReadBytes(ReadInt32())); break;
                    case SHORT_BINBYTES: Push(ReadBytes(ReadByte())); break;
                    case BINBYTES8: Push(ReadBytes((int)ReadInt64())); break;

                    case EMPTY_LIST: Push(new PyList()); break;
                    case EMPTY_DICT: Push(new PyDict()); break;
                    case EMPTY_TUPLE: Push(PyTuple.Empty); break;
                    case EMPTY_SET: Push(new PySet()); break;

                    case LIST: Push(new PyList(PopSince())); break;
                    case TUPLE: Push(new PyTuple(PopSince().ToArray())); break;
                    case TUPLE1: { var a = Pop(); Push(new PyTuple(new[] { a })); break; }
                    case TUPLE2: { var b = Pop(); var a = Pop(); Push(new PyTuple(new[] { a, b })); break; }
                    case TUPLE3: { var c = Pop(); var b = Pop(); var a = Pop(); Push(new PyTuple(new[] { a, b, c })); break; }

                    case DICT:
                    {
                        var items = PopSince();
                        var d = new PyDict();
                        for (int i = 0; i + 1 < items.Count; i += 2) d.Set(items[i], items[i + 1]);
                        Push(d);
                        break;
                    }

                    case FROZENSET: Push(new PySet(PopSince(), true)); break;

                    case APPEND:
                    {
                        object v = Pop();
                        ListTarget(Peek()).Add(v);
                        break;
                    }

                    case APPENDS:
                    {
                        var items = PopSince();
                        ListTarget(Peek()).AddRange(items);
                        break;
                    }

                    case ADDITEMS:
                    {
                        var items = PopSince();
                        var set = Peek() as PySet;
                        if (set == null) throw new PickleException("ADDITEMS on non-set");
                        foreach (var o in items) set.Items.Add(o);
                        break;
                    }

                    case SETITEM:
                    {
                        object v = Pop(), k = Pop();
                        DictTarget(Peek()).Set(k, v);
                        break;
                    }

                    case SETITEMS:
                    {
                        var items = PopSince();
                        var d = DictTarget(Peek());
                        for (int i = 0; i + 1 < items.Count; i += 2) d.Set(items[i], items[i + 1]);
                        break;
                    }

                    case PUT: memo[int.Parse(ReadLine(), CultureInfo.InvariantCulture)] = Peek(); break;
                    case BINPUT: memo[ReadByte()] = Peek(); break;
                    case LONG_BINPUT: memo[ReadInt32()] = Peek(); break;
                    case MEMOIZE: memo[memo.Count] = Peek(); break;

                    case GET: Push(GetMemo(int.Parse(ReadLine(), CultureInfo.InvariantCulture))); break;
                    case BINGET: Push(GetMemo(ReadByte())); break;
                    case LONG_BINGET: Push(GetMemo(ReadInt32())); break;

                    case GLOBAL:
                    {
                        string module = ReadLine();
                        string name = ReadLine();
                        Push(Resolve(module, name));
                        break;
                    }

                    case STACK_GLOBAL:
                    {
                        string name = PyOps.ToStr(Pop());
                        string module = PyOps.ToStr(Pop());
                        Push(Resolve(module, name));
                        break;
                    }

                    case REDUCE:
                    {
                        object args = Pop();
                        object fn = Pop();
                        Push(Reduce(fn, args as PyTuple));
                        break;
                    }

                    case NEWOBJ:
                    {
                        object args = Pop();
                        object cls = Pop();
                        Push(NewObject(cls, args as PyTuple, null));
                        break;
                    }

                    case NEWOBJ_EX:
                    {
                        object kwargs = Pop();
                        object args = Pop();
                        object cls = Pop();
                        Push(NewObject(cls, args as PyTuple, kwargs as PyDict));
                        break;
                    }

                    case OBJ:
                    {
                        var items = PopSince();
                        object cls = items[0];
                        items.RemoveAt(0);
                        Push(NewObject(cls, new PyTuple(items.ToArray()), null));
                        break;
                    }

                    case INST:
                    {
                        string module = ReadLine();
                        string name = ReadLine();
                        var items = PopSince();
                        Push(NewObject(Resolve(module, name), new PyTuple(items.ToArray()), null));
                        break;
                    }

                    case BUILD:
                    {
                        object state = Pop();
                        ApplyState(Peek(), state);
                        break;
                    }

                    case PERSID: Push(new PersistentId(ReadLine())); break;
                    case BINPERSID: Push(new PersistentId(PyOps.ToStr(Pop()))); break;

                    case EXT1: case EXT2: case EXT4:
                        throw new PickleException("extension registry opcodes are not supported");

                    default:
                        throw new PickleException(
                            "unknown pickle opcode 0x" + op.ToString("x2") + " ('" + (char)op + "') at " + (pos - 1));
                }
            }
        }

        object GetMemo(int key)
        {
            object v;
            if (!memo.TryGetValue(key, out v)) throw new PickleException("memo key " + key + " not found");
            return v;
        }

        static List<object> ListTarget(object o)
        {
            var l = o as PyList;
            if (l != null) return l.Items;
            var inst = o as PyInstance;
            if (inst != null)
            {
                // Subclasses of list (Ren'Py's RevertableList) keep their items here.
                var backing = inst.Field(ListBackingKey) as PyList;
                if (backing == null)
                {
                    backing = new PyList();
                    inst.Dict.Set(ListBackingKey, backing);
                }
                return backing.Items;
            }
            throw new PickleException("APPEND on " + Py.TypeName(o));
        }

        static PyDict DictTarget(object o)
        {
            var d = o as PyDict;
            if (d != null) return d;
            var inst = o as PyInstance;
            if (inst != null)
            {
                var backing = inst.Field(DictBackingKey) as PyDict;
                if (backing == null)
                {
                    backing = new PyDict();
                    inst.Dict.Set(DictBackingKey, backing);
                }
                return backing;
            }
            throw new PickleException("SETITEM on " + Py.TypeName(o));
        }

        /// <summary>Where list/dict payloads land for instances of list/dict subclasses.</summary>
        public const string ListBackingKey = "__list_items__";
        public const string DictBackingKey = "__dict_items__";

        object Resolve(string module, string name)
        {
            if (ClassResolver != null)
            {
                object o = ClassResolver(module, name);
                if (o != null) return o;
            }
            return PickleRegistry.Default(module, name);
        }

        object Reduce(object fn, PyTuple args)
        {
            object[] argv = args != null ? args.Items : Py.EmptyArgs;

            var native = fn as PyNative;
            if (native != null) return native.Call(argv, null);

            var type = fn as PyType;
            if (type != null) return NewObject(type, args, null);

            var callable = fn as IPyCallable;
            if (callable != null) return callable.Call(argv, null);

            throw new PickleException("cannot REDUCE with " + Py.TypeName(fn));
        }

        object NewObject(object cls, PyTuple args, PyDict kwargs)
        {
            var type = cls as PyType;
            if (type == null)
            {
                var native = cls as PyNative;
                if (native != null) return native.Call(args != null ? args.Items : Py.EmptyArgs, kwargs);
                throw new PickleException("NEWOBJ with non-class " + Py.TypeName(cls));
            }

            var inst = new PyInstance(type);

            // Ren'Py's PyExpr and other str subclasses carry their text as the first arg.
            if (args != null && args.Items.Length > 0)
                inst.Dict.Set(ConstructorArgsKey, args);

            return inst;
        }

        /// <summary>Constructor arguments preserved for classes reconstructed via REDUCE/NEWOBJ.</summary>
        public const string ConstructorArgsKey = "__reduce_args__";

        /// <summary>
        /// Applies BUILD state. Ren'Py nodes use __slots__, so their state arrives as
        /// a (instance_dict, slots_dict) tuple; PyCode uses a plain positional tuple.
        /// </summary>
        static void ApplyState(object target, object state)
        {
            var inst = target as PyInstance;
            if (inst == null)
            {
                // Applying state to a plain container is a no-op we can safely ignore.
                return;
            }

            var d = state as PyDict;
            if (d != null)
            {
                foreach (var kv in d) inst.Dict.Set(PyOps.ToStr(kv.Key), kv.Value);
                return;
            }

            var t = state as PyTuple;
            if (t != null && t.Items.Length == 2 &&
                (t.Items[0] == null || t.Items[0] is PyDict) &&
                (t.Items[1] == null || t.Items[1] is PyDict))
            {
                for (int i = 0; i < 2; i++)
                {
                    var part = t.Items[i] as PyDict;
                    if (part == null) continue;
                    foreach (var kv in part) inst.Dict.Set(PyOps.ToStr(kv.Key), kv.Value);
                }
                return;
            }

            // Anything else (PyCode's positional tuple) is stashed for the AST builder.
            inst.Dict.Set(RawStateKey, state);
        }

        /// <summary>Non-dict BUILD state, kept verbatim.</summary>
        public const string RawStateKey = "__state__";

        // ------------------------------------------------------------ literals

        static long DecodeLong(byte[] bytes)
        {
            if (bytes.Length == 0) return 0;
            // Little-endian two's complement, arbitrary width. Ren'Py never exceeds 64 bits.
            long v = 0;
            for (int i = bytes.Length - 1; i >= 0; i--) v = (v << 8) | bytes[i];
            if ((bytes[bytes.Length - 1] & 0x80) != 0 && bytes.Length < 8)
                v -= 1L << (bytes.Length * 8);
            return v;
        }

        /// <summary>Decodes a protocol-0 repr-quoted string.</summary>
        static string DecodeQuoted(string s)
        {
            s = s.Trim();
            if (s.Length >= 2 && (s[0] == '\'' || s[0] == '"') && s[s.Length - 1] == s[0])
                s = s.Substring(1, s.Length - 2);
            return Unescape(s);
        }

        static string DecodeRawUnicode(string s) { return Unescape(s); }

        static string Unescape(string s)
        {
            if (s.IndexOf('\\') < 0) return s;
            var sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != '\\') { sb.Append(s[i]); continue; }
                i++;
                if (i >= s.Length) break;
                char c = s[i];
                switch (c)
                {
                    case 'n': sb.Append('\n'); break;
                    case 't': sb.Append('\t'); break;
                    case 'r': sb.Append('\r'); break;
                    case '0': sb.Append('\0'); break;
                    case '\\': sb.Append('\\'); break;
                    case '\'': sb.Append('\''); break;
                    case '"': sb.Append('"'); break;
                    case 'x':
                        sb.Append((char)Convert.ToInt32(s.Substring(i + 1, 2), 16));
                        i += 2;
                        break;
                    case 'u':
                        sb.Append((char)Convert.ToInt32(s.Substring(i + 1, 4), 16));
                        i += 4;
                        break;
                    default: sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
    }

    /// <summary>A persistent id reference; Ren'Py's script pickles never use these.</summary>
    public sealed class PersistentId
    {
        public readonly string Id;
        public PersistentId(string id) { Id = id; }
        public override string ToString() { return "<persid " + Id + ">"; }
    }

    public class PickleException : Exception
    {
        public PickleException(string message) : base(message) { }
    }
}
