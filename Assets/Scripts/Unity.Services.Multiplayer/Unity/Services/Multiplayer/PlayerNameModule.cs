namespace Unity.Services.Multiplayer
{
	internal class PlayerNameModule : global::Unity.Services.Multiplayer.IModule
	{
		public const string PropertyKey = "_player_name";

		private readonly global::Unity.Services.Multiplayer.ISession m_Session;

		private readonly global::Unity.Services.Authentication.Internal.IPlayerNameComponent m_PlayerName;

		internal const string InvalidNameWarning = "Attempting to set player name in session to an invalid value.";

		internal const string SyncFailureError = "Failed to synchronize player name in session.";

		private global::Unity.Services.Multiplayer.PlayerNameSessionOption m_PlayerNameOption;

		internal PlayerNameModule(global::Unity.Services.Authentication.Internal.IPlayerNameComponent playerName, global::Unity.Services.Multiplayer.ISession session)
		{
			m_PlayerName = playerName;
			m_Session = session;
		}

		internal void Enable(global::Unity.Services.Multiplayer.PlayerNameSessionOption option)
		{
			m_PlayerNameOption = option;
			m_PlayerName.PlayerNameChanged -= OnPlayerNameChanged;
			m_PlayerName.PlayerNameChanged += OnPlayerNameChanged;
		}

		global::System.Threading.Tasks.Task global::Unity.Services.Multiplayer.IModule.InitializeAsync()
		{
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		internal async void OnPlayerNameChanged(string playerName)
		{
			try
			{
				await SyncPlayerNameAsync(playerName);
			}
			catch (global::System.Exception message)
			{
				global::Unity.Services.Multiplayer.Logger.LogError(message);
			}
		}

		internal async global::System.Threading.Tasks.Task SyncPlayerNameAsync(string playerName)
		{
			if (m_Session.State != global::Unity.Services.Multiplayer.SessionState.Connected || m_PlayerNameOption == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(playerName))
			{
				global::Unity.Services.Multiplayer.Logger.LogWarning("Attempting to set player name in session to an invalid value.");
				return;
			}
			m_Session.CurrentPlayer.SetProperty("_player_name", new global::Unity.Services.Multiplayer.PlayerProperty(playerName, m_PlayerNameOption.Visibility));
			try
			{
				await m_Session.SaveCurrentPlayerDataAsync();
			}
			catch (global::System.Exception)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Failed to synchronize player name in session.", global::Unity.Services.Multiplayer.SessionError.PlayerNameSynchronizationFailed);
			}
		}

		public global::System.Threading.Tasks.Task LeaveAsync()
		{
			return global::System.Threading.Tasks.Task.CompletedTask;
		}
	}
}
