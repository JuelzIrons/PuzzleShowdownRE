namespace Unity.Collections
{
	internal sealed class FixedList512BytesDebugView<T> where T : unmanaged
	{
		private global::Unity.Collections.FixedList512Bytes<T> m_List;

		public T[] Items => m_List.ToArray();

		public FixedList512BytesDebugView(global::Unity.Collections.FixedList512Bytes<T> list)
		{
			m_List = list;
		}
	}
}
