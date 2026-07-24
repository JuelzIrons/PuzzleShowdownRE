namespace Unity.Mathematics
{
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public static class svd
	{
		public const float k_EpsilonDeterminant = 1E-06f;

		public const float k_EpsilonRCP = 1E-09f;

		public const float k_EpsilonNormalSqrt = 1E-15f;

		public const float k_EpsilonNormal = 1E-30f;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static void condSwap(bool c, ref float x, ref float y)
		{
			float trueValue = x;
			x = global::Unity.Mathematics.math.select(x, y, c);
			y = global::Unity.Mathematics.math.select(y, trueValue, c);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static void condNegSwap(bool c, ref global::Unity.Mathematics.float3 x, ref global::Unity.Mathematics.float3 y)
		{
			global::Unity.Mathematics.float3 trueValue = -x;
			x = global::Unity.Mathematics.math.select(x, y, c);
			y = global::Unity.Mathematics.math.select(y, trueValue, c);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static global::Unity.Mathematics.quaternion condNegSwapQuat(bool c, global::Unity.Mathematics.quaternion q, global::Unity.Mathematics.float4 mask)
		{
			return global::Unity.Mathematics.math.mul(q, global::Unity.Mathematics.math.select(global::Unity.Mathematics.quaternion.identity.value, mask * 0.70710677f, c));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static void sortSingularValues(ref global::Unity.Mathematics.float3x3 b, ref global::Unity.Mathematics.quaternion v)
		{
			float x = global::Unity.Mathematics.math.lengthsq(b.c0);
			float y = global::Unity.Mathematics.math.lengthsq(b.c1);
			float y2 = global::Unity.Mathematics.math.lengthsq(b.c2);
			bool c = x < y;
			condNegSwap(c, ref b.c0, ref b.c1);
			v = condNegSwapQuat(c, v, global::Unity.Mathematics.math.float4(0f, 0f, 1f, 1f));
			condSwap(c, ref x, ref y);
			c = x < y2;
			condNegSwap(c, ref b.c0, ref b.c2);
			v = condNegSwapQuat(c, v, global::Unity.Mathematics.math.float4(0f, -1f, 0f, 1f));
			condSwap(c, ref x, ref y2);
			c = y < y2;
			condNegSwap(c, ref b.c1, ref b.c2);
			v = condNegSwapQuat(c, v, global::Unity.Mathematics.math.float4(1f, 0f, 0f, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static global::Unity.Mathematics.quaternion approxGivensQuat(global::Unity.Mathematics.float3 pq, global::Unity.Mathematics.float4 mask)
		{
			float num = 2f * (pq.x - pq.y);
			float z = pq.z;
			return global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.select(global::Unity.Mathematics.math.float4(0.38268343f, 0.38268343f, 0.38268343f, 0.9238795f), global::Unity.Mathematics.math.float4(z, z, z, num), 5.8284273f * z * z < num * num) * mask);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static global::Unity.Mathematics.quaternion qrGivensQuat(global::Unity.Mathematics.float2 pq, global::Unity.Mathematics.float4 mask)
		{
			float num = global::Unity.Mathematics.math.sqrt(pq.x * pq.x + pq.y * pq.y);
			float x = global::Unity.Mathematics.math.select(0f, pq.y, num > 1E-15f);
			float y = global::Unity.Mathematics.math.abs(pq.x) + global::Unity.Mathematics.math.max(num, 1E-15f);
			condSwap(pq.x < 0f, ref x, ref y);
			return global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.float4(x, x, x, y) * mask);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static global::Unity.Mathematics.quaternion givensQRFactorization(global::Unity.Mathematics.float3x3 b, out global::Unity.Mathematics.float3x3 r)
		{
			global::Unity.Mathematics.quaternion obj = qrGivensQuat(global::Unity.Mathematics.math.float2(b.c0.x, b.c0.y), global::Unity.Mathematics.math.float4(0f, 0f, 1f, 1f));
			global::Unity.Mathematics.float3x3 a = global::Unity.Mathematics.math.float3x3(global::Unity.Mathematics.math.conjugate(obj));
			r = global::Unity.Mathematics.math.mul(a, b);
			global::Unity.Mathematics.quaternion quaternion2 = qrGivensQuat(global::Unity.Mathematics.math.float2(r.c0.x, r.c0.z), global::Unity.Mathematics.math.float4(0f, -1f, 0f, 1f));
			global::Unity.Mathematics.quaternion a2 = global::Unity.Mathematics.math.mul(obj, quaternion2);
			a = global::Unity.Mathematics.math.float3x3(global::Unity.Mathematics.math.conjugate(quaternion2));
			r = global::Unity.Mathematics.math.mul(a, r);
			quaternion2 = qrGivensQuat(global::Unity.Mathematics.math.float2(r.c1.y, r.c1.z), global::Unity.Mathematics.math.float4(1f, 0f, 0f, 1f));
			global::Unity.Mathematics.quaternion result = global::Unity.Mathematics.math.mul(a2, quaternion2);
			a = global::Unity.Mathematics.math.float3x3(global::Unity.Mathematics.math.conjugate(quaternion2));
			r = global::Unity.Mathematics.math.mul(a, r);
			return result;
		}

		private static global::Unity.Mathematics.quaternion jacobiIteration(ref global::Unity.Mathematics.float3x3 s, int iterations = 5)
		{
			global::Unity.Mathematics.quaternion quaternion2 = global::Unity.Mathematics.quaternion.identity;
			for (int i = 0; i < iterations; i++)
			{
				global::Unity.Mathematics.quaternion quaternion3 = approxGivensQuat(global::Unity.Mathematics.math.float3(s.c0.x, s.c1.y, s.c0.y), global::Unity.Mathematics.math.float4(0f, 0f, 1f, 1f));
				quaternion2 = global::Unity.Mathematics.math.mul(quaternion2, quaternion3);
				global::Unity.Mathematics.float3x3 float3x5 = global::Unity.Mathematics.math.float3x3(quaternion3);
				s = global::Unity.Mathematics.math.mul(global::Unity.Mathematics.math.mul(global::Unity.Mathematics.math.transpose(float3x5), s), float3x5);
				quaternion3 = approxGivensQuat(global::Unity.Mathematics.math.float3(s.c1.y, s.c2.z, s.c1.z), global::Unity.Mathematics.math.float4(1f, 0f, 0f, 1f));
				quaternion2 = global::Unity.Mathematics.math.mul(quaternion2, quaternion3);
				float3x5 = global::Unity.Mathematics.math.float3x3(quaternion3);
				s = global::Unity.Mathematics.math.mul(global::Unity.Mathematics.math.mul(global::Unity.Mathematics.math.transpose(float3x5), s), float3x5);
				quaternion3 = approxGivensQuat(global::Unity.Mathematics.math.float3(s.c2.z, s.c0.x, s.c2.x), global::Unity.Mathematics.math.float4(0f, 1f, 0f, 1f));
				quaternion2 = global::Unity.Mathematics.math.mul(quaternion2, quaternion3);
				float3x5 = global::Unity.Mathematics.math.float3x3(quaternion3);
				s = global::Unity.Mathematics.math.mul(global::Unity.Mathematics.math.mul(global::Unity.Mathematics.math.transpose(float3x5), s), float3x5);
			}
			return quaternion2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static global::Unity.Mathematics.float3 singularValuesDecomposition(global::Unity.Mathematics.float3x3 a, out global::Unity.Mathematics.quaternion u, out global::Unity.Mathematics.quaternion v)
		{
			u = global::Unity.Mathematics.quaternion.identity;
			v = global::Unity.Mathematics.quaternion.identity;
			global::Unity.Mathematics.float3x3 s = global::Unity.Mathematics.math.mul(global::Unity.Mathematics.math.transpose(a), a);
			v = jacobiIteration(ref s);
			global::Unity.Mathematics.float3x3 b = global::Unity.Mathematics.math.float3x3(v);
			b = global::Unity.Mathematics.math.mul(a, b);
			sortSingularValues(ref b, ref v);
			u = givensQRFactorization(b, out var r);
			return global::Unity.Mathematics.math.float3(r.c0.x, r.c1.y, r.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static global::Unity.Mathematics.float3 rcpsafe(global::Unity.Mathematics.float3 x, float epsilon = 1E-09f)
		{
			return global::Unity.Mathematics.math.select(global::Unity.Mathematics.math.rcp(x), global::Unity.Mathematics.float3.zero, global::Unity.Mathematics.math.abs(x) < epsilon);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 svdInverse(global::Unity.Mathematics.float3x3 a)
		{
			global::Unity.Mathematics.quaternion u;
			global::Unity.Mathematics.quaternion v;
			global::Unity.Mathematics.float3 x = singularValuesDecomposition(a, out u, out v);
			global::Unity.Mathematics.float3x3 v2 = global::Unity.Mathematics.math.float3x3(u);
			return global::Unity.Mathematics.math.mul(global::Unity.Mathematics.math.float3x3(v), global::Unity.Mathematics.math.scaleMul(rcpsafe(x, 1E-06f), global::Unity.Mathematics.math.transpose(v2)));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion svdRotation(global::Unity.Mathematics.float3x3 a)
		{
			singularValuesDecomposition(a, out var u, out var v);
			return global::Unity.Mathematics.math.mul(u, global::Unity.Mathematics.math.conjugate(v));
		}
	}
}
