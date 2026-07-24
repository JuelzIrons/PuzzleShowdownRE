namespace Unity.VisualScripting.Dependencies.NCalc
{
	public sealed class EvaluationException : global::System.ApplicationException
	{
		public EvaluationException(string message)
			: base(message)
		{
		}

		public EvaluationException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
