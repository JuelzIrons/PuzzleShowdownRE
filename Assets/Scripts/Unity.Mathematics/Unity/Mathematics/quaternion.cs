namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct quaternion : global::System.IEquatable<global::Unity.Mathematics.quaternion>, global::System.IFormattable
	{
		public global::Unity.Mathematics.float4 value;

		public static readonly global::Unity.Mathematics.quaternion identity = new global::Unity.Mathematics.quaternion(0f, 0f, 0f, 1f);

		public static implicit operator global::UnityEngine.Quaternion(global::Unity.Mathematics.quaternion q)
		{
			return new global::UnityEngine.Quaternion(q.value.x, q.value.y, q.value.z, q.value.w);
		}

		public static implicit operator global::Unity.Mathematics.quaternion(global::UnityEngine.Quaternion q)
		{
			return new global::Unity.Mathematics.quaternion(q.x, q.y, q.z, q.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public quaternion(float x, float y, float z, float w)
		{
			value.x = x;
			value.y = y;
			value.z = z;
			value.w = w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public quaternion(global::Unity.Mathematics.float4 value)
		{
			this.value = value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.quaternion(global::Unity.Mathematics.float4 v)
		{
			return new global::Unity.Mathematics.quaternion(v);
		}

		public quaternion(global::Unity.Mathematics.float3x3 m)
		{
			global::Unity.Mathematics.float3 c = m.c0;
			global::Unity.Mathematics.float3 c2 = m.c1;
			global::Unity.Mathematics.float3 c3 = m.c2;
			uint num = global::Unity.Mathematics.math.asuint(c.x) & 0x80000000u;
			float x = c2.y + global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(c3.z) ^ num);
			global::Unity.Mathematics.uint4 uint5 = global::Unity.Mathematics.math.uint4((int)num >> 31);
			global::Unity.Mathematics.uint4 uint6 = global::Unity.Mathematics.math.uint4(global::Unity.Mathematics.math.asint(x) >> 31);
			float x2 = 1f + global::Unity.Mathematics.math.abs(c.x);
			global::Unity.Mathematics.uint4 uint7 = global::Unity.Mathematics.math.uint4(0u, 2147483648u, 2147483648u, 2147483648u) ^ (uint5 & global::Unity.Mathematics.math.uint4(0u, 2147483648u, 0u, 2147483648u)) ^ (uint6 & global::Unity.Mathematics.math.uint4(2147483648u, 2147483648u, 2147483648u, 0u));
			value = global::Unity.Mathematics.math.float4(x2, c.y, c3.x, c2.z) + global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(global::Unity.Mathematics.math.float4(x, c2.x, c.z, c3.y)) ^ uint7);
			value = global::Unity.Mathematics.math.asfloat((global::Unity.Mathematics.math.asuint(value) & ~uint5) | (global::Unity.Mathematics.math.asuint(value.zwxy) & uint5));
			value = global::Unity.Mathematics.math.asfloat((global::Unity.Mathematics.math.asuint(value.wzyx) & ~uint6) | (global::Unity.Mathematics.math.asuint(value) & uint6));
			value = global::Unity.Mathematics.math.normalize(value);
		}

		public quaternion(global::Unity.Mathematics.float4x4 m)
		{
			global::Unity.Mathematics.float4 c = m.c0;
			global::Unity.Mathematics.float4 c2 = m.c1;
			global::Unity.Mathematics.float4 c3 = m.c2;
			uint num = global::Unity.Mathematics.math.asuint(c.x) & 0x80000000u;
			float x = c2.y + global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(c3.z) ^ num);
			global::Unity.Mathematics.uint4 uint5 = global::Unity.Mathematics.math.uint4((int)num >> 31);
			global::Unity.Mathematics.uint4 uint6 = global::Unity.Mathematics.math.uint4(global::Unity.Mathematics.math.asint(x) >> 31);
			float x2 = 1f + global::Unity.Mathematics.math.abs(c.x);
			global::Unity.Mathematics.uint4 uint7 = global::Unity.Mathematics.math.uint4(0u, 2147483648u, 2147483648u, 2147483648u) ^ (uint5 & global::Unity.Mathematics.math.uint4(0u, 2147483648u, 0u, 2147483648u)) ^ (uint6 & global::Unity.Mathematics.math.uint4(2147483648u, 2147483648u, 2147483648u, 0u));
			value = global::Unity.Mathematics.math.float4(x2, c.y, c3.x, c2.z) + global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(global::Unity.Mathematics.math.float4(x, c2.x, c.z, c3.y)) ^ uint7);
			value = global::Unity.Mathematics.math.asfloat((global::Unity.Mathematics.math.asuint(value) & ~uint5) | (global::Unity.Mathematics.math.asuint(value.zwxy) & uint5));
			value = global::Unity.Mathematics.math.asfloat((global::Unity.Mathematics.math.asuint(value.wzyx) & ~uint6) | (global::Unity.Mathematics.math.asuint(value) & uint6));
			value = global::Unity.Mathematics.math.normalize(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion AxisAngle(global::Unity.Mathematics.float3 axis, float angle)
		{
			global::Unity.Mathematics.math.sincos(0.5f * angle, out var s, out var c);
			return global::Unity.Mathematics.math.quaternion(global::Unity.Mathematics.math.float4(axis * s, c));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerXYZ(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(0.5f * xyz, out var s, out var c);
			return global::Unity.Mathematics.math.quaternion(global::Unity.Mathematics.math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * global::Unity.Mathematics.math.float4(c.xyz, s.x) * global::Unity.Mathematics.math.float4(-1f, 1f, -1f, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerXZY(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(0.5f * xyz, out var s, out var c);
			return global::Unity.Mathematics.math.quaternion(global::Unity.Mathematics.math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * global::Unity.Mathematics.math.float4(c.xyz, s.x) * global::Unity.Mathematics.math.float4(1f, 1f, -1f, -1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerYXZ(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(0.5f * xyz, out var s, out var c);
			return global::Unity.Mathematics.math.quaternion(global::Unity.Mathematics.math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * global::Unity.Mathematics.math.float4(c.xyz, s.x) * global::Unity.Mathematics.math.float4(-1f, 1f, 1f, -1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerYZX(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(0.5f * xyz, out var s, out var c);
			return global::Unity.Mathematics.math.quaternion(global::Unity.Mathematics.math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * global::Unity.Mathematics.math.float4(c.xyz, s.x) * global::Unity.Mathematics.math.float4(-1f, -1f, 1f, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerZXY(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(0.5f * xyz, out var s, out var c);
			return global::Unity.Mathematics.math.quaternion(global::Unity.Mathematics.math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * global::Unity.Mathematics.math.float4(c.xyz, s.x) * global::Unity.Mathematics.math.float4(1f, -1f, -1f, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerZYX(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(0.5f * xyz, out var s, out var c);
			return global::Unity.Mathematics.math.quaternion(global::Unity.Mathematics.math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * global::Unity.Mathematics.math.float4(c.xyz, s.x) * global::Unity.Mathematics.math.float4(1f, -1f, 1f, -1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerXYZ(float x, float y, float z)
		{
			return EulerXYZ(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerXZY(float x, float y, float z)
		{
			return EulerXZY(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerYXZ(float x, float y, float z)
		{
			return EulerYXZ(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerYZX(float x, float y, float z)
		{
			return EulerYZX(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerZXY(float x, float y, float z)
		{
			return EulerZXY(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion EulerZYX(float x, float y, float z)
		{
			return EulerZYX(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion Euler(global::Unity.Mathematics.float3 xyz, global::Unity.Mathematics.math.RotationOrder order = global::Unity.Mathematics.math.RotationOrder.ZXY)
		{
			return order switch
			{
				global::Unity.Mathematics.math.RotationOrder.XYZ => EulerXYZ(xyz), 
				global::Unity.Mathematics.math.RotationOrder.XZY => EulerXZY(xyz), 
				global::Unity.Mathematics.math.RotationOrder.YXZ => EulerYXZ(xyz), 
				global::Unity.Mathematics.math.RotationOrder.YZX => EulerYZX(xyz), 
				global::Unity.Mathematics.math.RotationOrder.ZXY => EulerZXY(xyz), 
				global::Unity.Mathematics.math.RotationOrder.ZYX => EulerZYX(xyz), 
				_ => identity, 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion Euler(float x, float y, float z, global::Unity.Mathematics.math.RotationOrder order = global::Unity.Mathematics.math.RotationOrder.ZXY)
		{
			return Euler(global::Unity.Mathematics.math.float3(x, y, z), order);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion RotateX(float angle)
		{
			global::Unity.Mathematics.math.sincos(0.5f * angle, out var s, out var c);
			return global::Unity.Mathematics.math.quaternion(s, 0f, 0f, c);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion RotateY(float angle)
		{
			global::Unity.Mathematics.math.sincos(0.5f * angle, out var s, out var c);
			return global::Unity.Mathematics.math.quaternion(0f, s, 0f, c);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion RotateZ(float angle)
		{
			global::Unity.Mathematics.math.sincos(0.5f * angle, out var s, out var c);
			return global::Unity.Mathematics.math.quaternion(0f, 0f, s, c);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion LookRotation(global::Unity.Mathematics.float3 forward, global::Unity.Mathematics.float3 up)
		{
			global::Unity.Mathematics.float3 float5 = global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.cross(up, forward));
			return global::Unity.Mathematics.math.quaternion(global::Unity.Mathematics.math.float3x3(float5, global::Unity.Mathematics.math.cross(forward, float5), forward));
		}

		public static global::Unity.Mathematics.quaternion LookRotationSafe(global::Unity.Mathematics.float3 forward, global::Unity.Mathematics.float3 up)
		{
			float x = global::Unity.Mathematics.math.dot(forward, forward);
			float num = global::Unity.Mathematics.math.dot(up, up);
			forward *= global::Unity.Mathematics.math.rsqrt(x);
			up *= global::Unity.Mathematics.math.rsqrt(num);
			global::Unity.Mathematics.float3 float5 = global::Unity.Mathematics.math.cross(up, forward);
			float num2 = global::Unity.Mathematics.math.dot(float5, float5);
			float5 *= global::Unity.Mathematics.math.rsqrt(num2);
			float num3 = global::Unity.Mathematics.math.min(global::Unity.Mathematics.math.min(x, num), num2);
			float num4 = global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.max(x, num), num2);
			bool test = num3 > 1E-35f && num4 < 1E+35f && global::Unity.Mathematics.math.isfinite(x) && global::Unity.Mathematics.math.isfinite(num) && global::Unity.Mathematics.math.isfinite(num2);
			return global::Unity.Mathematics.math.quaternion(global::Unity.Mathematics.math.select(global::Unity.Mathematics.math.float4(0f, 0f, 0f, 1f), global::Unity.Mathematics.math.quaternion(global::Unity.Mathematics.math.float3x3(float5, global::Unity.Mathematics.math.cross(forward, float5), forward)).value, test));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.quaternion x)
		{
			if (value.x == x.value.x && value.y == x.value.y && value.z == x.value.z)
			{
				return value.w == x.value.w;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object x)
		{
			if (x is global::Unity.Mathematics.quaternion x2)
			{
				return Equals(x2);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)global::Unity.Mathematics.math.hash(this);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return $"quaternion({value.x}f, {value.y}f, {value.z}f, {value.w}f)";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"quaternion({value.x.ToString(format, formatProvider)}f, {value.y.ToString(format, formatProvider)}f, {value.z.ToString(format, formatProvider)}f, {value.w.ToString(format, formatProvider)}f)";
		}
	}
}
