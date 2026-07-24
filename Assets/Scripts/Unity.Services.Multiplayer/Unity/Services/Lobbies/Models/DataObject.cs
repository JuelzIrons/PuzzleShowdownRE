namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "DataObject")]
	public class DataObject
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
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		public enum IndexOptions
		{
			[global::System.Runtime.Serialization.EnumMember(Value = "S1")]
			S1 = 1,
			[global::System.Runtime.Serialization.EnumMember(Value = "S2")]
			S2 = 2,
			[global::System.Runtime.Serialization.EnumMember(Value = "S3")]
			S3 = 3,
			[global::System.Runtime.Serialization.EnumMember(Value = "S4")]
			S4 = 4,
			[global::System.Runtime.Serialization.EnumMember(Value = "S5")]
			S5 = 5,
			[global::System.Runtime.Serialization.EnumMember(Value = "N1")]
			N1 = 6,
			[global::System.Runtime.Serialization.EnumMember(Value = "N2")]
			N2 = 7,
			[global::System.Runtime.Serialization.EnumMember(Value = "N3")]
			N3 = 8,
			[global::System.Runtime.Serialization.EnumMember(Value = "N4")]
			N4 = 9,
			[global::System.Runtime.Serialization.EnumMember(Value = "N5")]
			N5 = 10
		}

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "value", EmitDefaultValue = false)]
		public string Value { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "visibility", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Lobbies.Models.DataObject.VisibilityOptions Visibility { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "index", EmitDefaultValue = false)]
		public global::Unity.Services.Lobbies.Models.DataObject.IndexOptions Index { get; }

		[global::UnityEngine.Scripting.Preserve]
		public DataObject(global::Unity.Services.Lobbies.Models.DataObject.VisibilityOptions visibility, string value = null, global::Unity.Services.Lobbies.Models.DataObject.IndexOptions index = (global::Unity.Services.Lobbies.Models.DataObject.IndexOptions)0)
		{
			Value = value;
			Visibility = visibility;
			Index = index;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Value != null)
			{
				text = text + "value," + Value + ",";
			}
			text = text + "visibility," + Visibility.ToString() + ",";
			return text + "index," + Index;
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
			string value3 = Index.ToString();
			dictionary.Add("index", value3);
			return dictionary;
		}
	}
}
