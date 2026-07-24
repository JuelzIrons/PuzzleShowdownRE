namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QueryFilter")]
	public class QueryFilter
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		public enum FieldOptions
		{
			[global::System.Runtime.Serialization.EnumMember(Value = "MaxPlayers")]
			MaxPlayers = 1,
			[global::System.Runtime.Serialization.EnumMember(Value = "AvailableSlots")]
			AvailableSlots = 2,
			[global::System.Runtime.Serialization.EnumMember(Value = "Name")]
			Name = 3,
			[global::System.Runtime.Serialization.EnumMember(Value = "Created")]
			Created = 4,
			[global::System.Runtime.Serialization.EnumMember(Value = "LastUpdated")]
			LastUpdated = 5,
			[global::System.Runtime.Serialization.EnumMember(Value = "S1")]
			S1 = 6,
			[global::System.Runtime.Serialization.EnumMember(Value = "S2")]
			S2 = 7,
			[global::System.Runtime.Serialization.EnumMember(Value = "S3")]
			S3 = 8,
			[global::System.Runtime.Serialization.EnumMember(Value = "S4")]
			S4 = 9,
			[global::System.Runtime.Serialization.EnumMember(Value = "S5")]
			S5 = 10,
			[global::System.Runtime.Serialization.EnumMember(Value = "N1")]
			N1 = 11,
			[global::System.Runtime.Serialization.EnumMember(Value = "N2")]
			N2 = 12,
			[global::System.Runtime.Serialization.EnumMember(Value = "N3")]
			N3 = 13,
			[global::System.Runtime.Serialization.EnumMember(Value = "N4")]
			N4 = 14,
			[global::System.Runtime.Serialization.EnumMember(Value = "N5")]
			N5 = 15,
			[global::System.Runtime.Serialization.EnumMember(Value = "IsLocked")]
			IsLocked = 16,
			[global::System.Runtime.Serialization.EnumMember(Value = "HasPassword")]
			HasPassword = 17
		}

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		public enum OpOptions
		{
			[global::System.Runtime.Serialization.EnumMember(Value = "CONTAINS")]
			CONTAINS = 1,
			[global::System.Runtime.Serialization.EnumMember(Value = "EQ")]
			EQ = 2,
			[global::System.Runtime.Serialization.EnumMember(Value = "NE")]
			NE = 3,
			[global::System.Runtime.Serialization.EnumMember(Value = "LT")]
			LT = 4,
			[global::System.Runtime.Serialization.EnumMember(Value = "LE")]
			LE = 5,
			[global::System.Runtime.Serialization.EnumMember(Value = "GT")]
			GT = 6,
			[global::System.Runtime.Serialization.EnumMember(Value = "GE")]
			GE = 7
		}

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "field", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions Field { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "value", IsRequired = true, EmitDefaultValue = true)]
		public string Value { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "op", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Lobbies.Models.QueryFilter.OpOptions Op { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QueryFilter(global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions field, string value, global::Unity.Services.Lobbies.Models.QueryFilter.OpOptions op)
		{
			Field = field;
			Value = value;
			Op = op;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			text = text + "field," + Field.ToString() + ",";
			if (Value != null)
			{
				text = text + "value," + Value + ",";
			}
			return text + "op," + Op;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			string value = Field.ToString();
			dictionary.Add("field", value);
			if (Value != null)
			{
				string value2 = Value.ToString();
				dictionary.Add("value", value2);
			}
			string value3 = Op.ToString();
			dictionary.Add("op", value3);
			return dictionary;
		}
	}
}
