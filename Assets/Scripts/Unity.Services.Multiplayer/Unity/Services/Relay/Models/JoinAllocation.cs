namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "JoinAllocation")]
	public class JoinAllocation
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "allocationId", IsRequired = true, EmitDefaultValue = true)]
		public global::System.Guid AllocationId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "serverEndpoints", IsRequired = true, EmitDefaultValue = true)]
		public global::System.Collections.Generic.List<global::Unity.Services.Relay.Models.RelayServerEndpoint> ServerEndpoints { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "relayServer", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Relay.Models.RelayServer RelayServer { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "key", IsRequired = true, EmitDefaultValue = true)]
		public byte[] Key { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "connectionData", IsRequired = true, EmitDefaultValue = true)]
		public byte[] ConnectionData { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "allocationIdBytes", IsRequired = true, EmitDefaultValue = true)]
		public byte[] AllocationIdBytes { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "region", IsRequired = true, EmitDefaultValue = true)]
		public string Region { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "hostConnectionData", IsRequired = true, EmitDefaultValue = true)]
		public byte[] HostConnectionData { get; }

		[global::UnityEngine.Scripting.Preserve]
		public JoinAllocation(global::System.Guid allocationId, global::System.Collections.Generic.List<global::Unity.Services.Relay.Models.RelayServerEndpoint> serverEndpoints, global::Unity.Services.Relay.Models.RelayServer relayServer, byte[] key, byte[] connectionData, byte[] allocationIdBytes, string region, byte[] hostConnectionData)
		{
			AllocationId = allocationId;
			ServerEndpoints = serverEndpoints;
			RelayServer = relayServer;
			Key = key;
			ConnectionData = connectionData;
			AllocationIdBytes = allocationIdBytes;
			Region = region;
			HostConnectionData = hostConnectionData;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			_ = AllocationId;
			text = text + "allocationId," + AllocationId.ToString() + ",";
			if (ServerEndpoints != null)
			{
				text = text + "serverEndpoints," + ServerEndpoints.ToString() + ",";
			}
			if (RelayServer != null)
			{
				text = text + "relayServer," + RelayServer.ToString() + ",";
			}
			if (Key != null)
			{
				text = text + "key," + Key.ToString() + ",";
			}
			if (ConnectionData != null)
			{
				text = text + "connectionData," + ConnectionData.ToString() + ",";
			}
			if (AllocationIdBytes != null)
			{
				text = text + "allocationIdBytes," + AllocationIdBytes.ToString() + ",";
			}
			if (Region != null)
			{
				text = text + "region," + Region + ",";
			}
			if (HostConnectionData != null)
			{
				text = text + "hostConnectionData," + HostConnectionData.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			_ = AllocationId;
			string value = AllocationId.ToString();
			dictionary.Add("allocationId", value);
			if (Key != null)
			{
				string value2 = Key.ToString();
				dictionary.Add("key", value2);
			}
			if (ConnectionData != null)
			{
				string value3 = ConnectionData.ToString();
				dictionary.Add("connectionData", value3);
			}
			if (AllocationIdBytes != null)
			{
				string value4 = AllocationIdBytes.ToString();
				dictionary.Add("allocationIdBytes", value4);
			}
			if (Region != null)
			{
				string value5 = Region.ToString();
				dictionary.Add("region", value5);
			}
			if (HostConnectionData != null)
			{
				string value6 = HostConnectionData.ToString();
				dictionary.Add("hostConnectionData", value6);
			}
			return dictionary;
		}
	}
}
