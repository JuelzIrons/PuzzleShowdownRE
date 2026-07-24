namespace Unity.Services.Qos.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "KeyValuePair")]
	internal class KeyValuePair
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
	}
}
