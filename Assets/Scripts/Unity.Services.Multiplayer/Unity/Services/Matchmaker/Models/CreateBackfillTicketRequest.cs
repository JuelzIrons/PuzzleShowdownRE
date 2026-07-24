namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "CreateBackfillTicketRequest")]
	internal class CreateBackfillTicketRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "queueName", EmitDefaultValue = false)]
		public string QueueName { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "connection", EmitDefaultValue = false)]
		public string Connection { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "connectionDetails", EmitDefaultValue = false)]
		public global::Unity.Services.Matchmaker.Models.ConnectionDetails ConnectionDetails { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Matchmaker.Http.JsonObjectCollectionConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "attributes", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Matchmaker.Http.IDeserializable> Attributes { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "poolId", EmitDefaultValue = false)]
		public string PoolId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "properties", IsRequired = true, EmitDefaultValue = true)]
		public global::System.Collections.Generic.Dictionary<string, byte[]> Properties { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "matchId", EmitDefaultValue = false)]
		public string MatchId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public CreateBackfillTicketRequest(global::System.Collections.Generic.Dictionary<string, byte[]> properties, string queueName = null, string connection = null, global::Unity.Services.Matchmaker.Models.ConnectionDetails connectionDetails = null, global::System.Collections.Generic.Dictionary<string, object> attributes = null, string poolId = null, string matchId = null)
		{
			QueueName = queueName;
			Connection = connection;
			ConnectionDetails = connectionDetails;
			Attributes = global::Unity.Services.Matchmaker.Http.JsonObject.GetNewJsonObjectResponse(attributes);
			PoolId = poolId;
			Properties = properties;
			MatchId = matchId;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (QueueName != null)
			{
				text = text + "queueName," + QueueName + ",";
			}
			if (Connection != null)
			{
				text = text + "connection," + Connection + ",";
			}
			if (ConnectionDetails != null)
			{
				text = text + "connectionDetails," + ConnectionDetails.ToString() + ",";
			}
			if (Attributes != null)
			{
				text = text + "attributes," + Attributes.ToString() + ",";
			}
			if (PoolId != null)
			{
				text = text + "poolId," + PoolId + ",";
			}
			if (Properties != null)
			{
				text = text + "properties," + Properties.ToString() + ",";
			}
			if (MatchId != null)
			{
				text = text + "matchId," + MatchId;
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
			if (Connection != null)
			{
				string value2 = Connection.ToString();
				dictionary.Add("connection", value2);
			}
			if (Attributes != null)
			{
				string value3 = Attributes.ToString();
				dictionary.Add("attributes", value3);
			}
			if (PoolId != null)
			{
				string value4 = PoolId.ToString();
				dictionary.Add("poolId", value4);
			}
			if (Properties != null)
			{
				string value5 = Properties.ToString();
				dictionary.Add("properties", value5);
			}
			if (MatchId != null)
			{
				string value6 = MatchId.ToString();
				dictionary.Add("matchId", value6);
			}
			return dictionary;
		}
	}
}
