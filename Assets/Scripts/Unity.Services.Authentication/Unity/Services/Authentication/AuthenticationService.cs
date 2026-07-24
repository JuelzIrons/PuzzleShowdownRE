namespace Unity.Services.Authentication
{
	public static class AuthenticationService
	{
		private static global::Unity.Services.Authentication.IAuthenticationService s_Instance;

		public static global::Unity.Services.Authentication.IAuthenticationService Instance
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
