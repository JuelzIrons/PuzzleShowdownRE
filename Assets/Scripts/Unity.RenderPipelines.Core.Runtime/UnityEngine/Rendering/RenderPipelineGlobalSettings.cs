namespace UnityEngine.Rendering
{
	public abstract class RenderPipelineGlobalSettings<TGlobalRenderPipelineSettings, TRenderPipeline> : global::UnityEngine.Rendering.RenderPipelineGlobalSettings where TGlobalRenderPipelineSettings : global::UnityEngine.Rendering.RenderPipelineGlobalSettings where TRenderPipeline : global::UnityEngine.Rendering.RenderPipeline
	{
		private static global::System.Lazy<TGlobalRenderPipelineSettings> s_Instance = new global::System.Lazy<TGlobalRenderPipelineSettings>(() => global::UnityEngine.Rendering.GraphicsSettings.GetSettingsForRenderPipeline<TRenderPipeline>() as TGlobalRenderPipelineSettings);

		public static TGlobalRenderPipelineSettings instance => s_Instance.Value;

		public virtual void Reset()
		{
		}
	}
}
