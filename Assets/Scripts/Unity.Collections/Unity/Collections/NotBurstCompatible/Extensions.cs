namespace Unity.Collections.NotBurstCompatible
{
	public static class Extensions
	{
		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed array")]
		public static T[] ToArray<T>(this global::Unity.Collections.NativeHashSet<T> set) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.NativeArray<T> nativeArray = set.ToNativeArray(global::Unity.Collections.Allocator.TempJob);
			T[] result = nativeArray.ToArray();
			nativeArray.Dispose();
			return result;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed array")]
		public static T[] ToArray<T>(this global::Unity.Collections.NativeParallelHashSet<T> set) where T : unmanaged, global::System.IEquatable<T>
		{
			global::Unity.Collections.NativeArray<T> nativeArray = set.ToNativeArray(global::Unity.Collections.Allocator.TempJob);
			T[] result = nativeArray.ToArray();
			nativeArray.Dispose();
			return result;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed array")]
		public static T[] ToArrayNBC<T>(this global::Unity.Collections.NativeList<T> list) where T : unmanaged
		{
			return list.AsArray().ToArray();
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Takes managed array")]
		public static void CopyFromNBC<T>(this global::Unity.Collections.NativeList<T> list, T[] array) where T : unmanaged
		{
			list.Clear();
			list.Resize(array.Length, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			list.AsArray().CopyFrom(array);
		}
	}
}
