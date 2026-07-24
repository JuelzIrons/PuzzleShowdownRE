namespace Unity.Multiplayer.Widgets
{
	internal interface ISessionLifecycleEvents
	{
		void OnSessionJoining()
		{
		}

		void OnSessionFailedToJoin(global::Unity.Services.Multiplayer.SessionException sessionException)
		{
		}

		void OnSessionJoined()
		{
		}

		void OnSessionLeft()
		{
		}
	}
}
