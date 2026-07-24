namespace UnityEngine.Rendering
{
	internal struct GPUInstanceDataBufferBuilder : global::System.IDisposable
	{
		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceComponentDesc> m_Components;

		private global::UnityEngine.Rendering.MetadataValue CreateMetadataValue(int nameID, int gpuAddress, bool isOverridden)
		{
			return new global::UnityEngine.Rendering.MetadataValue
			{
				NameID = nameID,
				Value = (uint)(gpuAddress | (isOverridden ? int.MinValue : 0))
			};
		}

		public void AddComponent<T>(int propertyID, bool isOverriden, bool isPerInstance, global::UnityEngine.Rendering.InstanceType instanceType, global::UnityEngine.Rendering.InstanceComponentGroup componentGroup = global::UnityEngine.Rendering.InstanceComponentGroup.Default) where T : unmanaged
		{
			AddComponent(propertyID, isOverriden, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(), isPerInstance, instanceType, componentGroup);
		}

		public void AddComponent(int propertyID, bool isOverriden, int byteSize, bool isPerInstance, global::UnityEngine.Rendering.InstanceType instanceType, global::UnityEngine.Rendering.InstanceComponentGroup componentGroup)
		{
			if (!m_Components.IsCreated)
			{
				m_Components = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.GPUInstanceComponentDesc>(64, global::Unity.Collections.Allocator.Temp);
			}
			_ = m_Components.Length;
			_ = 0;
			m_Components.Add(new global::UnityEngine.Rendering.GPUInstanceComponentDesc(propertyID, byteSize, isOverriden, isPerInstance, instanceType, componentGroup));
		}

		public unsafe global::UnityEngine.Rendering.GPUInstanceDataBuffer Build(in global::UnityEngine.Rendering.InstanceNumInfo instanceNumInfo)
		{
			int num = 0;
			global::Unity.Collections.NativeArray<int> data = new global::Unity.Collections.NativeArray<int>(m_Components.Length, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<int> data2 = new global::Unity.Collections.NativeArray<int>(m_Components.Length, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<int> data3 = new global::Unity.Collections.NativeArray<int>(m_Components.Length, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector2Int> data4 = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector2Int>(m_Components.Length, global::Unity.Collections.Allocator.Temp);
			global::UnityEngine.Rendering.GPUInstanceDataBuffer gPUInstanceDataBuffer = new global::UnityEngine.Rendering.GPUInstanceDataBuffer();
			gPUInstanceDataBuffer.instanceNumInfo = instanceNumInfo;
			gPUInstanceDataBuffer.instancesNumPrefixSum = new global::Unity.Collections.NativeArray<int>(2, global::Unity.Collections.Allocator.Persistent);
			gPUInstanceDataBuffer.instancesSpan = new global::Unity.Collections.NativeArray<int>(2, global::Unity.Collections.Allocator.Persistent);
			int num2 = 0;
			for (int i = 0; i < 2; i++)
			{
				gPUInstanceDataBuffer.instancesNumPrefixSum[i] = num2;
				num2 += instanceNumInfo.InstanceNums[i];
				gPUInstanceDataBuffer.instancesSpan[i] = instanceNumInfo.GetInstanceNumIncludingChildren((global::UnityEngine.Rendering.InstanceType)i);
			}
			gPUInstanceDataBuffer.layoutVersion = global::UnityEngine.Rendering.GPUInstanceDataBuffer.NextVersion();
			gPUInstanceDataBuffer.version = 0;
			gPUInstanceDataBuffer.defaultMetadata = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.MetadataValue>(m_Components.Length, global::Unity.Collections.Allocator.Persistent);
			gPUInstanceDataBuffer.descriptions = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUInstanceComponentDesc>(m_Components.Length, global::Unity.Collections.Allocator.Persistent);
			gPUInstanceDataBuffer.nameToMetadataMap = new global::Unity.Collections.NativeParallelHashMap<int, int>(m_Components.Length, global::Unity.Collections.Allocator.Persistent);
			gPUInstanceDataBuffer.gpuBufferComponentAddress = new global::Unity.Collections.NativeArray<int>(m_Components.Length, global::Unity.Collections.Allocator.Persistent);
			int num3 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.Vector4>();
			int num4 = 4 * num3;
			for (int j = 0; j < m_Components.Length; j++)
			{
				global::UnityEngine.Rendering.GPUInstanceComponentDesc value = m_Components[j];
				gPUInstanceDataBuffer.descriptions[j] = value;
				int num5 = gPUInstanceDataBuffer.instancesNumPrefixSum[(int)value.instanceType];
				int num6 = num5 + gPUInstanceDataBuffer.instancesSpan[(int)value.instanceType];
				int num7 = ((!value.isPerInstance) ? 1 : (num6 - num5));
				data4[j] = new global::UnityEngine.Vector2Int(num5, num5 + num7);
				int num8 = num4 - num5 * value.byteSize;
				gPUInstanceDataBuffer.gpuBufferComponentAddress[j] = num8;
				gPUInstanceDataBuffer.defaultMetadata[j] = CreateMetadataValue(value.propertyID, num8, value.isOverriden);
				data2[j] = num8;
				data3[j] = value.byteSize;
				int num9 = value.byteSize * num7;
				num4 += num9;
				gPUInstanceDataBuffer.nameToMetadataMap.TryAdd(value.propertyID, j);
				if (value.isPerInstance)
				{
					data[num] = j;
					num++;
				}
			}
			gPUInstanceDataBuffer.byteSize = num4;
			gPUInstanceDataBuffer.gpuBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Raw, gPUInstanceDataBuffer.byteSize / 4, 4);
			gPUInstanceDataBuffer.gpuBuffer.SetData(new global::Unity.Collections.NativeArray<global::UnityEngine.Vector4>(4, global::Unity.Collections.Allocator.Temp), 0, 0, 4);
			gPUInstanceDataBuffer.validComponentsIndicesGpuBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Raw, num, 4);
			gPUInstanceDataBuffer.validComponentsIndicesGpuBuffer.SetData(data, 0, 0, num);
			gPUInstanceDataBuffer.componentAddressesGpuBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Raw, m_Components.Length, 4);
			gPUInstanceDataBuffer.componentAddressesGpuBuffer.SetData(data2, 0, 0, m_Components.Length);
			gPUInstanceDataBuffer.componentInstanceIndexRangesGpuBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Raw, m_Components.Length, 8);
			gPUInstanceDataBuffer.componentInstanceIndexRangesGpuBuffer.SetData(data4, 0, 0, m_Components.Length);
			gPUInstanceDataBuffer.componentByteCountsGpuBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Raw, m_Components.Length, 4);
			gPUInstanceDataBuffer.componentByteCountsGpuBuffer.SetData(data3, 0, 0, m_Components.Length);
			gPUInstanceDataBuffer.perInstanceComponentCount = num;
			data.Dispose();
			data2.Dispose();
			data3.Dispose();
			return gPUInstanceDataBuffer;
		}

		public void Dispose()
		{
			if (m_Components.IsCreated)
			{
				m_Components.Dispose();
			}
		}
	}
}
