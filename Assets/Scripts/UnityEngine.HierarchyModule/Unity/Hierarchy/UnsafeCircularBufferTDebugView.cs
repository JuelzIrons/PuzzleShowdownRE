namespace Unity.Hierarchy
{
	internal sealed class UnsafeCircularBufferTDebugView<T> where T : class
	{
		private readonly global::Unity.Hierarchy.CircularBuffer<T> m_Buffer;

		public T[] Items => m_Buffer.ToArray();

		public UnsafeCircularBufferTDebugView(global::Unity.Hierarchy.CircularBuffer<T> buffer)
		{
			m_Buffer = buffer;
		}
	}
}
