namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct half : global::System.IEquatable<global::Unity.Mathematics.half>, global::System.IFormattable
	{
		public ushort value;

		public static readonly global::Unity.Mathematics.half zero;

		public static float MaxValue => 65504f;

		public static float MinValue => -65504f;

		public static global::Unity.Mathematics.half MaxValueAsHalf => new global::Unity.Mathematics.half(MaxValue);

		public static global::Unity.Mathematics.half MinValueAsHalf => new global::Unity.Mathematics.half(MinValue);

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public half(global::Unity.Mathematics.half x)
		{
			value = x.value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public half(float v)
		{
			value = (ushort)global::Unity.Mathematics.math.f32tof16(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public half(double v)
		{
			value = (ushort)global::Unity.Mathematics.math.f32tof16((float)v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.half(float v)
		{
			return new global::Unity.Mathematics.half(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static explicit operator global::Unity.Mathematics.half(double v)
		{
			return new global::Unity.Mathematics.half(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator float(global::Unity.Mathematics.half d)
		{
			return global::Unity.Mathematics.math.f16tof32(d.value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator double(global::Unity.Mathematics.half d)
		{
			return global::Unity.Mathematics.math.f16tof32(d.value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(global::Unity.Mathematics.half lhs, global::Unity.Mathematics.half rhs)
		{
			return lhs.value == rhs.value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(global::Unity.Mathematics.half lhs, global::Unity.Mathematics.half rhs)
		{
			return lhs.value != rhs.value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.half rhs)
		{
			return value == rhs.value;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.half rhs)
			{
				return Equals(rhs);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return global::Unity.Mathematics.math.f16tof32(value).ToString();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return global::Unity.Mathematics.math.f16tof32(value).ToString(format, formatProvider);
		}
	}
}
