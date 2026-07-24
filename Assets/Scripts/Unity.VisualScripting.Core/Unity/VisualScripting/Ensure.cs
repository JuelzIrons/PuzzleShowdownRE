namespace Unity.VisualScripting
{
	public static class Ensure
	{
		private static readonly global::Unity.VisualScripting.EnsureThat instance = new global::Unity.VisualScripting.EnsureThat();

		public static bool IsActive { get; set; }

		public static void Off()
		{
			IsActive = false;
		}

		public static void On()
		{
			IsActive = true;
		}

		public static global::Unity.VisualScripting.EnsureThat That(string paramName)
		{
			instance.paramName = paramName;
			return instance;
		}

		internal static void OnRuntimeMethodLoad()
		{
			IsActive = global::UnityEngine.Application.isEditor || global::UnityEngine.Debug.isDebugBuild;
		}
	}
}
