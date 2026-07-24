namespace UnityEngine.Rendering
{
	public static class ComponentSingleton<TType> where TType : global::UnityEngine.Component
	{
		private static TType s_Instance;

		public static TType instance
		{
			get
			{
				if (s_Instance == null)
				{
					global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject("Default " + typeof(TType).Name)
					{
						hideFlags = global::UnityEngine.HideFlags.HideAndDontSave
					};
					global::UnityEngine.Object.DontDestroyOnLoad(obj);
					obj.SetActive(value: false);
					s_Instance = obj.AddComponent<TType>();
				}
				return s_Instance;
			}
		}

		public static void Release()
		{
			if (s_Instance != null)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(s_Instance.gameObject);
				s_Instance = null;
			}
		}
	}
}
