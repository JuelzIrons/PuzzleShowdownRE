namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QueryResponse")]
	public class QueryResponse
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "results", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Lobby> Results { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "continuationToken", EmitDefaultValue = false)]
		public string ContinuationToken { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QueryResponse(global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.Lobby> results = null, string continuationToken = null)
		{
			Results = results;
			ContinuationToken = continuationToken;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Results != null)
			{
				text = text + "results," + Results.ToString() + ",";
			}
			if (ContinuationToken != null)
			{
				text = text + "continuationToken," + ContinuationToken;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (ContinuationToken != null)
			{
				string value = ContinuationToken.ToString();
				dictionary.Add("continuationToken", value);
			}
			return dictionary;
		}
	}
}
