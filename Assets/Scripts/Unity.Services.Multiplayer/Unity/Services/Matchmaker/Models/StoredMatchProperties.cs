namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "StoredMatchProperties")]
	public class StoredMatchProperties
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "teams", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Team> Teams { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "players", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Player> Players { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "region", EmitDefaultValue = false)]
		public string Region { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "backfillTicketId", EmitDefaultValue = false)]
		public string BackfillTicketId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "maxPlayers", EmitDefaultValue = false)]
		public int MaxPlayers { get; }

		[global::UnityEngine.Scripting.Preserve]
		public StoredMatchProperties(global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Team> teams = null, global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Player> players = null, string region = null, string backfillTicketId = null, int maxPlayers = 0)
		{
			Teams = teams;
			Players = players;
			Region = region;
			BackfillTicketId = backfillTicketId;
			MaxPlayers = maxPlayers;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Teams != null)
			{
				text = text + "teams," + Teams.ToString() + ",";
			}
			if (Players != null)
			{
				text = text + "players," + Players.ToString() + ",";
			}
			if (Region != null)
			{
				text = text + "region," + Region + ",";
			}
			if (BackfillTicketId != null)
			{
				text = text + "backfillTicketId," + BackfillTicketId + ",";
			}
			return text + "maxPlayers," + MaxPlayers;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Region != null)
			{
				string value = Region.ToString();
				dictionary.Add("region", value);
			}
			if (BackfillTicketId != null)
			{
				string value2 = BackfillTicketId.ToString();
				dictionary.Add("backfillTicketId", value2);
			}
			string value3 = MaxPlayers.ToString();
			dictionary.Add("maxPlayers", value3);
			return dictionary;
		}
	}
}
