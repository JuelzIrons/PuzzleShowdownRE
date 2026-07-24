namespace Unity.Multiplayer.Tools.NetStats
{
	internal static class TypeRegistration
	{
		public const string k_ClassName = "<NetStats_TypeRegistration>";

		public const string k_MethodName = "Run";

		private static bool s_TypeRegistrationComplete;

		private static readonly object s_LockObject = new object();

		public static void RunIfNeeded()
		{
			lock (s_LockObject)
			{
				if (s_TypeRegistrationComplete)
				{
					return;
				}
				s_TypeRegistrationComplete = true;
				global::System.Reflection.Assembly[] assemblies = global::System.AppDomain.CurrentDomain.GetAssemblies();
				foreach (global::System.Reflection.Assembly assembly in assemblies)
				{
					if (global::System.Linq.Enumerable.Any(global::System.Reflection.CustomAttributeExtensions.GetCustomAttributes<global::Unity.Multiplayer.Tools.NetStats.AssemblyRequiresTypeRegistrationAttribute>(assembly)))
					{
						global::System.Reflection.MethodInfo methodInfo = assembly.GetType("<NetStats_TypeRegistration>")?.GetMethod("Run", global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.NonPublic);
						if (methodInfo == null)
						{
							global::UnityEngine.Debug.LogError("Failed to load type initialization for assembly " + assembly.GetName().Name);
						}
						else
						{
							methodInfo.Invoke(null, null);
						}
					}
				}
				global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.TypeRegistrationPostProcess();
			}
		}
	}
}
