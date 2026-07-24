namespace Unity.VisualScripting
{
	public static class Singleton<T> where T : global::UnityEngine.MonoBehaviour, global::Unity.VisualScripting.ISingleton
	{
		private static readonly global::Unity.VisualScripting.SingletonAttribute attribute;

		private static readonly object _lock;

		private static readonly global::System.Collections.Generic.HashSet<T> awoken;

		private static T _instance;

		private static bool persistent => attribute.Persistent;

		private static bool automatic => attribute.Automatic;

		private static string name => attribute.Name;

		private static global::UnityEngine.HideFlags hideFlags => attribute.HideFlags;

		public static bool instantiated
		{
			get
			{
				lock (_lock)
				{
					if (global::UnityEngine.Application.isPlaying)
					{
						return _instance != null;
					}
					return FindInstances().Length == 1;
				}
			}
		}

		public static T instance
		{
			get
			{
				lock (_lock)
				{
					if (global::UnityEngine.Application.isPlaying)
					{
						if (_instance == null)
						{
							Instantiate();
						}
						return _instance;
					}
					return Instantiate();
				}
			}
		}

		static Singleton()
		{
			_lock = new object();
			awoken = new global::System.Collections.Generic.HashSet<T>();
			attribute = typeof(T).GetAttribute<global::Unity.VisualScripting.SingletonAttribute>();
			if (attribute == null)
			{
				throw new global::Unity.VisualScripting.InvalidImplementationException($"Missing singleton attribute for '{typeof(T)}'.");
			}
		}

		private static T[] FindObjectsOfType()
		{
			return global::UnityEngine.Object.FindObjectsByType<T>(global::UnityEngine.FindObjectsSortMode.None);
		}

		private static T[] FindInstances()
		{
			return FindObjectsOfType();
		}

		public static T Instantiate()
		{
			lock (_lock)
			{
				T[] array = FindInstances();
				if (array.Length == 1)
				{
					_instance = array[0];
				}
				else if (array.Length == 0)
				{
					if (!automatic)
					{
						throw new global::UnityEngine.UnityException($"Missing '{typeof(T)}' singleton in the scene.");
					}
					global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject(name ?? typeof(T).Name);
					gameObject.hideFlags = hideFlags;
					T val = gameObject.AddComponent<T>();
					val.hideFlags = hideFlags;
					Awake(val);
					if (persistent && global::UnityEngine.Application.isPlaying)
					{
						global::UnityEngine.Object.DontDestroyOnLoad(gameObject);
					}
				}
				else if (array.Length > 1)
				{
					throw new global::UnityEngine.UnityException($"More than one '{typeof(T)}' singleton in the scene.");
				}
				return _instance;
			}
		}

		public static void Awake(T instance)
		{
			global::Unity.VisualScripting.Ensure.That("instance").IsNotNull(instance);
			if (!awoken.Contains(instance))
			{
				if (_instance != null)
				{
					throw new global::UnityEngine.UnityException($"More than one '{typeof(T)}' singleton in the scene.");
				}
				_instance = instance;
				awoken.Add(instance);
			}
		}

		public static void OnDestroy(T instance)
		{
			global::Unity.VisualScripting.Ensure.That("instance").IsNotNull(instance);
			if (_instance == instance)
			{
				_instance = null;
				return;
			}
			throw new global::UnityEngine.UnityException($"Trying to destroy invalid instance of '{typeof(T)}' singleton.");
		}
	}
}
