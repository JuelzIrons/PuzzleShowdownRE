namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	[global::System.Obsolete("RenderGraphBuilder is deprecated, use IComputeRenderGraphBuilder/IRasterRenderGraphBuilder/IUnsafeRenderGraphBuilder instead.")]
	public struct RenderGraphBuilder : global::System.IDisposable
	{
		private global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass m_RenderPass;

		private global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry m_Resources;

		private global::UnityEngine.Rendering.RenderGraphModule.RenderGraph m_RenderGraph;

		private bool m_Disposed;

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle UseColorBuffer(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input, int index)
		{
			m_Resources.IncrementWriteCount(in input.handle);
			m_RenderPass.SetColorBuffer(in input, index);
			return input;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle UseDepthBuffer(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input, global::UnityEngine.Rendering.RenderGraphModule.DepthAccess flags)
		{
			if ((flags & global::UnityEngine.Rendering.RenderGraphModule.DepthAccess.Write) != 0)
			{
				m_Resources.IncrementWriteCount(in input.handle);
			}
			if ((flags & global::UnityEngine.Rendering.RenderGraphModule.DepthAccess.Read) != 0 && !m_Resources.IsRenderGraphResourceImported(in input.handle) && m_Resources.TextureNeedsFallback(in input))
			{
				WriteTexture(in input);
			}
			m_RenderPass.SetDepthBuffer(in input, flags);
			return input;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ReadTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input)
		{
			if (!m_Resources.IsRenderGraphResourceImported(in input.handle) && m_Resources.TextureNeedsFallback(in input))
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureResource textureResource = m_Resources.GetTextureResource(in input.handle);
				textureResource.desc.clearBuffer = true;
				textureResource.desc.clearColor = global::UnityEngine.Color.black;
				if (m_RenderGraph.GetImportedFallback(textureResource.desc, out var fallback))
				{
					return fallback;
				}
				WriteTexture(in input);
			}
			m_RenderPass.AddResourceRead(in input.handle);
			return input;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle WriteTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input)
		{
			m_Resources.IncrementWriteCount(in input.handle);
			m_RenderPass.AddResourceWrite(in input.handle);
			return input;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle ReadWriteTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input)
		{
			m_Resources.IncrementWriteCount(in input.handle);
			m_RenderPass.AddResourceWrite(in input.handle);
			m_RenderPass.AddResourceRead(in input.handle);
			return input;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateTransientTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle result = m_Resources.CreateTexture(in desc, m_RenderPass.index);
			m_RenderPass.AddTransientResource(in result.handle);
			return result;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateTransientTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture)
		{
			ref readonly global::UnityEngine.Rendering.RenderGraphModule.TextureDesc textureResourceDesc = ref m_Resources.GetTextureResourceDesc(in texture.handle);
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle result = m_Resources.CreateTexture(in textureResourceDesc, m_RenderPass.index);
			m_RenderPass.AddTransientResource(in result.handle);
			return result;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureHandle WriteRayTracingAccelerationStructure(in global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureHandle input)
		{
			m_Resources.IncrementWriteCount(in input.handle);
			m_RenderPass.AddResourceWrite(in input.handle);
			return input;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureHandle ReadRayTracingAccelerationStructure(in global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureHandle input)
		{
			m_RenderPass.AddResourceRead(in input.handle);
			return input;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle UseRendererList(in global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle input)
		{
			if (input.IsValid())
			{
				m_RenderPass.UseRendererList(in input);
			}
			return input;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle ReadBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferHandle input)
		{
			m_RenderPass.AddResourceRead(in input.handle);
			return input;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle WriteBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferHandle input)
		{
			m_RenderPass.AddResourceWrite(in input.handle);
			m_Resources.IncrementWriteCount(in input.handle);
			return input;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle CreateTransientBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc)
		{
			global::UnityEngine.Rendering.RenderGraphModule.BufferHandle result = m_Resources.CreateBuffer(in desc, m_RenderPass.index);
			m_RenderPass.AddTransientResource(in result.handle);
			return result;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle CreateTransientBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferHandle graphicsbuffer)
		{
			ref readonly global::UnityEngine.Rendering.RenderGraphModule.BufferDesc bufferResourceDesc = ref m_Resources.GetBufferResourceDesc(in graphicsbuffer.handle);
			global::UnityEngine.Rendering.RenderGraphModule.BufferHandle result = m_Resources.CreateBuffer(in bufferResourceDesc, m_RenderPass.index);
			m_RenderPass.AddTransientResource(in result.handle);
			return result;
		}

		public void SetRenderFunc<PassData>(global::UnityEngine.Rendering.RenderGraphModule.BaseRenderFunc<PassData, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphContext> renderFunc) where PassData : class, new()
		{
			((global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass<PassData>)m_RenderPass).renderFunc = renderFunc;
		}

		public void EnableAsyncCompute(bool value)
		{
			m_RenderPass.EnableAsyncCompute(value);
		}

		public void AllowPassCulling(bool value)
		{
			m_RenderPass.AllowPassCulling(value);
		}

		public void EnableFoveatedRasterization(bool value)
		{
			m_RenderPass.EnableFoveatedRasterization(value);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		public void AllowRendererListCulling(bool value)
		{
			m_RenderPass.AllowRendererListCulling(value);
		}

		public global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle DependsOn(in global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle input)
		{
			m_RenderPass.UseRendererList(in input);
			return input;
		}

		internal RenderGraphBuilder(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass renderPass, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			m_RenderPass = renderPass;
			m_Resources = resources;
			m_RenderGraph = renderGraph;
			m_Disposed = false;
		}

		private void Dispose(bool disposing)
		{
			if (!m_Disposed)
			{
				m_RenderGraph.RenderGraphState = global::UnityEngine.Rendering.RenderGraphModule.RenderGraphState.RecordingGraph;
				m_RenderGraph.OnPassAdded(m_RenderPass);
				m_Disposed = true;
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckResource(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res, bool checkTransientReadWrite = true)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				if (!res.IsValid())
				{
					throw new global::System.ArgumentException("Trying to use an invalid resource (pass " + m_RenderPass.name + ").");
				}
				int renderGraphResourceTransientIndex = m_Resources.GetRenderGraphResourceTransientIndex(in res);
				if (renderGraphResourceTransientIndex == m_RenderPass.index && checkTransientReadWrite)
				{
					global::UnityEngine.Debug.LogError("Trying to read or write a transient resource at pass " + m_RenderPass.name + ".Transient resource are always assumed to be both read and written.");
				}
				if (renderGraphResourceTransientIndex != -1 && renderGraphResourceTransientIndex != m_RenderPass.index)
				{
					throw new global::System.ArgumentException($"Trying to use a transient texture (pass index {renderGraphResourceTransientIndex}) in a different pass (pass index {m_RenderPass.index}).");
				}
			}
		}

		internal void GenerateDebugData(bool value)
		{
			m_RenderPass.GenerateDebugData(value);
		}
	}
}
