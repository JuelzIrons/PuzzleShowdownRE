namespace Unity.Services.Multiplayer
{
	internal class MatchmakerBackfillOption : global::Unity.Services.Multiplayer.IModuleOption
	{
		private readonly global::Unity.Services.Multiplayer.BackfillingConfiguration m_Options;

		public global::System.Type Type => typeof(global::Unity.Services.Multiplayer.MatchmakerBackfillOption);

		public MatchmakerBackfillOption(global::Unity.Services.Multiplayer.BackfillingConfiguration options)
		{
			m_Options = options;
		}

		public void Process(global::Unity.Services.Multiplayer.SessionHandler session)
		{
			global::Unity.Services.Multiplayer.MatchmakerModule module = session.GetModule<global::Unity.Services.Multiplayer.MatchmakerModule>();
			if (module == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Trying to setup connection in session but the module isn't registered.", global::Unity.Services.Multiplayer.SessionError.MatchmakerModuleMissing);
			}
			if (session.IsServer)
			{
				module.SetBackfillingConfiguration(m_Options);
				return;
			}
			throw new global::Unity.Services.Multiplayer.SessionException("Attempting to set backfilling configuration on a session handle that is not controlled by a server", global::Unity.Services.Multiplayer.SessionError.InvalidOperation);
		}
	}
}
