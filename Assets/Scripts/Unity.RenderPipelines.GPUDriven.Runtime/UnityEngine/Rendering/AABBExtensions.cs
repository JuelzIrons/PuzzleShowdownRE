namespace UnityEngine.Rendering
{
	internal static class AABBExtensions
	{
		public static global::UnityEngine.Rendering.AABB ToAABB(this global::UnityEngine.Bounds bounds)
		{
			return new global::UnityEngine.Rendering.AABB
			{
				center = bounds.center,
				extents = bounds.extents
			};
		}

		public static global::UnityEngine.Bounds ToBounds(this global::UnityEngine.Rendering.AABB aabb)
		{
			return new global::UnityEngine.Bounds
			{
				center = aabb.center,
				extents = aabb.extents
			};
		}
	}
}
