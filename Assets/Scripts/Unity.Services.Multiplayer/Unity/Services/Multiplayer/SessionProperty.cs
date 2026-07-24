namespace Unity.Services.Multiplayer
{
	public class SessionProperty : global::Unity.Services.Multiplayer.IValueProperty, global::Unity.Services.Multiplayer.IVisibilityProperty
	{
		public string Value { get; }

		public global::Unity.Services.Multiplayer.VisibilityPropertyOptions Visibility { get; }

		public global::Unity.Services.Multiplayer.PropertyIndex Index { get; }

		public SessionProperty(string value, global::Unity.Services.Multiplayer.VisibilityPropertyOptions visibility = global::Unity.Services.Multiplayer.VisibilityPropertyOptions.Public, global::Unity.Services.Multiplayer.PropertyIndex index = global::Unity.Services.Multiplayer.PropertyIndex.None)
		{
			Value = value;
			Visibility = visibility;
			Index = index;
		}
	}
}
