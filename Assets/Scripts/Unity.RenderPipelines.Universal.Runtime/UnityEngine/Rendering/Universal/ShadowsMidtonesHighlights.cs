namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Shadows, Midtones, Highlights")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class ShadowsMidtonesHighlights : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		public global::UnityEngine.Rendering.Vector4Parameter shadows = new global::UnityEngine.Rendering.Vector4Parameter(new global::UnityEngine.Vector4(1f, 1f, 1f, 0f));

		public global::UnityEngine.Rendering.Vector4Parameter midtones = new global::UnityEngine.Rendering.Vector4Parameter(new global::UnityEngine.Vector4(1f, 1f, 1f, 0f));

		public global::UnityEngine.Rendering.Vector4Parameter highlights = new global::UnityEngine.Rendering.Vector4Parameter(new global::UnityEngine.Vector4(1f, 1f, 1f, 0f));

		[global::UnityEngine.Header("Shadow Limits")]
		[global::UnityEngine.Tooltip("Start point of the transition between shadows and midtones.")]
		public global::UnityEngine.Rendering.MinFloatParameter shadowsStart = new global::UnityEngine.Rendering.MinFloatParameter(0f, 0f);

		[global::UnityEngine.Tooltip("End point of the transition between shadows and midtones.")]
		public global::UnityEngine.Rendering.MinFloatParameter shadowsEnd = new global::UnityEngine.Rendering.MinFloatParameter(0.3f, 0f);

		[global::UnityEngine.Header("Highlight Limits")]
		[global::UnityEngine.Tooltip("Start point of the transition between midtones and highlights.")]
		public global::UnityEngine.Rendering.MinFloatParameter highlightsStart = new global::UnityEngine.Rendering.MinFloatParameter(0.55f, 0f);

		[global::UnityEngine.Tooltip("End point of the transition between midtones and highlights.")]
		public global::UnityEngine.Rendering.MinFloatParameter highlightsEnd = new global::UnityEngine.Rendering.MinFloatParameter(1f, 0f);

		public bool IsActive()
		{
			global::UnityEngine.Vector4 vector = new global::UnityEngine.Vector4(1f, 1f, 1f, 0f);
			if (!(shadows != vector) && !(midtones != vector))
			{
				return highlights != vector;
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
