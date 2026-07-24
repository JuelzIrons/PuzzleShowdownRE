namespace Unity.Services.Multiplayer
{
	public class RelayNetworkOptions
	{
		[global::System.Obsolete("This value was moved to the SessionOptions.WithNetworkOptions() API.")]
		public global::Unity.Services.Multiplayer.RelayProtocol Protocol { get; } = global::Unity.Services.Multiplayer.RelayProtocol.DTLS;

		public string Region { get; }

		public bool PreserveRegion { get; }

		public static global::Unity.Services.Multiplayer.RelayNetworkOptions Default => new global::Unity.Services.Multiplayer.RelayNetworkOptions();

		[global::System.Obsolete("The RelayProtocol value should be se through SessionOptions.WithNetworkOptions() API.")]
		public RelayNetworkOptions(global::Unity.Services.Multiplayer.RelayProtocol protocol)
			: this(protocol, null, preserveRegion: false)
		{
		}

		[global::System.Obsolete("The RelayProtocol value should be se through SessionOptions.WithNetworkOptions() API.")]
		public RelayNetworkOptions(global::Unity.Services.Multiplayer.RelayProtocol protocol, string region)
			: this(protocol, region, preserveRegion: false)
		{
		}

		[global::System.Obsolete("The RelayProtocol value should be set through SessionOptions.WithNetworkOptions() API.")]
		public RelayNetworkOptions(global::Unity.Services.Multiplayer.RelayProtocol protocol, string region, bool preserveRegion)
		{
			Protocol = GetValidProtocol(protocol);
			Region = region;
			PreserveRegion = preserveRegion;
			static global::Unity.Services.Multiplayer.RelayProtocol GetValidProtocol(global::Unity.Services.Multiplayer.RelayProtocol relayProtocol)
			{
				if (global::Unity.Services.Relay.Models.AllocationUtils.IsValidProtocol(relayProtocol))
				{
					return relayProtocol;
				}
				global::Unity.Services.Multiplayer.Logger.LogWarning($"Invalid protocol \"{relayProtocol:G}\" detected. Using default protocol \"{(global::Unity.Services.Multiplayer.RelayProtocol.DTLS):G}\" instead.");
				return global::Unity.Services.Multiplayer.RelayProtocol.DTLS;
			}
		}

		public RelayNetworkOptions(string region = null, bool preserveRegion = false)
		{
			Region = region;
			PreserveRegion = preserveRegion;
		}
	}
}
