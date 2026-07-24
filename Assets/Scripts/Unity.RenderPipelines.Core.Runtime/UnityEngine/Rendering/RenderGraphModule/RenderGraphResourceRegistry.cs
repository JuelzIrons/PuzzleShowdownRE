namespace UnityEngine.Rendering.RenderGraphModule
{
	internal class RenderGraphResourceRegistry
	{
		private delegate bool ResourceCreateCallback(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource res);

		private delegate void ResourceCallback(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource res);

		private class RenderGraphResourcesData
		{
			public global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource> resourceArray = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource>();

			public int sharedResourcesCount;

			public global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResourcePool pool;

			public global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.ResourceCreateCallback createResourceCallback;

			public global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.ResourceCallback releaseResourceCallback;

			public RenderGraphResourcesData()
			{
				resourceArray.Resize(1);
			}

			public void Clear(bool onException, int frameIndex)
			{
				resourceArray.Resize(sharedResourcesCount + 1);
				if (pool != null)
				{
					pool.CheckFrameAllocation(onException, frameIndex);
				}
			}

			public void Cleanup()
			{
				for (int i = 1; i < sharedResourcesCount + 1; i++)
				{
					resourceArray[i]?.ReleaseGraphicsResource();
				}
				if (pool != null)
				{
					pool.Cleanup();
				}
			}

			public void PurgeUnusedGraphicsResources(int frameIndex)
			{
				if (pool != null)
				{
					pool.PurgeUnusedResources(frameIndex);
				}
			}

			public int AddNewRenderGraphResource<ResType>(out ResType outRes, bool pooledResource = true) where ResType : global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource, new()
			{
				int size = resourceArray.size;
				resourceArray.Resize(resourceArray.size + 1, keepContent: true);
				if (resourceArray[size] == null)
				{
					resourceArray[size] = new ResType();
				}
				outRes = resourceArray[size] as ResType;
				global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResourcePool _ = (pooledResource ? pool : null);
				outRes.Reset(_);
				return size;
			}
		}

		private const int kSharedResourceLifetime = 30;

		private static global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry m_CurrentRegistry;

		private global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.RenderGraphResourcesData[] m_RenderGraphResources = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.RenderGraphResourcesData[3];

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.RendererListResource> m_RendererListResources = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.RendererListResource>();

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource> m_RendererListLegacyResources = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource>();

		private global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugParams m_RenderGraphDebug;

		private global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger m_ResourceLogger = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger();

		private global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger m_FrameInformationLogger;

		private int m_CurrentFrameIndex;

		private int m_ExecutionCount;

		private global::UnityEngine.Rendering.RTHandle m_CurrentBackbuffer;

		private const int kInitialRendererListCount = 256;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.RendererList> m_ActiveRendererLists = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RendererList>(256);

		private static global::UnityEngine.Rendering.RenderTargetIdentifier emptyId = global::UnityEngine.Rendering.RenderTargetIdentifier.Invalid;

		private static global::UnityEngine.Rendering.RenderTargetIdentifier builtinCameraRenderTarget = new global::UnityEngine.Rendering.RenderTargetIdentifier(global::UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget);

		internal bool forceManualClearOfResource = true;

		internal static global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry current
		{
			get
			{
				return m_CurrentRegistry;
			}
			set
			{
				m_CurrentRegistry = value;
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckTextureResource(global::UnityEngine.Rendering.RenderGraphModule.TextureResource texResource)
		{
			if (texResource.graphicsResource == null && !texResource.imported)
			{
				throw new global::System.InvalidOperationException("Trying to use a texture (" + texResource.GetName() + ") that was already released or not yet created. Make sure you declare it for reading in your pass or you don't read it before it's been written to at least once.");
			}
		}

		internal global::UnityEngine.Rendering.RTHandle GetTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle handle)
		{
			if (!handle.IsValid())
			{
				return null;
			}
			return GetTextureResource(in handle.handle).graphicsResource;
		}

		internal global::UnityEngine.Rendering.RTHandle GetTexture(int index)
		{
			return GetTextureResource(index).graphicsResource;
		}

		internal bool TextureNeedsFallback(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle handle)
		{
			if (!handle.IsValid())
			{
				return false;
			}
			return GetTextureResource(in handle.handle).NeedsFallBack();
		}

		internal global::UnityEngine.Rendering.RendererList GetRendererList(in global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle handle)
		{
			if (!handle.IsValid())
			{
				return global::UnityEngine.Rendering.RendererList.nullRendererList;
			}
			switch (handle.type)
			{
			case global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Renderers:
				if ((int)handle >= m_RendererListResources.size)
				{
					return global::UnityEngine.Rendering.RendererList.nullRendererList;
				}
				return m_RendererListResources[handle].rendererList;
			case global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Legacy:
				if ((int)handle >= m_RendererListLegacyResources.size)
				{
					return global::UnityEngine.Rendering.RendererList.nullRendererList;
				}
				if (!m_RendererListLegacyResources[handle].isActive)
				{
					return global::UnityEngine.Rendering.RendererList.nullRendererList;
				}
				return m_RendererListLegacyResources[handle].rendererList;
			default:
				return global::UnityEngine.Rendering.RendererList.nullRendererList;
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckBufferResource(global::UnityEngine.Rendering.RenderGraphModule.BufferResource bufferResource)
		{
			if (bufferResource.graphicsResource == null)
			{
				throw new global::System.InvalidOperationException("Trying to use a graphics buffer (" + bufferResource.GetName() + ") that was already released or not yet created. Make sure you declare it for reading in your pass or you don't read it before it's been written to at least once.");
			}
		}

		internal global::UnityEngine.GraphicsBuffer GetBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferHandle handle)
		{
			if (!handle.IsValid())
			{
				return null;
			}
			return GetBufferResource(in handle.handle).graphicsResource;
		}

		internal global::UnityEngine.GraphicsBuffer GetBuffer(int index)
		{
			return GetBufferResource(index).graphicsResource;
		}

		internal global::UnityEngine.Rendering.RayTracingAccelerationStructure GetRayTracingAccelerationStructure(in global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureHandle handle)
		{
			if (!handle.IsValid())
			{
				return null;
			}
			return GetRayTracingAccelerationStructureResource(in handle.handle).graphicsResource;
		}

		internal int GetSharedResourceCount(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType type)
		{
			return m_RenderGraphResources[(int)type].sharedResourcesCount;
		}

		private RenderGraphResourceRegistry()
		{
		}

		internal RenderGraphResourceRegistry(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugParams renderGraphDebug, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphLogger frameInformationLogger)
		{
			m_RenderGraphDebug = renderGraphDebug;
			m_FrameInformationLogger = frameInformationLogger;
			for (int i = 0; i < 3; i++)
			{
				m_RenderGraphResources[i] = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.RenderGraphResourcesData();
			}
			m_RenderGraphResources[0].createResourceCallback = CreateTextureCallback;
			m_RenderGraphResources[0].releaseResourceCallback = ReleaseTextureCallback;
			m_RenderGraphResources[0].pool = new global::UnityEngine.Rendering.RenderGraphModule.TexturePool();
			m_RenderGraphResources[1].pool = new global::UnityEngine.Rendering.RenderGraphModule.BufferPool();
			m_RenderGraphResources[2].pool = null;
		}

		internal void BeginRenderGraph(int executionCount)
		{
			m_ExecutionCount = executionCount;
			global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle.NewFrame(executionCount);
			if (m_RenderGraphDebug.enableLogging)
			{
				m_ResourceLogger.Initialize("RenderGraph Resources");
			}
		}

		internal void BeginExecute(int currentFrameIndex)
		{
			m_CurrentFrameIndex = currentFrameIndex;
			ManageSharedRenderGraphResources();
			current = this;
		}

		internal void EndExecute()
		{
			current = null;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckHandleValidity(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckHandleValidity(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType type, int index)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource> resourceArray = m_RenderGraphResources[(int)type].resourceArray;
				if (index == 0)
				{
					throw new global::System.ArgumentException($"Trying to access resource of type {type} with an null resource index.");
				}
				if (index >= resourceArray.size)
				{
					throw new global::System.ArgumentException($"Trying to access resource of type {type} with an invalid resource index {index}");
				}
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle IncrementWriteCount(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			int version = (int)m_RenderGraphResources[res.iType].resourceArray[res.index].IncrementWriteCount();
			return new global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle(in res, version);
		}

		internal void IncrementReadCount(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			m_RenderGraphResources[res.iType].resourceArray[res.index].IncrementReadCount();
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle GetLatestVersionHandle(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			int writeCount = (int)m_RenderGraphResources[res.iType].resourceArray[res.index].writeCount;
			return new global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle(in res, writeCount);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle GetZeroVersionHandle(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			return new global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle(in res, 0);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource GetResourceLowLevel(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			return m_RenderGraphResources[res.iType].resourceArray[res.index];
		}

		internal string GetRenderGraphResourceName(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			return m_RenderGraphResources[res.iType].resourceArray[res.index].GetName();
		}

		internal string GetRenderGraphResourceName(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType type, int index)
		{
			return m_RenderGraphResources[(int)type].resourceArray[index].GetName();
		}

		internal bool IsRenderGraphResourceImported(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			return m_RenderGraphResources[res.iType].resourceArray[res.index].imported;
		}

		internal bool IsRenderGraphResourceShared(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType type, int index)
		{
			return index <= m_RenderGraphResources[(int)type].sharedResourcesCount;
		}

		internal bool IsRenderGraphResourceShared(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			return IsRenderGraphResourceShared(res.type, res.index);
		}

		internal bool IsGraphicsResourceCreated(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			return m_RenderGraphResources[res.iType].resourceArray[res.index].IsCreated();
		}

		internal bool IsRendererListCreated(in global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle res)
		{
			switch (res.type)
			{
			case global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Renderers:
				return m_RendererListResources[res].rendererList.isValid;
			case global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Legacy:
				if (m_RendererListLegacyResources[res].isActive)
				{
					return m_RendererListLegacyResources[res].rendererList.isValid;
				}
				return false;
			default:
				return false;
			}
		}

		internal bool IsRenderGraphResourceImported(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType type, int index)
		{
			return m_RenderGraphResources[(int)type].resourceArray[index].imported;
		}

		internal int GetRenderGraphResourceTransientIndex(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			return m_RenderGraphResources[res.iType].resourceArray[res.index].transientPassIndex;
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ImportTexture(in global::UnityEngine.Rendering.RTHandle rt, bool isBuiltin = false)
		{
			global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams importParams = new global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams
			{
				clearOnFirstUse = false,
				discardOnLastUse = false,
				textureUVOrigin = global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft
			};
			return ImportTexture(in rt, in importParams, isBuiltin);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ImportTexture(in global::UnityEngine.Rendering.RTHandle rt, in global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams importParams, bool isBuiltin = false)
		{
			if (rt != null && !(rt.m_RT != null))
			{
				_ = rt.m_ExternalTexture != null;
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureResource outRes;
			int handle = m_RenderGraphResources[0].AddNewRenderGraphResource<global::UnityEngine.Rendering.RenderGraphModule.TextureResource>(out outRes);
			outRes.graphicsResource = rt;
			outRes.imported = true;
			global::UnityEngine.RenderTexture renderTexture = ((rt == null) ? null : ((rt.m_RT != null) ? rt.m_RT : (rt.m_ExternalTexture as global::UnityEngine.RenderTexture)));
			if ((bool)renderTexture)
			{
				outRes.desc = new global::UnityEngine.Rendering.RenderGraphModule.TextureDesc(renderTexture);
				outRes.validDesc = true;
			}
			outRes.desc.clearBuffer = importParams.clearOnFirstUse;
			outRes.desc.clearColor = importParams.clearColor;
			outRes.desc.discardBuffer = importParams.discardOnLastUse;
			outRes.textureUVOrigin = (global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection)importParams.textureUVOrigin;
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle result = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle(handle, shared: false, isBuiltin);
			_ = rt;
			return result;
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ImportTexture(in global::UnityEngine.Rendering.RTHandle rt, global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo info, in global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams importParams)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureResource outRes;
			int handle = m_RenderGraphResources[0].AddNewRenderGraphResource<global::UnityEngine.Rendering.RenderGraphModule.TextureResource>(out outRes);
			outRes.graphicsResource = rt;
			outRes.imported = true;
			outRes.desc = default(global::UnityEngine.Rendering.RenderGraphModule.TextureDesc);
			if (rt != null && rt.m_NameID != emptyId)
			{
				outRes.desc.format = info.format;
				outRes.desc.width = info.width;
				outRes.desc.height = info.height;
				outRes.desc.slices = info.volumeDepth;
				outRes.desc.msaaSamples = (global::UnityEngine.Rendering.MSAASamples)info.msaaSamples;
				outRes.desc.bindTextureMS = info.bindMS;
				outRes.desc.clearBuffer = importParams.clearOnFirstUse;
				outRes.desc.clearColor = importParams.clearColor;
				outRes.desc.discardBuffer = importParams.discardOnLastUse;
				outRes.textureUVOrigin = (global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection)importParams.textureUVOrigin;
				outRes.validDesc = false;
			}
			return new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle(handle);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateSharedTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc, bool explicitRelease)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.RenderGraphResourcesData renderGraphResourcesData = m_RenderGraphResources[0];
			int sharedResourcesCount = renderGraphResourcesData.sharedResourcesCount;
			global::UnityEngine.Rendering.RenderGraphModule.TextureResource outRes = null;
			int handle = -1;
			for (int i = 1; i < sharedResourcesCount + 1; i++)
			{
				if (!renderGraphResourcesData.resourceArray[i].shared)
				{
					outRes = (global::UnityEngine.Rendering.RenderGraphModule.TextureResource)renderGraphResourcesData.resourceArray[i];
					handle = i;
					break;
				}
			}
			if (outRes == null)
			{
				handle = m_RenderGraphResources[0].AddNewRenderGraphResource<global::UnityEngine.Rendering.RenderGraphModule.TextureResource>(out outRes, pooledResource: false);
				renderGraphResourcesData.sharedResourcesCount++;
			}
			outRes.imported = true;
			outRes.shared = true;
			outRes.sharedExplicitRelease = explicitRelease;
			outRes.desc = desc;
			outRes.validDesc = true;
			return new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle(handle, shared: true);
		}

		internal void RefreshSharedTextureDesc(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture, in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureResource textureResource = GetTextureResource(in texture.handle);
			textureResource.ReleaseGraphicsResource();
			textureResource.desc = desc;
		}

		internal void ReleaseSharedTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.RenderGraphResourcesData renderGraphResourcesData = m_RenderGraphResources[0];
			if (texture.handle.index == renderGraphResourcesData.sharedResourcesCount)
			{
				renderGraphResourcesData.sharedResourcesCount--;
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureResource textureResource = GetTextureResource(in texture.handle);
			textureResource.ReleaseGraphicsResource();
			textureResource.Reset();
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ImportBackbuffer(global::UnityEngine.Rendering.RenderTargetIdentifier rt, in global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo info, in global::UnityEngine.Rendering.RenderGraphModule.ImportResourceParams importParams)
		{
			if (m_CurrentBackbuffer != null)
			{
				m_CurrentBackbuffer.SetTexture(rt);
			}
			else
			{
				m_CurrentBackbuffer = global::UnityEngine.Rendering.RTHandles.Alloc(rt, "Backbuffer");
			}
			global::UnityEngine.Rendering.RenderGraphModule.TextureResource outRes;
			int handle = m_RenderGraphResources[0].AddNewRenderGraphResource<global::UnityEngine.Rendering.RenderGraphModule.TextureResource>(out outRes);
			outRes.graphicsResource = m_CurrentBackbuffer;
			outRes.imported = true;
			outRes.desc = default(global::UnityEngine.Rendering.RenderGraphModule.TextureDesc);
			outRes.desc.width = info.width;
			outRes.desc.height = info.height;
			outRes.desc.slices = info.volumeDepth;
			outRes.desc.msaaSamples = (global::UnityEngine.Rendering.MSAASamples)info.msaaSamples;
			outRes.desc.bindTextureMS = info.bindMS;
			outRes.desc.format = info.format;
			outRes.desc.clearBuffer = importParams.clearOnFirstUse;
			outRes.desc.clearColor = importParams.clearColor;
			outRes.desc.discardBuffer = importParams.discardOnLastUse;
			outRes.textureUVOrigin = (global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection)importParams.textureUVOrigin;
			outRes.validDesc = false;
			return new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle(handle);
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void ValidateRenderTarget(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				GetRenderTargetInfo(in res, out var _);
			}
		}

		internal void GetRenderTargetInfo(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res, out global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo outInfo)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureResource textureResource = GetTextureResource(in res);
			if (textureResource.imported)
			{
				global::UnityEngine.Rendering.RTHandle graphicsResource = textureResource.graphicsResource;
				if (graphicsResource == null)
				{
					outInfo = default(global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo);
				}
				else if (graphicsResource.m_RT != null)
				{
					outInfo = default(global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo);
					outInfo.width = graphicsResource.m_RT.width;
					outInfo.height = graphicsResource.m_RT.height;
					outInfo.volumeDepth = graphicsResource.m_RT.volumeDepth;
					outInfo.format = GetFormat(graphicsResource.m_RT.graphicsFormat, graphicsResource.m_RT.depthStencilFormat);
					outInfo.msaaSamples = graphicsResource.m_RT.antiAliasing;
					outInfo.bindMS = graphicsResource.m_RT.bindTextureMS;
				}
				else if (graphicsResource.m_ExternalTexture != null)
				{
					outInfo = default(global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo);
					outInfo.width = graphicsResource.m_ExternalTexture.width;
					outInfo.height = graphicsResource.m_ExternalTexture.height;
					outInfo.volumeDepth = 1;
					if (graphicsResource.m_ExternalTexture is global::UnityEngine.RenderTexture)
					{
						global::UnityEngine.RenderTexture renderTexture = (global::UnityEngine.RenderTexture)graphicsResource.m_ExternalTexture;
						outInfo.format = GetFormat(renderTexture.graphicsFormat, renderTexture.depthStencilFormat);
						outInfo.msaaSamples = renderTexture.antiAliasing;
					}
					else
					{
						outInfo.format = graphicsResource.m_ExternalTexture.graphicsFormat;
						outInfo.msaaSamples = 1;
					}
					outInfo.bindMS = false;
				}
				else
				{
					if (!(graphicsResource.m_NameID != emptyId))
					{
						throw new global::System.Exception("Invalid imported texture. The RTHandle provided is invalid.");
					}
					ref readonly global::UnityEngine.Rendering.RenderGraphModule.TextureDesc textureResourceDesc = ref GetTextureResourceDesc(in res, noThrowOnInvalidDesc: true);
					outInfo.width = textureResourceDesc.width;
					outInfo.height = textureResourceDesc.height;
					outInfo.volumeDepth = textureResourceDesc.slices;
					outInfo.msaaSamples = (int)textureResourceDesc.msaaSamples;
					outInfo.format = textureResourceDesc.format;
					outInfo.bindMS = textureResourceDesc.bindTextureMS;
				}
			}
			else
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.TextureDesc textureResourceDesc2 = ref GetTextureResourceDesc(in res);
				global::UnityEngine.Vector2Int vector2Int = textureResourceDesc2.CalculateFinalDimensions();
				outInfo = default(global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo);
				outInfo.width = vector2Int.x;
				outInfo.height = vector2Int.y;
				outInfo.volumeDepth = textureResourceDesc2.slices;
				outInfo.msaaSamples = (int)textureResourceDesc2.msaaSamples;
				outInfo.bindMS = textureResourceDesc2.bindTextureMS;
				outInfo.format = textureResourceDesc2.format;
			}
		}

		internal global::UnityEngine.Experimental.Rendering.GraphicsFormat GetFormat(global::UnityEngine.Experimental.Rendering.GraphicsFormat color, global::UnityEngine.Experimental.Rendering.GraphicsFormat depthStencil)
		{
			if (depthStencil == global::UnityEngine.Experimental.Rendering.GraphicsFormat.None)
			{
				return color;
			}
			return depthStencil;
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		internal void ValidateFormat(global::UnityEngine.Experimental.Rendering.GraphicsFormat color, global::UnityEngine.Experimental.Rendering.GraphicsFormat depthStencil)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks && color != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None && depthStencil != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None)
			{
				throw new global::System.Exception("Invalid imported texture. Both a color and a depthStencil format are provided. The texture needs to either have a color format or a depth stencil format.");
			}
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc, int transientPassIndex = -1)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureResource outRes;
			int handle = m_RenderGraphResources[0].AddNewRenderGraphResource<global::UnityEngine.Rendering.RenderGraphModule.TextureResource>(out outRes);
			outRes.desc = desc;
			outRes.validDesc = true;
			outRes.transientPassIndex = transientPassIndex;
			outRes.requestFallBack = desc.fallBackToBlackTexture;
			outRes.textureUVOrigin = global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown;
			return new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle(handle);
		}

		internal void SetTextureAsMemoryLess(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle)
		{
			ref global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = ref GetTextureResource(in handle).desc;
			desc.memoryless = ((!global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthStencilFormat(desc.format)) ? global::UnityEngine.RenderTextureMemoryless.Color : global::UnityEngine.RenderTextureMemoryless.Depth);
			if (desc.msaaSamples != global::UnityEngine.Rendering.MSAASamples.None)
			{
				desc.memoryless |= global::UnityEngine.RenderTextureMemoryless.MSAA;
			}
		}

		internal int GetResourceCount(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType type)
		{
			return m_RenderGraphResources[(int)type].resourceArray.size;
		}

		internal int GetTextureResourceCount()
		{
			return GetResourceCount(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Texture);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureResource GetTextureResource(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle)
		{
			return m_RenderGraphResources[0].resourceArray[handle.index] as global::UnityEngine.Rendering.RenderGraphModule.TextureResource;
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.TextureResource GetTextureResource(int index)
		{
			return m_RenderGraphResources[0].resourceArray[index] as global::UnityEngine.Rendering.RenderGraphModule.TextureResource;
		}

		internal ref readonly global::UnityEngine.Rendering.RenderGraphModule.TextureDesc GetTextureResourceDesc(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle, bool noThrowOnInvalidDesc = false)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureResource obj = m_RenderGraphResources[0].resourceArray[handle.index] as global::UnityEngine.Rendering.RenderGraphModule.TextureResource;
			if (!obj.validDesc && !noThrowOnInvalidDesc)
			{
				throw new global::System.ArgumentException("The passed in texture handle does not have a valid descriptor. (This is most commonly cause by the handle referencing a built-in texture such as the system back buffer.)", "handle");
			}
			return ref obj.desc;
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle CreateRendererList(in global::UnityEngine.Rendering.RendererUtils.RendererListDesc desc)
		{
			return new global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle(m_RendererListResources.Add(new global::UnityEngine.Rendering.RenderGraphModule.RendererListResource(global::UnityEngine.Rendering.RendererUtils.RendererListDesc.ConvertToParameters(in desc))));
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle CreateRendererList(in global::UnityEngine.Rendering.RendererListParams desc)
		{
			return new global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle(m_RendererListResources.Add(new global::UnityEngine.Rendering.RenderGraphModule.RendererListResource(in desc)));
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle CreateShadowRendererList(global::UnityEngine.Rendering.ScriptableRenderContext context, ref global::UnityEngine.Rendering.ShadowDrawingSettings shadowDrawinSettings)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource value = new global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource
			{
				rendererList = context.CreateShadowRendererList(ref shadowDrawinSettings)
			};
			return new global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle(m_RendererListLegacyResources.Add(in value), global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Legacy);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle CreateGizmoRendererList(global::UnityEngine.Rendering.ScriptableRenderContext context, in global::UnityEngine.Camera camera, in global::UnityEngine.Rendering.GizmoSubset gizmoSubset)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource value = new global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource
			{
				rendererList = context.CreateGizmoRendererList(camera, gizmoSubset)
			};
			return new global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle(m_RendererListLegacyResources.Add(in value), global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Legacy);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle CreateUIOverlayRendererList(global::UnityEngine.Rendering.ScriptableRenderContext context, in global::UnityEngine.Camera camera, in global::UnityEngine.Rendering.UISubset uiSubset)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource value = new global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource
			{
				rendererList = context.CreateUIOverlayRendererList(camera, uiSubset)
			};
			return new global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle(m_RendererListLegacyResources.Add(in value), global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Legacy);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle CreateWireOverlayRendererList(global::UnityEngine.Rendering.ScriptableRenderContext context, in global::UnityEngine.Camera camera)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource value = new global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource
			{
				rendererList = context.CreateWireOverlayRendererList(camera)
			};
			return new global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle(m_RendererListLegacyResources.Add(in value), global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Legacy);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle CreateSkyboxRendererList(global::UnityEngine.Rendering.ScriptableRenderContext context, in global::UnityEngine.Camera camera)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource value = new global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource
			{
				rendererList = context.CreateSkyboxRendererList(camera)
			};
			return new global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle(m_RendererListLegacyResources.Add(in value), global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Legacy);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle CreateSkyboxRendererList(global::UnityEngine.Rendering.ScriptableRenderContext context, in global::UnityEngine.Camera camera, global::UnityEngine.Matrix4x4 projectionMatrix, global::UnityEngine.Matrix4x4 viewMatrix)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource value = new global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource
			{
				rendererList = context.CreateSkyboxRendererList(camera, projectionMatrix, viewMatrix)
			};
			return new global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle(m_RendererListLegacyResources.Add(in value), global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Legacy);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle CreateSkyboxRendererList(global::UnityEngine.Rendering.ScriptableRenderContext context, in global::UnityEngine.Camera camera, global::UnityEngine.Matrix4x4 projectionMatrixL, global::UnityEngine.Matrix4x4 viewMatrixL, global::UnityEngine.Matrix4x4 projectionMatrixR, global::UnityEngine.Matrix4x4 viewMatrixR)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource value = new global::UnityEngine.Rendering.RenderGraphModule.RendererListLegacyResource
			{
				rendererList = context.CreateSkyboxRendererList(camera, projectionMatrixL, viewMatrixL, projectionMatrixR, viewMatrixR)
			};
			return new global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle(m_RendererListLegacyResources.Add(in value), global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Legacy);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.BufferHandle ImportBuffer(global::UnityEngine.GraphicsBuffer graphicsBuffer)
		{
			global::UnityEngine.Rendering.RenderGraphModule.BufferResource outRes;
			int handle = m_RenderGraphResources[1].AddNewRenderGraphResource<global::UnityEngine.Rendering.RenderGraphModule.BufferResource>(out outRes);
			outRes.graphicsResource = graphicsBuffer;
			outRes.imported = true;
			outRes.validDesc = false;
			return new global::UnityEngine.Rendering.RenderGraphModule.BufferHandle(handle);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.BufferHandle CreateBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc, int transientPassIndex = -1)
		{
			global::UnityEngine.Rendering.RenderGraphModule.BufferResource outRes;
			int handle = m_RenderGraphResources[1].AddNewRenderGraphResource<global::UnityEngine.Rendering.RenderGraphModule.BufferResource>(out outRes);
			outRes.desc = desc;
			outRes.validDesc = true;
			outRes.transientPassIndex = transientPassIndex;
			return new global::UnityEngine.Rendering.RenderGraphModule.BufferHandle(handle);
		}

		internal ref readonly global::UnityEngine.Rendering.RenderGraphModule.BufferDesc GetBufferResourceDesc(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle, bool noThrowOnInvalidDesc = false)
		{
			global::UnityEngine.Rendering.RenderGraphModule.BufferResource obj = m_RenderGraphResources[1].resourceArray[handle.index] as global::UnityEngine.Rendering.RenderGraphModule.BufferResource;
			if (!obj.validDesc && !noThrowOnInvalidDesc)
			{
				throw new global::System.ArgumentException("The passed in buffer handle does not have a valid descriptor. (This is most commonly cause by importing the buffer.)", "handle");
			}
			return ref obj.desc;
		}

		internal int GetBufferResourceCount()
		{
			return GetResourceCount(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Buffer);
		}

		private global::UnityEngine.Rendering.RenderGraphModule.BufferResource GetBufferResource(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle)
		{
			return m_RenderGraphResources[1].resourceArray[handle.index] as global::UnityEngine.Rendering.RenderGraphModule.BufferResource;
		}

		private global::UnityEngine.Rendering.RenderGraphModule.BufferResource GetBufferResource(int index)
		{
			return m_RenderGraphResources[1].resourceArray[index] as global::UnityEngine.Rendering.RenderGraphModule.BufferResource;
		}

		private global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureResource GetRayTracingAccelerationStructureResource(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle)
		{
			return m_RenderGraphResources[2].resourceArray[handle.index] as global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureResource;
		}

		internal int GetRayTracingAccelerationStructureResourceCount()
		{
			return GetResourceCount(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.AccelerationStructure);
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureHandle ImportRayTracingAccelerationStructure(in global::UnityEngine.Rendering.RayTracingAccelerationStructure accelStruct, string name)
		{
			global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureResource outRes;
			int handle = m_RenderGraphResources[2].AddNewRenderGraphResource<global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureResource>(out outRes, pooledResource: false);
			outRes.graphicsResource = accelStruct;
			outRes.imported = true;
			outRes.desc.name = name;
			return new global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureHandle(handle);
		}

		internal void UpdateSharedResourceLastFrameIndex(int type, int index)
		{
			m_RenderGraphResources[type].resourceArray[index].sharedResourceLastFrameUsed = m_ExecutionCount;
		}

		internal void UpdateSharedResourceLastFrameIndex(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle)
		{
			UpdateSharedResourceLastFrameIndex((int)handle.type, handle.index);
		}

		private void ManageSharedRenderGraphResources()
		{
			for (int i = 0; i < 3; i++)
			{
				global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry.RenderGraphResourcesData renderGraphResourcesData = m_RenderGraphResources[i];
				for (int j = 1; j < renderGraphResourcesData.sharedResourcesCount + 1; j++)
				{
					global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource renderGraphResource = m_RenderGraphResources[i].resourceArray[j];
					bool flag = renderGraphResource.IsCreated();
					if (renderGraphResource.sharedResourceLastFrameUsed == m_ExecutionCount && !flag)
					{
						renderGraphResource.CreateGraphicsResource();
					}
					else if (flag && !renderGraphResource.sharedExplicitRelease && renderGraphResource.sharedResourceLastFrameUsed + 30 < m_ExecutionCount)
					{
						renderGraphResource.ReleaseGraphicsResource();
					}
				}
			}
		}

		internal bool CreatePooledResource(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, int type, int index)
		{
			bool? flag = false;
			global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource renderGraphResource = m_RenderGraphResources[type].resourceArray[index];
			if (!renderGraphResource.imported)
			{
				renderGraphResource.CreatePooledGraphicsResource(rgContext.forceResourceCreation);
				if (m_RenderGraphDebug.enableLogging)
				{
					renderGraphResource.LogCreation(m_FrameInformationLogger);
				}
				flag = m_RenderGraphResources[type].createResourceCallback?.Invoke(rgContext, renderGraphResource);
			}
			return flag == true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal bool CreatePooledResource(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle)
		{
			return CreatePooledResource(rgContext, handle.iType, handle.index);
		}

		private bool CreateTextureCallback(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource res)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureResource textureResource = res as global::UnityEngine.Rendering.RenderGraphModule.TextureResource;
			global::UnityEngine.Rendering.RenderGraphModule.FastMemoryDesc fastMemoryDesc = textureResource.desc.fastMemoryDesc;
			if (fastMemoryDesc.inFastMemory)
			{
				textureResource.graphicsResource.SwitchToFastMemory(rgContext.cmd, fastMemoryDesc.residencyFraction, fastMemoryDesc.flags);
			}
			bool result = false;
			if ((forceManualClearOfResource && textureResource.desc.clearBuffer) || m_RenderGraphDebug.clearRenderTargetsAtCreation)
			{
				ClearTexture(rgContext, textureResource);
				result = true;
			}
			return result;
		}

		internal bool ClearResource(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, int type, int index)
		{
			bool result = false;
			if (m_RenderGraphResources[type].resourceArray[index] is global::UnityEngine.Rendering.RenderGraphModule.TextureResource resource)
			{
				ClearTexture(rgContext, resource);
				result = true;
			}
			return result;
		}

		private void ClearTexture(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, global::UnityEngine.Rendering.RenderGraphModule.TextureResource resource)
		{
			if (resource != null)
			{
				bool num = m_RenderGraphDebug.clearRenderTargetsAtCreation && !resource.desc.clearBuffer;
				global::UnityEngine.Rendering.ClearFlag clearFlag = ((!global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthStencilFormat(resource.desc.format)) ? global::UnityEngine.Rendering.ClearFlag.Color : global::UnityEngine.Rendering.ClearFlag.DepthStencil);
				global::UnityEngine.Color clearColor = (num ? global::UnityEngine.Color.magenta : resource.desc.clearColor);
				global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(rgContext.cmd, resource.graphicsResource, clearFlag, clearColor);
			}
		}

		internal void ReleasePooledResource(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, int type, int index)
		{
			global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource renderGraphResource = m_RenderGraphResources[type].resourceArray[index];
			if (!renderGraphResource.imported)
			{
				m_RenderGraphResources[type].releaseResourceCallback?.Invoke(rgContext, renderGraphResource);
				if (m_RenderGraphDebug.enableLogging)
				{
					renderGraphResource.LogRelease(m_FrameInformationLogger);
				}
				renderGraphResource.ReleasePooledGraphicsResource(m_CurrentFrameIndex);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void ReleasePooledResource(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle)
		{
			ReleasePooledResource(rgContext, handle.iType, handle.index);
		}

		private void ReleaseTextureCallback(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource res)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureResource textureResource = res as global::UnityEngine.Rendering.RenderGraphModule.TextureResource;
			if (m_RenderGraphDebug.clearRenderTargetsAtRelease)
			{
				global::UnityEngine.Rendering.ClearFlag clearFlag = ((!global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthStencilFormat(textureResource.desc.format)) ? global::UnityEngine.Rendering.ClearFlag.Color : global::UnityEngine.Rendering.ClearFlag.DepthStencil);
				global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(rgContext.cmd, textureResource.graphicsResource, clearFlag, global::UnityEngine.Color.magenta);
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void ValidateTextureDesc(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				if (desc.format == global::UnityEngine.Experimental.Rendering.GraphicsFormat.None)
				{
					throw new global::System.ArgumentException("Texture was created with with no format. The texture needs to either have a color format or a depth stencil format.");
				}
				if (desc.dimension == global::UnityEngine.Rendering.TextureDimension.None || desc.dimension == global::UnityEngine.Rendering.TextureDimension.Any)
				{
					throw new global::System.ArgumentException("Texture was created with an invalid texture dimension.");
				}
				if (desc.slices == 0)
				{
					throw new global::System.ArgumentException("Texture was created with a slices parameter value of zero.");
				}
				if (desc.slices > 1 && (desc.dimension == global::UnityEngine.Rendering.TextureDimension.Tex2D || desc.dimension == global::UnityEngine.Rendering.TextureDimension.Cube) && global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3)
				{
					throw new global::System.ArgumentException("Non-array texture was created with a slices parameter larger than one.");
				}
				if (desc.msaaSamples <= global::UnityEngine.Rendering.MSAASamples.None && desc.bindTextureMS)
				{
					throw new global::System.ArgumentException("A single sample texture was created with bindTextureMS.");
				}
				if (desc.sizeMode == global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit && (desc.width == 0 || desc.height == 0))
				{
					throw new global::System.ArgumentException("Texture using Explicit size mode was create with either width or height at zero.");
				}
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void ValidateRendererListDesc(in global::UnityEngine.Rendering.RendererUtils.RendererListDesc desc)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				if (!desc.IsValid())
				{
					throw new global::System.ArgumentException("Renderer List descriptor is not valid.");
				}
				if (desc.renderQueueRange.lowerBound == 0 && desc.renderQueueRange.upperBound == 0)
				{
					throw new global::System.ArgumentException("Renderer List creation descriptor must have a valid RenderQueueRange.");
				}
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void ValidateBufferDesc(in global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				if (desc.stride % 4 != 0)
				{
					throw new global::System.ArgumentException("Invalid Graphics Buffer creation descriptor: Graphics Buffer stride must be at least 4.");
				}
				if (desc.count == 0)
				{
					throw new global::System.ArgumentException("Invalid Graphics Buffer creation descriptor: Graphics Buffer count  must be non zero.");
				}
			}
		}

		internal void CreateRendererLists(global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle> rendererLists, global::UnityEngine.Rendering.ScriptableRenderContext context, bool manualDispatch = false)
		{
			m_ActiveRendererLists.Clear();
			foreach (global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList in rendererLists)
			{
				switch (rendererList.type)
				{
				case global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Renderers:
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.RendererListResource reference = ref m_RendererListResources[rendererList];
					reference.rendererList = context.CreateRendererList(ref reference.desc);
					m_ActiveRendererLists.Add(reference.rendererList);
					break;
				}
				case global::UnityEngine.Rendering.RenderGraphModule.RendererListHandleType.Legacy:
					m_RendererListLegacyResources[rendererList].isActive = true;
					break;
				}
			}
			if (manualDispatch)
			{
				context.PrepareRendererListsAsync(m_ActiveRendererLists);
			}
		}

		internal void Clear(bool onException)
		{
			LogResources();
			for (int i = 0; i < 3; i++)
			{
				m_RenderGraphResources[i].Clear(onException, m_CurrentFrameIndex);
			}
			m_RendererListResources.Clear();
			m_RendererListLegacyResources.Clear();
			m_ActiveRendererLists.Clear();
		}

		internal void PurgeUnusedGraphicsResources()
		{
			for (int i = 0; i < 3; i++)
			{
				m_RenderGraphResources[i].PurgeUnusedGraphicsResources(m_CurrentFrameIndex);
			}
		}

		internal void Cleanup()
		{
			for (int i = 0; i < 3; i++)
			{
				m_RenderGraphResources[i].Cleanup();
			}
			global::UnityEngine.Rendering.RTHandles.Release(m_CurrentBackbuffer);
		}

		private void LogResources()
		{
			if (!m_RenderGraphDebug.enableLogging)
			{
				return;
			}
			m_ResourceLogger.LogLine("==== Render Graph Resource Log ====\n");
			for (int i = 0; i < 3; i++)
			{
				if (m_RenderGraphResources[i].pool != null)
				{
					m_RenderGraphResources[i].pool.LogResources(m_ResourceLogger);
					m_ResourceLogger.LogLine("");
				}
			}
		}

		internal void FlushLogs()
		{
			m_ResourceLogger.FlushLogs();
		}
	}
}
