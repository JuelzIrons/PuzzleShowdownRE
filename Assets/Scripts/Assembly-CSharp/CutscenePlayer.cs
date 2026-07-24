public class CutscenePlayer : global::UnityEngine.MonoBehaviour
{
	public string ActiveCutsceneClipName;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Video.VideoPlayer m_vidPlayer;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_vidCanvas;

	public global::UnityEngine.Events.UnityEvent CutsceneFinishedEvent;

	private float m_skipHeldTimer;

	private bool m_isHoldingSkip;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_skipButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_skipArea;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_skipCircle;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_lastSelected;

	private bool m_firstReleaseMitigation;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_pauseButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Sprite[] m_pauseSprites;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_CutsceneControlsObj;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Video.VideoClip[] m_winVideos;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Video.VideoClip[] m_linuxVideos;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<string> m_macCutsceneNames = new global::System.Collections.Generic.List<string>();

	private bool m_anySkipIsOn;

	private bool m_isUnskippable;

	private bool m_currentlyPlaying => m_vidCanvas.activeSelf;

	private void OnCutsceneFinished(global::UnityEngine.Video.VideoPlayer vp)
	{
		m_vidPlayer.loopPointReached -= OnCutsceneFinished;
		if (m_anySkipIsOn)
		{
			if (!m_isUnskippable)
			{
				m_vidCanvas.SetActive(value: false);
			}
			m_vidPlayer.Stop();
			UnSubscribeToInputs();
			m_anySkipIsOn = false;
			CutsceneFinishedEvent.Invoke();
			return;
		}
		if (!m_isUnskippable)
		{
			m_vidCanvas.SetActive(value: false);
		}
		CutsceneFinishedEvent.Invoke();
		UnSubscribeToInputs();
		m_vidPlayer.Stop();
		if (global::UnityEngine.EventSystems.EventSystem.current != null && m_lastSelected != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_lastSelected);
		}
	}

	public void PlayCutscene(CutsceneVideoIndicies type, bool playSilently = false, bool playWithAnySkip = false, bool activatePanel = false, bool isUnskippable = false)
	{
		m_isUnskippable = isUnskippable;
		m_vidPlayer.Stop();
		ClearVideoRenderTexture();
		m_vidPlayer.errorReceived += delegate(global::UnityEngine.Video.VideoPlayer vp, string msg)
		{
			global::UnityEngine.Debug.LogError("VideoPlayer error: " + msg);
		};
		m_vidPlayer.gameObject.SetActive(value: false);
		m_vidPlayer.gameObject.SetActive(value: true);
		m_vidPlayer.Stop();
		m_vidPlayer.frame = 0L;
		m_anySkipIsOn = playWithAnySkip;
		if (m_anySkipIsOn || isUnskippable)
		{
			m_skipArea.SetActive(value: false);
		}
		if (isUnskippable)
		{
			m_skipButton.SetActive(value: false);
		}
		else
		{
			m_skipButton.SetActive(value: true);
		}
		m_firstReleaseMitigation = true;
		AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic);
		m_lastSelected = global::UnityEngine.EventSystems.EventSystem.current?.currentSelectedGameObject;
		global::UnityEngine.EventSystems.EventSystem.current?.SetSelectedGameObject(null);
		m_vidPlayer.source = global::UnityEngine.Video.VideoSource.VideoClip;
		m_vidPlayer.clip = m_winVideos[(int)type];
		if (isUnskippable || !m_anySkipIsOn)
		{
			Invoke("SetPauseSelectedWithDelay", 1f);
		}
		m_vidPlayer.Play();
		if (!playSilently)
		{
			m_vidPlayer.isLooping = false;
			m_vidCanvas.SetActive(value: true);
			m_vidPlayer.loopPointReached += OnCutsceneFinished;
			if (!playWithAnySkip && !activatePanel)
			{
				SubscribeToInputs();
			}
		}
		else
		{
			m_vidPlayer.isLooping = true;
		}
		m_CutsceneControlsObj.SetActive(activatePanel);
		m_skipHeldTimer = 0f;
	}

	private void SetPauseSelectedWithDelay()
	{
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_pauseButton.gameObject);
	}

	private void ClearVideoRenderTexture()
	{
		global::UnityEngine.RenderTexture targetTexture = m_vidPlayer.targetTexture;
		if (!(targetTexture == null))
		{
			global::UnityEngine.RenderTexture active = global::UnityEngine.RenderTexture.active;
			global::UnityEngine.RenderTexture.active = targetTexture;
			global::UnityEngine.GL.Clear(clearDepth: true, clearColor: true, global::UnityEngine.Color.black);
			global::UnityEngine.RenderTexture.active = active;
		}
	}

	private void SubscribeToInputs()
	{
		if (PersistentInputReader.Instance == null)
		{
			global::UnityEngine.Debug.LogWarning("PersistentInputReader.Instance was null when subscribing — input skip won't work for this cutscene.");
			return;
		}
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.KeyboardSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance.KeyboardSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance2.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance3.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
		PersistentInputReader instance4 = PersistentInputReader.Instance;
		instance4.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance4.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
		PersistentInputReader instance5 = PersistentInputReader.Instance;
		instance5.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance5.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
		PersistentInputReader instance6 = PersistentInputReader.Instance;
		instance6.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance6.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
	}

	public void UnSubscribeToInputs()
	{
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.KeyboardSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance.KeyboardSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance2.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance3.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
		PersistentInputReader instance4 = PersistentInputReader.Instance;
		instance4.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance4.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
		PersistentInputReader instance5 = PersistentInputReader.Instance;
		instance5.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance5.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
		PersistentInputReader instance6 = PersistentInputReader.Instance;
		instance6.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance6.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TrySkip));
	}

	public void TrySkip(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!m_currentlyPlaying)
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
			if (m_skipHeldTimer < 0.2f && !m_firstReleaseMitigation)
			{
				TogglePauseCutscene();
			}
			if (m_firstReleaseMitigation)
			{
				m_firstReleaseMitigation = false;
			}
			m_skipHeldTimer = 0f;
			m_isHoldingSkip = false;
		}
	}

	private void Update()
	{
		if (m_isUnskippable)
		{
			m_skipHeldTimer = 0f;
			return;
		}
		if (m_anySkipIsOn && global::UnityEngine.Input.anyKeyDown)
		{
			m_isHoldingSkip = false;
			m_skipArea.SetActive(value: false);
			m_skipHeldTimer = 0f;
			m_vidPlayer.loopPointReached -= OnCutsceneFinished;
			OnCutsceneFinished(m_vidPlayer);
		}
		if (m_anySkipIsOn)
		{
			return;
		}
		if ((m_isHoldingSkip || global::UnityEngine.Input.anyKey) && m_currentlyPlaying)
		{
			m_skipHeldTimer += global::UnityEngine.Time.deltaTime;
			if (m_skipHeldTimer > 0.2f)
			{
				m_skipArea.SetActive(value: true);
				global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_skipButton);
				m_skipCircle.fillAmount = global::UnityEngine.Mathf.Clamp(m_skipHeldTimer / 0.75f, 0f, 1f);
				if (m_skipHeldTimer > 0.75f)
				{
					m_isHoldingSkip = false;
					m_skipArea.SetActive(value: false);
					m_skipHeldTimer = 0f;
					OnCutsceneFinished(m_vidPlayer);
				}
			}
		}
		else
		{
			m_skipHeldTimer = 0f;
			m_skipArea.SetActive(value: false);
		}
	}

	public void PressSkipButton()
	{
		OnCutsceneFinished(m_vidPlayer);
	}

	public void PressPauseButton()
	{
		if (!m_vidPlayer.isPaused)
		{
			m_vidPlayer.Pause();
			m_pauseButton.sprite = m_pauseSprites[0];
		}
		else
		{
			m_vidPlayer.Play();
			m_pauseButton.sprite = m_pauseSprites[1];
		}
	}

	public void TogglePauseCutscene()
	{
		if (m_vidPlayer.isPlaying)
		{
			m_vidPlayer.Pause();
		}
		else
		{
			m_vidPlayer.Play();
		}
	}

	public void StopCutscene()
	{
		m_vidPlayer.Stop();
		m_vidCanvas.SetActive(value: false);
	}
}
