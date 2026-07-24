namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class UnsafeListExtensions
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<T, U>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return global::Unity.Collections.NativeArrayExtensions.IndexOf<T, U>(list.Ptr, list.Length, value);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<T, U>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return list.IndexOf(value) != -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<T, U>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.ReadOnly list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return global::Unity.Collections.NativeArrayExtensions.IndexOf<T, U>(list.Ptr, list.Length, value);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<T, U>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.ReadOnly list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return list.IndexOf(value) != -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<T, U>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.ParallelReader list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return global::Unity.Collections.NativeArrayExtensions.IndexOf<T, U>(list.Ptr, list.Length, value);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<T, U>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.ParallelReader list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return list.IndexOf(value) != -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static bool ArraysEqual<T>(this global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> container, in global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			if (container.Length != other.Length)
			{
				return false;
			}
			for (int i = 0; i != container.Length; i++)
			{
				if (!container[i].Equals(other[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
