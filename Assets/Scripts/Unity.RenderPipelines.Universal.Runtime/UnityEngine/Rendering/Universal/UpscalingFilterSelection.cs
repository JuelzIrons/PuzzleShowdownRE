namespace UnityEngine.Rendering.Universal
{
	public enum UpscalingFilterSelection
	{
		[global::UnityEngine.InspectorName("Automatic")]
		[global::UnityEngine.Tooltip("Unity selects a filtering option automatically based on the Render Scale value and the current screen resolution.")]
		Auto = 0,
		[global::UnityEngine.InspectorName("Bilinear")]
		Linear = 1,
		[global::UnityEngine.InspectorName("Nearest-Neighbor")]
		Point = 2,
		[global::UnityEngine.InspectorName("FidelityFX Super Resolution 1.0")]
		[global::UnityEngine.Tooltip("If the target device does not support Unity shader model 4.5, Unity falls back to the Automatic option.")]
		FSR = 3,
		[global::UnityEngine.InspectorName("Spatial-Temporal Post-Processing")]
		[global::UnityEngine.Tooltip("If the target device does not support compute shaders or is running GLES, Unity falls back to the Automatic option.")]
		STP = 4
	}
}
