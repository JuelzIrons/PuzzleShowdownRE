namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "Lighting", Order = 21)]
	public class URPReflectionProbeSettings : global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int version = 1;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Use ReflectionProbe rotation. Enabling this will improve the appearance of reflections when the ReflectionProbe isn't axis aligned, but may worsen performance on lower end platforms.")]
		private bool useReflectionProbeRotation = true;

		int global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.version => version;

		public bool UseReflectionProbeRotation => useReflectionProbeRotation;
	}
}
