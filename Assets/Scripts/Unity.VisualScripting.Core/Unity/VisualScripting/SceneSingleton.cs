namespace Unity.VisualScripting
{
	public static class SceneSingleton<T> where T : global::UnityEngine.MonoBehaviour, global::Unity.VisualScripting.ISingleton
	{
		private static global::System.Collections.Generic.Dictionary<global::UnityEngine.SceneManagement.Scene, T> instances;

		private static readonly global::Unity.VisualScripting.SingletonAttribute attribute;

		private static bool persistent => attribute.Persistent;

		private static bool automatic => attribute.Automatic;

		private static string name => attribute.Name;

		private static global::UnityEngine.HideFlags hideFlags => attribute.HideFlags;

		static SceneSingleton()
		{
			instances = new global::System.Collections.Generic.Dictionary<global::UnityEngine.SceneManagement.Scene, T>();
			attribute = typeof(T).GetAttribute<global::Unity.VisualScripting.SingletonAttribute>();
			if (attribute == null)
			{
				throw new global::Unity.VisualScripting.InvalidImplementationException($"Missing singleton attribute for '{typeof(T)}'.");
			}
		}

		private static void EnsureSceneValid(global::UnityEngine.SceneManagement.Scene scene)
		{
			if (!scene.IsValid())
			{
				throw new global::System.InvalidOperationException("Scene '" + scene.name + "' is invalid and cannot be used in singleton operations.");
			}
		}

		public static bool InstantiatedIn(global::UnityEngine.SceneManagement.Scene scene)
		{
			EnsureSceneValid(scene);
			if (global::UnityEngine.Application.isPlaying)
			{
				return instances.ContainsKey(scene);
			}
			return FindInstances(scene).Length == 1;
		}

		public static T InstanceIn(global::UnityEngine.SceneManagement.Scene scene)
		{
			EnsureSceneValid(scene);
			if (global::UnityEngine.Application.isPlaying)
			{
				if (instances.ContainsKey(scene))
				{
					return instances[scene];
				}
				return FindOrCreateInstance(scene);
			}
			return FindOrCreateInstance(scene);
		}

		private static T[] FindObjectsOfType()
		{
			return global::UnityEngine.Object.FindObjectsByType<T>(global::UnityEngine.FindObjectsSortMode.None);
		}

		private static T[] FindInstances(global::UnityEngine.SceneManagement.Scene scene)
		{
			EnsureSceneValid(scene);
			return global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(FindObjectsOfType(), (T o) => o.gameObject.scene == scene));
		}

		private static T FindOrCreateInstance(global::UnityEngine.SceneManagement.Scene scene)
		{
			global::UnityEngine.SceneManagement.Scene scene2 = scene;
			EnsureSceneValid(scene2);
			T[] array = FindInstances(scene2);
			if (array.Length == 1)
			{
				return array[0];
			}
			if (array.Length == 0)
			{
				if (automatic)
				{
					if (persistent)
					{
						throw new global::UnityEngine.UnityException("Scene singletons cannot be persistent.");
					}
					global::UnityEngine.GameObject obj = new global::UnityEngine.GameObject(name ?? typeof(T).Name)
					{
						hideFlags = hideFlags
					};
					global::UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(obj, scene2);
					T val = obj.AddComponent<T>();
					val.hideFlags = hideFlags;
					return val;
				}
				throw new global::UnityEngine.UnityException($"Missing '{typeof(T)}' singleton in scene '{scene.name}'.");
			}
			throw new global::UnityEngine.UnityException($"More than one '{typeof(T)}' singleton in scene '{scene.name}'.");
		}

		public static void Awake(T instance)
		{
			global::Unity.VisualScripting.Ensure.That("instance").IsNotNull(instance);
			global::UnityEngine.SceneManagement.Scene scene = instance.gameObject.scene;
			EnsureSceneValid(scene);
			if (instances.ContainsKey(scene))
			{
				throw new global::UnityEngine.UnityException($"More than one '{typeof(T)}' singleton in scene '{scene.name}'.");
			}
			instances.Add(scene, instance);
		}

		public static void OnDestroy(T instance)
		{
			global::Unity.VisualScripting.Ensure.That("instance").IsNotNull(instance);
			global::UnityEngine.SceneManagement.Scene scene = instance.gameObject.scene;
			if (!scene.IsValid())
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.SceneManagement.Scene, T> instance2 in instances)
				{
					if (instance2.Value == instance)
					{
						instances.Remove(instance2.Key);
						break;
					}
				}
				return;
			}
			if (instances.ContainsKey(scene))
			{
				if (instances[scene] == instance)
				{
					instances.Remove(scene);
					return;
				}
				throw new global::UnityEngine.UnityException($"Trying to destroy invalid instance of '{typeof(T)}' singleton in scene '{scene.name}'.");
			}
			throw new global::UnityEngine.UnityException($"Trying to destroy invalid instance of '{typeof(T)}' singleton in scene '{scene.name}'.");
		}
	}
}
