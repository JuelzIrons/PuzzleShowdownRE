namespace Unity.Multiplayer.Widgets
{
	public abstract class CustomWidgetsNetworkHandler : global::UnityEngine.ScriptableObject, global::Unity.Services.Multiplayer.INetworkHandler
	{
		public abstract global::System.Threading.Tasks.Task StartAsync(global::Unity.Services.Multiplayer.NetworkConfiguration configuration);

		public abstract global::System.Threading.Tasks.Task StopAsync();
	}
}
