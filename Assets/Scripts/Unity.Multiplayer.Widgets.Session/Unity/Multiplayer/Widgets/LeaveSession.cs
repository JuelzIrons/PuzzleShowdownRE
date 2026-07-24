namespace Unity.Multiplayer.Widgets
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.UI.Button))]
	internal class LeaveSession : global::Unity.Multiplayer.Widgets.WidgetBehaviour, global::Unity.Multiplayer.Widgets.ISessionLifecycleEvents, global::Unity.Multiplayer.Widgets.ISessionProvider
	{
		[global::UnityEngine.Serialization.FormerlySerializedAs("ExitedSession")]
		[global::UnityEngine.Tooltip("Event invoked when the user has successfully left a session.")]
		public global::UnityEngine.Events.UnityEvent SessionLeft = new global::UnityEngine.Events.UnityEvent();

		private global::UnityEngine.UI.Button m_Button;

		public global::Unity.Services.Multiplayer.ISession Session { get; set; }

		private void Start()
		{
			m_Button = GetComponent<global::UnityEngine.UI.Button>();
			m_Button.onClick.AddListener(Leave);
			SetButtonActive();
		}

		public void OnSessionLeft()
		{
			SessionLeft.Invoke();
			SetButtonActive();
		}

		public void OnSessionJoined()
		{
			SetButtonActive();
		}

		private void SetButtonActive()
		{
			m_Button.interactable = Session != null;
		}

		private async void Leave()
		{
			await global::Unity.Multiplayer.Widgets.LazySingleton<global::Unity.Multiplayer.Widgets.SessionManager>.Instance.LeaveSession();
		}
	}
}
