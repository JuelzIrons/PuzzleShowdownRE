namespace UnityEngine.Rendering
{
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.AddComponentMenu("Rendering/Lens Flare (SRP)")]
	public sealed class LensFlareComponentSRP : global::UnityEngine.MonoBehaviour
	{
		private enum Version
		{
			Initial = 0
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.LensFlareDataSRP m_LensFlareData;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.LensFlareComponentSRP.Version version;

		[global::UnityEngine.Min(0f)]
		public float intensity = 1f;

		[global::UnityEngine.Min(1E-05f)]
		public float maxAttenuationDistance = 100f;

		[global::UnityEngine.Min(1E-05f)]
		public float maxAttenuationScale = 100f;

		public global::UnityEngine.AnimationCurve distanceAttenuationCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 1f), new global::UnityEngine.Keyframe(1f, 0f));

		public global::UnityEngine.AnimationCurve scaleByDistanceCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 1f), new global::UnityEngine.Keyframe(1f, 0f));

		public bool attenuationByLightShape = true;

		public global::UnityEngine.AnimationCurve radialScreenAttenuationCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 1f), new global::UnityEngine.Keyframe(1f, 1f));

		public bool useOcclusion;

		[global::System.Obsolete("Replaced by environmentOcclusion. #from(6000.0)")]
		public bool useBackgroundCloudOcclusion;

		[global::UnityEngine.Serialization.FormerlySerializedAs("volumetricCloudOcclusion")]
		[global::UnityEngine.Serialization.FormerlySerializedAs("useFogOpacityOcclusion")]
		public bool environmentOcclusion;

		[global::System.Obsolete("Replaced by environmentOcclusion. #from(6000.0)")]
		public bool useWaterOcclusion;

		[global::UnityEngine.Min(0f)]
		public float occlusionRadius = 0.1f;

		[global::UnityEngine.Range(1f, 64f)]
		public uint sampleCount = 32u;

		public float occlusionOffset = 0.05f;

		[global::UnityEngine.Min(0f)]
		public float scale = 1f;

		public bool allowOffScreen;

		[global::System.Obsolete("Please use environmentOcclusion instead. #from(6000.0)")]
		public bool volumetricCloudOcclusion;

		private static float sCelestialAngularRadius = 0.057595868f;

		public global::UnityEngine.Rendering.TextureCurve occlusionRemapCurve = new global::UnityEngine.Rendering.TextureCurve(global::UnityEngine.AnimationCurve.Linear(0f, 0f, 1f, 1f), 1f, loop: false, new global::UnityEngine.Vector2(0f, 1f));

		public global::UnityEngine.Light lightOverride;

		public global::UnityEngine.Rendering.LensFlareDataSRP lensFlareData
		{
			get
			{
				return m_LensFlareData;
			}
			set
			{
				m_LensFlareData = value;
				OnValidate();
			}
		}

		public float celestialProjectedOcclusionRadius(global::UnityEngine.Camera mainCam)
		{
			float num = (float)global::System.Math.Tan(sCelestialAngularRadius) * mainCam.farClipPlane;
			return occlusionRadius * num;
		}

		private void Awake()
		{
		}

		private void OnEnable()
		{
			if ((bool)lensFlareData)
			{
				global::UnityEngine.Rendering.LensFlareCommonSRP.Instance.AddData(this);
			}
			else
			{
				global::UnityEngine.Rendering.LensFlareCommonSRP.Instance.RemoveData(this);
			}
		}

		private void OnDisable()
		{
			global::UnityEngine.Rendering.LensFlareCommonSRP.Instance.RemoveData(this);
		}

		private void OnValidate()
		{
			if (base.isActiveAndEnabled && lensFlareData != null)
			{
				global::UnityEngine.Rendering.LensFlareCommonSRP.Instance.AddData(this);
			}
			else
			{
				global::UnityEngine.Rendering.LensFlareCommonSRP.Instance.RemoveData(this);
			}
		}

		private void OnDestroy()
		{
			occlusionRemapCurve.Release();
		}
	}
}
