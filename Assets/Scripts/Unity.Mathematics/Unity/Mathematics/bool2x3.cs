namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct bool2x3 : global::System.IEquatable<global::Unity.Mathematics.bool2x3>
	{
		public global::Unity.Mathematics.bool2 c0;

		public global::Unity.Mathematics.bool2 c1;

		public global::Unity.Mathematics.bool2 c2;

		public unsafe ref global::Unity.Mathematics.bool2 this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.bool2x3* ptr = &this)
				{
					return ref *(global::Unity.Mathematics.bool2*)((byte*)ptr + (nint)index * (nint)sizeof(global::Unity.Mathematics.bool2));
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool2x3(global::Unity.Mathematics.bool2 c0, global::Unity.Mathematics.bool2 c1, global::Unity.Mathematics.bool2 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool2x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12)
		{
			c0 = new global::Unity.Mathematics.bool2(m00, m10);
			c1 = new global::Unity.Mathematics.bool2(m01, m11);
			c2 = new global::Unity.Mathematics.bool2(m02, m12);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool2x3(bool v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.bool2x3(bool v)
		{
			return new global::Unity.Mathematics.bool2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator ==(global::Unity.Mathematics.bool2x3 lhs, global::Unity.Mathematics.bool2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator ==(global::Unity.Mathematics.bool2x3 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator ==(bool lhs, global::Unity.Mathematics.bool2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator !=(global::Unity.Mathematics.bool2x3 lhs, global::Unity.Mathematics.bool2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator !=(global::Unity.Mathematics.bool2x3 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator !=(bool lhs, global::Unity.Mathematics.bool2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator !(global::Unity.Mathematics.bool2x3 val)
		{
			return new global::Unity.Mathematics.bool2x3(!val.c0, !val.c1, !val.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator &(global::Unity.Mathematics.bool2x3 lhs, global::Unity.Mathematics.bool2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator &(global::Unity.Mathematics.bool2x3 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator &(bool lhs, global::Unity.Mathematics.bool2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator |(global::Unity.Mathematics.bool2x3 lhs, global::Unity.Mathematics.bool2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator |(global::Unity.Mathematics.bool2x3 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator |(bool lhs, global::Unity.Mathematics.bool2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator ^(global::Unity.Mathematics.bool2x3 lhs, global::Unity.Mathematics.bool2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator ^(global::Unity.Mathematics.bool2x3 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 operator ^(bool lhs, global::Unity.Mathematics.bool2x3 rhs)
		{
			return new global::Unity.Mathematics.bool2x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.bool2x3 rhs)
		{
			if (c0.Equals(rhs.c0) && c1.Equals(rhs.c1))
			{
				return c2.Equals(rhs.c2);
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.bool2x3 rhs)
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
			return $"bool2x3({c0.x}, {c1.x}, {c2.x},  {c0.y}, {c1.y}, {c2.y})";
		}
	}
}
