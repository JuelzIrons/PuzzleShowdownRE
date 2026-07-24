namespace Unity.Multiplayer.Widgets
{
	internal class EnterSessionBase : global::Unity.Multiplayer.Widgets.WidgetBehaviour, global::Unity.Multiplayer.Widgets.ISessionLifecycleEvents, global::Unity.Multiplayer.Widgets.ISessionProvider
	{
		[global::UnityEngine.Header("Widget Configuration")]
		[global::UnityEngine.Tooltip("General Widget Configuration.")]
		public global::Unity.Multiplayer.Widgets.WidgetConfiguration WidgetConfiguration;

		[global::UnityEngine.Header("Join Session Events")]
		[global::UnityEngine.Tooltip("Event invoked when the user is attempting to join a session.")]
		public global::UnityEngine.Events.UnityEvent JoiningSession = new global::UnityEngine.Events.UnityEvent();

		[global::UnityEngine.Tooltip("Event invoked when the user has successfully joined a session.")]
		public global::UnityEngine.Events.UnityEvent<global::Unity.Services.Multiplayer.ISession> JoinedSession = new global::UnityEngine.Events.UnityEvent<global::Unity.Services.Multiplayer.ISession>();

		[global::UnityEngine.Tooltip("Event invoked when the user has failed to join a session.")]
		public global::UnityEngine.Events.UnityEvent<global::Unity.Services.Multiplayer.SessionException> FailedToJoinSession = new global::UnityEngine.Events.UnityEvent<global::Unity.Services.Multiplayer.SessionException>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		protected global::UnityEngine.UI.Button m_EnterSessionButton;

		public global::Unity.Services.Multiplayer.ISession Session { get; set; }

		protected virtual void Awake()
		{
			if ((object)m_EnterSessionButton == null)
			{
				m_EnterSessionButton = GetComponentInChildren<global::UnityEngine.UI.Button>();
			}
			m_EnterSessionButton.onClick.AddListener(EnterSession);
			m_EnterSessionButton.interactable = false;
		}

		public override void OnServicesInitialized()
		{
			m_EnterSessionButton.interactable = true;
		}

		protected virtual void OnDestroy()
		{
			m_EnterSessionButton.onClick.RemoveListener(EnterSession);
		}

		public void OnSessionJoining()
		{
			JoiningSession?.Invoke();
			m_EnterSessionButton.interactable = false;
		}

		public void OnSessionFailedToJoin(global::Unity.Services.Multiplayer.SessionException sessionException)
		{
			FailedToJoinSession?.Invoke(sessionException);
			m_EnterSessionButton.interactable = true;
		}

		public void OnSessionJoined()
		{
			JoinedSession?.Invoke(Session);
			m_EnterSessionButton.interactable = Session == null;
		}

		public void OnSessionLeft()
		{
			m_EnterSessionButton.interactable = true;
		}

		protected virtual global::Unity.Multiplayer.Widgets.EnterSessionData GetSessionData()
		{
			return new global::Unity.Multiplayer.Widgets.EnterSessionData
			{
				SessionAction = global::Unity.Multiplayer.Widgets.SessionAction.Invalid
			};
		}

		protected async void EnterSession()
		{
			await global::Unity.Multiplayer.Widgets.LazySingleton<global::Unity.Multiplayer.Widgets.SessionManager>.Instance.EnterSession(GetSessionData());
		}
	}
}
