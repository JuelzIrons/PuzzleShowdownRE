namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QueryOrder")]
	public class QueryOrder
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		public enum FieldOptions
		{
			[global::System.Runtime.Serialization.EnumMember(Value = "Name")]
			Name = 1,
			[global::System.Runtime.Serialization.EnumMember(Value = "MaxPlayers")]
			MaxPlayers = 2,
			[global::System.Runtime.Serialization.EnumMember(Value = "AvailableSlots")]
			AvailableSlots = 3,
			[global::System.Runtime.Serialization.EnumMember(Value = "Created")]
			Created = 4,
			[global::System.Runtime.Serialization.EnumMember(Value = "LastUpdated")]
			LastUpdated = 5,
			[global::System.Runtime.Serialization.EnumMember(Value = "ID")]
			ID = 6,
			[global::System.Runtime.Serialization.EnumMember(Value = "S1")]
			S1 = 7,
			[global::System.Runtime.Serialization.EnumMember(Value = "S2")]
			S2 = 8,
			[global::System.Runtime.Serialization.EnumMember(Value = "S3")]
			S3 = 9,
			[global::System.Runtime.Serialization.EnumMember(Value = "S4")]
			S4 = 10,
			[global::System.Runtime.Serialization.EnumMember(Value = "S5")]
			S5 = 11,
			[global::System.Runtime.Serialization.EnumMember(Value = "N1")]
			N1 = 12,
			[global::System.Runtime.Serialization.EnumMember(Value = "N2")]
			N2 = 13,
			[global::System.Runtime.Serialization.EnumMember(Value = "N3")]
			N3 = 14,
			[global::System.Runtime.Serialization.EnumMember(Value = "N4")]
			N4 = 15,
			[global::System.Runtime.Serialization.EnumMember(Value = "N5")]
			N5 = 16
		}

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "asc", EmitDefaultValue = true)]
		public bool Asc { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Newtonsoft.Json.Converters.StringEnumConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "field", EmitDefaultValue = false)]
		public global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions Field { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QueryOrder(bool asc = false, global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions field = (global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions)0)
		{
			Asc = asc;
			Field = field;
		}

		internal string SerializeAsPathParam()
		{
			return string.Concat("" + "asc," + Asc + ",", "field,", Field.ToString());
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			string value = Asc.ToString();
			dictionary.Add("asc", value);
			string value2 = Field.ToString();
			dictionary.Add("field", value2);
			return dictionary;
		}
	}
}
