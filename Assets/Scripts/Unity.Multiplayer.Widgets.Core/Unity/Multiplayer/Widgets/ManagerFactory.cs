namespace Unity.Multiplayer.Widgets
{
	internal static class ManagerFactory
	{
		internal static bool IsInitialized { get; private set; }

		internal static void Initialize()
		{
			IsInitialized = true;
			CreateLazySingletonInstance("Unity.Multiplayer.Widgets.SessionManager, Unity.Multiplayer.Widgets.Session");
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			IsInitialized = false;
		}

		private static void CreateLazySingletonInstance(string typeAndAssembly)
		{
			global::System.Type type = global::System.Type.GetType(typeAndAssembly);
			if (type == null)
			{
				global::UnityEngine.Debug.LogError(typeAndAssembly + " not found. Did the assembly or name change?");
				return;
			}
			global::System.Type baseType = type.BaseType;
			if (baseType == null)
			{
				global::UnityEngine.Debug.LogError("Base type not found. LazySingleton<T> expected.");
				return;
			}
			global::System.Reflection.PropertyInfo property = baseType.GetProperty("Instance");
			if (property == null)
			{
				global::UnityEngine.Debug.LogError("Instance property not found on LazySingleton<T>. Did the name change?");
			}
			else
			{
				property.GetMethod.Invoke(null, null);
			}
		}
	}
}
