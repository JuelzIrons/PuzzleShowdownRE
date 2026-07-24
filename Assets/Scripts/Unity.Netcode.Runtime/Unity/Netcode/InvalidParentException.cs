namespace Unity.Netcode
{
	public class InvalidParentException : global::System.Exception
	{
		public InvalidParentException()
		{
		}

		public InvalidParentException(string message)
			: base(message)
		{
		}

		public InvalidParentException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
