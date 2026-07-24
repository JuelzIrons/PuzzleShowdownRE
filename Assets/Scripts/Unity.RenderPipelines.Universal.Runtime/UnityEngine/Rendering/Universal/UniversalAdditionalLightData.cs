namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Light))]
	public class UniversalAdditionalLightData : global::UnityEngine.MonoBehaviour, global::UnityEngine.ISerializationCallbackReceiver, global::UnityEngine.Rendering.IAdditionalData
	{
		private enum Version
		{
			Initial = 0,
			RenderingLayers = 2,
			SoftShadowQuality = 3,
			RenderingLayersMask = 4,
			Count = 5
		}

		[global::UnityEngine.Tooltip("Controls if light Shadow Bias parameters use pipeline settings.")]
		[global::UnityEngine.SerializeField]
		private bool m_UsePipelineSettings = true;

		public static readonly int AdditionalLightsShadowResolutionTierCustom = -1;

		public static readonly int AdditionalLightsShadowResolutionTierLow = 0;

		public static readonly int AdditionalLightsShadowResolutionTierMedium = 1;

		public static readonly int AdditionalLightsShadowResolutionTierHigh = 2;

		public static readonly int AdditionalLightsShadowDefaultResolutionTier = AdditionalLightsShadowResolutionTierHigh;

		public static readonly int AdditionalLightsShadowDefaultCustomResolution = 128;

		[global::System.NonSerialized]
		private global::UnityEngine.Light m_Light;

		public static readonly int AdditionalLightsShadowMinimumResolution = 128;

		[global::UnityEngine.Tooltip("Controls if light shadow resolution uses pipeline settings.")]
		[global::UnityEngine.SerializeField]
		private int m_AdditionalLightsShadowResolutionTier = AdditionalLightsShadowDefaultResolutionTier;

		[global::UnityEngine.SerializeField]
		private bool m_CustomShadowLayers;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector2 m_LightCookieSize = global::UnityEngine.Vector2.one;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector2 m_LightCookieOffset = global::UnityEngine.Vector2.zero;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.SoftShadowQuality m_SoftShadowQuality;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.RenderingLayerMask m_RenderingLayersMask = global::UnityEngine.RenderingLayerMask.defaultRenderingLayerMask;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.RenderingLayerMask m_ShadowRenderingLayersMask = global::UnityEngine.RenderingLayerMask.defaultRenderingLayerMask;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version m_Version = global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version.Count;

		[global::System.Obsolete("This is obsolete, please use m_RenderingLayerMask instead. #from(2023.1)")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.LightLayerEnum m_LightLayerMask = global::UnityEngine.Rendering.Universal.LightLayerEnum.LightLayerDefault;

		[global::System.Obsolete("This is obsolete, please use m_RenderingLayerMask instead. #from(2023.1)")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.LightLayerEnum m_ShadowLayerMask = global::UnityEngine.Rendering.Universal.LightLayerEnum.LightLayerDefault;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("This is obsolete, please use m_RenderingLayersMask instead. #from(6000.2)")]
		private uint m_RenderingLayers = 1u;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("This is obsolete, please use renderingLayersMask instead. #from(6000.2)")]
		private uint m_ShadowRenderingLayers = 1u;

		public bool usePipelineSettings
		{
			get
			{
				return m_UsePipelineSettings;
			}
			set
			{
				m_UsePipelineSettings = value;
			}
		}

		internal global::UnityEngine.Light light
		{
			get
			{
				if (!m_Light)
				{
					TryGetComponent<global::UnityEngine.Light>(out m_Light);
				}
				return m_Light;
			}
		}

		public int additionalLightsShadowResolutionTier => m_AdditionalLightsShadowResolutionTier;

		public bool customShadowLayers
		{
			get
			{
				return m_CustomShadowLayers;
			}
			set
			{
				if (m_CustomShadowLayers != value)
				{
					m_CustomShadowLayers = value;
					SyncLightAndShadowLayers();
				}
			}
		}

		[global::UnityEngine.Tooltip("Controls the size of the cookie mask currently assigned to the light.")]
		public global::UnityEngine.Vector2 lightCookieSize
		{
			get
			{
				return m_LightCookieSize;
			}
			set
			{
				m_LightCookieSize = value;
			}
		}

		[global::UnityEngine.Tooltip("Controls the offset of the cookie mask currently assigned to the light.")]
		public global::UnityEngine.Vector2 lightCookieOffset
		{
			get
			{
				return m_LightCookieOffset;
			}
			set
			{
				m_LightCookieOffset = value;
			}
		}

		[global::UnityEngine.Tooltip("Controls the filtering quality of soft shadows. Higher quality has lower performance.")]
		public global::UnityEngine.Rendering.Universal.SoftShadowQuality softShadowQuality
		{
			get
			{
				return m_SoftShadowQuality;
			}
			set
			{
				m_SoftShadowQuality = value;
			}
		}

		public global::UnityEngine.RenderingLayerMask renderingLayers
		{
			get
			{
				return m_RenderingLayersMask;
			}
			set
			{
				if ((int)m_RenderingLayersMask != (int)value)
				{
					m_RenderingLayersMask = value;
					SyncLightAndShadowLayers();
				}
			}
		}

		public global::UnityEngine.RenderingLayerMask shadowRenderingLayers
		{
			get
			{
				return m_ShadowRenderingLayersMask;
			}
			set
			{
				if ((int)value != (int)m_ShadowRenderingLayersMask)
				{
					m_ShadowRenderingLayersMask = value;
					SyncLightAndShadowLayers();
				}
			}
		}

		[global::System.Obsolete("This is obsolete, please use renderingLayerMask instead. #from(2023.1) #breakingFrom(2023.1)", true)]
		public global::UnityEngine.Rendering.Universal.LightLayerEnum lightLayerMask
		{
			get
			{
				return m_LightLayerMask;
			}
			set
			{
				m_LightLayerMask = value;
			}
		}

		[global::System.Obsolete("This is obsolete, please use shadowRenderingLayerMask instead. #from(2023.1) #breakingFrom(2023.1)", true)]
		public global::UnityEngine.Rendering.Universal.LightLayerEnum shadowLayerMask
		{
			get
			{
				return m_ShadowLayerMask;
			}
			set
			{
				m_ShadowLayerMask = value;
			}
		}

		private void SyncLightAndShadowLayers()
		{
			if ((bool)light)
			{
				light.renderingLayerMask = (m_CustomShadowLayers ? m_ShadowRenderingLayersMask : m_RenderingLayersMask);
			}
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			if (m_Version == global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version.Count)
			{
				m_Version = global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version.RenderingLayersMask;
			}
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (m_Version == global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version.Count)
			{
				m_Version = global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version.Initial;
			}
			if (m_Version < global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version.RenderingLayers)
			{
				m_RenderingLayers = (uint)m_LightLayerMask;
				m_ShadowRenderingLayers = (uint)m_ShadowLayerMask;
				m_Version = global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version.RenderingLayers;
			}
			if (m_Version < global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version.SoftShadowQuality)
			{
				m_SoftShadowQuality = (global::UnityEngine.Rendering.Universal.SoftShadowQuality)global::System.Math.Clamp((int)(m_SoftShadowQuality + 1), 0, 3);
				m_Version = global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version.SoftShadowQuality;
			}
			if (m_Version < global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version.RenderingLayersMask)
			{
				m_RenderingLayersMask = m_RenderingLayers;
				m_ShadowRenderingLayersMask = m_ShadowRenderingLayers;
				m_Version = global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData.Version.RenderingLayersMask;
			}
		}
	}
}
