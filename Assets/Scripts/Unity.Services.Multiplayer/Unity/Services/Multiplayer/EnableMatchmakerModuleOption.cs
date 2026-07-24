namespace Unity.Services.Multiplayer
{
	internal class EnableMatchmakerModuleOption : global::Unity.Services.Multiplayer.IModuleOption
	{
		public global::System.Type Type => typeof(global::Unity.Services.Multiplayer.EnableMatchmakerModuleOption);

		public void Process(global::Unity.Services.Multiplayer.SessionHandler session)
		{
			(session.GetModule<global::Unity.Services.Multiplayer.MatchmakerModule>() ?? throw new global::Unity.Services.Multiplayer.SessionException("Trying to setup connection in session but the module isn't registered.", global::Unity.Services.Multiplayer.SessionError.MatchmakerModuleMissing)).Enable();
		}
	}
}
