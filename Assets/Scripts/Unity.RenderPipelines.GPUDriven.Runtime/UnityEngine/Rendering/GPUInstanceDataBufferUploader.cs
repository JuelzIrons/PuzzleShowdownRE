namespace UnityEngine.Rendering
{
	internal struct GPUInstanceDataBufferUploader : global::System.IDisposable
	{
		private static class UploadKernelIDs
		{
			public static readonly int _InputValidComponentCounts = global::UnityEngine.Shader.PropertyToID("_InputValidComponentCounts");

			public static readonly int _InputInstanceCounts = global::UnityEngine.Shader.PropertyToID("_InputInstanceCounts");

			public static readonly int _InputInstanceByteSize = global::UnityEngine.Shader.PropertyToID("_InputInstanceByteSize");

			public static readonly int _InputComponentOffsets = global::UnityEngine.Shader.PropertyToID("_InputComponentOffsets");

			public static readonly int _InputInstanceData = global::UnityEngine.Shader.PropertyToID("_InputInstanceData");

			public static readonly int _InputInstanceIndices = global::UnityEngine.Shader.PropertyToID("_InputInstanceIndices");

			public static readonly int _InputValidComponentIndices = global::UnityEngine.Shader.PropertyToID("_InputValidComponentIndices");

			public static readonly int _InputComponentAddresses = global::UnityEngine.Shader.PropertyToID("_InputComponentAddresses");

			public static readonly int _InputComponentByteCounts = global::UnityEngine.Shader.PropertyToID("_InputComponentByteCounts");

			public static readonly int _InputComponentInstanceIndexRanges = global::UnityEngine.Shader.PropertyToID("_InputComponentInstanceIndexRanges");

			public static readonly int _OutputBuffer = global::UnityEngine.Shader.PropertyToID("_OutputBuffer");
		}

		public struct GPUResources : global::System.IDisposable
		{
			public global::UnityEngine.ComputeBuffer instanceData;

			public global::UnityEngine.ComputeBuffer instanceIndices;

			public global::UnityEngine.ComputeBuffer inputComponentOffsets;

			public global::UnityEngine.ComputeBuffer validComponentIndices;

			public global::UnityEngine.ComputeShader cs;

			public int kernelId;

			private int m_InstanceDataByteSize;

			private int m_InstanceCount;

			private int m_ComponentCounts;

			private int m_ValidComponentIndicesCount;

			public void LoadShaders(global::UnityEngine.Rendering.GPUResidentDrawerResources resources)
			{
				if (cs == null)
				{
					cs = resources.instanceDataBufferUploadKernels;
					kernelId = cs.FindKernel("MainUploadScatterInstances");
				}
			}

			public void CreateResources(int newInstanceCount, int sizePerInstance, int newComponentCounts, int validComponentIndicesCount)
			{
				int num = newInstanceCount * sizePerInstance;
				if (num > m_InstanceDataByteSize || instanceData == null)
				{
					if (instanceData != null)
					{
						instanceData.Release();
					}
					instanceData = new global::UnityEngine.ComputeBuffer((num + 3) / 4, 4, global::UnityEngine.ComputeBufferType.Raw);
					m_InstanceDataByteSize = num;
				}
				if (newInstanceCount > m_InstanceCount || instanceIndices == null)
				{
					if (instanceIndices != null)
					{
						instanceIndices.Release();
					}
					instanceIndices = new global::UnityEngine.ComputeBuffer(newInstanceCount, 4, global::UnityEngine.ComputeBufferType.Raw);
					m_InstanceCount = newInstanceCount;
				}
				if (newComponentCounts > m_ComponentCounts || inputComponentOffsets == null)
				{
					if (inputComponentOffsets != null)
					{
						inputComponentOffsets.Release();
					}
					inputComponentOffsets = new global::UnityEngine.ComputeBuffer(newComponentCounts, 4, global::UnityEngine.ComputeBufferType.Raw);
					m_ComponentCounts = newComponentCounts;
				}
				if (validComponentIndicesCount > m_ValidComponentIndicesCount || validComponentIndices == null)
				{
					if (validComponentIndices != null)
					{
						validComponentIndices.Release();
					}
					validComponentIndices = new global::UnityEngine.ComputeBuffer(validComponentIndicesCount, 4, global::UnityEngine.ComputeBufferType.Raw);
					m_ValidComponentIndicesCount = validComponentIndicesCount;
				}
			}

			public void Dispose()
			{
				cs = null;
				if (instanceData != null)
				{
					instanceData.Release();
				}
				if (instanceIndices != null)
				{
					instanceIndices.Release();
				}
				if (inputComponentOffsets != null)
				{
					inputComponentOffsets.Release();
				}
				if (validComponentIndices != null)
				{
					validComponentIndices.Release();
				}
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		internal struct WriteInstanceDataParameterJob : global::Unity.Jobs.IJobParallelFor
		{
			public const int k_BatchSize = 512;

			[global::Unity.Collections.ReadOnly]
			public bool gatherData;

			[global::Unity.Collections.ReadOnly]
			public int parameterIndex;

			[global::Unity.Collections.ReadOnly]
			public int uintPerParameter;

			[global::Unity.Collections.ReadOnly]
			public int uintPerInstance;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<int> componentDataIndex;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<int> gatherIndices;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<uint> instanceData;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
			[global::Unity.Burst.NoAlias]
			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<uint> tmpDataBuffer;

			public unsafe void Execute(int index)
			{
				int num = (gatherData ? gatherIndices[index] : index) * uintPerParameter;
				int num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<uint>();
				uint* source = (uint*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(instanceData) + num;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy((byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(tmpDataBuffer) + (nint)(index * uintPerInstance) * (nint)4 + (nint)componentDataIndex[parameterIndex] * (nint)4, source, uintPerParameter * num2);
			}
		}

		private int m_UintPerInstance;

		private int m_Capacity;

		private int m_InstanceCount;

		private global::Unity.Collections.NativeArray<bool> m_ComponentIsInstanced;

		private global::Unity.Collections.NativeArray<int> m_ComponentDataIndex;

		private global::Unity.Collections.NativeArray<int> m_DescriptionsUintSize;

		private global::Unity.Collections.NativeArray<uint> m_TmpDataBuffer;

		private global::Unity.Collections.NativeList<int> m_WritenComponentIndices;

		private global::Unity.Collections.NativeArray<int> m_DummyArray;

		public GPUInstanceDataBufferUploader(in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceComponentDesc> descriptions, int capacity, global::UnityEngine.Rendering.InstanceType instanceType)
		{
			m_Capacity = capacity;
			m_InstanceCount = 0;
			m_UintPerInstance = 0;
			m_ComponentDataIndex = new global::Unity.Collections.NativeArray<int>(descriptions.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			m_ComponentIsInstanced = new global::Unity.Collections.NativeArray<bool>(descriptions.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			m_DescriptionsUintSize = new global::Unity.Collections.NativeArray<int>(descriptions.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			m_WritenComponentIndices = new global::Unity.Collections.NativeList<int>(descriptions.Length, global::Unity.Collections.Allocator.TempJob);
			m_DummyArray = new global::Unity.Collections.NativeArray<int>(0, global::Unity.Collections.Allocator.Persistent);
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<uint>();
			for (int i = 0; i < descriptions.Length; i++)
			{
				global::UnityEngine.Rendering.GPUInstanceComponentDesc gPUInstanceComponentDesc = descriptions[i];
				m_ComponentIsInstanced[i] = gPUInstanceComponentDesc.isPerInstance;
				if (gPUInstanceComponentDesc.instanceType == instanceType)
				{
					m_ComponentDataIndex[i] = m_UintPerInstance;
					m_DescriptionsUintSize[i] = descriptions[i].byteSize / num;
					m_UintPerInstance += (gPUInstanceComponentDesc.isPerInstance ? (gPUInstanceComponentDesc.byteSize / num) : 0);
				}
				else
				{
					m_ComponentDataIndex[i] = -1;
					m_DescriptionsUintSize[i] = 0;
				}
			}
			m_TmpDataBuffer = new global::Unity.Collections.NativeArray<uint>(m_Capacity * m_UintPerInstance, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
		}

		public unsafe global::System.IntPtr GetUploadBufferPtr()
		{
			return new global::System.IntPtr(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_TmpDataBuffer));
		}

		public int GetUIntPerInstance()
		{
			return m_UintPerInstance;
		}

		public int GetParamUIntOffset(int parameterIndex)
		{
			return m_ComponentDataIndex[parameterIndex];
		}

		public int PrepareParamWrite<T>(int parameterIndex) where T : unmanaged
		{
			_ = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() / global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<uint>();
			if (!global::Unity.Collections.NativeListExtensions.Contains(m_WritenComponentIndices, parameterIndex))
			{
				m_WritenComponentIndices.Add(in parameterIndex);
			}
			return GetParamUIntOffset(parameterIndex);
		}

		public void AllocateUploadHandles(int handlesLength)
		{
			m_InstanceCount = handlesLength;
		}

		public global::Unity.Jobs.JobHandle WriteInstanceDataJob<T>(int parameterIndex, global::Unity.Collections.NativeArray<T> instanceData) where T : unmanaged
		{
			return WriteInstanceDataJob(parameterIndex, instanceData, m_DummyArray);
		}

		public global::Unity.Jobs.JobHandle WriteInstanceDataJob<T>(int parameterIndex, global::Unity.Collections.NativeArray<T> instanceData, global::Unity.Collections.NativeArray<int> gatherIndices) where T : unmanaged
		{
			if (m_InstanceCount == 0)
			{
				return default(global::Unity.Jobs.JobHandle);
			}
			bool gatherData = gatherIndices.Length != 0;
			int uintPerParameter = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() / global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<uint>();
			if (!global::Unity.Collections.NativeListExtensions.Contains(m_WritenComponentIndices, parameterIndex))
			{
				m_WritenComponentIndices.Add(in parameterIndex);
			}
			return global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.WriteInstanceDataParameterJob
			{
				gatherData = gatherData,
				gatherIndices = gatherIndices,
				parameterIndex = parameterIndex,
				uintPerParameter = uintPerParameter,
				uintPerInstance = m_UintPerInstance,
				componentDataIndex = m_ComponentDataIndex,
				instanceData = instanceData.Reinterpret<uint>(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>()),
				tmpDataBuffer = m_TmpDataBuffer
			}, m_InstanceCount, 512);
		}

		public void SubmitToGpu(global::UnityEngine.Rendering.GPUInstanceDataBuffer instanceDataBuffer, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> gpuInstanceIndices, ref global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.GPUResources gpuResources, bool submitOnlyWrittenParams)
		{
			if (m_InstanceCount != 0)
			{
				instanceDataBuffer.version++;
				int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<uint>();
				int num2 = m_UintPerInstance * num;
				gpuResources.CreateResources(m_InstanceCount, num2, m_ComponentDataIndex.Length, m_WritenComponentIndices.Length);
				gpuResources.instanceData.SetData(m_TmpDataBuffer, 0, 0, m_InstanceCount * m_UintPerInstance);
				gpuResources.instanceIndices.SetData(gpuInstanceIndices, 0, 0, m_InstanceCount);
				gpuResources.inputComponentOffsets.SetData(m_ComponentDataIndex, 0, 0, m_ComponentDataIndex.Length);
				gpuResources.cs.SetInt(global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputInstanceCounts, m_InstanceCount);
				gpuResources.cs.SetInt(global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputInstanceByteSize, num2);
				gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputInstanceData, gpuResources.instanceData);
				gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputInstanceIndices, gpuResources.instanceIndices);
				gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputComponentOffsets, gpuResources.inputComponentOffsets);
				if (submitOnlyWrittenParams)
				{
					gpuResources.validComponentIndices.SetData(m_WritenComponentIndices.AsArray(), 0, 0, m_WritenComponentIndices.Length);
					gpuResources.cs.SetInt(global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputValidComponentCounts, m_WritenComponentIndices.Length);
					gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputValidComponentIndices, gpuResources.validComponentIndices);
				}
				else
				{
					gpuResources.cs.SetInt(global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputValidComponentCounts, instanceDataBuffer.perInstanceComponentCount);
					gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputValidComponentIndices, instanceDataBuffer.validComponentsIndicesGpuBuffer);
				}
				gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputComponentAddresses, instanceDataBuffer.componentAddressesGpuBuffer);
				gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputComponentByteCounts, instanceDataBuffer.componentByteCountsGpuBuffer);
				gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._InputComponentInstanceIndexRanges, instanceDataBuffer.componentInstanceIndexRangesGpuBuffer);
				gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.UploadKernelIDs._OutputBuffer, instanceDataBuffer.gpuBuffer);
				gpuResources.cs.Dispatch(gpuResources.kernelId, (m_InstanceCount + 63) / 64, 1, 1);
				m_InstanceCount = 0;
				m_WritenComponentIndices.Clear();
			}
		}

		public void SubmitToGpu(global::UnityEngine.Rendering.GPUInstanceDataBuffer instanceDataBuffer, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, ref global::UnityEngine.Rendering.GPUInstanceDataBufferUploader.GPUResources gpuResources, bool submitOnlyWrittenParams)
		{
			if (m_InstanceCount != 0)
			{
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> gpuInstanceIndices = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex>(instances.Length, global::Unity.Collections.Allocator.TempJob, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				instanceDataBuffer.CPUInstanceArrayToGPUInstanceArray(instances, gpuInstanceIndices);
				SubmitToGpu(instanceDataBuffer, gpuInstanceIndices, ref gpuResources, submitOnlyWrittenParams);
				gpuInstanceIndices.Dispose();
			}
		}

		public void Dispose()
		{
			if (m_ComponentDataIndex.IsCreated)
			{
				m_ComponentDataIndex.Dispose();
			}
			if (m_ComponentIsInstanced.IsCreated)
			{
				m_ComponentIsInstanced.Dispose();
			}
			if (m_DescriptionsUintSize.IsCreated)
			{
				m_DescriptionsUintSize.Dispose();
			}
			if (m_TmpDataBuffer.IsCreated)
			{
				m_TmpDataBuffer.Dispose();
			}
			if (m_WritenComponentIndices.IsCreated)
			{
				m_WritenComponentIndices.Dispose();
			}
			if (m_DummyArray.IsCreated)
			{
				m_DummyArray.Dispose();
			}
		}
	}
}
