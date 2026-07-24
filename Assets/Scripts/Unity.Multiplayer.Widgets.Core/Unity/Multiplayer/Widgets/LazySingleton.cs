namespace Unity.Multiplayer.Widgets
{
	internal abstract class LazySingleton<T> : global::UnityEngine.MonoBehaviour where T : global::UnityEngine.MonoBehaviour
	{
		private static T s_Instance;

		public static T Instance
		{
			get
			{
				if (s_Instance == null || s_Instance.gameObject == null)
				{
					CreateInstance();
				}
				return s_Instance;
			}
		}

		private static void CreateInstance()
		{
			global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject(typeof(T).Name ?? "");
			s_Instance = obj.AddComponent<T>();
			global::UnityEngine.Object.DontDestroyOnLoad(obj);
		}
	}
}
