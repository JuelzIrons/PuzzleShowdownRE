namespace Unity.Services.Multiplayer
{
	internal class NetworkModuleOptions : global::Unity.Services.Multiplayer.IModuleOption
	{
		public global::System.Type Type => typeof(global::Unity.Services.Multiplayer.NetworkModuleOptions);

		private global::Unity.Services.Multiplayer.NetworkOptions Options { get; }

		public NetworkModuleOptions(global::Unity.Services.Multiplayer.NetworkOptions options)
		{
			Options = options;
		}

		public void Process(global::Unity.Services.Multiplayer.SessionHandler session)
		{
			global::Unity.Services.Multiplayer.NetworkModule obj = session.GetModule<global::Unity.Services.Multiplayer.NetworkModule>() ?? throw new global::System.Exception("Trying to setup network in session but the module isn't registered.");
			Options.RelayProtocol = GetValidProtocol(Options.RelayProtocol);
			obj.NetworkOptions = Options;
			static global::Unity.Services.Multiplayer.RelayProtocol GetValidProtocol(global::Unity.Services.Multiplayer.RelayProtocol protocol)
			{
				if (global::Unity.Services.Relay.Models.AllocationUtils.IsValidProtocol(protocol))
				{
					return protocol;
				}
				global::Unity.Services.Multiplayer.Logger.LogWarning($"Invalid protocol \"{protocol:G}\" detected. Using default protocol \"{(global::Unity.Services.Multiplayer.RelayProtocol.DTLS):G}\" instead.");
				return global::Unity.Services.Multiplayer.RelayProtocol.DTLS;
			}
		}
	}
}
