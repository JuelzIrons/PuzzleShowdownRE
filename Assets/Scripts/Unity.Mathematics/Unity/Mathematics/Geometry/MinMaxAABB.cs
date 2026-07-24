namespace Unity.Mathematics.Geometry
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct MinMaxAABB : global::System.IEquatable<global::Unity.Mathematics.Geometry.MinMaxAABB>
	{
		public global::Unity.Mathematics.float3 Min;

		public global::Unity.Mathematics.float3 Max;

		public global::Unity.Mathematics.float3 Extents => Max - Min;

		public global::Unity.Mathematics.float3 HalfExtents => (Max - Min) * 0.5f;

		public global::Unity.Mathematics.float3 Center => (Max + Min) * 0.5f;

		public bool IsValid => global::Unity.Mathematics.math.all(Min <= Max);

		public float SurfaceArea
		{
			get
			{
				global::Unity.Mathematics.float3 x = Max - Min;
				return 2f * global::Unity.Mathematics.math.dot(x, x.yzx);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public MinMaxAABB(global::Unity.Mathematics.float3 min, global::Unity.Mathematics.float3 max)
		{
			Min = min;
			Max = max;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.Geometry.MinMaxAABB CreateFromCenterAndExtents(global::Unity.Mathematics.float3 center, global::Unity.Mathematics.float3 extents)
		{
			return CreateFromCenterAndHalfExtents(center, extents * 0.5f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.Geometry.MinMaxAABB CreateFromCenterAndHalfExtents(global::Unity.Mathematics.float3 center, global::Unity.Mathematics.float3 halfExtents)
		{
			return new global::Unity.Mathematics.Geometry.MinMaxAABB(center - halfExtents, center + halfExtents);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Contains(global::Unity.Mathematics.float3 point)
		{
			return global::Unity.Mathematics.math.all((point >= Min) & (point <= Max));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Contains(global::Unity.Mathematics.Geometry.MinMaxAABB aabb)
		{
			return global::Unity.Mathematics.math.all((Min <= aabb.Min) & (Max >= aabb.Max));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(global::Unity.Mathematics.Geometry.MinMaxAABB aabb)
		{
			return global::Unity.Mathematics.math.all((Max >= aabb.Min) & (Min <= aabb.Max));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void Expand(float signedDistance)
		{
			Min -= signedDistance;
			Max += signedDistance;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(global::Unity.Mathematics.Geometry.MinMaxAABB aabb)
		{
			Min = global::Unity.Mathematics.math.min(Min, aabb.Min);
			Max = global::Unity.Mathematics.math.max(Max, aabb.Max);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(global::Unity.Mathematics.float3 point)
		{
			Min = global::Unity.Mathematics.math.min(Min, point);
			Max = global::Unity.Mathematics.math.max(Max, point);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.Geometry.MinMaxAABB other)
		{
			if (Min.Equals(other.Min))
			{
				return Max.Equals(other.Max);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return $"MinMaxAABB({Min}, {Max})";
		}
	}
}
