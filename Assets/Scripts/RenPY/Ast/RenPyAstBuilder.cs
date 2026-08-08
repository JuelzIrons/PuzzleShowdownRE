using System;
using System.Collections.Generic;
using RenPy.Pickle;
using RenPy.Python;

namespace RenPy.Ast
{
    /// <summary>
    /// Converts the raw unpickled object graph from a .rpyc into typed <see cref="Node"/>s.
    ///
    /// Statement nodes are allocated first and filled in afterwards from a work queue.
    /// A script chains thousands of nodes through `next`, so recursing over that chain
    /// would overflow the stack.
    /// </summary>
    public class RenPyAstBuilder
    {
        readonly Dictionary<PyInstance, Node> nodes = new Dictionary<PyInstance, Node>(ReferenceComparer.Instance);
        readonly Queue<PyInstance> pending = new Queue<PyInstance>();

        /// <summary>Node types encountered that we do not model, for diagnostics.</summary>
        public readonly Dictionary<string, int> UnhandledTypes = new Dictionary<string, int>();

        /// <summary>
        /// Builds the top-level statement list of a script file. The unpickled root is
        /// a (metadata, statements) tuple.
        /// </summary>
        public List<Node> BuildScript(object root)
        {
            object stmts = root;

            var tuple = root as PyTuple;
            if (tuple != null && tuple.Items.Length >= 2) stmts = tuple.Items[1];

            var list = ConvertBlock(stmts);
            Drain();
            return list;
        }

        void Drain()
        {
            while (pending.Count > 0)
            {
                var inst = pending.Dequeue();
                Fill(inst, nodes[inst]);
            }
        }

        // ---------------------------------------------------------------- dispatch

        /// <summary>Allocates (or returns) the typed node for a pickled statement.</summary>
        public Node Convert(object o)
        {
            var inst = o as PyInstance;
            if (inst == null) return null;

            Node existing;
            if (nodes.TryGetValue(inst, out existing)) return existing;

            Node node = Allocate(QualName(inst));
            nodes[inst] = node;
            pending.Enqueue(inst);
            return node;
        }

        static string QualName(PyInstance inst)
        {
            return inst.Type != null ? inst.Type.QualName : "?";
        }

        Node Allocate(string qual)
        {
            switch (qual)
            {
                case "renpy.ast.Init": return new InitNode();
                case "renpy.ast.Label": return new LabelNode();
                case "renpy.ast.Pass": return new PassNode();
                case "renpy.ast.Python": return new PythonNode();
                case "renpy.ast.EarlyPython": return new EarlyPythonNode();
                case "renpy.ast.Define": return new DefineNode();
                case "renpy.ast.Default": return new DefaultNode();
                case "renpy.ast.Show": return new ShowNode();
                case "renpy.ast.Scene": return new SceneNode();
                case "renpy.ast.Hide": return new HideNode();
                case "renpy.ast.ShowLayer": return new ShowLayerNode();
                case "renpy.ast.Camera": return new CameraNode();
                case "renpy.ast.With": return new WithNode();
                case "renpy.ast.Image": return new ImageNode();
                case "renpy.ast.Transform": return new TransformNode();
                case "renpy.ast.Say": return new SayNode();
                case "renpy.ast.TranslateSay": return new SayNode();
                case "renpy.ast.Menu": return new MenuNode();
                case "renpy.ast.Jump": return new JumpNode();
                case "renpy.ast.Call": return new CallNode();
                case "renpy.ast.Return": return new ReturnNode();
                case "renpy.ast.If": return new IfNode();
                case "renpy.ast.While": return new WhileNode();
                case "renpy.ast.Screen": return new ScreenNode();
                case "renpy.ast.Style": return new StyleNode();
                case "renpy.ast.UserStatement": return new UserStatementNode();
                case "renpy.ast.PostUserStatement": return new PassNode();
                case "renpy.ast.Translate": return new TranslateNode();
                case "renpy.ast.EndTranslate": return new EndTranslateNode();
                case "renpy.ast.TranslateString": return new TranslateStringNode();
                case "renpy.ast.TranslateBlock": return new TranslateNode();
                case "renpy.ast.TranslateEarlyBlock": return new TranslateNode();
                case "renpy.ast.RPY": return new PassNode();
                case "renpy.ast.Testcase": return new PassNode();
                default:
                    Count(UnhandledTypes, qual);
                    return new UnknownNode { OriginalType = qual };
            }
        }

        static void Count(Dictionary<string, int> d, string key)
        {
            int c;
            d.TryGetValue(key, out c);
            d[key] = c + 1;
        }

        // ---------------------------------------------------------------- filling

        void Fill(PyInstance inst, Node node)
        {
            node.Name = inst.Field("name");
            node.Filename = Str(inst.Field("filename"));
            node.LineNumber = inst.IntField("linenumber");
            node.Next = Convert(inst.Field("next"));

            var init = node as InitNode;
            if (init != null)
            {
                init.Priority = inst.IntField("priority");
                init.Block = ConvertBlock(inst.Field("block"));
                return;
            }

            var label = node as LabelNode;
            if (label != null)
            {
                label.LabelName = Str(inst.Field("name"));
                label.Block = ConvertBlock(inst.Field("block"));
                label.Parameters = BuildParameters(inst.Field("parameters"));
                label.Hide = Bool(inst.Field("hide"));
                return;
            }

            var python = node as PythonNode;
            if (python != null)
            {
                python.Code = BuildPyCode(inst.Field("code"));
                python.Store = Str(inst.Field("store")) ?? "store";
                python.Hide = Bool(inst.Field("hide"));
                return;
            }

            var early = node as EarlyPythonNode;
            if (early != null)
            {
                early.Code = BuildPyCode(inst.Field("code"));
                early.Store = Str(inst.Field("store")) ?? "store";
                early.Hide = Bool(inst.Field("hide"));
                return;
            }

            var define = node as DefineNode;
            if (define != null)
            {
                define.VarName = Str(inst.Field("varname"));
                define.Store = Str(inst.Field("store")) ?? "store";
                define.Operator = Str(inst.Field("operator")) ?? "=";
                define.Index = inst.Field("index");
                define.Code = BuildPyCode(inst.Field("code"));
                return;
            }

            var dflt = node as DefaultNode;
            if (dflt != null)
            {
                dflt.VarName = Str(inst.Field("varname"));
                dflt.Store = Str(inst.Field("store")) ?? "store";
                dflt.Code = BuildPyCode(inst.Field("code"));
                return;
            }

            var show = node as ShowNode;
            if (show != null)
            {
                show.Spec = BuildImSpec(inst.Field("imspec"));
                show.Atl = BuildAtlBlock(inst.Field("atl"));
                return;
            }

            var scene = node as SceneNode;
            if (scene != null)
            {
                scene.Spec = BuildImSpec(inst.Field("imspec"));
                scene.Layer = Str(inst.Field("layer"));
                scene.Atl = BuildAtlBlock(inst.Field("atl"));
                return;
            }

            var hide = node as HideNode;
            if (hide != null)
            {
                hide.Spec = BuildImSpec(inst.Field("imspec"));
                return;
            }

            var showLayer = node as ShowLayerNode;
            if (showLayer != null)
            {
                showLayer.Layer = Str(inst.Field("layer"));
                showLayer.Atl = BuildAtlBlock(inst.Field("atl"));
                showLayer.AtList = StrList(inst.Field("at_list"));
                return;
            }

            var camera = node as CameraNode;
            if (camera != null)
            {
                camera.Layer = Str(inst.Field("layer"));
                camera.Atl = BuildAtlBlock(inst.Field("atl"));
                camera.AtList = StrList(inst.Field("at_list"));
                return;
            }

            var with = node as WithNode;
            if (with != null)
            {
                with.Expr = Str(inst.Field("expr"));
                with.Paired = Str(inst.Field("paired"));
                return;
            }

            var image = node as ImageNode;
            if (image != null)
            {
                image.ImgName = StrList(inst.Field("imgname"));
                image.Code = BuildPyCode(inst.Field("code"));
                image.Atl = BuildAtlBlock(inst.Field("atl"));
                return;
            }

            var transform = node as TransformNode;
            if (transform != null)
            {
                transform.VarName = Str(inst.Field("varname"));
                transform.Store = Str(inst.Field("store")) ?? "store";
                transform.Atl = BuildAtlBlock(inst.Field("atl"));
                transform.Parameters = BuildParameters(inst.Field("parameters"));
                return;
            }

            var say = node as SayNode;
            if (say != null)
            {
                say.Who = Str(inst.Field("who"));
                say.What = Str(inst.Field("what"));
                say.With = Str(inst.Field("with_"));
                object interact = inst.Field("interact");
                say.Interact = interact == null || Py.Truthy(interact);
                say.Arguments = BuildArguments(inst.Field("arguments"));
                say.Attributes = Join(inst.Field("attributes"));
                say.TemporaryAttributes = Join(inst.Field("temporary_attributes"));
                say.Identifier = Str(inst.Field("identifier"));
                return;
            }

            var menu = node as MenuNode;
            if (menu != null)
            {
                FillMenu(inst, menu);
                return;
            }

            var jump = node as JumpNode;
            if (jump != null)
            {
                jump.Target = Str(inst.Field("target"));
                jump.IsExpression = Bool(inst.Field("expression"));
                return;
            }

            var call = node as CallNode;
            if (call != null)
            {
                call.Label = Str(inst.Field("label"));
                call.IsExpression = Bool(inst.Field("expression"));
                call.Arguments = BuildArguments(inst.Field("arguments"));
                return;
            }

            var ret = node as ReturnNode;
            if (ret != null)
            {
                ret.Expression = Str(inst.Field("expression"));
                return;
            }

            var ifNode = node as IfNode;
            if (ifNode != null)
            {
                foreach (var entry in Items(inst.Field("entries")))
                {
                    var pair = entry as PyTuple;
                    if (pair == null || pair.Items.Length < 2) continue;
                    ifNode.Entries.Add(new IfEntry
                    {
                        Condition = Str(pair.Items[0]) ?? "True",
                        Block = ConvertBlock(pair.Items[1]),
                    });
                }
                return;
            }

            var whileNode = node as WhileNode;
            if (whileNode != null)
            {
                whileNode.Condition = Str(inst.Field("condition")) ?? "True";
                whileNode.Block = ConvertBlock(inst.Field("block"));
                return;
            }

            var screen = node as ScreenNode;
            if (screen != null)
            {
                screen.Screen = BuildSL(inst.Field("screen")) as SLScreen;
                return;
            }

            var style = node as StyleNode;
            if (style != null)
            {
                style.StyleName = Str(inst.Field("style_name"));
                style.Parent = Str(inst.Field("parent"));
                style.Properties = inst.Field("properties") as PyDict ?? new PyDict();
                style.Clear = Bool(inst.Field("clear"));
                style.Take = Str(inst.Field("take"));
                style.DelAttr = StrList(inst.Field("delattr"));
                style.Variant = Str(inst.Field("variant"));
                return;
            }

            var user = node as UserStatementNode;
            if (user != null)
            {
                user.Line = Str(inst.Field("line")) ?? "";
                user.Parsed = inst.Field("parsed");
                user.Block = ConvertBlock(inst.Field("block"));
                object codeBlock = inst.Field("code_block");
                user.CodeBlock = codeBlock == null ? null : ConvertBlock(codeBlock);
                user.Translatable = Bool(inst.Field("translatable"));
                user.Rollback = Str(inst.Field("rollback")) ?? "normal";
                return;
            }

            var translate = node as TranslateNode;
            if (translate != null)
            {
                translate.Identifier = Str(inst.Field("identifier"));
                translate.Language = Str(inst.Field("language"));
                translate.Block = ConvertBlock(inst.Field("block"));
                return;
            }

            var tstring = node as TranslateStringNode;
            if (tstring != null)
            {
                tstring.Language = Str(inst.Field("language"));
                tstring.Old = Str(inst.Field("old"));
                tstring.New = Str(inst.Field("new"));
                return;
            }
        }

        void FillMenu(PyInstance inst, MenuNode menu)
        {
            menu.Set = Str(inst.Field("set"));
            menu.With = Str(inst.Field("with_"));
            menu.HasCaption = Bool(inst.Field("has_caption"));
            menu.Arguments = BuildArguments(inst.Field("arguments"));

            var itemArgs = inst.Field("item_arguments") as PyList;
            int index = 0;

            foreach (var raw in Items(inst.Field("items")))
            {
                var triple = raw as PyTuple;
                if (triple == null || triple.Items.Length < 3) { index++; continue; }

                var item = new MenuItem
                {
                    Label = Str(triple.Items[0]),
                    Condition = Str(triple.Items[1]) ?? "True",
                    // A null block marks a caption line rather than a selectable choice.
                    Block = triple.Items[2] == null ? null : ConvertBlock(triple.Items[2]),
                };

                if (itemArgs != null && index < itemArgs.Items.Count)
                    item.Arguments = BuildArguments(itemArgs.Items[index]);

                menu.Items.Add(item);
                index++;
            }
        }

        public List<Node> ConvertBlock(object o)
        {
            var rv = new List<Node>();
            foreach (var item in Items(o))
            {
                Node n = Convert(item);
                if (n != null) rv.Add(n);
            }
            return rv;
        }

        // ---------------------------------------------------------------- pieces

        public static PyCodeRef BuildPyCode(object o)
        {
            var inst = o as PyInstance;
            if (inst == null) return null;

            // PyCode pickles a positional tuple: (1, source, location, mode, py).
            var state = inst.Field(PickleReader.RawStateKey) as PyTuple;
            if (state == null) return null;

            var rv = new PyCodeRef();
            if (state.Items.Length > 1) rv.Source = Str(state.Items[1]) ?? "";
            if (state.Items.Length > 3) rv.Mode = Str(state.Items[3]) ?? "exec";

            var loc = state.Items.Length > 2 ? state.Items[2] as PyTuple : null;
            if (loc != null && loc.Items.Length >= 2)
            {
                rv.Filename = Str(loc.Items[0]) ?? "";
                rv.LineNumber = loc.Items[1] == null ? 0 : (int)Py.ToInt(loc.Items[1]);
            }

            return rv;
        }

        public static ImSpec BuildImSpec(object o)
        {
            var t = o as PyTuple;
            if (t == null) return null;

            var rv = new ImSpec();
            var items = t.Items;

            // Ren'Py has grown this tuple over time: 3, 6, and 7 element forms exist.
            if (items.Length >= 6)
            {
                rv.Name = StrList(items[0]);
                rv.Expression = Str(items[1]);
                rv.Tag = Str(items[2]);
                rv.AtList = StrList(items[3]);
                rv.Layer = Str(items[4]);
                rv.ZOrder = Str(items[5]);
                if (items.Length >= 7) rv.Behind = StrList(items[6]);
            }
            else if (items.Length >= 3)
            {
                rv.Name = StrList(items[0]);
                rv.AtList = StrList(items[1]);
                rv.Layer = Str(items[2]);
            }

            return rv;
        }

        public static ParameterInfo BuildParameters(object o)
        {
            var inst = o as PyInstance;
            if (inst == null) return null;

            var rv = new ParameterInfo
            {
                ExtraPos = Str(inst.Field("extrapos")),
                ExtraKw = Str(inst.Field("extrakw")),
            };

            foreach (var item in Items(inst.Field("parameters")))
            {
                var pair = item as PyTuple;
                if (pair == null || pair.Items.Length == 0) continue;
                rv.Parameters.Add(new Parameter
                {
                    Name = Str(pair.Items[0]),
                    Default = pair.Items.Length > 1 ? Str(pair.Items[1]) : null,
                });
            }

            foreach (var item in Items(inst.Field("positional_only")))
            {
                var pair = item as PyTuple;
                rv.PositionalOnly.Add(pair != null && pair.Items.Length > 0 ? Str(pair.Items[0]) : Str(item));
            }

            foreach (var item in Items(inst.Field("keyword_only")))
            {
                var pair = item as PyTuple;
                rv.KeywordOnly.Add(pair != null && pair.Items.Length > 0 ? Str(pair.Items[0]) : Str(item));
            }

            return rv;
        }

        public static ArgumentInfo BuildArguments(object o)
        {
            var inst = o as PyInstance;
            if (inst == null) return null;

            var rv = new ArgumentInfo
            {
                ExtraPos = Str(inst.Field("extrapos")),
                ExtraKw = Str(inst.Field("extrakw")),
            };

            foreach (var item in Items(inst.Field("arguments")))
            {
                var pair = item as PyTuple;
                if (pair == null || pair.Items.Length < 2) continue;
                rv.Arguments.Add(new KeyValuePair<string, string>(Str(pair.Items[0]), Str(pair.Items[1])));
            }

            return rv;
        }

        // ---------------------------------------------------------------- ATL

        public RawBlock BuildAtlBlock(object o)
        {
            return BuildAtl(o) as RawBlock;
        }

        public RawStatement BuildAtl(object o)
        {
            var inst = o as PyInstance;
            if (inst == null) return null;

            string qual = QualName(inst);
            RawStatement rv;

            switch (qual)
            {
                case "renpy.atl.RawBlock":
                {
                    var b = new RawBlock { Animation = Bool(inst.Field("animation")) };
                    foreach (var s in Items(inst.Field("statements")))
                    {
                        var built = BuildAtl(s);
                        if (built != null) b.Statements.Add(built);
                    }
                    rv = b;
                    break;
                }

                case "renpy.atl.RawMultipurpose":
                {
                    var m = new RawMultipurpose
                    {
                        Warper = Str(inst.Field("warper")),
                        Duration = Str(inst.Field("duration")),
                        WarpFunction = Str(inst.Field("warp_function")),
                        Revolution = Str(inst.Field("revolution")),
                        Circles = Str(inst.Field("circles")),
                    };

                    foreach (var e in Items(inst.Field("expressions")))
                    {
                        var pair = e as PyTuple;
                        if (pair == null || pair.Items.Length == 0) continue;
                        m.Expressions.Add(new KeyValuePair<string, string>(
                            Str(pair.Items[0]),
                            pair.Items.Length > 1 ? Str(pair.Items[1]) : null));
                    }

                    foreach (var p in Items(inst.Field("properties")))
                    {
                        var pair = p as PyTuple;
                        if (pair == null || pair.Items.Length < 2) continue;
                        m.Properties.Add(new KeyValuePair<string, string>(Str(pair.Items[0]), Str(pair.Items[1])));
                    }

                    foreach (var s in Items(inst.Field("splines")))
                    {
                        var pair = s as PyTuple;
                        if (pair == null || pair.Items.Length < 2) continue;
                        m.Splines.Add(new KeyValuePair<string, List<string>>(
                            Str(pair.Items[0]), StrList(pair.Items[1])));
                    }

                    rv = m;
                    break;
                }

                case "renpy.atl.RawParallel":
                {
                    var p = new RawParallel();
                    foreach (var b in Items(inst.Field("blocks")))
                    {
                        var built = BuildAtl(b) as RawBlock;
                        if (built != null) p.Blocks.Add(built);
                    }
                    rv = p;
                    break;
                }

                case "renpy.atl.RawChoice":
                {
                    var c = new RawChoice();
                    foreach (var item in Items(inst.Field("choices")))
                    {
                        var pair = item as PyTuple;
                        if (pair == null || pair.Items.Length < 2) continue;
                        c.Choices.Add(new KeyValuePair<string, RawBlock>(
                            Str(pair.Items[0]), BuildAtl(pair.Items[1]) as RawBlock));
                    }
                    rv = c;
                    break;
                }

                case "renpy.atl.RawRepeat":
                    rv = new RawRepeat { Repeats = Str(inst.Field("repeats")) };
                    break;

                case "renpy.atl.RawTime":
                    rv = new RawTime { Time = Str(inst.Field("time")) };
                    break;

                case "renpy.atl.RawOn":
                {
                    var on = new RawOn();
                    var handlers = inst.Field("handlers") as PyDict;
                    if (handlers != null)
                    {
                        foreach (var kv in handlers)
                        {
                            var block = BuildAtl(kv.Value) as RawBlock;
                            if (block != null) on.Handlers[PyOps.ToStr(kv.Key)] = block;
                        }
                    }
                    rv = on;
                    break;
                }

                case "renpy.atl.RawEvent":
                    rv = new RawEvent { EventName = Str(inst.Field("name")) };
                    break;

                case "renpy.atl.RawFunction":
                    rv = new RawFunction { Expr = Str(inst.Field("expr")) };
                    break;

                case "renpy.atl.RawContainsExpr":
                    rv = new RawContainsExpr { Expression = Str(inst.Field("expression")) };
                    break;

                case "renpy.atl.RawChild":
                {
                    var c = new RawChild();
                    foreach (var b in Items(inst.Field("children")))
                    {
                        var built = BuildAtl(b) as RawBlock;
                        if (built != null) c.Children.Add(built);
                    }
                    rv = c;
                    break;
                }

                default:
                    Count(UnhandledTypes, qual);
                    return null;
            }

            var loc = inst.Field("loc") as PyTuple;
            if (loc != null && loc.Items.Length >= 2)
            {
                rv.Filename = Str(loc.Items[0]);
                rv.LineNumber = loc.Items[1] == null ? 0 : (int)Py.ToInt(loc.Items[1]);
            }

            return rv;
        }

        // ---------------------------------------------------------------- screens

        public SLNode BuildSL(object o)
        {
            var inst = o as PyInstance;
            if (inst == null) return null;

            string qual = QualName(inst);
            SLNode rv;

            switch (qual)
            {
                case "renpy.sl2.slast.SLScreen":
                {
                    var s = new SLScreen
                    {
                        ScreenName = Str(inst.Field("name")),
                        Parameters = BuildParameters(inst.Field("parameters")),
                        Tag = Str(inst.Field("tag")),
                        Layer = Str(inst.Field("layer")),
                        Modal = Str(inst.Field("modal")),
                        ZOrder = Str(inst.Field("zorder")),
                        Variant = Str(inst.Field("variant")),
                        Predict = Str(inst.Field("predict")),
                        Sensitive = Str(inst.Field("sensitive")),
                        RollForward = Str(inst.Field("roll_forward")),
                    };
                    FillSLBlock(inst, s);
                    rv = s;
                    break;
                }

                case "renpy.sl2.slast.SLDisplayable":
                {
                    var d = new SLDisplayable
                    {
                        DisplayableName = CallableName(inst.Field("displayable")),
                        StatementName = Str(inst.Field("name")),
                        Positional = StrList(inst.Field("positional")),
                        Style = Str(inst.Field("style")),
                        ChildOrFixed = Bool(inst.Field("child_or_fixed")),
                        Scope = Bool(inst.Field("scope")),
                        ReplacesParameter = Bool(inst.Field("replaces")),
                        Variable = Str(inst.Field("variable")),
                        Imagemap = Bool(inst.Field("imagemap")),
                        Hotspot = Bool(inst.Field("hotspot")),
                    };

                    var defaults = inst.Field("default_keywords") as PyDict;
                    if (defaults != null)
                        foreach (var kv in defaults)
                            d.DefaultKeywords.Add(new KeyValuePair<string, string>(
                                PyOps.ToStr(kv.Key), kv.Value == null ? null : PyOps.ToStr(kv.Value)));

                    FillSLBlock(inst, d);
                    rv = d;
                    break;
                }

                case "renpy.sl2.slast.SLBlock":
                {
                    var b = new SLBlock();
                    FillSLBlock(inst, b);
                    rv = b;
                    break;
                }

                case "renpy.sl2.slast.SLIf":
                case "renpy.sl2.slast.SLShowIf":
                {
                    var i = new SLIf { ShowIf = qual.EndsWith("SLShowIf", StringComparison.Ordinal) };
                    foreach (var entry in Items(inst.Field("entries")))
                    {
                        var pair = entry as PyTuple;
                        if (pair == null || pair.Items.Length < 2) continue;
                        i.Entries.Add(new SLIfEntry
                        {
                            Condition = Str(pair.Items[0]),
                            Block = BuildSL(pair.Items[1]) as SLBlock,
                        });
                    }
                    rv = i;
                    break;
                }

                case "renpy.sl2.slast.SLFor":
                {
                    var f = new SLFor
                    {
                        Variable = Str(inst.Field("variable")),
                        Expression = Str(inst.Field("expression")),
                        IndexExpression = Str(inst.Field("index_expression")),
                    };
                    FillSLBlock(inst, f);
                    rv = f;
                    break;
                }

                case "renpy.sl2.slast.SLPython":
                    rv = new SLPython { Code = BuildPyCode(inst.Field("code")) };
                    break;

                case "renpy.sl2.slast.SLPass":
                    rv = new SLPass();
                    break;

                case "renpy.sl2.slast.SLDefault":
                    rv = new SLDefault
                    {
                        Variable = Str(inst.Field("variable")),
                        Expression = Str(inst.Field("expression")),
                    };
                    break;

                case "renpy.sl2.slast.SLUse":
                    rv = new SLUse
                    {
                        Target = Str(inst.Field("target")),
                        Args = BuildArguments(inst.Field("args")),
                        Block = BuildSL(inst.Field("block")) as SLBlock,
                        Id = Str(inst.Field("id")),
                        Ast = BuildSL(inst.Field("ast")) as SLScreen,
                    };
                    break;

                case "renpy.sl2.slast.SLCustomUse":
                    rv = new SLCustomUse
                    {
                        Target = Str(inst.Field("target")),
                        Positional = StrList(inst.Field("positional")),
                        Block = BuildSL(inst.Field("block")) as SLBlock,
                        Ast = BuildSL(inst.Field("ast")) as SLScreen,
                    };
                    break;

                case "renpy.sl2.slast.SLTransclude":
                    rv = new SLTransclude();
                    break;

                default:
                    Count(UnhandledTypes, qual);
                    return null;
            }

            var loc = inst.Field("location") as PyTuple;
            if (loc != null && loc.Items.Length >= 2)
            {
                rv.Filename = Str(loc.Items[0]);
                rv.LineNumber = loc.Items[1] == null ? 0 : (int)Py.ToInt(loc.Items[1]);
            }
            rv.Serial = inst.IntField("serial");

            return rv;
        }

        void FillSLBlock(PyInstance inst, SLBlock block)
        {
            foreach (var child in Items(inst.Field("children")))
            {
                var built = BuildSL(child);
                if (built != null) block.Children.Add(built);
            }

            // `keyword` is a list of (name, expression) pairs, ordered as written.
            foreach (var kw in Items(inst.Field("keyword")))
            {
                var pair = kw as PyTuple;
                if (pair == null || pair.Items.Length < 2) continue;
                block.Keyword.Add(new KeyValuePair<string, string>(
                    PyOps.ToStr(pair.Items[0]), pair.Items[1] == null ? null : PyOps.ToStr(pair.Items[1])));
            }
        }

        /// <summary>The qualified name behind a pickled function or class reference.</summary>
        public static string CallableName(object o)
        {
            var native = o as PyNative;
            if (native != null) return native.Name;

            var type = o as PyType;
            if (type != null) return type.QualName;

            var inst = o as PyInstance;
            if (inst != null && inst.Type != null) return inst.Type.QualName;

            return null;
        }

        // ---------------------------------------------------------------- helpers

        static IEnumerable<object> Items(object o)
        {
            var list = o as PyList;
            if (list != null) return list.Items;

            var tuple = o as PyTuple;
            if (tuple != null) return tuple.Items;

            var set = o as PySet;
            if (set != null) return set.Items;

            return System.Linq.Enumerable.Empty<object>();
        }

        static string Str(object o)
        {
            if (o == null) return null;
            string s = o as string;
            return s ?? PyOps.ToStr(o);
        }

        static bool Bool(object o) { return o != null && Py.Truthy(o); }

        static List<string> StrList(object o)
        {
            var rv = new List<string>();
            if (o == null) return rv;

            string single = o as string;
            if (single != null) { rv.Add(single); return rv; }

            foreach (var item in Items(o))
            {
                string s = Str(item);
                if (s != null) rv.Add(s);
            }
            return rv;
        }

        static string Join(object o)
        {
            var parts = StrList(o);
            return parts.Count == 0 ? null : string.Join(" ", parts.ToArray());
        }

        sealed class ReferenceComparer : IEqualityComparer<PyInstance>
        {
            public static readonly ReferenceComparer Instance = new ReferenceComparer();
            public bool Equals(PyInstance a, PyInstance b) { return ReferenceEquals(a, b); }
            public int GetHashCode(PyInstance o) { return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(o); }
        }
    }
}
