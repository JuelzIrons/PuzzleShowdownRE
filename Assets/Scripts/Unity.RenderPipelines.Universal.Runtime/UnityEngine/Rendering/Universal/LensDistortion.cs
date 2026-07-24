namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.VolumeComponentMenu("Post-processing/Lens Distortion")]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	public sealed class LensDistortion : global::UnityEngine.Rendering.VolumeComponent, global::UnityEngine.Rendering.IPostProcessComponent
	{
		[global::UnityEngine.Tooltip("Total distortion amount.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter intensity = new global::UnityEngine.Rendering.ClampedFloatParameter(0f, -1f, 1f);

		[global::UnityEngine.Tooltip("Intensity multiplier on X axis. Set it to 0 to disable distortion on this axis.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter xMultiplier = new global::UnityEngine.Rendering.ClampedFloatParameter(1f, 0f, 1f);

		[global::UnityEngine.Tooltip("Intensity multiplier on Y axis. Set it to 0 to disable distortion on this axis.")]
		public global::UnityEngine.Rendering.ClampedFloatParameter yMultiplier = new global::UnityEngine.Rendering.ClampedFloatParameter(1f, 0f, 1f);

		[global::UnityEngine.Tooltip("Distortion center point. 0.5,0.5 is center of the screen.")]
		public global::UnityEngine.Rendering.Vector2Parameter center = new global::UnityEngine.Rendering.Vector2Parameter(new global::UnityEngine.Vector2(0.5f, 0.5f));

		[global::UnityEngine.Tooltip("Controls global screen scaling for the distortion effect. Use this to hide the screen borders when using high \"Intensity.\"")]
		public global::UnityEngine.Rendering.ClampedFloatParameter scale = new global::UnityEngine.Rendering.ClampedFloatParameter(1f, 0.01f, 5f);

		public bool IsActive()
		{
			if (global::UnityEngine.Mathf.Abs(intensity.value) > 0f)
			{
				if (!(xMultiplier.value > 0f))
				{
					return yMultiplier.value > 0f;
				}
				return true;
			}
			return false;
		}

		[global::System.Obsolete("Unused. #from(2023.1)")]
		public bool IsTileCompatible()
		{
			return false;
		}
	}
}
