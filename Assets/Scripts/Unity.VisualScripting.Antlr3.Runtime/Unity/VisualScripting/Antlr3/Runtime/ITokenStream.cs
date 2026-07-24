namespace Unity.VisualScripting.Antlr3.Runtime
{
	public interface ITokenStream : global::Unity.VisualScripting.Antlr3.Runtime.IIntStream
	{
		global::Unity.VisualScripting.Antlr3.Runtime.ITokenSource TokenSource { get; }

		global::Unity.VisualScripting.Antlr3.Runtime.IToken LT(int k);

		global::Unity.VisualScripting.Antlr3.Runtime.IToken Get(int i);

		string ToString(int start, int stop);

		string ToString(global::Unity.VisualScripting.Antlr3.Runtime.IToken start, global::Unity.VisualScripting.Antlr3.Runtime.IToken stop);
	}
}
