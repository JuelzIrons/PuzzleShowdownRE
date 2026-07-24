namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "RelayServerEndpoint")]
	public class RelayServerEndpoint
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		public enum NetworkOptions
		{
			[global::System.Runtime.Serialization.EnumMember(Value = "udp")]
			Udp = 1,
			[global::System.Runtime.Serialization.EnumMember(Value = "tcp")]
			Tcp = 2
		}

		public const string ConnectionTypeUdp = "udp";

		public const string ConnectionTypeDtls = "dtls";

		public const string ConnectionTypeWss = "wss";

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "connectionType", IsRequired = true, EmitDefaultValue = true)]
		public string ConnectionType { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "network", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Relay.Models.RelayServerEndpoint.NetworkOptions Network { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "reliable", IsRequired = true, EmitDefaultValue = true)]
		public bool Reliable { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "secure", IsRequired = true, EmitDefaultValue = true)]
		public bool Secure { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "host", IsRequired = true, EmitDefaultValue = true)]
		public string Host { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "port", IsRequired = true, EmitDefaultValue = true)]
		public int Port { get; }

		[global::UnityEngine.Scripting.Preserve]
		public RelayServerEndpoint(string connectionType, global::Unity.Services.Relay.Models.RelayServerEndpoint.NetworkOptions network, bool reliable, bool secure, string host, int port)
		{
			ConnectionType = connectionType;
			Network = network;
			Reliable = reliable;
			Secure = secure;
			Host = host;
			Port = port;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (ConnectionType != null)
			{
				text = text + "connectionType," + ConnectionType + ",";
			}
			text = text + "network," + Network.ToString() + ",";
			text = text + "reliable," + Reliable + ",";
			text = text + "secure," + Secure + ",";
			if (Host != null)
			{
				text = text + "host," + Host + ",";
			}
			return text + "port," + Port;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (ConnectionType != null)
			{
				string value = ConnectionType.ToString();
				dictionary.Add("connectionType", value);
			}
			string value2 = Network.ToString();
			dictionary.Add("network", value2);
			string value3 = Reliable.ToString();
			dictionary.Add("reliable", value3);
			string value4 = Secure.ToString();
			dictionary.Add("secure", value4);
			if (Host != null)
			{
				string value5 = Host.ToString();
				dictionary.Add("host", value5);
			}
			string value6 = Port.ToString();
			dictionary.Add("port", value6);
			return dictionary;
		}
	}
}
