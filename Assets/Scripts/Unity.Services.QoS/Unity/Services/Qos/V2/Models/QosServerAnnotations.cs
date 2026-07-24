namespace Unity.Services.Qos.V2.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QosServerAnnotations")]
	public class QosServerAnnotations
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "projectId", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<string> ProjectId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "environmentId", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<string> EnvironmentId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "relayRegionId", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<string> RelayRegionId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "multiplayRegionId", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<string> MultiplayRegionId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "multiplayFleetId", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<string> MultiplayFleetId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "matchmakerQueueName", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<string> MatchmakerQueueName { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "matchmakerPoolId", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<string> MatchmakerPoolId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QosServerAnnotations(global::System.Collections.Generic.List<string> projectId = null, global::System.Collections.Generic.List<string> environmentId = null, global::System.Collections.Generic.List<string> relayRegionId = null, global::System.Collections.Generic.List<string> multiplayRegionId = null, global::System.Collections.Generic.List<string> multiplayFleetId = null, global::System.Collections.Generic.List<string> matchmakerQueueName = null, global::System.Collections.Generic.List<string> matchmakerPoolId = null)
		{
			ProjectId = projectId;
			EnvironmentId = environmentId;
			RelayRegionId = relayRegionId;
			MultiplayRegionId = multiplayRegionId;
			MultiplayFleetId = multiplayFleetId;
			MatchmakerQueueName = matchmakerQueueName;
			MatchmakerPoolId = matchmakerPoolId;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (ProjectId != null)
			{
				text = text + "projectId," + ProjectId.ToString() + ",";
			}
			if (EnvironmentId != null)
			{
				text = text + "environmentId," + EnvironmentId.ToString() + ",";
			}
			if (RelayRegionId != null)
			{
				text = text + "relayRegionId," + RelayRegionId.ToString() + ",";
			}
			if (MultiplayRegionId != null)
			{
				text = text + "multiplayRegionId," + MultiplayRegionId.ToString() + ",";
			}
			if (MultiplayFleetId != null)
			{
				text = text + "multiplayFleetId," + MultiplayFleetId.ToString() + ",";
			}
			if (MatchmakerQueueName != null)
			{
				text = text + "matchmakerQueueName," + MatchmakerQueueName.ToString() + ",";
			}
			if (MatchmakerPoolId != null)
			{
				text = text + "matchmakerPoolId," + MatchmakerPoolId.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (ProjectId != null)
			{
				string value = ProjectId.ToString();
				dictionary.Add("projectId", value);
			}
			if (EnvironmentId != null)
			{
				string value2 = EnvironmentId.ToString();
				dictionary.Add("environmentId", value2);
			}
			if (RelayRegionId != null)
			{
				string value3 = RelayRegionId.ToString();
				dictionary.Add("relayRegionId", value3);
			}
			if (MultiplayRegionId != null)
			{
				string value4 = MultiplayRegionId.ToString();
				dictionary.Add("multiplayRegionId", value4);
			}
			if (MultiplayFleetId != null)
			{
				string value5 = MultiplayFleetId.ToString();
				dictionary.Add("multiplayFleetId", value5);
			}
			if (MatchmakerQueueName != null)
			{
				string value6 = MatchmakerQueueName.ToString();
				dictionary.Add("matchmakerQueueName", value6);
			}
			if (MatchmakerPoolId != null)
			{
				string value7 = MatchmakerPoolId.ToString();
				dictionary.Add("matchmakerPoolId", value7);
			}
			return dictionary;
		}
	}
}
