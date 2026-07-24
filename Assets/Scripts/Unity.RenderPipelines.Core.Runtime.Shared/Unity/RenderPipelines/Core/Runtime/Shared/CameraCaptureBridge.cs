namespace Unity.RenderPipelines.Core.Runtime.Shared
{
	internal static class CameraCaptureBridge
	{
		public static global::System.Collections.Generic.IEnumerator<global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer>> GetCachedCaptureActionsEnumerator(global::UnityEngine.Camera camera)
		{
			return global::UnityEngine.Rendering.CameraCaptureBridge.GetCachedCaptureActionsEnumerator(camera);
		}
	}
}
