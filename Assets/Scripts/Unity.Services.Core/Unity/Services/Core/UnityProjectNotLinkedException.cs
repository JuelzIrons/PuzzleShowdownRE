namespace Unity.Services.Core
{
	internal class UnityProjectNotLinkedException : global::Unity.Services.Core.ServicesInitializationException
	{
		public UnityProjectNotLinkedException()
		{
		}

		public UnityProjectNotLinkedException(string message)
			: base(message)
		{
		}
	}
}
