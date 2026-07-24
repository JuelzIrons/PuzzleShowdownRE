namespace Unity.VisualScripting
{
	public sealed class InvalidOperatorException : global::Unity.VisualScripting.OperatorException
	{
		public InvalidOperatorException(string symbol, global::System.Type type)
			: base("Operator '" + symbol + "' cannot be applied to operand of type '" + (type?.ToString() ?? "null") + "'.")
		{
		}

		public InvalidOperatorException(string symbol, global::System.Type leftType, global::System.Type rightType)
			: base("Operator '" + symbol + "' cannot be applied to operands of type '" + (leftType?.ToString() ?? "null") + "' and '" + (rightType?.ToString() ?? "null") + "'.")
		{
		}
	}
}
