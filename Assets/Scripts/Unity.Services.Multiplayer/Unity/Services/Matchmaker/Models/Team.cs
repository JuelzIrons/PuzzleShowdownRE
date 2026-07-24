namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "Team")]
	public class Team
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "teamName", EmitDefaultValue = false)]
		public string TeamName { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "teamId", EmitDefaultValue = false)]
		public string TeamId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "playerIds", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<string> PlayerIds { get; }

		[global::UnityEngine.Scripting.Preserve]
		public Team(string teamName = null, string teamId = null, global::System.Collections.Generic.List<string> playerIds = null)
		{
			TeamName = teamName;
			TeamId = teamId;
			PlayerIds = playerIds;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (TeamName != null)
			{
				text = text + "teamName," + TeamName + ",";
			}
			if (TeamId != null)
			{
				text = text + "teamId," + TeamId + ",";
			}
			if (PlayerIds != null)
			{
				text = text + "playerIds," + PlayerIds.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (TeamName != null)
			{
				string value = TeamName.ToString();
				dictionary.Add("teamName", value);
			}
			if (TeamId != null)
			{
				string value2 = TeamId.ToString();
				dictionary.Add("teamId", value2);
			}
			if (PlayerIds != null)
			{
				string value3 = PlayerIds.ToString();
				dictionary.Add("playerIds", value3);
			}
			return dictionary;
		}
	}
}
