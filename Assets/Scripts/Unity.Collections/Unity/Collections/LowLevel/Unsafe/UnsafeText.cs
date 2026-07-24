namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	[global::System.Diagnostics.DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	public struct UnsafeText : global::Unity.Collections.INativeDisposable, global::System.IDisposable, global::Unity.Collections.IUTF8Bytes, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IIndexable<byte>
	{
		internal global::Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList m_UntypedListData;

		public readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.AsUnsafeListOfBytesRO().IsCreated;
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

		public unsafe byte this[int index]
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<byte>(m_UntypedListData.Ptr, index);
			}
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			set
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(m_UntypedListData.Ptr, index, value);
			}
		}

		public int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.AsUnsafeListOfBytesRO().Capacity - 1;
			}
			set
			{
				this.AsUnsafeListOfBytes().SetCapacity(value + 1);
			}
		}

		public int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.AsUnsafeListOfBytesRO().Length - 1;
			}
			set
			{
				this.AsUnsafeListOfBytes().Resize(value + 1);
				this.AsUnsafeListOfBytes()[value] = 0;
			}
		}

		public UnsafeText(int capacity, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			m_UntypedListData = default(global::Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList);
			this.AsUnsafeListOfBytes() = new global::Unity.Collections.LowLevel.Unsafe.UnsafeList<byte>(capacity + 1, allocator);
			Length = 0;
		}

		internal unsafe static global::Unity.Collections.LowLevel.Unsafe.UnsafeText* Alloc(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			return (global::Unity.Collections.LowLevel.Unsafe.UnsafeText*)global::Unity.Collections.Memory.Unmanaged.Allocate(sizeof(global::Unity.Collections.LowLevel.Unsafe.UnsafeText), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<global::Unity.Collections.LowLevel.Unsafe.UnsafeText>(), allocator);
		}

		internal unsafe static void Free(global::Unity.Collections.LowLevel.Unsafe.UnsafeText* data)
		{
			if (data == null)
			{
				throw new global::System.InvalidOperationException("UnsafeText has yet to be created or has been destroyed!");
			}
			global::Unity.Collections.AllocatorManager.AllocatorHandle allocator = data->m_UntypedListData.Allocator;
			data->Dispose();
			global::Unity.Collections.Memory.Unmanaged.Free(data, allocator);
		}

		public void Dispose()
		{
			this.AsUnsafeListOfBytes().Dispose();
		}

		public global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			return this.AsUnsafeListOfBytes().Dispose(inputDeps);
		}

		public unsafe ref byte ElementAt(int index)
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ArrayElementAsRef<byte>(m_UntypedListData.Ptr, index);
		}

		public void Clear()
		{
			Length = 0;
		}

		public unsafe byte* GetUnsafePtr()
		{
			return (byte*)m_UntypedListData.Ptr;
		}

		public bool TryResize(int newLength, global::Unity.Collections.NativeArrayOptions clearOptions = global::Unity.Collections.NativeArrayOptions.ClearMemory)
		{
			this.AsUnsafeListOfBytes().Resize(newLength + 1, clearOptions);
			this.AsUnsafeListOfBytes()[newLength] = 0;
			return true;
		}

		[global::Unity.Collections.ExcludeFromBurstCompatTesting("Returns managed string")]
		public override string ToString()
		{
			if (!IsCreated)
			{
				return "";
			}
			return global::Unity.Collections.FixedStringMethods.ConvertToString(ref this);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void CheckIndexInRange(int index)
		{
			if (index < 0)
			{
				throw new global::System.IndexOutOfRangeException($"Index {index} must be positive.");
			}
			if (index >= Length)
			{
				throw new global::System.IndexOutOfRangeException($"Index {index} is out of range in UnsafeText of {Length} length.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowCopyError(global::Unity.Collections.CopyError error, string source)
		{
			throw new global::System.ArgumentException($"UnsafeText: {error} while copying \"{source}\"");
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckCapacityInRange(int value, int length)
		{
			if (value < 0)
			{
				throw new global::System.ArgumentOutOfRangeException($"Value {value} must be positive.");
			}
			if ((uint)value < (uint)length)
			{
				throw new global::System.ArgumentOutOfRangeException($"Value {value} is out of range in NativeList of '{length}' Length.");
			}
		}
	}
}
