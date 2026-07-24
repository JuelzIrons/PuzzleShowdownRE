namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeKeyValueArrays<TKey, TValue> : global::Unity.Collections.INativeDisposable, global::System.IDisposable where TKey : unmanaged where TValue : unmanaged
	{
		public global::Unity.Collections.NativeArray<TKey> Keys;

		public global::Unity.Collections.NativeArray<TValue> Values;

		public int Length => Keys.Length;

		public NativeKeyValueArrays(int length, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::Unity.Collections.NativeArrayOptions options)
		{
			Keys = global::Unity.Collections.CollectionHelper.CreateNativeArray<TKey>(length, allocator, options);
			Values = global::Unity.Collections.CollectionHelper.CreateNativeArray<TValue>(length, allocator, options);
		}

		public void Dispose()
		{
			Keys.Dispose();
			Values.Dispose();
		}

		public global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			return Keys.Dispose(Values.Dispose(inputDeps));
		}
	}
}
