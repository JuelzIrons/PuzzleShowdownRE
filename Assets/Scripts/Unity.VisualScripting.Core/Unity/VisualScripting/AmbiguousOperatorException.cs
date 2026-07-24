namespace Unity.VisualScripting
{
	public sealed class AmbiguousOperatorException : global::Unity.VisualScripting.OperatorException
	{
		public AmbiguousOperatorException(string symbol, global::System.Type leftType, global::System.Type rightType)
			: base("Ambiguous use of operator '" + symbol + "' between types '" + (leftType?.ToString() ?? "null") + "' and '" + (rightType?.ToString() ?? "null") + "'.")
		{
		}
	}
}
