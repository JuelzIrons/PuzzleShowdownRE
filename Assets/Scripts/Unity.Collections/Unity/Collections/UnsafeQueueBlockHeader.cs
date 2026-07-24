namespace Unity.Collections
{
	internal struct UnsafeQueueBlockHeader
	{
		public unsafe global::Unity.Collections.UnsafeQueueBlockHeader* m_NextBlock;

		public int m_NumItems;
	}
}
