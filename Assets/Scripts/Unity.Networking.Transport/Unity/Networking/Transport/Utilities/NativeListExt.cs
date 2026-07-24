namespace Unity.Networking.Transport.Utilities
{
	internal static class NativeListExt
	{
		internal static void ResizeUninitializedTillPowerOf2<T>(this global::Unity.Collections.NativeList<T> list, int sizeToFit) where T : unmanaged
		{
			int capacity = list.Capacity;
			if (sizeToFit >= capacity)
			{
				sizeToFit |= sizeToFit >> 1;
				sizeToFit |= sizeToFit >> 2;
				sizeToFit |= sizeToFit >> 4;
				sizeToFit |= sizeToFit >> 8;
				sizeToFit |= sizeToFit >> 16;
				sizeToFit++;
				list.Capacity = sizeToFit;
			}
		}
	}
}
