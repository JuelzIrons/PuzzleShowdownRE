namespace UnityEngine.Rendering.Universal
{
	public static class ShaderInput
	{
		[global::System.Obsolete("ShaderInput.ShadowData was deprecated. Shadow slice matrices and per-light shadow parameters are now passed to the GPU using entries in buffers m_AdditionalLightsWorldToShadow_SSBO and m_AdditionalShadowParams_SSBO. #from(2021.1) #breakingFrom(2023.1)", true)]
		public struct ShadowData
		{
			public global::UnityEngine.Matrix4x4 worldToShadowMatrix;

			public global::UnityEngine.Vector4 shadowParams;
		}

		[global::UnityEngine.Rendering.GenerateHLSL(global::UnityEngine.Rendering.PackingRules.Exact, false, false, false, 1, false, false, false, -1, ".\\Library\\PackageCache\\com.unity.render-pipelines.universal@37e0d4fc2503\\ShaderLibrary\\ShaderTypes.cs")]
		public struct LightData
		{
			public global::UnityEngine.Vector4 position;

			public global::UnityEngine.Vector4 color;

			public global::UnityEngine.Vector4 attenuation;

			public global::UnityEngine.Vector4 spotDirection;

			public global::UnityEngine.Vector4 occlusionProbeChannels;

			public uint layerMask;
		}
	}
}
