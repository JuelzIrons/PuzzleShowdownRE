namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal struct NativePassData
	{
		public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.LoadAudit> loadAudit;

		public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.StoreAudit> storeAudit;

		public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit breakAudit;

		public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> fragments;

		public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassAttachment> attachments;

		public int firstGraphPass;

		public int lastGraphPass;

		public int numGraphPasses;

		public int firstNativeSubPass;

		public int numNativeSubPasses;

		public int width;

		public int height;

		public int volumeDepth;

		public int samples;

		public int shadingRateImageIndex;

		public bool hasDepth;

		public bool hasFoveatedRasterization;

		public bool hasShadingRateStates;

		public global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags extendedFeatureFlags;

		public global::UnityEngine.Rendering.ShadingRateFragmentSize shadingRateFragmentSize;

		public global::UnityEngine.Rendering.ShadingRateCombiner primitiveShadingRateCombiner;

		public global::UnityEngine.Rendering.ShadingRateCombiner fragmentShadingRateCombiner;

		public bool hasShadingRateImage => shadingRateImageIndex >= 0;

		public NativePassData(ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData pass, global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
		{
			firstGraphPass = pass.passId;
			lastGraphPass = pass.passId;
			numGraphPasses = 1;
			firstNativeSubPass = -1;
			numNativeSubPasses = 0;
			fragments = default(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData>);
			attachments = default(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassAttachment>);
			width = pass.fragmentInfoWidth;
			height = pass.fragmentInfoHeight;
			volumeDepth = pass.fragmentInfoVolumeDepth;
			samples = pass.fragmentInfoSamples;
			hasDepth = pass.fragmentInfoHasDepth;
			hasFoveatedRasterization = pass.hasFoveatedRasterization;
			extendedFeatureFlags = pass.extendedFeatureFlags;
			loadAudit = default(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.LoadAudit>);
			storeAudit = default(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.StoreAudit>);
			breakAudit = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.NotOptimized, -1);
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> readOnlySpan = pass.Fragments(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData data = ref readOnlySpan[i];
				fragments.Add(in data);
			}
			readOnlySpan = pass.FragmentInputs(ctx);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData data2 = ref readOnlySpan[i];
				fragments.Add(in data2);
			}
			if (pass.fragmentInfoHasShadingRateImage && !hasFoveatedRasterization)
			{
				shadingRateImageIndex = fragments.size;
				fragments.Add(pass.ShadingRateImage(ctx));
			}
			else
			{
				shadingRateImageIndex = -1;
			}
			hasShadingRateStates = pass.hasShadingRateStates && !hasFoveatedRasterization;
			shadingRateFragmentSize = pass.shadingRateFragmentSize;
			primitiveShadingRateCombiner = pass.primitiveShadingRateCombiner;
			fragmentShadingRateCombiner = pass.fragmentShadingRateCombiner;
			TryMergeNativeSubPass(ctx, ref this, ref pass);
		}

		public global::UnityEngine.Rendering.SubPassFlags GetSubPassFlagForMerging()
		{
			if (!hasDepth)
			{
				throw new global::System.Exception("SubPassFlag for merging cannot be determined if native pass doesn't have a depth attachment. Make sure your pass has a depth attachment.");
			}
			return global::UnityEngine.Rendering.SubPassFlags.ReadOnlyDepth;
		}

		public void Clear()
		{
			firstGraphPass = 0;
			numGraphPasses = 0;
			attachments.Clear();
			fragments.Clear();
			loadAudit.Clear();
			storeAudit.Clear();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public readonly bool IsValid()
		{
			return numGraphPasses > 0;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public readonly global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> GraphPasses(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> actualPasses)
		{
			if (lastGraphPass - firstGraphPass + 1 == numGraphPasses)
			{
				actualPasses = default(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData>);
				return global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.MakeReadOnlySpan(ref ctx.passData, firstGraphPass, numGraphPasses);
			}
			actualPasses = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData>(numGraphPasses, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int i = firstGraphPass;
			int num = 0;
			for (; i < lastGraphPass + 1; i++)
			{
				global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData value = ctx.passData[i];
				if (!value.culled)
				{
					actualPasses[num++] = value;
				}
			}
			return actualPasses;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public readonly void GetGraphPassNames(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name> dest)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> actualPasses;
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> readOnlySpan = GraphPasses(ctx, out actualPasses);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				dest.Add(readOnlySpan[i].GetName(ctx));
			}
			if (actualPasses.IsCreated)
			{
				actualPasses.Dispose();
			}
		}

		private static bool CanMergeMSAASamples(ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData nativePass, ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData passToMerge)
		{
			if (nativePass.samples != passToMerge.fragmentInfoSamples)
			{
				if (passToMerge.fragmentInfoSamples == 1)
				{
					return passToMerge.extendedFeatureFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultisampledShaderResolve);
				}
				return false;
			}
			return true;
		}

		private static bool AreExtendedFeatureFlagsCompatible(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags flags0, global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags flags1)
		{
			return true;
		}

		public static global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit CanMerge(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData contextData, int activeNativePassId, int passIdToMerge)
		{
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref contextData.passData.ElementAt(passIdToMerge);
			if (reference.type != global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPassType.Raster)
			{
				return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.NonRasterPass, passIdToMerge);
			}
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData reference2 = ref contextData.nativePassData.ElementAt(activeNativePassId);
			if (reference.numFragments > 0 || reference.numFragmentInputs > 0)
			{
				if (reference2.width != reference.fragmentInfoWidth || reference2.height != reference.fragmentInfoHeight || reference2.volumeDepth != reference.fragmentInfoVolumeDepth || !CanMergeMSAASamples(ref reference2, ref reference))
				{
					return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.TargetSizeMismatch, passIdToMerge);
				}
				if (reference2.hasDepth && reference.fragmentInfoHasDepth)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference3 = ref contextData.fragmentData.ElementAt(reference.firstFragment);
					if (reference2.fragments[0].resource.index != reference3.resource.index)
					{
						return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.DifferentDepthTextures, passIdToMerge);
					}
				}
				if (reference2.hasFoveatedRasterization != reference.hasFoveatedRasterization)
				{
					return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.FRStateMismatch, passIdToMerge);
				}
				if (!AreExtendedFeatureFlagsCompatible(reference2.extendedFeatureFlags, reference.extendedFeatureFlags))
				{
					return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.ExtendedFeatureFlagsIncompatible, passIdToMerge);
				}
				if (reference2.hasShadingRateImage != reference.fragmentInfoHasShadingRateImage)
				{
					return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.DifferentShadingRateImages, passIdToMerge);
				}
				if (reference2.hasShadingRateImage)
				{
					global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData passFragmentData = reference.ShadingRateImage(contextData);
					global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData passFragmentData2 = reference2.fragments[reference2.shadingRateImageIndex];
					if (passFragmentData2.resource.index != passFragmentData.resource.index)
					{
						return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.DifferentShadingRateImages, passIdToMerge);
					}
				}
				if (reference2.hasShadingRateStates != reference.hasShadingRateStates)
				{
					return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.DifferentShadingRateStates, passIdToMerge);
				}
				if (reference2.hasShadingRateStates && (reference2.shadingRateFragmentSize != reference.shadingRateFragmentSize || reference2.primitiveShadingRateCombiner != reference.primitiveShadingRateCombiner || reference2.fragmentShadingRateCombiner != reference.fragmentShadingRateCombiner))
				{
					return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.DifferentShadingRateStates, passIdToMerge);
				}
				if (reference2.extendedFeatureFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultisampledShaderResolve))
				{
					return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.MultisampledShaderResolveMustBeLastPass, passIdToMerge);
				}
			}
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> readOnlySpan = reference.SampledTexturesIfRaster(contextData);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData reference4 = ref contextData.VersionedResourceData(in readOnlySpan[i]);
				if (!contextData.passData[reference4.writePassId].culled && reference4.written && reference4.writePassId >= reference2.firstGraphPass && reference4.writePassId < reference2.lastGraphPass + 1)
				{
					return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.NextPassReadsTexture, passIdToMerge);
				}
			}
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> attachmentsToTryAdding = default(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData>);
			int num = 8 - reference2.fragments.size;
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> readOnlySpan3;
			if (reference.numFragments > 0)
			{
				global::System.Collections.Generic.HashSet<int> value;
				using (global::UnityEngine.Rendering.HashSetPool<int>.Get(out value))
				{
					global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> actualPasses;
					global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> readOnlySpan2 = reference2.GraphPasses(contextData, out actualPasses);
					for (int i = 0; i < readOnlySpan2.Length; i++)
					{
						ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference5 = ref readOnlySpan2[i];
						if (reference5.numSampledOnlyRaster > 0)
						{
							readOnlySpan = reference5.SampledTexturesIfRaster(contextData);
							for (int j = 0; j < readOnlySpan.Length; j++)
							{
								ref readonly global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle reference6 = ref readOnlySpan[j];
								value.Add(reference6.index);
							}
						}
					}
					if (actualPasses.IsCreated)
					{
						actualPasses.Dispose();
					}
					readOnlySpan3 = reference.Fragments(contextData);
					for (int i = 0; i < readOnlySpan3.Length; i++)
					{
						ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference7 = ref readOnlySpan3[i];
						bool flag = false;
						for (int k = 0; k < reference2.fragments.size; k++)
						{
							if (global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData.SameSubResource(in reference2.fragments[k], in reference7))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							if (num == 0)
							{
								return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.AttachmentLimitReached, passIdToMerge);
							}
							attachmentsToTryAdding.Add(in reference7);
							num--;
						}
						if (value.Contains(reference7.resource.index))
						{
							return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.NextPassTargetsTexture, passIdToMerge);
						}
					}
				}
			}
			readOnlySpan3 = reference.FragmentInputs(contextData);
			for (int i = 0; i < readOnlySpan3.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference8 = ref readOnlySpan3[i];
				bool flag2 = false;
				for (int l = 0; l < reference2.fragments.size; l++)
				{
					if (global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData.SameSubResource(in reference2.fragments[l], in reference8))
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					if (num == 0)
					{
						return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.AttachmentLimitReached, passIdToMerge);
					}
					attachmentsToTryAdding.Add(in reference8);
					num--;
				}
			}
			if (TotalAttachmentsSizeExceedPixelStorageLimit(contextData, ref reference2, ref attachmentsToTryAdding))
			{
				return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.AttachmentLimitReached, passIdToMerge);
			}
			if (reference2.numGraphPasses >= 8 && !CanMergeNativeSubPass(contextData, ref reference2, ref reference))
			{
				return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.SubPassLimitReached, passIdToMerge);
			}
			return new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.Merged, passIdToMerge);
		}

		private static bool TotalAttachmentsSizeExceedPixelStorageLimit(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData contextData, ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData nativePass, ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> attachmentsToTryAdding)
		{
			if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.IPhonePlayer && global::UnityEngine.SystemInfo.maxTiledPixelStorageSize <= 32)
			{
				int num = 0;
				for (int i = 0; i < nativePass.fragments.size; i++)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference = ref contextData.UnversionedResourceData(in nativePass.fragments[i].resource);
					num += global::UnityEngine.SystemInfo.GetTiledRenderTargetStorageSize(reference.graphicsFormat, reference.msaaSamples);
				}
				for (int j = 0; j < attachmentsToTryAdding.size; j++)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData reference2 = ref contextData.UnversionedResourceData(in attachmentsToTryAdding[j].resource);
					num += global::UnityEngine.SystemInfo.GetTiledRenderTargetStorageSize(reference2.graphicsFormat, reference2.msaaSamples);
				}
				return num > global::UnityEngine.SystemInfo.maxTiledPixelStorageSize;
			}
			return false;
		}

		private static bool CanMergeNativeSubPass(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData contextData, ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData nativePass, ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData passToMerge)
		{
			if (passToMerge.numFragments == 0 && passToMerge.numFragmentInputs == 0)
			{
				return true;
			}
			if (nativePass.numNativeSubPasses == 0)
			{
				return false;
			}
			ref global::UnityEngine.Rendering.SubPassDescriptor reference = ref contextData.nativeSubPassData.ElementAt(nativePass.firstNativeSubPass + nativePass.numNativeSubPasses - 1);
			bool fragmentInfoHasDepth = passToMerge.fragmentInfoHasDepth;
			int num = (fragmentInfoHasDepth ? (-1) : 0);
			if (passToMerge.numFragments + num != reference.colorOutputs.Length)
			{
				return false;
			}
			if (passToMerge.numFragmentInputs != reference.inputs.Length)
			{
				return false;
			}
			global::UnityEngine.Rendering.SubPassFlags subPassFlags = global::UnityEngine.Rendering.SubPassFlags.None;
			if (!fragmentInfoHasDepth && nativePass.hasDepth)
			{
				subPassFlags = nativePass.GetSubPassFlagForMerging();
			}
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> reference2 = ref nativePass.fragments;
			int num2 = 0;
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> readOnlySpan = passToMerge.Fragments(contextData);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference3 = ref readOnlySpan[i];
				if (fragmentInfoHasDepth && num2 == 0)
				{
					subPassFlags = ((!reference3.accessFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write)) ? global::UnityEngine.Rendering.SubPassFlags.ReadOnlyDepth : global::UnityEngine.Rendering.SubPassFlags.None);
				}
				else
				{
					int num3 = -1;
					int num4 = 0;
					while (true)
					{
						int num5 = num4;
						global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> fixedAttachmentArray = reference2;
						if (num5 >= fixedAttachmentArray.size)
						{
							break;
						}
						fixedAttachmentArray = reference2;
						if (global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData.SameSubResource(in fixedAttachmentArray[num4], in reference3))
						{
							num3 = num4;
							break;
						}
						num4++;
					}
					if (num3 < 0 || num3 != reference.colorOutputs[num2 + num])
					{
						return false;
					}
				}
				num2++;
			}
			int num6 = 0;
			readOnlySpan = passToMerge.FragmentInputs(contextData);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData y = ref readOnlySpan[i];
				int num7 = -1;
				int num8 = 0;
				while (true)
				{
					int num9 = num8;
					global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> fixedAttachmentArray = reference2;
					if (num9 >= fixedAttachmentArray.size)
					{
						break;
					}
					fixedAttachmentArray = reference2;
					if (global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData.SameSubResource(in fixedAttachmentArray[num8], in y))
					{
						num7 = num8;
						break;
					}
					num8++;
				}
				if (num7 < 0 || num7 != reference.inputs[num6])
				{
					return false;
				}
				num6++;
			}
			return subPassFlags == reference.flags;
		}

		public static void TryMergeNativeSubPass(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData contextData, ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData nativePass, ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData passToMerge)
		{
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.FixedAttachmentArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> reference = ref nativePass.fragments;
			if (nativePass.numNativeSubPasses == 0 && nativePass.fragments.size > 0)
			{
				nativePass.firstNativeSubPass = contextData.nativeSubPassData.Length;
			}
			global::UnityEngine.Rendering.SubPassDescriptor value = default(global::UnityEngine.Rendering.SubPassDescriptor);
			if (passToMerge.numFragments == 0 && passToMerge.numFragmentInputs == 0)
			{
				passToMerge.nativeSubPassIndex = nativePass.numNativeSubPasses - 1;
				passToMerge.beginNativeSubpass = false;
				return;
			}
			if (!passToMerge.fragmentInfoHasDepth && nativePass.hasDepth)
			{
				value.flags = nativePass.GetSubPassFlagForMerging();
			}
			int num = 0;
			int num2 = (passToMerge.fragmentInfoHasDepth ? (-1) : 0);
			value.colorOutputs = new global::UnityEngine.Rendering.AttachmentIndexArray(passToMerge.numFragments + num2);
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> readOnlySpan = passToMerge.Fragments(contextData);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference2 = ref readOnlySpan[i];
				if (passToMerge.fragmentInfoHasDepth && num == 0)
				{
					value.flags = ((!reference2.accessFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write)) ? global::UnityEngine.Rendering.SubPassFlags.ReadOnlyDepth : global::UnityEngine.Rendering.SubPassFlags.None);
				}
				else
				{
					int value2 = -1;
					for (int j = 0; j < reference.size; j++)
					{
						if (global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData.SameSubResource(in reference[j], in reference2))
						{
							value2 = j;
							break;
						}
					}
					value.colorOutputs[num + num2] = value2;
				}
				num++;
			}
			int num3 = 0;
			value.inputs = new global::UnityEngine.Rendering.AttachmentIndexArray(passToMerge.numFragmentInputs);
			readOnlySpan = passToMerge.FragmentInputs(contextData);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData y = ref readOnlySpan[i];
				int value3 = -1;
				for (int k = 0; k < reference.size; k++)
				{
					if (global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData.SameSubResource(in reference[k], in y))
					{
						value3 = k;
						break;
					}
				}
				value.inputs[num3] = value3;
				num3++;
			}
			if (passToMerge.fragmentInfoHasShadingRateImage)
			{
				value.flags |= global::UnityEngine.Rendering.SubPassFlags.UseShadingRateImage;
			}
			if (nativePass.numNativeSubPasses == 0 || !global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassCompiler.IsSameNativeSubPass(ref value, ref contextData.nativeSubPassData.ElementAt(nativePass.firstNativeSubPass + nativePass.numNativeSubPasses - 1)))
			{
				contextData.nativeSubPassData.Add(in value);
				nativePass.numNativeSubPasses++;
				passToMerge.beginNativeSubpass = true;
			}
			else
			{
				passToMerge.beginNativeSubpass = false;
			}
			passToMerge.nativeSubPassIndex = nativePass.numNativeSubPasses - 1;
		}

		private void AddDepthAttachmentFirstDuringMerge(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData contextData, in global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData depthAttachment)
		{
			fragments.Add(in depthAttachment);
			hasDepth = true;
			int size = fragments.size;
			if (size == 1)
			{
				return;
			}
			int num = size - 1;
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference = ref fragments[0];
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference2 = ref fragments[num];
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData passFragmentData = fragments[num];
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData passFragmentData2 = fragments[0];
			reference = passFragmentData;
			reference2 = passFragmentData2;
			global::UnityEngine.Rendering.SubPassFlags subPassFlagForMerging = GetSubPassFlagForMerging();
			for (int i = firstNativeSubPass; i < firstNativeSubPass + numNativeSubPasses; i++)
			{
				ref global::UnityEngine.Rendering.SubPassDescriptor reference3 = ref contextData.nativeSubPassData.ElementAt(i);
				reference3.flags |= subPassFlagForMerging;
				for (int j = 0; j < reference3.colorOutputs.Length; j++)
				{
					if (reference3.colorOutputs[j] == 0)
					{
						reference3.colorOutputs[j] = num;
					}
				}
				for (int k = 0; k < reference3.inputs.Length; k++)
				{
					if (reference3.inputs[k] == 0)
					{
						reference3.inputs[k] = num;
					}
				}
			}
			if (hasShadingRateImage && shadingRateImageIndex == 0)
			{
				shadingRateImageIndex = num;
			}
		}

		public static global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit TryMerge(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData contextData, int activeNativePassId, int passIdToMerge)
		{
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakAudit result = CanMerge(contextData, activeNativePassId, passIdToMerge);
			if (result.reason != global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassBreakReason.Merged)
			{
				return result;
			}
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData reference = ref contextData.passData.ElementAt(passIdToMerge);
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData reference2 = ref contextData.nativePassData.ElementAt(activeNativePassId);
			reference.mergeState = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.SubPass;
			if (reference.nativePassIndex >= 0)
			{
				contextData.nativePassData.ElementAt(reference.nativePassIndex).Clear();
			}
			reference.nativePassIndex = activeNativePassId;
			reference2.numGraphPasses++;
			reference2.lastGraphPass = passIdToMerge;
			if (reference.extendedFeatureFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultisampledShaderResolve))
			{
				reference2.extendedFeatureFlags |= global::UnityEngine.Rendering.RenderGraphModule.ExtendedFeatureFlags.MultisampledShaderResolve;
			}
			if (!reference2.hasDepth && reference.fragmentInfoHasDepth)
			{
				reference2.AddDepthAttachmentFirstDuringMerge(contextData, contextData.fragmentData[reference.firstFragment]);
			}
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> readOnlySpan = reference.Fragments(contextData);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference3 = ref readOnlySpan[i];
				bool flag = false;
				for (int j = 0; j < reference2.fragments.size; j++)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference4 = ref reference2.fragments[j];
					if (global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData.SameSubResource(in reference4, in reference3))
					{
						global::UnityEngine.Rendering.RenderGraphModule.AccessFlags accessFlags = reference3.accessFlags;
						if (reference4.accessFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Discard))
						{
							accessFlags &= ~global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read;
						}
						reference4 = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData(new global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle(in reference4.resource, reference3.resource.version), reference4.accessFlags | accessFlags, reference4.mipLevel, reference4.depthSlice);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					reference2.fragments.Add(in reference3);
				}
			}
			readOnlySpan = reference.FragmentInputs(contextData);
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference5 = ref readOnlySpan[i];
				bool flag2 = false;
				for (int k = 0; k < reference2.fragments.size; k++)
				{
					ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData reference6 = ref reference2.fragments[k];
					if (global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData.SameSubResource(in reference6, in reference5))
					{
						global::UnityEngine.Rendering.RenderGraphModule.AccessFlags accessFlags2 = reference5.accessFlags;
						if (reference6.accessFlags.HasFlag(global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Discard))
						{
							accessFlags2 &= ~global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Read;
						}
						reference6 = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData(new global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle(in reference6.resource, reference5.resource.version), reference6.accessFlags | accessFlags2, reference6.mipLevel, reference6.depthSlice);
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					reference2.fragments.Add(in reference5);
				}
			}
			TryMergeNativeSubPass(contextData, ref reference2, ref reference);
			SetPassStatesForNativePass(contextData, activeNativePassId);
			return result;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void SetPassStatesForNativePass(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData contextData, int nativePassId)
		{
			ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData reference = ref contextData.nativePassData.ElementAt(nativePassId);
			if (reference.numGraphPasses > 1)
			{
				contextData.passData.ElementAt(reference.firstGraphPass).mergeState = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.Begin;
				int num = reference.lastGraphPass - reference.firstGraphPass + 1;
				for (int i = 1; i < num; i++)
				{
					int index = reference.firstGraphPass + i;
					if (contextData.passData.ElementAt(index).culled)
					{
						contextData.passData.ElementAt(index).mergeState = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.None;
					}
					else
					{
						contextData.passData.ElementAt(reference.firstGraphPass + i).mergeState = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.SubPass;
					}
				}
				contextData.passData.ElementAt(reference.lastGraphPass).mergeState = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.End;
			}
			else
			{
				contextData.passData.ElementAt(reference.firstGraphPass).mergeState = global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassMergeState.None;
			}
		}
	}
}
