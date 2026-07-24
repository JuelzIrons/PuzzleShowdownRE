namespace Unity.Services.Multiplayer
{
	public interface INetworkHandler
	{
		global::System.Threading.Tasks.Task StartAsync(global::Unity.Services.Multiplayer.NetworkConfiguration configuration);

		global::System.Threading.Tasks.Task StopAsync();
	}
}
