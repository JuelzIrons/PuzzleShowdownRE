namespace Unity.VisualScripting
{
	public static class UnityObjectOwnershipUtility
	{
		public static void CopyOwner(object source, object destination)
		{
			if (destination is global::Unity.VisualScripting.IUnityObjectOwnable unityObjectOwnable)
			{
				unityObjectOwnable.owner = GetOwner(source);
			}
		}

		public static void RemoveOwner(object o)
		{
			if (o is global::Unity.VisualScripting.IUnityObjectOwnable unityObjectOwnable)
			{
				unityObjectOwnable.owner = null;
			}
		}

		public static global::UnityEngine.Object GetOwner(object o)
		{
			object obj = (o as global::UnityEngine.Component)?.gameObject;
			if (obj == null)
			{
				global::Unity.VisualScripting.IUnityObjectOwnable obj2 = o as global::Unity.VisualScripting.IUnityObjectOwnable;
				if (obj2 == null)
				{
					return null;
				}
				obj = obj2.owner;
			}
			return (global::UnityEngine.Object)obj;
		}
	}
}
