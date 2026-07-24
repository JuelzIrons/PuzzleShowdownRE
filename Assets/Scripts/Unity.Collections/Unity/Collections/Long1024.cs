namespace Unity.Collections
{
	internal struct Long1024 : global::Unity.Collections.IIndexable<long>
	{
		internal global::Unity.Collections.Long512 f0;

		internal global::Unity.Collections.Long512 f1;

		public int Length
		{
			get
			{
				return 1024;
			}
			set
			{
			}
		}

		public unsafe ref long ElementAt(int index)
		{
			fixed (global::Unity.Collections.Long512* ptr = &f0)
			{
				return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<long>((byte*)ptr + (nint)index * (nint)8);
			}
		}
	}
}
