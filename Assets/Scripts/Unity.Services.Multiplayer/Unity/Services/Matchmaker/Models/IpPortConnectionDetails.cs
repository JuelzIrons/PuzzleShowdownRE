namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "IpPortConnectionDetails")]
	public class IpPortConnectionDetails
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
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Matchmaker.Http.JsonObjectCollectionConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "customData", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Matchmaker.Http.IDeserializable> CustomData { get; }

		[global::UnityEngine.Scripting.Preserve]
		public IpPortConnectionDetails(string type, string ip = null, int port = 0, global::System.Collections.Generic.Dictionary<string, object> customData = null)
		{
			Type = type;
			Ip = ip;
			Port = port;
			CustomData = global::Unity.Services.Matchmaker.Http.JsonObject.GetNewJsonObjectResponse(customData);
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
			text = text + "port," + Port + ",";
			if (CustomData != null)
			{
				text = text + "customData," + CustomData.ToString();
			}
			return text;
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
			if (CustomData != null)
			{
				string value4 = CustomData.ToString();
				dictionary.Add("customData", value4);
			}
			return dictionary;
		}
	}
}
