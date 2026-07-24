namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct double2x3 : global::System.IEquatable<global::Unity.Mathematics.double2x3>, global::System.IFormattable
	{
		public global::Unity.Mathematics.double2 c0;

		public global::Unity.Mathematics.double2 c1;

		public global::Unity.Mathematics.double2 c2;

		public static readonly global::Unity.Mathematics.double2x3 zero;

		public unsafe ref global::Unity.Mathematics.double2 this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.double2x3* ptr = &this)
				{
					return ref *(global::Unity.Mathematics.double2*)((byte*)ptr + (nint)index * (nint)sizeof(global::Unity.Mathematics.double2));
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double2x3(global::Unity.Mathematics.double2 c0, global::Unity.Mathematics.double2 c1, global::Unity.Mathematics.double2 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double2x3(double m00, double m01, double m02, double m10, double m11, double m12)
		{
			c0 = new global::Unity.Mathematics.double2(m00, m10);
			c1 = new global::Unity.Mathematics.double2(m01, m11);
			c2 = new global::Unity.Mathematics.double2(m02, m12);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double2x3(double v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double2x3(bool v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.double2(0.0), new global::Unity.Mathematics.double2(1.0), v);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.double2(0.0), new global::Unity.Mathematics.double2(1.0), v);
			c2 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.double2(0.0), new global::Unity.Mathematics.double2(1.0), v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double2x3(global::Unity.Mathematics.bool2x3 v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.double2(0.0), new global::Unity.Mathematics.double2(1.0), v.c0);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.double2(0.0), new global::Unity.Mathematics.double2(1.0), v.c1);
			c2 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.double2(0.0), new global::Unity.Mathematics.double2(1.0), v.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double2x3(int v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double2x3(global::Unity.Mathematics.int2x3 v)
		{
			c0 = v.c0;
			c1 = v.c1;
			c2 = v.c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double2x3(uint v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double2x3(global::Unity.Mathematics.uint2x3 v)
		{
			c0 = v.c0;
			c1 = v.c1;
			c2 = v.c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double2x3(float v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public double2x3(global::Unity.Mathematics.float2x3 v)
		{
			c0 = v.c0;
			c1 = v.c1;
			c2 = v.c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.double2x3(double v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.double2x3(bool v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.double2x3(global::Unity.Mathematics.bool2x3 v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.double2x3(int v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.double2x3(global::Unity.Mathematics.int2x3 v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.double2x3(uint v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.double2x3(global::Unity.Mathematics.uint2x3 v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.double2x3(float v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.double2x3(global::Unity.Mathematics.float2x3 v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator *(global::Unity.Mathematics.double2x3 lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator *(global::Unity.Mathematics.double2x3 lhs, double rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator *(double lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator +(global::Unity.Mathematics.double2x3 lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator +(global::Unity.Mathematics.double2x3 lhs, double rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator +(double lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator -(global::Unity.Mathematics.double2x3 lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator -(global::Unity.Mathematics.double2x3 lhs, double rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator -(double lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator /(global::Unity.Mathematics.double2x3 lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator /(global::Unity.Mathematics.double2x3 lhs, double rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator /(double lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator %(global::Unity.Mathematics.double2x3 lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator %(global::Unity.Mathematics.double2x3 lhs, double rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator %(double lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.double2x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator ++(global::Unity.Mathematics.double2x3 val)
		{
			return new global::Unity.Mathematics.double2x3(++val.c0, ++val.c1, ++val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator --(global::Unity.Mathematics.double2x3 val)
		{
			return new global::Unity.Mathematics.double2x3(--val.c0, --val.c1, --val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator <(global::Unity.Mathematics.double2x3 lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator <(global::Unity.Mathematics.double2x3 lhs, double rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator <(double lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator <=(global::Unity.Mathematics.double2x3 lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator <=(global::Unity.Mathematics.double2x3 lhs, double rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator <=(double lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator >(global::Unity.Mathematics.double2x3 lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator >(global::Unity.Mathematics.double2x3 lhs, double rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator >(double lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator >=(global::Unity.Mathematics.double2x3 lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator >=(global::Unity.Mathematics.double2x3 lhs, double rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator >=(double lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator -(global::Unity.Mathematics.double2x3 val)
		{
			return new global::Unity.Mathematics.double2x3(-val.c0, -val.c1, -val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 operator +(global::Unity.Mathematics.double2x3 val)
		{
			return new global::Unity.Mathematics.double2x3(+val.c0, +val.c1, +val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator ==(global::Unity.Mathematics.double2x3 lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator ==(global::Unity.Mathematics.double2x3 lhs, double rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator ==(double lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator !=(global::Unity.Mathematics.double2x3 lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator !=(global::Unity.Mathematics.double2x3 lhs, double rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator !=(double lhs, global::Unity.Mathematics.double2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.double2x3 rhs)
		{
			if (c0.Equals(rhs.c0) && c1.Equals(rhs.c1))
			{
				return c2.Equals(rhs.c2);
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.double2x3 rhs)
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
			return $"double2x3({c0.x}, {c1.x}, {c2.x},  {c0.y}, {c1.y}, {c2.y})";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"double2x3({c0.x.ToString(format, formatProvider)}, {c1.x.ToString(format, formatProvider)}, {c2.x.ToString(format, formatProvider)},  {c0.y.ToString(format, formatProvider)}, {c1.y.ToString(format, formatProvider)}, {c2.y.ToString(format, formatProvider)})";
		}
	}
}
