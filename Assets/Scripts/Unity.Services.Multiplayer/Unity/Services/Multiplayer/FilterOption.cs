namespace Unity.Services.Multiplayer
{
	public class FilterOption
	{
		public global::Unity.Services.Multiplayer.FilterField Field { get; }

		public string Value { get; }

		public global::Unity.Services.Multiplayer.FilterOperation Operation { get; }

		public FilterOption(global::Unity.Services.Multiplayer.FilterField field, string value, global::Unity.Services.Multiplayer.FilterOperation operation)
		{
			Field = field;
			Value = value;
			Operation = operation;
		}
	}
}
