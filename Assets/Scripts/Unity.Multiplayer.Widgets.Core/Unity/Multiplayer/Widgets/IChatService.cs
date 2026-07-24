namespace Unity.Multiplayer.Widgets
{
	internal interface IChatService
	{
		float InputDeviceVolume { get; }

		float OutputDeviceVolume { get; }

		event global::System.Action<string> OnChatJoined;

		event global::System.Action<string> OnChatLeft;

		event global::System.Action<global::Unity.Multiplayer.Widgets.IChatParticipant> OnPlayerAddedToChat;

		event global::System.Action<global::Unity.Multiplayer.Widgets.IChatParticipant> OnPlayerRemovedFromChat;

		event global::System.Action<global::Unity.Multiplayer.Widgets.IChatMessage> OnChatMessageReceived;

		global::System.Threading.Tasks.Task InitializeAsync();

		bool IsLoggedIn();

		global::System.Threading.Tasks.Task JoinChatAsync(string channelId, global::Unity.Multiplayer.Widgets.ChatOption chatOption);

		global::System.Threading.Tasks.Task LeaveChatAsync(string channelId);

		global::System.Threading.Tasks.Task SendChatMessageAsync(string channelId, string message);

		global::System.Collections.ObjectModel.ReadOnlyCollection<global::Unity.Multiplayer.Widgets.IAudioDevice> GetAvailableInputDevices();

		global::System.Collections.ObjectModel.ReadOnlyCollection<global::Unity.Multiplayer.Widgets.IAudioDevice> GetAvailableOutputDevices();

		global::Unity.Multiplayer.Widgets.IAudioDevice GetActiveInputDevice();

		global::Unity.Multiplayer.Widgets.IAudioDevice GetActiveOutputDevice();

		void SetOutputDeviceVolume(float volume);

		void SetInputDeviceVolume(float volume);
	}
}
