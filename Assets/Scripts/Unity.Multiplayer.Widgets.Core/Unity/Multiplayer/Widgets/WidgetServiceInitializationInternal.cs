namespace Unity.Multiplayer.Widgets
{
	internal class WidgetServiceInitializationInternal : global::Unity.Multiplayer.Widgets.IServiceInitialization
	{
		public async global::System.Threading.Tasks.Task InitializeAsync()
		{
			global::Unity.Multiplayer.Widgets.MultiplayerWidgetsSettings multiplayerWidgetsSettings = global::UnityEngine.Resources.Load<global::Unity.Multiplayer.Widgets.MultiplayerWidgetsSettings>("MultiplayerWidgetsSettings");
			if (!(multiplayerWidgetsSettings != null) || !multiplayerWidgetsSettings.UseCustomServiceInitialization)
			{
				global::Unity.Multiplayer.Widgets.WidgetDependencies widgetDependencies = global::Unity.Multiplayer.Widgets.WidgetDependencies.Instance;
				if (global::Unity.Services.Core.UnityServices.State != global::Unity.Services.Core.ServicesInitializationState.Initialized)
				{
					await global::Unity.Services.Core.UnityServices.InitializeAsync();
					global::UnityEngine.Debug.Log("Initialized Unity Services");
				}
				if (!widgetDependencies.AuthenticationService.IsSignedIn)
				{
					await widgetDependencies.AuthenticationService.SignInAnonymouslyAsync();
					global::UnityEngine.Debug.Log("Signed in anonymously. Name: " + await widgetDependencies.AuthenticationService.GetPlayerNameAsync() + ". ID: " + widgetDependencies.AuthenticationService.PlayerId);
				}
				global::Unity.Multiplayer.Widgets.IChatService chatService = global::Unity.Multiplayer.Widgets.WidgetDependencies.Instance.ChatService;
				if (chatService != null)
				{
					await chatService.InitializeAsync();
				}
				global::Unity.Multiplayer.Widgets.WidgetServiceInitialization.ServicesInitialized();
			}
		}
	}
}
