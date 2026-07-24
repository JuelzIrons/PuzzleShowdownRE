namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct UnsafeStreamRange
	{
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* Block;

		internal int OffsetInFirstBlock;

		internal int ElementCount;

		internal int LastOffset;

		internal int NumberOfBlocks;
	}
}
