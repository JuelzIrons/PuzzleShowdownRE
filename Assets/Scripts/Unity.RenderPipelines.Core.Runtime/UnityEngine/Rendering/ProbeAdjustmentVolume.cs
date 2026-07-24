namespace UnityEngine.Rendering
{
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.AddComponentMenu("Rendering/Probe Adjustment Volume")]
	public class ProbeAdjustmentVolume : global::UnityEngine.MonoBehaviour, global::UnityEngine.ISerializationCallbackReceiver
	{
		public enum Shape
		{
			Box = 0,
			Sphere = 1
		}

		public enum Mode
		{
			InvalidateProbes = 0,
			OverrideValidityThreshold = 1,
			ApplyVirtualOffset = 2,
			OverrideVirtualOffsetSettings = 3,
			OverrideSkyDirection = 4,
			OverrideSampleCount = 5,
			OverrideRenderingLayerMask = 6,
			IntensityScale = 99
		}

		public enum RenderingLayerMaskOperation
		{
			Override = 0,
			Add = 1,
			Remove = 2
		}

		private enum Version
		{
			Initial = 0,
			Mode = 1,
			Count = 2
		}

		[global::UnityEngine.Tooltip("Select the shape used for this Probe Adjustment Volume.")]
		public global::UnityEngine.Rendering.ProbeAdjustmentVolume.Shape shape;

		[global::UnityEngine.Min(0f)]
		[global::UnityEngine.Tooltip("Modify the size of this Probe Adjustment Volume. This is unaffected by the GameObject's Transform's Scale property.")]
		public global::UnityEngine.Vector3 size = new global::UnityEngine.Vector3(1f, 1f, 1f);

		[global::UnityEngine.Min(0f)]
		[global::UnityEngine.Tooltip("Modify the radius of this Probe Adjustment Volume. This is unaffected by the GameObject's Transform's Scale property.")]
		public float radius = 1f;

		public global::UnityEngine.Rendering.ProbeAdjustmentVolume.Mode mode;

		[global::UnityEngine.Range(0.0001f, 2f)]
		[global::UnityEngine.Tooltip("A multiplier applied to the intensity of probes covered by this Probe Adjustment Volume.")]
		public float intensityScale = 1f;

		[global::UnityEngine.Range(0f, 0.95f)]
		public float overriddenDilationThreshold = 0.75f;

		public global::UnityEngine.Vector3 virtualOffsetRotation = global::UnityEngine.Vector3.zero;

		[global::UnityEngine.Min(0f)]
		public float virtualOffsetDistance = 1f;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.Tooltip("Determines how far Unity pushes a probe out of geometry after a ray hit.")]
		public float geometryBias = 0.01f;

		[global::UnityEngine.Range(0f, 0.95f)]
		public float virtualOffsetThreshold = 0.75f;

		[global::UnityEngine.Range(-0.05f, 0f)]
		[global::UnityEngine.Tooltip("Distance from the probe position used to determine the origin of the sampling ray.")]
		public float rayOriginBias = -0.001f;

		[global::UnityEngine.Tooltip("The direction for sampling the ambient probe in worldspace when using the Sky Visibility feature.")]
		public global::UnityEngine.Vector3 skyDirection = global::UnityEngine.Vector3.zero;

		internal global::UnityEngine.Vector3 skyShadingDirectionRotation = global::UnityEngine.Vector3.zero;

		[global::UnityEngine.Rendering.Logarithmic(1, 1024)]
		[global::UnityEngine.Tooltip("Number of samples for direct lighting computations.")]
		public int directSampleCount = 32;

		[global::UnityEngine.Rendering.Logarithmic(1, 8192)]
		[global::UnityEngine.Tooltip("Number of samples for indirect lighting computations. This includes environment samples.")]
		public int indirectSampleCount = 512;

		[global::UnityEngine.Min(0f)]
		[global::UnityEngine.Tooltip("Multiplier for the number of samples specified above.")]
		public int sampleCountMultiplier = 4;

		[global::UnityEngine.Min(0f)]
		[global::UnityEngine.Tooltip("Maximum number of bounces for indirect lighting.")]
		public int maxBounces = 2;

		[global::UnityEngine.Rendering.Logarithmic(1, 8192)]
		public int skyOcclusionSampleCount = 2048;

		[global::UnityEngine.Range(0f, 5f)]
		public int skyOcclusionMaxBounces = 2;

		public global::UnityEngine.Rendering.ProbeAdjustmentVolume.RenderingLayerMaskOperation renderingLayerMaskOperation;

		public byte renderingLayerMask;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.ProbeAdjustmentVolume.Version version = global::UnityEngine.Rendering.ProbeAdjustmentVolume.Version.Count;

		[global::System.Obsolete("This field is only kept for migration purpose. Use mode instead. #from(2023.1)")]
		public bool invalidateProbes;

		[global::System.Obsolete("This field is only kept for migration purpose. Use mode instead. #from(2023.1)")]
		public bool overrideDilationThreshold;

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			if (version == global::UnityEngine.Rendering.ProbeAdjustmentVolume.Version.Count)
			{
				version = global::UnityEngine.Rendering.ProbeAdjustmentVolume.Version.Mode;
			}
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (version == global::UnityEngine.Rendering.ProbeAdjustmentVolume.Version.Count)
			{
				version = global::UnityEngine.Rendering.ProbeAdjustmentVolume.Version.Initial;
			}
			if (version < global::UnityEngine.Rendering.ProbeAdjustmentVolume.Version.Mode)
			{
				if (invalidateProbes)
				{
					mode = global::UnityEngine.Rendering.ProbeAdjustmentVolume.Mode.InvalidateProbes;
				}
				else if (overrideDilationThreshold)
				{
					mode = global::UnityEngine.Rendering.ProbeAdjustmentVolume.Mode.OverrideValidityThreshold;
				}
				version = global::UnityEngine.Rendering.ProbeAdjustmentVolume.Version.Mode;
			}
		}
	}
}
