namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct Random
	{
		public uint state;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public Random(uint seed)
		{
			state = seed;
			NextState();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.Random CreateFromIndex(uint index)
		{
			return new global::Unity.Mathematics.Random(WangHash(index + 62));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static uint WangHash(uint n)
		{
			n = n ^ 0x3D ^ (n >> 16);
			n *= 9;
			n ^= n >> 4;
			n *= 668265261;
			n ^= n >> 15;
			return n;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void InitState(uint seed = 1851936439u)
		{
			state = seed;
			NextState();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool NextBool()
		{
			return (NextState() & 1) == 1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.bool2 NextBool2()
		{
			return (global::Unity.Mathematics.math.uint2(NextState()) & global::Unity.Mathematics.math.uint2(1u, 2u)) == 0u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.bool3 NextBool3()
		{
			return (global::Unity.Mathematics.math.uint3(NextState()) & global::Unity.Mathematics.math.uint3(1u, 2u, 4u)) == 0u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.bool4 NextBool4()
		{
			return (global::Unity.Mathematics.math.uint4(NextState()) & global::Unity.Mathematics.math.uint4(1u, 2u, 4u, 8u)) == 0u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int NextInt()
		{
			return (int)NextState() ^ int.MinValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.int2 NextInt2()
		{
			return global::Unity.Mathematics.math.int2((int)NextState(), (int)NextState()) ^ int.MinValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.int3 NextInt3()
		{
			return global::Unity.Mathematics.math.int3((int)NextState(), (int)NextState(), (int)NextState()) ^ int.MinValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.int4 NextInt4()
		{
			return global::Unity.Mathematics.math.int4((int)NextState(), (int)NextState(), (int)NextState(), (int)NextState()) ^ int.MinValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int NextInt(int max)
		{
			return (int)((ulong)(NextState() * max) >> 32);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.int2 NextInt2(global::Unity.Mathematics.int2 max)
		{
			return global::Unity.Mathematics.math.int2((int)((ulong)(NextState() * max.x) >> 32), (int)((ulong)(NextState() * max.y) >> 32));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.int3 NextInt3(global::Unity.Mathematics.int3 max)
		{
			return global::Unity.Mathematics.math.int3((int)((ulong)(NextState() * max.x) >> 32), (int)((ulong)(NextState() * max.y) >> 32), (int)((ulong)(NextState() * max.z) >> 32));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.int4 NextInt4(global::Unity.Mathematics.int4 max)
		{
			return global::Unity.Mathematics.math.int4((int)((ulong)(NextState() * max.x) >> 32), (int)((ulong)(NextState() * max.y) >> 32), (int)((ulong)(NextState() * max.z) >> 32), (int)((ulong)(NextState() * max.w) >> 32));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int NextInt(int min, int max)
		{
			uint num = (uint)(max - min);
			return (int)((ulong)((long)NextState() * (long)num) >> 32) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.int2 NextInt2(global::Unity.Mathematics.int2 min, global::Unity.Mathematics.int2 max)
		{
			global::Unity.Mathematics.uint2 uint5 = (global::Unity.Mathematics.uint2)(max - min);
			return global::Unity.Mathematics.math.int2((int)((ulong)((long)NextState() * (long)uint5.x) >> 32), (int)((ulong)((long)NextState() * (long)uint5.y) >> 32)) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.int3 NextInt3(global::Unity.Mathematics.int3 min, global::Unity.Mathematics.int3 max)
		{
			global::Unity.Mathematics.uint3 uint5 = (global::Unity.Mathematics.uint3)(max - min);
			return global::Unity.Mathematics.math.int3((int)((ulong)((long)NextState() * (long)uint5.x) >> 32), (int)((ulong)((long)NextState() * (long)uint5.y) >> 32), (int)((ulong)((long)NextState() * (long)uint5.z) >> 32)) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.int4 NextInt4(global::Unity.Mathematics.int4 min, global::Unity.Mathematics.int4 max)
		{
			global::Unity.Mathematics.uint4 uint5 = (global::Unity.Mathematics.uint4)(max - min);
			return global::Unity.Mathematics.math.int4((int)((ulong)((long)NextState() * (long)uint5.x) >> 32), (int)((ulong)((long)NextState() * (long)uint5.y) >> 32), (int)((ulong)((long)NextState() * (long)uint5.z) >> 32), (int)((ulong)((long)NextState() * (long)uint5.w) >> 32)) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint NextUInt()
		{
			return NextState() - 1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.uint2 NextUInt2()
		{
			return global::Unity.Mathematics.math.uint2(NextState(), NextState()) - 1u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.uint3 NextUInt3()
		{
			return global::Unity.Mathematics.math.uint3(NextState(), NextState(), NextState()) - 1u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.uint4 NextUInt4()
		{
			return global::Unity.Mathematics.math.uint4(NextState(), NextState(), NextState(), NextState()) - 1u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint NextUInt(uint max)
		{
			return (uint)((ulong)((long)NextState() * (long)max) >> 32);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.uint2 NextUInt2(global::Unity.Mathematics.uint2 max)
		{
			return global::Unity.Mathematics.math.uint2((uint)((ulong)((long)NextState() * (long)max.x) >> 32), (uint)((ulong)((long)NextState() * (long)max.y) >> 32));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.uint3 NextUInt3(global::Unity.Mathematics.uint3 max)
		{
			return global::Unity.Mathematics.math.uint3((uint)((ulong)((long)NextState() * (long)max.x) >> 32), (uint)((ulong)((long)NextState() * (long)max.y) >> 32), (uint)((ulong)((long)NextState() * (long)max.z) >> 32));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.uint4 NextUInt4(global::Unity.Mathematics.uint4 max)
		{
			return global::Unity.Mathematics.math.uint4((uint)((ulong)((long)NextState() * (long)max.x) >> 32), (uint)((ulong)((long)NextState() * (long)max.y) >> 32), (uint)((ulong)((long)NextState() * (long)max.z) >> 32), (uint)((ulong)((long)NextState() * (long)max.w) >> 32));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint NextUInt(uint min, uint max)
		{
			uint num = max - min;
			return (uint)(int)((ulong)((long)NextState() * (long)num) >> 32) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.uint2 NextUInt2(global::Unity.Mathematics.uint2 min, global::Unity.Mathematics.uint2 max)
		{
			global::Unity.Mathematics.uint2 uint5 = max - min;
			return global::Unity.Mathematics.math.uint2((uint)((ulong)((long)NextState() * (long)uint5.x) >> 32), (uint)((ulong)((long)NextState() * (long)uint5.y) >> 32)) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.uint3 NextUInt3(global::Unity.Mathematics.uint3 min, global::Unity.Mathematics.uint3 max)
		{
			global::Unity.Mathematics.uint3 uint5 = max - min;
			return global::Unity.Mathematics.math.uint3((uint)((ulong)((long)NextState() * (long)uint5.x) >> 32), (uint)((ulong)((long)NextState() * (long)uint5.y) >> 32), (uint)((ulong)((long)NextState() * (long)uint5.z) >> 32)) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.uint4 NextUInt4(global::Unity.Mathematics.uint4 min, global::Unity.Mathematics.uint4 max)
		{
			global::Unity.Mathematics.uint4 uint5 = max - min;
			return global::Unity.Mathematics.math.uint4((uint)((ulong)((long)NextState() * (long)uint5.x) >> 32), (uint)((ulong)((long)NextState() * (long)uint5.y) >> 32), (uint)((ulong)((long)NextState() * (long)uint5.z) >> 32), (uint)((ulong)((long)NextState() * (long)uint5.w) >> 32)) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float NextFloat()
		{
			return global::Unity.Mathematics.math.asfloat(0x3F800000 | (NextState() >> 9)) - 1f;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float2 NextFloat2()
		{
			return global::Unity.Mathematics.math.asfloat(1065353216u | (global::Unity.Mathematics.math.uint2(NextState(), NextState()) >> 9)) - 1f;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float3 NextFloat3()
		{
			return global::Unity.Mathematics.math.asfloat(1065353216u | (global::Unity.Mathematics.math.uint3(NextState(), NextState(), NextState()) >> 9)) - 1f;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float4 NextFloat4()
		{
			return global::Unity.Mathematics.math.asfloat(1065353216u | (global::Unity.Mathematics.math.uint4(NextState(), NextState(), NextState(), NextState()) >> 9)) - 1f;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float NextFloat(float max)
		{
			return NextFloat() * max;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float2 NextFloat2(global::Unity.Mathematics.float2 max)
		{
			return NextFloat2() * max;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float3 NextFloat3(global::Unity.Mathematics.float3 max)
		{
			return NextFloat3() * max;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float4 NextFloat4(global::Unity.Mathematics.float4 max)
		{
			return NextFloat4() * max;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float NextFloat(float min, float max)
		{
			return NextFloat() * (max - min) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float2 NextFloat2(global::Unity.Mathematics.float2 min, global::Unity.Mathematics.float2 max)
		{
			return NextFloat2() * (max - min) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float3 NextFloat3(global::Unity.Mathematics.float3 min, global::Unity.Mathematics.float3 max)
		{
			return NextFloat3() * (max - min) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float4 NextFloat4(global::Unity.Mathematics.float4 min, global::Unity.Mathematics.float4 max)
		{
			return NextFloat4() * (max - min) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double NextDouble()
		{
			ulong num = ((ulong)NextState() << 20) ^ NextState();
			return global::Unity.Mathematics.math.asdouble(0x3FF0000000000000L | num) - 1.0;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.double2 NextDouble2()
		{
			ulong num = ((ulong)NextState() << 20) ^ NextState();
			ulong num2 = ((ulong)NextState() << 20) ^ NextState();
			return global::Unity.Mathematics.math.double2(global::Unity.Mathematics.math.asdouble(0x3FF0000000000000L | num), global::Unity.Mathematics.math.asdouble(0x3FF0000000000000L | num2)) - 1.0;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.double3 NextDouble3()
		{
			ulong num = ((ulong)NextState() << 20) ^ NextState();
			ulong num2 = ((ulong)NextState() << 20) ^ NextState();
			ulong num3 = ((ulong)NextState() << 20) ^ NextState();
			return global::Unity.Mathematics.math.double3(global::Unity.Mathematics.math.asdouble(0x3FF0000000000000L | num), global::Unity.Mathematics.math.asdouble(0x3FF0000000000000L | num2), global::Unity.Mathematics.math.asdouble(0x3FF0000000000000L | num3)) - 1.0;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.double4 NextDouble4()
		{
			ulong num = ((ulong)NextState() << 20) ^ NextState();
			ulong num2 = ((ulong)NextState() << 20) ^ NextState();
			ulong num3 = ((ulong)NextState() << 20) ^ NextState();
			ulong num4 = ((ulong)NextState() << 20) ^ NextState();
			return global::Unity.Mathematics.math.double4(global::Unity.Mathematics.math.asdouble(0x3FF0000000000000L | num), global::Unity.Mathematics.math.asdouble(0x3FF0000000000000L | num2), global::Unity.Mathematics.math.asdouble(0x3FF0000000000000L | num3), global::Unity.Mathematics.math.asdouble(0x3FF0000000000000L | num4)) - 1.0;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double NextDouble(double max)
		{
			return NextDouble() * max;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.double2 NextDouble2(global::Unity.Mathematics.double2 max)
		{
			return NextDouble2() * max;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.double3 NextDouble3(global::Unity.Mathematics.double3 max)
		{
			return NextDouble3() * max;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.double4 NextDouble4(global::Unity.Mathematics.double4 max)
		{
			return NextDouble4() * max;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double NextDouble(double min, double max)
		{
			return NextDouble() * (max - min) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.double2 NextDouble2(global::Unity.Mathematics.double2 min, global::Unity.Mathematics.double2 max)
		{
			return NextDouble2() * (max - min) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.double3 NextDouble3(global::Unity.Mathematics.double3 min, global::Unity.Mathematics.double3 max)
		{
			return NextDouble3() * (max - min) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.double4 NextDouble4(global::Unity.Mathematics.double4 min, global::Unity.Mathematics.double4 max)
		{
			return NextDouble4() * (max - min) + min;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float2 NextFloat2Direction()
		{
			global::Unity.Mathematics.math.sincos(NextFloat() * global::System.MathF.PI * 2f, out var s, out var c);
			return global::Unity.Mathematics.math.float2(c, s);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.double2 NextDouble2Direction()
		{
			global::Unity.Mathematics.math.sincos(NextDouble() * global::System.Math.PI * 2.0, out var s, out var c);
			return global::Unity.Mathematics.math.double2(c, s);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float3 NextFloat3Direction()
		{
			global::Unity.Mathematics.float2 obj = NextFloat2();
			float num = obj.x * 2f - 1f;
			float num2 = global::Unity.Mathematics.math.sqrt(global::Unity.Mathematics.math.max(1f - num * num, 0f));
			global::Unity.Mathematics.math.sincos(obj.y * global::System.MathF.PI * 2f, out var s, out var c);
			return global::Unity.Mathematics.math.float3(c * num2, s * num2, num);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.double3 NextDouble3Direction()
		{
			global::Unity.Mathematics.double2 obj = NextDouble2();
			double num = obj.x * 2.0 - 1.0;
			double num2 = global::Unity.Mathematics.math.sqrt(global::Unity.Mathematics.math.max(1.0 - num * num, 0.0));
			global::Unity.Mathematics.math.sincos(obj.y * global::System.Math.PI * 2.0, out var s, out var c);
			return global::Unity.Mathematics.math.double3(c * num2, s * num2, num);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.quaternion NextQuaternionRotation()
		{
			global::Unity.Mathematics.float3 float5 = NextFloat3(global::Unity.Mathematics.math.float3(global::System.MathF.PI * 2f, global::System.MathF.PI * 2f, 1f));
			float z = float5.z;
			global::Unity.Mathematics.float2 xy = float5.xy;
			float num = global::Unity.Mathematics.math.sqrt(1f - z);
			float num2 = global::Unity.Mathematics.math.sqrt(z);
			global::Unity.Mathematics.math.sincos(xy, out var s, out var c);
			global::Unity.Mathematics.quaternion quaternion2 = global::Unity.Mathematics.math.quaternion(num * s.x, num * c.x, num2 * s.y, num2 * c.y);
			return global::Unity.Mathematics.math.quaternion(global::Unity.Mathematics.math.select(quaternion2.value, -quaternion2.value, quaternion2.value.w < 0f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private uint NextState()
		{
			uint result = state;
			state ^= state << 13;
			state ^= state >> 17;
			state ^= state << 5;
			return result;
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckInitState()
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckIndexForHash(uint index)
		{
			if (index == uint.MaxValue)
			{
				throw new global::System.ArgumentException("Index must not be uint.MaxValue");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckState()
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNextIntMax(int max)
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNextIntMinMax(int min, int max)
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNextUIntMinMax(uint min, uint max)
		{
		}
	}
}
