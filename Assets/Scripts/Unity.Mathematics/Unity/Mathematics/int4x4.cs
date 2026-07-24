namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct int4x4 : global::System.IEquatable<global::Unity.Mathematics.int4x4>, global::System.IFormattable
	{
		public global::Unity.Mathematics.int4 c0;

		public global::Unity.Mathematics.int4 c1;

		public global::Unity.Mathematics.int4 c2;

		public global::Unity.Mathematics.int4 c3;

		public static readonly global::Unity.Mathematics.int4x4 identity = new global::Unity.Mathematics.int4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

		public static readonly global::Unity.Mathematics.int4x4 zero;

		public unsafe ref global::Unity.Mathematics.int4 this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.int4x4* ptr = &this)
				{
					return ref *(global::Unity.Mathematics.int4*)((byte*)ptr + (nint)index * (nint)sizeof(global::Unity.Mathematics.int4));
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int4x4(global::Unity.Mathematics.int4 c0, global::Unity.Mathematics.int4 c1, global::Unity.Mathematics.int4 c2, global::Unity.Mathematics.int4 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int4x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13, int m20, int m21, int m22, int m23, int m30, int m31, int m32, int m33)
		{
			c0 = new global::Unity.Mathematics.int4(m00, m10, m20, m30);
			c1 = new global::Unity.Mathematics.int4(m01, m11, m21, m31);
			c2 = new global::Unity.Mathematics.int4(m02, m12, m22, m32);
			c3 = new global::Unity.Mathematics.int4(m03, m13, m23, m33);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int4x4(int v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
			c3 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int4x4(bool v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.int4(0), new global::Unity.Mathematics.int4(1), v);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.int4(0), new global::Unity.Mathematics.int4(1), v);
			c2 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.int4(0), new global::Unity.Mathematics.int4(1), v);
			c3 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.int4(0), new global::Unity.Mathematics.int4(1), v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int4x4(global::Unity.Mathematics.bool4x4 v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.int4(0), new global::Unity.Mathematics.int4(1), v.c0);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.int4(0), new global::Unity.Mathematics.int4(1), v.c1);
			c2 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.int4(0), new global::Unity.Mathematics.int4(1), v.c2);
			c3 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.int4(0), new global::Unity.Mathematics.int4(1), v.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int4x4(uint v)
		{
			c0 = (global::Unity.Mathematics.int4)v;
			c1 = (global::Unity.Mathematics.int4)v;
			c2 = (global::Unity.Mathematics.int4)v;
			c3 = (global::Unity.Mathematics.int4)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int4x4(global::Unity.Mathematics.uint4x4 v)
		{
			c0 = (global::Unity.Mathematics.int4)v.c0;
			c1 = (global::Unity.Mathematics.int4)v.c1;
			c2 = (global::Unity.Mathematics.int4)v.c2;
			c3 = (global::Unity.Mathematics.int4)v.c3;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int4x4(float v)
		{
			c0 = (global::Unity.Mathematics.int4)v;
			c1 = (global::Unity.Mathematics.int4)v;
			c2 = (global::Unity.Mathematics.int4)v;
			c3 = (global::Unity.Mathematics.int4)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int4x4(global::Unity.Mathematics.float4x4 v)
		{
			c0 = (global::Unity.Mathematics.int4)v.c0;
			c1 = (global::Unity.Mathematics.int4)v.c1;
			c2 = (global::Unity.Mathematics.int4)v.c2;
			c3 = (global::Unity.Mathematics.int4)v.c3;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int4x4(double v)
		{
			c0 = (global::Unity.Mathematics.int4)v;
			c1 = (global::Unity.Mathematics.int4)v;
			c2 = (global::Unity.Mathematics.int4)v;
			c3 = (global::Unity.Mathematics.int4)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int4x4(global::Unity.Mathematics.double4x4 v)
		{
			c0 = (global::Unity.Mathematics.int4)v.c0;
			c1 = (global::Unity.Mathematics.int4)v.c1;
			c2 = (global::Unity.Mathematics.int4)v.c2;
			c3 = (global::Unity.Mathematics.int4)v.c3;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.int4x4(int v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int4x4(bool v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int4x4(global::Unity.Mathematics.bool4x4 v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int4x4(uint v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int4x4(global::Unity.Mathematics.uint4x4 v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int4x4(float v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int4x4(global::Unity.Mathematics.float4x4 v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int4x4(double v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int4x4(global::Unity.Mathematics.double4x4 v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator *(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator *(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator *(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator +(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator +(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator +(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator -(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator -(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator -(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator /(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator /(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator /(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator %(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator %(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator %(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator ++(global::Unity.Mathematics.int4x4 val)
		{
			return new global::Unity.Mathematics.int4x4(++val.c0, ++val.c1, ++val.c2, ++val.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator --(global::Unity.Mathematics.int4x4 val)
		{
			return new global::Unity.Mathematics.int4x4(--val.c0, --val.c1, --val.c2, --val.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator <(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator <(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator <(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator <=(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator <=(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator <=(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator >(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator >(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator >(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator >=(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator >=(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator >=(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator -(global::Unity.Mathematics.int4x4 val)
		{
			return new global::Unity.Mathematics.int4x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator +(global::Unity.Mathematics.int4x4 val)
		{
			return new global::Unity.Mathematics.int4x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator <<(global::Unity.Mathematics.int4x4 x, int n)
		{
			return new global::Unity.Mathematics.int4x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator >>(global::Unity.Mathematics.int4x4 x, int n)
		{
			return new global::Unity.Mathematics.int4x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator ==(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator ==(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator ==(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator !=(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator !=(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 operator !=(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.bool4x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator ~(global::Unity.Mathematics.int4x4 val)
		{
			return new global::Unity.Mathematics.int4x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator &(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator &(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator &(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator |(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator |(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator |(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator ^(global::Unity.Mathematics.int4x4 lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator ^(global::Unity.Mathematics.int4x4 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 operator ^(int lhs, global::Unity.Mathematics.int4x4 rhs)
		{
			return new global::Unity.Mathematics.int4x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.int4x4 rhs)
		{
			if (c0.Equals(rhs.c0) && c1.Equals(rhs.c1) && c2.Equals(rhs.c2))
			{
				return c3.Equals(rhs.c3);
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.int4x4 rhs)
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
			return $"int4x4({c0.x}, {c1.x}, {c2.x}, {c3.x},  {c0.y}, {c1.y}, {c2.y}, {c3.y},  {c0.z}, {c1.z}, {c2.z}, {c3.z},  {c0.w}, {c1.w}, {c2.w}, {c3.w})";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"int4x4({c0.x.ToString(format, formatProvider)}, {c1.x.ToString(format, formatProvider)}, {c2.x.ToString(format, formatProvider)}, {c3.x.ToString(format, formatProvider)},  {c0.y.ToString(format, formatProvider)}, {c1.y.ToString(format, formatProvider)}, {c2.y.ToString(format, formatProvider)}, {c3.y.ToString(format, formatProvider)},  {c0.z.ToString(format, formatProvider)}, {c1.z.ToString(format, formatProvider)}, {c2.z.ToString(format, formatProvider)}, {c3.z.ToString(format, formatProvider)},  {c0.w.ToString(format, formatProvider)}, {c1.w.ToString(format, formatProvider)}, {c2.w.ToString(format, formatProvider)}, {c3.w.ToString(format, formatProvider)})";
		}
	}
}
