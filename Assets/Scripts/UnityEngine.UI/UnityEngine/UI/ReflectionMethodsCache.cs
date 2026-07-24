namespace UnityEngine.UI
{
	internal class ReflectionMethodsCache
	{
		public delegate bool Raycast3DCallback(global::UnityEngine.Ray r, out global::UnityEngine.RaycastHit hit, float f, int i);

		public delegate global::UnityEngine.RaycastHit[] RaycastAllCallback(global::UnityEngine.Ray r, float f, int i);

		public delegate int GetRaycastNonAllocCallback(global::UnityEngine.Ray r, global::UnityEngine.RaycastHit[] results, float f, int i);

		public delegate global::UnityEngine.RaycastHit2D Raycast2DCallback(global::UnityEngine.Vector2 p1, global::UnityEngine.Vector2 p2, float f, int i);

		public delegate global::UnityEngine.RaycastHit2D[] GetRayIntersectionAllCallback(global::UnityEngine.Ray r, float f, int i);

		public delegate int GetRayIntersectionAllNonAllocCallback(global::UnityEngine.Ray r, global::UnityEngine.RaycastHit2D[] results, float f, int i);

		public global::UnityEngine.UI.ReflectionMethodsCache.Raycast3DCallback raycast3D;

		public global::UnityEngine.UI.ReflectionMethodsCache.RaycastAllCallback raycast3DAll;

		public global::UnityEngine.UI.ReflectionMethodsCache.GetRaycastNonAllocCallback getRaycastNonAlloc;

		public global::UnityEngine.UI.ReflectionMethodsCache.Raycast2DCallback raycast2D;

		public global::UnityEngine.UI.ReflectionMethodsCache.GetRayIntersectionAllCallback getRayIntersectionAll;

		public global::UnityEngine.UI.ReflectionMethodsCache.GetRayIntersectionAllNonAllocCallback getRayIntersectionAllNonAlloc;

		private static global::UnityEngine.UI.ReflectionMethodsCache s_ReflectionMethodsCache;

		public static global::UnityEngine.UI.ReflectionMethodsCache Singleton
		{
			get
			{
				if (s_ReflectionMethodsCache == null)
				{
					s_ReflectionMethodsCache = new global::UnityEngine.UI.ReflectionMethodsCache();
				}
				return s_ReflectionMethodsCache;
			}
		}

		public ReflectionMethodsCache()
		{
			global::System.Reflection.MethodInfo method = typeof(global::UnityEngine.Physics).GetMethod("Raycast", new global::System.Type[4]
			{
				typeof(global::UnityEngine.Ray),
				typeof(global::UnityEngine.RaycastHit).MakeByRefType(),
				typeof(float),
				typeof(int)
			});
			if (method != null)
			{
				raycast3D = (global::UnityEngine.UI.ReflectionMethodsCache.Raycast3DCallback)global::System.Delegate.CreateDelegate(typeof(global::UnityEngine.UI.ReflectionMethodsCache.Raycast3DCallback), method);
			}
			global::System.Reflection.MethodInfo method2 = typeof(global::UnityEngine.Physics).GetMethod("RaycastAll", new global::System.Type[3]
			{
				typeof(global::UnityEngine.Ray),
				typeof(float),
				typeof(int)
			});
			if (method2 != null)
			{
				raycast3DAll = (global::UnityEngine.UI.ReflectionMethodsCache.RaycastAllCallback)global::System.Delegate.CreateDelegate(typeof(global::UnityEngine.UI.ReflectionMethodsCache.RaycastAllCallback), method2);
			}
			global::System.Reflection.MethodInfo method3 = typeof(global::UnityEngine.Physics).GetMethod("RaycastNonAlloc", new global::System.Type[4]
			{
				typeof(global::UnityEngine.Ray),
				typeof(global::UnityEngine.RaycastHit[]),
				typeof(float),
				typeof(int)
			});
			if (method3 != null)
			{
				getRaycastNonAlloc = (global::UnityEngine.UI.ReflectionMethodsCache.GetRaycastNonAllocCallback)global::System.Delegate.CreateDelegate(typeof(global::UnityEngine.UI.ReflectionMethodsCache.GetRaycastNonAllocCallback), method3);
			}
			global::System.Reflection.MethodInfo method4 = typeof(global::UnityEngine.Physics2D).GetMethod("Raycast", new global::System.Type[4]
			{
				typeof(global::UnityEngine.Vector2),
				typeof(global::UnityEngine.Vector2),
				typeof(float),
				typeof(int)
			});
			if (method4 != null)
			{
				raycast2D = (global::UnityEngine.UI.ReflectionMethodsCache.Raycast2DCallback)global::System.Delegate.CreateDelegate(typeof(global::UnityEngine.UI.ReflectionMethodsCache.Raycast2DCallback), method4);
			}
			global::System.Reflection.MethodInfo method5 = typeof(global::UnityEngine.Physics2D).GetMethod("GetRayIntersectionAll", new global::System.Type[3]
			{
				typeof(global::UnityEngine.Ray),
				typeof(float),
				typeof(int)
			});
			if (method5 != null)
			{
				getRayIntersectionAll = (global::UnityEngine.UI.ReflectionMethodsCache.GetRayIntersectionAllCallback)global::System.Delegate.CreateDelegate(typeof(global::UnityEngine.UI.ReflectionMethodsCache.GetRayIntersectionAllCallback), method5);
			}
			global::System.Reflection.MethodInfo method6 = typeof(global::UnityEngine.Physics2D).GetMethod("GetRayIntersectionNonAlloc", new global::System.Type[4]
			{
				typeof(global::UnityEngine.Ray),
				typeof(global::UnityEngine.RaycastHit2D[]),
				typeof(float),
				typeof(int)
			});
			if (method6 != null)
			{
				getRayIntersectionAllNonAlloc = (global::UnityEngine.UI.ReflectionMethodsCache.GetRayIntersectionAllNonAllocCallback)global::System.Delegate.CreateDelegate(typeof(global::UnityEngine.UI.ReflectionMethodsCache.GetRayIntersectionAllNonAllocCallback), method6);
			}
		}
	}
}
