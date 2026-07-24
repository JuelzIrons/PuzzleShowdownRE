namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Mathematics.uint2.DebuggerProxy))]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct uint2 : global::System.IEquatable<global::Unity.Mathematics.uint2>, global::System.IFormattable
	{
		internal sealed class DebuggerProxy
		{
			public uint x;

			public uint y;

			public DebuggerProxy(global::Unity.Mathematics.uint2 v)
			{
				x = v.x;
				y = v.y;
			}
		}

		public uint x;

		public uint y;

		public static readonly global::Unity.Mathematics.uint2 zero;

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xxxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, x, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xxxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, x, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xxyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, x, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xxyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, x, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xyxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, y, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xyxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, y, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xyyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, y, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xyyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, y, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yxxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, x, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yxxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, x, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yxyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, x, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yxyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, x, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yyxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, y, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yyxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, y, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yyyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, y, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yyyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, y, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 xxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(x, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 xxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(x, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 xyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(x, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 xyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(x, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 yxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(y, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 yxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(y, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 yyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(y, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 yyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(y, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint2 xx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint2(x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint2 xy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint2(x, y);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				x = value.x;
				y = value.y;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint2 yx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint2(y, x);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				y = value.x;
				x = value.y;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint2 yy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint2(y, y);
			}
		}

		public unsafe uint this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.uint2* ptr = &this)
				{
					return ((uint*)ptr)[index];
				}
			}
			set
			{
				fixed (uint* ptr = &x)
				{
					ptr[index] = value;
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint2(uint x, uint y)
		{
			this.x = x;
			this.y = y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint2(global::Unity.Mathematics.uint2 xy)
		{
			x = xy.x;
			y = xy.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint2(uint v)
		{
			x = v;
			y = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint2(bool v)
		{
			x = (v ? 1u : 0u);
			y = (v ? 1u : 0u);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint2(global::Unity.Mathematics.bool2 v)
		{
			x = (v.x ? 1u : 0u);
			y = (v.y ? 1u : 0u);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint2(int v)
		{
			x = (uint)v;
			y = (uint)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint2(global::Unity.Mathematics.int2 v)
		{
			x = (uint)v.x;
			y = (uint)v.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint2(float v)
		{
			x = (uint)v;
			y = (uint)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint2(global::Unity.Mathematics.float2 v)
		{
			x = (uint)v.x;
			y = (uint)v.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint2(double v)
		{
			x = (uint)v;
			y = (uint)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint2(global::Unity.Mathematics.double2 v)
		{
			x = (uint)v.x;
			y = (uint)v.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.uint2(uint v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint2(bool v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint2(global::Unity.Mathematics.bool2 v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint2(int v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint2(global::Unity.Mathematics.int2 v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint2(float v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint2(global::Unity.Mathematics.float2 v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint2(double v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint2(global::Unity.Mathematics.double2 v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator *(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator *(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x * rhs, lhs.y * rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator *(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs * rhs.x, lhs * rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator +(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator +(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x + rhs, lhs.y + rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator +(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs + rhs.x, lhs + rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator -(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator -(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x - rhs, lhs.y - rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator -(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs - rhs.x, lhs - rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator /(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator /(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x / rhs, lhs.y / rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator /(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs / rhs.x, lhs / rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator %(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x % rhs.x, lhs.y % rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator %(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x % rhs, lhs.y % rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator %(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs % rhs.x, lhs % rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator ++(global::Unity.Mathematics.uint2 val)
		{
			return new global::Unity.Mathematics.uint2(++val.x, ++val.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator --(global::Unity.Mathematics.uint2 val)
		{
			return new global::Unity.Mathematics.uint2(--val.x, --val.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x < rhs.x, lhs.y < rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x < rhs, lhs.y < rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs < rhs.x, lhs < rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <=(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x <= rhs.x, lhs.y <= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <=(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x <= rhs, lhs.y <= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <=(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs <= rhs.x, lhs <= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x > rhs.x, lhs.y > rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x > rhs, lhs.y > rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs > rhs.x, lhs > rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >=(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x >= rhs.x, lhs.y >= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >=(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x >= rhs, lhs.y >= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >=(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs >= rhs.x, lhs >= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator -(global::Unity.Mathematics.uint2 val)
		{
			return new global::Unity.Mathematics.uint2((uint)(0uL - (ulong)val.x), (uint)(0uL - (ulong)val.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator +(global::Unity.Mathematics.uint2 val)
		{
			return new global::Unity.Mathematics.uint2(val.x, val.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator <<(global::Unity.Mathematics.uint2 x, int n)
		{
			return new global::Unity.Mathematics.uint2(x.x << n, x.y << n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator >>(global::Unity.Mathematics.uint2 x, int n)
		{
			return new global::Unity.Mathematics.uint2(x.x >> n, x.y >> n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator ==(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator ==(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x == rhs, lhs.y == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator ==(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs == rhs.x, lhs == rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator !=(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator !=(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x != rhs, lhs.y != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator !=(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs != rhs.x, lhs != rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator ~(global::Unity.Mathematics.uint2 val)
		{
			return new global::Unity.Mathematics.uint2(~val.x, ~val.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator &(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x & rhs.x, lhs.y & rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator &(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x & rhs, lhs.y & rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator &(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs & rhs.x, lhs & rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator |(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x | rhs.x, lhs.y | rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator |(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x | rhs, lhs.y | rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator |(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs | rhs.x, lhs | rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator ^(global::Unity.Mathematics.uint2 lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x ^ rhs.x, lhs.y ^ rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator ^(global::Unity.Mathematics.uint2 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs.x ^ rhs, lhs.y ^ rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 operator ^(uint lhs, global::Unity.Mathematics.uint2 rhs)
		{
			return new global::Unity.Mathematics.uint2(lhs ^ rhs.x, lhs ^ rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.uint2 rhs)
		{
			if (x == rhs.x)
			{
				return y == rhs.y;
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.uint2 rhs)
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
			return $"uint2({x}, {y})";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"uint2({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)})";
		}
	}
}
