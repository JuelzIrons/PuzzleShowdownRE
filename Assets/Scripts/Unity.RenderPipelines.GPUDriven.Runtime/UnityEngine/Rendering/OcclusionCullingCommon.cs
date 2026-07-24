namespace UnityEngine.Rendering
{
	internal class OcclusionCullingCommon : global::System.IDisposable
	{
		private struct OccluderContextSlot
		{
			public bool valid;

			public int lastUsedFrameIndex;

			public int viewInstanceID;
		}

		private static class ShaderIDs
		{
			public static readonly int OcclusionCullingCommonShaderVariables = global::UnityEngine.Shader.PropertyToID("OcclusionCullingCommonShaderVariables");

			public static readonly int _OccluderDepthPyramid = global::UnityEngine.Shader.PropertyToID("_OccluderDepthPyramid");

			public static readonly int _OcclusionDebugOverlay = global::UnityEngine.Shader.PropertyToID("_OcclusionDebugOverlay");

			public static readonly int OcclusionCullingDebugShaderVariables = global::UnityEngine.Shader.PropertyToID("OcclusionCullingDebugShaderVariables");
		}

		private class OcclusionTestOverlaySetupPassData
		{
			public global::UnityEngine.Rendering.OcclusionCullingDebugShaderVariables cb;
		}

		private class OcclusionTestOverlayPassData
		{
			public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle debugPyramid;
		}

		private struct DebugOccluderViewData
		{
			public int passIndex;

			public global::UnityEngine.Rect viewport;

			public bool valid;
		}

		private class OccluderOverlayPassData
		{
			public global::UnityEngine.Material debugMaterial;

			public global::UnityEngine.Rendering.RTHandle occluderTexture;

			public global::UnityEngine.Rect viewport;

			public int passIndex;

			public global::UnityEngine.Vector2 validRange;
		}

		private class UpdateOccludersPassData
		{
			public global::UnityEngine.Rendering.OccluderParameters occluderParams;

			public global::System.Collections.Generic.List<global::UnityEngine.Rendering.OccluderSubviewUpdate> occluderSubviewUpdates;

			public global::UnityEngine.Rendering.OccluderHandles occluderHandles;
		}

		private static readonly int s_MaxContextGCFrame = 8;

		private global::UnityEngine.Material m_DebugOcclusionTestMaterial;

		private global::UnityEngine.Material m_OccluderDebugViewMaterial;

		private global::UnityEngine.ComputeShader m_OcclusionDebugCS;

		private int m_ClearOcclusionDebugKernel;

		private global::UnityEngine.ComputeShader m_OccluderDepthPyramidCS;

		private int m_OccluderDepthDownscaleKernel;

		private int m_FrameIndex;

		private global::UnityEngine.Rendering.SilhouettePlaneCache m_SilhouettePlaneCache;

		private global::Unity.Collections.NativeParallelHashMap<int, int> m_ViewIDToIndexMap;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.OccluderContext> m_OccluderContextData;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.OcclusionCullingCommon.OccluderContextSlot> m_OccluderContextSlots;

		private global::Unity.Collections.NativeList<int> m_FreeOccluderContexts;

		private global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.OcclusionCullingCommonShaderVariables> m_CommonShaderVariables;

		private global::UnityEngine.ComputeBuffer m_CommonConstantBuffer;

		private global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.OcclusionCullingDebugShaderVariables> m_DebugShaderVariables;

		private global::UnityEngine.ComputeBuffer m_DebugConstantBuffer;

		private global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerUpdateOccluders;

		private global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerOcclusionTestOverlay;

		private global::UnityEngine.Rendering.ProfilingSampler m_ProfilingSamplerOccluderOverlay;

		internal void Init(global::UnityEngine.Rendering.GPUResidentDrawerResources resources)
		{
			m_DebugOcclusionTestMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(resources.debugOcclusionTestPS);
			m_OccluderDebugViewMaterial = global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(resources.debugOccluderPS);
			m_OcclusionDebugCS = resources.occlusionCullingDebugKernels;
			m_ClearOcclusionDebugKernel = m_OcclusionDebugCS.FindKernel("ClearOcclusionDebug");
			m_OccluderDepthPyramidCS = resources.occluderDepthPyramidKernels;
			m_OccluderDepthDownscaleKernel = m_OccluderDepthPyramidCS.FindKernel("OccluderDepthDownscale");
			m_SilhouettePlaneCache.Init();
			m_ViewIDToIndexMap = new global::Unity.Collections.NativeParallelHashMap<int, int>(64, global::Unity.Collections.Allocator.Persistent);
			m_OccluderContextData = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.OccluderContext>();
			m_OccluderContextSlots = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.OcclusionCullingCommon.OccluderContextSlot>(64, global::Unity.Collections.Allocator.Persistent);
			m_FreeOccluderContexts = new global::Unity.Collections.NativeList<int>(64, global::Unity.Collections.Allocator.Persistent);
			m_ProfilingSamplerUpdateOccluders = new global::UnityEngine.Rendering.ProfilingSampler("UpdateOccluders");
			m_ProfilingSamplerOcclusionTestOverlay = new global::UnityEngine.Rendering.ProfilingSampler("OcclusionTestOverlay");
			m_ProfilingSamplerOccluderOverlay = new global::UnityEngine.Rendering.ProfilingSampler("OccluderOverlay");
			m_CommonShaderVariables = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.OcclusionCullingCommonShaderVariables>(1, global::Unity.Collections.Allocator.Persistent);
			m_CommonConstantBuffer = new global::UnityEngine.ComputeBuffer(1, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.Rendering.OcclusionCullingCommonShaderVariables>(), global::UnityEngine.ComputeBufferType.Constant);
			m_DebugShaderVariables = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.OcclusionCullingDebugShaderVariables>(1, global::Unity.Collections.Allocator.Persistent);
			m_DebugConstantBuffer = new global::UnityEngine.ComputeBuffer(1, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.Rendering.OcclusionCullingDebugShaderVariables>(), global::UnityEngine.ComputeBufferType.Constant);
		}

		internal static bool UseOcclusionDebug(in global::UnityEngine.Rendering.OccluderContext occluderCtx)
		{
			return occluderCtx.occlusionDebugOverlaySize != 0;
		}

		internal void PrepareCulling(global::UnityEngine.Rendering.ComputeCommandBuffer cmd, in global::UnityEngine.Rendering.OccluderContext occluderCtx, in global::UnityEngine.Rendering.OcclusionCullingSettings settings, in global::UnityEngine.Rendering.InstanceOcclusionTestSubviewSettings subviewSettings, in global::UnityEngine.Rendering.OcclusionTestComputeShader shader, bool useOcclusionDebug)
		{
			global::UnityEngine.Rendering.OccluderContext.SetKeyword(cmd, shader.cs, in shader.occlusionDebugKeyword, useOcclusionDebug);
			global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats();
			m_CommonShaderVariables[0] = new global::UnityEngine.Rendering.OcclusionCullingCommonShaderVariables(in occluderCtx, in subviewSettings, debugStats?.occlusionOverlayCountVisible ?? false, debugStats?.overrideOcclusionTestToAlwaysPass ?? false);
			cmd.SetBufferData(m_CommonConstantBuffer, m_CommonShaderVariables);
			cmd.SetComputeConstantBufferParam(shader.cs, global::UnityEngine.Rendering.OcclusionCullingCommon.ShaderIDs.OcclusionCullingCommonShaderVariables, m_CommonConstantBuffer, 0, m_CommonConstantBuffer.stride);
			DispatchDebugClear(cmd, settings.viewInstanceID);
		}

		internal static void SetDepthPyramid(global::UnityEngine.Rendering.ComputeCommandBuffer cmd, in global::UnityEngine.Rendering.OcclusionTestComputeShader shader, int kernel, in global::UnityEngine.Rendering.OccluderHandles occluderHandles)
		{
			cmd.SetComputeTextureParam(shader.cs, kernel, global::UnityEngine.Rendering.OcclusionCullingCommon.ShaderIDs._OccluderDepthPyramid, occluderHandles.occluderDepthPyramid);
		}

		internal static void SetDebugPyramid(global::UnityEngine.Rendering.ComputeCommandBuffer cmd, in global::UnityEngine.Rendering.OcclusionTestComputeShader shader, int kernel, in global::UnityEngine.Rendering.OccluderHandles occluderHandles)
		{
			cmd.SetComputeBufferParam(shader.cs, kernel, global::UnityEngine.Rendering.OcclusionCullingCommon.ShaderIDs._OcclusionDebugOverlay, occluderHandles.occlusionDebugOverlay);
		}

		public void RenderDebugOcclusionTestOverlay(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer debugSettings, int viewInstanceID, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorBuffer)
		{
			if (debugSettings == null || !debugSettings.occlusionTestOverlayEnable)
			{
				return;
			}
			global::UnityEngine.Rendering.OcclusionCullingDebugOutput occlusionTestDebugOutput = GetOcclusionTestDebugOutput(viewInstanceID);
			if (occlusionTestDebugOutput.occlusionDebugOverlay == null)
			{
				return;
			}
			global::UnityEngine.Rendering.OcclusionCullingCommon.OcclusionTestOverlaySetupPassData passData;
			using (global::UnityEngine.Rendering.RenderGraphModule.IComputeRenderGraphBuilder computeRenderGraphBuilder = renderGraph.AddComputePass<global::UnityEngine.Rendering.OcclusionCullingCommon.OcclusionTestOverlaySetupPassData>("OcclusionTestOverlay", out passData, m_ProfilingSamplerOcclusionTestOverlay, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\GPUDriven\\OcclusionCullingCommon.cs", 275))
			{
				computeRenderGraphBuilder.AllowPassCulling(value: false);
				passData.cb = occlusionTestDebugOutput.cb;
				computeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.OcclusionCullingCommon.OcclusionTestOverlaySetupPassData data, global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext ctx)
				{
					global::UnityEngine.Rendering.OcclusionCullingCommon occlusionCullingCommon = global::UnityEngine.Rendering.GPUResidentDrawer.instance.batcher.occlusionCullingCommon;
					occlusionCullingCommon.m_DebugShaderVariables[0] = data.cb;
					ctx.cmd.SetBufferData(occlusionCullingCommon.m_DebugConstantBuffer, occlusionCullingCommon.m_DebugShaderVariables);
					occlusionCullingCommon.m_DebugOcclusionTestMaterial.SetConstantBuffer(global::UnityEngine.Rendering.OcclusionCullingCommon.ShaderIDs.OcclusionCullingDebugShaderVariables, occlusionCullingCommon.m_DebugConstantBuffer, 0, occlusionCullingCommon.m_DebugConstantBuffer.stride);
				});
			}
			global::UnityEngine.Rendering.OcclusionCullingCommon.OcclusionTestOverlayPassData passData2;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.OcclusionCullingCommon.OcclusionTestOverlayPassData>("OcclusionTestOverlay", out passData2, m_ProfilingSamplerOcclusionTestOverlay, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\GPUDriven\\OcclusionCullingCommon.cs", 297);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			passData2.debugPyramid = renderGraph.ImportBuffer(occlusionTestDebugOutput.occlusionDebugOverlay);
			rasterRenderGraphBuilder.SetRenderAttachment(colorBuffer, 0);
			rasterRenderGraphBuilder.UseBuffer(in passData2.debugPyramid);
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.OcclusionCullingCommon.OcclusionTestOverlayPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext ctx)
			{
				ctx.cmd.SetGlobalBuffer(global::UnityEngine.Rendering.OcclusionCullingCommon.ShaderIDs._OcclusionDebugOverlay, data.debugPyramid);
				global::UnityEngine.Rendering.CoreUtils.DrawFullScreen(ctx.cmd, m_DebugOcclusionTestMaterial);
			});
		}

		public void RenderDebugOccluderOverlay(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer debugSettings, global::UnityEngine.Vector2 screenPos, float maxHeight, in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle colorBuffer)
		{
			if (debugSettings == null || !debugSettings.occluderDebugViewEnable || !debugSettings.GetOccluderViewInstanceID(out var viewInstanceID))
			{
				return;
			}
			global::UnityEngine.Rendering.RTHandle occluderDepthPyramid = GetOcclusionTestDebugOutput(viewInstanceID).occluderDepthPyramid;
			if (occluderDepthPyramid == null)
			{
				return;
			}
			global::UnityEngine.Material occluderDebugViewMaterial = m_OccluderDebugViewMaterial;
			int passIndex = occluderDebugViewMaterial.FindPass("DebugOccluder");
			global::UnityEngine.Vector2 vector = occluderDepthPyramid.referenceSize;
			float num = maxHeight / vector.y;
			vector *= num;
			global::UnityEngine.Rect viewport = new global::UnityEngine.Rect(screenPos.x, screenPos.y, vector.x, vector.y);
			global::UnityEngine.Rendering.OcclusionCullingCommon.OccluderOverlayPassData passData;
			using global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder rasterRenderGraphBuilder = renderGraph.AddRasterRenderPass<global::UnityEngine.Rendering.OcclusionCullingCommon.OccluderOverlayPassData>("OccluderOverlay", out passData, m_ProfilingSamplerOccluderOverlay, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\GPUDriven\\OcclusionCullingCommon.cs", 353);
			rasterRenderGraphBuilder.AllowGlobalStateModification(value: true);
			rasterRenderGraphBuilder.SetRenderAttachment(colorBuffer, 0);
			passData.debugMaterial = occluderDebugViewMaterial;
			passData.occluderTexture = occluderDepthPyramid;
			passData.viewport = viewport;
			passData.passIndex = passIndex;
			passData.validRange = debugSettings.occluderDebugViewRange;
			rasterRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.OcclusionCullingCommon.OccluderOverlayPassData data, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext ctx)
			{
				global::UnityEngine.MaterialPropertyBlock tempMaterialPropertyBlock = ctx.renderGraphPool.GetTempMaterialPropertyBlock();
				tempMaterialPropertyBlock.SetTexture("_OccluderTexture", data.occluderTexture);
				tempMaterialPropertyBlock.SetVector("_ValidRange", data.validRange);
				ctx.cmd.SetViewport(data.viewport);
				ctx.cmd.DrawProcedural(global::UnityEngine.Matrix4x4.identity, data.debugMaterial, data.passIndex, global::UnityEngine.MeshTopology.Triangles, 3, 1, tempMaterialPropertyBlock);
			});
		}

		private void DispatchDebugClear(global::UnityEngine.Rendering.ComputeCommandBuffer cmd, int viewInstanceID)
		{
			if (m_ViewIDToIndexMap.TryGetValue(viewInstanceID, out var item))
			{
				global::UnityEngine.Rendering.OccluderContext occluderCtx = m_OccluderContextData[item];
				if (UseOcclusionDebug(in occluderCtx) && occluderCtx.debugNeedsClear)
				{
					global::UnityEngine.ComputeShader occlusionDebugCS = m_OcclusionDebugCS;
					int clearOcclusionDebugKernel = m_ClearOcclusionDebugKernel;
					cmd.SetComputeConstantBufferParam(occlusionDebugCS, global::UnityEngine.Rendering.OcclusionCullingCommon.ShaderIDs.OcclusionCullingCommonShaderVariables, m_CommonConstantBuffer, 0, m_CommonConstantBuffer.stride);
					cmd.SetComputeBufferParam(occlusionDebugCS, clearOcclusionDebugKernel, global::UnityEngine.Rendering.OcclusionCullingCommon.ShaderIDs._OcclusionDebugOverlay, occluderCtx.occlusionDebugOverlay);
					global::UnityEngine.Vector2Int size = occluderCtx.occluderMipBounds[0].size;
					cmd.DispatchCompute(occlusionDebugCS, clearOcclusionDebugKernel, (size.x + 7) / 8, (size.y + 7) / 8, occluderCtx.subviewCount);
					occluderCtx.debugNeedsClear = false;
					m_OccluderContextData[item] = occluderCtx;
				}
			}
		}

		private global::UnityEngine.Rendering.OccluderHandles PrepareOccluders(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.OccluderParameters occluderParams)
		{
			global::UnityEngine.Rendering.OccluderHandles result = default(global::UnityEngine.Rendering.OccluderHandles);
			if (occluderParams.depthTexture.IsValid())
			{
				if (!m_ViewIDToIndexMap.TryGetValue(occluderParams.viewInstanceID, out var item))
				{
					item = NewContext(occluderParams.viewInstanceID);
				}
				global::UnityEngine.Rendering.OccluderContext value = m_OccluderContextData[item];
				value.PrepareOccluders(in occluderParams);
				result = value.Import(renderGraph);
				m_OccluderContextData[item] = value;
			}
			else
			{
				DeleteContext(occluderParams.viewInstanceID);
			}
			return result;
		}

		private void CreateFarDepthPyramid(global::UnityEngine.Rendering.ComputeCommandBuffer cmd, in global::UnityEngine.Rendering.OccluderParameters occluderParams, global::System.ReadOnlySpan<global::UnityEngine.Rendering.OccluderSubviewUpdate> occluderSubviewUpdates, in global::UnityEngine.Rendering.OccluderHandles occluderHandles)
		{
			if (m_ViewIDToIndexMap.TryGetValue(occluderParams.viewInstanceID, out var item))
			{
				global::Unity.Collections.NativeArray<global::UnityEngine.Plane> subArray = m_SilhouettePlaneCache.GetSubArray(occluderParams.viewInstanceID);
				global::UnityEngine.Rendering.OccluderContext value = m_OccluderContextData[item];
				value.CreateFarDepthPyramid(cmd, in occluderParams, occluderSubviewUpdates, in occluderHandles, subArray, m_OccluderDepthPyramidCS, m_OccluderDepthDownscaleKernel);
				value.version++;
				m_OccluderContextData[item] = value;
				global::UnityEngine.Rendering.OcclusionCullingCommon.OccluderContextSlot value2 = m_OccluderContextSlots[item];
				value2.lastUsedFrameIndex = m_FrameIndex;
				m_OccluderContextSlots[item] = value2;
			}
		}

		public bool UpdateInstanceOccluders(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, in global::UnityEngine.Rendering.OccluderParameters occluderParams, global::System.ReadOnlySpan<global::UnityEngine.Rendering.OccluderSubviewUpdate> occluderSubviewUpdates)
		{
			global::UnityEngine.Rendering.OccluderHandles occluderHandles = PrepareOccluders(renderGraph, in occluderParams);
			if (!occluderHandles.occluderDepthPyramid.IsValid())
			{
				return false;
			}
			global::UnityEngine.Rendering.OcclusionCullingCommon.UpdateOccludersPassData passData;
			using (global::UnityEngine.Rendering.RenderGraphModule.IComputeRenderGraphBuilder computeRenderGraphBuilder = renderGraph.AddComputePass<global::UnityEngine.Rendering.OcclusionCullingCommon.UpdateOccludersPassData>("Update Occluders", out passData, m_ProfilingSamplerUpdateOccluders, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\GPUDriven\\OcclusionCullingCommon.cs", 454))
			{
				computeRenderGraphBuilder.AllowGlobalStateModification(value: true);
				passData.occluderParams = occluderParams;
				if (passData.occluderSubviewUpdates == null)
				{
					passData.occluderSubviewUpdates = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.OccluderSubviewUpdate>();
				}
				else
				{
					passData.occluderSubviewUpdates.Clear();
				}
				for (int i = 0; i < occluderSubviewUpdates.Length; i++)
				{
					passData.occluderSubviewUpdates.Add(occluderSubviewUpdates[i]);
				}
				passData.occluderHandles = occluderHandles;
				computeRenderGraphBuilder.UseTexture(in passData.occluderParams.depthTexture);
				passData.occluderHandles.UseForOccluderUpdate(computeRenderGraphBuilder);
				computeRenderGraphBuilder.SetRenderFunc(delegate(global::UnityEngine.Rendering.OcclusionCullingCommon.UpdateOccludersPassData data, global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext context)
				{
					global::System.Span<global::UnityEngine.Rendering.OccluderSubviewUpdate> span = stackalloc global::UnityEngine.Rendering.OccluderSubviewUpdate[data.occluderSubviewUpdates.Count];
					int num = 0;
					for (int j = 0; j < data.occluderSubviewUpdates.Count; j++)
					{
						span[j] = data.occluderSubviewUpdates[j];
						num |= 1 << data.occluderSubviewUpdates[j].subviewIndex;
					}
					global::UnityEngine.Rendering.GPUResidentBatcher batcher = global::UnityEngine.Rendering.GPUResidentDrawer.instance.batcher;
					batcher.occlusionCullingCommon.CreateFarDepthPyramid(context.cmd, in data.occluderParams, span, in data.occluderHandles);
					batcher.instanceCullingBatcher.InstanceOccludersUpdated(data.occluderParams.viewInstanceID, num);
				});
			}
			return true;
		}

		internal void UpdateSilhouettePlanes(int viewInstanceID, global::Unity.Collections.NativeArray<global::UnityEngine.Plane> planes)
		{
			m_SilhouettePlaneCache.Update(viewInstanceID, planes, m_FrameIndex);
		}

		internal global::UnityEngine.Rendering.OcclusionCullingDebugOutput GetOcclusionTestDebugOutput(int viewInstanceID)
		{
			if (m_ViewIDToIndexMap.TryGetValue(viewInstanceID, out var item) && m_OccluderContextSlots[item].valid)
			{
				return m_OccluderContextData[item].GetDebugOutput();
			}
			return default(global::UnityEngine.Rendering.OcclusionCullingDebugOutput);
		}

		public void UpdateOccluderStats(global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats)
		{
			debugStats.occluderStats.Clear();
			foreach (global::Unity.Collections.LowLevel.Unsafe.KeyValue<int, int> item in m_ViewIDToIndexMap)
			{
				if (item.Value < m_OccluderContextSlots.Length && m_OccluderContextSlots[item.Value].valid)
				{
					ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DebugOccluderStats> occluderStats = ref debugStats.occluderStats;
					global::UnityEngine.Rendering.DebugOccluderStats value = new global::UnityEngine.Rendering.DebugOccluderStats
					{
						viewInstanceID = item.Key,
						subviewCount = m_OccluderContextData[item.Value].subviewCount,
						occluderMipLayoutSize = m_OccluderContextData[item.Value].occluderMipLayoutSize
					};
					occluderStats.Add(in value);
				}
			}
		}

		internal bool HasOccluderContext(int viewInstanceID)
		{
			return m_ViewIDToIndexMap.ContainsKey(viewInstanceID);
		}

		internal bool GetOccluderContext(int viewInstanceID, out global::UnityEngine.Rendering.OccluderContext occluderContext)
		{
			if (m_ViewIDToIndexMap.TryGetValue(viewInstanceID, out var item) && m_OccluderContextSlots[item].valid)
			{
				occluderContext = m_OccluderContextData[item];
				return true;
			}
			occluderContext = default(global::UnityEngine.Rendering.OccluderContext);
			return false;
		}

		internal void UpdateFrame()
		{
			for (int i = 0; i < m_OccluderContextData.Count; i++)
			{
				if (m_OccluderContextSlots[i].valid)
				{
					global::UnityEngine.Rendering.OccluderContext value = m_OccluderContextData[i];
					global::UnityEngine.Rendering.OcclusionCullingCommon.OccluderContextSlot occluderContextSlot = m_OccluderContextSlots[i];
					if (m_FrameIndex - occluderContextSlot.lastUsedFrameIndex >= s_MaxContextGCFrame)
					{
						DeleteContext(occluderContextSlot.viewInstanceID);
						continue;
					}
					value.debugNeedsClear = true;
					m_OccluderContextData[i] = value;
				}
			}
			m_SilhouettePlaneCache.FreeUnusedSlots(m_FrameIndex, s_MaxContextGCFrame);
			m_FrameIndex++;
		}

		private int NewContext(int viewInstanceID)
		{
			int num = -1;
			global::UnityEngine.Rendering.OcclusionCullingCommon.OccluderContextSlot value = new global::UnityEngine.Rendering.OcclusionCullingCommon.OccluderContextSlot
			{
				valid = true,
				viewInstanceID = viewInstanceID,
				lastUsedFrameIndex = m_FrameIndex
			};
			global::UnityEngine.Rendering.OccluderContext occluderContext = default(global::UnityEngine.Rendering.OccluderContext);
			if (m_FreeOccluderContexts.Length > 0)
			{
				num = m_FreeOccluderContexts[m_FreeOccluderContexts.Length - 1];
				m_FreeOccluderContexts.RemoveAt(m_FreeOccluderContexts.Length - 1);
				m_OccluderContextData[num] = occluderContext;
				m_OccluderContextSlots[num] = value;
			}
			else
			{
				num = m_OccluderContextData.Count;
				m_OccluderContextData.Add(occluderContext);
				m_OccluderContextSlots.Add(in value);
			}
			m_ViewIDToIndexMap.Add(viewInstanceID, num);
			return num;
		}

		private void DeleteContext(int viewInstanceID)
		{
			if (m_ViewIDToIndexMap.TryGetValue(viewInstanceID, out var item) && m_OccluderContextSlots[item].valid)
			{
				m_OccluderContextData[item].Dispose();
				m_OccluderContextSlots[item] = new global::UnityEngine.Rendering.OcclusionCullingCommon.OccluderContextSlot
				{
					valid = false
				};
				m_FreeOccluderContexts.Add(in item);
				m_ViewIDToIndexMap.Remove(viewInstanceID);
			}
		}

		public void Dispose()
		{
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_DebugOcclusionTestMaterial);
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_OccluderDebugViewMaterial);
			for (int i = 0; i < m_OccluderContextData.Count; i++)
			{
				if (m_OccluderContextSlots[i].valid)
				{
					m_OccluderContextData[i].Dispose();
				}
			}
			m_SilhouettePlaneCache.Dispose();
			m_ViewIDToIndexMap.Dispose();
			m_FreeOccluderContexts.Dispose();
			m_OccluderContextData.Clear();
			m_OccluderContextSlots.Dispose();
			m_CommonShaderVariables.Dispose();
			m_CommonConstantBuffer.Release();
			m_DebugShaderVariables.Dispose();
			m_DebugConstantBuffer.Release();
		}
	}
}
