namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.Singleton(Name = "VisualScripting GlobalEventListener", Automatic = true, Persistent = true)]
	[global::Unity.VisualScripting.DisableAnnotation]
	[global::UnityEngine.AddComponentMenu("")]
	[global::Unity.VisualScripting.IncludeInSettings(false)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.MessageListener))]
	public sealed class GlobalMessageListener : global::UnityEngine.MonoBehaviour, global::Unity.VisualScripting.ISingleton
	{
		private void OnGUI()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnGUI");
		}

		private void OnApplicationFocus(bool focus)
		{
			if (focus)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnApplicationFocus");
			}
			else
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnApplicationLostFocus");
			}
		}

		private void OnApplicationPause(bool paused)
		{
			if (paused)
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnApplicationPause");
			}
			else
			{
				global::Unity.VisualScripting.EventBus.Trigger("OnApplicationResume");
			}
		}

		private void OnApplicationQuit()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnApplicationQuit");
		}

		public static void Require()
		{
			_ = global::Unity.VisualScripting.Singleton<global::Unity.VisualScripting.GlobalMessageListener>.instance;
		}
	}
}
