namespace UnityEngine.Rendering.Universal
{
	internal class DebugRendererLists
	{
		private readonly global::UnityEngine.Rendering.Universal.DebugHandler m_DebugHandler;

		private readonly global::UnityEngine.Rendering.FilteringSettings m_FilteringSettings;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DebugRenderSetup> m_DebugRenderSetups = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.DebugRenderSetup>(2);

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.RendererList> m_ActiveDebugRendererList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RendererList>(2);

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle> m_ActiveDebugRendererListHdl = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle>(2);

		public DebugRendererLists(global::UnityEngine.Rendering.Universal.DebugHandler debugHandler, global::UnityEngine.Rendering.FilteringSettings filteringSettings)
		{
			m_DebugHandler = debugHandler;
			m_FilteringSettings = filteringSettings;
		}

		private void CreateDebugRenderSetups(global::UnityEngine.Rendering.FilteringSettings filteringSettings)
		{
			global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode sceneOverrideMode = m_DebugHandler.DebugDisplaySettings.renderingSettings.sceneOverrideMode;
			int num = ((sceneOverrideMode != global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.SolidWireframe && sceneOverrideMode != global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.ShadedWireframe) ? 1 : 2);
			for (int i = 0; i < num; i++)
			{
				m_DebugRenderSetups.Add(new global::UnityEngine.Rendering.Universal.DebugRenderSetup(m_DebugHandler, i, filteringSettings));
			}
		}

		private void DisposeDebugRenderLists()
		{
			foreach (global::UnityEngine.Rendering.Universal.DebugRenderSetup debugRenderSetup in m_DebugRenderSetups)
			{
				debugRenderSetup.Dispose();
			}
			m_DebugRenderSetups.Clear();
			m_ActiveDebugRendererList.Clear();
			m_ActiveDebugRendererListHdl.Clear();
		}

		internal void CreateRendererListsWithDebugRenderState(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.DrawingSettings drawingSettings, ref global::UnityEngine.Rendering.FilteringSettings filteringSettings, ref global::UnityEngine.Rendering.RenderStateBlock renderStateBlock)
		{
			CreateDebugRenderSetups(filteringSettings);
			foreach (global::UnityEngine.Rendering.Universal.DebugRenderSetup debugRenderSetup in m_DebugRenderSetups)
			{
				global::UnityEngine.Rendering.DrawingSettings ds = debugRenderSetup.CreateDrawingSettings(drawingSettings);
				global::UnityEngine.Rendering.RenderStateBlock renderStateBlock2 = debugRenderSetup.GetRenderStateBlock(renderStateBlock);
				global::UnityEngine.Rendering.RendererList rl = default(global::UnityEngine.Rendering.RendererList);
				global::UnityEngine.Rendering.Universal.RenderingUtils.CreateRendererListWithRenderStateBlock(context, ref cullResults, ds, filteringSettings, renderStateBlock2, ref rl);
				m_ActiveDebugRendererList.Add(rl);
			}
		}

		internal void CreateRendererListsWithDebugRenderState(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, ref global::UnityEngine.Rendering.CullingResults cullResults, ref global::UnityEngine.Rendering.DrawingSettings drawingSettings, ref global::UnityEngine.Rendering.FilteringSettings filteringSettings, ref global::UnityEngine.Rendering.RenderStateBlock renderStateBlock)
		{
			CreateDebugRenderSetups(filteringSettings);
			foreach (global::UnityEngine.Rendering.Universal.DebugRenderSetup debugRenderSetup in m_DebugRenderSetups)
			{
				global::UnityEngine.Rendering.DrawingSettings ds = debugRenderSetup.CreateDrawingSettings(drawingSettings);
				global::UnityEngine.Rendering.RenderStateBlock renderStateBlock2 = debugRenderSetup.GetRenderStateBlock(renderStateBlock);
				global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rl = default(global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle);
				global::UnityEngine.Rendering.Universal.RenderingUtils.CreateRendererListWithRenderStateBlock(renderGraph, ref cullResults, ds, filteringSettings, renderStateBlock2, ref rl);
				m_ActiveDebugRendererListHdl.Add(rl);
			}
		}

		internal void PrepareRendererListForRasterPass(global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder builder)
		{
			foreach (global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle item in m_ActiveDebugRendererListHdl)
			{
				builder.UseRendererList(item);
			}
		}

		internal void DrawWithRendererList(global::UnityEngine.Rendering.RasterCommandBuffer cmd)
		{
			foreach (global::UnityEngine.Rendering.Universal.DebugRenderSetup debugRenderSetup in m_DebugRenderSetups)
			{
				debugRenderSetup.Begin(cmd);
				global::UnityEngine.Rendering.RendererList rendererList = default(global::UnityEngine.Rendering.RendererList);
				if (m_ActiveDebugRendererList.Count > 0)
				{
					rendererList = m_ActiveDebugRendererList[debugRenderSetup.GetIndex()];
				}
				else if (m_ActiveDebugRendererListHdl.Count > 0)
				{
					rendererList = m_ActiveDebugRendererListHdl[debugRenderSetup.GetIndex()];
				}
				debugRenderSetup.DrawWithRendererList(cmd, ref rendererList);
				debugRenderSetup.End(cmd);
			}
			DisposeDebugRenderLists();
		}
	}
}
