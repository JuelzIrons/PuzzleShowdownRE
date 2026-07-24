namespace Unity.Netcode
{
	public struct FastBufferReader : global::System.IDisposable
	{
		internal struct ReaderHandle
		{
			internal unsafe byte* BufferPointer;

			internal int Position;

			internal int Length;

			internal global::Unity.Collections.Allocator Allocator;
		}

		internal unsafe global::Unity.Netcode.FastBufferReader.ReaderHandle* Handle;

		public unsafe int Position
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Handle->Position;
			}
		}

		public unsafe int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Handle->Length;
			}
		}

		public unsafe bool IsInitialized => Handle != null;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void CommitBitwiseReads(int amount)
		{
			Handle->Position += amount;
		}

		private unsafe static global::Unity.Netcode.FastBufferReader.ReaderHandle* CreateHandle(byte* buffer, int length, int offset, global::Unity.Collections.Allocator copyAllocator, global::Unity.Collections.Allocator internalAllocator)
		{
			global::Unity.Netcode.FastBufferReader.ReaderHandle* ptr;
			if (copyAllocator == global::Unity.Collections.Allocator.None)
			{
				ptr = (global::Unity.Netcode.FastBufferReader.ReaderHandle*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::Unity.Netcode.FastBufferReader.ReaderHandle), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<byte>(), internalAllocator);
				ptr->BufferPointer = buffer;
				ptr->Position = offset;
			}
			else
			{
				ptr = (global::Unity.Netcode.FastBufferReader.ReaderHandle*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::Unity.Netcode.FastBufferReader.ReaderHandle) + length, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<byte>(), copyAllocator);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr + 1, buffer + offset, length);
				ptr->BufferPointer = (byte*)ptr + sizeof(global::Unity.Netcode.FastBufferReader.ReaderHandle);
				ptr->Position = 0;
			}
			ptr->Length = length;
			ptr->Allocator = ((copyAllocator == global::Unity.Collections.Allocator.None) ? internalAllocator : copyAllocator);
			return ptr;
		}

		public unsafe FastBufferReader(global::Unity.Collections.NativeArray<byte> buffer, global::Unity.Collections.Allocator copyAllocator, int length = -1, int offset = 0, global::Unity.Collections.Allocator internalAllocator = global::Unity.Collections.Allocator.Temp)
		{
			Handle = CreateHandle((byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(buffer), (length == -1) ? buffer.Length : length, offset, copyAllocator, internalAllocator);
		}

		public unsafe FastBufferReader(global::System.ArraySegment<byte> buffer, global::Unity.Collections.Allocator copyAllocator, int length = -1, int offset = 0)
		{
			if (copyAllocator == global::Unity.Collections.Allocator.None)
			{
				throw new global::System.NotSupportedException("Allocator.None cannot be used with managed source buffers.");
			}
			fixed (byte* array = buffer.Array)
			{
				Handle = CreateHandle(array, (length == -1) ? buffer.Count : length, offset, copyAllocator, global::Unity.Collections.Allocator.Temp);
			}
		}

		public unsafe FastBufferReader(global::System.ArraySegment<byte> buffer, global::Unity.Collections.Allocator copyAllocator, int length = -1)
		{
			if (copyAllocator == global::Unity.Collections.Allocator.None)
			{
				throw new global::System.NotSupportedException("Allocator.None cannot be used with managed source buffers.");
			}
			fixed (byte* array = buffer.Array)
			{
				Handle = CreateHandle(array, (length == -1) ? buffer.Count : length, buffer.Offset, copyAllocator, global::Unity.Collections.Allocator.Temp);
			}
		}

		public unsafe FastBufferReader(global::System.ArraySegment<byte> buffer, global::Unity.Collections.Allocator copyAllocator)
		{
			if (copyAllocator == global::Unity.Collections.Allocator.None)
			{
				throw new global::System.NotSupportedException("Allocator.None cannot be used with managed source buffers.");
			}
			fixed (byte* array = buffer.Array)
			{
				Handle = CreateHandle(array, buffer.Count, buffer.Offset, copyAllocator, global::Unity.Collections.Allocator.Temp);
			}
		}

		public unsafe FastBufferReader(byte[] buffer, global::Unity.Collections.Allocator copyAllocator, int length = -1, int offset = 0)
		{
			if (copyAllocator == global::Unity.Collections.Allocator.None)
			{
				throw new global::System.NotSupportedException("Allocator.None cannot be used with managed source buffers.");
			}
			fixed (byte* buffer2 = buffer)
			{
				Handle = CreateHandle(buffer2, (length == -1) ? buffer.Length : length, offset, copyAllocator, global::Unity.Collections.Allocator.Temp);
			}
		}

		public unsafe FastBufferReader(byte* buffer, global::Unity.Collections.Allocator copyAllocator, int length, int offset = 0, global::Unity.Collections.Allocator internalAllocator = global::Unity.Collections.Allocator.Temp)
		{
			Handle = CreateHandle(buffer, length, offset, copyAllocator, internalAllocator);
		}

		public unsafe FastBufferReader(global::Unity.Netcode.FastBufferWriter writer, global::Unity.Collections.Allocator copyAllocator, int length = -1, int offset = 0, global::Unity.Collections.Allocator internalAllocator = global::Unity.Collections.Allocator.Temp)
		{
			Handle = CreateHandle(writer.GetUnsafePtr(), (length == -1) ? writer.Length : length, offset, copyAllocator, internalAllocator);
		}

		public unsafe FastBufferReader(global::Unity.Netcode.FastBufferReader reader, global::Unity.Collections.Allocator copyAllocator, int length = -1, int offset = 0, global::Unity.Collections.Allocator internalAllocator = global::Unity.Collections.Allocator.Temp)
		{
			Handle = CreateHandle(reader.GetUnsafePtr(), (length == -1) ? reader.Length : length, offset, copyAllocator, internalAllocator);
		}

		public unsafe void Dispose()
		{
			if (Handle != null)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(Handle, Handle->Allocator);
				Handle = null;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void Seek(int where)
		{
			Handle->Position = global::System.Math.Min(Length, where);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void MarkBytesRead(int amount)
		{
			Handle->Position += amount;
		}

		public global::Unity.Netcode.BitReader EnterBitwiseContext()
		{
			return new global::Unity.Netcode.BitReader(this);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe bool TryBeginRead(int bytes)
		{
			if (Handle->Position + bytes > Handle->Length)
			{
				return false;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe bool TryBeginReadValue<T>(in T value) where T : unmanaged
		{
			int num = sizeof(T);
			if (Handle->Position + num > Handle->Length)
			{
				return false;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe bool TryBeginReadInternal(int bytes)
		{
			if (Handle->Position + bytes > Handle->Length)
			{
				return false;
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe byte[] ToArray()
		{
			byte[] array = new byte[Length];
			fixed (byte* destination = array)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, Handle->BufferPointer, Length);
			}
			return array;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe byte* GetUnsafePtr()
		{
			return Handle->BufferPointer;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe byte* GetUnsafePtrAtCurrentPosition()
		{
			return Handle->BufferPointer + Handle->Position;
		}

		public void ReadNetworkSerializable<T>(out T value) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			value = new T();
			global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerReader> serializer = new global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerReader>(new global::Unity.Netcode.BufferSerializerReader(this));
			value.NetworkSerialize(serializer);
		}

		public void ReadNetworkSerializable<T>(out T[] value) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			ReadValueSafe(out int value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			value = new T[value2];
			for (int i = 0; i < value2; i++)
			{
				ReadNetworkSerializable(out value[i]);
			}
		}

		public void ReadNetworkSerializable<T>(out global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator) where T : unmanaged, global::Unity.Netcode.INetworkSerializable
		{
			ReadValueSafe(out int value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			value = new global::Unity.Collections.NativeArray<T>(value2, allocator);
			for (int i = 0; i < value2; i++)
			{
				ReadNetworkSerializable(out T value3);
				value[i] = value3;
			}
		}

		public void ReadNetworkSerializableInPlace<T>(ref T value) where T : global::Unity.Netcode.INetworkSerializable
		{
			global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerReader> serializer = new global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerReader>(new global::Unity.Netcode.BufferSerializerReader(this));
			value.NetworkSerialize(serializer);
		}

		public unsafe void ReadValue(out string s, bool oneByteChars = false)
		{
			ReadLength(out int length);
			s = "".PadRight(length);
			int length2 = s.Length;
			fixed (char* ptr = s)
			{
				if (oneByteChars)
				{
					for (int i = 0; i < length2; i++)
					{
						ReadByte(out var value);
						ptr[i] = (char)value;
					}
				}
				else
				{
					ReadBytes((byte*)ptr, length2 * 2);
				}
			}
		}

		public unsafe void ReadValueSafe(out string s, bool oneByteChars = false)
		{
			if (!TryBeginReadInternal(SizeOfLengthField()))
			{
				throw new global::System.OverflowException("Reading past the end of the buffer");
			}
			ReadLength(out int length);
			if (!TryBeginReadInternal(length * (oneByteChars ? 1 : 2)))
			{
				throw new global::System.OverflowException("Reading past the end of the buffer");
			}
			s = "".PadRight(length);
			int length2 = s.Length;
			fixed (char* ptr = s)
			{
				if (oneByteChars)
				{
					for (int i = 0; i < length2; i++)
					{
						ReadByte(out var value);
						ptr[i] = (char)value;
					}
				}
				else
				{
					ReadBytes((byte*)ptr, length2 * 2);
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static int SizeOfLengthField()
		{
			return 4;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void ReadLengthSafe(out uint length)
		{
			ReadUnmanagedSafe(out length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void ReadLength(out uint length)
		{
			ReadUnmanaged(out length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void ReadLengthSafe(out int length)
		{
			ReadLengthSafe(out uint length2);
			if (length2 > int.MaxValue)
			{
				throw new global::System.InvalidCastException("length value outside of int32 range");
			}
			length = (int)length2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void ReadLength(out int length)
		{
			ReadLength(out uint length2);
			length = (int)length2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadPartialValue<T>(out T value, int bytesToRead, int offsetBytes = 0) where T : unmanaged
		{
			T val = new T();
			byte* destination = (byte*)(&val) + offsetBytes;
			byte* source = Handle->BufferPointer + Handle->Position;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, source, bytesToRead);
			Handle->Position += bytesToRead;
			value = val;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadByte(out byte value)
		{
			value = Handle->BufferPointer[Handle->Position++];
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadByteSafe(out byte value)
		{
			if (!TryBeginReadInternal(1))
			{
				throw new global::System.OverflowException("Reading past the end of the buffer");
			}
			value = Handle->BufferPointer[Handle->Position++];
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadBytes(byte* value, int size, int offset = 0)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(value + offset, Handle->BufferPointer + Handle->Position, size);
			Handle->Position += size;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadBytesSafe(byte* value, int size, int offset = 0)
		{
			if (!TryBeginReadInternal(size))
			{
				throw new global::System.OverflowException("Reading past the end of the buffer");
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(value + offset, Handle->BufferPointer + Handle->Position, size);
			Handle->Position += size;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadBytes(ref byte[] value, int size, int offset = 0)
		{
			fixed (byte* value2 = value)
			{
				ReadBytes(value2, size, offset);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadBytesSafe(ref byte[] value, int size, int offset = 0)
		{
			fixed (byte* value2 = value)
			{
				ReadBytesSafe(value2, size, offset);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void ReadUnmanaged<T>(out T value) where T : unmanaged
		{
			fixed (T* ptr = &value)
			{
				byte* value2 = (byte*)ptr;
				ReadBytes(value2, sizeof(T));
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void ReadUnmanagedSafe<T>(out T value) where T : unmanaged
		{
			fixed (T* ptr = &value)
			{
				byte* value2 = (byte*)ptr;
				ReadBytesSafe(value2, sizeof(T));
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void ReadUnmanaged<T>(out T[] value) where T : unmanaged
		{
			ReadLength(out int length);
			int size = length * sizeof(T);
			value = new T[length];
			fixed (T* ptr = value)
			{
				byte* value2 = (byte*)ptr;
				ReadBytes(value2, size);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void ReadUnmanagedSafe<T>(out T[] value) where T : unmanaged
		{
			ReadLengthSafe(out int length);
			int size = length * sizeof(T);
			value = new T[length];
			fixed (T* ptr = value)
			{
				byte* value2 = (byte*)ptr;
				ReadBytesSafe(value2, size);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void ReadUnmanaged<T>(out global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator) where T : unmanaged
		{
			ReadLength(out int length);
			int size = length * sizeof(T);
			value = new global::Unity.Collections.NativeArray<T>(length, allocator);
			byte* unsafePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(value);
			ReadBytes(unsafePtr, size);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void ReadUnmanagedSafe<T>(out global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator) where T : unmanaged
		{
			ReadLengthSafe(out int length);
			int size = length * sizeof(T);
			value = new global::Unity.Collections.NativeArray<T>(length, allocator);
			byte* unsafePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(value);
			ReadBytesSafe(unsafePtr, size);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue<T>(out T value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			ReadNetworkSerializable(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue<T>(out T[] value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			ReadNetworkSerializable(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe<T>(out T value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			ReadNetworkSerializable(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe<T>(out T[] value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable, new()
		{
			ReadNetworkSerializable(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe<T>(out global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : unmanaged, global::Unity.Netcode.INetworkSerializable
		{
			ReadNetworkSerializable(out value, allocator);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue<T>(out T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue<T>(out T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue<T>(out global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			if (typeof(global::Unity.Netcode.INetworkSerializable).IsAssignableFrom(typeof(T)))
			{
				global::Unity.Netcode.NetworkVariableSerialization<global::Unity.Collections.NativeArray<T>>.Serializer.ReadWithAllocator(this, out value, allocator);
			}
			else
			{
				ReadUnmanaged(out value, allocator);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueTemp<T>(out global::Unity.Collections.NativeArray<T> value, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			if (typeof(global::Unity.Netcode.INetworkSerializable).IsAssignableFrom(typeof(T)))
			{
				global::Unity.Netcode.NetworkVariableSerialization<global::Unity.Collections.NativeArray<T>>.Serializer.ReadWithAllocator(this, out value, global::Unity.Collections.Allocator.Temp);
			}
			else
			{
				ReadUnmanaged(out value, global::Unity.Collections.Allocator.Temp);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe<T>(out T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe<T>(out T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe<T>(out global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			if (typeof(global::Unity.Netcode.INetworkSerializable).IsAssignableFrom(typeof(T)))
			{
				global::Unity.Netcode.NetworkVariableSerialization<global::Unity.Collections.NativeArray<T>>.Serializer.ReadWithAllocator(this, out value, allocator);
			}
			else
			{
				ReadUnmanagedSafe(out value, allocator);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafeTemp<T>(out global::Unity.Collections.NativeArray<T> value, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			if (typeof(global::Unity.Netcode.INetworkSerializable).IsAssignableFrom(typeof(T)))
			{
				global::Unity.Netcode.NetworkVariableSerialization<global::Unity.Collections.NativeArray<T>>.Serializer.ReadWithAllocator(this, out value, global::Unity.Collections.Allocator.Temp);
			}
			else
			{
				ReadUnmanagedSafe(out value, global::Unity.Collections.Allocator.Temp);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue<T>(out T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue<T>(out T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe<T>(out T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe<T>(out T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue<T>(out T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue<T>(out T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe<T>(out T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe<T>(out T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Vector2 value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Vector2[] value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Vector3 value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Vector3[] value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Vector2Int value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Vector2Int[] value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Vector3Int value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Vector3Int[] value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Vector4 value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Vector4[] value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Quaternion value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Quaternion[] value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Pose value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Pose[] value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Color value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Color[] value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Color32 value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Color32[] value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Ray value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Ray[] value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Ray2D value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValue(out global::UnityEngine.Ray2D[] value)
		{
			ReadUnmanaged(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Vector2 value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Vector2[] value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Vector3 value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Vector3[] value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Vector2Int value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Vector2Int[] value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Vector3Int value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Vector3Int[] value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Vector4 value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Vector4[] value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Quaternion value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Quaternion[] value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Pose value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Pose[] value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Color value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Color[] value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Color32 value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Color32[] value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Ray value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Ray[] value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Ray2D value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe(out global::UnityEngine.Ray2D[] value)
		{
			ReadUnmanagedSafe(out value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadValue<T>(out T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ReadLength(out int length);
			value = new T
			{
				Length = length
			};
			ReadBytes(value.GetUnsafePtr(), length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadValueSafe<T>(out T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ReadLengthSafe(out int length);
			value = new T
			{
				Length = length
			};
			ReadBytesSafe(value.GetUnsafePtr(), length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadValueSafeInPlace<T>(ref T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ReadLengthSafe(out int length);
			value.Length = length;
			ReadBytesSafe(value.GetUnsafePtr(), length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadValueSafe<T>(out global::Unity.Collections.NativeArray<T> value, global::Unity.Collections.Allocator allocator) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ReadLengthSafe(out int length);
			value = new global::Unity.Collections.NativeArray<T>(length, allocator);
			T* unsafePtr = (T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(value);
			for (int i = 0; i < length; i++)
			{
				ReadValueSafeInPlace(ref unsafePtr[i]);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadValueSafeTemp<T>(out global::Unity.Collections.NativeArray<T> value) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ReadLengthSafe(out int length);
			value = new global::Unity.Collections.NativeArray<T>(length, global::Unity.Collections.Allocator.Temp);
			T* unsafePtr = (T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(value);
			for (int i = 0; i < length; i++)
			{
				ReadValueSafeInPlace(ref unsafePtr[i]);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void ReadValueSafe<T>(out T[] value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			ReadLengthSafe(out int length);
			value = new T[length];
			for (int i = 0; i < length; i++)
			{
				ReadValueSafeInPlace(ref value[i]);
			}
		}
	}
}
