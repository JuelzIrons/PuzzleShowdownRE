namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct float4x3 : global::System.IEquatable<global::Unity.Mathematics.float4x3>, global::System.IFormattable
	{
		public global::Unity.Mathematics.float4 c0;

		public global::Unity.Mathematics.float4 c1;

		public global::Unity.Mathematics.float4 c2;

		public static readonly global::Unity.Mathematics.float4x3 zero;

		public unsafe ref global::Unity.Mathematics.float4 this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.float4x3* ptr = &this)
				{
					return ref *(global::Unity.Mathematics.float4*)((byte*)ptr + (nint)index * (nint)sizeof(global::Unity.Mathematics.float4));
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float4x3(global::Unity.Mathematics.float4 c0, global::Unity.Mathematics.float4 c1, global::Unity.Mathematics.float4 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float4x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22, float m30, float m31, float m32)
		{
			c0 = new global::Unity.Mathematics.float4(m00, m10, m20, m30);
			c1 = new global::Unity.Mathematics.float4(m01, m11, m21, m31);
			c2 = new global::Unity.Mathematics.float4(m02, m12, m22, m32);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float4x3(float v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float4x3(bool v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float4(0f), new global::Unity.Mathematics.float4(1f), v);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float4(0f), new global::Unity.Mathematics.float4(1f), v);
			c2 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float4(0f), new global::Unity.Mathematics.float4(1f), v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float4x3(global::Unity.Mathematics.bool4x3 v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float4(0f), new global::Unity.Mathematics.float4(1f), v.c0);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float4(0f), new global::Unity.Mathematics.float4(1f), v.c1);
			c2 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float4(0f), new global::Unity.Mathematics.float4(1f), v.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float4x3(int v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float4x3(global::Unity.Mathematics.int4x3 v)
		{
			c0 = v.c0;
			c1 = v.c1;
			c2 = v.c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float4x3(uint v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float4x3(global::Unity.Mathematics.uint4x3 v)
		{
			c0 = v.c0;
			c1 = v.c1;
			c2 = v.c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float4x3(double v)
		{
			c0 = (global::Unity.Mathematics.float4)v;
			c1 = (global::Unity.Mathematics.float4)v;
			c2 = (global::Unity.Mathematics.float4)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float4x3(global::Unity.Mathematics.double4x3 v)
		{
			c0 = (global::Unity.Mathematics.float4)v.c0;
			c1 = (global::Unity.Mathematics.float4)v.c1;
			c2 = (global::Unity.Mathematics.float4)v.c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float4x3(float v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float4x3(bool v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float4x3(global::Unity.Mathematics.bool4x3 v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float4x3(int v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float4x3(global::Unity.Mathematics.int4x3 v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float4x3(uint v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float4x3(global::Unity.Mathematics.uint4x3 v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float4x3(double v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float4x3(global::Unity.Mathematics.double4x3 v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator *(global::Unity.Mathematics.float4x3 lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator *(global::Unity.Mathematics.float4x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator *(float lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator +(global::Unity.Mathematics.float4x3 lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator +(global::Unity.Mathematics.float4x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator +(float lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator -(global::Unity.Mathematics.float4x3 lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator -(global::Unity.Mathematics.float4x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator -(float lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator /(global::Unity.Mathematics.float4x3 lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator /(global::Unity.Mathematics.float4x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator /(float lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator %(global::Unity.Mathematics.float4x3 lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator %(global::Unity.Mathematics.float4x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator %(float lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.float4x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator ++(global::Unity.Mathematics.float4x3 val)
		{
			return new global::Unity.Mathematics.float4x3(++val.c0, ++val.c1, ++val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator --(global::Unity.Mathematics.float4x3 val)
		{
			return new global::Unity.Mathematics.float4x3(--val.c0, --val.c1, --val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator <(global::Unity.Mathematics.float4x3 lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator <(global::Unity.Mathematics.float4x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator <(float lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator <=(global::Unity.Mathematics.float4x3 lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator <=(global::Unity.Mathematics.float4x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator <=(float lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator >(global::Unity.Mathematics.float4x3 lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator >(global::Unity.Mathematics.float4x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator >(float lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator >=(global::Unity.Mathematics.float4x3 lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator >=(global::Unity.Mathematics.float4x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator >=(float lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator -(global::Unity.Mathematics.float4x3 val)
		{
			return new global::Unity.Mathematics.float4x3(-val.c0, -val.c1, -val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 operator +(global::Unity.Mathematics.float4x3 val)
		{
			return new global::Unity.Mathematics.float4x3(+val.c0, +val.c1, +val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator ==(global::Unity.Mathematics.float4x3 lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator ==(global::Unity.Mathematics.float4x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator ==(float lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator !=(global::Unity.Mathematics.float4x3 lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator !=(global::Unity.Mathematics.float4x3 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 operator !=(float lhs, global::Unity.Mathematics.float4x3 rhs)
		{
			return new global::Unity.Mathematics.bool4x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.float4x3 rhs)
		{
			if (c0.Equals(rhs.c0) && c1.Equals(rhs.c1))
			{
				return c2.Equals(rhs.c2);
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.float4x3 rhs)
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
			return $"float4x3({c0.x}f, {c1.x}f, {c2.x}f,  {c0.y}f, {c1.y}f, {c2.y}f,  {c0.z}f, {c1.z}f, {c2.z}f,  {c0.w}f, {c1.w}f, {c2.w}f)";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"float4x3({c0.x.ToString(format, formatProvider)}f, {c1.x.ToString(format, formatProvider)}f, {c2.x.ToString(format, formatProvider)}f,  {c0.y.ToString(format, formatProvider)}f, {c1.y.ToString(format, formatProvider)}f, {c2.y.ToString(format, formatProvider)}f,  {c0.z.ToString(format, formatProvider)}f, {c1.z.ToString(format, formatProvider)}f, {c2.z.ToString(format, formatProvider)}f,  {c0.w.ToString(format, formatProvider)}f, {c1.w.ToString(format, formatProvider)}f, {c2.w.ToString(format, formatProvider)}f)";
		}
	}
}
