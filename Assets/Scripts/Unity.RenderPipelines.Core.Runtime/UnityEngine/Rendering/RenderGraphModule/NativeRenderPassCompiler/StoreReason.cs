namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal enum StoreReason
	{
		InvalidReason = 0,
		StoreImported = 1,
		StoreUsedByLaterPass = 2,
		DiscardImported = 3,
		DiscardUnused = 4,
		DiscardBindMs = 5,
		NoMSAABuffer = 6,
		Count = 7
	}
}
