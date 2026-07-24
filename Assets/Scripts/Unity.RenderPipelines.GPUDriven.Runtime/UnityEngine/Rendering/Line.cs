namespace UnityEngine.Rendering
{
	internal struct Line
	{
		public global::Unity.Mathematics.float3 m;

		public global::Unity.Mathematics.float3 t;

		internal static global::UnityEngine.Rendering.Line LineOfPlaneIntersectingPlane(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4 b)
		{
			return new global::UnityEngine.Rendering.Line
			{
				m = a.w * b.xyz - b.w * a.xyz,
				t = global::Unity.Mathematics.math.cross(a.xyz, b.xyz)
			};
		}

		internal static global::Unity.Mathematics.float4 PlaneContainingLineAndPoint(global::UnityEngine.Rendering.Line a, global::Unity.Mathematics.float3 b)
		{
			return new global::Unity.Mathematics.float4(a.m + global::Unity.Mathematics.math.cross(a.t, b), 0f - global::Unity.Mathematics.math.dot(a.m, b));
		}

		internal static global::Unity.Mathematics.float4 PlaneContainingLineWithNormalPerpendicularToVector(global::UnityEngine.Rendering.Line a, global::Unity.Mathematics.float3 b)
		{
			return new global::Unity.Mathematics.float4(global::Unity.Mathematics.math.cross(a.t, b), 0f - global::Unity.Mathematics.math.dot(a.m, b));
		}
	}
}
