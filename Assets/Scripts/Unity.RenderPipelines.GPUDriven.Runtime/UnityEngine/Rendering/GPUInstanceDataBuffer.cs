namespace UnityEngine.Rendering
{
	internal class GPUInstanceDataBuffer : global::System.IDisposable
	{
		internal readonly struct ReadOnly
		{
			private readonly global::Unity.Collections.NativeArray<int> instancesNumPrefixSum;

			public ReadOnly(global::UnityEngine.Rendering.GPUInstanceDataBuffer buffer)
			{
				instancesNumPrefixSum = buffer.instancesNumPrefixSum;
			}

			public global::UnityEngine.Rendering.GPUInstanceIndex CPUInstanceToGPUInstance(global::UnityEngine.Rendering.InstanceHandle instance)
			{
				return global::UnityEngine.Rendering.GPUInstanceDataBuffer.CPUInstanceToGPUInstance(in instancesNumPrefixSum, instance);
			}

			public void CPUInstanceArrayToGPUInstanceArray(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> gpuInstanceIndices)
			{
				global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.GPUInstanceDataBuffer.ConvertCPUInstancesToGPUInstancesJob
				{
					instancesNumPrefixSum = instancesNumPrefixSum,
					instances = instances,
					gpuInstanceIndices = gpuInstanceIndices
				}, instances.Length, 512).Complete();
			}
		}

		[global::Unity.Burst.BurstCompile(DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
		private struct ConvertCPUInstancesToGPUInstancesJob : global::Unity.Jobs.IJobParallelFor
		{
			public const int k_BatchSize = 512;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<int> instancesNumPrefixSum;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances;

			[global::Unity.Collections.WriteOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> gpuInstanceIndices;

			public void Execute(int index)
			{
				gpuInstanceIndices[index] = CPUInstanceToGPUInstance(in instancesNumPrefixSum, instances[index]);
			}
		}

		private static int s_NextLayoutVersion;

		public global::UnityEngine.Rendering.InstanceNumInfo instanceNumInfo;

		public global::Unity.Collections.NativeArray<int> instancesNumPrefixSum;

		public global::Unity.Collections.NativeArray<int> instancesSpan;

		public int byteSize;

		public int perInstanceComponentCount;

		public int version;

		public int layoutVersion;

		public global::UnityEngine.GraphicsBuffer gpuBuffer;

		public global::UnityEngine.GraphicsBuffer validComponentsIndicesGpuBuffer;

		public global::UnityEngine.GraphicsBuffer componentAddressesGpuBuffer;

		public global::UnityEngine.GraphicsBuffer componentInstanceIndexRangesGpuBuffer;

		public global::UnityEngine.GraphicsBuffer componentByteCountsGpuBuffer;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceComponentDesc> descriptions;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.MetadataValue> defaultMetadata;

		public global::Unity.Collections.NativeArray<int> gpuBufferComponentAddress;

		public global::Unity.Collections.NativeParallelHashMap<int, int> nameToMetadataMap;

		public bool valid => instancesSpan.IsCreated;

		public static int NextVersion()
		{
			return ++s_NextLayoutVersion;
		}

		private static global::UnityEngine.Rendering.GPUInstanceIndex CPUInstanceToGPUInstance(in global::Unity.Collections.NativeArray<int> instancesNumPrefixSum, global::UnityEngine.Rendering.InstanceHandle instance)
		{
			if (!instance.valid || instance.type >= global::UnityEngine.Rendering.InstanceType.Count)
			{
				return global::UnityEngine.Rendering.GPUInstanceIndex.Invalid;
			}
			int type = (int)instance.type;
			int instanceIndex = instance.instanceIndex;
			int index = instancesNumPrefixSum[type] + instanceIndex;
			return new global::UnityEngine.Rendering.GPUInstanceIndex
			{
				index = index
			};
		}

		public int GetPropertyIndex(int propertyID, bool assertOnFail = true)
		{
			if (nameToMetadataMap.TryGetValue(propertyID, out var item))
			{
				return item;
			}
			return -1;
		}

		public int GetGpuAddress(string strName, bool assertOnFail = true)
		{
			int propertyIndex = GetPropertyIndex(global::UnityEngine.Shader.PropertyToID(strName), assertOnFail: false);
			if (assertOnFail)
			{
				_ = -1;
			}
			if (propertyIndex == -1)
			{
				return -1;
			}
			return gpuBufferComponentAddress[propertyIndex];
		}

		public int GetGpuAddress(int propertyID, bool assertOnFail = true)
		{
			int propertyIndex = GetPropertyIndex(propertyID, assertOnFail);
			if (propertyIndex == -1)
			{
				return -1;
			}
			return gpuBufferComponentAddress[propertyIndex];
		}

		public global::UnityEngine.Rendering.GPUInstanceIndex CPUInstanceToGPUInstance(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			return CPUInstanceToGPUInstance(in instancesNumPrefixSum, instance);
		}

		public global::UnityEngine.Rendering.InstanceHandle GPUInstanceToCPUInstance(global::UnityEngine.Rendering.GPUInstanceIndex gpuInstanceIndex)
		{
			int num = gpuInstanceIndex.index;
			global::UnityEngine.Rendering.InstanceType instanceType = global::UnityEngine.Rendering.InstanceType.Count;
			for (int i = 0; i < 2; i++)
			{
				int instanceNum = instanceNumInfo.GetInstanceNum((global::UnityEngine.Rendering.InstanceType)i);
				if (num < instanceNum)
				{
					instanceType = (global::UnityEngine.Rendering.InstanceType)i;
					break;
				}
				num -= instanceNum;
			}
			if (instanceType == global::UnityEngine.Rendering.InstanceType.Count)
			{
				return global::UnityEngine.Rendering.InstanceHandle.Invalid;
			}
			return global::UnityEngine.Rendering.InstanceHandle.Create(num, instanceType);
		}

		public void CPUInstanceArrayToGPUInstanceArray(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceIndex> gpuInstanceIndices)
		{
			global::Unity.Jobs.IJobParallelForExtensions.Schedule(new global::UnityEngine.Rendering.GPUInstanceDataBuffer.ConvertCPUInstancesToGPUInstancesJob
			{
				instancesNumPrefixSum = instancesNumPrefixSum,
				instances = instances,
				gpuInstanceIndices = gpuInstanceIndices
			}, instances.Length, 512).Complete();
		}

		public void Dispose()
		{
			if (instancesSpan.IsCreated)
			{
				instancesSpan.Dispose();
			}
			if (instancesNumPrefixSum.IsCreated)
			{
				instancesNumPrefixSum.Dispose();
			}
			if (descriptions.IsCreated)
			{
				descriptions.Dispose();
			}
			if (defaultMetadata.IsCreated)
			{
				defaultMetadata.Dispose();
			}
			if (gpuBufferComponentAddress.IsCreated)
			{
				gpuBufferComponentAddress.Dispose();
			}
			if (nameToMetadataMap.IsCreated)
			{
				nameToMetadataMap.Dispose();
			}
			if (gpuBuffer != null)
			{
				gpuBuffer.Release();
			}
			if (validComponentsIndicesGpuBuffer != null)
			{
				validComponentsIndicesGpuBuffer.Release();
			}
			if (componentAddressesGpuBuffer != null)
			{
				componentAddressesGpuBuffer.Release();
			}
			if (componentInstanceIndexRangesGpuBuffer != null)
			{
				componentInstanceIndexRangesGpuBuffer.Release();
			}
			if (componentByteCountsGpuBuffer != null)
			{
				componentByteCountsGpuBuffer.Release();
			}
		}

		public global::UnityEngine.Rendering.GPUInstanceDataBuffer.ReadOnly AsReadOnly()
		{
			return new global::UnityEngine.Rendering.GPUInstanceDataBuffer.ReadOnly(this);
		}
	}
}
