namespace Unity.Multiplayer.Widgets
{
	internal class WidgetEventDispatcher : global::Unity.Multiplayer.Widgets.LazySingleton<global::Unity.Multiplayer.Widgets.WidgetEventDispatcher>
	{
		internal global::UnityEngine.Events.UnityEvent OnServicesInitializedEvent = new global::UnityEngine.Events.UnityEvent();

		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.IWidget> m_Widgets = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.IWidget>();

		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.ISessionProvider> m_SessionProviders = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.ISessionProvider>();

		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.IWidgetConfigurationProvider> m_WidgetConfigurationProviders = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.IWidgetConfigurationProvider>();

		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.ISessionLifecycleEvents> m_SessionLifecycleListeners = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.ISessionLifecycleEvents>();

		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.ISessionEvents> m_SessionEventListeners = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.ISessionEvents>();

		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.IChatParticipantEvents> m_VoiceChatParticipantEvents = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.IChatParticipantEvents>();

		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.IChatEvents> m_ChatEvents = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Widgets.IChatEvents>();

		private global::Unity.Services.Multiplayer.ISession m_Session;

		private global::Unity.Multiplayer.Widgets.WidgetConfiguration m_WidgetConfiguration;

		internal void OnServicesInitialized()
		{
			for (int num = m_Widgets.Count - 1; num >= 0; num--)
			{
				global::Unity.Multiplayer.Widgets.IWidget widget = m_Widgets[num];
				widget.IsInitialized = true;
				widget.OnServicesInitialized();
			}
			OnServicesInitializedEvent?.Invoke();
		}

		internal void RegisterWidget(global::Unity.Multiplayer.Widgets.IWidget widget)
		{
			if (!global::Unity.Multiplayer.Widgets.ManagerFactory.IsInitialized)
			{
				global::Unity.Multiplayer.Widgets.ManagerFactory.Initialize();
			}
			if (widget is global::Unity.Multiplayer.Widgets.ISessionProvider sessionProvider)
			{
				m_SessionProviders.Add(sessionProvider);
				sessionProvider.Session = m_Session;
			}
			if (widget is global::Unity.Multiplayer.Widgets.IWidgetConfigurationProvider widgetConfigurationProvider)
			{
				m_WidgetConfigurationProviders.Add(widgetConfigurationProvider);
				widgetConfigurationProvider.WidgetConfiguration = m_WidgetConfiguration;
			}
			if (widget is global::Unity.Multiplayer.Widgets.ISessionLifecycleEvents item)
			{
				m_SessionLifecycleListeners.Add(item);
			}
			if (widget is global::Unity.Multiplayer.Widgets.ISessionEvents item2)
			{
				m_SessionEventListeners.Add(item2);
			}
			if (widget is global::Unity.Multiplayer.Widgets.IChatParticipantEvents item3)
			{
				m_VoiceChatParticipantEvents.Add(item3);
			}
			if (widget is global::Unity.Multiplayer.Widgets.IChatEvents item4)
			{
				m_ChatEvents.Add(item4);
			}
			m_Widgets.Add(widget);
			widget.IsInitialized = global::Unity.Multiplayer.Widgets.WidgetServiceInitialization.IsInitialized;
			if (global::Unity.Multiplayer.Widgets.WidgetServiceInitialization.IsInitialized)
			{
				widget.OnServicesInitialized();
			}
		}

		internal void UnregisterWidget(global::Unity.Multiplayer.Widgets.IWidget widget)
		{
			if (widget is global::Unity.Multiplayer.Widgets.ISessionProvider sessionProvider)
			{
				sessionProvider.Session = null;
				m_SessionProviders.Remove(sessionProvider);
			}
			if (widget is global::Unity.Multiplayer.Widgets.IWidgetConfigurationProvider widgetConfigurationProvider)
			{
				widgetConfigurationProvider.WidgetConfiguration = null;
				m_WidgetConfigurationProviders.Remove(widgetConfigurationProvider);
			}
			if (widget is global::Unity.Multiplayer.Widgets.ISessionLifecycleEvents item)
			{
				m_SessionLifecycleListeners.Remove(item);
			}
			if (widget is global::Unity.Multiplayer.Widgets.ISessionEvents item2)
			{
				m_SessionEventListeners.Remove(item2);
			}
			if (widget is global::Unity.Multiplayer.Widgets.IChatParticipantEvents item3)
			{
				m_VoiceChatParticipantEvents.Remove(item3);
			}
			if (widget is global::Unity.Multiplayer.Widgets.IChatEvents item4)
			{
				m_ChatEvents.Remove(item4);
			}
			m_Widgets.Remove(widget);
		}

		internal void OnSessionJoined(global::Unity.Services.Multiplayer.ISession session, global::Unity.Multiplayer.Widgets.WidgetConfiguration widgetConfiguration)
		{
			m_Session = session;
			m_WidgetConfiguration = widgetConfiguration;
			for (int num = m_SessionProviders.Count - 1; num >= 0; num--)
			{
				m_SessionProviders[num].Session = m_Session;
			}
			for (int num2 = m_WidgetConfigurationProviders.Count - 1; num2 >= 0; num2--)
			{
				m_WidgetConfigurationProviders[num2].WidgetConfiguration = m_WidgetConfiguration;
			}
			for (int num3 = m_SessionLifecycleListeners.Count - 1; num3 >= 0; num3--)
			{
				m_SessionLifecycleListeners[num3].OnSessionJoined();
			}
		}

		internal void OnSessionLeft()
		{
			m_Session = null;
			m_WidgetConfiguration = null;
			for (int num = m_SessionProviders.Count - 1; num >= 0; num--)
			{
				m_SessionProviders[num].Session = null;
			}
			for (int num2 = m_WidgetConfigurationProviders.Count - 1; num2 >= 0; num2--)
			{
				m_WidgetConfigurationProviders[num2].WidgetConfiguration = null;
			}
			for (int num3 = m_SessionLifecycleListeners.Count - 1; num3 >= 0; num3--)
			{
				m_SessionLifecycleListeners[num3].OnSessionLeft();
			}
		}

		internal void OnSessionJoining()
		{
			for (int num = m_SessionLifecycleListeners.Count - 1; num >= 0; num--)
			{
				m_SessionLifecycleListeners[num].OnSessionJoining();
			}
		}

		internal void OnSessionFailedToJoin(global::Unity.Services.Multiplayer.SessionException sessionException)
		{
			for (int num = m_SessionLifecycleListeners.Count - 1; num >= 0; num--)
			{
				m_SessionLifecycleListeners[num].OnSessionFailedToJoin(sessionException);
			}
		}

		internal void OnSessionChanged()
		{
			for (int num = m_SessionEventListeners.Count - 1; num >= 0; num--)
			{
				m_SessionEventListeners[num].OnSessionChanged();
			}
		}

		internal void OnSessionStateChanged(global::Unity.Services.Multiplayer.SessionState sessionState)
		{
			for (int num = m_SessionEventListeners.Count - 1; num >= 0; num--)
			{
				m_SessionEventListeners[num].OnSessionStateChanged(sessionState);
			}
		}

		internal void OnPlayerJoinedSession(string playerId)
		{
			for (int num = m_SessionEventListeners.Count - 1; num >= 0; num--)
			{
				m_SessionEventListeners[num].OnPlayerJoinedSession(playerId);
			}
		}

		internal void OnPlayerLeftSession(string playerId)
		{
			for (int num = m_SessionEventListeners.Count - 1; num >= 0; num--)
			{
				m_SessionEventListeners[num].OnPlayerLeftSession(playerId);
			}
		}

		internal void OnSessionPropertiesChanged()
		{
			for (int num = m_SessionEventListeners.Count - 1; num >= 0; num--)
			{
				m_SessionEventListeners[num].OnSessionPropertiesChanged();
			}
		}

		internal void OnPlayerPropertiesChanged()
		{
			for (int num = m_SessionEventListeners.Count - 1; num >= 0; num--)
			{
				m_SessionEventListeners[num].OnPlayerPropertiesChanged();
			}
		}

		internal void OnRemovedFromSession()
		{
			for (int num = m_SessionEventListeners.Count - 1; num >= 0; num--)
			{
				m_SessionEventListeners[num].OnRemovedFromSession();
			}
		}

		internal void OnSessionDeleted()
		{
			for (int num = m_SessionEventListeners.Count - 1; num >= 0; num--)
			{
				m_SessionEventListeners[num].OnSessionDeleted();
			}
		}

		internal void OnParticipantMuteStateChanged(string playerId, bool participantIsMuted)
		{
			for (int num = m_VoiceChatParticipantEvents.Count - 1; num >= 0; num--)
			{
				global::Unity.Multiplayer.Widgets.IChatParticipantEvents chatParticipantEvents = m_VoiceChatParticipantEvents[num];
				if (chatParticipantEvents.PlayerId == playerId)
				{
					chatParticipantEvents.OnPlayerMuteStateChanged(participantIsMuted);
				}
			}
		}

		internal void OnParticipantSpeechDetected(string playerId)
		{
			for (int num = m_VoiceChatParticipantEvents.Count - 1; num >= 0; num--)
			{
				global::Unity.Multiplayer.Widgets.IChatParticipantEvents chatParticipantEvents = m_VoiceChatParticipantEvents[num];
				if (chatParticipantEvents.PlayerId == playerId)
				{
					chatParticipantEvents.OnPlayerSpeechDetected();
				}
			}
		}

		internal void OnParticipantAudioEnergyChanged(string playerId)
		{
			for (int num = m_VoiceChatParticipantEvents.Count - 1; num >= 0; num--)
			{
				global::Unity.Multiplayer.Widgets.IChatParticipantEvents chatParticipantEvents = m_VoiceChatParticipantEvents[num];
				if (chatParticipantEvents.PlayerId == playerId)
				{
					chatParticipantEvents.OnPlayerAudioEnergyChanged();
				}
			}
		}

		internal void OnChatJoined(string channelId)
		{
			for (int num = m_ChatEvents.Count - 1; num >= 0; num--)
			{
				m_ChatEvents[num].OnChatJoined(channelId);
			}
		}

		internal void OnChatLeft(string channelId)
		{
			for (int num = m_ChatEvents.Count - 1; num >= 0; num--)
			{
				m_ChatEvents[num].OnChatLeft(channelId);
			}
		}

		internal void OnPlayerAddedToChat(global::Unity.Multiplayer.Widgets.IChatParticipant participant)
		{
			for (int num = m_ChatEvents.Count - 1; num >= 0; num--)
			{
				m_ChatEvents[num].OnPlayerAddedToChat(participant);
			}
		}

		internal void OnChatMessageReceived(global::Unity.Multiplayer.Widgets.IChatMessage chatMessage)
		{
			for (int num = m_ChatEvents.Count - 1; num >= 0; num--)
			{
				m_ChatEvents[num].OnChatMessageReceived(chatMessage);
			}
		}
	}
}
