namespace Unity.Networking.Transport.Utilities
{
	internal struct NativeMultiQueue<T> : global::System.IDisposable where T : unmanaged
	{
		private global::Unity.Collections.NativeList<T> m_Queue;

		private global::Unity.Collections.NativeList<int> m_QueueHeadTail;

		private global::Unity.Collections.NativeArray<int> m_MaxItems;

		public bool IsCreated => m_Queue.IsCreated;

		public NativeMultiQueue(int initialMessageCapacity)
		{
			m_MaxItems = new global::Unity.Collections.NativeArray<int>(1, global::Unity.Collections.Allocator.Persistent);
			m_MaxItems[0] = initialMessageCapacity;
			m_Queue = new global::Unity.Collections.NativeList<T>(initialMessageCapacity, global::Unity.Collections.Allocator.Persistent);
			m_QueueHeadTail = new global::Unity.Collections.NativeList<int>(2, global::Unity.Collections.Allocator.Persistent);
		}

		public void Dispose()
		{
			m_MaxItems.Dispose();
			m_Queue.Dispose();
			m_QueueHeadTail.Dispose();
		}

		public void Enqueue(int bucket, T value)
		{
			if (bucket >= m_QueueHeadTail.Length / 2)
			{
				int i = m_QueueHeadTail.Length;
				m_QueueHeadTail.ResizeUninitialized((bucket + 1) * 2);
				for (; i < m_QueueHeadTail.Length; i++)
				{
					m_QueueHeadTail[i] = 0;
				}
				m_Queue.ResizeUninitialized(m_QueueHeadTail.Length / 2 * m_MaxItems[0]);
			}
			int num = m_QueueHeadTail[bucket * 2 + 1];
			if (num >= m_MaxItems[0])
			{
				int num2 = m_MaxItems[0];
				while (num >= m_MaxItems[0])
				{
					m_MaxItems[0] = m_MaxItems[0] * 2;
				}
				int num3 = m_QueueHeadTail.Length / 2;
				m_Queue.ResizeUninitialized(num3 * m_MaxItems[0]);
				for (int num4 = num3 - 1; num4 >= 0; num4--)
				{
					for (int num5 = m_QueueHeadTail[num4 * 2 + 1] - 1; num5 >= m_QueueHeadTail[num4 * 2]; num5--)
					{
						m_Queue[num4 * m_MaxItems[0] + num5] = m_Queue[num4 * num2 + num5];
					}
				}
			}
			m_Queue[m_MaxItems[0] * bucket + num] = value;
			m_QueueHeadTail[bucket * 2 + 1] = num + 1;
		}

		public bool Dequeue(int bucket, out T value)
		{
			if (bucket < 0 || bucket >= m_QueueHeadTail.Length / 2)
			{
				value = default(T);
				return false;
			}
			int num = m_QueueHeadTail[bucket * 2];
			if (num >= m_QueueHeadTail[bucket * 2 + 1])
			{
				ref global::Unity.Collections.NativeList<int> queueHeadTail = ref m_QueueHeadTail;
				int index = bucket * 2;
				int value2 = (m_QueueHeadTail[bucket * 2 + 1] = 0);
				queueHeadTail[index] = value2;
				value = default(T);
				return false;
			}
			if (num + 1 == m_QueueHeadTail[bucket * 2 + 1])
			{
				ref global::Unity.Collections.NativeList<int> queueHeadTail2 = ref m_QueueHeadTail;
				int index2 = bucket * 2;
				int value2 = (m_QueueHeadTail[bucket * 2 + 1] = 0);
				queueHeadTail2[index2] = value2;
			}
			else
			{
				m_QueueHeadTail[bucket * 2] = num + 1;
			}
			value = m_Queue[m_MaxItems[0] * bucket + num];
			return true;
		}

		public bool Peek(int bucket, out T value)
		{
			if (bucket < 0 || bucket >= m_QueueHeadTail.Length / 2)
			{
				value = default(T);
				return false;
			}
			int num = m_QueueHeadTail[bucket * 2];
			if (num >= m_QueueHeadTail[bucket * 2 + 1])
			{
				value = default(T);
				return false;
			}
			value = m_Queue[m_MaxItems[0] * bucket + num];
			return true;
		}

		public void Clear(int bucket)
		{
			if (bucket >= 0 && bucket < m_QueueHeadTail.Length / 2)
			{
				m_QueueHeadTail[bucket * 2] = 0;
				m_QueueHeadTail[bucket * 2 + 1] = 0;
			}
		}
	}
}
