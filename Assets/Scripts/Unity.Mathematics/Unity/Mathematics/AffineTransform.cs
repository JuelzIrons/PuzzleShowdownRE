namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct AffineTransform : global::System.IEquatable<global::Unity.Mathematics.AffineTransform>, global::System.IFormattable
	{
		public global::Unity.Mathematics.float3x3 rs;

		public global::Unity.Mathematics.float3 t;

		public static readonly global::Unity.Mathematics.AffineTransform identity = new global::Unity.Mathematics.AffineTransform(global::Unity.Mathematics.float3.zero, global::Unity.Mathematics.float3x3.identity);

		public static readonly global::Unity.Mathematics.AffineTransform zero;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public AffineTransform(global::Unity.Mathematics.float3 translation, global::Unity.Mathematics.quaternion rotation)
		{
			rs = global::Unity.Mathematics.math.float3x3(rotation);
			t = translation;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public AffineTransform(global::Unity.Mathematics.float3 translation, global::Unity.Mathematics.quaternion rotation, global::Unity.Mathematics.float3 scale)
		{
			rs = global::Unity.Mathematics.math.mulScale(global::Unity.Mathematics.math.float3x3(rotation), scale);
			t = translation;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public AffineTransform(global::Unity.Mathematics.float3 translation, global::Unity.Mathematics.float3x3 rotationScale)
		{
			rs = rotationScale;
			t = translation;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public AffineTransform(global::Unity.Mathematics.float3x3 rotationScale)
		{
			rs = rotationScale;
			t = global::Unity.Mathematics.float3.zero;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public AffineTransform(global::Unity.Mathematics.RigidTransform rigid)
		{
			rs = global::Unity.Mathematics.math.float3x3(rigid.rot);
			t = rigid.pos;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public AffineTransform(global::Unity.Mathematics.float3x4 m)
		{
			rs = global::Unity.Mathematics.math.float3x3(m.c0, m.c1, m.c2);
			t = m.c3;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public AffineTransform(global::Unity.Mathematics.float4x4 m)
		{
			rs = global::Unity.Mathematics.math.float3x3(m.c0.xyz, m.c1.xyz, m.c2.xyz);
			t = m.c3.xyz;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float3x4(global::Unity.Mathematics.AffineTransform m)
		{
			return global::Unity.Mathematics.math.float3x4(m.rs.c0, m.rs.c1, m.rs.c2, m.t);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float4x4(global::Unity.Mathematics.AffineTransform m)
		{
			return global::Unity.Mathematics.math.float4x4(global::Unity.Mathematics.math.float4(m.rs.c0, 0f), global::Unity.Mathematics.math.float4(m.rs.c1, 0f), global::Unity.Mathematics.math.float4(m.rs.c2, 0f), global::Unity.Mathematics.math.float4(m.t, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.AffineTransform rhs)
		{
			if (rs.Equals(rhs.rs))
			{
				return t.Equals(rhs.t);
			}
			return false;
		}

		public override bool Equals(object o)
		{
			if (o is global::Unity.Mathematics.AffineTransform rhs)
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
			return $"AffineTransform(({rs.c0.x}f, {rs.c1.x}f, {rs.c2.x}f,  {rs.c0.y}f, {rs.c1.y}f, {rs.c2.y}f,  {rs.c0.z}f, {rs.c1.z}f, {rs.c2.z}f), ({t.x}f, {t.y}f, {t.z}f))";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"AffineTransform(({rs.c0.x.ToString(format, formatProvider)}f, {rs.c1.x.ToString(format, formatProvider)}f, {rs.c2.x.ToString(format, formatProvider)}f,  {rs.c0.y.ToString(format, formatProvider)}f, {rs.c1.y.ToString(format, formatProvider)}f, {rs.c2.y.ToString(format, formatProvider)}f,  {rs.c0.z.ToString(format, formatProvider)}f, {rs.c1.z.ToString(format, formatProvider)}f, {rs.c2.z.ToString(format, formatProvider)}f), ({t.x.ToString(format, formatProvider)}f, {t.y.ToString(format, formatProvider)}f, {t.z.ToString(format, formatProvider)}f))";
		}
	}
}
