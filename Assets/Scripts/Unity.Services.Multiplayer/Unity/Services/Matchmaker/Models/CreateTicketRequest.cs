namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "CreateTicketRequest")]
	internal class CreateTicketRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "queueName", EmitDefaultValue = false)]
		public string QueueName { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Matchmaker.Http.JsonObjectCollectionConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "attributes", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Matchmaker.Http.IDeserializable> Attributes { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "players", IsRequired = true, EmitDefaultValue = true)]
		public global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Player> Players { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "overrides", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Override> Overrides { get; }

		[global::UnityEngine.Scripting.Preserve]
		public CreateTicketRequest(global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Player> players, string queueName = null, global::System.Collections.Generic.Dictionary<string, object> attributes = null, global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Override> overrides = null)
		{
			QueueName = queueName;
			Attributes = global::Unity.Services.Matchmaker.Http.JsonObject.GetNewJsonObjectResponse(attributes);
			Players = players;
			Overrides = overrides;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (QueueName != null)
			{
				text = text + "queueName," + QueueName + ",";
			}
			if (Attributes != null)
			{
				text = text + "attributes," + Attributes.ToString() + ",";
			}
			if (Players != null)
			{
				text = text + "players," + Players.ToString() + ",";
			}
			if (Overrides != null)
			{
				text = text + "overrides," + Overrides.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (QueueName != null)
			{
				string value = QueueName.ToString();
				dictionary.Add("queueName", value);
			}
			if (Attributes != null)
			{
				string value2 = Attributes.ToString();
				dictionary.Add("attributes", value2);
			}
			return dictionary;
		}
	}
}
