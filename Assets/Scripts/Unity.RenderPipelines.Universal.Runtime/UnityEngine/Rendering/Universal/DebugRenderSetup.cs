namespace UnityEngine.Rendering.Universal
{
	internal class DebugRenderSetup : global::System.IDisposable
	{
		private readonly global::UnityEngine.Rendering.Universal.DebugHandler m_DebugHandler;

		private readonly global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private readonly int m_Index;

		private global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial MaterialSettings => m_DebugHandler.DebugDisplaySettings.materialSettings;

		private global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering RenderingSettings => m_DebugHandler.DebugDisplaySettings.renderingSettings;

		private global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting LightingSettings => m_DebugHandler.DebugDisplaySettings.lightingSettings;

		internal void Begin(global::UnityEngine.Rendering.RasterCommandBuffer cmd)
		{
			switch (RenderingSettings.sceneOverrideMode)
			{
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Wireframe:
				cmd.SetWireframe(enable: true);
				break;
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.SolidWireframe:
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.ShadedWireframe:
				if (m_Index == 1)
				{
					cmd.SetWireframe(enable: true);
				}
				break;
			}
		}

		internal void End(global::UnityEngine.Rendering.RasterCommandBuffer cmd)
		{
			switch (RenderingSettings.sceneOverrideMode)
			{
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Wireframe:
				cmd.SetWireframe(enable: false);
				break;
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.SolidWireframe:
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.ShadedWireframe:
				if (m_Index == 1)
				{
					cmd.SetWireframe(enable: false);
				}
				break;
			}
		}

		internal DebugRenderSetup(global::UnityEngine.Rendering.Universal.DebugHandler debugHandler, int index, global::UnityEngine.Rendering.FilteringSettings filteringSettings)
		{
			m_DebugHandler = debugHandler;
			m_FilteringSettings = filteringSettings;
			m_Index = index;
		}

		internal void CreateRendererList(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.DrawingSettings drawingSettings, ref global::UnityEngine.Rendering.FilteringSettings filteringSettings, ref global::UnityEngine.Rendering.RenderStateBlock renderStateBlock, ref global::UnityEngine.Rendering.RendererList rendererList)
		{
			global::UnityEngine.Rendering.Universal.RenderingUtils.CreateRendererListWithRenderStateBlock(context, ref cullResults, drawingSettings, filteringSettings, renderStateBlock, ref rendererList);
		}

		internal void CreateRendererList(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.DrawingSettings drawingSettings, ref global::UnityEngine.Rendering.FilteringSettings filteringSettings, ref global::UnityEngine.Rendering.RenderStateBlock renderStateBlock, ref global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererListHdl)
		{
			global::UnityEngine.Rendering.Universal.RenderingUtils.CreateRendererListWithRenderStateBlock(renderGraph, ref cullResults, drawingSettings, filteringSettings, renderStateBlock, ref rendererListHdl);
		}

		internal void DrawWithRendererList(global::UnityEngine.Rendering.RasterCommandBuffer cmd, ref global::UnityEngine.Rendering.RendererList rendererList)
		{
			if (rendererList.isValid)
			{
				cmd.DrawRendererList(rendererList);
			}
		}

		internal global::UnityEngine.Rendering.DrawingSettings CreateDrawingSettings(global::UnityEngine.Rendering.DrawingSettings drawingSettings)
		{
			if (MaterialSettings.vertexAttributeDebugMode != global::UnityEngine.Rendering.Universal.DebugVertexAttributeMode.None)
			{
				global::UnityEngine.Material replacementMaterial = m_DebugHandler.ReplacementMaterial;
				global::UnityEngine.Rendering.DrawingSettings result = drawingSettings;
				result.overrideMaterial = replacementMaterial;
				result.overrideMaterialPassIndex = 0;
				return result;
			}
			return drawingSettings;
		}

		internal global::UnityEngine.Rendering.RenderStateBlock GetRenderStateBlock(global::UnityEngine.Rendering.RenderStateBlock renderStateBlock)
		{
			switch (RenderingSettings.sceneOverrideMode)
			{
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Overdraw:
			{
				bool num = m_FilteringSettings.renderQueueRange == global::UnityEngine.Rendering.RenderQueueRange.opaque || m_FilteringSettings.renderQueueRange == global::UnityEngine.Rendering.RenderQueueRange.all;
				bool flag = m_FilteringSettings.renderQueueRange == global::UnityEngine.Rendering.RenderQueueRange.transparent || m_FilteringSettings.renderQueueRange == global::UnityEngine.Rendering.RenderQueueRange.all;
				bool flag2 = m_DebugHandler.DebugDisplaySettings.renderingSettings.overdrawMode == global::UnityEngine.Rendering.Universal.DebugOverdrawMode.Opaque || m_DebugHandler.DebugDisplaySettings.renderingSettings.overdrawMode == global::UnityEngine.Rendering.Universal.DebugOverdrawMode.All;
				bool flag3 = m_DebugHandler.DebugDisplaySettings.renderingSettings.overdrawMode == global::UnityEngine.Rendering.Universal.DebugOverdrawMode.Transparent || m_DebugHandler.DebugDisplaySettings.renderingSettings.overdrawMode == global::UnityEngine.Rendering.Universal.DebugOverdrawMode.All;
				global::UnityEngine.Rendering.BlendMode destinationColorBlendMode = (((num && flag2) || (flag && flag3)) ? global::UnityEngine.Rendering.BlendMode.One : global::UnityEngine.Rendering.BlendMode.Zero);
				global::UnityEngine.Rendering.RenderTargetBlendState blendState = new global::UnityEngine.Rendering.RenderTargetBlendState(global::UnityEngine.Rendering.ColorWriteMask.All, global::UnityEngine.Rendering.BlendMode.One, destinationColorBlendMode);
				renderStateBlock.blendState = new global::UnityEngine.Rendering.BlendState
				{
					blendState0 = blendState
				};
				renderStateBlock.mask = global::UnityEngine.Rendering.RenderStateMask.Blend;
				break;
			}
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Wireframe:
				renderStateBlock.rasterState = new global::UnityEngine.Rendering.RasterState(global::UnityEngine.Rendering.CullMode.Off);
				renderStateBlock.mask = global::UnityEngine.Rendering.RenderStateMask.Raster;
				break;
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.SolidWireframe:
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.ShadedWireframe:
				if (m_Index == 1)
				{
					renderStateBlock.rasterState = new global::UnityEngine.Rendering.RasterState(global::UnityEngine.Rendering.CullMode.Back, -1, -1f);
					renderStateBlock.mask = global::UnityEngine.Rendering.RenderStateMask.Raster;
				}
				break;
			}
			return renderStateBlock;
		}

		internal int GetIndex()
		{
			return m_Index;
		}

		public void Dispose()
		{
		}
	}
}
