namespace UnityEngine.Rendering
{
	internal struct CPUPerCameraInstanceData : global::System.IDisposable
	{
		internal struct PerCameraInstanceDataArrays : global::System.IDisposable
		{
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeList<byte> meshLods;

			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeList<byte> crossFades;

			public bool IsCreated
			{
				get
				{
					if (meshLods.IsCreated)
					{
						return crossFades.IsCreated;
					}
					return false;
				}
			}

			public PerCameraInstanceDataArrays(int initCapacity)
			{
				meshLods = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<byte>(initCapacity, global::Unity.Collections.Allocator.Persistent);
				meshLods.Length = initCapacity;
				crossFades = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<byte>(initCapacity, global::Unity.Collections.Allocator.Persistent);
				crossFades.Length = initCapacity;
			}

			public void Dispose()
			{
				meshLods.Dispose();
				crossFades.Dispose();
			}

			internal void Remove(int index, int lastIndex)
			{
				meshLods[index] = meshLods[lastIndex];
				crossFades[index] = crossFades[lastIndex];
			}

			internal void Grow(int previousCapacity, int newCapacity)
			{
				meshLods.Length = newCapacity;
				crossFades.Length = newCapacity;
			}

			internal void SetDefault(int index)
			{
				meshLods[index] = byte.MaxValue;
				crossFades[index] = byte.MaxValue;
			}
		}

		public const byte k_InvalidByteData = byte.MaxValue;

		public global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays> perCameraData;

		private global::Unity.Collections.NativeArray<int> m_StructData;

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

		public int cameraCount => perCameraData.Count();

		public void Initialize(int initCapacity)
		{
			perCameraData = new global::Unity.Collections.NativeParallelHashMap<int, global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays>(1, global::Unity.Collections.Allocator.Persistent);
			m_StructData = new global::Unity.Collections.NativeArray<int>(2, global::Unity.Collections.Allocator.Persistent);
			instancesCapacity = initCapacity;
			instancesLength = 0;
		}

		public void DeallocateCameras(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> cameraIDs)
		{
			foreach (global::UnityEngine.EntityId item2 in cameraIDs)
			{
				if (perCameraData.TryGetValue(item2, out var item))
				{
					item.Dispose();
					perCameraData.Remove(item2);
				}
			}
		}

		public void AllocateCameras(global::Unity.Collections.NativeArray<global::UnityEngine.EntityId> cameraIDs)
		{
			foreach (global::UnityEngine.EntityId item2 in cameraIDs)
			{
				if (!perCameraData.TryGetValue(item2, out var item))
				{
					item = new global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays(instancesCapacity);
					perCameraData.Add(item2, item);
				}
			}
		}

		public void Remove(int index)
		{
			int lastIndex = instancesLength - 1;
			foreach (global::Unity.Collections.LowLevel.Unsafe.KeyValue<int, global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays> perCameraDatum in perCameraData)
			{
				perCameraDatum.Value.Remove(index, lastIndex);
			}
			instancesLength--;
		}

		public void IncreaseInstanceCount()
		{
			instancesLength++;
		}

		public void Dispose()
		{
			foreach (global::Unity.Collections.LowLevel.Unsafe.KeyValue<int, global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays> perCameraDatum in perCameraData)
			{
				perCameraDatum.Value.Dispose();
			}
			m_StructData.Dispose();
			perCameraData.Dispose();
		}

		internal void Grow(int newCapacity)
		{
			if (newCapacity < instancesCapacity)
			{
				return;
			}
			int previousCapacity = instancesCapacity;
			instancesCapacity = newCapacity;
			foreach (global::Unity.Collections.LowLevel.Unsafe.KeyValue<int, global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays> perCameraDatum in perCameraData)
			{
				perCameraDatum.Value.Grow(previousCapacity, instancesCapacity);
			}
		}

		public void SetDefault(int index)
		{
			foreach (global::Unity.Collections.LowLevel.Unsafe.KeyValue<int, global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays> perCameraDatum in perCameraData)
			{
				perCameraDatum.Value.SetDefault(index);
			}
		}
	}
}
