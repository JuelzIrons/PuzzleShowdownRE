namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public struct UnsafeAppendBuffer : global::Unity.Collections.INativeDisposable, global::System.IDisposable
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility]
		public struct Reader
		{
			public unsafe readonly byte* Ptr;

			public readonly int Size;

			public int Offset;

			public bool EndOfBuffer => Offset == Size;

			public unsafe Reader(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer buffer)
			{
				Ptr = buffer.Ptr;
				Size = buffer.Length;
				Offset = 0;
			}

			public unsafe Reader(void* ptr, int length)
			{
				Ptr = (byte*)ptr;
				Size = length;
				Offset = 0;
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe void ReadNext<T>(out T value) where T : unmanaged
			{
				int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				void* ptr = Ptr + Offset;
				if (global::Unity.Collections.CollectionHelper.IsAligned((ulong)ptr, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<T>()))
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyPtrToStructure<T>(ptr, out value);
				}
				else
				{
					fixed (T* ptr2 = &value)
					{
						void* destination = ptr2;
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, ptr, num);
					}
				}
				Offset += num;
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe T ReadNext<T>() where T : unmanaged
			{
				int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				void* ptr = Ptr + Offset;
				T result = default(T);
				if (global::Unity.Collections.CollectionHelper.IsAligned((ulong)ptr, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<T>()))
				{
					result = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(ptr, 0);
				}
				else
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(&result, ptr, num);
				}
				Offset += num;
				return result;
			}

			public unsafe void* ReadNext(int structSize)
			{
				void* result = (void*)((global::System.IntPtr)Ptr + Offset);
				Offset += structSize;
				return result;
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe void ReadNext<T>(out global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
			{
				int num = ReadNext<int>();
				value = global::Unity.Collections.CollectionHelper.CreateNativeArray<T>(num, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				int num2 = num * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				if (num2 > 0)
				{
					void* source = ReadNext(num2);
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(value.GetUnsafePtr(), source, num2);
				}
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe void* ReadNextArray<T>(out int length) where T : unmanaged
			{
				length = ReadNext<int>();
				if (length != 0)
				{
					return ReadNext(length * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>());
				}
				return null;
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private void CheckBounds(int structSize)
			{
				if (Offset + structSize > Size)
				{
					throw new global::System.ArgumentException($"Requested value outside bounds of UnsafeAppendOnlyBuffer. Remaining bytes: {Size - Offset} Requested: {structSize}");
				}
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public unsafe byte* Ptr;

		public int Length;

		public int Capacity;

		public global::Unity.Collections.AllocatorManager.AllocatorHandle Allocator;

		public readonly int Alignment;

		public readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Length == 0;
			}
		}

		public unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Ptr != null;
			}
		}

		public unsafe UnsafeAppendBuffer(int initialCapacity, int alignment, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			Alignment = alignment;
			Allocator = allocator;
			Ptr = null;
			Length = 0;
			Capacity = 0;
			SetCapacity(global::Unity.Mathematics.math.max(initialCapacity, 1));
		}

		public unsafe UnsafeAppendBuffer(void* ptr, int length)
		{
			Alignment = 0;
			Allocator = global::Unity.Collections.AllocatorManager.None;
			Ptr = (byte*)ptr;
			Length = 0;
			Capacity = length;
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				if (global::Unity.Collections.CollectionHelper.ShouldDeallocate(Allocator))
				{
					global::Unity.Collections.Memory.Unmanaged.Free(Ptr, Allocator);
					Allocator = global::Unity.Collections.AllocatorManager.Invalid;
				}
				Ptr = null;
				Length = 0;
				Capacity = 0;
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			if (global::Unity.Collections.CollectionHelper.ShouldDeallocate(Allocator))
			{
				global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.LowLevel.Unsafe.UnsafeDisposeJob
				{
					Ptr = Ptr,
					Allocator = Allocator
				}, inputDeps);
				Ptr = null;
				Allocator = global::Unity.Collections.AllocatorManager.Invalid;
				return result;
			}
			Ptr = null;
			return inputDeps;
		}

		public void Reset()
		{
			Length = 0;
		}

		public unsafe void SetCapacity(int capacity)
		{
			if (capacity > Capacity)
			{
				capacity = global::Unity.Mathematics.math.max(64, global::Unity.Mathematics.math.ceilpow2(capacity));
				byte* ptr = (byte*)global::Unity.Collections.Memory.Unmanaged.Allocate(capacity, Alignment, Allocator);
				if (Ptr != null)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, Ptr, Length);
					global::Unity.Collections.Memory.Unmanaged.Free(Ptr, Allocator);
				}
				Ptr = ptr;
				Capacity = capacity;
			}
		}

		public void ResizeUninitialized(int length)
		{
			SetCapacity(length);
			Length = length;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe void Add<T>(T value) where T : unmanaged
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			SetCapacity(Length + num);
			void* ptr = Ptr + Length;
			if (global::Unity.Collections.CollectionHelper.IsAligned((ulong)ptr, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<T>()))
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr(ref value, ptr);
			}
			else
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, &value, num);
			}
			Length += num;
		}

		public unsafe void Add(void* ptr, int structSize)
		{
			SetCapacity(Length + structSize);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Ptr + Length, ptr, structSize);
			Length += structSize;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe void AddArray<T>(void* ptr, int length) where T : unmanaged
		{
			Add(length);
			if (length != 0)
			{
				Add(ptr, length * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>());
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe void Add<T>(global::Unity.Collections.NativeArray<T> value) where T : unmanaged
		{
			Add(value.Length);
			Add(value.GetUnsafeReadOnlyPtr(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * value.Length);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe T Pop<T>() where T : unmanaged
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			long num2 = (long)Ptr;
			long num3 = Length;
			long num4 = num2 + num3 - num;
			T result = default(T);
			if (global::Unity.Collections.CollectionHelper.IsAligned((ulong)num4, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<T>()))
			{
				result = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>((void*)num4, 0);
			}
			else
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(&result, (void*)num4, num);
			}
			Length -= num;
			return result;
		}

		public unsafe void Pop(void* ptr, int structSize)
		{
			long num = (long)Ptr;
			long num2 = Length;
			long num3 = num + num2 - structSize;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, (void*)num3, structSize);
			Length -= structSize;
		}

		public global::Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer.Reader AsReader()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer.Reader(ref this);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckAlignment(int alignment)
		{
			bool num = alignment == 0;
			bool flag = ((alignment - 1) & alignment) == 0;
			if (!(!num && flag))
			{
				throw new global::System.ArgumentException($"Specified alignment must be non-zero positive power of two. Requested: {alignment}");
			}
		}
	}
}
