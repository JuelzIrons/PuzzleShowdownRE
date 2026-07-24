namespace Unity.Services.Qos.V2.Http
{
	[global::System.Serializable]
	internal class DeserializationException : global::System.Exception
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
