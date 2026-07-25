public class LineClearingManager : global::UnityEngine.MonoBehaviour
{
	[global::System.Flags]
	public enum Endings
	{
		None = 0,
		A = 1,
		B = 2,
		C = 4,
		D = 8,
		E = 0x10,
		F = 0x20,
		G = 0x40,
		H = 0x80
	}

	public static LineClearingManager Instance;

	public int SelectedSaveSlot;

	public int CurrentLevelNumber;

	public int CurrentSublevelNumber;

	public LevelDataSO CurrentLevelDataSO;

	public WakAllDataSO AllDataSO;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image CG;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image Fader;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject ChoicePanel;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject ContinuePanel;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_continueBtn;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_retryBtn;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject RetryPanel;

	[global::UnityEngine.SerializeField]
	private AudioOnDemandPlayer m_ChoiceAudioOnDemand;

	[global::UnityEngine.SerializeField]
	private LevelDataSO m_introLevel;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::TMPro.TextMeshProUGUI> m_choiceTexts = new global::System.Collections.Generic.List<global::TMPro.TextMeshProUGUI>();

	private bool m_isFirstDialogue;

	private const int MAX_SUBLEVEL = 5;

	private void Awake()
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

	private void DisableAllMenuButtons()
	{
		GameManager.Instance.MenuScreens[0].ParentObject.SetActive(value: false);
	}

	public void SelectSaveSlot(int number)
	{
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
		AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic);
		global::UnityEngine.EventSystems.EventSystem.current?.SetSelectedGameObject(null);
		SelectedSaveSlot = number;
		LoadData();
		CurrentLevelDataSO = GetCurrentLevelToLoad();
		CG.sprite = CurrentLevelDataSO.LevelCG;
		if (CurrentSublevelNumber == 0 && CurrentLevelNumber == 0)
		{
			LoadNewGameCuscene();
			return;
		}
		if (CurrentSublevelNumber == 5)
		{
			DisableAllMenuButtons();
			HasWonOnLastSublevel();
			return;
		}
		CG.enabled = false;
		SceneLoader.Instance.DoBlackFade(1.5f, 0.6f, 0.5f, muteSound: false, delegate
		{
			DisableAllMenuButtons();
		}).OnComplete(delegate
		{
			m_isFirstDialogue = false;
			LoadData();
			CurrentLevelDataSO = m_introLevel;
			global::UnityEngine.EventSystems.EventSystem.current?.SetSelectedGameObject(null);
			LoadLevel();
		});
	}

	private void LoadNewGameCuscene()
	{
		SceneLoader.Instance.DoBlackFade(1.5f, 0.6f, 0.5f, muteSound: true, delegate
		{
			DisableAllMenuButtons();
		}).OnComplete(delegate
		{
			m_isFirstDialogue = true;
			LoadData();
			CurrentLevelDataSO = m_introLevel;
			global::UnityEngine.EventSystems.EventSystem.current?.SetSelectedGameObject(null);
			CG.sprite = CurrentLevelDataSO.LevelCG;
			CG.enabled = true;
			CG.DOFade(1f, 1f).OnComplete(delegate
			{
				DialogueManager.Instance.DialogueFinishedEvent?.AddListener(LoadFirstSectionOfLevelWithFade);
				DialogueManager.Instance.CurrentData = CurrentLevelDataSO.LevelSpeechData[CurrentSublevelNumber];
				DialogueManager.Instance.ReadSegment(DialogueManager.Instance.CurrentData);
			});
		});
	}

	private void LoadFirstSectionOfLevelWithFade()
	{
		SceneLoader.Instance.DoBlackFade(1f, 1f, 1f, muteSound: false, delegate
		{
			LoadFirstSectionOfLevel();
		}, null, 1f);
	}

	private void LoadFirstSectionOfLevel()
	{
		DialogueManager.Instance.DialogueFinishedEvent.RemoveListener(LoadFirstSectionOfLevel);
		DialogueManager.Instance.DialogueFinishedEvent.RemoveListener(LoadFirstSectionOfLevelWithFade);
		m_isFirstDialogue = true;
		LoadData();
		global::UnityEngine.EventSystems.EventSystem.current?.SetSelectedGameObject(null);
		CurrentLevelDataSO = GetCurrentLevelToLoad();
		CG.sprite = CurrentLevelDataSO.LevelCG;
		CG.enabled = true;
		CG.DOFade(1f, 1f).OnComplete(delegate
		{
			DialogueManager.Instance.DialogueFinishedEvent?.AddListener(BlurbDialogueFinished);
			DialogueManager.Instance.CurrentData = CurrentLevelDataSO.LevelSpeechData[CurrentSublevelNumber];
			DialogueManager.Instance.ReadSegment(DialogueManager.Instance.CurrentData);
		});
	}

	public void PressContinue()
	{
		ContinuePanel.SetActive(value: false);
		LoadLevel();
	}

	public void PressRetry()
	{
		RetryPanel.SetActive(value: false);
		LoadLevel();
	}

	public void PressBackToMenu()
	{
		CG.enabled = true;
		CG.DOFade(0f, 1f).OnComplete(delegate
		{
			GameManager.Instance.LoadMenu();
		});
	}

	public void LoadLevel()
	{
		LoadData();
		PersistentInputReader.Instance.SetSelfUpdate(selfUpdating: true);
		CurrentLevelDataSO = GetCurrentLevelToLoad();
		CG.enabled = true;
		CG.sprite = CurrentLevelDataSO.LevelCG;
		CG.DOFade(1f, 1f).OnComplete(delegate
		{
			SceneLoader.Instance.SceneLoadedEvent.AddListener(OnLevelLoadedIn);
			SceneLoader.Instance.LoadSceneByActiveGameModeRegularFade();
			GameManager.Instance.LocallySelectedCharacter = CharacterType.Kelly;
			GameManager.Instance.SelectedSpeedLevel = CurrentLevelDataSO.LevelSpeed[CurrentSublevelNumber];
			GameManager.Instance.SelectedDepthLevel = CurrentLevelDataSO.LevelDepth[CurrentSublevelNumber];
		});
	}

	private void OnLevelLoadedIn()
	{
		SceneLoader.Instance.SceneLoadedEvent.RemoveListener(OnLevelLoadedIn);
		global::UnityEngine.GameObject.Find("BG").GetComponent<global::UnityEngine.UI.Image>().sprite = CurrentLevelDataSO.LevelCG;
		global::UnityEngine.Object.FindFirstObjectByType<PodManager>().SetSelectedCharacter(CharacterType.Kelly);
		global::UnityEngine.Object.FindFirstObjectByType<PodManager>().Setup();
		global::UnityEngine.Object.FindFirstObjectByType<PodManager>().StartGame();
		CG.DOFade(0f, 1f).OnComplete(delegate
		{
			CG.enabled = false;
		});
	}

	private void BlurbDialogueFinished()
	{
		DialogueManager.Instance.DialogueFinishedEvent.RemoveListener(BlurbDialogueFinished);
		if (m_isFirstDialogue)
		{
			LoadLevel();
			m_isFirstDialogue = false;
		}
		else
		{
			ContinuePanel.SetActive(value: true);
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_continueBtn);
		}
	}

	private void FinalLevelDialogueFinished()
	{
		DialogueManager.Instance.DialogueFinishedEvent?.RemoveListener(FinalLevelDialogueFinished);
		if (GetLevelNumber(CurrentLevelDataSO.name) == 5)
		{
			char c = CurrentLevelDataSO.name[CurrentLevelDataSO.name.Length - 1];
			SteamAchievementsHandler.Instance.GrantAchievement($"WAK_ENDING_{c}");
			SaveData saveData = SaveSystem.Load();
			saveData.WAKendingsUnlocked |= 1 << c - 65;
			SaveSystem.Save(saveData);
			CG.enabled = true;
			CG.DOFade(1f, 1f).OnComplete(delegate
			{
				SceneLoader.Instance.DoBlackFade(1f, 1.5f, 0.5f, muteSound: false, delegate
				{
					GameManager.Instance.LoadMenuNoWater();
				});
			});
		}
		else if (GetLevelNumber(CurrentLevelDataSO.name) == 4)
		{
			MakeChoice(0, isSilent: true);
		}
		else
		{
			ShowChoiceSelection();
		}
	}

	private void LoadData()
	{
		if (SelectedSaveSlot == 0)
		{
			CurrentLevelNumber = SaveSystem.Load().WAK_Slot0;
			CurrentSublevelNumber = SaveSystem.Load().WAK_Slot0Sub;
		}
	}

	private void SaveData()
	{
		int selectedSaveSlot = SelectedSaveSlot;
		SaveData saveData = SaveSystem.Load();
		if (selectedSaveSlot == 0)
		{
			saveData.WAK_Slot0 = CurrentLevelNumber;
			saveData.WAK_Slot0Sub = CurrentSublevelNumber;
		}
		SaveSystem.Save(saveData);
	}

	public void MakeChoice(int choiceNum)
	{
		MakeChoice(choiceNum, false);
	}

	public void MakeChoice(int choiceNum, bool isSilent = false)
	{
		ChoicePanel.GetComponent<global::UnityEngine.Animator>().SetBool("play", value: false);
		if (!isSilent)
		{
			m_ChoiceAudioOnDemand.PlayAudioOnDemandIndex(1);
		}
		CurrentLevelNumber = GetIndexForLevelData(CurrentLevelDataSO.NextLevels[choiceNum]);
		CurrentSublevelNumber = 0;
		SaveData();
		SceneLoader.Instance.DoBlackFade(1f, 1.2f, 1f, muteSound: false, delegate
		{
			LoadFirstSectionOfLevel();
			ChoicePanel.SetActive(value: false);
		});
	}

	private void ShowChoiceSelection()
	{
		ChoicePanel.SetActive(value: true);
		ChoicePanel.GetComponent<global::UnityEngine.Animator>().SetBool("play", value: true);
		m_ChoiceAudioOnDemand.PlayAudioOnDemandIndex(0);
	}

	public void FlipFinishedEvent()
	{
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_choiceTexts[0].GetComponentInParent<global::UnityEngine.UI.Button>().gameObject);
		m_choiceTexts[0].text = CurrentLevelDataSO.Choices[0];
		m_choiceTexts[1].text = CurrentLevelDataSO.Choices[1];
	}

	public void AdvanceSubLevel(out bool isChoiceTime)
	{
		CurrentSublevelNumber++;
		if (CurrentSublevelNumber >= 5)
		{
			isChoiceTime = true;
		}
		else
		{
			isChoiceTime = false;
		}
	}

	public void SectionWon()
	{
		AdvanceSubLevel(out var isChoiceTime);
		if (isChoiceTime)
		{
			HasWonOnLastSublevel();
			return;
		}
		SaveData();
		DialogueManager.Instance.DialogueFinishedEvent?.AddListener(BlurbDialogueFinished);
		DialogueManager.Instance.CurrentData = CurrentLevelDataSO.LevelSpeechData[CurrentSublevelNumber];
		DialogueManager.Instance.ReadSegment(DialogueManager.Instance.CurrentData, isShortWAK: true);
	}

	private void HasWonOnLastSublevel()
	{
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
		CurrentLevelDataSO = GetCurrentLevelToLoad();
		CG.sprite = CurrentLevelDataSO.LevelCG;
		SaveData();
		CG.enabled = true;
		CG.DOFade(1f, 1f).OnComplete(delegate
		{
			DialogueManager.Instance.DialogueFinishedEvent?.AddListener(FinalLevelDialogueFinished);
			DialogueManager.Instance.CurrentData = CurrentLevelDataSO.LevelSpeechData[CurrentSublevelNumber];
			DialogueManager.Instance.ReadSegment(DialogueManager.Instance.CurrentData);
		});
	}

	public void SectionLose()
	{
		RetryPanel.SetActive(value: true);
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_retryBtn);
	}

	private LevelDataSO GetCurrentLevelToLoad()
	{
		return AllDataSO.AllLevelsByIndex[CurrentLevelNumber];
	}

	private int GetIndexForLevelData(LevelDataSO data)
	{
		return AllDataSO.AllLevelsByIndex.IndexOf(data);
	}

	public int GetLevelNameForSaveData(int saveDataIndex)
	{
		return GetLevelNumber(AllDataSO.AllLevelsByIndex[saveDataIndex].name);
	}

	public static int GetLevelNumber(string input)
	{
		global::System.Text.RegularExpressions.Match match = global::System.Text.RegularExpressions.Regex.Match(input, "\\d+");
		if (match.Success)
		{
			return int.Parse(match.Value);
		}
		return -1;
	}
}
