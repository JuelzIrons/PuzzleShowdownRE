namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class FixedList512BytesExtensions
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<T, U>(this ref global::Unity.Collections.FixedList512Bytes<T> list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return global::Unity.Collections.NativeArrayExtensions.IndexOf<T, U>(list.Buffer, list.Length, value);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<T, U>(this ref global::Unity.Collections.FixedList512Bytes<T> list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return IndexOf(ref list, value) != -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Remove<T, U>(this ref global::Unity.Collections.FixedList512Bytes<T> list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			int num = IndexOf(ref list, value);
			if (num < 0)
			{
				return false;
			}
			list.RemoveAt(num);
			return true;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool RemoveSwapBack<T, U>(this ref global::Unity.Collections.FixedList512Bytes<T> list, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			int num = IndexOf(ref list, value);
			if (num == -1)
			{
				return false;
			}
			list.RemoveAtSwapBack(num);
			return true;
		}
	}
}
