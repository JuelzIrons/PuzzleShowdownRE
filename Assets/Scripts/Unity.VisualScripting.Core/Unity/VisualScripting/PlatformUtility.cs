namespace Unity.VisualScripting
{
	public static class PlatformUtility
	{
		public static readonly bool supportsJit;

		static PlatformUtility()
		{
			supportsJit = CheckJitSupport();
		}

		private static bool CheckJitSupport()
		{
			return false;
		}

		public static bool IsEditor(this global::UnityEngine.RuntimePlatform platform)
		{
			if (platform != global::UnityEngine.RuntimePlatform.WindowsEditor && platform != global::UnityEngine.RuntimePlatform.OSXEditor)
			{
				return platform == global::UnityEngine.RuntimePlatform.LinuxEditor;
			}
			return true;
		}

		public static bool IsStandalone(this global::UnityEngine.RuntimePlatform platform)
		{
			if (platform != global::UnityEngine.RuntimePlatform.WindowsPlayer && platform != global::UnityEngine.RuntimePlatform.OSXPlayer)
			{
				return platform == global::UnityEngine.RuntimePlatform.LinuxPlayer;
			}
			return true;
		}
	}
}
