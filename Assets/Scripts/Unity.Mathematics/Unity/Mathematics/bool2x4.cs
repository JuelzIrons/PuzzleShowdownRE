namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct bool2x4 : global::System.IEquatable<global::Unity.Mathematics.bool2x4>
	{
		public global::Unity.Mathematics.bool2 c0;

		public global::Unity.Mathematics.bool2 c1;

		public global::Unity.Mathematics.bool2 c2;

		public global::Unity.Mathematics.bool2 c3;

		public unsafe ref global::Unity.Mathematics.bool2 this[int index]
		{
			get
			{
				fixed (global::Unity.Mathematics.bool2x4* ptr = &this)
				{
					return ref *(global::Unity.Mathematics.bool2*)((byte*)ptr + (nint)index * (nint)sizeof(global::Unity.Mathematics.bool2));
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool2x4(global::Unity.Mathematics.bool2 c0, global::Unity.Mathematics.bool2 c1, global::Unity.Mathematics.bool2 c2, global::Unity.Mathematics.bool2 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool2x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13)
		{
			c0 = new global::Unity.Mathematics.bool2(m00, m10);
			c1 = new global::Unity.Mathematics.bool2(m01, m11);
			c2 = new global::Unity.Mathematics.bool2(m02, m12);
			c3 = new global::Unity.Mathematics.bool2(m03, m13);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool2x4(bool v)
		{
			c0 = v;
			c1 = v;
			c2 = v;
			c3 = v;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.bool2x4(bool v)
		{
			return new global::Unity.Mathematics.bool2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator ==(global::Unity.Mathematics.bool2x4 lhs, global::Unity.Mathematics.bool2x4 rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator ==(global::Unity.Mathematics.bool2x4 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator ==(bool lhs, global::Unity.Mathematics.bool2x4 rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator !=(global::Unity.Mathematics.bool2x4 lhs, global::Unity.Mathematics.bool2x4 rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator !=(global::Unity.Mathematics.bool2x4 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator !=(bool lhs, global::Unity.Mathematics.bool2x4 rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator !(global::Unity.Mathematics.bool2x4 val)
		{
			return new global::Unity.Mathematics.bool2x4(!val.c0, !val.c1, !val.c2, !val.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator &(global::Unity.Mathematics.bool2x4 lhs, global::Unity.Mathematics.bool2x4 rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator &(global::Unity.Mathematics.bool2x4 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator &(bool lhs, global::Unity.Mathematics.bool2x4 rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator |(global::Unity.Mathematics.bool2x4 lhs, global::Unity.Mathematics.bool2x4 rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator |(global::Unity.Mathematics.bool2x4 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator |(bool lhs, global::Unity.Mathematics.bool2x4 rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator ^(global::Unity.Mathematics.bool2x4 lhs, global::Unity.Mathematics.bool2x4 rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator ^(global::Unity.Mathematics.bool2x4 lhs, bool rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 operator ^(bool lhs, global::Unity.Mathematics.bool2x4 rhs)
		{
			return new global::Unity.Mathematics.bool2x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.bool2x4 rhs)
		{
			if (c0.Equals(rhs.c0) && c1.Equals(rhs.c1) && c2.Equals(rhs.c2))
			{
				return c3.Equals(rhs.c3);
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.bool2x4 rhs)
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
			return $"bool2x4({c0.x}, {c1.x}, {c2.x}, {c3.x},  {c0.y}, {c1.y}, {c2.y}, {c3.y})";
		}
	}
}
