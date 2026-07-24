namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	internal struct AABB
	{
		public global::Unity.Mathematics.float3 center;

		public global::Unity.Mathematics.float3 extents;

		public global::Unity.Mathematics.float3 min => center - extents;

		public global::Unity.Mathematics.float3 max => center + extents;

		public override string ToString()
		{
			return $"AABB(Center:{center}, Extents:{extents}";
		}

		private static global::Unity.Mathematics.float3 RotateExtents(global::Unity.Mathematics.float3 extents, global::Unity.Mathematics.float3 m0, global::Unity.Mathematics.float3 m1, global::Unity.Mathematics.float3 m2)
		{
			return global::Unity.Mathematics.math.abs(m0 * extents.x) + global::Unity.Mathematics.math.abs(m1 * extents.y) + global::Unity.Mathematics.math.abs(m2 * extents.z);
		}

		public static global::UnityEngine.Rendering.AABB Transform(global::Unity.Mathematics.float4x4 transform, global::UnityEngine.Rendering.AABB localBounds)
		{
			global::UnityEngine.Rendering.AABB result = default(global::UnityEngine.Rendering.AABB);
			result.extents = RotateExtents(localBounds.extents, transform.c0.xyz, transform.c1.xyz, transform.c2.xyz);
			result.center = global::Unity.Mathematics.math.transform(transform, localBounds.center);
			return result;
		}
	}
}
