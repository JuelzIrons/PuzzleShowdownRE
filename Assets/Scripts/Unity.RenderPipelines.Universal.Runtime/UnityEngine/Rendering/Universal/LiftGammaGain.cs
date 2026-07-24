namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Lift, Gamma, Gain")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class LiftGammaGain : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		public global::UnityEngine.Rendering.Vector4Parameter lift = new global::UnityEngine.Rendering.Vector4Parameter(new global::UnityEngine.Vector4(1f, 1f, 1f, 0f));

		public global::UnityEngine.Rendering.Vector4Parameter gamma = new global::UnityEngine.Rendering.Vector4Parameter(new global::UnityEngine.Vector4(1f, 1f, 1f, 0f));

		public global::UnityEngine.Rendering.Vector4Parameter gain = new global::UnityEngine.Rendering.Vector4Parameter(new global::UnityEngine.Vector4(1f, 1f, 1f, 0f));

		public bool IsActive()
		{
			global::UnityEngine.Vector4 vector = new global::UnityEngine.Vector4(1f, 1f, 1f, 0f);
			if (!(lift != vector) && !(gamma != vector))
			{
				return gain != vector;
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
