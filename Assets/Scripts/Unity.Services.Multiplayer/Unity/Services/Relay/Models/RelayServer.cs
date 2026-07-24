namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "RelayServer")]
	public class RelayServer
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "ipV4", IsRequired = true, EmitDefaultValue = true)]
		public string IpV4 { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "port", IsRequired = true, EmitDefaultValue = true)]
		public int Port { get; }

		[global::UnityEngine.Scripting.Preserve]
		public RelayServer(string ipV4, int port)
		{
			IpV4 = ipV4;
			Port = port;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (IpV4 != null)
			{
				text = text + "ipV4," + IpV4 + ",";
			}
			return text + "port," + Port;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (IpV4 != null)
			{
				string value = IpV4.ToString();
				dictionary.Add("ipV4", value);
			}
			string value2 = Port.ToString();
			dictionary.Add("port", value2);
			return dictionary;
		}
	}
}
