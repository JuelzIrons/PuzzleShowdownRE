namespace Unity.Multiplayer.Widgets
{
	internal class ShowJoinCode : global::Unity.Multiplayer.Widgets.WidgetBehaviour, global::Unity.Multiplayer.Widgets.ISessionLifecycleEvents, global::Unity.Multiplayer.Widgets.ISessionProvider
	{
		private const string k_NoCode = "–";

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_Text m_Text;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Button m_CopyCodeButton;

		public global::Unity.Services.Multiplayer.ISession Session { get; set; }

		private void Start()
		{
			if (m_Text == null)
			{
				m_Text = GetComponentInChildren<global::TMPro.TMP_Text>();
			}
			if (m_CopyCodeButton == null)
			{
				m_CopyCodeButton = GetComponentInChildren<global::UnityEngine.UI.Button>();
			}
			m_CopyCodeButton.onClick.AddListener(CopySessionCodeToClipboard);
		}

		public override void OnServicesInitialized()
		{
			m_CopyCodeButton.interactable = false;
		}

		public void OnSessionJoined()
		{
			m_Text.text = Session?.Code ?? "–";
			m_CopyCodeButton.interactable = true;
		}

		public void OnSessionLeft()
		{
			m_Text.text = "–";
			m_CopyCodeButton.interactable = false;
		}

		private void CopySessionCodeToClipboard()
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
			string text = m_Text.text;
			if (Session?.Code != null && !string.IsNullOrEmpty(text))
			{
				global::UnityEngine.GUIUtility.systemCopyBuffer = text;
			}
		}
	}
}
