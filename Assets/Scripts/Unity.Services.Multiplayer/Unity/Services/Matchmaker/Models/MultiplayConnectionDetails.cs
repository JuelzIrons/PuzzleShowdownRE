namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "MultiplayConnectionDetails")]
	public class MultiplayConnectionDetails
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
		public string Type { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "ip", EmitDefaultValue = false)]
		public string Ip { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "port", EmitDefaultValue = false)]
		public int Port { get; }

		[global::UnityEngine.Scripting.Preserve]
		public MultiplayConnectionDetails(string type, string ip = null, int port = 0)
		{
			Type = type;
			Ip = ip;
			Port = port;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Type != null)
			{
				text = text + "type," + Type + ",";
			}
			if (Ip != null)
			{
				text = text + "ip," + Ip + ",";
			}
			return text + "port," + Port;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Type != null)
			{
				string value = Type.ToString();
				dictionary.Add("type", value);
			}
			if (Ip != null)
			{
				string value2 = Ip.ToString();
				dictionary.Add("ip", value2);
			}
			string value3 = Port.ToString();
			dictionary.Add("port", value3);
			return dictionary;
		}
	}
}
