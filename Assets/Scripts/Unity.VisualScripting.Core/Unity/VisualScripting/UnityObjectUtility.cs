namespace Unity.VisualScripting
{
	public static class UnityObjectUtility
	{
		public static bool IsDestroyed(this global::UnityEngine.Object target)
		{
			if ((object)target != null)
			{
				return target == null;
			}
			return false;
		}

		public static bool IsUnityNull(this object obj)
		{
			if (obj != null)
			{
				if (obj is global::UnityEngine.Object)
				{
					return (global::UnityEngine.Object)obj == null;
				}
				return false;
			}
			return true;
		}

		public static string ToSafeString(this global::UnityEngine.Object uo)
		{
			if ((object)uo == null)
			{
				return "(null)";
			}
			if (!global::Unity.VisualScripting.UnityThread.allowsAPI)
			{
				return uo.GetType().Name;
			}
			if (uo == null)
			{
				return "(Destroyed)";
			}
			try
			{
				return uo.name;
			}
			catch (global::System.Exception ex)
			{
				return "(" + ex.GetType().Name + " in ToString: " + ex.Message + ")";
			}
		}

		public static string ToSafeString(this object obj)
		{
			if (obj == null)
			{
				return "(null)";
			}
			if (obj is global::UnityEngine.Object uo)
			{
				return uo.ToSafeString();
			}
			try
			{
				return obj.ToString();
			}
			catch (global::System.Exception ex)
			{
				return "(" + ex.GetType().Name + " in ToString: " + ex.Message + ")";
			}
		}

		public static T AsUnityNull<T>(this T obj) where T : global::UnityEngine.Object
		{
			if (obj == null)
			{
				return null;
			}
			return obj;
		}

		public static bool TrulyEqual(global::UnityEngine.Object a, global::UnityEngine.Object b)
		{
			if (a != b)
			{
				return false;
			}
			if (a == null != (b == null))
			{
				return false;
			}
			return true;
		}

		public static global::System.Collections.Generic.IEnumerable<T> NotUnityNull<T>(this global::System.Collections.Generic.IEnumerable<T> enumerable) where T : global::UnityEngine.Object
		{
			return global::System.Linq.Enumerable.Where(enumerable, (T i) => i != null);
		}

		public static global::System.Collections.Generic.IEnumerable<T> FindObjectsOfTypeIncludingInactive<T>()
		{
			for (int i = 0; i < global::UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
			{
				global::UnityEngine.SceneManagement.Scene sceneAt = global::UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
				if (!sceneAt.isLoaded)
				{
					continue;
				}
				global::UnityEngine.GameObject[] rootGameObjects = sceneAt.GetRootGameObjects();
				foreach (global::UnityEngine.GameObject gameObject in rootGameObjects)
				{
					T[] componentsInChildren = gameObject.GetComponentsInChildren<T>(includeInactive: true);
					for (int k = 0; k < componentsInChildren.Length; k++)
					{
						yield return componentsInChildren[k];
					}
				}
			}
		}
	}
}
