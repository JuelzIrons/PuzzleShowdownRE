namespace Unity.Collections
{
	public static class ListExtensions
	{
		public static bool RemoveSwapBack<T>(this global::System.Collections.Generic.List<T> list, T value)
		{
			int num = list.IndexOf(value);
			if (num < 0)
			{
				return false;
			}
			list.RemoveAtSwapBack(num);
			return true;
		}

		public static bool RemoveSwapBack<T>(this global::System.Collections.Generic.List<T> list, global::System.Predicate<T> matcher)
		{
			int num = list.FindIndex(matcher);
			if (num < 0)
			{
				return false;
			}
			list.RemoveAtSwapBack(num);
			return true;
		}

		public static void RemoveAtSwapBack<T>(this global::System.Collections.Generic.List<T> list, int index)
		{
			int index2 = list.Count - 1;
			list[index] = list[index2];
			list.RemoveAt(index2);
		}

		public static global::Unity.Collections.NativeList<T> ToNativeList<T>(this global::System.Collections.Generic.List<T> list, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
		{
			global::Unity.Collections.NativeList<T> result = new global::Unity.Collections.NativeList<T>(list.Count, allocator);
			for (int i = 0; i < list.Count; i++)
			{
				result.AddNoResize(list[i]);
			}
			return result;
		}

		public static global::Unity.Collections.NativeArray<T> ToNativeArray<T>(this global::System.Collections.Generic.List<T> list, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
		{
			global::Unity.Collections.NativeArray<T> result = global::Unity.Collections.CollectionHelper.CreateNativeArray<T>(list.Count, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < list.Count; i++)
			{
				result[i] = list[i];
			}
			return result;
		}
	}
}
