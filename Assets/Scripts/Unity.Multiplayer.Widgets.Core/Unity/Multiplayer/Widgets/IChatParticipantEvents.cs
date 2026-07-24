namespace Unity.Multiplayer.Widgets
{
	internal interface IChatParticipantEvents : global::Unity.Multiplayer.Widgets.IPlayerId
	{
		void OnPlayerAudioEnergyChanged()
		{
		}

		void OnPlayerSpeechDetected()
		{
		}

		void OnPlayerMuteStateChanged(bool isMuted)
		{
		}
	}
}
