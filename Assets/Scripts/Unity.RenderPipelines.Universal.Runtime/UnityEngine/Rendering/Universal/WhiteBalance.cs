namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/White Balance")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class WhiteBalance : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("Sets the white balance to a custom color temperature.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter temperature = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -100f, 100f);

		[global::UnityEngine.Tooltip("Sets the white balance to compensate for a green or magenta tint.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter tint = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -100f, 100f);

		public bool IsActive()
		{
			if (temperature.value == 0f)
			{
				return tint.value != 0f;
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
