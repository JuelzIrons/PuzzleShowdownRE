namespace UnityEngine.UI
{
	internal static class Misc
	{
		public static void Destroy(global::UnityEngine.Object obj)
		{
			if (!(obj != null))
			{
				return;
			}
			if (global::UnityEngine.Application.isPlaying)
			{
				if (obj is global::UnityEngine.GameObject)
				{
					(obj as global::UnityEngine.GameObject).transform.parent = null;
				}
				global::UnityEngine.Object.Destroy(obj);
			}
			else
			{
				global::UnityEngine.Object.DestroyImmediate(obj);
			}
		}

		public static void DestroyImmediate(global::UnityEngine.Object obj)
		{
			if (obj != null)
			{
				if (global::UnityEngine.Application.isEditor)
				{
					global::UnityEngine.Object.DestroyImmediate(obj);
				}
				else
				{
					global::UnityEngine.Object.Destroy(obj);
				}
			}
		}
	}
}
