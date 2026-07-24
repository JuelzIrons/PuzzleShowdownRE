namespace Unity.Services.Core.Internal
{
	internal class DependencyTreePackageHashException : global::Unity.Services.Core.Internal.HashException
	{
		public DependencyTreePackageHashException(int hash)
			: base(hash)
		{
		}

		public DependencyTreePackageHashException(int hash, string message)
			: base(hash, message)
		{
		}

		public DependencyTreePackageHashException(int hash, string message, global::System.Exception inner)
			: base(hash, message, inner)
		{
		}
	}
}
