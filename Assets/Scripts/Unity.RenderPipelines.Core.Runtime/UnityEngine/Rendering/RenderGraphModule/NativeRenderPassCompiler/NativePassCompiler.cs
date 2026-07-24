namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal class NativePassCompiler : global::System.IDisposable
	{
		internal struct RenderGraphInputInfo
		{
			public global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry m_ResourcesForDebugOnly;

			public global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass> m_RenderPasses;

			public string debugName;

			public bool disablePassCulling;

			public bool disablePassMerging;

			public global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy renderTextureUVOriginStrategy;
		}

		internal enum NativeCompilerProfileId
		{
			NRPRGComp_PrepareNativePass = 0,
			NRPRGComp_SetupContextData = 1,
			NRPRGComp_BuildGraph = 2,
			NRPRGComp_CullNodes = 3,
			NRPRGComp_TryMergeNativePasses = 4,
			NRPRGComp_FindResourceUsageRanges = 5,
			NRPRGComp_DetectMemorylessResources = 6,
			NRPRGComp_PropagateTextureUVOrigin = 7,
			NRPRGComp_ExecuteInitializeResources = 8,
			NRPRGComp_ExecuteBeginRenderpassCommand = 9,
			NRPRGComp_ExecuteDestroyResources = 10
		}

		internal global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.RenderGraphInputInfo graph;

		internal global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData contextData;

		internal global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData defaultContextData;

		internal global::UnityEngine.Rendering.CommandBuffer previousCommandBuffer;

		private global::System.Collections.Generic.Stack<int> m_HasSideEffectPassIdCullingStack;

		private global::System.Collections.Generic.List<global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>> m_UnusedVersionedResourceIdCullingStacks;

		private global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>> m_DelayedLastUseListPerPassMap;

		private RenderGraphCompilationCache m_CompilationCache;

		private global::UnityEngine.Rendering.RenderTargetIdentifier[][] m_TempMRTArrays;

		internal const int k_EstimatedPassCount = 100;

		internal const int k_MaxSubpass = 8;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.AttachmentDescriptor> m_BeginRenderPassAttachments;

		internal static bool s_ForceGenerateAuditsForTests;

		private const int ArbitraryMaxNbMergedPasses = 16;

		private global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name> graphPassNamesForDebug = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name>(16);

		public NativePassCompiler(RenderGraphCompilationCache cache)
		{
			m_CompilationCache = cache;
			defaultContextData = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData();
			m_HasSideEffectPassIdCullingStack = new global::System.Collections.Generic.Stack<int>(100);
			m_UnusedVersionedResourceIdCullingStacks = new global::System.Collections.Generic.List<global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>>();
			for (int i = 0; i < 3; i++)
			{
				m_UnusedVersionedResourceIdCullingStacks.Add(new global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>());
			}
			m_DelayedLastUseListPerPassMap = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>>(100);
			for (int j = 0; j < 100; j++)
			{
				m_DelayedLastUseListPerPassMap.Add(j, new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>());
			}
			m_TempMRTArrays = new global::UnityEngine.Rendering.RenderTargetIdentifier[global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.kMaxMRTCount][];
			for (int k = 0; k < global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.kMaxMRTCount; k++)
			{
				m_TempMRTArrays[k] = new global::UnityEngine.Rendering.RenderTargetIdentifier[k + 1];
			}
		}

		~NativePassCompiler()
		{
			Cleanup();
		}

		public void Dispose()
		{
			Cleanup();
			global::System.GC.SuppressFinalize(this);
		}

		public void Cleanup()
		{
			contextData?.Dispose();
			defaultContextData?.Dispose();
			if (m_BeginRenderPassAttachments.IsCreated)
			{
				m_BeginRenderPassAttachments.Dispose();
			}
		}

		public bool Initialize(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass> renderPasses, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugParams debugParams, string debugName, bool useCompilationCaching, int graphHash, int frameIndex, global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy renderTextureUVOriginStrategy)
		{
			bool result = false;
			if (!useCompilationCaching)
			{
				contextData = defaultContextData;
			}
			else
			{
				result = m_CompilationCache.GetCompilationCache(graphHash, frameIndex, out contextData);
			}
			graph.m_ResourcesForDebugOnly = resources;
			graph.m_RenderPasses = renderPasses;
			graph.disablePassCulling = debugParams.disablePassCulling;
			graph.disablePassMerging = debugParams.disablePassMerging;
			graph.debugName = debugName;
			graph.renderTextureUVOriginStrategy = renderTextureUVOriginStrategy;
			Clear(!useCompilationCaching);
			return result;
		}

		private void HandleExtendedFeatureFlags()
		{
			for (int i = 0; i < contextData.nativePassData.Length; i++)
			{
				int firstNativeSubPass = contextData.nativePassData[i].firstNativeSubPass;
				if (firstNativeSubPass < 0)
				{
					continue;
				}
				int firstGraphPass = contextData.nativePassData[i].firstGraphPass;
				int j = 0;
				for (int k = 0; k < contextData.nativePassData[i].numNativeSubPasses; k++)
				{
					global::UnityEngine.Rendering.SubPassFlags subPassFlags = global::UnityEngine.Rendering.SubPassFlags.MultiviewRenderRegionsCompatible;
					for (; j < contextData.nativePassData[i].numGraphPasses && contextData.passData[j + firstGraphPass].nativeSubPassIndex == k; j++)
					{
						if (contextData.passData[j + firstGraphPass].extendedFeatureFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.TileProperties))
						{
							subPassFlags |= global::UnityEngine.Rendering.SubPassFlags.TileProperties;
						}
						if (!contextData.passData[j + firstGraphPass].extendedFeatureFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultiviewRenderRegionsCompatible))
						{
							subPassFlags &= ~global::UnityEngine.Rendering.SubPassFlags.MultiviewRenderRegionsCompatible;
						}
					}
					contextData.nativeSubPassData.ElementAt(firstNativeSubPass + k).flags |= subPassFlags;
				}
			}
		}

		public void Compile(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources)
		{
			SetupContextData(resources);
			BuildGraph();
			CullUnusedRenderGraphPasses();
			TryMergeNativePasses();
			HandleExtendedFeatureFlags();
			FindResourceUsageRangeAndSynchronization();
			DetectMemoryLessResources();
			PrepareNativeRenderPasses();
			if (graph.renderTextureUVOriginStrategy == global::UnityEngine.Rendering.RenderGraphModule.RenderTextureUVOriginStrategy.PropagateAttachmentOrientation)
			{
				PropagateTextureUVOrigin();
			}
		}

		public void Clear(bool clearContextData)
		{
			if (clearContextData)
			{
				contextData.Clear();
			}
			m_HasSideEffectPassIdCullingStack.Clear();
			for (int i = 0; i < 3; i++)
			{
				m_UnusedVersionedResourceIdCullingStacks[i].Clear();
			}
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>> item in m_DelayedLastUseListPerPassMap)
			{
				item.Value.Clear();
			}
			m_DelayedLastUseListPerPassMap.Clear();
		}

		private void SetPassStatesForNativePass(int nativePassId)
		{
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData.SetPassStatesForNativePass(contextData, nativePassId);
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void ValidatePasses()
		{
			if (!global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < graph.m_RenderPasses.Count; i++)
			{
				if (graph.m_RenderPasses[i].extendedFeatureFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.TileProperties))
				{
					if (num > -1)
					{
						throw new global::System.Exception("ExtendedFeatureFlags.TileProperties can only be set once per render graph (render graph " + graph.debugName + ", pass " + graph.m_RenderPasses[i].name + "), previously set at (pass " + graph.m_RenderPasses[num].name + ").");
					}
					num = i;
				}
			}
		}

		private void SetupContextData(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.NativeCompilerProfileId.NRPRGComp_SetupContextData)))
			{
				contextData.Initialize(resources, 100);
			}
		}

		private bool TrySetupRasterFragmentList(ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData ctxPass, ref global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass inputPass, out string errorMessage)
		{
			errorMessage = null;
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData compilerContextData = contextData;
			ctxPass.firstFragment = compilerContextData.fragmentData.Length;
			if (inputPass.depthAccess.textureHandle.handle.IsValid())
			{
				ctxPass.fragmentInfoHasDepth = true;
				if (compilerContextData.TryAddToFragmentList(inputPass.depthAccess, ctxPass.firstFragment, ctxPass.numFragments, out errorMessage))
				{
					global::UnityEngine.Rendering.RenderGraphModule.TextureAccess depthAccess = inputPass.depthAccess;
					ctxPass.TryAddFragment(in depthAccess.textureHandle.handle, compilerContextData, out errorMessage);
				}
				if (errorMessage != null)
				{
					errorMessage = $"when trying to add depth attachment of type {inputPass.depthAccess.textureHandle.handle.type} at index {inputPass.depthAccess.textureHandle.handle.index} - {errorMessage}";
					return false;
				}
			}
			for (int i = 0; i < inputPass.colorBufferMaxIndex + 1; i++)
			{
				if (inputPass.colorBufferAccess[i].textureHandle.handle.IsValid())
				{
					if (compilerContextData.TryAddToFragmentList(in inputPass.colorBufferAccess[i], ctxPass.firstFragment, ctxPass.numFragments, out errorMessage))
					{
						ctxPass.TryAddFragment(in inputPass.colorBufferAccess[i].textureHandle.handle, compilerContextData, out errorMessage);
					}
					if (errorMessage != null)
					{
						errorMessage = $"when trying to add render attachment of type {inputPass.colorBufferAccess[i].textureHandle.handle.type} at index {inputPass.colorBufferAccess[i].textureHandle.handle.index} - {errorMessage}";
						return false;
					}
				}
			}
			if (inputPass.hasShadingRateImage && inputPass.shadingRateAccess.textureHandle.handle.IsValid())
			{
				if (compilerContextData.TryAddToFragmentList(inputPass.shadingRateAccess, ctxPass.firstFragment, ctxPass.numFragments, out errorMessage))
				{
					ctxPass.shadingRateImageIndex = compilerContextData.fragmentData.Length - 1;
				}
				if (errorMessage != null)
				{
					errorMessage = $"when trying to add VRS attachment of type {inputPass.shadingRateAccess.textureHandle.handle.type} at index {inputPass.shadingRateAccess.textureHandle.handle.index} - {errorMessage}";
					return false;
				}
			}
			ctxPass.firstFragmentInput = compilerContextData.fragmentData.Length;
			for (int j = 0; j < inputPass.fragmentInputMaxIndex + 1; j++)
			{
				if (inputPass.fragmentInputAccess[j].textureHandle.IsValid())
				{
					if (compilerContextData.TryAddToFragmentList(in inputPass.fragmentInputAccess[j], ctxPass.firstFragmentInput, ctxPass.numFragmentInputs, out errorMessage))
					{
						ctxPass.TryAddFragmentInput(in inputPass.fragmentInputAccess[j].textureHandle.handle, compilerContextData, out errorMessage);
					}
					if (errorMessage != null)
					{
						errorMessage = $"when trying to add input attachment of type {inputPass.fragmentInputAccess[j].textureHandle.handle.type} at index {inputPass.fragmentInputAccess[j].textureHandle.handle.index} - {errorMessage}";
						return false;
					}
				}
			}
			ctxPass.firstRandomAccessResource = compilerContextData.randomAccessResourceData.Length;
			for (int k = 0; k < inputPass.randomAccessResourceMaxIndex + 1; k++)
			{
				ref global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass.RandomWriteResourceInfo reference = ref inputPass.randomAccessResource[k];
				if (reference.h.IsValid())
				{
					if (compilerContextData.TryAddToRandomAccessResourceList(in reference.h, k, reference.preserveCounterValue, ctxPass.firstRandomAccessResource, ctxPass.numRandomAccessResources, out errorMessage))
					{
						ctxPass.AddRandomAccessResource();
					}
					if (errorMessage != null)
					{
						errorMessage = $"when trying to add random access attachment of type {reference.h.type} at index {reference.h.index} - {errorMessage}";
						return false;
					}
				}
			}
			_ = ctxPass.numFragments;
			return true;
		}

		private void BuildGraph()
		{
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData compilerContextData = contextData;
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass> renderPasses = graph.m_RenderPasses;
			compilerContextData.passData.ResizeUninitialized(renderPasses.Count);
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.NativeCompilerProfileId.NRPRGComp_BuildGraph)))
			{
				for (int i = 0; i < renderPasses.Count; i++)
				{
					global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass pass = renderPasses[i];
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref compilerContextData.passData.ElementAt(i);
					reference.ResetAndInitialize(in pass, i);
					compilerContextData.passNames.Add(new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name(pass.name, computeUTF8ByteCount: true));
					if (reference.hasSideEffects)
					{
						m_HasSideEffectPassIdCullingStack.Push(i);
					}
					if (reference.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Raster && !TrySetupRasterFragmentList(ref reference, ref pass, out var errorMessage))
					{
						throw new global::System.Exception("In pass '" + pass.name + "', " + errorMessage);
					}
					reference.firstInput = compilerContextData.inputData.Length;
					reference.firstOutput = compilerContextData.outputData.Length;
					for (int j = 0; j < 3; j++)
					{
						global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> list = pass.resourceWriteLists[j];
						int count = list.Count;
						for (int k = 0; k < count; k++)
						{
							global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h = list[k];
							if (compilerContextData.UnversionedResourceData(in h).isImported && !reference.hasSideEffects)
							{
								reference.hasSideEffects = true;
								m_HasSideEffectPassIdCullingStack.Push(i);
							}
							compilerContextData.resources[h].SetWritingPass(compilerContextData, in h, i);
							compilerContextData.outputData.Add(new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassOutputData(in h));
							reference.numOutputs++;
						}
						global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> list2 = pass.resourceReadLists[j];
						int count2 = list2.Count;
						for (int l = 0; l < count2; l++)
						{
							global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h2 = list2[l];
							compilerContextData.resources[h2].RegisterReadingPass(compilerContextData, in h2, i, reference.numInputs);
							compilerContextData.inputData.Add(new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData(in h2));
							reference.numInputs++;
						}
						global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> list3 = pass.transientResourceList[j];
						int count3 = list3.Count;
						for (int m = 0; m < count3; m++)
						{
							global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h3 = list3[m];
							compilerContextData.resources[h3].RegisterReadingPass(compilerContextData, in h3, i, reference.numInputs);
							compilerContextData.inputData.Add(new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData(in h3));
							reference.numInputs++;
							compilerContextData.resources[h3].SetWritingPass(compilerContextData, in h3, i);
							compilerContextData.outputData.Add(new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassOutputData(in h3));
							reference.numOutputs++;
						}
						if (j != 0 || reference.type != global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Raster)
						{
							continue;
						}
						reference.firstSampledOnlyRaster = compilerContextData.sampledData.Length;
						global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData> readOnlySpan = reference.Inputs(compilerContextData);
						for (int n = 0; n < readOnlySpan.Length; n++)
						{
							ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData reference2 = ref readOnlySpan[n];
							if (!reference.IsUsedAsFragment(in reference2.resource, compilerContextData))
							{
								compilerContextData.sampledData.Add(in reference2.resource);
								reference.numSampledOnlyRaster++;
							}
						}
					}
				}
			}
		}

		private void CullUnusedRenderGraphPasses()
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.NativeCompilerProfileId.NRPRGComp_CullNodes)))
			{
				if (graph.disablePassCulling)
				{
					return;
				}
				global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData compilerContextData = contextData;
				compilerContextData.CullAllPasses(isCulled: true);
				while (m_HasSideEffectPassIdCullingStack.Count != 0)
				{
					int index = m_HasSideEffectPassIdCullingStack.Pop();
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref compilerContextData.passData.ElementAt(index);
					if (!reference.culled)
					{
						continue;
					}
					global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData> readOnlySpan = reference.Inputs(compilerContextData);
					for (int i = 0; i < readOnlySpan.Length; i++)
					{
						ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData reference2 = ref readOnlySpan[i];
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData reference3 = ref compilerContextData.resources[reference2.resource];
						if (reference3.written)
						{
							m_HasSideEffectPassIdCullingStack.Push(reference3.writePassId);
						}
					}
					reference.culled = false;
				}
				for (int num = compilerContextData.passData.Length - 1; num >= 0; num--)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference4 = ref compilerContextData.passData.ElementAt(num);
					if (reference4.culled)
					{
						global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData passData = reference4;
						passData.DisconnectFromResources(compilerContextData);
					}
				}
			}
		}

		private void CullRenderGraphPassesWritingOnlyUnusedResources()
		{
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData compilerContextData = contextData;
			int length = compilerContextData.passData.Length;
			for (int i = 0; i < length; i++)
			{
				ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref compilerContextData.passData.ElementAt(i);
				reference.tag = reference.numOutputs;
				global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassOutputData> readOnlySpan = reference.Outputs(compilerContextData);
				for (int j = 0; j < readOnlySpan.Length; j++)
				{
					ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource = ref readOnlySpan[j].resource;
					if (compilerContextData.resources[resource].numReaders == 0)
					{
						m_UnusedVersionedResourceIdCullingStacks[resource.iType].Push(resource);
					}
				}
			}
			for (int k = 0; k < 3; k++)
			{
				global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> stack = m_UnusedVersionedResourceIdCullingStacks[k];
				while (stack.Count != 0)
				{
					global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h = stack.Pop();
					if (compilerContextData.resources.unversionedData[k].ElementAt(h.index).isImported)
					{
						continue;
					}
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData reference2 = ref compilerContextData.resources[h];
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference3 = ref compilerContextData.passData.ElementAt(reference2.writePassId);
					if (reference3.culled)
					{
						continue;
					}
					reference3.tag--;
					if (reference3.tag == 0 && !reference3.hasSideEffects)
					{
						reference3.culled = true;
						reference3.DisconnectFromResources(compilerContextData, stack, k);
						continue;
					}
					global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h2 = new global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle(in h, h.version - 1);
					if (graph.m_RenderPasses[reference3.passId].implicitReadsList.Contains(h2))
					{
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData reference4 = ref compilerContextData.resources[h2];
						reference4.RemoveReadingPass(compilerContextData, in h2, reference3.passId);
						if (reference4.written && reference4.numReaders == 0)
						{
							stack.Push(h2);
						}
					}
				}
			}
		}

		private void TryMergeNativePasses()
		{
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData compilerContextData = contextData;
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.NativeCompilerProfileId.NRPRGComp_TryMergeNativePasses)))
			{
				int num = -1;
				for (int i = 0; i < compilerContextData.passData.Length; i++)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref compilerContextData.passData.ElementAt(i);
					if (reference.culled)
					{
						continue;
					}
					if (num == -1)
					{
						if (reference.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Raster)
						{
							compilerContextData.nativePassData.Add(new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData(ref reference, compilerContextData));
							reference.nativePassIndex = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.LastIndex(ref compilerContextData.nativePassData);
							num = reference.nativePassIndex;
						}
						continue;
					}
					global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit passBreakAudit = (graph.disablePassMerging ? new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.PassMergingDisabled, i) : global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData.TryMerge(contextData, num, i));
					if (passBreakAudit.reason != global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.Merged)
					{
						SetPassStatesForNativePass(num);
						if (passBreakAudit.reason == global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.NonRasterPass)
						{
							num = -1;
							continue;
						}
						compilerContextData.nativePassData.Add(new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData(ref reference, compilerContextData));
						reference.nativePassIndex = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.LastIndex(ref compilerContextData.nativePassData);
						num = reference.nativePassIndex;
					}
				}
				if (num >= 0)
				{
					SetPassStatesForNativePass(num);
				}
			}
		}

		private bool FindFirstPassIdOnGraphicsQueueAwaitingFenceGoingForward(ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData startAsyncPass, out int firstPassIdAwaiting)
		{
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData compilerContextData = contextData;
			firstPassIdAwaiting = startAsyncPass.awaitingMyGraphicsFencePassId;
			if (firstPassIdAwaiting == -1)
			{
				int num = startAsyncPass.passId + 1;
				int num2 = compilerContextData.passData.Length - 1;
				while (firstPassIdAwaiting == -1 && num <= num2)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref compilerContextData.passData.ElementAt(num);
					if (reference.asyncCompute && !reference.culled)
					{
						firstPassIdAwaiting = reference.awaitingMyGraphicsFencePassId;
					}
					num++;
				}
				if (num > num2)
				{
					firstPassIdAwaiting = num2;
					return false;
				}
			}
			return true;
		}

		private int FindFirstNonCulledPassIdGoingBackward(int startPassId, bool startPassIsIncluded)
		{
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData compilerContextData = contextData;
			int num = (startPassIsIncluded ? startPassId : global::System.Math.Max(0, startPassId - 1));
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref compilerContextData.passData.ElementAt(num);
			while (reference.culled && num > 0)
			{
				reference = ref compilerContextData.passData.ElementAt(--num);
			}
			return reference.passId;
		}

		private void FindResourceUsageRangeAndSynchronization()
		{
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData compilerContextData = contextData;
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.NativeCompilerProfileId.NRPRGComp_FindResourceUsageRanges)))
			{
				for (int i = 0; i < compilerContextData.passData.Length; i++)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref compilerContextData.passData.ElementAt(i);
					if (reference.culled)
					{
						continue;
					}
					ClearDelayedLastUseListAtPass(i);
					reference.waitOnGraphicsFencePassId = -1;
					reference.awaitingMyGraphicsFencePassId = -1;
					reference.insertGraphicsFence = false;
					global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData> readOnlySpan = reference.Inputs(compilerContextData);
					for (int j = 0; j < readOnlySpan.Length; j++)
					{
						global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h = readOnlySpan[j].resource;
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference2 = ref compilerContextData.UnversionedResourceData(in h);
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData reference3 = ref compilerContextData.VersionedResourceData(in h);
						reference2.lastUsePassID = -1;
						if (reference2.firstUsePassID < 0)
						{
							reference2.firstUsePassID = reference.passId;
							reference.AddFirstUse(in h, compilerContextData);
						}
						if (reference2.latestVersionNumber == h.version)
						{
							reference2.tag++;
						}
						if (reference3.written)
						{
							ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference4 = ref compilerContextData.passData.ElementAt(reference3.writePassId);
							if (reference4.asyncCompute != reference.asyncCompute)
							{
								int waitOnGraphicsFencePassId = reference.waitOnGraphicsFencePassId;
								reference.waitOnGraphicsFencePassId = global::System.Math.Max(reference4.passId, waitOnGraphicsFencePassId);
							}
						}
					}
					global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassOutputData> readOnlySpan2 = reference.Outputs(compilerContextData);
					for (int j = 0; j < readOnlySpan2.Length; j++)
					{
						global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h2 = readOnlySpan2[j].resource;
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference5 = ref compilerContextData.UnversionedResourceData(in h2);
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData reference6 = ref compilerContextData.VersionedResourceData(in h2);
						if (reference5.firstUsePassID < 0)
						{
							reference5.firstUsePassID = reference.passId;
							reference.AddFirstUse(in h2, compilerContextData);
						}
						if (reference5.latestVersionNumber == h2.version)
						{
							reference5.lastWritePassID = reference.passId;
						}
						int numReaders = reference6.numReaders;
						for (int k = 0; k < numReaders; k++)
						{
							int index = compilerContextData.resources.IndexReader(in h2, k);
							ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceReaderData reference7 = ref compilerContextData.resources.readerData[h2.iType].ElementAt(index);
							ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference8 = ref compilerContextData.passData.ElementAt(reference7.passId);
							if (reference.asyncCompute != reference8.asyncCompute)
							{
								reference.insertGraphicsFence = true;
								int awaitingMyGraphicsFencePassId = reference.awaitingMyGraphicsFencePassId;
								reference.awaitingMyGraphicsFencePassId = ((awaitingMyGraphicsFencePassId == -1) ? reference7.passId : global::System.Math.Min(awaitingMyGraphicsFencePassId, reference7.passId));
							}
						}
					}
				}
				for (int l = 0; l < compilerContextData.passData.Length; l++)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference9 = ref compilerContextData.passData.ElementAt(l);
					if (reference9.culled)
					{
						continue;
					}
					bool asyncCompute = reference9.asyncCompute;
					global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData> readOnlySpan = reference9.Inputs(compilerContextData);
					for (int j = 0; j < readOnlySpan.Length; j++)
					{
						global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h3 = readOnlySpan[j].resource;
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference10 = ref compilerContextData.UnversionedResourceData(in h3);
						if (reference10.latestVersionNumber != h3.version)
						{
							continue;
						}
						int num = reference10.tag - 1;
						if (num == 0)
						{
							if (asyncCompute)
							{
								int firstPassIdAwaiting;
								bool flag = FindFirstPassIdOnGraphicsQueueAwaitingFenceGoingForward(ref reference9, out firstPassIdAwaiting);
								AddDelayedLastUseToPass(in h3, reference10.lastUsePassID = FindFirstNonCulledPassIdGoingBackward(firstPassIdAwaiting, !flag));
							}
							else
							{
								reference10.lastUsePassID = reference9.passId;
								reference9.AddLastUse(in h3, compilerContextData);
							}
						}
						reference10.tag = num;
					}
					global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassOutputData> readOnlySpan2 = reference9.Outputs(compilerContextData);
					for (int j = 0; j < readOnlySpan2.Length; j++)
					{
						global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h4 = readOnlySpan2[j].resource;
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference11 = ref compilerContextData.UnversionedResourceData(in h4);
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData reference12 = ref compilerContextData.VersionedResourceData(in h4);
						if (reference11.latestVersionNumber == h4.version && reference12.numReaders == 0)
						{
							if (asyncCompute)
							{
								int firstPassIdAwaiting2;
								bool flag2 = FindFirstPassIdOnGraphicsQueueAwaitingFenceGoingForward(ref reference9, out firstPassIdAwaiting2);
								AddDelayedLastUseToPass(in h4, reference11.lastUsePassID = FindFirstNonCulledPassIdGoingBackward(firstPassIdAwaiting2, !flag2));
							}
							else
							{
								reference11.lastUsePassID = reference9.passId;
								reference9.AddLastUse(in h4, compilerContextData);
							}
						}
					}
					AddLastUseFromDelayedList(ref reference9);
				}
			}
		}

		private void ClearDelayedLastUseListAtPass(int passId)
		{
			if (m_DelayedLastUseListPerPassMap.TryGetValue(passId, out var value))
			{
				value.Clear();
			}
		}

		private void AddDelayedLastUseToPass(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle releaseResource, int passId)
		{
			if (!m_DelayedLastUseListPerPassMap.TryGetValue(passId, out var value))
			{
				value = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>();
				m_DelayedLastUseListPerPassMap.Add(passId, value);
			}
			value.Add(releaseResource);
		}

		public void AddLastUseFromDelayedList(ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData passData)
		{
			if (!m_DelayedLastUseListPerPassMap.TryGetValue(passData.passId, out var value))
			{
				return;
			}
			foreach (global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle item in value)
			{
				passData.AddLastUse(item, contextData);
			}
			value.Clear();
		}

		private void PrepareNativeRenderPasses()
		{
			for (int i = 0; i < contextData.nativePassData.Length; i++)
			{
				DetermineLoadStoreActions(ref contextData.nativePassData.ElementAt(i));
			}
		}

		private void PropagateTextureUVOrigin()
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.NativeCompilerProfileId.NRPRGComp_PropagateTextureUVOrigin)))
			{
				for (int num = contextData.nativePassData.Length - 1; num >= 0; num--)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData reference = ref contextData.nativePassData.ElementAt(num);
					int size = reference.attachments.size;
					int index = 0;
					global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection textureUVOriginSelection = global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown;
					for (int i = 0; i < size; i++)
					{
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassAttachment reference2 = ref reference.attachments[i];
						if (reference2.storeAction != global::UnityEngine.Rendering.RenderBufferStoreAction.DontCare && reference2.handle.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Texture)
						{
							textureUVOriginSelection = contextData.UnversionedResourceData(in reference2.handle).textureUVOrigin;
							index = i;
							break;
						}
					}
					for (int j = 0; j < size; j++)
					{
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassAttachment reference3 = ref reference.attachments[j];
						if (reference3.handle.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Texture)
						{
							ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference4 = ref contextData.UnversionedResourceData(in reference3.handle);
							if (textureUVOriginSelection != global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown && reference4.textureUVOrigin != global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.Unknown && reference4.textureUVOrigin != textureUVOriginSelection)
							{
								ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassAttachment reference5 = ref reference.attachments[index];
								string renderGraphResourceName = graph.m_ResourcesForDebugOnly.GetRenderGraphResourceName(in reference5.handle);
								string renderGraphResourceName2 = graph.m_ResourcesForDebugOnly.GetRenderGraphResourceName(in reference3.handle);
								throw new global::System.InvalidOperationException($"From pass '{contextData.passNames[reference.firstGraphPass]}' to pass '{contextData.passNames[reference.lastGraphPass]}' when trying to store resource '{renderGraphResourceName2}' of type {reference3.handle.type} at index {reference3.handle.index} - " + global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.RenderGraphExceptionMessages.IncompatibleTextureUVOriginStore(renderGraphResourceName, textureUVOriginSelection, renderGraphResourceName2, reference4.textureUVOrigin));
							}
							reference4.textureUVOrigin = textureUVOriginSelection;
						}
					}
				}
			}
		}

		private static bool IsGlobalTextureInPass(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass pass, in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle)
		{
			foreach (var setGlobals in pass.setGlobalsList)
			{
				if (setGlobals.Item1.handle.index == handle.index)
				{
					return true;
				}
			}
			return false;
		}

		private void DetectMemoryLessResources()
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.NativeCompilerProfileId.NRPRGComp_DetectMemorylessResources)))
			{
				if (!global::UnityEngine.SystemInfo.supportsMemorylessTextures)
				{
					return;
				}
				global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData.NativePassIterator enumerator = contextData.NativePasses.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData current = ref enumerator.Current;
					global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> actualPasses;
					global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> readOnlySpan = current.GraphPasses(contextData, out actualPasses);
					global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> readOnlySpan2 = readOnlySpan;
					for (int i = 0; i < readOnlySpan2.Length; i++)
					{
						ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref readOnlySpan2[i];
						global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> readOnlySpan3 = reference.FirstUsedResources(contextData);
						for (int j = 0; j < readOnlySpan3.Length; j++)
						{
							ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle reference2 = ref readOnlySpan3[j];
							ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference3 = ref contextData.UnversionedResourceData(in reference2);
							if (reference2.type != global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Texture || reference3.isImported)
							{
								continue;
							}
							bool flag = IsGlobalTextureInPass(graph.m_RenderPasses[reference.passId], in reference2);
							global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> readOnlySpan4 = readOnlySpan;
							for (int k = 0; k < readOnlySpan4.Length; k++)
							{
								ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference4 = ref readOnlySpan4[k];
								global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> readOnlySpan5 = reference4.LastUsedResources(contextData);
								for (int l = 0; l < readOnlySpan5.Length; l++)
								{
									ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle reference5 = ref readOnlySpan5[l];
									ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference6 = ref contextData.UnversionedResourceData(in reference5);
									if (reference5.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Texture && !reference6.isImported && reference2.index == reference5.index && !flag && (current.numNativeSubPasses > 1 || reference4.IsUsedAsFragment(in reference2, contextData)))
									{
										reference3.memoryLess = true;
										reference6.memoryLess = true;
									}
								}
							}
						}
					}
					if (actualPasses.IsCreated)
					{
						actualPasses.Dispose();
					}
				}
			}
		}

		internal static bool IsSameNativeSubPass(ref global::UnityEngine.Rendering.SubPassDescriptor a, ref global::UnityEngine.Rendering.SubPassDescriptor b)
		{
			global::UnityEngine.Rendering.SubPassFlags num = a.flags & ~(global::UnityEngine.Rendering.SubPassFlags.TileProperties | global::UnityEngine.Rendering.SubPassFlags.MultiviewRenderRegionsCompatible);
			global::UnityEngine.Rendering.SubPassFlags subPassFlags = b.flags & ~(global::UnityEngine.Rendering.SubPassFlags.TileProperties | global::UnityEngine.Rendering.SubPassFlags.MultiviewRenderRegionsCompatible);
			if (num != subPassFlags || a.colorOutputs.Length != b.colorOutputs.Length || a.inputs.Length != b.inputs.Length)
			{
				return false;
			}
			for (int i = 0; i < a.colorOutputs.Length; i++)
			{
				if (a.colorOutputs[i] != b.colorOutputs[i])
				{
					return false;
				}
			}
			for (int j = 0; j < a.inputs.Length; j++)
			{
				if (a.inputs[j] != b.inputs[j])
				{
					return false;
				}
			}
			return true;
		}

		private bool ExecuteInitializeResource(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, in global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData pass)
		{
			bool flag = false;
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.NativeCompilerProfileId.NRPRGComp_ExecuteInitializeResources)))
			{
				resources.forceManualClearOfResource = true;
				if (pass.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Raster && pass.nativePassIndex >= 0)
				{
					if (pass.mergeState == global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.Begin || pass.mergeState == global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.None)
					{
						global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> actualPasses;
						global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> readOnlySpan = contextData.nativePassData.ElementAt(pass.nativePassIndex).GraphPasses(contextData, out actualPasses);
						for (int i = 0; i < readOnlySpan.Length; i++)
						{
							ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref readOnlySpan[i];
							global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> readOnlySpan2 = reference.FirstUsedResources(contextData);
							for (int j = 0; j < readOnlySpan2.Length; j++)
							{
								ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle reference2 = ref readOnlySpan2[j];
								ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference3 = ref contextData.UnversionedResourceData(in reference2);
								bool flag2 = reference.IsUsedAsFragment(in reference2, contextData);
								resources.forceManualClearOfResource = !flag2;
								if (!reference3.isImported)
								{
									if (reference3.memoryLess)
									{
										resources.SetTextureAsMemoryLess(in reference2);
									}
									flag |= resources.CreatePooledResource(rgContext, reference2.iType, reference2.index);
								}
								else if (reference3.clear && !reference3.memoryLess && resources.forceManualClearOfResource)
								{
									flag |= resources.ClearResource(rgContext, reference2.iType, reference2.index);
								}
							}
						}
						if (actualPasses.IsCreated)
						{
							actualPasses.Dispose();
						}
					}
				}
				else
				{
					global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> readOnlySpan2 = pass.FirstUsedResources(contextData);
					for (int i = 0; i < readOnlySpan2.Length; i++)
					{
						ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle reference4 = ref readOnlySpan2[i];
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference5 = ref contextData.UnversionedResourceData(in reference4);
						if (!reference5.isImported)
						{
							flag |= resources.CreatePooledResource(rgContext, reference4.iType, reference4.index);
						}
						else if (reference5.clear)
						{
							flag |= resources.ClearResource(rgContext, reference4.iType, reference4.index);
						}
					}
				}
				resources.forceManualClearOfResource = true;
				return flag;
			}
		}

		private void DetermineLoadStoreActions(ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData nativePass)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.NativeCompilerProfileId.NRPRGComp_PrepareNativePass)))
			{
				contextData.passData.ElementAt(nativePass.firstGraphPass);
				contextData.passData.ElementAt(nativePass.lastGraphPass);
				if (nativePass.fragments.size <= 0)
				{
					return;
				}
				ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> fragments = ref nativePass.fragments;
				int num = 0;
				while (true)
				{
					int num2 = num;
					global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> fixedAttachmentArray = fragments;
					if (num2 >= fixedAttachmentArray.size)
					{
						break;
					}
					fixedAttachmentArray = fragments;
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference = ref fixedAttachmentArray[num];
					global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle = reference.resource;
					bool memoryless = false;
					int mipLevel = reference.mipLevel;
					int depthSlice = reference.depthSlice;
					global::UnityEngine.Rendering.RenderBufferLoadAction loadAction = global::UnityEngine.Rendering.RenderBufferLoadAction.DontCare;
					global::UnityEngine.Rendering.RenderBufferStoreAction storeAction = global::UnityEngine.Rendering.RenderBufferStoreAction.DontCare;
					bool flag = reference.accessFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write) && !reference.accessFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Discard);
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference2 = ref contextData.UnversionedResourceData(in reference.resource);
					bool isImported = reference2.isImported;
					int lastUsePassID = reference2.lastUsePassID;
					bool flag2 = lastUsePassID >= nativePass.lastGraphPass + 1;
					if (reference.accessFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read) || flag)
					{
						if (reference2.firstUsePassID >= nativePass.firstGraphPass)
						{
							loadAction = ((!isImported) ? global::UnityEngine.Rendering.RenderBufferLoadAction.Clear : (reference2.clear ? global::UnityEngine.Rendering.RenderBufferLoadAction.Clear : global::UnityEngine.Rendering.RenderBufferLoadAction.Load));
						}
						else
						{
							loadAction = global::UnityEngine.Rendering.RenderBufferLoadAction.Load;
							if (flag2)
							{
								storeAction = global::UnityEngine.Rendering.RenderBufferStoreAction.Store;
							}
						}
					}
					if (reference.accessFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write))
					{
						if (nativePass.samples <= 1)
						{
							storeAction = ((!flag2) ? ((!isImported) ? global::UnityEngine.Rendering.RenderBufferStoreAction.DontCare : (reference2.discard ? global::UnityEngine.Rendering.RenderBufferStoreAction.DontCare : global::UnityEngine.Rendering.RenderBufferStoreAction.Store)) : global::UnityEngine.Rendering.RenderBufferStoreAction.Store);
						}
						else
						{
							storeAction = global::UnityEngine.Rendering.RenderBufferStoreAction.DontCare;
							bool flag3 = reference2.latestVersionNumber == reference.resource.version;
							bool flag4 = isImported && flag3;
							if (lastUsePassID >= nativePass.firstGraphPass + nativePass.numGraphPasses)
							{
								bool flag5 = flag4 && !reference2.discard;
								bool flag6 = flag4 && !reference2.bindMS;
								global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceReaderData> readOnlySpan = contextData.Readers(in reference.resource);
								for (int i = 0; i < readOnlySpan.Length; i++)
								{
									ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceReaderData reference3 = ref readOnlySpan[i];
									ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference4 = ref contextData.passData.ElementAt(reference3.passId);
									bool flag7 = reference4.IsUsedAsFragment(in reference.resource, contextData);
									if (reference4.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Unsafe)
									{
										flag5 = true;
										flag6 = !reference2.bindMS;
										break;
									}
									if (flag7)
									{
										flag5 = true;
									}
									else if (reference2.bindMS)
									{
										flag5 = true;
									}
									else
									{
										flag6 = true;
									}
								}
								if (flag5 && flag6)
								{
									storeAction = global::UnityEngine.Rendering.RenderBufferStoreAction.StoreAndResolve;
								}
								else if (flag6)
								{
									storeAction = global::UnityEngine.Rendering.RenderBufferStoreAction.Resolve;
								}
								else if (flag5)
								{
									storeAction = global::UnityEngine.Rendering.RenderBufferStoreAction.Store;
								}
							}
							else if (flag4)
							{
								storeAction = (reference2.bindMS ? (reference2.discard ? global::UnityEngine.Rendering.RenderBufferStoreAction.DontCare : global::UnityEngine.Rendering.RenderBufferStoreAction.Store) : ((!reference2.discard) ? global::UnityEngine.Rendering.RenderBufferStoreAction.StoreAndResolve : ((!nativePass.hasDepth || nativePass.attachments.size != 0) ? global::UnityEngine.Rendering.RenderBufferStoreAction.Resolve : global::UnityEngine.Rendering.RenderBufferStoreAction.DontCare)));
							}
						}
					}
					if (reference2.memoryLess)
					{
						memoryless = true;
					}
					global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassAttachment data = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassAttachment(in handle, loadAction, storeAction, memoryless, mipLevel, depthSlice);
					nativePass.attachments.Add(in data);
					num++;
				}
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void ValidateNativePass(in global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData nativePass, int width, int height, int depth, int samples, int attachmentCount)
		{
			if (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				if (nativePass.attachments.size == 0 || nativePass.numNativeSubPasses == 0)
				{
					throw new global::System.Exception("Empty render pass");
				}
				if (width == 0 || height == 0 || depth == 0 || samples == 0 || nativePass.numNativeSubPasses == 0 || attachmentCount == 0)
				{
					throw new global::System.Exception("Invalid render pass properties. One or more properties are zero.");
				}
			}
		}

		[global::System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		private void ValidateAttachment(in global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo attRenderTargetInfo, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, int nativePassWidth, int nativePassHeight, int nativePassMSAASamples, bool isVrs, bool isShaderResolve)
		{
			if (!global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks)
			{
				return;
			}
			if (isVrs)
			{
				global::UnityEngine.Vector2Int allocTileSize = global::UnityEngine.Rendering.ShadingRateImage.GetAllocTileSize(nativePassWidth, nativePassHeight);
				if (attRenderTargetInfo.width != allocTileSize.x || attRenderTargetInfo.height != allocTileSize.y || attRenderTargetInfo.msaaSamples != 1)
				{
					throw new global::System.Exception("Low level rendergraph error: Shading rate image attachment in renderpass does not match.");
				}
			}
			else if (attRenderTargetInfo.width != nativePassWidth || attRenderTargetInfo.height != nativePassHeight || (attRenderTargetInfo.msaaSamples != nativePassMSAASamples && !isShaderResolve))
			{
				throw new global::System.Exception("Low level rendergraph error: Attachments in renderpass do not match.");
			}
		}

		internal unsafe void ExecuteBeginRenderPass(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData nativePass)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.NativeCompilerProfileId.NRPRGComp_ExecuteBeginRenderpassCommand)))
			{
				ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassAttachment> attachments = ref nativePass.attachments;
				int size = attachments.size;
				int width = nativePass.width;
				int height = nativePass.height;
				int volumeDepth = nativePass.volumeDepth;
				int samples = nativePass.samples;
				nativePass.extendedFeatureFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultisampledShaderResolve);
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SubPassDescriptor> subPasses = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::UnityEngine.Rendering.SubPassDescriptor>(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafeReadOnlyPtr(contextData.nativeSubPassData) + nativePass.firstNativeSubPass, nativePass.numNativeSubPasses, global::Unity.Collections.Allocator.None);
				if (nativePass.hasFoveatedRasterization)
				{
					rgContext.cmd.SetFoveatedRenderingMode(global::UnityEngine.Rendering.FoveatedRenderingMode.Enabled);
				}
				if (nativePass.hasShadingRateStates)
				{
					rgContext.cmd.SetShadingRateFragmentSize(nativePass.shadingRateFragmentSize);
					rgContext.cmd.SetShadingRateCombiner(global::UnityEngine.Rendering.ShadingRateCombinerStage.Primitive, nativePass.primitiveShadingRateCombiner);
					rgContext.cmd.SetShadingRateCombiner(global::UnityEngine.Rendering.ShadingRateCombinerStage.Fragment, nativePass.fragmentShadingRateCombiner);
				}
				if (!m_BeginRenderPassAttachments.IsCreated)
				{
					m_BeginRenderPassAttachments = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.AttachmentDescriptor>(8, global::Unity.Collections.Allocator.Persistent);
				}
				m_BeginRenderPassAttachments.Resize(size, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				for (int i = 0; i < size; i++)
				{
					ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle handle = ref attachments[i].handle;
					resources.GetRenderTargetInfo(in handle, out var outInfo);
					ref global::UnityEngine.Rendering.AttachmentDescriptor reference = ref m_BeginRenderPassAttachments.ElementAt(i);
					reference = new global::UnityEngine.Rendering.AttachmentDescriptor(outInfo.format);
					global::UnityEngine.Rendering.RTHandle texture = resources.GetTexture(handle.index);
					global::UnityEngine.Rendering.RenderTargetIdentifier renderTargetIdentifier = texture;
					reference.loadStoreTarget = new global::UnityEngine.Rendering.RenderTargetIdentifier(renderTargetIdentifier, attachments[i].mipLevel, global::UnityEngine.CubemapFace.Unknown, attachments[i].depthSlice);
					if (attachments[i].storeAction == global::UnityEngine.Rendering.RenderBufferStoreAction.Resolve || attachments[i].storeAction == global::UnityEngine.Rendering.RenderBufferStoreAction.StoreAndResolve)
					{
						reference.resolveTarget = texture;
					}
					reference.loadAction = attachments[i].loadAction;
					reference.storeAction = attachments[i].storeAction;
					if (attachments[i].loadAction == global::UnityEngine.Rendering.RenderBufferLoadAction.Clear)
					{
						reference.clearColor = global::UnityEngine.Color.red;
						reference.clearDepth = 1f;
						reference.clearStencil = 0u;
						ref readonly global::UnityEngine.Rendering.RenderGraphModule.TextureDesc textureResourceDesc = ref resources.GetTextureResourceDesc(in handle, noThrowOnInvalidDesc: true);
						if (i == 0 && nativePass.hasDepth)
						{
							reference.clearDepth = 1f;
						}
						else
						{
							reference.clearColor = textureResourceDesc.clearColor;
						}
					}
				}
				if (nativePass.extendedFeatureFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultisampledShaderResolve))
				{
					global::UnityEngine.Rendering.SubPassDescriptor subPassDescriptor = subPasses[subPasses.Length - 1];
					for (int j = 0; j < subPassDescriptor.inputs.Length; j++)
					{
						int index = subPassDescriptor.inputs[j];
						if (m_BeginRenderPassAttachments.ElementAt(index).storeAction != global::UnityEngine.Rendering.RenderBufferStoreAction.DontCare)
						{
							throw new global::System.Exception("Low level rendergraph error: last subpass with shader resolve must have all input attachments as memoryless attachments.");
						}
					}
					if (subPassDescriptor.colorOutputs.Length != 1)
					{
						throw new global::System.Exception("Low level rendergraph error: last subpass with shader resolve must have one color attachment.");
					}
					if (global::UnityEngine.SystemInfo.supportsMultisampledShaderResolve)
					{
						int index2 = subPassDescriptor.colorOutputs[0];
						ref global::UnityEngine.Rendering.AttachmentDescriptor reference2 = ref m_BeginRenderPassAttachments.ElementAt(index2);
						reference2.resolveTarget = reference2.loadStoreTarget;
						reference2.loadStoreTarget = new global::UnityEngine.Rendering.RenderTargetIdentifier(global::UnityEngine.Rendering.BuiltinRenderTextureType.None);
						reference2.storeAction = global::UnityEngine.Rendering.RenderBufferStoreAction.Store;
					}
				}
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.AttachmentDescriptor> attachments2 = m_BeginRenderPassAttachments.AsArray();
				int depthAttachmentIndex = ((!nativePass.hasDepth) ? (-1) : 0);
				global::System.ReadOnlySpan<byte> empty = global::System.ReadOnlySpan<byte>.Empty;
				rgContext.cmd.BeginRenderPass(width, height, volumeDepth, samples, attachments2, depthAttachmentIndex, nativePass.shadingRateImageIndex, subPasses, empty);
				global::UnityEngine.Rendering.CommandBuffer.ThrowOnSetRenderTarget = true;
			}
		}

		private void ExecuteDestroyResource(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData pass)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.NativeCompilerProfileId.NRPRGComp_ExecuteDestroyResources)))
			{
				rgContext.renderGraphPool.ReleaseAllTempAlloc();
				global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> readOnlySpan2;
				if (pass.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Raster && pass.nativePassIndex >= 0)
				{
					if (pass.mergeState != global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.End && pass.mergeState != global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.None)
					{
						return;
					}
					global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> actualPasses;
					global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> readOnlySpan = contextData.nativePassData.ElementAt(pass.nativePassIndex).GraphPasses(contextData, out actualPasses);
					for (int i = 0; i < readOnlySpan.Length; i++)
					{
						readOnlySpan2 = readOnlySpan[i].LastUsedResources(contextData);
						for (int j = 0; j < readOnlySpan2.Length; j++)
						{
							ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle reference = ref readOnlySpan2[j];
							if (!contextData.UnversionedResourceData(in reference).isImported)
							{
								resources.ReleasePooledResource(rgContext, reference.iType, reference.index);
							}
						}
					}
					if (actualPasses.IsCreated)
					{
						actualPasses.Dispose();
					}
					return;
				}
				readOnlySpan2 = pass.LastUsedResources(contextData);
				for (int i = 0; i < readOnlySpan2.Length; i++)
				{
					ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle reference2 = ref readOnlySpan2[i];
					if (!contextData.UnversionedResourceData(in reference2).isImported)
					{
						resources.ReleasePooledResource(rgContext, reference2.iType, reference2.index);
					}
				}
			}
		}

		private void ExecuteSetRenderTargets(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass pass, global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext)
		{
			bool flag = pass.depthAccess.textureHandle.IsValid();
			if (!flag && pass.colorBufferMaxIndex == -1)
			{
				return;
			}
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resourcesForDebugOnly = graph.m_ResourcesForDebugOnly;
			global::UnityEngine.Rendering.RenderGraphModule.TextureAccess[] colorBufferAccess = pass.colorBufferAccess;
			if (pass.colorBufferMaxIndex > 0)
			{
				global::UnityEngine.Rendering.RenderTargetIdentifier[] array = m_TempMRTArrays[pass.colorBufferMaxIndex];
				for (int i = 0; i <= pass.colorBufferMaxIndex; i++)
				{
					array[i] = resourcesForDebugOnly.GetTexture(in colorBufferAccess[i].textureHandle);
				}
				if (!flag)
				{
					throw new global::System.InvalidOperationException("In pass " + pass.name + " - Setting multiple render textures (MRTs) without a depth buffer is not supported.");
				}
				global::UnityEngine.Rendering.CommandBuffer cmd = rgContext.cmd;
				global::UnityEngine.Rendering.RenderGraphModule.TextureAccess depthAccess = pass.depthAccess;
				global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, array, resourcesForDebugOnly.GetTexture(in depthAccess.textureHandle));
			}
			else if (flag)
			{
				if (pass.colorBufferMaxIndex > -1)
				{
					global::UnityEngine.Rendering.CommandBuffer cmd2 = rgContext.cmd;
					global::UnityEngine.Rendering.RTHandle texture = resourcesForDebugOnly.GetTexture(in pass.colorBufferAccess[0].textureHandle);
					global::UnityEngine.Rendering.RenderGraphModule.TextureAccess depthAccess = pass.depthAccess;
					global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd2, texture, resourcesForDebugOnly.GetTexture(in depthAccess.textureHandle));
				}
				else
				{
					global::UnityEngine.Rendering.CommandBuffer cmd3 = rgContext.cmd;
					global::UnityEngine.Rendering.RenderGraphModule.TextureAccess depthAccess = pass.depthAccess;
					global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd3, resourcesForDebugOnly.GetTexture(in depthAccess.textureHandle));
				}
			}
			else
			{
				if (!pass.colorBufferAccess[0].textureHandle.IsValid())
				{
					throw new global::System.InvalidOperationException("In pass " + pass.name + " - Neither depth nor color render targets are correctly set up.");
				}
				global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(rgContext.cmd, resourcesForDebugOnly.GetTexture(in pass.colorBufferAccess[0].textureHandle));
			}
		}

		internal void ExecuteSetRandomWriteTarget(in global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, int index, in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource, bool preserveCounterValue = true)
		{
			if (resource.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Texture)
			{
				global::UnityEngine.Rendering.RTHandle texture = resources.GetTexture(resource.index);
				cmd.SetRandomWriteTarget(index, texture);
				return;
			}
			if (resource.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Buffer)
			{
				global::UnityEngine.GraphicsBuffer buffer = resources.GetBuffer(resource.index);
				if (preserveCounterValue)
				{
					cmd.SetRandomWriteTarget(index, buffer);
				}
				else
				{
					cmd.SetRandomWriteTarget(index, buffer, preserveCounterValue: false);
				}
				return;
			}
			string renderGraphResourceName = resources.GetRenderGraphResourceName(in resource);
			throw new global::System.Exception($"When trying to use resource '{renderGraphResourceName}' of type {resource.type} - " + "Invalid resource type, expected texture or buffer");
		}

		internal void ExecuteRenderGraphPass(ref global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass pass)
		{
			rgContext.executingPass = pass;
			if (!pass.HasRenderFunc())
			{
				throw new global::System.InvalidOperationException("In pass " + pass.name + " - RenderPass was not provided with an execute function.");
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(rgContext.cmd, pass.customSampler))
			{
				pass.Execute(rgContext);
				foreach (var setGlobals in pass.setGlobalsList)
				{
					rgContext.cmd.SetGlobalTexture(setGlobals.Item2, setGlobals.Item1);
				}
			}
		}

		public void ExecuteGraph(global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, in global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass> passes)
		{
			bool inRenderPass = false;
			previousCommandBuffer = rgContext.cmd;
			rgContext.cmd.ClearRandomWriteTargets();
			for (int i = 0; i < contextData.passData.Length; i++)
			{
				ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref contextData.passData.ElementAt(i);
				if (reference.culled)
				{
					continue;
				}
				bool nrpBegan = false;
				bool flag = ExecuteInitializeResource(rgContext, resources, in reference);
				if (reference.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Compute && reference.asyncCompute)
				{
					global::UnityEngine.Rendering.GraphicsFence fence = default(global::UnityEngine.Rendering.GraphicsFence);
					if (flag)
					{
						fence = rgContext.cmd.CreateGraphicsFence(global::UnityEngine.Rendering.GraphicsFenceType.AsyncQueueSynchronisation, global::UnityEngine.Rendering.SynchronisationStageFlags.AllGPUOperations);
					}
					if (!rgContext.contextlessTesting)
					{
						rgContext.renderContext.ExecuteCommandBuffer(rgContext.cmd);
					}
					rgContext.cmd.Clear();
					global::UnityEngine.Rendering.CommandBuffer commandBuffer = global::UnityEngine.Rendering.CommandBufferPool.Get("async cmd");
					commandBuffer.SetExecutionFlags(global::UnityEngine.Rendering.CommandBufferExecutionFlags.AsyncCompute);
					rgContext.cmd = commandBuffer;
					if (flag)
					{
						rgContext.cmd.WaitOnAsyncGraphicsFence(fence, global::UnityEngine.Rendering.SynchronisationStageFlags.PixelProcessing);
					}
				}
				if (reference.waitOnGraphicsFencePassId != -1)
				{
					rgContext.cmd.WaitOnAsyncGraphicsFence(contextData.fences[reference.waitOnGraphicsFencePassId], global::UnityEngine.Rendering.SynchronisationStageFlags.PixelProcessing);
				}
				if (reference.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Raster && reference.mergeState <= global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.Begin)
				{
					if (reference.nativePassIndex >= 0)
					{
						ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData reference2 = ref contextData.nativePassData.ElementAt(reference.nativePassIndex);
						if (reference2.fragments.size > 0)
						{
							ExecuteBeginRenderPass(rgContext, resources, ref reference2);
							nrpBegan = true;
							inRenderPass = true;
						}
					}
				}
				else if (reference.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Unsafe)
				{
					ExecuteSetRenderTargets(passes[i], rgContext);
				}
				if (reference.mergeState >= global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.SubPass && reference.beginNativeSubpass)
				{
					if (!inRenderPass)
					{
						throw new global::System.Exception("Compiler error: Pass is marked as beginning a native sub pass but no pass is currently active.");
					}
					rgContext.cmd.NextSubPass();
				}
				if (reference.numRandomAccessResources > 0)
				{
					global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassRandomWriteData> readOnlySpan = reference.RandomWriteTextures(contextData);
					for (int j = 0; j < readOnlySpan.Length; j++)
					{
						ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassRandomWriteData reference3 = ref readOnlySpan[j];
						ExecuteSetRandomWriteTarget(in rgContext.cmd, resources, reference3.index, in reference3.resource);
					}
				}
				ExecuteRenderGraphPass(ref rgContext, resources, passes[reference.passId]);
				EndRenderGraphPass(ref rgContext, ref reference, ref inRenderPass, resources, nrpBegan);
			}
		}

		private void EndRenderGraphPass(ref global::UnityEngine.Rendering.RenderGraphModule.InternalRenderGraphContext rgContext, ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData passData, ref bool inRenderPass, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, bool nrpBegan)
		{
			if (passData.numRandomAccessResources > 0)
			{
				rgContext.cmd.ClearRandomWriteTargets();
			}
			if (passData.insertGraphicsFence)
			{
				global::UnityEngine.Rendering.GraphicsFence value = rgContext.cmd.CreateAsyncGraphicsFence();
				contextData.fences[passData.passId] = value;
			}
			if (passData.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Raster)
			{
				if (((passData.mergeState == global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.None && nrpBegan) || passData.mergeState == global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.End) && passData.nativePassIndex >= 0)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData reference = ref contextData.nativePassData.ElementAt(passData.nativePassIndex);
					if (reference.fragments.size > 0)
					{
						if (!inRenderPass)
						{
							throw new global::System.Exception("Compiler error: Generated a subpass pass but no pass is currently active.");
						}
						if (reference.hasFoveatedRasterization)
						{
							rgContext.cmd.SetFoveatedRenderingMode(global::UnityEngine.Rendering.FoveatedRenderingMode.Disabled);
						}
						rgContext.cmd.EndRenderPass();
						global::UnityEngine.Rendering.CommandBuffer.ThrowOnSetRenderTarget = false;
						inRenderPass = false;
						if (reference.hasShadingRateStates || reference.hasShadingRateImage)
						{
							rgContext.cmd.ResetShadingRate();
						}
					}
				}
			}
			else if (passData.type == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Compute && passData.asyncCompute)
			{
				rgContext.renderContext.ExecuteCommandBufferAsync(rgContext.cmd, global::UnityEngine.Rendering.ComputeQueueType.Background);
				global::UnityEngine.Rendering.CommandBufferPool.Release(rgContext.cmd);
				rgContext.cmd = previousCommandBuffer;
			}
			ExecuteDestroyResource(rgContext, resources, ref passData);
		}

		private static global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.AttachmentInfo MakeAttachmentInfo(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, in global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData nativePass, int attachmentIndex)
		{
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassAttachment att = nativePass.attachments[attachmentIndex];
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData resourceUnversionedData = ctx.UnversionedResourceData(in att.handle);
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.LoadAudit loadAudit = nativePass.loadAudit[attachmentIndex];
			string text = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.LoadAudit.LoadReasonMessages[(int)loadAudit.reason];
			if (loadAudit.passId >= 0)
			{
				text = text.Replace("{pass}", "<b>" + ctx.passNames[loadAudit.passId].name + "</b>");
			}
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.StoreAudit storeAudit = nativePass.storeAudit[attachmentIndex];
			string text2 = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.StoreAudit.StoreReasonMessages[(int)storeAudit.reason];
			if (storeAudit.passId >= 0)
			{
				text2 = text2.Replace("{pass}", "<b>" + ctx.passNames[storeAudit.passId].name + "</b>");
			}
			string text3 = string.Empty;
			if (storeAudit.msaaReason != global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.StoreReason.InvalidReason && storeAudit.msaaReason != global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.StoreReason.NoMSAABuffer)
			{
				text3 = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.StoreAudit.StoreReasonMessages[(int)storeAudit.msaaReason];
				if (storeAudit.msaaPassId >= 0)
				{
					text3 = text3.Replace("{pass}", "<b>" + ctx.passNames[storeAudit.msaaPassId].name + "</b>");
				}
			}
			return new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.AttachmentInfo
			{
				resourceName = resourceUnversionedData.GetName(ctx, in att.handle),
				attachmentIndex = attachmentIndex,
				loadReason = text,
				storeReason = text2,
				storeMsaaReason = text3,
				attachment = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.SerializableNativePassAttachment(att)
			};
		}

		internal static string MakePassBreakInfoMessage(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, in global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData nativePass)
		{
			string text = "";
			if (nativePass.breakAudit.breakPass >= 0)
			{
				text = text + "Failed to merge " + ctx.passNames[nativePass.breakAudit.breakPass].name + " into this native pass.\n";
			}
			return text + global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit.BreakReasonMessages[(int)nativePass.breakAudit.reason];
		}

		internal static string MakePassMergeMessage(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, in global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData pass, in global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData prevPass, in global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit mergeResult)
		{
			string text = ((mergeResult.reason == global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.Merged) ? "The passes are <b>compatible</b> to be merged.\n\n" : "The passes are <b>incompatible</b> to be merged.\n\n");
			string text2 = InjectSpaces(pass.GetName(ctx).name);
			string text3 = InjectSpaces(prevPass.GetName(ctx).name);
			switch (mergeResult.reason)
			{
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.Merged:
				if (pass.nativePassIndex == prevPass.nativePassIndex && pass.mergeState != global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.None)
				{
					return text + "Passes are merged.";
				}
				return text + "Passes can be merged but are not recorded consecutively.";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.TargetSizeMismatch:
				return text + "The fragment attachments of the passes have different sizes or sample counts.\n" + $"- {text3}: {prevPass.fragmentInfoWidth}x{prevPass.fragmentInfoHeight}, {prevPass.fragmentInfoSamples} sample(s).\n" + $"- {text2}: {pass.fragmentInfoWidth}x{pass.fragmentInfoHeight}, {pass.fragmentInfoSamples} sample(s).";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.NextPassReadsTexture:
				return text + text3 + " output is sampled by " + text2 + " as a regular texture, the pass needs to break.";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.NextPassTargetsTexture:
				return text + text3 + " reads a texture that " + text2 + " targets to, the pass needs to break.";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.NonRasterPass:
				return text + $"{text3} is type {prevPass.type}. Only Raster passes can be merged.";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.DifferentDepthTextures:
				return text + text3 + " uses a different depth buffer than " + text2 + ".";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.AttachmentLimitReached:
				return text + $"Merging the passes would use more than {8} attachments.";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.SubPassLimitReached:
				return text + $"Merging the passes would use more than {8} native subpasses.";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.EndOfGraph:
				return text + "The pass is the last pass in the graph.";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.DifferentShadingRateImages:
				return text + text3 + " uses a different shading rate image than " + text2 + ".";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.DifferentShadingRateStates:
				return text + text3 + " uses different shading rate states than " + text2 + ".";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.MultisampledShaderResolveMustBeLastPass:
				return text + text3 + " uses multisampled shader resolve and so can't have any more passes merged into it.";
			case global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.PassMergingDisabled:
				return text + "The pass merging is disabled.";
			default:
				throw new global::System.ArgumentOutOfRangeException();
			}
		}

		private static string InjectSpaces(string camelCaseString)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			for (int i = 0; i < camelCaseString.Length; i++)
			{
				if (char.IsUpper(camelCaseString[i]) && i != 0 && char.IsLower(camelCaseString[i - 1]))
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(camelCaseString[i]);
			}
			return stringBuilder.ToString();
		}

		internal void GenerateNativeCompilerDebugData(ref global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData debugData)
		{
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData reference = ref contextData;
			debugData.isNRPCompiler = true;
			global::System.Collections.Generic.Dictionary<(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType, int), global::System.Collections.Generic.List<int>> dictionary = new global::System.Collections.Generic.Dictionary<(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType, int), global::System.Collections.Generic.List<int>>();
			global::System.Collections.Generic.Dictionary<(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType, int), global::System.Collections.Generic.List<int>> dictionary2 = new global::System.Collections.Generic.Dictionary<(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType, int), global::System.Collections.Generic.List<int>>();
			foreach (global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass renderPass in graph.m_RenderPasses)
			{
				for (int i = 0; i < 3; i++)
				{
					int length = reference.resources.unversionedData[i].Length;
					for (int j = 0; j < length; j++)
					{
						foreach (global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle item3 in renderPass.resourceReadLists[i])
						{
							if (!renderPass.implicitReadsList.Contains(item3) && item3.type == (global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)i && item3.index == j)
							{
								(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType, int) key = ((global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)i, j);
								if (!dictionary.ContainsKey(key))
								{
									dictionary[key] = new global::System.Collections.Generic.List<int>();
								}
								dictionary[key].Add(renderPass.index);
							}
						}
						foreach (global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle item4 in renderPass.resourceWriteLists[i])
						{
							if (item4.type == (global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)i && item4.index == j)
							{
								(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType, int) key2 = ((global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)i, j);
								if (!dictionary2.ContainsKey(key2))
								{
									dictionary2[key2] = new global::System.Collections.Generic.List<int>();
								}
								dictionary2[key2].Add(renderPass.index);
							}
						}
						foreach (global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle item5 in renderPass.transientResourceList[i])
						{
							if (item5.type == (global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)i && item5.index == j)
							{
								(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType, int) key3 = ((global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)i, j);
								if (!dictionary.ContainsKey(key3))
								{
									dictionary[key3] = new global::System.Collections.Generic.List<int>();
								}
								dictionary[key3].Add(renderPass.index);
								if (!dictionary2.ContainsKey(key3))
								{
									dictionary2[key3] = new global::System.Collections.Generic.List<int>();
								}
								dictionary2[key3].Add(renderPass.index);
							}
						}
					}
				}
			}
			for (int k = 0; k < 3; k++)
			{
				int length2 = reference.resources.unversionedData[k].Length;
				for (int l = 0; l < length2; l++)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference2 = ref reference.resources.unversionedData[k].ElementAt(l);
					global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.ResourceData item = default(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.ResourceData);
					global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType renderGraphResourceType = (global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)k;
					bool flag = l == 0;
					if (!flag)
					{
						string name = reference.resources.resourceNames[k][l].name;
						item.name = ((!string.IsNullOrEmpty(name)) ? name : "(unnamed)");
						item.imported = reference2.isImported;
					}
					else
					{
						item.name = "<null>";
						item.imported = true;
					}
					global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo outInfo = default(global::UnityEngine.Rendering.RenderGraphModule.RenderTargetInfo);
					if (renderGraphResourceType == global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Texture && !flag)
					{
						global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res = new global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle(l, renderGraphResourceType, shared: false);
						try
						{
							graph.m_ResourcesForDebugOnly.GetRenderTargetInfo(in res, out outInfo);
						}
						catch (global::System.Exception)
						{
						}
					}
					item.creationPassIndex = reference2.firstUsePassID;
					item.releasePassIndex = reference2.lastUsePassID;
					item.textureData = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.TextureResourceData();
					item.textureData.width = reference2.width;
					item.textureData.height = reference2.height;
					item.textureData.depth = reference2.volumeDepth;
					item.textureData.samples = reference2.msaaSamples;
					item.textureData.format = outInfo.format;
					item.textureData.bindMS = reference2.bindMS;
					item.textureData.clearBuffer = reference2.clear;
					item.memoryless = reference2.memoryLess;
					item.consumerList = new global::System.Collections.Generic.List<int>();
					item.producerList = new global::System.Collections.Generic.List<int>();
					if (dictionary.ContainsKey(((global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)k, l)))
					{
						item.consumerList = dictionary[((global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)k, l)];
					}
					if (dictionary2.ContainsKey(((global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)k, l)))
					{
						item.producerList = dictionary2[((global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)k, l)];
					}
					debugData.resourceLists[k].Add(item);
				}
			}
			for (int m = 0; m < reference.passData.Length; m++)
			{
				global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass renderGraphPass = graph.m_RenderPasses[m];
				ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference3 = ref reference.passData.ElementAt(m);
				string name2 = InjectSpaces(reference3.GetName(reference).name);
				global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData item2 = default(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData);
				item2.name = name2;
				item2.type = reference3.type;
				item2.culled = reference3.culled;
				item2.async = reference3.asyncCompute;
				item2.nativeSubPassIndex = reference3.nativeSubPassIndex;
				item2.generateDebugData = renderGraphPass.generateDebugData;
				item2.resourceReadLists = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.ResourceIdLists();
				item2.resourceWriteLists = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.ResourceIdLists();
				item2.syncFromPassIndex = reference3.awaitingMyGraphicsFencePassId;
				item2.syncToPassIndex = reference3.waitOnGraphicsFencePassId;
				item2.nrpInfo = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.NRPInfo();
				item2.nrpInfo.width = reference3.fragmentInfoWidth;
				item2.nrpInfo.height = reference3.fragmentInfoHeight;
				item2.nrpInfo.volumeDepth = reference3.fragmentInfoVolumeDepth;
				item2.nrpInfo.samples = reference3.fragmentInfoSamples;
				item2.nrpInfo.hasDepth = reference3.fragmentInfoHasDepth;
				foreach (var setGlobals in renderGraphPass.setGlobalsList)
				{
					item2.nrpInfo.setGlobals.Add(setGlobals.Item1.handle.index);
				}
				for (int n = 0; n < 3; n++)
				{
					item2.resourceReadLists[n] = new global::System.Collections.Generic.List<int>();
					item2.resourceWriteLists[n] = new global::System.Collections.Generic.List<int>();
					foreach (global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle item6 in renderGraphPass.resourceReadLists[n])
					{
						if (!renderGraphPass.implicitReadsList.Contains(item6))
						{
							item2.resourceReadLists[n].Add(item6.index);
						}
					}
					foreach (global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle item7 in renderGraphPass.resourceWriteLists[n])
					{
						item2.resourceWriteLists[n].Add(item7.index);
					}
				}
				global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> readOnlySpan = reference3.FragmentInputs(reference);
				for (int num = 0; num < readOnlySpan.Length; num++)
				{
					global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData passFragmentData = readOnlySpan[num];
					item2.nrpInfo.textureFBFetchList.Add(passFragmentData.resource.index);
				}
				debugData.passList.Add(item2);
			}
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData.NativePassIterator enumerator4 = reference.NativePasses.GetEnumerator();
			while (enumerator4.MoveNext())
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData current8 = ref enumerator4.Current;
				global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
				for (int num2 = current8.firstGraphPass; num2 < current8.lastGraphPass + 1; num2++)
				{
					list.Add(num2);
				}
				if (current8.numGraphPasses > 0)
				{
					global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo nativeRenderPassInfo = new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo();
					nativeRenderPassInfo.passBreakReasoning = MakePassBreakInfoMessage(reference, in current8);
					nativeRenderPassInfo.attachmentInfos = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.AttachmentInfo>();
					for (int num3 = 0; num3 < current8.attachments.size; num3++)
					{
						nativeRenderPassInfo.attachmentInfos.Add(MakeAttachmentInfo(reference, in current8, num3));
					}
					nativeRenderPassInfo.passCompatibility = new global::UnityEngine.Rendering.SerializedDictionary<int, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.PassCompatibilityInfo>();
					nativeRenderPassInfo.mergedPassIds = list;
					for (int num4 = 0; num4 < list.Count; num4++)
					{
						int index = list[num4];
						global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData value = debugData.passList[index];
						value.nrpInfo.nativePassInfo = nativeRenderPassInfo;
						debugData.passList[index] = value;
					}
				}
			}
			for (int num5 = 0; num5 < reference.passData.Length; num5++)
			{
				ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference4 = ref reference.passData.ElementAt(num5);
				global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo nativePassInfo = debugData.passList[reference4.passId].nrpInfo.nativePassInfo;
				if (nativePassInfo == null)
				{
					continue;
				}
				global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData> readOnlySpan2 = reference4.Inputs(reference);
				for (int num = 0; num < readOnlySpan2.Length; num++)
				{
					ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData reference5 = ref readOnlySpan2[num];
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData reference6 = ref reference.VersionedResourceData(in reference5.resource);
					if (reference6.written)
					{
						global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData prevPass = reference.passData[reference6.writePassId];
						global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit mergeResult = ((prevPass.nativePassIndex >= 0) ? global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData.CanMerge(reference, prevPass.nativePassIndex, reference4.passId) : new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.NonRasterPass, reference4.passId));
						string message = "This pass writes to a resource that is read by the currently selected pass.\n\n" + MakePassMergeMessage(reference, in reference4, in prevPass, in mergeResult);
						nativePassInfo.passCompatibility.TryAdd(prevPass.passId, new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.PassCompatibilityInfo
						{
							message = message,
							isCompatible = (mergeResult.reason == global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.Merged)
						});
					}
				}
				if (reference4.nativePassIndex < 0)
				{
					continue;
				}
				global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassOutputData> readOnlySpan3 = reference4.Outputs(reference);
				for (int num = 0; num < readOnlySpan3.Length; num++)
				{
					ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassOutputData reference7 = ref readOnlySpan3[num];
					if (reference.UnversionedResourceData(in reference7.resource).lastUsePassID != reference4.passId)
					{
						int numReaders = reference.VersionedResourceData(in reference7.resource).numReaders;
						for (int num6 = 0; num6 < numReaders; num6++)
						{
							int index2 = reference.resources.IndexReader(in reference7.resource, num6);
							ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceReaderData reference8 = ref reference.resources.readerData[reference7.resource.iType].ElementAt(index2);
							global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData pass = reference.passData[reference8.passId];
							global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit mergeResult2 = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData.CanMerge(reference, reference4.nativePassIndex, pass.passId);
							string message2 = "This pass reads a resource that is written to by the currently selected pass.\n\n" + MakePassMergeMessage(reference, in pass, in reference4, in mergeResult2);
							nativePassInfo.passCompatibility.TryAdd(pass.passId, new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData.PassData.NRPInfo.NativeRenderPassInfo.PassCompatibilityInfo
							{
								message = message2,
								isCompatible = (mergeResult2.reason == global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.Merged)
							});
						}
					}
				}
			}
		}
	}
}
