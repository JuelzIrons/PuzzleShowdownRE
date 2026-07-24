namespace Unity.Collections.LowLevel.Unsafe
{
	[global::System.Diagnostics.DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeListTDebugView<>))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct UnsafeList<T> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::Unity.Collections.INativeList<T>, global::Unity.Collections.IIndexable<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable where T : unmanaged
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ReadOnly : global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe readonly T* Ptr;

			public readonly int Length;

			public unsafe readonly bool IsCreated
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return Ptr != null;
				}
			}

			public readonly bool IsEmpty
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					if (IsCreated)
					{
						return Length == 0;
					}
					return true;
				}
			}

			internal unsafe ReadOnly(T* ptr, int length)
			{
				Ptr = ptr;
				Length = length;
			}

			public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.Enumerator GetEnumerator()
			{
				return new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.Enumerator
				{
					m_Ptr = Ptr,
					m_Length = Length,
					m_Index = -1
				};
			}

			global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
			{
				throw new global::System.NotImplementedException();
			}

			global::System.Collections.Generic.IEnumerator<T> global::System.Collections.Generic.IEnumerable<T>.GetEnumerator()
			{
				throw new global::System.NotImplementedException();
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ParallelReader
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe readonly T* Ptr;

			public readonly int Length;

			internal unsafe ParallelReader(T* ptr, int length)
			{
				Ptr = ptr;
				Length = length;
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* ListData;

			public unsafe readonly void* Ptr
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return ListData->Ptr;
				}
			}

			internal unsafe ParallelWriter(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* listData)
			{
				ListData = listData;
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe void AddNoResize(T value)
			{
				int index = global::System.Threading.Interlocked.Increment(ref ListData->m_length) - 1;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(ListData->Ptr, index, value);
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe void AddRangeNoResize(void* ptr, int count)
			{
				int num = global::System.Threading.Interlocked.Add(ref ListData->m_length, count) - count;
				void* destination = (byte*)ListData->Ptr + num * sizeof(T);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, ptr, count * sizeof(T));
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe void AddRangeNoResize(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list)
			{
				AddRangeNoResize(list.Ptr, list.Length);
			}
		}

		public struct Enumerator : global::System.Collections.Generic.IEnumerator<T>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			internal unsafe T* m_Ptr;

			internal int m_Length;

			internal int m_Index;

			public unsafe T Current
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Ptr[m_Index];
				}
			}

			object global::System.Collections.IEnumerator.Current => Current;

			public void Dispose()
			{
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return ++m_Index < m_Length;
			}

			public void Reset()
			{
				m_Index = -1;
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public unsafe T* Ptr;

		public int m_length;

		public int m_capacity;

		public global::Unity.Collections.AllocatorManager.AllocatorHandle Allocator;

		private readonly int padding;

		public int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return global::Unity.Collections.CollectionHelper.AssumePositive(m_length);
			}
			set
			{
				if (value > Capacity)
				{
					Resize(value);
				}
				else
				{
					m_length = value;
				}
			}
		}

		public int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return global::Unity.Collections.CollectionHelper.AssumePositive(m_capacity);
			}
			set
			{
				SetCapacity(value);
			}
		}

		public unsafe T this[int index]
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Ptr[global::Unity.Collections.CollectionHelper.AssumePositive(index)];
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				Ptr[global::Unity.Collections.CollectionHelper.AssumePositive(index)] = value;
			}
		}

		public readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (IsCreated)
				{
					return m_length == 0;
				}
				return true;
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

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe ref T ElementAt(int index)
		{
			return ref Ptr[global::Unity.Collections.CollectionHelper.AssumePositive(index)];
		}

		public unsafe UnsafeList(T* ptr, int length)
		{
			this = default(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>);
			Ptr = ptr;
			m_length = length;
			m_capacity = length;
			Allocator = global::Unity.Collections.AllocatorManager.None;
		}

		public unsafe UnsafeList(int initialCapacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.UninitializedMemory)
		{
			Ptr = null;
			m_length = 0;
			m_capacity = 0;
			Allocator = allocator;
			padding = 0;
			SetCapacity(global::Unity.Mathematics.math.max(initialCapacity, 1));
			if (options == global::Unity.Collections.NativeArrayOptions.ClearMemory && Ptr != null)
			{
				int num = sizeof(T);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(Ptr, Capacity * num);
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle) })]
		internal unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* Create<U>(int initialCapacity, ref U allocator, global::Unity.Collections.NativeArrayOptions options) where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* intPtr = global::Unity.Collections.AllocatorManager.Allocate(ref allocator, default(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>), 1);
			*intPtr = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(initialCapacity, allocator.Handle, options);
			return intPtr;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle) })]
		internal unsafe static void Destroy<U>(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* listData, ref U allocator) where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			listData->Dispose(ref allocator);
			global::Unity.Collections.AllocatorManager.Free(ref allocator, listData, sizeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>>(), 1);
		}

		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* Create(int initialCapacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.UninitializedMemory)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* intPtr = global::Unity.Collections.AllocatorManager.Allocate<global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>>(allocator);
			*intPtr = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>(initialCapacity, allocator, options);
			return intPtr;
		}

		public unsafe static void Destroy(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* listData)
		{
			global::Unity.Collections.AllocatorManager.AllocatorHandle allocator = listData->Allocator;
			listData->Dispose();
			global::Unity.Collections.AllocatorManager.Free(allocator, listData);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle) })]
		internal unsafe void Dispose<U>(ref U allocator) where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			global::Unity.Collections.AllocatorManager.Free(ref allocator, Ptr, m_capacity);
			Ptr = null;
			m_length = 0;
			m_capacity = 0;
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				if (global::Unity.Collections.CollectionHelper.ShouldDeallocate(Allocator))
				{
					global::Unity.Collections.AllocatorManager.Free(Allocator, Ptr, m_capacity);
					Allocator = global::Unity.Collections.AllocatorManager.Invalid;
				}
				Ptr = null;
				m_length = 0;
				m_capacity = 0;
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

		public void Clear()
		{
			m_length = 0;
		}

		public unsafe void Resize(int length, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.UninitializedMemory)
		{
			int length2 = m_length;
			if (length > Capacity)
			{
				SetCapacity(length);
			}
			m_length = length;
			if (options == global::Unity.Collections.NativeArrayOptions.ClearMemory && length2 < length)
			{
				int num = length - length2;
				byte* ptr = (byte*)Ptr;
				int num2 = sizeof(T);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(ptr + length2 * num2, num * num2);
			}
		}

		private unsafe void ResizeExact<U>(ref U allocator, int newCapacity) where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			newCapacity = global::Unity.Mathematics.math.max(0, newCapacity);
			T* ptr = null;
			int alignOf = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<T>();
			int num = sizeof(T);
			if (newCapacity > 0)
			{
				ptr = (T*)global::Unity.Collections.AllocatorManager.Allocate(ref allocator, num, alignOf, newCapacity);
				if (Ptr != null && m_capacity > 0)
				{
					int num2 = global::Unity.Mathematics.math.min(newCapacity, Capacity) * num;
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, Ptr, num2);
				}
			}
			global::Unity.Collections.AllocatorManager.Free(ref allocator, Ptr, Capacity);
			Ptr = ptr;
			m_capacity = newCapacity;
			m_length = global::Unity.Mathematics.math.min(m_length, newCapacity);
		}

		private void ResizeExact(int capacity)
		{
			ResizeExact(ref Allocator, capacity);
		}

		private unsafe void SetCapacity<U>(ref U allocator, int capacity) where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			int num = sizeof(T);
			int x = global::Unity.Mathematics.math.max(capacity, 64 / num);
			x = global::Unity.Mathematics.math.ceilpow2(x);
			if (x != Capacity)
			{
				ResizeExact(ref allocator, x);
			}
		}

		public void SetCapacity(int capacity)
		{
			SetCapacity(ref Allocator, capacity);
		}

		public void TrimExcess()
		{
			if (Capacity != m_length)
			{
				ResizeExact(m_length);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void AddNoResize(T value)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(Ptr, m_length, value);
			m_length++;
		}

		public unsafe void AddRangeNoResize(void* ptr, int count)
		{
			int num = sizeof(T);
			void* destination = (byte*)Ptr + m_length * num;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, ptr, count * num);
			m_length += count;
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe void AddRangeNoResize(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list)
		{
			AddRangeNoResize(list.Ptr, global::Unity.Collections.CollectionHelper.AssumePositive(list.Length));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void Add(in T value)
		{
			int length = m_length;
			if (m_length < m_capacity)
			{
				Ptr[length] = value;
				m_length++;
			}
			else
			{
				Resize(length + 1);
				Ptr[length] = value;
			}
		}

		public unsafe void AddRange(void* ptr, int count)
		{
			int length = m_length;
			if (m_length + count > Capacity)
			{
				Resize(m_length + count);
			}
			else
			{
				m_length += count;
			}
			int num = sizeof(T);
			void* destination = (byte*)Ptr + length * num;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, ptr, count * num);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe void AddRange(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list)
		{
			AddRange(list.Ptr, list.Length);
		}

		public unsafe void AddReplicate(in T value, int count)
		{
			int length = m_length;
			if (m_length + count > Capacity)
			{
				Resize(m_length + count);
			}
			else
			{
				m_length += count;
			}
			fixed (T* ptr = &value)
			{
				void* source = ptr;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpyReplicate(Ptr + length, source, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(), count);
			}
		}

		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			begin = global::Unity.Collections.CollectionHelper.AssumePositive(begin);
			end = global::Unity.Collections.CollectionHelper.AssumePositive(end);
			int num = end - begin;
			if (num >= 1)
			{
				int length = m_length;
				if (m_length + num > Capacity)
				{
					Resize(m_length + num);
				}
				else
				{
					m_length += num;
				}
				int num2 = length - begin;
				if (num2 >= 1)
				{
					int num3 = sizeof(T);
					int num4 = num2 * num3;
					byte* ptr = (byte*)Ptr;
					byte* destination = ptr + end * num3;
					byte* source = ptr + begin * num3;
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(destination, source, num4);
				}
			}
		}

		public void InsertRange(int index, int count)
		{
			InsertRangeWithBeginEnd(index, index + count);
		}

		public unsafe void RemoveAtSwapBack(int index)
		{
			index = global::Unity.Collections.CollectionHelper.AssumePositive(index);
			int num = m_length - 1;
			T* num2 = Ptr + index;
			T* ptr = Ptr + num;
			*num2 = *ptr;
			m_length--;
		}

		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			index = global::Unity.Collections.CollectionHelper.AssumePositive(index);
			count = global::Unity.Collections.CollectionHelper.AssumePositive(count);
			if (count > 0)
			{
				int num = global::Unity.Mathematics.math.max(m_length - count, index + count);
				int num2 = sizeof(T);
				void* destination = (byte*)Ptr + index * num2;
				void* source = (byte*)Ptr + num * num2;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, source, (m_length - num) * num2);
				m_length -= count;
			}
		}

		public unsafe void RemoveAt(int index)
		{
			index = global::Unity.Collections.CollectionHelper.AssumePositive(index);
			T* ptr = Ptr + index;
			T* ptr2 = ptr + 1;
			m_length--;
			for (int i = index; i < m_length; i++)
			{
				*(ptr++) = *(ptr2++);
			}
		}

		public unsafe void RemoveRange(int index, int count)
		{
			index = global::Unity.Collections.CollectionHelper.AssumePositive(index);
			count = global::Unity.Collections.CollectionHelper.AssumePositive(count);
			if (count > 0)
			{
				int num = global::Unity.Mathematics.math.min(index + count, m_length);
				int num2 = sizeof(T);
				void* destination = (byte*)Ptr + index * num2;
				void* source = (byte*)Ptr + num * num2;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, source, (m_length - num) * num2);
				m_length -= count;
			}
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.ReadOnly(Ptr, Length);
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.ParallelReader AsParallelReader()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.ParallelReader(Ptr, Length);
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.ParallelWriter AsParallelWriter()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.ParallelWriter((global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref this));
		}

		public unsafe void CopyFrom(in global::Unity.Collections.NativeArray<T> other)
		{
			Resize(other.Length);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Ptr, other.GetUnsafeReadOnlyPtr(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * other.Length);
		}

		public unsafe void CopyFrom(in global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other)
		{
			Resize(other.Length);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Ptr, other.Ptr, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * other.Length);
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.Enumerator GetEnumerator()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.Enumerator
			{
				m_Ptr = Ptr,
				m_Length = Length,
				m_Index = -1
			};
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		global::System.Collections.Generic.IEnumerator<T> global::System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		internal unsafe static void CheckNull(void* listData)
		{
			if (listData == null)
			{
				throw new global::System.InvalidOperationException("UnsafeList has yet to be created or has been destroyed!");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckIndexCount(int index, int count)
		{
			if (count < 0)
			{
				throw new global::System.ArgumentOutOfRangeException($"Value for count {count} must be positive.");
			}
			if (index < 0)
			{
				throw new global::System.IndexOutOfRangeException($"Value for index {index} must be positive.");
			}
			if (index > Length)
			{
				throw new global::System.IndexOutOfRangeException($"Value for index {index} is out of bounds.");
			}
			if (index + count > Length)
			{
				throw new global::System.ArgumentOutOfRangeException($"Value for count {count} is out of bounds.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckBeginEndNoLength(int begin, int end)
		{
			if (begin > end)
			{
				throw new global::System.ArgumentException($"Value for begin {begin} index must less or equal to end {end}.");
			}
			if (begin < 0)
			{
				throw new global::System.ArgumentOutOfRangeException($"Value for begin {begin} must be positive.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckBeginEnd(int begin, int end)
		{
			if (begin > Length)
			{
				throw new global::System.ArgumentOutOfRangeException($"Value for begin {begin} is out of bounds.");
			}
			if (end > Length)
			{
				throw new global::System.ArgumentOutOfRangeException($"Value for end {end} is out of bounds.");
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckNoResizeHasEnoughCapacity(int length)
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckNoResizeHasEnoughCapacity(int length, int index)
		{
			if (Capacity < index + length)
			{
				throw new global::System.InvalidOperationException($"AddNoResize assumes that list capacity is sufficient (Capacity {Capacity}, Length {Length}), requested length {length}!");
			}
		}
	}
}
