namespace Unity.Multiplayer.Widgets
{
	internal interface IChatParticipant
	{
		string Id { get; }

		string ChatId { get; }

		bool IsMuted { get; }

		double AudioEnergy { get; }

		bool SpeechDetected { get; }

		event global::System.Action OnMuteStateChanged;

		event global::System.Action OnSpeechDetected;

		event global::System.Action OnAudioEnergyChanged;

		void MuteLocally(bool mute);
	}
}
