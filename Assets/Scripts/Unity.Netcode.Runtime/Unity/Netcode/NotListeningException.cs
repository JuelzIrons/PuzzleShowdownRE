namespace Unity.Netcode
{
	public class NotListeningException : global::System.Exception
	{
		public NotListeningException()
		{
		}

		public NotListeningException(string message)
			: base(message)
		{
		}

		public NotListeningException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}
