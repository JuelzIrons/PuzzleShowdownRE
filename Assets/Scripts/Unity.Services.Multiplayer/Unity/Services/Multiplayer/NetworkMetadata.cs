namespace Unity.Services.Multiplayer
{
	internal class NetworkMetadata
	{
		private const string k_EnclosingTypeName = "NetworkMetadata";

		public global::Unity.Services.Multiplayer.NetworkType Network { get; set; }

		[global::Newtonsoft.Json.JsonProperty]
		private string Ip { get; set; }

		[global::Newtonsoft.Json.JsonProperty]
		private ushort Port { get; set; }

		public string RelayJoinCode { get; set; }

		public string RelayRegion { get; set; }

		public string HostId { get; set; }

		[global::Newtonsoft.Json.JsonIgnore]
		public global::Unity.Services.Multiplayer.NetworkEndpointAddress Endpoint
		{
			get
			{
				return new global::Unity.Services.Multiplayer.NetworkEndpointAddress(Ip, Port);
			}
			set
			{
				string addressNoPort = value.AddressNoPort;
				ushort port = value.Port;
				Ip = addressNoPort;
				Port = port;
			}
		}

		internal static global::Unity.Services.Multiplayer.NetworkMetadata Deserialize(string content)
		{
			try
			{
				return global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Unity.Services.Multiplayer.NetworkMetadata>(content) ?? throw new global::Unity.Services.Multiplayer.SessionException("Invalid network metadata.", global::Unity.Services.Multiplayer.SessionError.InvalidSessionMetadata);
			}
			catch (global::System.Exception)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Missing network metadata.", global::Unity.Services.Multiplayer.SessionError.InvalidSessionMetadata);
			}
		}

		internal static string Serialize(global::Unity.Services.Multiplayer.NetworkMetadata networkMetadata)
		{
			try
			{
				return global::Newtonsoft.Json.JsonConvert.SerializeObject(networkMetadata);
			}
			catch (global::System.Exception)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Failed to serialize network metadata.", global::Unity.Services.Multiplayer.SessionError.InvalidSessionMetadata);
			}
		}
	}
}
