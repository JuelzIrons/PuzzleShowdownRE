namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Vignette")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class Vignette : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("Vignette color.")]
		public global::UnityEngine.Rendering.ColorParameter color = new global::UnityEngine.Rendering.ColorParameter(global::UnityEngine.Color.black, hdr: false, showAlpha: false, showEyeDropper: true);

		[global::UnityEngine.Tooltip("Sets the vignette center point (screen center is [0.5,0.5]).")]
		public global::UnityEngine.Rendering.Vector2Parameter center = new global::UnityEngine.Rendering.Vector2Parameter(new global::UnityEngine.Vector2(0.5f, 0.5f));

		[global::UnityEngine.Tooltip("Use the slider to set the strength of the Vignette effect.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter intensity = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, 0f, 1f);

		[global::UnityEngine.Tooltip("Smoothness of the vignette borders.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter smoothness = new global::UnityEngine.Rendering.ClampedFloatParameter(0.2f, 0.01f, 1f);

		[global::UnityEngine.Tooltip("Should the vignette be perfectly round or be dependent on the current aspect ratio?")]
		public global::UnityEngine.Rendering.BoolParameter rounded = new global::UnityEngine.Rendering.BoolParameter(value: false);

		public bool IsActive()
		{
			return intensity.value > 0f;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return true;
		}
	}
}
