using System;
using System.Collections.Generic;
using System.Text;

namespace RenPy.Python
{
    /// <summary>How a statement finished, so loops and functions can unwind.</summary>
    public enum Flow { Normal, Break, Continue, Return }

    /// <summary>
    /// One execution scope. Module-level code uses a frame whose locals and globals
    /// are the same dict, which is how Ren'Py's `store` namespace behaves.
    /// </summary>
    public sealed class PyFrame
    {
        public PyDict Locals;
        public PyDict Globals;
        /// <summary>Lexically enclosing frame, for closures.</summary>
        public PyFrame Parent;
        /// <summary>Names bound by a `global` declaration in this frame.</summary>
        public HashSet<string> GlobalNames;
        public object ReturnValue;

        public PyFrame(PyDict locals, PyDict globals, PyFrame parent = null)
        {
            Locals = locals;
            Globals = globals;
            Parent = parent;
        }
    }

    /// <summary>A function defined by Python source.</summary>
    public sealed class PyFunction : IPyBindable, IPyAttrs
    {
        public string Name = "<lambda>";
        public PyParams Params;
        public List<PyStmt> Body;
        /// <summary>Single-expression body, for lambdas.</summary>
        public PyExprNode ExprBody;
        public PyFrame Closure;
        public PyDict Globals;
        public PyInterp Interp;
        public readonly PyDict Attributes = new PyDict();

        public object Call(object[] args, PyDict kwargs)
        {
            return Interp.CallFunction(this, args, kwargs);
        }

        public bool TryGetAttr(string name, out object value)
        {
            if (name == "__name__") { value = Name; return true; }
            return Attributes.TryGet(name, out value);
        }

        public void SetAttr(string name, object value) { Attributes.Set(name, value); }

        public override string ToString() { return "<function " + Name + ">"; }
    }

    /// <summary>
    /// A tree-walking Python interpreter. Deliberately free of reflection and code
    /// generation so it runs under IL2CPP on Android.
    /// </summary>
    public class PyInterp
    {
        /// <summary>Global builtins (len, range, str, ...).</summary>
        public readonly PyDict Builtins = new PyDict();

        /// <summary>Named namespaces: "store", "store.gui", "persistent", ...</summary>
        public readonly Dictionary<string, PyDict> Stores = new Dictionary<string, PyDict>();

        /// <summary>Modules resolvable by `import`; keyed by dotted name.</summary>
        public readonly Dictionary<string, object> Modules = new Dictionary<string, object>();

        /// <summary>Raised for diagnostics when a name cannot be resolved.</summary>
        public Action<string> Warn;

        /// <summary>Guards against runaway recursion, which would kill the player.</summary>
        public int MaxDepth = 120;
        int depth;

        /// <summary>
        /// The default `store` namespace. Names not found in a more specific store
        /// fall back to it, matching how Ren'Py's submodule stores resolve.
        /// </summary>
        public PyDict DefaultStore { get; private set; }

        public PyInterp()
        {
            PyBuiltins.Install(this);
            DefaultStore = GetStore("store");
        }

        public PyDict GetStore(string name)
        {
            PyDict store;
            if (!Stores.TryGetValue(name, out store))
            {
                store = new PyDict();
                Stores[name] = store;
            }
            return store;
        }

        // ---------------------------------------------------------------- compile

        readonly Dictionary<string, PyModule> moduleCache = new Dictionary<string, PyModule>();
        readonly Dictionary<string, PyExprNode> exprCache = new Dictionary<string, PyExprNode>();

        public PyModule CompileModule(string source, string filename)
        {
            PyModule cached;
            if (moduleCache.TryGetValue(source, out cached)) return cached;

            var module = PyParser.ParseModule(source, filename);
            moduleCache[source] = module;
            return module;
        }

        public PyExprNode CompileExpression(string source, string filename)
        {
            PyExprNode cached;
            if (exprCache.TryGetValue(source, out cached)) return cached;

            var expr = PyParser.ParseExpression(source, filename);
            exprCache[source] = expr;
            return expr;
        }

        // ---------------------------------------------------------------- run

        /// <summary>Executes a block of statements in the named store namespace.</summary>
        public void Exec(string source, string storeName = "store", string filename = "<python>")
        {
            var module = CompileModule(source, filename);
            var globals = GetStore(storeName);
            var frame = new PyFrame(globals, globals);
            ExecBlock(module.Body, frame);
        }

        /// <summary>Evaluates an expression in the named store namespace.</summary>
        public object Eval(string source, string storeName = "store", string filename = "<python>")
        {
            var expr = CompileExpression(source, filename);
            var globals = GetStore(storeName);
            return Eval(expr, new PyFrame(globals, globals));
        }

        public object EvalIn(PyExprNode expr, PyFrame frame) { return Eval(expr, frame); }

        public PyFrame ModuleFrame(string storeName = "store")
        {
            var globals = GetStore(storeName);
            return new PyFrame(globals, globals);
        }

        // ---------------------------------------------------------------- statements

        public Flow ExecBlock(List<PyStmt> body, PyFrame frame)
        {
            for (int i = 0; i < body.Count; i++)
            {
                Flow flow = ExecStmt(body[i], frame);
                if (flow != Flow.Normal) return flow;
            }
            return Flow.Normal;
        }

        public Flow ExecStmt(PyStmt stmt, PyFrame frame)
        {
            var expr = stmt as ExprStmt;
            if (expr != null) { Eval(expr.Value, frame); return Flow.Normal; }

            var assign = stmt as AssignStmt;
            if (assign != null)
            {
                object value = Eval(assign.Value, frame);
                for (int i = 0; i < assign.Targets.Count; i++) Bind(assign.Targets[i], value, frame);
                return Flow.Normal;
            }

            var aug = stmt as AugAssignStmt;
            if (aug != null)
            {
                object current = Eval(aug.Target, frame);
                object value = Eval(aug.Value, frame);
                Bind(aug.Target, ApplyBinary(aug.Op, current, value), frame);
                return Flow.Normal;
            }

            var ifStmt = stmt as IfStmt;
            if (ifStmt != null)
            {
                for (int i = 0; i < ifStmt.Branches.Count; i++)
                {
                    var branch = ifStmt.Branches[i];
                    if (branch.Condition == null || Py.Truthy(Eval(branch.Condition, frame)))
                        return ExecBlock(branch.Body, frame);
                }
                return Flow.Normal;
            }

            var whileStmt = stmt as WhileStmt;
            if (whileStmt != null) return ExecWhile(whileStmt, frame);

            var forStmt = stmt as ForStmt;
            if (forStmt != null) return ExecFor(forStmt, frame);

            if (stmt is PassStmt) return Flow.Normal;
            if (stmt is BreakStmt) return Flow.Break;
            if (stmt is ContinueStmt) return Flow.Continue;

            var ret = stmt as ReturnStmt;
            if (ret != null)
            {
                frame.ReturnValue = ret.Value == null ? null : Eval(ret.Value, frame);
                return Flow.Return;
            }

            var func = stmt as FuncDefStmt;
            if (func != null)
            {
                object fn = MakeFunction(func, frame);
                for (int i = func.Decorators.Count - 1; i >= 0; i--)
                    fn = PyOps.CallObject(Eval(func.Decorators[i], frame), new[] { fn }, null);
                SetName(frame, func.Name, fn);
                return Flow.Normal;
            }

            var cls = stmt as ClassDefStmt;
            if (cls != null) { ExecClassDef(cls, frame); return Flow.Normal; }

            var global = stmt as GlobalStmt;
            if (global != null)
            {
                if (!global.NonLocal)
                {
                    if (frame.GlobalNames == null) frame.GlobalNames = new HashSet<string>();
                    foreach (var n in global.Names) frame.GlobalNames.Add(n);
                }
                return Flow.Normal;
            }

            var del = stmt as DelStmt;
            if (del != null)
            {
                foreach (var target in del.Targets) Delete(target, frame);
                return Flow.Normal;
            }

            var raise = stmt as RaiseStmt;
            if (raise != null) throw MakeError(raise, frame);

            var assert = stmt as AssertStmt;
            if (assert != null)
            {
                if (!Py.Truthy(Eval(assert.Condition, frame)))
                {
                    string message = assert.Message == null ? "" : PyOps.ToStr(Eval(assert.Message, frame));
                    throw new PyError("AssertionError", message);
                }
                return Flow.Normal;
            }

            var tryStmt = stmt as TryStmt;
            if (tryStmt != null) return ExecTry(tryStmt, frame);

            var with = stmt as WithStmt;
            if (with != null) return ExecWith(with, frame);

            var import = stmt as ImportStmt;
            if (import != null)
            {
                foreach (var entry in import.Entries)
                {
                    object module = ResolveModule(entry.Module);
                    // `import a.b` binds the root name unless aliased.
                    string bind = entry.Alias ?? entry.Module.Split('.')[0];
                    SetName(frame, bind, entry.Alias != null ? module : ResolveModule(bind));
                }
                return Flow.Normal;
            }

            var importFrom = stmt as ImportFromStmt;
            if (importFrom != null)
            {
                object module = ResolveModule(importFrom.Module);
                if (importFrom.Wildcard)
                {
                    var dict = ModuleDict(module);
                    if (dict != null)
                        foreach (var kv in dict)
                        {
                            string key = PyOps.ToStr(kv.Key);
                            if (!key.StartsWith("_", StringComparison.Ordinal)) SetName(frame, key, kv.Value);
                        }
                    return Flow.Normal;
                }

                foreach (var entry in importFrom.Names)
                {
                    object value;
                    if (!PyOps.TryGetAttr(module, entry.Module, out value))
                        value = ResolveModule(importFrom.Module + "." + entry.Module);
                    SetName(frame, entry.Alias ?? entry.Module, value);
                }
                return Flow.Normal;
            }

            var print = stmt as PrintStmt;
            if (print != null)
            {
                var sb = new StringBuilder();
                for (int i = 0; i < print.Values.Count; i++)
                {
                    if (i > 0) sb.Append(' ');
                    sb.Append(PyOps.ToStr(Eval(print.Values[i], frame)));
                }
                if (Warn != null) Warn(sb.ToString());
                return Flow.Normal;
            }

            throw new PyError("SystemError", "unsupported statement " + stmt.GetType().Name);
        }

        Flow ExecWhile(WhileStmt stmt, PyFrame frame)
        {
            while (Py.Truthy(Eval(stmt.Condition, frame)))
            {
                Flow flow = ExecBlock(stmt.Body, frame);
                if (flow == Flow.Break) return Flow.Normal;
                if (flow == Flow.Return) return flow;
            }
            if (stmt.OrElse != null) return ExecBlock(stmt.OrElse, frame);
            return Flow.Normal;
        }

        Flow ExecFor(ForStmt stmt, PyFrame frame)
        {
            object iterable = Eval(stmt.Iterable, frame);

            foreach (var item in PyOps.Iterate(iterable))
            {
                Bind(stmt.Target, item, frame);
                Flow flow = ExecBlock(stmt.Body, frame);
                if (flow == Flow.Break) return Flow.Normal;
                if (flow == Flow.Return) return flow;
            }

            if (stmt.OrElse != null) return ExecBlock(stmt.OrElse, frame);
            return Flow.Normal;
        }

        Flow ExecTry(TryStmt stmt, PyFrame frame)
        {
            Flow flow = Flow.Normal;
            try
            {
                try
                {
                    flow = ExecBlock(stmt.Body, frame);
                    if (flow == Flow.Normal && stmt.OrElse != null) flow = ExecBlock(stmt.OrElse, frame);
                }
                catch (PyError error)
                {
                    bool handled = false;
                    foreach (var handler in stmt.Handlers)
                    {
                        if (!HandlerMatches(handler, error, frame)) continue;
                        if (handler.Name != null) SetName(frame, handler.Name, ErrorValue(error));
                        flow = ExecBlock(handler.Body, frame);
                        handled = true;
                        break;
                    }
                    if (!handled) throw;
                }
            }
            finally
            {
                if (stmt.Finally != null)
                {
                    Flow finallyFlow = ExecBlock(stmt.Finally, frame);
                    // A `return` inside finally wins over whatever was pending.
                    if (finallyFlow != Flow.Normal) flow = finallyFlow;
                }
            }
            return flow;
        }

        bool HandlerMatches(TryStmt.Handler handler, PyError error, PyFrame frame)
        {
            if (handler.ExceptionType == null) return true;

            object type;
            try { type = Eval(handler.ExceptionType, frame); }
            catch (PyError) { return false; }

            return ExceptionTypeMatches(type, error);
        }

        static bool ExceptionTypeMatches(object type, PyError error)
        {
            var tuple = type as PyTuple;
            if (tuple != null)
            {
                foreach (var t in tuple.Items) if (ExceptionTypeMatches(t, error)) return true;
                return false;
            }

            var exc = type as PyExceptionType;
            if (exc != null) return exc.Matches(error.PyType);

            var pyType = type as PyType;
            if (pyType != null) return pyType.Name == error.PyType || pyType.Name == "Exception" || pyType.Name == "BaseException";

            return false;
        }

        static object ErrorValue(PyError error)
        {
            var instance = error.Value as PyInstance;
            if (instance != null) return instance;
            return new PyExceptionInstance(error);
        }

        PyError MakeError(RaiseStmt stmt, PyFrame frame)
        {
            if (stmt.Value == null) return new PyError("RuntimeError", "no active exception to re-raise");

            object value = Eval(stmt.Value, frame);

            var existing = value as PyExceptionInstance;
            if (existing != null) return existing.Error;

            var type = value as PyExceptionType;
            if (type != null) return new PyError(type.Name, "");

            var instance = value as PyInstance;
            if (instance != null)
            {
                object message;
                string text = instance.TryGetAttr("args", out message) ? PyOps.ToStr(message) : "";
                return new PyError(instance.Type != null ? instance.Type.Name : "Exception", text, instance);
            }

            return new PyError("Exception", PyOps.ToStr(value));
        }

        Flow ExecWith(WithStmt stmt, PyFrame frame)
        {
            var entered = new List<object>();

            foreach (var item in stmt.Items)
            {
                object context = Eval(item.Context, frame);
                object enter;
                object value = context;
                if (PyOps.TryGetAttr(context, "__enter__", out enter))
                    value = PyOps.CallObject(enter, Py.EmptyArgs, null);
                if (item.Target != null) Bind(item.Target, value, frame);
                entered.Add(context);
            }

            try
            {
                return ExecBlock(stmt.Body, frame);
            }
            finally
            {
                for (int i = entered.Count - 1; i >= 0; i--)
                {
                    object exit;
                    if (PyOps.TryGetAttr(entered[i], "__exit__", out exit))
                        PyOps.CallObject(exit, new object[] { null, null, null }, null);
                }
            }
        }

        void ExecClassDef(ClassDefStmt stmt, PyFrame frame)
        {
            var type = new PyType(stmt.Name) { QualName = stmt.Name };

            foreach (var baseExpr in stmt.Bases)
            {
                object b = Eval(baseExpr, frame);
                var bt = b as PyType;
                if (bt != null) type.Bases.Add(bt);
            }

            // The class body runs in its own scope; whatever it binds becomes the class dict.
            var body = new PyDict();
            var classFrame = new PyFrame(body, frame.Globals, frame);
            ExecBlock(stmt.Body, classFrame);

            foreach (var kv in body) type.Dict.Set(kv.Key, kv.Value);

            object result = type;
            for (int i = stmt.Decorators.Count - 1; i >= 0; i--)
                result = PyOps.CallObject(Eval(stmt.Decorators[i], frame), new[] { result }, null);

            SetName(frame, stmt.Name, result);
        }

        object MakeFunction(FuncDefStmt stmt, PyFrame frame)
        {
            return new PyFunction
            {
                Name = stmt.Name,
                Params = stmt.Params,
                Body = stmt.Body,
                Closure = frame,
                Globals = frame.Globals,
                Interp = this,
            };
        }

        // ---------------------------------------------------------------- calling

        public object CallFunction(PyFunction fn, object[] args, PyDict kwargs)
        {
            if (++depth > MaxDepth)
            {
                depth--;
                throw new PyError("RecursionError", "maximum recursion depth exceeded in " + fn.Name);
            }

            try
            {
                var locals = new PyDict();
                BindParameters(fn, args ?? Py.EmptyArgs, kwargs, locals);

                var frame = new PyFrame(locals, fn.Globals, fn.Closure);

                if (fn.ExprBody != null) return Eval(fn.ExprBody, frame);

                Flow flow = ExecBlock(fn.Body, frame);
                return flow == Flow.Return ? frame.ReturnValue : null;
            }
            finally
            {
                depth--;
            }
        }

        void BindParameters(PyFunction fn, object[] args, PyDict kwargs, PyDict locals)
        {
            var ps = fn.Params;
            int positionalIndex = 0;

            // Positional parameters, in order, skipping keyword-only ones.
            var positional = new List<PyParam>();
            foreach (var p in ps.Params) if (!p.KeywordOnly) positional.Add(p);

            for (; positionalIndex < positional.Count && positionalIndex < args.Length; positionalIndex++)
                locals.Set(positional[positionalIndex].Name, args[positionalIndex]);

            if (ps.StarArgs != null)
            {
                var extra = new List<object>();
                for (int i = positionalIndex; i < args.Length; i++) extra.Add(args[i]);
                locals.Set(ps.StarArgs, new PyTuple(extra.ToArray()));
            }
            else if (args.Length > positional.Count)
            {
                throw new PyError("TypeError",
                    fn.Name + "() takes " + positional.Count + " positional arguments but " + args.Length + " were given");
            }

            var consumed = new HashSet<string>();

            if (kwargs != null)
            {
                foreach (var kv in kwargs)
                {
                    string name = PyOps.ToStr(kv.Key);
                    if (HasParam(ps, name))
                    {
                        if (locals.Contains(name))
                            throw new PyError("TypeError", fn.Name + "() got multiple values for argument '" + name + "'");
                        locals.Set(name, kv.Value);
                        consumed.Add(name);
                    }
                    else if (ps.StarStarKwargs == null)
                    {
                        throw new PyError("TypeError", fn.Name + "() got an unexpected keyword argument '" + name + "'");
                    }
                }
            }

            if (ps.StarStarKwargs != null)
            {
                var rest = new PyDict();
                if (kwargs != null)
                    foreach (var kv in kwargs)
                    {
                        string name = PyOps.ToStr(kv.Key);
                        if (!HasParam(ps, name)) rest.Set(name, kv.Value);
                    }
                locals.Set(ps.StarStarKwargs, rest);
            }

            // Defaults, evaluated in the defining scope.
            foreach (var p in ps.Params)
            {
                if (locals.Contains(p.Name)) continue;
                if (p.Default == null)
                    throw new PyError("TypeError", fn.Name + "() missing required argument: '" + p.Name + "'");
                locals.Set(p.Name, Eval(p.Default, fn.Closure ?? new PyFrame(fn.Globals, fn.Globals)));
            }
        }

        static bool HasParam(PyParams ps, string name)
        {
            foreach (var p in ps.Params) if (p.Name == name) return true;
            return false;
        }

        // ---------------------------------------------------------------- names

        public bool TryLookup(PyFrame frame, string name, out object value)
        {
            // Locals, then any enclosing function scopes.
            for (PyFrame f = frame; f != null; f = f.Parent)
            {
                if (f.Locals.TryGet(name, out value)) return true;
                // Globals declared here always come from the module namespace.
                if (f.GlobalNames != null && f.GlobalNames.Contains(name)) break;
            }

            if (frame.Globals.TryGet(name, out value)) return true;

            // Named stores such as `store.gui` still see everything defined in the
            // default store, which is where Ren'Py puts Character, Borders and friends.
            if (!ReferenceEquals(frame.Globals, DefaultStore) &&
                DefaultStore != null && DefaultStore.TryGet(name, out value)) return true;

            if (Builtins.TryGet(name, out value)) return true;

            value = null;
            return false;
        }

        public object Lookup(PyFrame frame, string name)
        {
            object value;
            if (TryLookup(frame, name, out value)) return value;
            throw new PyError("NameError", "name '" + name + "' is not defined");
        }

        public void SetName(PyFrame frame, string name, object value)
        {
            if (frame.GlobalNames != null && frame.GlobalNames.Contains(name))
            {
                frame.Globals.Set(name, value);
                return;
            }
            frame.Locals.Set(name, value);
        }

        // ---------------------------------------------------------------- binding

        /// <summary>Assigns a value to an assignment target, unpacking sequences.</summary>
        public void Bind(PyExprNode target, object value, PyFrame frame)
        {
            var name = target as NameExpr;
            if (name != null) { SetName(frame, name.Name, value); return; }

            var attr = target as AttrExpr;
            if (attr != null)
            {
                PyOps.SetAttr(Eval(attr.Target, frame), attr.Name, value);
                return;
            }

            var index = target as IndexExpr;
            if (index != null)
            {
                PyOps.SetItem(Eval(index.Target, frame), Eval(index.Index, frame), value);
                return;
            }

            var seq = target as SequenceExpr;
            if (seq != null) { BindSequence(seq.Items, value, frame); return; }

            var star = target as StarExpr;
            if (star != null) { Bind(star.Value, value, frame); return; }

            throw new PyError("SyntaxError", "cannot assign to this expression");
        }

        void BindSequence(List<PyExprNode> targets, object value, PyFrame frame)
        {
            var items = new List<object>(PyOps.Iterate(value));

            int starIndex = -1;
            for (int i = 0; i < targets.Count; i++)
                if (targets[i] is StarExpr) { starIndex = i; break; }

            if (starIndex < 0)
            {
                if (items.Count != targets.Count)
                {
                    throw new PyError("ValueError", items.Count < targets.Count
                        ? "not enough values to unpack (expected " + targets.Count + ", got " + items.Count + ")"
                        : "too many values to unpack (expected " + targets.Count + ")");
                }
                for (int i = 0; i < targets.Count; i++) Bind(targets[i], items[i], frame);
                return;
            }

            int after = targets.Count - starIndex - 1;
            if (items.Count < targets.Count - 1)
                throw new PyError("ValueError", "not enough values to unpack");

            for (int i = 0; i < starIndex; i++) Bind(targets[i], items[i], frame);

            int starCount = items.Count - starIndex - after;
            Bind(((StarExpr)targets[starIndex]).Value,
                 new PyList(items.GetRange(starIndex, starCount)), frame);

            for (int i = 0; i < after; i++)
                Bind(targets[starIndex + 1 + i], items[items.Count - after + i], frame);
        }

        void Delete(PyExprNode target, PyFrame frame)
        {
            var name = target as NameExpr;
            if (name != null)
            {
                if (!frame.Locals.Remove(name.Name) && !frame.Globals.Remove(name.Name))
                    throw new PyError("NameError", "name '" + name.Name + "' is not defined");
                return;
            }

            var index = target as IndexExpr;
            if (index != null)
            {
                PyOps.DelItem(Eval(index.Target, frame), Eval(index.Index, frame));
                return;
            }

            var attr = target as AttrExpr;
            if (attr != null)
            {
                object obj = Eval(attr.Target, frame);
                var instance = obj as PyInstance;
                if (instance != null) { instance.Dict.Remove(attr.Name); return; }
                throw new PyError("AttributeError", "cannot delete attribute");
            }

            throw new PyError("SyntaxError", "cannot delete this expression");
        }

        // ---------------------------------------------------------------- expressions

        public object Eval(PyExprNode expr, PyFrame frame)
        {
            var constant = expr as ConstExpr;
            if (constant != null) return constant.Value;

            var name = expr as NameExpr;
            if (name != null) return Lookup(frame, name.Name);

            var binary = expr as BinaryExpr;
            if (binary != null)
                return ApplyBinary(binary.Op, Eval(binary.Left, frame), Eval(binary.Right, frame));

            var call = expr as CallExpr;
            if (call != null) return EvalCall(call, frame);

            var attr = expr as AttrExpr;
            if (attr != null) return PyOps.GetAttr(Eval(attr.Target, frame), attr.Name);

            var index = expr as IndexExpr;
            if (index != null) return PyOps.GetItem(Eval(index.Target, frame), Eval(index.Index, frame));

            var compare = expr as CompareExpr;
            if (compare != null) return EvalCompare(compare, frame);

            var boolExpr = expr as BoolExpr;
            if (boolExpr != null)
            {
                object last = null;
                foreach (var operand in boolExpr.Operands)
                {
                    last = Eval(operand, frame);
                    bool truthy = Py.Truthy(last);
                    // `and` stops on the first falsy value, `or` on the first truthy one.
                    if (boolExpr.IsAnd ? !truthy : truthy) return last;
                }
                return last;
            }

            var unary = expr as UnaryExpr;
            if (unary != null)
            {
                object operand = Eval(unary.Operand, frame);
                switch (unary.Op)
                {
                    case "-": return PyOps.Neg(operand);
                    case "+": return operand;
                    case "~": return PyOps.Invert(operand);
                    case "not": return !Py.Truthy(operand);
                }
                throw new PyError("SystemError", "unknown unary operator " + unary.Op);
            }

            var seq = expr as SequenceExpr;
            if (seq != null) return EvalSequence(seq, frame);

            var dict = expr as DictExpr;
            if (dict != null)
            {
                var rv = new PyDict();
                foreach (var entry in dict.Entries)
                {
                    if (entry.Key == null)
                    {
                        var spread = Eval(entry.Value, frame) as PyDict;
                        if (spread != null) foreach (var kv in spread) rv.Set(kv.Key, kv.Value);
                        continue;
                    }
                    rv.Set(Eval(entry.Key, frame), Eval(entry.Value, frame));
                }
                return rv;
            }

            var ternary = expr as IfExpr;
            if (ternary != null)
                return Py.Truthy(Eval(ternary.Condition, frame))
                    ? Eval(ternary.Then, frame)
                    : Eval(ternary.Else, frame);

            var slice = expr as SliceExpr;
            if (slice != null)
                return new PySlice(
                    slice.Start == null ? null : Eval(slice.Start, frame),
                    slice.Stop == null ? null : Eval(slice.Stop, frame),
                    slice.Step == null ? null : Eval(slice.Step, frame));

            var lambda = expr as LambdaExpr;
            if (lambda != null)
                return new PyFunction
                {
                    Name = "<lambda>",
                    Params = lambda.Params,
                    ExprBody = lambda.Body,
                    Closure = frame,
                    Globals = frame.Globals,
                    Interp = this,
                };

            var comp = expr as ComprehensionExpr;
            if (comp != null) return EvalComprehension(comp, frame);

            var fstring = expr as FStringExpr;
            if (fstring != null) return EvalFString(fstring, frame);

            var star = expr as StarExpr;
            if (star != null) return Eval(star.Value, frame);

            throw new PyError("SystemError", "unsupported expression " + expr.GetType().Name);
        }

        object EvalSequence(SequenceExpr seq, PyFrame frame)
        {
            var items = new List<object>(seq.Items.Count);

            foreach (var item in seq.Items)
            {
                var star = item as StarExpr;
                if (star != null) items.AddRange(PyOps.Iterate(Eval(star.Value, frame)));
                else items.Add(Eval(item, frame));
            }

            switch (seq.Type)
            {
                case SequenceExpr.Kind.List: return new PyList(items);
                case SequenceExpr.Kind.Set: return new PySet(items);
                default: return new PyTuple(items.ToArray());
            }
        }

        object EvalCompare(CompareExpr expr, PyFrame frame)
        {
            object left = Eval(expr.Left, frame);

            for (int i = 0; i < expr.Ops.Count; i++)
            {
                object right = Eval(expr.Comparators[i], frame);
                if (!CompareOnce(expr.Ops[i], left, right)) return false;
                left = right;
            }

            return true;
        }

        static bool CompareOnce(string op, object a, object b)
        {
            switch (op)
            {
                case "==": return PyOps.Eq(a, b);
                case "!=": return !PyOps.Eq(a, b);
                case "<": return PyOps.Compare(a, b) < 0;
                case "<=": return PyOps.Compare(a, b) <= 0;
                case ">": return PyOps.Compare(a, b) > 0;
                case ">=": return PyOps.Compare(a, b) >= 0;
                case "in": return PyOps.Contains(b, a);
                case "not in": return !PyOps.Contains(b, a);
                case "is": return IsIdentical(a, b);
                case "is not": return !IsIdentical(a, b);
            }
            throw new PyError("SystemError", "unknown comparison " + op);
        }

        static bool IsIdentical(object a, object b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a == null || b == null) return false;
            // Small immutables are interned in CPython; compare by value so
            // `x is True` and `n is 0` behave as scripts expect.
            if (a is bool && b is bool) return (bool)a == (bool)b;
            if (Py.IsInt(a) && Py.IsInt(b)) return Py.ToInt(a) == Py.ToInt(b);
            if (a is string && b is string) return (string)a == (string)b;
            return false;
        }

        object EvalCall(CallExpr call, PyFrame frame)
        {
            object fn = Eval(call.Func, frame);

            var args = new List<object>(call.Args.Count);
            PyDict kwargs = null;

            foreach (var arg in call.Args)
            {
                if (arg.Star)
                {
                    args.AddRange(PyOps.Iterate(Eval(arg.Value, frame)));
                    continue;
                }

                if (arg.DoubleStar)
                {
                    var spread = Eval(arg.Value, frame) as PyDict;
                    if (spread != null)
                    {
                        if (kwargs == null) kwargs = new PyDict();
                        foreach (var kv in spread) kwargs.Set(PyOps.ToStr(kv.Key), kv.Value);
                    }
                    continue;
                }

                if (arg.Name != null)
                {
                    if (kwargs == null) kwargs = new PyDict();
                    kwargs.Set(arg.Name, Eval(arg.Value, frame));
                    continue;
                }

                args.Add(Eval(arg.Value, frame));
            }

            return PyOps.CallObject(fn, args.ToArray(), kwargs);
        }

        object EvalComprehension(ComprehensionExpr comp, PyFrame frame)
        {
            var results = new List<object>();
            var dictResult = comp.Type == ComprehensionExpr.Kind.Dict ? new PyDict() : null;

            // Comprehensions get their own scope so the loop variable does not leak.
            var scope = new PyFrame(new PyDict(), frame.Globals, frame);

            RunComprehension(comp, 0, scope, results, dictResult);

            switch (comp.Type)
            {
                case ComprehensionExpr.Kind.List: return new PyList(results);
                case ComprehensionExpr.Kind.Set: return new PySet(results);
                case ComprehensionExpr.Kind.Dict: return dictResult;
                default: return new PyList(results); // generators are materialised
            }
        }

        void RunComprehension(ComprehensionExpr comp, int clauseIndex, PyFrame scope,
                              List<object> results, PyDict dictResult)
        {
            if (clauseIndex >= comp.Clauses.Count)
            {
                if (dictResult != null)
                    dictResult.Set(Eval(comp.Element, scope), Eval(comp.ValueElement, scope));
                else
                    results.Add(Eval(comp.Element, scope));
                return;
            }

            var clause = comp.Clauses[clauseIndex];

            foreach (var item in PyOps.Iterate(Eval(clause.Iterable, scope)))
            {
                Bind(clause.Target, item, scope);

                bool passed = true;
                foreach (var condition in clause.Conditions)
                {
                    if (!Py.Truthy(Eval(condition, scope))) { passed = false; break; }
                }
                if (!passed) continue;

                RunComprehension(comp, clauseIndex + 1, scope, results, dictResult);
            }
        }

        string EvalFString(FStringExpr expr, PyFrame frame)
        {
            var sb = new StringBuilder();

            foreach (var part in expr.Parts)
            {
                if (part.Expr == null) { sb.Append(part.Literal); continue; }

                // The `{x=}` form prints the source text before the value.
                if (part.Literal != null) sb.Append(part.Literal);

                object value = Eval(part.Expr, frame);

                if (part.Conversion == "r") sb.Append(PyOps.Repr(value));
                else if (part.Conversion == "a") sb.Append(PyOps.Repr(value));
                else if (part.FormatSpec != null) sb.Append(PyFormat.FormatSpec(value, part.FormatSpec));
                else sb.Append(PyOps.ToStr(value));
            }

            return sb.ToString();
        }

        public static object ApplyBinary(string op, object a, object b)
        {
            switch (op)
            {
                case "+": return PyOps.Add(a, b);
                case "-": return PyOps.Sub(a, b);
                case "*": return PyOps.Mul(a, b);
                case "/": return PyOps.Div(a, b);
                case "//": return PyOps.FloorDiv(a, b);
                case "%": return PyOps.Mod(a, b);
                case "**": return PyOps.Pow(a, b);
                case "&": return PyOps.BitAnd(a, b);
                case "|": return PyOps.BitOr(a, b);
                case "^": return PyOps.BitXor(a, b);
                case "<<": return PyOps.LShift(a, b);
                case ">>": return PyOps.RShift(a, b);
                case "@": throw new PyError("TypeError", "matrix multiplication is not supported");
            }
            throw new PyError("SystemError", "unknown operator " + op);
        }

        // ---------------------------------------------------------------- modules

        public object ResolveModule(string name)
        {
            object module;
            if (Modules.TryGetValue(name, out module)) return module;

            // Ren'Py stores are addressable as modules ("store.gui" and friends).
            PyDict store;
            if (Stores.TryGetValue(name, out store)) return new PyNamespace(name, store);

            if (Warn != null) Warn("import of unknown module '" + name + "'");

            var placeholder = new PyNamespace(name, new PyDict());
            Modules[name] = placeholder;
            return placeholder;
        }

        static PyDict ModuleDict(object module)
        {
            var ns = module as PyNamespace;
            if (ns != null) return ns.Dict;
            return module as PyDict;
        }
    }

    /// <summary>A dict-backed namespace exposed to Python as an object with attributes.</summary>
    public sealed class PyNamespace : IPyAttrs
    {
        public readonly string Name;
        public readonly PyDict Dict;

        public PyNamespace(string name, PyDict dict) { Name = name; Dict = dict; }

        public bool TryGetAttr(string name, out object value) { return Dict.TryGet(name, out value); }
        public void SetAttr(string name, object value) { Dict.Set(name, value); }

        public override string ToString() { return "<module '" + Name + "'>"; }
    }

    /// <summary>A built-in exception class, callable to construct an instance.</summary>
    public sealed class PyExceptionType : IPyCallable
    {
        public readonly string Name;
        public readonly PyExceptionType Base;

        public PyExceptionType(string name, PyExceptionType baseType = null) { Name = name; Base = baseType; }

        public bool Matches(string typeName)
        {
            for (var t = this; t != null; t = t.Base)
                if (t.Name == typeName) return true;
            // Exception and BaseException catch everything the runtime raises.
            return Name == "Exception" || Name == "BaseException";
        }

        public object Call(object[] args, PyDict kwargs)
        {
            string message = args.Length > 0 ? PyOps.ToStr(args[0]) : "";
            return new PyExceptionInstance(new PyError(Name, message));
        }

        public override string ToString() { return "<class '" + Name + "'>"; }
    }

    /// <summary>An exception value bound by `except ... as e`.</summary>
    public sealed class PyExceptionInstance : IPyAttrs
    {
        public readonly PyError Error;

        public PyExceptionInstance(PyError error) { Error = error; }

        public bool TryGetAttr(string name, out object value)
        {
            switch (name)
            {
                case "args": value = new PyTuple(new object[] { Error.Value }); return true;
                case "message": value = Error.Value; return true;
                default: value = null; return false;
            }
        }

        public void SetAttr(string name, object value) { }

        public override string ToString() { return PyOps.ToStr(Error.Value); }
    }
}
