public class DialogueManager : global::UnityEngine.MonoBehaviour
{
	public static DialogueManager Instance;

	public global::UnityEngine.GameObject m_dialogueVis;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_nameText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_speechText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioSource m_audioSource;

	[global::UnityEngine.SerializeField]
	private bool m_isCampaignMode;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.UI.Image> CampaignPortraits = new global::System.Collections.Generic.List<global::UnityEngine.UI.Image>();

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.GameObject> CampaignBubbles = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::TMPro.TextMeshProUGUI> CampaignTextFields = new global::System.Collections.Generic.List<global::TMPro.TextMeshProUGUI>();

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<string> ActivePlayerCharsCampaign = new global::System.Collections.Generic.List<string>();

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_bottomShadow;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_campaignContinueTriangle;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_stageText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_extraButtonsObj;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Animation m_slideInAnim;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioReverbFilter m_dialogueReverb;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_altSkipObj;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_altSkipFillCirlce;

	private bool m_isShortWakDialogue;

	public AudioSegmentDataSO CurrentData;

	public global::UnityEngine.Events.UnityEvent DialogueFinishedEvent;

	private readonly string colorTag = "<color=#00000000>";

	private readonly float TIME_ADJUSTMENT_FOR_TEXT = 0.6f;

	private global::UnityEngine.AudioClip m_tempAc;

	private global::System.Collections.Generic.List<string> tempTimestamps = new global::System.Collections.Generic.List<string>();

	private global::System.Collections.Generic.List<string> tempTextLines = new global::System.Collections.Generic.List<string>();

	private global::System.Collections.Generic.List<string> tempCharNames = new global::System.Collections.Generic.List<string>();

	private global::UnityEngine.Coroutine m_currentTextCoroutine;

	private int m_currentDialgoueIndex;

	public bool m_currentyReading;

	private double m_dialogueStartTs;

	private float m_skipHeldTimer;

	private bool m_isHoldingSkip;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_skipArea;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_skipCircle;

	[global::UnityEngine.SerializeField]
	private DataHolder m_dataholder;

	[global::UnityEngine.SerializeField]
	private CharacterData m_defaultCharacterData;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Sprite m_playButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Sprite m_pauseButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_pausePlayButtonImg;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_backToMenuButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_pauseButtonObj;

	public bool IsPaused;

	private double pauseStartTime;

	private double totalPausedDuration;

	private double lastDspTime;

	[global::UnityEngine.Header("Ambience Crossfade")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioSource m_ambienceSource;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioSource m_ambienceSourceB;

	[global::UnityEngine.SerializeField]
	private float m_ambienceCrossfadeDuration = 1.5f;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Range(0f, 1f)]
	private float m_ambienceTargetVolume = 1f;

	private global::UnityEngine.AudioSource m_activeAmbienceSource;

	private global::UnityEngine.AudioSource m_inactiveAmbienceSource;

	private global::UnityEngine.Coroutine m_ambienceLoopWatcher;

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

	private void PauseLogicalDSP()
	{
		pauseStartTime = global::UnityEngine.AudioSettings.dspTime;
		IsPaused = true;
	}

	private void ResumeLogicalDSP()
	{
		totalPausedDuration += global::UnityEngine.AudioSettings.dspTime - pauseStartTime;
		IsPaused = false;
	}

	private double GetLogicalDspTime()
	{
		return global::UnityEngine.AudioSettings.dspTime - totalPausedDuration;
	}

	private void SubscribeToInput()
	{
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(MoveReader));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance2.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(MoveReader));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance3.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(MoveReader));
		PersistentInputReader instance4 = PersistentInputReader.Instance;
		instance4.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance4.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryPause));
		PersistentInputReader instance5 = PersistentInputReader.Instance;
		instance5.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance5.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryPause));
		PersistentInputReader instance6 = PersistentInputReader.Instance;
		instance6.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance6.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryPause));
	}

	private void UnsubscribeToInput()
	{
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(MoveReader));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance2.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(MoveReader));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance3.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(MoveReader));
		PersistentInputReader instance4 = PersistentInputReader.Instance;
		instance4.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance4.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryPause));
		PersistentInputReader instance5 = PersistentInputReader.Instance;
		instance5.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance5.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryPause));
		PersistentInputReader instance6 = PersistentInputReader.Instance;
		instance6.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance6.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryPause));
	}

	private void TryPause(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		TogglePause();
	}

	private void MoveReader(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		global::UnityEngine.Vector2 vector = context.ReadValue<global::UnityEngine.Vector2>();
		if (vector == global::UnityEngine.Vector2.right || (context.canceled && m_isHoldingSkip))
		{
			TrySkip(context);
		}
		else if (vector == global::UnityEngine.Vector2.left && context.performed)
		{
			TryGoBack(context);
		}
	}

	private void Update()
	{
		if (global::UnityEngine.Input.anyKeyDown && m_isCampaignMode && m_campaignContinueTriangle.activeSelf && m_currentDialgoueIndex + 1 >= tempTimestamps.Count - 1)
		{
			NextDialogue();
		}
		if (m_isHoldingSkip && m_currentyReading)
		{
			m_skipHeldTimer += global::UnityEngine.Time.deltaTime;
			if (!(m_skipHeldTimer > 0.2f))
			{
				return;
			}
			if (m_isShortWakDialogue)
			{
				m_altSkipObj.SetActive(value: true);
				m_altSkipFillCirlce.fillAmount = global::UnityEngine.Mathf.Clamp(m_skipHeldTimer / 0.8f, 0f, 1f);
			}
			else
			{
				m_skipArea.SetActive(value: true);
				m_skipCircle.fillAmount = global::UnityEngine.Mathf.Clamp(m_skipHeldTimer / 0.8f, 0f, 1f);
			}
			if (!(m_skipHeldTimer > 0.8f))
			{
				return;
			}
			m_isHoldingSkip = false;
			if (m_isShortWakDialogue)
			{
				m_altSkipObj.SetActive(value: false);
			}
			else
			{
				m_skipArea.SetActive(value: false);
			}
			m_skipHeldTimer = 0f;
			if (m_currentyReading)
			{
				if (m_currentTextCoroutine != null)
				{
					StopCoroutine(m_currentTextCoroutine);
				}
				m_currentyReading = false;
			}
			DialogueFinished();
		}
		else if (m_isShortWakDialogue)
		{
			m_altSkipObj.SetActive(value: false);
		}
		else
		{
			m_skipArea.SetActive(value: false);
		}
	}

	public void SkipOne()
	{
		NextDialogue();
	}

	public void GoBackOne()
	{
		GoToPreviousDialogue();
	}

	public void TogglePause()
	{
		if (IsPaused)
		{
			UnpauseText();
		}
		else
		{
			PauseText();
		}
	}

	public void PauseText()
	{
		if (m_currentyReading)
		{
			m_pausePlayButtonImg.sprite = m_playButton;
			m_backToMenuButton.SetActive(value: true);
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_pauseButtonObj);
			m_audioSource.Pause();
			m_ambienceSource?.Pause();
			m_ambienceSourceB?.Pause();
			PauseLogicalDSP();
		}
	}

	public void UnpauseText()
	{
		if (m_currentyReading)
		{
			m_pausePlayButtonImg.sprite = m_pauseButton;
			m_backToMenuButton.SetActive(value: false);
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_pausePlayButtonImg.gameObject);
			m_audioSource.Play();
			m_ambienceSource?.Play();
			m_ambienceSourceB?.Play();
			ResumeLogicalDSP();
		}
	}

	public void GoBackToMenu()
	{
		UnsubscribeToInput();
		global::UnityEngine.Object.Destroy(global::UnityEngine.EventSystems.EventSystem.current.gameObject);
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: true);
		GameManager.Instance.LoadMenu();
	}

	public void SetStageIndicator(string name)
	{
		m_stageText.gameObject.SetActive(value: true);
		m_stageText.text = name;
	}

	public void ReadSegment(AudioSegmentDataSO segmentData, bool isShortWAK = false)
	{
		AudioManager.Instance.SetMusicSpeed(1f);
		SubscribeToInput();
		m_skipHeldTimer = 0f;
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: false);
		m_dialogueVis.SetActive(value: true);
		m_currentDialgoueIndex = -1;
		m_tempAc = segmentData.Clip;
		m_audioSource.clip = m_tempAc;
		tempTimestamps = global::System.Linq.Enumerable.ToList(segmentData.Timestamps.Split(new string[2] { "\r\n", "\n" }, global::System.StringSplitOptions.None));
		tempTextLines = global::System.Linq.Enumerable.ToList(segmentData.WrittenDialogue.Split(new string[2] { "\r\n", "\n" }, global::System.StringSplitOptions.None));
		tempCharNames = global::System.Linq.Enumerable.ToList(segmentData.DialogueCharacterNames.Split(new string[2] { "\r\n", "\n" }, global::System.StringSplitOptions.None));
		if (m_isCampaignMode)
		{
			SetupPortraits();
			m_ambienceSource.gameObject.SetActive(value: true);
			LevelDataSO currentLevelDataSO = CampaignManager.Instance.CurrentLevelDataSO;
			if (currentLevelDataSO.ReverbPreset == global::UnityEngine.AudioReverbPreset.User && currentLevelDataSO.CustomReverbPreset != null)
			{
				currentLevelDataSO.CustomReverbPreset.ApplyTo(m_dialogueReverb);
			}
			else
			{
				m_dialogueReverb.reverbPreset = currentLevelDataSO.ReverbPreset;
			}
			PlayAmbienceLooped(currentLevelDataSO.AmbienceTrack);
			m_extraButtonsObj.SetActive(value: true);
			m_slideInAnim.Play();
		}
		m_currentyReading = true;
		if (isShortWAK)
		{
			LevelDataSO currentLevelDataSO2 = LineClearingManager.Instance.CurrentLevelDataSO;
			m_isShortWakDialogue = true;
			m_bottomShadow.DOFade(1f, 0.8f);
			m_ambienceSource.gameObject.SetActive(value: true);
			PlayAmbienceLooped(currentLevelDataSO2.AmbienceTrack);
		}
		else
		{
			m_bottomShadow.DOFade(1f, 0.8f);
			m_isShortWakDialogue = false;
			m_ambienceSource.gameObject.SetActive(value: true);
			if (!m_isCampaignMode)
			{
				LevelDataSO currentLevelDataSO3 = LineClearingManager.Instance.CurrentLevelDataSO;
				if (currentLevelDataSO3.ReverbPreset == global::UnityEngine.AudioReverbPreset.User && currentLevelDataSO3.CustomReverbPreset != null)
				{
					currentLevelDataSO3.CustomReverbPreset.ApplyTo(m_dialogueReverb);
				}
				else
				{
					m_dialogueReverb.reverbPreset = currentLevelDataSO3.ReverbPreset;
				}
				PlayAmbienceLooped(currentLevelDataSO3.AmbienceTrack);
			}
			if (!m_isCampaignMode)
			{
				m_extraButtonsObj.SetActive(value: true);
			}
		}
		NextDialogue();
	}

	public void TrySkip(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!m_currentyReading)
		{
			return;
		}
		if (context.started)
		{
			m_skipHeldTimer = 0f;
			m_isHoldingSkip = true;
		}
		if (context.canceled)
		{
			if (m_skipHeldTimer < 0.3f)
			{
				NextDialogue();
			}
			m_skipHeldTimer = 0f;
			m_isHoldingSkip = false;
		}
	}

	public void TryGoBack(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (m_currentyReading && context.performed)
		{
			double.TryParse(tempTimestamps[m_currentDialgoueIndex], out var result);
			double result2;
			if (m_currentDialgoueIndex == tempTimestamps.Count - 2)
			{
				result2 = m_tempAc.length;
			}
			else
			{
				double.TryParse(tempTimestamps[m_currentDialgoueIndex + 1], out result2);
			}
			double num = result2 - result;
			double num2 = GetLogicalDspTime() - m_dialogueStartTs;
			if (num <= 3.0)
			{
				GoToPreviousDialogue();
			}
			else if (num2 > num / 2.0)
			{
				RepeatCurrentDialogue();
			}
			else
			{
				GoToPreviousDialogue();
			}
		}
	}

	public void NextDialogue()
	{
		if (IsPaused)
		{
			UnpauseText();
		}
		if (m_currentyReading)
		{
			if (m_currentTextCoroutine != null)
			{
				StopCoroutine(m_currentTextCoroutine);
			}
			m_currentDialgoueIndex++;
			if (m_currentDialgoueIndex >= tempTimestamps.Count - 1)
			{
				DialogueFinished();
			}
			else if (m_currentDialgoueIndex == tempTimestamps.Count - 2)
			{
				float.TryParse(tempTimestamps[m_currentDialgoueIndex], out var result);
				m_currentTextCoroutine = StartCoroutine(WriteText(result, m_tempAc.length, tempTextLines[m_currentDialgoueIndex], tempCharNames[m_currentDialgoueIndex]));
			}
			else
			{
				double.TryParse(tempTimestamps[m_currentDialgoueIndex], out var result2);
				double.TryParse(tempTimestamps[m_currentDialgoueIndex + 1], out var result3);
				m_currentTextCoroutine = StartCoroutine(WriteText(result2, result3, tempTextLines[m_currentDialgoueIndex], tempCharNames[m_currentDialgoueIndex]));
			}
		}
	}

	public void RepeatCurrentDialogue()
	{
		if (m_currentyReading)
		{
			if (IsPaused)
			{
				UnpauseText();
			}
			if (m_currentTextCoroutine != null)
			{
				StopCoroutine(m_currentTextCoroutine);
				double.TryParse(tempTimestamps[m_currentDialgoueIndex], out var result);
				double.TryParse(tempTimestamps[m_currentDialgoueIndex + 1], out var result2);
				m_currentTextCoroutine = StartCoroutine(WriteText(result, result2, tempTextLines[m_currentDialgoueIndex], tempCharNames[m_currentDialgoueIndex]));
			}
		}
	}

	public void GoToPreviousDialogue()
	{
		if (m_currentyReading)
		{
			if (IsPaused)
			{
				UnpauseText();
			}
			if (m_currentTextCoroutine != null && m_currentDialgoueIndex > 0)
			{
				StopCoroutine(m_currentTextCoroutine);
				m_currentDialgoueIndex--;
				double.TryParse(tempTimestamps[m_currentDialgoueIndex], out var result);
				double.TryParse(tempTimestamps[m_currentDialgoueIndex + 1], out var result2);
				m_currentTextCoroutine = StartCoroutine(WriteText(result, result2, tempTextLines[m_currentDialgoueIndex], tempCharNames[m_currentDialgoueIndex]));
			}
			else
			{
				RepeatCurrentDialogue();
			}
		}
	}

	public void DialogueFinished()
	{
		UnsubscribeToInput();
		m_skipHeldTimer = 0f;
		m_isHoldingSkip = false;
		StopAmbienceFaded(1.4f);
		if (m_isCampaignMode)
		{
			m_campaignContinueTriangle.SetActive(value: false);
			m_extraButtonsObj.SetActive(value: false);
			AnimateOutCampaign();
		}
		if (!m_isCampaignMode && m_extraButtonsObj != null)
		{
			m_extraButtonsObj.SetActive(value: false);
		}
		m_stageText?.gameObject.SetActive(value: false);
		m_bottomShadow.DOFade(0f, 0.6f);
		AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic, isFromDialogue: true);
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: true);
		m_audioSource?.Stop();
		global::UnityEngine.Debug.Log("Finished");
		DialogueFinishedEvent.Invoke();
		m_currentyReading = false;
		m_dialogueVis.SetActive(value: false);
	}

	public void SetUpCampaignData(int difficulty)
	{
		global::UnityEngine.Debug.Log(difficulty + "DIFF");
		ActivePlayerCharsCampaign.Clear();
		if (difficulty == 0)
		{
			ActivePlayerCharsCampaign.Add("nicole");
		}
		if (difficulty == 1)
		{
			ActivePlayerCharsCampaign.Add("jecka");
		}
		if (difficulty == 2)
		{
			ActivePlayerCharsCampaign.Add("ari");
		}
		if (difficulty == 3)
		{
			ActivePlayerCharsCampaign.Add("emily");
		}
		if (difficulty == 4)
		{
			ActivePlayerCharsCampaign.Add("nicole");
			ActivePlayerCharsCampaign.Add("jecka");
		}
	}

	public global::UnityEngine.Sprite GetPortraitByName(string name, bool isRight)
	{
		if (name == "the puzzler")
		{
			for (int i = 0; i < m_dataholder.AllCharacterData.Count; i++)
			{
				if (m_dataholder.AllCharacterData[i].Type == CharacterType.Puzzler)
				{
					return m_dataholder.AllCharacterData[i].CharacterPortraitLeft;
				}
			}
		}
		if (name == "gamer brother")
		{
			for (int j = 0; j < m_dataholder.AllCharacterData.Count; j++)
			{
				if (m_dataholder.AllCharacterData[j].Type == CharacterType.GamerBrother)
				{
					return m_dataholder.AllCharacterData[j].CharacterPortraitLeft;
				}
			}
		}
		if (name == "coach colby")
		{
			for (int k = 0; k < m_dataholder.AllCharacterData.Count; k++)
			{
				if (m_dataholder.AllCharacterData[k].Type == CharacterType.CoachColby)
				{
					return m_dataholder.AllCharacterData[k].CharacterPortraitLeft;
				}
			}
		}
		if (name == "principal lynn")
		{
			for (int l = 0; l < m_dataholder.AllCharacterData.Count; l++)
			{
				if (m_dataholder.AllCharacterData[l].Type == CharacterType.Principal)
				{
					return m_dataholder.AllCharacterData[l].CharacterPortraitLeft;
				}
			}
		}
		if (name == "the chustler")
		{
			for (int m = 0; m < m_dataholder.AllCharacterData.Count; m++)
			{
				if (m_dataholder.AllCharacterData[m].Type == CharacterType.Chustler)
				{
					return m_dataholder.AllCharacterData[m].CharacterPortraitLeft;
				}
			}
		}
		if (name == "jeffery" && CampaignManager.Instance.CurrentLevelDataSO.EnemyChar == CharacterType.DarkJefferey)
		{
			for (int n = 0; n < m_dataholder.AllCharacterData.Count; n++)
			{
				if (m_dataholder.AllCharacterData[n].Type == CharacterType.DarkJefferey)
				{
					return m_dataholder.AllCharacterData[n].CharacterPortraitLeft;
				}
			}
		}
		if (name == "jecka’s dad" && CampaignManager.Instance.CurrentLevelDataSO.EnemyChar == CharacterType.Dad)
		{
			for (int num = 0; num < m_dataholder.AllCharacterData.Count; num++)
			{
				if (m_dataholder.AllCharacterData[num].Type == CharacterType.Dad)
				{
					return m_dataholder.AllCharacterData[num].CharacterPortraitLeft;
				}
			}
		}
		if (global::System.Enum.TryParse(typeof(CharacterType), name, ignoreCase: true, out var result))
		{
			foreach (CharacterData allCharacterDatum in m_dataholder.AllCharacterData)
			{
				if (allCharacterDatum.Type == (CharacterType)result)
				{
					if (isRight)
					{
						return allCharacterDatum.CharacterPortraitRight;
					}
					return allCharacterDatum.CharacterPortraitLeft;
				}
			}
		}
		return null;
	}

	public global::UnityEngine.Color GetColorForCharacter(string character)
	{
		return m_defaultCharacterData.PodColorShiftValue;
	}

	private void AnimateOutCampaign()
	{
		if (m_isCampaignMode)
		{
			CampaignBubbles[0].SetActive(value: false);
			CampaignBubbles[1].SetActive(value: false);
			CampaignPortraits[0].gameObject.SetActive(value: false);
			CampaignPortraits[1].gameObject.SetActive(value: false);
		}
	}

	private void SetupPortraits()
	{
		CampaignPortraits[0].sprite = GetPortraitByName(ActivePlayerCharsCampaign[0].ToLower(), isRight: false);
		if (CampaignManager.Instance.CurrentLevelDataSO.EnemyChar == CharacterType.Chustler && CampaignManager.Instance.CurrentLevelDataSO.PlayersChar == CharacterType.Jecka)
		{
			CampaignPortraits[1].color = global::UnityEngine.Color.black;
		}
		else
		{
			CampaignPortraits[1].color = global::UnityEngine.Color.white;
		}
		for (int i = 0; i < tempCharNames.Count; i++)
		{
			if (!tempCharNames[i].ToLower().Equals(ActivePlayerCharsCampaign[0].ToLower()))
			{
				CampaignPortraits[1].sprite = GetPortraitByName(tempCharNames[i].ToLower(), isRight: true);
				break;
			}
		}
	}

	public global::System.Collections.IEnumerator WriteText(double startTS, double endTS, string text, string characterName)
	{
		if (m_isCampaignMode)
		{
			m_campaignContinueTriangle.SetActive(value: false);
			CampaignPortraits[0].gameObject.SetActive(value: true);
			CampaignPortraits[1].gameObject.SetActive(value: true);
			if (ActivePlayerCharsCampaign.Contains(characterName.ToLower()))
			{
				CampaignBubbles[1].SetActive(value: false);
				CampaignBubbles[0].SetActive(value: true);
				m_speechText = CampaignTextFields[0];
				CampaignPortraits[0].sprite = GetPortraitByName(characterName.ToLower(), isRight: false);
			}
			else
			{
				if (CampaignManager.Instance.CurrentLevelDataSO.EnemyChar == CharacterType.Chustler && CampaignManager.Instance.CurrentLevelDataSO.PlayersChar == CharacterType.Jecka && CampaignPortraits[1].color == global::UnityEngine.Color.black)
				{
					CampaignPortraits[1].DOColor(global::UnityEngine.Color.white, 1f);
				}
				else
				{
					CampaignPortraits[1].color = global::UnityEngine.Color.white;
				}
				CampaignBubbles[0].SetActive(value: false);
				CampaignBubbles[1].SetActive(value: true);
				m_speechText = CampaignTextFields[1];
				CampaignPortraits[1].sprite = GetPortraitByName(characterName.ToLower(), isRight: true);
			}
		}
		m_dialogueStartTs = GetLogicalDspTime();
		m_audioSource.Play();
		ProcessText(text, out text, out var timeDelayDictionary, out var totalPauseTime);
		bool isHidden = false;
		if (characterName == "ACTION")
		{
			m_dialogueVis.SetActive(value: false);
			isHidden = true;
			m_nameText.text = "";
		}
		else
		{
			m_dialogueVis.SetActive(value: true);
			m_nameText.text = characterName;
		}
		if (m_isCampaignMode)
		{
			m_dialogueVis.SetActive(value: false);
		}
		int timeSamples = (int)((double)m_tempAc.samples * (startTS / (double)m_tempAc.length));
		double num = endTS - startTS;
		double startAudioTime = GetLogicalDspTime();
		double endAudioTime = GetLogicalDspTime() + num;
		m_audioSource.timeSamples = timeSamples;
		global::UnityEngine.Debug.Log(text);
		global::UnityEngine.Debug.Log(num);
		global::UnityEngine.Debug.Log(endTS);
		double timePerChar = (num - (double)TIME_ADJUSTMENT_FOR_TEXT - (double)totalPauseTime) / (double)text.Length;
		int displayIndex = 0;
		float holdTimeToNextIndex = 0f;
		if (!m_isCampaignMode)
		{
			m_speechText.color = GetColorForCharacter(characterName);
		}
		while (endAudioTime > GetLogicalDspTime() || IsPaused)
		{
			if (IsPaused)
			{
				yield return null;
				continue;
			}
			GetLogicalDspTime();
			_ = startAudioTime;
			if (timePerChar * (double)displayIndex + (double)holdTimeToNextIndex < GetLogicalDspTime() - startAudioTime && displayIndex < text.Length)
			{
				displayIndex++;
				if (timeDelayDictionary.ContainsKey(displayIndex))
				{
					holdTimeToNextIndex = timeDelayDictionary[displayIndex];
				}
			}
			if (isHidden)
			{
				displayIndex = 0;
			}
			m_speechText.text = text.Substring(0, displayIndex) + colorTag + text.Substring(displayIndex) + "</color>";
			yield return null;
		}
		m_audioSource.Stop();
		if (!m_isCampaignMode || !m_isCampaignMode || m_currentDialgoueIndex + 1 < tempTimestamps.Count - 1)
		{
			NextDialogue();
		}
		else
		{
			m_campaignContinueTriangle.SetActive(value: true);
		}
	}

	private void ProcessText(string inText, out string outText, out global::System.Collections.Generic.Dictionary<int, float> outTimeDelayDictionary, out float totalPauseTime)
	{
		outText = inText;
		outTimeDelayDictionary = new global::System.Collections.Generic.Dictionary<int, float>();
		totalPauseTime = 0f;
		string[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(inText.Split('{', '}'), (string s, int i) => i % 2 == 1), (string s) => "{" + s + "}"));
		for (int num = 0; num < array.Length; num++)
		{
			global::UnityEngine.Debug.Log(array[num]);
			string text = "";
			for (int num2 = 1; num2 < array[num].Length - 1; num2++)
			{
				text += array[num][num2];
			}
			global::UnityEngine.Debug.Log(text);
			float.TryParse(text, out var result);
			totalPauseTime += result;
			outTimeDelayDictionary.Add(outText.IndexOf(array[num]), result);
			global::System.Text.RegularExpressions.Regex regex = new global::System.Text.RegularExpressions.Regex(global::System.Text.RegularExpressions.Regex.Escape(array[num]));
			outText = regex.Replace(outText, string.Empty, 1);
		}
	}

	private void PlayAmbienceLooped(global::UnityEngine.AudioClip clip)
	{
		if (!(clip == null))
		{
			m_activeAmbienceSource = m_ambienceSource;
			m_inactiveAmbienceSource = m_ambienceSourceB;
			m_activeAmbienceSource.clip = clip;
			m_activeAmbienceSource.loop = false;
			m_activeAmbienceSource.volume = 0f;
			m_activeAmbienceSource.Play();
			m_activeAmbienceSource.DOFade(m_ambienceTargetVolume, 1f);
			if (m_ambienceLoopWatcher != null)
			{
				StopCoroutine(m_ambienceLoopWatcher);
			}
			m_ambienceLoopWatcher = StartCoroutine(AmbienceLoopWatcher());
		}
	}

	private global::System.Collections.IEnumerator AmbienceLoopWatcher()
	{
		while (true)
		{
			if (m_activeAmbienceSource == null || m_activeAmbienceSource.clip == null || !m_activeAmbienceSource.isPlaying)
			{
				yield return null;
				continue;
			}
			float num = m_activeAmbienceSource.clip.length - m_activeAmbienceSource.time;
			if (num <= m_ambienceCrossfadeDuration)
			{
				double time = global::UnityEngine.AudioSettings.dspTime + 0.02;
				global::UnityEngine.AudioSource inactiveAmbienceSource = m_inactiveAmbienceSource;
				global::UnityEngine.AudioSource activeAmbienceSource = m_activeAmbienceSource;
				inactiveAmbienceSource.clip = activeAmbienceSource.clip;
				inactiveAmbienceSource.volume = 0f;
				inactiveAmbienceSource.loop = false;
				inactiveAmbienceSource.PlayScheduled(time);
				activeAmbienceSource.DOFade(0f, num);
				inactiveAmbienceSource.DOFade(m_ambienceTargetVolume, num);
				m_activeAmbienceSource = inactiveAmbienceSource;
				m_inactiveAmbienceSource = activeAmbienceSource;
				StartCoroutine(StopSourceAfter(activeAmbienceSource, num + 0.1f));
				yield return WaitRespectingPause(num);
			}
			else
			{
				yield return null;
			}
		}
	}

	private global::System.Collections.IEnumerator StopSourceAfter(global::UnityEngine.AudioSource src, float delay)
	{
		yield return new global::UnityEngine.WaitForSeconds(delay);
		src.Stop();
	}

	private void StopAmbienceFaded(float duration)
	{
		if (m_ambienceLoopWatcher != null)
		{
			StopCoroutine(m_ambienceLoopWatcher);
			m_ambienceLoopWatcher = null;
		}
		float fadeDuration = 0f;
		if (m_ambienceSource != null && m_ambienceSource.isPlaying)
		{
			m_ambienceSource.DOFade(0f, duration);
			fadeDuration = duration;
		}
		if (m_ambienceSourceB != null && m_ambienceSourceB.isPlaying)
		{
			m_ambienceSourceB.DOFade(0f, duration);
			fadeDuration = duration;
		}
		// Both fades run in parallel, so a bare timer of the same length stands in
		// for the sequence that used to own them.
		SimpleTween.Create(null, fadeDuration, null).OnComplete(delegate
		{
			m_ambienceSource?.Stop();
			m_ambienceSourceB?.Stop();
			m_ambienceSource?.gameObject.SetActive(value: false);
		});
	}

	private global::System.Collections.IEnumerator WaitRespectingPause(float seconds)
	{
		float t = 0f;
		while (t < seconds)
		{
			if (!IsPaused)
			{
				t += global::UnityEngine.Time.deltaTime;
			}
			yield return null;
		}
	}
}
