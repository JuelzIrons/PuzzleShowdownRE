namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal enum LoadReason
	{
		InvalidReason = 0,
		LoadImported = 1,
		LoadPreviouslyWritten = 2,
		ClearImported = 3,
		ClearCreated = 4,
		FullyRewritten = 5,
		Count = 6
	}
}
