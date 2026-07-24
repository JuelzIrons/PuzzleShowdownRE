namespace UnityEngine.Rendering.RenderGraphModule
{
	[global::System.Diagnostics.DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal abstract class RenderGraphPass
	{
		public struct RandomWriteResourceInfo
		{
			public global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h;

			public bool preserveCounterValue;
		}

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>[] resourceReadLists = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>[3];

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>[] resourceWriteLists = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>[3];

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>[] transientResourceList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>[3];

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle> usedRendererListList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle>();

		public global::System.Collections.Generic.List<(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle, int)> setGlobalsList = new global::System.Collections.Generic.List<(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle, int)>();

		public bool useAllGlobalTextures;

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> implicitReadsList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>();

		public string name { get; protected set; }

		public int index { get; protected set; }

		public global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType type { get; internal set; }

		public global::UnityEngine.Rendering.ProfilingSampler customSampler { get; protected set; }

		public bool enableAsyncCompute { get; protected set; }

		public bool allowPassCulling { get; protected set; }

		public bool allowGlobalState { get; protected set; }

		public bool enableFoveatedRasterization { get; protected set; }

		public global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags extendedFeatureFlags { get; protected set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureAccess depthAccess { get; protected set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureAccess[] colorBufferAccess { get; protected set; } = new global::UnityEngine.Rendering.RenderGraphModule.TextureAccess[global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.kMaxMRTCount];

		public int colorBufferMaxIndex { get; protected set; } = -1;

		public bool hasShadingRateImage { get; protected set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureAccess shadingRateAccess { get; protected set; }

		public bool hasShadingRateStates { get; protected set; }

		public global::UnityEngine.Rendering.ShadingRateFragmentSize shadingRateFragmentSize { get; protected set; }

		public global::UnityEngine.Rendering.ShadingRateCombiner primitiveShadingRateCombiner { get; protected set; }

		public global::UnityEngine.Rendering.ShadingRateCombiner fragmentShadingRateCombiner { get; protected set; }

		public global::UnityEngine.Rendering.RenderGraphModule.TextureAccess[] fragmentInputAccess { get; protected set; } = new global::UnityEngine.Rendering.RenderGraphModule.TextureAccess[global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.kMaxMRTCount];

		public int fragmentInputMaxIndex { get; protected set; } = -1;

		public global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass.RandomWriteResourceInfo[] randomAccessResource { get; protected set; } = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass.RandomWriteResourceInfo[global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.kMaxMRTCount];

		public int randomAccessResourceMaxIndex { get; protected set; } = -1;

		public bool generateDebugData { get; protected set; }

		public bool allowRendererListCulling { get; protected set; }

		public abstract void Execute(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext renderGraphContext);

		public abstract void Release(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool pool);

		public abstract bool HasRenderFunc();

		public abstract int GetRenderFuncHash();

		public RenderGraphPass()
		{
			for (int i = 0; i < 3; i++)
			{
				resourceReadLists[i] = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>();
				resourceWriteLists[i] = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>();
				transientResourceList[i] = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>();
			}
		}

		public void Clear()
		{
			name = "";
			index = -1;
			customSampler = null;
			for (int i = 0; i < 3; i++)
			{
				resourceReadLists[i].Clear();
				resourceWriteLists[i].Clear();
				transientResourceList[i].Clear();
			}
			usedRendererListList.Clear();
			setGlobalsList.Clear();
			useAllGlobalTextures = false;
			implicitReadsList.Clear();
			enableAsyncCompute = false;
			allowPassCulling = true;
			allowRendererListCulling = true;
			allowGlobalState = false;
			enableFoveatedRasterization = false;
			generateDebugData = true;
			colorBufferMaxIndex = -1;
			fragmentInputMaxIndex = -1;
			randomAccessResourceMaxIndex = -1;
			depthAccess = default(global::UnityEngine.Rendering.RenderGraphModule.TextureAccess);
			hasShadingRateImage = false;
			hasShadingRateStates = false;
			shadingRateFragmentSize = global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize1x1;
			primitiveShadingRateCombiner = global::UnityEngine.Rendering.ShadingRateCombiner.Keep;
			fragmentShadingRateCombiner = global::UnityEngine.Rendering.ShadingRateCombiner.Keep;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool HasRenderAttachments()
		{
			if (!depthAccess.textureHandle.IsValid() && !colorBufferAccess[0].textureHandle.IsValid())
			{
				return colorBufferMaxIndex > 0;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool IsTransient(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			for (int i = 0; i < transientResourceList[res.iType].Count; i++)
			{
				if (transientResourceList[res.iType][i].index == res.index)
				{
					return true;
				}
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool IsWritten(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			for (int i = 0; i < resourceWriteLists[res.iType].Count; i++)
			{
				if (resourceWriteLists[res.iType][i].index == res.index)
				{
					return true;
				}
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool IsRead(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			if (res.IsVersioned)
			{
				return resourceReadLists[res.iType].Contains(res);
			}
			for (int i = 0; i < resourceReadLists[res.iType].Count; i++)
			{
				if (resourceReadLists[res.iType][i].index == res.index)
				{
					return true;
				}
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool IsAttachment(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle res)
		{
			if (depthAccess.textureHandle.IsValid() && depthAccess.textureHandle.handle.index == res.handle.index)
			{
				return true;
			}
			for (int i = 0; i < colorBufferAccess.Length; i++)
			{
				if (colorBufferAccess[i].textureHandle.IsValid() && colorBufferAccess[i].textureHandle.handle.index == res.handle.index)
				{
					return true;
				}
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void AddResourceWrite(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			resourceWriteLists[res.iType].Add(res);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void AddResourceRead(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			resourceReadLists[res.iType].Add(res);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void AddTransientResource(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res)
		{
			transientResourceList[res.iType].Add(res);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void UseRendererList(in global::UnityEngine.Rendering.RenderGraphModule.RendererListHandle rendererList)
		{
			usedRendererListList.Add(rendererList);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void EnableAsyncCompute(bool value)
		{
			enableAsyncCompute = value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void AllowPassCulling(bool value)
		{
			allowPassCulling = value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void EnableFoveatedRasterization(bool value)
		{
			enableFoveatedRasterization = value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void AllowRendererListCulling(bool value)
		{
			allowRendererListCulling = value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void AllowGlobalState(bool value)
		{
			allowGlobalState = value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void GenerateDebugData(bool value)
		{
			generateDebugData = value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetColorBuffer(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle resource, int index)
		{
			colorBufferMaxIndex = global::System.Math.Max(colorBufferMaxIndex, index);
			colorBufferAccess[index] = new global::UnityEngine.Rendering.RenderGraphModule.TextureAccess(in colorBufferAccess[index], in resource);
			AddResourceWrite(in resource.handle);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetColorBufferRaw(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle resource, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags accessFlags, int mipLevel, int depthSlice)
		{
			if (colorBufferAccess[index].textureHandle.handle.Equals(resource.handle) || !colorBufferAccess[index].textureHandle.IsValid())
			{
				colorBufferMaxIndex = global::System.Math.Max(colorBufferMaxIndex, index);
				colorBufferAccess[index] = new global::UnityEngine.Rendering.RenderGraphModule.TextureAccess(in resource, accessFlags, mipLevel, depthSlice);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetFragmentInputRaw(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle resource, int index, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags accessFlags, int mipLevel, int depthSlice)
		{
			if (fragmentInputAccess[index].textureHandle.handle.Equals(resource.handle) || !fragmentInputAccess[index].textureHandle.IsValid())
			{
				fragmentInputMaxIndex = global::System.Math.Max(fragmentInputMaxIndex, index);
				fragmentInputAccess[index] = new global::UnityEngine.Rendering.RenderGraphModule.TextureAccess(in resource, accessFlags, mipLevel, depthSlice);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetRandomWriteResourceRaw(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource, int index, bool preserveCounterValue, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags accessFlags)
		{
			if (randomAccessResource[index].h.Equals(resource) || !randomAccessResource[index].h.IsValid())
			{
				randomAccessResourceMaxIndex = global::System.Math.Max(randomAccessResourceMaxIndex, index);
				ref global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass.RandomWriteResourceInfo reference = ref randomAccessResource[index];
				reference.h = resource;
				reference.preserveCounterValue = preserveCounterValue;
				return;
			}
			throw new global::System.InvalidOperationException($"In pass '{name}' when trying to call SetRandomAccessAttachment/UseBufferRandomAccess with resource of type {resource.type} at index {index} - " + "You can only bind a single texture to a random write input index. Verify your indexes are correct.");
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetDepthBuffer(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle resource, global::UnityEngine.Rendering.RenderGraphModule.DepthAccess flags)
		{
			depthAccess = new global::UnityEngine.Rendering.RenderGraphModule.TextureAccess(in resource, (global::UnityEngine.Rendering.RenderGraphModule.AccessFlags)flags, 0, 0);
			if ((flags & global::UnityEngine.Rendering.RenderGraphModule.DepthAccess.Read) != 0)
			{
				AddResourceRead(in resource.handle);
			}
			if ((flags & global::UnityEngine.Rendering.RenderGraphModule.DepthAccess.Write) != 0)
			{
				AddResourceWrite(in resource.handle);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetDepthBufferRaw(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle resource, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags accessFlags, int mipLevel, int depthSlice)
		{
			if (depthAccess.textureHandle.handle.Equals(resource.handle) || !depthAccess.textureHandle.IsValid())
			{
				depthAccess = new global::UnityEngine.Rendering.RenderGraphModule.TextureAccess(in resource, accessFlags, mipLevel, depthSlice);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void ComputeTextureHash(ref global::UnityEngine.Rendering.HashFNV1A32 generator, in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources)
		{
			if (handle.index == 0)
			{
				return;
			}
			if (resources.IsRenderGraphResourceImported(in handle))
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureResource textureResource = resources.GetTextureResource(in handle);
				global::UnityEngine.Rendering.RTHandle graphicsResource = textureResource.graphicsResource;
				ref global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc = ref textureResource.desc;
				global::UnityEngine.Texture externalTexture = graphicsResource.externalTexture;
				if (externalTexture != null)
				{
					generator.Append((int)externalTexture.graphicsFormat);
					generator.Append((int)externalTexture.dimension);
					generator.Append(externalTexture.width);
					generator.Append(externalTexture.height);
					if (externalTexture is global::UnityEngine.RenderTexture renderTexture)
					{
						generator.Append(renderTexture.antiAliasing);
					}
				}
				else if (graphicsResource.rt != null)
				{
					global::UnityEngine.RenderTexture rt = graphicsResource.rt;
					generator.Append((int)rt.graphicsFormat);
					generator.Append((int)rt.dimension);
					generator.Append(rt.antiAliasing);
					if (graphicsResource.useScaling)
					{
						if (graphicsResource.scaleFunc != null)
						{
							generator.Append(global::UnityEngine.Rendering.DelegateHashCodeUtils.GetFuncHashCode(graphicsResource.scaleFunc));
						}
						else
						{
							generator.Append(graphicsResource.scaleFactor);
						}
					}
					else
					{
						generator.Append(rt.width);
						generator.Append(rt.height);
					}
				}
				else if (graphicsResource.nameID != default(global::UnityEngine.Rendering.RenderTargetIdentifier))
				{
					int input = (int)desc.format;
					generator.Append(in input);
					input = (int)desc.dimension;
					generator.Append(in input);
					input = (int)desc.msaaSamples;
					generator.Append(in input);
					generator.Append(in desc.width);
					generator.Append(in desc.height);
				}
				generator.Append(in desc.clearBuffer);
				generator.Append(in desc.discardBuffer);
			}
			else
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.TextureDesc textureResourceDesc = ref resources.GetTextureResourceDesc(in handle);
				int input = (int)textureResourceDesc.format;
				generator.Append(in input);
				input = (int)textureResourceDesc.dimension;
				generator.Append(in input);
				input = (int)textureResourceDesc.msaaSamples;
				generator.Append(in input);
				generator.Append(in textureResourceDesc.clearBuffer);
				generator.Append(in textureResourceDesc.discardBuffer);
				switch (textureResourceDesc.sizeMode)
				{
				case global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Explicit:
					generator.Append(in textureResourceDesc.width);
					generator.Append(in textureResourceDesc.height);
					break;
				case global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Scale:
					generator.Append(in textureResourceDesc.scale);
					break;
				case global::UnityEngine.Rendering.RenderGraphModule.TextureSizeMode.Functor:
					generator.Append(global::UnityEngine.Rendering.DelegateHashCodeUtils.GetFuncHashCode(textureResourceDesc.func));
					break;
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static void ComputeHashForTextureAccess(ref global::UnityEngine.Rendering.HashFNV1A32 generator, in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle, in global::UnityEngine.Rendering.RenderGraphModule.TextureAccess textureAccess)
		{
			generator.Append(handle.index);
			int input = (int)textureAccess.flags;
			generator.Append(in input);
			generator.Append(in textureAccess.mipLevel);
			generator.Append(in textureAccess.depthSlice);
		}

		public void ComputeHash(ref global::UnityEngine.Rendering.HashFNV1A32 generator, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources)
		{
			generator.Append((int)type);
			generator.Append(enableAsyncCompute);
			generator.Append(allowPassCulling);
			generator.Append(allowGlobalState);
			generator.Append(enableFoveatedRasterization);
			global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle = depthAccess.textureHandle.handle;
			global::UnityEngine.Rendering.RenderGraphModule.TextureAccess textureAccess;
			if (handle.IsValid())
			{
				ComputeTextureHash(ref generator, in handle, resources);
				textureAccess = depthAccess;
				ComputeHashForTextureAccess(ref generator, in handle, in textureAccess);
			}
			for (int i = 0; i < colorBufferMaxIndex + 1; i++)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureAccess textureAccess2 = colorBufferAccess[i];
				global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle2 = textureAccess2.textureHandle.handle;
				if (handle2.IsValid())
				{
					ComputeTextureHash(ref generator, in handle2, resources);
					ComputeHashForTextureAccess(ref generator, in handle2, in textureAccess2);
				}
			}
			generator.Append(colorBufferMaxIndex);
			generator.Append(hasShadingRateImage);
			if (hasShadingRateImage)
			{
				global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle3 = shadingRateAccess.textureHandle.handle;
				if (handle3.IsValid())
				{
					ComputeTextureHash(ref generator, in handle3, resources);
					textureAccess = shadingRateAccess;
					ComputeHashForTextureAccess(ref generator, in handle3, in textureAccess);
				}
			}
			generator.Append(hasShadingRateStates);
			generator.Append((int)shadingRateFragmentSize);
			generator.Append((int)primitiveShadingRateCombiner);
			generator.Append((int)fragmentShadingRateCombiner);
			for (int j = 0; j < fragmentInputMaxIndex + 1; j++)
			{
				global::UnityEngine.Rendering.RenderGraphModule.TextureAccess textureAccess3 = fragmentInputAccess[j];
				global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle4 = textureAccess3.textureHandle.handle;
				if (handle4.IsValid())
				{
					ComputeTextureHash(ref generator, in handle4, resources);
					ComputeHashForTextureAccess(ref generator, in handle4, in textureAccess3);
				}
			}
			for (int k = 0; k < randomAccessResourceMaxIndex + 1; k++)
			{
				global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass.RandomWriteResourceInfo randomWriteResourceInfo = randomAccessResource[k];
				if (randomWriteResourceInfo.h.IsValid())
				{
					generator.Append(randomWriteResourceInfo.h.index);
					generator.Append(in randomWriteResourceInfo.preserveCounterValue);
				}
			}
			generator.Append(randomAccessResourceMaxIndex);
			generator.Append(fragmentInputMaxIndex);
			generator.Append(generateDebugData);
			generator.Append(allowRendererListCulling);
			for (int l = 0; l < 3; l++)
			{
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> list = resourceReadLists[l];
				int count = list.Count;
				for (int m = 0; m < count; m++)
				{
					generator.Append(list[m].index);
				}
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> list2 = resourceWriteLists[l];
				int count2 = list2.Count;
				for (int n = 0; n < count2; n++)
				{
					generator.Append(list2[n].index);
				}
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> list3 = transientResourceList[l];
				int count3 = list3.Count;
				for (int num = 0; num < count3; num++)
				{
					generator.Append(list3[num].index);
				}
			}
			int count4 = usedRendererListList.Count;
			for (int num2 = 0; num2 < count4; num2++)
			{
				generator.Append(usedRendererListList[num2].handle);
			}
			int count5 = setGlobalsList.Count;
			for (int num3 = 0; num3 < count5; num3++)
			{
				(global::UnityEngine.Rendering.RenderGraphModule.TextureHandle, int) tuple = setGlobalsList[num3];
				generator.Append(tuple.Item1.handle.index);
				generator.Append(in tuple.Item2);
			}
			generator.Append(in useAllGlobalTextures);
			int count6 = implicitReadsList.Count;
			for (int num4 = 0; num4 < count6; num4++)
			{
				generator.Append(implicitReadsList[num4].index);
			}
			generator.Append(GetRenderFuncHash());
		}

		public void SetShadingRateImageRaw(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle shadingRateImage)
		{
			if (global::UnityEngine.Rendering.ShadingRateInfo.supportsPerImageTile)
			{
				hasShadingRateImage = true;
				shadingRateAccess = new global::UnityEngine.Rendering.RenderGraphModule.TextureAccess(in shadingRateImage, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read, 0, 0);
			}
		}

		public void SetShadingRateImage(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle shadingRateImage, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags accessFlags, int mipLevel, int depthSlice)
		{
			if (global::UnityEngine.Rendering.ShadingRateInfo.supportsPerImageTile)
			{
				hasShadingRateImage = true;
				shadingRateAccess = new global::UnityEngine.Rendering.RenderGraphModule.TextureAccess(in shadingRateImage, accessFlags, mipLevel, depthSlice);
				global::UnityEngine.Rendering.RenderGraphModule.TextureAccess textureAccess = shadingRateAccess;
				AddResourceRead(in textureAccess.textureHandle.handle);
			}
		}

		public void SetShadingRateFragmentSize(global::UnityEngine.Rendering.ShadingRateFragmentSize shadingRateFragmentSize)
		{
			if (global::UnityEngine.Rendering.ShadingRateInfo.supportsPerDrawCall)
			{
				hasShadingRateStates = true;
				this.shadingRateFragmentSize = shadingRateFragmentSize;
			}
		}

		public void SetShadingRateCombiner(global::UnityEngine.Rendering.ShadingRateCombinerStage stage, global::UnityEngine.Rendering.ShadingRateCombiner combiner)
		{
			if (global::UnityEngine.Rendering.ShadingRateInfo.supportsPerImageTile)
			{
				switch (stage)
				{
				case global::UnityEngine.Rendering.ShadingRateCombinerStage.Primitive:
					hasShadingRateStates = true;
					primitiveShadingRateCombiner = combiner;
					break;
				case global::UnityEngine.Rendering.ShadingRateCombinerStage.Fragment:
					hasShadingRateStates = true;
					fragmentShadingRateCombiner = combiner;
					break;
				}
			}
		}

		public void SetExtendedFeatureFlags(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags value)
		{
			extendedFeatureFlags |= value;
		}
	}
	[global::System.Diagnostics.DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	[global::System.Obsolete("RenderGraphPass is deprecated, use RasterRenderGraphPass/ComputeRenderGraphPass/UnsafeRenderGraphPass instead.")]
	internal sealed class RenderGraphPass<PassData> : global::UnityEngine.Rendering.RenderGraphModule.BaseRenderGraphPass<PassData, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphContext> where PassData : class, new()
	{
		internal static global::UnityEngine.Rendering.RenderGraphModule.RenderGraphContext c;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override void Execute(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext renderGraphContext)
		{
			c.FromInternalContext(renderGraphContext);
			renderFunc(data, c);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override void Release(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool pool)
		{
			base.Release(pool);
			pool.Release(this);
		}
	}
}
