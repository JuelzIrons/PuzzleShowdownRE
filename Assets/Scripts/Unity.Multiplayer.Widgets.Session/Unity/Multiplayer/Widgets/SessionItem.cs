namespace Unity.Multiplayer.Widgets
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.UI.Selectable))]
	internal class SessionItem : global::UnityEngine.MonoBehaviour, global::UnityEngine.EventSystems.ISelectHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_Text m_SessionNameText;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_Text m_SessionPlayersText;

		public global::UnityEngine.Events.UnityEvent<global::Unity.Services.Multiplayer.ISessionInfo> OnSessionSelected;

		private global::Unity.Services.Multiplayer.ISessionInfo m_SessionInfo;

		public void SetSession(global::Unity.Services.Multiplayer.ISessionInfo sessionInfo)
		{
			m_SessionInfo = sessionInfo;
			SetSessionName(m_SessionInfo.Name);
			int currentPlayers = m_SessionInfo.MaxPlayers - m_SessionInfo.AvailableSlots;
			SetPlayers(currentPlayers, m_SessionInfo.MaxPlayers);
		}

		private void SetSessionName(string sessionName)
		{
			m_SessionNameText.text = sessionName;
		}

		private void SetPlayers(int currentPlayers, int maxPlayers)
		{
			m_SessionPlayersText.text = $"{currentPlayers}/{maxPlayers}";
		}

		public void OnSelect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			OnSessionSelected?.Invoke(m_SessionInfo);
		}
	}
}
