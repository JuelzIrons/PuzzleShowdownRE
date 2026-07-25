public class RelayManager : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_hostBtn;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_hostFirstBtn;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI joinCodeText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TMP_InputField joinCodeField;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject MenuCanvas;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject ConnectedHostCanvas;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject ConnectedClientCanvas;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject LoadingCanvas;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_lastErrorMsg;

	[global::UnityEngine.SerializeField]
	private MessageFader m_errorFader;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Used to send disconnect event from client to server")]
	private global::Unity.Netcode.NetworkObject m_recieverPrefab;

	private global::Unity.Services.Relay.Models.Allocation m_alloc;

	public static RelayManager Instance;

	public global::UnityEngine.SceneManagement.Scene MatchScene;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_selectCharCanvas;

	public CharacterSelectGrid CharGrid;

	private bool _exiting;

	private void Awake()
	{
		m_lastErrorMsg.text = "";
		if (Instance == null)
		{
			global::UnityEngine.Object.Destroy(Instance);
		}
		Instance = this;
		global::Unity.Netcode.NetworkManager.Singleton.OnTransportFailure += NetworkManager_OnTransportFailure;
		global::UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void NetworkManager_OnTransportFailure()
	{
		ReturnToMainMenu();
	}

	private async void Start()
	{
		await global::Unity.Services.Core.UnityServices.InitializeAsync();
		if (!global::Unity.Services.Authentication.AuthenticationService.Instance.IsSignedIn)
		{
			await global::Unity.Services.Authentication.AuthenticationService.Instance.SignInAnonymouslyAsync();
		}
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void DisconnectButton()
	{
		if (global::Unity.Netcode.NetworkManager.Singleton.IsHost)
		{
			global::System.Collections.Generic.IReadOnlyList<ulong> connectedClientsIds = global::Unity.Netcode.NetworkManager.Singleton.ConnectedClientsIds;
			for (int i = 0; i < connectedClientsIds.Count; i++)
			{
				if (connectedClientsIds[i] != global::Unity.Netcode.NetworkManager.Singleton.LocalClientId)
				{
					global::Unity.Netcode.NetworkManager.Singleton.DisconnectClient(connectedClientsIds[i]);
				}
			}
			global::Unity.Netcode.NetworkManager.Singleton.Shutdown();
		}
		else
		{
			LoadingCanvas.SetActive(value: true);
			NetworkServerReciever.Instance.DisconnectAsClientRpc(global::Unity.Netcode.NetworkManager.Singleton.LocalClientId);
		}
	}

	public void LocalDisconnect()
	{
		LoadingCanvas.SetActive(value: false);
		joinCodeField.text = "";
		joinCodeText.text = "";
		MenuCanvas.SetActive(value: true);
		ConnectedHostCanvas.SetActive(value: false);
		ConnectedClientCanvas.SetActive(value: false);
	}

	public async void StartRelay()
	{
		LoadingCanvas.SetActive(value: true);
		m_lastErrorMsg.text = "";
		string text = await StartHostWithRelay();
		global::Unity.Netcode.NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(m_recieverPrefab, 0uL);
		joinCodeField.text = text;
		joinCodeText.text = text;
		MenuCanvas.SetActive(value: false);
		ConnectedHostCanvas.SetActive(value: true);
		PersistentInputReader.Instance.CustomMenuCtrlSwapper.SetSelected(m_hostFirstBtn.GetComponent<global::UnityEngine.UI.Selectable>());
		LoadingCanvas.SetActive(value: false);
	}

	public void CopyJoinCodeToClipboard()
	{
		global::UnityEngine.GUIUtility.systemCopyBuffer = joinCodeText.text;
	}

	public async void JoinRelay()
	{
		LoadingCanvas.SetActive(value: true);
		m_lastErrorMsg.text = "";
		if (joinCodeField.text.Length < 6)
		{
			LoadingCanvas.SetActive(value: false);
			m_lastErrorMsg.text = "Join code too short";
			m_errorFader.DisplayMessage();
		}
		else
		{
			await StartClientWithRelay(joinCodeField.text.Substring(0, 6));
			LoadingCanvas.SetActive(value: false);
		}
	}

	private async global::System.Threading.Tasks.Task<string> StartHostWithRelay(int maxConnections = 4)
	{
		try
		{
			m_alloc = await global::Unity.Services.Relay.RelayService.Instance.CreateAllocationAsync(maxConnections);
		}
		catch
		{
			global::UnityEngine.Debug.LogError("Creating allocation failed!");
			m_lastErrorMsg.text = "Creating allocation failed!";
			m_errorFader.DisplayMessage();
			LoadingCanvas.SetActive(value: false);
			throw;
		}
		global::Unity.Netcode.NetworkManager.Singleton.GetComponent<global::Unity.Netcode.Transports.UTP.UnityTransport>().SetRelayServerData(global::Unity.Services.Relay.Models.AllocationUtils.ToRelayServerData(m_alloc, "wss"));
		string text = await global::Unity.Services.Relay.RelayService.Instance.GetJoinCodeAsync(m_alloc.AllocationId);
		return global::Unity.Netcode.NetworkManager.Singleton.StartHost() ? text : null;
	}

	private async global::System.Threading.Tasks.Task<bool> StartClientWithRelay(string joinCode)
	{
		global::Unity.Services.Relay.Models.JoinAllocation allocation;
		try
		{
			allocation = await global::Unity.Services.Relay.RelayService.Instance.JoinAllocationAsync(joinCode);
		}
		catch
		{
			m_lastErrorMsg.text = "Couldnt connect!";
			m_errorFader.DisplayMessage();
			LoadingCanvas.SetActive(value: false);
			return false;
		}
		global::Unity.Netcode.NetworkManager.Singleton.GetComponent<global::Unity.Netcode.Transports.UTP.UnityTransport>().SetRelayServerData(global::Unity.Services.Relay.Models.AllocationUtils.ToRelayServerData(allocation, "wss"));
		if (string.IsNullOrEmpty(joinCode) || !global::Unity.Netcode.NetworkManager.Singleton.StartClient())
		{
			m_lastErrorMsg.text = "Couldnt connect!";
			m_errorFader.DisplayMessage();
			LoadingCanvas.SetActive(value: false);
			return false;
		}
		global::System.Threading.Tasks.TaskCompletionSource<bool> tcs = new global::System.Threading.Tasks.TaskCompletionSource<bool>();
		global::Unity.Netcode.NetworkManager.Singleton.OnClientConnectedCallback += OnConnected;
		global::Unity.Netcode.NetworkManager.Singleton.OnClientDisconnectCallback += OnDisconnected;
		global::System.Threading.Tasks.Task obj2 = await global::System.Threading.Tasks.Task.WhenAny(tcs.Task, global::System.Threading.Tasks.Task.Delay(7000));
		global::Unity.Netcode.NetworkManager.Singleton.OnClientConnectedCallback -= OnConnected;
		global::Unity.Netcode.NetworkManager.Singleton.OnClientDisconnectCallback -= OnDisconnected;
		if (obj2 == tcs.Task && tcs.Task.Result)
		{
			MenuCanvas.SetActive(value: false);
			ConnectedClientCanvas.SetActive(value: true);
			return true;
		}
		global::Unity.Netcode.NetworkManager.Singleton.Shutdown();
		m_lastErrorMsg.text = "Couldnt connect!";
		m_errorFader.DisplayMessage();
		LoadingCanvas.SetActive(value: false);
		return false;
		void OnConnected(ulong clientId)
		{
			if (clientId == global::Unity.Netcode.NetworkManager.Singleton.LocalClientId)
			{
				tcs.TrySetResult(result: true);
			}
		}
		void OnDisconnected(ulong clientId)
		{
			if (clientId == global::Unity.Netcode.NetworkManager.Singleton.LocalClientId)
			{
				tcs.TrySetResult(result: false);
			}
		}
	}

	private void OnClientFailedToConnect(ulong clientId)
	{
		if (clientId == global::Unity.Netcode.NetworkManager.Singleton.LocalClientId)
		{
			global::Unity.Netcode.NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientFailedToConnect;
			if (!global::Unity.Netcode.NetworkManager.Singleton.IsConnectedClient)
			{
				m_lastErrorMsg.text = "Host not found, returning to menu.";
				m_errorFader.DisplayMessage();
				ReturnToMainMenu();
			}
		}
	}

	private void OnLevelWasLoaded(int level)
	{
		if (level == 0)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnSceneLoaded(global::UnityEngine.SceneManagement.Scene scene, global::UnityEngine.SceneManagement.LoadSceneMode sceneMode)
	{
		if (sceneMode == global::UnityEngine.SceneManagement.LoadSceneMode.Additive && scene.name == "MP2P")
		{
			MatchScene = scene;
			global::UnityEngine.SceneManagement.SceneManager.SetActiveScene(MatchScene);
		}
	}

	public void SetupCharSelectScreenLocally()
	{
		if (!m_selectCharCanvas.activeSelf)
		{
			m_selectCharCanvas.SetActive(value: true);
			CharacterSelectGrid componentInChildren = m_selectCharCanvas.GetComponentInChildren<CharacterSelectGrid>();
			LocallySubscribeToCharSelectCursor(componentInChildren);
		}
	}

	private void LocallySubscribeToCharSelectCursor(CharacterSelectGrid m_selectGrid)
	{
		PersistentInputReader.Instance.AllowPausing = false;
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.KeyboardSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance2.KeyboardSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance3.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
		PersistentInputReader instance4 = PersistentInputReader.Instance;
		instance4.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance4.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
		PersistentInputReader instance5 = PersistentInputReader.Instance;
		instance5.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance5.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
		PersistentInputReader instance6 = PersistentInputReader.Instance;
		instance6.KeyboardSpecialEnterAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance6.KeyboardSpecialEnterAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
		PersistentInputReader instance7 = PersistentInputReader.Instance;
		instance7.KeyboardAltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance7.KeyboardAltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
		PersistentInputReader instance8 = PersistentInputReader.Instance;
		instance8.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance8.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
		PersistentInputReader instance9 = PersistentInputReader.Instance;
		instance9.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance9.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
		PersistentInputReader instance10 = PersistentInputReader.Instance;
		instance10.Controller1AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance10.Controller1AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
		PersistentInputReader instance11 = PersistentInputReader.Instance;
		instance11.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance11.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
		PersistentInputReader instance12 = PersistentInputReader.Instance;
		instance12.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance12.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
		PersistentInputReader instance13 = PersistentInputReader.Instance;
		instance13.Controller2AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance13.Controller2AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: false);
		m_selectGrid.ALLREADYACTION = (global::System.Action)global::System.Delegate.Combine(m_selectGrid.ALLREADYACTION, new global::System.Action(AllPlayersHaveSelected));
	}

	public void AllPlayersHaveSelected()
	{
		CharacterSelectGrid charGrid = CharGrid;
		charGrid.ALLREADYACTION = (global::System.Action)global::System.Delegate.Remove(charGrid.ALLREADYACTION, new global::System.Action(AllPlayersHaveSelected));
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: true);
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.MoveCursorP1));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.KeyboardSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance2.KeyboardSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.SelectP1));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance3.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.SelectP1));
		PersistentInputReader instance4 = PersistentInputReader.Instance;
		instance4.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance4.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.SelectP1));
		PersistentInputReader instance5 = PersistentInputReader.Instance;
		instance5.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance5.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.SelectP1));
		PersistentInputReader instance6 = PersistentInputReader.Instance;
		instance6.KeyboardAltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance6.KeyboardAltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.DeselectP1));
		PersistentInputReader instance7 = PersistentInputReader.Instance;
		instance7.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance7.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.MoveCursorP1));
		PersistentInputReader instance8 = PersistentInputReader.Instance;
		instance8.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance8.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.SelectP1));
		PersistentInputReader instance9 = PersistentInputReader.Instance;
		instance9.Controller1AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance9.Controller1AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.DeselectP1));
		PersistentInputReader instance10 = PersistentInputReader.Instance;
		instance10.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance10.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.MoveCursorP1));
		PersistentInputReader instance11 = PersistentInputReader.Instance;
		instance11.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance11.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.SelectP1));
		PersistentInputReader instance12 = PersistentInputReader.Instance;
		instance12.Controller2AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance12.Controller2AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.DeselectP1));
		PersistentInputReader instance13 = PersistentInputReader.Instance;
		instance13.KeyboardSpecialEnterAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance13.KeyboardSpecialEnterAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CharGrid.SelectP1));
		CharGrid.enabled = false;
		global::UnityEngine.EventSystems.EventSystem.current.enabled = false;
		SceneLoader.Instance.DoBlackFade(1f, 4f, 0.5f, muteSound: true, delegate
		{
			m_selectCharCanvas.SetActive(value: false);
			if (NetworkServerReciever.Instance.IsHost)
			{
				LoadGame();
			}
		}, null, 1f);
	}

	public void LoadGame()
	{
		if (global::Unity.Netcode.NetworkManager.Singleton.ConnectedClients.Count != 2)
		{
			m_lastErrorMsg.text = "Not enough players connected to start game!";
			m_errorFader.DisplayMessage();
		}
		else if (NetworkServerReciever.Instance.HostCharacter.Value == -1)
		{
			m_lastErrorMsg.text = "Please Select a character to start the game";
			m_errorFader.DisplayMessage();
		}
		else if (NetworkServerReciever.Instance.ClientCharacter.Value == -1)
		{
			m_lastErrorMsg.text = "Waiting for other player to select a character";
			m_errorFader.DisplayMessage();
		}
		else
		{
			NetworkServerReciever.Instance.NetworkedRandomSeed.Value = global::UnityEngine.Random.Range(0, 2147483645);
			Invoke("LoadDelayed", 2f);
		}
	}

	private void LoadDelayed()
	{
		if (global::Unity.Netcode.NetworkManager.Singleton.ConnectedClients.Count != 2)
		{
			m_lastErrorMsg.text = "Not enough players connected to start game!";
			m_errorFader.DisplayMessage();
			return;
		}
		global::Unity.Netcode.NetworkManager.Singleton.SceneManager.LoadScene("MP2P", global::UnityEngine.SceneManagement.LoadSceneMode.Additive);
		NetworkServerReciever.Instance.CloseConnectionScreenForAllClientRpc();
		PersistentInputReader.Instance.SetSelfUpdate(selfUpdating: true);
		NetworkServerReciever.Instance.HostHasLaunchedScene.Value = true;
		global::UnityEngine.Debug.Log("LOADED NEW MATCH!");
	}

	public void ReturnToMainMenu()
	{
		if (_exiting)
		{
			return;
		}
		_exiting = true;
		if (global::Unity.Netcode.NetworkManager.Singleton.IsHost || global::Unity.Netcode.NetworkManager.Singleton.IsConnectedClient)
		{
			global::Unity.Netcode.NetworkManager.Singleton.OnServerStopped += OnNetworkStopped;
			global::Unity.Netcode.NetworkManager.Singleton.OnClientStopped += OnNetworkStopped;
			if (global::Unity.Netcode.NetworkManager.Singleton.IsHost)
			{
				global::Unity.Netcode.NetworkManager.Singleton.Shutdown();
				return;
			}
			if (NetworkServerReciever.Instance != null)
			{
				NetworkServerReciever.Instance.DisconnectAsClientRpc(global::Unity.Netcode.NetworkManager.Singleton.LocalClientId);
			}
			global::Unity.Netcode.NetworkManager.Singleton.Shutdown();
		}
		else
		{
			FinishReturnToMenu(noUnload: true);
		}
	}

	private void OnNetworkStopped(bool wasHost)
	{
		global::Unity.Netcode.NetworkManager.Singleton.OnServerStopped -= OnNetworkStopped;
		global::Unity.Netcode.NetworkManager.Singleton.OnClientStopped -= OnNetworkStopped;
		FinishReturnToMenu();
	}

	private void FinishReturnToMenu(bool noUnload = false)
	{
		m_alloc = null;
		if (TallyManager.Instance != null)
		{
			global::UnityEngine.Object.Destroy(TallyManager.Instance.gameObject);
		}
		global::UnityEngine.Time.timeScale = 1f;
		GameManager.Instance.OutOfGamemodeDestroy();
		if (noUnload)
		{
			SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.MultiplayerMenu);
		}
		else
		{
			SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.MultiplayerMenu, AllGameScenes.Matchmake);
		}
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	public void LocallyCloseLobbyCanvas(bool isHost = false)
	{
		ConnectedClientCanvas.SetActive(value: false);
		ConnectedHostCanvas.SetActive(value: false);
	}

	public void LocallyOpenLobbyCanvas(bool isHost = false)
	{
		ConnectedClientCanvas.SetActive(!isHost);
		ConnectedHostCanvas.SetActive(isHost);
	}
}
