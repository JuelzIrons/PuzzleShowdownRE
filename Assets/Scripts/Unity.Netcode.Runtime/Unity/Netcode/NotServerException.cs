namespace Unity.Netcode
{
	public class NotServerException : global::System.Exception
	{
		public NotServerException()
		{
		}

		public NotServerException(string message)
			: base(message)
		{
		}

		public NotServerException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}
