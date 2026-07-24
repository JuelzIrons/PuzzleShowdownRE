namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "StoredMatchmakingResults")]
	public class StoredMatchmakingResults
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "matchProperties", EmitDefaultValue = false)]
		public global::Unity.Services.Matchmaker.Models.StoredMatchProperties MatchProperties { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "generatorName", EmitDefaultValue = false)]
		public string GeneratorName { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "queueName", EmitDefaultValue = false)]
		public string QueueName { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "poolName", EmitDefaultValue = false)]
		public string PoolName { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "environmentId", EmitDefaultValue = false)]
		public string EnvironmentId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "backfillTicketId", EmitDefaultValue = false)]
		public string BackfillTicketId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "matchId", EmitDefaultValue = false)]
		public string MatchId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "poolId", EmitDefaultValue = false)]
		public string PoolId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public StoredMatchmakingResults(global::Unity.Services.Matchmaker.Models.StoredMatchProperties matchProperties = null, string generatorName = null, string queueName = null, string poolName = null, string environmentId = null, string backfillTicketId = null, string matchId = null, string poolId = null)
		{
			MatchProperties = matchProperties;
			GeneratorName = generatorName;
			QueueName = queueName;
			PoolName = poolName;
			EnvironmentId = environmentId;
			BackfillTicketId = backfillTicketId;
			MatchId = matchId;
			PoolId = poolId;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (MatchProperties != null)
			{
				text = text + "matchProperties," + MatchProperties.ToString() + ",";
			}
			if (GeneratorName != null)
			{
				text = text + "generatorName," + GeneratorName + ",";
			}
			if (QueueName != null)
			{
				text = text + "queueName," + QueueName + ",";
			}
			if (PoolName != null)
			{
				text = text + "poolName," + PoolName + ",";
			}
			if (EnvironmentId != null)
			{
				text = text + "environmentId," + EnvironmentId + ",";
			}
			if (BackfillTicketId != null)
			{
				text = text + "backfillTicketId," + BackfillTicketId + ",";
			}
			if (MatchId != null)
			{
				text = text + "matchId," + MatchId + ",";
			}
			if (PoolId != null)
			{
				text = text + "poolId," + PoolId;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (GeneratorName != null)
			{
				string value = GeneratorName.ToString();
				dictionary.Add("generatorName", value);
			}
			if (QueueName != null)
			{
				string value2 = QueueName.ToString();
				dictionary.Add("queueName", value2);
			}
			if (PoolName != null)
			{
				string value3 = PoolName.ToString();
				dictionary.Add("poolName", value3);
			}
			if (EnvironmentId != null)
			{
				string value4 = EnvironmentId.ToString();
				dictionary.Add("environmentId", value4);
			}
			if (BackfillTicketId != null)
			{
				string value5 = BackfillTicketId.ToString();
				dictionary.Add("backfillTicketId", value5);
			}
			if (MatchId != null)
			{
				string value6 = MatchId.ToString();
				dictionary.Add("matchId", value6);
			}
			if (PoolId != null)
			{
				string value7 = PoolId.ToString();
				dictionary.Add("poolId", value7);
			}
			return dictionary;
		}
	}
}
