namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QueryRequest")]
	public class QueryRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "count", EmitDefaultValue = false)]
		public int? Count { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "skip", EmitDefaultValue = false)]
		public int? Skip { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "sampleResults", EmitDefaultValue = true)]
		public bool SampleResults { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "filter", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryFilter> Filter { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "order", EmitDefaultValue = false)]
		public global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryOrder> Order { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "continuationToken", EmitDefaultValue = false)]
		public string ContinuationToken { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QueryRequest(int? count = 10, int? skip = 0, bool sampleResults = false, global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryFilter> filter = null, global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryOrder> order = null, string continuationToken = null)
		{
			Count = count;
			Skip = skip;
			SampleResults = sampleResults;
			Filter = filter;
			Order = order;
			ContinuationToken = continuationToken;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Count.HasValue)
			{
				text = text + "count," + Count + ",";
			}
			if (Skip.HasValue)
			{
				text = text + "skip," + Skip + ",";
			}
			text = text + "sampleResults," + SampleResults + ",";
			if (Filter != null)
			{
				text = text + "filter," + Filter.ToString() + ",";
			}
			if (Order != null)
			{
				text = text + "order," + Order.ToString() + ",";
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
			if (Count.HasValue)
			{
				string value = Count.ToString();
				dictionary.Add("count", value);
			}
			if (Skip.HasValue)
			{
				string value2 = Skip.ToString();
				dictionary.Add("skip", value2);
			}
			string value3 = SampleResults.ToString();
			dictionary.Add("sampleResults", value3);
			if (ContinuationToken != null)
			{
				string value4 = ContinuationToken.ToString();
				dictionary.Add("continuationToken", value4);
			}
			return dictionary;
		}
	}
}
