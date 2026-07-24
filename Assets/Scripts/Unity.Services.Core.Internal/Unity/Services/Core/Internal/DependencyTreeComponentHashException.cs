namespace Unity.Services.Core.Internal
{
	internal class DependencyTreeComponentHashException : global::Unity.Services.Core.Internal.HashException
	{
		public DependencyTreeComponentHashException(int hash)
			: base(hash)
		{
		}

		public DependencyTreeComponentHashException(int hash, string message)
			: base(hash, message)
		{
		}

		public DependencyTreeComponentHashException(int hash, string message, global::System.Exception inner)
			: base(hash, message, inner)
		{
		}
	}
}
