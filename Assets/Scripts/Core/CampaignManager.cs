public class CampaignManager : global::UnityEngine.MonoBehaviour
{
	public static CampaignManager Instance;

	public CampaignAllDataSO AllDataSO;

	public LevelDataSO CurrentLevelDataSO;

	public int SelectedDifficulty;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_warningObject;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject RetryPanel;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_retryCountText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject ContinuePanel;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_CG;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_retryButton;

	[global::UnityEngine.SerializeField]
	private CutscenePlayer m_cutscenePlayer;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_warnContinueBtn;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_cutsceneTheatreOverlay;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_mainCampaignOverlay;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Animation m_barnDoors;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.Sprite> m_lossScreenSprites;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_lossScreenImg;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_lossScreenOpponentPortrait;

	[global::UnityEngine.SerializeField]
	private DataHolder m_charDataHolder;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_blocker;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_firstSelectedCampaign;

	private bool m_shouldPlayDialogueOnLevelStart = true;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Sprite[] m_textDifficultySprite;

	private bool m_hasPressedAnyKey;

	private bool m_waitingForBarnDoors;

	private bool m_waitingForBarnDoorsLoss;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_ReadyCheckObj;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_DoneCheckObj;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip m_cheerClip;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AnimationCurve m_musicFadeoutCurve;

	private void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void PressNicole()
	{
		SelectedDifficulty = 0;
		PressedCharacter();
	}

	public void PressJecka()
	{
		SelectedDifficulty = 1;
		PressedCharacter();
	}

	public void PressAri()
	{
		SelectedDifficulty = 2;
		PressedCharacter();
	}

	public void PressEmily()
	{
		SelectedDifficulty = 3;
		PressedCharacter();
	}

	public void PressNicoleJecka()
	{
		SelectedDifficulty = 4;
		PressedCharacter();
	}

	public void PressCutsceneTheater()
	{
		m_mainCampaignOverlay.transform.GetComponent<global::UnityEngine.CanvasGroup>().interactable = false;
		m_mainCampaignOverlay.transform.GetComponent<global::UnityEngine.CanvasGroup>().alpha = 0f;
		m_mainCampaignOverlay.transform.GetComponent<global::UnityEngine.CanvasGroup>().blocksRaycasts = false;
		m_cutsceneTheatreOverlay.SetActive(value: true);
	}

	public void BackFromCutsceneTheater()
	{
		m_cutsceneTheatreOverlay.SetActive(value: false);
		m_mainCampaignOverlay.transform.GetComponent<global::UnityEngine.CanvasGroup>().interactable = true;
		m_mainCampaignOverlay.transform.GetComponent<global::UnityEngine.CanvasGroup>().alpha = 1f;
		m_mainCampaignOverlay.transform.GetComponent<global::UnityEngine.CanvasGroup>().blocksRaycasts = true;
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_firstSelectedCampaign);
	}

	private string GetDifficultyName()
	{
		return SelectedDifficulty switch
		{
			0 => "easy", 
			1 => "normal", 
			2 => "hard", 
			3 => "harder", 
			4 => "hardest", 
			_ => "hardest", 
		};
	}

	private global::UnityEngine.Sprite GetDifficultyTextColor()
	{
		return m_textDifficultySprite[SelectedDifficulty];
	}

	private void PressedCharacter()
	{
		m_blocker.SetActive(value: true);
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
		if (GetStageNum() <= 0)
		{
			AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic);
			m_warningObject.SetActive(value: false);
			SaveData saveData = SaveSystem.Load();
			switch (SelectedDifficulty)
			{
			case 0:
				saveData.NicoleCampaign = 0;
				break;
			case 1:
				saveData.JeckaCampaign = 0;
				break;
			case 2:
				saveData.AriCampaign = 0;
				break;
			case 3:
				saveData.EmilyCampaign = 0;
				break;
			case 4:
				saveData.NicoleJeckaCampaign = 0;
				break;
			}
			SaveSystem.Save(saveData);
			PressNewGame();
		}
		else
		{
			m_warningObject.SetActive(value: true);
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_warnContinueBtn);
			PersistentInputReader instance = PersistentInputReader.Instance;
			instance.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
			PersistentInputReader instance2 = PersistentInputReader.Instance;
			instance2.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance2.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
			PersistentInputReader instance3 = PersistentInputReader.Instance;
			instance3.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance3.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
		}
	}

	public void EscapeFromWarning(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance2.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance3.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
		m_warningObject.SetActive(value: false);
		m_blocker.SetActive(value: false);
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_firstSelectedCampaign);
	}

	private int GetStageNum()
	{
		SaveData saveData = SaveSystem.Load();
		return SelectedDifficulty switch
		{
			0 => saveData.NicoleCampaign, 
			1 => saveData.JeckaCampaign, 
			2 => saveData.AriCampaign, 
			3 => saveData.EmilyCampaign, 
			4 => saveData.NicoleJeckaCampaign, 
			_ => saveData.NicoleCampaign, 
		};
	}

	private LevelDataSO GetCurrentLevelDataSO()
	{
		return SelectedDifficulty switch
		{
			0 => AllDataSO.NicoleLevels[GetStageNum()], 
			1 => AllDataSO.JeckaLevels[GetStageNum()], 
			2 => AllDataSO.AriLevels[GetStageNum()], 
			3 => AllDataSO.EmilyLevels[GetStageNum()], 
			4 => AllDataSO.NicoleJeckaLevels[GetStageNum()], 
			_ => AllDataSO.NicoleLevels[GetStageNum()], 
		};
	}

	public int GetCurrentLevelDataCount(int difficultyLevel)
	{
		return difficultyLevel switch
		{
			0 => AllDataSO.NicoleLevels.Count, 
			1 => AllDataSO.JeckaLevels.Count, 
			2 => AllDataSO.AriLevels.Count, 
			3 => AllDataSO.EmilyLevels.Count, 
			4 => AllDataSO.NicoleJeckaLevels.Count, 
			_ => AllDataSO.NicoleLevels.Count, 
		};
	}

	private bool WinLevelCheck()
	{
		SaveData saveData = SaveSystem.Load();
		if (GetStageNum() >= GetCurrentLevelDataCount(SelectedDifficulty) - 1)
		{
			switch (SelectedDifficulty)
			{
			case 0:
				if (saveData.JeckaCampaign == -1)
				{
					saveData.JeckaCampaign = 0;
				}
				SteamAchievementsHandler.Instance?.GrantAchievement("COMPLETE_NICOLE_CAMPAIGN");
				break;
			case 1:
				if (saveData.AriCampaign == -1)
				{
					saveData.AriCampaign = 0;
				}
				SteamAchievementsHandler.Instance?.GrantAchievement("COMPLETE_JECKA_CAMPAIGN");
				break;
			case 2:
				if (saveData.EmilyCampaign == -1)
				{
					saveData.EmilyCampaign = 0;
				}
				SteamAchievementsHandler.Instance?.GrantAchievement("COMPLETE_ARI_CAMPAIGN");
				break;
			case 3:
				if (saveData.NicoleJeckaCampaign == -1)
				{
					saveData.NicoleJeckaCampaign = 0;
				}
				SteamAchievementsHandler.Instance?.GrantAchievement("COMPLETE_EMILY_CAMPAIGN");
				break;
			}
			if (SelectedDifficulty == 4)
			{
				m_cutscenePlayer.PlayCutscene((CutsceneVideoIndicies)SelectedDifficulty, playSilently: false, playWithAnySkip: false, activatePanel: true, isUnskippable: true);
				m_cutscenePlayer.CutsceneFinishedEvent.AddListener(CutscenePlayedFinal);
				saveData.HasUnlockedFinalCampaignCutscene = 1;
			}
			else
			{
				m_cutscenePlayer.PlayCutscene((CutsceneVideoIndicies)SelectedDifficulty, playSilently: false, playWithAnySkip: false, activatePanel: true);
				m_cutscenePlayer.CutsceneFinishedEvent.AddListener(CutscenePlayed);
			}
			SaveSystem.Save(saveData);
			return true;
		}
		switch (SelectedDifficulty)
		{
		case 0:
			saveData.NicoleCampaign++;
			break;
		case 1:
			saveData.JeckaCampaign++;
			break;
		case 2:
			saveData.AriCampaign++;
			break;
		case 3:
			saveData.EmilyCampaign++;
			break;
		case 4:
			saveData.NicoleJeckaCampaign++;
			break;
		default:
			saveData.NicoleCampaign++;
			break;
		}
		SaveSystem.Save(saveData);
		return false;
	}

	private void CutscenePlayed()
	{
		m_cutscenePlayer.CutsceneFinishedEvent.RemoveAllListeners();
		SceneLoader.Instance.DoBlackFade(0f, 1f, 0f, muteSound: false, delegate
		{
		}).OnComplete(delegate
		{
			GameManager.Instance.LoadMenuNoWater();
		});
	}

	private void CutscenePlayedFinal()
	{
		m_cutscenePlayer.CutsceneFinishedEvent.RemoveAllListeners();
		global::UnityEngine.Application.Quit();
	}

	public void PressContinue()
	{
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance2.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance3.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
		CurrentLevelDataSO = GetCurrentLevelDataSO();
		m_shouldPlayDialogueOnLevelStart = true;
		AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic);
		LoadLevel();
	}

	public void PressNewGame()
	{
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance2.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance3.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(EscapeFromWarning));
		AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic);
		SaveData saveData = SaveSystem.Load();
		switch (SelectedDifficulty)
		{
		case 0:
			saveData.NicoleCampaign = 0;
			global::UnityEngine.PlayerPrefs.SetInt("RETRYCOUNT_0", 0);
			break;
		case 1:
			saveData.JeckaCampaign = 0;
			global::UnityEngine.PlayerPrefs.SetInt("RETRYCOUNT_1", 0);
			break;
		case 2:
			saveData.AriCampaign = 0;
			global::UnityEngine.PlayerPrefs.SetInt("RETRYCOUNT_2", 0);
			break;
		case 3:
			saveData.EmilyCampaign = 0;
			global::UnityEngine.PlayerPrefs.SetInt("RETRYCOUNT_3", 0);
			break;
		case 4:
			saveData.NicoleJeckaCampaign = 0;
			global::UnityEngine.PlayerPrefs.SetInt("RETRYCOUNT_4", 0);
			break;
		default:
			saveData.NicoleCampaign = 0;
			break;
		}
		SaveSystem.Save(saveData);
		SceneLoader.Instance.DoBlackFade(1f, 0.2f, 0.5f, muteSound: false, delegate
		{
			m_cutscenePlayer.PlayCutscene((CutsceneVideoIndicies)(SelectedDifficulty + 10), playSilently: false, playWithAnySkip: true);
			m_cutscenePlayer.CutsceneFinishedEvent.AddListener(IntroCutsceneFinished);
		});
	}

	private void IntroCutsceneFinished()
	{
		m_cutscenePlayer.CutsceneFinishedEvent.RemoveAllListeners();
		PressContinue();
	}

	public void LoadLevel()
	{
		m_CG.sprite = CurrentLevelDataSO.LevelCG;
		m_CG.enabled = true;
		m_CG.DOFade(1f, 0f);
		RetryPanel.SetActive(value: false);
		SceneLoader.ActiveGameMode = GameModeType.Campaign;
		SceneLoader.Instance.SceneLoadedEvent.AddListener(OnLevelLoadedIn);
		SceneLoader.Instance.LoadSceneByActiveGameModeRegularFade();
	}

	private void OnLevelLoadedIn()
	{
		SceneLoader.Instance.SceneLoadedEvent.RemoveListener(OnLevelLoadedIn);
		if (ContinuePanel.activeSelf)
		{
			StartCoroutine(OpenUpBarnDoors());
			return;
		}
		if (ExtraElementsManager.Instance != null)
		{
			ExtraElementsManager.Instance.STAGE_TEXT.text = $"stage \n {GetStageNum() + 1}";
			ExtraElementsManager.Instance.DIFFICULTY_ICON.sprite = GetDifficultyTextColor();
		}
		if (m_shouldPlayDialogueOnLevelStart)
		{
			PlayIntroDialogue();
			return;
		}
		IntroDialogueFinished();
		m_shouldPlayDialogueOnLevelStart = true;
	}

	private void PlayIntroDialogue()
	{
		DialogueManager.Instance.SetUpCampaignData(SelectedDifficulty);
		DialogueManager.Instance.DialogueFinishedEvent?.AddListener(IntroDialogueFinished);
		DialogueManager.Instance.CurrentData = CurrentLevelDataSO.LevelSpeechData[0];
		DialogueManager.Instance.ReadSegment(DialogueManager.Instance.CurrentData);
		if (CurrentLevelDataSO.name[CurrentLevelDataSO.name.Length - 2] == '0')
		{
			DialogueManager.Instance.SetStageIndicator($"Stage {CurrentLevelDataSO.name[CurrentLevelDataSO.name.Length - 1]}");
		}
		else
		{
			DialogueManager.Instance.SetStageIndicator($"Stage {CurrentLevelDataSO.name[CurrentLevelDataSO.name.Length - 2]}{CurrentLevelDataSO.name[CurrentLevelDataSO.name.Length - 1]}");
		}
	}

	private void IntroDialogueFinished()
	{
		global::UnityEngine.GameObject.Find("BG").GetComponent<global::UnityEngine.SpriteRenderer>().sprite = CurrentLevelDataSO.LevelCG;
		m_CG.DOFade(0f, 1f).OnComplete(delegate
		{
			m_CG.enabled = false;
		});
		DialogueManager.Instance.DialogueFinishedEvent?.RemoveListener(IntroDialogueFinished);
		GameManager.Instance.SelectedSpeedLevel = CurrentLevelDataSO.LevelSpeed[0];
		PodManager[] array = global::UnityEngine.Object.FindObjectsByType<PodManager>(global::UnityEngine.FindObjectsSortMode.InstanceID);
		for (int num = 0; num < array.Length; num++)
		{
			if (array[num].ISPLAYER1)
			{
				array[num].SetSelectedCharacter(CurrentLevelDataSO.PlayersChar);
				continue;
			}
			array[num].SetAiDifficulty(CurrentLevelDataSO.AiDifficultyLevel);
			array[num].SetSelectedCharacter(CurrentLevelDataSO.EnemyChar);
		}
		for (int num2 = 0; num2 < array.Length; num2++)
		{
			array[num2].Setup();
			array[num2].StartGame();
			global::UnityEngine.EventSystems.EventSystem.current.firstSelectedGameObject = global::UnityEngine.EventSystems.EventSystem.current.transform.GetComponent<PauseManager>().UnpauseButton;
		}
	}

	private void Update()
	{
		if (m_waitingForBarnDoors && (global::UnityEngine.Input.anyKeyDown || m_hasPressedAnyKey))
		{
			m_waitingForBarnDoors = false;
			if (WinLevelCheck())
			{
				m_waitingForBarnDoors = false;
				return;
			}
			ActivateBarnDoors();
		}
		if (m_waitingForBarnDoorsLoss && global::UnityEngine.Input.anyKeyDown)
		{
			m_waitingForBarnDoorsLoss = false;
			RetryPanel.SetActive(value: true);
			AudioManager.Instance.SetMusicSpeed(0.2f);
			AudioManager.Instance.ChangeSong(MusicTrackType.LossMusic);
			AudioManager.Instance.FadeMusicSpeed(1f, 0.5f);
			string text = "";
			switch (SelectedDifficulty)
			{
			case 0:
				text = "RETRYCOUNT_0";
				if (global::UnityEngine.PlayerPrefs.HasKey(text))
				{
					global::UnityEngine.PlayerPrefs.SetInt(text, global::UnityEngine.PlayerPrefs.GetInt(text) + 1);
				}
				else
				{
					global::UnityEngine.PlayerPrefs.SetInt(text, 1);
				}
				if (global::UnityEngine.PlayerPrefs.GetInt(text) >= 999)
				{
					m_retryCountText.text = "999";
				}
				else
				{
					m_retryCountText.text = global::UnityEngine.PlayerPrefs.GetInt(text).ToString("D2");
				}
				break;
			case 1:
				text = "RETRYCOUNT_1";
				if (global::UnityEngine.PlayerPrefs.HasKey(text))
				{
					global::UnityEngine.PlayerPrefs.SetInt(text, global::UnityEngine.PlayerPrefs.GetInt(text) + 1);
				}
				else
				{
					global::UnityEngine.PlayerPrefs.SetInt(text, 1);
				}
				if (global::UnityEngine.PlayerPrefs.GetInt(text) >= 999)
				{
					m_retryCountText.text = "999";
				}
				else
				{
					m_retryCountText.text = global::UnityEngine.PlayerPrefs.GetInt(text).ToString("D2");
				}
				break;
			case 2:
				text = "RETRYCOUNT_2";
				if (global::UnityEngine.PlayerPrefs.HasKey(text))
				{
					global::UnityEngine.PlayerPrefs.SetInt(text, global::UnityEngine.PlayerPrefs.GetInt(text) + 1);
				}
				else
				{
					global::UnityEngine.PlayerPrefs.SetInt(text, 1);
				}
				if (global::UnityEngine.PlayerPrefs.GetInt(text) >= 999)
				{
					m_retryCountText.text = "999";
				}
				else
				{
					m_retryCountText.text = global::UnityEngine.PlayerPrefs.GetInt(text).ToString("D2");
				}
				break;
			case 3:
				text = "RETRYCOUNT_3";
				if (global::UnityEngine.PlayerPrefs.HasKey(text))
				{
					global::UnityEngine.PlayerPrefs.SetInt(text, global::UnityEngine.PlayerPrefs.GetInt(text) + 1);
				}
				else
				{
					global::UnityEngine.PlayerPrefs.SetInt(text, 1);
				}
				if (global::UnityEngine.PlayerPrefs.GetInt(text) >= 999)
				{
					m_retryCountText.text = "999";
				}
				else
				{
					m_retryCountText.text = global::UnityEngine.PlayerPrefs.GetInt(text).ToString("D2");
				}
				break;
			case 4:
				text = "RETRYCOUNT_4";
				if (global::UnityEngine.PlayerPrefs.HasKey(text))
				{
					global::UnityEngine.PlayerPrefs.SetInt(text, global::UnityEngine.PlayerPrefs.GetInt(text) + 1);
				}
				else
				{
					global::UnityEngine.PlayerPrefs.SetInt(text, 1);
				}
				if (global::UnityEngine.PlayerPrefs.GetInt(text) >= 999)
				{
					m_retryCountText.text = "999";
				}
				else
				{
					m_retryCountText.text = global::UnityEngine.PlayerPrefs.GetInt(text).ToString("D2");
				}
				break;
			}
			StartCoroutine(SelectNextFrame(m_retryButton));
		}
		if (m_ReadyCheckObj.activeSelf && global::UnityEngine.Input.anyKeyDown)
		{
			StartCoroutine(CloseOutBarndoors());
		}
		if (m_DoneCheckObj.activeSelf)
		{
			StartCoroutine(CloseOutBarndoors());
		}
	}

	private global::System.Collections.IEnumerator SelectNextFrame(global::UnityEngine.GameObject buttonToSelect)
	{
		yield return null;
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(buttonToSelect);
	}

	public void OnLevelLost()
	{
		m_lossScreenImg.sprite = m_lossScreenSprites[SelectedDifficulty];
		for (int i = 0; i < m_charDataHolder.AllCharacterData.Count; i++)
		{
			if (m_charDataHolder.AllCharacterData[i].Type == CurrentLevelDataSO.EnemyChar)
			{
				m_lossScreenOpponentPortrait.sprite = m_charDataHolder.AllCharacterData[i].CharacterPortraitRight;
			}
		}
		PersistentInputReader.Instance.SetSelfUpdate(selfUpdating: true);
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: false);
		PersistentInputReader.Instance.AllowPausing = false;
		m_waitingForBarnDoorsLoss = true;
	}

	public void OnLevelWon()
	{
		PersistentInputReader.Instance.SetSelfUpdate(selfUpdating: true);
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: false);
		PersistentInputReader.Instance.AllowPausing = false;
		m_hasPressedAnyKey = false;
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.KeyboardAnyAction = (global::System.Action)global::System.Delegate.Combine(instance.KeyboardAnyAction, new global::System.Action(HasDoneAnyInput));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.Controller1AnyAction = (global::System.Action)global::System.Delegate.Combine(instance2.Controller1AnyAction, new global::System.Action(HasDoneAnyInput));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.Controller2AnyAction = (global::System.Action)global::System.Delegate.Combine(instance3.Controller2AnyAction, new global::System.Action(HasDoneAnyInput));
		m_waitingForBarnDoors = true;
	}

	private void HasDoneAnyInput()
	{
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.KeyboardAnyAction = (global::System.Action)global::System.Delegate.Remove(instance.KeyboardAnyAction, new global::System.Action(HasDoneAnyInput));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.Controller1AnyAction = (global::System.Action)global::System.Delegate.Remove(instance2.Controller1AnyAction, new global::System.Action(HasDoneAnyInput));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.Controller2AnyAction = (global::System.Action)global::System.Delegate.Remove(instance3.Controller2AnyAction, new global::System.Action(HasDoneAnyInput));
		m_hasPressedAnyKey = true;
	}

	private void ActivateBarnDoors()
	{
		ContinuePanel.SetActive(value: true);
		m_barnDoors.clip = m_barnDoors.GetClip("barndoor");
		m_barnDoors.Play();
		m_cutscenePlayer.PlayCutscene((CutsceneVideoIndicies)(SelectedDifficulty + 5), playSilently: true);
		Invoke("PlayWinMusic", 1f);
	}

	private void PlayWinMusic()
	{
		switch (SelectedDifficulty)
		{
		case 0:
			AudioManager.Instance.ChangeSong(MusicTrackType.NicoleTheme);
			break;
		case 1:
			AudioManager.Instance.ChangeSong(MusicTrackType.JeckaTheme);
			break;
		case 2:
			AudioManager.Instance.ChangeSong(MusicTrackType.AriTheme);
			break;
		case 3:
			AudioManager.Instance.ChangeSong(MusicTrackType.EmilyTheme);
			break;
		case 4:
			AudioManager.Instance.ChangeSong(MusicTrackType.NicoleTheme);
			break;
		}
		AudioManager.Instance.PlaySfx(m_cheerClip);
	}

	private global::System.Collections.IEnumerator CloseOutBarndoors()
	{
		m_barnDoors.clip = m_barnDoors.GetClip("barndoorOpen");
		m_barnDoors.Play();
		m_ReadyCheckObj.SetActive(value: false);
		m_DoneCheckObj.SetActive(value: false);
		AudioManager.Instance.MusicAS.DOFade(0f, 1f);
		yield return new global::UnityEngine.WaitForSeconds(1f);
		AudioManager.Instance.MusicAS.volume = 1f;
		AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic);
		m_cutscenePlayer.StopCutscene();
		ReadyUp();
	}

	private global::System.Collections.IEnumerator OpenUpBarnDoors()
	{
		m_barnDoors.clip = m_barnDoors.GetClip("barndoorVanish");
		m_barnDoors.Play();
		yield return new global::UnityEngine.WaitForSeconds(0.7f);
		ContinuePanel.SetActive(value: false);
		if (ExtraElementsManager.Instance != null)
		{
			ExtraElementsManager.Instance.STAGE_TEXT.text = $"stage \n {GetStageNum() + 1}";
			ExtraElementsManager.Instance.DIFFICULTY_ICON.sprite = GetDifficultyTextColor();
		}
		if (m_shouldPlayDialogueOnLevelStart)
		{
			PlayIntroDialogue();
			yield break;
		}
		IntroDialogueFinished();
		m_shouldPlayDialogueOnLevelStart = true;
	}

	public void ReadyUp()
	{
		PressContinue();
	}

	public void Retry()
	{
		m_shouldPlayDialogueOnLevelStart = false;
		LoadLevel();
	}

	public void ReturnToMenu()
	{
		GameManager.Instance.LoadMenu();
	}
}
