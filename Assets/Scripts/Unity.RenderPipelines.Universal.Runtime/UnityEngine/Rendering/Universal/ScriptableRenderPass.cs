namespace UnityEngine.Rendering.Universal
{
	public abstract class ScriptableRenderPass : global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphRecorder
	{
		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public static global::UnityEngine.Rendering.RTHandle k_CameraTarget;

		private global::UnityEngine.Rendering.ProfilingSampler m_ProfingSampler;

		private string m_PassName;

		internal global::Unity.Collections.NativeArray<int> m_ColorAttachmentIndices;

		internal global::Unity.Collections.NativeArray<int> m_InputAttachmentIndices;

		private global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput m_Input;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public global::UnityEngine.Rendering.RTHandle[] colorAttachmentHandles => null;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public global::UnityEngine.Rendering.RTHandle colorAttachmentHandle => null;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public global::UnityEngine.Rendering.RTHandle depthAttachmentHandle => null;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public global::UnityEngine.Rendering.RenderBufferStoreAction[] colorStoreActions => null;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public global::UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction => global::UnityEngine.Rendering.RenderBufferStoreAction.Store;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public global::UnityEngine.Rendering.ClearFlag clearFlag => global::UnityEngine.Rendering.ClearFlag.None;

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public global::UnityEngine.Color clearColor => default(global::UnityEngine.Color);

		public global::UnityEngine.Rendering.Universal.RenderPassEvent renderPassEvent { get; set; }

		public global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput input => m_Input;

		public bool requiresIntermediateTexture { get; set; }

		protected internal global::UnityEngine.Rendering.ProfilingSampler profilingSampler
		{
			get
			{
				return null;
			}
			set
			{
				m_ProfingSampler = value;
				m_PassName = ((value != null) ? value.name : GetType().Name);
			}
		}

		protected internal string passName => m_PassName;

		internal bool isBlitRenderPass { get; set; }

		internal int renderPassQueueIndex { get; set; }

		internal global::UnityEngine.Experimental.Rendering.GraphicsFormat[] renderTargetFormat { get; set; }

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public virtual void FrameCleanup(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			OnCameraCleanup(cmd);
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void ConfigureColorStoreAction(global::UnityEngine.Rendering.RenderBufferStoreAction storeAction, uint attachmentIndex = 0u)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void ConfigureColorStoreActions(global::UnityEngine.Rendering.RenderBufferStoreAction[] storeActions)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void ConfigureDepthStoreAction(global::UnityEngine.Rendering.RenderBufferStoreAction storeAction)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void ResetTarget()
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void ConfigureTarget(global::UnityEngine.Rendering.RTHandle colorAttachment, global::UnityEngine.Rendering.RTHandle depthAttachment)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void ConfigureTarget(global::UnityEngine.Rendering.RTHandle[] colorAttachments, global::UnityEngine.Rendering.RTHandle depthAttachment)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void ConfigureTarget(global::UnityEngine.Rendering.RTHandle colorAttachment)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void ConfigureTarget(global::UnityEngine.Rendering.RTHandle[] colorAttachments)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void ConfigureClear(global::UnityEngine.Rendering.ClearFlag clearFlag, global::UnityEngine.Color clearColor)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public virtual void OnCameraSetup(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public virtual void Configure(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.RenderTextureDescriptor cameraTextureDescriptor)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public virtual void OnFinishCameraStackRendering(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public virtual void Execute(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void Blit(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Rendering.RTHandle destination, global::UnityEngine.Material material = null, int passIndex = 0)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void Blit(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.RenderingData data, global::UnityEngine.Material material, int passIndex = 0)
		{
		}

		[global::System.Obsolete("This rendering path is for Compatibility Mode only which has been deprecated and hidden behind URP_COMPATIBILITY_MODE define. This will do nothing.")]
		public void Blit(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.RenderingData data, global::UnityEngine.Rendering.RTHandle source, global::UnityEngine.Material material, int passIndex = 0)
		{
		}

		internal static global::UnityEngine.Rendering.Universal.DebugHandler GetActiveDebugHandler(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Rendering.Universal.DebugHandler debugHandler = cameraData.renderer.DebugHandler;
			if (debugHandler != null && debugHandler.IsActiveForCamera(cameraData.isPreviewCamera))
			{
				return debugHandler;
			}
			return null;
		}

		public ScriptableRenderPass()
		{
			renderPassEvent = global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingOpaques;
			profilingSampler = new global::UnityEngine.Rendering.ProfilingSampler(GetType().Name);
		}

		public void ConfigureInput(global::UnityEngine.Rendering.Universal.ScriptableRenderPassInput passInput)
		{
			m_Input = passInput;
		}

		public virtual void OnCameraCleanup(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
		}

		public virtual void RecordRenderGraph(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.ContextContainer frameData)
		{
			global::UnityEngine.Debug.LogWarning("The render pass " + ToString() + " does not have an implementation of the RecordRenderGraph method. Please implement this method, or consider turning on Compatibility Mode (RenderGraph disabled) in the menu Edit > Project Settings > Graphics > URP. Otherwise the render pass will have no effect. For more information, refer to https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest/index.html?subfolder=/manual/customizing-urp.html.");
		}

		public global::UnityEngine.Rendering.DrawingSettings CreateDrawingSettings(global::UnityEngine.Rendering.ShaderTagId shaderTagId, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData, global::UnityEngine.Rendering.SortingCriteria sortingCriteria)
		{
			global::UnityEngine.Rendering.ContextContainer frameData = renderingData.frameData;
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData2 = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			return global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(shaderTagId, renderingData2, cameraData, lightData, sortingCriteria);
		}

		public global::UnityEngine.Rendering.DrawingSettings CreateDrawingSettings(global::UnityEngine.Rendering.ShaderTagId shaderTagId, global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.SortingCriteria sortingCriteria)
		{
			return global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(shaderTagId, renderingData, cameraData, lightData, sortingCriteria);
		}

		public global::UnityEngine.Rendering.DrawingSettings CreateDrawingSettings(global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> shaderTagIdList, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData, global::UnityEngine.Rendering.SortingCriteria sortingCriteria)
		{
			global::UnityEngine.Rendering.ContextContainer frameData = renderingData.frameData;
			global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData2 = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalRenderingData>();
			global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalCameraData>();
			global::UnityEngine.Rendering.Universal.UniversalLightData lightData = frameData.Get<global::UnityEngine.Rendering.Universal.UniversalLightData>();
			return global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(shaderTagIdList, renderingData2, cameraData, lightData, sortingCriteria);
		}

		public global::UnityEngine.Rendering.DrawingSettings CreateDrawingSettings(global::System.Collections.Generic.List<global::UnityEngine.Rendering.ShaderTagId> shaderTagIdList, global::UnityEngine.Rendering.Universal.UniversalRenderingData renderingData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.SortingCriteria sortingCriteria)
		{
			return global::UnityEngine.Rendering.Universal.RenderingUtils.CreateDrawingSettings(shaderTagIdList, renderingData, cameraData, lightData, sortingCriteria);
		}

		public static bool operator <(global::UnityEngine.Rendering.Universal.ScriptableRenderPass lhs, global::UnityEngine.Rendering.Universal.ScriptableRenderPass rhs)
		{
			return lhs.renderPassEvent < rhs.renderPassEvent;
		}

		public static bool operator >(global::UnityEngine.Rendering.Universal.ScriptableRenderPass lhs, global::UnityEngine.Rendering.Universal.ScriptableRenderPass rhs)
		{
			return lhs.renderPassEvent > rhs.renderPassEvent;
		}

		internal static int GetRenderPassEventRange(global::UnityEngine.Rendering.Universal.RenderPassEvent renderPassEvent)
		{
			int num = global::UnityEngine.Rendering.Universal.RenderPassEventsEnumValues.values.Length;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (global::UnityEngine.Rendering.Universal.RenderPassEventsEnumValues.values[num2] == (int)renderPassEvent)
				{
					break;
				}
				num2++;
			}
			if (num2 >= num)
			{
				global::UnityEngine.Debug.LogError("GetRenderPassEventRange: invalid renderPassEvent value cannot be found in the RenderPassEvent enumeration");
				return 0;
			}
			if (num2 + 1 >= num)
			{
				return 50;
			}
			return (int)(global::UnityEngine.Rendering.Universal.RenderPassEventsEnumValues.values[num2 + 1] - renderPassEvent);
		}
	}
}
