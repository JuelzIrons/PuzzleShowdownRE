namespace UnityEngine.Rendering
{
	internal struct InstanceAllocator
	{
		private global::Unity.Collections.NativeArray<int> m_StructData;

		private global::Unity.Collections.NativeList<int> m_FreeInstances;

		private int m_BaseInstanceOffset;

		private int m_InstanceStride;

		public int length
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

		public bool valid => m_StructData.IsCreated;

		public void Initialize(int baseInstanceOffset = 0, int instanceStride = 1)
		{
			m_StructData = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.Persistent);
			m_FreeInstances = new global::Unity.Collections.NativeList<int>(global::Unity.Collections.Allocator.Persistent);
			m_BaseInstanceOffset = baseInstanceOffset;
			m_InstanceStride = instanceStride;
		}

		public void Dispose()
		{
			m_StructData.Dispose();
			m_FreeInstances.Dispose();
		}

		public int AllocateInstance()
		{
			int result;
			if (m_FreeInstances.Length > 0)
			{
				result = m_FreeInstances[m_FreeInstances.Length - 1];
				m_FreeInstances.RemoveAtSwapBack(m_FreeInstances.Length - 1);
			}
			else
			{
				result = length * m_InstanceStride + m_BaseInstanceOffset;
				length++;
			}
			return result;
		}

		public void FreeInstance(int instance)
		{
			m_FreeInstances.Add(in instance);
		}

		public int GetNumAllocated()
		{
			return length - m_FreeInstances.Length;
		}
	}
}
