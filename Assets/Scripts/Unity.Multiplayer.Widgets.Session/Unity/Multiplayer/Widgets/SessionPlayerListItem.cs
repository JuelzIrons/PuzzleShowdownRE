namespace Unity.Multiplayer.Widgets
{
	internal class SessionPlayerListItem : global::Unity.Multiplayer.Widgets.WidgetBehaviour, global::Unity.Multiplayer.Widgets.IChatParticipantEvents, global::Unity.Multiplayer.Widgets.IPlayerId, global::Unity.Multiplayer.Widgets.IChatEvents, global::Unity.Multiplayer.Widgets.ISessionEvents, global::Unity.Multiplayer.Widgets.ISessionProvider
	{
		internal struct Configuration
		{
			public bool HostCanKickPlayers;

			public bool PlayersCanBeMuted;
		}

		private bool m_IsLocalPlayer;

		private global::Unity.Multiplayer.Widgets.SessionPlayerListItem.Configuration m_Configuration;

		public global::TMPro.TMP_Text PlayerNameText;

		public global::UnityEngine.UI.Button KickButton;

		public global::UnityEngine.UI.Button MuteButton;

		public global::UnityEngine.UI.Image VoiceIndicator;

		[global::UnityEngine.Header("Voice Indicator Icons")]
		public global::UnityEngine.Sprite VoiceIndicatorNoSound;

		public global::UnityEngine.Sprite VoiceIndicatorLowSound;

		public global::UnityEngine.Sprite VoiceIndicatorHighSound;

		public global::UnityEngine.Sprite VoiceIndicatorNoAudio;

		public global::UnityEngine.Sprite VoiceIndicatorMuted;

		public global::Unity.Services.Multiplayer.ISession Session { get; set; }

		public string PlayerId { get; set; }

		public global::Unity.Multiplayer.Widgets.IChatParticipant ChatParticipant { get; set; }

		internal void Init(string playerName, string playerId, global::Unity.Multiplayer.Widgets.SessionPlayerListItem.Configuration configuration)
		{
			PlayerNameText.text = playerName;
			PlayerId = playerId;
			m_Configuration = configuration;
			m_IsLocalPlayer = playerId == Session.CurrentPlayer.Id;
			ShowKickButtonIfConditionsAreMet();
			MuteButton.gameObject.SetActive(m_Configuration.PlayersCanBeMuted);
			SetMuteButtonInteractable(isInteractable: false);
			SetVoiceIndicatorEnabled(isEnabled: false);
			KickButton.onClick.AddListener(OnKickButtonClicked);
			MuteButton.onClick.AddListener(OnMuteButtonClicked);
		}

		internal void Reset()
		{
			KickButton.onClick.RemoveListener(OnKickButtonClicked);
			MuteButton.onClick.RemoveListener(OnMuteButtonClicked);
			PlayerId = null;
			ChatParticipant = null;
		}

		private void ShowKickButtonIfConditionsAreMet()
		{
			KickButton.gameObject.SetActive(m_Configuration.HostCanKickPlayers && Session.IsHost && !m_IsLocalPlayer);
		}

		public void OnSessionChanged()
		{
			ShowKickButtonIfConditionsAreMet();
		}

		public void OnChatJoined(string chatId)
		{
			SetVoiceIndicatorEnabled(isEnabled: false);
		}

		public void OnChatLeft(string chatId)
		{
			SetVoiceIndicatorEnabled(isEnabled: false);
		}

		internal void SetMuteButtonInteractable(bool isInteractable)
		{
			MuteButton.interactable = isInteractable;
		}

		internal void SetVoiceIndicatorEnabled(bool isEnabled)
		{
			VoiceIndicator.enabled = isEnabled;
		}

		private void OnKickButtonClicked()
		{
			global::Unity.Multiplayer.Widgets.LazySingleton<global::Unity.Multiplayer.Widgets.SessionManager>.Instance.KickPlayer(PlayerId);
		}

		private void OnMuteButtonClicked()
		{
			if (ChatParticipant == null)
			{
				global::UnityEngine.Debug.LogWarning("No Player is assigned to this PlayerListItem (ChatParticipant == null). Cannot mute player.");
				return;
			}
			bool flag = ChatParticipant?.IsMuted ?? false;
			ChatParticipant?.MuteLocally(!flag);
			OnPlayerMuteStateChanged(!flag);
		}

		public void OnPlayerMuteStateChanged(bool isMuted)
		{
			MuteButton.GetComponentInChildren<global::TMPro.TMP_Text>().text = (isMuted ? "Unmute" : "Mute");
			VoiceIndicator.sprite = (isMuted ? VoiceIndicatorMuted : VoiceIndicatorNoSound);
		}

		public void OnPlayerSpeechDetected()
		{
			if (!ChatParticipant.IsMuted)
			{
				VoiceIndicator.sprite = VoiceIndicatorHighSound;
			}
		}

		public void OnPlayerAudioEnergyChanged()
		{
			if (!ChatParticipant.IsMuted)
			{
				if (!ChatParticipant.SpeechDetected)
				{
					VoiceIndicator.sprite = VoiceIndicatorNoSound;
				}
				else if (RemapAudioEnergy(ChatParticipant.AudioEnergy) >= 0.1)
				{
					VoiceIndicator.sprite = VoiceIndicatorHighSound;
				}
			}
		}

		private static double RemapAudioEnergy(double audioEnergy)
		{
			return (audioEnergy - 0.4) / 0.19999999999999996;
		}
	}
}
