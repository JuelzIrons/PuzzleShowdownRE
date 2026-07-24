namespace Unity.Services.Multiplayer
{
	public static class SessionOptionsExtensions
	{
		private static readonly global::System.TimeSpan DefaultDataUploadInterval = global::System.TimeSpan.FromSeconds(5.0);

		private static readonly global::System.TimeSpan DefaultDataHandlingTimeout = global::System.TimeSpan.FromSeconds(3.0);

		internal static T WithMatchmaker<T>(this T options) where T : global::Unity.Services.Multiplayer.SessionOptions
		{
			return options.WithOption(new global::Unity.Services.Multiplayer.EnableMatchmakerModuleOption());
		}

		public static T WithRelayNetwork<T>(this T options, string region = null) where T : global::Unity.Services.Multiplayer.SessionOptions
		{
			return options.WithRelayNetwork(new global::Unity.Services.Multiplayer.RelayNetworkOptions(region));
		}

		public static T WithRelayNetwork<T>(this T options, global::Unity.Services.Multiplayer.RelayNetworkOptions relayOptions) where T : global::Unity.Services.Multiplayer.SessionOptions
		{
			return options.WithOption(new global::Unity.Services.Multiplayer.NetworkInfoOption(global::Unity.Services.Multiplayer.NetworkInfo.BuildRelay(relayOptions)));
		}

		public static T WithDirectNetwork<T>(this T options, string listenIp = "127.0.0.1", string publishIp = "127.0.0.1", int port = 0) where T : global::Unity.Services.Multiplayer.SessionOptions
		{
			return options.WithDirectNetwork(new global::Unity.Services.Multiplayer.DirectNetworkOptions(new global::Unity.Services.Multiplayer.ListenIPAddress(listenIp), new global::Unity.Services.Multiplayer.PublishIPAddress(publishIp), (ushort)port));
		}

		public static T WithDirectNetwork<T>(this T options) where T : global::Unity.Services.Multiplayer.SessionOptions
		{
			return options.WithOption(new global::Unity.Services.Multiplayer.NetworkInfoOption(global::Unity.Services.Multiplayer.NetworkInfo.BuildDirect(new global::Unity.Services.Multiplayer.DirectNetworkOptions())));
		}

		public static T WithDirectNetwork<T>(this T options, global::Unity.Services.Multiplayer.DirectNetworkOptions networkOptions) where T : global::Unity.Services.Multiplayer.SessionOptions
		{
			return options.WithOption(new global::Unity.Services.Multiplayer.NetworkInfoOption(global::Unity.Services.Multiplayer.NetworkInfo.BuildDirect(networkOptions)));
		}

		public static T WithNetworkHandler<T>(this T options, global::Unity.Services.Multiplayer.INetworkHandler networkHandler) where T : global::Unity.Services.Multiplayer.BaseSessionOptions
		{
			return options.WithOption(new global::Unity.Services.Multiplayer.NetworkHandlerOption(networkHandler));
		}

		public static T WithNetworkOptions<T>(this T options, global::Unity.Services.Multiplayer.NetworkOptions networkOptions) where T : global::Unity.Services.Multiplayer.BaseSessionOptions
		{
			return options.WithOption(new global::Unity.Services.Multiplayer.NetworkModuleOptions(networkOptions));
		}

		public static T WithDistributedAuthorityNetwork<T>(this T options, string region = null) where T : global::Unity.Services.Multiplayer.SessionOptions
		{
			return options.WithDistributedAuthorityNetwork(new global::Unity.Services.Multiplayer.RelayNetworkOptions(region));
		}

		public static T WithDistributedAuthorityNetwork<T>(this T options, global::Unity.Services.Multiplayer.RelayNetworkOptions relayOptions) where T : global::Unity.Services.Multiplayer.SessionOptions
		{
			return options.WithOption(new global::Unity.Services.Multiplayer.NetworkInfoOption(global::Unity.Services.Multiplayer.NetworkInfo.BuildDistributed(relayOptions)));
		}

		public static T WithPlayerName<T>(this T options, global::Unity.Services.Multiplayer.VisibilityPropertyOptions visibility = global::Unity.Services.Multiplayer.VisibilityPropertyOptions.Member) where T : global::Unity.Services.Multiplayer.BaseSessionOptions
		{
			return options.WithOption(new global::Unity.Services.Multiplayer.PlayerNameSessionOption(visibility));
		}

		public static string GetPlayerName(this global::Unity.Services.Multiplayer.IReadOnlyPlayer player)
		{
			global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> properties = player.Properties;
			if (properties != null && properties.ContainsKey("_player_name"))
			{
				return player.Properties["_player_name"].Value;
			}
			return null;
		}

		public static T WithHostMigration<T>(this T options, global::Unity.Services.Multiplayer.IMigrationDataHandler migrationDataHandler) where T : global::Unity.Services.Multiplayer.BaseSessionOptions
		{
			return options.WithHostMigration(migrationDataHandler, DefaultDataUploadInterval, DefaultDataHandlingTimeout);
		}

		public static T WithHostMigration<T>(this T options, global::Unity.Services.Multiplayer.IMigrationDataHandler migrationDataHandler, global::System.TimeSpan dataUploadInterval, global::System.TimeSpan dataHandlingTimeout) where T : global::Unity.Services.Multiplayer.BaseSessionOptions
		{
			if (migrationDataHandler == null)
			{
				throw new global::System.ArgumentNullException("migrationDataHandler", "migrationDataHandler is a required property for WithHostMigration and cannot be null.");
			}
			if (dataUploadInterval < global::System.TimeSpan.FromSeconds(1.0))
			{
				throw new global::System.ArgumentOutOfRangeException("dataUploadInterval", "dataUploadInterval cannot be lower than 1 second.");
			}
			return options.WithOption(new global::Unity.Services.Multiplayer.HostMigrationOption
			{
				DataHandler = migrationDataHandler,
				DataUploadInterval = dataUploadInterval,
				DataHandlingTimeout = dataHandlingTimeout
			});
		}

		internal static T WithOption<T, U>(this T options, U moduleOptions) where T : global::Unity.Services.Multiplayer.BaseSessionOptions where U : global::Unity.Services.Multiplayer.IModuleOption
		{
			global::System.Type typeFromHandle = typeof(U);
			if (!options.Options.TryAdd(typeFromHandle, moduleOptions))
			{
				throw new global::System.Exception(typeFromHandle.Name + " Option is already included");
			}
			return options;
		}

		internal static T WithOptionAddOrUpdate<T, U>(this T options, U moduleOptions) where T : global::Unity.Services.Multiplayer.BaseSessionOptions where U : global::Unity.Services.Multiplayer.IModuleOption
		{
			options.Options[typeof(U)] = moduleOptions;
			return options;
		}
	}
}
