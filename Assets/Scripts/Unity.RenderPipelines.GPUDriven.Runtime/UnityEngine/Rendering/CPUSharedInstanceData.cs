namespace UnityEngine.Rendering
{
	internal struct CPUSharedInstanceData : global::System.IDisposable
	{
		internal readonly struct ReadOnly
		{
			public readonly global::Unity.Collections.NativeArray<int>.ReadOnly instanceIndices;

			public readonly global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SharedInstanceHandle>.ReadOnly instances;

			public readonly global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly rendererGroupIDs;

			public readonly global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SmallEntityIdArray>.ReadOnly materialIDArrays;

			public readonly global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>.ReadOnly meshIDs;

			public readonly global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.AABB>.ReadOnly localAABBs;

			public readonly global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.CPUSharedInstanceFlags>.ReadOnly flags;

			public readonly global::Unity.Collections.NativeArray<uint>.ReadOnly lodGroupAndMasks;

			public readonly global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenMeshLodInfo>.ReadOnly meshLodInfos;

			public readonly global::Unity.Collections.NativeArray<int>.ReadOnly gameObjectLayers;

			public readonly global::Unity.Collections.NativeArray<int>.ReadOnly refCounts;

			public int handlesLength => instanceIndices.Length;

			public int instancesLength => instances.Length;

			public ReadOnly(in global::UnityEngine.Rendering.CPUSharedInstanceData instanceData)
			{
				instanceIndices = instanceData.m_InstanceIndices.AsArray().AsReadOnly();
				instances = instanceData.instances.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				rendererGroupIDs = instanceData.rendererGroupIDs.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				materialIDArrays = instanceData.materialIDArrays.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				meshIDs = instanceData.meshIDs.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				localAABBs = instanceData.localAABBs.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				flags = instanceData.flags.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				lodGroupAndMasks = instanceData.lodGroupAndMasks.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				meshLodInfos = instanceData.meshLodInfos.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				gameObjectLayers = instanceData.gameObjectLayers.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				refCounts = instanceData.refCounts.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
			}

			public int SharedInstanceToIndex(global::UnityEngine.Rendering.SharedInstanceHandle instance)
			{
				return instanceIndices[instance.index];
			}

			public global::UnityEngine.Rendering.SharedInstanceHandle IndexToSharedInstance(int index)
			{
				return instances[index];
			}

			public bool IsValidSharedInstance(global::UnityEngine.Rendering.SharedInstanceHandle instance)
			{
				if (instance.valid && instance.index < instanceIndices.Length)
				{
					int num = instanceIndices[instance.index];
					if (num >= 0 && num < instances.Length)
					{
						return instances[num].Equals(instance);
					}
					return false;
				}
				return false;
			}

			public bool IsValidIndex(int index)
			{
				if (index >= 0 && index < instances.Length)
				{
					global::UnityEngine.Rendering.SharedInstanceHandle sharedInstanceHandle = instances[index];
					return index == instanceIndices[sharedInstanceHandle.index];
				}
				return false;
			}

			public int InstanceToIndex(in global::UnityEngine.Rendering.CPUInstanceData.ReadOnly instanceData, global::UnityEngine.Rendering.InstanceHandle instance)
			{
				int index = instanceData.InstanceToIndex(instance);
				global::UnityEngine.Rendering.SharedInstanceHandle instance2 = instanceData.sharedInstances[index];
				return SharedInstanceToIndex(instance2);
			}
		}

		private const int k_InvalidIndex = -1;

		private const uint k_InvalidLODGroupAndMask = uint.MaxValue;

		private global::Unity.Collections.NativeArray<int> m_StructData;

		private global::Unity.Collections.NativeList<int> m_InstanceIndices;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SharedInstanceHandle> instances;

		public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> rendererGroupIDs;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SmallEntityIdArray> materialIDArrays;

		public global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> meshIDs;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.AABB> localAABBs;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.CPUSharedInstanceFlags> flags;

		public global::Unity.Collections.NativeArray<uint> lodGroupAndMasks;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenMeshLodInfo> meshLodInfos;

		public global::Unity.Collections.NativeArray<int> gameObjectLayers;

		public global::Unity.Collections.NativeArray<int> refCounts;

		public int instancesLength
		{
			get
			{
				return m_StructData[0];
			}
			set
			{
				m_StructData[0] = value;
			}
		}

		public int instancesCapacity
		{
			get
			{
				return m_StructData[1];
			}
			set
			{
				m_StructData[1] = value;
			}
		}

		public int handlesLength => m_InstanceIndices.Length;

		public void Initialize(int initCapacity)
		{
			m_StructData = new global::Unity.Collections.NativeArray<int>(2, global::Unity.Collections.Allocator.Persistent);
			instancesCapacity = initCapacity;
			m_InstanceIndices = new global::Unity.Collections.NativeList<int>(global::Unity.Collections.Allocator.Persistent);
			instances = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SharedInstanceHandle>(instancesCapacity, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			instances.FillArray(in global::UnityEngine.Rendering.SharedInstanceHandle.Invalid);
			rendererGroupIDs = new global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			materialIDArrays = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SmallEntityIdArray>(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			meshIDs = new global::Unity.Collections.NativeArray<global::UnityEngine.EntityId>(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			localAABBs = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.AABB>(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			flags = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.CPUSharedInstanceFlags>(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			lodGroupAndMasks = new global::Unity.Collections.NativeArray<uint>(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			global::UnityEngine.Rendering.ArrayExtensions.FillArray(ref lodGroupAndMasks, uint.MaxValue);
			meshLodInfos = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenMeshLodInfo>(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			gameObjectLayers = new global::Unity.Collections.NativeArray<int>(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			refCounts = new global::Unity.Collections.NativeArray<int>(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
		}

		public void Dispose()
		{
			m_StructData.Dispose();
			m_InstanceIndices.Dispose();
			instances.Dispose();
			rendererGroupIDs.Dispose();
			foreach (global::UnityEngine.Rendering.SmallEntityIdArray materialIDArray in materialIDArrays)
			{
				materialIDArray.Dispose();
			}
			materialIDArrays.Dispose();
			meshIDs.Dispose();
			localAABBs.Dispose();
			flags.Dispose();
			lodGroupAndMasks.Dispose();
			meshLodInfos.Dispose();
			gameObjectLayers.Dispose();
			refCounts.Dispose();
		}

		private void Grow(int newCapacity)
		{
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref instances, newCapacity);
			instances.FillArray(in global::UnityEngine.Rendering.SharedInstanceHandle.Invalid, instancesCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref rendererGroupIDs, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref materialIDArrays, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.FillArray(ref materialIDArrays, default(global::UnityEngine.Rendering.SmallEntityIdArray), instancesCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref meshIDs, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref localAABBs, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref flags, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref lodGroupAndMasks, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.FillArray(ref lodGroupAndMasks, uint.MaxValue, instancesCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref meshLodInfos, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref gameObjectLayers, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref refCounts, newCapacity);
			instancesCapacity = newCapacity;
		}

		private void AddUnsafe(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			if (instance.index >= m_InstanceIndices.Length)
			{
				int length = m_InstanceIndices.Length;
				m_InstanceIndices.ResizeUninitialized(instance.index + 1);
				for (int i = length; i < m_InstanceIndices.Length - 1; i++)
				{
					m_InstanceIndices[i] = -1;
				}
			}
			m_InstanceIndices[instance.index] = instancesLength;
			instances[instancesLength] = instance;
			int num = instancesLength + 1;
			instancesLength = num;
		}

		public int SharedInstanceToIndex(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			return m_InstanceIndices[instance.index];
		}

		public global::UnityEngine.Rendering.SharedInstanceHandle IndexToSharedInstance(int index)
		{
			return instances[index];
		}

		public int InstanceToIndex(in global::UnityEngine.Rendering.CPUInstanceData instanceData, global::UnityEngine.Rendering.InstanceHandle instance)
		{
			int index = instanceData.InstanceToIndex(instance);
			global::UnityEngine.Rendering.SharedInstanceHandle instance2 = instanceData.sharedInstances[index];
			return SharedInstanceToIndex(instance2);
		}

		public bool IsValidInstance(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			if (instance.valid && instance.index < m_InstanceIndices.Length)
			{
				int num = m_InstanceIndices[instance.index];
				if (num >= 0 && num < instancesLength)
				{
					return instances[num].Equals(instance);
				}
				return false;
			}
			return false;
		}

		public bool IsFreeInstanceHandle(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			if (instance.valid)
			{
				if (instance.index < m_InstanceIndices.Length)
				{
					return m_InstanceIndices[instance.index] == -1;
				}
				return true;
			}
			return false;
		}

		public bool IsValidIndex(int index)
		{
			if (index >= 0 && index < instancesLength)
			{
				global::UnityEngine.Rendering.SharedInstanceHandle sharedInstanceHandle = instances[index];
				return index == m_InstanceIndices[sharedInstanceHandle.index];
			}
			return false;
		}

		public int GetFreeInstancesCount()
		{
			return instancesCapacity - instancesLength;
		}

		public void EnsureFreeInstances(int instancesCount)
		{
			int freeInstancesCount = GetFreeInstancesCount();
			int num = instancesCount - freeInstancesCount;
			if (num > 0)
			{
				Grow(instancesCapacity + num + 256);
			}
		}

		public void AddNoGrow(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			AddUnsafe(instance);
			SetDefault(instance);
		}

		public void Add(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			EnsureFreeInstances(1);
			AddNoGrow(instance);
		}

		public void Remove(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			int num = SharedInstanceToIndex(instance);
			int index = instancesLength - 1;
			instances[num] = instances[index];
			rendererGroupIDs[num] = rendererGroupIDs[index];
			materialIDArrays[num].Dispose();
			materialIDArrays[num] = materialIDArrays[index];
			materialIDArrays[index] = default(global::UnityEngine.Rendering.SmallEntityIdArray);
			meshIDs[num] = meshIDs[index];
			localAABBs[num] = localAABBs[index];
			flags[num] = flags[index];
			lodGroupAndMasks[num] = lodGroupAndMasks[index];
			meshLodInfos[num] = meshLodInfos[index];
			gameObjectLayers[num] = gameObjectLayers[index];
			refCounts[num] = refCounts[index];
			m_InstanceIndices[instances[index].index] = num;
			m_InstanceIndices[instance.index] = -1;
			instancesLength--;
		}

		public int Get_RendererGroupID(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			return rendererGroupIDs[SharedInstanceToIndex(instance)];
		}

		public int Get_MeshID(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			return meshIDs[SharedInstanceToIndex(instance)];
		}

		public unsafe ref global::UnityEngine.Rendering.AABB Get_LocalAABB(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<global::UnityEngine.Rendering.AABB>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(localAABBs), SharedInstanceToIndex(instance));
		}

		public global::UnityEngine.Rendering.CPUSharedInstanceFlags Get_Flags(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			return flags[SharedInstanceToIndex(instance)];
		}

		public uint Get_LODGroupAndMask(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			return lodGroupAndMasks[SharedInstanceToIndex(instance)];
		}

		public int Get_GameObjectLayer(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			return gameObjectLayers[SharedInstanceToIndex(instance)];
		}

		public int Get_RefCount(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			return refCounts[SharedInstanceToIndex(instance)];
		}

		public unsafe ref global::UnityEngine.Rendering.SmallEntityIdArray Get_MaterialIDs(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<global::UnityEngine.Rendering.SmallEntityIdArray>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(materialIDArrays), SharedInstanceToIndex(instance));
		}

		public void Set_RendererGroupID(global::UnityEngine.Rendering.SharedInstanceHandle instance, int rendererGroupID)
		{
			rendererGroupIDs[SharedInstanceToIndex(instance)] = rendererGroupID;
		}

		public void Set_MeshID(global::UnityEngine.Rendering.SharedInstanceHandle instance, int meshID)
		{
			meshIDs[SharedInstanceToIndex(instance)] = meshID;
		}

		public void Set_LocalAABB(global::UnityEngine.Rendering.SharedInstanceHandle instance, in global::UnityEngine.Rendering.AABB localAABB)
		{
			localAABBs[SharedInstanceToIndex(instance)] = localAABB;
		}

		public void Set_Flags(global::UnityEngine.Rendering.SharedInstanceHandle instance, global::UnityEngine.Rendering.CPUSharedInstanceFlags instanceFlags)
		{
			flags[SharedInstanceToIndex(instance)] = instanceFlags;
		}

		public void Set_LODGroupAndMask(global::UnityEngine.Rendering.SharedInstanceHandle instance, uint lodGroupAndMask)
		{
			lodGroupAndMasks[SharedInstanceToIndex(instance)] = lodGroupAndMask;
		}

		public void Set_GameObjectLayer(global::UnityEngine.Rendering.SharedInstanceHandle instance, int gameObjectLayer)
		{
			gameObjectLayers[SharedInstanceToIndex(instance)] = gameObjectLayer;
		}

		public void Set_RefCount(global::UnityEngine.Rendering.SharedInstanceHandle instance, int refCount)
		{
			refCounts[SharedInstanceToIndex(instance)] = refCount;
		}

		public void Set_MaterialIDs(global::UnityEngine.Rendering.SharedInstanceHandle instance, in global::UnityEngine.Rendering.SmallEntityIdArray materialIDs)
		{
			int index = SharedInstanceToIndex(instance);
			materialIDArrays[index].Dispose();
			materialIDArrays[index] = materialIDs;
		}

		public void Set(global::UnityEngine.Rendering.SharedInstanceHandle instance, global::UnityEngine.EntityId rendererGroupID, in global::UnityEngine.Rendering.SmallEntityIdArray materialIDs, int meshID, in global::UnityEngine.Rendering.AABB localAABB, global::UnityEngine.Rendering.TransformUpdateFlags transformUpdateFlags, global::UnityEngine.Rendering.InstanceFlags instanceFlags, uint lodGroupAndMask, global::UnityEngine.Rendering.GPUDrivenMeshLodInfo meshLodInfo, int gameObjectLayer, int refCount)
		{
			int index = SharedInstanceToIndex(instance);
			rendererGroupIDs[index] = rendererGroupID;
			materialIDArrays[index].Dispose();
			materialIDArrays[index] = materialIDs;
			meshIDs[index] = meshID;
			localAABBs[index] = localAABB;
			flags[index] = new global::UnityEngine.Rendering.CPUSharedInstanceFlags
			{
				transformUpdateFlags = transformUpdateFlags,
				instanceFlags = instanceFlags
			};
			lodGroupAndMasks[index] = lodGroupAndMask;
			meshLodInfos[index] = meshLodInfo;
			gameObjectLayers[index] = gameObjectLayer;
			refCounts[index] = refCount;
		}

		public void SetDefault(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			Set(instance, global::UnityEngine.EntityId.None, default(global::UnityEngine.Rendering.SmallEntityIdArray), 0, default(global::UnityEngine.Rendering.AABB), global::UnityEngine.Rendering.TransformUpdateFlags.None, global::UnityEngine.Rendering.InstanceFlags.None, uint.MaxValue, default(global::UnityEngine.Rendering.GPUDrivenMeshLodInfo), 0, 0);
		}

		public global::UnityEngine.Rendering.CPUSharedInstanceData.ReadOnly AsReadOnly()
		{
			return new global::UnityEngine.Rendering.CPUSharedInstanceData.ReadOnly(in this);
		}
	}
}
