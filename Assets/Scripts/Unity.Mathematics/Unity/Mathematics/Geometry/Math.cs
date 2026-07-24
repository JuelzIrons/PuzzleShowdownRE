namespace Unity.Mathematics.Geometry
{
	public static class Math
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.Geometry.MinMaxAABB Transform(global::Unity.Mathematics.RigidTransform transform, global::Unity.Mathematics.Geometry.MinMaxAABB aabb)
		{
			global::Unity.Mathematics.float3 halfExtents = aabb.HalfExtents;
			global::Unity.Mathematics.float3 x = global::Unity.Mathematics.math.rotate(transform.rot, new global::Unity.Mathematics.float3(halfExtents.x, 0f, 0f));
			global::Unity.Mathematics.float3 x2 = global::Unity.Mathematics.math.rotate(transform.rot, new global::Unity.Mathematics.float3(0f, halfExtents.y, 0f));
			global::Unity.Mathematics.float3 x3 = global::Unity.Mathematics.math.rotate(transform.rot, new global::Unity.Mathematics.float3(0f, 0f, halfExtents.z));
			global::Unity.Mathematics.float3 float5 = global::Unity.Mathematics.math.abs(x) + global::Unity.Mathematics.math.abs(x2) + global::Unity.Mathematics.math.abs(x3);
			global::Unity.Mathematics.float3 float6 = global::Unity.Mathematics.math.transform(transform, aabb.Center);
			return new global::Unity.Mathematics.Geometry.MinMaxAABB(float6 - float5, float6 + float5);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.Geometry.MinMaxAABB Transform(global::Unity.Mathematics.float4x4 transform, global::Unity.Mathematics.Geometry.MinMaxAABB aabb)
		{
			global::Unity.Mathematics.Geometry.MinMaxAABB result = Transform(new global::Unity.Mathematics.float3x3(transform), aabb);
			result.Min += transform.c3.xyz;
			result.Max += transform.c3.xyz;
			return result;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.Geometry.MinMaxAABB Transform(global::Unity.Mathematics.float3x3 transform, global::Unity.Mathematics.Geometry.MinMaxAABB aabb)
		{
			global::Unity.Mathematics.float3 float5 = transform.c0.xyz * aabb.Min.xxx;
			global::Unity.Mathematics.float3 float6 = transform.c0.xyz * aabb.Max.xxx;
			global::Unity.Mathematics.bool3 bool5 = float5 < float6;
			global::Unity.Mathematics.Geometry.MinMaxAABB result = new global::Unity.Mathematics.Geometry.MinMaxAABB(global::Unity.Mathematics.math.select(float6, float5, bool5), global::Unity.Mathematics.math.select(float6, float5, !bool5));
			float5 = transform.c1.xyz * aabb.Min.yyy;
			float6 = transform.c1.xyz * aabb.Max.yyy;
			bool5 = float5 < float6;
			result.Min += global::Unity.Mathematics.math.select(float6, float5, bool5);
			result.Max += global::Unity.Mathematics.math.select(float6, float5, !bool5);
			float5 = transform.c2.xyz * aabb.Min.zzz;
			float6 = transform.c2.xyz * aabb.Max.zzz;
			bool5 = float5 < float6;
			result.Min += global::Unity.Mathematics.math.select(float6, float5, bool5);
			result.Max += global::Unity.Mathematics.math.select(float6, float5, !bool5);
			return result;
		}
	}
}
