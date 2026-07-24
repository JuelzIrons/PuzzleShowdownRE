namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "KeyValuePair")]
	public class KeyValuePair
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "key", IsRequired = true, EmitDefaultValue = true)]
		public string Key { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "value", IsRequired = true, EmitDefaultValue = true)]
		public string Value { get; }

		[global::UnityEngine.Scripting.Preserve]
		public KeyValuePair(string key, string value)
		{
			Key = key;
			Value = value;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Key != null)
			{
				text = text + "key," + Key + ",";
			}
			if (Value != null)
			{
				text = text + "value," + Value;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Key != null)
			{
				string value = Key.ToString();
				dictionary.Add("key", value);
			}
			if (Value != null)
			{
				string value2 = Value.ToString();
				dictionary.Add("value", value2);
			}
			return dictionary;
		}
	}
}
