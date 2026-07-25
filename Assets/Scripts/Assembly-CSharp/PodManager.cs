public class PodManager : global::UnityEngine.MonoBehaviour
{
	public GameLoop GameLoop;

	public GridManager GridManager;

	public AudioManager AudioManager;

	public CursorController CursorController;

	public HUD HUD;

	public FinalStatScreen FinalStatScreen;

	public CPUInput CPUInput;

	public CPUSearch CPUSearch;

	public CPUAct CPUAct;

	public PodManager OpponentManager;

	public VisualGarbageQueue VisualGarbageQueue;

	public bool IsControlledByCPU;

	public bool IsOnlineMp;

	public bool WillLogCpuMoves;

	public DebugInputReader DebugInputReader;

	public global::UnityEngine.Material MaterialTemplate;

	public int SpecificInputType;

	public bool ISPLAYER1;

	private global::UnityEngine.Material ActualMaterial;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.SpriteRenderer> m_allSprites;

	[global::UnityEngine.SerializeField]
	private DataHolder m_dataholder;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshPro m_countDownText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.SpriteRenderer m_innerBannerRend;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_newHiscoreObj;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip[] m_countdownClips;

	public global::UnityEngine.GameObject FirstButtonAfterGAMEOVER;

	public bool TheOneThatExecutesInputSystemUpdates;

	public global::System.Random RandomManager;

	public int Seed;

	public bool HasStarted;

	public bool ISScenarioCreator;

	private CharacterData m_characterData;

	private int countdownOnceIndex;

	public bool IsVersus => OpponentManager != null;

	private void Start()
	{
		AudioManager = AudioManager.Instance;
		HasStarted = false;
		if (IsOnlineMp && !ISPLAYER1)
		{
			NetworkServerReciever.Instance.LocalOpponentPodManager = this;
		}
		else if (IsOnlineMp && ISPLAYER1)
		{
			NetworkServerReciever.Instance.LocalPlayerPodManager = this;
		}
		if (ISScenarioCreator)
		{
			Setup();
			StartGame();
		}
		if (!ISScenarioCreator && GameManager.Instance.DefinedGameMode == GameModeType.LocalMp)
		{
			if (ISPLAYER1)
			{
				SetSelectedCharacter(GameManager.Instance.LocallySelectedCharacter);
				SpecificInputType = GameManager.Instance.P1InputScheme;
			}
			else
			{
				SetSelectedCharacter(GameManager.Instance.LocallySelectedP2Character);
				SpecificInputType = GameManager.Instance.P2InputScheme;
			}
			Setup();
			StartGame();
		}
		else
		{
			if (GameManager.Instance.DefinedGameMode == GameModeType.LineClear)
			{
				return;
			}
			if (GameManager.Instance.DefinedGameMode == GameModeType.Online)
			{
				Setup();
				StartGame();
			}
			else if (GameManager.Instance.DefinedGameMode != GameModeType.Campaign)
			{
				if (GameManager.Instance.DefinedGameMode == GameModeType.Marathon)
				{
					SetSelectedCharacter(GameManager.Instance.LocallySelectedCharacter);
				}
				global::UnityEngine.Debug.Log("Started game from start method");
				Setup();
				StartGame();
			}
		}
	}

	public void SetAiDifficulty(int difficulty)
	{
		CPUAct.CPULevel = difficulty;
	}

	private void SubscribeToAllPlayerInputSingleplayer()
	{
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: true);
		if (ISPLAYER1)
		{
			PersistentInputReader.Instance.ClearAllSubscriptions();
			PersistentInputReader instance = PersistentInputReader.Instance;
			instance.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryMove));
			PersistentInputReader instance2 = PersistentInputReader.Instance;
			instance2.KeyboardSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance2.KeyboardSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance3 = PersistentInputReader.Instance;
			instance3.KeyboardAltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance3.KeyboardAltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance4 = PersistentInputReader.Instance;
			instance4.KeyboardForceUpAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance4.KeyboardForceUpAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryForceSpeedup));
			PersistentInputReader instance5 = PersistentInputReader.Instance;
			instance5.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance5.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryMove));
			PersistentInputReader instance6 = PersistentInputReader.Instance;
			instance6.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance6.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance7 = PersistentInputReader.Instance;
			instance7.Controller1AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance7.Controller1AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance8 = PersistentInputReader.Instance;
			instance8.Controller1ForceUpAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance8.Controller1ForceUpAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryForceSpeedup));
			PersistentInputReader instance9 = PersistentInputReader.Instance;
			instance9.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance9.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryMove));
			PersistentInputReader instance10 = PersistentInputReader.Instance;
			instance10.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance10.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance11 = PersistentInputReader.Instance;
			instance11.Controller2AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance11.Controller2AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance12 = PersistentInputReader.Instance;
			instance12.Controller2ForceUpAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance12.Controller2ForceUpAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryForceSpeedup));
		}
	}

	private void SubscribeToSpecificInput()
	{
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: true);
		if (SpecificInputType == 0)
		{
			PersistentInputReader instance = PersistentInputReader.Instance;
			instance.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryMove));
			PersistentInputReader instance2 = PersistentInputReader.Instance;
			instance2.KeyboardSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance2.KeyboardSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance3 = PersistentInputReader.Instance;
			instance3.KeyboardAltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance3.KeyboardAltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance4 = PersistentInputReader.Instance;
			instance4.KeyboardForceUpAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance4.KeyboardForceUpAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryForceSpeedup));
		}
		else if (SpecificInputType == 1)
		{
			PersistentInputReader instance5 = PersistentInputReader.Instance;
			instance5.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance5.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryMove));
			PersistentInputReader instance6 = PersistentInputReader.Instance;
			instance6.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance6.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance7 = PersistentInputReader.Instance;
			instance7.Controller1AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance7.Controller1AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance8 = PersistentInputReader.Instance;
			instance8.Controller1ForceUpAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance8.Controller1ForceUpAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryForceSpeedup));
		}
		else if (SpecificInputType == 2)
		{
			PersistentInputReader instance9 = PersistentInputReader.Instance;
			instance9.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance9.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryMove));
			PersistentInputReader instance10 = PersistentInputReader.Instance;
			instance10.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance10.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance11 = PersistentInputReader.Instance;
			instance11.Controller2AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance11.Controller2AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.SwapButton));
			PersistentInputReader instance12 = PersistentInputReader.Instance;
			instance12.Controller2ForceUpAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance12.Controller2ForceUpAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(CursorController.TryForceSpeedup));
		}
	}

	public void Setup()
	{
		countdownOnceIndex = 0;
		if (!ISScenarioCreator)
		{
			if (GameManager.Instance.DefinedGameMode == GameModeType.LocalMp)
			{
				SubscribeToSpecificInput();
			}
			else
			{
				SubscribeToAllPlayerInputSingleplayer();
			}
		}
		GameLoop.MyPodManager = this;
		GridManager.MyPodManager = this;
		CursorController.MyPodManager = this;
		HUD.MyPodManager = this;
		FinalStatScreen.MyPodManager = this;
		CPUInput.MyPodManager = this;
		CPUSearch.MyPodManager = this;
		CPUAct.MyPodManager = this;
		VisualGarbageQueue.MyPodManager = this;
		CPUSearch.OnGameStarted();
		if (!IsOnlineMp)
		{
			RandomManager = new global::System.Random();
		}
		else
		{
			InitRandom();
			NetworkServerReciever.Instance.ClearAllQueues();
			NetworkServerReciever.Instance.LocalOpponentHasDied = false;
			NetworkServerReciever.Instance.ForceTrueThisFrame = false;
			NetworkServerReciever.Instance.LocalTS = 0u;
			NetworkServerReciever.Instance.StartTimer();
		}
		if (OpponentManager != null)
		{
			HUD.ToggleSinglePlayer(isOn: false);
			FinalStatScreen.Set2Pmode();
		}
		else
		{
			HUD.ToggleSinglePlayer(isOn: true);
		}
		if (!ISScenarioCreator && GameManager.Instance.DefinedGameMode == GameModeType.LineClear)
		{
			HUD.DisplayStage($"{LineClearingManager.GetLevelNumber(LineClearingManager.Instance.CurrentLevelDataSO.name)}-{LineClearingManager.Instance.CurrentSublevelNumber + 1}");
		}
		GameLoop.Setup();
		CursorController.Setup();
	}

	public void StartGame()
	{
		HasStarted = true;
	}

	public void SetSelectedCharacter(CharacterType playableChar)
	{
		for (int i = 0; i < m_dataholder.AllCharacterData.Count; i++)
		{
			if (m_dataholder.AllCharacterData[i].Type == playableChar)
			{
				m_characterData = m_dataholder.AllCharacterData[i];
			}
		}
		GameLoop.VoiceBox = m_characterData.VoiceBoxSO;
		ActualMaterial = new global::UnityEngine.Material(MaterialTemplate);
		for (int j = 0; j < m_allSprites.Count; j++)
		{
			m_allSprites[j].material = ActualMaterial;
		}
		ActualMaterial.SetColor("_TargetColor", m_characterData.PodColorShiftValue);
		global::UnityEngine.ColorUtility.TryParseHtmlString("#959595", out var color);
		if (ISPLAYER1)
		{
			m_innerBannerRend.sprite = m_characterData.CharacterInnerBannerP1;
			
		}
		else
		{
			m_innerBannerRend.sprite = m_characterData.CharacterInnerBannerP2;
			
		}
	}

	private void Update()
	{
	}

	public void ShowNewHighscoreUI()
	{
		if (!(m_newHiscoreObj == null))
		{
			m_newHiscoreObj.SetActive(value: true);
		}
	}

	public void InitRandom()
	{
		if (NetworkServerReciever.Instance.IsHost)
		{
			if (ISPLAYER1)
			{
				RandomManager = new global::System.Random(NetworkServerReciever.Instance.NetworkedRandomSeed.Value);
			}
			else
			{
				RandomManager = new global::System.Random(NetworkServerReciever.Instance.NetworkedRandomSeed.Value + 1);
			}
		}
		else if (ISPLAYER1)
		{
			RandomManager = new global::System.Random(NetworkServerReciever.Instance.NetworkedRandomSeed.Value + 1);
		}
		else
		{
			RandomManager = new global::System.Random(NetworkServerReciever.Instance.NetworkedRandomSeed.Value);
		}
	}

	public void GoToMenu()
	{
		if (IsOnlineMp)
		{
			if (GameManager.Instance.DefinedGameMode == GameModeType.Online)
			{
				global::UnityEngine.Object.Destroy(TallyManager.Instance.gameObject);
			}
			RelayManager.Instance.ReturnToMainMenu();
		}
		else
		{
			if (GameManager.Instance.DefinedGameMode == GameModeType.LocalMp)
			{
				global::UnityEngine.Object.Destroy(TallyManager.Instance.gameObject);
			}
			GameManager.Instance.LoadMenu();
		}
	}

	public void PlayAgain()
	{
		SceneLoader.Instance.LoadSceneByActiveGameModeRegularFade();
	}

	public void Lose()
	{
		GameLoop.LoseLogo.enabled = true;
		if (GameLoop.m_pressAnyKeyToContinueObj != null)
		{
			GameLoop.m_pressAnyKeyToContinueObj.SetActive(value: true);
		}
		if (FirstButtonAfterGAMEOVER != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(FirstButtonAfterGAMEOVER);
		}
	}

	public void Win()
	{
		GameLoop.WinLogo.enabled = true;
		if (GameLoop.m_pressAnyKeyToContinueObj != null)
		{
			GameLoop.m_pressAnyKeyToContinueObj.SetActive(value: true);
		}
	}

	public void Draw()
	{
		GameLoop.GameOverLogo.enabled = true;
		if (GameLoop.m_pressAnyKeyToContinueObj != null)
		{
			GameLoop.m_pressAnyKeyToContinueObj.SetActive(value: true);
		}
	}

	public void HostRematch()
	{
		if (GameManager.Instance.DefinedGameMode == GameModeType.LocalMp)
		{
			PersistentInputReader.Instance.SetSelfUpdate(selfUpdating: true);
			global::UnityEngine.SceneManagement.SceneManager.LoadScene("2P");
		}
		else if (NetworkServerReciever.Instance != null && NetworkServerReciever.Instance.IsHost && global::Unity.Netcode.NetworkManager.Singleton.ConnectedClients.Count >= 2)
		{
			NetworkServerReciever.Instance.NetworkReadyCounter.Value = 0;
			NetworkServerReciever.Instance.HasLoadedInLocally = false;
			global::Unity.Netcode.NetworkManager.Singleton.SceneManager.OnUnloadComplete += UnloadedFully;
			global::Unity.Netcode.NetworkManager.Singleton.SceneManager.UnloadScene(RelayManager.Instance.MatchScene);
		}
	}

	private void UnloadedFully(ulong clientId, string sceneName)
	{
		global::Unity.Netcode.NetworkManager.Singleton.SceneManager.OnUnloadComplete -= UnloadedFully;
		RelayManager.Instance.LoadGame();
	}

	public void HandleCountDown(int framesPassed)
	{
		m_countDownText.enabled = true;
		if (framesPassed == 239)
		{
			CursorController.enabled = true;
			m_countDownText.enabled = false;
			PersistentInputReader.Instance.SetSelfUpdate(selfUpdating: false);
			PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: false);
			AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic);
			if (GameManager.Instance.DefinedGameMode == GameModeType.Campaign)
			{
				AudioManager.Instance.ChangeSong(AudioManager.Instance.GetTrackForCharacter(CampaignManager.Instance.CurrentLevelDataSO.EnemyChar), isFromDialogue: false, isFromGameplay: true);
			}
			else if (GameManager.Instance.DefinedGameMode == GameModeType.LineClear)
			{
				AudioManager.Instance.ChangeSong(LineClearingManager.Instance.CurrentLevelDataSO.WAKTrack, isFromDialogue: false, isFromGameplay: true);
			}
			else if (GameManager.Instance != null && GameManager.Instance.DefinedGameMode == GameModeType.Online)
			{
				AudioManager.Instance.ChangeSong(AudioManager.Instance.GetTrackForCharacter((CharacterType)NetworkServerReciever.Instance.HostCharacter.Value), isFromDialogue: false, isFromGameplay: true);
			}
			else
			{
				AudioManager.Instance.ChangeSong(AudioManager.Instance.GetTrackForCharacter(GameManager.Instance.LocallySelectedCharacter), isFromDialogue: false, isFromGameplay: true);
			}
			if (ISPLAYER1)
			{
				AudioManager.Instance.PlaySfx(m_countdownClips[1], 0.4f, antiOverlap: false);
			}
		}
		else if (countdownOnceIndex < 1 && framesPassed >= 60)
		{
			m_countDownText.text = "READY \n3";
			if (ISPLAYER1)
			{
				AudioManager.Instance.PlaySfx(m_countdownClips[0], 0.4f, antiOverlap: false);
			}
			countdownOnceIndex = 1;
		}
		else if (framesPassed >= 120 && countdownOnceIndex < 2)
		{
			m_countDownText.text = "READY \n2";
			if (ISPLAYER1)
			{
				AudioManager.Instance.PlaySfx(m_countdownClips[0], 0.4f, antiOverlap: false);
			}
			countdownOnceIndex = 2;
		}
		else if (framesPassed >= 180 && countdownOnceIndex < 3)
		{
			m_countDownText.text = "READY \n1";
			if (ISPLAYER1)
			{
				AudioManager.Instance.PlaySfx(m_countdownClips[0], 0.4f, antiOverlap: false);
			}
			countdownOnceIndex = 3;
		}
	}
}
