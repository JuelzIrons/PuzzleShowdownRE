namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Obsolete("BatchRendererGroupGlobals and associated cbuffer are now set automatically by Unity. Setting it manually is no longer necessary or supported. #from(2023.1)")]
	public struct BatchRendererGroupGlobals : global::System.IEquatable<global::UnityEngine.Rendering.BatchRendererGroupGlobals>
	{
		public const string kGlobalsPropertyName = "unity_DOTSInstanceGlobalValues";

		public static readonly int kGlobalsPropertyId = global::UnityEngine.Shader.PropertyToID("unity_DOTSInstanceGlobalValues");

		public global::UnityEngine.Vector4 ProbesOcclusion;

		public global::UnityEngine.Vector4 SpecCube0_HDR;

		public global::UnityEngine.Vector4 SpecCube1_HDR;

		public global::UnityEngine.Rendering.SHCoefficients SHCoefficients;

		public static global::UnityEngine.Rendering.BatchRendererGroupGlobals Default
		{
			get
			{
				global::UnityEngine.Rendering.BatchRendererGroupGlobals result = default(global::UnityEngine.Rendering.BatchRendererGroupGlobals);
				result.ProbesOcclusion = global::UnityEngine.Vector4.one;
				result.SpecCube0_HDR = global::UnityEngine.ReflectionProbe.defaultTextureHDRDecodeValues;
				result.SpecCube1_HDR = result.SpecCube0_HDR;
				result.SHCoefficients = new global::UnityEngine.Rendering.SHCoefficients(global::UnityEngine.RenderSettings.ambientProbe);
				return result;
			}
		}

		public bool Equals(global::UnityEngine.Rendering.BatchRendererGroupGlobals other)
		{
			if (ProbesOcclusion.Equals(other.ProbesOcclusion) && SpecCube0_HDR.Equals(other.SpecCube0_HDR) && SpecCube1_HDR.Equals(other.SpecCube1_HDR))
			{
				return SHCoefficients.Equals(other.SHCoefficients);
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::UnityEngine.Rendering.BatchRendererGroupGlobals other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return global::System.HashCode.Combine(ProbesOcclusion, SpecCube0_HDR, SpecCube1_HDR, SHCoefficients);
		}

		public static bool operator ==(global::UnityEngine.Rendering.BatchRendererGroupGlobals left, global::UnityEngine.Rendering.BatchRendererGroupGlobals right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.Rendering.BatchRendererGroupGlobals left, global::UnityEngine.Rendering.BatchRendererGroupGlobals right)
		{
			return !left.Equals(right);
		}
	}
}
