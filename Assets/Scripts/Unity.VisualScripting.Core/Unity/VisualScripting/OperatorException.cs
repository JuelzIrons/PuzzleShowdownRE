namespace Unity.VisualScripting
{
	public abstract class OperatorException : global::System.InvalidCastException
	{
		protected OperatorException()
		{
		}

		protected OperatorException(string message)
			: base(message)
		{
		}

		protected OperatorException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
