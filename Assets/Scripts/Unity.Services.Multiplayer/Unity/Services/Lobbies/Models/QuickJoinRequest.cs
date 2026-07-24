namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QuickJoinRequest")]
	public class QuickJoinRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "filter", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryFilter> Filter { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "player", EmitDefaultValue = false)]
		public global::Unity.Services.Lobbies.Models.Player Player { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QuickJoinRequest(global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryFilter> filter = null, global::Unity.Services.Lobbies.Models.Player player = null)
		{
			Filter = filter;
			Player = player;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Filter != null)
			{
				text = text + "filter," + Filter.ToString() + ",";
			}
			if (Player != null)
			{
				text = text + "player," + Player.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			return new global::System.Collections.Generic.Dictionary<string, string>();
		}
	}
}
