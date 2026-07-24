namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class NativeListExtensions
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static bool Contains<T, U>(this global::Unity.Collections.NativeList<T> list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return global::Unity.Collections.NativeArrayExtensions.IndexOf<T, U>(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafeReadOnlyPtr(list), list.Length, value) != -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<T, U>(this global::Unity.Collections.NativeList<T> list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return global::Unity.Collections.NativeArrayExtensions.IndexOf<T, U>(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafeReadOnlyPtr(list), list.Length, value);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static bool ArraysEqual<T>(this global::Unity.Collections.NativeArray<T> container, in global::Unity.Collections.NativeList<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			return container.ArraysEqual(other.AsArray());
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static bool ArraysEqual<T>(this global::Unity.Collections.NativeList<T> container, in global::Unity.Collections.NativeArray<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			return other.ArraysEqual(in container);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static bool ArraysEqual<T>(this global::Unity.Collections.NativeList<T> container, in global::Unity.Collections.NativeList<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			return container.AsArray().ArraysEqual(other.AsArray());
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static bool ArraysEqual<T>(this global::Unity.Collections.NativeList<T> container, in global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeListExtensions.ArraysEqual(*container.m_ListData, in other);
		}
	}
}
