namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Chromatic Aberration")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class ChromaticAberration : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("Use the slider to set the strength of the Chromatic Aberration effect.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter intensity = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, 0f, 1f);

		public bool IsActive()
		{
			return intensity.value > 0f;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return false;
		}
	}
}
