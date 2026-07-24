namespace Unity.Multiplayer.Widgets
{
	internal interface IChatEvents
	{
		void OnChatJoined(string chatId)
		{
		}

		void OnChatLeft(string chatId)
		{
		}

		void OnPlayerAddedToChat(global::Unity.Multiplayer.Widgets.IChatParticipant participant)
		{
		}

		void OnChatMessageReceived(global::Unity.Multiplayer.Widgets.IChatMessage message)
		{
		}
	}
}
