namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct NativeParallelMultiHashMapIterator<TKey> where TKey : unmanaged
	{
		internal TKey key;

		internal int NextEntryIndex;

		internal int EntryIndex;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int GetEntryIndex()
		{
			return EntryIndex;
		}
	}
}
