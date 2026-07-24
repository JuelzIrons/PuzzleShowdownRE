namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct UnsafeStreamBlock
	{
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* Next;

		internal unsafe fixed byte Data[1];
	}
}
