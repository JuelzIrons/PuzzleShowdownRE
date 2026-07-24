namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public struct SphericalHarmonicsL1
	{
		public global::UnityEngine.Vector4 shAr;

		public global::UnityEngine.Vector4 shAg;

		public global::UnityEngine.Vector4 shAb;

		public static readonly global::UnityEngine.Rendering.SphericalHarmonicsL1 zero = new global::UnityEngine.Rendering.SphericalHarmonicsL1
		{
			shAr = global::UnityEngine.Vector4.zero,
			shAg = global::UnityEngine.Vector4.zero,
			shAb = global::UnityEngine.Vector4.zero
		};

		public static global::UnityEngine.Rendering.SphericalHarmonicsL1 operator +(global::UnityEngine.Rendering.SphericalHarmonicsL1 lhs, global::UnityEngine.Rendering.SphericalHarmonicsL1 rhs)
		{
			return new global::UnityEngine.Rendering.SphericalHarmonicsL1
			{
				shAr = lhs.shAr + rhs.shAr,
				shAg = lhs.shAg + rhs.shAg,
				shAb = lhs.shAb + rhs.shAb
			};
		}

		public static global::UnityEngine.Rendering.SphericalHarmonicsL1 operator -(global::UnityEngine.Rendering.SphericalHarmonicsL1 lhs, global::UnityEngine.Rendering.SphericalHarmonicsL1 rhs)
		{
			return new global::UnityEngine.Rendering.SphericalHarmonicsL1
			{
				shAr = lhs.shAr - rhs.shAr,
				shAg = lhs.shAg - rhs.shAg,
				shAb = lhs.shAb - rhs.shAb
			};
		}

		public static global::UnityEngine.Rendering.SphericalHarmonicsL1 operator *(global::UnityEngine.Rendering.SphericalHarmonicsL1 lhs, float rhs)
		{
			return new global::UnityEngine.Rendering.SphericalHarmonicsL1
			{
				shAr = lhs.shAr * rhs,
				shAg = lhs.shAg * rhs,
				shAb = lhs.shAb * rhs
			};
		}

		public static global::UnityEngine.Rendering.SphericalHarmonicsL1 operator /(global::UnityEngine.Rendering.SphericalHarmonicsL1 lhs, float rhs)
		{
			return new global::UnityEngine.Rendering.SphericalHarmonicsL1
			{
				shAr = lhs.shAr / rhs,
				shAg = lhs.shAg / rhs,
				shAb = lhs.shAb / rhs
			};
		}

		public static bool operator ==(global::UnityEngine.Rendering.SphericalHarmonicsL1 lhs, global::UnityEngine.Rendering.SphericalHarmonicsL1 rhs)
		{
			if (lhs.shAr == rhs.shAr && lhs.shAg == rhs.shAg)
			{
				return lhs.shAb == rhs.shAb;
			}
			return false;
		}

		public static bool operator !=(global::UnityEngine.Rendering.SphericalHarmonicsL1 lhs, global::UnityEngine.Rendering.SphericalHarmonicsL1 rhs)
		{
			return !(lhs == rhs);
		}

		public override bool Equals(object other)
		{
			if (!(other is global::UnityEngine.Rendering.SphericalHarmonicsL1))
			{
				return false;
			}
			return this == (global::UnityEngine.Rendering.SphericalHarmonicsL1)other;
		}

		public override int GetHashCode()
		{
			return ((391 + shAr.GetHashCode()) * 23 + shAg.GetHashCode()) * 23 + shAb.GetHashCode();
		}
	}
}
