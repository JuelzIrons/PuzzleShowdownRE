namespace Unity.Services.Multiplayer
{
	internal class NetworkInfoOption : global::Unity.Services.Multiplayer.IModuleOption
	{
		public global::System.Type Type => typeof(global::Unity.Services.Multiplayer.NetworkModule);

		internal global::Unity.Services.Multiplayer.NetworkInfo Options { get; }

		public NetworkInfoOption(global::Unity.Services.Multiplayer.NetworkInfo options)
		{
			Options = options;
		}

		public void Process(global::Unity.Services.Multiplayer.SessionHandler session)
		{
			(session.GetModule<global::Unity.Services.Multiplayer.NetworkModule>() ?? throw new global::System.Exception("Trying to setup network in session but the module isn't registered.")).NetworkInfo = Options;
		}
	}
}
