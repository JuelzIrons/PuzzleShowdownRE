namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "BulkUpdateRequest")]
	internal class BulkUpdateRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Lobbies.Http.JsonObjectConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "lobbyUpdate", EmitDefaultValue = false)]
		public global::Unity.Services.Lobbies.Models.UpdateRequest LobbyUpdate { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::Newtonsoft.Json.JsonConverter(typeof(global::Unity.Services.Lobbies.Http.JsonObjectCollectionConverter))]
		[global::System.Runtime.Serialization.DataMember(Name = "playerUpdates", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerUpdateRequest> PlayerUpdates { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "playersToAdd", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player> PlayersToAdd { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "playersToRemove", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<string> PlayersToRemove { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "ignoreIneffectualUpdates", EmitDefaultValue = true)]
		public bool? IgnoreIneffectualUpdates { get; }

		[global::UnityEngine.Scripting.Preserve]
		public BulkUpdateRequest(global::Unity.Services.Lobbies.Models.UpdateRequest lobbyUpdate = null, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerUpdateRequest> playerUpdates = null, global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Player> playersToAdd = null, global::System.Collections.Generic.List<string> playersToRemove = null, bool? ignoreIneffectualUpdates = false)
		{
			LobbyUpdate = lobbyUpdate;
			PlayerUpdates = playerUpdates;
			PlayersToAdd = playersToAdd;
			PlayersToRemove = playersToRemove;
			IgnoreIneffectualUpdates = ignoreIneffectualUpdates;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (LobbyUpdate != null)
			{
				text = text + "lobbyUpdate," + LobbyUpdate.ToString() + ",";
			}
			if (PlayerUpdates != null)
			{
				text = text + "playerUpdates," + PlayerUpdates.ToString() + ",";
			}
			if (PlayersToAdd != null)
			{
				text = text + "playersToAdd," + PlayersToAdd.ToString() + ",";
			}
			if (PlayersToRemove != null)
			{
				text = text + "playersToRemove," + PlayersToRemove.ToString() + ",";
			}
			if (IgnoreIneffectualUpdates.HasValue)
			{
				text = text + "ignoreIneffectualUpdates," + IgnoreIneffectualUpdates;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (PlayersToRemove != null)
			{
				string value = PlayersToRemove.ToString();
				dictionary.Add("playersToRemove", value);
			}
			if (IgnoreIneffectualUpdates.HasValue)
			{
				string value2 = IgnoreIneffectualUpdates.ToString();
				dictionary.Add("ignoreIneffectualUpdates", value2);
			}
			return dictionary;
		}
	}
}
