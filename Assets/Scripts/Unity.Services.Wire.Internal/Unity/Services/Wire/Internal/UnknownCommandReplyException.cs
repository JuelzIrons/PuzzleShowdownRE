namespace Unity.Services.Wire.Internal
{
	internal class UnknownCommandReplyException : global::System.Exception
	{
		public UnknownCommandReplyException(uint id)
			: base($"Received a command reply with unknown id: {id}")
		{
		}
	}
}
