namespace Unity.Collections
{
	internal sealed class FixedList4096BytesDebugView<T> where T : unmanaged
	{
		private global::Unity.Collections.FixedList4096Bytes<T> m_List;

		public T[] Items => m_List.ToArray();

		public FixedList4096BytesDebugView(global::Unity.Collections.FixedList4096Bytes<T> list)
		{
			m_List = list;
		}
	}
}
