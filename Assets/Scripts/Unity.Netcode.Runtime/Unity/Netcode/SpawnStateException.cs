namespace Unity.Netcode
{
	public class SpawnStateException : global::System.Exception
	{
		public SpawnStateException()
		{
		}

		public SpawnStateException(string message)
			: base(message)
		{
		}

		public SpawnStateException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}
