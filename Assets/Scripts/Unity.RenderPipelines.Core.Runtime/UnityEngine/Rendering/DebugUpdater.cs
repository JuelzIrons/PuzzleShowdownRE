namespace UnityEngine.Rendering
{
	internal class DebugUpdater : global::UnityEngine.MonoBehaviour
	{
		private static global::UnityEngine.Rendering.DebugUpdater s_Instance;

		private global::UnityEngine.ScreenOrientation m_Orientation;

		private bool m_RuntimeUiWasVisibleLastFrame;

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void RuntimeInit()
		{
		}

		internal static void SetEnabled(bool enabled)
		{
			if (enabled)
			{
				EnableRuntime();
			}
			else
			{
				DisableRuntime();
			}
		}

		private static void EnableRuntime()
		{
			if (!(s_Instance != null))
			{
				global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject
				{
					name = "[Debug Updater]"
				};
				s_Instance = obj.AddComponent<global::UnityEngine.Rendering.DebugUpdater>();
				s_Instance.m_Orientation = global::UnityEngine.Screen.orientation;
				global::UnityEngine.Object.DontDestroyOnLoad(obj);
				global::UnityEngine.Rendering.DebugManager.instance.EnableInputActions();
				global::UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();
			}
		}

		private static void DisableRuntime()
		{
			global::UnityEngine.Rendering.DebugManager instance = global::UnityEngine.Rendering.DebugManager.instance;
			instance.displayRuntimeUI = false;
			instance.displayPersistentRuntimeUI = false;
			if (s_Instance != null)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(s_Instance.gameObject);
				s_Instance = null;
			}
		}

		internal static void HandleInternalEventSystemComponents(bool uiEnabled)
		{
			if (!(s_Instance == null))
			{
				if (uiEnabled)
				{
					s_Instance.EnsureExactlyOneEventSystem();
				}
				else
				{
					s_Instance.DestroyDebugEventSystem();
				}
			}
		}

		private void EnsureExactlyOneEventSystem()
		{
			global::UnityEngine.EventSystems.EventSystem[] array = global::UnityEngine.Object.FindObjectsByType<global::UnityEngine.EventSystems.EventSystem>(global::UnityEngine.FindObjectsSortMode.None);
			global::UnityEngine.EventSystems.EventSystem component = GetComponent<global::UnityEngine.EventSystems.EventSystem>();
			if (array.Length > 1 && component != null)
			{
				global::UnityEngine.Debug.Log("More than one EventSystem detected in scene. Destroying EventSystem owned by DebugUpdater.");
				DestroyDebugEventSystem();
			}
			else if (array.Length == 0)
			{
				global::UnityEngine.Debug.Log("No EventSystem available. Creating a new EventSystem to enable Rendering Debugger runtime UI.");
				CreateDebugEventSystem();
			}
			else
			{
				StartCoroutine(DoAfterInputModuleUpdated(CheckInputModuleExists));
			}
		}

		private global::System.Collections.IEnumerator DoAfterInputModuleUpdated(global::System.Action action)
		{
			yield return new global::UnityEngine.WaitForEndOfFrame();
			yield return new global::UnityEngine.WaitForEndOfFrame();
			action();
		}

		private void CheckInputModuleExists()
		{
			if (global::UnityEngine.EventSystems.EventSystem.current != null && global::UnityEngine.EventSystems.EventSystem.current.currentInputModule == null)
			{
				global::UnityEngine.Debug.LogWarning("Found a game object with EventSystem component but no corresponding BaseInputModule component - Debug UI input might not work correctly.");
			}
		}

		private void AssignDefaultActions()
		{
			if (global::UnityEngine.EventSystems.EventSystem.current != null && global::UnityEngine.EventSystems.EventSystem.current.currentInputModule is global::UnityEngine.InputSystem.UI.InputSystemUIInputModule inputSystemUIInputModule)
			{
				global::System.Reflection.MethodInfo method = inputSystemUIInputModule.GetType().GetMethod("AssignDefaultActions");
				if (method != null)
				{
					method.Invoke(inputSystemUIInputModule, null);
				}
			}
			CheckInputModuleExists();
		}

		private void CreateDebugEventSystem()
		{
			base.gameObject.AddComponent<global::UnityEngine.EventSystems.EventSystem>();
			base.gameObject.AddComponent<global::UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
			StartCoroutine(DoAfterInputModuleUpdated(AssignDefaultActions));
		}

		private void DestroyDebugEventSystem()
		{
			global::UnityEngine.EventSystems.EventSystem component = GetComponent<global::UnityEngine.EventSystems.EventSystem>();
			global::UnityEngine.InputSystem.UI.InputSystemUIInputModule component2 = GetComponent<global::UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
			if ((bool)component2)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(component2);
				StartCoroutine(DoAfterInputModuleUpdated(AssignDefaultActions));
			}
			global::UnityEngine.Rendering.CoreUtils.Destroy(component);
		}

		private void Update()
		{
			global::UnityEngine.Rendering.DebugManager instance = global::UnityEngine.Rendering.DebugManager.instance;
			if (m_RuntimeUiWasVisibleLastFrame != instance.displayRuntimeUI)
			{
				HandleInternalEventSystemComponents(instance.displayRuntimeUI);
			}
			instance.UpdateActions();
			if (instance.GetAction(global::UnityEngine.Rendering.DebugAction.EnableDebugMenu) != 0f || instance.GetActionToggleDebugMenuWithTouch())
			{
				instance.displayRuntimeUI = !instance.displayRuntimeUI;
			}
			if (instance.displayRuntimeUI)
			{
				if (instance.GetAction(global::UnityEngine.Rendering.DebugAction.ResetAll) != 0f)
				{
					instance.Reset();
				}
				if (instance.GetActionReleaseScrollTarget())
				{
					instance.SetScrollTarget(null);
				}
			}
			if (m_Orientation != global::UnityEngine.Screen.orientation)
			{
				StartCoroutine(RefreshRuntimeUINextFrame());
				m_Orientation = global::UnityEngine.Screen.orientation;
			}
			m_RuntimeUiWasVisibleLastFrame = instance.displayRuntimeUI;
		}

		private static global::System.Collections.IEnumerator RefreshRuntimeUINextFrame()
		{
			yield return null;
			global::UnityEngine.Rendering.DebugManager.instance.ReDrawOnScreenDebug();
		}
	}
}
