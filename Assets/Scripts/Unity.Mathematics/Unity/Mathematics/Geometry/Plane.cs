namespace Unity.Mathematics.Geometry
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{Normal}, {Distance}")]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct Plane
	{
		public global::Unity.Mathematics.float4 NormalAndDistance;

		public global::Unity.Mathematics.float3 Normal
		{
			get
			{
				return NormalAndDistance.xyz;
			}
			set
			{
				NormalAndDistance.xyz = value;
			}
		}

		public float Distance
		{
			get
			{
				return NormalAndDistance.w;
			}
			set
			{
				NormalAndDistance.w = value;
			}
		}

		public global::Unity.Mathematics.Geometry.Plane Flipped => new global::Unity.Mathematics.Geometry.Plane
		{
			NormalAndDistance = -NormalAndDistance
		};

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public Plane(float coefficientA, float coefficientB, float coefficientC, float coefficientD)
		{
			NormalAndDistance = Normalize(new global::Unity.Mathematics.float4(coefficientA, coefficientB, coefficientC, coefficientD));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public Plane(global::Unity.Mathematics.float3 normal, float distance)
		{
			NormalAndDistance = Normalize(new global::Unity.Mathematics.float4(normal, distance));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public Plane(global::Unity.Mathematics.float3 normal, global::Unity.Mathematics.float3 pointInPlane)
			: this(normal, 0f - global::Unity.Mathematics.math.dot(normal, pointInPlane))
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public Plane(global::Unity.Mathematics.float3 vector1InPlane, global::Unity.Mathematics.float3 vector2InPlane, global::Unity.Mathematics.float3 pointInPlane)
			: this(global::Unity.Mathematics.math.cross(vector1InPlane, vector2InPlane), pointInPlane)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.Geometry.Plane CreateFromUnitNormalAndDistance(global::Unity.Mathematics.float3 unitNormal, float distance)
		{
			return new global::Unity.Mathematics.Geometry.Plane
			{
				NormalAndDistance = new global::Unity.Mathematics.float4(unitNormal, distance)
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.Geometry.Plane CreateFromUnitNormalAndPointInPlane(global::Unity.Mathematics.float3 unitNormal, global::Unity.Mathematics.float3 pointInPlane)
		{
			return new global::Unity.Mathematics.Geometry.Plane
			{
				NormalAndDistance = new global::Unity.Mathematics.float4(unitNormal, 0f - global::Unity.Mathematics.math.dot(unitNormal, pointInPlane))
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.Geometry.Plane Normalize(global::Unity.Mathematics.Geometry.Plane plane)
		{
			return new global::Unity.Mathematics.Geometry.Plane
			{
				NormalAndDistance = Normalize(plane.NormalAndDistance)
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.float4 Normalize(global::Unity.Mathematics.float4 planeCoefficients)
		{
			float num = global::Unity.Mathematics.math.rsqrt(global::Unity.Mathematics.math.lengthsq(planeCoefficients.xyz));
			return new global::Unity.Mathematics.Geometry.Plane
			{
				NormalAndDistance = planeCoefficients * num
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public float SignedDistanceToPoint(global::Unity.Mathematics.float3 point)
		{
			return global::Unity.Mathematics.math.dot(NormalAndDistance, new global::Unity.Mathematics.float4(point, 1f));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::Unity.Mathematics.float3 Projection(global::Unity.Mathematics.float3 point)
		{
			return point - Normal * SignedDistanceToPoint(point);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::Unity.Mathematics.float4(global::Unity.Mathematics.Geometry.Plane plane)
		{
			return plane.NormalAndDistance;
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckPlaneIsNormalized()
		{
			float num = global::Unity.Mathematics.math.lengthsq(Normal.xyz);
			if (num < 0.99800104f || num > 1.002001f)
			{
				throw new global::System.ArgumentException("Plane must be normalized. Call Plane.Normalize() to normalize plane.");
			}
		}
	}
}
