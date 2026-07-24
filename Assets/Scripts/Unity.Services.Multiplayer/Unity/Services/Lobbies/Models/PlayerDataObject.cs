namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "PlayerDataObject")]
	public class PlayerDataObject
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		public enum VisibilityOptions
		{
			[global::System.Runtime.Serialization.EnumMember(Value = "public")]
			Public = 1,
			[global::System.Runtime.Serialization.EnumMember(Value = "member")]
			Member = 2,
			[global::System.Runtime.Serialization.EnumMember(Value = "private")]
			Private = 3
		}

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "value", EmitDefaultValue = false)]
		public string Value { get; set; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "visibility", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Lobbies.Models.PlayerDataObject.VisibilityOptions Visibility { get; internal set; }

		[global::UnityEngine.Scripting.Preserve]
		public PlayerDataObject(global::Unity.Services.Lobbies.Models.PlayerDataObject.VisibilityOptions visibility, string value = null)
		{
			Value = value;
			Visibility = visibility;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Value != null)
			{
				text = text + "value," + Value + ",";
			}
			return text + "visibility," + Visibility;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Value != null)
			{
				string value = Value.ToString();
				dictionary.Add("value", value);
			}
			string value2 = Visibility.ToString();
			dictionary.Add("visibility", value2);
			return dictionary;
		}
	}
}
