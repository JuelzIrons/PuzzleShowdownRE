namespace UnityEngine.U2D.Animation
{
	internal static class NativeArrayHelpers
	{
		public static void ResizeIfNeeded<T>(ref global::Unity.Collections.NativeArray<T> nativeArray, int size, global::Unity.Collections.Allocator allocator = global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.ClearMemory) where T : struct
		{
			bool flag = nativeArray.IsCreated;
			if (flag && nativeArray.Length != size)
			{
				nativeArray.Dispose();
				flag = false;
			}
			if (!flag)
			{
				nativeArray = new global::Unity.Collections.NativeArray<T>(size, allocator, options);
			}
		}

		public static void ResizeAndCopyIfNeeded<T>(ref global::Unity.Collections.NativeArray<T> nativeArray, int size, global::Unity.Collections.Allocator allocator = global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.ClearMemory) where T : struct
		{
			bool isCreated = nativeArray.IsCreated;
			if (!isCreated || nativeArray.Length != size)
			{
				global::Unity.Collections.NativeArray<T> nativeArray2 = new global::Unity.Collections.NativeArray<T>(size, allocator, options);
				if (isCreated)
				{
					global::Unity.Collections.NativeArray<T>.Copy(nativeArray, nativeArray2, (size < nativeArray.Length) ? size : nativeArray.Length);
					nativeArray.Dispose();
				}
				nativeArray = nativeArray2;
			}
		}

		public static void DisposeIfCreated<T>(this global::Unity.Collections.NativeArray<T> nativeArray) where T : struct
		{
			if (nativeArray != default(global::Unity.Collections.NativeArray<T>) && nativeArray.IsCreated)
			{
				nativeArray.Dispose();
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.WriteAccessRequired]
		public unsafe static void CopyFromNativeSlice<T, S>(this global::Unity.Collections.NativeArray<T> nativeArray, int dstStartIndex, int dstEndIndex, global::Unity.Collections.NativeSlice<S> slice, int srcStartIndex, int srcEndIndex) where T : struct where S : struct
		{
			if (dstEndIndex - dstStartIndex != srcEndIndex - srcStartIndex)
			{
				throw new global::System.ArgumentException("Destination and Source copy counts must match.", "slice");
			}
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			int num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			byte* unsafeReadOnlyPtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.GetUnsafeReadOnlyPtr(slice);
			unsafeReadOnlyPtr += srcStartIndex * num2;
			byte* unsafePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
			unsafePtr += dstStartIndex * num;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpyStride(unsafePtr, num2, unsafeReadOnlyPtr, slice.Stride, num, srcEndIndex - srcStartIndex);
		}
	}
}
