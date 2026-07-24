namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Mathematics.uint3.DebuggerProxy))]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct uint3 : global::System.IEquatable<global::Unity.Mathematics.uint3>, global::System.IFormattable
	{
		internal sealed class DebuggerProxy
		{
			public uint x;

			public uint y;

			public uint z;

			public DebuggerProxy(global::Unity.Mathematics.uint3 v)
			{
				x = v.x;
				y = v.y;
				z = v.z;
			}
		}

		public uint x;

		public uint y;

		public uint z;

		public static readonly global::Unity.Mathematics.uint3 zero;

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
		public global::Unity.Mathematics.uint4 xxxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, x, x, z);
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
		public global::Unity.Mathematics.uint4 xxyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, x, y, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xxzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, x, z, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xxzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, x, z, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xxzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, x, z, z);
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
		public global::Unity.Mathematics.uint4 xyxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, y, x, z);
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
		public global::Unity.Mathematics.uint4 xyyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, y, y, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xyzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, y, z, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xyzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, y, z, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xyzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, y, z, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xzxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, z, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xzxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, z, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xzxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, z, x, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xzyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, z, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xzyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, z, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xzyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, z, y, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xzzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, z, z, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xzzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, z, z, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 xzzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(x, z, z, z);
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
		public global::Unity.Mathematics.uint4 yxxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, x, x, z);
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
		public global::Unity.Mathematics.uint4 yxyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, x, y, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yxzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, x, z, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yxzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, x, z, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yxzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, x, z, z);
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
		public global::Unity.Mathematics.uint4 yyxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, y, x, z);
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
		public global::Unity.Mathematics.uint4 yyyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, y, y, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yyzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, y, z, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yyzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, y, z, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yyzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, y, z, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yzxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, z, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yzxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, z, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yzxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, z, x, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yzyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, z, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yzyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, z, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yzyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, z, y, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yzzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, z, z, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yzzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, z, z, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 yzzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(y, z, z, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zxxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, x, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zxxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, x, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zxxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, x, x, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zxyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, x, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zxyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, x, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zxyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, x, y, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zxzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, x, z, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zxzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, x, z, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zxzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, x, z, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zyxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, y, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zyxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, y, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zyxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, y, x, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zyyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, y, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zyyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, y, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zyyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, y, y, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zyzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, y, z, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zyzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, y, z, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zyzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, y, z, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zzxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, z, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zzxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, z, x, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zzxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, z, x, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zzyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, z, y, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zzyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, z, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zzyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, z, y, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zzzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, z, z, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zzzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, z, z, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint4 zzzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint4(z, z, z, z);
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
		public global::Unity.Mathematics.uint3 xxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(x, x, z);
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
		public global::Unity.Mathematics.uint3 xyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(x, y, z);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				x = value.x;
				y = value.y;
				z = value.z;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 xzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(x, z, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 xzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(x, z, y);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				x = value.x;
				z = value.y;
				y = value.z;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 xzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(x, z, z);
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
		public global::Unity.Mathematics.uint3 yxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(y, x, z);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				y = value.x;
				x = value.y;
				z = value.z;
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
		public global::Unity.Mathematics.uint3 yyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(y, y, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 yzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(y, z, x);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				y = value.x;
				z = value.y;
				x = value.z;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 yzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(y, z, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 yzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(y, z, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 zxx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(z, x, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 zxy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(z, x, y);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				z = value.x;
				x = value.y;
				y = value.z;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 zxz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(z, x, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 zyx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(z, y, x);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				z = value.x;
				y = value.y;
				x = value.z;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 zyy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(z, y, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 zyz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(z, y, z);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 zzx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(z, z, x);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 zzy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(z, z, y);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint3 zzz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint3(z, z, z);
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
		public global::Unity.Mathematics.uint2 xz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint2(x, z);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				x = value.x;
				z = value.y;
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

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint2 yz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint2(y, z);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				y = value.x;
				z = value.y;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint2 zx
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint2(z, x);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				z = value.x;
				x = value.y;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint2 zy
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint2(z, y);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				z = value.x;
				y = value.y;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public global::Unity.Mathematics.uint2 zz
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return new global::Unity.Mathematics.uint2(z, z);
			}
		}

		public unsafe uint this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.uint3* ptr = &this)
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
		public uint3(uint x, uint y, uint z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(uint x, global::Unity.Mathematics.uint2 yz)
		{
			this.x = x;
			y = yz.x;
			z = yz.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(global::Unity.Mathematics.uint2 xy, uint z)
		{
			x = xy.x;
			y = xy.y;
			this.z = z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(global::Unity.Mathematics.uint3 xyz)
		{
			x = xyz.x;
			y = xyz.y;
			z = xyz.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(uint v)
		{
			x = v;
			y = v;
			z = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(bool v)
		{
			x = (v ? 1u : 0u);
			y = (v ? 1u : 0u);
			z = (v ? 1u : 0u);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(global::Unity.Mathematics.bool3 v)
		{
			x = (v.x ? 1u : 0u);
			y = (v.y ? 1u : 0u);
			z = (v.z ? 1u : 0u);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(int v)
		{
			x = (uint)v;
			y = (uint)v;
			z = (uint)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(global::Unity.Mathematics.int3 v)
		{
			x = (uint)v.x;
			y = (uint)v.y;
			z = (uint)v.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(float v)
		{
			x = (uint)v;
			y = (uint)v;
			z = (uint)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(global::Unity.Mathematics.float3 v)
		{
			x = (uint)v.x;
			y = (uint)v.y;
			z = (uint)v.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(double v)
		{
			x = (uint)v;
			y = (uint)v;
			z = (uint)v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public uint3(global::Unity.Mathematics.double3 v)
		{
			x = (uint)v.x;
			y = (uint)v.y;
			z = (uint)v.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.uint3(uint v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint3(bool v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint3(global::Unity.Mathematics.bool3 v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint3(int v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint3(global::Unity.Mathematics.int3 v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint3(float v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint3(global::Unity.Mathematics.float3 v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint3(double v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.uint3(global::Unity.Mathematics.double3 v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator *(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator *(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator *(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator +(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator +(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator +(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator -(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator -(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator -(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator /(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator /(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator /(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator %(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator %(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator %(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator ++(global::Unity.Mathematics.uint3 val)
		{
			return new global::Unity.Mathematics.uint3(++val.x, ++val.y, ++val.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator --(global::Unity.Mathematics.uint3 val)
		{
			return new global::Unity.Mathematics.uint3(--val.x, --val.y, --val.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator <(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator <(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator <(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator <=(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator <=(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator <=(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator >(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator >(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator >(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator >=(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator >=(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator >=(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator -(global::Unity.Mathematics.uint3 val)
		{
			return new global::Unity.Mathematics.uint3((uint)(0uL - (ulong)val.x), (uint)(0uL - (ulong)val.y), (uint)(0uL - (ulong)val.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator +(global::Unity.Mathematics.uint3 val)
		{
			return new global::Unity.Mathematics.uint3(val.x, val.y, val.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator <<(global::Unity.Mathematics.uint3 x, int n)
		{
			return new global::Unity.Mathematics.uint3(x.x << n, x.y << n, x.z << n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator >>(global::Unity.Mathematics.uint3 x, int n)
		{
			return new global::Unity.Mathematics.uint3(x.x >> n, x.y >> n, x.z >> n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator ==(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator ==(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator ==(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator !=(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator !=(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 operator !=(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator ~(global::Unity.Mathematics.uint3 val)
		{
			return new global::Unity.Mathematics.uint3(~val.x, ~val.y, ~val.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator &(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator &(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x & rhs, lhs.y & rhs, lhs.z & rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator &(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator |(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator |(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x | rhs, lhs.y | rhs, lhs.z | rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator |(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator ^(global::Unity.Mathematics.uint3 lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator ^(global::Unity.Mathematics.uint3 lhs, uint rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 operator ^(uint lhs, global::Unity.Mathematics.uint3 rhs)
		{
			return new global::Unity.Mathematics.uint3(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.uint3 rhs)
		{
			if (x == rhs.x && y == rhs.y)
			{
				return z == rhs.z;
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.uint3 rhs)
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
			return $"uint3({x}, {y}, {z})";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"uint3({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)}, {z.ToString(format, formatProvider)})";
		}
	}
}
