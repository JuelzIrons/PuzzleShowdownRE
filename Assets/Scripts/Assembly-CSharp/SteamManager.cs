[global::UnityEngine.DisallowMultipleComponent]
public class SteamManager : global::UnityEngine.MonoBehaviour
{
	protected static bool s_EverInitialized;

	protected static SteamManager s_instance;

	protected bool m_bInitialized;

	protected global::Steamworks.SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

	protected static SteamManager Instance
	{
		get
		{
			if (s_instance == null)
			{
				return new global::UnityEngine.GameObject("SteamManager").AddComponent<SteamManager>();
			}
			return s_instance;
		}
	}

	public static bool Initialized => Instance.m_bInitialized;

	[global::AOT.MonoPInvokeCallback(typeof(global::Steamworks.SteamAPIWarningMessageHook_t))]
	protected static void SteamAPIDebugTextHook(int nSeverity, global::System.Text.StringBuilder pchDebugText)
	{
		global::UnityEngine.Debug.LogWarning(pchDebugText);
	}

	[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void InitOnPlayMode()
	{
		s_EverInitialized = false;
		s_instance = null;
	}

	protected virtual void Awake()
	{
		if (s_instance != null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		s_instance = this;
		if (s_EverInitialized)
		{
			throw new global::System.Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (!global::Steamworks.Packsize.Test())
		{
			global::UnityEngine.Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!global::Steamworks.DllCheck.Test())
		{
			global::UnityEngine.Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		try
		{
			if (global::Steamworks.SteamAPI.RestartAppIfNecessary(global::Steamworks.AppId_t.Invalid))
			{
				global::UnityEngine.Debug.Log("[Steamworks.NET] Shutting down because RestartAppIfNecessary returned true. Steam will restart the application.");
				global::UnityEngine.Application.Quit();
				return;
			}
		}
		catch (global::System.DllNotFoundException ex)
		{
			global::UnityEngine.Debug.LogError("[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" + ex, this);
			global::UnityEngine.Application.Quit();
			return;
		}
		m_bInitialized = global::Steamworks.SteamAPI.Init();
		if (!m_bInitialized)
		{
			global::UnityEngine.Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
		}
		else
		{
			s_EverInitialized = true;
		}
	}

	protected virtual void OnEnable()
	{
		if (s_instance == null)
		{
			s_instance = this;
		}
		if (m_bInitialized && m_SteamAPIWarningMessageHook == null)
		{
			m_SteamAPIWarningMessageHook = SteamAPIDebugTextHook;
			global::Steamworks.SteamClient.SetWarningMessageHook(m_SteamAPIWarningMessageHook);
		}
	}

	protected virtual void OnDestroy()
	{
		if (!(s_instance != this))
		{
			s_instance = null;
			if (m_bInitialized)
			{
				global::Steamworks.SteamAPI.Shutdown();
			}
		}
	}

	protected virtual void Update()
	{
		if (m_bInitialized)
		{
			global::Steamworks.SteamAPI.RunCallbacks();
		}
	}
}
