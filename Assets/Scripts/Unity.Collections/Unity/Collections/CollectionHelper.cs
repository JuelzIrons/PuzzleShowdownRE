namespace Unity.Collections
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public static class CollectionHelper
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		internal struct LongDoubleUnion
		{
			[global::System.Runtime.InteropServices.FieldOffset(0)]
			internal long longValue;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			internal double doubleValue;
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		[global::Unity.Burst.BurstCompile]
		public struct DummyJob : global::Unity.Jobs.IJob
		{
			public void Execute()
			{
			}
		}

		public const int CacheLineSize = 64;

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckAllocator(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			if (!ShouldDeallocate(allocator))
			{
				throw new global::System.ArgumentException($"Allocator {allocator} must not be None or Invalid");
			}
		}

		public static int Log2Floor(int value)
		{
			return 31 - global::Unity.Mathematics.math.lzcnt((uint)value);
		}

		public static int Log2Ceil(int value)
		{
			return 32 - global::Unity.Mathematics.math.lzcnt((uint)(value - 1));
		}

		public static int Align(int size, int alignmentPowerOfTwo)
		{
			if (alignmentPowerOfTwo == 0)
			{
				return size;
			}
			return (size + alignmentPowerOfTwo - 1) & ~(alignmentPowerOfTwo - 1);
		}

		public static ulong Align(ulong size, ulong alignmentPowerOfTwo)
		{
			if (alignmentPowerOfTwo == 0L)
			{
				return size;
			}
			return (size + alignmentPowerOfTwo - 1) & ~(alignmentPowerOfTwo - 1);
		}

		internal unsafe static void* AlignPointer(void* ptr, int alignmentPowerOfTwo)
		{
			if (alignmentPowerOfTwo == 0)
			{
				return ptr;
			}
			nuint num = (nuint)alignmentPowerOfTwo;
			return (void*)((nuint)((byte*)ptr + num - 1) & ~(num - 1));
		}

		public unsafe static bool IsAligned(void* p, int alignmentPowerOfTwo)
		{
			return ((ulong)p & (ulong)((long)alignmentPowerOfTwo - 1L)) == 0;
		}

		public static bool IsAligned(ulong offset, int alignmentPowerOfTwo)
		{
			return (offset & (ulong)((long)alignmentPowerOfTwo - 1L)) == 0;
		}

		public static bool IsPowerOfTwo(int value)
		{
			return (value & (value - 1)) == 0;
		}

		public unsafe static uint Hash(void* ptr, int bytes)
		{
			ulong num = 5381uL;
			while (bytes > 0)
			{
				ulong num2 = ((byte*)ptr)[--bytes];
				num = (num << 5) + num + num2;
			}
			return (uint)num;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Used only for debugging, and uses managed strings")]
		internal static void WriteLayout(global::System.Type type)
		{
			global::System.Console.WriteLine($"   Offset | Bytes  | Name     Layout: {0}", type.Name);
			global::System.Reflection.FieldInfo[] fields = type.GetFields(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
			foreach (global::System.Reflection.FieldInfo fieldInfo in fields)
			{
				global::System.Console.WriteLine("   {0, 6} | {1, 6} | {2}", global::System.Runtime.InteropServices.Marshal.OffsetOf(type, fieldInfo.Name), global::System.Runtime.InteropServices.Marshal.SizeOf(fieldInfo.FieldType), fieldInfo.Name);
			}
		}

		internal static bool ShouldDeallocate(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return allocator.ToAllocator > global::Unity.Collections.Allocator.None;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[return: global::Unity.Burst.CompilerServices.AssumeRange(0L, 2147483647L)]
		internal static int AssumePositive(int value)
		{
			return value;
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "ENABLE_UNITY_COLLECTIONS_CHECKS", GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.NativeArray<int>) })]
		internal static void CheckIsUnmanaged<T>()
		{
			if (!global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.IsUnmanaged<T>())
			{
				throw new global::System.ArgumentException($"{typeof(T)} used in native collection is not blittable or not primitive");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckIntPositivePowerOfTwo(int value)
		{
			if (value <= 0 || (value & (value - 1)) != 0)
			{
				throw new global::System.ArgumentException($"Alignment requested: {value} is not a non-zero, positive power of two.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckUlongPositivePowerOfTwo(ulong value)
		{
			if (value == 0 || (value & (value - 1)) != 0)
			{
				throw new global::System.ArgumentException($"Alignment requested: {value} is not a non-zero, positive power of two.");
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckIndexInRange(int index, int length)
		{
			if ((uint)index >= (uint)length)
			{
				throw new global::System.IndexOutOfRangeException($"Index {index} is out of range in container of '{length}' Length.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckCapacityInRange(int capacity, int length)
		{
			if (capacity < 0)
			{
				throw new global::System.ArgumentOutOfRangeException($"Capacity {capacity} must be positive.");
			}
			if (capacity < length)
			{
				throw new global::System.ArgumentOutOfRangeException($"Capacity {capacity} is out of range in container of '{length}' Length.");
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle)
		})]
		public static global::Unity.Collections.NativeArray<T> CreateNativeArray<T, U>(int length, ref U allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.ClearMemory) where T : unmanaged where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			global::Unity.Collections.NativeArray<T> array;
			if (!allocator.IsCustomAllocator)
			{
				array = new global::Unity.Collections.NativeArray<T>(length, allocator.ToAllocator, options);
			}
			else
			{
				array = default(global::Unity.Collections.NativeArray<T>);
				global::Unity.Collections.NativeArrayExtensions.Initialize(ref array, length, ref allocator, options);
			}
			return array;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static global::Unity.Collections.NativeArray<T> CreateNativeArray<T>(int length, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.ClearMemory) where T : unmanaged
		{
			global::Unity.Collections.NativeArray<T> array;
			if (!global::Unity.Collections.AllocatorManager.IsCustomAllocator(allocator))
			{
				array = new global::Unity.Collections.NativeArray<T>(length, allocator.ToAllocator, options);
			}
			else
			{
				array = default(global::Unity.Collections.NativeArray<T>);
				global::Unity.Collections.NativeArrayExtensions.Initialize(ref array, length, allocator, options);
			}
			return array;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static global::Unity.Collections.NativeArray<T> CreateNativeArray<T>(global::Unity.Collections.NativeArray<T> array, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
		{
			global::Unity.Collections.NativeArray<T> array2;
			if (!global::Unity.Collections.AllocatorManager.IsCustomAllocator(allocator))
			{
				array2 = new global::Unity.Collections.NativeArray<T>(array, allocator.ToAllocator);
			}
			else
			{
				array2 = default(global::Unity.Collections.NativeArray<T>);
				global::Unity.Collections.NativeArrayExtensions.Initialize(ref array2, array.Length, allocator);
				array2.CopyFrom(array);
			}
			return array2;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Managed array")]
		public static global::Unity.Collections.NativeArray<T> CreateNativeArray<T>(T[] array, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
		{
			global::Unity.Collections.NativeArray<T> array2;
			if (!global::Unity.Collections.AllocatorManager.IsCustomAllocator(allocator))
			{
				array2 = new global::Unity.Collections.NativeArray<T>(array, allocator.ToAllocator);
			}
			else
			{
				array2 = default(global::Unity.Collections.NativeArray<T>);
				global::Unity.Collections.NativeArrayExtensions.Initialize(ref array2, array.Length, allocator);
				array2.CopyFrom(array);
			}
			return array2;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Managed array")]
		public static global::Unity.Collections.NativeArray<T> CreateNativeArray<T, U>(T[] array, ref U allocator) where T : unmanaged where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			global::Unity.Collections.NativeArray<T> array2;
			if (!allocator.IsCustomAllocator)
			{
				array2 = new global::Unity.Collections.NativeArray<T>(array, allocator.ToAllocator);
			}
			else
			{
				array2 = default(global::Unity.Collections.NativeArray<T>);
				global::Unity.Collections.NativeArrayExtensions.Initialize(ref array2, array.Length, ref allocator);
				array2.CopyFrom(array);
			}
			return array2;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static void DisposeNativeArray<T>(global::Unity.Collections.NativeArray<T> nativeArray, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
		{
			global::Unity.Collections.NativeArrayExtensions.DisposeCheckAllocator(ref nativeArray);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public static void Dispose<T>(global::Unity.Collections.NativeArray<T> nativeArray) where T : unmanaged
		{
			global::Unity.Collections.NativeArrayExtensions.DisposeCheckAllocator(ref nativeArray);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckConvertArguments<T>(int length) where T : unmanaged
		{
			if (length < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("length", "Length must be >= 0");
			}
			if (!global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.IsUnmanaged<T>())
			{
				throw new global::System.InvalidOperationException($"{typeof(T)} used in NativeArray<{typeof(T)}> must be unmanaged (contain no managed types).");
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static global::Unity.Collections.NativeArray<T> ConvertExistingDataToNativeArray<T>(void* dataPointer, int length, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, bool setTempMemoryHandle = false) where T : unmanaged
		{
			global::Unity.Collections.NativeArray<T> result = new global::Unity.Collections.NativeArray<T>
			{
				m_Buffer = dataPointer,
				m_Length = length
			};
			if (!allocator.IsCustomAllocator)
			{
				result.m_AllocatorLabel = allocator.ToAllocator;
			}
			else
			{
				result.m_AllocatorLabel = global::Unity.Collections.Allocator.None;
			}
			return result;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static global::Unity.Collections.NativeArray<T> ConvertExistingNativeListToNativeArray<T>(ref global::Unity.Collections.NativeList<T> nativeList, int length, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
		{
			return ConvertExistingDataToNativeArray<T>(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(nativeList), length, allocator);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int),
			typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle)
		})]
		public static global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue> CreateNativeParallelMultiHashMap<TKey, TValue, U>(int length, ref U allocator) where TKey : unmanaged, global::System.IEquatable<TKey> where TValue : unmanaged where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue> result = default(global::Unity.Collections.NativeParallelMultiHashMap<TKey, TValue>);
			result.Initialize(length, ref allocator);
			return result;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(RequiredUnityDefine = "ENABLE_UNITY_COLLECTIONS_CHECKS", GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.CollectionHelper.DummyJob) })]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		public static void CheckReflectionDataCorrect<T>(global::System.IntPtr reflectionData)
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		[global::Unity.Burst.BurstDiscard]
		private static void CheckReflectionDataCorrectInternal<T>(global::System.IntPtr reflectionData, ref bool burstCompiled)
		{
			if (reflectionData == global::System.IntPtr.Zero)
			{
				throw new global::System.InvalidOperationException($"Reflection data was not set up by an Initialize() call. For generic job types, please include [assembly: RegisterGenericJobType(typeof({typeof(T)}))] in your source file.");
			}
			burstCompiled = false;
		}
	}
}
