namespace Unity.VisualScripting.Antlr3.Runtime
{
	public class UnwantedTokenException : global::Unity.VisualScripting.Antlr3.Runtime.MismatchedTokenException
	{
		public global::Unity.VisualScripting.Antlr3.Runtime.IToken UnexpectedToken => token;

		public UnwantedTokenException()
		{
		}

		public UnwantedTokenException(int expecting, global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
			: base(expecting, input)
		{
		}

		public override string ToString()
		{
			string text = ", expected " + base.Expecting;
			if (base.Expecting == 0)
			{
				text = "";
			}
			if (token == null)
			{
				return "UnwantedTokenException(found=" + text + ")";
			}
			return "UnwantedTokenException(found=" + token.Text + text + ")";
		}
	}
}
