public class GameManager : global::UnityEngine.MonoBehaviour
{
	public static GameManager Instance;

	public GameModeType DefinedGameMode;

	public CharacterType LocallySelectedCharacter;

	public CharacterType LocallySelectedP2Character;

	public int P1InputScheme;

	public int P2InputScheme = 1;

	[global::UnityEngine.SerializeField]
	private CharacterSelectGrid m_selectGrid;

	public int SelectedSpeedLevel = 1;

	public int SelectedDepthLevel;

	public DifficultyType SelectedDifficulty = DifficultyType.Easy;

	public global::System.Collections.Generic.List<GameMenuScreen> MenuScreens = new global::System.Collections.Generic.List<GameMenuScreen>();

	public int MenuScreenIndex;

	public global::System.Collections.Generic.List<global::UnityEngine.GameObject> ObjsToDespawnWhenMenu = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_warnLocal;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_warnLocalOp;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_warnOnline;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_warnOnlineOp;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_backBtnMultiplayer;

	[global::UnityEngine.SerializeField]
	private DifficultyManager m_diffMan;

	private void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			SceneLoader.ActiveGameMode = DefinedGameMode;
			MenuScreenIndex = -1;
			NextMenuScreen();
			if (m_diffMan != null)
			{
				m_diffMan.Setup();
			}
		}
		else
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void NextMenuScreen()
	{
		MenuScreenIndex++;
		if (MenuScreenIndex == MenuScreens.Count)
		{
			if (DefinedGameMode == GameModeType.Marathon)
			{
				PersistentInputReader.Instance.ClearAllSubscriptions();
				PersistentInputReader.Instance.AllowPausing = false;
				global::DG.Tweening.TweenSettingsExtensions.OnComplete(SceneLoader.Instance.DoBlackFade(1f, 0.6f, 0.5f, muteSound: true, delegate
				{
					SceneLoader.Instance.LoadSceneByActiveGameModeRegularFade();
				}, null, 1f), delegate
				{
					GameStarted();
				});
			}
			return;
		}
		for (int num = 0; num < MenuScreens.Count; num++)
		{
			if (num == MenuScreenIndex)
			{
				MenuScreens[num].Activate();
			}
			else
			{
				MenuScreens[num].Deactivate();
			}
		}
		if (DefinedGameMode == GameModeType.Marathon && MenuScreenIndex == MenuScreens.Count - 1)
		{
			PersistentInputReader.Instance.AllowPausing = false;
			PersistentInputReader instance = PersistentInputReader.Instance;
			instance.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
			PersistentInputReader instance2 = PersistentInputReader.Instance;
			instance2.KeyboardSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance2.KeyboardSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
			PersistentInputReader instance3 = PersistentInputReader.Instance;
			instance3.KeyboardAltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance3.KeyboardAltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
			PersistentInputReader instance4 = PersistentInputReader.Instance;
			instance4.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance4.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
			PersistentInputReader instance5 = PersistentInputReader.Instance;
			instance5.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance5.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
			PersistentInputReader instance6 = PersistentInputReader.Instance;
			instance6.KeyboardSpecialEnterAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance6.KeyboardSpecialEnterAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
			PersistentInputReader instance7 = PersistentInputReader.Instance;
			instance7.Controller1AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance7.Controller1AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
			PersistentInputReader instance8 = PersistentInputReader.Instance;
			instance8.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance8.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
			PersistentInputReader instance9 = PersistentInputReader.Instance;
			instance9.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance9.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
			PersistentInputReader instance10 = PersistentInputReader.Instance;
			instance10.Controller2AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance10.Controller2AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
			PersistentInputReader instance11 = PersistentInputReader.Instance;
			instance11.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance11.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
			PersistentInputReader instance12 = PersistentInputReader.Instance;
			instance12.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance12.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
			PersistentInputReader instance13 = PersistentInputReader.Instance;
			instance13.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance13.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
			PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: false);
			CharacterSelectGrid selectGrid = m_selectGrid;
			selectGrid.ALLREADYACTION = (global::System.Action)global::System.Delegate.Combine(selectGrid.ALLREADYACTION, new global::System.Action(NextMenuScreen));
		}
	}

	public void PreviousMenuScreen()
	{
		MenuScreenIndex--;
		if (MenuScreenIndex <= -1)
		{
			LoadMenu();
			return;
		}
		for (int i = 0; i < MenuScreens.Count; i++)
		{
			if (i == MenuScreenIndex)
			{
				MenuScreens[i].Activate();
			}
			else
			{
				MenuScreens[i].Deactivate();
			}
		}
	}

	public void CloseWarningMultiplayer()
	{
		m_warnOnline.SetActive(value: false);
		m_warnLocal.SetActive(value: false);
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_backBtnMultiplayer);
	}

	public void PressOnlinePlay()
	{
		if (!SteamManager.Initialized)
		{
			m_warnOnline.SetActive(value: true);
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_warnOnlineOp);
		}
		else
		{
			DefinedGameMode = GameModeType.Online;
			SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.Matchmake);
		}
	}

	public void PressLocalMultiplayer()
	{
		if (!PersistentInputReader.Instance.CanPlayLocalMp())
		{
			m_warnLocal.SetActive(value: true);
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_warnLocalOp);
			return;
		}
		DefinedGameMode = GameModeType.LocalMp;
		LocallySelectedCharacter = CharacterType.None;
		LocallySelectedP2Character = CharacterType.None;
		SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.MatchmakeLocally);
	}

	private void GameStarted()
	{
		switch (DefinedGameMode)
		{
		case GameModeType.LocalMp:
			PersistentInputReader.Instance.SetSelfUpdate(selfUpdating: true);
			break;
		case GameModeType.Campaign:
		case GameModeType.Marathon:
		case GameModeType.LineClear:
		case GameModeType.Online:
			break;
		}
	}

	public void SelectCharacter(int enumIndex)
	{
		LocallySelectedCharacter = (CharacterType)enumIndex;
	}

	public void PlayLocalMp()
	{
		SceneLoader.Instance.LoadSceneByActiveGameModeRegularFade();
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: true);
		GameStarted();
		PersistentInputReader.Instance.AllowPausing = true;
	}

	public void LoadMenu()
	{
		global::UnityEngine.Time.timeScale = 1f;
		PersistentInputReader.Instance.ClearAllSubscriptions();
		SceneLoader.Instance.LoadSceneByEnumRegularFade(AllGameScenes.MainMenu);
	}

	public void LoadMenuNoWater()
	{
		global::UnityEngine.Time.timeScale = 1f;
		OutOfGamemodeDestroy();
		PersistentInputReader.Instance.ClearAllSubscriptions();
		SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.MainMenu);
	}

	public void OutOfGamemodeDestroy()
	{
		Instance = null;
		for (int i = 0; i < ObjsToDespawnWhenMenu.Count; i++)
		{
			global::UnityEngine.Object.Destroy(ObjsToDespawnWhenMenu[i]);
		}
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
