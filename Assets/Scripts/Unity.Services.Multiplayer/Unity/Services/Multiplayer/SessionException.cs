namespace Unity.Services.Multiplayer
{
	public class SessionException : global::System.Exception
	{
		public global::Unity.Services.Multiplayer.SessionError Error { get; private set; }

		internal SessionException(string message, global::Unity.Services.Multiplayer.SessionError error)
			: base(message)
		{
			Error = error;
		}

		public override string ToString()
		{
			return $"SessionException: [Error: {Error}] [Message: {Message}]";
		}
	}
}
