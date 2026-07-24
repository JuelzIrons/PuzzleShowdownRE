namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Panini Projection")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class PaniniProjection : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("Panini projection distance.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter distance = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, 0f, 1f);

		[global::UnityEngine.Tooltip("Panini projection crop to fit.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter cropToFit = new global::UnityEngine.Rendering.ClampedFloatParameter(1f, 0f, 1f);

		public bool IsActive()
		{
			return distance.value > 0f;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return false;
		}
	}
}
