namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Split Toning")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class SplitToning : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("The color to use for shadows.")]
		public global::UnityEngine.Rendering.ColorParameter shadows = new global::UnityEngine.Rendering.ColorParameter(global::UnityEngine.Color.grey, hdr: false, showAlpha: false, showEyeDropper: true);

		[global::UnityEngine.Tooltip("The color to use for highlights.")]
		public global::UnityEngine.Rendering.ColorParameter highlights = new global::UnityEngine.Rendering.ColorParameter(global::UnityEngine.Color.grey, hdr: false, showAlpha: false, showEyeDropper: true);

		[global::UnityEngine.Tooltip("Balance between the colors in the highlights and shadows.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter balance = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -100f, 100f);

		public bool IsActive()
		{
			if (!(shadows != global::UnityEngine.Color.grey))
			{
				return highlights != global::UnityEngine.Color.grey;
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
