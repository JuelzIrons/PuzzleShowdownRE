namespace Unity.Services.Multiplayer
{
	public class ReconnectSessionOptions
	{
		public string Type { get; set; } = global::System.Guid.NewGuid().ToString();

		internal global::Unity.Services.Multiplayer.INetworkHandler NetworkHandler { get; private set; }

		public global::Unity.Services.Multiplayer.ReconnectSessionOptions WithNetworkHandler(global::Unity.Services.Multiplayer.INetworkHandler networkHandler)
		{
			NetworkHandler = networkHandler;
			return this;
		}
	}
}
