namespace Unity.Services.Multiplayer
{
	public class SortOption
	{
		public global::Unity.Services.Multiplayer.SortOrder Order { get; set; }

		public global::Unity.Services.Multiplayer.SortField Field { get; set; }

		public SortOption(global::Unity.Services.Multiplayer.SortOrder order, global::Unity.Services.Multiplayer.SortField field)
		{
			Order = order;
			Field = field;
		}

		public SortOption()
		{
			Order = global::Unity.Services.Multiplayer.SortOrder.Ascending;
			Field = global::Unity.Services.Multiplayer.SortField.CreationTime;
		}
	}
}
