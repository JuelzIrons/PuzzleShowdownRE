namespace Unity.Services.Multiplayer
{
	internal class PlayerNameSessionOption : global::Unity.Services.Multiplayer.IModuleOption
	{
		global::System.Type global::Unity.Services.Multiplayer.IModuleOption.Type => typeof(global::Unity.Services.Multiplayer.PlayerNameModule);

		public global::Unity.Services.Multiplayer.VisibilityPropertyOptions Visibility { get; private set; }

		internal PlayerNameSessionOption(global::Unity.Services.Multiplayer.VisibilityPropertyOptions visibility)
		{
			Visibility = visibility;
		}

		public void Process(global::Unity.Services.Multiplayer.SessionHandler session)
		{
			session.GetModule<global::Unity.Services.Multiplayer.PlayerNameModule>()?.Enable(this);
		}
	}
}
