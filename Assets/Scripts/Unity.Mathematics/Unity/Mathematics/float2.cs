namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Mathematics.float2.DebuggerProxy))]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct float2 : global::System.IEquatable<global::Unity.Mathematics.float2>, global::System.IFormattable
	{
		internal sealed class DebuggerProxy
		{
			public float x;

			public float y;

			public DebuggerProxy(global::Unity.Mathematics.float2 v)
			{
				x = v.x;
				y = v.y;
			}
		}

		public float x;

		public float y;

		public static readonly global::Unity.Mathematics.float2 zero;

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 xxxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(x, x, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 xxxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(x, x, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 xxyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(x, x, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 xxyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(x, x, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 xyxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(x, y, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 xyxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(x, y, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 xyyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(x, y, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 xyyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(x, y, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 yxxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(y, x, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 yxxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(y, x, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 yxyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(y, x, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 yxyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(y, x, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 yyxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(y, y, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 yyxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(y, y, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 yyyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(y, y, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float4 yyyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float4(y, y, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float3 xxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float3(x, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float3 xxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float3(x, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float3 xyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float3(x, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float3 xyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float3(x, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float3 yxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float3(y, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float3 yxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float3(y, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float3 yyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float3(y, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float3 yyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float3(y, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float2 xx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float2(x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float2 xy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float2(x, y);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				x = value.x;
				y = value.y;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float2 yx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float2(y, x);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				y = value.x;
				x = value.y;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.float2 yy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.float2(y, y);
			}
		}

		public unsafe float this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.float2* ptr = &this)
				{
					return ((float*)ptr)[index];
				}
			}
			set
			{
				fixed (float* ptr = &x)
				{
					ptr[index] = value;
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(global::Unity.Mathematics.float2 xy)
		{
			x = xy.x;
			y = xy.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(float v)
		{
			x = v;
			y = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(bool v)
		{
			x = (v ? 1f : 0f);
			y = (v ? 1f : 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(global::Unity.Mathematics.bool2 v)
		{
			x = (v.x ? 1f : 0f);
			y = (v.y ? 1f : 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(int v)
		{
			x = v;
			y = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(global::Unity.Mathematics.int2 v)
		{
			x = v.x;
			y = v.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(uint v)
		{
			x = v;
			y = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(global::Unity.Mathematics.uint2 v)
		{
			x = v.x;
			y = v.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(global::Unity.Mathematics.half v)
		{
			x = v;
			y = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(global::Unity.Mathematics.half2 v)
		{
			x = v.x;
			y = v.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(double v)
		{
			x = (float)v;
			y = (float)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float2(global::Unity.Mathematics.double2 v)
		{
			x = (float)v.x;
			y = (float)v.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2(float v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float2(bool v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float2(global::Unity.Mathematics.bool2 v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2(int v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2(global::Unity.Mathematics.int2 v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2(uint v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2(global::Unity.Mathematics.uint2 v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2(global::Unity.Mathematics.half v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float2(global::Unity.Mathematics.half2 v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float2(double v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.float2(global::Unity.Mathematics.double2 v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator *(global::Unity.Mathematics.float2 lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.float2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator *(global::Unity.Mathematics.float2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float2(lhs.x * rhs, lhs.y * rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator *(float lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.float2(lhs * rhs.x, lhs * rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator +(global::Unity.Mathematics.float2 lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.float2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator +(global::Unity.Mathematics.float2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float2(lhs.x + rhs, lhs.y + rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator +(float lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.float2(lhs + rhs.x, lhs + rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator -(global::Unity.Mathematics.float2 lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.float2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator -(global::Unity.Mathematics.float2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float2(lhs.x - rhs, lhs.y - rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator -(float lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.float2(lhs - rhs.x, lhs - rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator /(global::Unity.Mathematics.float2 lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.float2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator /(global::Unity.Mathematics.float2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float2(lhs.x / rhs, lhs.y / rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator /(float lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.float2(lhs / rhs.x, lhs / rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator %(global::Unity.Mathematics.float2 lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.float2(lhs.x % rhs.x, lhs.y % rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator %(global::Unity.Mathematics.float2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.float2(lhs.x % rhs, lhs.y % rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator %(float lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.float2(lhs % rhs.x, lhs % rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator ++(global::Unity.Mathematics.float2 val)
		{
			return new global::Unity.Mathematics.float2(val.x += 1f, val.y += 1f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator --(global::Unity.Mathematics.float2 val)
		{
			return new global::Unity.Mathematics.float2(val.x -= 1f, val.y -= 1f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <(global::Unity.Mathematics.float2 lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x < rhs.x, lhs.y < rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <(global::Unity.Mathematics.float2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x < rhs, lhs.y < rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <(float lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs < rhs.x, lhs < rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <=(global::Unity.Mathematics.float2 lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x <= rhs.x, lhs.y <= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <=(global::Unity.Mathematics.float2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x <= rhs, lhs.y <= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator <=(float lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs <= rhs.x, lhs <= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >(global::Unity.Mathematics.float2 lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x > rhs.x, lhs.y > rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >(global::Unity.Mathematics.float2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x > rhs, lhs.y > rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >(float lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs > rhs.x, lhs > rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >=(global::Unity.Mathematics.float2 lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x >= rhs.x, lhs.y >= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >=(global::Unity.Mathematics.float2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x >= rhs, lhs.y >= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator >=(float lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs >= rhs.x, lhs >= rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator -(global::Unity.Mathematics.float2 val)
		{
			return new global::Unity.Mathematics.float2(0f - val.x, 0f - val.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 operator +(global::Unity.Mathematics.float2 val)
		{
			return new global::Unity.Mathematics.float2(val.x, val.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator ==(global::Unity.Mathematics.float2 lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator ==(global::Unity.Mathematics.float2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x == rhs, lhs.y == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator ==(float lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs == rhs.x, lhs == rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator !=(global::Unity.Mathematics.float2 lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator !=(global::Unity.Mathematics.float2 lhs, float rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs.x != rhs, lhs.y != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 operator !=(float lhs, global::Unity.Mathematics.float2 rhs)
		{
			return new global::Unity.Mathematics.bool2(lhs != rhs.x, lhs != rhs.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.float2 rhs)
		{
			if (x == rhs.x)
			{
				return y == rhs.y;
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.float2 rhs)
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
			return $"float2({x}f, {y}f)";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"float2({x.ToString(format, formatProvider)}f, {y.ToString(format, formatProvider)}f)";
		}

		public static implicit operator global::UnityEngine.Vector2(global::Unity.Mathematics.float2 v)
		{
			return new global::UnityEngine.Vector2(v.x, v.y);
		}

		public static implicit operator global::Unity.Mathematics.float2(global::UnityEngine.Vector2 v)
		{
			return new global::Unity.Mathematics.float2(v.x, v.y);
		}
	}
}
