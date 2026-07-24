namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.ExcludeFromPreset]
	public abstract class ScriptableRendererFeature : global::UnityEngine.ScriptableObject, global::System.IDisposable
	{
		[global::System.Obsolete("This enum is not used. #from(6000.3)", false)]
		public enum IntermediateTextureUsage
		{
			Unknown = 0,
			Required = 1,
			NotRequired = 2
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private bool m_Active = true;

		public bool isActive => m_Active;

		[global::System.Obsolete("This property is not used. #from(6000.3)", false)]
		protected virtual global::UnityEngine.Rendering.Universal.ScriptableRendererFeature.IntermediateTextureUsage useIntermediateTextures => global::UnityEngine.Rendering.Universal.ScriptableRendererFeature.IntermediateTextureUsage.Unknown;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public virtual void SetupRenderPasses(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, in global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		public abstract void Create();

		public virtual void OnCameraPreCull(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, in global::UnityEngine.Rendering.Universal.CameraData cameraData)
		{
		}

		public abstract void AddRenderPasses(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData);

		private void OnEnable()
		{
			if (global::UnityEngine.Rendering.RenderPipelineManager.currentPipeline is global::UnityEngine.Rendering.Universal.UniversalRenderPipeline)
			{
				Create();
			}
		}

		private void OnValidate()
		{
			if (global::UnityEngine.Rendering.RenderPipelineManager.currentPipeline is global::UnityEngine.Rendering.Universal.UniversalRenderPipeline)
			{
				Create();
			}
		}

		internal virtual bool RequireRenderingLayers(bool isDeferred, bool needsGBufferAccurateNormals, out global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event atEvent, out global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize maskSize)
		{
			atEvent = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.Event.DepthNormalPrePass;
			maskSize = global::UnityEngine.Rendering.Universal.RenderingLayerUtils.MaskSize.Bits8;
			return false;
		}

		public void SetActive(bool active)
		{
			m_Active = active;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			global::System.GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
