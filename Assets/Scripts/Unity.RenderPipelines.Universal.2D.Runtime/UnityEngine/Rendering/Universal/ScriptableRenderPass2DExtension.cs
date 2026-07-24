namespace UnityEngine.Rendering.Universal
{
	internal static class ScriptableRenderPass2DExtension
	{
		internal static void GetInjectionPoint2D(this global::UnityEngine.Rendering.Universal.ScriptableRenderPass renderPass, out global::UnityEngine.Rendering.Universal.RenderPassEvent2D rpEvent, out int rpLayer)
		{
			rpLayer = int.MinValue;
			if (renderPass.renderPassEvent <= global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingTransparents)
			{
				rpEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent2D.BeforeRendering;
			}
			else if (renderPass.renderPassEvent <= global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing)
			{
				rpEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent2D.BeforeRenderingPostProcessing;
			}
			else if (renderPass.renderPassEvent <= global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingPostProcessing)
			{
				rpEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent2D.AfterRenderingPostProcessing;
			}
			else
			{
				rpEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent2D.AfterRendering;
			}
		}
	}
}
