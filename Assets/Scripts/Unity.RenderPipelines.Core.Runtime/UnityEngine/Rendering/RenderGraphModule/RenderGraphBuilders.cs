namespace UnityEngine.Rendering.RenderGraphModule
{
	internal class RenderGraphBuilders : global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder, global::System.IDisposable, global::UnityEngine.Rendering.RenderGraphModule.IComputeRenderGraphBuilder, global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder, global::UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder, global::UnityEngine.Rendering.RenderGraphModule.IUnsafeRenderGraphBuilder
	{
		private global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass m_RenderPass;

		private global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry m_Resources;

		private global::UnityEngine.Rendering.RenderGraphModule.RenderGraph m_RenderGraph;

		private bool m_Disposed;

		public RenderGraphBuilders()
		{
			m_RenderPass = null;
			m_Resources = null;
			m_RenderGraph = null;
			m_Disposed = true;
		}

		public void Setup(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass renderPass, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph)
		{
			m_RenderPass = renderPass;
			m_Resources = resources;
			m_RenderGraph = renderGraph;
			m_Disposed = false;
			renderPass.useAllGlobalTextures = false;
			if (renderPass.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Raster)
			{
				global::UnityEngine.Rendering.CommandBuffer.ThrowOnSetRenderTarget = true;
			}
		}

		public void EnableAsyncCompute(bool value)
		{
			m_RenderPass.EnableAsyncCompute(value);
		}

		public void AllowPassCulling(bool value)
		{
			if (!value || !m_RenderPass.allowGlobalState)
			{
				m_RenderPass.AllowPassCulling(value);
			}
		}

		public void AllowGlobalStateModification(bool value)
		{
			m_RenderPass.AllowGlobalState(value);
			if (value)
			{
				AllowPassCulling(value: false);
			}
		}

		public void EnableFoveatedRasterization(bool value)
		{
			m_RenderPass.EnableFoveatedRasterization(value);
		}

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle CreateTransientBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc)
		{
			global::UnityEngine.Rendering.RenderGraphModule.BufferHandle result = m_Resources.CreateBuffer(in desc, m_RenderPass.index);
			UseTransientResource(in result.handle);
			return result;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle CreateTransientBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferHandle computebuffer)
		{
			return CreateTransientBuffer(in m_Resources.GetBufferResourceDesc(in computebuffer.handle));
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateTransientTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle result = m_Resources.CreateTexture(in desc, m_RenderPass.index);
			UseTransientResource(in result.handle);
			return result;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle CreateTransientTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture)
		{
			return CreateTransientTexture(in m_Resources.GetTextureResourceDesc(in texture.handle));
		}

		public void GenerateDebugData(bool value)
		{
			m_RenderPass.GenerateDebugData(value);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (m_Disposed)
			{
				return;
			}
			try
			{
				if (!disposing)
				{
					return;
				}
				m_RenderGraph.RenderGraphState = global::UnityEngine.Rendering.RenderGraphModule.RenderGraphState.RecordingGraph;
				if (m_RenderPass.useAllGlobalTextures)
				{
					foreach (global::UnityEngine.Rendering.RenderGraphModule.TextureHandle item in m_RenderGraph.AllGlobals())
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input = item;
						if (input.IsValid())
						{
							UseTexture(in input, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read);
						}
					}
				}
				foreach (var setGlobals in m_RenderPass.setGlobalsList)
				{
					(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle, int) current = setGlobals;
					m_RenderGraph.SetGlobal(in current.Item1, current.Item2);
				}
				m_RenderGraph.OnPassAdded(m_RenderPass);
			}
			finally
			{
				if (m_RenderPass.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Raster)
				{
					global::UnityEngine.Rendering.CommandBuffer.ThrowOnSetRenderTarget = false;
				}
				m_RenderPass = null;
				m_Resources = null;
				m_RenderGraph = null;
				m_Disposed = true;
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckWriteTo(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				if (handle.IsVersioned)
				{
					string renderGraphResourceName = m_Resources.GetRenderGraphResourceName(in handle);
					throw new global::System.InvalidOperationException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName}' of type {handle.type} at index {handle.index} - " + "The pass writes to a versioned resource handle. You can only write to unversioned resource handles to avoid branches in the resource history.");
				}
				if (m_RenderPass.IsWritten(in handle))
				{
					string renderGraphResourceName2 = m_Resources.GetRenderGraphResourceName(in handle);
					throw new global::System.InvalidOperationException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName2}' of type {handle.type} at index {handle.index} - " + "The pass writes to a resource twice. You can only write the same resource once within a pass.");
				}
			}
		}

		private global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle UseTransientResource(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle inputHandle)
		{
			global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res = (inputHandle.IsVersioned ? inputHandle : m_Resources.GetLatestVersionHandle(in inputHandle));
			m_RenderPass.AddTransientResource(in res);
			return res;
		}

		private global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle UseResource(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle inputHandle, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags)
		{
			bool num = (flags & global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Discard) != 0;
			bool flag = (flags & global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read) != 0;
			bool flag2 = (flags & global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write) != 0;
			global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res = (inputHandle.IsVersioned ? inputHandle : m_Resources.GetLatestVersionHandle(in inputHandle));
			if (!num)
			{
				m_Resources.IncrementReadCount(in res);
				m_RenderPass.AddResourceRead(in res);
				if (!flag)
				{
					m_RenderPass.implicitReadsList.Add(res);
				}
			}
			else if (flag)
			{
				global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res2 = m_Resources.GetZeroVersionHandle(in res);
				m_Resources.IncrementReadCount(in res2);
				m_RenderPass.AddResourceRead(in res2);
			}
			if (flag2)
			{
				res = m_Resources.IncrementWriteCount(in inputHandle);
				m_RenderPass.AddResourceWrite(in res);
			}
			return res;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle UseBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferHandle input, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags)
		{
			UseResource(in input.handle, flags);
			return input;
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckNotUseFragment(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex)
		{
			if (!global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				return;
			}
			bool flag = m_RenderPass.depthAccess.textureHandle.IsValid() && m_RenderPass.depthAccess.textureHandle.handle.index == tex.handle.index;
			if (!flag)
			{
				for (int i = 0; i <= m_RenderPass.colorBufferMaxIndex; i++)
				{
					if (m_RenderPass.colorBufferAccess[i].textureHandle.IsValid() && m_RenderPass.colorBufferAccess[i].textureHandle.handle.index == tex.handle.index)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				string renderGraphResourceName = m_Resources.GetRenderGraphResourceName(in tex.handle);
				throw new global::System.ArgumentException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName}' of type {tex.handle.type} at index {tex.handle.index} - " + "UseTexture is called on a texture that is already used through SetRenderAttachment. Check your code and make sure the texture is only used once.");
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckTextureUVOriginIsValid(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle, global::UnityEngine.Rendering.RenderGraphModule.TextureResource texRes)
		{
			if (texRes.textureUVOrigin == global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.TopLeft)
			{
				string renderGraphResourceName = m_Resources.GetRenderGraphResourceName(in handle);
				throw new global::System.ArgumentException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName}' of type `{handle.type}` at index `{handle.index}` - " + global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.RenderGraphExceptionMessages.IncompatibleTextureUVOriginUseTexture(texRes.textureUVOrigin));
			}
		}

		public void UseTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags)
		{
			UseResource(in input.handle, flags);
			if ((flags & global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read) == global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read && m_RenderGraph.renderTextureUVOriginStrategy == global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy.PropagateAttachmentOrientation)
			{
				m_Resources.GetTextureResource(in input.handle).textureUVOrigin = global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.BottomLeft;
			}
		}

		public void UseGlobalTexture(int propertyId, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input = m_RenderGraph.GetGlobal(propertyId);
			if (input.IsValid())
			{
				UseTexture(in input, flags);
				return;
			}
			string renderGraphResourceName = m_Resources.GetRenderGraphResourceName(in input.handle);
			throw new global::System.ArgumentException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName}' of type {input.handle.type} at index {input.handle.index} - " + global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.RenderGraphExceptionMessages.NoGlobalTextureAtPropertyID(propertyId));
		}

		public void UseAllGlobalTextures(bool enable)
		{
			m_RenderPass.useAllGlobalTextures = enable;
		}

		public void SetGlobalTextureAfterPass(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input, int propertyId)
		{
			m_RenderPass.setGlobalsList.Add(global::System.ValueTuple.Create(input, propertyId));
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckUseFragment(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex, bool isDepth)
		{
			if (!global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < m_RenderPass.resourceReadLists[tex.handle.iType].Count; i++)
			{
				if (m_RenderPass.resourceReadLists[tex.handle.iType][i].index == tex.handle.index)
				{
					flag = true;
					break;
				}
			}
			for (int j = 0; j < m_RenderPass.resourceWriteLists[tex.handle.iType].Count; j++)
			{
				if (m_RenderPass.resourceWriteLists[tex.handle.iType][j].index == tex.handle.index)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				string renderGraphResourceName = m_Resources.GetRenderGraphResourceName(in tex.handle);
				throw new global::System.InvalidOperationException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName}' of type {tex.handle.type} at index {tex.handle.index} - " + "SetRenderAttachment is called on a texture that is already used through UseTexture/SetRenderAttachment. Check your code and make sure the texture is only used once.");
			}
			m_Resources.GetRenderTargetInfo(in tex.handle, out var outInfo);
			if (m_RenderGraph.nativeRenderPassesEnabled)
			{
				if (isDepth)
				{
					if (!global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthFormat(outInfo.format))
					{
						string renderGraphResourceName2 = m_Resources.GetRenderGraphResourceName(in tex.handle);
						throw new global::System.InvalidOperationException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName2}' of type {tex.handle.type} at index {tex.handle.index} - " + global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.RenderGraphExceptionMessages.UseDepthWithColorFormat(outInfo.format));
					}
				}
				else if (global::UnityEngine.Experimental.Rendering.GraphicsFormatUtility.IsDepthFormat(outInfo.format))
				{
					string renderGraphResourceName3 = m_Resources.GetRenderGraphResourceName(in tex.handle);
					throw new global::System.InvalidOperationException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName3}' of type {tex.handle.type} at index {tex.handle.index} - " + "SetRenderAttachment is called on a texture that has a depth format. Use a texture with a color format instead, or call SetRenderDepthAttachment.");
				}
				if (m_RenderGraph.renderTextureUVOriginStrategy == global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy.PropagateAttachmentOrientation)
				{
					global::UnityEngine.Rendering.RenderGraphModule.TextureResource textureResource = m_Resources.GetTextureResource(in tex.handle);
					global::UnityEngine.Rendering.RenderGraphModule.TextureResource textureResource2 = null;
					for (int k = 0; k < m_RenderPass.fragmentInputMaxIndex + 1; k++)
					{
						if (m_RenderPass.fragmentInputAccess[k].textureHandle.IsValid())
						{
							ref readonly global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle = ref m_RenderPass.fragmentInputAccess[k].textureHandle;
							textureResource2 = m_Resources.GetTextureResource(in textureHandle.handle);
							if (textureResource.textureUVOrigin != global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown && textureResource2.textureUVOrigin != global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown && textureResource.textureUVOrigin != textureResource2.textureUVOrigin)
							{
								string renderGraphResourceName4 = m_Resources.GetRenderGraphResourceName(in tex.handle);
								string renderGraphResourceName5 = m_Resources.GetRenderGraphResourceName(in textureHandle.handle);
								throw new global::System.InvalidOperationException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName4}' of type {tex.handle.type} at index {tex.handle.index} - " + global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.RenderGraphExceptionMessages.IncompatibleTextureUVOrigin(textureResource.textureUVOrigin, "input", renderGraphResourceName5, textureHandle.handle.type, textureHandle.handle.index, textureResource2.textureUVOrigin));
							}
						}
					}
					for (int l = 0; l < m_RenderPass.colorBufferMaxIndex + 1; l++)
					{
						if (m_RenderPass.colorBufferAccess[l].textureHandle.IsValid())
						{
							ref readonly global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle2 = ref m_RenderPass.colorBufferAccess[l].textureHandle;
							textureResource2 = m_Resources.GetTextureResource(in textureHandle2.handle);
							if (textureResource.textureUVOrigin != global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown && textureResource2.textureUVOrigin != global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown && textureResource.textureUVOrigin != textureResource2.textureUVOrigin)
							{
								string renderGraphResourceName6 = m_Resources.GetRenderGraphResourceName(in tex.handle);
								string renderGraphResourceName7 = m_Resources.GetRenderGraphResourceName(in textureHandle2.handle);
								throw new global::System.InvalidOperationException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName6}' of type {tex.handle.type} at index {tex.handle.index} - " + global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.RenderGraphExceptionMessages.IncompatibleTextureUVOrigin(textureResource.textureUVOrigin, "render", renderGraphResourceName7, textureHandle2.handle.type, textureHandle2.handle.index, textureResource2.textureUVOrigin));
							}
						}
					}
					if (!isDepth && m_RenderPass.depthAccess.textureHandle.IsValid())
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureHandle textureHandle3 = m_RenderPass.depthAccess.textureHandle;
						textureResource2 = m_Resources.GetTextureResource(in textureHandle3.handle);
						if (textureResource.textureUVOrigin != global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown && textureResource2.textureUVOrigin != global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown && textureResource.textureUVOrigin != textureResource2.textureUVOrigin)
						{
							string renderGraphResourceName8 = m_Resources.GetRenderGraphResourceName(in tex.handle);
							string renderGraphResourceName9 = m_Resources.GetRenderGraphResourceName(in textureHandle3.handle);
							throw new global::System.InvalidOperationException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName8}' of type {tex.handle.type} at index {tex.handle.index} - " + global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.RenderGraphExceptionMessages.IncompatibleTextureUVOrigin(textureResource.textureUVOrigin, "depth", renderGraphResourceName9, textureHandle3.handle.type, textureHandle3.handle.index, textureResource2.textureUVOrigin));
						}
					}
				}
			}
			foreach (var setGlobals in m_RenderPass.setGlobalsList)
			{
				if (setGlobals.Item1.handle.index == tex.handle.index)
				{
					string renderGraphResourceName10 = m_Resources.GetRenderGraphResourceName(in tex.handle);
					throw new global::System.InvalidOperationException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName10}' of type {tex.handle.type} at index {tex.handle.index} - " + "SetRenderAttachment is called on a texture that is currently bound to a global texture slot. Shaders might be using the texture using samplers. Make sure textures are not set as globals when using them as fragment attachments.");
				}
			}
		}

		public void SetRenderAttachment(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags, int mipLevel, int depthSlice)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle resource = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle(UseResource(in tex.handle, flags));
			m_RenderPass.SetColorBufferRaw(in resource, index, flags, mipLevel, depthSlice);
		}

		public void SetInputAttachment(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags, int mipLevel, int depthSlice)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle resource = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle(UseResource(in tex.handle, flags));
			m_RenderPass.SetFragmentInputRaw(in resource, index, flags, mipLevel, depthSlice);
		}

		public void SetRenderAttachmentDepth(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags, int mipLevel, int depthSlice)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle resource = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle(UseResource(in tex.handle, flags));
			m_RenderPass.SetDepthBufferRaw(in resource, flags, mipLevel, depthSlice);
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureHandle SetRandomAccessAttachment(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read)
		{
			global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource = UseResource(in input.handle, flags);
			m_RenderPass.SetRandomWriteResourceRaw(in resource, index, preserveCounterValue: false, flags);
			return input;
		}

		public void SetShadingRateImageAttachment(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex)
		{
			global::UnityEngine.Rendering.RenderGraphModule.TextureHandle shadingRateImage = new global::UnityEngine.Rendering.RenderGraphModule.TextureHandle(UseResource(in tex.handle, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read));
			m_RenderPass.SetShadingRateImageRaw(in shadingRateImage);
		}

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle UseBufferRandomAccess(global::UnityEngine.Rendering.RenderGraphModule.BufferHandle input, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read)
		{
			global::UnityEngine.Rendering.RenderGraphModule.BufferHandle bufferHandle = UseBuffer(in input, flags);
			m_RenderPass.SetRandomWriteResourceRaw(in bufferHandle.handle, index, preserveCounterValue: true, flags);
			return input;
		}

		public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle UseBufferRandomAccess(global::UnityEngine.Rendering.RenderGraphModule.BufferHandle input, int index, bool preserveCounterValue, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags = global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read)
		{
			global::UnityEngine.Rendering.RenderGraphModule.BufferHandle bufferHandle = UseBuffer(in input, flags);
			m_RenderPass.SetRandomWriteResourceRaw(in bufferHandle.handle, index, preserveCounterValue, flags);
			return input;
		}

		public void SetRenderFunc<PassData>(global::UnityEngine.Rendering.RenderGraphModule.BaseRenderFunc<PassData, global::UnityEngine.Rendering.RenderGraphModule.ComputeGraphContext> renderFunc) where PassData : class, new()
		{
			((global::UnityEngine.Rendering.RenderGraphModule.ComputeRenderGraphPass<PassData>)m_RenderPass).renderFunc = renderFunc;
		}

		public void SetRenderFunc<PassData>(global::UnityEngine.Rendering.RenderGraphModule.BaseRenderFunc<PassData, global::UnityEngine.Rendering.RenderGraphModule.RasterGraphContext> renderFunc) where PassData : class, new()
		{
			((global::UnityEngine.Rendering.RenderGraphModule.RasterRenderGraphPass<PassData>)m_RenderPass).renderFunc = renderFunc;
		}

		public void SetRenderFunc<PassData>(global::UnityEngine.Rendering.RenderGraphModule.BaseRenderFunc<PassData, global::UnityEngine.Rendering.RenderGraphModule.UnsafeGraphContext> renderFunc) where PassData : class, new()
		{
			((global::UnityEngine.Rendering.RenderGraphModule.UnsafeRenderGraphPass<PassData>)m_RenderPass).renderFunc = renderFunc;
		}

		public void UseRendererList(in global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle input)
		{
			m_RenderPass.UseRendererList(in input);
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckResource(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res, bool checkTransientReadWrite = false)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				if (!res.IsValid())
				{
					string renderGraphResourceName = m_Resources.GetRenderGraphResourceName(in res);
					throw new global::System.Exception($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName}' of type {res.type} at index {res.index} - " + "Using an invalid resource. Invalid resources can be resources leftover from a previous execution.");
				}
				int renderGraphResourceTransientIndex = m_Resources.GetRenderGraphResourceTransientIndex(in res);
				if (renderGraphResourceTransientIndex == m_RenderPass.index && checkTransientReadWrite)
				{
					string renderGraphResourceName2 = m_Resources.GetRenderGraphResourceName(in res);
					global::UnityEngine.Debug.LogError($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName2}' of type {res.type} at index {res.index} - " + "This pass is reading or writing a transient resource. Transient resources are always assumed to be both read and written using 'AccessFlags.ReadWrite'.");
				}
				if (renderGraphResourceTransientIndex != -1 && renderGraphResourceTransientIndex != m_RenderPass.index)
				{
					string renderGraphResourceName3 = m_Resources.GetRenderGraphResourceName(in res);
					throw new global::System.ArgumentException($"In pass '{m_RenderPass.name}' when trying to use resource '{renderGraphResourceName3}' of type {res.type} at index {res.index} - " + global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.RenderGraphExceptionMessages.UseTransientTextureInWrongPass(renderGraphResourceTransientIndex));
				}
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void CheckFrameBufferFetchEmulationIsSupported(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				if (!global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.IsFramebufferFetchEmulationSupportedOnCurrentPlatform())
				{
					throw new global::System.InvalidOperationException($"This API is not supported on the current platform: {(global::UnityEngine.SystemInfo.graphicsDeviceType)}");
				}
				if (!global::UnityEngine.Rendering.RenderGraphModule.Util.RenderGraphUtils.IsFramebufferFetchEmulationMSAASupportedOnCurrentPlatform() && m_RenderGraph.GetRenderTargetInfo(tex).bindMS)
				{
					throw new global::System.InvalidOperationException($"This API is not supported with MSAA attachments on the current platform: {(global::UnityEngine.SystemInfo.graphicsDeviceType)}");
				}
			}
		}

		public void SetShadingRateFragmentSize(global::UnityEngine.Rendering.ShadingRateFragmentSize shadingRateFragmentSize)
		{
			m_RenderPass.SetShadingRateFragmentSize(shadingRateFragmentSize);
		}

		public void SetShadingRateCombiner(global::UnityEngine.Rendering.ShadingRateCombinerStage stage, global::UnityEngine.Rendering.ShadingRateCombiner combiner)
		{
			m_RenderPass.SetShadingRateCombiner(stage, combiner);
		}

		public void SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags extendedFeatureFlags)
		{
			m_RenderPass.SetExtendedFeatureFlags(extendedFeatureFlags);
		}

		void global::UnityEngine.Rendering.RenderGraphModule.IRasterRenderGraphBuilder.SetShadingRateImageAttachment(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle tex)
		{
			SetShadingRateImageAttachment(in tex);
		}

		void global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder.UseTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags)
		{
			UseTexture(in input, flags);
		}

		void global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder.SetGlobalTextureAfterPass(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle input, int propertyId)
		{
			SetGlobalTextureAfterPass(in input, propertyId);
		}

		global::UnityEngine.Rendering.RenderGraphModule.BufferHandle global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder.UseBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferHandle input, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags flags)
		{
			return UseBuffer(in input, flags);
		}

		global::UnityEngine.Rendering.RenderGraphModule.TextureHandle global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder.CreateTransientTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc)
		{
			return CreateTransientTexture(in desc);
		}

		global::UnityEngine.Rendering.RenderGraphModule.TextureHandle global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder.CreateTransientTexture(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle texture)
		{
			return CreateTransientTexture(in texture);
		}

		global::UnityEngine.Rendering.RenderGraphModule.BufferHandle global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder.CreateTransientBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc)
		{
			return CreateTransientBuffer(in desc);
		}

		global::UnityEngine.Rendering.RenderGraphModule.BufferHandle global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder.CreateTransientBuffer(in global::UnityEngine.Rendering.RenderGraphModule.BufferHandle computebuffer)
		{
			return CreateTransientBuffer(in computebuffer);
		}

		void global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder.UseRendererList(in global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle input)
		{
			UseRendererList(in input);
		}
	}
}
