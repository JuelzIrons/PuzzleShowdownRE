namespace UnityEngine.Rendering
{
	public static class ArrayExtensions
	{
		public static void ResizeArray<T>(this ref global::Unity.Collections.NativeArray<T> array, int capacity) where T : struct
		{
			global::Unity.Collections.NativeArray<T> nativeArray = new global::Unity.Collections.NativeArray<T>(capacity, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			if (array.IsCreated)
			{
				global::Unity.Collections.NativeArray<T>.Copy(array, nativeArray, array.Length);
				array.Dispose();
			}
			array = nativeArray;
		}

		public static void ResizeArray(this ref global::UnityEngine.Jobs.TransformAccessArray array, int capacity)
		{
			global::UnityEngine.Jobs.TransformAccessArray transformAccessArray = new global::UnityEngine.Jobs.TransformAccessArray(capacity);
			if (array.isCreated)
			{
				for (int i = 0; i < array.length; i++)
				{
					transformAccessArray.Add(array[i]);
				}
				array.Dispose();
			}
			array = transformAccessArray;
		}

		public static void ResizeArray<T>(ref T[] array, int capacity)
		{
			if (array == null)
			{
				array = new T[capacity];
			}
			else
			{
				global::System.Array.Resize(ref array, capacity);
			}
		}

		public unsafe static void FillArray<T>(this ref global::Unity.Collections.NativeArray<T> array, in T value, int startIndex = 0, int length = -1) where T : unmanaged
		{
			T* unsafePtr = (T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(array);
			int num = ((length == -1) ? array.Length : (startIndex + length));
			for (int i = startIndex; i < num; i++)
			{
				unsafePtr[i] = value;
			}
		}
	}
}
