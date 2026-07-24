namespace Unity.VisualScripting
{
	public sealed class GenericClosingException : global::System.Exception
	{
		public GenericClosingException(string message)
			: base(message)
		{
		}

		public GenericClosingException(global::System.Type open, global::System.Type closed)
			: base($"Open-constructed type '{open}' is not assignable from closed-constructed type '{closed}'.")
		{
		}
	}
}
