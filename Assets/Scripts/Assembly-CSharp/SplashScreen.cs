public class SplashScreen : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Animation m_splashAnim;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AnimationClip m_clip;

	private bool m_hasSplashed;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip m_startSound;

	[global::UnityEngine.SerializeField]
	private LoopFromTimestamp m_splashMusicManager;

	private float m_paddingTime;

	private void Start()
	{
		m_paddingTime = 0f;
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.KeyboardAnyAction = (global::System.Action)global::System.Delegate.Combine(instance.KeyboardAnyAction, new global::System.Action(SplashScreenConfirm));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.Controller1AnyAction = (global::System.Action)global::System.Delegate.Combine(instance2.Controller1AnyAction, new global::System.Action(SplashScreenConfirm));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.Controller2AnyAction = (global::System.Action)global::System.Delegate.Combine(instance3.Controller2AnyAction, new global::System.Action(SplashScreenConfirm));
	}

	private void FixedUpdate()
	{
		if (m_paddingTime < 70f)
		{
			m_paddingTime += 1f;
		}
	}

	private void Update()
	{
		if (!m_hasSplashed && global::UnityEngine.Input.anyKeyDown)
		{
			SplashScreenConfirm();
		}
	}

	public void SplashScreenConfirm()
	{
		if (!(m_paddingTime < 60f) && !SceneLoader.Instance.IsLoading)
		{
			PersistentInputReader instance = PersistentInputReader.Instance;
			instance.KeyboardAnyAction = (global::System.Action)global::System.Delegate.Remove(instance.KeyboardAnyAction, new global::System.Action(SplashScreenConfirm));
			PersistentInputReader instance2 = PersistentInputReader.Instance;
			instance2.Controller1AnyAction = (global::System.Action)global::System.Delegate.Remove(instance2.Controller1AnyAction, new global::System.Action(SplashScreenConfirm));
			PersistentInputReader instance3 = PersistentInputReader.Instance;
			instance3.Controller2AnyAction = (global::System.Action)global::System.Delegate.Remove(instance3.Controller2AnyAction, new global::System.Action(SplashScreenConfirm));
			if (!m_hasSplashed)
			{
				m_hasSplashed = true;
				StartCoroutine(SplashScreenRoutine());
			}
		}
	}

	private global::System.Collections.IEnumerator SplashScreenRoutine()
	{
		AudioManager.Instance.PlayMenuSfx(m_startSound, 0.3f, antiOverlap: false);
		m_splashAnim.clip = m_clip;
		m_splashMusicManager.FadeOut();
		m_splashAnim.Play();
		yield return new global::UnityEngine.WaitForSeconds(m_splashAnim.clip.length);
		SceneLoader.Instance.DoBlackFade(0f, 0.15f, 1f, muteSound: true, delegate
		{
			float hardMenuVolume = AudioManager.Instance.GetHardMenuVolume();
			AudioManager.Instance.SetMenuForced(-80f);
			AudioManager.Instance.CancelMenuFade();
			AudioManager.Instance.FadeInMenu(3f, 1.5f, hardMenuVolume);
			SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.MainMenu);
		}, null, 0f, global::UnityEngine.Color.white);
	}
}
