namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public struct SHCoefficients : global::System.IEquatable<global::UnityEngine.Rendering.SHCoefficients>
	{
		public global::UnityEngine.Vector4 SHAr;

		public global::UnityEngine.Vector4 SHAg;

		public global::UnityEngine.Vector4 SHAb;

		public global::UnityEngine.Vector4 SHBr;

		public global::UnityEngine.Vector4 SHBg;

		public global::UnityEngine.Vector4 SHBb;

		public global::UnityEngine.Vector4 SHC;

		public global::UnityEngine.Vector4 ProbesOcclusion;

		public SHCoefficients(global::UnityEngine.Rendering.SphericalHarmonicsL2 sh)
		{
			SHAr = GetSHA(sh, 0);
			SHAg = GetSHA(sh, 1);
			SHAb = GetSHA(sh, 2);
			SHBr = GetSHB(sh, 0);
			SHBg = GetSHB(sh, 1);
			SHBb = GetSHB(sh, 2);
			SHC = GetSHC(sh);
			ProbesOcclusion = global::UnityEngine.Vector4.one;
		}

		public SHCoefficients(global::UnityEngine.Rendering.SphericalHarmonicsL2 sh, global::UnityEngine.Vector4 probesOcclusion)
			: this(sh)
		{
			ProbesOcclusion = probesOcclusion;
		}

		private static global::UnityEngine.Vector4 GetSHA(global::UnityEngine.Rendering.SphericalHarmonicsL2 sh, int i)
		{
			return new global::UnityEngine.Vector4(sh[i, 3], sh[i, 1], sh[i, 2], sh[i, 0] - sh[i, 6]);
		}

		private static global::UnityEngine.Vector4 GetSHB(global::UnityEngine.Rendering.SphericalHarmonicsL2 sh, int i)
		{
			return new global::UnityEngine.Vector4(sh[i, 4], sh[i, 5], sh[i, 6] * 3f, sh[i, 7]);
		}

		private static global::UnityEngine.Vector4 GetSHC(global::UnityEngine.Rendering.SphericalHarmonicsL2 sh)
		{
			return new global::UnityEngine.Vector4(sh[0, 8], sh[1, 8], sh[2, 8], 1f);
		}

		public bool Equals(global::UnityEngine.Rendering.SHCoefficients other)
		{
			if (SHAr.Equals(other.SHAr) && SHAg.Equals(other.SHAg) && SHAb.Equals(other.SHAb) && SHBr.Equals(other.SHBr) && SHBg.Equals(other.SHBg) && SHBb.Equals(other.SHBb) && SHC.Equals(other.SHC))
			{
				return ProbesOcclusion.Equals(other.ProbesOcclusion);
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::UnityEngine.Rendering.SHCoefficients other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return global::System.HashCode.Combine(SHAr, SHAg, SHAb, SHBr, SHBg, SHBb, SHC, ProbesOcclusion);
		}

		public static bool operator ==(global::UnityEngine.Rendering.SHCoefficients left, global::UnityEngine.Rendering.SHCoefficients right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.Rendering.SHCoefficients left, global::UnityEngine.Rendering.SHCoefficients right)
		{
			return !left.Equals(right);
		}
	}
}
