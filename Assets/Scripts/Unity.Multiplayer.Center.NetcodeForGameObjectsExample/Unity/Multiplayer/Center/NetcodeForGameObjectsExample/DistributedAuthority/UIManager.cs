namespace Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority
{
	public class UIManager : global::UnityEngine.MonoBehaviour
	{
		private global::Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority.ConnectionManager m_ConnectionManager;

		private global::UnityEngine.UIElements.VisualElement m_Root;

		private global::UnityEngine.UIElements.TextField m_PlayerNameField;

		private global::UnityEngine.UIElements.TextField m_SessionNameField;

		private global::UnityEngine.UIElements.VisualElement m_LoginUI;

		private global::UnityEngine.UIElements.VisualElement m_InGameUI;

		private global::UnityEngine.UIElements.Button m_LoginButton;

		private void Start()
		{
			global::UnityEngine.UIElements.UIDocument component = GetComponent<global::UnityEngine.UIElements.UIDocument>();
			m_ConnectionManager = GetComponent<global::Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority.ConnectionManager>();
			m_Root = component.rootVisualElement;
			m_LoginUI = global::UnityEngine.UIElements.UQueryExtensions.Q<global::UnityEngine.UIElements.VisualElement>(m_Root, "login");
			m_InGameUI = global::UnityEngine.UIElements.UQueryExtensions.Q<global::UnityEngine.UIElements.VisualElement>(m_Root, "ingame");
			m_PlayerNameField = global::UnityEngine.UIElements.UQueryExtensions.Q<global::UnityEngine.UIElements.TextField>(m_Root, "player-name");
			m_SessionNameField = global::UnityEngine.UIElements.UQueryExtensions.Q<global::UnityEngine.UIElements.TextField>(m_Root, "session-name");
			m_LoginButton = global::UnityEngine.UIElements.UQueryExtensions.Q<global::UnityEngine.UIElements.Button>(m_Root, "login-button");
			m_LoginButton.clicked += async delegate
			{
				await m_ConnectionManager.CreateOrJoinSessionAsync(m_SessionNameField.value, m_PlayerNameField.value);
			};
			global::UnityEngine.UIElements.UQueryExtensions.Q<global::UnityEngine.UIElements.Button>(m_Root, "logout-button").clicked += m_ConnectionManager.Disconnect;
		}

		private void Update()
		{
			m_LoginButton.enabledSelf = !string.IsNullOrWhiteSpace(m_PlayerNameField.value) && !string.IsNullOrWhiteSpace(m_SessionNameField.value);
			m_LoginUI.style.display = ((m_ConnectionManager.State != global::Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority.ConnectionManager.ConnectionState.Disconnected) ? global::UnityEngine.UIElements.DisplayStyle.None : global::UnityEngine.UIElements.DisplayStyle.Flex);
			m_InGameUI.style.display = ((m_ConnectionManager.State != global::Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority.ConnectionManager.ConnectionState.Connected) ? global::UnityEngine.UIElements.DisplayStyle.None : global::UnityEngine.UIElements.DisplayStyle.Flex);
		}
	}
}
