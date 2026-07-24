namespace Unity.Services.Authentication.PlayerAccounts
{
	internal class PlayerAccountsPackageInitializer : global::Unity.Services.Core.Internal.IInitializablePackageV2, global::Unity.Services.Core.Internal.IInitializablePackage
	{
		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeOnLoad()
		{
			new global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsPackageInitializer().Register(global::Unity.Services.Core.Internal.CorePackageRegistry.Instance);
		}

		public void Register(global::Unity.Services.Core.Internal.CorePackageRegistry registry)
		{
			registry.Register(this).DependsOn<global::Unity.Services.Core.Configuration.Internal.ICloudProjectId>();
		}

		public global::System.Threading.Tasks.Task Initialize(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountService.Instance = InitializeService(registry);
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		public global::System.Threading.Tasks.Task InitializeInstanceAsync(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			InitializeService(registry);
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		private global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountServiceInternal InitializeService(global::Unity.Services.Core.Internal.CoreRegistry registry)
		{
			global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings unityPlayerAccountSettings = global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings.Load();
			if ((object)unityPlayerAccountSettings == null)
			{
				return null;
			}
			global::Unity.Services.Authentication.PlayerAccounts.NetworkHandler networkingClient = new global::Unity.Services.Authentication.PlayerAccounts.NetworkHandler();
			global::Unity.Services.Authentication.PlayerAccounts.JwtDecoder jwtDecoder = new global::Unity.Services.Authentication.PlayerAccounts.JwtDecoder(new global::Unity.Services.Authentication.PlayerAccounts.DateTimeWrapper());
			global::Unity.Services.Core.Configuration.Internal.ICloudProjectId serviceComponent = registry.GetServiceComponent<global::Unity.Services.Core.Configuration.Internal.ICloudProjectId>();
			global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountServiceInternal playerAccountServiceInternal = new global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountServiceInternal(unityPlayerAccountSettings, serviceComponent, jwtDecoder, networkingClient);
			registry.RegisterService((global::Unity.Services.Authentication.PlayerAccounts.IPlayerAccountService)playerAccountServiceInternal);
			return playerAccountServiceInternal;
		}
	}
}
