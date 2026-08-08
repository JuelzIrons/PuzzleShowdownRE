public class SceneLoader : global::UnityEngine.MonoBehaviour
{
	public string BUILDNAME = "BUILD X";

	public static SceneLoader Instance;

	public static AllGameScenes CurrentScene;

	public static GameModeType ActiveGameMode;

	public bool IsLoading;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_buildNumText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Material m_transitionMaterialPrep;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_blackScreen;

	public global::UnityEngine.Events.UnityEvent SceneLoadedEvent;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Canvas m_canvas;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip m_waterSplashSound;

	public static readonly global::System.Collections.Generic.Dictionary<GameModeType, int> GameModeToSceneIndex = new global::System.Collections.Generic.Dictionary<GameModeType, int>
	{
		{
			GameModeType.LineClear,
			9
		},
		{
			GameModeType.Campaign,
			7
		},
		{
			GameModeType.Marathon,
			8
		},
		{
			GameModeType.LocalMp,
			16
		},
		{
			GameModeType.Online,
			12
		}
	};

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_loadCanvas;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.CanvasGroup m_blackFadeGroup;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_fadeImg;

	private global::UnityEngine.GameObject m_prevSelectedBlack;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			IsLoading = false;
			m_buildNumText.text = BUILDNAME;
		}
		else
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnEnable()
	{
		global::UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		global::UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public SimpleTween DoBlackFade(float inTime = 1f, float holdTime = 0f, float outTime = 1f, bool muteSound = false, global::System.Action onMiddleReached = null, global::System.Action onComplete = null, float waitTime = 0f, global::UnityEngine.Color clr = default(global::UnityEngine.Color))
	{
		m_blackFadeGroup.DOKill();
		m_blackFadeGroup.alpha = 0f;
		if (clr == default(global::UnityEngine.Color))
		{
			m_fadeImg.color = global::UnityEngine.Color.black;
		}
		else
		{
			m_fadeImg.color = clr;
		}
		float prevMasterVal = 0f;
		if (muteSound)
		{
			prevMasterVal = AudioManager.Instance.GetHardMasterVolume();
		}
		if (global::UnityEngine.EventSystems.EventSystem.current != null && global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null)
		{
			m_prevSelectedBlack = global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
		}
		else
		{
			m_prevSelectedBlack = null;
		}
		m_blackFadeGroup.blocksRaycasts = true;
		bool fadesBack = outTime < 100f;
		float fadeInEnd = waitTime + inTime;
		float holdEnd = fadeInEnd + holdTime;
		float total = holdEnd + (fadesBack ? outTime : 0f);
		bool middleReached = false;
		// The whole wait -> fade in -> hold -> fade out sequence runs as a single
		// tween so the handle we return completes at the end, not at the middle.
		SimpleTween tween = SimpleTween.Create(m_blackFadeGroup, total, delegate(float t)
		{
			float elapsed = t * total;
			if (elapsed < fadeInEnd)
			{
				m_blackFadeGroup.alpha = ((inTime > 0f) ? EaseOutQuad((elapsed - waitTime) / inTime) : 0f);
				return;
			}
			if (!middleReached)
			{
				middleReached = true;
				m_blackFadeGroup.alpha = 1f;
				onMiddleReached?.Invoke();
			}
			if (!fadesBack || elapsed < holdEnd)
			{
				m_blackFadeGroup.alpha = 1f;
				return;
			}
			float outProgress = ((outTime > 0f) ? global::UnityEngine.Mathf.Clamp01((elapsed - holdEnd) / outTime) : 1f);
			m_blackFadeGroup.alpha = 1f - EaseOutQuad(outProgress);
			if (muteSound)
			{
				AudioManager.Instance.SetMasterForced(global::UnityEngine.Mathf.Lerp(-80f, prevMasterVal, outProgress));
			}
		});
		tween.SetEase(SimpleTween.Ease.Linear);
		tween.SetUpdate(isIndependentUpdate: true);
		tween.OnComplete(delegate
		{
			if (!middleReached)
			{
				middleReached = true;
				onMiddleReached?.Invoke();
			}
			m_blackFadeGroup.blocksRaycasts = false;
			if (m_prevSelectedBlack != null)
			{
				global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_prevSelectedBlack);
			}
			onComplete?.Invoke();
		});
		return tween;
	}

	private static float EaseOutQuad(float t)
	{
		t = global::UnityEngine.Mathf.Clamp01(t);
		return t * (2f - t);
	}

	public void LoadSceneByEnumSplash(AllGameScenes scene)
	{
		global::UnityEngine.Debug.Log("CALLING LOAD SCENE BY ENUM");
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.enabled = false;
		}
		StartCoroutine(LoadYourAsyncSceneRegularFade((int)scene));
	}

	public void LoadSceneByEnumRegularFade(AllGameScenes scene, AllGameScenes extraSceneToUnload = AllGameScenes.DUMMY)
	{
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.enabled = false;
		}
		if (extraSceneToUnload != AllGameScenes.DUMMY && global::UnityEngine.SceneManagement.SceneManager.loadedSceneCount >= 2)
		{
			StartCoroutine(LoadYourAsyncSceneWaterSplash((int)scene, (int)extraSceneToUnload));
		}
		else
		{
			StartCoroutine(LoadYourAsyncSceneWaterSplash((int)scene));
		}
	}

	public void LoadSceneByEnumRegularFadeNoWater(AllGameScenes scene, AllGameScenes extraSceneToUnload = AllGameScenes.DUMMY)
	{
		global::UnityEngine.Debug.Log("CALLING LOAD SCENE BY ENUM");
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.enabled = false;
		}
		if (extraSceneToUnload != AllGameScenes.DUMMY && global::UnityEngine.SceneManagement.SceneManager.loadedSceneCount >= 2)
		{
			StartCoroutine(LoadYourAsyncSceneRegularFade((int)scene, (int)extraSceneToUnload));
		}
		else
		{
			StartCoroutine(LoadYourAsyncSceneRegularFade((int)scene));
		}
	}

	public void LoadSceneByActiveGameModeSplash()
	{
		StartCoroutine(LoadYourAsyncSceneWaterSplash(GameModeToSceneIndex[ActiveGameMode]));
	}

	public void LoadSceneByActiveGameModeRegularFade()
	{
		AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic);
		StartCoroutine(LoadYourAsyncSceneRegularFade(GameModeToSceneIndex[ActiveGameMode]));
	}

	private void OnSceneLoaded(global::UnityEngine.SceneManagement.Scene scene, global::UnityEngine.SceneManagement.LoadSceneMode mode)
	{
		CurrentScene = (AllGameScenes)global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
		SceneLoader[] array = global::UnityEngine.Object.FindObjectsByType<SceneLoader>(global::UnityEngine.FindObjectsSortMode.InstanceID);
		foreach (SceneLoader sceneLoader in array)
		{
			if (!(sceneLoader == this))
			{
				global::UnityEngine.Object.Destroy(sceneLoader);
			}
		}
		Instance = this;
	}

	private global::System.Collections.IEnumerator LoadYourAsyncSceneWaterSplash(int buildIndex, int? extraSceneToUnload = null)
	{
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: true);
		AudioManager.Instance.PlayMenuSfx(m_waterSplashSound, 0.5f, antiOverlap: false);
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.enabled = false;
		}
		m_transitionMaterialPrep.SetFloat("_Progress", 0f);
		m_transitionMaterialPrep.SetFloat("_Alpha", 1f);
		int currentSceneIndex = global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
		global::UnityEngine.Debug.Log($"LOAD {IsLoading} INIT!");
		if (IsLoading)
		{
			yield break;
		}
		global::UnityEngine.Debug.Log("LOAD INIT!");
		IsLoading = true;
		if (extraSceneToUnload.HasValue && extraSceneToUnload.Value != 16 && global::UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(extraSceneToUnload.Value).isLoaded)
		{
			global::UnityEngine.AsyncOperation extraUnload = global::UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(extraSceneToUnload.Value);
			while (!extraUnload.isDone)
			{
				yield return null;
			}
		}
		global::UnityEngine.AsyncOperation asyncLoad = global::UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(buildIndex, global::UnityEngine.SceneManagement.LoadSceneMode.Additive);
		LoadingScreenLogic.CaptureAndLock();
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		if (GameManager.Instance != null)
		{
			GameManager.Instance.OutOfGamemodeDestroy();
		}
		global::UnityEngine.SceneManagement.SceneManager.SetActiveScene(global::UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(buildIndex));
		global::UnityEngine.AsyncOperation asyncUnLoad = global::UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currentSceneIndex);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(m_loadCanvas);
		CameraResolution.CAM.GetComponent<CameraResolution>().AssignToAllCanvases();
		LoadingScreenLogic loadComp = gameObject.GetComponentInChildren<LoadingScreenLogic>();
		yield return new global::UnityEngine.WaitForSeconds(1.5f);
		while (!asyncUnLoad.isDone)
		{
			yield return null;
		}
		loadComp.ExitTransition();
		global::UnityEngine.Debug.Log("LOAD DONE!");
		IsLoading = false;
		SceneLoadedEvent.Invoke();
		PersistentInputReader.Instance.OnNewSceneLoaded();
		if (GameModeToSceneIndex[GameModeType.Marathon] == buildIndex || GameModeToSceneIndex[GameModeType.LineClear] == buildIndex || GameModeToSceneIndex[GameModeType.Campaign] == buildIndex || GameModeToSceneIndex[GameModeType.LocalMp] == buildIndex || GameModeToSceneIndex[GameModeType.Online] == buildIndex)
		{
			if (GameModeToSceneIndex[GameModeType.Online] != buildIndex)
			{
				PersistentInputReader.Instance.AllowPausing = true;
			}
		}
		else
		{
			PersistentInputReader.Instance.IsSelfUpdating = true;
			PersistentInputReader.Instance.AllowPausing = false;
			PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: false);
		}
	}

	private global::System.Collections.IEnumerator LoadYourAsyncSceneRegularFade(int buildIndex, int? extraSceneToUnload = null)
	{
		PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: true);
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.enabled = false;
		}
		int currentSceneIndex = global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
		global::UnityEngine.Debug.Log($"LOAD {IsLoading} INIT!");
		if (IsLoading)
		{
			yield break;
		}
		global::UnityEngine.Debug.Log("LOAD INIT!");
		IsLoading = true;
		if (extraSceneToUnload.HasValue && extraSceneToUnload.Value != 16 && global::UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(extraSceneToUnload.Value).isLoaded)
		{
			global::UnityEngine.AsyncOperation extraUnload = global::UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(extraSceneToUnload.Value);
			while (!extraUnload.isDone)
			{
				yield return null;
			}
		}
		global::UnityEngine.AsyncOperation asyncLoad = global::UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(buildIndex, global::UnityEngine.SceneManagement.LoadSceneMode.Additive);
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		global::UnityEngine.SceneManagement.SceneManager.SetActiveScene(global::UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(buildIndex));
		global::UnityEngine.AsyncOperation asyncUnLoad = global::UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currentSceneIndex);
		while (!asyncUnLoad.isDone)
		{
			yield return null;
		}
		global::UnityEngine.Debug.Log("LOAD DONE!");
		IsLoading = false;
		SceneLoadedEvent.Invoke();
		PersistentInputReader.Instance.OnNewSceneLoaded();
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.enabled = true;
		}
		if (GameModeToSceneIndex[GameModeType.Marathon] == buildIndex || GameModeToSceneIndex[GameModeType.LineClear] == buildIndex || GameModeToSceneIndex[GameModeType.Campaign] == buildIndex || GameModeToSceneIndex[GameModeType.LocalMp] == buildIndex || GameModeToSceneIndex[GameModeType.Online] == buildIndex)
		{
			if (GameModeToSceneIndex[GameModeType.Online] != buildIndex)
			{
				PersistentInputReader.Instance.AllowPausing = true;
			}
		}
		else
		{
			PersistentInputReader.Instance.IsSelfUpdating = true;
			PersistentInputReader.Instance.AllowPausing = false;
			PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: false);
		}
	}
}
