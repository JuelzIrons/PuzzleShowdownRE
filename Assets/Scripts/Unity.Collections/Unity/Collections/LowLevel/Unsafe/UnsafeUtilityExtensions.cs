namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class UnsafeUtilityExtensions
	{
		internal unsafe static void MemSwap(void* ptr, void* otherPtr, long size)
		{
			byte* ptr2 = (byte*)ptr;
			byte* ptr3 = (byte*)otherPtr;
			byte* ptr4 = stackalloc byte[1024];
			while (size > 0)
			{
				long num = global::Unity.Mathematics.math.min(size, 1024L);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr4, ptr2, num);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr2, ptr3, num);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr3, ptr4, num);
				size -= num;
				ptr3 += num;
				ptr2 += num;
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static T ReadArrayElementBoundsChecked<T>(void* source, int index, int capacity) where T : unmanaged
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(source, index);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static void WriteArrayElementBoundsChecked<T>(void* destination, int index, T value, int capacity) where T : unmanaged
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(destination, index, value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static void* AddressOf<T>(in T value) where T : unmanaged
		{
			return global::Unity.Collections.LowLevel.Unsafe.ILSupport.AddressOf(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static ref T AsRef<T>(in T value) where T : unmanaged
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.ILSupport.AsRef(in value);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private unsafe static void CheckMemSwapOverlap(byte* dst, byte* src, long size)
		{
			if (dst + size > src && src + size > dst)
			{
				throw new global::System.InvalidOperationException("MemSwap memory blocks are overlapped.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckIndexRange(int index, int capacity)
		{
			if (index > capacity - 1 || index < 0)
			{
				throw new global::System.IndexOutOfRangeException($"Attempt to read or write from array index {index}, which is out of bounds. Array capacity is {capacity}. " + "This may lead to a crash, data corruption, or reading invalid data.");
			}
		}
	}
}
