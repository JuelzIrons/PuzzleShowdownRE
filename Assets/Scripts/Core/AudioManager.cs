public class AudioManager : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_sfxPrefab;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_musicPrefab;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AnimationCurve m_roomSizeCurve;

	private float m_sfxTimestamp;

	private global::UnityEngine.AudioClip m_lastclip;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Playables.PlayableDirector m_musicDirector;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Playables.PlayableAsset[] m_playableAssets;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioSource m_as;

	[global::UnityEngine.Header("Mixer")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Audio.AudioMixer gameMixer;

	[global::UnityEngine.Header("Mixer Groups")]
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Audio.AudioMixerGroup StoryGroup;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Audio.AudioMixerGroup MenuGroup;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Audio.AudioMixerGroup GameGroup;

	private const string STORY_PARAM = "StoryVolume";

	private const string MENU_PARAM = "MenuVolume";

	private const string GAME_PARAM = "GameVolume";

	private const string MASTER_PARAM = "MasterVolume";

	private const float MIN_VOLUME = 0.0001f;

	private const string PP_MASTER = "Vol_Master";

	private const string PP_GAME = "Vol_Game";

	private const string PP_STORY = "Vol_Story";

	private const string PP_MENU = "Vol_Menu";

	private MusicTrackType m_currentPlayingSong;

	[global::UnityEngine.Header("Menu SFX Pool")]
	[global::UnityEngine.SerializeField]
	private int m_menuSfxPoolSize = 18;

	private global::System.Collections.Generic.List<global::UnityEngine.AudioSource> m_menuSfxPool = new global::System.Collections.Generic.List<global::UnityEngine.AudioSource>();

	private int m_menuSfxRoundRobin;

	private float[] m_voiceLineTimestamp = new float[2];

	private global::UnityEngine.AudioClip[] m_lastVLClip = new global::UnityEngine.AudioClip[2];

	private int[] m_currentVoiceLineValue = new int[2];

	private global::UnityEngine.GameObject[] m_activeVL = new global::UnityEngine.GameObject[2];

	private bool m_isFadingMaster;

	private float m_originalMasterValue;

	private SimpleTween m_masterFadeTween;

	private bool m_isFadingGame;

	private float m_originalGameValue;

	private SimpleTween m_gameFadeTween;

	private bool m_isFadingStory;

	private float m_originalStoryValue;

	private SimpleTween m_storyFadeTween;

	private bool m_isFadingMenu;

	private float m_originalMenuValue;

	private SimpleTween m_menuFadeTween;

	private SimpleTween m_speedTween;

	private float m_currentSpeedValue = 1f;

	public static AudioManager Instance { get; private set; }

	public global::UnityEngine.AudioSource MusicAS => m_as;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		Instance = this;
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		InitMenuSfxPool();
	}

	public MusicTrackType GetTrackForCharacter(CharacterType charType)
	{
		switch (charType)
		{
		case CharacterType.Nicole:
			return MusicTrackType.NicoleTheme;
		case CharacterType.Jecka:
			return MusicTrackType.JeckaTheme;
		case CharacterType.Ari:
			return MusicTrackType.AriTheme;
		case CharacterType.Emily:
			return MusicTrackType.EmilyTheme;
		case CharacterType.Kelly:
			if (GameManager.Instance.DefinedGameMode == GameModeType.Campaign && CampaignManager.Instance.CurrentLevelDataSO.PlayersChar != CharacterType.Kelly)
			{
				return GetTrackForCharacter(CampaignManager.Instance.CurrentLevelDataSO.PlayersChar);
			}
			return MusicTrackType.NicoleTheme;
		case CharacterType.Chustler:
			return MusicTrackType.ChustlerTheme;
		case CharacterType.Crispin:
			return MusicTrackType.CrispinTheme;
		case CharacterType.CoachColby:
			return MusicTrackType.CoachColbyTheme;
		case CharacterType.Cop:
			return MusicTrackType.CopTheme;
		case CharacterType.Dad:
			return MusicTrackType.JeckaDadTheme;
		case CharacterType.GamerBrother:
			return MusicTrackType.GamerBrotherTheme;
		case CharacterType.Jeffery:
			return MusicTrackType.JeffreyTheme;
		case CharacterType.Kylar:
			return MusicTrackType.KylarTheme;
		case CharacterType.Principal:
			return MusicTrackType.PrincipalLynnTheme;
		case CharacterType.Puzzler:
			return MusicTrackType.PuzzlersTheme;
		case CharacterType.DarkJefferey:
			return MusicTrackType.DarkJeffereyTheme;
		default:
			return MusicTrackType.NicoleTheme;
		}
	}

	private float LinearToDb(float linear)
	{
		linear = global::UnityEngine.Mathf.Max(linear, 0.0001f);
		return global::UnityEngine.Mathf.Log10(linear) * 20f;
	}

	public float GetHardMasterVolume()
	{
		gameMixer.GetFloat("MasterVolume", out var value);
		return value;
	}

	public float GetHardGameVolume()
	{
		gameMixer.GetFloat("GameVolume", out var value);
		return value;
	}

	public float GetHardStoryVolume()
	{
		gameMixer.GetFloat("StoryVolume", out var value);
		return value;
	}

	public float GetHardMenuVolume()
	{
		gameMixer.GetFloat("MenuVolume", out var value);
		return value;
	}

	public void SetMasterForced(float hardVal)
	{
		gameMixer.SetFloat("MasterVolume", hardVal);
	}

	public void SetGameForced(float hardVal)
	{
		gameMixer.SetFloat("GameVolume", hardVal);
	}

	public void SetStoryForced(float hardVal)
	{
		gameMixer.SetFloat("StoryVolume", hardVal);
	}

	public void SetMenuForced(float hardVal)
	{
		gameMixer.SetFloat("MenuVolume", hardVal);
	}

	public float GetSavedMasterVolume()
	{
		return global::UnityEngine.PlayerPrefs.GetFloat("Vol_Master", 1f);
	}

	public float GetSavedGameVolume()
	{
		return global::UnityEngine.PlayerPrefs.GetFloat("Vol_Game", 1f);
	}

	public float GetSavedStoryVolume()
	{
		return global::UnityEngine.PlayerPrefs.GetFloat("Vol_Story", 1f);
	}

	public float GetSavedMenuVolume()
	{
		return global::UnityEngine.PlayerPrefs.GetFloat("Vol_Menu", 1f);
	}

	public void SetMasterVolume(float sliderValue)
	{
		gameMixer.SetFloat("MasterVolume", LinearToDb(sliderValue));
		global::UnityEngine.PlayerPrefs.SetFloat("Vol_Master", sliderValue);
	}

	public void SetMenuVolume(float sliderValue)
	{
		gameMixer.SetFloat("MenuVolume", LinearToDb(sliderValue));
		global::UnityEngine.PlayerPrefs.SetFloat("Vol_Menu", sliderValue);
	}

	public void SetGameVolume(float sliderValue)
	{
		gameMixer.SetFloat("GameVolume", LinearToDb(sliderValue));
		global::UnityEngine.PlayerPrefs.SetFloat("Vol_Game", sliderValue);
	}

	public void SetStoryVolume(float sliderValue)
	{
		gameMixer.SetFloat("StoryVolume", LinearToDb(sliderValue));
		global::UnityEngine.PlayerPrefs.SetFloat("Vol_Story", sliderValue);
	}

	public void ChangeSong(MusicTrackType trackType, bool isFromDialogue = false, bool isFromGameplay = false)
	{
		if (MusicAS.volume == 0f)
		{
			MusicAS.DOFade(1f, 0.7f);
		}
		if (m_currentPlayingSong == trackType)
		{
			return;
		}
		m_currentPlayingSong = trackType;
		m_musicDirector.playableAsset = m_playableAssets[(int)trackType];
		foreach (global::UnityEngine.Timeline.TrackAsset outputTrack in (m_musicDirector.playableAsset as global::UnityEngine.Timeline.TimelineAsset).GetOutputTracks())
		{
			if (outputTrack is global::UnityEngine.Timeline.AudioTrack)
			{
				m_musicDirector.SetGenericBinding(outputTrack, m_as);
				if (trackType == MusicTrackType.MainMenuTheme || trackType == MusicTrackType.OptionsMenuTheme)
				{
					m_as.outputAudioMixerGroup = MenuGroup;
					m_as.volume = 1f;
				}
				else
				{
					m_as.outputAudioMixerGroup = GameGroup;
					m_as.volume = 0.5f;
				}
			}
		}
		if (isFromDialogue)
		{
			m_as.outputAudioMixerGroup = StoryGroup;
			m_as.volume = 1f;
		}
		m_musicDirector.Play();
	}

	public void PlaySfx(global::UnityEngine.AudioClip clip, float volume = 1f, bool antiOverlap = true, float pitch = 1f, float antiOverlapTime = 0.05f)
	{
		if (!antiOverlap || !(m_lastclip == clip) || !(global::UnityEngine.Mathf.Abs(m_sfxTimestamp - global::UnityEngine.Time.time) < antiOverlapTime))
		{
			global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(m_sfxPrefab);
			KillAfterTime component = obj.GetComponent<KillAfterTime>();
			global::UnityEngine.AudioSource component2 = obj.GetComponent<global::UnityEngine.AudioSource>();
			component2.clip = clip;
			component2.volume = volume;
			component2.pitch = pitch;
			component2.Play();
			component2.outputAudioMixerGroup = GameGroup;
			component.KillIn(clip.length);
			m_sfxTimestamp = global::UnityEngine.Time.time;
			m_lastclip = clip;
		}
	}

	private void InitMenuSfxPool()
	{
		for (int i = 0; i < m_menuSfxPoolSize; i++)
		{
			global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject($"MenuSfxSource_{i}");
			obj.transform.SetParent(base.transform);
			global::UnityEngine.AudioSource audioSource = obj.AddComponent<global::UnityEngine.AudioSource>();
			audioSource.playOnAwake = false;
			audioSource.outputAudioMixerGroup = MenuGroup;
			m_menuSfxPool.Add(audioSource);
		}
	}

	public void PlayMenuSfx(global::UnityEngine.AudioClip clip, float volume = 1f, bool antiOverlap = true, float pitch = 1f, float antiOverlapTime = 0.05f)
	{
		if (antiOverlap && m_lastclip == clip && global::UnityEngine.Mathf.Abs(m_sfxTimestamp - global::UnityEngine.Time.time) < antiOverlapTime)
		{
			return;
		}
		global::UnityEngine.AudioSource audioSource = null;
		for (int i = 0; i < m_menuSfxPool.Count; i++)
		{
			if (!m_menuSfxPool[i].isPlaying)
			{
				audioSource = m_menuSfxPool[i];
				break;
			}
		}
		if (audioSource == null)
		{
			for (int j = 0; j < m_menuSfxPool.Count; j++)
			{
				if (m_menuSfxPool[j].isPlaying && m_menuSfxPool[j].clip == clip)
				{
					audioSource = m_menuSfxPool[j];
					break;
				}
			}
		}
		if (audioSource == null)
		{
			audioSource = m_menuSfxPool[m_menuSfxRoundRobin];
			m_menuSfxRoundRobin = (m_menuSfxRoundRobin + 1) % m_menuSfxPool.Count;
		}
		audioSource.clip = clip;
		audioSource.volume = volume;
		audioSource.pitch = pitch;
		audioSource.Play();
		m_sfxTimestamp = global::UnityEngine.Time.time;
		m_lastclip = clip;
	}

	public void PlayStorySfx(global::UnityEngine.AudioClip clip, float volume = 1f, bool antiOverlap = true, float pitch = 1f, float antiOverlapTime = 0.05f)
	{
		if (!antiOverlap || !(m_lastclip == clip) || !(global::UnityEngine.Mathf.Abs(m_sfxTimestamp - global::UnityEngine.Time.time) < antiOverlapTime))
		{
			global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(m_sfxPrefab);
			global::UnityEngine.Object.DontDestroyOnLoad(obj);
			KillAfterTime component = obj.GetComponent<KillAfterTime>();
			global::UnityEngine.AudioSource component2 = obj.GetComponent<global::UnityEngine.AudioSource>();
			component2.clip = clip;
			component2.volume = volume;
			component2.pitch = pitch;
			component2.outputAudioMixerGroup = StoryGroup;
			component2.Play();
			component.KillIn(clip.length);
			m_sfxTimestamp = global::UnityEngine.Time.time;
			m_lastclip = clip;
		}
	}

	public void PlayGameVoiceLine(global::UnityEngine.AudioClip clip, int vlVal = 0, float volume = 1f, bool antiOverlap = true, float pitch = 1f, bool isPlayer1 = true)
	{
		int num = ((!isPlayer1) ? 1 : 0);
		if (!antiOverlap || !(m_lastVLClip[num] == clip) || !(m_voiceLineTimestamp[num] > global::UnityEngine.Time.time) || vlVal > m_currentVoiceLineValue[num])
		{
			m_currentVoiceLineValue[num] = vlVal;
			if (m_activeVL[num] != null)
			{
				global::UnityEngine.Object.Destroy(m_activeVL[num]);
			}
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(m_sfxPrefab);
			m_activeVL[num] = gameObject;
			global::UnityEngine.AudioSource component = gameObject.GetComponent<global::UnityEngine.AudioSource>();
			component.clip = clip;
			component.outputAudioMixerGroup = GameGroup;
			m_voiceLineTimestamp[num] = global::UnityEngine.Time.time + component.clip.length + 0.2f;
			component.volume = volume;
			if (global::UnityEngine.PlayerPrefs.HasKey("CHEERS"))
			{
				component.volume = ((global::UnityEngine.PlayerPrefs.GetInt("CHEERS") > 0) ? volume : 0f);
			}
			component.pitch = pitch;
			global::UnityEngine.AudioReverbFilter component2 = gameObject.GetComponent<global::UnityEngine.AudioReverbFilter>();
			if (component2 != null)
			{
				component2.enabled = true;
				component2.reverbLevel = m_roomSizeCurve.Evaluate(vlVal);
			}
			m_lastVLClip[num] = clip;
			StartCoroutine(PlayVoiceLineAfterDelay(gameObject));
		}
	}

	private global::System.Collections.IEnumerator PlayVoiceLineAfterDelay(global::UnityEngine.GameObject GO)
	{
		if (!(GO == null))
		{
			yield return new global::UnityEngine.WaitForSeconds(0.2f);
			if (!(GO == null))
			{
				KillAfterTime component = GO.GetComponent<KillAfterTime>();
				global::UnityEngine.AudioSource component2 = GO.GetComponent<global::UnityEngine.AudioSource>();
				component2.Play();
				component.KillIn(component2.clip.length);
			}
		}
	}

	public void FadeOutMaster(float time = 1f)
	{
		if (!m_isFadingMaster)
		{
			m_isFadingMaster = true;
			m_originalMasterValue = Instance.GetHardMasterVolume();
			float volVal = 0f;
			m_masterFadeTween = SimpleTween.To(() => volVal, delegate(float x)
			{
				volVal = x;
			}, 1f, time);
			m_masterFadeTween.OnUpdate(delegate
			{
				Instance.SetMasterForced(global::UnityEngine.Mathf.Lerp(m_originalMasterValue, -80f, volVal));
			});
			m_masterFadeTween.OnComplete(delegate
			{
				m_isFadingMaster = false;
				m_masterFadeTween = null;
			});
		}
	}

	public void FadeInMaster(float time = 1f, float startTime = 0f, float? targetOverride = null)
	{
		if (!m_isFadingMaster)
		{
			m_isFadingMaster = true;
			m_originalMasterValue = targetOverride ?? Instance.GetHardMasterVolume();
			float num = ((time > 0f) ? global::UnityEngine.Mathf.Clamp01(startTime / time) : 1f);
			float duration = global::UnityEngine.Mathf.Max(0.01f, time - startTime);
			float volVal = num;
			Instance.SetMasterForced(global::UnityEngine.Mathf.Lerp(-80f, m_originalMasterValue, volVal));
			m_masterFadeTween = SimpleTween.To(() => volVal, delegate(float x)
			{
				volVal = x;
			}, 1f, duration);
			m_masterFadeTween.OnUpdate(delegate
			{
				Instance.SetMasterForced(global::UnityEngine.Mathf.Lerp(-80f, m_originalMasterValue, volVal));
			});
			m_masterFadeTween.OnComplete(delegate
			{
				m_isFadingMaster = false;
				m_masterFadeTween = null;
			});
		}
	}

	public void CancelMasterFade(bool snapToOriginal = false)
	{
		if (m_isFadingMaster)
		{
			m_masterFadeTween?.Kill();
			m_masterFadeTween = null;
			m_isFadingMaster = false;
			if (snapToOriginal)
			{
				Instance.SetMasterForced(m_originalMasterValue);
			}
		}
	}

	public void FadeOutGame(float time = 1f)
	{
		if (!m_isFadingGame)
		{
			m_isFadingGame = true;
			m_originalGameValue = Instance.GetHardGameVolume();
			float volVal = 0f;
			m_gameFadeTween = SimpleTween.To(() => volVal, delegate(float x)
			{
				volVal = x;
			}, 1f, time);
			m_gameFadeTween.OnUpdate(delegate
			{
				Instance.SetGameForced(global::UnityEngine.Mathf.Lerp(m_originalGameValue, -80f, volVal));
			});
			m_gameFadeTween.OnComplete(delegate
			{
				m_isFadingGame = false;
				m_gameFadeTween = null;
			});
		}
	}

	public void FadeInGame(float time = 1f, float startTime = 0f, float? targetOverride = null)
	{
		if (!m_isFadingGame)
		{
			m_isFadingGame = true;
			m_originalGameValue = targetOverride ?? Instance.GetHardGameVolume();
			float num = ((time > 0f) ? global::UnityEngine.Mathf.Clamp01(startTime / time) : 1f);
			float duration = global::UnityEngine.Mathf.Max(0.01f, time - startTime);
			float volVal = num;
			Instance.SetGameForced(global::UnityEngine.Mathf.Lerp(-80f, m_originalGameValue, volVal));
			m_gameFadeTween = SimpleTween.To(() => volVal, delegate(float x)
			{
				volVal = x;
			}, 1f, duration);
			m_gameFadeTween.OnUpdate(delegate
			{
				Instance.SetGameForced(global::UnityEngine.Mathf.Lerp(-80f, m_originalGameValue, volVal));
			});
			m_gameFadeTween.OnComplete(delegate
			{
				m_isFadingGame = false;
				m_gameFadeTween = null;
			});
		}
	}

	public void CancelGameFade(bool snapToOriginal = false)
	{
		if (m_isFadingGame)
		{
			m_gameFadeTween?.Kill();
			m_gameFadeTween = null;
			m_isFadingGame = false;
			if (snapToOriginal)
			{
				Instance.SetGameForced(m_originalGameValue);
			}
		}
	}

	public void FadeOutStory(float time = 1f)
	{
		if (!m_isFadingStory)
		{
			m_isFadingStory = true;
			m_originalStoryValue = Instance.GetHardStoryVolume();
			float volVal = 0f;
			m_storyFadeTween = SimpleTween.To(() => volVal, delegate(float x)
			{
				volVal = x;
			}, 1f, time);
			m_storyFadeTween.OnUpdate(delegate
			{
				Instance.SetStoryForced(global::UnityEngine.Mathf.Lerp(m_originalStoryValue, -80f, volVal));
			});
			m_storyFadeTween.OnComplete(delegate
			{
				m_isFadingStory = false;
				m_storyFadeTween = null;
			});
		}
	}

	public void FadeInStory(float time = 1f, float startTime = 0f, float? targetOverride = null)
	{
		if (!m_isFadingStory)
		{
			m_isFadingStory = true;
			m_originalStoryValue = targetOverride ?? Instance.GetHardStoryVolume();
			float num = ((time > 0f) ? global::UnityEngine.Mathf.Clamp01(startTime / time) : 1f);
			float duration = global::UnityEngine.Mathf.Max(0.01f, time - startTime);
			float volVal = num;
			Instance.SetStoryForced(global::UnityEngine.Mathf.Lerp(-80f, m_originalStoryValue, volVal));
			m_storyFadeTween = SimpleTween.To(() => volVal, delegate(float x)
			{
				volVal = x;
			}, 1f, duration);
			m_storyFadeTween.OnUpdate(delegate
			{
				Instance.SetStoryForced(global::UnityEngine.Mathf.Lerp(-80f, m_originalStoryValue, volVal));
			});
			m_storyFadeTween.OnComplete(delegate
			{
				m_isFadingStory = false;
				m_storyFadeTween = null;
			});
		}
	}

	public void CancelStoryFade(bool snapToOriginal = false)
	{
		if (m_isFadingStory)
		{
			m_storyFadeTween?.Kill();
			m_storyFadeTween = null;
			m_isFadingStory = false;
			if (snapToOriginal)
			{
				Instance.SetStoryForced(m_originalStoryValue);
			}
		}
	}

	public void FadeOutMenu(float time = 1f, global::UnityEngine.AnimationCurve curve = null)
	{
		if (!m_isFadingMenu)
		{
			m_isFadingMenu = true;
			m_originalMenuValue = Instance.GetHardMenuVolume();
			float volVal = 0f;
			m_menuFadeTween = SimpleTween.To(() => volVal, delegate(float x)
			{
				volVal = x;
			}, 1f, time);
			m_menuFadeTween.OnUpdate(delegate
			{
				Instance.SetMenuForced(global::UnityEngine.Mathf.Lerp(m_originalMenuValue, -80f, volVal));
			});
			m_menuFadeTween.OnComplete(delegate
			{
				m_isFadingMenu = false;
				m_menuFadeTween = null;
			});
			if (curve != null)
			{
				m_menuFadeTween.SetEase(curve);
			}
		}
	}

	public void FadeInMenu(float time = 1f, float startTime = 0f, float? targetOverride = null, global::UnityEngine.AnimationCurve curve = null)
	{
		if (!m_isFadingMenu)
		{
			m_isFadingMenu = true;
			m_originalMenuValue = targetOverride ?? Instance.GetHardMenuVolume();
			float num = ((time > 0f) ? global::UnityEngine.Mathf.Clamp01(startTime / time) : 1f);
			float duration = global::UnityEngine.Mathf.Max(0.01f, time - startTime);
			float volVal = num;
			Instance.SetMenuForced(global::UnityEngine.Mathf.Lerp(-80f, m_originalMenuValue, volVal));
			m_menuFadeTween = SimpleTween.To(() => volVal, delegate(float x)
			{
				volVal = x;
			}, 1f, duration);
			m_menuFadeTween.OnUpdate(delegate
			{
				Instance.SetMenuForced(global::UnityEngine.Mathf.Lerp(-80f, m_originalMenuValue, volVal));
			});
			m_menuFadeTween.OnComplete(delegate
			{
				m_isFadingMenu = false;
				m_menuFadeTween = null;
			});
			if (curve != null)
			{
				m_menuFadeTween.SetEase(curve);
			}
		}
	}

	public void CancelMenuFade(bool snapToOriginal = false)
	{
		if (m_isFadingMenu)
		{
			m_menuFadeTween?.Kill();
			m_menuFadeTween = null;
			m_isFadingMenu = false;
			if (snapToOriginal)
			{
				Instance.SetMenuForced(m_originalMenuValue);
			}
		}
	}

	public void CancelAllFades(bool snapToOriginal = false)
	{
		CancelMasterFade(snapToOriginal);
		CancelGameFade(snapToOriginal);
		CancelStoryFade(snapToOriginal);
		CancelMenuFade(snapToOriginal);
	}

	public void SetMusicSpeed(float speedMultiplier)
	{
		if (m_musicDirector.playableGraph.IsValid())
		{
			m_speedTween?.Kill();
			m_speedTween = null;
			m_currentSpeedValue = speedMultiplier;
			global::UnityEngine.Playables.PlayableExtensions.SetSpeed(m_musicDirector.playableGraph.GetRootPlayable(0), speedMultiplier);
		}
	}

	public void FadeMusicSpeed(float targetSpeed, float time = 1f)
	{
		if (m_currentSpeedValue == targetSpeed || !m_musicDirector.playableGraph.IsValid())
		{
			return;
		}
		m_speedTween?.Kill();
		m_speedTween = null;
		if (time <= 0f)
		{
			SetMusicSpeed(targetSpeed);
			return;
		}
		m_speedTween = SimpleTween.To(() => m_currentSpeedValue, delegate(float x)
		{
			m_currentSpeedValue = x;
			global::UnityEngine.Playables.PlayableExtensions.SetSpeed(m_musicDirector.playableGraph.GetRootPlayable(0), x);
		}, targetSpeed, time).OnComplete(delegate
		{
			m_speedTween = null;
		});
	}

	public void ResetMusicSpeed(float time = 1f)
	{
		FadeMusicSpeed(1f, time);
	}

	public void StopMusicSpeedFade()
	{
		m_speedTween?.Kill();
		m_speedTween = null;
	}

	public float GetCurrentMusicSpeed()
	{
		return m_currentSpeedValue;
	}
}
