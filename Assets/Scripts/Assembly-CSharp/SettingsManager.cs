public class SettingsManager : global::UnityEngine.MonoBehaviour
{
	private const string STORY_PARAM = "StoryVolume";

	private const string MENU_PARAM = "MenuVolume";

	private const string GAME_PARAM = "GameVolume";

	private const string MASTER_PARAM = "MasterVolume";

	[global::UnityEngine.Header("Mixer")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Audio.AudioMixer gameMixer;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Slider m_masterSlider;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Slider m_GameSlider;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Slider m_StorySlider;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Slider m_MenuSlider;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Toggle m_CheerToggle;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Toggle m_FsToggle;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Toggle m_VsToggle;

	[global::UnityEngine.SerializeField]
	private VoiceBoxSO m_testingVoiceBox;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.AudioClip> m_GameClips;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.AudioClip> m_StoryClips;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.AudioClip> m_MenuClips;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.GameObject> m_optionPanels;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.GameObject> m_firstSelected;

	private bool m_canMakeSliderSound;

	private void Start()
	{
		InitSettings();
	}

	private void InitSettings()
	{
		m_FsToggle.onValueChanged.RemoveListener(SetFullscreen);
		m_FsToggle.isOn = global::UnityEngine.Screen.fullScreen;
		m_FsToggle.onValueChanged.AddListener(SetFullscreen);
		bool isOn = global::UnityEngine.PlayerPrefs.GetInt("Vsync", 1) == 1;
		m_VsToggle.isOn = isOn;
		m_canMakeSliderSound = false;
		m_masterSlider.SetValueWithoutNotify(AudioManager.Instance.GetSavedMasterVolume());
		m_MenuSlider.SetValueWithoutNotify(AudioManager.Instance.GetSavedMenuVolume());
		m_GameSlider.SetValueWithoutNotify(AudioManager.Instance.GetSavedGameVolume());
		m_StorySlider.SetValueWithoutNotify(AudioManager.Instance.GetSavedStoryVolume());
		if (global::UnityEngine.PlayerPrefs.HasKey("CHEERS"))
		{
			m_CheerToggle.isOn = global::UnityEngine.PlayerPrefs.GetInt("CHEERS") > 0;
		}
		m_StorySlider.GetComponent<SliderPointerUp>().OnRelease.AddListener(OnStoryVolumeReleased);
		m_GameSlider.GetComponent<SliderPointerUp>().OnRelease.AddListener(OnGameVolumeReleased);
		m_MenuSlider.GetComponent<SliderPointerUp>().OnRelease.AddListener(OnMenuVolumeReleased);
		SetActiveMenuPanel(0);
	}

	public void PressSoundMenu()
	{
		SetActiveMenuPanel(1);
	}

	public void PressVideoMenu()
	{
		SetActiveMenuPanel(2);
	}

	public void PressControls()
	{
		SetActiveMenuPanel(3);
	}

	public void GotoSelectionMenu()
	{
		SetActiveMenuPanel(0);
	}

	private void SetActiveMenuPanel(int index)
	{
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
		for (int i = 0; i < m_optionPanels.Count; i++)
		{
			if (i == index)
			{
				m_optionPanels[i].SetActive(value: true);
			}
			else
			{
				m_optionPanels[i].SetActive(value: false);
			}
		}
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_firstSelected[index]);
		PersistentInputReader.Instance.CustomMenuCtrlSwapper.SetSelected(m_firstSelected[index].GetComponent<global::UnityEngine.UI.Selectable>());
	}

	private void OnStoryVolumeReleased()
	{
		if (m_canMakeSliderSound)
		{
			m_testingVoiceBox.GetGroup(2);
			AudioManager.Instance.PlayStorySfx(m_StoryClips[global::UnityEngine.Random.Range(0, m_StoryClips.Count)], 1f, antiOverlap: false);
		}
	}

	private void OnGameVolumeReleased()
	{
		if (m_canMakeSliderSound)
		{
			AudioManager.Instance.PlaySfx(m_GameClips[global::UnityEngine.Random.Range(0, m_GameClips.Count)], 1f, antiOverlap: false);
		}
	}

	private void OnMenuVolumeReleased()
	{
		_ = m_canMakeSliderSound;
	}

	public void BackToMainMenu()
	{
		SceneLoader.Instance.LoadSceneByEnumRegularFade(AllGameScenes.MainMenu);
	}

	public void SetMasterVolume(float value)
	{
		AudioManager.Instance.SetMasterVolume(value);
	}

	public void SetMenuVolume(float value)
	{
		AudioManager.Instance.SetMenuVolume(value);
	}

	public void SetStoryVolume(float value)
	{
		AudioManager.Instance.SetStoryVolume(value);
		if (PersistentInputReader.Instance.m_keyboardInput.currentControlScheme == "Gamepad")
		{
			OnStoryVolumeReleased();
		}
	}

	public void SetGameVolume(float value)
	{
		AudioManager.Instance.SetGameVolume(value);
		if (PersistentInputReader.Instance.m_keyboardInput.currentControlScheme == "Gamepad")
		{
			OnGameVolumeReleased();
		}
	}

	public void SetChainCheers(bool isOn)
	{
		global::UnityEngine.PlayerPrefs.SetInt("CHEERS", isOn ? 1 : 0);
	}

	public void SetFullscreen(bool value)
	{
		global::UnityEngine.Screen.fullScreen = value;
		global::UnityEngine.PlayerPrefs.SetInt("Fullscreen", value ? 1 : 0);
	}

	public void SetVsync(bool enabled)
	{
		global::UnityEngine.QualitySettings.vSyncCount = (enabled ? 1 : 0);
		global::UnityEngine.PlayerPrefs.SetInt("Vsync", enabled ? 1 : 0);
	}

	public void SelectOperationMode()
	{
		PersistentInputReader.Instance.m_keyboardInput.neverAutoSwitchControlSchemes = false;
		SceneLoader.Instance.LoadSceneByEnumRegularFade(AllGameScenes.OperationModeSelect);
	}
}
