namespace Unity.Multiplayer.Center.NetcodeForGameObjectsExample
{
	public class TemporaryUI : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Button m_StartHostButton;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Button m_StartClientButton;

		private void Awake()
		{
			if (!global::UnityEngine.Object.FindAnyObjectByType<global::UnityEngine.EventSystems.EventSystem>())
			{
				global::System.Type typeFromHandle = typeof(global::UnityEngine.EventSystems.StandaloneInputModule);
				typeFromHandle = typeof(global::UnityEngine.InputSystem.UI.InputSystemUIInputModule);
				new global::UnityEngine.GameObject("EventSystem", typeof(global::UnityEngine.EventSystems.EventSystem), typeFromHandle).transform.SetParent(base.transform);
			}
		}

		private void Start()
		{
			m_StartHostButton.onClick.AddListener(StartHost);
			m_StartClientButton.onClick.AddListener(StartClient);
		}

		private void StartClient()
		{
			global::Unity.Netcode.NetworkManager.Singleton.StartClient();
			DeactivateButtons();
		}

		private void StartHost()
		{
			global::Unity.Netcode.NetworkManager.Singleton.StartHost();
			DeactivateButtons();
		}

		private void DeactivateButtons()
		{
			m_StartHostButton.interactable = false;
			m_StartClientButton.interactable = false;
		}
	}
}
