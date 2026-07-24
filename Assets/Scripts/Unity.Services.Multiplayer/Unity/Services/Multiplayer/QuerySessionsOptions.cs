namespace Unity.Services.Multiplayer
{
	public class QuerySessionsOptions
	{
		public int Count { get; set; } = 100;

		public int Skip { get; set; }

		public global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.FilterOption> FilterOptions { get; set; } = new global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.FilterOption>();

		public global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.SortOption> SortOptions { get; set; } = new global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.SortOption>();

		public string ContinuationToken { get; set; }
	}
}
