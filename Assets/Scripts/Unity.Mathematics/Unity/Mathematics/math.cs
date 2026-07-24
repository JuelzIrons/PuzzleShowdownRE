namespace Unity.Mathematics
{
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public static class math
	{
		public enum RotationOrder : byte
		{
			XYZ = 0,
			XZY = 1,
			YXZ = 2,
			YZX = 3,
			ZXY = 4,
			ZYX = 5,
			Default = 4
		}

		public enum ShuffleComponent : byte
		{
			LeftX = 0,
			LeftY = 1,
			LeftZ = 2,
			LeftW = 3,
			RightX = 4,
			RightY = 5,
			RightZ = 6,
			RightW = 7
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		internal struct LongDoubleUnion
		{
			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public long longValue;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public double doubleValue;
		}

		public const double E_DBL = global::System.Math.E;

		public const double LOG2E_DBL = 1.4426950408889634;

		public const double LOG10E_DBL = 0.4342944819032518;

		public const double LN2_DBL = 0.6931471805599453;

		public const double LN10_DBL = 2.302585092994046;

		public const double PI_DBL = global::System.Math.PI;

		public const double PI2_DBL = global::System.Math.PI * 2.0;

		public const double PIHALF_DBL = global::System.Math.PI / 2.0;

		public const double TAU_DBL = global::System.Math.PI * 2.0;

		public const double TODEGREES_DBL = 180.0 / global::System.Math.PI;

		public const double TORADIANS_DBL = global::System.Math.PI / 180.0;

		public const double SQRT2_DBL = 1.4142135623730951;

		public const double EPSILON_DBL = 2.220446049250313E-16;

		public const double INFINITY_DBL = double.PositiveInfinity;

		public const double NAN_DBL = double.NaN;

		public const float FLT_MIN_NORMAL = 1.1754944E-38f;

		public const double DBL_MIN_NORMAL = 2.2250738585072014E-308;

		public const float E = global::System.MathF.E;

		public const float LOG2E = 1.442695f;

		public const float LOG10E = 0.4342945f;

		public const float LN2 = 0.6931472f;

		public const float LN10 = 2.3025851f;

		public const float PI = global::System.MathF.PI;

		public const float PI2 = global::System.MathF.PI * 2f;

		public const float PIHALF = global::System.MathF.PI / 2f;

		public const float TAU = global::System.MathF.PI * 2f;

		public const float TODEGREES = 57.29578f;

		public const float TORADIANS = global::System.MathF.PI / 180f;

		public const float SQRT2 = 1.4142135f;

		public const float EPSILON = 1.1920929E-07f;

		public const float INFINITY = float.PositiveInfinity;

		public const float NAN = float.NaN;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.AffineTransform AffineTransform(global::Unity.Mathematics.float3 translation, global::Unity.Mathematics.quaternion rotation)
		{
			return new global::Unity.Mathematics.AffineTransform(translation, rotation);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.AffineTransform AffineTransform(global::Unity.Mathematics.float3 translation, global::Unity.Mathematics.quaternion rotation, global::Unity.Mathematics.float3 scale)
		{
			return new global::Unity.Mathematics.AffineTransform(translation, rotation, scale);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.AffineTransform AffineTransform(global::Unity.Mathematics.float3 translation, global::Unity.Mathematics.float3x3 rotationScale)
		{
			return new global::Unity.Mathematics.AffineTransform(translation, rotationScale);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.AffineTransform AffineTransform(global::Unity.Mathematics.float3x3 rotationScale)
		{
			return new global::Unity.Mathematics.AffineTransform(rotationScale);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.AffineTransform AffineTransform(global::Unity.Mathematics.float4x4 m)
		{
			return new global::Unity.Mathematics.AffineTransform(m);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.AffineTransform AffineTransform(global::Unity.Mathematics.float3x4 m)
		{
			return new global::Unity.Mathematics.AffineTransform(m);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.AffineTransform AffineTransform(global::Unity.Mathematics.RigidTransform rigid)
		{
			return new global::Unity.Mathematics.AffineTransform(rigid);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(global::Unity.Mathematics.AffineTransform transform)
		{
			return float4x4(float4(transform.rs.c0, 0f), float4(transform.rs.c1, 0f), float4(transform.rs.c2, 0f), float4(transform.t, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(global::Unity.Mathematics.AffineTransform transform)
		{
			return float3x4(transform.rs.c0, transform.rs.c1, transform.rs.c2, transform.t);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.AffineTransform mul(global::Unity.Mathematics.AffineTransform a, global::Unity.Mathematics.AffineTransform b)
		{
			return new global::Unity.Mathematics.AffineTransform(transform(a, b.t), mul(a.rs, b.rs));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.AffineTransform mul(global::Unity.Mathematics.float3x3 a, global::Unity.Mathematics.AffineTransform b)
		{
			return new global::Unity.Mathematics.AffineTransform(mul(a, b.t), mul(a, b.rs));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.AffineTransform mul(global::Unity.Mathematics.AffineTransform a, global::Unity.Mathematics.float3x3 b)
		{
			return new global::Unity.Mathematics.AffineTransform(a.t, mul(b, a.rs));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 mul(global::Unity.Mathematics.AffineTransform a, global::Unity.Mathematics.float4 pos)
		{
			return float4(mul(a.rs, pos.xyz) + a.t * pos.w, pos.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 rotate(global::Unity.Mathematics.AffineTransform a, global::Unity.Mathematics.float3 dir)
		{
			return mul(a.rs, dir);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 transform(global::Unity.Mathematics.AffineTransform a, global::Unity.Mathematics.float3 pos)
		{
			return a.t + mul(a.rs, pos);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.AffineTransform inverse(global::Unity.Mathematics.AffineTransform a)
		{
			global::Unity.Mathematics.AffineTransform result = default(global::Unity.Mathematics.AffineTransform);
			result.rs = pseudoinverse(a.rs);
			result.t = mul(result.rs, -a.t);
			return result;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void decompose(global::Unity.Mathematics.AffineTransform a, out global::Unity.Mathematics.float3 translation, out global::Unity.Mathematics.quaternion rotation, out global::Unity.Mathematics.float3 scale)
		{
			translation = a.t;
			rotation = global::Unity.Mathematics.math.rotation(a.rs);
			global::Unity.Mathematics.float3x3 float3x5 = mul(float3x3(conjugate(rotation)), a.rs);
			scale = float3(float3x5.c0.x, float3x5.c1.y, float3x5.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.AffineTransform a)
		{
			return hash(a.rs) + (uint)(-976930485 * (int)hash(a.t));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.AffineTransform a)
		{
			return hashwide(a.rs).xyzz + 3318036811u * hashwide(a.t).xyzz;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 bool2(bool x, bool y)
		{
			return new global::Unity.Mathematics.bool2(x, y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 bool2(global::Unity.Mathematics.bool2 xy)
		{
			return new global::Unity.Mathematics.bool2(xy);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 bool2(bool v)
		{
			return new global::Unity.Mathematics.bool2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool2 v)
		{
			return csum(select(uint2(2426570171u, 1561977301u), uint2(4205774813u, 1650214333u), v));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.bool2 v)
		{
			return select(uint2(3388112843u, 1831150513u), uint2(1848374953u, 3430200247u), v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool shuffle(global::Unity.Mathematics.bool2 left, global::Unity.Mathematics.bool2 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 shuffle(global::Unity.Mathematics.bool2 left, global::Unity.Mathematics.bool2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return bool2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 shuffle(global::Unity.Mathematics.bool2 left, global::Unity.Mathematics.bool2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return bool3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 shuffle(global::Unity.Mathematics.bool2 left, global::Unity.Mathematics.bool2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return bool4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static bool select_shuffle_component(global::Unity.Mathematics.bool2 a, global::Unity.Mathematics.bool2 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 bool2x2(global::Unity.Mathematics.bool2 c0, global::Unity.Mathematics.bool2 c1)
		{
			return new global::Unity.Mathematics.bool2x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 bool2x2(bool m00, bool m01, bool m10, bool m11)
		{
			return new global::Unity.Mathematics.bool2x2(m00, m01, m10, m11);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 bool2x2(bool v)
		{
			return new global::Unity.Mathematics.bool2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x2 transpose(global::Unity.Mathematics.bool2x2 v)
		{
			return bool2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool2x2 v)
		{
			return csum(select(uint2(2062756937u, 2920485769u), uint2(1562056283u, 2265541847u), v.c0) + select(uint2(1283419601u, 1210229737u), uint2(2864955997u, 3525118277u), v.c1));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.bool2x2 v)
		{
			return select(uint2(2298260269u, 1632478733u), uint2(1537393931u, 2353355467u), v.c0) + select(uint2(3441847433u, 4052036147u), uint2(2011389559u, 2252224297u), v.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 bool2x3(global::Unity.Mathematics.bool2 c0, global::Unity.Mathematics.bool2 c1, global::Unity.Mathematics.bool2 c2)
		{
			return new global::Unity.Mathematics.bool2x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 bool2x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12)
		{
			return new global::Unity.Mathematics.bool2x3(m00, m01, m02, m10, m11, m12);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 bool2x3(bool v)
		{
			return new global::Unity.Mathematics.bool2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x2 transpose(global::Unity.Mathematics.bool2x3 v)
		{
			return bool3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool2x3 v)
		{
			return csum(select(uint2(2078515003u, 4206465343u), uint2(3025146473u, 3763046909u), v.c0) + select(uint2(3678265601u, 2070747979u), uint2(1480171127u, 1588341193u), v.c1) + select(uint2(4234155257u, 1811310911u), uint2(2635799963u, 4165137857u), v.c2));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.bool2x3 v)
		{
			return select(uint2(2759770933u, 2759319383u), uint2(3299952959u, 3121178323u), v.c0) + select(uint2(2948522579u, 1531026433u), uint2(1365086453u, 3969870067u), v.c1) + select(uint2(4192899797u, 3271228601u), uint2(1634639009u, 3318036811u), v.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 bool2x4(global::Unity.Mathematics.bool2 c0, global::Unity.Mathematics.bool2 c1, global::Unity.Mathematics.bool2 c2, global::Unity.Mathematics.bool2 c3)
		{
			return new global::Unity.Mathematics.bool2x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 bool2x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13)
		{
			return new global::Unity.Mathematics.bool2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 bool2x4(bool v)
		{
			return new global::Unity.Mathematics.bool2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 transpose(global::Unity.Mathematics.bool2x4 v)
		{
			return bool4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool2x4 v)
		{
			return csum(select(uint2(1168253063u, 4228926523u), uint2(1610574617u, 1584185147u), v.c0) + select(uint2(3041325733u, 3150930919u), uint2(3309258581u, 1770373673u), v.c1) + select(uint2(3778261171u, 3286279097u), uint2(4264629071u, 1898591447u), v.c2) + select(uint2(2641864091u, 1229113913u), uint2(3020867117u, 1449055807u), v.c3));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.bool2x4 v)
		{
			return select(uint2(2479033387u, 3702457169u), uint2(1845824257u, 1963973621u), v.c0) + select(uint2(2134758553u, 1391111867u), uint2(1167706003u, 2209736489u), v.c1) + select(uint2(3261535807u, 1740411209u), uint2(2910609089u, 2183822701u), v.c2) + select(uint2(3029516053u, 3547472099u), uint2(2057487037u, 3781937309u), v.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 bool3(bool x, bool y, bool z)
		{
			return new global::Unity.Mathematics.bool3(x, y, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 bool3(bool x, global::Unity.Mathematics.bool2 yz)
		{
			return new global::Unity.Mathematics.bool3(x, yz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 bool3(global::Unity.Mathematics.bool2 xy, bool z)
		{
			return new global::Unity.Mathematics.bool3(xy, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 bool3(global::Unity.Mathematics.bool3 xyz)
		{
			return new global::Unity.Mathematics.bool3(xyz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 bool3(bool v)
		{
			return new global::Unity.Mathematics.bool3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool3 v)
		{
			return csum(select(uint3(2716413241u, 1166264321u, 2503385333u), uint3(2944493077u, 2599999021u, 3814721321u), v));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.bool3 v)
		{
			return select(uint3(1595355149u, 1728931849u, 2062756937u), uint3(2920485769u, 1562056283u, 2265541847u), v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool shuffle(global::Unity.Mathematics.bool3 left, global::Unity.Mathematics.bool3 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 shuffle(global::Unity.Mathematics.bool3 left, global::Unity.Mathematics.bool3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return bool2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 shuffle(global::Unity.Mathematics.bool3 left, global::Unity.Mathematics.bool3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return bool3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 shuffle(global::Unity.Mathematics.bool3 left, global::Unity.Mathematics.bool3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return bool4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static bool select_shuffle_component(global::Unity.Mathematics.bool3 a, global::Unity.Mathematics.bool3 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftZ => a.z, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightZ => b.z, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x2 bool3x2(global::Unity.Mathematics.bool3 c0, global::Unity.Mathematics.bool3 c1)
		{
			return new global::Unity.Mathematics.bool3x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x2 bool3x2(bool m00, bool m01, bool m10, bool m11, bool m20, bool m21)
		{
			return new global::Unity.Mathematics.bool3x2(m00, m01, m10, m11, m20, m21);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x2 bool3x2(bool v)
		{
			return new global::Unity.Mathematics.bool3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x3 transpose(global::Unity.Mathematics.bool3x2 v)
		{
			return bool2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool3x2 v)
		{
			return csum(select(uint3(2627668003u, 1520214331u, 2949502447u), uint3(2827819133u, 3480140317u, 2642994593u), v.c0) + select(uint3(3940484981u, 1954192763u, 1091696537u), uint3(3052428017u, 4253034763u, 2338696631u), v.c1));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.bool3x2 v)
		{
			return select(uint3(3757372771u, 1885959949u, 3508684087u), uint3(3919501043u, 1209161033u, 4007793211u), v.c0) + select(uint3(3819806693u, 3458005183u, 2078515003u), uint3(4206465343u, 3025146473u, 3763046909u), v.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 bool3x3(global::Unity.Mathematics.bool3 c0, global::Unity.Mathematics.bool3 c1, global::Unity.Mathematics.bool3 c2)
		{
			return new global::Unity.Mathematics.bool3x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 bool3x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12, bool m20, bool m21, bool m22)
		{
			return new global::Unity.Mathematics.bool3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 bool3x3(bool v)
		{
			return new global::Unity.Mathematics.bool3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x3 transpose(global::Unity.Mathematics.bool3x3 v)
		{
			return bool3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool3x3 v)
		{
			return csum(select(uint3(3881277847u, 4017968839u, 1727237899u), uint3(1648514723u, 1385344481u, 3538260197u), v.c0) + select(uint3(4066109527u, 2613148903u, 3367528529u), uint3(1678332449u, 2918459647u, 2744611081u), v.c1) + select(uint3(1952372791u, 2631698677u, 4200781601u), uint3(2119021007u, 1760485621u, 3157985881u), v.c2));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.bool3x3 v)
		{
			return select(uint3(2171534173u, 2723054263u, 1168253063u), uint3(4228926523u, 1610574617u, 1584185147u), v.c0) + select(uint3(3041325733u, 3150930919u, 3309258581u), uint3(1770373673u, 3778261171u, 3286279097u), v.c1) + select(uint3(4264629071u, 1898591447u, 2641864091u), uint3(1229113913u, 3020867117u, 1449055807u), v.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x4 bool3x4(global::Unity.Mathematics.bool3 c0, global::Unity.Mathematics.bool3 c1, global::Unity.Mathematics.bool3 c2, global::Unity.Mathematics.bool3 c3)
		{
			return new global::Unity.Mathematics.bool3x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x4 bool3x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13, bool m20, bool m21, bool m22, bool m23)
		{
			return new global::Unity.Mathematics.bool3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x4 bool3x4(bool v)
		{
			return new global::Unity.Mathematics.bool3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 transpose(global::Unity.Mathematics.bool3x4 v)
		{
			return bool4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool3x4 v)
		{
			return csum(select(uint3(2209710647u, 2201894441u, 2849577407u), uint3(3287031191u, 3098675399u, 1564399943u), v.c0) + select(uint3(1148435377u, 3416333663u, 1750611407u), uint3(3285396193u, 3110507567u, 4271396531u), v.c1) + select(uint3(4198118021u, 2908068253u, 3705492289u), uint3(2497566569u, 2716413241u, 1166264321u), v.c2) + select(uint3(2503385333u, 2944493077u, 2599999021u), uint3(3814721321u, 1595355149u, 1728931849u), v.c3));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.bool3x4 v)
		{
			return select(uint3(2062756937u, 2920485769u, 1562056283u), uint3(2265541847u, 1283419601u, 1210229737u), v.c0) + select(uint3(2864955997u, 3525118277u, 2298260269u), uint3(1632478733u, 1537393931u, 2353355467u), v.c1) + select(uint3(3441847433u, 4052036147u, 2011389559u), uint3(2252224297u, 3784421429u, 1750626223u), v.c2) + select(uint3(3571447507u, 3412283213u, 2601761069u), uint3(1254033427u, 2248573027u, 3612677113u), v.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 bool4(bool x, bool y, bool z, bool w)
		{
			return new global::Unity.Mathematics.bool4(x, y, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 bool4(bool x, bool y, global::Unity.Mathematics.bool2 zw)
		{
			return new global::Unity.Mathematics.bool4(x, y, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 bool4(bool x, global::Unity.Mathematics.bool2 yz, bool w)
		{
			return new global::Unity.Mathematics.bool4(x, yz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 bool4(bool x, global::Unity.Mathematics.bool3 yzw)
		{
			return new global::Unity.Mathematics.bool4(x, yzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 bool4(global::Unity.Mathematics.bool2 xy, bool z, bool w)
		{
			return new global::Unity.Mathematics.bool4(xy, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 bool4(global::Unity.Mathematics.bool2 xy, global::Unity.Mathematics.bool2 zw)
		{
			return new global::Unity.Mathematics.bool4(xy, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 bool4(global::Unity.Mathematics.bool3 xyz, bool w)
		{
			return new global::Unity.Mathematics.bool4(xyz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 bool4(global::Unity.Mathematics.bool4 xyzw)
		{
			return new global::Unity.Mathematics.bool4(xyzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 bool4(bool v)
		{
			return new global::Unity.Mathematics.bool4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool4 v)
		{
			return csum(select(uint4(1610574617u, 1584185147u, 3041325733u, 3150930919u), uint4(3309258581u, 1770373673u, 3778261171u, 3286279097u), v));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.bool4 v)
		{
			return select(uint4(4264629071u, 1898591447u, 2641864091u, 1229113913u), uint4(3020867117u, 1449055807u, 2479033387u, 3702457169u), v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool shuffle(global::Unity.Mathematics.bool4 left, global::Unity.Mathematics.bool4 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 shuffle(global::Unity.Mathematics.bool4 left, global::Unity.Mathematics.bool4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return bool2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 shuffle(global::Unity.Mathematics.bool4 left, global::Unity.Mathematics.bool4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return bool3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 shuffle(global::Unity.Mathematics.bool4 left, global::Unity.Mathematics.bool4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return bool4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static bool select_shuffle_component(global::Unity.Mathematics.bool4 a, global::Unity.Mathematics.bool4 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftZ => a.z, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftW => a.w, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightZ => b.z, 
				global::Unity.Mathematics.math.ShuffleComponent.RightW => b.w, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 bool4x2(global::Unity.Mathematics.bool4 c0, global::Unity.Mathematics.bool4 c1)
		{
			return new global::Unity.Mathematics.bool4x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 bool4x2(bool m00, bool m01, bool m10, bool m11, bool m20, bool m21, bool m30, bool m31)
		{
			return new global::Unity.Mathematics.bool4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x2 bool4x2(bool v)
		{
			return new global::Unity.Mathematics.bool4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2x4 transpose(global::Unity.Mathematics.bool4x2 v)
		{
			return bool2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool4x2 v)
		{
			return csum(select(uint4(3516359879u, 3050356579u, 4178586719u, 2558655391u), uint4(1453413133u, 2152428077u, 1938706661u, 1338588197u), v.c0) + select(uint4(3439609253u, 3535343003u, 3546061613u, 2702024231u), uint4(1452124841u, 1966089551u, 2668168249u, 1587512777u), v.c1));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.bool4x2 v)
		{
			return select(uint4(2353831999u, 3101256173u, 2891822459u, 2837054189u), uint4(3016004371u, 4097481403u, 2229788699u, 2382715877u), v.c0) + select(uint4(1851936439u, 1938025801u, 3712598587u, 3956330501u), uint4(2437373431u, 1441286183u, 2426570171u, 1561977301u), v.c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 bool4x3(global::Unity.Mathematics.bool4 c0, global::Unity.Mathematics.bool4 c1, global::Unity.Mathematics.bool4 c2)
		{
			return new global::Unity.Mathematics.bool4x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 bool4x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12, bool m20, bool m21, bool m22, bool m30, bool m31, bool m32)
		{
			return new global::Unity.Mathematics.bool4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x3 bool4x3(bool v)
		{
			return new global::Unity.Mathematics.bool4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3x4 transpose(global::Unity.Mathematics.bool4x3 v)
		{
			return bool3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool4x3 v)
		{
			return csum(select(uint4(3940484981u, 1954192763u, 1091696537u, 3052428017u), uint4(4253034763u, 2338696631u, 3757372771u, 1885959949u), v.c0) + select(uint4(3508684087u, 3919501043u, 1209161033u, 4007793211u), uint4(3819806693u, 3458005183u, 2078515003u, 4206465343u), v.c1) + select(uint4(3025146473u, 3763046909u, 3678265601u, 2070747979u), uint4(1480171127u, 1588341193u, 4234155257u, 1811310911u), v.c2));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.bool4x3 v)
		{
			return select(uint4(2635799963u, 4165137857u, 2759770933u, 2759319383u), uint4(3299952959u, 3121178323u, 2948522579u, 1531026433u), v.c0) + select(uint4(1365086453u, 3969870067u, 4192899797u, 3271228601u), uint4(1634639009u, 3318036811u, 3404170631u, 2048213449u), v.c1) + select(uint4(4164671783u, 1780759499u, 1352369353u, 2446407751u), uint4(1391928079u, 3475533443u, 3777095341u, 3385463369u), v.c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 bool4x4(global::Unity.Mathematics.bool4 c0, global::Unity.Mathematics.bool4 c1, global::Unity.Mathematics.bool4 c2, global::Unity.Mathematics.bool4 c3)
		{
			return new global::Unity.Mathematics.bool4x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 bool4x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13, bool m20, bool m21, bool m22, bool m23, bool m30, bool m31, bool m32, bool m33)
		{
			return new global::Unity.Mathematics.bool4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 bool4x4(bool v)
		{
			return new global::Unity.Mathematics.bool4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4x4 transpose(global::Unity.Mathematics.bool4x4 v)
		{
			return bool4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.bool4x4 v)
		{
			return csum(select(uint4(3516359879u, 3050356579u, 4178586719u, 2558655391u), uint4(1453413133u, 2152428077u, 1938706661u, 1338588197u), v.c0) + select(uint4(3439609253u, 3535343003u, 3546061613u, 2702024231u), uint4(1452124841u, 1966089551u, 2668168249u, 1587512777u), v.c1) + select(uint4(2353831999u, 3101256173u, 2891822459u, 2837054189u), uint4(3016004371u, 4097481403u, 2229788699u, 2382715877u), v.c2) + select(uint4(1851936439u, 1938025801u, 3712598587u, 3956330501u), uint4(2437373431u, 1441286183u, 2426570171u, 1561977301u), v.c3));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.bool4x4 v)
		{
			return select(uint4(4205774813u, 1650214333u, 3388112843u, 1831150513u), uint4(1848374953u, 3430200247u, 2209710647u, 2201894441u), v.c0) + select(uint4(2849577407u, 3287031191u, 3098675399u, 1564399943u), uint4(1148435377u, 3416333663u, 1750611407u, 3285396193u), v.c1) + select(uint4(3110507567u, 4271396531u, 4198118021u, 2908068253u), uint4(3705492289u, 2497566569u, 2716413241u, 1166264321u), v.c2) + select(uint4(2503385333u, 2944493077u, 2599999021u, 3814721321u), uint4(1595355149u, 1728931849u, 2062756937u, 2920485769u), v.c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(double x, double y)
		{
			return new global::Unity.Mathematics.double2(x, y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(global::Unity.Mathematics.double2 xy)
		{
			return new global::Unity.Mathematics.double2(xy);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(double v)
		{
			return new global::Unity.Mathematics.double2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(bool v)
		{
			return new global::Unity.Mathematics.double2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(global::Unity.Mathematics.bool2 v)
		{
			return new global::Unity.Mathematics.double2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(int v)
		{
			return new global::Unity.Mathematics.double2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(global::Unity.Mathematics.int2 v)
		{
			return new global::Unity.Mathematics.double2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(uint v)
		{
			return new global::Unity.Mathematics.double2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(global::Unity.Mathematics.uint2 v)
		{
			return new global::Unity.Mathematics.double2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(global::Unity.Mathematics.half v)
		{
			return new global::Unity.Mathematics.double2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(global::Unity.Mathematics.half2 v)
		{
			return new global::Unity.Mathematics.double2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(float v)
		{
			return new global::Unity.Mathematics.double2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 double2(global::Unity.Mathematics.float2 v)
		{
			return new global::Unity.Mathematics.double2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double2 v)
		{
			return csum(fold_to_uint(v) * uint2(2503385333u, 2944493077u)) + 2599999021u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.double2 v)
		{
			return fold_to_uint(v) * uint2(3814721321u, 1595355149u) + 1728931849u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double shuffle(global::Unity.Mathematics.double2 left, global::Unity.Mathematics.double2 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 shuffle(global::Unity.Mathematics.double2 left, global::Unity.Mathematics.double2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return double2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 shuffle(global::Unity.Mathematics.double2 left, global::Unity.Mathematics.double2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return double3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 shuffle(global::Unity.Mathematics.double2 left, global::Unity.Mathematics.double2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return double4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static double select_shuffle_component(global::Unity.Mathematics.double2 a, global::Unity.Mathematics.double2 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 double2x2(global::Unity.Mathematics.double2 c0, global::Unity.Mathematics.double2 c1)
		{
			return new global::Unity.Mathematics.double2x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 double2x2(double m00, double m01, double m10, double m11)
		{
			return new global::Unity.Mathematics.double2x2(m00, m01, m10, m11);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 double2x2(double v)
		{
			return new global::Unity.Mathematics.double2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 double2x2(bool v)
		{
			return new global::Unity.Mathematics.double2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 double2x2(global::Unity.Mathematics.bool2x2 v)
		{
			return new global::Unity.Mathematics.double2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 double2x2(int v)
		{
			return new global::Unity.Mathematics.double2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 double2x2(global::Unity.Mathematics.int2x2 v)
		{
			return new global::Unity.Mathematics.double2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 double2x2(uint v)
		{
			return new global::Unity.Mathematics.double2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 double2x2(global::Unity.Mathematics.uint2x2 v)
		{
			return new global::Unity.Mathematics.double2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 double2x2(float v)
		{
			return new global::Unity.Mathematics.double2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 double2x2(global::Unity.Mathematics.float2x2 v)
		{
			return new global::Unity.Mathematics.double2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 transpose(global::Unity.Mathematics.double2x2 v)
		{
			return double2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 inverse(global::Unity.Mathematics.double2x2 m)
		{
			double x = m.c0.x;
			double x2 = m.c1.x;
			double y = m.c0.y;
			double y2 = m.c1.y;
			double num = x * y2 - x2 * y;
			return double2x2(y2, 0.0 - x2, 0.0 - y, x) * (1.0 / num);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double determinant(global::Unity.Mathematics.double2x2 m)
		{
			double x = m.c0.x;
			double x2 = m.c1.x;
			double y = m.c0.y;
			double y2 = m.c1.y;
			return x * y2 - x2 * y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double2x2 v)
		{
			return csum(fold_to_uint(v.c0) * uint2(4253034763u, 2338696631u) + fold_to_uint(v.c1) * uint2(3757372771u, 1885959949u)) + 3508684087u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.double2x2 v)
		{
			return fold_to_uint(v.c0) * uint2(3919501043u, 1209161033u) + fold_to_uint(v.c1) * uint2(4007793211u, 3819806693u) + 3458005183u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 double2x3(global::Unity.Mathematics.double2 c0, global::Unity.Mathematics.double2 c1, global::Unity.Mathematics.double2 c2)
		{
			return new global::Unity.Mathematics.double2x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 double2x3(double m00, double m01, double m02, double m10, double m11, double m12)
		{
			return new global::Unity.Mathematics.double2x3(m00, m01, m02, m10, m11, m12);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 double2x3(double v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 double2x3(bool v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 double2x3(global::Unity.Mathematics.bool2x3 v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 double2x3(int v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 double2x3(global::Unity.Mathematics.int2x3 v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 double2x3(uint v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 double2x3(global::Unity.Mathematics.uint2x3 v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 double2x3(float v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 double2x3(global::Unity.Mathematics.float2x3 v)
		{
			return new global::Unity.Mathematics.double2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 transpose(global::Unity.Mathematics.double2x3 v)
		{
			return double3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double2x3 v)
		{
			return csum(fold_to_uint(v.c0) * uint2(4066109527u, 2613148903u) + fold_to_uint(v.c1) * uint2(3367528529u, 1678332449u) + fold_to_uint(v.c2) * uint2(2918459647u, 2744611081u)) + 1952372791;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.double2x3 v)
		{
			return fold_to_uint(v.c0) * uint2(2631698677u, 4200781601u) + fold_to_uint(v.c1) * uint2(2119021007u, 1760485621u) + fold_to_uint(v.c2) * uint2(3157985881u, 2171534173u) + 2723054263u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 double2x4(global::Unity.Mathematics.double2 c0, global::Unity.Mathematics.double2 c1, global::Unity.Mathematics.double2 c2, global::Unity.Mathematics.double2 c3)
		{
			return new global::Unity.Mathematics.double2x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 double2x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13)
		{
			return new global::Unity.Mathematics.double2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 double2x4(double v)
		{
			return new global::Unity.Mathematics.double2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 double2x4(bool v)
		{
			return new global::Unity.Mathematics.double2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 double2x4(global::Unity.Mathematics.bool2x4 v)
		{
			return new global::Unity.Mathematics.double2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 double2x4(int v)
		{
			return new global::Unity.Mathematics.double2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 double2x4(global::Unity.Mathematics.int2x4 v)
		{
			return new global::Unity.Mathematics.double2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 double2x4(uint v)
		{
			return new global::Unity.Mathematics.double2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 double2x4(global::Unity.Mathematics.uint2x4 v)
		{
			return new global::Unity.Mathematics.double2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 double2x4(float v)
		{
			return new global::Unity.Mathematics.double2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 double2x4(global::Unity.Mathematics.float2x4 v)
		{
			return new global::Unity.Mathematics.double2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 transpose(global::Unity.Mathematics.double2x4 v)
		{
			return double4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double2x4 v)
		{
			return csum(fold_to_uint(v.c0) * uint2(2437373431u, 1441286183u) + fold_to_uint(v.c1) * uint2(2426570171u, 1561977301u) + fold_to_uint(v.c2) * uint2(4205774813u, 1650214333u) + fold_to_uint(v.c3) * uint2(3388112843u, 1831150513u)) + 1848374953;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.double2x4 v)
		{
			return fold_to_uint(v.c0) * uint2(3430200247u, 2209710647u) + fold_to_uint(v.c1) * uint2(2201894441u, 2849577407u) + fold_to_uint(v.c2) * uint2(3287031191u, 3098675399u) + fold_to_uint(v.c3) * uint2(1564399943u, 1148435377u) + 3416333663u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(double x, double y, double z)
		{
			return new global::Unity.Mathematics.double3(x, y, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(double x, global::Unity.Mathematics.double2 yz)
		{
			return new global::Unity.Mathematics.double3(x, yz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(global::Unity.Mathematics.double2 xy, double z)
		{
			return new global::Unity.Mathematics.double3(xy, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(global::Unity.Mathematics.double3 xyz)
		{
			return new global::Unity.Mathematics.double3(xyz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(double v)
		{
			return new global::Unity.Mathematics.double3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(bool v)
		{
			return new global::Unity.Mathematics.double3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(global::Unity.Mathematics.bool3 v)
		{
			return new global::Unity.Mathematics.double3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(int v)
		{
			return new global::Unity.Mathematics.double3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(global::Unity.Mathematics.int3 v)
		{
			return new global::Unity.Mathematics.double3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(uint v)
		{
			return new global::Unity.Mathematics.double3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(global::Unity.Mathematics.uint3 v)
		{
			return new global::Unity.Mathematics.double3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(global::Unity.Mathematics.half v)
		{
			return new global::Unity.Mathematics.double3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(global::Unity.Mathematics.half3 v)
		{
			return new global::Unity.Mathematics.double3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(float v)
		{
			return new global::Unity.Mathematics.double3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 double3(global::Unity.Mathematics.float3 v)
		{
			return new global::Unity.Mathematics.double3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double3 v)
		{
			return csum(fold_to_uint(v) * uint3(2937008387u, 3835713223u, 2216526373u)) + 3375971453u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.double3 v)
		{
			return fold_to_uint(v) * uint3(3559829411u, 3652178029u, 2544260129u) + 2013864031u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double shuffle(global::Unity.Mathematics.double3 left, global::Unity.Mathematics.double3 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 shuffle(global::Unity.Mathematics.double3 left, global::Unity.Mathematics.double3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return double2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 shuffle(global::Unity.Mathematics.double3 left, global::Unity.Mathematics.double3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return double3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 shuffle(global::Unity.Mathematics.double3 left, global::Unity.Mathematics.double3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return double4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static double select_shuffle_component(global::Unity.Mathematics.double3 a, global::Unity.Mathematics.double3 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftZ => a.z, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightZ => b.z, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 double3x2(global::Unity.Mathematics.double3 c0, global::Unity.Mathematics.double3 c1)
		{
			return new global::Unity.Mathematics.double3x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 double3x2(double m00, double m01, double m10, double m11, double m20, double m21)
		{
			return new global::Unity.Mathematics.double3x2(m00, m01, m10, m11, m20, m21);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 double3x2(double v)
		{
			return new global::Unity.Mathematics.double3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 double3x2(bool v)
		{
			return new global::Unity.Mathematics.double3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 double3x2(global::Unity.Mathematics.bool3x2 v)
		{
			return new global::Unity.Mathematics.double3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 double3x2(int v)
		{
			return new global::Unity.Mathematics.double3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 double3x2(global::Unity.Mathematics.int3x2 v)
		{
			return new global::Unity.Mathematics.double3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 double3x2(uint v)
		{
			return new global::Unity.Mathematics.double3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 double3x2(global::Unity.Mathematics.uint3x2 v)
		{
			return new global::Unity.Mathematics.double3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 double3x2(float v)
		{
			return new global::Unity.Mathematics.double3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 double3x2(global::Unity.Mathematics.float3x2 v)
		{
			return new global::Unity.Mathematics.double3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 transpose(global::Unity.Mathematics.double3x2 v)
		{
			return double2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double3x2 v)
		{
			return csum(fold_to_uint(v.c0) * uint3(3996716183u, 2626301701u, 1306289417u) + fold_to_uint(v.c1) * uint3(2096137163u, 1548578029u, 4178800919u)) + 3898072289u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.double3x2 v)
		{
			return fold_to_uint(v.c0) * uint3(4129428421u, 2631575897u, 2854656703u) + fold_to_uint(v.c1) * uint3(3578504047u, 4245178297u, 2173281923u) + 2973357649u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 double3x3(global::Unity.Mathematics.double3 c0, global::Unity.Mathematics.double3 c1, global::Unity.Mathematics.double3 c2)
		{
			return new global::Unity.Mathematics.double3x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 double3x3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22)
		{
			return new global::Unity.Mathematics.double3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 double3x3(double v)
		{
			return new global::Unity.Mathematics.double3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 double3x3(bool v)
		{
			return new global::Unity.Mathematics.double3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 double3x3(global::Unity.Mathematics.bool3x3 v)
		{
			return new global::Unity.Mathematics.double3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 double3x3(int v)
		{
			return new global::Unity.Mathematics.double3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 double3x3(global::Unity.Mathematics.int3x3 v)
		{
			return new global::Unity.Mathematics.double3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 double3x3(uint v)
		{
			return new global::Unity.Mathematics.double3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 double3x3(global::Unity.Mathematics.uint3x3 v)
		{
			return new global::Unity.Mathematics.double3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 double3x3(float v)
		{
			return new global::Unity.Mathematics.double3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 double3x3(global::Unity.Mathematics.float3x3 v)
		{
			return new global::Unity.Mathematics.double3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 transpose(global::Unity.Mathematics.double3x3 v)
		{
			return double3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		public static global::Unity.Mathematics.double3x3 inverse(global::Unity.Mathematics.double3x3 m)
		{
			global::Unity.Mathematics.double3 c = m.c0;
			global::Unity.Mathematics.double3 c2 = m.c1;
			global::Unity.Mathematics.double3 c3 = m.c2;
			global::Unity.Mathematics.double3 double5 = double3(c2.x, c3.x, c.x);
			global::Unity.Mathematics.double3 double6 = double3(c2.y, c3.y, c.y);
			global::Unity.Mathematics.double3 double7 = double3(c2.z, c3.z, c.z);
			global::Unity.Mathematics.double3 double8 = double6 * double7.yzx - double6.yzx * double7;
			global::Unity.Mathematics.double3 c4 = double5.yzx * double7 - double5 * double7.yzx;
			global::Unity.Mathematics.double3 c5 = double5 * double6.yzx - double5.yzx * double6;
			double num = 1.0 / csum(double5.zxy * double8);
			return double3x3(double8, c4, c5) * num;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double determinant(global::Unity.Mathematics.double3x3 m)
		{
			global::Unity.Mathematics.double3 c = m.c0;
			global::Unity.Mathematics.double3 c2 = m.c1;
			global::Unity.Mathematics.double3 c3 = m.c2;
			double num = c2.y * c3.z - c2.z * c3.y;
			double num2 = c.y * c3.z - c.z * c3.y;
			double num3 = c.y * c2.z - c.z * c2.y;
			return c.x * num - c2.x * num2 + c3.x * num3;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double3x3 v)
		{
			return csum(fold_to_uint(v.c0) * uint3(2891822459u, 2837054189u, 3016004371u) + fold_to_uint(v.c1) * uint3(4097481403u, 2229788699u, 2382715877u) + fold_to_uint(v.c2) * uint3(1851936439u, 1938025801u, 3712598587u)) + 3956330501u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.double3x3 v)
		{
			return fold_to_uint(v.c0) * uint3(2437373431u, 1441286183u, 2426570171u) + fold_to_uint(v.c1) * uint3(1561977301u, 4205774813u, 1650214333u) + fold_to_uint(v.c2) * uint3(3388112843u, 1831150513u, 1848374953u) + 3430200247u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 double3x4(global::Unity.Mathematics.double3 c0, global::Unity.Mathematics.double3 c1, global::Unity.Mathematics.double3 c2, global::Unity.Mathematics.double3 c3)
		{
			return new global::Unity.Mathematics.double3x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 double3x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23)
		{
			return new global::Unity.Mathematics.double3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 double3x4(double v)
		{
			return new global::Unity.Mathematics.double3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 double3x4(bool v)
		{
			return new global::Unity.Mathematics.double3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 double3x4(global::Unity.Mathematics.bool3x4 v)
		{
			return new global::Unity.Mathematics.double3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 double3x4(int v)
		{
			return new global::Unity.Mathematics.double3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 double3x4(global::Unity.Mathematics.int3x4 v)
		{
			return new global::Unity.Mathematics.double3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 double3x4(uint v)
		{
			return new global::Unity.Mathematics.double3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 double3x4(global::Unity.Mathematics.uint3x4 v)
		{
			return new global::Unity.Mathematics.double3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 double3x4(float v)
		{
			return new global::Unity.Mathematics.double3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 double3x4(global::Unity.Mathematics.float3x4 v)
		{
			return new global::Unity.Mathematics.double3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 transpose(global::Unity.Mathematics.double3x4 v)
		{
			return double4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		public static global::Unity.Mathematics.double3x4 fastinverse(global::Unity.Mathematics.double3x4 m)
		{
			global::Unity.Mathematics.double3 c = m.c0;
			global::Unity.Mathematics.double3 c2 = m.c1;
			global::Unity.Mathematics.double3 c3 = m.c2;
			global::Unity.Mathematics.double3 c4 = m.c3;
			global::Unity.Mathematics.double3 double5 = double3(c.x, c2.x, c3.x);
			global::Unity.Mathematics.double3 double6 = double3(c.y, c2.y, c3.y);
			global::Unity.Mathematics.double3 double7 = double3(c.z, c2.z, c3.z);
			c4 = -(double5 * c4.x + double6 * c4.y + double7 * c4.z);
			return double3x4(double5, double6, double7, c4);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double3x4 v)
		{
			return csum(fold_to_uint(v.c0) * uint3(3996716183u, 2626301701u, 1306289417u) + fold_to_uint(v.c1) * uint3(2096137163u, 1548578029u, 4178800919u) + fold_to_uint(v.c2) * uint3(3898072289u, 4129428421u, 2631575897u) + fold_to_uint(v.c3) * uint3(2854656703u, 3578504047u, 4245178297u)) + 2173281923u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.double3x4 v)
		{
			return fold_to_uint(v.c0) * uint3(2973357649u, 3881277847u, 4017968839u) + fold_to_uint(v.c1) * uint3(1727237899u, 1648514723u, 1385344481u) + fold_to_uint(v.c2) * uint3(3538260197u, 4066109527u, 2613148903u) + fold_to_uint(v.c3) * uint3(3367528529u, 1678332449u, 2918459647u) + 2744611081u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(double x, double y, double z, double w)
		{
			return new global::Unity.Mathematics.double4(x, y, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(double x, double y, global::Unity.Mathematics.double2 zw)
		{
			return new global::Unity.Mathematics.double4(x, y, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(double x, global::Unity.Mathematics.double2 yz, double w)
		{
			return new global::Unity.Mathematics.double4(x, yz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(double x, global::Unity.Mathematics.double3 yzw)
		{
			return new global::Unity.Mathematics.double4(x, yzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(global::Unity.Mathematics.double2 xy, double z, double w)
		{
			return new global::Unity.Mathematics.double4(xy, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(global::Unity.Mathematics.double2 xy, global::Unity.Mathematics.double2 zw)
		{
			return new global::Unity.Mathematics.double4(xy, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(global::Unity.Mathematics.double3 xyz, double w)
		{
			return new global::Unity.Mathematics.double4(xyz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(global::Unity.Mathematics.double4 xyzw)
		{
			return new global::Unity.Mathematics.double4(xyzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(double v)
		{
			return new global::Unity.Mathematics.double4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(bool v)
		{
			return new global::Unity.Mathematics.double4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(global::Unity.Mathematics.bool4 v)
		{
			return new global::Unity.Mathematics.double4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(int v)
		{
			return new global::Unity.Mathematics.double4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(global::Unity.Mathematics.int4 v)
		{
			return new global::Unity.Mathematics.double4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(uint v)
		{
			return new global::Unity.Mathematics.double4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(global::Unity.Mathematics.uint4 v)
		{
			return new global::Unity.Mathematics.double4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(global::Unity.Mathematics.half v)
		{
			return new global::Unity.Mathematics.double4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(global::Unity.Mathematics.half4 v)
		{
			return new global::Unity.Mathematics.double4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(float v)
		{
			return new global::Unity.Mathematics.double4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 double4(global::Unity.Mathematics.float4 v)
		{
			return new global::Unity.Mathematics.double4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double4 v)
		{
			return csum(fold_to_uint(v) * uint4(2669441947u, 1260114311u, 2650080659u, 4052675461u)) + 2652487619u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.double4 v)
		{
			return fold_to_uint(v) * uint4(2174136431u, 3528391193u, 2105559227u, 1899745391u) + 1966790317u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double shuffle(global::Unity.Mathematics.double4 left, global::Unity.Mathematics.double4 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 shuffle(global::Unity.Mathematics.double4 left, global::Unity.Mathematics.double4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return double2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 shuffle(global::Unity.Mathematics.double4 left, global::Unity.Mathematics.double4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return double3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 shuffle(global::Unity.Mathematics.double4 left, global::Unity.Mathematics.double4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return double4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static double select_shuffle_component(global::Unity.Mathematics.double4 a, global::Unity.Mathematics.double4 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftZ => a.z, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftW => a.w, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightZ => b.z, 
				global::Unity.Mathematics.math.ShuffleComponent.RightW => b.w, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 double4x2(global::Unity.Mathematics.double4 c0, global::Unity.Mathematics.double4 c1)
		{
			return new global::Unity.Mathematics.double4x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 double4x2(double m00, double m01, double m10, double m11, double m20, double m21, double m30, double m31)
		{
			return new global::Unity.Mathematics.double4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 double4x2(double v)
		{
			return new global::Unity.Mathematics.double4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 double4x2(bool v)
		{
			return new global::Unity.Mathematics.double4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 double4x2(global::Unity.Mathematics.bool4x2 v)
		{
			return new global::Unity.Mathematics.double4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 double4x2(int v)
		{
			return new global::Unity.Mathematics.double4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 double4x2(global::Unity.Mathematics.int4x2 v)
		{
			return new global::Unity.Mathematics.double4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 double4x2(uint v)
		{
			return new global::Unity.Mathematics.double4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 double4x2(global::Unity.Mathematics.uint4x2 v)
		{
			return new global::Unity.Mathematics.double4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 double4x2(float v)
		{
			return new global::Unity.Mathematics.double4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 double4x2(global::Unity.Mathematics.float4x2 v)
		{
			return new global::Unity.Mathematics.double4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 transpose(global::Unity.Mathematics.double4x2 v)
		{
			return double2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double4x2 v)
		{
			return csum(fold_to_uint(v.c0) * uint4(1521739981u, 1735296007u, 3010324327u, 1875523709u) + fold_to_uint(v.c1) * uint4(2937008387u, 3835713223u, 2216526373u, 3375971453u)) + 3559829411u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.double4x2 v)
		{
			return fold_to_uint(v.c0) * uint4(3652178029u, 2544260129u, 2013864031u, 2627668003u) + fold_to_uint(v.c1) * uint4(1520214331u, 2949502447u, 2827819133u, 3480140317u) + 2642994593u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 double4x3(global::Unity.Mathematics.double4 c0, global::Unity.Mathematics.double4 c1, global::Unity.Mathematics.double4 c2)
		{
			return new global::Unity.Mathematics.double4x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 double4x3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22, double m30, double m31, double m32)
		{
			return new global::Unity.Mathematics.double4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 double4x3(double v)
		{
			return new global::Unity.Mathematics.double4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 double4x3(bool v)
		{
			return new global::Unity.Mathematics.double4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 double4x3(global::Unity.Mathematics.bool4x3 v)
		{
			return new global::Unity.Mathematics.double4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 double4x3(int v)
		{
			return new global::Unity.Mathematics.double4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 double4x3(global::Unity.Mathematics.int4x3 v)
		{
			return new global::Unity.Mathematics.double4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 double4x3(uint v)
		{
			return new global::Unity.Mathematics.double4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 double4x3(global::Unity.Mathematics.uint4x3 v)
		{
			return new global::Unity.Mathematics.double4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 double4x3(float v)
		{
			return new global::Unity.Mathematics.double4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 double4x3(global::Unity.Mathematics.float4x3 v)
		{
			return new global::Unity.Mathematics.double4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 transpose(global::Unity.Mathematics.double4x3 v)
		{
			return double3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double4x3 v)
		{
			return csum(fold_to_uint(v.c0) * uint4(2057338067u, 2942577577u, 2834440507u, 2671762487u) + fold_to_uint(v.c1) * uint4(2892026051u, 2455987759u, 3868600063u, 3170963179u) + fold_to_uint(v.c2) * uint4(2632835537u, 1136528209u, 2944626401u, 2972762423u)) + 1417889653;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.double4x3 v)
		{
			return fold_to_uint(v.c0) * uint4(2080514593u, 2731544287u, 2828498809u, 2669441947u) + fold_to_uint(v.c1) * uint4(1260114311u, 2650080659u, 4052675461u, 2652487619u) + fold_to_uint(v.c2) * uint4(2174136431u, 3528391193u, 2105559227u, 1899745391u) + 1966790317u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 double4x4(global::Unity.Mathematics.double4 c0, global::Unity.Mathematics.double4 c1, global::Unity.Mathematics.double4 c2, global::Unity.Mathematics.double4 c3)
		{
			return new global::Unity.Mathematics.double4x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 double4x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23, double m30, double m31, double m32, double m33)
		{
			return new global::Unity.Mathematics.double4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 double4x4(double v)
		{
			return new global::Unity.Mathematics.double4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 double4x4(bool v)
		{
			return new global::Unity.Mathematics.double4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 double4x4(global::Unity.Mathematics.bool4x4 v)
		{
			return new global::Unity.Mathematics.double4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 double4x4(int v)
		{
			return new global::Unity.Mathematics.double4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 double4x4(global::Unity.Mathematics.int4x4 v)
		{
			return new global::Unity.Mathematics.double4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 double4x4(uint v)
		{
			return new global::Unity.Mathematics.double4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 double4x4(global::Unity.Mathematics.uint4x4 v)
		{
			return new global::Unity.Mathematics.double4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 double4x4(float v)
		{
			return new global::Unity.Mathematics.double4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 double4x4(global::Unity.Mathematics.float4x4 v)
		{
			return new global::Unity.Mathematics.double4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 rotate(global::Unity.Mathematics.double4x4 a, global::Unity.Mathematics.double3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z).xyz;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 transform(global::Unity.Mathematics.double4x4 a, global::Unity.Mathematics.double3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3).xyz;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 transpose(global::Unity.Mathematics.double4x4 v)
		{
			return double4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		public static global::Unity.Mathematics.double4x4 inverse(global::Unity.Mathematics.double4x4 m)
		{
			global::Unity.Mathematics.double4 c = m.c0;
			global::Unity.Mathematics.double4 c2 = m.c1;
			global::Unity.Mathematics.double4 c3 = m.c2;
			global::Unity.Mathematics.double4 c4 = m.c3;
			global::Unity.Mathematics.double4 double5 = movelh(c2, c);
			global::Unity.Mathematics.double4 double6 = movelh(c3, c4);
			global::Unity.Mathematics.double4 double7 = movehl(c, c2);
			global::Unity.Mathematics.double4 double8 = movehl(c4, c3);
			global::Unity.Mathematics.double4 obj = shuffle(c2, c, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightY, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			global::Unity.Mathematics.double4 double9 = shuffle(c3, c4, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightY, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			global::Unity.Mathematics.double4 double10 = shuffle(c2, c, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.double4 double11 = shuffle(c3, c4, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.double4 double12 = shuffle(double6, double5, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			global::Unity.Mathematics.double4 double13 = shuffle(double6, double5, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.RightY, global::Unity.Mathematics.math.ShuffleComponent.RightW);
			global::Unity.Mathematics.double4 double14 = shuffle(double8, double7, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			global::Unity.Mathematics.double4 double15 = shuffle(double8, double7, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.RightY, global::Unity.Mathematics.math.ShuffleComponent.RightW);
			global::Unity.Mathematics.double4 double16 = shuffle(double5, double6, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			global::Unity.Mathematics.double4 obj2 = obj * double8 - double9 * double7;
			global::Unity.Mathematics.double4 double17 = double5 * double8 - double6 * double7;
			global::Unity.Mathematics.double4 double18 = double11 * double5 - double10 * double6;
			global::Unity.Mathematics.double4 double19 = shuffle(obj2, obj2, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightZ, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.double4 double20 = shuffle(obj2, obj2, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightY);
			global::Unity.Mathematics.double4 double21 = shuffle(double17, double17, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightZ, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.double4 double22 = shuffle(double17, double17, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightY);
			global::Unity.Mathematics.double4 double23 = double15 * double19 - double14 * double22 + double13 * double20;
			global::Unity.Mathematics.double4 double24 = double16 * double23;
			double24 += shuffle(double24, double24, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			double24 -= shuffle(double24, double24, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.double4 double25 = double4(1.0) / double24;
			global::Unity.Mathematics.double4x4 result = default(global::Unity.Mathematics.double4x4);
			result.c0 = double23 * double25;
			global::Unity.Mathematics.double4 double26 = shuffle(double18, double18, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightZ, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.double4 double27 = shuffle(double18, double18, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightY);
			global::Unity.Mathematics.double4 double28 = double14 * double26 - double12 * double20 - double15 * double21;
			result.c1 = double28 * double25;
			global::Unity.Mathematics.double4 double29 = double12 * double22 - double13 * double26 - double15 * double27;
			result.c2 = double29 * double25;
			global::Unity.Mathematics.double4 double30 = double13 * double21 - double12 * double19 + double14 * double27;
			result.c3 = double30 * double25;
			return result;
		}

		public static global::Unity.Mathematics.double4x4 fastinverse(global::Unity.Mathematics.double4x4 m)
		{
			global::Unity.Mathematics.double4 c = m.c0;
			global::Unity.Mathematics.double4 c2 = m.c1;
			global::Unity.Mathematics.double4 c3 = m.c2;
			global::Unity.Mathematics.double4 c4 = m.c3;
			global::Unity.Mathematics.double4 b = double4(0);
			global::Unity.Mathematics.double4 a = unpacklo(c, c3);
			global::Unity.Mathematics.double4 b2 = unpacklo(c2, b);
			global::Unity.Mathematics.double4 a2 = unpackhi(c, c3);
			global::Unity.Mathematics.double4 b3 = unpackhi(c2, b);
			global::Unity.Mathematics.double4 double5 = unpacklo(a, b2);
			global::Unity.Mathematics.double4 double6 = unpackhi(a, b2);
			global::Unity.Mathematics.double4 double7 = unpacklo(a2, b3);
			c4 = -(double5 * c4.x + double6 * c4.y + double7 * c4.z);
			c4.w = 1.0;
			return double4x4(double5, double6, double7, c4);
		}

		public static double determinant(global::Unity.Mathematics.double4x4 m)
		{
			global::Unity.Mathematics.double4 c = m.c0;
			global::Unity.Mathematics.double4 c2 = m.c1;
			global::Unity.Mathematics.double4 c3 = m.c2;
			global::Unity.Mathematics.double4 c4 = m.c3;
			double num = c2.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c2.z * c4.w - c2.w * c4.z) + c4.y * (c2.z * c3.w - c2.w * c3.z);
			double num2 = c.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c3.w - c.w * c3.z);
			double num3 = c.y * (c2.z * c4.w - c2.w * c4.z) - c2.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c2.w - c.w * c2.z);
			double num4 = c.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c.z * c3.w - c.w * c3.z) + c3.y * (c.z * c2.w - c.w * c2.z);
			return c.x * num - c2.x * num2 + c3.x * num3 - c4.x * num4;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.double4x4 v)
		{
			return csum(fold_to_uint(v.c0) * uint4(1306289417u, 2096137163u, 1548578029u, 4178800919u) + fold_to_uint(v.c1) * uint4(3898072289u, 4129428421u, 2631575897u, 2854656703u) + fold_to_uint(v.c2) * uint4(3578504047u, 4245178297u, 2173281923u, 2973357649u) + fold_to_uint(v.c3) * uint4(3881277847u, 4017968839u, 1727237899u, 1648514723u)) + 1385344481;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.double4x4 v)
		{
			return fold_to_uint(v.c0) * uint4(3538260197u, 4066109527u, 2613148903u, 3367528529u) + fold_to_uint(v.c1) * uint4(1678332449u, 2918459647u, 2744611081u, 1952372791u) + fold_to_uint(v.c2) * uint4(2631698677u, 4200781601u, 2119021007u, 1760485621u) + fold_to_uint(v.c3) * uint4(3157985881u, 2171534173u, 2723054263u, 1168253063u) + 4228926523u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(float x, float y)
		{
			return new global::Unity.Mathematics.float2(x, y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(global::Unity.Mathematics.float2 xy)
		{
			return new global::Unity.Mathematics.float2(xy);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(float v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(bool v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(global::Unity.Mathematics.bool2 v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(int v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(global::Unity.Mathematics.int2 v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(uint v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(global::Unity.Mathematics.uint2 v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(global::Unity.Mathematics.half v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(global::Unity.Mathematics.half2 v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(double v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 float2(global::Unity.Mathematics.double2 v)
		{
			return new global::Unity.Mathematics.float2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float2 v)
		{
			return csum(asuint(v) * uint2(4198118021u, 2908068253u)) + 3705492289u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.float2 v)
		{
			return asuint(v) * uint2(2497566569u, 2716413241u) + 1166264321u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float shuffle(global::Unity.Mathematics.float2 left, global::Unity.Mathematics.float2 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 shuffle(global::Unity.Mathematics.float2 left, global::Unity.Mathematics.float2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return float2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 shuffle(global::Unity.Mathematics.float2 left, global::Unity.Mathematics.float2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return float3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 shuffle(global::Unity.Mathematics.float2 left, global::Unity.Mathematics.float2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return float4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static float select_shuffle_component(global::Unity.Mathematics.float2 a, global::Unity.Mathematics.float2 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 float2x2(global::Unity.Mathematics.float2 c0, global::Unity.Mathematics.float2 c1)
		{
			return new global::Unity.Mathematics.float2x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 float2x2(float m00, float m01, float m10, float m11)
		{
			return new global::Unity.Mathematics.float2x2(m00, m01, m10, m11);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 float2x2(float v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 float2x2(bool v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 float2x2(global::Unity.Mathematics.bool2x2 v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 float2x2(int v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 float2x2(global::Unity.Mathematics.int2x2 v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 float2x2(uint v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 float2x2(global::Unity.Mathematics.uint2x2 v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 float2x2(double v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 float2x2(global::Unity.Mathematics.double2x2 v)
		{
			return new global::Unity.Mathematics.float2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 transpose(global::Unity.Mathematics.float2x2 v)
		{
			return float2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 inverse(global::Unity.Mathematics.float2x2 m)
		{
			float x = m.c0.x;
			float x2 = m.c1.x;
			float y = m.c0.y;
			float y2 = m.c1.y;
			float num = x * y2 - x2 * y;
			return float2x2(y2, 0f - x2, 0f - y, x) * (1f / num);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float determinant(global::Unity.Mathematics.float2x2 m)
		{
			float x = m.c0.x;
			float x2 = m.c1.x;
			float y = m.c0.y;
			float y2 = m.c1.y;
			return x * y2 - x2 * y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float2x2 v)
		{
			return csum(asuint(v.c0) * uint2(2627668003u, 1520214331u) + asuint(v.c1) * uint2(2949502447u, 2827819133u)) + 3480140317u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.float2x2 v)
		{
			return asuint(v.c0) * uint2(2642994593u, 3940484981u) + asuint(v.c1) * uint2(1954192763u, 1091696537u) + 3052428017u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 float2x3(global::Unity.Mathematics.float2 c0, global::Unity.Mathematics.float2 c1, global::Unity.Mathematics.float2 c2)
		{
			return new global::Unity.Mathematics.float2x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 float2x3(float m00, float m01, float m02, float m10, float m11, float m12)
		{
			return new global::Unity.Mathematics.float2x3(m00, m01, m02, m10, m11, m12);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 float2x3(float v)
		{
			return new global::Unity.Mathematics.float2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 float2x3(bool v)
		{
			return new global::Unity.Mathematics.float2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 float2x3(global::Unity.Mathematics.bool2x3 v)
		{
			return new global::Unity.Mathematics.float2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 float2x3(int v)
		{
			return new global::Unity.Mathematics.float2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 float2x3(global::Unity.Mathematics.int2x3 v)
		{
			return new global::Unity.Mathematics.float2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 float2x3(uint v)
		{
			return new global::Unity.Mathematics.float2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 float2x3(global::Unity.Mathematics.uint2x3 v)
		{
			return new global::Unity.Mathematics.float2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 float2x3(double v)
		{
			return new global::Unity.Mathematics.float2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 float2x3(global::Unity.Mathematics.double2x3 v)
		{
			return new global::Unity.Mathematics.float2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 transpose(global::Unity.Mathematics.float2x3 v)
		{
			return float3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float2x3 v)
		{
			return csum(asuint(v.c0) * uint2(3898072289u, 4129428421u) + asuint(v.c1) * uint2(2631575897u, 2854656703u) + asuint(v.c2) * uint2(3578504047u, 4245178297u)) + 2173281923u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.float2x3 v)
		{
			return asuint(v.c0) * uint2(2973357649u, 3881277847u) + asuint(v.c1) * uint2(4017968839u, 1727237899u) + asuint(v.c2) * uint2(1648514723u, 1385344481u) + 3538260197u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 float2x4(global::Unity.Mathematics.float2 c0, global::Unity.Mathematics.float2 c1, global::Unity.Mathematics.float2 c2, global::Unity.Mathematics.float2 c3)
		{
			return new global::Unity.Mathematics.float2x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 float2x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13)
		{
			return new global::Unity.Mathematics.float2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 float2x4(float v)
		{
			return new global::Unity.Mathematics.float2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 float2x4(bool v)
		{
			return new global::Unity.Mathematics.float2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 float2x4(global::Unity.Mathematics.bool2x4 v)
		{
			return new global::Unity.Mathematics.float2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 float2x4(int v)
		{
			return new global::Unity.Mathematics.float2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 float2x4(global::Unity.Mathematics.int2x4 v)
		{
			return new global::Unity.Mathematics.float2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 float2x4(uint v)
		{
			return new global::Unity.Mathematics.float2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 float2x4(global::Unity.Mathematics.uint2x4 v)
		{
			return new global::Unity.Mathematics.float2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 float2x4(double v)
		{
			return new global::Unity.Mathematics.float2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 float2x4(global::Unity.Mathematics.double2x4 v)
		{
			return new global::Unity.Mathematics.float2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 transpose(global::Unity.Mathematics.float2x4 v)
		{
			return float4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float2x4 v)
		{
			return csum(asuint(v.c0) * uint2(3546061613u, 2702024231u) + asuint(v.c1) * uint2(1452124841u, 1966089551u) + asuint(v.c2) * uint2(2668168249u, 1587512777u) + asuint(v.c3) * uint2(2353831999u, 3101256173u)) + 2891822459u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.float2x4 v)
		{
			return asuint(v.c0) * uint2(2837054189u, 3016004371u) + asuint(v.c1) * uint2(4097481403u, 2229788699u) + asuint(v.c2) * uint2(2382715877u, 1851936439u) + asuint(v.c3) * uint2(1938025801u, 3712598587u) + 3956330501u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(float x, float y, float z)
		{
			return new global::Unity.Mathematics.float3(x, y, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(float x, global::Unity.Mathematics.float2 yz)
		{
			return new global::Unity.Mathematics.float3(x, yz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(global::Unity.Mathematics.float2 xy, float z)
		{
			return new global::Unity.Mathematics.float3(xy, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(global::Unity.Mathematics.float3 xyz)
		{
			return new global::Unity.Mathematics.float3(xyz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(float v)
		{
			return new global::Unity.Mathematics.float3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(bool v)
		{
			return new global::Unity.Mathematics.float3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(global::Unity.Mathematics.bool3 v)
		{
			return new global::Unity.Mathematics.float3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(int v)
		{
			return new global::Unity.Mathematics.float3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(global::Unity.Mathematics.int3 v)
		{
			return new global::Unity.Mathematics.float3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(uint v)
		{
			return new global::Unity.Mathematics.float3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(global::Unity.Mathematics.uint3 v)
		{
			return new global::Unity.Mathematics.float3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(global::Unity.Mathematics.half v)
		{
			return new global::Unity.Mathematics.float3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(global::Unity.Mathematics.half3 v)
		{
			return new global::Unity.Mathematics.float3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(double v)
		{
			return new global::Unity.Mathematics.float3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 float3(global::Unity.Mathematics.double3 v)
		{
			return new global::Unity.Mathematics.float3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float3 v)
		{
			return csum(asuint(v) * uint3(2601761069u, 1254033427u, 2248573027u)) + 3612677113u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.float3 v)
		{
			return asuint(v) * uint3(1521739981u, 1735296007u, 3010324327u) + 1875523709u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float shuffle(global::Unity.Mathematics.float3 left, global::Unity.Mathematics.float3 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 shuffle(global::Unity.Mathematics.float3 left, global::Unity.Mathematics.float3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return float2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 shuffle(global::Unity.Mathematics.float3 left, global::Unity.Mathematics.float3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return float3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 shuffle(global::Unity.Mathematics.float3 left, global::Unity.Mathematics.float3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return float4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static float select_shuffle_component(global::Unity.Mathematics.float3 a, global::Unity.Mathematics.float3 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftZ => a.z, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightZ => b.z, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 float3x2(global::Unity.Mathematics.float3 c0, global::Unity.Mathematics.float3 c1)
		{
			return new global::Unity.Mathematics.float3x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 float3x2(float m00, float m01, float m10, float m11, float m20, float m21)
		{
			return new global::Unity.Mathematics.float3x2(m00, m01, m10, m11, m20, m21);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 float3x2(float v)
		{
			return new global::Unity.Mathematics.float3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 float3x2(bool v)
		{
			return new global::Unity.Mathematics.float3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 float3x2(global::Unity.Mathematics.bool3x2 v)
		{
			return new global::Unity.Mathematics.float3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 float3x2(int v)
		{
			return new global::Unity.Mathematics.float3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 float3x2(global::Unity.Mathematics.int3x2 v)
		{
			return new global::Unity.Mathematics.float3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 float3x2(uint v)
		{
			return new global::Unity.Mathematics.float3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 float3x2(global::Unity.Mathematics.uint3x2 v)
		{
			return new global::Unity.Mathematics.float3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 float3x2(double v)
		{
			return new global::Unity.Mathematics.float3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 float3x2(global::Unity.Mathematics.double3x2 v)
		{
			return new global::Unity.Mathematics.float3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 transpose(global::Unity.Mathematics.float3x2 v)
		{
			return float2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float3x2 v)
		{
			return csum(asuint(v.c0) * uint3(3777095341u, 3385463369u, 1773538433u) + asuint(v.c1) * uint3(3773525029u, 4131962539u, 1809525511u)) + 4016293529u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.float3x2 v)
		{
			return asuint(v.c0) * uint3(2416021567u, 2828384717u, 2636362241u) + asuint(v.c1) * uint3(1258410977u, 1952565773u, 2037535609u) + 3592785499u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(global::Unity.Mathematics.float3 c0, global::Unity.Mathematics.float3 c1, global::Unity.Mathematics.float3 c2)
		{
			return new global::Unity.Mathematics.float3x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
		{
			return new global::Unity.Mathematics.float3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(float v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(bool v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(global::Unity.Mathematics.bool3x3 v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(int v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(global::Unity.Mathematics.int3x3 v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(uint v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(global::Unity.Mathematics.uint3x3 v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(double v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(global::Unity.Mathematics.double3x3 v)
		{
			return new global::Unity.Mathematics.float3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 transpose(global::Unity.Mathematics.float3x3 v)
		{
			return float3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		public static global::Unity.Mathematics.float3x3 inverse(global::Unity.Mathematics.float3x3 m)
		{
			global::Unity.Mathematics.float3 c = m.c0;
			global::Unity.Mathematics.float3 c2 = m.c1;
			global::Unity.Mathematics.float3 c3 = m.c2;
			global::Unity.Mathematics.float3 float5 = float3(c2.x, c3.x, c.x);
			global::Unity.Mathematics.float3 float6 = float3(c2.y, c3.y, c.y);
			global::Unity.Mathematics.float3 float7 = float3(c2.z, c3.z, c.z);
			global::Unity.Mathematics.float3 float8 = float6 * float7.yzx - float6.yzx * float7;
			global::Unity.Mathematics.float3 c4 = float5.yzx * float7 - float5 * float7.yzx;
			global::Unity.Mathematics.float3 c5 = float5 * float6.yzx - float5.yzx * float6;
			float num = 1f / csum(float5.zxy * float8);
			return float3x3(float8, c4, c5) * num;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float determinant(global::Unity.Mathematics.float3x3 m)
		{
			global::Unity.Mathematics.float3 c = m.c0;
			global::Unity.Mathematics.float3 c2 = m.c1;
			global::Unity.Mathematics.float3 c3 = m.c2;
			float num = c2.y * c3.z - c2.z * c3.y;
			float num2 = c.y * c3.z - c.z * c3.y;
			float num3 = c.y * c2.z - c.z * c2.y;
			return c.x * num - c2.x * num2 + c3.x * num3;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float3x3 v)
		{
			return csum(asuint(v.c0) * uint3(1899745391u, 1966790317u, 3516359879u) + asuint(v.c1) * uint3(3050356579u, 4178586719u, 2558655391u) + asuint(v.c2) * uint3(1453413133u, 2152428077u, 1938706661u)) + 1338588197;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.float3x3 v)
		{
			return asuint(v.c0) * uint3(3439609253u, 3535343003u, 3546061613u) + asuint(v.c1) * uint3(2702024231u, 1452124841u, 1966089551u) + asuint(v.c2) * uint3(2668168249u, 1587512777u, 2353831999u) + 3101256173u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(global::Unity.Mathematics.float3 c0, global::Unity.Mathematics.float3 c1, global::Unity.Mathematics.float3 c2, global::Unity.Mathematics.float3 c3)
		{
			return new global::Unity.Mathematics.float3x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23)
		{
			return new global::Unity.Mathematics.float3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(float v)
		{
			return new global::Unity.Mathematics.float3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(bool v)
		{
			return new global::Unity.Mathematics.float3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(global::Unity.Mathematics.bool3x4 v)
		{
			return new global::Unity.Mathematics.float3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(int v)
		{
			return new global::Unity.Mathematics.float3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(global::Unity.Mathematics.int3x4 v)
		{
			return new global::Unity.Mathematics.float3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(uint v)
		{
			return new global::Unity.Mathematics.float3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(global::Unity.Mathematics.uint3x4 v)
		{
			return new global::Unity.Mathematics.float3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(double v)
		{
			return new global::Unity.Mathematics.float3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 float3x4(global::Unity.Mathematics.double3x4 v)
		{
			return new global::Unity.Mathematics.float3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 transpose(global::Unity.Mathematics.float3x4 v)
		{
			return float4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		public static global::Unity.Mathematics.float3x4 fastinverse(global::Unity.Mathematics.float3x4 m)
		{
			global::Unity.Mathematics.float3 c = m.c0;
			global::Unity.Mathematics.float3 c2 = m.c1;
			global::Unity.Mathematics.float3 c3 = m.c2;
			global::Unity.Mathematics.float3 c4 = m.c3;
			global::Unity.Mathematics.float3 float5 = float3(c.x, c2.x, c3.x);
			global::Unity.Mathematics.float3 float6 = float3(c.y, c2.y, c3.y);
			global::Unity.Mathematics.float3 float7 = float3(c.z, c2.z, c3.z);
			c4 = -(float5 * c4.x + float6 * c4.y + float7 * c4.z);
			return float3x4(float5, float6, float7, c4);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float3x4 v)
		{
			return csum(asuint(v.c0) * uint3(4192899797u, 3271228601u, 1634639009u) + asuint(v.c1) * uint3(3318036811u, 3404170631u, 2048213449u) + asuint(v.c2) * uint3(4164671783u, 1780759499u, 1352369353u) + asuint(v.c3) * uint3(2446407751u, 1391928079u, 3475533443u)) + 3777095341u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.float3x4 v)
		{
			return asuint(v.c0) * uint3(3385463369u, 1773538433u, 3773525029u) + asuint(v.c1) * uint3(4131962539u, 1809525511u, 4016293529u) + asuint(v.c2) * uint3(2416021567u, 2828384717u, 2636362241u) + asuint(v.c3) * uint3(1258410977u, 1952565773u, 2037535609u) + 3592785499u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(float x, float y, float z, float w)
		{
			return new global::Unity.Mathematics.float4(x, y, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(float x, float y, global::Unity.Mathematics.float2 zw)
		{
			return new global::Unity.Mathematics.float4(x, y, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(float x, global::Unity.Mathematics.float2 yz, float w)
		{
			return new global::Unity.Mathematics.float4(x, yz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(float x, global::Unity.Mathematics.float3 yzw)
		{
			return new global::Unity.Mathematics.float4(x, yzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(global::Unity.Mathematics.float2 xy, float z, float w)
		{
			return new global::Unity.Mathematics.float4(xy, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(global::Unity.Mathematics.float2 xy, global::Unity.Mathematics.float2 zw)
		{
			return new global::Unity.Mathematics.float4(xy, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(global::Unity.Mathematics.float3 xyz, float w)
		{
			return new global::Unity.Mathematics.float4(xyz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(global::Unity.Mathematics.float4 xyzw)
		{
			return new global::Unity.Mathematics.float4(xyzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(float v)
		{
			return new global::Unity.Mathematics.float4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(bool v)
		{
			return new global::Unity.Mathematics.float4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(global::Unity.Mathematics.bool4 v)
		{
			return new global::Unity.Mathematics.float4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(int v)
		{
			return new global::Unity.Mathematics.float4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(global::Unity.Mathematics.int4 v)
		{
			return new global::Unity.Mathematics.float4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(uint v)
		{
			return new global::Unity.Mathematics.float4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(global::Unity.Mathematics.uint4 v)
		{
			return new global::Unity.Mathematics.float4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(global::Unity.Mathematics.half v)
		{
			return new global::Unity.Mathematics.float4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(global::Unity.Mathematics.half4 v)
		{
			return new global::Unity.Mathematics.float4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(double v)
		{
			return new global::Unity.Mathematics.float4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 float4(global::Unity.Mathematics.double4 v)
		{
			return new global::Unity.Mathematics.float4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float4 v)
		{
			return csum(asuint(v) * uint4(3868600063u, 3170963179u, 2632835537u, 1136528209u)) + 2944626401u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.float4 v)
		{
			return asuint(v) * uint4(2972762423u, 1417889653u, 2080514593u, 2731544287u) + 2828498809u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float shuffle(global::Unity.Mathematics.float4 left, global::Unity.Mathematics.float4 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 shuffle(global::Unity.Mathematics.float4 left, global::Unity.Mathematics.float4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return float2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 shuffle(global::Unity.Mathematics.float4 left, global::Unity.Mathematics.float4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return float3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 shuffle(global::Unity.Mathematics.float4 left, global::Unity.Mathematics.float4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return float4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static float select_shuffle_component(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftZ => a.z, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftW => a.w, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightZ => b.z, 
				global::Unity.Mathematics.math.ShuffleComponent.RightW => b.w, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 float4x2(global::Unity.Mathematics.float4 c0, global::Unity.Mathematics.float4 c1)
		{
			return new global::Unity.Mathematics.float4x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 float4x2(float m00, float m01, float m10, float m11, float m20, float m21, float m30, float m31)
		{
			return new global::Unity.Mathematics.float4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 float4x2(float v)
		{
			return new global::Unity.Mathematics.float4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 float4x2(bool v)
		{
			return new global::Unity.Mathematics.float4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 float4x2(global::Unity.Mathematics.bool4x2 v)
		{
			return new global::Unity.Mathematics.float4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 float4x2(int v)
		{
			return new global::Unity.Mathematics.float4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 float4x2(global::Unity.Mathematics.int4x2 v)
		{
			return new global::Unity.Mathematics.float4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 float4x2(uint v)
		{
			return new global::Unity.Mathematics.float4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 float4x2(global::Unity.Mathematics.uint4x2 v)
		{
			return new global::Unity.Mathematics.float4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 float4x2(double v)
		{
			return new global::Unity.Mathematics.float4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 float4x2(global::Unity.Mathematics.double4x2 v)
		{
			return new global::Unity.Mathematics.float4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 transpose(global::Unity.Mathematics.float4x2 v)
		{
			return float2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float4x2 v)
		{
			return csum(asuint(v.c0) * uint4(2864955997u, 3525118277u, 2298260269u, 1632478733u) + asuint(v.c1) * uint4(1537393931u, 2353355467u, 3441847433u, 4052036147u)) + 2011389559;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.float4x2 v)
		{
			return asuint(v.c0) * uint4(2252224297u, 3784421429u, 1750626223u, 3571447507u) + asuint(v.c1) * uint4(3412283213u, 2601761069u, 1254033427u, 2248573027u) + 3612677113u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 float4x3(global::Unity.Mathematics.float4 c0, global::Unity.Mathematics.float4 c1, global::Unity.Mathematics.float4 c2)
		{
			return new global::Unity.Mathematics.float4x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 float4x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22, float m30, float m31, float m32)
		{
			return new global::Unity.Mathematics.float4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 float4x3(float v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 float4x3(bool v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 float4x3(global::Unity.Mathematics.bool4x3 v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 float4x3(int v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 float4x3(global::Unity.Mathematics.int4x3 v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 float4x3(uint v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 float4x3(global::Unity.Mathematics.uint4x3 v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 float4x3(double v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 float4x3(global::Unity.Mathematics.double4x3 v)
		{
			return new global::Unity.Mathematics.float4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 transpose(global::Unity.Mathematics.float4x3 v)
		{
			return float3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float4x3 v)
		{
			return csum(asuint(v.c0) * uint4(3309258581u, 1770373673u, 3778261171u, 3286279097u) + asuint(v.c1) * uint4(4264629071u, 1898591447u, 2641864091u, 1229113913u) + asuint(v.c2) * uint4(3020867117u, 1449055807u, 2479033387u, 3702457169u)) + 1845824257;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.float4x3 v)
		{
			return asuint(v.c0) * uint4(1963973621u, 2134758553u, 1391111867u, 1167706003u) + asuint(v.c1) * uint4(2209736489u, 3261535807u, 1740411209u, 2910609089u) + asuint(v.c2) * uint4(2183822701u, 3029516053u, 3547472099u, 2057487037u) + 3781937309u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(global::Unity.Mathematics.float4 c0, global::Unity.Mathematics.float4 c1, global::Unity.Mathematics.float4 c2, global::Unity.Mathematics.float4 c3)
		{
			return new global::Unity.Mathematics.float4x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
		{
			return new global::Unity.Mathematics.float4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(float v)
		{
			return new global::Unity.Mathematics.float4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(bool v)
		{
			return new global::Unity.Mathematics.float4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(global::Unity.Mathematics.bool4x4 v)
		{
			return new global::Unity.Mathematics.float4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(int v)
		{
			return new global::Unity.Mathematics.float4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(global::Unity.Mathematics.int4x4 v)
		{
			return new global::Unity.Mathematics.float4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(uint v)
		{
			return new global::Unity.Mathematics.float4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(global::Unity.Mathematics.uint4x4 v)
		{
			return new global::Unity.Mathematics.float4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(double v)
		{
			return new global::Unity.Mathematics.float4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(global::Unity.Mathematics.double4x4 v)
		{
			return new global::Unity.Mathematics.float4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 rotate(global::Unity.Mathematics.float4x4 a, global::Unity.Mathematics.float3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z).xyz;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 transform(global::Unity.Mathematics.float4x4 a, global::Unity.Mathematics.float3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3).xyz;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 transpose(global::Unity.Mathematics.float4x4 v)
		{
			return float4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		public static global::Unity.Mathematics.float4x4 inverse(global::Unity.Mathematics.float4x4 m)
		{
			global::Unity.Mathematics.float4 c = m.c0;
			global::Unity.Mathematics.float4 c2 = m.c1;
			global::Unity.Mathematics.float4 c3 = m.c2;
			global::Unity.Mathematics.float4 c4 = m.c3;
			global::Unity.Mathematics.float4 float5 = movelh(c2, c);
			global::Unity.Mathematics.float4 float6 = movelh(c3, c4);
			global::Unity.Mathematics.float4 float7 = movehl(c, c2);
			global::Unity.Mathematics.float4 float8 = movehl(c4, c3);
			global::Unity.Mathematics.float4 obj = shuffle(c2, c, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightY, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			global::Unity.Mathematics.float4 float9 = shuffle(c3, c4, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightY, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			global::Unity.Mathematics.float4 float10 = shuffle(c2, c, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.float4 float11 = shuffle(c3, c4, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.float4 float12 = shuffle(float6, float5, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			global::Unity.Mathematics.float4 float13 = shuffle(float6, float5, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.RightY, global::Unity.Mathematics.math.ShuffleComponent.RightW);
			global::Unity.Mathematics.float4 float14 = shuffle(float8, float7, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			global::Unity.Mathematics.float4 float15 = shuffle(float8, float7, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.RightY, global::Unity.Mathematics.math.ShuffleComponent.RightW);
			global::Unity.Mathematics.float4 float16 = shuffle(float5, float6, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			global::Unity.Mathematics.float4 obj2 = obj * float8 - float9 * float7;
			global::Unity.Mathematics.float4 float17 = float5 * float8 - float6 * float7;
			global::Unity.Mathematics.float4 float18 = float11 * float5 - float10 * float6;
			global::Unity.Mathematics.float4 float19 = shuffle(obj2, obj2, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightZ, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.float4 float20 = shuffle(obj2, obj2, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightY);
			global::Unity.Mathematics.float4 float21 = shuffle(float17, float17, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightZ, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.float4 float22 = shuffle(float17, float17, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightY);
			global::Unity.Mathematics.float4 float23 = float15 * float19 - float14 * float22 + float13 * float20;
			global::Unity.Mathematics.float4 float24 = float16 * float23;
			float24 += shuffle(float24, float24, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightZ);
			float24 -= shuffle(float24, float24, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.float4 float25 = float4(1f) / float24;
			global::Unity.Mathematics.float4x4 result = default(global::Unity.Mathematics.float4x4);
			result.c0 = float23 * float25;
			global::Unity.Mathematics.float4 float26 = shuffle(float18, float18, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightZ, global::Unity.Mathematics.math.ShuffleComponent.RightX);
			global::Unity.Mathematics.float4 float27 = shuffle(float18, float18, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.RightW, global::Unity.Mathematics.math.ShuffleComponent.RightY);
			global::Unity.Mathematics.float4 float28 = float14 * float26 - float12 * float20 - float15 * float21;
			result.c1 = float28 * float25;
			global::Unity.Mathematics.float4 float29 = float12 * float22 - float13 * float26 - float15 * float27;
			result.c2 = float29 * float25;
			global::Unity.Mathematics.float4 float30 = float13 * float21 - float12 * float19 + float14 * float27;
			result.c3 = float30 * float25;
			return result;
		}

		public static global::Unity.Mathematics.float4x4 fastinverse(global::Unity.Mathematics.float4x4 m)
		{
			global::Unity.Mathematics.float4 c = m.c0;
			global::Unity.Mathematics.float4 c2 = m.c1;
			global::Unity.Mathematics.float4 c3 = m.c2;
			global::Unity.Mathematics.float4 c4 = m.c3;
			global::Unity.Mathematics.float4 b = float4(0);
			global::Unity.Mathematics.float4 a = unpacklo(c, c3);
			global::Unity.Mathematics.float4 b2 = unpacklo(c2, b);
			global::Unity.Mathematics.float4 a2 = unpackhi(c, c3);
			global::Unity.Mathematics.float4 b3 = unpackhi(c2, b);
			global::Unity.Mathematics.float4 float5 = unpacklo(a, b2);
			global::Unity.Mathematics.float4 float6 = unpackhi(a, b2);
			global::Unity.Mathematics.float4 float7 = unpacklo(a2, b3);
			c4 = -(float5 * c4.x + float6 * c4.y + float7 * c4.z);
			c4.w = 1f;
			return float4x4(float5, float6, float7, c4);
		}

		public static float determinant(global::Unity.Mathematics.float4x4 m)
		{
			global::Unity.Mathematics.float4 c = m.c0;
			global::Unity.Mathematics.float4 c2 = m.c1;
			global::Unity.Mathematics.float4 c3 = m.c2;
			global::Unity.Mathematics.float4 c4 = m.c3;
			float num = c2.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c2.z * c4.w - c2.w * c4.z) + c4.y * (c2.z * c3.w - c2.w * c3.z);
			float num2 = c.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c3.w - c.w * c3.z);
			float num3 = c.y * (c2.z * c4.w - c2.w * c4.z) - c2.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c2.w - c.w * c2.z);
			float num4 = c.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c.z * c3.w - c.w * c3.z) + c3.y * (c.z * c2.w - c.w * c2.z);
			return c.x * num - c2.x * num2 + c3.x * num3 - c4.x * num4;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.float4x4 v)
		{
			return csum(asuint(v.c0) * uint4(3299952959u, 3121178323u, 2948522579u, 1531026433u) + asuint(v.c1) * uint4(1365086453u, 3969870067u, 4192899797u, 3271228601u) + asuint(v.c2) * uint4(1634639009u, 3318036811u, 3404170631u, 2048213449u) + asuint(v.c3) * uint4(4164671783u, 1780759499u, 1352369353u, 2446407751u)) + 1391928079;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.float4x4 v)
		{
			return asuint(v.c0) * uint4(3475533443u, 3777095341u, 3385463369u, 1773538433u) + asuint(v.c1) * uint4(3773525029u, 4131962539u, 1809525511u, 4016293529u) + asuint(v.c2) * uint4(2416021567u, 2828384717u, 2636362241u, 1258410977u) + asuint(v.c3) * uint4(1952565773u, 2037535609u, 3592785499u, 3996716183u) + 2626301701u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half half(global::Unity.Mathematics.half x)
		{
			return new global::Unity.Mathematics.half(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half half(float v)
		{
			return new global::Unity.Mathematics.half(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half half(double v)
		{
			return new global::Unity.Mathematics.half(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.half v)
		{
			return (uint)(v.value * 1952372791 + -2123433123);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half2 half2(global::Unity.Mathematics.half x, global::Unity.Mathematics.half y)
		{
			return new global::Unity.Mathematics.half2(x, y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half2 half2(global::Unity.Mathematics.half2 xy)
		{
			return new global::Unity.Mathematics.half2(xy);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half2 half2(global::Unity.Mathematics.half v)
		{
			return new global::Unity.Mathematics.half2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half2 half2(float v)
		{
			return new global::Unity.Mathematics.half2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half2 half2(global::Unity.Mathematics.float2 v)
		{
			return new global::Unity.Mathematics.half2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half2 half2(double v)
		{
			return new global::Unity.Mathematics.half2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half2 half2(global::Unity.Mathematics.double2 v)
		{
			return new global::Unity.Mathematics.half2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.half2 v)
		{
			return csum(uint2(v.x.value, v.y.value) * uint2(1851936439u, 1938025801u)) + 3712598587u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.half2 v)
		{
			return uint2(v.x.value, v.y.value) * uint2(3956330501u, 2437373431u) + 1441286183u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half3 half3(global::Unity.Mathematics.half x, global::Unity.Mathematics.half y, global::Unity.Mathematics.half z)
		{
			return new global::Unity.Mathematics.half3(x, y, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half3 half3(global::Unity.Mathematics.half x, global::Unity.Mathematics.half2 yz)
		{
			return new global::Unity.Mathematics.half3(x, yz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half3 half3(global::Unity.Mathematics.half2 xy, global::Unity.Mathematics.half z)
		{
			return new global::Unity.Mathematics.half3(xy, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half3 half3(global::Unity.Mathematics.half3 xyz)
		{
			return new global::Unity.Mathematics.half3(xyz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half3 half3(global::Unity.Mathematics.half v)
		{
			return new global::Unity.Mathematics.half3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half3 half3(float v)
		{
			return new global::Unity.Mathematics.half3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half3 half3(global::Unity.Mathematics.float3 v)
		{
			return new global::Unity.Mathematics.half3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half3 half3(double v)
		{
			return new global::Unity.Mathematics.half3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half3 half3(global::Unity.Mathematics.double3 v)
		{
			return new global::Unity.Mathematics.half3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.half3 v)
		{
			return csum(uint3(v.x.value, v.y.value, v.z.value) * uint3(1750611407u, 3285396193u, 3110507567u)) + 4271396531u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.half3 v)
		{
			return uint3(v.x.value, v.y.value, v.z.value) * uint3(4198118021u, 2908068253u, 3705492289u) + 2497566569u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(global::Unity.Mathematics.half x, global::Unity.Mathematics.half y, global::Unity.Mathematics.half z, global::Unity.Mathematics.half w)
		{
			return new global::Unity.Mathematics.half4(x, y, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(global::Unity.Mathematics.half x, global::Unity.Mathematics.half y, global::Unity.Mathematics.half2 zw)
		{
			return new global::Unity.Mathematics.half4(x, y, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(global::Unity.Mathematics.half x, global::Unity.Mathematics.half2 yz, global::Unity.Mathematics.half w)
		{
			return new global::Unity.Mathematics.half4(x, yz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(global::Unity.Mathematics.half x, global::Unity.Mathematics.half3 yzw)
		{
			return new global::Unity.Mathematics.half4(x, yzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(global::Unity.Mathematics.half2 xy, global::Unity.Mathematics.half z, global::Unity.Mathematics.half w)
		{
			return new global::Unity.Mathematics.half4(xy, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(global::Unity.Mathematics.half2 xy, global::Unity.Mathematics.half2 zw)
		{
			return new global::Unity.Mathematics.half4(xy, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(global::Unity.Mathematics.half3 xyz, global::Unity.Mathematics.half w)
		{
			return new global::Unity.Mathematics.half4(xyz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(global::Unity.Mathematics.half4 xyzw)
		{
			return new global::Unity.Mathematics.half4(xyzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(global::Unity.Mathematics.half v)
		{
			return new global::Unity.Mathematics.half4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(float v)
		{
			return new global::Unity.Mathematics.half4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(global::Unity.Mathematics.float4 v)
		{
			return new global::Unity.Mathematics.half4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(double v)
		{
			return new global::Unity.Mathematics.half4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.half4 half4(global::Unity.Mathematics.double4 v)
		{
			return new global::Unity.Mathematics.half4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.half4 v)
		{
			return csum(uint4(v.x.value, v.y.value, v.z.value, v.w.value) * uint4(1952372791u, 2631698677u, 4200781601u, 2119021007u)) + 1760485621;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.half4 v)
		{
			return uint4(v.x.value, v.y.value, v.z.value, v.w.value) * uint4(3157985881u, 2171534173u, 2723054263u, 1168253063u) + 4228926523u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 int2(int x, int y)
		{
			return new global::Unity.Mathematics.int2(x, y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 int2(global::Unity.Mathematics.int2 xy)
		{
			return new global::Unity.Mathematics.int2(xy);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 int2(int v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 int2(bool v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 int2(global::Unity.Mathematics.bool2 v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 int2(uint v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 int2(global::Unity.Mathematics.uint2 v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 int2(float v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 int2(global::Unity.Mathematics.float2 v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 int2(double v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 int2(global::Unity.Mathematics.double2 v)
		{
			return new global::Unity.Mathematics.int2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int2 v)
		{
			return csum(asuint(v) * uint2(2209710647u, 2201894441u)) + 2849577407u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.int2 v)
		{
			return asuint(v) * uint2(3287031191u, 3098675399u) + 1564399943u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int shuffle(global::Unity.Mathematics.int2 left, global::Unity.Mathematics.int2 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 shuffle(global::Unity.Mathematics.int2 left, global::Unity.Mathematics.int2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return int2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 shuffle(global::Unity.Mathematics.int2 left, global::Unity.Mathematics.int2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return int3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 shuffle(global::Unity.Mathematics.int2 left, global::Unity.Mathematics.int2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return int4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static int select_shuffle_component(global::Unity.Mathematics.int2 a, global::Unity.Mathematics.int2 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 int2x2(global::Unity.Mathematics.int2 c0, global::Unity.Mathematics.int2 c1)
		{
			return new global::Unity.Mathematics.int2x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 int2x2(int m00, int m01, int m10, int m11)
		{
			return new global::Unity.Mathematics.int2x2(m00, m01, m10, m11);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 int2x2(int v)
		{
			return new global::Unity.Mathematics.int2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 int2x2(bool v)
		{
			return new global::Unity.Mathematics.int2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 int2x2(global::Unity.Mathematics.bool2x2 v)
		{
			return new global::Unity.Mathematics.int2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 int2x2(uint v)
		{
			return new global::Unity.Mathematics.int2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 int2x2(global::Unity.Mathematics.uint2x2 v)
		{
			return new global::Unity.Mathematics.int2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 int2x2(float v)
		{
			return new global::Unity.Mathematics.int2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 int2x2(global::Unity.Mathematics.float2x2 v)
		{
			return new global::Unity.Mathematics.int2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 int2x2(double v)
		{
			return new global::Unity.Mathematics.int2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 int2x2(global::Unity.Mathematics.double2x2 v)
		{
			return new global::Unity.Mathematics.int2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 transpose(global::Unity.Mathematics.int2x2 v)
		{
			return int2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int determinant(global::Unity.Mathematics.int2x2 m)
		{
			int x = m.c0.x;
			int x2 = m.c1.x;
			int y = m.c0.y;
			int y2 = m.c1.y;
			return x * y2 - x2 * y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int2x2 v)
		{
			return csum(asuint(v.c0) * uint2(3784421429u, 1750626223u) + asuint(v.c1) * uint2(3571447507u, 3412283213u)) + 2601761069u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.int2x2 v)
		{
			return asuint(v.c0) * uint2(1254033427u, 2248573027u) + asuint(v.c1) * uint2(3612677113u, 1521739981u) + 1735296007u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 int2x3(global::Unity.Mathematics.int2 c0, global::Unity.Mathematics.int2 c1, global::Unity.Mathematics.int2 c2)
		{
			return new global::Unity.Mathematics.int2x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 int2x3(int m00, int m01, int m02, int m10, int m11, int m12)
		{
			return new global::Unity.Mathematics.int2x3(m00, m01, m02, m10, m11, m12);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 int2x3(int v)
		{
			return new global::Unity.Mathematics.int2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 int2x3(bool v)
		{
			return new global::Unity.Mathematics.int2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 int2x3(global::Unity.Mathematics.bool2x3 v)
		{
			return new global::Unity.Mathematics.int2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 int2x3(uint v)
		{
			return new global::Unity.Mathematics.int2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 int2x3(global::Unity.Mathematics.uint2x3 v)
		{
			return new global::Unity.Mathematics.int2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 int2x3(float v)
		{
			return new global::Unity.Mathematics.int2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 int2x3(global::Unity.Mathematics.float2x3 v)
		{
			return new global::Unity.Mathematics.int2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 int2x3(double v)
		{
			return new global::Unity.Mathematics.int2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 int2x3(global::Unity.Mathematics.double2x3 v)
		{
			return new global::Unity.Mathematics.int2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 transpose(global::Unity.Mathematics.int2x3 v)
		{
			return int3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int2x3 v)
		{
			return csum(asuint(v.c0) * uint2(3404170631u, 2048213449u) + asuint(v.c1) * uint2(4164671783u, 1780759499u) + asuint(v.c2) * uint2(1352369353u, 2446407751u)) + 1391928079;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.int2x3 v)
		{
			return asuint(v.c0) * uint2(3475533443u, 3777095341u) + asuint(v.c1) * uint2(3385463369u, 1773538433u) + asuint(v.c2) * uint2(3773525029u, 4131962539u) + 1809525511u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 int2x4(global::Unity.Mathematics.int2 c0, global::Unity.Mathematics.int2 c1, global::Unity.Mathematics.int2 c2, global::Unity.Mathematics.int2 c3)
		{
			return new global::Unity.Mathematics.int2x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 int2x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13)
		{
			return new global::Unity.Mathematics.int2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 int2x4(int v)
		{
			return new global::Unity.Mathematics.int2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 int2x4(bool v)
		{
			return new global::Unity.Mathematics.int2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 int2x4(global::Unity.Mathematics.bool2x4 v)
		{
			return new global::Unity.Mathematics.int2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 int2x4(uint v)
		{
			return new global::Unity.Mathematics.int2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 int2x4(global::Unity.Mathematics.uint2x4 v)
		{
			return new global::Unity.Mathematics.int2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 int2x4(float v)
		{
			return new global::Unity.Mathematics.int2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 int2x4(global::Unity.Mathematics.float2x4 v)
		{
			return new global::Unity.Mathematics.int2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 int2x4(double v)
		{
			return new global::Unity.Mathematics.int2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 int2x4(global::Unity.Mathematics.double2x4 v)
		{
			return new global::Unity.Mathematics.int2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 transpose(global::Unity.Mathematics.int2x4 v)
		{
			return int4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int2x4 v)
		{
			return csum(asuint(v.c0) * uint2(2057338067u, 2942577577u) + asuint(v.c1) * uint2(2834440507u, 2671762487u) + asuint(v.c2) * uint2(2892026051u, 2455987759u) + asuint(v.c3) * uint2(3868600063u, 3170963179u)) + 2632835537u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.int2x4 v)
		{
			return asuint(v.c0) * uint2(1136528209u, 2944626401u) + asuint(v.c1) * uint2(2972762423u, 1417889653u) + asuint(v.c2) * uint2(2080514593u, 2731544287u) + asuint(v.c3) * uint2(2828498809u, 2669441947u) + 1260114311u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(int x, int y, int z)
		{
			return new global::Unity.Mathematics.int3(x, y, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(int x, global::Unity.Mathematics.int2 yz)
		{
			return new global::Unity.Mathematics.int3(x, yz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(global::Unity.Mathematics.int2 xy, int z)
		{
			return new global::Unity.Mathematics.int3(xy, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(global::Unity.Mathematics.int3 xyz)
		{
			return new global::Unity.Mathematics.int3(xyz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(int v)
		{
			return new global::Unity.Mathematics.int3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(bool v)
		{
			return new global::Unity.Mathematics.int3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(global::Unity.Mathematics.bool3 v)
		{
			return new global::Unity.Mathematics.int3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(uint v)
		{
			return new global::Unity.Mathematics.int3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(global::Unity.Mathematics.uint3 v)
		{
			return new global::Unity.Mathematics.int3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(float v)
		{
			return new global::Unity.Mathematics.int3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(global::Unity.Mathematics.float3 v)
		{
			return new global::Unity.Mathematics.int3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(double v)
		{
			return new global::Unity.Mathematics.int3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 int3(global::Unity.Mathematics.double3 v)
		{
			return new global::Unity.Mathematics.int3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int3 v)
		{
			return csum(asuint(v) * uint3(1283419601u, 1210229737u, 2864955997u)) + 3525118277u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.int3 v)
		{
			return asuint(v) * uint3(2298260269u, 1632478733u, 1537393931u) + 2353355467u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int shuffle(global::Unity.Mathematics.int3 left, global::Unity.Mathematics.int3 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 shuffle(global::Unity.Mathematics.int3 left, global::Unity.Mathematics.int3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return int2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 shuffle(global::Unity.Mathematics.int3 left, global::Unity.Mathematics.int3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return int3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 shuffle(global::Unity.Mathematics.int3 left, global::Unity.Mathematics.int3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return int4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static int select_shuffle_component(global::Unity.Mathematics.int3 a, global::Unity.Mathematics.int3 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftZ => a.z, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightZ => b.z, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 int3x2(global::Unity.Mathematics.int3 c0, global::Unity.Mathematics.int3 c1)
		{
			return new global::Unity.Mathematics.int3x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 int3x2(int m00, int m01, int m10, int m11, int m20, int m21)
		{
			return new global::Unity.Mathematics.int3x2(m00, m01, m10, m11, m20, m21);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 int3x2(int v)
		{
			return new global::Unity.Mathematics.int3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 int3x2(bool v)
		{
			return new global::Unity.Mathematics.int3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 int3x2(global::Unity.Mathematics.bool3x2 v)
		{
			return new global::Unity.Mathematics.int3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 int3x2(uint v)
		{
			return new global::Unity.Mathematics.int3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 int3x2(global::Unity.Mathematics.uint3x2 v)
		{
			return new global::Unity.Mathematics.int3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 int3x2(float v)
		{
			return new global::Unity.Mathematics.int3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 int3x2(global::Unity.Mathematics.float3x2 v)
		{
			return new global::Unity.Mathematics.int3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 int3x2(double v)
		{
			return new global::Unity.Mathematics.int3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 int3x2(global::Unity.Mathematics.double3x2 v)
		{
			return new global::Unity.Mathematics.int3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 transpose(global::Unity.Mathematics.int3x2 v)
		{
			return int2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int3x2 v)
		{
			return csum(asuint(v.c0) * uint3(3678265601u, 2070747979u, 1480171127u) + asuint(v.c1) * uint3(1588341193u, 4234155257u, 1811310911u)) + 2635799963u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.int3x2 v)
		{
			return asuint(v.c0) * uint3(4165137857u, 2759770933u, 2759319383u) + asuint(v.c1) * uint3(3299952959u, 3121178323u, 2948522579u) + 1531026433u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 int3x3(global::Unity.Mathematics.int3 c0, global::Unity.Mathematics.int3 c1, global::Unity.Mathematics.int3 c2)
		{
			return new global::Unity.Mathematics.int3x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 int3x3(int m00, int m01, int m02, int m10, int m11, int m12, int m20, int m21, int m22)
		{
			return new global::Unity.Mathematics.int3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 int3x3(int v)
		{
			return new global::Unity.Mathematics.int3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 int3x3(bool v)
		{
			return new global::Unity.Mathematics.int3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 int3x3(global::Unity.Mathematics.bool3x3 v)
		{
			return new global::Unity.Mathematics.int3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 int3x3(uint v)
		{
			return new global::Unity.Mathematics.int3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 int3x3(global::Unity.Mathematics.uint3x3 v)
		{
			return new global::Unity.Mathematics.int3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 int3x3(float v)
		{
			return new global::Unity.Mathematics.int3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 int3x3(global::Unity.Mathematics.float3x3 v)
		{
			return new global::Unity.Mathematics.int3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 int3x3(double v)
		{
			return new global::Unity.Mathematics.int3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 int3x3(global::Unity.Mathematics.double3x3 v)
		{
			return new global::Unity.Mathematics.int3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 transpose(global::Unity.Mathematics.int3x3 v)
		{
			return int3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int determinant(global::Unity.Mathematics.int3x3 m)
		{
			global::Unity.Mathematics.int3 c = m.c0;
			global::Unity.Mathematics.int3 c2 = m.c1;
			global::Unity.Mathematics.int3 c3 = m.c2;
			int num = c2.y * c3.z - c2.z * c3.y;
			int num2 = c.y * c3.z - c.z * c3.y;
			int num3 = c.y * c2.z - c.z * c2.y;
			return c.x * num - c2.x * num2 + c3.x * num3;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int3x3 v)
		{
			return csum(asuint(v.c0) * uint3(2479033387u, 3702457169u, 1845824257u) + asuint(v.c1) * uint3(1963973621u, 2134758553u, 1391111867u) + asuint(v.c2) * uint3(1167706003u, 2209736489u, 3261535807u)) + 1740411209;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.int3x3 v)
		{
			return asuint(v.c0) * uint3(2910609089u, 2183822701u, 3029516053u) + asuint(v.c1) * uint3(3547472099u, 2057487037u, 3781937309u) + asuint(v.c2) * uint3(2057338067u, 2942577577u, 2834440507u) + 2671762487u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 int3x4(global::Unity.Mathematics.int3 c0, global::Unity.Mathematics.int3 c1, global::Unity.Mathematics.int3 c2, global::Unity.Mathematics.int3 c3)
		{
			return new global::Unity.Mathematics.int3x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 int3x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13, int m20, int m21, int m22, int m23)
		{
			return new global::Unity.Mathematics.int3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 int3x4(int v)
		{
			return new global::Unity.Mathematics.int3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 int3x4(bool v)
		{
			return new global::Unity.Mathematics.int3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 int3x4(global::Unity.Mathematics.bool3x4 v)
		{
			return new global::Unity.Mathematics.int3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 int3x4(uint v)
		{
			return new global::Unity.Mathematics.int3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 int3x4(global::Unity.Mathematics.uint3x4 v)
		{
			return new global::Unity.Mathematics.int3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 int3x4(float v)
		{
			return new global::Unity.Mathematics.int3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 int3x4(global::Unity.Mathematics.float3x4 v)
		{
			return new global::Unity.Mathematics.int3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 int3x4(double v)
		{
			return new global::Unity.Mathematics.int3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 int3x4(global::Unity.Mathematics.double3x4 v)
		{
			return new global::Unity.Mathematics.int3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 transpose(global::Unity.Mathematics.int3x4 v)
		{
			return int4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int3x4 v)
		{
			return csum(asuint(v.c0) * uint3(1521739981u, 1735296007u, 3010324327u) + asuint(v.c1) * uint3(1875523709u, 2937008387u, 3835713223u) + asuint(v.c2) * uint3(2216526373u, 3375971453u, 3559829411u) + asuint(v.c3) * uint3(3652178029u, 2544260129u, 2013864031u)) + 2627668003u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.int3x4 v)
		{
			return asuint(v.c0) * uint3(1520214331u, 2949502447u, 2827819133u) + asuint(v.c1) * uint3(3480140317u, 2642994593u, 3940484981u) + asuint(v.c2) * uint3(1954192763u, 1091696537u, 3052428017u) + asuint(v.c3) * uint3(4253034763u, 2338696631u, 3757372771u) + 1885959949u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(int x, int y, int z, int w)
		{
			return new global::Unity.Mathematics.int4(x, y, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(int x, int y, global::Unity.Mathematics.int2 zw)
		{
			return new global::Unity.Mathematics.int4(x, y, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(int x, global::Unity.Mathematics.int2 yz, int w)
		{
			return new global::Unity.Mathematics.int4(x, yz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(int x, global::Unity.Mathematics.int3 yzw)
		{
			return new global::Unity.Mathematics.int4(x, yzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(global::Unity.Mathematics.int2 xy, int z, int w)
		{
			return new global::Unity.Mathematics.int4(xy, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(global::Unity.Mathematics.int2 xy, global::Unity.Mathematics.int2 zw)
		{
			return new global::Unity.Mathematics.int4(xy, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(global::Unity.Mathematics.int3 xyz, int w)
		{
			return new global::Unity.Mathematics.int4(xyz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(global::Unity.Mathematics.int4 xyzw)
		{
			return new global::Unity.Mathematics.int4(xyzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(int v)
		{
			return new global::Unity.Mathematics.int4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(bool v)
		{
			return new global::Unity.Mathematics.int4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(global::Unity.Mathematics.bool4 v)
		{
			return new global::Unity.Mathematics.int4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(uint v)
		{
			return new global::Unity.Mathematics.int4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(global::Unity.Mathematics.uint4 v)
		{
			return new global::Unity.Mathematics.int4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(float v)
		{
			return new global::Unity.Mathematics.int4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(global::Unity.Mathematics.float4 v)
		{
			return new global::Unity.Mathematics.int4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(double v)
		{
			return new global::Unity.Mathematics.int4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 int4(global::Unity.Mathematics.double4 v)
		{
			return new global::Unity.Mathematics.int4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int4 v)
		{
			return csum(asuint(v) * uint4(1845824257u, 1963973621u, 2134758553u, 1391111867u)) + 1167706003;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.int4 v)
		{
			return asuint(v) * uint4(2209736489u, 3261535807u, 1740411209u, 2910609089u) + 2183822701u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int shuffle(global::Unity.Mathematics.int4 left, global::Unity.Mathematics.int4 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 shuffle(global::Unity.Mathematics.int4 left, global::Unity.Mathematics.int4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return int2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 shuffle(global::Unity.Mathematics.int4 left, global::Unity.Mathematics.int4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return int3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 shuffle(global::Unity.Mathematics.int4 left, global::Unity.Mathematics.int4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return int4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static int select_shuffle_component(global::Unity.Mathematics.int4 a, global::Unity.Mathematics.int4 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftZ => a.z, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftW => a.w, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightZ => b.z, 
				global::Unity.Mathematics.math.ShuffleComponent.RightW => b.w, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 int4x2(global::Unity.Mathematics.int4 c0, global::Unity.Mathematics.int4 c1)
		{
			return new global::Unity.Mathematics.int4x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 int4x2(int m00, int m01, int m10, int m11, int m20, int m21, int m30, int m31)
		{
			return new global::Unity.Mathematics.int4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 int4x2(int v)
		{
			return new global::Unity.Mathematics.int4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 int4x2(bool v)
		{
			return new global::Unity.Mathematics.int4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 int4x2(global::Unity.Mathematics.bool4x2 v)
		{
			return new global::Unity.Mathematics.int4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 int4x2(uint v)
		{
			return new global::Unity.Mathematics.int4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 int4x2(global::Unity.Mathematics.uint4x2 v)
		{
			return new global::Unity.Mathematics.int4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 int4x2(float v)
		{
			return new global::Unity.Mathematics.int4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 int4x2(global::Unity.Mathematics.float4x2 v)
		{
			return new global::Unity.Mathematics.int4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 int4x2(double v)
		{
			return new global::Unity.Mathematics.int4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 int4x2(global::Unity.Mathematics.double4x2 v)
		{
			return new global::Unity.Mathematics.int4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 transpose(global::Unity.Mathematics.int4x2 v)
		{
			return int2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int4x2 v)
		{
			return csum(asuint(v.c0) * uint4(4205774813u, 1650214333u, 3388112843u, 1831150513u) + asuint(v.c1) * uint4(1848374953u, 3430200247u, 2209710647u, 2201894441u)) + 2849577407u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.int4x2 v)
		{
			return asuint(v.c0) * uint4(3287031191u, 3098675399u, 1564399943u, 1148435377u) + asuint(v.c1) * uint4(3416333663u, 1750611407u, 3285396193u, 3110507567u) + 4271396531u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 int4x3(global::Unity.Mathematics.int4 c0, global::Unity.Mathematics.int4 c1, global::Unity.Mathematics.int4 c2)
		{
			return new global::Unity.Mathematics.int4x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 int4x3(int m00, int m01, int m02, int m10, int m11, int m12, int m20, int m21, int m22, int m30, int m31, int m32)
		{
			return new global::Unity.Mathematics.int4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 int4x3(int v)
		{
			return new global::Unity.Mathematics.int4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 int4x3(bool v)
		{
			return new global::Unity.Mathematics.int4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 int4x3(global::Unity.Mathematics.bool4x3 v)
		{
			return new global::Unity.Mathematics.int4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 int4x3(uint v)
		{
			return new global::Unity.Mathematics.int4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 int4x3(global::Unity.Mathematics.uint4x3 v)
		{
			return new global::Unity.Mathematics.int4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 int4x3(float v)
		{
			return new global::Unity.Mathematics.int4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 int4x3(global::Unity.Mathematics.float4x3 v)
		{
			return new global::Unity.Mathematics.int4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 int4x3(double v)
		{
			return new global::Unity.Mathematics.int4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 int4x3(global::Unity.Mathematics.double4x3 v)
		{
			return new global::Unity.Mathematics.int4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 transpose(global::Unity.Mathematics.int4x3 v)
		{
			return int3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int4x3 v)
		{
			return csum(asuint(v.c0) * uint4(1773538433u, 3773525029u, 4131962539u, 1809525511u) + asuint(v.c1) * uint4(4016293529u, 2416021567u, 2828384717u, 2636362241u) + asuint(v.c2) * uint4(1258410977u, 1952565773u, 2037535609u, 3592785499u)) + 3996716183u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.int4x3 v)
		{
			return asuint(v.c0) * uint4(2626301701u, 1306289417u, 2096137163u, 1548578029u) + asuint(v.c1) * uint4(4178800919u, 3898072289u, 4129428421u, 2631575897u) + asuint(v.c2) * uint4(2854656703u, 3578504047u, 4245178297u, 2173281923u) + 2973357649u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 int4x4(global::Unity.Mathematics.int4 c0, global::Unity.Mathematics.int4 c1, global::Unity.Mathematics.int4 c2, global::Unity.Mathematics.int4 c3)
		{
			return new global::Unity.Mathematics.int4x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 int4x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13, int m20, int m21, int m22, int m23, int m30, int m31, int m32, int m33)
		{
			return new global::Unity.Mathematics.int4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 int4x4(int v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 int4x4(bool v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 int4x4(global::Unity.Mathematics.bool4x4 v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 int4x4(uint v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 int4x4(global::Unity.Mathematics.uint4x4 v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 int4x4(float v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 int4x4(global::Unity.Mathematics.float4x4 v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 int4x4(double v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 int4x4(global::Unity.Mathematics.double4x4 v)
		{
			return new global::Unity.Mathematics.int4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 transpose(global::Unity.Mathematics.int4x4 v)
		{
			return int4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		public static int determinant(global::Unity.Mathematics.int4x4 m)
		{
			global::Unity.Mathematics.int4 c = m.c0;
			global::Unity.Mathematics.int4 c2 = m.c1;
			global::Unity.Mathematics.int4 c3 = m.c2;
			global::Unity.Mathematics.int4 c4 = m.c3;
			int num = c2.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c2.z * c4.w - c2.w * c4.z) + c4.y * (c2.z * c3.w - c2.w * c3.z);
			int num2 = c.y * (c3.z * c4.w - c3.w * c4.z) - c3.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c3.w - c.w * c3.z);
			int num3 = c.y * (c2.z * c4.w - c2.w * c4.z) - c2.y * (c.z * c4.w - c.w * c4.z) + c4.y * (c.z * c2.w - c.w * c2.z);
			int num4 = c.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c.z * c3.w - c.w * c3.z) + c3.y * (c.z * c2.w - c.w * c2.z);
			return c.x * num - c2.x * num2 + c3.x * num3 - c4.x * num4;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.int4x4 v)
		{
			return csum(asuint(v.c0) * uint4(1562056283u, 2265541847u, 1283419601u, 1210229737u) + asuint(v.c1) * uint4(2864955997u, 3525118277u, 2298260269u, 1632478733u) + asuint(v.c2) * uint4(1537393931u, 2353355467u, 3441847433u, 4052036147u) + asuint(v.c3) * uint4(2011389559u, 2252224297u, 3784421429u, 1750626223u)) + 3571447507u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.int4x4 v)
		{
			return asuint(v.c0) * uint4(3412283213u, 2601761069u, 1254033427u, 2248573027u) + asuint(v.c1) * uint4(3612677113u, 1521739981u, 1735296007u, 3010324327u) + asuint(v.c2) * uint4(1875523709u, 2937008387u, 3835713223u, 2216526373u) + asuint(v.c3) * uint4(3375971453u, 3559829411u, 3652178029u, 2544260129u) + 2013864031u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int asint(uint x)
		{
			return (int)x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.int2 asint(global::Unity.Mathematics.uint2 x)
		{
			return *(global::Unity.Mathematics.int2*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.int3 asint(global::Unity.Mathematics.uint3 x)
		{
			return *(global::Unity.Mathematics.int3*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.int4 asint(global::Unity.Mathematics.uint4 x)
		{
			return *(global::Unity.Mathematics.int4*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static int asint(float x)
		{
			return *(int*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.int2 asint(global::Unity.Mathematics.float2 x)
		{
			return *(global::Unity.Mathematics.int2*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.int3 asint(global::Unity.Mathematics.float3 x)
		{
			return *(global::Unity.Mathematics.int3*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.int4 asint(global::Unity.Mathematics.float4 x)
		{
			return *(global::Unity.Mathematics.int4*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint asuint(int x)
		{
			return (uint)x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.uint2 asuint(global::Unity.Mathematics.int2 x)
		{
			return *(global::Unity.Mathematics.uint2*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.uint3 asuint(global::Unity.Mathematics.int3 x)
		{
			return *(global::Unity.Mathematics.uint3*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.uint4 asuint(global::Unity.Mathematics.int4 x)
		{
			return *(global::Unity.Mathematics.uint4*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static uint asuint(float x)
		{
			return *(uint*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.uint2 asuint(global::Unity.Mathematics.float2 x)
		{
			return *(global::Unity.Mathematics.uint2*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.uint3 asuint(global::Unity.Mathematics.float3 x)
		{
			return *(global::Unity.Mathematics.uint3*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.uint4 asuint(global::Unity.Mathematics.float4 x)
		{
			return *(global::Unity.Mathematics.uint4*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long aslong(ulong x)
		{
			return (long)x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static long aslong(double x)
		{
			return *(long*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ulong asulong(long x)
		{
			return (ulong)x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong asulong(double x)
		{
			return *(ulong*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static float asfloat(int x)
		{
			return *(float*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.float2 asfloat(global::Unity.Mathematics.int2 x)
		{
			return *(global::Unity.Mathematics.float2*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.float3 asfloat(global::Unity.Mathematics.int3 x)
		{
			return *(global::Unity.Mathematics.float3*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.float4 asfloat(global::Unity.Mathematics.int4 x)
		{
			return *(global::Unity.Mathematics.float4*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static float asfloat(uint x)
		{
			return *(float*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.float2 asfloat(global::Unity.Mathematics.uint2 x)
		{
			return *(global::Unity.Mathematics.float2*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.float3 asfloat(global::Unity.Mathematics.uint3 x)
		{
			return *(global::Unity.Mathematics.float3*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static global::Unity.Mathematics.float4 asfloat(global::Unity.Mathematics.uint4 x)
		{
			return *(global::Unity.Mathematics.float4*)(&x);
		}

		public static int bitmask(global::Unity.Mathematics.bool4 value)
		{
			int num = 0;
			if (value.x)
			{
				num |= 1;
			}
			if (value.y)
			{
				num |= 2;
			}
			if (value.z)
			{
				num |= 4;
			}
			if (value.w)
			{
				num |= 8;
			}
			return num;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static double asdouble(long x)
		{
			return *(double*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static double asdouble(ulong x)
		{
			return *(double*)(&x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool isfinite(float x)
		{
			return abs(x) < float.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 isfinite(global::Unity.Mathematics.float2 x)
		{
			return abs(x) < float.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 isfinite(global::Unity.Mathematics.float3 x)
		{
			return abs(x) < float.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 isfinite(global::Unity.Mathematics.float4 x)
		{
			return abs(x) < float.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool isfinite(double x)
		{
			return abs(x) < double.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 isfinite(global::Unity.Mathematics.double2 x)
		{
			return abs(x) < double.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 isfinite(global::Unity.Mathematics.double3 x)
		{
			return abs(x) < double.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 isfinite(global::Unity.Mathematics.double4 x)
		{
			return abs(x) < double.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool isinf(float x)
		{
			return abs(x) == float.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 isinf(global::Unity.Mathematics.float2 x)
		{
			return abs(x) == float.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 isinf(global::Unity.Mathematics.float3 x)
		{
			return abs(x) == float.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 isinf(global::Unity.Mathematics.float4 x)
		{
			return abs(x) == float.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool isinf(double x)
		{
			return abs(x) == double.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 isinf(global::Unity.Mathematics.double2 x)
		{
			return abs(x) == double.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 isinf(global::Unity.Mathematics.double3 x)
		{
			return abs(x) == double.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 isinf(global::Unity.Mathematics.double4 x)
		{
			return abs(x) == double.PositiveInfinity;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool isnan(float x)
		{
			return (asuint(x) & 0x7FFFFFFF) > 2139095040;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 isnan(global::Unity.Mathematics.float2 x)
		{
			return (asuint(x) & 2147483647u) > 2139095040u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 isnan(global::Unity.Mathematics.float3 x)
		{
			return (asuint(x) & 2147483647u) > 2139095040u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 isnan(global::Unity.Mathematics.float4 x)
		{
			return (asuint(x) & 2147483647u) > 2139095040u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool isnan(double x)
		{
			return (asulong(x) & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312L;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 isnan(global::Unity.Mathematics.double2 x)
		{
			return bool2((asulong(x.x) & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312L, (asulong(x.y) & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312L);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 isnan(global::Unity.Mathematics.double3 x)
		{
			return bool3((asulong(x.x) & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312L, (asulong(x.y) & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312L, (asulong(x.z) & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312L);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 isnan(global::Unity.Mathematics.double4 x)
		{
			return bool4((asulong(x.x) & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312L, (asulong(x.y) & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312L, (asulong(x.z) & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312L, (asulong(x.w) & 0x7FFFFFFFFFFFFFFFL) > 9218868437227405312L);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool ispow2(int x)
		{
			if (x > 0)
			{
				return (x & (x - 1)) == 0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 ispow2(global::Unity.Mathematics.int2 x)
		{
			return new global::Unity.Mathematics.bool2(ispow2(x.x), ispow2(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 ispow2(global::Unity.Mathematics.int3 x)
		{
			return new global::Unity.Mathematics.bool3(ispow2(x.x), ispow2(x.y), ispow2(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 ispow2(global::Unity.Mathematics.int4 x)
		{
			return new global::Unity.Mathematics.bool4(ispow2(x.x), ispow2(x.y), ispow2(x.z), ispow2(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool ispow2(uint x)
		{
			if (x != 0)
			{
				return (x & (x - 1)) == 0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool2 ispow2(global::Unity.Mathematics.uint2 x)
		{
			return new global::Unity.Mathematics.bool2(ispow2(x.x), ispow2(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool3 ispow2(global::Unity.Mathematics.uint3 x)
		{
			return new global::Unity.Mathematics.bool3(ispow2(x.x), ispow2(x.y), ispow2(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.bool4 ispow2(global::Unity.Mathematics.uint4 x)
		{
			return new global::Unity.Mathematics.bool4(ispow2(x.x), ispow2(x.y), ispow2(x.z), ispow2(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int min(int x, int y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 min(global::Unity.Mathematics.int2 x, global::Unity.Mathematics.int2 y)
		{
			return new global::Unity.Mathematics.int2(min(x.x, y.x), min(x.y, y.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 min(global::Unity.Mathematics.int3 x, global::Unity.Mathematics.int3 y)
		{
			return new global::Unity.Mathematics.int3(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 min(global::Unity.Mathematics.int4 x, global::Unity.Mathematics.int4 y)
		{
			return new global::Unity.Mathematics.int4(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z), min(x.w, y.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint min(uint x, uint y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 min(global::Unity.Mathematics.uint2 x, global::Unity.Mathematics.uint2 y)
		{
			return new global::Unity.Mathematics.uint2(min(x.x, y.x), min(x.y, y.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 min(global::Unity.Mathematics.uint3 x, global::Unity.Mathematics.uint3 y)
		{
			return new global::Unity.Mathematics.uint3(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 min(global::Unity.Mathematics.uint4 x, global::Unity.Mathematics.uint4 y)
		{
			return new global::Unity.Mathematics.uint4(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z), min(x.w, y.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long min(long x, long y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ulong min(ulong x, ulong y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float min(float x, float y)
		{
			if (!float.IsNaN(y) && !(x < y))
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 min(global::Unity.Mathematics.float2 x, global::Unity.Mathematics.float2 y)
		{
			return new global::Unity.Mathematics.float2(min(x.x, y.x), min(x.y, y.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 min(global::Unity.Mathematics.float3 x, global::Unity.Mathematics.float3 y)
		{
			return new global::Unity.Mathematics.float3(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 min(global::Unity.Mathematics.float4 x, global::Unity.Mathematics.float4 y)
		{
			return new global::Unity.Mathematics.float4(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z), min(x.w, y.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double min(double x, double y)
		{
			if (!double.IsNaN(y) && !(x < y))
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 min(global::Unity.Mathematics.double2 x, global::Unity.Mathematics.double2 y)
		{
			return new global::Unity.Mathematics.double2(min(x.x, y.x), min(x.y, y.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 min(global::Unity.Mathematics.double3 x, global::Unity.Mathematics.double3 y)
		{
			return new global::Unity.Mathematics.double3(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 min(global::Unity.Mathematics.double4 x, global::Unity.Mathematics.double4 y)
		{
			return new global::Unity.Mathematics.double4(min(x.x, y.x), min(x.y, y.y), min(x.z, y.z), min(x.w, y.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int max(int x, int y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 max(global::Unity.Mathematics.int2 x, global::Unity.Mathematics.int2 y)
		{
			return new global::Unity.Mathematics.int2(max(x.x, y.x), max(x.y, y.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 max(global::Unity.Mathematics.int3 x, global::Unity.Mathematics.int3 y)
		{
			return new global::Unity.Mathematics.int3(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 max(global::Unity.Mathematics.int4 x, global::Unity.Mathematics.int4 y)
		{
			return new global::Unity.Mathematics.int4(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z), max(x.w, y.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint max(uint x, uint y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 max(global::Unity.Mathematics.uint2 x, global::Unity.Mathematics.uint2 y)
		{
			return new global::Unity.Mathematics.uint2(max(x.x, y.x), max(x.y, y.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 max(global::Unity.Mathematics.uint3 x, global::Unity.Mathematics.uint3 y)
		{
			return new global::Unity.Mathematics.uint3(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 max(global::Unity.Mathematics.uint4 x, global::Unity.Mathematics.uint4 y)
		{
			return new global::Unity.Mathematics.uint4(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z), max(x.w, y.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long max(long x, long y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ulong max(ulong x, ulong y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float max(float x, float y)
		{
			if (!float.IsNaN(y) && !(x > y))
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 max(global::Unity.Mathematics.float2 x, global::Unity.Mathematics.float2 y)
		{
			return new global::Unity.Mathematics.float2(max(x.x, y.x), max(x.y, y.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 max(global::Unity.Mathematics.float3 x, global::Unity.Mathematics.float3 y)
		{
			return new global::Unity.Mathematics.float3(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 max(global::Unity.Mathematics.float4 x, global::Unity.Mathematics.float4 y)
		{
			return new global::Unity.Mathematics.float4(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z), max(x.w, y.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double max(double x, double y)
		{
			if (!double.IsNaN(y) && !(x > y))
			{
				return y;
			}
			return x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 max(global::Unity.Mathematics.double2 x, global::Unity.Mathematics.double2 y)
		{
			return new global::Unity.Mathematics.double2(max(x.x, y.x), max(x.y, y.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 max(global::Unity.Mathematics.double3 x, global::Unity.Mathematics.double3 y)
		{
			return new global::Unity.Mathematics.double3(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 max(global::Unity.Mathematics.double4 x, global::Unity.Mathematics.double4 y)
		{
			return new global::Unity.Mathematics.double4(max(x.x, y.x), max(x.y, y.y), max(x.z, y.z), max(x.w, y.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float lerp(float start, float end, float t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 lerp(global::Unity.Mathematics.float2 start, global::Unity.Mathematics.float2 end, float t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 lerp(global::Unity.Mathematics.float3 start, global::Unity.Mathematics.float3 end, float t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 lerp(global::Unity.Mathematics.float4 start, global::Unity.Mathematics.float4 end, float t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 lerp(global::Unity.Mathematics.float2 start, global::Unity.Mathematics.float2 end, global::Unity.Mathematics.float2 t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 lerp(global::Unity.Mathematics.float3 start, global::Unity.Mathematics.float3 end, global::Unity.Mathematics.float3 t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 lerp(global::Unity.Mathematics.float4 start, global::Unity.Mathematics.float4 end, global::Unity.Mathematics.float4 t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double lerp(double start, double end, double t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 lerp(global::Unity.Mathematics.double2 start, global::Unity.Mathematics.double2 end, double t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 lerp(global::Unity.Mathematics.double3 start, global::Unity.Mathematics.double3 end, double t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 lerp(global::Unity.Mathematics.double4 start, global::Unity.Mathematics.double4 end, double t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 lerp(global::Unity.Mathematics.double2 start, global::Unity.Mathematics.double2 end, global::Unity.Mathematics.double2 t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 lerp(global::Unity.Mathematics.double3 start, global::Unity.Mathematics.double3 end, global::Unity.Mathematics.double3 t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 lerp(global::Unity.Mathematics.double4 start, global::Unity.Mathematics.double4 end, global::Unity.Mathematics.double4 t)
		{
			return start + t * (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float unlerp(float start, float end, float x)
		{
			return (x - start) / (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 unlerp(global::Unity.Mathematics.float2 start, global::Unity.Mathematics.float2 end, global::Unity.Mathematics.float2 x)
		{
			return (x - start) / (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 unlerp(global::Unity.Mathematics.float3 start, global::Unity.Mathematics.float3 end, global::Unity.Mathematics.float3 x)
		{
			return (x - start) / (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 unlerp(global::Unity.Mathematics.float4 start, global::Unity.Mathematics.float4 end, global::Unity.Mathematics.float4 x)
		{
			return (x - start) / (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double unlerp(double start, double end, double x)
		{
			return (x - start) / (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 unlerp(global::Unity.Mathematics.double2 start, global::Unity.Mathematics.double2 end, global::Unity.Mathematics.double2 x)
		{
			return (x - start) / (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 unlerp(global::Unity.Mathematics.double3 start, global::Unity.Mathematics.double3 end, global::Unity.Mathematics.double3 x)
		{
			return (x - start) / (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 unlerp(global::Unity.Mathematics.double4 start, global::Unity.Mathematics.double4 end, global::Unity.Mathematics.double4 x)
		{
			return (x - start) / (end - start);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float remap(float srcStart, float srcEnd, float dstStart, float dstEnd, float x)
		{
			return lerp(dstStart, dstEnd, unlerp(srcStart, srcEnd, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 remap(global::Unity.Mathematics.float2 srcStart, global::Unity.Mathematics.float2 srcEnd, global::Unity.Mathematics.float2 dstStart, global::Unity.Mathematics.float2 dstEnd, global::Unity.Mathematics.float2 x)
		{
			return lerp(dstStart, dstEnd, unlerp(srcStart, srcEnd, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 remap(global::Unity.Mathematics.float3 srcStart, global::Unity.Mathematics.float3 srcEnd, global::Unity.Mathematics.float3 dstStart, global::Unity.Mathematics.float3 dstEnd, global::Unity.Mathematics.float3 x)
		{
			return lerp(dstStart, dstEnd, unlerp(srcStart, srcEnd, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 remap(global::Unity.Mathematics.float4 srcStart, global::Unity.Mathematics.float4 srcEnd, global::Unity.Mathematics.float4 dstStart, global::Unity.Mathematics.float4 dstEnd, global::Unity.Mathematics.float4 x)
		{
			return lerp(dstStart, dstEnd, unlerp(srcStart, srcEnd, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double remap(double srcStart, double srcEnd, double dstStart, double dstEnd, double x)
		{
			return lerp(dstStart, dstEnd, unlerp(srcStart, srcEnd, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 remap(global::Unity.Mathematics.double2 srcStart, global::Unity.Mathematics.double2 srcEnd, global::Unity.Mathematics.double2 dstStart, global::Unity.Mathematics.double2 dstEnd, global::Unity.Mathematics.double2 x)
		{
			return lerp(dstStart, dstEnd, unlerp(srcStart, srcEnd, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 remap(global::Unity.Mathematics.double3 srcStart, global::Unity.Mathematics.double3 srcEnd, global::Unity.Mathematics.double3 dstStart, global::Unity.Mathematics.double3 dstEnd, global::Unity.Mathematics.double3 x)
		{
			return lerp(dstStart, dstEnd, unlerp(srcStart, srcEnd, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 remap(global::Unity.Mathematics.double4 srcStart, global::Unity.Mathematics.double4 srcEnd, global::Unity.Mathematics.double4 dstStart, global::Unity.Mathematics.double4 dstEnd, global::Unity.Mathematics.double4 x)
		{
			return lerp(dstStart, dstEnd, unlerp(srcStart, srcEnd, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int mad(int mulA, int mulB, int addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 mad(global::Unity.Mathematics.int2 mulA, global::Unity.Mathematics.int2 mulB, global::Unity.Mathematics.int2 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 mad(global::Unity.Mathematics.int3 mulA, global::Unity.Mathematics.int3 mulB, global::Unity.Mathematics.int3 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 mad(global::Unity.Mathematics.int4 mulA, global::Unity.Mathematics.int4 mulB, global::Unity.Mathematics.int4 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint mad(uint mulA, uint mulB, uint addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 mad(global::Unity.Mathematics.uint2 mulA, global::Unity.Mathematics.uint2 mulB, global::Unity.Mathematics.uint2 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 mad(global::Unity.Mathematics.uint3 mulA, global::Unity.Mathematics.uint3 mulB, global::Unity.Mathematics.uint3 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 mad(global::Unity.Mathematics.uint4 mulA, global::Unity.Mathematics.uint4 mulB, global::Unity.Mathematics.uint4 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long mad(long mulA, long mulB, long addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ulong mad(ulong mulA, ulong mulB, ulong addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float mad(float mulA, float mulB, float addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 mad(global::Unity.Mathematics.float2 mulA, global::Unity.Mathematics.float2 mulB, global::Unity.Mathematics.float2 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 mad(global::Unity.Mathematics.float3 mulA, global::Unity.Mathematics.float3 mulB, global::Unity.Mathematics.float3 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 mad(global::Unity.Mathematics.float4 mulA, global::Unity.Mathematics.float4 mulB, global::Unity.Mathematics.float4 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double mad(double mulA, double mulB, double addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 mad(global::Unity.Mathematics.double2 mulA, global::Unity.Mathematics.double2 mulB, global::Unity.Mathematics.double2 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 mad(global::Unity.Mathematics.double3 mulA, global::Unity.Mathematics.double3 mulB, global::Unity.Mathematics.double3 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 mad(global::Unity.Mathematics.double4 mulA, global::Unity.Mathematics.double4 mulB, global::Unity.Mathematics.double4 addC)
		{
			return mulA * mulB + addC;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int clamp(int valueToClamp, int lowerBound, int upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 clamp(global::Unity.Mathematics.int2 valueToClamp, global::Unity.Mathematics.int2 lowerBound, global::Unity.Mathematics.int2 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 clamp(global::Unity.Mathematics.int3 valueToClamp, global::Unity.Mathematics.int3 lowerBound, global::Unity.Mathematics.int3 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 clamp(global::Unity.Mathematics.int4 valueToClamp, global::Unity.Mathematics.int4 lowerBound, global::Unity.Mathematics.int4 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint clamp(uint valueToClamp, uint lowerBound, uint upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 clamp(global::Unity.Mathematics.uint2 valueToClamp, global::Unity.Mathematics.uint2 lowerBound, global::Unity.Mathematics.uint2 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 clamp(global::Unity.Mathematics.uint3 valueToClamp, global::Unity.Mathematics.uint3 lowerBound, global::Unity.Mathematics.uint3 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 clamp(global::Unity.Mathematics.uint4 valueToClamp, global::Unity.Mathematics.uint4 lowerBound, global::Unity.Mathematics.uint4 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long clamp(long valueToClamp, long lowerBound, long upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ulong clamp(ulong valueToClamp, ulong lowerBound, ulong upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float clamp(float valueToClamp, float lowerBound, float upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 clamp(global::Unity.Mathematics.float2 valueToClamp, global::Unity.Mathematics.float2 lowerBound, global::Unity.Mathematics.float2 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 clamp(global::Unity.Mathematics.float3 valueToClamp, global::Unity.Mathematics.float3 lowerBound, global::Unity.Mathematics.float3 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 clamp(global::Unity.Mathematics.float4 valueToClamp, global::Unity.Mathematics.float4 lowerBound, global::Unity.Mathematics.float4 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double clamp(double valueToClamp, double lowerBound, double upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 clamp(global::Unity.Mathematics.double2 valueToClamp, global::Unity.Mathematics.double2 lowerBound, global::Unity.Mathematics.double2 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 clamp(global::Unity.Mathematics.double3 valueToClamp, global::Unity.Mathematics.double3 lowerBound, global::Unity.Mathematics.double3 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 clamp(global::Unity.Mathematics.double4 valueToClamp, global::Unity.Mathematics.double4 lowerBound, global::Unity.Mathematics.double4 upperBound)
		{
			return max(lowerBound, min(upperBound, valueToClamp));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float saturate(float x)
		{
			return clamp(x, 0f, 1f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 saturate(global::Unity.Mathematics.float2 x)
		{
			return clamp(x, new global::Unity.Mathematics.float2(0f), new global::Unity.Mathematics.float2(1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 saturate(global::Unity.Mathematics.float3 x)
		{
			return clamp(x, new global::Unity.Mathematics.float3(0f), new global::Unity.Mathematics.float3(1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 saturate(global::Unity.Mathematics.float4 x)
		{
			return clamp(x, new global::Unity.Mathematics.float4(0f), new global::Unity.Mathematics.float4(1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double saturate(double x)
		{
			return clamp(x, 0.0, 1.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 saturate(global::Unity.Mathematics.double2 x)
		{
			return clamp(x, new global::Unity.Mathematics.double2(0.0), new global::Unity.Mathematics.double2(1.0));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 saturate(global::Unity.Mathematics.double3 x)
		{
			return clamp(x, new global::Unity.Mathematics.double3(0.0), new global::Unity.Mathematics.double3(1.0));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 saturate(global::Unity.Mathematics.double4 x)
		{
			return clamp(x, new global::Unity.Mathematics.double4(0.0), new global::Unity.Mathematics.double4(1.0));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int abs(int x)
		{
			return max(-x, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 abs(global::Unity.Mathematics.int2 x)
		{
			return max(-x, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 abs(global::Unity.Mathematics.int3 x)
		{
			return max(-x, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 abs(global::Unity.Mathematics.int4 x)
		{
			return max(-x, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long abs(long x)
		{
			return max(-x, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float abs(float x)
		{
			return asfloat(asuint(x) & 0x7FFFFFFF);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 abs(global::Unity.Mathematics.float2 x)
		{
			return asfloat(asuint(x) & 2147483647u);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 abs(global::Unity.Mathematics.float3 x)
		{
			return asfloat(asuint(x) & 2147483647u);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 abs(global::Unity.Mathematics.float4 x)
		{
			return asfloat(asuint(x) & 2147483647u);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double abs(double x)
		{
			return asdouble(asulong(x) & 0x7FFFFFFFFFFFFFFFL);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 abs(global::Unity.Mathematics.double2 x)
		{
			return double2(asdouble(asulong(x.x) & 0x7FFFFFFFFFFFFFFFL), asdouble(asulong(x.y) & 0x7FFFFFFFFFFFFFFFL));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 abs(global::Unity.Mathematics.double3 x)
		{
			return double3(asdouble(asulong(x.x) & 0x7FFFFFFFFFFFFFFFL), asdouble(asulong(x.y) & 0x7FFFFFFFFFFFFFFFL), asdouble(asulong(x.z) & 0x7FFFFFFFFFFFFFFFL));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 abs(global::Unity.Mathematics.double4 x)
		{
			return double4(asdouble(asulong(x.x) & 0x7FFFFFFFFFFFFFFFL), asdouble(asulong(x.y) & 0x7FFFFFFFFFFFFFFFL), asdouble(asulong(x.z) & 0x7FFFFFFFFFFFFFFFL), asdouble(asulong(x.w) & 0x7FFFFFFFFFFFFFFFL));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int dot(int x, int y)
		{
			return x * y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int dot(global::Unity.Mathematics.int2 x, global::Unity.Mathematics.int2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int dot(global::Unity.Mathematics.int3 x, global::Unity.Mathematics.int3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int dot(global::Unity.Mathematics.int4 x, global::Unity.Mathematics.int4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint dot(uint x, uint y)
		{
			return x * y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint dot(global::Unity.Mathematics.uint2 x, global::Unity.Mathematics.uint2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint dot(global::Unity.Mathematics.uint3 x, global::Unity.Mathematics.uint3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint dot(global::Unity.Mathematics.uint4 x, global::Unity.Mathematics.uint4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float dot(float x, float y)
		{
			return x * y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float dot(global::Unity.Mathematics.float2 x, global::Unity.Mathematics.float2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float dot(global::Unity.Mathematics.float3 x, global::Unity.Mathematics.float3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float dot(global::Unity.Mathematics.float4 x, global::Unity.Mathematics.float4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double dot(double x, double y)
		{
			return x * y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double dot(global::Unity.Mathematics.double2 x, global::Unity.Mathematics.double2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double dot(global::Unity.Mathematics.double3 x, global::Unity.Mathematics.double3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double dot(global::Unity.Mathematics.double4 x, global::Unity.Mathematics.double4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float tan(float x)
		{
			return (float)global::System.Math.Tan(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 tan(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(tan(x.x), tan(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 tan(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(tan(x.x), tan(x.y), tan(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 tan(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(tan(x.x), tan(x.y), tan(x.z), tan(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double tan(double x)
		{
			return global::System.Math.Tan(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 tan(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(tan(x.x), tan(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 tan(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(tan(x.x), tan(x.y), tan(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 tan(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(tan(x.x), tan(x.y), tan(x.z), tan(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float tanh(float x)
		{
			return (float)global::System.Math.Tanh(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 tanh(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(tanh(x.x), tanh(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 tanh(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(tanh(x.x), tanh(x.y), tanh(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 tanh(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(tanh(x.x), tanh(x.y), tanh(x.z), tanh(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double tanh(double x)
		{
			return global::System.Math.Tanh(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 tanh(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(tanh(x.x), tanh(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 tanh(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(tanh(x.x), tanh(x.y), tanh(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 tanh(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(tanh(x.x), tanh(x.y), tanh(x.z), tanh(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float atan(float x)
		{
			return (float)global::System.Math.Atan(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 atan(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(atan(x.x), atan(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 atan(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(atan(x.x), atan(x.y), atan(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 atan(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(atan(x.x), atan(x.y), atan(x.z), atan(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double atan(double x)
		{
			return global::System.Math.Atan(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 atan(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(atan(x.x), atan(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 atan(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(atan(x.x), atan(x.y), atan(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 atan(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(atan(x.x), atan(x.y), atan(x.z), atan(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float atan2(float y, float x)
		{
			return (float)global::System.Math.Atan2(y, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 atan2(global::Unity.Mathematics.float2 y, global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(atan2(y.x, x.x), atan2(y.y, x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 atan2(global::Unity.Mathematics.float3 y, global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(atan2(y.x, x.x), atan2(y.y, x.y), atan2(y.z, x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 atan2(global::Unity.Mathematics.float4 y, global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(atan2(y.x, x.x), atan2(y.y, x.y), atan2(y.z, x.z), atan2(y.w, x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double atan2(double y, double x)
		{
			return global::System.Math.Atan2(y, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 atan2(global::Unity.Mathematics.double2 y, global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(atan2(y.x, x.x), atan2(y.y, x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 atan2(global::Unity.Mathematics.double3 y, global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(atan2(y.x, x.x), atan2(y.y, x.y), atan2(y.z, x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 atan2(global::Unity.Mathematics.double4 y, global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(atan2(y.x, x.x), atan2(y.y, x.y), atan2(y.z, x.z), atan2(y.w, x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float cos(float x)
		{
			return (float)global::System.Math.Cos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 cos(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(cos(x.x), cos(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 cos(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(cos(x.x), cos(x.y), cos(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 cos(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(cos(x.x), cos(x.y), cos(x.z), cos(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double cos(double x)
		{
			return global::System.Math.Cos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 cos(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(cos(x.x), cos(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 cos(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(cos(x.x), cos(x.y), cos(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 cos(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(cos(x.x), cos(x.y), cos(x.z), cos(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float cosh(float x)
		{
			return (float)global::System.Math.Cosh(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 cosh(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(cosh(x.x), cosh(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 cosh(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(cosh(x.x), cosh(x.y), cosh(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 cosh(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(cosh(x.x), cosh(x.y), cosh(x.z), cosh(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double cosh(double x)
		{
			return global::System.Math.Cosh(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 cosh(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(cosh(x.x), cosh(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 cosh(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(cosh(x.x), cosh(x.y), cosh(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 cosh(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(cosh(x.x), cosh(x.y), cosh(x.z), cosh(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float acos(float x)
		{
			return (float)global::System.Math.Acos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 acos(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(acos(x.x), acos(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 acos(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(acos(x.x), acos(x.y), acos(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 acos(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(acos(x.x), acos(x.y), acos(x.z), acos(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double acos(double x)
		{
			return global::System.Math.Acos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 acos(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(acos(x.x), acos(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 acos(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(acos(x.x), acos(x.y), acos(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 acos(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(acos(x.x), acos(x.y), acos(x.z), acos(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float sin(float x)
		{
			return (float)global::System.Math.Sin(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 sin(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(sin(x.x), sin(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 sin(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(sin(x.x), sin(x.y), sin(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 sin(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(sin(x.x), sin(x.y), sin(x.z), sin(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double sin(double x)
		{
			return global::System.Math.Sin(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 sin(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(sin(x.x), sin(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 sin(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(sin(x.x), sin(x.y), sin(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 sin(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(sin(x.x), sin(x.y), sin(x.z), sin(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float sinh(float x)
		{
			return (float)global::System.Math.Sinh(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 sinh(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(sinh(x.x), sinh(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 sinh(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(sinh(x.x), sinh(x.y), sinh(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 sinh(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(sinh(x.x), sinh(x.y), sinh(x.z), sinh(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double sinh(double x)
		{
			return global::System.Math.Sinh(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 sinh(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(sinh(x.x), sinh(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 sinh(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(sinh(x.x), sinh(x.y), sinh(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 sinh(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(sinh(x.x), sinh(x.y), sinh(x.z), sinh(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float asin(float x)
		{
			return (float)global::System.Math.Asin(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 asin(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(asin(x.x), asin(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 asin(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(asin(x.x), asin(x.y), asin(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 asin(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(asin(x.x), asin(x.y), asin(x.z), asin(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double asin(double x)
		{
			return global::System.Math.Asin(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 asin(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(asin(x.x), asin(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 asin(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(asin(x.x), asin(x.y), asin(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 asin(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(asin(x.x), asin(x.y), asin(x.z), asin(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float floor(float x)
		{
			return (float)global::System.Math.Floor(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 floor(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(floor(x.x), floor(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 floor(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(floor(x.x), floor(x.y), floor(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 floor(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(floor(x.x), floor(x.y), floor(x.z), floor(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double floor(double x)
		{
			return global::System.Math.Floor(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 floor(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(floor(x.x), floor(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 floor(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(floor(x.x), floor(x.y), floor(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 floor(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(floor(x.x), floor(x.y), floor(x.z), floor(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float ceil(float x)
		{
			return (float)global::System.Math.Ceiling(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 ceil(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(ceil(x.x), ceil(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 ceil(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(ceil(x.x), ceil(x.y), ceil(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 ceil(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(ceil(x.x), ceil(x.y), ceil(x.z), ceil(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double ceil(double x)
		{
			return global::System.Math.Ceiling(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 ceil(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(ceil(x.x), ceil(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 ceil(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(ceil(x.x), ceil(x.y), ceil(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 ceil(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(ceil(x.x), ceil(x.y), ceil(x.z), ceil(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float round(float x)
		{
			return (float)global::System.Math.Round(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 round(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(round(x.x), round(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 round(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(round(x.x), round(x.y), round(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 round(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(round(x.x), round(x.y), round(x.z), round(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double round(double x)
		{
			return global::System.Math.Round(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 round(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(round(x.x), round(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 round(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(round(x.x), round(x.y), round(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 round(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(round(x.x), round(x.y), round(x.z), round(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float trunc(float x)
		{
			return (float)global::System.Math.Truncate(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 trunc(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(trunc(x.x), trunc(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 trunc(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(trunc(x.x), trunc(x.y), trunc(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 trunc(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(trunc(x.x), trunc(x.y), trunc(x.z), trunc(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double trunc(double x)
		{
			return global::System.Math.Truncate(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 trunc(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(trunc(x.x), trunc(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 trunc(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(trunc(x.x), trunc(x.y), trunc(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 trunc(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(trunc(x.x), trunc(x.y), trunc(x.z), trunc(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float frac(float x)
		{
			return x - floor(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 frac(global::Unity.Mathematics.float2 x)
		{
			return x - floor(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 frac(global::Unity.Mathematics.float3 x)
		{
			return x - floor(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 frac(global::Unity.Mathematics.float4 x)
		{
			return x - floor(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double frac(double x)
		{
			return x - floor(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 frac(global::Unity.Mathematics.double2 x)
		{
			return x - floor(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 frac(global::Unity.Mathematics.double3 x)
		{
			return x - floor(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 frac(global::Unity.Mathematics.double4 x)
		{
			return x - floor(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float rcp(float x)
		{
			return 1f / x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 rcp(global::Unity.Mathematics.float2 x)
		{
			return 1f / x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 rcp(global::Unity.Mathematics.float3 x)
		{
			return 1f / x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 rcp(global::Unity.Mathematics.float4 x)
		{
			return 1f / x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double rcp(double x)
		{
			return 1.0 / x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 rcp(global::Unity.Mathematics.double2 x)
		{
			return 1.0 / x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 rcp(global::Unity.Mathematics.double3 x)
		{
			return 1.0 / x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 rcp(global::Unity.Mathematics.double4 x)
		{
			return 1.0 / x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int sign(int x)
		{
			return ((x > 0) ? 1 : 0) - ((x < 0) ? 1 : 0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 sign(global::Unity.Mathematics.int2 x)
		{
			return new global::Unity.Mathematics.int2(sign(x.x), sign(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 sign(global::Unity.Mathematics.int3 x)
		{
			return new global::Unity.Mathematics.int3(sign(x.x), sign(x.y), sign(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 sign(global::Unity.Mathematics.int4 x)
		{
			return new global::Unity.Mathematics.int4(sign(x.x), sign(x.y), sign(x.z), sign(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float sign(float x)
		{
			return ((x > 0f) ? 1f : 0f) - ((x < 0f) ? 1f : 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 sign(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(sign(x.x), sign(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 sign(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(sign(x.x), sign(x.y), sign(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 sign(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(sign(x.x), sign(x.y), sign(x.z), sign(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double sign(double x)
		{
			if (x != 0.0)
			{
				return ((x > 0.0) ? 1.0 : 0.0) - ((x < 0.0) ? 1.0 : 0.0);
			}
			return 0.0;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 sign(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(sign(x.x), sign(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 sign(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(sign(x.x), sign(x.y), sign(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 sign(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(sign(x.x), sign(x.y), sign(x.z), sign(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float pow(float x, float y)
		{
			return (float)global::System.Math.Pow(x, y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 pow(global::Unity.Mathematics.float2 x, global::Unity.Mathematics.float2 y)
		{
			return new global::Unity.Mathematics.float2(pow(x.x, y.x), pow(x.y, y.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 pow(global::Unity.Mathematics.float3 x, global::Unity.Mathematics.float3 y)
		{
			return new global::Unity.Mathematics.float3(pow(x.x, y.x), pow(x.y, y.y), pow(x.z, y.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 pow(global::Unity.Mathematics.float4 x, global::Unity.Mathematics.float4 y)
		{
			return new global::Unity.Mathematics.float4(pow(x.x, y.x), pow(x.y, y.y), pow(x.z, y.z), pow(x.w, y.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double pow(double x, double y)
		{
			return global::System.Math.Pow(x, y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 pow(global::Unity.Mathematics.double2 x, global::Unity.Mathematics.double2 y)
		{
			return new global::Unity.Mathematics.double2(pow(x.x, y.x), pow(x.y, y.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 pow(global::Unity.Mathematics.double3 x, global::Unity.Mathematics.double3 y)
		{
			return new global::Unity.Mathematics.double3(pow(x.x, y.x), pow(x.y, y.y), pow(x.z, y.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 pow(global::Unity.Mathematics.double4 x, global::Unity.Mathematics.double4 y)
		{
			return new global::Unity.Mathematics.double4(pow(x.x, y.x), pow(x.y, y.y), pow(x.z, y.z), pow(x.w, y.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float exp(float x)
		{
			return (float)global::System.Math.Exp(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 exp(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(exp(x.x), exp(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 exp(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(exp(x.x), exp(x.y), exp(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 exp(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(exp(x.x), exp(x.y), exp(x.z), exp(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double exp(double x)
		{
			return global::System.Math.Exp(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 exp(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(exp(x.x), exp(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 exp(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(exp(x.x), exp(x.y), exp(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 exp(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(exp(x.x), exp(x.y), exp(x.z), exp(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float exp2(float x)
		{
			return (float)global::System.Math.Exp(x * 0.6931472f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 exp2(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(exp2(x.x), exp2(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 exp2(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(exp2(x.x), exp2(x.y), exp2(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 exp2(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(exp2(x.x), exp2(x.y), exp2(x.z), exp2(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double exp2(double x)
		{
			return global::System.Math.Exp(x * 0.6931471805599453);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 exp2(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(exp2(x.x), exp2(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 exp2(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(exp2(x.x), exp2(x.y), exp2(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 exp2(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(exp2(x.x), exp2(x.y), exp2(x.z), exp2(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float exp10(float x)
		{
			return (float)global::System.Math.Exp(x * 2.3025851f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 exp10(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(exp10(x.x), exp10(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 exp10(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(exp10(x.x), exp10(x.y), exp10(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 exp10(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(exp10(x.x), exp10(x.y), exp10(x.z), exp10(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double exp10(double x)
		{
			return global::System.Math.Exp(x * 2.302585092994046);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 exp10(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(exp10(x.x), exp10(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 exp10(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(exp10(x.x), exp10(x.y), exp10(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 exp10(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(exp10(x.x), exp10(x.y), exp10(x.z), exp10(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float log(float x)
		{
			return (float)global::System.Math.Log(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 log(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(log(x.x), log(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 log(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(log(x.x), log(x.y), log(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 log(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(log(x.x), log(x.y), log(x.z), log(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double log(double x)
		{
			return global::System.Math.Log(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 log(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(log(x.x), log(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 log(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(log(x.x), log(x.y), log(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 log(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(log(x.x), log(x.y), log(x.z), log(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float log2(float x)
		{
			return (float)global::System.Math.Log(x, 2.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 log2(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(log2(x.x), log2(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 log2(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(log2(x.x), log2(x.y), log2(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 log2(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(log2(x.x), log2(x.y), log2(x.z), log2(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double log2(double x)
		{
			return global::System.Math.Log(x, 2.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 log2(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(log2(x.x), log2(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 log2(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(log2(x.x), log2(x.y), log2(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 log2(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(log2(x.x), log2(x.y), log2(x.z), log2(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float log10(float x)
		{
			return (float)global::System.Math.Log10(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 log10(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(log10(x.x), log10(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 log10(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(log10(x.x), log10(x.y), log10(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 log10(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(log10(x.x), log10(x.y), log10(x.z), log10(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double log10(double x)
		{
			return global::System.Math.Log10(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 log10(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(log10(x.x), log10(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 log10(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(log10(x.x), log10(x.y), log10(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 log10(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(log10(x.x), log10(x.y), log10(x.z), log10(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float fmod(float x, float y)
		{
			return x % y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 fmod(global::Unity.Mathematics.float2 x, global::Unity.Mathematics.float2 y)
		{
			return new global::Unity.Mathematics.float2(x.x % y.x, x.y % y.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 fmod(global::Unity.Mathematics.float3 x, global::Unity.Mathematics.float3 y)
		{
			return new global::Unity.Mathematics.float3(x.x % y.x, x.y % y.y, x.z % y.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 fmod(global::Unity.Mathematics.float4 x, global::Unity.Mathematics.float4 y)
		{
			return new global::Unity.Mathematics.float4(x.x % y.x, x.y % y.y, x.z % y.z, x.w % y.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double fmod(double x, double y)
		{
			return x % y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 fmod(global::Unity.Mathematics.double2 x, global::Unity.Mathematics.double2 y)
		{
			return new global::Unity.Mathematics.double2(x.x % y.x, x.y % y.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 fmod(global::Unity.Mathematics.double3 x, global::Unity.Mathematics.double3 y)
		{
			return new global::Unity.Mathematics.double3(x.x % y.x, x.y % y.y, x.z % y.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 fmod(global::Unity.Mathematics.double4 x, global::Unity.Mathematics.double4 y)
		{
			return new global::Unity.Mathematics.double4(x.x % y.x, x.y % y.y, x.z % y.z, x.w % y.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float modf(float x, out float i)
		{
			i = trunc(x);
			return x - i;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 modf(global::Unity.Mathematics.float2 x, out global::Unity.Mathematics.float2 i)
		{
			i = trunc(x);
			return x - i;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 modf(global::Unity.Mathematics.float3 x, out global::Unity.Mathematics.float3 i)
		{
			i = trunc(x);
			return x - i;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 modf(global::Unity.Mathematics.float4 x, out global::Unity.Mathematics.float4 i)
		{
			i = trunc(x);
			return x - i;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double modf(double x, out double i)
		{
			i = trunc(x);
			return x - i;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 modf(global::Unity.Mathematics.double2 x, out global::Unity.Mathematics.double2 i)
		{
			i = trunc(x);
			return x - i;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 modf(global::Unity.Mathematics.double3 x, out global::Unity.Mathematics.double3 i)
		{
			i = trunc(x);
			return x - i;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 modf(global::Unity.Mathematics.double4 x, out global::Unity.Mathematics.double4 i)
		{
			i = trunc(x);
			return x - i;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float sqrt(float x)
		{
			return (float)global::System.Math.Sqrt(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 sqrt(global::Unity.Mathematics.float2 x)
		{
			return new global::Unity.Mathematics.float2(sqrt(x.x), sqrt(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 sqrt(global::Unity.Mathematics.float3 x)
		{
			return new global::Unity.Mathematics.float3(sqrt(x.x), sqrt(x.y), sqrt(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 sqrt(global::Unity.Mathematics.float4 x)
		{
			return new global::Unity.Mathematics.float4(sqrt(x.x), sqrt(x.y), sqrt(x.z), sqrt(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double sqrt(double x)
		{
			return global::System.Math.Sqrt(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 sqrt(global::Unity.Mathematics.double2 x)
		{
			return new global::Unity.Mathematics.double2(sqrt(x.x), sqrt(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 sqrt(global::Unity.Mathematics.double3 x)
		{
			return new global::Unity.Mathematics.double3(sqrt(x.x), sqrt(x.y), sqrt(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 sqrt(global::Unity.Mathematics.double4 x)
		{
			return new global::Unity.Mathematics.double4(sqrt(x.x), sqrt(x.y), sqrt(x.z), sqrt(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float rsqrt(float x)
		{
			return 1f / sqrt(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 rsqrt(global::Unity.Mathematics.float2 x)
		{
			return 1f / sqrt(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 rsqrt(global::Unity.Mathematics.float3 x)
		{
			return 1f / sqrt(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 rsqrt(global::Unity.Mathematics.float4 x)
		{
			return 1f / sqrt(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double rsqrt(double x)
		{
			return 1.0 / sqrt(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 rsqrt(global::Unity.Mathematics.double2 x)
		{
			return 1.0 / sqrt(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 rsqrt(global::Unity.Mathematics.double3 x)
		{
			return 1.0 / sqrt(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 rsqrt(global::Unity.Mathematics.double4 x)
		{
			return 1.0 / sqrt(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 normalize(global::Unity.Mathematics.float2 x)
		{
			return rsqrt(dot(x, x)) * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 normalize(global::Unity.Mathematics.float3 x)
		{
			return rsqrt(dot(x, x)) * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 normalize(global::Unity.Mathematics.float4 x)
		{
			return rsqrt(dot(x, x)) * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 normalize(global::Unity.Mathematics.double2 x)
		{
			return rsqrt(dot(x, x)) * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 normalize(global::Unity.Mathematics.double3 x)
		{
			return rsqrt(dot(x, x)) * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 normalize(global::Unity.Mathematics.double4 x)
		{
			return rsqrt(dot(x, x)) * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 normalizesafe(global::Unity.Mathematics.float2 x, global::Unity.Mathematics.float2 defaultvalue = default(global::Unity.Mathematics.float2))
		{
			float num = dot(x, x);
			return select(defaultvalue, x * rsqrt(num), num > 1.1754944E-38f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 normalizesafe(global::Unity.Mathematics.float3 x, global::Unity.Mathematics.float3 defaultvalue = default(global::Unity.Mathematics.float3))
		{
			float num = dot(x, x);
			return select(defaultvalue, x * rsqrt(num), num > 1.1754944E-38f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 normalizesafe(global::Unity.Mathematics.float4 x, global::Unity.Mathematics.float4 defaultvalue = default(global::Unity.Mathematics.float4))
		{
			float num = dot(x, x);
			return select(defaultvalue, x * rsqrt(num), num > 1.1754944E-38f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 normalizesafe(global::Unity.Mathematics.double2 x, global::Unity.Mathematics.double2 defaultvalue = default(global::Unity.Mathematics.double2))
		{
			double num = dot(x, x);
			return select(defaultvalue, x * rsqrt(num), num > 1.1754943508222875E-38);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 normalizesafe(global::Unity.Mathematics.double3 x, global::Unity.Mathematics.double3 defaultvalue = default(global::Unity.Mathematics.double3))
		{
			double num = dot(x, x);
			return select(defaultvalue, x * rsqrt(num), num > 1.1754943508222875E-38);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 normalizesafe(global::Unity.Mathematics.double4 x, global::Unity.Mathematics.double4 defaultvalue = default(global::Unity.Mathematics.double4))
		{
			double num = dot(x, x);
			return select(defaultvalue, x * rsqrt(num), num > 1.1754943508222875E-38);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float length(float x)
		{
			return abs(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float length(global::Unity.Mathematics.float2 x)
		{
			return sqrt(dot(x, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float length(global::Unity.Mathematics.float3 x)
		{
			return sqrt(dot(x, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float length(global::Unity.Mathematics.float4 x)
		{
			return sqrt(dot(x, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double length(double x)
		{
			return abs(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double length(global::Unity.Mathematics.double2 x)
		{
			return sqrt(dot(x, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double length(global::Unity.Mathematics.double3 x)
		{
			return sqrt(dot(x, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double length(global::Unity.Mathematics.double4 x)
		{
			return sqrt(dot(x, x));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(float x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(global::Unity.Mathematics.float2 x)
		{
			return dot(x, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(global::Unity.Mathematics.float3 x)
		{
			return dot(x, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(global::Unity.Mathematics.float4 x)
		{
			return dot(x, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(double x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(global::Unity.Mathematics.double2 x)
		{
			return dot(x, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(global::Unity.Mathematics.double3 x)
		{
			return dot(x, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(global::Unity.Mathematics.double4 x)
		{
			return dot(x, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float distance(float x, float y)
		{
			return abs(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float distance(global::Unity.Mathematics.float2 x, global::Unity.Mathematics.float2 y)
		{
			return length(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float distance(global::Unity.Mathematics.float3 x, global::Unity.Mathematics.float3 y)
		{
			return length(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float distance(global::Unity.Mathematics.float4 x, global::Unity.Mathematics.float4 y)
		{
			return length(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double distance(double x, double y)
		{
			return abs(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double distance(global::Unity.Mathematics.double2 x, global::Unity.Mathematics.double2 y)
		{
			return length(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double distance(global::Unity.Mathematics.double3 x, global::Unity.Mathematics.double3 y)
		{
			return length(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double distance(global::Unity.Mathematics.double4 x, global::Unity.Mathematics.double4 y)
		{
			return length(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float distancesq(float x, float y)
		{
			return (y - x) * (y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float distancesq(global::Unity.Mathematics.float2 x, global::Unity.Mathematics.float2 y)
		{
			return lengthsq(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float distancesq(global::Unity.Mathematics.float3 x, global::Unity.Mathematics.float3 y)
		{
			return lengthsq(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float distancesq(global::Unity.Mathematics.float4 x, global::Unity.Mathematics.float4 y)
		{
			return lengthsq(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double distancesq(double x, double y)
		{
			return (y - x) * (y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double distancesq(global::Unity.Mathematics.double2 x, global::Unity.Mathematics.double2 y)
		{
			return lengthsq(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double distancesq(global::Unity.Mathematics.double3 x, global::Unity.Mathematics.double3 y)
		{
			return lengthsq(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double distancesq(global::Unity.Mathematics.double4 x, global::Unity.Mathematics.double4 y)
		{
			return lengthsq(y - x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 cross(global::Unity.Mathematics.float3 x, global::Unity.Mathematics.float3 y)
		{
			return (x * y.yzx - x.yzx * y).yzx;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 cross(global::Unity.Mathematics.double3 x, global::Unity.Mathematics.double3 y)
		{
			return (x * y.yzx - x.yzx * y).yzx;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float smoothstep(float xMin, float xMax, float x)
		{
			float num = saturate((x - xMin) / (xMax - xMin));
			return num * num * (3f - 2f * num);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 smoothstep(global::Unity.Mathematics.float2 xMin, global::Unity.Mathematics.float2 xMax, global::Unity.Mathematics.float2 x)
		{
			global::Unity.Mathematics.float2 float5 = saturate((x - xMin) / (xMax - xMin));
			return float5 * float5 * (3f - 2f * float5);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 smoothstep(global::Unity.Mathematics.float3 xMin, global::Unity.Mathematics.float3 xMax, global::Unity.Mathematics.float3 x)
		{
			global::Unity.Mathematics.float3 float5 = saturate((x - xMin) / (xMax - xMin));
			return float5 * float5 * (3f - 2f * float5);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 smoothstep(global::Unity.Mathematics.float4 xMin, global::Unity.Mathematics.float4 xMax, global::Unity.Mathematics.float4 x)
		{
			global::Unity.Mathematics.float4 float5 = saturate((x - xMin) / (xMax - xMin));
			return float5 * float5 * (3f - 2f * float5);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double smoothstep(double xMin, double xMax, double x)
		{
			double num = saturate((x - xMin) / (xMax - xMin));
			return num * num * (3.0 - 2.0 * num);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 smoothstep(global::Unity.Mathematics.double2 xMin, global::Unity.Mathematics.double2 xMax, global::Unity.Mathematics.double2 x)
		{
			global::Unity.Mathematics.double2 double5 = saturate((x - xMin) / (xMax - xMin));
			return double5 * double5 * (3.0 - 2.0 * double5);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 smoothstep(global::Unity.Mathematics.double3 xMin, global::Unity.Mathematics.double3 xMax, global::Unity.Mathematics.double3 x)
		{
			global::Unity.Mathematics.double3 double5 = saturate((x - xMin) / (xMax - xMin));
			return double5 * double5 * (3.0 - 2.0 * double5);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 smoothstep(global::Unity.Mathematics.double4 xMin, global::Unity.Mathematics.double4 xMax, global::Unity.Mathematics.double4 x)
		{
			global::Unity.Mathematics.double4 double5 = saturate((x - xMin) / (xMax - xMin));
			return double5 * double5 * (3.0 - 2.0 * double5);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.bool2 x)
		{
			if (!x.x)
			{
				return x.y;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.bool3 x)
		{
			if (!x.x && !x.y)
			{
				return x.z;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.bool4 x)
		{
			if (!x.x && !x.y && !x.z)
			{
				return x.w;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.int2 x)
		{
			if (x.x == 0)
			{
				return x.y != 0;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.int3 x)
		{
			if (x.x == 0 && x.y == 0)
			{
				return x.z != 0;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.int4 x)
		{
			if (x.x == 0 && x.y == 0 && x.z == 0)
			{
				return x.w != 0;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.uint2 x)
		{
			if (x.x == 0)
			{
				return x.y != 0;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.uint3 x)
		{
			if (x.x == 0 && x.y == 0)
			{
				return x.z != 0;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.uint4 x)
		{
			if (x.x == 0 && x.y == 0 && x.z == 0)
			{
				return x.w != 0;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.float2 x)
		{
			if (x.x == 0f)
			{
				return x.y != 0f;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.float3 x)
		{
			if (x.x == 0f && x.y == 0f)
			{
				return x.z != 0f;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.float4 x)
		{
			if (x.x == 0f && x.y == 0f && x.z == 0f)
			{
				return x.w != 0f;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.double2 x)
		{
			if (x.x == 0.0)
			{
				return x.y != 0.0;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.double3 x)
		{
			if (x.x == 0.0 && x.y == 0.0)
			{
				return x.z != 0.0;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool any(global::Unity.Mathematics.double4 x)
		{
			if (x.x == 0.0 && x.y == 0.0 && x.z == 0.0)
			{
				return x.w != 0.0;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.bool2 x)
		{
			if (x.x)
			{
				return x.y;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.bool3 x)
		{
			if (x.x && x.y)
			{
				return x.z;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.bool4 x)
		{
			if (x.x && x.y && x.z)
			{
				return x.w;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.int2 x)
		{
			if (x.x != 0)
			{
				return x.y != 0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.int3 x)
		{
			if (x.x != 0 && x.y != 0)
			{
				return x.z != 0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.int4 x)
		{
			if (x.x != 0 && x.y != 0 && x.z != 0)
			{
				return x.w != 0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.uint2 x)
		{
			if (x.x != 0)
			{
				return x.y != 0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.uint3 x)
		{
			if (x.x != 0 && x.y != 0)
			{
				return x.z != 0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.uint4 x)
		{
			if (x.x != 0 && x.y != 0 && x.z != 0)
			{
				return x.w != 0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.float2 x)
		{
			if (x.x != 0f)
			{
				return x.y != 0f;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.float3 x)
		{
			if (x.x != 0f && x.y != 0f)
			{
				return x.z != 0f;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.float4 x)
		{
			if (x.x != 0f && x.y != 0f && x.z != 0f)
			{
				return x.w != 0f;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.double2 x)
		{
			if (x.x != 0.0)
			{
				return x.y != 0.0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.double3 x)
		{
			if (x.x != 0.0 && x.y != 0.0)
			{
				return x.z != 0.0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool all(global::Unity.Mathematics.double4 x)
		{
			if (x.x != 0.0 && x.y != 0.0 && x.z != 0.0)
			{
				return x.w != 0.0;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int select(int falseValue, int trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 select(global::Unity.Mathematics.int2 falseValue, global::Unity.Mathematics.int2 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 select(global::Unity.Mathematics.int3 falseValue, global::Unity.Mathematics.int3 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 select(global::Unity.Mathematics.int4 falseValue, global::Unity.Mathematics.int4 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 select(global::Unity.Mathematics.int2 falseValue, global::Unity.Mathematics.int2 trueValue, global::Unity.Mathematics.bool2 test)
		{
			return new global::Unity.Mathematics.int2(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 select(global::Unity.Mathematics.int3 falseValue, global::Unity.Mathematics.int3 trueValue, global::Unity.Mathematics.bool3 test)
		{
			return new global::Unity.Mathematics.int3(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 select(global::Unity.Mathematics.int4 falseValue, global::Unity.Mathematics.int4 trueValue, global::Unity.Mathematics.bool4 test)
		{
			return new global::Unity.Mathematics.int4(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z, test.w ? trueValue.w : falseValue.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint select(uint falseValue, uint trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 select(global::Unity.Mathematics.uint2 falseValue, global::Unity.Mathematics.uint2 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 select(global::Unity.Mathematics.uint3 falseValue, global::Unity.Mathematics.uint3 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 select(global::Unity.Mathematics.uint4 falseValue, global::Unity.Mathematics.uint4 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 select(global::Unity.Mathematics.uint2 falseValue, global::Unity.Mathematics.uint2 trueValue, global::Unity.Mathematics.bool2 test)
		{
			return new global::Unity.Mathematics.uint2(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 select(global::Unity.Mathematics.uint3 falseValue, global::Unity.Mathematics.uint3 trueValue, global::Unity.Mathematics.bool3 test)
		{
			return new global::Unity.Mathematics.uint3(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 select(global::Unity.Mathematics.uint4 falseValue, global::Unity.Mathematics.uint4 trueValue, global::Unity.Mathematics.bool4 test)
		{
			return new global::Unity.Mathematics.uint4(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z, test.w ? trueValue.w : falseValue.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long select(long falseValue, long trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ulong select(ulong falseValue, ulong trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float select(float falseValue, float trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 select(global::Unity.Mathematics.float2 falseValue, global::Unity.Mathematics.float2 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 select(global::Unity.Mathematics.float3 falseValue, global::Unity.Mathematics.float3 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 select(global::Unity.Mathematics.float4 falseValue, global::Unity.Mathematics.float4 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 select(global::Unity.Mathematics.float2 falseValue, global::Unity.Mathematics.float2 trueValue, global::Unity.Mathematics.bool2 test)
		{
			return new global::Unity.Mathematics.float2(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 select(global::Unity.Mathematics.float3 falseValue, global::Unity.Mathematics.float3 trueValue, global::Unity.Mathematics.bool3 test)
		{
			return new global::Unity.Mathematics.float3(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 select(global::Unity.Mathematics.float4 falseValue, global::Unity.Mathematics.float4 trueValue, global::Unity.Mathematics.bool4 test)
		{
			return new global::Unity.Mathematics.float4(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z, test.w ? trueValue.w : falseValue.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double select(double falseValue, double trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 select(global::Unity.Mathematics.double2 falseValue, global::Unity.Mathematics.double2 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 select(global::Unity.Mathematics.double3 falseValue, global::Unity.Mathematics.double3 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 select(global::Unity.Mathematics.double4 falseValue, global::Unity.Mathematics.double4 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 select(global::Unity.Mathematics.double2 falseValue, global::Unity.Mathematics.double2 trueValue, global::Unity.Mathematics.bool2 test)
		{
			return new global::Unity.Mathematics.double2(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 select(global::Unity.Mathematics.double3 falseValue, global::Unity.Mathematics.double3 trueValue, global::Unity.Mathematics.bool3 test)
		{
			return new global::Unity.Mathematics.double3(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 select(global::Unity.Mathematics.double4 falseValue, global::Unity.Mathematics.double4 trueValue, global::Unity.Mathematics.bool4 test)
		{
			return new global::Unity.Mathematics.double4(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z, test.w ? trueValue.w : falseValue.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float step(float threshold, float x)
		{
			return select(0f, 1f, x >= threshold);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 step(global::Unity.Mathematics.float2 threshold, global::Unity.Mathematics.float2 x)
		{
			return select(float2(0f), float2(1f), x >= threshold);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 step(global::Unity.Mathematics.float3 threshold, global::Unity.Mathematics.float3 x)
		{
			return select(float3(0f), float3(1f), x >= threshold);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 step(global::Unity.Mathematics.float4 threshold, global::Unity.Mathematics.float4 x)
		{
			return select(float4(0f), float4(1f), x >= threshold);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double step(double threshold, double x)
		{
			return select(0.0, 1.0, x >= threshold);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 step(global::Unity.Mathematics.double2 threshold, global::Unity.Mathematics.double2 x)
		{
			return select(double2(0.0), double2(1.0), x >= threshold);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 step(global::Unity.Mathematics.double3 threshold, global::Unity.Mathematics.double3 x)
		{
			return select(double3(0.0), double3(1.0), x >= threshold);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 step(global::Unity.Mathematics.double4 threshold, global::Unity.Mathematics.double4 x)
		{
			return select(double4(0.0), double4(1.0), x >= threshold);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 reflect(global::Unity.Mathematics.float2 i, global::Unity.Mathematics.float2 n)
		{
			return i - 2f * n * dot(i, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 reflect(global::Unity.Mathematics.float3 i, global::Unity.Mathematics.float3 n)
		{
			return i - 2f * n * dot(i, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 reflect(global::Unity.Mathematics.float4 i, global::Unity.Mathematics.float4 n)
		{
			return i - 2f * n * dot(i, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 reflect(global::Unity.Mathematics.double2 i, global::Unity.Mathematics.double2 n)
		{
			return i - 2.0 * n * dot(i, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 reflect(global::Unity.Mathematics.double3 i, global::Unity.Mathematics.double3 n)
		{
			return i - 2.0 * n * dot(i, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 reflect(global::Unity.Mathematics.double4 i, global::Unity.Mathematics.double4 n)
		{
			return i - 2.0 * n * dot(i, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 refract(global::Unity.Mathematics.float2 i, global::Unity.Mathematics.float2 n, float indexOfRefraction)
		{
			float num = dot(n, i);
			float num2 = 1f - indexOfRefraction * indexOfRefraction * (1f - num * num);
			return select(0f, indexOfRefraction * i - (indexOfRefraction * num + sqrt(num2)) * n, num2 >= 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 refract(global::Unity.Mathematics.float3 i, global::Unity.Mathematics.float3 n, float indexOfRefraction)
		{
			float num = dot(n, i);
			float num2 = 1f - indexOfRefraction * indexOfRefraction * (1f - num * num);
			return select(0f, indexOfRefraction * i - (indexOfRefraction * num + sqrt(num2)) * n, num2 >= 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 refract(global::Unity.Mathematics.float4 i, global::Unity.Mathematics.float4 n, float indexOfRefraction)
		{
			float num = dot(n, i);
			float num2 = 1f - indexOfRefraction * indexOfRefraction * (1f - num * num);
			return select(0f, indexOfRefraction * i - (indexOfRefraction * num + sqrt(num2)) * n, num2 >= 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 refract(global::Unity.Mathematics.double2 i, global::Unity.Mathematics.double2 n, double indexOfRefraction)
		{
			double num = dot(n, i);
			double num2 = 1.0 - indexOfRefraction * indexOfRefraction * (1.0 - num * num);
			return select(0f, indexOfRefraction * i - (indexOfRefraction * num + sqrt(num2)) * n, num2 >= 0.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 refract(global::Unity.Mathematics.double3 i, global::Unity.Mathematics.double3 n, double indexOfRefraction)
		{
			double num = dot(n, i);
			double num2 = 1.0 - indexOfRefraction * indexOfRefraction * (1.0 - num * num);
			return select(0f, indexOfRefraction * i - (indexOfRefraction * num + sqrt(num2)) * n, num2 >= 0.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 refract(global::Unity.Mathematics.double4 i, global::Unity.Mathematics.double4 n, double indexOfRefraction)
		{
			double num = dot(n, i);
			double num2 = 1.0 - indexOfRefraction * indexOfRefraction * (1.0 - num * num);
			return select(0f, indexOfRefraction * i - (indexOfRefraction * num + sqrt(num2)) * n, num2 >= 0.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 project(global::Unity.Mathematics.float2 a, global::Unity.Mathematics.float2 ontoB)
		{
			return dot(a, ontoB) / dot(ontoB, ontoB) * ontoB;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 project(global::Unity.Mathematics.float3 a, global::Unity.Mathematics.float3 ontoB)
		{
			return dot(a, ontoB) / dot(ontoB, ontoB) * ontoB;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 project(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4 ontoB)
		{
			return dot(a, ontoB) / dot(ontoB, ontoB) * ontoB;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 projectsafe(global::Unity.Mathematics.float2 a, global::Unity.Mathematics.float2 ontoB, global::Unity.Mathematics.float2 defaultValue = default(global::Unity.Mathematics.float2))
		{
			global::Unity.Mathematics.float2 float5 = project(a, ontoB);
			return select(defaultValue, float5, all(isfinite(float5)));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 projectsafe(global::Unity.Mathematics.float3 a, global::Unity.Mathematics.float3 ontoB, global::Unity.Mathematics.float3 defaultValue = default(global::Unity.Mathematics.float3))
		{
			global::Unity.Mathematics.float3 float5 = project(a, ontoB);
			return select(defaultValue, float5, all(isfinite(float5)));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 projectsafe(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4 ontoB, global::Unity.Mathematics.float4 defaultValue = default(global::Unity.Mathematics.float4))
		{
			global::Unity.Mathematics.float4 float5 = project(a, ontoB);
			return select(defaultValue, float5, all(isfinite(float5)));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 project(global::Unity.Mathematics.double2 a, global::Unity.Mathematics.double2 ontoB)
		{
			return dot(a, ontoB) / dot(ontoB, ontoB) * ontoB;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 project(global::Unity.Mathematics.double3 a, global::Unity.Mathematics.double3 ontoB)
		{
			return dot(a, ontoB) / dot(ontoB, ontoB) * ontoB;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 project(global::Unity.Mathematics.double4 a, global::Unity.Mathematics.double4 ontoB)
		{
			return dot(a, ontoB) / dot(ontoB, ontoB) * ontoB;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 projectsafe(global::Unity.Mathematics.double2 a, global::Unity.Mathematics.double2 ontoB, global::Unity.Mathematics.double2 defaultValue = default(global::Unity.Mathematics.double2))
		{
			global::Unity.Mathematics.double2 double5 = project(a, ontoB);
			return select(defaultValue, double5, all(isfinite(double5)));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 projectsafe(global::Unity.Mathematics.double3 a, global::Unity.Mathematics.double3 ontoB, global::Unity.Mathematics.double3 defaultValue = default(global::Unity.Mathematics.double3))
		{
			global::Unity.Mathematics.double3 double5 = project(a, ontoB);
			return select(defaultValue, double5, all(isfinite(double5)));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 projectsafe(global::Unity.Mathematics.double4 a, global::Unity.Mathematics.double4 ontoB, global::Unity.Mathematics.double4 defaultValue = default(global::Unity.Mathematics.double4))
		{
			global::Unity.Mathematics.double4 double5 = project(a, ontoB);
			return select(defaultValue, double5, all(isfinite(double5)));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 faceforward(global::Unity.Mathematics.float2 n, global::Unity.Mathematics.float2 i, global::Unity.Mathematics.float2 ng)
		{
			return select(n, -n, dot(ng, i) >= 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 faceforward(global::Unity.Mathematics.float3 n, global::Unity.Mathematics.float3 i, global::Unity.Mathematics.float3 ng)
		{
			return select(n, -n, dot(ng, i) >= 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 faceforward(global::Unity.Mathematics.float4 n, global::Unity.Mathematics.float4 i, global::Unity.Mathematics.float4 ng)
		{
			return select(n, -n, dot(ng, i) >= 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 faceforward(global::Unity.Mathematics.double2 n, global::Unity.Mathematics.double2 i, global::Unity.Mathematics.double2 ng)
		{
			return select(n, -n, dot(ng, i) >= 0.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 faceforward(global::Unity.Mathematics.double3 n, global::Unity.Mathematics.double3 i, global::Unity.Mathematics.double3 ng)
		{
			return select(n, -n, dot(ng, i) >= 0.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 faceforward(global::Unity.Mathematics.double4 n, global::Unity.Mathematics.double4 i, global::Unity.Mathematics.double4 ng)
		{
			return select(n, -n, dot(ng, i) >= 0.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void sincos(float x, out float s, out float c)
		{
			s = sin(x);
			c = cos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void sincos(global::Unity.Mathematics.float2 x, out global::Unity.Mathematics.float2 s, out global::Unity.Mathematics.float2 c)
		{
			s = sin(x);
			c = cos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void sincos(global::Unity.Mathematics.float3 x, out global::Unity.Mathematics.float3 s, out global::Unity.Mathematics.float3 c)
		{
			s = sin(x);
			c = cos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void sincos(global::Unity.Mathematics.float4 x, out global::Unity.Mathematics.float4 s, out global::Unity.Mathematics.float4 c)
		{
			s = sin(x);
			c = cos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void sincos(double x, out double s, out double c)
		{
			s = sin(x);
			c = cos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void sincos(global::Unity.Mathematics.double2 x, out global::Unity.Mathematics.double2 s, out global::Unity.Mathematics.double2 c)
		{
			s = sin(x);
			c = cos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void sincos(global::Unity.Mathematics.double3 x, out global::Unity.Mathematics.double3 s, out global::Unity.Mathematics.double3 c)
		{
			s = sin(x);
			c = cos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void sincos(global::Unity.Mathematics.double4 x, out global::Unity.Mathematics.double4 s, out global::Unity.Mathematics.double4 c)
		{
			s = sin(x);
			c = cos(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int countbits(int x)
		{
			return countbits((uint)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 countbits(global::Unity.Mathematics.int2 x)
		{
			return countbits((global::Unity.Mathematics.uint2)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 countbits(global::Unity.Mathematics.int3 x)
		{
			return countbits((global::Unity.Mathematics.uint3)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 countbits(global::Unity.Mathematics.int4 x)
		{
			return countbits((global::Unity.Mathematics.uint4)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int countbits(uint x)
		{
			x -= (x >> 1) & 0x55555555;
			x = (x & 0x33333333) + ((x >> 2) & 0x33333333);
			return (int)(((x + (x >> 4)) & 0xF0F0F0F) * 16843009 >> 24);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 countbits(global::Unity.Mathematics.uint2 x)
		{
			x -= (x >> 1) & 1431655765u;
			x = (x & 858993459u) + ((x >> 2) & 858993459u);
			return int2(((x + (x >> 4)) & 252645135u) * 16843009u >> 24);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 countbits(global::Unity.Mathematics.uint3 x)
		{
			x -= (x >> 1) & 1431655765u;
			x = (x & 858993459u) + ((x >> 2) & 858993459u);
			return int3(((x + (x >> 4)) & 252645135u) * 16843009u >> 24);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 countbits(global::Unity.Mathematics.uint4 x)
		{
			x -= (x >> 1) & 1431655765u;
			x = (x & 858993459u) + ((x >> 2) & 858993459u);
			return int4(((x + (x >> 4)) & 252645135u) * 16843009u >> 24);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int countbits(ulong x)
		{
			x -= (x >> 1) & 0x5555555555555555L;
			x = (x & 0x3333333333333333L) + ((x >> 2) & 0x3333333333333333L);
			return (int)(((x + (x >> 4)) & 0xF0F0F0F0F0F0F0FL) * 72340172838076673L >> 56);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int countbits(long x)
		{
			return countbits((ulong)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(int x)
		{
			return lzcnt((uint)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 lzcnt(global::Unity.Mathematics.int2 x)
		{
			return int2(lzcnt(x.x), lzcnt(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 lzcnt(global::Unity.Mathematics.int3 x)
		{
			return int3(lzcnt(x.x), lzcnt(x.y), lzcnt(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 lzcnt(global::Unity.Mathematics.int4 x)
		{
			return int4(lzcnt(x.x), lzcnt(x.y), lzcnt(x.z), lzcnt(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(uint x)
		{
			if (x == 0)
			{
				return 32;
			}
			global::Unity.Mathematics.math.LongDoubleUnion longDoubleUnion = default(global::Unity.Mathematics.math.LongDoubleUnion);
			longDoubleUnion.doubleValue = 0.0;
			longDoubleUnion.longValue = 4841369599423283200L + (long)x;
			longDoubleUnion.doubleValue -= 4503599627370496.0;
			return 1054 - (int)(longDoubleUnion.longValue >> 52);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 lzcnt(global::Unity.Mathematics.uint2 x)
		{
			return int2(lzcnt(x.x), lzcnt(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 lzcnt(global::Unity.Mathematics.uint3 x)
		{
			return int3(lzcnt(x.x), lzcnt(x.y), lzcnt(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 lzcnt(global::Unity.Mathematics.uint4 x)
		{
			return int4(lzcnt(x.x), lzcnt(x.y), lzcnt(x.z), lzcnt(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(long x)
		{
			return lzcnt((ulong)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(ulong x)
		{
			if (x == 0L)
			{
				return 64;
			}
			uint num = (uint)(x >> 32);
			uint num2 = (uint)((num != 0) ? num : x);
			int num3 = ((num != 0) ? 1054 : 1086);
			global::Unity.Mathematics.math.LongDoubleUnion longDoubleUnion = default(global::Unity.Mathematics.math.LongDoubleUnion);
			longDoubleUnion.doubleValue = 0.0;
			longDoubleUnion.longValue = 4841369599423283200L + (long)num2;
			longDoubleUnion.doubleValue -= 4503599627370496.0;
			return num3 - (int)(longDoubleUnion.longValue >> 52);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(int x)
		{
			return tzcnt((uint)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 tzcnt(global::Unity.Mathematics.int2 x)
		{
			return int2(tzcnt(x.x), tzcnt(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 tzcnt(global::Unity.Mathematics.int3 x)
		{
			return int3(tzcnt(x.x), tzcnt(x.y), tzcnt(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 tzcnt(global::Unity.Mathematics.int4 x)
		{
			return int4(tzcnt(x.x), tzcnt(x.y), tzcnt(x.z), tzcnt(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(uint x)
		{
			if (x == 0)
			{
				return 32;
			}
			x &= (uint)(int)(0L - (long)x);
			global::Unity.Mathematics.math.LongDoubleUnion longDoubleUnion = default(global::Unity.Mathematics.math.LongDoubleUnion);
			longDoubleUnion.doubleValue = 0.0;
			longDoubleUnion.longValue = 4841369599423283200L + (long)x;
			longDoubleUnion.doubleValue -= 4503599627370496.0;
			return (int)(longDoubleUnion.longValue >> 52) - 1023;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 tzcnt(global::Unity.Mathematics.uint2 x)
		{
			return int2(tzcnt(x.x), tzcnt(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 tzcnt(global::Unity.Mathematics.uint3 x)
		{
			return int3(tzcnt(x.x), tzcnt(x.y), tzcnt(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 tzcnt(global::Unity.Mathematics.uint4 x)
		{
			return int4(tzcnt(x.x), tzcnt(x.y), tzcnt(x.z), tzcnt(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(long x)
		{
			return tzcnt((ulong)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(ulong x)
		{
			if (x == 0L)
			{
				return 64;
			}
			x &= 0L - x;
			uint num = (uint)x;
			uint num2 = (uint)((num != 0) ? num : (x >> 32));
			int num3 = ((num != 0) ? 1023 : 991);
			global::Unity.Mathematics.math.LongDoubleUnion longDoubleUnion = default(global::Unity.Mathematics.math.LongDoubleUnion);
			longDoubleUnion.doubleValue = 0.0;
			longDoubleUnion.longValue = 4841369599423283200L + (long)num2;
			longDoubleUnion.doubleValue -= 4503599627370496.0;
			return (int)(longDoubleUnion.longValue >> 52) - num3;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int reversebits(int x)
		{
			return (int)reversebits((uint)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 reversebits(global::Unity.Mathematics.int2 x)
		{
			return (global::Unity.Mathematics.int2)reversebits((global::Unity.Mathematics.uint2)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 reversebits(global::Unity.Mathematics.int3 x)
		{
			return (global::Unity.Mathematics.int3)reversebits((global::Unity.Mathematics.uint3)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 reversebits(global::Unity.Mathematics.int4 x)
		{
			return (global::Unity.Mathematics.int4)reversebits((global::Unity.Mathematics.uint4)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint reversebits(uint x)
		{
			x = ((x >> 1) & 0x55555555) | ((x & 0x55555555) << 1);
			x = ((x >> 2) & 0x33333333) | ((x & 0x33333333) << 2);
			x = ((x >> 4) & 0xF0F0F0F) | ((x & 0xF0F0F0F) << 4);
			x = ((x >> 8) & 0xFF00FF) | ((x & 0xFF00FF) << 8);
			return (x >> 16) | (x << 16);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 reversebits(global::Unity.Mathematics.uint2 x)
		{
			x = ((x >> 1) & 1431655765u) | ((x & 1431655765u) << 1);
			x = ((x >> 2) & 858993459u) | ((x & 858993459u) << 2);
			x = ((x >> 4) & 252645135u) | ((x & 252645135u) << 4);
			x = ((x >> 8) & 16711935u) | ((x & 16711935u) << 8);
			return (x >> 16) | (x << 16);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 reversebits(global::Unity.Mathematics.uint3 x)
		{
			x = ((x >> 1) & 1431655765u) | ((x & 1431655765u) << 1);
			x = ((x >> 2) & 858993459u) | ((x & 858993459u) << 2);
			x = ((x >> 4) & 252645135u) | ((x & 252645135u) << 4);
			x = ((x >> 8) & 16711935u) | ((x & 16711935u) << 8);
			return (x >> 16) | (x << 16);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 reversebits(global::Unity.Mathematics.uint4 x)
		{
			x = ((x >> 1) & 1431655765u) | ((x & 1431655765u) << 1);
			x = ((x >> 2) & 858993459u) | ((x & 858993459u) << 2);
			x = ((x >> 4) & 252645135u) | ((x & 252645135u) << 4);
			x = ((x >> 8) & 16711935u) | ((x & 16711935u) << 8);
			return (x >> 16) | (x << 16);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long reversebits(long x)
		{
			return (long)reversebits((ulong)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ulong reversebits(ulong x)
		{
			x = ((x >> 1) & 0x5555555555555555L) | ((x & 0x5555555555555555L) << 1);
			x = ((x >> 2) & 0x3333333333333333L) | ((x & 0x3333333333333333L) << 2);
			x = ((x >> 4) & 0xF0F0F0F0F0F0F0FL) | ((x & 0xF0F0F0F0F0F0F0FL) << 4);
			x = ((x >> 8) & 0xFF00FF00FF00FFL) | ((x & 0xFF00FF00FF00FFL) << 8);
			x = ((x >> 16) & 0xFFFF0000FFFFL) | ((x & 0xFFFF0000FFFFL) << 16);
			return (x >> 32) | (x << 32);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int rol(int x, int n)
		{
			return (int)rol((uint)x, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 rol(global::Unity.Mathematics.int2 x, int n)
		{
			return (global::Unity.Mathematics.int2)rol((global::Unity.Mathematics.uint2)x, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 rol(global::Unity.Mathematics.int3 x, int n)
		{
			return (global::Unity.Mathematics.int3)rol((global::Unity.Mathematics.uint3)x, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 rol(global::Unity.Mathematics.int4 x, int n)
		{
			return (global::Unity.Mathematics.int4)rol((global::Unity.Mathematics.uint4)x, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint rol(uint x, int n)
		{
			return (x << n) | (x >> 32 - n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 rol(global::Unity.Mathematics.uint2 x, int n)
		{
			return (x << n) | (x >> 32 - n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 rol(global::Unity.Mathematics.uint3 x, int n)
		{
			return (x << n) | (x >> 32 - n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 rol(global::Unity.Mathematics.uint4 x, int n)
		{
			return (x << n) | (x >> 32 - n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long rol(long x, int n)
		{
			return (long)rol((ulong)x, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ulong rol(ulong x, int n)
		{
			return (x << n) | (x >> 64 - n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int ror(int x, int n)
		{
			return (int)ror((uint)x, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 ror(global::Unity.Mathematics.int2 x, int n)
		{
			return (global::Unity.Mathematics.int2)ror((global::Unity.Mathematics.uint2)x, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 ror(global::Unity.Mathematics.int3 x, int n)
		{
			return (global::Unity.Mathematics.int3)ror((global::Unity.Mathematics.uint3)x, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 ror(global::Unity.Mathematics.int4 x, int n)
		{
			return (global::Unity.Mathematics.int4)ror((global::Unity.Mathematics.uint4)x, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint ror(uint x, int n)
		{
			return (x >> n) | (x << 32 - n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 ror(global::Unity.Mathematics.uint2 x, int n)
		{
			return (x >> n) | (x << 32 - n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 ror(global::Unity.Mathematics.uint3 x, int n)
		{
			return (x >> n) | (x << 32 - n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 ror(global::Unity.Mathematics.uint4 x, int n)
		{
			return (x >> n) | (x << 32 - n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long ror(long x, int n)
		{
			return (long)ror((ulong)x, n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ulong ror(ulong x, int n)
		{
			return (x >> n) | (x << 64 - n);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int ceilpow2(int x)
		{
			x--;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 ceilpow2(global::Unity.Mathematics.int2 x)
		{
			x -= 1;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 ceilpow2(global::Unity.Mathematics.int3 x)
		{
			x -= 1;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 ceilpow2(global::Unity.Mathematics.int4 x)
		{
			x -= 1;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint ceilpow2(uint x)
		{
			x--;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 ceilpow2(global::Unity.Mathematics.uint2 x)
		{
			x -= 1u;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 ceilpow2(global::Unity.Mathematics.uint3 x)
		{
			x -= 1u;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 ceilpow2(global::Unity.Mathematics.uint4 x)
		{
			x -= 1u;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static long ceilpow2(long x)
		{
			x--;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			x |= x >> 32;
			return x + 1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static ulong ceilpow2(ulong x)
		{
			x--;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			x |= x >> 32;
			return x + 1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int ceillog2(int x)
		{
			return 32 - lzcnt((uint)(x - 1));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 ceillog2(global::Unity.Mathematics.int2 x)
		{
			return new global::Unity.Mathematics.int2(ceillog2(x.x), ceillog2(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 ceillog2(global::Unity.Mathematics.int3 x)
		{
			return new global::Unity.Mathematics.int3(ceillog2(x.x), ceillog2(x.y), ceillog2(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 ceillog2(global::Unity.Mathematics.int4 x)
		{
			return new global::Unity.Mathematics.int4(ceillog2(x.x), ceillog2(x.y), ceillog2(x.z), ceillog2(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int ceillog2(uint x)
		{
			return 32 - lzcnt(x - 1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 ceillog2(global::Unity.Mathematics.uint2 x)
		{
			return new global::Unity.Mathematics.int2(ceillog2(x.x), ceillog2(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 ceillog2(global::Unity.Mathematics.uint3 x)
		{
			return new global::Unity.Mathematics.int3(ceillog2(x.x), ceillog2(x.y), ceillog2(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 ceillog2(global::Unity.Mathematics.uint4 x)
		{
			return new global::Unity.Mathematics.int4(ceillog2(x.x), ceillog2(x.y), ceillog2(x.z), ceillog2(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int floorlog2(int x)
		{
			return 31 - lzcnt((uint)x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 floorlog2(global::Unity.Mathematics.int2 x)
		{
			return new global::Unity.Mathematics.int2(floorlog2(x.x), floorlog2(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 floorlog2(global::Unity.Mathematics.int3 x)
		{
			return new global::Unity.Mathematics.int3(floorlog2(x.x), floorlog2(x.y), floorlog2(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 floorlog2(global::Unity.Mathematics.int4 x)
		{
			return new global::Unity.Mathematics.int4(floorlog2(x.x), floorlog2(x.y), floorlog2(x.z), floorlog2(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int floorlog2(uint x)
		{
			return 31 - lzcnt(x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 floorlog2(global::Unity.Mathematics.uint2 x)
		{
			return new global::Unity.Mathematics.int2(floorlog2(x.x), floorlog2(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 floorlog2(global::Unity.Mathematics.uint3 x)
		{
			return new global::Unity.Mathematics.int3(floorlog2(x.x), floorlog2(x.y), floorlog2(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 floorlog2(global::Unity.Mathematics.uint4 x)
		{
			return new global::Unity.Mathematics.int4(floorlog2(x.x), floorlog2(x.y), floorlog2(x.z), floorlog2(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float radians(float x)
		{
			return x * (global::System.MathF.PI / 180f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 radians(global::Unity.Mathematics.float2 x)
		{
			return x * (global::System.MathF.PI / 180f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 radians(global::Unity.Mathematics.float3 x)
		{
			return x * (global::System.MathF.PI / 180f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 radians(global::Unity.Mathematics.float4 x)
		{
			return x * (global::System.MathF.PI / 180f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double radians(double x)
		{
			return x * (global::System.Math.PI / 180.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 radians(global::Unity.Mathematics.double2 x)
		{
			return x * (global::System.Math.PI / 180.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 radians(global::Unity.Mathematics.double3 x)
		{
			return x * (global::System.Math.PI / 180.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 radians(global::Unity.Mathematics.double4 x)
		{
			return x * (global::System.Math.PI / 180.0);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float degrees(float x)
		{
			return x * 57.29578f;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 degrees(global::Unity.Mathematics.float2 x)
		{
			return x * 57.29578f;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 degrees(global::Unity.Mathematics.float3 x)
		{
			return x * 57.29578f;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 degrees(global::Unity.Mathematics.float4 x)
		{
			return x * 57.29578f;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double degrees(double x)
		{
			return x * (180.0 / global::System.Math.PI);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 degrees(global::Unity.Mathematics.double2 x)
		{
			return x * (180.0 / global::System.Math.PI);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 degrees(global::Unity.Mathematics.double3 x)
		{
			return x * (180.0 / global::System.Math.PI);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 degrees(global::Unity.Mathematics.double4 x)
		{
			return x * (180.0 / global::System.Math.PI);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int cmin(global::Unity.Mathematics.int2 x)
		{
			return min(x.x, x.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int cmin(global::Unity.Mathematics.int3 x)
		{
			return min(min(x.x, x.y), x.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int cmin(global::Unity.Mathematics.int4 x)
		{
			return min(min(x.x, x.y), min(x.z, x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint cmin(global::Unity.Mathematics.uint2 x)
		{
			return min(x.x, x.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint cmin(global::Unity.Mathematics.uint3 x)
		{
			return min(min(x.x, x.y), x.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint cmin(global::Unity.Mathematics.uint4 x)
		{
			return min(min(x.x, x.y), min(x.z, x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float cmin(global::Unity.Mathematics.float2 x)
		{
			return min(x.x, x.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float cmin(global::Unity.Mathematics.float3 x)
		{
			return min(min(x.x, x.y), x.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float cmin(global::Unity.Mathematics.float4 x)
		{
			return min(min(x.x, x.y), min(x.z, x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double cmin(global::Unity.Mathematics.double2 x)
		{
			return min(x.x, x.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double cmin(global::Unity.Mathematics.double3 x)
		{
			return min(min(x.x, x.y), x.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double cmin(global::Unity.Mathematics.double4 x)
		{
			return min(min(x.x, x.y), min(x.z, x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int cmax(global::Unity.Mathematics.int2 x)
		{
			return max(x.x, x.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int cmax(global::Unity.Mathematics.int3 x)
		{
			return max(max(x.x, x.y), x.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int cmax(global::Unity.Mathematics.int4 x)
		{
			return max(max(x.x, x.y), max(x.z, x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint cmax(global::Unity.Mathematics.uint2 x)
		{
			return max(x.x, x.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint cmax(global::Unity.Mathematics.uint3 x)
		{
			return max(max(x.x, x.y), x.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint cmax(global::Unity.Mathematics.uint4 x)
		{
			return max(max(x.x, x.y), max(x.z, x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float cmax(global::Unity.Mathematics.float2 x)
		{
			return max(x.x, x.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float cmax(global::Unity.Mathematics.float3 x)
		{
			return max(max(x.x, x.y), x.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float cmax(global::Unity.Mathematics.float4 x)
		{
			return max(max(x.x, x.y), max(x.z, x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double cmax(global::Unity.Mathematics.double2 x)
		{
			return max(x.x, x.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double cmax(global::Unity.Mathematics.double3 x)
		{
			return max(max(x.x, x.y), x.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double cmax(global::Unity.Mathematics.double4 x)
		{
			return max(max(x.x, x.y), max(x.z, x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int csum(global::Unity.Mathematics.int2 x)
		{
			return x.x + x.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int csum(global::Unity.Mathematics.int3 x)
		{
			return x.x + x.y + x.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int csum(global::Unity.Mathematics.int4 x)
		{
			return x.x + x.y + x.z + x.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint csum(global::Unity.Mathematics.uint2 x)
		{
			return x.x + x.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint csum(global::Unity.Mathematics.uint3 x)
		{
			return x.x + x.y + x.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint csum(global::Unity.Mathematics.uint4 x)
		{
			return x.x + x.y + x.z + x.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float csum(global::Unity.Mathematics.float2 x)
		{
			return x.x + x.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float csum(global::Unity.Mathematics.float3 x)
		{
			return x.x + x.y + x.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float csum(global::Unity.Mathematics.float4 x)
		{
			return x.x + x.y + (x.z + x.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double csum(global::Unity.Mathematics.double2 x)
		{
			return x.x + x.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double csum(global::Unity.Mathematics.double3 x)
		{
			return x.x + x.y + x.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double csum(global::Unity.Mathematics.double4 x)
		{
			return x.x + x.y + (x.z + x.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float square(float x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 square(global::Unity.Mathematics.float2 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 square(global::Unity.Mathematics.float3 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 square(global::Unity.Mathematics.float4 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double square(double x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 square(global::Unity.Mathematics.double2 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 square(global::Unity.Mathematics.double3 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 square(global::Unity.Mathematics.double4 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int square(int x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 square(global::Unity.Mathematics.int2 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 square(global::Unity.Mathematics.int3 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 square(global::Unity.Mathematics.int4 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint square(uint x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 square(global::Unity.Mathematics.uint2 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 square(global::Unity.Mathematics.uint3 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 square(global::Unity.Mathematics.uint4 x)
		{
			return x * x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static int compress(int* output, int index, global::Unity.Mathematics.int4 val, global::Unity.Mathematics.bool4 mask)
		{
			if (mask.x)
			{
				output[index++] = val.x;
			}
			if (mask.y)
			{
				output[index++] = val.y;
			}
			if (mask.z)
			{
				output[index++] = val.z;
			}
			if (mask.w)
			{
				output[index++] = val.w;
			}
			return index;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static int compress(uint* output, int index, global::Unity.Mathematics.uint4 val, global::Unity.Mathematics.bool4 mask)
		{
			return compress((int*)output, index, *(global::Unity.Mathematics.int4*)(&val), mask);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static int compress(float* output, int index, global::Unity.Mathematics.float4 val, global::Unity.Mathematics.bool4 mask)
		{
			return compress((int*)output, index, *(global::Unity.Mathematics.int4*)(&val), mask);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float f16tof32(uint x)
		{
			uint num = (x & 0x7FFF) << 13;
			uint num2 = num & 0xF800000;
			uint num3 = num + 939524096 + select(0u, 939524096u, num2 == 260046848);
			return asfloat(select(num3, asuint(asfloat(num3 + 8388608) - 6.1035156E-05f), num2 == 0) | ((x & 0x8000) << 16));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 f16tof32(global::Unity.Mathematics.uint2 x)
		{
			global::Unity.Mathematics.uint2 obj = (x & 32767u) << 13;
			global::Unity.Mathematics.uint2 uint5 = obj & 260046848u;
			global::Unity.Mathematics.uint2 obj2 = obj + 939524096u + select(0u, 939524096u, uint5 == 260046848u);
			return asfloat(select(obj2, asuint(asfloat(obj2 + 8388608u) - 6.1035156E-05f), uint5 == 0u) | ((x & 32768u) << 16));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 f16tof32(global::Unity.Mathematics.uint3 x)
		{
			global::Unity.Mathematics.uint3 obj = (x & 32767u) << 13;
			global::Unity.Mathematics.uint3 uint5 = obj & 260046848u;
			global::Unity.Mathematics.uint3 obj2 = obj + 939524096u + select(0u, 939524096u, uint5 == 260046848u);
			return asfloat(select(obj2, asuint(asfloat(obj2 + 8388608u) - 6.1035156E-05f), uint5 == 0u) | ((x & 32768u) << 16));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 f16tof32(global::Unity.Mathematics.uint4 x)
		{
			global::Unity.Mathematics.uint4 obj = (x & 32767u) << 13;
			global::Unity.Mathematics.uint4 uint5 = obj & 260046848u;
			global::Unity.Mathematics.uint4 obj2 = obj + 939524096u + select(0u, 939524096u, uint5 == 260046848u);
			return asfloat(select(obj2, asuint(asfloat(obj2 + 8388608u) - 6.1035156E-05f), uint5 == 0u) | ((x & 32768u) << 16));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint f32tof16(float x)
		{
			uint num = asuint(x);
			uint num2 = num & 0x7FFFF000;
			return select(asuint(min(asfloat(num2) * 1.92593E-34f, 260042750f)) + 4096 >> 13, select(31744u, 32256u, (int)num2 > 2139095040), (int)num2 >= 2139095040) | ((num & 0x80000FFFu) >> 16);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 f32tof16(global::Unity.Mathematics.float2 x)
		{
			global::Unity.Mathematics.uint2 uint5 = asuint(x);
			global::Unity.Mathematics.uint2 uint6 = uint5 & 2147479552u;
			return select((global::Unity.Mathematics.uint2)(asint(min(asfloat(uint6) * 1.92593E-34f, 260042750f)) + 4096) >> 13, select(31744u, 32256u, (global::Unity.Mathematics.int2)uint6 > 2139095040), (global::Unity.Mathematics.int2)uint6 >= 2139095040) | ((uint5 & 2147487743u) >> 16);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 f32tof16(global::Unity.Mathematics.float3 x)
		{
			global::Unity.Mathematics.uint3 uint5 = asuint(x);
			global::Unity.Mathematics.uint3 uint6 = uint5 & 2147479552u;
			return select((global::Unity.Mathematics.uint3)(asint(min(asfloat(uint6) * 1.92593E-34f, 260042750f)) + 4096) >> 13, select(31744u, 32256u, (global::Unity.Mathematics.int3)uint6 > 2139095040), (global::Unity.Mathematics.int3)uint6 >= 2139095040) | ((uint5 & 2147487743u) >> 16);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 f32tof16(global::Unity.Mathematics.float4 x)
		{
			global::Unity.Mathematics.uint4 uint5 = asuint(x);
			global::Unity.Mathematics.uint4 uint6 = uint5 & 2147479552u;
			return select((global::Unity.Mathematics.uint4)(asint(min(asfloat(uint6) * 1.92593E-34f, 260042750f)) + 4096) >> 13, select(31744u, 32256u, (global::Unity.Mathematics.int4)uint6 > 2139095040), (global::Unity.Mathematics.int4)uint6 >= 2139095040) | ((uint5 & 2147487743u) >> 16);
		}

		public static void orthonormal_basis(global::Unity.Mathematics.float3 normal, out global::Unity.Mathematics.float3 basis1, out global::Unity.Mathematics.float3 basis2)
		{
			float num = ((normal.z >= 0f) ? 1f : (-1f));
			float num2 = -1f / (num + normal.z);
			float num3 = normal.x * normal.y * num2;
			basis1.x = 1f + num * normal.x * normal.x * num2;
			basis1.y = num * num3;
			basis1.z = (0f - num) * normal.x;
			basis2.x = num3;
			basis2.y = num + normal.y * normal.y * num2;
			basis2.z = 0f - normal.y;
		}

		public static void orthonormal_basis(global::Unity.Mathematics.double3 normal, out global::Unity.Mathematics.double3 basis1, out global::Unity.Mathematics.double3 basis2)
		{
			double num = ((normal.z >= 0.0) ? 1.0 : (-1.0));
			double num2 = -1.0 / (num + normal.z);
			double num3 = normal.x * normal.y * num2;
			basis1.x = 1.0 + num * normal.x * normal.x * num2;
			basis1.y = num * num3;
			basis1.z = (0.0 - num) * normal.x;
			basis2.x = num3;
			basis2.y = num + normal.y * normal.y * num2;
			basis2.z = 0.0 - normal.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float chgsign(float x, float y)
		{
			return asfloat(asuint(x) ^ (asuint(y) & 0x80000000u));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 chgsign(global::Unity.Mathematics.float2 x, global::Unity.Mathematics.float2 y)
		{
			return asfloat(asuint(x) ^ (asuint(y) & 2147483648u));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 chgsign(global::Unity.Mathematics.float3 x, global::Unity.Mathematics.float3 y)
		{
			return asfloat(asuint(x) ^ (asuint(y) & 2147483648u));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 chgsign(global::Unity.Mathematics.float4 x, global::Unity.Mathematics.float4 y)
		{
			return asfloat(asuint(x) ^ (asuint(y) & 2147483648u));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private unsafe static uint read32_little_endian(void* pBuffer)
		{
			return (uint)(*(byte*)pBuffer | (((byte*)pBuffer)[1] << 8) | (((byte*)pBuffer)[2] << 16) | (((byte*)pBuffer)[3] << 24));
		}

		private unsafe static uint hash_with_unaligned_loads(void* pBuffer, int numBytes, uint seed)
		{
			global::Unity.Mathematics.uint4* ptr = (global::Unity.Mathematics.uint4*)pBuffer;
			uint num = seed + 374761393;
			if (numBytes >= 16)
			{
				global::Unity.Mathematics.uint4 uint5 = new global::Unity.Mathematics.uint4(606290984u, 2246822519u, 0u, 1640531535u) + seed;
				int num2 = numBytes >> 4;
				for (int i = 0; i < num2; i++)
				{
					uint5 += *(ptr++) * 2246822519u;
					uint5 = (uint5 << 13) | (uint5 >> 19);
					uint5 *= 2654435761u;
				}
				num = rol(uint5.x, 1) + rol(uint5.y, 7) + rol(uint5.z, 12) + rol(uint5.w, 18);
			}
			num += (uint)numBytes;
			uint* ptr2 = (uint*)ptr;
			for (int j = 0; j < ((numBytes >> 2) & 3); j++)
			{
				num += (uint)((int)(*(ptr2++)) * -1028477379);
				num = rol(num, 17) * 668265263;
			}
			byte* ptr3 = (byte*)ptr2;
			for (int k = 0; k < (numBytes & 3); k++)
			{
				num += (uint)(*(ptr3++) * 374761393);
				num = rol(num, 11) * 2654435761u;
			}
			num ^= num >> 15;
			num *= 2246822519u;
			num ^= num >> 13;
			num *= 3266489917u;
			return num ^ (num >> 16);
		}

		private unsafe static uint hash_without_unaligned_loads(void* pBuffer, int numBytes, uint seed)
		{
			byte* ptr = (byte*)pBuffer;
			uint num = seed + 374761393;
			if (numBytes >= 16)
			{
				global::Unity.Mathematics.uint4 x = new global::Unity.Mathematics.uint4(606290984u, 2246822519u, 0u, 1640531535u) + seed;
				int num2 = numBytes >> 4;
				for (int i = 0; i < num2; i++)
				{
					global::Unity.Mathematics.uint4 uint5 = new global::Unity.Mathematics.uint4(read32_little_endian(ptr), read32_little_endian(ptr + 4), read32_little_endian(ptr + 8), read32_little_endian(ptr + 12));
					x += uint5 * 2246822519u;
					x = rol(x, 13);
					x *= 2654435761u;
					ptr += 16;
				}
				num = rol(x.x, 1) + rol(x.y, 7) + rol(x.z, 12) + rol(x.w, 18);
			}
			num += (uint)numBytes;
			for (int j = 0; j < ((numBytes >> 2) & 3); j++)
			{
				num += (uint)((int)read32_little_endian(ptr) * -1028477379);
				num = rol(num, 17) * 668265263;
				ptr += 4;
			}
			for (int k = 0; k < (numBytes & 3); k++)
			{
				num += (uint)(*(ptr++) * 374761393);
				num = rol(num, 11) * 2654435761u;
			}
			num ^= num >> 15;
			num *= 2246822519u;
			num ^= num >> 13;
			num *= 3266489917u;
			return num ^ (num >> 16);
		}

		public unsafe static uint hash(void* pBuffer, int numBytes, uint seed = 0u)
		{
			return hash_with_unaligned_loads(pBuffer, numBytes, seed);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 up()
		{
			return new global::Unity.Mathematics.float3(0f, 1f, 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 down()
		{
			return new global::Unity.Mathematics.float3(0f, -1f, 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 forward()
		{
			return new global::Unity.Mathematics.float3(0f, 0f, 1f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 back()
		{
			return new global::Unity.Mathematics.float3(0f, 0f, -1f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 left()
		{
			return new global::Unity.Mathematics.float3(-1f, 0f, 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 right()
		{
			return new global::Unity.Mathematics.float3(1f, 0f, 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 EulerXYZ(global::Unity.Mathematics.quaternion q)
		{
			global::Unity.Mathematics.float4 value = q.value;
			global::Unity.Mathematics.float4 float5 = value * value.wwww * float4(2f);
			global::Unity.Mathematics.float4 float6 = value * value.yzxw * float4(2f);
			global::Unity.Mathematics.float4 float7 = value * value;
			global::Unity.Mathematics.float3 zero = global::Unity.Mathematics.float3.zero;
			float num = float6.z - float5.y;
			if (num * num < 0.99999595f)
			{
				float y = float6.y + float5.x;
				float x = float7.z + float7.w - float7.y - float7.x;
				float y2 = float6.x + float5.z;
				return float3(z: atan2(y2, float7.x + float7.w - float7.y - float7.z), x: atan2(y, x), y: 0f - asin(num));
			}
			num = clamp(num, -1f, 1f);
			global::Unity.Mathematics.float4 float8 = float4(float6.z, float5.y, float6.x, float5.z);
			float y3 = 2f * (float8.x * float8.w + float8.y * float8.z);
			float x2 = csum(float8 * float8 * float4(-1f, 1f, -1f, 1f));
			return float3(atan2(y3, x2), 0f - asin(num), 0f);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 EulerXZY(global::Unity.Mathematics.quaternion q)
		{
			global::Unity.Mathematics.float4 value = q.value;
			global::Unity.Mathematics.float4 float5 = value * value.wwww * float4(2f);
			global::Unity.Mathematics.float4 float6 = value * value.yzxw * float4(2f);
			global::Unity.Mathematics.float4 float7 = value * value;
			global::Unity.Mathematics.float3 zero = global::Unity.Mathematics.float3.zero;
			float num = float6.x + float5.z;
			if (num * num < 0.99999595f)
			{
				float y = 0f - float6.y + float5.x;
				float x = float7.y + float7.w - float7.z - float7.x;
				float y2 = 0f - float6.z + float5.y;
				zero = float3(z: atan2(y2, float7.x + float7.w - float7.y - float7.z), x: atan2(y, x), y: asin(num));
			}
			else
			{
				num = clamp(num, -1f, 1f);
				global::Unity.Mathematics.float4 float8 = float4(float6.x, float5.z, float6.z, float5.y);
				float y3 = 2f * (float8.x * float8.w + float8.y * float8.z);
				float x2 = csum(float8 * float8 * float4(-1f, 1f, -1f, 1f));
				zero = float3(atan2(y3, x2), asin(num), 0f);
			}
			return zero.xzy;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 EulerYXZ(global::Unity.Mathematics.quaternion q)
		{
			global::Unity.Mathematics.float4 value = q.value;
			global::Unity.Mathematics.float4 float5 = value * value.wwww * float4(2f);
			global::Unity.Mathematics.float4 float6 = value * value.yzxw * float4(2f);
			global::Unity.Mathematics.float4 float7 = value * value;
			global::Unity.Mathematics.float3 zero = global::Unity.Mathematics.float3.zero;
			float num = float6.y + float5.x;
			if (num * num < 0.99999595f)
			{
				float y = 0f - float6.z + float5.y;
				float x = float7.z + float7.w - float7.x - float7.y;
				float y2 = 0f - float6.x + float5.z;
				zero = float3(z: atan2(y2, float7.y + float7.w - float7.z - float7.x), x: atan2(y, x), y: asin(num));
			}
			else
			{
				num = clamp(num, -1f, 1f);
				global::Unity.Mathematics.float4 float8 = float4(float6.x, float5.z, float6.y, float5.x);
				float y3 = 2f * (float8.x * float8.w + float8.y * float8.z);
				float x2 = csum(float8 * float8 * float4(-1f, 1f, -1f, 1f));
				zero = float3(atan2(y3, x2), asin(num), 0f);
			}
			return zero.yxz;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 EulerYZX(global::Unity.Mathematics.quaternion q)
		{
			global::Unity.Mathematics.float4 value = q.value;
			global::Unity.Mathematics.float4 float5 = value * value.wwww * float4(2f);
			global::Unity.Mathematics.float4 float6 = value * value.yzxw * float4(2f);
			global::Unity.Mathematics.float4 float7 = value * value;
			global::Unity.Mathematics.float3 zero = global::Unity.Mathematics.float3.zero;
			float num = float6.x - float5.z;
			if (num * num < 0.99999595f)
			{
				float y = float6.z + float5.y;
				float x = float7.x + float7.w - float7.z - float7.y;
				float y2 = float6.y + float5.x;
				zero = float3(z: atan2(y2, float7.y + float7.w - float7.x - float7.z), x: atan2(y, x), y: 0f - asin(num));
			}
			else
			{
				num = clamp(num, -1f, 1f);
				global::Unity.Mathematics.float4 float8 = float4(float6.x, float5.z, float6.y, float5.x);
				float y3 = 2f * (float8.x * float8.w + float8.y * float8.z);
				float x2 = csum(float8 * float8 * float4(-1f, 1f, -1f, 1f));
				zero = float3(atan2(y3, x2), 0f - asin(num), 0f);
			}
			return zero.zxy;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 EulerZXY(global::Unity.Mathematics.quaternion q)
		{
			global::Unity.Mathematics.float4 value = q.value;
			global::Unity.Mathematics.float4 float5 = value * value.wwww * float4(2f);
			global::Unity.Mathematics.float4 float6 = value * value.yzxw * float4(2f);
			global::Unity.Mathematics.float4 float7 = value * value;
			global::Unity.Mathematics.float3 zero = global::Unity.Mathematics.float3.zero;
			float num = float6.y - float5.x;
			if (num * num < 0.99999595f)
			{
				float y = float6.x + float5.z;
				float x = float7.y + float7.w - float7.x - float7.z;
				float y2 = float6.z + float5.y;
				zero = float3(z: atan2(y2, float7.z + float7.w - float7.x - float7.y), x: atan2(y, x), y: 0f - asin(num));
			}
			else
			{
				num = clamp(num, -1f, 1f);
				global::Unity.Mathematics.float4 float8 = float4(float6.z, float5.y, float6.y, float5.x);
				float y3 = 2f * (float8.x * float8.w + float8.y * float8.z);
				float x2 = csum(float8 * float8 * float4(-1f, 1f, -1f, 1f));
				zero = float3(atan2(y3, x2), 0f - asin(num), 0f);
			}
			return zero.yzx;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 EulerZYX(global::Unity.Mathematics.quaternion q)
		{
			global::Unity.Mathematics.float4 value = q.value;
			global::Unity.Mathematics.float4 float5 = value * value.wwww * float4(2f);
			global::Unity.Mathematics.float4 float6 = value * value.yzxw * float4(2f);
			global::Unity.Mathematics.float4 float7 = value * value;
			global::Unity.Mathematics.float3 zero = global::Unity.Mathematics.float3.zero;
			float num = float6.z + float5.y;
			if (num * num < 0.99999595f)
			{
				float y = 0f - float6.x + float5.z;
				float x = float7.x + float7.w - float7.y - float7.z;
				float y2 = 0f - float6.y + float5.x;
				zero = float3(z: atan2(y2, float7.z + float7.w - float7.y - float7.x), x: atan2(y, x), y: asin(num));
			}
			else
			{
				num = clamp(num, -1f, 1f);
				global::Unity.Mathematics.float4 float8 = float4(float6.z, float5.y, float6.y, float5.x);
				float y3 = 2f * (float8.x * float8.w + float8.y * float8.z);
				float x2 = csum(float8 * float8 * float4(-1f, 1f, -1f, 1f));
				zero = float3(atan2(y3, x2), asin(num), 0f);
			}
			return zero.zyx;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 Euler(global::Unity.Mathematics.quaternion q, global::Unity.Mathematics.math.RotationOrder order = global::Unity.Mathematics.math.RotationOrder.ZXY)
		{
			return order switch
			{
				global::Unity.Mathematics.math.RotationOrder.XYZ => EulerXYZ(q), 
				global::Unity.Mathematics.math.RotationOrder.XZY => EulerXZY(q), 
				global::Unity.Mathematics.math.RotationOrder.YXZ => EulerYXZ(q), 
				global::Unity.Mathematics.math.RotationOrder.YZX => EulerYZX(q), 
				global::Unity.Mathematics.math.RotationOrder.ZXY => EulerZXY(q), 
				global::Unity.Mathematics.math.RotationOrder.ZYX => EulerZYX(q), 
				_ => global::Unity.Mathematics.float3.zero, 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 mulScale(global::Unity.Mathematics.float3x3 m, global::Unity.Mathematics.float3 s)
		{
			return new global::Unity.Mathematics.float3x3(m.c0 * s.x, m.c1 * s.y, m.c2 * s.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 scaleMul(global::Unity.Mathematics.float3 s, global::Unity.Mathematics.float3x3 m)
		{
			return new global::Unity.Mathematics.float3x3(m.c0 * s, m.c1 * s, m.c2 * s);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::Unity.Mathematics.float4 unpacklo(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4 b)
		{
			return shuffle(a, b, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.RightY);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::Unity.Mathematics.double4 unpacklo(global::Unity.Mathematics.double4 a, global::Unity.Mathematics.double4 b)
		{
			return shuffle(a, b, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.RightY);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::Unity.Mathematics.float4 unpackhi(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4 b)
		{
			return shuffle(a, b, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightZ, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.RightW);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::Unity.Mathematics.double4 unpackhi(global::Unity.Mathematics.double4 a, global::Unity.Mathematics.double4 b)
		{
			return shuffle(a, b, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.RightZ, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.RightW);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::Unity.Mathematics.float4 movelh(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4 b)
		{
			return shuffle(a, b, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.RightY);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::Unity.Mathematics.double4 movelh(global::Unity.Mathematics.double4 a, global::Unity.Mathematics.double4 b)
		{
			return shuffle(a, b, global::Unity.Mathematics.math.ShuffleComponent.LeftX, global::Unity.Mathematics.math.ShuffleComponent.LeftY, global::Unity.Mathematics.math.ShuffleComponent.RightX, global::Unity.Mathematics.math.ShuffleComponent.RightY);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::Unity.Mathematics.float4 movehl(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4 b)
		{
			return shuffle(b, a, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.RightZ, global::Unity.Mathematics.math.ShuffleComponent.RightW);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::Unity.Mathematics.double4 movehl(global::Unity.Mathematics.double4 a, global::Unity.Mathematics.double4 b)
		{
			return shuffle(b, a, global::Unity.Mathematics.math.ShuffleComponent.LeftZ, global::Unity.Mathematics.math.ShuffleComponent.LeftW, global::Unity.Mathematics.math.ShuffleComponent.RightZ, global::Unity.Mathematics.math.ShuffleComponent.RightW);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static uint fold_to_uint(double x)
		{
			global::Unity.Mathematics.math.LongDoubleUnion longDoubleUnion = default(global::Unity.Mathematics.math.LongDoubleUnion);
			longDoubleUnion.longValue = 0L;
			longDoubleUnion.doubleValue = x;
			return (uint)((int)(longDoubleUnion.longValue >> 32) ^ (int)longDoubleUnion.longValue);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::Unity.Mathematics.uint2 fold_to_uint(global::Unity.Mathematics.double2 x)
		{
			return uint2(fold_to_uint(x.x), fold_to_uint(x.y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::Unity.Mathematics.uint3 fold_to_uint(global::Unity.Mathematics.double3 x)
		{
			return uint3(fold_to_uint(x.x), fold_to_uint(x.y), fold_to_uint(x.z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static global::Unity.Mathematics.uint4 fold_to_uint(global::Unity.Mathematics.double4 x)
		{
			return uint4(fold_to_uint(x.x), fold_to_uint(x.y), fold_to_uint(x.z), fold_to_uint(x.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(global::Unity.Mathematics.float4x4 f4x4)
		{
			return new global::Unity.Mathematics.float3x3(f4x4);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 float3x3(global::Unity.Mathematics.quaternion rotation)
		{
			return new global::Unity.Mathematics.float3x3(rotation);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(global::Unity.Mathematics.float3x3 rotation, global::Unity.Mathematics.float3 translation)
		{
			return new global::Unity.Mathematics.float4x4(rotation, translation);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(global::Unity.Mathematics.quaternion rotation, global::Unity.Mathematics.float3 translation)
		{
			return new global::Unity.Mathematics.float4x4(rotation, translation);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 float4x4(global::Unity.Mathematics.RigidTransform transform)
		{
			return new global::Unity.Mathematics.float4x4(transform);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 orthonormalize(global::Unity.Mathematics.float3x3 i)
		{
			global::Unity.Mathematics.float3 c = i.c0;
			global::Unity.Mathematics.float3 float5 = i.c1 - i.c0 * dot(i.c1, i.c0);
			float num = length(c);
			float num2 = length(float5);
			bool test = num > 1E-30f && num2 > 1E-30f;
			global::Unity.Mathematics.float3x3 result = default(global::Unity.Mathematics.float3x3);
			result.c0 = select(float3(1f, 0f, 0f), c / num, test);
			result.c1 = select(float3(0f, 1f, 0f), float5 / num2, test);
			result.c2 = cross(result.c0, result.c1);
			return result;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 pseudoinverse(global::Unity.Mathematics.float3x3 m)
		{
			float num = 0.333333f * (lengthsq(m.c0) + lengthsq(m.c1) + lengthsq(m.c2));
			if (num < 1E-30f)
			{
				return global::Unity.Mathematics.float3x3.zero;
			}
			global::Unity.Mathematics.float3 s = rsqrt(num);
			global::Unity.Mathematics.float3x3 float3x5 = mulScale(m, s);
			if (!adjInverse(float3x5, out var i, 1E-06f))
			{
				i = global::Unity.Mathematics.svd.svdInverse(float3x5);
			}
			return mulScale(i, s);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float mul(float a, float b)
		{
			return a * b;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float mul(global::Unity.Mathematics.float2 a, global::Unity.Mathematics.float2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 mul(global::Unity.Mathematics.float2 a, global::Unity.Mathematics.float2x2 b)
		{
			return float2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 mul(global::Unity.Mathematics.float2 a, global::Unity.Mathematics.float2x3 b)
		{
			return float3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 mul(global::Unity.Mathematics.float2 a, global::Unity.Mathematics.float2x4 b)
		{
			return float4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float mul(global::Unity.Mathematics.float3 a, global::Unity.Mathematics.float3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 mul(global::Unity.Mathematics.float3 a, global::Unity.Mathematics.float3x2 b)
		{
			return float2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 mul(global::Unity.Mathematics.float3 a, global::Unity.Mathematics.float3x3 b)
		{
			return float3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 mul(global::Unity.Mathematics.float3 a, global::Unity.Mathematics.float3x4 b)
		{
			return float4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float mul(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 mul(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4x2 b)
		{
			return float2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 mul(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4x3 b)
		{
			return float3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 mul(global::Unity.Mathematics.float4 a, global::Unity.Mathematics.float4x4 b)
		{
			return float4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 mul(global::Unity.Mathematics.float2x2 a, global::Unity.Mathematics.float2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 mul(global::Unity.Mathematics.float2x2 a, global::Unity.Mathematics.float2x2 b)
		{
			return float2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 mul(global::Unity.Mathematics.float2x2 a, global::Unity.Mathematics.float2x3 b)
		{
			return float2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 mul(global::Unity.Mathematics.float2x2 a, global::Unity.Mathematics.float2x4 b)
		{
			return float2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 mul(global::Unity.Mathematics.float2x3 a, global::Unity.Mathematics.float3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 mul(global::Unity.Mathematics.float2x3 a, global::Unity.Mathematics.float3x2 b)
		{
			return float2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 mul(global::Unity.Mathematics.float2x3 a, global::Unity.Mathematics.float3x3 b)
		{
			return float2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 mul(global::Unity.Mathematics.float2x3 a, global::Unity.Mathematics.float3x4 b)
		{
			return float2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2 mul(global::Unity.Mathematics.float2x4 a, global::Unity.Mathematics.float4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x2 mul(global::Unity.Mathematics.float2x4 a, global::Unity.Mathematics.float4x2 b)
		{
			return float2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x3 mul(global::Unity.Mathematics.float2x4 a, global::Unity.Mathematics.float4x3 b)
		{
			return float2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float2x4 mul(global::Unity.Mathematics.float2x4 a, global::Unity.Mathematics.float4x4 b)
		{
			return float2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 mul(global::Unity.Mathematics.float3x2 a, global::Unity.Mathematics.float2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 mul(global::Unity.Mathematics.float3x2 a, global::Unity.Mathematics.float2x2 b)
		{
			return float3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 mul(global::Unity.Mathematics.float3x2 a, global::Unity.Mathematics.float2x3 b)
		{
			return float3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 mul(global::Unity.Mathematics.float3x2 a, global::Unity.Mathematics.float2x4 b)
		{
			return float3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 mul(global::Unity.Mathematics.float3x3 a, global::Unity.Mathematics.float3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 mul(global::Unity.Mathematics.float3x3 a, global::Unity.Mathematics.float3x2 b)
		{
			return float3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 mul(global::Unity.Mathematics.float3x3 a, global::Unity.Mathematics.float3x3 b)
		{
			return float3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 mul(global::Unity.Mathematics.float3x3 a, global::Unity.Mathematics.float3x4 b)
		{
			return float3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 mul(global::Unity.Mathematics.float3x4 a, global::Unity.Mathematics.float4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x2 mul(global::Unity.Mathematics.float3x4 a, global::Unity.Mathematics.float4x2 b)
		{
			return float3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x3 mul(global::Unity.Mathematics.float3x4 a, global::Unity.Mathematics.float4x3 b)
		{
			return float3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3x4 mul(global::Unity.Mathematics.float3x4 a, global::Unity.Mathematics.float4x4 b)
		{
			return float3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 mul(global::Unity.Mathematics.float4x2 a, global::Unity.Mathematics.float2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 mul(global::Unity.Mathematics.float4x2 a, global::Unity.Mathematics.float2x2 b)
		{
			return float4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 mul(global::Unity.Mathematics.float4x2 a, global::Unity.Mathematics.float2x3 b)
		{
			return float4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 mul(global::Unity.Mathematics.float4x2 a, global::Unity.Mathematics.float2x4 b)
		{
			return float4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 mul(global::Unity.Mathematics.float4x3 a, global::Unity.Mathematics.float3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 mul(global::Unity.Mathematics.float4x3 a, global::Unity.Mathematics.float3x2 b)
		{
			return float4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 mul(global::Unity.Mathematics.float4x3 a, global::Unity.Mathematics.float3x3 b)
		{
			return float4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 mul(global::Unity.Mathematics.float4x3 a, global::Unity.Mathematics.float3x4 b)
		{
			return float4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 mul(global::Unity.Mathematics.float4x4 a, global::Unity.Mathematics.float4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x2 mul(global::Unity.Mathematics.float4x4 a, global::Unity.Mathematics.float4x2 b)
		{
			return float4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x3 mul(global::Unity.Mathematics.float4x4 a, global::Unity.Mathematics.float4x3 b)
		{
			return float4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4x4 mul(global::Unity.Mathematics.float4x4 a, global::Unity.Mathematics.float4x4 b)
		{
			return float4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double mul(double a, double b)
		{
			return a * b;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double mul(global::Unity.Mathematics.double2 a, global::Unity.Mathematics.double2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 mul(global::Unity.Mathematics.double2 a, global::Unity.Mathematics.double2x2 b)
		{
			return double2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 mul(global::Unity.Mathematics.double2 a, global::Unity.Mathematics.double2x3 b)
		{
			return double3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 mul(global::Unity.Mathematics.double2 a, global::Unity.Mathematics.double2x4 b)
		{
			return double4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double mul(global::Unity.Mathematics.double3 a, global::Unity.Mathematics.double3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 mul(global::Unity.Mathematics.double3 a, global::Unity.Mathematics.double3x2 b)
		{
			return double2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 mul(global::Unity.Mathematics.double3 a, global::Unity.Mathematics.double3x3 b)
		{
			return double3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 mul(global::Unity.Mathematics.double3 a, global::Unity.Mathematics.double3x4 b)
		{
			return double4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static double mul(global::Unity.Mathematics.double4 a, global::Unity.Mathematics.double4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 mul(global::Unity.Mathematics.double4 a, global::Unity.Mathematics.double4x2 b)
		{
			return double2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 mul(global::Unity.Mathematics.double4 a, global::Unity.Mathematics.double4x3 b)
		{
			return double3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 mul(global::Unity.Mathematics.double4 a, global::Unity.Mathematics.double4x4 b)
		{
			return double4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 mul(global::Unity.Mathematics.double2x2 a, global::Unity.Mathematics.double2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 mul(global::Unity.Mathematics.double2x2 a, global::Unity.Mathematics.double2x2 b)
		{
			return double2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 mul(global::Unity.Mathematics.double2x2 a, global::Unity.Mathematics.double2x3 b)
		{
			return double2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 mul(global::Unity.Mathematics.double2x2 a, global::Unity.Mathematics.double2x4 b)
		{
			return double2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 mul(global::Unity.Mathematics.double2x3 a, global::Unity.Mathematics.double3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 mul(global::Unity.Mathematics.double2x3 a, global::Unity.Mathematics.double3x2 b)
		{
			return double2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 mul(global::Unity.Mathematics.double2x3 a, global::Unity.Mathematics.double3x3 b)
		{
			return double2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 mul(global::Unity.Mathematics.double2x3 a, global::Unity.Mathematics.double3x4 b)
		{
			return double2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2 mul(global::Unity.Mathematics.double2x4 a, global::Unity.Mathematics.double4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x2 mul(global::Unity.Mathematics.double2x4 a, global::Unity.Mathematics.double4x2 b)
		{
			return double2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x3 mul(global::Unity.Mathematics.double2x4 a, global::Unity.Mathematics.double4x3 b)
		{
			return double2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double2x4 mul(global::Unity.Mathematics.double2x4 a, global::Unity.Mathematics.double4x4 b)
		{
			return double2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 mul(global::Unity.Mathematics.double3x2 a, global::Unity.Mathematics.double2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 mul(global::Unity.Mathematics.double3x2 a, global::Unity.Mathematics.double2x2 b)
		{
			return double3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 mul(global::Unity.Mathematics.double3x2 a, global::Unity.Mathematics.double2x3 b)
		{
			return double3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 mul(global::Unity.Mathematics.double3x2 a, global::Unity.Mathematics.double2x4 b)
		{
			return double3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 mul(global::Unity.Mathematics.double3x3 a, global::Unity.Mathematics.double3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 mul(global::Unity.Mathematics.double3x3 a, global::Unity.Mathematics.double3x2 b)
		{
			return double3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 mul(global::Unity.Mathematics.double3x3 a, global::Unity.Mathematics.double3x3 b)
		{
			return double3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 mul(global::Unity.Mathematics.double3x3 a, global::Unity.Mathematics.double3x4 b)
		{
			return double3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3 mul(global::Unity.Mathematics.double3x4 a, global::Unity.Mathematics.double4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x2 mul(global::Unity.Mathematics.double3x4 a, global::Unity.Mathematics.double4x2 b)
		{
			return double3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x3 mul(global::Unity.Mathematics.double3x4 a, global::Unity.Mathematics.double4x3 b)
		{
			return double3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double3x4 mul(global::Unity.Mathematics.double3x4 a, global::Unity.Mathematics.double4x4 b)
		{
			return double3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 mul(global::Unity.Mathematics.double4x2 a, global::Unity.Mathematics.double2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 mul(global::Unity.Mathematics.double4x2 a, global::Unity.Mathematics.double2x2 b)
		{
			return double4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 mul(global::Unity.Mathematics.double4x2 a, global::Unity.Mathematics.double2x3 b)
		{
			return double4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 mul(global::Unity.Mathematics.double4x2 a, global::Unity.Mathematics.double2x4 b)
		{
			return double4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 mul(global::Unity.Mathematics.double4x3 a, global::Unity.Mathematics.double3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 mul(global::Unity.Mathematics.double4x3 a, global::Unity.Mathematics.double3x2 b)
		{
			return double4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 mul(global::Unity.Mathematics.double4x3 a, global::Unity.Mathematics.double3x3 b)
		{
			return double4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 mul(global::Unity.Mathematics.double4x3 a, global::Unity.Mathematics.double3x4 b)
		{
			return double4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4 mul(global::Unity.Mathematics.double4x4 a, global::Unity.Mathematics.double4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x2 mul(global::Unity.Mathematics.double4x4 a, global::Unity.Mathematics.double4x2 b)
		{
			return double4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x3 mul(global::Unity.Mathematics.double4x4 a, global::Unity.Mathematics.double4x3 b)
		{
			return double4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.double4x4 mul(global::Unity.Mathematics.double4x4 a, global::Unity.Mathematics.double4x4 b)
		{
			return double4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int mul(int a, int b)
		{
			return a * b;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int mul(global::Unity.Mathematics.int2 a, global::Unity.Mathematics.int2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 mul(global::Unity.Mathematics.int2 a, global::Unity.Mathematics.int2x2 b)
		{
			return int2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 mul(global::Unity.Mathematics.int2 a, global::Unity.Mathematics.int2x3 b)
		{
			return int3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 mul(global::Unity.Mathematics.int2 a, global::Unity.Mathematics.int2x4 b)
		{
			return int4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int mul(global::Unity.Mathematics.int3 a, global::Unity.Mathematics.int3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 mul(global::Unity.Mathematics.int3 a, global::Unity.Mathematics.int3x2 b)
		{
			return int2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 mul(global::Unity.Mathematics.int3 a, global::Unity.Mathematics.int3x3 b)
		{
			return int3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 mul(global::Unity.Mathematics.int3 a, global::Unity.Mathematics.int3x4 b)
		{
			return int4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int mul(global::Unity.Mathematics.int4 a, global::Unity.Mathematics.int4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 mul(global::Unity.Mathematics.int4 a, global::Unity.Mathematics.int4x2 b)
		{
			return int2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 mul(global::Unity.Mathematics.int4 a, global::Unity.Mathematics.int4x3 b)
		{
			return int3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 mul(global::Unity.Mathematics.int4 a, global::Unity.Mathematics.int4x4 b)
		{
			return int4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 mul(global::Unity.Mathematics.int2x2 a, global::Unity.Mathematics.int2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 mul(global::Unity.Mathematics.int2x2 a, global::Unity.Mathematics.int2x2 b)
		{
			return int2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 mul(global::Unity.Mathematics.int2x2 a, global::Unity.Mathematics.int2x3 b)
		{
			return int2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 mul(global::Unity.Mathematics.int2x2 a, global::Unity.Mathematics.int2x4 b)
		{
			return int2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 mul(global::Unity.Mathematics.int2x3 a, global::Unity.Mathematics.int3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 mul(global::Unity.Mathematics.int2x3 a, global::Unity.Mathematics.int3x2 b)
		{
			return int2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 mul(global::Unity.Mathematics.int2x3 a, global::Unity.Mathematics.int3x3 b)
		{
			return int2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 mul(global::Unity.Mathematics.int2x3 a, global::Unity.Mathematics.int3x4 b)
		{
			return int2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2 mul(global::Unity.Mathematics.int2x4 a, global::Unity.Mathematics.int4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x2 mul(global::Unity.Mathematics.int2x4 a, global::Unity.Mathematics.int4x2 b)
		{
			return int2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x3 mul(global::Unity.Mathematics.int2x4 a, global::Unity.Mathematics.int4x3 b)
		{
			return int2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int2x4 mul(global::Unity.Mathematics.int2x4 a, global::Unity.Mathematics.int4x4 b)
		{
			return int2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 mul(global::Unity.Mathematics.int3x2 a, global::Unity.Mathematics.int2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 mul(global::Unity.Mathematics.int3x2 a, global::Unity.Mathematics.int2x2 b)
		{
			return int3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 mul(global::Unity.Mathematics.int3x2 a, global::Unity.Mathematics.int2x3 b)
		{
			return int3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 mul(global::Unity.Mathematics.int3x2 a, global::Unity.Mathematics.int2x4 b)
		{
			return int3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 mul(global::Unity.Mathematics.int3x3 a, global::Unity.Mathematics.int3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 mul(global::Unity.Mathematics.int3x3 a, global::Unity.Mathematics.int3x2 b)
		{
			return int3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 mul(global::Unity.Mathematics.int3x3 a, global::Unity.Mathematics.int3x3 b)
		{
			return int3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 mul(global::Unity.Mathematics.int3x3 a, global::Unity.Mathematics.int3x4 b)
		{
			return int3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3 mul(global::Unity.Mathematics.int3x4 a, global::Unity.Mathematics.int4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x2 mul(global::Unity.Mathematics.int3x4 a, global::Unity.Mathematics.int4x2 b)
		{
			return int3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x3 mul(global::Unity.Mathematics.int3x4 a, global::Unity.Mathematics.int4x3 b)
		{
			return int3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int3x4 mul(global::Unity.Mathematics.int3x4 a, global::Unity.Mathematics.int4x4 b)
		{
			return int3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 mul(global::Unity.Mathematics.int4x2 a, global::Unity.Mathematics.int2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 mul(global::Unity.Mathematics.int4x2 a, global::Unity.Mathematics.int2x2 b)
		{
			return int4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 mul(global::Unity.Mathematics.int4x2 a, global::Unity.Mathematics.int2x3 b)
		{
			return int4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 mul(global::Unity.Mathematics.int4x2 a, global::Unity.Mathematics.int2x4 b)
		{
			return int4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 mul(global::Unity.Mathematics.int4x3 a, global::Unity.Mathematics.int3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 mul(global::Unity.Mathematics.int4x3 a, global::Unity.Mathematics.int3x2 b)
		{
			return int4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 mul(global::Unity.Mathematics.int4x3 a, global::Unity.Mathematics.int3x3 b)
		{
			return int4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 mul(global::Unity.Mathematics.int4x3 a, global::Unity.Mathematics.int3x4 b)
		{
			return int4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4 mul(global::Unity.Mathematics.int4x4 a, global::Unity.Mathematics.int4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x2 mul(global::Unity.Mathematics.int4x4 a, global::Unity.Mathematics.int4x2 b)
		{
			return int4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x3 mul(global::Unity.Mathematics.int4x4 a, global::Unity.Mathematics.int4x3 b)
		{
			return int4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.int4x4 mul(global::Unity.Mathematics.int4x4 a, global::Unity.Mathematics.int4x4 b)
		{
			return int4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint mul(uint a, uint b)
		{
			return a * b;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint mul(global::Unity.Mathematics.uint2 a, global::Unity.Mathematics.uint2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 mul(global::Unity.Mathematics.uint2 a, global::Unity.Mathematics.uint2x2 b)
		{
			return uint2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 mul(global::Unity.Mathematics.uint2 a, global::Unity.Mathematics.uint2x3 b)
		{
			return uint3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 mul(global::Unity.Mathematics.uint2 a, global::Unity.Mathematics.uint2x4 b)
		{
			return uint4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint mul(global::Unity.Mathematics.uint3 a, global::Unity.Mathematics.uint3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 mul(global::Unity.Mathematics.uint3 a, global::Unity.Mathematics.uint3x2 b)
		{
			return uint2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 mul(global::Unity.Mathematics.uint3 a, global::Unity.Mathematics.uint3x3 b)
		{
			return uint3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 mul(global::Unity.Mathematics.uint3 a, global::Unity.Mathematics.uint3x4 b)
		{
			return uint4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint mul(global::Unity.Mathematics.uint4 a, global::Unity.Mathematics.uint4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 mul(global::Unity.Mathematics.uint4 a, global::Unity.Mathematics.uint4x2 b)
		{
			return uint2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 mul(global::Unity.Mathematics.uint4 a, global::Unity.Mathematics.uint4x3 b)
		{
			return uint3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 mul(global::Unity.Mathematics.uint4 a, global::Unity.Mathematics.uint4x4 b)
		{
			return uint4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 mul(global::Unity.Mathematics.uint2x2 a, global::Unity.Mathematics.uint2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 mul(global::Unity.Mathematics.uint2x2 a, global::Unity.Mathematics.uint2x2 b)
		{
			return uint2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 mul(global::Unity.Mathematics.uint2x2 a, global::Unity.Mathematics.uint2x3 b)
		{
			return uint2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 mul(global::Unity.Mathematics.uint2x2 a, global::Unity.Mathematics.uint2x4 b)
		{
			return uint2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 mul(global::Unity.Mathematics.uint2x3 a, global::Unity.Mathematics.uint3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 mul(global::Unity.Mathematics.uint2x3 a, global::Unity.Mathematics.uint3x2 b)
		{
			return uint2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 mul(global::Unity.Mathematics.uint2x3 a, global::Unity.Mathematics.uint3x3 b)
		{
			return uint2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 mul(global::Unity.Mathematics.uint2x3 a, global::Unity.Mathematics.uint3x4 b)
		{
			return uint2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 mul(global::Unity.Mathematics.uint2x4 a, global::Unity.Mathematics.uint4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 mul(global::Unity.Mathematics.uint2x4 a, global::Unity.Mathematics.uint4x2 b)
		{
			return uint2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 mul(global::Unity.Mathematics.uint2x4 a, global::Unity.Mathematics.uint4x3 b)
		{
			return uint2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 mul(global::Unity.Mathematics.uint2x4 a, global::Unity.Mathematics.uint4x4 b)
		{
			return uint2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 mul(global::Unity.Mathematics.uint3x2 a, global::Unity.Mathematics.uint2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 mul(global::Unity.Mathematics.uint3x2 a, global::Unity.Mathematics.uint2x2 b)
		{
			return uint3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 mul(global::Unity.Mathematics.uint3x2 a, global::Unity.Mathematics.uint2x3 b)
		{
			return uint3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 mul(global::Unity.Mathematics.uint3x2 a, global::Unity.Mathematics.uint2x4 b)
		{
			return uint3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 mul(global::Unity.Mathematics.uint3x3 a, global::Unity.Mathematics.uint3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 mul(global::Unity.Mathematics.uint3x3 a, global::Unity.Mathematics.uint3x2 b)
		{
			return uint3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 mul(global::Unity.Mathematics.uint3x3 a, global::Unity.Mathematics.uint3x3 b)
		{
			return uint3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 mul(global::Unity.Mathematics.uint3x3 a, global::Unity.Mathematics.uint3x4 b)
		{
			return uint3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 mul(global::Unity.Mathematics.uint3x4 a, global::Unity.Mathematics.uint4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 mul(global::Unity.Mathematics.uint3x4 a, global::Unity.Mathematics.uint4x2 b)
		{
			return uint3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 mul(global::Unity.Mathematics.uint3x4 a, global::Unity.Mathematics.uint4x3 b)
		{
			return uint3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 mul(global::Unity.Mathematics.uint3x4 a, global::Unity.Mathematics.uint4x4 b)
		{
			return uint3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 mul(global::Unity.Mathematics.uint4x2 a, global::Unity.Mathematics.uint2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 mul(global::Unity.Mathematics.uint4x2 a, global::Unity.Mathematics.uint2x2 b)
		{
			return uint4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 mul(global::Unity.Mathematics.uint4x2 a, global::Unity.Mathematics.uint2x3 b)
		{
			return uint4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 mul(global::Unity.Mathematics.uint4x2 a, global::Unity.Mathematics.uint2x4 b)
		{
			return uint4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 mul(global::Unity.Mathematics.uint4x3 a, global::Unity.Mathematics.uint3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 mul(global::Unity.Mathematics.uint4x3 a, global::Unity.Mathematics.uint3x2 b)
		{
			return uint4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 mul(global::Unity.Mathematics.uint4x3 a, global::Unity.Mathematics.uint3x3 b)
		{
			return uint4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 mul(global::Unity.Mathematics.uint4x3 a, global::Unity.Mathematics.uint3x4 b)
		{
			return uint4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 mul(global::Unity.Mathematics.uint4x4 a, global::Unity.Mathematics.uint4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 mul(global::Unity.Mathematics.uint4x4 a, global::Unity.Mathematics.uint4x2 b)
		{
			return uint4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 mul(global::Unity.Mathematics.uint4x4 a, global::Unity.Mathematics.uint4x3 b)
		{
			return uint4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 mul(global::Unity.Mathematics.uint4x4 a, global::Unity.Mathematics.uint4x4 b)
		{
			return uint4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion quaternion(float x, float y, float z, float w)
		{
			return new global::Unity.Mathematics.quaternion(x, y, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion quaternion(global::Unity.Mathematics.float4 value)
		{
			return new global::Unity.Mathematics.quaternion(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion quaternion(global::Unity.Mathematics.float3x3 m)
		{
			return new global::Unity.Mathematics.quaternion(m);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion quaternion(global::Unity.Mathematics.float4x4 m)
		{
			return new global::Unity.Mathematics.quaternion(m);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion conjugate(global::Unity.Mathematics.quaternion q)
		{
			return quaternion(q.value * float4(-1f, -1f, -1f, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion inverse(global::Unity.Mathematics.quaternion q)
		{
			global::Unity.Mathematics.float4 value = q.value;
			return quaternion(rcp(dot(value, value)) * value * float4(-1f, -1f, -1f, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float dot(global::Unity.Mathematics.quaternion a, global::Unity.Mathematics.quaternion b)
		{
			return dot(a.value, b.value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float length(global::Unity.Mathematics.quaternion q)
		{
			return sqrt(dot(q.value, q.value));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(global::Unity.Mathematics.quaternion q)
		{
			return dot(q.value, q.value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion normalize(global::Unity.Mathematics.quaternion q)
		{
			global::Unity.Mathematics.float4 value = q.value;
			return quaternion(rsqrt(dot(value, value)) * value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion normalizesafe(global::Unity.Mathematics.quaternion q)
		{
			global::Unity.Mathematics.float4 value = q.value;
			float num = dot(value, value);
			return quaternion(select(global::Unity.Mathematics.quaternion.identity.value, value * rsqrt(num), num > 1.1754944E-38f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion normalizesafe(global::Unity.Mathematics.quaternion q, global::Unity.Mathematics.quaternion defaultvalue)
		{
			global::Unity.Mathematics.float4 value = q.value;
			float num = dot(value, value);
			return quaternion(select(defaultvalue.value, value * rsqrt(num), num > 1.1754944E-38f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion unitexp(global::Unity.Mathematics.quaternion q)
		{
			float num = rsqrt(dot(q.value.xyz, q.value.xyz));
			sincos(rcp(num), out var s, out var c);
			return quaternion(float4(q.value.xyz * num * s, c));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion exp(global::Unity.Mathematics.quaternion q)
		{
			float num = rsqrt(dot(q.value.xyz, q.value.xyz));
			sincos(rcp(num), out var s, out var c);
			return quaternion(float4(q.value.xyz * num * s, c) * exp(q.value.w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion unitlog(global::Unity.Mathematics.quaternion q)
		{
			float num = clamp(q.value.w, -1f, 1f);
			float num2 = acos(num) * rsqrt(1f - num * num);
			return quaternion(float4(q.value.xyz * num2, 0f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion log(global::Unity.Mathematics.quaternion q)
		{
			float num = dot(q.value.xyz, q.value.xyz);
			float x = num + q.value.w * q.value.w;
			float num2 = acos(clamp(q.value.w * rsqrt(x), -1f, 1f)) * rsqrt(num);
			return quaternion(float4(q.value.xyz * num2, 0.5f * log(x)));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion mul(global::Unity.Mathematics.quaternion a, global::Unity.Mathematics.quaternion b)
		{
			return quaternion(a.value.wwww * b.value + (a.value.xyzx * b.value.wwwx + a.value.yzxy * b.value.zxyy) * float4(1f, 1f, 1f, -1f) - a.value.zxyz * b.value.yzxz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 mul(global::Unity.Mathematics.quaternion q, global::Unity.Mathematics.float3 v)
		{
			global::Unity.Mathematics.float3 float5 = 2f * cross(q.value.xyz, v);
			return v + q.value.w * float5 + cross(q.value.xyz, float5);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 rotate(global::Unity.Mathematics.quaternion q, global::Unity.Mathematics.float3 v)
		{
			global::Unity.Mathematics.float3 float5 = 2f * cross(q.value.xyz, v);
			return v + q.value.w * float5 + cross(q.value.xyz, float5);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion nlerp(global::Unity.Mathematics.quaternion q1, global::Unity.Mathematics.quaternion q2, float t)
		{
			return normalize(q1.value + t * (chgsign(q2.value, dot(q1, q2)) - q1.value));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion slerp(global::Unity.Mathematics.quaternion q1, global::Unity.Mathematics.quaternion q2, float t)
		{
			float num = dot(q1, q2);
			if (num < 0f)
			{
				num = 0f - num;
				q2.value = -q2.value;
			}
			if (num < 0.9995f)
			{
				float num2 = acos(num);
				float num3 = rsqrt(1f - num * num);
				float num4 = sin(num2 * (1f - t)) * num3;
				float num5 = sin(num2 * t) * num3;
				return quaternion(q1.value * num4 + q2.value * num5);
			}
			return nlerp(q1, q2, t);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static float angle(global::Unity.Mathematics.quaternion q1, global::Unity.Mathematics.quaternion q2)
		{
			float num = asin(length(normalize(mul(conjugate(q1), q2)).value.xyz));
			return num + num;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.quaternion rotation(global::Unity.Mathematics.float3x3 m)
		{
			float num = determinant(m);
			if (abs(1f - num) < 1E-06f)
			{
				return quaternion(m);
			}
			if (abs(num) > 1E-06f)
			{
				global::Unity.Mathematics.float3x3 m2 = mulScale(m, rsqrt(float3(lengthsq(m.c0), lengthsq(m.c1), lengthsq(m.c2))));
				if (abs(1f - determinant(m2)) < 1E-06f)
				{
					return quaternion(m2);
				}
			}
			return global::Unity.Mathematics.svd.svdRotation(m);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static global::Unity.Mathematics.float3x3 adj(global::Unity.Mathematics.float3x3 m, out float det)
		{
			global::Unity.Mathematics.float3x3 v = default(global::Unity.Mathematics.float3x3);
			v.c0 = cross(m.c1, m.c2);
			v.c1 = cross(m.c2, m.c0);
			v.c2 = cross(m.c0, m.c1);
			det = dot(m.c0, v.c0);
			return transpose(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static bool adjInverse(global::Unity.Mathematics.float3x3 m, out global::Unity.Mathematics.float3x3 i, float epsilon = 1E-30f)
		{
			i = adj(m, out var det);
			bool flag = abs(det) > epsilon;
			global::Unity.Mathematics.float3 s = select(float3(1f), rcp(det), flag);
			i = scaleMul(s, i);
			return flag;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.quaternion q)
		{
			return hash(q.value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.quaternion q)
		{
			return hashwide(q.value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 forward(global::Unity.Mathematics.quaternion q)
		{
			return mul(q, float3(0f, 0f, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform RigidTransform(global::Unity.Mathematics.quaternion rot, global::Unity.Mathematics.float3 pos)
		{
			return new global::Unity.Mathematics.RigidTransform(rot, pos);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform RigidTransform(global::Unity.Mathematics.float3x3 rotation, global::Unity.Mathematics.float3 translation)
		{
			return new global::Unity.Mathematics.RigidTransform(rotation, translation);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform RigidTransform(global::Unity.Mathematics.float4x4 transform)
		{
			return new global::Unity.Mathematics.RigidTransform(transform);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform inverse(global::Unity.Mathematics.RigidTransform t)
		{
			global::Unity.Mathematics.quaternion q = inverse(t.rot);
			global::Unity.Mathematics.float3 translation = mul(q, -t.pos);
			return new global::Unity.Mathematics.RigidTransform(q, translation);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform mul(global::Unity.Mathematics.RigidTransform a, global::Unity.Mathematics.RigidTransform b)
		{
			return new global::Unity.Mathematics.RigidTransform(mul(a.rot, b.rot), mul(a.rot, b.pos) + a.pos);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 mul(global::Unity.Mathematics.RigidTransform a, global::Unity.Mathematics.float4 pos)
		{
			return float4(mul(a.rot, pos.xyz) + a.pos * pos.w, pos.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 rotate(global::Unity.Mathematics.RigidTransform a, global::Unity.Mathematics.float3 dir)
		{
			return mul(a.rot, dir);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float3 transform(global::Unity.Mathematics.RigidTransform a, global::Unity.Mathematics.float3 pos)
		{
			return mul(a.rot, pos) + a.pos;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.RigidTransform t)
		{
			return hash(t.rot) + (uint)(-976930485 * (int)hash(t.pos));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.RigidTransform t)
		{
			return hashwide(t.rot) + 3318036811u * hashwide(t.pos).xyzz;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 uint2(uint x, uint y)
		{
			return new global::Unity.Mathematics.uint2(x, y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 uint2(global::Unity.Mathematics.uint2 xy)
		{
			return new global::Unity.Mathematics.uint2(xy);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 uint2(uint v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 uint2(bool v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 uint2(global::Unity.Mathematics.bool2 v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 uint2(int v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 uint2(global::Unity.Mathematics.int2 v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 uint2(float v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 uint2(global::Unity.Mathematics.float2 v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 uint2(double v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 uint2(global::Unity.Mathematics.double2 v)
		{
			return new global::Unity.Mathematics.uint2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint2 v)
		{
			return csum(v * uint2(1148435377u, 3416333663u)) + 1750611407;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.uint2 v)
		{
			return v * uint2(3285396193u, 3110507567u) + 4271396531u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint shuffle(global::Unity.Mathematics.uint2 left, global::Unity.Mathematics.uint2 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 shuffle(global::Unity.Mathematics.uint2 left, global::Unity.Mathematics.uint2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return uint2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 shuffle(global::Unity.Mathematics.uint2 left, global::Unity.Mathematics.uint2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return uint3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 shuffle(global::Unity.Mathematics.uint2 left, global::Unity.Mathematics.uint2 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return uint4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static uint select_shuffle_component(global::Unity.Mathematics.uint2 a, global::Unity.Mathematics.uint2 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 uint2x2(global::Unity.Mathematics.uint2 c0, global::Unity.Mathematics.uint2 c1)
		{
			return new global::Unity.Mathematics.uint2x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 uint2x2(uint m00, uint m01, uint m10, uint m11)
		{
			return new global::Unity.Mathematics.uint2x2(m00, m01, m10, m11);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 uint2x2(uint v)
		{
			return new global::Unity.Mathematics.uint2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 uint2x2(bool v)
		{
			return new global::Unity.Mathematics.uint2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 uint2x2(global::Unity.Mathematics.bool2x2 v)
		{
			return new global::Unity.Mathematics.uint2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 uint2x2(int v)
		{
			return new global::Unity.Mathematics.uint2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 uint2x2(global::Unity.Mathematics.int2x2 v)
		{
			return new global::Unity.Mathematics.uint2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 uint2x2(float v)
		{
			return new global::Unity.Mathematics.uint2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 uint2x2(global::Unity.Mathematics.float2x2 v)
		{
			return new global::Unity.Mathematics.uint2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 uint2x2(double v)
		{
			return new global::Unity.Mathematics.uint2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 uint2x2(global::Unity.Mathematics.double2x2 v)
		{
			return new global::Unity.Mathematics.uint2x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x2 transpose(global::Unity.Mathematics.uint2x2 v)
		{
			return uint2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint2x2 v)
		{
			return csum(v.c0 * uint2(3010324327u, 1875523709u) + v.c1 * uint2(2937008387u, 3835713223u)) + 2216526373u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.uint2x2 v)
		{
			return v.c0 * uint2(3375971453u, 3559829411u) + v.c1 * uint2(3652178029u, 2544260129u) + 2013864031u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 uint2x3(global::Unity.Mathematics.uint2 c0, global::Unity.Mathematics.uint2 c1, global::Unity.Mathematics.uint2 c2)
		{
			return new global::Unity.Mathematics.uint2x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 uint2x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12)
		{
			return new global::Unity.Mathematics.uint2x3(m00, m01, m02, m10, m11, m12);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 uint2x3(uint v)
		{
			return new global::Unity.Mathematics.uint2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 uint2x3(bool v)
		{
			return new global::Unity.Mathematics.uint2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 uint2x3(global::Unity.Mathematics.bool2x3 v)
		{
			return new global::Unity.Mathematics.uint2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 uint2x3(int v)
		{
			return new global::Unity.Mathematics.uint2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 uint2x3(global::Unity.Mathematics.int2x3 v)
		{
			return new global::Unity.Mathematics.uint2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 uint2x3(float v)
		{
			return new global::Unity.Mathematics.uint2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 uint2x3(global::Unity.Mathematics.float2x3 v)
		{
			return new global::Unity.Mathematics.uint2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 uint2x3(double v)
		{
			return new global::Unity.Mathematics.uint2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 uint2x3(global::Unity.Mathematics.double2x3 v)
		{
			return new global::Unity.Mathematics.uint2x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 transpose(global::Unity.Mathematics.uint2x3 v)
		{
			return uint3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint2x3 v)
		{
			return csum(v.c0 * uint2(4016293529u, 2416021567u) + v.c1 * uint2(2828384717u, 2636362241u) + v.c2 * uint2(1258410977u, 1952565773u)) + 2037535609;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.uint2x3 v)
		{
			return v.c0 * uint2(3592785499u, 3996716183u) + v.c1 * uint2(2626301701u, 1306289417u) + v.c2 * uint2(2096137163u, 1548578029u) + 4178800919u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 uint2x4(global::Unity.Mathematics.uint2 c0, global::Unity.Mathematics.uint2 c1, global::Unity.Mathematics.uint2 c2, global::Unity.Mathematics.uint2 c3)
		{
			return new global::Unity.Mathematics.uint2x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 uint2x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13)
		{
			return new global::Unity.Mathematics.uint2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 uint2x4(uint v)
		{
			return new global::Unity.Mathematics.uint2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 uint2x4(bool v)
		{
			return new global::Unity.Mathematics.uint2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 uint2x4(global::Unity.Mathematics.bool2x4 v)
		{
			return new global::Unity.Mathematics.uint2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 uint2x4(int v)
		{
			return new global::Unity.Mathematics.uint2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 uint2x4(global::Unity.Mathematics.int2x4 v)
		{
			return new global::Unity.Mathematics.uint2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 uint2x4(float v)
		{
			return new global::Unity.Mathematics.uint2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 uint2x4(global::Unity.Mathematics.float2x4 v)
		{
			return new global::Unity.Mathematics.uint2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 uint2x4(double v)
		{
			return new global::Unity.Mathematics.uint2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 uint2x4(global::Unity.Mathematics.double2x4 v)
		{
			return new global::Unity.Mathematics.uint2x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 transpose(global::Unity.Mathematics.uint2x4 v)
		{
			return uint4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint2x4 v)
		{
			return csum(v.c0 * uint2(2650080659u, 4052675461u) + v.c1 * uint2(2652487619u, 2174136431u) + v.c2 * uint2(3528391193u, 2105559227u) + v.c3 * uint2(1899745391u, 1966790317u)) + 3516359879u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 hashwide(global::Unity.Mathematics.uint2x4 v)
		{
			return v.c0 * uint2(3050356579u, 4178586719u) + v.c1 * uint2(2558655391u, 1453413133u) + v.c2 * uint2(2152428077u, 1938706661u) + v.c3 * uint2(1338588197u, 3439609253u) + 3535343003u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(uint x, uint y, uint z)
		{
			return new global::Unity.Mathematics.uint3(x, y, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(uint x, global::Unity.Mathematics.uint2 yz)
		{
			return new global::Unity.Mathematics.uint3(x, yz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(global::Unity.Mathematics.uint2 xy, uint z)
		{
			return new global::Unity.Mathematics.uint3(xy, z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(global::Unity.Mathematics.uint3 xyz)
		{
			return new global::Unity.Mathematics.uint3(xyz);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(uint v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(bool v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(global::Unity.Mathematics.bool3 v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(int v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(global::Unity.Mathematics.int3 v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(float v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(global::Unity.Mathematics.float3 v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(double v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 uint3(global::Unity.Mathematics.double3 v)
		{
			return new global::Unity.Mathematics.uint3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint3 v)
		{
			return csum(v * uint3(3441847433u, 4052036147u, 2011389559u)) + 2252224297u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.uint3 v)
		{
			return v * uint3(3784421429u, 1750626223u, 3571447507u) + 3412283213u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint shuffle(global::Unity.Mathematics.uint3 left, global::Unity.Mathematics.uint3 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 shuffle(global::Unity.Mathematics.uint3 left, global::Unity.Mathematics.uint3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return uint2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 shuffle(global::Unity.Mathematics.uint3 left, global::Unity.Mathematics.uint3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return uint3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 shuffle(global::Unity.Mathematics.uint3 left, global::Unity.Mathematics.uint3 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return uint4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static uint select_shuffle_component(global::Unity.Mathematics.uint3 a, global::Unity.Mathematics.uint3 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftZ => a.z, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightZ => b.z, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 uint3x2(global::Unity.Mathematics.uint3 c0, global::Unity.Mathematics.uint3 c1)
		{
			return new global::Unity.Mathematics.uint3x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 uint3x2(uint m00, uint m01, uint m10, uint m11, uint m20, uint m21)
		{
			return new global::Unity.Mathematics.uint3x2(m00, m01, m10, m11, m20, m21);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 uint3x2(uint v)
		{
			return new global::Unity.Mathematics.uint3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 uint3x2(bool v)
		{
			return new global::Unity.Mathematics.uint3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 uint3x2(global::Unity.Mathematics.bool3x2 v)
		{
			return new global::Unity.Mathematics.uint3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 uint3x2(int v)
		{
			return new global::Unity.Mathematics.uint3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 uint3x2(global::Unity.Mathematics.int3x2 v)
		{
			return new global::Unity.Mathematics.uint3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 uint3x2(float v)
		{
			return new global::Unity.Mathematics.uint3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 uint3x2(global::Unity.Mathematics.float3x2 v)
		{
			return new global::Unity.Mathematics.uint3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 uint3x2(double v)
		{
			return new global::Unity.Mathematics.uint3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x2 uint3x2(global::Unity.Mathematics.double3x2 v)
		{
			return new global::Unity.Mathematics.uint3x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x3 transpose(global::Unity.Mathematics.uint3x2 v)
		{
			return uint2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint3x2 v)
		{
			return csum(v.c0 * uint3(1365086453u, 3969870067u, 4192899797u) + v.c1 * uint3(3271228601u, 1634639009u, 3318036811u)) + 3404170631u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.uint3x2 v)
		{
			return v.c0 * uint3(2048213449u, 4164671783u, 1780759499u) + v.c1 * uint3(1352369353u, 2446407751u, 1391928079u) + 3475533443u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 uint3x3(global::Unity.Mathematics.uint3 c0, global::Unity.Mathematics.uint3 c1, global::Unity.Mathematics.uint3 c2)
		{
			return new global::Unity.Mathematics.uint3x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 uint3x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12, uint m20, uint m21, uint m22)
		{
			return new global::Unity.Mathematics.uint3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 uint3x3(uint v)
		{
			return new global::Unity.Mathematics.uint3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 uint3x3(bool v)
		{
			return new global::Unity.Mathematics.uint3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 uint3x3(global::Unity.Mathematics.bool3x3 v)
		{
			return new global::Unity.Mathematics.uint3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 uint3x3(int v)
		{
			return new global::Unity.Mathematics.uint3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 uint3x3(global::Unity.Mathematics.int3x3 v)
		{
			return new global::Unity.Mathematics.uint3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 uint3x3(float v)
		{
			return new global::Unity.Mathematics.uint3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 uint3x3(global::Unity.Mathematics.float3x3 v)
		{
			return new global::Unity.Mathematics.uint3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 uint3x3(double v)
		{
			return new global::Unity.Mathematics.uint3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 uint3x3(global::Unity.Mathematics.double3x3 v)
		{
			return new global::Unity.Mathematics.uint3x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x3 transpose(global::Unity.Mathematics.uint3x3 v)
		{
			return uint3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint3x3 v)
		{
			return csum(v.c0 * uint3(2892026051u, 2455987759u, 3868600063u) + v.c1 * uint3(3170963179u, 2632835537u, 1136528209u) + v.c2 * uint3(2944626401u, 2972762423u, 1417889653u)) + 2080514593;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.uint3x3 v)
		{
			return v.c0 * uint3(2731544287u, 2828498809u, 2669441947u) + v.c1 * uint3(1260114311u, 2650080659u, 4052675461u) + v.c2 * uint3(2652487619u, 2174136431u, 3528391193u) + 2105559227u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 uint3x4(global::Unity.Mathematics.uint3 c0, global::Unity.Mathematics.uint3 c1, global::Unity.Mathematics.uint3 c2, global::Unity.Mathematics.uint3 c3)
		{
			return new global::Unity.Mathematics.uint3x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 uint3x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13, uint m20, uint m21, uint m22, uint m23)
		{
			return new global::Unity.Mathematics.uint3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 uint3x4(uint v)
		{
			return new global::Unity.Mathematics.uint3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 uint3x4(bool v)
		{
			return new global::Unity.Mathematics.uint3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 uint3x4(global::Unity.Mathematics.bool3x4 v)
		{
			return new global::Unity.Mathematics.uint3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 uint3x4(int v)
		{
			return new global::Unity.Mathematics.uint3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 uint3x4(global::Unity.Mathematics.int3x4 v)
		{
			return new global::Unity.Mathematics.uint3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 uint3x4(float v)
		{
			return new global::Unity.Mathematics.uint3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 uint3x4(global::Unity.Mathematics.float3x4 v)
		{
			return new global::Unity.Mathematics.uint3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 uint3x4(double v)
		{
			return new global::Unity.Mathematics.uint3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 uint3x4(global::Unity.Mathematics.double3x4 v)
		{
			return new global::Unity.Mathematics.uint3x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 transpose(global::Unity.Mathematics.uint3x4 v)
		{
			return uint4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint3x4 v)
		{
			return csum(v.c0 * uint3(3508684087u, 3919501043u, 1209161033u) + v.c1 * uint3(4007793211u, 3819806693u, 3458005183u) + v.c2 * uint3(2078515003u, 4206465343u, 3025146473u) + v.c3 * uint3(3763046909u, 3678265601u, 2070747979u)) + 1480171127;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 hashwide(global::Unity.Mathematics.uint3x4 v)
		{
			return v.c0 * uint3(1588341193u, 4234155257u, 1811310911u) + v.c1 * uint3(2635799963u, 4165137857u, 2759770933u) + v.c2 * uint3(2759319383u, 3299952959u, 3121178323u) + v.c3 * uint3(2948522579u, 1531026433u, 1365086453u) + 3969870067u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(uint x, uint y, uint z, uint w)
		{
			return new global::Unity.Mathematics.uint4(x, y, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(uint x, uint y, global::Unity.Mathematics.uint2 zw)
		{
			return new global::Unity.Mathematics.uint4(x, y, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(uint x, global::Unity.Mathematics.uint2 yz, uint w)
		{
			return new global::Unity.Mathematics.uint4(x, yz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(uint x, global::Unity.Mathematics.uint3 yzw)
		{
			return new global::Unity.Mathematics.uint4(x, yzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(global::Unity.Mathematics.uint2 xy, uint z, uint w)
		{
			return new global::Unity.Mathematics.uint4(xy, z, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(global::Unity.Mathematics.uint2 xy, global::Unity.Mathematics.uint2 zw)
		{
			return new global::Unity.Mathematics.uint4(xy, zw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(global::Unity.Mathematics.uint3 xyz, uint w)
		{
			return new global::Unity.Mathematics.uint4(xyz, w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(global::Unity.Mathematics.uint4 xyzw)
		{
			return new global::Unity.Mathematics.uint4(xyzw);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(uint v)
		{
			return new global::Unity.Mathematics.uint4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(bool v)
		{
			return new global::Unity.Mathematics.uint4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(global::Unity.Mathematics.bool4 v)
		{
			return new global::Unity.Mathematics.uint4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(int v)
		{
			return new global::Unity.Mathematics.uint4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(global::Unity.Mathematics.int4 v)
		{
			return new global::Unity.Mathematics.uint4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(float v)
		{
			return new global::Unity.Mathematics.uint4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(global::Unity.Mathematics.float4 v)
		{
			return new global::Unity.Mathematics.uint4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(double v)
		{
			return new global::Unity.Mathematics.uint4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 uint4(global::Unity.Mathematics.double4 v)
		{
			return new global::Unity.Mathematics.uint4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint4 v)
		{
			return csum(v * uint4(3029516053u, 3547472099u, 2057487037u, 3781937309u)) + 2057338067;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.uint4 v)
		{
			return v * uint4(2942577577u, 2834440507u, 2671762487u, 2892026051u) + 2455987759u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint shuffle(global::Unity.Mathematics.uint4 left, global::Unity.Mathematics.uint4 right, global::Unity.Mathematics.math.ShuffleComponent x)
		{
			return select_shuffle_component(left, right, x);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2 shuffle(global::Unity.Mathematics.uint4 left, global::Unity.Mathematics.uint4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y)
		{
			return uint2(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3 shuffle(global::Unity.Mathematics.uint4 left, global::Unity.Mathematics.uint4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z)
		{
			return uint3(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 shuffle(global::Unity.Mathematics.uint4 left, global::Unity.Mathematics.uint4 right, global::Unity.Mathematics.math.ShuffleComponent x, global::Unity.Mathematics.math.ShuffleComponent y, global::Unity.Mathematics.math.ShuffleComponent z, global::Unity.Mathematics.math.ShuffleComponent w)
		{
			return uint4(select_shuffle_component(left, right, x), select_shuffle_component(left, right, y), select_shuffle_component(left, right, z), select_shuffle_component(left, right, w));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static uint select_shuffle_component(global::Unity.Mathematics.uint4 a, global::Unity.Mathematics.uint4 b, global::Unity.Mathematics.math.ShuffleComponent component)
		{
			return component switch
			{
				global::Unity.Mathematics.math.ShuffleComponent.LeftX => a.x, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftY => a.y, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftZ => a.z, 
				global::Unity.Mathematics.math.ShuffleComponent.LeftW => a.w, 
				global::Unity.Mathematics.math.ShuffleComponent.RightX => b.x, 
				global::Unity.Mathematics.math.ShuffleComponent.RightY => b.y, 
				global::Unity.Mathematics.math.ShuffleComponent.RightZ => b.z, 
				global::Unity.Mathematics.math.ShuffleComponent.RightW => b.w, 
				_ => throw new global::System.ArgumentException("Invalid shuffle component: " + component), 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 uint4x2(global::Unity.Mathematics.uint4 c0, global::Unity.Mathematics.uint4 c1)
		{
			return new global::Unity.Mathematics.uint4x2(c0, c1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 uint4x2(uint m00, uint m01, uint m10, uint m11, uint m20, uint m21, uint m30, uint m31)
		{
			return new global::Unity.Mathematics.uint4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 uint4x2(uint v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 uint4x2(bool v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 uint4x2(global::Unity.Mathematics.bool4x2 v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 uint4x2(int v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 uint4x2(global::Unity.Mathematics.int4x2 v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 uint4x2(float v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 uint4x2(global::Unity.Mathematics.float4x2 v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 uint4x2(double v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x2 uint4x2(global::Unity.Mathematics.double4x2 v)
		{
			return new global::Unity.Mathematics.uint4x2(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint2x4 transpose(global::Unity.Mathematics.uint4x2 v)
		{
			return uint2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint4x2 v)
		{
			return csum(v.c0 * uint4(4198118021u, 2908068253u, 3705492289u, 2497566569u) + v.c1 * uint4(2716413241u, 1166264321u, 2503385333u, 2944493077u)) + 2599999021u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.uint4x2 v)
		{
			return v.c0 * uint4(3814721321u, 1595355149u, 1728931849u, 2062756937u) + v.c1 * uint4(2920485769u, 1562056283u, 2265541847u, 1283419601u) + 1210229737u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 uint4x3(global::Unity.Mathematics.uint4 c0, global::Unity.Mathematics.uint4 c1, global::Unity.Mathematics.uint4 c2)
		{
			return new global::Unity.Mathematics.uint4x3(c0, c1, c2);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 uint4x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12, uint m20, uint m21, uint m22, uint m30, uint m31, uint m32)
		{
			return new global::Unity.Mathematics.uint4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 uint4x3(uint v)
		{
			return new global::Unity.Mathematics.uint4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 uint4x3(bool v)
		{
			return new global::Unity.Mathematics.uint4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 uint4x3(global::Unity.Mathematics.bool4x3 v)
		{
			return new global::Unity.Mathematics.uint4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 uint4x3(int v)
		{
			return new global::Unity.Mathematics.uint4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 uint4x3(global::Unity.Mathematics.int4x3 v)
		{
			return new global::Unity.Mathematics.uint4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 uint4x3(float v)
		{
			return new global::Unity.Mathematics.uint4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 uint4x3(global::Unity.Mathematics.float4x3 v)
		{
			return new global::Unity.Mathematics.uint4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 uint4x3(double v)
		{
			return new global::Unity.Mathematics.uint4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x3 uint4x3(global::Unity.Mathematics.double4x3 v)
		{
			return new global::Unity.Mathematics.uint4x3(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint3x4 transpose(global::Unity.Mathematics.uint4x3 v)
		{
			return uint3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint4x3 v)
		{
			return csum(v.c0 * uint4(3881277847u, 4017968839u, 1727237899u, 1648514723u) + v.c1 * uint4(1385344481u, 3538260197u, 4066109527u, 2613148903u) + v.c2 * uint4(3367528529u, 1678332449u, 2918459647u, 2744611081u)) + 1952372791;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.uint4x3 v)
		{
			return v.c0 * uint4(2631698677u, 4200781601u, 2119021007u, 1760485621u) + v.c1 * uint4(3157985881u, 2171534173u, 2723054263u, 1168253063u) + v.c2 * uint4(4228926523u, 1610574617u, 1584185147u, 3041325733u) + 3150930919u;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 uint4x4(global::Unity.Mathematics.uint4 c0, global::Unity.Mathematics.uint4 c1, global::Unity.Mathematics.uint4 c2, global::Unity.Mathematics.uint4 c3)
		{
			return new global::Unity.Mathematics.uint4x4(c0, c1, c2, c3);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 uint4x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13, uint m20, uint m21, uint m22, uint m23, uint m30, uint m31, uint m32, uint m33)
		{
			return new global::Unity.Mathematics.uint4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 uint4x4(uint v)
		{
			return new global::Unity.Mathematics.uint4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 uint4x4(bool v)
		{
			return new global::Unity.Mathematics.uint4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 uint4x4(global::Unity.Mathematics.bool4x4 v)
		{
			return new global::Unity.Mathematics.uint4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 uint4x4(int v)
		{
			return new global::Unity.Mathematics.uint4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 uint4x4(global::Unity.Mathematics.int4x4 v)
		{
			return new global::Unity.Mathematics.uint4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 uint4x4(float v)
		{
			return new global::Unity.Mathematics.uint4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 uint4x4(global::Unity.Mathematics.float4x4 v)
		{
			return new global::Unity.Mathematics.uint4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 uint4x4(double v)
		{
			return new global::Unity.Mathematics.uint4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 uint4x4(global::Unity.Mathematics.double4x4 v)
		{
			return new global::Unity.Mathematics.uint4x4(v);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4x4 transpose(global::Unity.Mathematics.uint4x4 v)
		{
			return uint4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static uint hash(global::Unity.Mathematics.uint4x4 v)
		{
			return csum(v.c0 * uint4(2627668003u, 1520214331u, 2949502447u, 2827819133u) + v.c1 * uint4(3480140317u, 2642994593u, 3940484981u, 1954192763u) + v.c2 * uint4(1091696537u, 3052428017u, 4253034763u, 2338696631u) + v.c3 * uint4(3757372771u, 1885959949u, 3508684087u, 3919501043u)) + 1209161033;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.uint4 hashwide(global::Unity.Mathematics.uint4x4 v)
		{
			return v.c0 * uint4(4007793211u, 3819806693u, 3458005183u, 2078515003u) + v.c1 * uint4(4206465343u, 3025146473u, 3763046909u, 3678265601u) + v.c2 * uint4(2070747979u, 1480171127u, 1588341193u, 4234155257u) + v.c3 * uint4(1811310911u, 2635799963u, 4165137857u, 2759770933u) + 2759319383u;
		}
	}
}
