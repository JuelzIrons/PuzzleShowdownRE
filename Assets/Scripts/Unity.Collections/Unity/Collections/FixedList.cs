namespace Unity.Collections
{
	[global::System.Serializable]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
	{
		typeof(int),
		typeof(global::Unity.Collections.FixedBytes32Align8)
	})]
	internal struct FixedList<T, U> : global::Unity.Collections.INativeList<T>, global::Unity.Collections.IIndexable<T> where T : unmanaged where U : unmanaged
	{
		[global::UnityEngine.SerializeField]
		internal U data;

		internal unsafe ushort length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				fixed (U* ptr = &data)
				{
					void* ptr2 = ptr;
					return *(ushort*)ptr2;
				}
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				fixed (U* ptr = &data)
				{
					void* ptr2 = ptr;
					*(ushort*)ptr2 = value;
				}
			}
		}

		internal unsafe readonly byte* buffer
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (U* ptr = &data)
				{
					void* ptr2 = ptr;
					return (byte*)ptr2 + global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<ushort>();
				}
			}
		}

		[global::Unity.Properties.CreateProperty]
		public int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return length;
			}
			set
			{
				length = (ushort)value;
			}
		}

		[global::Unity.Properties.CreateProperty]
		private global::System.Collections.Generic.IEnumerable<T> Elements => ToArray();

		public readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Length == 0;
			}
		}

		internal readonly int LengthInBytes => Length * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();

		internal unsafe readonly byte* Buffer
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return buffer + global::Unity.Collections.FixedList.PaddingBytes<T>();
			}
		}

		public int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return global::Unity.Collections.FixedList.Capacity<U, T>();
			}
			set
			{
			}
		}

		public unsafe T this[int index]
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<T>(Buffer, global::Unity.Collections.CollectionHelper.AssumePositive(index));
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(Buffer, global::Unity.Collections.CollectionHelper.AssumePositive(index), value);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe ref T ElementAt(int index)
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<T>(Buffer, index);
		}

		public unsafe override int GetHashCode()
		{
			return (int)global::Unity.Collections.CollectionHelper.Hash(Buffer, LengthInBytes);
		}

		public void Add(in T item)
		{
			AddNoResize(in item);
		}

		public unsafe void AddRange(void* ptr, int length)
		{
			AddRangeNoResize(ptr, length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void AddNoResize(in T item)
		{
			this[Length++] = item;
		}

		public unsafe void AddRangeNoResize(void* ptr, int length)
		{
			int num = Length;
			Length += length;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Buffer + (nint)num * (nint)sizeof(T), ptr, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * length);
		}

		public unsafe void AddReplicate(in T value, int count)
		{
			int num = Length;
			Length += count;
			fixed (T* source = &value)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpyReplicate(Buffer + (nint)num * (nint)sizeof(T), source, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(), count);
			}
		}

		public void Clear()
		{
			Length = 0;
		}

		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			int num = end - begin;
			if (num >= 1)
			{
				int num2 = length - begin;
				Length += num;
				if (num2 >= 1)
				{
					int num3 = num2 * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
					byte* num4 = Buffer;
					byte* destination = num4 + end * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
					byte* source = num4 + begin * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(destination, source, num3);
				}
			}
		}

		public void InsertRange(int index, int count)
		{
			InsertRangeWithBeginEnd(index, index + count);
		}

		public void Insert(int index, in T item)
		{
			InsertRangeWithBeginEnd(index, index + 1);
			this[index] = item;
		}

		public void RemoveAtSwapBack(int index)
		{
			RemoveRangeSwapBack(index, 1);
		}

		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			if (count > 0)
			{
				int num = global::Unity.Mathematics.math.max(Length - count, index + count);
				int num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				void* destination = Buffer + index * num2;
				void* source = Buffer + num * num2;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, source, (Length - num) * num2);
				Length -= count;
			}
		}

		public void RemoveAt(int index)
		{
			RemoveRange(index, 1);
		}

		public unsafe void RemoveRange(int index, int count)
		{
			if (count > 0)
			{
				int num = global::Unity.Mathematics.math.min(index + count, Length);
				int num2 = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				void* destination = Buffer + index * num2;
				void* source = Buffer + num * num2;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, source, (Length - num) * num2);
				Length -= count;
			}
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed array")]
		public unsafe T[] ToArray()
		{
			T[] array = new T[Length];
			byte* source = Buffer;
			fixed (T* destination = array)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, source, LengthInBytes);
			}
			return array;
		}

		public unsafe global::Unity.Collections.NativeArray<T> ToNativeArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.NativeArray<T> nativeArray = global::Unity.Collections.CollectionHelper.CreateNativeArray<T>(Length, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray), Buffer, LengthInBytes);
			return nativeArray;
		}
	}
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct FixedList
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		internal static int PaddingBytes<T>() where T : unmanaged
		{
			return global::Unity.Mathematics.math.max(0, global::Unity.Mathematics.math.min(6, (1 << global::Unity.Mathematics.math.tzcnt(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>())) - 2));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal static int StorageBytes<BUFFER, T>() where BUFFER : unmanaged where T : unmanaged
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<BUFFER>() - global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<ushort>() - PaddingBytes<T>();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal static int Capacity<BUFFER, T>() where BUFFER : unmanaged where T : unmanaged
		{
			return StorageBytes<BUFFER, T>() / global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[]
		{
			typeof(int),
			typeof(int)
		})]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal static void CheckResize<BUFFER, T>(int newLength) where BUFFER : unmanaged where T : unmanaged
		{
			int num = Capacity<BUFFER, T>();
			if (newLength < 0 || newLength > num)
			{
				throw new global::System.IndexOutOfRangeException($"NewLength {newLength} is out of range of '{num}' Capacity.");
			}
		}
	}
}
