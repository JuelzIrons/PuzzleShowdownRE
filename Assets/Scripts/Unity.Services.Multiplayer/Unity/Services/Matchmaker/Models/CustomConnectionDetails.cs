namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "CustomConnectionDetails")]
	public class CustomConnectionDetails
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
		public string Type { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Matchmaker.Http.JsonObjectCollectionConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "customData", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Matchmaker.Http.IDeserializable> CustomData { get; }

		[global::UnityEngine.Scripting.Preserve]
		public CustomConnectionDetails(string type, global::System.Collections.Generic.Dictionary<string, object> customData = null)
		{
			Type = type;
			CustomData = global::Unity.Services.Matchmaker.Http.JsonObject.GetNewJsonObjectResponse(customData);
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Type != null)
			{
				text = text + "type," + Type + ",";
			}
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
			if (CustomData != null)
			{
				string value2 = CustomData.ToString();
				dictionary.Add("customData", value2);
			}
			return dictionary;
		}
	}
}
