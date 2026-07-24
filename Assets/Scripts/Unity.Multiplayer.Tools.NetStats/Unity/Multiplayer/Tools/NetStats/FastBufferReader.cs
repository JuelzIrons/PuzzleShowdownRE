namespace Unity.Multiplayer.Tools.NetStats
{
	internal struct FastBufferReader : global::System.IDisposable
	{
		internal struct ReaderHandle
		{
			internal unsafe byte* BufferPointer;

			internal int Position;

			internal int Length;

			internal global::Unity.Collections.Allocator Allocator;
		}

		internal unsafe readonly global::Unity.Multiplayer.Tools.NetStats.FastBufferReader.ReaderHandle* Handle;

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

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void CommitBitwiseReads(int amount)
		{
			Handle->Position += amount;
		}

		private unsafe static global::Unity.Multiplayer.Tools.NetStats.FastBufferReader.ReaderHandle* CreateHandle(byte* buffer, int length, int offset, global::Unity.Collections.Allocator allocator)
		{
			global::Unity.Multiplayer.Tools.NetStats.FastBufferReader.ReaderHandle* ptr = null;
			if (allocator == global::Unity.Collections.Allocator.None)
			{
				ptr = (global::Unity.Multiplayer.Tools.NetStats.FastBufferReader.ReaderHandle*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::Unity.Multiplayer.Tools.NetStats.FastBufferReader.ReaderHandle) + length, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<byte>(), global::Unity.Collections.Allocator.Temp);
				ptr->BufferPointer = buffer;
				ptr->Position = offset;
			}
			else
			{
				ptr = (global::Unity.Multiplayer.Tools.NetStats.FastBufferReader.ReaderHandle*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::Unity.Multiplayer.Tools.NetStats.FastBufferReader.ReaderHandle) + length, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<byte>(), allocator);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr + 1, buffer + offset, length);
				ptr->BufferPointer = (byte*)ptr + sizeof(global::Unity.Multiplayer.Tools.NetStats.FastBufferReader.ReaderHandle);
				ptr->Position = 0;
			}
			ptr->Length = length;
			ptr->Allocator = allocator;
			return ptr;
		}

		public unsafe FastBufferReader(global::Unity.Collections.NativeArray<byte> buffer, global::Unity.Collections.Allocator allocator, int length = -1, int offset = 0)
		{
			Handle = CreateHandle((byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(buffer), global::System.Math.Max(1, (length == -1) ? buffer.Length : length), offset, allocator);
		}

		public unsafe FastBufferReader(global::System.ArraySegment<byte> buffer, global::Unity.Collections.Allocator allocator, int length = -1, int offset = 0)
		{
			if (allocator == global::Unity.Collections.Allocator.None)
			{
				throw new global::System.NotSupportedException("Allocator.None cannot be used with managed source buffers.");
			}
			fixed (byte* array = buffer.Array)
			{
				Handle = CreateHandle(array, global::System.Math.Max(1, (length == -1) ? buffer.Count : length), offset, allocator);
			}
		}

		public unsafe FastBufferReader(byte[] buffer, global::Unity.Collections.Allocator allocator, int length = -1, int offset = 0)
		{
			if (allocator == global::Unity.Collections.Allocator.None)
			{
				throw new global::System.NotSupportedException("Allocator.None cannot be used with managed source buffers.");
			}
			fixed (byte* buffer2 = buffer)
			{
				Handle = CreateHandle(buffer2, global::System.Math.Max(1, (length == -1) ? buffer.Length : length), offset, allocator);
			}
		}

		public unsafe FastBufferReader(byte* buffer, global::Unity.Collections.Allocator allocator, int length, int offset = 0)
		{
			Handle = CreateHandle(buffer, global::System.Math.Max(1, length), offset, allocator);
		}

		public unsafe FastBufferReader(global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter writer, global::Unity.Collections.Allocator allocator, int length = -1, int offset = 0)
		{
			Handle = CreateHandle(writer.GetUnsafePtr(), global::System.Math.Max(1, (length == -1) ? writer.Length : length), offset, allocator);
		}

		public unsafe void Dispose()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(Handle, Handle->Allocator);
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

		public global::Unity.Multiplayer.Tools.NetStats.BitReader EnterBitwiseContext()
		{
			return new global::Unity.Multiplayer.Tools.NetStats.BitReader(this);
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

		public void ReadNetworkSerializable<T>(out T value) where T : global::Unity.Multiplayer.Tools.NetStats.INetworkSerializable, new()
		{
			value = new T();
			global::Unity.Multiplayer.Tools.NetStats.BufferSerializer<global::Unity.Multiplayer.Tools.NetStats.BufferSerializerReader> serializer = new global::Unity.Multiplayer.Tools.NetStats.BufferSerializer<global::Unity.Multiplayer.Tools.NetStats.BufferSerializerReader>(new global::Unity.Multiplayer.Tools.NetStats.BufferSerializerReader(this));
			value.NetworkSerialize(serializer);
		}

		public void ReadNetworkSerializable<T>(out T[] value) where T : global::Unity.Multiplayer.Tools.NetStats.INetworkSerializable, new()
		{
			ReadValueSafe(out int value2);
			value = new T[value2];
			for (int i = 0; i < value2; i++)
			{
				ReadNetworkSerializable(out value[i]);
			}
		}

		public unsafe void ReadValue(out string s, bool oneByteChars = false)
		{
			ReadValue(out uint value);
			s = "".PadRight((int)value);
			int length = s.Length;
			fixed (char* ptr = s)
			{
				if (oneByteChars)
				{
					for (int i = 0; i < length; i++)
					{
						ReadByte(out var value2);
						ptr[i] = (char)value2;
					}
				}
				else
				{
					ReadBytes((byte*)ptr, length * 2);
				}
			}
		}

		public unsafe void ReadValueSafe(out string s, bool oneByteChars = false)
		{
			if (!TryBeginReadInternal(4))
			{
				throw new global::System.OverflowException("Reading past the end of the buffer");
			}
			ReadValue(out uint value);
			if (!TryBeginReadInternal((int)value * (oneByteChars ? 1 : 2)))
			{
				throw new global::System.OverflowException("Reading past the end of the buffer");
			}
			s = "".PadRight((int)value);
			int length = s.Length;
			fixed (char* ptr = s)
			{
				if (oneByteChars)
				{
					for (int i = 0; i < length; i++)
					{
						ReadByte(out var value2);
						ptr[i] = (char)value2;
					}
				}
				else
				{
					ReadBytes((byte*)ptr, length * 2);
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadValue<T>(out T[] array) where T : unmanaged
		{
			ReadValue(out int value);
			int size = value * sizeof(T);
			array = new T[value];
			fixed (T* ptr = array)
			{
				byte* value2 = (byte*)ptr;
				ReadBytes(value2, size);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadValueSafe<T>(out T[] array) where T : unmanaged
		{
			if (!TryBeginReadInternal(4))
			{
				throw new global::System.OverflowException("Reading past the end of the buffer");
			}
			ReadValue(out int value);
			int num = value * sizeof(T);
			if (!TryBeginReadInternal(num))
			{
				throw new global::System.OverflowException("Reading past the end of the buffer");
			}
			array = new T[value];
			fixed (T* ptr = array)
			{
				byte* value2 = (byte*)ptr;
				ReadBytes(value2, num);
			}
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
		public unsafe void ReadValue<T>(out T value) where T : unmanaged
		{
			int num = sizeof(T);
			fixed (T* destination = &value)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, Handle->BufferPointer + Handle->Position, num);
			}
			Handle->Position += num;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void ReadValueSafe<T>(out T value) where T : unmanaged
		{
			int num = sizeof(T);
			if (!TryBeginReadInternal(num))
			{
				throw new global::System.OverflowException("Reading past the end of the buffer");
			}
			fixed (T* destination = &value)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, Handle->BufferPointer + Handle->Position, num);
			}
			Handle->Position += num;
		}
	}
}
