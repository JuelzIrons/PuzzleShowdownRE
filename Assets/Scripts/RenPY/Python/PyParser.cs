using System;
using System.Collections.Generic;
using System.Text;

namespace RenPy.Python
{
    /// <summary>
    /// A recursive-descent parser for the Python subset used inside Ren'Py scripts.
    /// Produces the tree in <see cref="PyAst"/>, which the interpreter walks directly.
    /// </summary>
    public class PyParser
    {
        readonly List<Token> tokens;
        readonly string filename;
        int index;

        public PyParser(List<Token> tokens, string filename = "<python>")
        {
            this.tokens = tokens;
            this.filename = filename;
        }

        // ---------------------------------------------------------------- entry points

        public static PyModule ParseModule(string source, string filename = "<python>")
        {
            var parser = new PyParser(PyLexer.Tokenize(source, filename), filename);
            return parser.Module();
        }

        /// <summary>Parses a single expression, for `eval`-mode Ren'Py code.</summary>
        public static PyExprNode ParseExpression(string source, string filename = "<python>")
        {
            var parser = new PyParser(PyLexer.Tokenize(source, filename), filename);
            parser.SkipNewlines();
            var expr = parser.ExprList();
            parser.SkipNewlines();
            if (parser.Current.Type != TokType.EndOfFile)
                throw parser.Error("unexpected token '" + parser.Current.Text + "' after expression");
            return expr;
        }

        public PyModule Module()
        {
            var module = new PyModule { Filename = filename };
            SkipNewlines();
            while (Current.Type != TokType.EndOfFile)
            {
                module.Body.AddRange(Statement());
                SkipNewlines();
            }
            return module;
        }

        // ---------------------------------------------------------------- token helpers

        Token Current { get { return tokens[index]; } }
        Token Ahead(int n = 1) { return tokens[Math.Min(index + n, tokens.Count - 1)]; }

        bool IsOp(string op) { return Current.Type == TokType.Op && Current.Text == op; }
        bool IsKeyword(string kw) { return Current.Type == TokType.Keyword && Current.Text == kw; }

        Token Advance() { return tokens[index++]; }

        bool TryEatOp(string op)
        {
            if (!IsOp(op)) return false;
            index++;
            return true;
        }

        bool TryEatKeyword(string kw)
        {
            if (!IsKeyword(kw)) return false;
            index++;
            return true;
        }

        void ExpectOp(string op)
        {
            if (!TryEatOp(op)) throw Error("expected '" + op + "' but found '" + Describe(Current) + "'");
        }

        void ExpectKeyword(string kw)
        {
            if (!TryEatKeyword(kw)) throw Error("expected '" + kw + "' but found '" + Describe(Current) + "'");
        }

        string ExpectName()
        {
            if (Current.Type != TokType.Name) throw Error("expected a name but found '" + Describe(Current) + "'");
            return Advance().Text;
        }

        void ExpectNewline()
        {
            if (Current.Type == TokType.EndOfFile) return;
            if (Current.Type == TokType.Newline) { index++; return; }
            if (Current.Type == TokType.Dedent) return;
            throw Error("expected end of statement but found '" + Describe(Current) + "'");
        }

        void SkipNewlines()
        {
            while (Current.Type == TokType.Newline) index++;
        }

        static string Describe(Token t)
        {
            if (t.Type == TokType.Newline) return "newline";
            if (t.Type == TokType.Indent) return "indent";
            if (t.Type == TokType.Dedent) return "dedent";
            if (t.Type == TokType.EndOfFile) return "end of input";
            return t.Text;
        }

        PyError Error(string message)
        {
            return new PyError("SyntaxError", message + " (" + filename + ", line " + Current.Line + ")");
        }

        // ---------------------------------------------------------------- statements

        List<PyStmt> Statement()
        {
            if (Current.Type == TokType.Keyword)
            {
                switch (Current.Text)
                {
                    case "if": return One(IfStatement());
                    case "while": return One(WhileStatement());
                    case "for": return One(ForStatement());
                    case "def": return One(FuncDef(null));
                    case "class": return One(ClassDef(null));
                    case "try": return One(TryStatement());
                    case "with": return One(WithStatement());
                    case "async": index++; return Statement();
                }
            }

            if (IsOp("@")) return One(Decorated());

            return SimpleStatementLine();
        }

        static List<PyStmt> One(PyStmt s) { return new List<PyStmt> { s }; }

        /// <summary>A line of one or more `;`-separated simple statements.</summary>
        List<PyStmt> SimpleStatementLine()
        {
            var rv = new List<PyStmt> { SimpleStatement() };
            while (TryEatOp(";"))
            {
                if (Current.Type == TokType.Newline || Current.Type == TokType.EndOfFile) break;
                rv.Add(SimpleStatement());
            }
            ExpectNewline();
            return rv;
        }

        PyStmt SimpleStatement()
        {
            int line = Current.Line;

            if (Current.Type == TokType.Keyword)
            {
                switch (Current.Text)
                {
                    case "pass": index++; return new PassStmt { Line = line };
                    case "break": index++; return new BreakStmt { Line = line };
                    case "continue": index++; return new ContinueStmt { Line = line };

                    case "return":
                    {
                        index++;
                        var stmt = new ReturnStmt { Line = line };
                        if (!AtStatementEnd()) stmt.Value = ExprList();
                        return stmt;
                    }

                    case "raise":
                    {
                        index++;
                        var stmt = new RaiseStmt { Line = line };
                        if (!AtStatementEnd())
                        {
                            stmt.Value = Ternary();
                            if (TryEatKeyword("from")) stmt.Cause = Ternary();
                            // Python 2 form: raise Type, value
                            else if (TryEatOp(",")) stmt.Cause = Ternary();
                        }
                        return stmt;
                    }

                    case "global":
                    case "nonlocal":
                    {
                        bool nonlocal = Current.Text == "nonlocal";
                        index++;
                        var stmt = new GlobalStmt { Line = line, NonLocal = nonlocal };
                        stmt.Names.Add(ExpectName());
                        while (TryEatOp(",")) stmt.Names.Add(ExpectName());
                        return stmt;
                    }

                    case "del":
                    {
                        index++;
                        var stmt = new DelStmt { Line = line };
                        stmt.Targets.Add(Ternary());
                        while (TryEatOp(",")) stmt.Targets.Add(Ternary());
                        return stmt;
                    }

                    case "assert":
                    {
                        index++;
                        var stmt = new AssertStmt { Line = line, Condition = Ternary() };
                        if (TryEatOp(",")) stmt.Message = Ternary();
                        return stmt;
                    }

                    case "import": return ImportStatement();
                    case "from": return FromImportStatement();

                    case "print":
                        // Python 2 statement form; the call form is handled as an expression.
                        if (!IsCallLike(Ahead())) return PrintStatement();
                        break;

                    case "exec":
                        index++;
                        return new ExprStmt { Line = line, Value = Ternary() };
                }
            }

            return ExpressionStatement();
        }

        static bool IsCallLike(Token t)
        {
            return t.Type == TokType.Op && (t.Text == "(" || t.Text == "=" || t.Text == ".");
        }

        bool AtStatementEnd()
        {
            return Current.Type == TokType.Newline || Current.Type == TokType.EndOfFile ||
                   Current.Type == TokType.Dedent || IsOp(";");
        }

        PyStmt PrintStatement()
        {
            int line = Current.Line;
            index++;
            var stmt = new PrintStmt { Line = line };

            // print >>stream, ... — the stream target is not modelled.
            if (TryEatOp(">>")) { Ternary(); TryEatOp(","); }

            while (!AtStatementEnd())
            {
                stmt.Values.Add(Ternary());
                if (!TryEatOp(",")) break;
                if (AtStatementEnd()) { stmt.TrailingComma = true; break; }
            }
            return stmt;
        }

        PyStmt ImportStatement()
        {
            int line = Current.Line;
            ExpectKeyword("import");
            var stmt = new ImportStmt { Line = line };
            do
            {
                var entry = new ImportStmt.Entry { Module = DottedName() };
                if (TryEatKeyword("as")) entry.Alias = ExpectName();
                stmt.Entries.Add(entry);
            } while (TryEatOp(","));
            return stmt;
        }

        PyStmt FromImportStatement()
        {
            int line = Current.Line;
            ExpectKeyword("from");

            var sb = new StringBuilder();
            while (IsOp(".") || IsOp("...")) sb.Append(Advance().Text);
            if (Current.Type == TokType.Name) sb.Append(DottedName());

            var stmt = new ImportFromStmt { Line = line, Module = sb.ToString() };
            ExpectKeyword("import");

            if (TryEatOp("*")) { stmt.Wildcard = true; return stmt; }

            bool paren = TryEatOp("(");
            do
            {
                if (paren && IsOp(")")) break;
                var entry = new ImportStmt.Entry { Module = ExpectName() };
                if (TryEatKeyword("as")) entry.Alias = ExpectName();
                stmt.Names.Add(entry);
            } while (TryEatOp(","));
            if (paren) ExpectOp(")");

            return stmt;
        }

        string DottedName()
        {
            var sb = new StringBuilder(ExpectName());
            while (IsOp(".") && Ahead().Type == TokType.Name)
            {
                index++;
                sb.Append('.').Append(ExpectName());
            }
            return sb.ToString();
        }

        PyStmt ExpressionStatement()
        {
            int line = Current.Line;
            var first = ExprList();

            // Augmented assignment
            if (Current.Type == TokType.Op && Current.Text.Length >= 2 && Current.Text.EndsWith("=") &&
                Current.Text != "==" && Current.Text != "!=" && Current.Text != "<=" && Current.Text != ">=" &&
                Current.Text != ":=")
            {
                string op = Advance().Text;
                return new AugAssignStmt
                {
                    Line = line,
                    Target = first,
                    Op = op.Substring(0, op.Length - 1),
                    Value = ExprList(),
                };
            }

            if (IsOp("="))
            {
                var stmt = new AssignStmt { Line = line };
                stmt.Targets.Add(first);
                while (TryEatOp("="))
                {
                    var value = ExprList();
                    stmt.Targets.Add(value);
                }
                // The last parsed item is the value, everything before it a target.
                stmt.Value = stmt.Targets[stmt.Targets.Count - 1];
                stmt.Targets.RemoveAt(stmt.Targets.Count - 1);
                return stmt;
            }

            // Annotated declaration: `x: int` or `x: int = value`.
            if (IsOp(":"))
            {
                index++;
                Ternary();
                if (TryEatOp("="))
                {
                    var stmt = new AssignStmt { Line = line, Value = ExprList() };
                    stmt.Targets.Add(first);
                    return stmt;
                }
                return new PassStmt { Line = line };
            }

            return new ExprStmt { Line = line, Value = first };
        }

        PyStmt Decorated()
        {
            var decorators = new List<PyExprNode>();
            while (TryEatOp("@"))
            {
                decorators.Add(Ternary());
                ExpectNewline();
                SkipNewlines();
            }

            if (IsKeyword("class")) return ClassDef(decorators);
            return FuncDef(decorators);
        }

        PyStmt FuncDef(List<PyExprNode> decorators)
        {
            int line = Current.Line;
            ExpectKeyword("def");

            var stmt = new FuncDefStmt { Line = line, Name = ExpectName() };
            if (decorators != null) stmt.Decorators = decorators;

            ExpectOp("(");
            stmt.Params = ParameterList(")");
            ExpectOp(")");

            // Return annotation, ignored.
            if (TryEatOp("->")) Ternary();

            ExpectOp(":");
            stmt.Body = Suite();
            return stmt;
        }

        PyStmt ClassDef(List<PyExprNode> decorators)
        {
            int line = Current.Line;
            ExpectKeyword("class");

            var stmt = new ClassDefStmt { Line = line, Name = ExpectName() };
            if (decorators != null) stmt.Decorators = decorators;

            if (TryEatOp("("))
            {
                while (!IsOp(")"))
                {
                    // Metaclass and other keyword bases are not modelled.
                    if (Current.Type == TokType.Name && Ahead().Type == TokType.Op && Ahead().Text == "=")
                    {
                        index += 2;
                        Ternary();
                    }
                    else if (TryEatOp("*") || TryEatOp("**")) Ternary();
                    else stmt.Bases.Add(Ternary());

                    if (!TryEatOp(",")) break;
                }
                ExpectOp(")");
            }

            ExpectOp(":");
            stmt.Body = Suite();
            return stmt;
        }

        /// <summary>
        /// Parses a parameter list. Lambdas disallow annotations, since their `:`
        /// terminates the parameter list rather than introducing a type.
        /// </summary>
        PyParams ParameterList(string terminator, bool allowAnnotations = true)
        {
            var rv = new PyParams();
            bool keywordOnly = false;

            while (!IsOp(terminator) && Current.Type != TokType.EndOfFile)
            {
                if (TryEatOp("**")) { rv.StarStarKwargs = ExpectName(); }
                else if (IsOp("*"))
                {
                    index++;
                    // A bare `*` starts the keyword-only section.
                    if (Current.Type == TokType.Name) rv.StarArgs = ExpectName();
                    keywordOnly = true;
                }
                else if (TryEatOp("/"))
                {
                    // Positional-only marker; we do not enforce it.
                }
                else
                {
                    var p = new PyParam { Name = ExpectName(), KeywordOnly = keywordOnly };
                    if (allowAnnotations && TryEatOp(":")) Ternary();
                    if (TryEatOp("=")) p.Default = Ternary();
                    rv.Params.Add(p);
                }

                if (!TryEatOp(",")) break;
            }

            return rv;
        }

        PyStmt IfStatement()
        {
            int line = Current.Line;
            var stmt = new IfStmt { Line = line };

            ExpectKeyword("if");
            var branch = new IfStmt.Branch { Condition = Ternary() };
            ExpectOp(":");
            branch.Body = Suite();
            stmt.Branches.Add(branch);

            while (IsKeyword("elif"))
            {
                index++;
                var elif = new IfStmt.Branch { Condition = Ternary() };
                ExpectOp(":");
                elif.Body = Suite();
                stmt.Branches.Add(elif);
            }

            if (TryEatKeyword("else"))
            {
                ExpectOp(":");
                stmt.Branches.Add(new IfStmt.Branch { Condition = null, Body = Suite() });
            }

            return stmt;
        }

        PyStmt WhileStatement()
        {
            int line = Current.Line;
            ExpectKeyword("while");
            var stmt = new WhileStmt { Line = line, Condition = Ternary() };
            ExpectOp(":");
            stmt.Body = Suite();
            if (TryEatKeyword("else")) { ExpectOp(":"); stmt.OrElse = Suite(); }
            return stmt;
        }

        PyStmt ForStatement()
        {
            int line = Current.Line;
            ExpectKeyword("for");
            var stmt = new ForStmt { Line = line, Target = TargetList() };
            ExpectKeyword("in");
            stmt.Iterable = ExprList();
            ExpectOp(":");
            stmt.Body = Suite();
            if (TryEatKeyword("else")) { ExpectOp(":"); stmt.OrElse = Suite(); }
            return stmt;
        }

        PyStmt TryStatement()
        {
            int line = Current.Line;
            ExpectKeyword("try");
            ExpectOp(":");

            var stmt = new TryStmt { Line = line, Body = Suite() };

            while (IsKeyword("except"))
            {
                index++;
                var handler = new TryStmt.Handler();
                if (!IsOp(":"))
                {
                    TryEatOp("*");
                    handler.ExceptionType = Ternary();
                    if (TryEatKeyword("as")) handler.Name = ExpectName();
                    else if (TryEatOp(",")) handler.Name = ExpectName(); // Python 2 form
                }
                ExpectOp(":");
                handler.Body = Suite();
                stmt.Handlers.Add(handler);
            }

            if (TryEatKeyword("else")) { ExpectOp(":"); stmt.OrElse = Suite(); }
            if (TryEatKeyword("finally")) { ExpectOp(":"); stmt.Finally = Suite(); }

            return stmt;
        }

        PyStmt WithStatement()
        {
            int line = Current.Line;
            ExpectKeyword("with");
            var stmt = new WithStmt { Line = line };

            do
            {
                var item = new WithStmt.Item { Context = Ternary() };
                if (TryEatKeyword("as")) item.Target = TargetList();
                stmt.Items.Add(item);
            } while (TryEatOp(","));

            ExpectOp(":");
            stmt.Body = Suite();
            return stmt;
        }

        /// <summary>Parses either an indented block or a same-line simple statement list.</summary>
        List<PyStmt> Suite()
        {
            if (Current.Type != TokType.Newline)
                return SimpleStatementLine();

            index++;
            SkipNewlines();

            if (Current.Type != TokType.Indent)
                throw Error("expected an indented block");
            index++;

            var body = new List<PyStmt>();
            SkipNewlines();
            while (Current.Type != TokType.Dedent && Current.Type != TokType.EndOfFile)
            {
                body.AddRange(Statement());
                SkipNewlines();
            }

            if (Current.Type == TokType.Dedent) index++;
            return body;
        }

        // ---------------------------------------------------------------- expressions

        /// <summary>A bare comma-separated list, which builds a tuple.</summary>
        PyExprNode ExprList()
        {
            var first = Ternary();
            if (!IsOp(",")) return first;

            var seq = new SequenceExpr { Type = SequenceExpr.Kind.Tuple, Line = first.Line };
            seq.Items.Add(first);

            while (TryEatOp(","))
            {
                if (AtExpressionEnd()) break;
                seq.Items.Add(Ternary());
            }

            return seq;
        }

        bool AtExpressionEnd()
        {
            if (Current.Type == TokType.Newline || Current.Type == TokType.EndOfFile ||
                Current.Type == TokType.Dedent) return true;
            if (Current.Type == TokType.Op)
            {
                string t = Current.Text;
                return t == ")" || t == "]" || t == "}" || t == ":" || t == "=" || t == ";";
            }
            return Current.Type == TokType.Keyword && Current.Text == "in";
        }

        PyExprNode TargetList()
        {
            var first = Target();
            if (!IsOp(",")) return first;

            var seq = new SequenceExpr { Type = SequenceExpr.Kind.Tuple, Line = first.Line };
            seq.Items.Add(first);
            while (TryEatOp(","))
            {
                if (AtExpressionEnd()) break;
                seq.Items.Add(Target());
            }
            return seq;
        }

        PyExprNode Target()
        {
            if (TryEatOp("*")) return new StarExpr { Value = Target(), Line = Current.Line };
            if (TryEatOp("("))
            {
                var inner = TargetList();
                ExpectOp(")");
                return inner;
            }
            if (TryEatOp("["))
            {
                var inner = TargetList();
                ExpectOp("]");
                return inner;
            }
            return Unary();
        }

        PyExprNode Ternary()
        {
            if (IsKeyword("lambda")) return Lambda();

            var value = OrExpr();

            if (IsKeyword("if"))
            {
                index++;
                var condition = OrExpr();
                ExpectKeyword("else");
                return new IfExpr { Condition = condition, Then = value, Else = Ternary(), Line = value.Line };
            }

            return value;
        }

        PyExprNode Lambda()
        {
            int line = Current.Line;
            ExpectKeyword("lambda");
            var expr = new LambdaExpr { Line = line, Params = ParameterList(":", false) };
            ExpectOp(":");
            expr.Body = Ternary();
            return expr;
        }

        PyExprNode OrExpr()
        {
            var left = AndExpr();
            if (!IsKeyword("or")) return left;

            var rv = new BoolExpr { IsAnd = false, Line = left.Line };
            rv.Operands.Add(left);
            while (TryEatKeyword("or")) rv.Operands.Add(AndExpr());
            return rv;
        }

        PyExprNode AndExpr()
        {
            var left = NotExpr();
            if (!IsKeyword("and")) return left;

            var rv = new BoolExpr { IsAnd = true, Line = left.Line };
            rv.Operands.Add(left);
            while (TryEatKeyword("and")) rv.Operands.Add(NotExpr());
            return rv;
        }

        PyExprNode NotExpr()
        {
            if (IsKeyword("not"))
            {
                int line = Current.Line;
                index++;
                return new UnaryExpr { Op = "not", Operand = NotExpr(), Line = line };
            }
            return Comparison();
        }

        PyExprNode Comparison()
        {
            var left = BitOr();
            CompareExpr chain = null;

            while (true)
            {
                string op = null;

                if (Current.Type == TokType.Op)
                {
                    string t = Current.Text;
                    if (t == "<" || t == ">" || t == "==" || t == "!=" || t == "<=" || t == ">=")
                    {
                        op = t;
                        index++;
                    }
                }
                else if (IsKeyword("in")) { op = "in"; index++; }
                else if (IsKeyword("not") && Ahead().Type == TokType.Keyword && Ahead().Text == "in")
                {
                    op = "not in";
                    index += 2;
                }
                else if (IsKeyword("is"))
                {
                    index++;
                    if (TryEatKeyword("not")) op = "is not";
                    else op = "is";
                }

                if (op == null) break;

                if (chain == null) chain = new CompareExpr { Left = left, Line = left.Line };
                chain.Ops.Add(op);
                chain.Comparators.Add(BitOr());
            }

            return (PyExprNode)chain ?? left;
        }

        PyExprNode BitOr()
        {
            var left = BitXor();
            while (IsOp("|"))
            {
                index++;
                left = new BinaryExpr { Op = "|", Left = left, Right = BitXor(), Line = left.Line };
            }
            return left;
        }

        PyExprNode BitXor()
        {
            var left = BitAnd();
            while (IsOp("^"))
            {
                index++;
                left = new BinaryExpr { Op = "^", Left = left, Right = BitAnd(), Line = left.Line };
            }
            return left;
        }

        PyExprNode BitAnd()
        {
            var left = Shift();
            while (IsOp("&"))
            {
                index++;
                left = new BinaryExpr { Op = "&", Left = left, Right = Shift(), Line = left.Line };
            }
            return left;
        }

        PyExprNode Shift()
        {
            var left = Arithmetic();
            while (IsOp("<<") || IsOp(">>"))
            {
                string op = Advance().Text;
                left = new BinaryExpr { Op = op, Left = left, Right = Arithmetic(), Line = left.Line };
            }
            return left;
        }

        PyExprNode Arithmetic()
        {
            var left = Term();
            while (IsOp("+") || IsOp("-"))
            {
                string op = Advance().Text;
                left = new BinaryExpr { Op = op, Left = left, Right = Term(), Line = left.Line };
            }
            return left;
        }

        PyExprNode Term()
        {
            var left = Unary();
            while (IsOp("*") || IsOp("/") || IsOp("//") || IsOp("%") || IsOp("@"))
            {
                string op = Advance().Text;
                left = new BinaryExpr { Op = op, Left = left, Right = Unary(), Line = left.Line };
            }
            return left;
        }

        PyExprNode Unary()
        {
            if (IsOp("-") || IsOp("+") || IsOp("~"))
            {
                int line = Current.Line;
                string op = Advance().Text;
                return new UnaryExpr { Op = op, Operand = Unary(), Line = line };
            }
            if (IsKeyword("await")) { index++; return Unary(); }
            return Power();
        }

        PyExprNode Power()
        {
            var left = Postfix();
            if (IsOp("**"))
            {
                index++;
                // Right-associative, and the exponent may itself be unary-negated.
                return new BinaryExpr { Op = "**", Left = left, Right = Unary(), Line = left.Line };
            }
            return left;
        }

        PyExprNode Postfix()
        {
            var expr = Atom();

            while (true)
            {
                if (IsOp("."))
                {
                    index++;
                    expr = new AttrExpr { Target = expr, Name = ExpectName(), Line = expr.Line };
                }
                else if (IsOp("("))
                {
                    index++;
                    expr = CallArguments(expr);
                    ExpectOp(")");
                }
                else if (IsOp("["))
                {
                    index++;
                    expr = new IndexExpr { Target = expr, Index = Subscript(), Line = expr.Line };
                    ExpectOp("]");
                }
                else break;
            }

            return expr;
        }

        PyExprNode CallArguments(PyExprNode func)
        {
            var call = new CallExpr { Func = func, Line = func.Line };

            while (!IsOp(")") && Current.Type != TokType.EndOfFile)
            {
                if (TryEatOp("**"))
                {
                    call.Args.Add(new CallArg { DoubleStar = true, Value = Ternary() });
                }
                else if (TryEatOp("*"))
                {
                    call.Args.Add(new CallArg { Star = true, Value = Ternary() });
                }
                else if (Current.Type == TokType.Name && Ahead().Type == TokType.Op && Ahead().Text == "=")
                {
                    string name = ExpectName();
                    index++;
                    call.Args.Add(new CallArg { Name = name, Value = Ternary() });
                }
                else
                {
                    var value = Ternary();

                    // A trailing `for` makes this a generator expression argument.
                    if (IsKeyword("for"))
                    {
                        var comp = new ComprehensionExpr
                        {
                            Type = ComprehensionExpr.Kind.Generator,
                            Element = value,
                            Line = value.Line,
                        };
                        ParseComprehensionClauses(comp);
                        value = comp;
                    }

                    call.Args.Add(new CallArg { Value = value });
                }

                if (!TryEatOp(",")) break;
            }

            return call;
        }

        PyExprNode Subscript()
        {
            // A subscript may be a slice, an index, or a comma-separated tuple of either.
            var parts = new List<PyExprNode>();

            do
            {
                if (IsOp("]")) break;
                parts.Add(SubscriptItem());
            } while (TryEatOp(","));

            if (parts.Count == 0) return new ConstExpr(null);
            if (parts.Count == 1) return parts[0];

            var seq = new SequenceExpr { Type = SequenceExpr.Kind.Tuple, Line = parts[0].Line };
            seq.Items.AddRange(parts);
            return seq;
        }

        PyExprNode SubscriptItem()
        {
            PyExprNode start = null;
            if (!IsOp(":")) start = Ternary();

            if (!IsOp(":")) return start;

            var slice = new SliceExpr { Start = start, Line = Current.Line };
            ExpectOp(":");

            if (!IsOp("]") && !IsOp(":") && !IsOp(",")) slice.Stop = Ternary();

            if (TryEatOp(":"))
            {
                if (!IsOp("]") && !IsOp(",")) slice.Step = Ternary();
            }

            return slice;
        }

        PyExprNode Atom()
        {
            int line = Current.Line;

            switch (Current.Type)
            {
                case TokType.Number:
                    return new ConstExpr(Advance().Value) { Line = line };

                case TokType.String:
                    return StringLiteral();

                case TokType.FString:
                    return FStringLiteral();

                case TokType.Name:
                    return new NameExpr(Advance().Text) { Line = line };

                case TokType.Keyword:
                    switch (Current.Text)
                    {
                        case "True": index++; return new ConstExpr(true) { Line = line };
                        case "False": index++; return new ConstExpr(false) { Line = line };
                        case "None": index++; return new ConstExpr(null) { Line = line };
                        case "lambda": return Lambda();
                        case "not": return NotExpr();
                        // `print` used as a plain name (Python 3 builtin).
                        case "print": index++; return new NameExpr("print") { Line = line };
                    }
                    break;

                case TokType.Op:
                    if (Current.Text == "(") return ParenExpr();
                    if (Current.Text == "[") return ListDisplay();
                    if (Current.Text == "{") return DictOrSetDisplay();
                    if (Current.Text == "*")
                    {
                        index++;
                        return new StarExpr { Value = Ternary(), Line = line };
                    }
                    if (Current.Text == "...") { index++; return new ConstExpr(null) { Line = line }; }
                    break;
            }

            throw Error("unexpected '" + Describe(Current) + "' in expression");
        }

        /// <summary>Adjacent string literals concatenate, as in Python.</summary>
        PyExprNode StringLiteral()
        {
            int line = Current.Line;
            object first = Advance().Value;

            if (first is byte[] || Current.Type != TokType.String)
            {
                if (Current.Type != TokType.String && Current.Type != TokType.FString)
                    return new ConstExpr(first) { Line = line };
            }

            var sb = new StringBuilder(PyOps.ToStr(first));
            bool sawFormat = false;
            var parts = new List<Token>();

            while (Current.Type == TokType.String || Current.Type == TokType.FString)
            {
                if (Current.Type == TokType.FString) sawFormat = true;
                parts.Add(Current);
                sb.Append(PyOps.ToStr(Advance().Value));
            }

            if (!sawFormat) return new ConstExpr(sb.ToString()) { Line = line };

            // Mixed literal/f-string concatenation: re-parse the whole thing as an f-string.
            return ParseFString(sb.ToString(), line);
        }

        PyExprNode FStringLiteral()
        {
            int line = Current.Line;
            var sb = new StringBuilder(PyOps.ToStr(Advance().Value));

            while (Current.Type == TokType.String || Current.Type == TokType.FString)
                sb.Append(PyOps.ToStr(Advance().Value));

            return ParseFString(sb.ToString(), line);
        }

        /// <summary>Splits f-string text into literal and expression parts.</summary>
        PyExprNode ParseFString(string text, int line)
        {
            var rv = new FStringExpr { Line = line };
            var literal = new StringBuilder();

            int i = 0;
            while (i < text.Length)
            {
                char c = text[i];

                if (c == '{')
                {
                    if (i + 1 < text.Length && text[i + 1] == '{') { literal.Append('{'); i += 2; continue; }

                    int depth = 0;
                    int start = i;
                    int end = -1;
                    for (int j = i; j < text.Length; j++)
                    {
                        if (text[j] == '{') depth++;
                        else if (text[j] == '}') { depth--; if (depth == 0) { end = j; break; } }
                    }
                    if (end < 0) throw Error("unterminated '{' in f-string");

                    string body = text.Substring(start + 1, end - start - 1);
                    if (literal.Length > 0)
                    {
                        rv.Parts.Add(new FStringExpr.Part { Literal = literal.ToString() });
                        literal.Clear();
                    }
                    rv.Parts.Add(BuildFStringPart(body, line));
                    i = end + 1;
                    continue;
                }

                if (c == '}')
                {
                    if (i + 1 < text.Length && text[i + 1] == '}') { literal.Append('}'); i += 2; continue; }
                    literal.Append('}');
                    i++;
                    continue;
                }

                literal.Append(c);
                i++;
            }

            if (literal.Length > 0) rv.Parts.Add(new FStringExpr.Part { Literal = literal.ToString() });
            return rv;
        }

        FStringExpr.Part BuildFStringPart(string body, int line)
        {
            var part = new FStringExpr.Part();

            // Split off the format spec and conversion, ignoring anything inside brackets
            // or string literals.
            int depth = 0;
            char quote = '\0';
            for (int i = 0; i < body.Length; i++)
            {
                char c = body[i];

                if (quote != '\0') { if (c == quote) quote = '\0'; continue; }
                if (c == '\'' || c == '"') { quote = c; continue; }
                if (c == '(' || c == '[' || c == '{') { depth++; continue; }
                if (c == ')' || c == ']' || c == '}') { depth--; continue; }
                if (depth != 0) continue;

                if (c == ':')
                {
                    part.FormatSpec = body.Substring(i + 1);
                    body = body.Substring(0, i);
                    break;
                }

                if (c == '!' && i + 1 < body.Length && body[i + 1] != '=')
                {
                    part.Conversion = body.Substring(i + 1);
                    body = body.Substring(0, i);
                    break;
                }
            }

            // `{x=}` debugging form.
            body = body.TrimEnd();
            if (body.EndsWith("=", StringComparison.Ordinal))
            {
                body = body.Substring(0, body.Length - 1);
                part.Literal = body.Trim() + "=";
            }

            part.Expr = ParseExpression(body, filename);
            return part;
        }

        PyExprNode ParenExpr()
        {
            int line = Current.Line;
            ExpectOp("(");

            if (TryEatOp(")"))
                return new SequenceExpr { Type = SequenceExpr.Kind.Tuple, Line = line };

            var first = Ternary();

            if (IsKeyword("for"))
            {
                var comp = new ComprehensionExpr
                {
                    Type = ComprehensionExpr.Kind.Generator,
                    Element = first,
                    Line = line,
                };
                ParseComprehensionClauses(comp);
                ExpectOp(")");
                return comp;
            }

            if (IsOp(","))
            {
                var seq = new SequenceExpr { Type = SequenceExpr.Kind.Tuple, Line = line };
                seq.Items.Add(first);
                while (TryEatOp(","))
                {
                    if (IsOp(")")) break;
                    seq.Items.Add(Ternary());
                }
                ExpectOp(")");
                return seq;
            }

            ExpectOp(")");
            return first;
        }

        PyExprNode ListDisplay()
        {
            int line = Current.Line;
            ExpectOp("[");

            if (TryEatOp("]"))
                return new SequenceExpr { Type = SequenceExpr.Kind.List, Line = line };

            var first = Ternary();

            if (IsKeyword("for"))
            {
                var comp = new ComprehensionExpr
                {
                    Type = ComprehensionExpr.Kind.List,
                    Element = first,
                    Line = line,
                };
                ParseComprehensionClauses(comp);
                ExpectOp("]");
                return comp;
            }

            var seq = new SequenceExpr { Type = SequenceExpr.Kind.List, Line = line };
            seq.Items.Add(first);
            while (TryEatOp(","))
            {
                if (IsOp("]")) break;
                seq.Items.Add(Ternary());
            }
            ExpectOp("]");
            return seq;
        }

        PyExprNode DictOrSetDisplay()
        {
            int line = Current.Line;
            ExpectOp("{");

            if (TryEatOp("}")) return new DictExpr { Line = line };

            // `{**mapping}` is always a dict.
            if (IsOp("**"))
            {
                var dict = new DictExpr { Line = line };
                while (!IsOp("}"))
                {
                    if (TryEatOp("**"))
                        dict.Entries.Add(new KeyValuePair<PyExprNode, PyExprNode>(null, Ternary()));
                    else
                    {
                        var k = Ternary();
                        ExpectOp(":");
                        dict.Entries.Add(new KeyValuePair<PyExprNode, PyExprNode>(k, Ternary()));
                    }
                    if (!TryEatOp(",")) break;
                }
                ExpectOp("}");
                return dict;
            }

            var first = Ternary();

            if (IsOp(":"))
            {
                index++;
                var firstValue = Ternary();

                if (IsKeyword("for"))
                {
                    var comp = new ComprehensionExpr
                    {
                        Type = ComprehensionExpr.Kind.Dict,
                        Element = first,
                        ValueElement = firstValue,
                        Line = line,
                    };
                    ParseComprehensionClauses(comp);
                    ExpectOp("}");
                    return comp;
                }

                var dict = new DictExpr { Line = line };
                dict.Entries.Add(new KeyValuePair<PyExprNode, PyExprNode>(first, firstValue));

                while (TryEatOp(","))
                {
                    if (IsOp("}")) break;
                    if (TryEatOp("**"))
                    {
                        dict.Entries.Add(new KeyValuePair<PyExprNode, PyExprNode>(null, Ternary()));
                        continue;
                    }
                    var k = Ternary();
                    ExpectOp(":");
                    dict.Entries.Add(new KeyValuePair<PyExprNode, PyExprNode>(k, Ternary()));
                }

                ExpectOp("}");
                return dict;
            }

            if (IsKeyword("for"))
            {
                var comp = new ComprehensionExpr
                {
                    Type = ComprehensionExpr.Kind.Set,
                    Element = first,
                    Line = line,
                };
                ParseComprehensionClauses(comp);
                ExpectOp("}");
                return comp;
            }

            var set = new SequenceExpr { Type = SequenceExpr.Kind.Set, Line = line };
            set.Items.Add(first);
            while (TryEatOp(","))
            {
                if (IsOp("}")) break;
                set.Items.Add(Ternary());
            }
            ExpectOp("}");
            return set;
        }

        void ParseComprehensionClauses(ComprehensionExpr comp)
        {
            while (IsKeyword("for"))
            {
                index++;
                var clause = new ComprehensionClause { Target = TargetList() };
                ExpectKeyword("in");
                // The iterable binds tighter than a trailing `if`, so stop before it.
                clause.Iterable = OrExpr();

                while (IsKeyword("if"))
                {
                    index++;
                    clause.Conditions.Add(OrExpr());
                }

                comp.Clauses.Add(clause);
            }
        }
    }
}
