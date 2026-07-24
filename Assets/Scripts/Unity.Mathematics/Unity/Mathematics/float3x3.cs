namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct float3x3 : global::System.IEquatable<global::Unity.Mathematics.float3x3>, global::System.IFormattable
	{
		public global::Unity.Mathematics.float3 c0;

		public global::Unity.Mathematics.float3 c1;

		public global::Unity.Mathematics.float3 c2;

		public static readonly global::Unity.Mathematics.float3x3 identity = new global::Unity.Mathematics.float3x3(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f);

		public static readonly global::Unity.Mathematics.float3x3 zero;

		public unsafe ref global::Unity.Mathematics.float3 this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.float3x3* ptr = &this)
				{
					return ref *(global::Unity.Mathematics.float3*)((byte*)ptr + (nint)index * (nint)sizeof(global::Unity.Mathematics.float3));
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float3x3(global::Unity.Mathematics.float3 c0, global::Unity.Mathematics.float3 c1, global::Unity.Mathematics.float3 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float3x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
		{
			c0 = new global::Unity.Mathematics.float3(m00, m10, m20);
			c1 = new global::Unity.Mathematics.float3(m01, m11, m21);
			c2 = new global::Unity.Mathematics.float3(m02, m12, m22);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float3x3(float v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float3x3(bool v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float3(0f), new global::Unity.Mathematics.float3(1f), v);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float3(0f), new global::Unity.Mathematics.float3(1f), v);
			c2 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float3(0f), new global::Unity.Mathematics.float3(1f), v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float3x3(global::Unity.Mathematics.bool3x3 v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float3(0f), new global::Unity.Mathematics.float3(1f), v.c0);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float3(0f), new global::Unity.Mathematics.float3(1f), v.c1);
			c2 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float3(0f), new global::Unity.Mathematics.float3(1f), v.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float3x3(int v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float3x3(global::Unity.Mathematics.int3x3 v)
		{
			c0 = v.c0;
			c1 = v.c1;
			c2 = v.c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float3x3(uint v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float3x3(global::Unity.Mathematics.uint3x3 v)
		{
			c0 = v.c0;
			c1 = v.c1;
			c2 = v.c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float3x3(double v)
		{
			c0 = (global::Unity.Mathematics.float3)v;
			c1 = (global::Unity.Mathematics.float3)v;
			c2 = (global::Unity.Mathematics.float3)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float3x3(global::Unity.Mathematics.double3x3 v)
		{
			c0 = (global::Unity.Mathematics.float3)v.c0;
			c1 = (global::Unity.Mathematics.float3)v.c1;
			c2 = (global::Unity.Mathematics.float3)v.c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float3x3(float v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float3x3(bool v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float3x3(global::Unity.Mathematics.bool3x3 v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float3x3(int v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float3x3(global::Unity.Mathematics.int3x3 v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float3x3(uint v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float3x3(global::Unity.Mathematics.uint3x3 v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float3x3(double v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float3x3(global::Unity.Mathematics.double3x3 v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator *(global::Unity.Mathematics.float3x3 lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator *(global::Unity.Mathematics.float3x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator *(float lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator +(global::Unity.Mathematics.float3x3 lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator +(global::Unity.Mathematics.float3x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator +(float lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator -(global::Unity.Mathematics.float3x3 lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator -(global::Unity.Mathematics.float3x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator -(float lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator /(global::Unity.Mathematics.float3x3 lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator /(global::Unity.Mathematics.float3x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator /(float lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator %(global::Unity.Mathematics.float3x3 lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator %(global::Unity.Mathematics.float3x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator %(float lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.float3x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator ++(global::Unity.Mathematics.float3x3 val)
		{
			return new global::Unity.Mathematics.float3x3(++val.c0, ++val.c1, ++val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator --(global::Unity.Mathematics.float3x3 val)
		{
			return new global::Unity.Mathematics.float3x3(--val.c0, --val.c1, --val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator <(global::Unity.Mathematics.float3x3 lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator <(global::Unity.Mathematics.float3x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator <(float lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator <=(global::Unity.Mathematics.float3x3 lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator <=(global::Unity.Mathematics.float3x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator <=(float lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator >(global::Unity.Mathematics.float3x3 lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator >(global::Unity.Mathematics.float3x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator >(float lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator >=(global::Unity.Mathematics.float3x3 lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator >=(global::Unity.Mathematics.float3x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator >=(float lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator -(global::Unity.Mathematics.float3x3 val)
		{
			return new global::Unity.Mathematics.float3x3(-val.c0, -val.c1, -val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 operator +(global::Unity.Mathematics.float3x3 val)
		{
			return new global::Unity.Mathematics.float3x3(+val.c0, +val.c1, +val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator ==(global::Unity.Mathematics.float3x3 lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator ==(global::Unity.Mathematics.float3x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator ==(float lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator !=(global::Unity.Mathematics.float3x3 lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator !=(global::Unity.Mathematics.float3x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 operator !=(float lhs, global::Unity.Mathematics.float3x3 rhs)
		{
			return new global::Unity.Mathematics.bool3x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.float3x3 rhs)
		{
			if (c0.Equals(rhs.c0) && c1.Equals(rhs.c1))
			{
				return c2.Equals(rhs.c2);
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.float3x3 rhs)
			{
				return Equals(rhs);
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
			return $"float3x3({c0.x}f, {c1.x}f, {c2.x}f,  {c0.y}f, {c1.y}f, {c2.y}f,  {c0.z}f, {c1.z}f, {c2.z}f)";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"float3x3({c0.x.ToString(format, formatProvider)}f, {c1.x.ToString(format, formatProvider)}f, {c2.x.ToString(format, formatProvider)}f,  {c0.y.ToString(format, formatProvider)}f, {c1.y.ToString(format, formatProvider)}f, {c2.y.ToString(format, formatProvider)}f,  {c0.z.ToString(format, formatProvider)}f, {c1.z.ToString(format, formatProvider)}f, {c2.z.ToString(format, formatProvider)}f)";
		}

		public float3x3(global::Unity.Mathematics.float4x4 f4x4)
		{
			c0 = f4x4.c0.xyz;
			c1 = f4x4.c1.xyz;
			c2 = f4x4.c2.xyz;
		}

		public float3x3(global::Unity.Mathematics.quaternion q)
		{
			global::Unity.Mathematics.float4 value = q.value;
			global::Unity.Mathematics.float4 float5 = value + value;
			global::Unity.Mathematics.uint3 uint5 = global::Unity.Mathematics.math.uint3(2147483648u, 0u, 2147483648u);
			global::Unity.Mathematics.uint3 uint6 = global::Unity.Mathematics.math.uint3(2147483648u, 2147483648u, 0u);
			global::Unity.Mathematics.uint3 uint7 = global::Unity.Mathematics.math.uint3(0u, 2147483648u, 2147483648u);
			c0 = float5.y * global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(value.yxw) ^ uint5) - float5.z * global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(value.zwx) ^ uint7) + global::Unity.Mathematics.math.float3(1f, 0f, 0f);
			c1 = float5.z * global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(value.wzy) ^ uint6) - float5.x * global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(value.yxw) ^ uint5) + global::Unity.Mathematics.math.float3(0f, 1f, 0f);
			c2 = float5.x * global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(value.zwx) ^ uint7) - float5.y * global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(value.wzy) ^ uint6) + global::Unity.Mathematics.math.float3(0f, 0f, 1f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 AxisAngle(global::Unity.Mathematics.float3 axis, float angle)
		{
			global::Unity.Mathematics.math.sincos(angle, out var s, out var c);
			global::Unity.Mathematics.float3 float5 = axis;
			_ = float5.yzx;
			_ = float5.zxy;
			global::Unity.Mathematics.float3 float6 = float5 - float5 * c;
			global::Unity.Mathematics.float4 float7 = global::Unity.Mathematics.math.float4(float5 * s, c);
			global::Unity.Mathematics.uint3 uint5 = global::Unity.Mathematics.math.uint3(0u, 0u, 2147483648u);
			global::Unity.Mathematics.uint3 uint6 = global::Unity.Mathematics.math.uint3(2147483648u, 0u, 0u);
			global::Unity.Mathematics.uint3 uint7 = global::Unity.Mathematics.math.uint3(0u, 2147483648u, 0u);
			return global::Unity.Mathematics.math.float3x3(float5.x * float6 + global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(float7.wzy) ^ uint5), float5.y * float6 + global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(float7.zwx) ^ uint6), float5.z * float6 + global::Unity.Mathematics.math.asfloat(global::Unity.Mathematics.math.asuint(float7.yxw) ^ uint7));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerXYZ(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(xyz, out var s, out var c);
			return global::Unity.Mathematics.math.float3x3(c.y * c.z, c.z * s.x * s.y - c.x * s.z, c.x * c.z * s.y + s.x * s.z, c.y * s.z, c.x * c.z + s.x * s.y * s.z, c.x * s.y * s.z - c.z * s.x, 0f - s.y, c.y * s.x, c.x * c.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerXZY(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(xyz, out var s, out var c);
			return global::Unity.Mathematics.math.float3x3(c.y * c.z, s.x * s.y - c.x * c.y * s.z, c.x * s.y + c.y * s.x * s.z, s.z, c.x * c.z, (0f - c.z) * s.x, (0f - c.z) * s.y, c.y * s.x + c.x * s.y * s.z, c.x * c.y - s.x * s.y * s.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerYXZ(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(xyz, out var s, out var c);
			return global::Unity.Mathematics.math.float3x3(c.y * c.z - s.x * s.y * s.z, (0f - c.x) * s.z, c.z * s.y + c.y * s.x * s.z, c.z * s.x * s.y + c.y * s.z, c.x * c.z, s.y * s.z - c.y * c.z * s.x, (0f - c.x) * s.y, s.x, c.x * c.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerYZX(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(xyz, out var s, out var c);
			return global::Unity.Mathematics.math.float3x3(c.y * c.z, 0f - s.z, c.z * s.y, s.x * s.y + c.x * c.y * s.z, c.x * c.z, c.x * s.y * s.z - c.y * s.x, c.y * s.x * s.z - c.x * s.y, c.z * s.x, c.x * c.y + s.x * s.y * s.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerZXY(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(xyz, out var s, out var c);
			return global::Unity.Mathematics.math.float3x3(c.y * c.z + s.x * s.y * s.z, c.z * s.x * s.y - c.y * s.z, c.x * s.y, c.x * s.z, c.x * c.z, 0f - s.x, c.y * s.x * s.z - c.z * s.y, c.y * c.z * s.x + s.y * s.z, c.x * c.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerZYX(global::Unity.Mathematics.float3 xyz)
		{
			global::Unity.Mathematics.math.sincos(xyz, out var s, out var c);
			return global::Unity.Mathematics.math.float3x3(c.y * c.z, (0f - c.y) * s.z, s.y, c.z * s.x * s.y + c.x * s.z, c.x * c.z - s.x * s.y * s.z, (0f - c.y) * s.x, s.x * s.z - c.x * c.z * s.y, c.z * s.x + c.x * s.y * s.z, c.x * c.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerXYZ(float x, float y, float z)
		{
			return EulerXYZ(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerXZY(float x, float y, float z)
		{
			return EulerXZY(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerYXZ(float x, float y, float z)
		{
			return EulerYXZ(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerYZX(float x, float y, float z)
		{
			return EulerYZX(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerZXY(float x, float y, float z)
		{
			return EulerZXY(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 EulerZYX(float x, float y, float z)
		{
			return EulerZYX(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 Euler(global::Unity.Mathematics.float3 xyz, global::Unity.Mathematics.math.RotationOrder order = global::Unity.Mathematics.math.RotationOrder.ZXY)
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
		public static global::Unity.Mathematics.float3x3 Euler(float x, float y, float z, global::Unity.Mathematics.math.RotationOrder order = global::Unity.Mathematics.math.RotationOrder.ZXY)
		{
			return Euler(global::Unity.Mathematics.math.float3(x, y, z), order);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 RotateX(float angle)
		{
			global::Unity.Mathematics.math.sincos(angle, out var s, out var c);
			return global::Unity.Mathematics.math.float3x3(1f, 0f, 0f, 0f, c, 0f - s, 0f, s, c);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 RotateY(float angle)
		{
			global::Unity.Mathematics.math.sincos(angle, out var s, out var c);
			return global::Unity.Mathematics.math.float3x3(c, 0f, s, 0f, 1f, 0f, 0f - s, 0f, c);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 RotateZ(float angle)
		{
			global::Unity.Mathematics.math.sincos(angle, out var s, out var c);
			return global::Unity.Mathematics.math.float3x3(c, 0f - s, 0f, s, c, 0f, 0f, 0f, 1f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 Scale(float s)
		{
			return global::Unity.Mathematics.math.float3x3(s, 0f, 0f, 0f, s, 0f, 0f, 0f, s);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 Scale(float x, float y, float z)
		{
			return global::Unity.Mathematics.math.float3x3(x, 0f, 0f, 0f, y, 0f, 0f, 0f, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 Scale(global::Unity.Mathematics.float3 v)
		{
			return Scale(v.x, v.y, v.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 LookRotation(global::Unity.Mathematics.float3 forward, global::Unity.Mathematics.float3 up)
		{
			global::Unity.Mathematics.float3 y = global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.cross(up, forward));
			return global::Unity.Mathematics.math.float3x3(y, global::Unity.Mathematics.math.cross(forward, y), forward);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 LookRotationSafe(global::Unity.Mathematics.float3 forward, global::Unity.Mathematics.float3 up)
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
			return global::Unity.Mathematics.math.float3x3(global::Unity.Mathematics.math.select(global::Unity.Mathematics.math.float3(1f, 0f, 0f), float5, test), global::Unity.Mathematics.math.select(global::Unity.Mathematics.math.float3(0f, 1f, 0f), global::Unity.Mathematics.math.cross(forward, float5), test), global::Unity.Mathematics.math.select(global::Unity.Mathematics.math.float3(0f, 0f, 1f), forward, test));
		}

		public static explicit operator global::Unity.Mathematics.float3x3(global::Unity.Mathematics.float4x4 f4x4)
		{
			return new global::Unity.Mathematics.float3x3(f4x4);
		}
	}
}
