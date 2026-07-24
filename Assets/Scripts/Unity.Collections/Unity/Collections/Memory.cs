namespace Unity.Collections
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct Memory
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility]
		internal struct Unmanaged
		{
			[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
			[global::Unity.Collections.GenerateTestsForBurstCompatibility]
			internal struct Array
			{
				private static bool IsCustom(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
				{
					return allocator.Index >= 64;
				}

				private unsafe static void* CustomResize(void* oldPointer, long oldCount, long newCount, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, long size, int align)
				{
					global::Unity.Collections.AllocatorManager.Block block = new global::Unity.Collections.AllocatorManager.Block
					{
						Range = 
						{
							Allocator = allocator,
							Items = (int)newCount,
							Pointer = (global::System.IntPtr)oldPointer
						},
						BytesPerItem = (int)size,
						Alignment = align,
						AllocatedItems = (int)oldCount
					};
					global::Unity.Collections.AllocatorManager.Try(ref block);
					return (void*)block.Range.Pointer;
				}

				internal unsafe static void* Resize(void* oldPointer, long oldCount, long newCount, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, long size, int align)
				{
					int num = global::Unity.Mathematics.math.max(64, align);
					if (IsCustom(allocator))
					{
						return CustomResize(oldPointer, oldCount, newCount, allocator, size, num);
					}
					void* ptr = default(void*);
					if (newCount > 0)
					{
						ptr = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MallocTracked(newCount * size, num, allocator.ToAllocator, 0);
						if (oldCount > 0)
						{
							long size2 = global::Unity.Mathematics.math.min(oldCount, newCount) * size;
							global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, oldPointer, size2);
						}
					}
					if (oldCount > 0)
					{
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.FreeTracked(oldPointer, allocator.ToAllocator);
					}
					return ptr;
				}

				[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
				internal unsafe static T* Resize<T>(T* oldPointer, long oldCount, long newCount, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
				{
					return (T*)Resize(oldPointer, oldCount, newCount, allocator, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<T>());
				}

				[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
				internal unsafe static T* Allocate<T>(long count, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
				{
					return Resize<T>(null, 0L, count, allocator);
				}

				[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
				internal unsafe static void Free<T>(T* pointer, long count, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
				{
					if (pointer != null)
					{
						Resize(pointer, count, 0L, allocator);
					}
				}
			}

			internal unsafe static void* Allocate(long size, int align, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				return global::Unity.Collections.Memory.Unmanaged.Array.Resize(null, 0L, 1L, allocator, size, align);
			}

			internal unsafe static void Free(void* pointer, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			{
				if (pointer != null)
				{
					global::Unity.Collections.Memory.Unmanaged.Array.Resize(pointer, 1L, 0L, allocator, 1L, 1);
				}
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			internal unsafe static T* Allocate<T>(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
			{
				return global::Unity.Collections.Memory.Unmanaged.Array.Resize<T>(null, 0L, 1L, allocator);
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			internal unsafe static void Free<T>(T* pointer, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
			{
				if (pointer != null)
				{
					global::Unity.Collections.Memory.Unmanaged.Array.Resize(pointer, 1L, 0L, allocator);
				}
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility]
		internal struct Array
		{
			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			internal unsafe static void Set<T>(T* pointer, long count, T t = default(T)) where T : unmanaged
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				for (int i = 0; i < count; i++)
				{
					pointer[i] = t;
				}
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			internal unsafe static void Clear<T>(T* pointer, long count) where T : unmanaged
			{
				long size = count * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(pointer, size);
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			internal unsafe static void Copy<T>(T* dest, T* src, long count) where T : unmanaged
			{
				long size = count * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(dest, src, size);
			}
		}

		internal const long k_MaximumRamSizeInBytes = 1099511627776L;

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckByteCountIsReasonable(long size)
		{
			if (size < 0)
			{
				throw new global::System.InvalidOperationException($"Attempted to operate on {size} bytes of memory: negative size");
			}
			if (size > 1099511627776L)
			{
				throw new global::System.InvalidOperationException($"Attempted to operate on {size} bytes of memory: size too big");
			}
		}
	}
}
