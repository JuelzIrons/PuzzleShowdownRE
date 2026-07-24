namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public struct GlobalDynamicResolutionSettings
	{
		public bool enabled;

		public bool useMipBias;

		[global::System.Obsolete("Obsolete, use advancedUpscalerNames list instead.")]
		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.AdvancedUpscalers> advancedUpscalersByPriority;

		public global::System.Collections.Generic.List<string> advancedUpscalerNames;

		public uint DLSSPerfQualitySetting;

		public global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType DLSSInjectionPoint;

		public global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType TAAUInjectionPoint;

		public global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType STPInjectionPoint;

		public global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType defaultInjectionPoint;

		public bool DLSSUseOptimalSettings;

		[global::UnityEngine.Range(0f, 1f)]
		public float DLSSSharpness;

		public uint DLSSRenderPresetForQuality;

		public uint DLSSRenderPresetForBalanced;

		public uint DLSSRenderPresetForPerformance;

		public uint DLSSRenderPresetForUltraPerformance;

		public uint DLSSRenderPresetForDLAA;

		public bool FSR2EnableSharpness;

		[global::UnityEngine.Range(0f, 1f)]
		public float FSR2Sharpness;

		public bool FSR2UseOptimalSettings;

		public uint FSR2QualitySetting;

		public global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType FSR2InjectionPoint;

		public bool fsrOverrideSharpness;

		[global::UnityEngine.Range(0f, 1f)]
		public float fsrSharpness;

		public float maxPercentage;

		public float minPercentage;

		public global::UnityEngine.Rendering.DynamicResolutionType dynResType;

		public global::UnityEngine.Rendering.DynamicResUpscaleFilter upsampleFilter;

		public bool forceResolution;

		public float forcedPercentage;

		public float lowResTransparencyMinimumThreshold;

		public float rayTracingHalfResThreshold;

		public float lowResSSGIMinimumThreshold;

		public float lowResVolumetricCloudsMinimumThreshold;

		[global::System.Obsolete("Obsolete, used only for data migration. Use the advancedUpscalersByPriority list instead to add the proper supported advanced upscaler by priority. #from(2023.3)")]
		public bool enableDLSS;

		public static global::UnityEngine.Rendering.GlobalDynamicResolutionSettings NewDefault()
		{
			return new global::UnityEngine.Rendering.GlobalDynamicResolutionSettings
			{
				useMipBias = false,
				maxPercentage = 100f,
				minPercentage = 100f,
				dynResType = global::UnityEngine.Rendering.DynamicResolutionType.Hardware,
				upsampleFilter = global::UnityEngine.Rendering.DynamicResUpscaleFilter.CatmullRom,
				forcedPercentage = 100f,
				lowResTransparencyMinimumThreshold = 0f,
				lowResVolumetricCloudsMinimumThreshold = 50f,
				rayTracingHalfResThreshold = 50f,
				DLSSUseOptimalSettings = true,
				DLSSPerfQualitySetting = 0u,
				DLSSSharpness = 0.5f,
				DLSSRenderPresetForQuality = 0u,
				DLSSRenderPresetForBalanced = 0u,
				DLSSRenderPresetForPerformance = 0u,
				DLSSRenderPresetForUltraPerformance = 0u,
				DLSSRenderPresetForDLAA = 0u,
				DLSSInjectionPoint = global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType.BeforePost,
				FSR2InjectionPoint = global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType.BeforePost,
				TAAUInjectionPoint = global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType.BeforePost,
				defaultInjectionPoint = global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType.AfterPost,
				advancedUpscalerNames = new global::System.Collections.Generic.List<string> { global::UnityEngine.Rendering.AdvancedUpscalers.STP.ToString() },
				fsrOverrideSharpness = false,
				fsrSharpness = 0.92f
			};
		}
	}
}
