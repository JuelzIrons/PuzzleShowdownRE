namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Channel Mixer")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class ChannelMixer : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("Modify influence of the red channel in the overall mix.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter redOutRedIn = new global::UnityEngine.Rendering.ClampedFloatParameter(100f, -200f, 200f);

		[global::UnityEngine.Tooltip("Modify influence of the green channel in the overall mix.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter redOutGreenIn = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -200f, 200f);

		[global::UnityEngine.Tooltip("Modify influence of the blue channel in the overall mix.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter redOutBlueIn = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -200f, 200f);

		[global::UnityEngine.Tooltip("Modify influence of the red channel in the overall mix.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter greenOutRedIn = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -200f, 200f);

		[global::UnityEngine.Tooltip("Modify influence of the green channel in the overall mix.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter greenOutGreenIn = new global::UnityEngine.Rendering.ClampedFloatParameter(100f, -200f, 200f);

		[global::UnityEngine.Tooltip("Modify influence of the blue channel in the overall mix.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter greenOutBlueIn = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -200f, 200f);

		[global::UnityEngine.Tooltip("Modify influence of the red channel in the overall mix.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter blueOutRedIn = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -200f, 200f);

		[global::UnityEngine.Tooltip("Modify influence of the green channel in the overall mix.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter blueOutGreenIn = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -200f, 200f);

		[global::UnityEngine.Tooltip("Modify influence of the blue channel in the overall mix.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter blueOutBlueIn = new global::UnityEngine.Rendering.ClampedFloatParameter(100f, -200f, 200f);

		public bool IsActive()
		{
			if (redOutRedIn.value == 100f && redOutGreenIn.value == 0f && redOutBlueIn.value == 0f && greenOutRedIn.value == 0f && greenOutGreenIn.value == 100f && greenOutBlueIn.value == 0f && blueOutRedIn.value == 0f && blueOutGreenIn.value == 0f)
			{
				return blueOutBlueIn.value != 100f;
			}
			return true;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return true;
		}
	}
}
