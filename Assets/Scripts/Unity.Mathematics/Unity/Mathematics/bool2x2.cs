namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct bool2x2 : global::System.IEquatable<global::Unity.Mathematics.bool2x2>
	{
		public global::Unity.Mathematics.bool2 c0;

		public global::Unity.Mathematics.bool2 c1;

		public unsafe ref global::Unity.Mathematics.bool2 this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.bool2x2* ptr = &this)
				{
					return ref *(global::Unity.Mathematics.bool2*)((byte*)ptr + (nint)index * (nint)sizeof(global::Unity.Mathematics.bool2));
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool2x2(global::Unity.Mathematics.bool2 c0, global::Unity.Mathematics.bool2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool2x2(bool m00, bool m01, bool m10, bool m11)
		{
			c0 = new global::Unity.Mathematics.bool2(m00, m10);
			c1 = new global::Unity.Mathematics.bool2(m01, m11);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool2x2(bool v)
		{
			c0 = v;
			c1 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.bool2x2(bool v)
		{
			return new global::Unity.Mathematics.bool2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator ==(global::Unity.Mathematics.bool2x2 lhs, global::Unity.Mathematics.bool2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator ==(global::Unity.Mathematics.bool2x2 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator ==(bool lhs, global::Unity.Mathematics.bool2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator !=(global::Unity.Mathematics.bool2x2 lhs, global::Unity.Mathematics.bool2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator !=(global::Unity.Mathematics.bool2x2 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator !=(bool lhs, global::Unity.Mathematics.bool2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator !(global::Unity.Mathematics.bool2x2 val)
		{
			return new global::Unity.Mathematics.bool2x2(!val.c0, !val.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator &(global::Unity.Mathematics.bool2x2 lhs, global::Unity.Mathematics.bool2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator &(global::Unity.Mathematics.bool2x2 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator &(bool lhs, global::Unity.Mathematics.bool2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator |(global::Unity.Mathematics.bool2x2 lhs, global::Unity.Mathematics.bool2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator |(global::Unity.Mathematics.bool2x2 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator |(bool lhs, global::Unity.Mathematics.bool2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator ^(global::Unity.Mathematics.bool2x2 lhs, global::Unity.Mathematics.bool2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator ^(global::Unity.Mathematics.bool2x2 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 operator ^(bool lhs, global::Unity.Mathematics.bool2x2 rhs)
		{
			return new global::Unity.Mathematics.bool2x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.bool2x2 rhs)
		{
			if (c0.Equals(rhs.c0))
			{
				return c1.Equals(rhs.c1);
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.bool2x2 rhs)
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
			return $"bool2x2({c0.x}, {c1.x},  {c0.y}, {c1.y})";
		}
	}
}
