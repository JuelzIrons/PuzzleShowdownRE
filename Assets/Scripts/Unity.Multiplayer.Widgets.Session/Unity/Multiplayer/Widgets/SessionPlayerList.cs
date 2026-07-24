namespace Unity.Multiplayer.Widgets
{
	internal class SessionPlayerList : global::Unity.Multiplayer.Widgets.WidgetBehaviour, global::Unity.Multiplayer.Widgets.ISessionLifecycleEvents, global::Unity.Multiplayer.Widgets.ISessionProvider, global::Unity.Multiplayer.Widgets.IWidgetConfigurationProvider, global::Unity.Multiplayer.Widgets.ISessionEvents, global::Unity.Multiplayer.Widgets.IChatEvents
	{
		[global::UnityEngine.Header("Settings")]
		public bool HostCanKickPlayers = true;

		public bool PlayersCanBeMuted = true;

		[global::UnityEngine.Header("References")]
		[global::UnityEngine.Tooltip("The GameObject that will be instantiated for each player in the session.")]
		public global::UnityEngine.GameObject ListItem;

		[global::UnityEngine.Tooltip("The parent transform all ListItems will be instantiated under.")]
		public global::UnityEngine.Transform ContentRoot;

		private global::System.Collections.Generic.Dictionary<string, global::Unity.Multiplayer.Widgets.SessionPlayerListItem> m_PlayerListItems = new global::System.Collections.Generic.Dictionary<string, global::Unity.Multiplayer.Widgets.SessionPlayerListItem>();

		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.SessionPlayerListItem> m_CachedPlayerListItems = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.SessionPlayerListItem>();

		public global::Unity.Services.Multiplayer.ISession Session { get; set; }

		public global::Unity.Multiplayer.Widgets.WidgetConfiguration WidgetConfiguration { get; set; }

		protected override void OnEnable()
		{
			base.OnEnable();
			UpdatePlayerList();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			DisableAllPlayerListItems();
		}

		public void OnSessionLeft()
		{
			DisableAllPlayerListItems();
		}

		public void OnSessionJoined()
		{
			UpdatePlayerList();
		}

		public void OnPlayerJoinedSession(string playerId)
		{
			UpdatePlayerList();
		}

		public void OnPlayerLeftSession(string playerId)
		{
			if (m_PlayerListItems.TryGetValue(playerId, out var value))
			{
				value.Reset();
				value.gameObject.SetActive(value: false);
				m_CachedPlayerListItems.Add(value);
				m_PlayerListItems.Remove(playerId);
			}
		}

		private void UpdatePlayerList()
		{
			if (Session == null)
			{
				return;
			}
			foreach (global::Unity.Services.Multiplayer.IReadOnlyPlayer player in Session.Players)
			{
				string id = player.Id;
				if (!m_PlayerListItems.ContainsKey(id))
				{
					global::Unity.Multiplayer.Widgets.SessionPlayerListItem playerListItem = GetPlayerListItem(id);
					playerListItem.gameObject.SetActive(value: true);
					string playerName = "Unknown";
					if (player.Properties.TryGetValue("w_PlayerName", out var value))
					{
						playerName = value.Value;
					}
					global::Unity.Multiplayer.Widgets.SessionPlayerListItem.Configuration configuration = new global::Unity.Multiplayer.Widgets.SessionPlayerListItem.Configuration
					{
						HostCanKickPlayers = HostCanKickPlayers
					};
					global::Unity.Multiplayer.Widgets.WidgetConfiguration widgetConfiguration = WidgetConfiguration;
					configuration.PlayersCanBeMuted = (object)widgetConfiguration != null && widgetConfiguration.EnableVoiceChat && PlayersCanBeMuted;
					global::Unity.Multiplayer.Widgets.SessionPlayerListItem.Configuration configuration2 = configuration;
					playerListItem.Init(playerName, id, configuration2);
				}
			}
		}

		private global::Unity.Multiplayer.Widgets.SessionPlayerListItem GetPlayerListItem(string playerId)
		{
			if (m_PlayerListItems.TryGetValue(playerId, out var value))
			{
				return value;
			}
			if (m_CachedPlayerListItems.Count > 0)
			{
				value = m_CachedPlayerListItems[0];
				m_CachedPlayerListItems.RemoveAt(0);
			}
			else
			{
				value = global::UnityEngine.Object.Instantiate(ListItem, ContentRoot).GetComponent<global::Unity.Multiplayer.Widgets.SessionPlayerListItem>();
			}
			m_PlayerListItems.Add(playerId, value);
			return value;
		}

		private void DisableAllPlayerListItems()
		{
			foreach (global::Unity.Multiplayer.Widgets.SessionPlayerListItem value in m_PlayerListItems.Values)
			{
				value.Reset();
				value.gameObject.SetActive(value: false);
				m_CachedPlayerListItems.Add(value);
			}
			m_PlayerListItems.Clear();
		}

		public void OnPlayerAddedToChat(global::Unity.Multiplayer.Widgets.IChatParticipant participant)
		{
			if (PlayersCanBeMuted && m_PlayerListItems.TryGetValue(participant.Id, out var value))
			{
				value.ChatParticipant = participant;
				value.SetMuteButtonInteractable(WidgetConfiguration.EnableVoiceChat);
				value.SetVoiceIndicatorEnabled(WidgetConfiguration.EnableVoiceChat);
			}
		}
	}
}
