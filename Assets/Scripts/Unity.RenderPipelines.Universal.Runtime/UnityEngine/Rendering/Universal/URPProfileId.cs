namespace UnityEngine.Rendering.Universal
{
	internal enum URPProfileId
	{
		UniversalRenderTotal = 0,
		UpdateVolumeFramework = 1,
		RenderCameraStack = 2,
		AdditionalLightsShadow = 3,
		ColorGradingLUT = 4,
		CopyColor = 5,
		CopyDepth = 6,
		DrawDepthNormalPrepass = 7,
		DepthPrepass = 8,
		UpdateReflectionProbeAtlas = 9,
		DrawOpaqueObjects = 10,
		DrawTransparentObjects = 11,
		DrawScreenSpaceUI = 12,
		RecordRenderGraph = 13,
		LightCookies = 14,
		MainLightShadow = 15,
		ResolveShadows = 16,
		SSAO = 17,
		StopNaNs = 18,
		SMAA = 19,
		GaussianDepthOfField = 20,
		BokehDepthOfField = 21,
		TemporalAA = 22,
		MotionBlur = 23,
		PaniniProjection = 24,
		UberPostProcess = 25,
		Bloom = 26,
		LensFlareDataDrivenComputeOcclusion = 27,
		LensFlareDataDriven = 28,
		LensFlareScreenSpace = 29,
		DrawMotionVectors = 30,
		DrawFullscreen = 31,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_SetupPostFX = 32,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_StopNaNs = 33,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_SMAAMaterialSetup = 34,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_SMAAEdgeDetection = 35,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_SMAABlendWeight = 36,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_SMAANeighborhoodBlend = 37,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_SetupDoF = 38,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_DOFComputeCOC = 39,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_DOFDownscalePrefilter = 40,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_DOFBlurH = 41,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_DOFBlurV = 42,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_DOFBlurBokeh = 43,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_DOFPostFilter = 44,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_DOFComposite = 45,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_TAA = 46,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_TAACopyHistory = 47,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_MotionBlur = 48,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_BloomSetup = 49,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_BloomPrefilter = 50,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_BloomDownsample = 51,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_BloomUpsample = 52,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_UberPostSetupBloomPass = 53,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_UberPost = 54,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_FinalSetup = 55,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_FinalFSRScale = 56,
		[global::UnityEngine.Rendering.HideInDebugUI]
		RG_FinalBlit = 57,
		BlitFinalToBackBuffer = 58,
		DrawSkybox = 59
	}
}
