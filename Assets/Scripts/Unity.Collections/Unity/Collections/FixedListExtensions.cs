namespace Unity.Collections
{
	public static class FixedListExtensions
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static void Sort<T>(this ref global::Unity.Collections.FixedList32Bytes<T> list) where T : unmanaged, global::System.IComparable<T>
		{
			global::Unity.Collections.NativeSortExtension.Sort((T*)(list.buffer + global::Unity.Collections.FixedList.PaddingBytes<T>()), list.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<T, U>(this ref global::Unity.Collections.FixedList32Bytes<T> list, U comp) where T : unmanaged, global::System.IComparable<T> where U : global::System.Collections.Generic.IComparer<T>
		{
			global::Unity.Collections.NativeSortExtension.Sort((T*)(list.buffer + global::Unity.Collections.FixedList.PaddingBytes<T>()), list.Length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static void Sort<T>(this ref global::Unity.Collections.FixedList64Bytes<T> list) where T : unmanaged, global::System.IComparable<T>
		{
			global::Unity.Collections.NativeSortExtension.Sort((T*)(list.buffer + global::Unity.Collections.FixedList.PaddingBytes<T>()), list.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<T, U>(this ref global::Unity.Collections.FixedList64Bytes<T> list, U comp) where T : unmanaged, global::System.IComparable<T> where U : global::System.Collections.Generic.IComparer<T>
		{
			global::Unity.Collections.NativeSortExtension.Sort((T*)(list.buffer + global::Unity.Collections.FixedList.PaddingBytes<T>()), list.Length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static void Sort<T>(this ref global::Unity.Collections.FixedList128Bytes<T> list) where T : unmanaged, global::System.IComparable<T>
		{
			global::Unity.Collections.NativeSortExtension.Sort((T*)(list.buffer + global::Unity.Collections.FixedList.PaddingBytes<T>()), list.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<T, U>(this ref global::Unity.Collections.FixedList128Bytes<T> list, U comp) where T : unmanaged, global::System.IComparable<T> where U : global::System.Collections.Generic.IComparer<T>
		{
			global::Unity.Collections.NativeSortExtension.Sort((T*)(list.buffer + global::Unity.Collections.FixedList.PaddingBytes<T>()), list.Length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static void Sort<T>(this ref global::Unity.Collections.FixedList512Bytes<T> list) where T : unmanaged, global::System.IComparable<T>
		{
			global::Unity.Collections.NativeSortExtension.Sort((T*)(list.buffer + global::Unity.Collections.FixedList.PaddingBytes<T>()), list.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<T, U>(this ref global::Unity.Collections.FixedList512Bytes<T> list, U comp) where T : unmanaged, global::System.IComparable<T> where U : global::System.Collections.Generic.IComparer<T>
		{
			global::Unity.Collections.NativeSortExtension.Sort((T*)(list.buffer + global::Unity.Collections.FixedList.PaddingBytes<T>()), list.Length, comp);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static void Sort<T>(this ref global::Unity.Collections.FixedList4096Bytes<T> list) where T : unmanaged, global::System.IComparable<T>
		{
			global::Unity.Collections.NativeSortExtension.Sort((T*)(list.buffer + global::Unity.Collections.FixedList.PaddingBytes<T>()), list.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<T, U>(this ref global::Unity.Collections.FixedList4096Bytes<T> list, U comp) where T : unmanaged, global::System.IComparable<T> where U : global::System.Collections.Generic.IComparer<T>
		{
			global::Unity.Collections.NativeSortExtension.Sort((T*)(list.buffer + global::Unity.Collections.FixedList.PaddingBytes<T>()), list.Length, comp);
		}
	}
}
