namespace Unity.Multiplayer.Widgets
{
	internal interface ISessionEvents
	{
		void OnSessionChanged()
		{
		}

		void OnSessionStateChanged(global::Unity.Services.Multiplayer.SessionState sessionState)
		{
		}

		void OnPlayerJoinedSession(string playerId)
		{
		}

		void OnPlayerLeftSession(string playerId)
		{
		}

		void OnSessionPropertiesChanged()
		{
		}

		void OnPlayerPropertiesChanged()
		{
		}

		void OnRemovedFromSession()
		{
		}

		void OnSessionDeleted()
		{
		}
	}
}
