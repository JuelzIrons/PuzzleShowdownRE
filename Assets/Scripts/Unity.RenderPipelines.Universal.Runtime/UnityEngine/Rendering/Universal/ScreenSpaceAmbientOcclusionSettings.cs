namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	internal class ScreenSpaceAmbientOcclusionSettings
	{
		internal enum DepthSource
		{
			Depth = 0,
			DepthNormals = 1
		}

		internal enum NormalQuality
		{
			Low = 0,
			Medium = 1,
			High = 2
		}

		internal enum AOSampleOption
		{
			High = 0,
			Medium = 1,
			Low = 2
		}

		internal enum AOMethodOptions
		{
			BlueNoise = 0,
			InterleavedGradient = 1
		}

		internal enum BlurQualityOptions
		{
			High = 0,
			Medium = 1,
			Low = 2
		}

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOMethodOptions AOMethod;

		[global::UnityEngine.SerializeField]
		internal bool Downsample;

		[global::UnityEngine.SerializeField]
		internal bool AfterOpaque;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.DepthSource Source = global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.NormalQuality NormalSamples = global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.NormalQuality.Medium;

		[global::UnityEngine.SerializeField]
		internal float Intensity = 3f;

		[global::UnityEngine.SerializeField]
		internal float DirectLightingStrength = 0.25f;

		[global::UnityEngine.SerializeField]
		internal float Radius = 0.035f;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOSampleOption Samples = global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.AOSampleOption.Medium;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.Universal.ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions BlurQuality;

		[global::UnityEngine.SerializeField]
		internal float Falloff = 100f;

		[global::UnityEngine.SerializeField]
		internal int SampleCount = -1;
	}
}
