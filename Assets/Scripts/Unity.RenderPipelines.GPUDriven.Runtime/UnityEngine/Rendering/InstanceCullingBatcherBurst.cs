namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile]
	internal static class InstanceCullingBatcherBurst
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void RemoveDrawInstanceIndices_00000188_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<int> drawInstanceIndices, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches);

		internal static class RemoveDrawInstanceIndices_00000188_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.InstanceCullingBatcherBurst.RemoveDrawInstanceIndices_00000188_0024PostfixBurstDelegate>(RemoveDrawInstanceIndices).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(in global::Unity.Collections.NativeArray<int> drawInstanceIndices, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<int>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance>, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int>, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch>, void>)functionPointer)(ref drawInstanceIndices, ref drawInstances, ref rangeHash, ref batchHash, ref drawRanges, ref drawBatches);
						return;
					}
				}
				RemoveDrawInstanceIndices_0024BurstManaged(in drawInstanceIndices, ref drawInstances, ref rangeHash, ref batchHash, ref drawRanges, ref drawBatches);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void CreateDrawBatches_0000018C_0024PostfixBurstDelegate(bool implicitInstanceIndices, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMeshID> batchMeshHash, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID> batchMaterialHash, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDataHash, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances);

		internal static class CreateDrawBatches_0000018C_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.InstanceCullingBatcherBurst.CreateDrawBatches_0000018C_0024PostfixBurstDelegate>(CreateDrawBatches).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(bool implicitInstanceIndices, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMeshID> batchMeshHash, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID> batchMaterialHash, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDataHash, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<bool, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>, ref global::UnityEngine.Rendering.GPUDrivenRendererGroupData, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMeshID>, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID>, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData>, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange>, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch>, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance>, void>)functionPointer)(implicitInstanceIndices, ref instances, ref rendererData, ref batchMeshHash, ref batchMaterialHash, ref packedMaterialDataHash, ref rangeHash, ref drawRanges, ref batchHash, ref drawBatches, ref drawInstances);
						return;
					}
				}
				CreateDrawBatches_0024BurstManaged(implicitInstanceIndices, in instances, in rendererData, in batchMeshHash, in batchMaterialHash, in packedMaterialDataHash, ref rangeHash, ref drawRanges, ref batchHash, ref drawBatches, ref drawInstances);
			}
		}

		private static void RemoveDrawRange(in global::UnityEngine.Rendering.RangeKey key, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges)
		{
			int num = rangeHash[key];
			rangeHash[drawRanges.ElementAt(drawRanges.Length - 1).key] = num;
			rangeHash.Remove(key);
			drawRanges.RemoveAtSwapBack(num);
		}

		private static void RemoveDrawBatch(in global::UnityEngine.Rendering.DrawKey key, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches)
		{
			int num = batchHash[key];
			int index = rangeHash[key.range];
			ref global::UnityEngine.Rendering.DrawRange reference = ref drawRanges.ElementAt(index);
			if (--reference.drawCount == 0)
			{
				RemoveDrawRange(in reference.key, ref rangeHash, ref drawRanges);
			}
			batchHash[drawBatches.ElementAt(drawBatches.Length - 1).key] = num;
			batchHash.Remove(key);
			drawBatches.RemoveAtSwapBack(num);
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002ERemoveDrawInstanceIndices_00000188_0024PostfixBurstDelegate))]
		public static void RemoveDrawInstanceIndices(in global::Unity.Collections.NativeArray<int> drawInstanceIndices, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches)
		{
			global::UnityEngine.Rendering.InstanceCullingBatcherBurst.RemoveDrawInstanceIndices_00000188_0024BurstDirectCall.Invoke(in drawInstanceIndices, ref drawInstances, ref rangeHash, ref batchHash, ref drawRanges, ref drawBatches);
		}

		private static ref global::UnityEngine.Rendering.DrawRange EditDrawRange(in global::UnityEngine.Rendering.RangeKey key, global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges)
		{
			if (!rangeHash.TryGetValue(key, out var item))
			{
				global::UnityEngine.Rendering.DrawRange value = new global::UnityEngine.Rendering.DrawRange
				{
					key = key,
					drawCount = 0,
					drawOffset = 0
				};
				item = drawRanges.Length;
				rangeHash.Add(key, item);
				drawRanges.Add(in value);
			}
			return ref drawRanges.ElementAt(item);
		}

		private static ref global::UnityEngine.Rendering.DrawBatch EditDrawBatch(in global::UnityEngine.Rendering.DrawKey key, in global::UnityEngine.Rendering.SubMeshDescriptor subMeshDescriptor, global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches)
		{
			global::UnityEngine.Rendering.MeshProceduralInfo procInfo = new global::UnityEngine.Rendering.MeshProceduralInfo
			{
				topology = subMeshDescriptor.topology,
				baseVertex = (uint)subMeshDescriptor.baseVertex,
				firstIndex = (uint)subMeshDescriptor.indexStart,
				indexCount = (uint)subMeshDescriptor.indexCount
			};
			if (!batchHash.TryGetValue(key, out var item))
			{
				global::UnityEngine.Rendering.DrawBatch value = new global::UnityEngine.Rendering.DrawBatch
				{
					key = key,
					instanceCount = 0,
					instanceOffset = 0,
					procInfo = procInfo
				};
				item = drawBatches.Length;
				batchHash.Add(key, item);
				drawBatches.Add(in value);
			}
			return ref drawBatches.ElementAt(item);
		}

		private static void ProcessRenderer(int i, bool implicitInstanceIndices, in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData, global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMeshID> batchMeshHash, global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDataHash, global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID> batchMaterialHash, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances, global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges, global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches)
		{
			int index = rendererData.meshIndex[i];
			global::UnityEngine.EntityId key = rendererData.meshID[index];
			global::UnityEngine.Rendering.GPUDrivenMeshLodInfo gPUDrivenMeshLodInfo = rendererData.meshLodInfo[index];
			short num = rendererData.subMeshCount[index];
			int num2 = rendererData.subMeshDescOffset[index];
			global::UnityEngine.Rendering.BatchMeshID meshID = batchMeshHash[key];
			global::UnityEngine.EntityId entityId = rendererData.rendererGroupID[i];
			short num3 = rendererData.subMeshStartIndex[i];
			int num4 = rendererData.gameObjectLayer[i];
			uint renderingLayerMask = rendererData.renderingLayerMask[i];
			int num5 = rendererData.materialsOffset[i];
			short num6 = rendererData.materialsCount[i];
			int num7 = rendererData.lightmapIndex[i];
			global::UnityEngine.Rendering.GPUDrivenPackedRendererData gPUDrivenPackedRendererData = rendererData.packedRendererData[i];
			int rendererPriority = rendererData.rendererPriority[i];
			int num8;
			int num9;
			if (implicitInstanceIndices)
			{
				num8 = 1;
				num9 = i;
			}
			else
			{
				num8 = rendererData.instancesCount[i];
				num9 = rendererData.instancesOffset[i];
			}
			if (num8 == 0)
			{
				return;
			}
			global::UnityEngine.Rendering.InstanceComponentGroup instanceComponentGroup = global::UnityEngine.Rendering.InstanceComponentGroup.Default;
			if (gPUDrivenPackedRendererData.hasTree)
			{
				instanceComponentGroup |= global::UnityEngine.Rendering.InstanceComponentGroup.Wind;
			}
			if ((num7 & 0xFFFF) >= 65534)
			{
				if (gPUDrivenPackedRendererData.lightProbeUsage == global::UnityEngine.Rendering.LightProbeUsage.BlendProbes)
				{
					instanceComponentGroup |= global::UnityEngine.Rendering.InstanceComponentGroup.LightProbe;
				}
			}
			else
			{
				instanceComponentGroup |= global::UnityEngine.Rendering.InstanceComponentGroup.Lightmap;
			}
			global::System.Span<global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> span = stackalloc global::UnityEngine.Rendering.GPUDrivenPackedMaterialData[(int)num6];
			bool flag = true;
			for (int j = 0; j < num6; j++)
			{
				if (j >= num)
				{
					global::UnityEngine.Debug.LogWarning("Material count in the shared material list is higher than sub mesh count for the mesh. Object may be corrupted.");
					continue;
				}
				int index2 = rendererData.materialIndex[num5 + j];
				global::UnityEngine.Rendering.GPUDrivenPackedMaterialData item;
				if (rendererData.packedMaterialData.Length > 0)
				{
					item = rendererData.packedMaterialData[index2];
				}
				else
				{
					global::UnityEngine.EntityId key2 = rendererData.materialID[index2];
					packedMaterialDataHash.TryGetValue(key2, out item);
				}
				flag &= item.isIndirectSupported;
				span[j] = item;
			}
			global::UnityEngine.Rendering.RangeKey key3 = new global::UnityEngine.Rendering.RangeKey
			{
				layer = (byte)num4,
				renderingLayerMask = renderingLayerMask,
				motionMode = gPUDrivenPackedRendererData.motionVecGenMode,
				shadowCastingMode = gPUDrivenPackedRendererData.shadowCastingMode,
				staticShadowCaster = gPUDrivenPackedRendererData.staticShadowCaster,
				rendererPriority = rendererPriority,
				supportsIndirect = flag
			};
			ref global::UnityEngine.Rendering.DrawRange reference = ref EditDrawRange(in key3, rangeHash, drawRanges);
			for (int k = 0; k < num6; k++)
			{
				if (k >= num)
				{
					global::UnityEngine.Debug.LogWarning("Material count in the shared material list is higher than sub mesh count for the mesh. Object may be corrupted.");
					continue;
				}
				int index3 = rendererData.materialIndex[num5 + k];
				global::UnityEngine.EntityId entityId2 = rendererData.materialID[index3];
				global::UnityEngine.Rendering.GPUDrivenPackedMaterialData gPUDrivenPackedMaterialData = span[k];
				if (entityId2 == 0)
				{
					global::UnityEngine.Debug.LogWarning("Material in the shared materials list is null. Object will be partially rendered.");
					continue;
				}
				batchMaterialHash.TryGetValue(entityId2, out var item2);
				global::UnityEngine.Rendering.BatchDrawCommandFlags batchDrawCommandFlags = global::UnityEngine.Rendering.BatchDrawCommandFlags.LODCrossFadeValuePacked;
				batchDrawCommandFlags |= global::UnityEngine.Rendering.BatchDrawCommandFlags.UseLegacyLightmapsKeyword;
				if (gPUDrivenPackedMaterialData.isMotionVectorsPassEnabled)
				{
					batchDrawCommandFlags |= global::UnityEngine.Rendering.BatchDrawCommandFlags.HasMotion;
				}
				if (gPUDrivenPackedMaterialData.isTransparent)
				{
					batchDrawCommandFlags |= global::UnityEngine.Rendering.BatchDrawCommandFlags.HasSortingPosition;
				}
				if (gPUDrivenPackedMaterialData.supportsCrossFade)
				{
					batchDrawCommandFlags |= global::UnityEngine.Rendering.BatchDrawCommandFlags.LODCrossFadeKeyword;
				}
				int num10 = global::Unity.Mathematics.math.max(gPUDrivenMeshLodInfo.levelCount, 1);
				for (int l = 0; l < num10; l++)
				{
					int num11 = num3 + k;
					global::UnityEngine.Rendering.SubMeshDescriptor subMeshDescriptor = rendererData.subMeshDesc[num2 + num11 * num10 + l];
					global::UnityEngine.Rendering.DrawKey key4 = new global::UnityEngine.Rendering.DrawKey
					{
						materialID = item2,
						meshID = meshID,
						submeshIndex = num11,
						activeMeshLod = (gPUDrivenMeshLodInfo.lodSelectionActive ? l : (-1)),
						flags = batchDrawCommandFlags,
						transparentInstanceId = (gPUDrivenPackedMaterialData.isTransparent ? ((int)entityId) : 0),
						range = key3,
						overridenComponents = (uint)instanceComponentGroup,
						lightmapIndex = num7
					};
					ref global::UnityEngine.Rendering.DrawBatch reference2 = ref EditDrawBatch(in key4, in subMeshDescriptor, batchHash, drawBatches);
					if (reference2.instanceCount == 0)
					{
						reference.drawCount++;
					}
					reference2.instanceCount += num8;
					for (int m = 0; m < num8; m++)
					{
						int index4 = num9 + m;
						global::UnityEngine.Rendering.InstanceHandle instanceHandle = instances[index4];
						drawInstances.Add(new global::UnityEngine.Rendering.DrawInstance
						{
							key = key4,
							instanceIndex = instanceHandle.index
						});
					}
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002ECreateDrawBatches_0000018C_0024PostfixBurstDelegate))]
		public static void CreateDrawBatches(bool implicitInstanceIndices, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMeshID> batchMeshHash, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID> batchMaterialHash, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDataHash, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances)
		{
			global::UnityEngine.Rendering.InstanceCullingBatcherBurst.CreateDrawBatches_0000018C_0024BurstDirectCall.Invoke(implicitInstanceIndices, in instances, in rendererData, in batchMeshHash, in batchMaterialHash, in packedMaterialDataHash, ref rangeHash, ref drawRanges, ref batchHash, ref drawBatches, ref drawInstances);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal unsafe static void RemoveDrawInstanceIndices_0024BurstManaged(in global::Unity.Collections.NativeArray<int> drawInstanceIndices, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches)
		{
			global::UnityEngine.Rendering.DrawInstance* unsafePtr = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(drawInstances);
			int num = drawInstances.Length - 1;
			for (int num2 = drawInstanceIndices.Length - 1; num2 >= 0; num2--)
			{
				int num3 = drawInstanceIndices[num2];
				global::UnityEngine.Rendering.DrawInstance* ptr = unsafePtr + num3;
				int index = batchHash[ptr->key];
				ref global::UnityEngine.Rendering.DrawBatch reference = ref drawBatches.ElementAt(index);
				if (--reference.instanceCount == 0)
				{
					RemoveDrawBatch(in reference.key, ref drawRanges, ref rangeHash, ref batchHash, ref drawBatches);
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, unsafePtr + num--, sizeof(global::UnityEngine.Rendering.DrawInstance));
			}
			drawInstances.ResizeUninitialized(num + 1);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal static void CreateDrawBatches_0024BurstManaged(bool implicitInstanceIndices, in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, in global::UnityEngine.Rendering.GPUDrivenRendererGroupData rendererData, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMeshID> batchMeshHash, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.BatchMaterialID> batchMaterialHash, in global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.EntityId, global::UnityEngine.Rendering.GPUDrivenPackedMaterialData> packedMaterialDataHash, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.RangeKey, int> rangeHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawRange> drawRanges, ref global::Unity.Collections.NativeParallelHashMap<global::UnityEngine.Rendering.DrawKey, int> batchHash, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawBatch> drawBatches, ref global::Unity.Collections.NativeList<global::UnityEngine.Rendering.DrawInstance> drawInstances)
		{
			for (int i = 0; i < rendererData.rendererGroupID.Length; i++)
			{
				ProcessRenderer(i, implicitInstanceIndices, in rendererData, batchMeshHash, packedMaterialDataHash, batchMaterialHash, instances, drawInstances, rangeHash, drawRanges, batchHash, drawBatches);
			}
		}
	}
}
