namespace Unity.Networking.Transport
{
	public struct PacketProcessor
	{
		private readonly global::Unity.Networking.Transport.PacketBuffer m_PacketBuffer;

		public bool IsCreated => m_PacketBuffer.Payload != global::System.IntPtr.Zero;

		public int Length => PacketMetadataRef.DataLength;

		public int Offset => PacketMetadataRef.DataOffset;

		public int Capacity => PacketMetadataRef.DataCapacity;

		public int BytesAvailableAtEnd => Capacity - (Offset + Length);

		public int BytesAvailableAtStart => Offset;

		public unsafe ref global::Unity.Networking.Transport.NetworkEndpoint EndpointRef => ref *(global::Unity.Networking.Transport.NetworkEndpoint*)(void*)m_PacketBuffer.Endpoint;

		internal ref global::Unity.Networking.Transport.ConnectionId ConnectionRef => ref PacketMetadataRef.Connection;

		private unsafe ref global::Unity.Networking.Transport.PacketMetadata PacketMetadataRef => ref *(global::Unity.Networking.Transport.PacketMetadata*)(void*)m_PacketBuffer.Metadata;

		internal PacketProcessor(global::Unity.Networking.Transport.PacketBuffer packetBuffer)
		{
			m_PacketBuffer = packetBuffer;
		}

		public unsafe ref T GetPayloadDataRef<T>(int offset = 0) where T : unmanaged
		{
			return ref *(T*)((byte*)GetUnsafePayloadPtr() + Offset + offset);
		}

		public unsafe void AppendToPayload(void* dataPtr, int size)
		{
			if (size > BytesAvailableAtEnd)
			{
				global::UnityEngine.Debug.LogError($"The requested data size ({size}) does not fit at the end of the payload ({BytesAvailableAtEnd} Bytes available).");
				return;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy((byte*)(void*)m_PacketBuffer.Payload + Offset + Length, dataPtr, size);
			PacketMetadataRef.DataLength += size;
		}

		public unsafe void AppendToPayload(global::Unity.Networking.Transport.PacketProcessor processor)
		{
			AppendToPayload((byte*)processor.GetUnsafePayloadPtr() + processor.Offset, processor.Length);
		}

		public unsafe void AppendToPayload<T>(T value) where T : unmanaged
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			if (num > BytesAvailableAtEnd)
			{
				global::UnityEngine.Debug.LogError($"The requested data size ({num}) does not fit at the end of the payload ({BytesAvailableAtEnd} Bytes available).");
				return;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy((byte*)(void*)m_PacketBuffer.Payload + Offset + Length, &value, num);
			PacketMetadataRef.DataLength += num;
		}

		public unsafe void PrependToPayload<T>(T value) where T : unmanaged
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			if (num > BytesAvailableAtStart)
			{
				global::UnityEngine.Debug.LogError($"The requested data size ({num}) does not fit at the start of the payload ({BytesAvailableAtStart} Bytes available).");
				return;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy((byte*)(void*)m_PacketBuffer.Payload + Offset - num, &value, num);
			PacketMetadataRef.DataOffset -= num;
			PacketMetadataRef.DataLength += num;
		}

		public T RemoveFromPayloadStart<T>() where T : unmanaged
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			if (num > Length)
			{
				global::UnityEngine.Debug.LogError($"The size of the required type ({num}) does not fit in the payload ({Length}).");
				return default(T);
			}
			T payloadDataRef = GetPayloadDataRef<T>();
			PacketMetadataRef.DataOffset += num;
			PacketMetadataRef.DataLength -= num;
			return payloadDataRef;
		}

		public unsafe void RemoveFromPayloadStart(void* ptr, int size)
		{
			if (size > Length)
			{
				global::UnityEngine.Debug.LogError($"The size of the buffer ({size}) is larger than the payload ({Length}).");
				return;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, (byte*)(void*)m_PacketBuffer.Payload + Offset, size);
			PacketMetadataRef.DataOffset += size;
			PacketMetadataRef.DataLength -= size;
		}

		public unsafe int CopyPayload(void* destinationPtr, int size)
		{
			if (Length <= 0)
			{
				return 0;
			}
			int num = Length;
			if (size < Length)
			{
				global::UnityEngine.Debug.LogError($"The payload size ({Length}) does not fit in the provided pointer ({size})");
				num = size;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destinationPtr, (byte*)(void*)m_PacketBuffer.Payload + Offset, num);
			return num;
		}

		public unsafe void* GetUnsafePayloadPtr()
		{
			return (void*)m_PacketBuffer.Payload;
		}

		internal void SetUnsafeMetadata(int size, int offset = 0)
		{
			PacketMetadataRef.DataLength = size;
			PacketMetadataRef.DataOffset = offset;
		}

		public void Drop()
		{
			SetUnsafeMetadata(0);
		}
	}
}
