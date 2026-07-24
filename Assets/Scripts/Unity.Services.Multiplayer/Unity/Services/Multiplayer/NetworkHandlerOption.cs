namespace Unity.Services.Multiplayer
{
	internal class NetworkHandlerOption : global::Unity.Services.Multiplayer.IModuleOption
	{
		public global::System.Type Type => typeof(global::Unity.Services.Multiplayer.NetworkModule);

		private global::Unity.Services.Multiplayer.INetworkHandler NetworkHandler { get; }

		public NetworkHandlerOption(global::Unity.Services.Multiplayer.INetworkHandler networkHandler)
		{
			NetworkHandler = networkHandler;
		}

		public void Process(global::Unity.Services.Multiplayer.SessionHandler session)
		{
			(session.GetModule<global::Unity.Services.Multiplayer.NetworkModule>() ?? throw new global::System.Exception("Trying to setup session network but the module isn't registered.")).NetworkHandler = NetworkHandler;
		}
	}
}
