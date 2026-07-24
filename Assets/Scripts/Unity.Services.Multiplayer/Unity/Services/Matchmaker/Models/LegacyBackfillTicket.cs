namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "BackfillTicket")]
	internal class LegacyBackfillTicket
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "connection", EmitDefaultValue = false)]
		public string Connection { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "attributes", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, object> Attributes { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "properties", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, byte[]> Properties { get; }

		[global::UnityEngine.Scripting.Preserve]
		public LegacyBackfillTicket(string id = null, string connection = null, global::System.Collections.Generic.Dictionary<string, object> attributes = null, global::System.Collections.Generic.Dictionary<string, byte[]> properties = null)
		{
			Id = id;
			Connection = connection;
			Attributes = attributes;
			Properties = properties;
		}
	}
}
