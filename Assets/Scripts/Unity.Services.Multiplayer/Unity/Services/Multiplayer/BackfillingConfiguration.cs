namespace Unity.Services.Multiplayer
{
	public class BackfillingConfiguration
	{
		public bool Enable { get; set; }

		public bool AutomaticallyRemovePlayers { get; private set; } = true;

		public bool AutoStart { get; private set; } = true;

		public int BackfillingLoopInterval { get; private set; } = 1;

		public int PlayerConnectionTimeout { get; set; } = 30;

		public static global::Unity.Services.Multiplayer.BackfillingConfiguration WithBackfillingConfiguration(bool enable = true, bool automaticallyRemovePlayers = true, bool autoStart = true, int playerConnectionTimeout = 30, int backfillingLoopInterval = 1)
		{
			if (playerConnectionTimeout < 0)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Player connection timeout must be greater or equal to 0", global::Unity.Services.Multiplayer.SessionError.InvalidParameter);
			}
			if (backfillingLoopInterval < 1)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Backfilling loop interval must be greater or equal to 1", global::Unity.Services.Multiplayer.SessionError.InvalidParameter);
			}
			return new global::Unity.Services.Multiplayer.BackfillingConfiguration
			{
				Enable = enable,
				AutomaticallyRemovePlayers = automaticallyRemovePlayers,
				AutoStart = autoStart,
				BackfillingLoopInterval = backfillingLoopInterval,
				PlayerConnectionTimeout = playerConnectionTimeout
			};
		}
	}
}
