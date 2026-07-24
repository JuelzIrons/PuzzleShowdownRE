namespace Unity.Multiplayer.Widgets
{
	public class MultiplayerWidgetsSettings : global::UnityEngine.ScriptableObject
	{
		internal const string k_CustomserviceInitializationTooltip = "Enable if service initialization is handled manually.\n\nRequires initialization of UnityServices and Vivox (if used) and a signed-in user via Authentication.\n\nCall WidgetServiceInitialization.ServicesInitialized after initialization is finished.";

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Enable if service initialization is handled manually.\n\nRequires initialization of UnityServices and Vivox (if used) and a signed-in user via Authentication.\n\nCall WidgetServiceInitialization.ServicesInitialized after initialization is finished.")]
		private bool m_UseCustomServiceInitialization;

		internal bool UseCustomServiceInitialization
		{
			get
			{
				return m_UseCustomServiceInitialization;
			}
			set
			{
				m_UseCustomServiceInitialization = value;
			}
		}
	}
}
