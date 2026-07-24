namespace UnityEngine.Rendering
{
	internal struct CPUInstanceData : global::System.IDisposable
	{
		internal readonly struct ReadOnly
		{
			public readonly global::Unity.Collections.NativeArray<int>.ReadOnly instanceIndices;

			public readonly global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>.ReadOnly instances;

			public readonly global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SharedInstanceHandle>.ReadOnly sharedInstances;

			public readonly global::UnityEngine.Rendering.ParallelBitArray localToWorldIsFlippedBits;

			public readonly global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.AABB>.ReadOnly worldAABBs;

			public readonly global::Unity.Collections.NativeArray<int>.ReadOnly tetrahedronCacheIndices;

			public readonly global::UnityEngine.Rendering.ParallelBitArray movedInCurrentFrameBits;

			public readonly global::UnityEngine.Rendering.ParallelBitArray movedInPreviousFrameBits;

			public readonly global::UnityEngine.Rendering.ParallelBitArray visibleInPreviousFrameBits;

			public readonly global::UnityEngine.Rendering.EditorInstanceDataArrays.ReadOnly editorData;

			public readonly global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenRendererMeshLodData>.ReadOnly meshLodData;

			public int handlesLength => instanceIndices.Length;

			public int instancesLength => instances.Length;

			public ReadOnly(in global::UnityEngine.Rendering.CPUInstanceData instanceData)
			{
				instanceIndices = instanceData.m_InstanceIndices.AsArray().AsReadOnly();
				instances = instanceData.instances.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				sharedInstances = instanceData.sharedInstances.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				localToWorldIsFlippedBits = instanceData.localToWorldIsFlippedBits.GetSubArray(instanceData.instancesLength);
				worldAABBs = instanceData.worldAABBs.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				tetrahedronCacheIndices = instanceData.tetrahedronCacheIndices.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
				movedInCurrentFrameBits = instanceData.movedInCurrentFrameBits.GetSubArray(instanceData.instancesLength);
				movedInPreviousFrameBits = instanceData.movedInPreviousFrameBits.GetSubArray(instanceData.instancesLength);
				visibleInPreviousFrameBits = instanceData.visibleInPreviousFrameBits.GetSubArray(instanceData.instancesLength);
				editorData = new global::UnityEngine.Rendering.EditorInstanceDataArrays.ReadOnly(in instanceData);
				meshLodData = instanceData.meshLodData.GetSubArray(0, instanceData.instancesLength).AsReadOnly();
			}

			public int InstanceToIndex(global::UnityEngine.Rendering.InstanceHandle instance)
			{
				return instanceIndices[instance.index];
			}

			public global::UnityEngine.Rendering.InstanceHandle IndexToInstance(int index)
			{
				return instances[index];
			}

			public bool IsValidInstance(global::UnityEngine.Rendering.InstanceHandle instance)
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
					global::UnityEngine.Rendering.InstanceHandle instanceHandle = instances[index];
					return index == instanceIndices[instanceHandle.index];
				}
				return false;
			}
		}

		private const int k_InvalidIndex = -1;

		private global::Unity.Collections.NativeArray<int> m_StructData;

		private global::Unity.Collections.NativeList<int> m_InstanceIndices;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle> instances;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SharedInstanceHandle> sharedInstances;

		public global::UnityEngine.Rendering.ParallelBitArray localToWorldIsFlippedBits;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.AABB> worldAABBs;

		public global::Unity.Collections.NativeArray<int> tetrahedronCacheIndices;

		public global::UnityEngine.Rendering.ParallelBitArray movedInCurrentFrameBits;

		public global::UnityEngine.Rendering.ParallelBitArray movedInPreviousFrameBits;

		public global::UnityEngine.Rendering.ParallelBitArray visibleInPreviousFrameBits;

		public global::UnityEngine.Rendering.EditorInstanceDataArrays editorData;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenRendererMeshLodData> meshLodData;

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
			instances = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.InstanceHandle>(instancesCapacity, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			instances.FillArray(in global::UnityEngine.Rendering.InstanceHandle.Invalid);
			sharedInstances = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.SharedInstanceHandle>(instancesCapacity, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			sharedInstances.FillArray(in global::UnityEngine.Rendering.SharedInstanceHandle.Invalid);
			localToWorldIsFlippedBits = new global::UnityEngine.Rendering.ParallelBitArray(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			worldAABBs = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.AABB>(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			tetrahedronCacheIndices = new global::Unity.Collections.NativeArray<int>(instancesCapacity, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::UnityEngine.Rendering.ArrayExtensions.FillArray(ref tetrahedronCacheIndices, -1);
			movedInCurrentFrameBits = new global::UnityEngine.Rendering.ParallelBitArray(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			movedInPreviousFrameBits = new global::UnityEngine.Rendering.ParallelBitArray(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			visibleInPreviousFrameBits = new global::UnityEngine.Rendering.ParallelBitArray(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
			editorData.Initialize(initCapacity);
			meshLodData = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.GPUDrivenRendererMeshLodData>(instancesCapacity, global::Unity.Collections.Allocator.Persistent);
		}

		public void Dispose()
		{
			m_StructData.Dispose();
			m_InstanceIndices.Dispose();
			instances.Dispose();
			sharedInstances.Dispose();
			localToWorldIsFlippedBits.Dispose();
			worldAABBs.Dispose();
			tetrahedronCacheIndices.Dispose();
			movedInCurrentFrameBits.Dispose();
			movedInPreviousFrameBits.Dispose();
			visibleInPreviousFrameBits.Dispose();
			editorData.Dispose();
			meshLodData.Dispose();
		}

		private void Grow(int newCapacity)
		{
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref instances, newCapacity);
			instances.FillArray(in global::UnityEngine.Rendering.InstanceHandle.Invalid, instancesCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref sharedInstances, newCapacity);
			sharedInstances.FillArray(in global::UnityEngine.Rendering.SharedInstanceHandle.Invalid, instancesCapacity);
			localToWorldIsFlippedBits.Resize(newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref worldAABBs, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref tetrahedronCacheIndices, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.FillArray(ref tetrahedronCacheIndices, -1, instancesCapacity);
			movedInCurrentFrameBits.Resize(newCapacity);
			movedInPreviousFrameBits.Resize(newCapacity);
			visibleInPreviousFrameBits.Resize(newCapacity);
			editorData.Grow(newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref meshLodData, newCapacity);
			instancesCapacity = newCapacity;
		}

		private void AddUnsafe(global::UnityEngine.Rendering.InstanceHandle instance)
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

		public int InstanceToIndex(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			return m_InstanceIndices[instance.index];
		}

		public global::UnityEngine.Rendering.InstanceHandle IndexToInstance(int index)
		{
			return instances[index];
		}

		public bool IsValidInstance(global::UnityEngine.Rendering.InstanceHandle instance)
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

		public bool IsFreeInstanceHandle(global::UnityEngine.Rendering.InstanceHandle instance)
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
				global::UnityEngine.Rendering.InstanceHandle instanceHandle = instances[index];
				return index == m_InstanceIndices[instanceHandle.index];
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

		public void AddNoGrow(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			AddUnsafe(instance);
			SetDefault(instance);
		}

		public void Add(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			EnsureFreeInstances(1);
			AddNoGrow(instance);
		}

		public void Remove(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			int num = InstanceToIndex(instance);
			int num2 = instancesLength - 1;
			instances[num] = instances[num2];
			sharedInstances[num] = sharedInstances[num2];
			localToWorldIsFlippedBits.Set(num, localToWorldIsFlippedBits.Get(num2));
			worldAABBs[num] = worldAABBs[num2];
			tetrahedronCacheIndices[num] = tetrahedronCacheIndices[num2];
			movedInCurrentFrameBits.Set(num, movedInCurrentFrameBits.Get(num2));
			movedInPreviousFrameBits.Set(num, movedInPreviousFrameBits.Get(num2));
			visibleInPreviousFrameBits.Set(num, visibleInPreviousFrameBits.Get(num2));
			editorData.Remove(num, num2);
			meshLodData[num] = meshLodData[num2];
			m_InstanceIndices[instances[num2].index] = num;
			m_InstanceIndices[instance.index] = -1;
			instancesLength--;
		}

		public void Set(global::UnityEngine.Rendering.InstanceHandle instance, global::UnityEngine.Rendering.SharedInstanceHandle sharedInstance, bool localToWorldIsFlipped, in global::UnityEngine.Rendering.AABB worldAABB, int tetrahedronCacheIndex, bool movedInCurrentFrame, bool movedInPreviousFrame, bool visibleInPreviousFrame, in global::UnityEngine.Rendering.GPUDrivenRendererMeshLodData meshLod)
		{
			int num = InstanceToIndex(instance);
			sharedInstances[num] = sharedInstance;
			localToWorldIsFlippedBits.Set(num, localToWorldIsFlipped);
			worldAABBs[num] = worldAABB;
			tetrahedronCacheIndices[num] = tetrahedronCacheIndex;
			movedInCurrentFrameBits.Set(num, movedInCurrentFrame);
			movedInPreviousFrameBits.Set(num, movedInPreviousFrame);
			visibleInPreviousFrameBits.Set(num, visibleInPreviousFrame);
			editorData.SetDefault(num);
			meshLodData[num] = meshLod;
		}

		public void SetDefault(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			Set(instance, global::UnityEngine.Rendering.SharedInstanceHandle.Invalid, localToWorldIsFlipped: false, default(global::UnityEngine.Rendering.AABB), -1, movedInCurrentFrame: false, movedInPreviousFrame: false, visibleInPreviousFrame: false, default(global::UnityEngine.Rendering.GPUDrivenRendererMeshLodData));
		}

		public global::UnityEngine.Rendering.SharedInstanceHandle Get_SharedInstance(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			return sharedInstances[InstanceToIndex(instance)];
		}

		public bool Get_LocalToWorldIsFlipped(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			return localToWorldIsFlippedBits.Get(InstanceToIndex(instance));
		}

		public global::UnityEngine.Rendering.AABB Get_WorldAABB(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			return worldAABBs[InstanceToIndex(instance)];
		}

		public int Get_TetrahedronCacheIndex(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			return tetrahedronCacheIndices[InstanceToIndex(instance)];
		}

		public unsafe ref global::UnityEngine.Rendering.AABB Get_WorldBounds(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<global::UnityEngine.Rendering.AABB>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(worldAABBs), InstanceToIndex(instance));
		}

		public bool Get_MovedInCurrentFrame(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			return movedInCurrentFrameBits.Get(InstanceToIndex(instance));
		}

		public bool Get_MovedInPreviousFrame(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			return movedInPreviousFrameBits.Get(InstanceToIndex(instance));
		}

		public bool Get_VisibleInPreviousFrame(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			return visibleInPreviousFrameBits.Get(InstanceToIndex(instance));
		}

		public global::UnityEngine.Rendering.GPUDrivenRendererMeshLodData Get_MeshLodData(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			return meshLodData[InstanceToIndex(instance)];
		}

		public void Set_SharedInstance(global::UnityEngine.Rendering.InstanceHandle instance, global::UnityEngine.Rendering.SharedInstanceHandle sharedInstance)
		{
			sharedInstances[InstanceToIndex(instance)] = sharedInstance;
		}

		public void Set_LocalToWorldIsFlipped(global::UnityEngine.Rendering.InstanceHandle instance, bool isFlipped)
		{
			localToWorldIsFlippedBits.Set(InstanceToIndex(instance), isFlipped);
		}

		public void Set_WorldAABB(global::UnityEngine.Rendering.InstanceHandle instance, in global::UnityEngine.Rendering.AABB worldBounds)
		{
			worldAABBs[InstanceToIndex(instance)] = worldBounds;
		}

		public void Set_TetrahedronCacheIndex(global::UnityEngine.Rendering.InstanceHandle instance, int tetrahedronCacheIndex)
		{
			tetrahedronCacheIndices[InstanceToIndex(instance)] = tetrahedronCacheIndex;
		}

		public void Set_MovedInCurrentFrame(global::UnityEngine.Rendering.InstanceHandle instance, bool movedInCurrentFrame)
		{
			movedInCurrentFrameBits.Set(InstanceToIndex(instance), movedInCurrentFrame);
		}

		public void Set_MovedInPreviousFrame(global::UnityEngine.Rendering.InstanceHandle instance, bool movedInPreviousFrame)
		{
			movedInPreviousFrameBits.Set(InstanceToIndex(instance), movedInPreviousFrame);
		}

		public void Set_VisibleInPreviousFrame(global::UnityEngine.Rendering.InstanceHandle instance, bool visibleInPreviousFrame)
		{
			visibleInPreviousFrameBits.Set(InstanceToIndex(instance), visibleInPreviousFrame);
		}

		public void Set_MeshLodData(global::UnityEngine.Rendering.InstanceHandle instance, global::UnityEngine.Rendering.GPUDrivenRendererMeshLodData meshLod)
		{
			meshLodData[InstanceToIndex(instance)] = meshLod;
		}

		public global::UnityEngine.Rendering.CPUInstanceData.ReadOnly AsReadOnly()
		{
			return new global::UnityEngine.Rendering.CPUInstanceData.ReadOnly(in this);
		}
	}
}
