namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct uint4x2 : global::System.IEquatable<global::Unity.Mathematics.uint4x2>, global::System.IFormattable
	{
		public global::Unity.Mathematics.uint4 c0;

		public global::Unity.Mathematics.uint4 c1;

		public static readonly global::Unity.Mathematics.uint4x2 zero;

		public unsafe ref global::Unity.Mathematics.uint4 this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.uint4x2* ptr = &this)
				{
					return ref *(global::Unity.Mathematics.uint4*)((byte*)ptr + (nint)index * (nint)sizeof(global::Unity.Mathematics.uint4));
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint4x2(global::Unity.Mathematics.uint4 c0, global::Unity.Mathematics.uint4 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint4x2(uint m00, uint m01, uint m10, uint m11, uint m20, uint m21, uint m30, uint m31)
		{
			c0 = new global::Unity.Mathematics.uint4(m00, m10, m20, m30);
			c1 = new global::Unity.Mathematics.uint4(m01, m11, m21, m31);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint4x2(uint v)
		{
			c0 = v;
			c1 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint4x2(bool v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.uint4(0u), new global::Unity.Mathematics.uint4(1u), v);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.uint4(0u), new global::Unity.Mathematics.uint4(1u), v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint4x2(global::Unity.Mathematics.bool4x2 v)
		{
			c0 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.uint4(0u), new global::Unity.Mathematics.uint4(1u), v.c0);
			c1 = global::Unity.Mathematics.math.select(new global::Unity.Mathematics.uint4(0u), new global::Unity.Mathematics.uint4(1u), v.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint4x2(int v)
		{
			c0 = (global::Unity.Mathematics.uint4)v;
			c1 = (global::Unity.Mathematics.uint4)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint4x2(global::Unity.Mathematics.int4x2 v)
		{
			c0 = (global::Unity.Mathematics.uint4)v.c0;
			c1 = (global::Unity.Mathematics.uint4)v.c1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint4x2(float v)
		{
			c0 = (global::Unity.Mathematics.uint4)v;
			c1 = (global::Unity.Mathematics.uint4)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint4x2(global::Unity.Mathematics.float4x2 v)
		{
			c0 = (global::Unity.Mathematics.uint4)v.c0;
			c1 = (global::Unity.Mathematics.uint4)v.c1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint4x2(double v)
		{
			c0 = (global::Unity.Mathematics.uint4)v;
			c1 = (global::Unity.Mathematics.uint4)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint4x2(global::Unity.Mathematics.double4x2 v)
		{
			c0 = (global::Unity.Mathematics.uint4)v.c0;
			c1 = (global::Unity.Mathematics.uint4)v.c1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.uint4x2(uint v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint4x2(bool v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint4x2(global::Unity.Mathematics.bool4x2 v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint4x2(int v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint4x2(global::Unity.Mathematics.int4x2 v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint4x2(float v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint4x2(global::Unity.Mathematics.float4x2 v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint4x2(double v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint4x2(global::Unity.Mathematics.double4x2 v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator *(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator *(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator *(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator +(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator +(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator +(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator -(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator -(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator -(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator /(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator /(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator /(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator %(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator %(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator %(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator ++(global::Unity.Mathematics.uint4x2 val)
		{
			return new global::Unity.Mathematics.uint4x2(++val.c0, ++val.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator --(global::Unity.Mathematics.uint4x2 val)
		{
			return new global::Unity.Mathematics.uint4x2(--val.c0, --val.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator <(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator <(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator <(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator <=(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator <=(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator <=(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator >(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator >(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator >(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator >=(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator >=(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator >=(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator -(global::Unity.Mathematics.uint4x2 val)
		{
			return new global::Unity.Mathematics.uint4x2(-val.c0, -val.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator +(global::Unity.Mathematics.uint4x2 val)
		{
			return new global::Unity.Mathematics.uint4x2(+val.c0, +val.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator <<(global::Unity.Mathematics.uint4x2 x, int n)
		{
			return new global::Unity.Mathematics.uint4x2(x.c0 << n, x.c1 << n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator >>(global::Unity.Mathematics.uint4x2 x, int n)
		{
			return new global::Unity.Mathematics.uint4x2(x.c0 >> n, x.c1 >> n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator ==(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator ==(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator ==(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator !=(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator !=(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 operator !=(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.bool4x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator ~(global::Unity.Mathematics.uint4x2 val)
		{
			return new global::Unity.Mathematics.uint4x2(~val.c0, ~val.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator &(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator &(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator &(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator |(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator |(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator |(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator ^(global::Unity.Mathematics.uint4x2 lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator ^(global::Unity.Mathematics.uint4x2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 operator ^(uint lhs, global::Unity.Mathematics.uint4x2 rhs)
		{
			return new global::Unity.Mathematics.uint4x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.uint4x2 rhs)
		{
			if (c0.Equals(rhs.c0))
			{
				return c1.Equals(rhs.c1);
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.uint4x2 rhs)
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
			return $"uint4x2({c0.x}, {c1.x},  {c0.y}, {c1.y},  {c0.z}, {c1.z},  {c0.w}, {c1.w})";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"uint4x2({c0.x.ToString(format, formatProvider)}, {c1.x.ToString(format, formatProvider)},  {c0.y.ToString(format, formatProvider)}, {c1.y.ToString(format, formatProvider)},  {c0.z.ToString(format, formatProvider)}, {c1.z.ToString(format, formatProvider)},  {c0.w.ToString(format, formatProvider)}, {c1.w.ToString(format, formatProvider)})";
		}
	}
}
