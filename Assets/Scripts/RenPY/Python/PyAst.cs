using System;
using System.Collections.Generic;

namespace RenPy.Python
{
    // ================================================================ expressions

    public abstract class PyExprNode
    {
        public int Line;
    }

    public sealed class NameExpr : PyExprNode
    {
        public string Name;
        public NameExpr(string name) { Name = name; }
    }

    public sealed class ConstExpr : PyExprNode
    {
        public object Value;
        public ConstExpr(object value) { Value = value; }
    }

    /// <summary>A tuple, list or set display.</summary>
    public sealed class SequenceExpr : PyExprNode
    {
        public enum Kind { Tuple, List, Set }
        public Kind Type;
        public List<PyExprNode> Items = new List<PyExprNode>();
    }

    public sealed class DictExpr : PyExprNode
    {
        /// <summary>A null key marks `**mapping` unpacking, with the mapping in Value.</summary>
        public List<KeyValuePair<PyExprNode, PyExprNode>> Entries =
            new List<KeyValuePair<PyExprNode, PyExprNode>>();
    }

    public sealed class AttrExpr : PyExprNode
    {
        public PyExprNode Target;
        public string Name;
    }

    public sealed class IndexExpr : PyExprNode
    {
        public PyExprNode Target;
        public PyExprNode Index;
    }

    public sealed class SliceExpr : PyExprNode
    {
        public PyExprNode Start, Stop, Step;
    }

    public sealed class CallArg
    {
        /// <summary>Null for positional arguments.</summary>
        public string Name;
        public PyExprNode Value;
        /// <summary>`*args` unpacking.</summary>
        public bool Star;
        /// <summary>`**kwargs` unpacking.</summary>
        public bool DoubleStar;
    }

    public sealed class CallExpr : PyExprNode
    {
        public PyExprNode Func;
        public List<CallArg> Args = new List<CallArg>();
    }

    public sealed class BinaryExpr : PyExprNode
    {
        public string Op;
        public PyExprNode Left, Right;
    }

    public sealed class UnaryExpr : PyExprNode
    {
        public string Op;
        public PyExprNode Operand;
    }

    /// <summary>Short-circuiting `and` / `or`.</summary>
    public sealed class BoolExpr : PyExprNode
    {
        public bool IsAnd;
        public List<PyExprNode> Operands = new List<PyExprNode>();
    }

    /// <summary>A comparison chain such as `0 &lt;= x &lt; 10`.</summary>
    public sealed class CompareExpr : PyExprNode
    {
        public PyExprNode Left;
        public List<string> Ops = new List<string>();
        public List<PyExprNode> Comparators = new List<PyExprNode>();
    }

    public sealed class IfExpr : PyExprNode
    {
        public PyExprNode Condition, Then, Else;
    }

    public sealed class LambdaExpr : PyExprNode
    {
        public PyParams Params = new PyParams();
        public PyExprNode Body;
    }

    public sealed class ComprehensionClause
    {
        public PyExprNode Target;
        public PyExprNode Iterable;
        public List<PyExprNode> Conditions = new List<PyExprNode>();
    }

    public sealed class ComprehensionExpr : PyExprNode
    {
        public enum Kind { List, Set, Dict, Generator }
        public Kind Type;
        public PyExprNode Element;
        /// <summary>Only set for dict comprehensions.</summary>
        public PyExprNode ValueElement;
        public List<ComprehensionClause> Clauses = new List<ComprehensionClause>();
    }

    /// <summary>An f-string: literal chunks interleaved with expressions.</summary>
    public sealed class FStringExpr : PyExprNode
    {
        public sealed class Part
        {
            public string Literal;
            public PyExprNode Expr;
            public string FormatSpec;
            public string Conversion;
        }
        public List<Part> Parts = new List<Part>();
    }

    /// <summary>`*x` in an assignment target or sequence display.</summary>
    public sealed class StarExpr : PyExprNode
    {
        public PyExprNode Value;
    }

    // ================================================================ parameters

    public sealed class PyParam
    {
        public string Name;
        public PyExprNode Default;
        public bool KeywordOnly;
    }

    public sealed class PyParams
    {
        public List<PyParam> Params = new List<PyParam>();
        public string StarArgs;
        public string StarStarKwargs;
    }

    // ================================================================ statements

    public abstract class PyStmt
    {
        public int Line;
    }

    public sealed class ExprStmt : PyStmt
    {
        public PyExprNode Value;
    }

    public sealed class AssignStmt : PyStmt
    {
        /// <summary>Chained assignment (`a = b = value`) yields several targets.</summary>
        public List<PyExprNode> Targets = new List<PyExprNode>();
        public PyExprNode Value;
    }

    public sealed class AugAssignStmt : PyStmt
    {
        public PyExprNode Target;
        /// <summary>The bare operator, e.g. "+" for `+=`.</summary>
        public string Op;
        public PyExprNode Value;
    }

    public sealed class IfStmt : PyStmt
    {
        public sealed class Branch
        {
            /// <summary>Null on the `else` branch.</summary>
            public PyExprNode Condition;
            public List<PyStmt> Body = new List<PyStmt>();
        }
        public List<Branch> Branches = new List<Branch>();
    }

    public sealed class WhileStmt : PyStmt
    {
        public PyExprNode Condition;
        public List<PyStmt> Body = new List<PyStmt>();
        public List<PyStmt> OrElse;
    }

    public sealed class ForStmt : PyStmt
    {
        public PyExprNode Target;
        public PyExprNode Iterable;
        public List<PyStmt> Body = new List<PyStmt>();
        public List<PyStmt> OrElse;
    }

    public sealed class FuncDefStmt : PyStmt
    {
        public string Name;
        public PyParams Params = new PyParams();
        public List<PyStmt> Body = new List<PyStmt>();
        public List<PyExprNode> Decorators = new List<PyExprNode>();
    }

    public sealed class ClassDefStmt : PyStmt
    {
        public string Name;
        public List<PyExprNode> Bases = new List<PyExprNode>();
        public List<PyStmt> Body = new List<PyStmt>();
        public List<PyExprNode> Decorators = new List<PyExprNode>();
    }

    public sealed class ReturnStmt : PyStmt
    {
        public PyExprNode Value;
    }

    public sealed class BreakStmt : PyStmt { }
    public sealed class ContinueStmt : PyStmt { }
    public sealed class PassStmt : PyStmt { }

    public sealed class DelStmt : PyStmt
    {
        public List<PyExprNode> Targets = new List<PyExprNode>();
    }

    public sealed class GlobalStmt : PyStmt
    {
        public List<string> Names = new List<string>();
        public bool NonLocal;
    }

    public sealed class RaiseStmt : PyStmt
    {
        public PyExprNode Value;
        public PyExprNode Cause;
    }

    public sealed class AssertStmt : PyStmt
    {
        public PyExprNode Condition;
        public PyExprNode Message;
    }

    public sealed class TryStmt : PyStmt
    {
        public sealed class Handler
        {
            /// <summary>Null catches everything.</summary>
            public PyExprNode ExceptionType;
            public string Name;
            public List<PyStmt> Body = new List<PyStmt>();
        }
        public List<PyStmt> Body = new List<PyStmt>();
        public List<Handler> Handlers = new List<Handler>();
        public List<PyStmt> OrElse;
        public List<PyStmt> Finally;
    }

    public sealed class WithStmt : PyStmt
    {
        public sealed class Item
        {
            public PyExprNode Context;
            public PyExprNode Target;
        }
        public List<Item> Items = new List<Item>();
        public List<PyStmt> Body = new List<PyStmt>();
    }

    public sealed class ImportStmt : PyStmt
    {
        public sealed class Entry
        {
            public string Module;
            public string Alias;
        }
        public List<Entry> Entries = new List<Entry>();
    }

    public sealed class ImportFromStmt : PyStmt
    {
        public string Module;
        public List<ImportStmt.Entry> Names = new List<ImportStmt.Entry>();
        public bool Wildcard;
    }

    /// <summary>Python 2 `print` statement, still present in older Ren'Py scripts.</summary>
    public sealed class PrintStmt : PyStmt
    {
        public List<PyExprNode> Values = new List<PyExprNode>();
        public bool TrailingComma;
    }

    /// <summary>A parsed module: the unit the interpreter executes.</summary>
    public sealed class PyModule
    {
        public List<PyStmt> Body = new List<PyStmt>();
        public string Filename = "<python>";
    }
}
