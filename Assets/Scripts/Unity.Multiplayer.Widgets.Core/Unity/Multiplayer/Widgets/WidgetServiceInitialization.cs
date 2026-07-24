namespace Unity.Multiplayer.Widgets
{
	public static class WidgetServiceInitialization
	{
		public static bool IsInitialized { get; private set; }

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			IsInitialized = false;
		}

		public static void ServicesInitialized()
		{
			IsInitialized = true;
			global::Unity.Multiplayer.Widgets.LazySingleton<global::Unity.Multiplayer.Widgets.WidgetEventDispatcher>.Instance.OnServicesInitialized();
		}
	}
}
