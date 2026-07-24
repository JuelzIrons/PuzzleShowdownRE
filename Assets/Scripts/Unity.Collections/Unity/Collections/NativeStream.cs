namespace Unity.Collections
{
	[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public struct NativeStream : global::Unity.Collections.INativeDisposable, global::System.IDisposable
	{
		[global::Unity.Burst.BurstCompile]
		private struct ConstructJobList : global::Unity.Jobs.IJob
		{
			public global::Unity.Collections.NativeStream Container;

			[global::Unity.Collections.ReadOnly]
			[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
			public unsafe global::Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList* List;

			public unsafe void Execute()
			{
				Container.AllocateForEach(List->m_length);
			}
		}

		[global::Unity.Burst.BurstCompile]
		private struct ConstructJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Collections.NativeStream Container;

			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<int> Length;

			public void Execute()
			{
				Container.AllocateForEach(Length[0]);
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerSupportsMinMaxWriteRestriction]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility]
		public struct Writer
		{
			private global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.Writer m_Writer;

			public int ForEachCount => m_Writer.ForEachCount;

			internal Writer(ref global::Unity.Collections.NativeStream stream)
			{
				m_Writer = stream.m_Stream.AsWriter();
			}

			public void PatchMinMaxRange(int foreEachIndex)
			{
			}

			public void BeginForEachIndex(int foreachIndex)
			{
				m_Writer.BeginForEachIndex(foreachIndex);
			}

			public void EndForEachIndex()
			{
				m_Writer.EndForEachIndex();
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public void Write<T>(T value) where T : unmanaged
			{
				Allocate<T>() = value;
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe ref T Allocate<T>() where T : unmanaged
			{
				int size = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(Allocate(size));
			}

			public unsafe byte* Allocate(int size)
			{
				return m_Writer.Allocate(size);
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private void CheckBeginForEachIndex(int foreachIndex)
			{
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private void CheckEndForEachIndex()
			{
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private void CheckAllocateSize(int size)
			{
			}
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeContainer]
		[global::Unity.Collections.LowLevel.Unsafe.NativeContainerIsReadOnly]
		[global::Unity.Collections.GenerateTestsForBurstCompatibility]
		public struct Reader
		{
			private global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.Reader m_Reader;

			public int ForEachCount => m_Reader.ForEachCount;

			public int RemainingItemCount => m_Reader.RemainingItemCount;

			internal Reader(ref global::Unity.Collections.NativeStream stream)
			{
				m_Reader = stream.m_Stream.AsReader();
			}

			public int BeginForEachIndex(int foreachIndex)
			{
				return m_Reader.BeginForEachIndex(foreachIndex);
			}

			public void EndForEachIndex()
			{
				m_Reader.EndForEachIndex();
			}

			public unsafe byte* ReadUnsafePtr(int size)
			{
				m_Reader.m_RemainingItemCount--;
				byte* currentPtr = m_Reader.m_CurrentPtr;
				m_Reader.m_CurrentPtr += size;
				if (m_Reader.m_CurrentPtr > m_Reader.m_CurrentBlockEnd)
				{
					m_Reader.m_CurrentBlock = m_Reader.m_CurrentBlock->Next;
					m_Reader.m_CurrentPtr = m_Reader.m_CurrentBlock->Data;
					m_Reader.m_CurrentBlockEnd = (byte*)m_Reader.m_CurrentBlock + 4096;
					currentPtr = m_Reader.m_CurrentPtr;
					m_Reader.m_CurrentPtr += size;
				}
				return currentPtr;
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public unsafe ref T Read<T>() where T : unmanaged
			{
				int size = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(ReadUnsafePtr(size));
			}

			[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
			public ref T Peek<T>() where T : unmanaged
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
				return ref m_Reader.Peek<T>();
			}

			public int Count()
			{
				return m_Reader.Count();
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private void CheckNotReadingOutOfBounds(int size)
			{
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckRead()
			{
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private void CheckReadSize(int size)
			{
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private void CheckBeginForEachIndex(int forEachIndex)
			{
			}

			[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
			private unsafe void CheckEndForEachIndex()
			{
				if (m_Reader.m_RemainingItemCount != 0)
				{
					throw new global::System.ArgumentException("Not all elements (Count) have been read. If this is intentional, simply skip calling EndForEachIndex();");
				}
				if (m_Reader.m_CurrentBlockEnd != m_Reader.m_CurrentPtr)
				{
					throw new global::System.ArgumentException("Not all data (Data Size) has been read. If this is intentional, simply skip calling EndForEachIndex();");
				}
			}
		}

		private global::Unity.Collections.LowLevel.Unsafe.UnsafeStream m_Stream;

		public readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Stream.IsCreated;
			}
		}

		public readonly int ForEachCount => m_Stream.ForEachCount;

		public NativeStream(int bufferCount, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			AllocateBlock(out this, allocator);
			m_Stream.AllocateForEach(bufferCount);
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public unsafe static global::Unity.Jobs.JobHandle ScheduleConstruct<T>(out global::Unity.Collections.NativeStream stream, global::Unity.Collections.NativeList<T> bufferCount, global::Unity.Jobs.JobHandle dependency, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
		{
			AllocateBlock(out stream, allocator);
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.NativeStream.ConstructJobList
			{
				List = (global::Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList*)bufferCount.GetUnsafeList(),
				Container = stream
			}, dependency);
		}

		public static global::Unity.Jobs.JobHandle ScheduleConstruct(out global::Unity.Collections.NativeStream stream, global::Unity.Collections.NativeArray<int> bufferCount, global::Unity.Jobs.JobHandle dependency, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			AllocateBlock(out stream, allocator);
			return global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.NativeStream.ConstructJob
			{
				Length = bufferCount,
				Container = stream
			}, dependency);
		}

		public readonly bool IsEmpty()
		{
			return m_Stream.IsEmpty();
		}

		public global::Unity.Collections.NativeStream.Reader AsReader()
		{
			return new global::Unity.Collections.NativeStream.Reader(ref this);
		}

		public global::Unity.Collections.NativeStream.Writer AsWriter()
		{
			return new global::Unity.Collections.NativeStream.Writer(ref this);
		}

		public int Count()
		{
			return m_Stream.Count();
		}

		[global::Unity.Collections.GenerateTestsForBurstCompatibility(GenericTypeArguments = new global::System.Type[] { typeof(int) })]
		public global::Unity.Collections.NativeArray<T> ToNativeArray<T>(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator) where T : unmanaged
		{
			return m_Stream.ToNativeArray<T>(allocator);
		}

		public void Dispose()
		{
			if (IsCreated)
			{
				m_Stream.Dispose();
			}
		}

		public global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps)
		{
			if (!IsCreated)
			{
				return inputDeps;
			}
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(new global::Unity.Collections.NativeStreamDisposeJob
			{
				Data = new global::Unity.Collections.NativeStreamDispose
				{
					m_StreamData = m_Stream
				}
			}, inputDeps);
			m_Stream = default(global::Unity.Collections.LowLevel.Unsafe.UnsafeStream);
			return result;
		}

		private static void AllocateBlock(out global::Unity.Collections.NativeStream stream, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeStream.AllocateBlock(out stream.m_Stream, allocator);
		}

		private void AllocateForEach(int forEachCount)
		{
			m_Stream.AllocateForEach(forEachCount);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckForEachCountGreaterThanZero(int forEachCount)
		{
			if (forEachCount <= 0)
			{
				throw new global::System.ArgumentException("foreachCount must be > 0", "foreachCount");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private readonly void CheckRead()
		{
		}
	}
}
