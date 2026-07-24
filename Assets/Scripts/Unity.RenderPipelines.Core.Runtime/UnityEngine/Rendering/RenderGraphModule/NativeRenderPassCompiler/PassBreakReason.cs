namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal enum PassBreakReason
	{
		NotOptimized = 0,
		TargetSizeMismatch = 1,
		NextPassReadsTexture = 2,
		NextPassTargetsTexture = 3,
		NonRasterPass = 4,
		DifferentDepthTextures = 5,
		AttachmentLimitReached = 6,
		SubPassLimitReached = 7,
		EndOfGraph = 8,
		FRStateMismatch = 9,
		DifferentShadingRateImages = 10,
		DifferentShadingRateStates = 11,
		MultisampledShaderResolveMustBeLastPass = 12,
		ExtendedFeatureFlagsIncompatible = 13,
		PassMergingDisabled = 14,
		Merged = 15,
		Count = 16
	}
}
