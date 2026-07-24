namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.Rendering.DisplayInfo(name = "URP Global Settings Asset", order = 40002)]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::System.ComponentModel.DisplayName("URP")]
	internal class UniversalRenderPipelineGlobalSettings : global::UnityEngine.Rendering.RenderPipelineGlobalSettings<global::UnityEngine.Rendering.Universal.UniversalRenderPipelineGlobalSettings, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline>
	{
		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Keep for migration. #from(2023.2)")]
		internal global::UnityEngine.Rendering.ShaderStrippingSetting m_ShaderStrippingSetting = new global::UnityEngine.Rendering.ShaderStrippingSetting();

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Keep for migration. #from(2023.2)")]
		internal global::UnityEngine.Rendering.Universal.URPShaderStrippingSetting m_URPShaderStrippingSetting = new global::UnityEngine.Rendering.Universal.URPShaderStrippingSetting();

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Keep for migration. #from(2023.2)")]
		internal global::UnityEngine.Rendering.ShaderVariantLogLevel m_ShaderVariantLogLevel;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Keep for migration. #from(2023.2)")]
		internal bool m_ExportShaderVariants = true;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Keep for migration. #from(2023.2)")]
		internal bool m_StripDebugVariants = true;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Keep for migration. #from(2023.2)")]
		internal bool m_StripUnusedPostProcessingVariants;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Keep for migration. #from(2023.2)")]
		internal bool m_StripUnusedVariants = true;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Keep for migration. #from(2023.2)")]
		internal bool m_StripScreenCoordOverrideVariants = true;

		[global::System.Obsolete("Please use stripRuntimeDebugShaders instead. #from(2023.1)")]
		public bool supportRuntimeDebugDisplay;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Keep for migration. #from(2023.2)")]
		internal bool m_EnableRenderGraph;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.RenderPipelineGraphicsSettingsContainer m_Settings = new global::UnityEngine.Rendering.RenderPipelineGraphicsSettingsContainer();

		internal const int k_LastVersion = 10;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("k_AssetVersion")]
		internal int m_AssetVersion = 10;

		public const string defaultAssetName = "UniversalRenderPipelineGlobalSettings";

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_DefaultVolumeProfile")]
		[global::System.Obsolete("Kept For Migration. #from(2023.3)")]
		internal global::UnityEngine.Rendering.VolumeProfile m_ObsoleteDefaultVolumeProfile;

		[global::UnityEngine.SerializeField]
		internal string[] m_RenderingLayerNames = new string[1] { "Default" };

		[global::UnityEngine.SerializeField]
		private uint m_ValidRenderingLayers;

		[global::System.Obsolete("This is obsolete, please use renderingLayerMaskNames instead. #from(2022.2)")]
		public string lightLayerName0;

		[global::System.Obsolete("This is obsolete, please use renderingLayerMaskNames instead. #from(2022.2)")]
		public string lightLayerName1;

		[global::System.Obsolete("This is obsolete, please use renderingLayerMaskNames instead. #from(2022.2)")]
		public string lightLayerName2;

		[global::System.Obsolete("This is obsolete, please use renderingLayerMaskNames instead. #from(2022.2)")]
		public string lightLayerName3;

		[global::System.Obsolete("This is obsolete, please use renderingLayerMaskNames instead. #from(2022.2)")]
		public string lightLayerName4;

		[global::System.Obsolete("This is obsolete, please use renderingLayerMaskNames instead. #from(2022.2)")]
		public string lightLayerName5;

		[global::System.Obsolete("This is obsolete, please use renderingLayerMaskNames instead. #from(2022.2)")]
		public string lightLayerName6;

		[global::System.Obsolete("This is obsolete, please use renderingLayerNames instead. #from(2022.2)")]
		public string lightLayerName7;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.ProbeVolumeSceneData apvScenesData;

		[global::System.Obsolete("No longer used as Shader Prefiltering automatically strips out unused LOD Crossfade variants. Please use the LOD Crossfade setting in the URP Asset to disable the feature if not used. #from(2023.1)")]
		public bool stripUnusedLODCrossFadeVariants
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		protected override global::System.Collections.Generic.List<global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings> settingsList => m_Settings.settingsList;

		[global::System.Obsolete("This property is obsolete. Use RenderingLayerMask API and Tags & Layers project settings instead. #from(2022.2) #breackingFrom(2023.1)", true)]
		public string[] prefixedLightLayerNames => new string[0];

		[global::System.Obsolete("This is obsolete, please use renderingLayerMaskNames instead. #from(2022.2)")]
		public string[] lightLayerNames => new string[0];

		internal bool IsAtLastVersion()
		{
			return 10 == m_AssetVersion;
		}

		public override void Reset()
		{
			base.Reset();
			global::UnityEngine.Rendering.Universal.DecalProjector.UpdateAllDecalProperties();
		}

		internal static global::UnityEngine.Rendering.VolumeProfile GetOrCreateDefaultVolumeProfile(global::UnityEngine.Rendering.VolumeProfile defaultVolumeProfile)
		{
			return defaultVolumeProfile;
		}

		internal void ResetRenderingLayerNames()
		{
			m_RenderingLayerNames = new string[1] { "Default" };
		}

		internal global::UnityEngine.Rendering.ProbeVolumeSceneData GetOrCreateAPVSceneData()
		{
			if (apvScenesData == null)
			{
				apvScenesData = new global::UnityEngine.Rendering.ProbeVolumeSceneData(this);
			}
			apvScenesData.SetParentObject(this);
			return apvScenesData;
		}
	}
}
