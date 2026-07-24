namespace Unity.Services.DistributedAuthority
{
	internal static class DistributedAuthorityService
	{
		private const string k_InitializationErrorMessage = "Singleton is not initialized. Please call UnityServices.InitializeAsync() to initialize.";

		private static global::Unity.Services.DistributedAuthority.IDistributedAuthorityService s_Instance;

		public static global::Unity.Services.DistributedAuthority.IDistributedAuthorityService Instance
		{
			get
			{
				if (s_Instance == null)
				{
					throw new global::Unity.Services.Core.ServicesInitializationException("Singleton is not initialized. Please call UnityServices.InitializeAsync() to initialize.");
				}
				return s_Instance;
			}
			internal set
			{
				s_Instance = value;
			}
		}
	}
}
