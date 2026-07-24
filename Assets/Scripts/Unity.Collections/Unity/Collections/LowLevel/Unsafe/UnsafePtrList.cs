namespace Unity.Collections.LowLevel.Unsafe
{
	[global::System.Diagnostics.DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListDebugView<>))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
	public struct UnsafePtrList<T> : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::System.Collections.Generic.IEnumerable<global::System.IntPtr>, global::System.Collections.IEnumerable where T : unmanaged
	{
		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ReadOnly
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe readonly T** Ptr;

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

			internal unsafe ReadOnly(T** ptr, int length)
			{
				Ptr = ptr;
				Length = length;
			}

			public unsafe int IndexOf(void* ptr)
			{
				for (int i = 0; i < Length; i++)
				{
					if (Ptr[i] == ptr)
					{
						return i;
					}
				}
				return -1;
			}

			public unsafe bool Contains(void* ptr)
			{
				return IndexOf(ptr) != -1;
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ParallelReader
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe readonly T** Ptr;

			public readonly int Length;

			internal unsafe ParallelReader(T** ptr, int length)
			{
				Ptr = ptr;
				Length = length;
			}

			public unsafe int IndexOf(void* ptr)
			{
				for (int i = 0; i < Length; i++)
				{
					if (Ptr[i] == ptr)
					{
						return i;
					}
				}
				return -1;
			}

			public unsafe bool Contains(void* ptr)
			{
				return IndexOf(ptr) != -1;
			}
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe readonly T** Ptr;

			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::System.IntPtr>* ListData;

			internal unsafe ParallelWriter(T** ptr, global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::System.IntPtr>* listData)
			{
				Ptr = ptr;
				ListData = listData;
			}

			public unsafe void AddNoResize(T* value)
			{
				ListData->AddNoResize((global::System.IntPtr)value);
			}

			public unsafe void AddRangeNoResize(T** ptr, int count)
			{
				ListData->AddRangeNoResize(ptr, count);
			}

			public unsafe void AddRangeNoResize(global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T> list)
			{
				ListData->AddRangeNoResize(list.Ptr, list.Length);
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		public unsafe readonly T** Ptr;

		public readonly int m_length;

		public readonly int m_capacity;

		public readonly global::Unity.Collections.AllocatorManager.AllocatorHandle Allocator;

		private readonly int padding;

		public int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.ListDataRO().Length;
			}
			set
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).Length = value;
			}
		}

		public int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.ListDataRO().Capacity;
			}
			set
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).Capacity = value;
			}
		}

		public unsafe T* this[int index]
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
					return Length == 0;
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
		public unsafe ref T* ElementAt(int index)
		{
			return ref Ptr[global::Unity.Collections.CollectionHelper.AssumePositive(index)];
		}

		public unsafe UnsafePtrList(T** ptr, int length)
		{
			this = default(global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>);
			Ptr = ptr;
			m_length = length;
			m_capacity = length;
			Allocator = global::Unity.Collections.AllocatorManager.None;
		}

		public unsafe UnsafePtrList(int initialCapacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.UninitializedMemory)
		{
			Ptr = null;
			m_length = 0;
			m_capacity = 0;
			padding = 0;
			Allocator = global::Unity.Collections.AllocatorManager.None;
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this) = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::System.IntPtr>(initialCapacity, allocator, options);
		}

		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>* Create(T** ptr, int length)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>* intPtr = global::Unity.Collections.AllocatorManager.Allocate<global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>>(global::Unity.Collections.AllocatorManager.Persistent);
			*intPtr = new global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>(ptr, length);
			return intPtr;
		}

		public unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>* Create(int initialCapacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.UninitializedMemory)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>* intPtr = global::Unity.Collections.AllocatorManager.Allocate<global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>>(allocator);
			*intPtr = new global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>(initialCapacity, allocator, options);
			return intPtr;
		}

		public unsafe static void Destroy(global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>* listData)
		{
			global::Unity.Collections.AllocatorManager.AllocatorHandle handle = ((global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref *listData).Allocator.Value == global::Unity.Collections.AllocatorManager.Invalid.Value) ? global::Unity.Collections.AllocatorManager.Persistent : global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref *listData).Allocator);
			listData->Dispose();
			global::Unity.Collections.AllocatorManager.Free(handle, listData);
		}

		public void Dispose()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).Dispose();
		}

		public global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			return global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).Dispose(inputDeps);
		}

		public void Clear()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).Clear();
		}

		public void Resize(int length, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.UninitializedMemory)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).Resize(length, options);
		}

		public void SetCapacity(int capacity)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).SetCapacity(capacity);
		}

		public void TrimExcess()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).TrimExcess();
		}

		public unsafe int IndexOf(void* ptr)
		{
			for (int i = 0; i < Length; i++)
			{
				if (Ptr[i] == ptr)
				{
					return i;
				}
			}
			return -1;
		}

		public unsafe bool Contains(void* ptr)
		{
			return IndexOf(ptr) != -1;
		}

		public unsafe void AddNoResize(void* value)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).AddNoResize((global::System.IntPtr)value);
		}

		public unsafe void AddRangeNoResize(void** ptr, int count)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).AddRangeNoResize(ptr, count);
		}

		public unsafe void AddRangeNoResize(global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T> list)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).AddRangeNoResize(list.Ptr, list.Length);
		}

		public void Add(in global::System.IntPtr value)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).Add(in value);
		}

		public unsafe void Add(void* value)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).Add((global::System.IntPtr)value);
		}

		public unsafe void AddRange(void* ptr, int length)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).AddRange(ptr, length);
		}

		public void AddRange(global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T> list)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).AddRange(global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref list));
		}

		public void InsertRangeWithBeginEnd(int begin, int end)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).InsertRangeWithBeginEnd(begin, end);
		}

		public void RemoveAtSwapBack(int index)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).RemoveAtSwapBack(index);
		}

		public void RemoveRangeSwapBack(int index, int count)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).RemoveRangeSwapBack(index, count);
		}

		public void RemoveAt(int index)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).RemoveAt(index);
		}

		public void RemoveRange(int index, int count)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafePtrListExtensions.ListData(ref this).RemoveRange(index, count);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		global::System.Collections.Generic.IEnumerator<global::System.IntPtr> global::System.Collections.Generic.IEnumerable<global::System.IntPtr>.GetEnumerator()
		{
			throw new global::System.NotImplementedException();
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>.ReadOnly(Ptr, Length);
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>.ParallelReader AsParallelReader()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>.ParallelReader(Ptr, Length);
		}

		public unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>.ParallelWriter AsParallelWriter()
		{
			return new global::Unity.Collections.LowLevel.Unsafe.UnsafePtrList<T>.ParallelWriter(Ptr, (global::Unity.Collections.LowLevel.Unsafe.UnsafeList<global::System.IntPtr>*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref this));
		}
	}
}
