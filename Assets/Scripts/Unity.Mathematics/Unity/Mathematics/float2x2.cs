namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct float2x2 : global::System.IEquatable<global::Unity.Mathematics.float2x2>, global::System.IFormattable
	{
		public global::Unity.Mathematics.float2 c0;

		public global::Unity.Mathematics.float2 c1;

		public static readonly global::Unity.Mathematics.float2x2 identity = new global::Unity.Mathematics.float2x2(1f, 0f, 0f, 1f);

		public static readonly global::Unity.Mathematics.float2x2 zero;

		public unsafe ref global::Unity.Mathematics.float2 this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.float2x2* ptr = &this)
				{
					return ref *(global::Unity.Mathematics.float2*)((byte*)ptr + (nint)index * (nint)sizeof(global::Unity.Mathematics.float2));
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2x2(global::Unity.Mathematics.float2 c0, global::Unity.Mathematics.float2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2x2(float m00, float m01, float m10, float m11)
		{
			c0 = new global::Unity.Mathematics.float2(m00, m10);
			c1 = new global::Unity.Mathematics.float2(m01, m11);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2x2(float v)
		{
			c0 = v;
			c1 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2x2(bool v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float2(0f), new global::Unity.Mathematics.float2(1f), v);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float2(0f), new global::Unity.Mathematics.float2(1f), v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2x2(global::Unity.Mathematics.bool2x2 v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float2(0f), new global::Unity.Mathematics.float2(1f), v.c0);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.float2(0f), new global::Unity.Mathematics.float2(1f), v.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2x2(int v)
		{
			c0 = v;
			c1 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2x2(global::Unity.Mathematics.int2x2 v)
		{
			c0 = v.c0;
			c1 = v.c1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2x2(uint v)
		{
			c0 = v;
			c1 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2x2(global::Unity.Mathematics.uint2x2 v)
		{
			c0 = v.c0;
			c1 = v.c1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2x2(double v)
		{
			c0 = (global::Unity.Mathematics.float2)v;
			c1 = (global::Unity.Mathematics.float2)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2x2(global::Unity.Mathematics.double2x2 v)
		{
			c0 = (global::Unity.Mathematics.float2)v.c0;
			c1 = (global::Unity.Mathematics.float2)v.c1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2x2(float v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float2x2(bool v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float2x2(global::Unity.Mathematics.bool2x2 v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2x2(int v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2x2(global::Unity.Mathematics.int2x2 v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2x2(uint v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2x2(global::Unity.Mathematics.uint2x2 v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float2x2(double v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float2x2(global::Unity.Mathematics.double2x2 v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator *(global::Unity.Mathematics.float2x2 lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator *(global::Unity.Mathematics.float2x2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator *(float lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator +(global::Unity.Mathematics.float2x2 lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator +(global::Unity.Mathematics.float2x2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator +(float lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator -(global::Unity.Mathematics.float2x2 lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator -(global::Unity.Mathematics.float2x2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator -(float lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator /(global::Unity.Mathematics.float2x2 lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator /(global::Unity.Mathematics.float2x2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator /(float lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator %(global::Unity.Mathematics.float2x2 lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator %(global::Unity.Mathematics.float2x2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator %(float lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.float2x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator ++(global::Unity.Mathematics.float2x2 val)
		{
			return new global::Unity.Mathematics.float2x2(++val.c0, ++val.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator --(global::Unity.Mathematics.float2x2 val)
		{
			return new global::Unity.Mathematics.float2x2(--val.c0, --val.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator <(global::Unity.Mathematics.float2x2 lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator <(global::Unity.Mathematics.float2x2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator <(float lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator <=(global::Unity.Mathematics.float2x2 lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator <=(global::Unity.Mathematics.float2x2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator <=(float lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator >(global::Unity.Mathematics.float2x2 lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator >(global::Unity.Mathematics.float2x2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator >(float lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator >=(global::Unity.Mathematics.float2x2 lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator >=(global::Unity.Mathematics.float2x2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator >=(float lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator -(global::Unity.Mathematics.float2x2 val)
		{
			return new global::Unity.Mathematics.float2x2(-val.c0, -val.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 operator +(global::Unity.Mathematics.float2x2 val)
		{
			return new global::Unity.Mathematics.float2x2(+val.c0, +val.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator ==(global::Unity.Mathematics.float2x2 lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator ==(global::Unity.Mathematics.float2x2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator ==(float lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator !=(global::Unity.Mathematics.float2x2 lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator !=(global::Unity.Mathematics.float2x2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator !=(float lhs, global::Unity.Mathematics.float2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.float2x2 rhs)
		{
			if (c0.Equals(rhs.c0))
			{
				return c1.Equals(rhs.c1);
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.float2x2 rhs)
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
			return $"float2x2({c0.x}f, {c1.x}f,  {c0.y}f, {c1.y}f)";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"float2x2({c0.x.ToString(format, formatProvider)}f, {c1.x.ToString(format, formatProvider)}f,  {c0.y.ToString(format, formatProvider)}f, {c1.y.ToString(format, formatProvider)}f)";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 Rotate(float angle)
		{
			global::Unity.Mathematics.math.sincos(angle, out var s, out var c);
			return global::Unity.Mathematics.math.float2x2(c, 0f - s, s, c);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 Scale(float s)
		{
			return global::Unity.Mathematics.math.float2x2(s, 0f, 0f, s);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 Scale(float x, float y)
		{
			return global::Unity.Mathematics.math.float2x2(x, 0f, 0f, y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 Scale(global::Unity.Mathematics.float2 v)
		{
			return Scale(v.x, v.y);
		}
	}
}
