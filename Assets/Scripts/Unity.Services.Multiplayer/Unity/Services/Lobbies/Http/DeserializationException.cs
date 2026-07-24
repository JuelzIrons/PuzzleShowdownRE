namespace Unity.Services.Lobbies.Http
{
	[global::System.Serializable]
	public class DeserializationException : global::System.Exception
	{
		public DeserializationException()
		{
		}

		public DeserializationException(string message)
			: base(message)
		{
		}

		private DeserializationException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}
