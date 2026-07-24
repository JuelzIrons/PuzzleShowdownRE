namespace Unity.Services.Lobbies
{
	public class QueryLobbiesOptions
	{
		public int Count { get; set; } = 10;

		public int Skip { get; set; }

		public bool SampleResults { get; set; }

		public global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryFilter> Filters { get; set; }

		public global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryOrder> Order { get; set; }

		public string ContinuationToken { get; set; }
	}
}
