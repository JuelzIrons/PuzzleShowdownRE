public class PauseManager : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_pauseMenuObj;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.LayerMask m_pauseCamLayers;

	private global::UnityEngine.LayerMask m_prevMask;

	private bool lastPausedByP1;

	private bool isPaused;

	private bool wasSupressed;

	private bool wasSelfUpdating;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_menuButton;

	private global::UnityEngine.GameObject m_prevSelectedButton;

	private float m_prevVol;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Audio.AudioMixerGroup m_mainGroup;

	public global::UnityEngine.GameObject UnpauseButton;

	private global::UnityEngine.Coroutine m_selectCoroutine;

	public void BackToMenu()
	{
		if (TallyManager.Instance != null)
		{
			global::UnityEngine.Object.Destroy(TallyManager.Instance.gameObject);
		}
		TogglePause(lastPausedByP1);
		GameManager.Instance.LoadMenu();
	}

	public void ToggleForcePause()
	{
		TogglePause(lastPausedByP1);
	}

	public void TogglePause(bool fromP1)
	{
		if (SceneLoader.Instance.IsLoading || (isPaused && fromP1 != lastPausedByP1))
		{
			return;
		}
		isPaused = !isPaused;
		if (isPaused)
		{
			m_prevMask = global::UnityEngine.Camera.main.cullingMask;
			global::UnityEngine.Camera.main.cullingMask = m_pauseCamLayers;
			m_prevVol = AudioManager.Instance.GetHardMasterVolume();
			AudioManager.Instance.SetMasterForced(-80f);
			global::UnityEngine.Time.timeScale = 0f;
			wasSelfUpdating = PersistentInputReader.Instance.IsSelfUpdating;
			PersistentInputReader.Instance.SetSelfUpdate(selfUpdating: true);
			if (!PersistentInputReader.Instance.IsSupressing)
			{
				wasSupressed = false;
				m_prevSelectedButton = global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
			}
			else
			{
				wasSupressed = true;
			}
			if (DialogueManager.Instance != null && !DialogueManager.Instance.IsPaused)
			{
				DialogueManager.Instance.PauseText();
			}
			PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: true);
			SafeSelectNextFrame(m_menuButton);
		}
		else
		{
			global::UnityEngine.Camera.main.cullingMask = m_prevMask;
			AudioManager.Instance.SetMasterForced(m_prevVol);
			global::UnityEngine.Time.timeScale = 1f;
			if (wasSupressed)
			{
				PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: true);
			}
			else
			{
				PersistentInputReader.Instance.SetSupressAllEvents(shouldSupressEvents: false);
			}
			if (m_prevSelectedButton != null)
			{
				SafeSelectNextFrame(m_prevSelectedButton);
			}
			else
			{
				global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
			}
			if (DialogueManager.Instance != null && DialogueManager.Instance.IsPaused)
			{
				DialogueManager.Instance.UnpauseText();
			}
			PersistentInputReader.Instance.SetSelfUpdate(wasSelfUpdating);
		}
		m_pauseMenuObj.SetActive(isPaused);
	}

	private global::System.Collections.IEnumerator SelectNextFrame(global::UnityEngine.GameObject buttonToSelect)
	{
		yield return null;
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(buttonToSelect);
	}

	private void SafeSelectNextFrame(global::UnityEngine.GameObject buttonToSelect)
	{
		if (m_selectCoroutine != null)
		{
			StopCoroutine(m_selectCoroutine);
		}
		m_selectCoroutine = StartCoroutine(SelectNextFrame(buttonToSelect));
	}
}
