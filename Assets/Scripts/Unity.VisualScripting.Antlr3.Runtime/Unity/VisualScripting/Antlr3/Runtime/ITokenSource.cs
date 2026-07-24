namespace Unity.VisualScripting.Antlr3.Runtime
{
	public interface ITokenSource
	{
		string SourceName { get; }

		global::Unity.VisualScripting.Antlr3.Runtime.IToken NextToken();
	}
}
