namespace Unity.Services.Multiplayer
{
	public class PlayerProperty : global::Unity.Services.Multiplayer.IValueProperty, global::Unity.Services.Multiplayer.IVisibilityProperty
	{
		public string Value { get; }

		public global::Unity.Services.Multiplayer.VisibilityPropertyOptions Visibility { get; }

		public PlayerProperty(string value = null, global::Unity.Services.Multiplayer.VisibilityPropertyOptions visibility = global::Unity.Services.Multiplayer.VisibilityPropertyOptions.Public)
		{
			Value = value;
			Visibility = visibility;
		}
	}
}
