public class StartupScreen : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Video.VideoPlayer m_vidPlayer;

	private bool m_hasPlayed;

	[global::UnityEngine.SerializeField]
	private CutsceneVideoIndicies[] m_allIntroVideosInOrder;

	private int VIDINDEX;

	[global::UnityEngine.SerializeField]
	private CutscenePlayer m_CutscenePlayer;

	private const string STORY_PARAM = "StoryVolume";

	private const string MENU_PARAM = "MenuVolume";

	private const string GAME_PARAM = "GameVolume";

	private const string MASTER_PARAM = "MasterVolume";

	private void Start()
	{
		VIDINDEX = 0;
		InitSettings();
		StartCoroutine(DelayedStart());
		global::UnityEngine.Debug.unityLogger.logEnabled = false;
	}

	private void Awake()
	{
		global::UnityEngine.Debug.unityLogger.logEnabled = false;
	}

	private global::System.Collections.IEnumerator DelayedStart()
	{
		yield return null;
		m_CutscenePlayer.PlayCutscene(m_allIntroVideosInOrder[VIDINDEX], playSilently: false, playWithAnySkip: true);
		m_CutscenePlayer.CutsceneFinishedEvent.AddListener(CutsceneDone);
	}

	private void CutsceneDone()
	{
		m_CutscenePlayer.CutsceneFinishedEvent.RemoveAllListeners();
		VIDINDEX++;
		if (VIDINDEX == m_allIntroVideosInOrder.Length - 1)
		{
			m_CutscenePlayer.PlayCutscene(m_allIntroVideosInOrder[VIDINDEX], playSilently: false, playWithAnySkip: false, activatePanel: true);
			m_CutscenePlayer.CutsceneFinishedEvent.AddListener(GoToSplashScreen);
		}
		else
		{
			m_CutscenePlayer.PlayCutscene(m_allIntroVideosInOrder[VIDINDEX], playSilently: false, playWithAnySkip: true);
			m_CutscenePlayer.CutsceneFinishedEvent.AddListener(CutsceneDone);
		}
	}

	private void InitSettings()
	{
		AudioManager.Instance.SetMasterVolume(global::UnityEngine.PlayerPrefs.GetFloat("MasterVolume", 1f));
		AudioManager.Instance.SetMenuVolume(global::UnityEngine.PlayerPrefs.GetFloat("MenuVolume", 1f));
		AudioManager.Instance.SetGameVolume(global::UnityEngine.PlayerPrefs.GetFloat("GameVolume", 1f));
		AudioManager.Instance.SetStoryVolume(global::UnityEngine.PlayerPrefs.GetFloat("StoryVolume", 1f));
		global::UnityEngine.Screen.fullScreen = global::UnityEngine.PlayerPrefs.GetInt("Fullscreen", 1) == 1;
		global::UnityEngine.QualitySettings.vSyncCount = ((global::UnityEngine.PlayerPrefs.GetInt("Vsync", 1) == 1) ? 1 : 0);
	}

	public void GoToSplashScreen()
	{
		m_CutscenePlayer.CutsceneFinishedEvent.RemoveAllListeners();
		m_hasPlayed = true;
		SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.SplashScreen);
	}
}
