namespace Unity.Services.Core.Internal
{
	internal class HashException : global::System.Exception
	{
		public int Hash { get; }

		public HashException(int hash)
		{
			Hash = hash;
		}

		public HashException(int hash, string message)
		{
			Hash = hash;
		}

		public HashException(int hash, string message, global::System.Exception inner)
			: base(message, inner)
		{
			Hash = hash;
		}
	}
}
