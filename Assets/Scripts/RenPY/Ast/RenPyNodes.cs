using System;
using System.Collections.Generic;
using RenPy.Python;

namespace RenPy.Ast
{
    /// <summary>
    /// Base for every Ren'Py script statement. Nodes form a linked list through
    /// <see cref="Next"/>, exactly as Ren'Py chains them, so execution is a walk
    /// rather than an index into a block.
    /// </summary>
    public abstract class Node
    {
        /// <summary>Unique node identity from the pickle; used for save/rollback.</summary>
        public object Name;
        public string Filename;
        public int LineNumber;
        public Node Next;

        public virtual string Kind { get { return GetType().Name; } }

        public override string ToString()
        {
            return Kind + " (" + Filename + ":" + LineNumber + ")";
        }
    }

    // ---------------------------------------------------------------- structure

    public sealed class InitNode : Node
    {
        public int Priority;
        public List<Node> Block = new List<Node>();
    }

    public sealed class LabelNode : Node
    {
        public string LabelName;
        public List<Node> Block = new List<Node>();
        public ParameterInfo Parameters;
        public bool Hide;
    }

    public sealed class PassNode : Node { }

    // ---------------------------------------------------------------- python

    /// <summary>
    /// A block of embedded Python. <see cref="PyCodeRef"/> holds the source text;
    /// the interpreter parses it on first execution and caches the result.
    /// </summary>
    public sealed class PythonNode : Node
    {
        public PyCodeRef Code;
        public string Store = "store";
        public bool Hide;
    }

    /// <summary>Same as <see cref="PythonNode"/> but runs during the early init pass.</summary>
    public sealed class EarlyPythonNode : Node
    {
        public PyCodeRef Code;
        public string Store = "store";
        public bool Hide;
    }

    /// <summary>
    /// A reference to a chunk of Python source pulled out of a .rpyc. Mode is
    /// "exec" for statement blocks or "eval" for single expressions.
    /// </summary>
    public sealed class PyCodeRef
    {
        public string Source = "";
        public string Mode = "exec";
        public string Filename = "";
        public int LineNumber;

        /// <summary>Parsed form, filled in lazily by the interpreter.</summary>
        public object Compiled;

        public override string ToString()
        {
            string s = Source.Length > 60 ? Source.Substring(0, 60) + "..." : Source;
            return "<" + Mode + " " + s.Replace("\n", "\\n") + ">";
        }
    }

    public sealed class DefineNode : Node
    {
        public string VarName;
        public string Store = "store";
        public string Operator = "=";
        public object Index;
        public PyCodeRef Code;
    }

    public sealed class DefaultNode : Node
    {
        public string VarName;
        public string Store = "store";
        public PyCodeRef Code;
    }

    // ---------------------------------------------------------------- display

    /// <summary>
    /// An image specifier: the parsed form of `show eileen happy at right behind bg`.
    /// </summary>
    public sealed class ImSpec
    {
        public List<string> Name = new List<string>();
        /// <summary>Set when the statement was `show expression ...`.</summary>
        public string Expression;
        public string Tag;
        public List<string> AtList = new List<string>();
        public string Layer;
        public string ZOrder;
        public List<string> Behind = new List<string>();

        /// <summary>The tag this spec occupies on its layer.</summary>
        public string EffectiveTag
        {
            get
            {
                if (!string.IsNullOrEmpty(Tag)) return Tag;
                return Name.Count > 0 ? Name[0] : null;
            }
        }

        public string FullName { get { return string.Join(" ", Name.ToArray()); } }
    }

    public sealed class ShowNode : Node
    {
        public ImSpec Spec;
        public RawBlock Atl;
    }

    public sealed class SceneNode : Node
    {
        /// <summary>Null for a bare `scene` that only clears the layer.</summary>
        public ImSpec Spec;
        public string Layer;
        public RawBlock Atl;
    }

    public sealed class HideNode : Node
    {
        public ImSpec Spec;
    }

    public sealed class ShowLayerNode : Node
    {
        public string Layer;
        public RawBlock Atl;
        public List<string> AtList = new List<string>();
    }

    public sealed class CameraNode : Node
    {
        public string Layer;
        public RawBlock Atl;
        public List<string> AtList = new List<string>();
    }

    public sealed class WithNode : Node
    {
        public string Expr;
        /// <summary>Set on the opening half of a paired `with` around a scene change.</summary>
        public string Paired;
    }

    public sealed class ImageNode : Node
    {
        public List<string> ImgName = new List<string>();
        public PyCodeRef Code;
        public RawBlock Atl;

        public string FullName { get { return string.Join(" ", ImgName.ToArray()); } }
    }

    public sealed class TransformNode : Node
    {
        public string VarName;
        public string Store = "store";
        public RawBlock Atl;
        public ParameterInfo Parameters;
    }

    // ---------------------------------------------------------------- dialogue

    public sealed class SayNode : Node
    {
        /// <summary>Speaker expression, or null for narration.</summary>
        public string Who;
        public string What;
        public string With;
        public bool Interact = true;
        public ArgumentInfo Arguments;
        public string Attributes;
        public string TemporaryAttributes;
        public string Identifier;
    }

    public sealed class MenuItem
    {
        public string Label;
        public string Condition = "True";
        /// <summary>Null for a caption line rather than a choice.</summary>
        public List<Node> Block;
        public ArgumentInfo Arguments;
    }

    public sealed class MenuNode : Node
    {
        public List<MenuItem> Items = new List<MenuItem>();
        /// <summary>`menu set` expression: a set tracking already-taken choices.</summary>
        public string Set;
        public string With;
        public bool HasCaption;
        public ArgumentInfo Arguments;
    }

    // ---------------------------------------------------------------- control flow

    public sealed class JumpNode : Node
    {
        public string Target;
        /// <summary>True when the target is an expression to evaluate, not a literal.</summary>
        public bool IsExpression;
    }

    public sealed class CallNode : Node
    {
        public string Label;
        public bool IsExpression;
        public ArgumentInfo Arguments;
        public string From;
    }

    public sealed class ReturnNode : Node
    {
        public string Expression;
    }

    public sealed class IfEntry
    {
        /// <summary>The `else` branch has condition "True".</summary>
        public string Condition = "True";
        public List<Node> Block = new List<Node>();
    }

    public sealed class IfNode : Node
    {
        public List<IfEntry> Entries = new List<IfEntry>();
    }

    public sealed class WhileNode : Node
    {
        public string Condition = "True";
        public List<Node> Block = new List<Node>();
    }

    // ---------------------------------------------------------------- misc

    public sealed class ScreenNode : Node
    {
        public SLScreen Screen;
    }

    public sealed class StyleNode : Node
    {
        public string StyleName;
        public string Parent;
        public PyDict Properties = new PyDict();
        public bool Clear;
        public string Take;
        public List<string> DelAttr = new List<string>();
        public string Variant;
    }

    /// <summary>
    /// A creator-defined statement. Ren'Py re-parses <see cref="Line"/> at runtime
    /// with the parser registered by the game's init code.
    /// </summary>
    public sealed class UserStatementNode : Node
    {
        public string Line = "";
        public object Parsed;
        public List<Node> Block = new List<Node>();
        public List<Node> CodeBlock;
        public bool Translatable;
        public string Rollback = "normal";

        /// <summary>The leading keyword, which selects the registered handler.</summary>
        public string Keyword
        {
            get
            {
                int i = Line.IndexOf(' ');
                return i < 0 ? Line : Line.Substring(0, i);
            }
        }
    }

    public sealed class TranslateNode : Node
    {
        public string Identifier;
        public string Language;
        public List<Node> Block = new List<Node>();
    }

    public sealed class EndTranslateNode : Node { }

    public sealed class TranslateStringNode : Node
    {
        public string Language;
        public string Old;
        public string New;
    }

    /// <summary>A node kind we recognise but do not execute.</summary>
    public sealed class UnknownNode : Node
    {
        public string OriginalType = "";
        public override string Kind { get { return "Unknown<" + OriginalType + ">"; } }
    }

    // ---------------------------------------------------------------- signatures

    public sealed class Parameter
    {
        public string Name;
        /// <summary>Default value expression, or null when required.</summary>
        public string Default;
    }

    public sealed class ParameterInfo
    {
        public List<Parameter> Parameters = new List<Parameter>();
        public List<string> PositionalOnly = new List<string>();
        public List<string> KeywordOnly = new List<string>();
        /// <summary>Name bound by `*args`, if any.</summary>
        public string ExtraPos;
        /// <summary>Name bound by `**kwargs`, if any.</summary>
        public string ExtraKw;
    }

    public sealed class ArgumentInfo
    {
        /// <summary>(name, expression) pairs; name is null for positional arguments.</summary>
        public List<KeyValuePair<string, string>> Arguments = new List<KeyValuePair<string, string>>();
        public string ExtraPos;
        public string ExtraKw;
    }

    // ---------------------------------------------------------------- ATL

    public abstract class RawStatement
    {
        public string Filename;
        public int LineNumber;
    }

    public sealed class RawBlock : RawStatement
    {
        public List<RawStatement> Statements = new List<RawStatement>();
        /// <summary>True for `image ... :` animation blocks, which loop by default.</summary>
        public bool Animation;
    }

    /// <summary>
    /// The workhorse ATL statement: an optional warper and duration plus any number
    /// of transform property assignments, e.g. `linear 0.5 xpos 100 alpha 1.0`.
    /// </summary>
    public sealed class RawMultipurpose : RawStatement
    {
        public string Warper;
        public string Duration;
        public string WarpFunction;
        /// <summary>Bare expressions: a child displayable or a named transform to apply.</summary>
        public List<KeyValuePair<string, string>> Expressions = new List<KeyValuePair<string, string>>();
        /// <summary>Transform property name to value expression.</summary>
        public List<KeyValuePair<string, string>> Properties = new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, List<string>>> Splines = new List<KeyValuePair<string, List<string>>>();
        public string Revolution;
        public string Circles;
    }

    public sealed class RawParallel : RawStatement
    {
        public List<RawBlock> Blocks = new List<RawBlock>();
    }

    public sealed class RawChoice : RawStatement
    {
        /// <summary>(weight expression, block) pairs.</summary>
        public List<KeyValuePair<string, RawBlock>> Choices = new List<KeyValuePair<string, RawBlock>>();
    }

    public sealed class RawRepeat : RawStatement
    {
        /// <summary>Repeat count expression; null means forever.</summary>
        public string Repeats;
    }

    public sealed class RawTime : RawStatement
    {
        public string Time;
    }

    public sealed class RawOn : RawStatement
    {
        public Dictionary<string, RawBlock> Handlers = new Dictionary<string, RawBlock>();
    }

    public sealed class RawEvent : RawStatement
    {
        public string EventName;
    }

    public sealed class RawFunction : RawStatement
    {
        public string Expr;
    }

    public sealed class RawContainsExpr : RawStatement
    {
        public string Expression;
    }

    public sealed class RawChild : RawStatement
    {
        public List<RawBlock> Children = new List<RawBlock>();
    }

    // ---------------------------------------------------------------- screen language

    public abstract class SLNode
    {
        public string Filename;
        public int LineNumber;
        public int Serial;
    }

    /// <summary>A node with children and `key value` keyword properties.</summary>
    public class SLBlock : SLNode
    {
        public List<SLNode> Children = new List<SLNode>();
        public List<KeyValuePair<string, string>> Keyword = new List<KeyValuePair<string, string>>();
    }

    public sealed class SLScreen : SLBlock
    {
        public string ScreenName;
        public ParameterInfo Parameters;
        public string Tag;
        public string Layer;
        public string Modal;
        public string ZOrder;
        public string Variant;
        public string Predict;
        public string Sensitive;
        public string RollForward;
    }

    /// <summary>
    /// A displayable statement such as `text`, `vbox`, `imagebutton`. The pickled
    /// `displayable` field names the Ren'Py factory; we map it by name at build time.
    /// </summary>
    public sealed class SLDisplayable : SLBlock
    {
        /// <summary>The Ren'Py displayable factory, e.g. "renpy.text.text.Text".</summary>
        public string DisplayableName;
        /// <summary>The screen-language statement name, e.g. "text" or "vbox".</summary>
        public string StatementName;
        public List<string> Positional = new List<string>();
        public string Style;
        public bool ChildOrFixed;
        public bool Scope;
        public bool ReplacesParameter;
        public string Variable;
        public bool Imagemap;
        public bool Hotspot;
        public List<KeyValuePair<string, string>> DefaultKeywords = new List<KeyValuePair<string, string>>();
    }

    public sealed class SLIfEntry
    {
        /// <summary>Null condition marks the `else` branch.</summary>
        public string Condition;
        public SLBlock Block;
    }

    public sealed class SLIf : SLNode
    {
        public List<SLIfEntry> Entries = new List<SLIfEntry>();
        /// <summary>True for `showif`, which keeps children alive but hidden.</summary>
        public bool ShowIf;
    }

    public sealed class SLFor : SLBlock
    {
        public string Variable;
        public string Expression;
        public string IndexExpression;
    }

    public sealed class SLPython : SLNode
    {
        public PyCodeRef Code;
    }

    public sealed class SLPass : SLNode { }

    public sealed class SLDefault : SLNode
    {
        public string Variable;
        public string Expression;
    }

    public sealed class SLUse : SLNode
    {
        public string Target;
        public ArgumentInfo Args;
        public SLBlock Block;
        public string Id;
        /// <summary>Set when `use` targets a screen defined in the same file.</summary>
        public SLScreen Ast;
    }

    public sealed class SLTransclude : SLNode { }

    public sealed class SLCustomUse : SLNode
    {
        public string Target;
        public List<string> Positional = new List<string>();
        public SLBlock Block;
        public SLScreen Ast;
    }
}
