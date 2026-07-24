namespace Unity.Multiplayer.Widgets
{
	internal class WidgetDependencies
	{
		private static global::Unity.Multiplayer.Widgets.WidgetDependencies s_Instance;

		private global::Unity.Services.Authentication.IAuthenticationService m_AuthenticationService;

		private global::Unity.Multiplayer.Widgets.IServiceInitialization m_ServiceInitialization;

		private global::Unity.Services.Multiplayer.IMultiplayerService m_MultiplayerService;

		private global::Unity.Multiplayer.Widgets.IChatService m_ChatService;

		public static global::Unity.Multiplayer.Widgets.WidgetDependencies Instance => s_Instance ?? (s_Instance = new global::Unity.Multiplayer.Widgets.WidgetDependencies());

		public global::Unity.Services.Authentication.IAuthenticationService AuthenticationService
		{
			get
			{
				return m_AuthenticationService ?? (m_AuthenticationService = global::Unity.Services.Authentication.AuthenticationService.Instance);
			}
			set
			{
				m_AuthenticationService = value;
			}
		}

		public global::Unity.Multiplayer.Widgets.IServiceInitialization ServiceInitialization
		{
			get
			{
				return m_ServiceInitialization ?? (m_ServiceInitialization = new global::Unity.Multiplayer.Widgets.WidgetServiceInitializationInternal());
			}
			set
			{
				m_ServiceInitialization = value;
			}
		}

		public global::Unity.Services.Multiplayer.IMultiplayerService MultiplayerService
		{
			get
			{
				return m_MultiplayerService ?? (m_MultiplayerService = global::Unity.Services.Multiplayer.MultiplayerService.Instance);
			}
			set
			{
				m_MultiplayerService = value;
			}
		}

		public global::Unity.Multiplayer.Widgets.IChatService ChatService
		{
			get
			{
				return null;
			}
			set
			{
				m_ChatService = value;
			}
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			s_Instance = null;
		}
	}
}
