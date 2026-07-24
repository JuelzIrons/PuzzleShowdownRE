namespace UnityEngine.Rendering
{
	internal struct SmallEntityIdArray : global::System.IDisposable
	{
		private global::Unity.Collections.FixedList32Bytes<global::UnityEngine.EntityId> m_FixedArray;

		private global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.EntityId> m_List;

		private readonly bool m_IsEmbedded;

		public readonly int Length;

		public bool Valid { get; private set; }

		public global::UnityEngine.EntityId this[int index]
		{
			get
			{
				if (m_IsEmbedded)
				{
					return m_FixedArray[index];
				}
				return m_List[index];
			}
			set
			{
				if (m_IsEmbedded)
				{
					m_FixedArray[index] = value;
				}
				else
				{
					m_List[index] = value;
				}
			}
		}

		public SmallEntityIdArray(int length, global::Unity.Collections.Allocator allocator)
		{
			m_FixedArray = default(global::Unity.Collections.FixedList32Bytes<global::UnityEngine.EntityId>);
			m_List = default(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.EntityId>);
			Length = length;
			Valid = true;
			if (Length <= m_FixedArray.Capacity)
			{
				m_FixedArray = default(global::Unity.Collections.FixedList32Bytes<global::UnityEngine.EntityId>);
				m_FixedArray.Length = Length;
				m_IsEmbedded = true;
			}
			else
			{
				m_List = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::UnityEngine.EntityId>(Length, allocator);
				m_List.Resize(Length);
				m_IsEmbedded = false;
			}
		}

		public void Dispose()
		{
			if (Valid)
			{
				m_List.Dispose();
				Valid = false;
			}
		}
	}
}
