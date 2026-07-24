namespace Unity.Networking.Transport.Utilities
{
	internal struct ManagedReference<T> : global::System.IDisposable
	{
		private class ElementSlot
		{
			public T Element;
		}

		private static global::System.Collections.Generic.List<global::Unity.Networking.Transport.Utilities.ManagedReference<T>.ElementSlot> s_ElementList = new global::System.Collections.Generic.List<global::Unity.Networking.Transport.Utilities.ManagedReference<T>.ElementSlot>();

		private global::Unity.Collections.NativeReference<int> m_ElementIndex;

		public ref T Element => ref s_ElementList[m_ElementIndex.Value].Element;

		private static int AllocateElement(ref T element)
		{
			int count = s_ElementList.Count;
			global::Unity.Networking.Transport.Utilities.ManagedReference<T>.ElementSlot elementSlot = new global::Unity.Networking.Transport.Utilities.ManagedReference<T>.ElementSlot
			{
				Element = element
			};
			int num = s_ElementList.FindIndex(0, count, (global::Unity.Networking.Transport.Utilities.ManagedReference<T>.ElementSlot e) => e == null);
			if (num >= 0)
			{
				s_ElementList[num] = elementSlot;
				return num;
			}
			s_ElementList.Add(elementSlot);
			return count;
		}

		private static void DeallocateElement(int index)
		{
			s_ElementList[index] = null;
		}

		public ManagedReference(ref T element)
		{
			m_ElementIndex = new global::Unity.Collections.NativeReference<int>(AllocateElement(ref element), global::Unity.Collections.Allocator.Persistent);
		}

		public void Dispose()
		{
			if (m_ElementIndex.IsCreated)
			{
				DeallocateElement(m_ElementIndex.Value);
				m_ElementIndex.Dispose();
			}
		}
	}
}
