namespace UnityEngine.InputSystem.LowLevel
{
	public struct InputEventBuffer : global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.LowLevel.InputEventPtr>, global::System.Collections.IEnumerable, global::System.IDisposable, global::System.ICloneable
	{
		private struct Enumerator : global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.LowLevel.InputEventPtr>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private unsafe readonly global::UnityEngine.InputSystem.LowLevel.InputEvent* m_Buffer;

			private readonly int m_EventCount;

			private unsafe global::UnityEngine.InputSystem.LowLevel.InputEvent* m_CurrentEvent;

			private int m_CurrentIndex;

			public unsafe global::UnityEngine.InputSystem.LowLevel.InputEventPtr Current => m_CurrentEvent;

			object global::System.Collections.IEnumerator.Current => Current;

			public unsafe Enumerator(global::UnityEngine.InputSystem.LowLevel.InputEventBuffer buffer)
			{
				m_Buffer = buffer.bufferPtr;
				m_EventCount = buffer.m_EventCount;
				m_CurrentEvent = null;
				m_CurrentIndex = 0;
			}

			public unsafe bool MoveNext()
			{
				if (m_CurrentIndex == m_EventCount)
				{
					return false;
				}
				if (m_CurrentEvent == null)
				{
					m_CurrentEvent = m_Buffer;
					return m_CurrentEvent != null;
				}
				m_CurrentIndex++;
				if (m_CurrentIndex == m_EventCount)
				{
					return false;
				}
				m_CurrentEvent = global::UnityEngine.InputSystem.LowLevel.InputEvent.GetNextInMemory(m_CurrentEvent);
				return true;
			}

			public unsafe void Reset()
			{
				m_CurrentEvent = null;
				m_CurrentIndex = 0;
			}

			public void Dispose()
			{
			}
		}

		public const long BufferSizeUnknown = -1L;

		private global::Unity.Collections.NativeArray<byte> m_Buffer;

		private long m_SizeInBytes;

		private int m_EventCount;

		private bool m_WeOwnTheBuffer;

		public int eventCount => m_EventCount;

		public long sizeInBytes => m_SizeInBytes;

		public long capacityInBytes
		{
			get
			{
				if (!m_Buffer.IsCreated)
				{
					return 0L;
				}
				return m_Buffer.Length;
			}
		}

		public global::Unity.Collections.NativeArray<byte> data => m_Buffer;

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputEventPtr bufferPtr => (global::UnityEngine.InputSystem.LowLevel.InputEvent*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(m_Buffer);

		public unsafe InputEventBuffer(global::UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr, int eventCount, int sizeInBytes = -1, int capacityInBytes = -1)
		{
			this = default(global::UnityEngine.InputSystem.LowLevel.InputEventBuffer);
			if (eventPtr == null && eventCount != 0)
			{
				throw new global::System.ArgumentException("eventPtr is NULL but eventCount is != 0", "eventCount");
			}
			if (capacityInBytes != 0 && capacityInBytes < sizeInBytes)
			{
				throw new global::System.ArgumentException($"capacity({capacityInBytes}) cannot be smaller than size({sizeInBytes})", "capacityInBytes");
			}
			if (eventPtr != null)
			{
				if (capacityInBytes < 0)
				{
					capacityInBytes = sizeInBytes;
				}
				m_Buffer = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(eventPtr, (capacityInBytes > 0) ? capacityInBytes : 0, global::Unity.Collections.Allocator.None);
				m_SizeInBytes = ((sizeInBytes >= 0) ? sizeInBytes : (-1));
				m_EventCount = eventCount;
				m_WeOwnTheBuffer = false;
			}
		}

		public InputEventBuffer(global::Unity.Collections.NativeArray<byte> buffer, int eventCount, int sizeInBytes = -1, bool transferNativeArrayOwnership = false)
		{
			if (eventCount > 0 && !buffer.IsCreated)
			{
				throw new global::System.ArgumentException("buffer has no data but eventCount is > 0", "eventCount");
			}
			if (sizeInBytes > buffer.Length)
			{
				throw new global::System.ArgumentOutOfRangeException("sizeInBytes");
			}
			m_Buffer = buffer;
			m_WeOwnTheBuffer = transferNativeArrayOwnership;
			m_SizeInBytes = ((sizeInBytes >= 0) ? sizeInBytes : buffer.Length);
			m_EventCount = eventCount;
		}

		public unsafe void AppendEvent(global::UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr, int capacityIncrementInBytes = 2048, global::Unity.Collections.Allocator allocator = global::Unity.Collections.Allocator.Persistent)
		{
			if (eventPtr == null)
			{
				throw new global::System.ArgumentNullException("eventPtr");
			}
			uint num = eventPtr->sizeInBytes;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(AllocateEvent((int)num, capacityIncrementInBytes, allocator), eventPtr, num);
		}

		public unsafe global::UnityEngine.InputSystem.LowLevel.InputEvent* AllocateEvent(int sizeInBytes, int capacityIncrementInBytes = 2048, global::Unity.Collections.Allocator allocator = global::Unity.Collections.Allocator.Persistent)
		{
			if (sizeInBytes < 20)
			{
				throw new global::System.ArgumentException($"sizeInBytes must be >= sizeof(InputEvent) == {20} (was {sizeInBytes})", "sizeInBytes");
			}
			int num = global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(sizeInBytes, 4);
			long num2 = m_SizeInBytes + num;
			if (capacityInBytes < num2)
			{
				long num3 = global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(num2, capacityIncrementInBytes);
				if (num3 > int.MaxValue)
				{
					throw new global::System.NotImplementedException("NativeArray long support");
				}
				global::Unity.Collections.NativeArray<byte> nativeArray = new global::Unity.Collections.NativeArray<byte>((int)num3, allocator);
				if (m_Buffer.IsCreated)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray), global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(m_Buffer), this.sizeInBytes);
					if (m_WeOwnTheBuffer)
					{
						m_Buffer.Dispose();
					}
				}
				m_Buffer = nativeArray;
				m_WeOwnTheBuffer = true;
			}
			global::UnityEngine.InputSystem.LowLevel.InputEvent* ptr = (global::UnityEngine.InputSystem.LowLevel.InputEvent*)((byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(m_Buffer) + m_SizeInBytes);
			ptr->sizeInBytes = (uint)sizeInBytes;
			m_SizeInBytes += num;
			m_EventCount++;
			return ptr;
		}

		public unsafe bool Contains(global::UnityEngine.InputSystem.LowLevel.InputEvent* eventPtr)
		{
			if (eventPtr == null)
			{
				return false;
			}
			if (sizeInBytes == 0L)
			{
				return false;
			}
			void* unsafeBufferPointerWithoutChecks = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(data);
			if (eventPtr < unsafeBufferPointerWithoutChecks)
			{
				return false;
			}
			if (sizeInBytes != -1 && eventPtr >= (byte*)unsafeBufferPointerWithoutChecks + sizeInBytes)
			{
				return false;
			}
			return true;
		}

		public void Reset()
		{
			m_EventCount = 0;
			if (m_SizeInBytes != -1)
			{
				m_SizeInBytes = 0L;
			}
		}

		internal unsafe void AdvanceToNextEvent(ref global::UnityEngine.InputSystem.LowLevel.InputEvent* currentReadPos, ref global::UnityEngine.InputSystem.LowLevel.InputEvent* currentWritePos, ref int numEventsRetainedInBuffer, ref int numRemainingEvents, bool leaveEventInBuffer)
		{
			global::UnityEngine.InputSystem.LowLevel.InputEvent* ptr = currentReadPos;
			if (numRemainingEvents > 1)
			{
				ptr = global::UnityEngine.InputSystem.LowLevel.InputEvent.GetNextInMemory(currentReadPos);
			}
			if (leaveEventInBuffer)
			{
				uint num = currentReadPos->sizeInBytes;
				if (currentReadPos != currentWritePos)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(currentWritePos, currentReadPos, num);
				}
				currentWritePos = (global::UnityEngine.InputSystem.LowLevel.InputEvent*)((byte*)currentWritePos + global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(num, 4u));
				numEventsRetainedInBuffer++;
			}
			currentReadPos = ptr;
			numRemainingEvents--;
		}

		public global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> GetEnumerator()
		{
			return new global::UnityEngine.InputSystem.LowLevel.InputEventBuffer.Enumerator(this);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Dispose()
		{
			if (m_WeOwnTheBuffer)
			{
				m_Buffer.Dispose();
				m_WeOwnTheBuffer = false;
				m_SizeInBytes = 0L;
				m_EventCount = 0;
			}
		}

		public global::UnityEngine.InputSystem.LowLevel.InputEventBuffer Clone()
		{
			global::UnityEngine.InputSystem.LowLevel.InputEventBuffer result = default(global::UnityEngine.InputSystem.LowLevel.InputEventBuffer);
			if (m_Buffer.IsCreated)
			{
				result.m_Buffer = new global::Unity.Collections.NativeArray<byte>(m_Buffer.Length, global::Unity.Collections.Allocator.Persistent);
				result.m_Buffer.CopyFrom(m_Buffer);
				result.m_WeOwnTheBuffer = true;
			}
			result.m_SizeInBytes = m_SizeInBytes;
			result.m_EventCount = m_EventCount;
			return result;
		}

		object global::System.ICloneable.Clone()
		{
			return Clone();
		}
	}
}
