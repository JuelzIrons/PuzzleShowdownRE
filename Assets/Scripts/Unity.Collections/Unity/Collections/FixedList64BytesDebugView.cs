namespace Unity.Collections
{
	internal sealed class FixedList64BytesDebugView<T> where T : unmanaged
	{
		private global::Unity.Collections.FixedList64Bytes<T> m_List;

		public T[] Items => m_List.ToArray();

		public FixedList64BytesDebugView(global::Unity.Collections.FixedList64Bytes<T> list)
		{
			m_List = list;
		}
	}
}
