namespace Unity.Multiplayer.Widgets
{
	internal interface IChatMessage
	{
		string SenderPlayerId { get; }

		string SenderDisplayName { get; }

		string ChannelName { get; }

		string Text { get; }

		bool FromSelf { get; }

		global::System.DateTime ReceivedTime { get; }

		string Id { get; }
	}
}
