namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::System.Diagnostics.DebuggerDisplay("Length = {Length}, IsCreated = {IsCreated}")]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public struct NativeBitArray : global::Unity.Collections.INativeDisposable, global::System.IDisposable
	{
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsReadOnly]
		public struct ReadOnly
		{
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			internal global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray.ReadOnly m_BitArray;

			public readonly bool IsCreated => m_BitArray.IsCreated;

			public readonly bool IsEmpty => m_BitArray.IsEmpty;

			public readonly int Length => global::Unity.Collections.CollectionHelper.AssumePositive(m_BitArray.Length);

			internal unsafe ReadOnly(ref global::Unity.Collections.NativeBitArray data)
			{
				m_BitArray = data.m_BitArray->AsReadOnly();
			}

			public readonly ulong GetBits(int pos, int numBits = 1)
			{
				return m_BitArray.GetBits(pos, numBits);
			}

			public readonly bool IsSet(int pos)
			{
				return m_BitArray.IsSet(pos);
			}

			public readonly int Find(int pos, int numBits)
			{
				return m_BitArray.Find(pos, numBits);
			}

			public readonly int Find(int pos, int count, int numBits)
			{
				return m_BitArray.Find(pos, count, numBits);
			}

			public readonly bool TestNone(int pos, int numBits = 1)
			{
				return m_BitArray.TestNone(pos, numBits);
			}

			public readonly bool TestAny(int pos, int numBits = 1)
			{
				return m_BitArray.TestAny(pos, numBits);
			}

			public readonly bool TestAll(int pos, int numBits = 1)
			{
				return m_BitArray.TestAll(pos, numBits);
			}

			public readonly int CountBits(int pos, int numBits = 1)
			{
				return m_BitArray.CountBits(pos, numBits);
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private readonly void CheckRead()
			{
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		internal unsafe global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray* m_BitArray;

		internal global::Unity.Collections.AllocatorManager.AllocatorHandle m_Allocator;

		public unsafe readonly bool IsCreated
		{
			get
			{
				if (m_BitArray != null)
				{
					return m_BitArray->IsCreated;
				}
				return false;
			}
		}

		public readonly bool IsEmpty
		{
			get
			{
				if (IsCreated)
				{
					return Length == 0;
				}
				return true;
			}
		}

		public unsafe readonly int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return global::Unity.Collections.CollectionHelper.AssumePositive(m_BitArray->Length);
			}
		}

		public unsafe readonly int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return global::Unity.Collections.CollectionHelper.AssumePositive(m_BitArray->Capacity);
			}
		}

		public unsafe NativeBitArray(int numBits, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.ClearMemory)
		{
			m_BitArray = global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray.Alloc(allocator);
			m_Allocator = allocator;
			*m_BitArray = new global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray(numBits, allocator, options);
		}

		public unsafe void Resize(int numBits, global::Unity.Collections.NativeArrayOptions options = global::Unity.Collections.NativeArrayOptions.UninitializedMemory)
		{
			m_BitArray->Resize(numBits, options);
		}

		public unsafe void SetCapacity(int capacityInBits)
		{
			m_BitArray->SetCapacity(capacityInBits);
		}

		public unsafe void TrimExcess()
		{
			m_BitArray->TrimExcess();
		}

		public unsafe void Dispose()
		{
			if (IsCreated)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray.Free(m_BitArray, m_Allocator);
				m_BitArray = null;
				m_Allocator = global::Unity.Collections.AllocatorManager.Invalid;
			}
		}

		public unsafe global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.NativeBitArrayDisposeJob
			{
				Data = new global::Unity.Collections.NativeBitArrayDispose
				{
					m_BitArrayData = m_BitArray,
					m_Allocator = m_Allocator
				}
			}, inputDeps);
			m_BitArray = null;
			m_Allocator = global::Unity.Collections.AllocatorManager.Invalid;
			return result;
		}

		public unsafe void Clear()
		{
			m_BitArray->Clear();
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe global::Unity.Collections.NativeArray<T> AsNativeArray<T>() where T : unmanaged
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * 8;
			int length = m_BitArray->Length / num;
			return global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(m_BitArray->Ptr, length, global::Unity.Collections.Allocator.None);
		}

		public unsafe void Set(int pos, bool value)
		{
			m_BitArray->Set(pos, value);
		}

		public unsafe void SetBits(int pos, bool value, int numBits)
		{
			m_BitArray->SetBits(pos, value, numBits);
		}

		public unsafe void SetBits(int pos, ulong value, int numBits = 1)
		{
			m_BitArray->SetBits(pos, value, numBits);
		}

		public unsafe ulong GetBits(int pos, int numBits = 1)
		{
			return m_BitArray->GetBits(pos, numBits);
		}

		public unsafe bool IsSet(int pos)
		{
			return m_BitArray->IsSet(pos);
		}

		public unsafe void Copy(int dstPos, int srcPos, int numBits)
		{
			m_BitArray->Copy(dstPos, srcPos, numBits);
		}

		public unsafe void Copy(int dstPos, ref global::Unity.Collections.NativeBitArray srcBitArray, int srcPos, int numBits)
		{
			m_BitArray->Copy(dstPos, ref *srcBitArray.m_BitArray, srcPos, numBits);
		}

		public unsafe int Find(int pos, int numBits)
		{
			return m_BitArray->Find(pos, numBits);
		}

		public unsafe int Find(int pos, int count, int numBits)
		{
			return m_BitArray->Find(pos, count, numBits);
		}

		public unsafe bool TestNone(int pos, int numBits = 1)
		{
			return m_BitArray->TestNone(pos, numBits);
		}

		public unsafe bool TestAny(int pos, int numBits = 1)
		{
			return m_BitArray->TestAny(pos, numBits);
		}

		public unsafe bool TestAll(int pos, int numBits = 1)
		{
			return m_BitArray->TestAll(pos, numBits);
		}

		public unsafe int CountBits(int pos, int numBits = 1)
		{
			return m_BitArray->CountBits(pos, numBits);
		}

		public global::Unity.Collections.NativeBitArray.ReadOnly AsReadOnly()
		{
			return new global::Unity.Collections.NativeBitArray.ReadOnly(ref this);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private readonly void CheckRead()
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private unsafe void CheckReadBounds<T>() where T : unmanaged
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * 8;
			int num2 = m_BitArray->Length / num;
			if (num2 == 0)
			{
				throw new global::System.InvalidOperationException($"Number of bits in the NativeBitArray {m_BitArray->Length} is not sufficient to cast to NativeArray<T> {(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() * 8)}.");
			}
			if (m_BitArray->Length != num * num2)
			{
				throw new global::System.InvalidOperationException($"Number of bits in the NativeBitArray {m_BitArray->Length} couldn't hold multiple of T {(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>())}. Output array would be truncated.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}
	}
}
