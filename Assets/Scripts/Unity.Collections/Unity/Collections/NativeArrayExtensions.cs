namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class NativeArrayExtensions
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public struct NativeArrayStaticId<T> where T : unmanaged
		{
			internal static readonly global::Unity.Burst.SharedStatic<int> s_staticSafetyId = global::Unity.Burst.SharedStatic<int>.GetOrCreate<global::Unity.Collections.NativeArray<T>>();
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static bool Contains<T, U>(this global::Unity.Collections.NativeArray<T> array, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return IndexOf<T, U>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), array.Length, value) != -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<T, U>(this global::Unity.Collections.NativeArray<T> array, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return IndexOf<T, U>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), array.Length, value);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static bool Contains<T, U>(this global::Unity.Collections.NativeArray<T>.ReadOnly array, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return IndexOf<T, U>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), array.m_Length, value) != -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<T, U>(this global::Unity.Collections.NativeArray<T>.ReadOnly array, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return IndexOf<T, U>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), array.m_Length, value);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static bool Contains<T, U>(void* ptr, int length, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			return IndexOf<T, U>(ptr, length, value) != -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<T, U>(void* ptr, int length, U value) where T : unmanaged, global::System.IEquatable<U>
		{
			for (int i = 0; i != length; i++)
			{
				if (global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(ptr, i).Equals(value))
				{
					return i;
				}
			}
			return -1;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static void CopyFrom<T>(this ref global::Unity.Collections.NativeArray<T> container, global::Unity.Collections.NativeList<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			container.CopyFrom(other.AsArray());
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static void CopyFrom<T>(this ref global::Unity.Collections.NativeArray<T> container, in global::Unity.Collections.NativeHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			using global::Unity.Collections.NativeArray<T> array = other.ToNativeArray(global::Unity.Collections.Allocator.TempJob);
			container.CopyFrom(array);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static void CopyFrom<T>(this ref global::Unity.Collections.NativeArray<T> container, in global::Unity.Collections.LowLevel.Unsafe.UnsafeHashSet<T> other) where T : unmanaged, global::System.IEquatable<T>
		{
			using global::Unity.Collections.NativeArray<T> array = other.ToNativeArray(global::Unity.Collections.Allocator.TempJob);
			container.CopyFrom(array);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static global::Unity.Collections.NativeArray<U> Reinterpret<T, U>(this global::Unity.Collections.NativeArray<T> array) where T : unmanaged where U : unmanaged
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			int num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<U>();
			long num3 = (long)array.Length * (long)num / num2;
			return global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<U>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(array), (int)num3, global::Unity.Collections.Allocator.None);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static bool ArraysEqual<T>(this global::Unity.Collections.NativeArray<T> container, global::Unity.Collections.NativeArray<T> other) where T : unmanaged, global::System.IEquatable<T>
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

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckReinterpretSize<T, U>(ref global::Unity.Collections.NativeArray<T> array) where T : unmanaged where U : unmanaged
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			int num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<U>();
			long num3 = (long)array.Length * (long)num;
			if (num3 / num2 * num2 != num3)
			{
				throw new global::System.InvalidOperationException($"Types {typeof(T)} (array length {array.Length}) and {typeof(U)} cannot be aliased due to size constraints. The size of the types and lengths involved must line up.");
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		internal unsafe static void Initialize<T>(this ref global::Unity.Collections.NativeArray<T> array, int length, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.ClearMemory) where T : unmanaged
		{
			global::Unity.Collections.AllocatorManager.AllocatorHandle t = allocator;
			array = default(global::Unity.Collections.NativeArray<T>);
			array.m_Buffer = global::Unity.Collections.AllocatorManager.AllocateStruct(ref t, default(T), length);
			array.m_Length = length;
			array.m_AllocatorLabel = (allocator.IsAutoDispose ? global::Unity.Collections.Allocator.None : allocator.ToAllocator);
			if (options == global::Unity.Collections.NativeArrayOptions.ClearMemory)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(array.m_Buffer, array.m_Length * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>());
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle)
		})]
		internal unsafe static void Initialize<T, U>(this ref global::Unity.Collections.NativeArray<T> array, int length, ref U allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.ClearMemory) where T : unmanaged where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			array = default(global::Unity.Collections.NativeArray<T>);
			array.m_Buffer = global::Unity.Collections.AllocatorManager.AllocateStruct(ref allocator, default(T), length);
			array.m_Length = length;
			array.m_AllocatorLabel = (allocator.IsAutoDispose ? global::Unity.Collections.Allocator.None : allocator.ToAllocator);
			if (options == global::Unity.Collections.NativeArrayOptions.ClearMemory)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(array.m_Buffer, array.m_Length * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>());
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		internal unsafe static void DisposeCheckAllocator<T>(this ref global::Unity.Collections.NativeArray<T> array) where T : unmanaged
		{
			if (array.m_Buffer == null)
			{
				throw new global::System.ObjectDisposedException("The NativeArray is already disposed.");
			}
			if (!global::Unity.Collections.AllocatorManager.IsCustomAllocator(array.m_AllocatorLabel))
			{
				array.Dispose();
				return;
			}
			global::Unity.Collections.AllocatorManager.Free(array.m_AllocatorLabel, array.m_Buffer);
			array.m_AllocatorLabel = global::Unity.Collections.Allocator.Invalid;
			array.m_Buffer = null;
		}
	}
}
