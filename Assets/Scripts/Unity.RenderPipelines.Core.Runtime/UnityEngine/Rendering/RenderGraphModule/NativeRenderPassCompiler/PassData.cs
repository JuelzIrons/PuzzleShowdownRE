namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal struct PassData
	{
		public int passId;

		public global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType type;

		public bool hasFoveatedRasterization;

		public global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags extendedFeatureFlags;

		public int tag;

		public global::UnityEngine.Rendering.ShadingRateFragmentSize shadingRateFragmentSize;

		public global::UnityEngine.Rendering.ShadingRateCombiner primitiveShadingRateCombiner;

		public global::UnityEngine.Rendering.ShadingRateCombiner fragmentShadingRateCombiner;

		public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState mergeState;

		public int nativePassIndex;

		public int nativeSubPassIndex;

		public int firstInput;

		public int numInputs;

		public int firstOutput;

		public int numOutputs;

		public int firstFragment;

		public int numFragments;

		public int firstFragmentInput;

		public int numFragmentInputs;

		public int firstSampledOnlyRaster;

		public int numSampledOnlyRaster;

		public int firstRandomAccessResource;

		public int numRandomAccessResources;

		public int firstCreate;

		public int numCreated;

		public int firstDestroy;

		public int numDestroyed;

		public int shadingRateImageIndex;

		public int fragmentInfoWidth;

		public int fragmentInfoHeight;

		public int fragmentInfoVolumeDepth;

		public int fragmentInfoSamples;

		public int waitOnGraphicsFencePassId;

		public int awaitingMyGraphicsFencePassId;

		public bool asyncCompute;

		public bool hasSideEffects;

		public bool culled;

		public bool beginNativeSubpass;

		public bool fragmentInfoValid;

		public bool fragmentInfoHasDepth;

		public bool insertGraphicsFence;

		public bool hasShadingRateStates;

		public bool fragmentInfoHasShadingRateImage => shadingRateImageIndex > 0;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name GetName(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			return ctx.GetFullPassName(passId);
		}

		public PassData(in global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass pass, int passIndex)
		{
			passId = passIndex;
			type = pass.type;
			asyncCompute = pass.enableAsyncCompute;
			hasSideEffects = !pass.allowPassCulling;
			hasFoveatedRasterization = pass.enableFoveatedRasterization;
			extendedFeatureFlags = pass.extendedFeatureFlags;
			mergeState = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.None;
			nativePassIndex = -1;
			nativeSubPassIndex = -1;
			beginNativeSubpass = false;
			culled = false;
			tag = 0;
			firstInput = 0;
			numInputs = 0;
			firstOutput = 0;
			numOutputs = 0;
			firstFragment = 0;
			numFragments = 0;
			firstSampledOnlyRaster = 0;
			numSampledOnlyRaster = 0;
			firstRandomAccessResource = 0;
			numRandomAccessResources = 0;
			firstFragmentInput = 0;
			numFragmentInputs = 0;
			firstCreate = 0;
			numCreated = 0;
			firstDestroy = 0;
			numDestroyed = 0;
			fragmentInfoValid = false;
			fragmentInfoWidth = 0;
			fragmentInfoHeight = 0;
			fragmentInfoVolumeDepth = 0;
			fragmentInfoSamples = 0;
			fragmentInfoHasDepth = false;
			insertGraphicsFence = false;
			waitOnGraphicsFencePassId = -1;
			awaitingMyGraphicsFencePassId = -1;
			hasShadingRateStates = pass.hasShadingRateStates;
			shadingRateFragmentSize = pass.shadingRateFragmentSize;
			primitiveShadingRateCombiner = pass.primitiveShadingRateCombiner;
			fragmentShadingRateCombiner = pass.fragmentShadingRateCombiner;
			shadingRateImageIndex = -1;
		}

		public void ResetAndInitialize(in global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass pass, int passIndex)
		{
			passId = passIndex;
			type = pass.type;
			asyncCompute = pass.enableAsyncCompute;
			hasSideEffects = !pass.allowPassCulling;
			hasFoveatedRasterization = pass.enableFoveatedRasterization;
			extendedFeatureFlags = pass.extendedFeatureFlags;
			mergeState = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.None;
			nativePassIndex = -1;
			nativeSubPassIndex = -1;
			beginNativeSubpass = false;
			culled = false;
			tag = 0;
			firstInput = 0;
			numInputs = 0;
			firstOutput = 0;
			numOutputs = 0;
			firstFragment = 0;
			numFragments = 0;
			firstFragmentInput = 0;
			numFragmentInputs = 0;
			firstSampledOnlyRaster = 0;
			numSampledOnlyRaster = 0;
			firstRandomAccessResource = 0;
			numRandomAccessResources = 0;
			firstCreate = 0;
			numCreated = 0;
			firstDestroy = 0;
			numDestroyed = 0;
			fragmentInfoValid = false;
			fragmentInfoWidth = 0;
			fragmentInfoHeight = 0;
			fragmentInfoVolumeDepth = 0;
			fragmentInfoSamples = 0;
			fragmentInfoHasDepth = false;
			insertGraphicsFence = false;
			waitOnGraphicsFencePassId = -1;
			awaitingMyGraphicsFencePassId = -1;
			hasShadingRateStates = pass.hasShadingRateStates;
			shadingRateFragmentSize = pass.shadingRateFragmentSize;
			primitiveShadingRateCombiner = pass.primitiveShadingRateCombiner;
			fragmentShadingRateCombiner = pass.fragmentShadingRateCombiner;
			shadingRateImageIndex = -1;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public readonly global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassOutputData> Outputs(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			return global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.MakeReadOnlySpan(ref ctx.outputData, firstOutput, numOutputs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public readonly global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData> Inputs(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			return global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.MakeReadOnlySpan(ref ctx.inputData, firstInput, numInputs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public readonly global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> Fragments(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			return global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.MakeReadOnlySpan(ref ctx.fragmentData, firstFragment, numFragments);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public readonly global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> SampledTexturesIfRaster(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			return global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.MakeReadOnlySpan(ref ctx.sampledData, firstSampledOnlyRaster, numSampledOnlyRaster);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData ShadingRateImage(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			return ctx.fragmentData[shadingRateImageIndex];
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public readonly global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> FragmentInputs(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			return global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.MakeReadOnlySpan(ref ctx.fragmentData, firstFragmentInput, numFragmentInputs);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public readonly global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> FirstUsedResources(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			return global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.MakeReadOnlySpan(ref ctx.createData, firstCreate, numCreated);
		}

		public global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassRandomWriteData> RandomWriteTextures(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			return global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.MakeReadOnlySpan(ref ctx.randomAccessResourceData, firstRandomAccessResource, numRandomAccessResources);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public readonly global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> LastUsedResources(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			return global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.MakeReadOnlySpan(ref ctx.destroyData, firstDestroy, numDestroyed);
		}

		private bool TrySetupAndValidateFragmentInfo(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, out string errorMessage)
		{
			errorMessage = null;
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference = ref ctx.UnversionedResourceData(in h);
			if (!global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.enableValidityChecks || !fragmentInfoValid)
			{
				fragmentInfoWidth = reference.width;
				fragmentInfoHeight = reference.height;
				fragmentInfoSamples = reference.msaaSamples;
				fragmentInfoVolumeDepth = reference.volumeDepth;
				fragmentInfoValid = true;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void TryAddFragment(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, out string errorMessage)
		{
			if (TrySetupAndValidateFragmentInfo(in h, ctx, out errorMessage))
			{
				numFragments++;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void TryAddFragmentInput(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, out string errorMessage)
		{
			if (TrySetupAndValidateFragmentInfo(in h, ctx, out errorMessage))
			{
				numFragmentInputs++;
			}
		}

		internal void AddRandomAccessResource()
		{
			numRandomAccessResources++;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void AddFirstUse(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> readOnlySpan = FirstUsedResources(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle reference = ref readOnlySpan[i];
				if (reference.index == h.index && reference.type == h.type)
				{
					return;
				}
			}
			ctx.createData.Add(in h);
			int num = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.LastIndex(ref ctx.createData);
			if (numCreated == 0)
			{
				firstCreate = num;
			}
			numCreated++;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal void AddLastUse(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> readOnlySpan = LastUsedResources(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle reference = ref readOnlySpan[i];
				if (reference.index == h.index && reference.type == h.type)
				{
					return;
				}
			}
			ctx.destroyData.Add(in h);
			int num = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.LastIndex(ref ctx.destroyData);
			if (numDestroyed == 0)
			{
				firstDestroy = num;
			}
			numDestroyed++;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal readonly bool IsUsedAsFragment(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			if (h.type != global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType.Texture)
			{
				return false;
			}
			if (type != global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Raster)
			{
				return false;
			}
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> readOnlySpan = Fragments(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				if (readOnlySpan[i].resource.index == h.index)
				{
					return true;
				}
			}
			readOnlySpan = FragmentInputs(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				if (readOnlySpan[i].resource.index == h.index)
				{
					return true;
				}
			}
			return false;
		}

		internal void DisconnectFromResources(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> unusedVersionedResourceIdCullingStack = null, int type = 0)
		{
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassOutputData> readOnlySpan = Outputs(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource = ref readOnlySpan[i].resource;
				if (resource.version == ctx.UnversionedResourceData(in resource).latestVersionNumber)
				{
					ctx.UnversionedResourceData(in resource).latestVersionNumber--;
				}
			}
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData> readOnlySpan2 = Inputs(ctx);
			for (int i = 0; i < readOnlySpan2.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle resource2 = ref readOnlySpan2[i].resource;
				ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData reference = ref ctx.resources[resource2];
				reference.RemoveReadingPass(ctx, in resource2, passId);
				if (unusedVersionedResourceIdCullingStack != null && resource2.iType == type && reference.written && reference.numReaders == 0)
				{
					unusedVersionedResourceIdCullingStack.Push(resource2);
				}
			}
		}
	}
}
