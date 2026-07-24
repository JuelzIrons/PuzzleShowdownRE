namespace Unity.Services.Authentication.PlayerAccounts
{
	public static class PlayerAccountService
	{
		private static global::Unity.Services.Authentication.PlayerAccounts.IPlayerAccountService s_Instance;

		public static global::Unity.Services.Authentication.PlayerAccounts.IPlayerAccountService Instance
		{
			get
			{
				if (s_Instance == null)
				{
					throw new global::Unity.Services.Core.ServicesInitializationException("Singleton is not initialized. Please call UnityServices.InitializeAsync() to initialize. Please make sure Player Accounts is configured in the Unity Editor Settings");
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
