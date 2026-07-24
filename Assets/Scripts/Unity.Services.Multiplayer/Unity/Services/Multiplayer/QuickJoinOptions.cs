namespace Unity.Services.Multiplayer
{
	public class QuickJoinOptions
	{
		public global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.FilterOption> Filters { get; set; } = new global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.FilterOption>();

		public global::System.TimeSpan Timeout { get; set; }

		public bool CreateSession { get; set; }
	}
}
