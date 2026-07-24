namespace UnityEngine.Rendering
{
	internal struct SilhouettePlaneCache : global::System.IDisposable
	{
		private struct Slot
		{
			public bool isActive;

			public int viewInstanceID;

			public int planeCount;

			public int lastUsedFrameIndex;

			public Slot(int viewInstanceID, int planeCount, int frameIndex)
			{
				isActive = true;
				this.viewInstanceID = viewInstanceID;
				this.planeCount = planeCount;
				lastUsedFrameIndex = frameIndex;
			}
		}

		private const int kMaxSilhouettePlanes = 6;

		private global::Unity.Collections.NativeParallelHashMap<int, int> m_SubviewIDToIndexMap;

		private global::Unity.Collections.NativeList<int> m_SlotFreeList;

		private global::Unity.Collections.NativeList<global::UnityEngine.Rendering.SilhouettePlaneCache.Slot> m_Slots;

		private global::Unity.Collections.NativeList<global::UnityEngine.Plane> m_PlaneStorage;

		public void Init()
		{
			m_SubviewIDToIndexMap = new global::Unity.Collections.NativeParallelHashMap<int, int>(16, global::Unity.Collections.Allocator.Persistent);
			m_SlotFreeList = new global::Unity.Collections.NativeList<int>(16, global::Unity.Collections.Allocator.Persistent);
			m_Slots = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.SilhouettePlaneCache.Slot>(16, global::Unity.Collections.Allocator.Persistent);
			m_PlaneStorage = new global::Unity.Collections.NativeList<global::UnityEngine.Plane>(96, global::Unity.Collections.Allocator.Persistent);
		}

		public void Dispose()
		{
			m_SubviewIDToIndexMap.Dispose();
			m_SlotFreeList.Dispose();
			m_Slots.Dispose();
			m_PlaneStorage.Dispose();
		}

		public void Update(int viewInstanceID, global::Unity.Collections.NativeArray<global::UnityEngine.Plane> planes, int frameIndex)
		{
			int num = global::System.Math.Min(planes.Length, 6);
			if (!m_SubviewIDToIndexMap.TryGetValue(viewInstanceID, out var item))
			{
				if (m_SlotFreeList.Length > 0)
				{
					item = m_SlotFreeList[m_SlotFreeList.Length - 1];
					m_SlotFreeList.Length -= 1;
				}
				else
				{
					if (m_Slots.Length == m_Slots.Capacity)
					{
						int num2 = m_Slots.Length + 8;
						m_Slots.SetCapacity(num2);
						m_PlaneStorage.SetCapacity(num2 * 6);
					}
					item = m_Slots.Length;
					int num3 = item + 1;
					m_Slots.ResizeUninitialized(num3);
					m_PlaneStorage.ResizeUninitialized(num3 * 6);
				}
				m_SubviewIDToIndexMap.Add(viewInstanceID, item);
			}
			m_Slots[item] = new global::UnityEngine.Rendering.SilhouettePlaneCache.Slot(viewInstanceID, num, frameIndex);
			m_PlaneStorage.AsArray().GetSubArray(item * 6, num).CopyFrom(planes);
		}

		public void FreeUnusedSlots(int frameIndex, int maximumAge)
		{
			for (int i = 0; i < m_Slots.Length; i++)
			{
				global::UnityEngine.Rendering.SilhouettePlaneCache.Slot value = m_Slots[i];
				if (value.isActive && frameIndex - value.lastUsedFrameIndex > maximumAge)
				{
					value.isActive = false;
					m_Slots[i] = value;
					m_SubviewIDToIndexMap.Remove(value.viewInstanceID);
					m_SlotFreeList.Add(in i);
				}
			}
		}

		public global::Unity.Collections.NativeArray<global::UnityEngine.Plane> GetSubArray(int viewInstanceID)
		{
			int start = 0;
			int length = 0;
			if (m_SubviewIDToIndexMap.TryGetValue(viewInstanceID, out var item))
			{
				start = item * 6;
				length = m_Slots[item].planeCount;
			}
			return m_PlaneStorage.AsArray().GetSubArray(start, length);
		}
	}
}
