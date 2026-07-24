namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Flags]
	internal enum RenderGraphState
	{
		Idle = 0,
		RecordingGraph = 1,
		RecordingPass = 2,
		Executing = 4,
		Active = 7
	}
}
