namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Mathematics.int2.DebuggerProxy))]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct int2 : global::System.IEquatable<global::Unity.Mathematics.int2>, global::System.IFormattable
	{
		internal sealed class DebuggerProxy
		{
			public int x;

			public int y;

			public DebuggerProxy(global::Unity.Mathematics.int2 v)
			{
				x = v.x;
				y = v.y;
			}
		}

		public int x;

		public int y;

		public static readonly global::Unity.Mathematics.int2 zero;

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 xxxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(x, x, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 xxxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(x, x, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 xxyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(x, x, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 xxyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(x, x, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 xyxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(x, y, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 xyxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(x, y, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 xyyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(x, y, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 xyyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(x, y, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 yxxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(y, x, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 yxxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(y, x, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 yxyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(y, x, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 yxyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(y, x, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 yyxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(y, y, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 yyxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(y, y, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 yyyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(y, y, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int4 yyyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int4(y, y, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int3 xxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int3(x, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int3 xxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int3(x, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int3 xyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int3(x, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int3 xyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int3(x, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int3 yxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int3(y, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int3 yxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int3(y, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int3 yyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int3(y, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int3 yyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int3(y, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int2 xx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int2(x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int2 xy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int2(x, y);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				x = value.x;
				y = value.y;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int2 yx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int2(y, x);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				y = value.x;
				x = value.y;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.int2 yy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.int2(y, y);
			}
		}

		public unsafe int this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.int2* ptr = &this)
				{
					return ((int*)ptr)[index];
				}
			}
			set
			{
				fixed (int* ptr = &x)
				{
					ptr[index] = value;
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int2(global::Unity.Mathematics.int2 xy)
		{
			x = xy.x;
			y = xy.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int2(int v)
		{
			x = v;
			y = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int2(bool v)
		{
			x = (v ? 1 : 0);
			y = (v ? 1 : 0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int2(global::Unity.Mathematics.bool2 v)
		{
			x = (v.x ? 1 : 0);
			y = (v.y ? 1 : 0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int2(uint v)
		{
			x = (int)v;
			y = (int)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int2(global::Unity.Mathematics.uint2 v)
		{
			x = (int)v.x;
			y = (int)v.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int2(float v)
		{
			x = (int)v;
			y = (int)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int2(global::Unity.Mathematics.float2 v)
		{
			x = (int)v.x;
			y = (int)v.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int2(double v)
		{
			x = (int)v;
			y = (int)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int2(global::Unity.Mathematics.double2 v)
		{
			x = (int)v.x;
			y = (int)v.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.int2(int v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int2(bool v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int2(global::Unity.Mathematics.bool2 v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int2(uint v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int2(global::Unity.Mathematics.uint2 v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int2(float v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int2(global::Unity.Mathematics.float2 v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int2(double v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.int2(global::Unity.Mathematics.double2 v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator *(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator *(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x * rhs, lhs.y * rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator *(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs * rhs.x, lhs * rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator +(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator +(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x + rhs, lhs.y + rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator +(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs + rhs.x, lhs + rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator -(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator -(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x - rhs, lhs.y - rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator -(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs - rhs.x, lhs - rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator /(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator /(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x / rhs, lhs.y / rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator /(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs / rhs.x, lhs / rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator %(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x % rhs.x, lhs.y % rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator %(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x % rhs, lhs.y % rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator %(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs % rhs.x, lhs % rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator ++(global::Unity.Mathematics.int2 val)
		{
			return new global::Unity.Mathematics.int2(++val.x, ++val.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator --(global::Unity.Mathematics.int2 val)
		{
			return new global::Unity.Mathematics.int2(--val.x, --val.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x < rhs.x, lhs.y < rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x < rhs, lhs.y < rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs < rhs.x, lhs < rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <=(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x <= rhs.x, lhs.y <= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <=(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x <= rhs, lhs.y <= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <=(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs <= rhs.x, lhs <= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x > rhs.x, lhs.y > rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x > rhs, lhs.y > rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs > rhs.x, lhs > rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >=(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x >= rhs.x, lhs.y >= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >=(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x >= rhs, lhs.y >= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >=(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs >= rhs.x, lhs >= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator -(global::Unity.Mathematics.int2 val)
		{
			return new global::Unity.Mathematics.int2(-val.x, -val.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator +(global::Unity.Mathematics.int2 val)
		{
			return new global::Unity.Mathematics.int2(val.x, val.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator <<(global::Unity.Mathematics.int2 x, int n)
		{
			return new global::Unity.Mathematics.int2(x.x << n, x.y << n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator >>(global::Unity.Mathematics.int2 x, int n)
		{
			return new global::Unity.Mathematics.int2(x.x >> n, x.y >> n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator ==(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator ==(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x == rhs, lhs.y == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator ==(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs == rhs.x, lhs == rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator !=(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator !=(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x != rhs, lhs.y != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator !=(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs != rhs.x, lhs != rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator ~(global::Unity.Mathematics.int2 val)
		{
			return new global::Unity.Mathematics.int2(~val.x, ~val.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator &(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x & rhs.x, lhs.y & rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator &(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x & rhs, lhs.y & rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator &(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs & rhs.x, lhs & rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator |(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x | rhs.x, lhs.y | rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator |(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x | rhs, lhs.y | rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator |(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs | rhs.x, lhs | rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator ^(global::Unity.Mathematics.int2 lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x ^ rhs.x, lhs.y ^ rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator ^(global::Unity.Mathematics.int2 lhs, int rhs)
		{
			return new global::Unity.Mathematics.int2(lhs.x ^ rhs, lhs.y ^ rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 operator ^(int lhs, global::Unity.Mathematics.int2 rhs)
		{
			return new global::Unity.Mathematics.int2(lhs ^ rhs.x, lhs ^ rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.int2 rhs)
		{
			if (x == rhs.x)
			{
				return y == rhs.y;
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.int2 rhs)
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
			return $"int2({x}, {y})";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"int2({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)})";
		}
	}
}
