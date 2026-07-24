namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::System.Diagnostics.DebuggerDisplay("Length = {m_ListData == null ? default : m_ListData->Length}, Capacity = {m_ListData == null ? default : m_ListData->Capacity}")]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.NativeListDebugView<>))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct NativeList<T> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::Unity.Collections.INativeList<T>, global::Unity.Collections.IIndexable<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable where T : unmanaged
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsAtomicWriteOnly]
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

			public unsafe void AddNoResize(T value)
			{
				int index = global::System.Threading.Interlocked.Increment(ref ListData->m_length) - 1;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(ListData->Ptr, index, value);
			}

			public unsafe void AddRangeNoResize(void* ptr, int count)
			{
				int num = global::System.Threading.Interlocked.Add(ref ListData->m_length, count) - count;
				int num2 = sizeof(T);
				void* destination = (byte*)ListData->Ptr + num * num2;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, ptr, count * num2);
			}

			public unsafe void AddRangeNoResize(global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> list)
			{
				AddRangeNoResize(list.Ptr, list.Length);
			}

			public unsafe void AddRangeNoResize(global::Unity.Collections.NativeList<T> list)
			{
				AddRangeNoResize(*list.m_ListData);
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* m_ListData;

		public unsafe T this[int index]
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return (*m_ListData)[index];
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				(*m_ListData)[index] = value;
			}
		}

		public unsafe int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return global::Unity.Collections.CollectionHelper.AssumePositive(m_ListData->Length);
			}
			set
			{
				m_ListData->Resize(value, global::Unity.Collections.NativeArrayOptions.ClearMemory);
			}
		}

		public unsafe int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return m_ListData->Capacity;
			}
			set
			{
				m_ListData->Capacity = value;
			}
		}

		public unsafe readonly bool IsEmpty
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (m_ListData != null)
				{
					return m_ListData->Length == 0;
				}
				return true;
			}
		}

		public unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_ListData != null;
			}
		}

		public NativeList(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
			: this(1, allocator)
		{
		}

		public NativeList(int initialCapacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			this = default(global::Unity.Collections.NativeList<T>);
			global::Unity.Collections.AllocatorManager.AllocatorHandle allocator2 = allocator;
			Initialize(initialCapacity, ref allocator2);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle) })]
		internal unsafe void Initialize<U>(int initialCapacity, ref U allocator) where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			m_ListData = global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.Create(initialCapacity, ref allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle) })]
		internal static global::Unity.Collections.NativeList<T> New<U>(int initialCapacity, ref U allocator) where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			global::Unity.Collections.NativeList<T> result = default(global::Unity.Collections.NativeList<T>);
			result.Initialize(initialCapacity, ref allocator);
			return result;
		}

		public unsafe ref T ElementAt(int index)
		{
			return ref m_ListData->ElementAt(index);
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* GetUnsafeList()
		{
			return m_ListData;
		}

		public unsafe void AddNoResize(T value)
		{
			m_ListData->AddNoResize(value);
		}

		public unsafe void AddRangeNoResize(void* ptr, int count)
		{
			m_ListData->AddRangeNoResize(ptr, count);
		}

		public unsafe void AddRangeNoResize(global::Unity.Collections.NativeList<T> list)
		{
			m_ListData->AddRangeNoResize(*list.m_ListData);
		}

		public unsafe void Add(in T value)
		{
			m_ListData->Add(in value);
		}

		public unsafe void AddRange(global::Unity.Collections.NativeArray<T> array)
		{
			AddRange(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), array.Length);
		}

		public unsafe void AddRange(void* ptr, int count)
		{
			m_ListData->AddRange(ptr, global::Unity.Collections.CollectionHelper.AssumePositive(count));
		}

		public unsafe void AddReplicate(in T value, int count)
		{
			m_ListData->AddReplicate(in value, global::Unity.Collections.CollectionHelper.AssumePositive(count));
		}

		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			m_ListData->InsertRangeWithBeginEnd(begin, end);
		}

		public void InsertRange(int index, int count)
		{
			InsertRangeWithBeginEnd(index, index + count);
		}

		public unsafe void RemoveAtSwapBack(int index)
		{
			m_ListData->RemoveAtSwapBack(index);
		}

		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			m_ListData->RemoveRangeSwapBack(index, count);
		}

		public unsafe void RemoveAt(int index)
		{
			m_ListData->RemoveAt(index);
		}

		public unsafe void RemoveRange(int index, int count)
		{
			m_ListData->RemoveRange(index, count);
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.Destroy(m_ListData);
				m_ListData = null;
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(global::Unity.Collections.AllocatorManager.AllocatorHandle) })]
		internal unsafe void Dispose<U>(ref U allocator) where U : unmanaged, global::Unity.Collections.AllocatorManager.IAllocator
		{
			if (IsCreated)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T>.Destroy(m_ListData, ref allocator);
				m_ListData = null;
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.NativeListDisposeJob
			{
				Data = new global::Unity.Collections.NativeListDispose
				{
					m_ListData = (global::Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList*)m_ListData
				}
			}, inputDeps);
			m_ListData = null;
			return result;
		}

		public unsafe void Clear()
		{
			m_ListData->Clear();
		}

		[global::System.Obsolete("Implicit cast from `NativeList<T>` to `NativeArray<T>` has been deprecated; Use '.AsArray()' method to do explicit cast instead.", false)]
		public static implicit operator global::Unity.Collections.NativeArray<T>(global::Unity.Collections.NativeList<T> nativeList)
		{
			return nativeList.AsArray();
		}

		public unsafe global::Unity.Collections.NativeArray<T> AsArray()
		{
			return global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(m_ListData->Ptr, m_ListData->Length, global::Unity.Collections.Allocator.None);
		}

		public unsafe global::Unity.Collections.NativeArray<T> AsDeferredJobArray()
		{
			byte* listData = (byte*)m_ListData;
			listData++;
			return global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(listData, 0, global::Unity.Collections.Allocator.Invalid);
		}

		public unsafe global::Unity.Collections.NativeArray<T> ToArray(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.NativeArray<T> result = global::Unity.Collections.CollectionHelper.CreateNativeArray<T>(Length, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(result.m_Buffer, m_ListData->Ptr, Length * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>());
			return result;
		}

		public unsafe void CopyFrom(in global::Unity.Collections.NativeArray<T> other)
		{
			m_ListData->CopyFrom(in other);
		}

		public unsafe void CopyFrom(in global::Unity.Collections.LowLevel.Unsafe.UnsafeList<T> other)
		{
			m_ListData->CopyFrom(in other);
		}

		public unsafe void CopyFrom(in global::Unity.Collections.NativeList<T> other)
		{
			CopyFrom(in *other.m_ListData);
		}

		public global::Unity.Collections.NativeArray<T>.Enumerator GetEnumerator()
		{
			global::Unity.Collections.NativeArray<T> array = AsArray();
			return new global::Unity.Collections.NativeArray<T>.Enumerator(ref array);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		global::System.Collections.Generic.IEnumerator<T> global::System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		public unsafe void Resize(int length, global::Unity.Collections.NativeArrayOptions options)
		{
			m_ListData->Resize(length, options);
		}

		public void ResizeUninitialized(int length)
		{
			Resize(length, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
		}

		public unsafe void SetCapacity(int capacity)
		{
			m_ListData->SetCapacity(capacity);
		}

		public unsafe void TrimExcess()
		{
			m_ListData->TrimExcess();
		}

		public unsafe global::Unity.Collections.NativeArray<T>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.NativeArray<T>.ReadOnly(m_ListData->Ptr, m_ListData->Length);
		}

		public unsafe global::Unity.Collections.NativeArray<T>.ReadOnly AsParallelReader()
		{
			return new global::Unity.Collections.NativeArray<T>.ReadOnly(m_ListData->Ptr, m_ListData->Length);
		}

		public unsafe global::Unity.Collections.NativeList<T>.ParallelWriter AsParallelWriter()
		{
			return new global::Unity.Collections.NativeList<T>.ParallelWriter(m_ListData);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckInitialCapacity(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("initialCapacity", "Capacity must be >= 0");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckTotalSize(int initialCapacity, long totalSize)
		{
			if (totalSize > int.MaxValue)
			{
				throw new global::System.ArgumentOutOfRangeException("initialCapacity", $"Capacity * sizeof(T) cannot exceed {int.MaxValue} bytes");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckSufficientCapacity(int capacity, int length)
		{
			if (capacity < length)
			{
				throw new global::System.InvalidOperationException($"Length {length} exceeds Capacity {capacity}");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckIndexInRange(int value, int length)
		{
			if (value < 0)
			{
				throw new global::System.IndexOutOfRangeException($"Value {value} must be positive.");
			}
			if ((uint)value >= (uint)length)
			{
				throw new global::System.IndexOutOfRangeException($"Value {value} is out of range in NativeList of '{length}' Length.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckArgPositive(int value)
		{
			if (value < 0)
			{
				throw new global::System.ArgumentOutOfRangeException($"Value {value} must be positive.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private unsafe void CheckHandleMatches(global::Unity.Collections.AllocatorManager.AllocatorHandle handle)
		{
			if (m_ListData == null)
			{
				throw new global::System.ArgumentOutOfRangeException($"Allocator handle {handle} can't match because container is not initialized.");
			}
			if (m_ListData->Allocator.Index != handle.Index)
			{
				throw new global::System.ArgumentOutOfRangeException($"Allocator handle {handle} can't match because container handle index doesn't match.");
			}
			if (m_ListData->Allocator.Version != handle.Version)
			{
				throw new global::System.ArgumentOutOfRangeException($"Allocator handle {handle} matches container handle index, but has different version.");
			}
		}
	}
}
