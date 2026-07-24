namespace Unity.Multiplayer.Tools.NetStats
{
	internal struct FastBufferWriter : global::System.IDisposable
	{
		internal struct WriterHandle
		{
			internal unsafe byte* BufferPointer;

			internal int Position;

			internal int Length;

			internal int Capacity;

			internal int MaxCapacity;

			internal global::Unity.Collections.Allocator Allocator;

			internal bool BufferGrew;
		}

		internal unsafe readonly global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter.WriterHandle* Handle;

		public unsafe int Position
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Handle->Position;
			}
		}

		public unsafe int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Handle->Capacity;
			}
		}

		public unsafe int MaxCapacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return Handle->MaxCapacity;
			}
		}

		public unsafe int Length
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				if (Handle->Position <= Handle->Length)
				{
					return Handle->Length;
				}
				return Handle->Position;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void CommitBitwiseWrites(int amount)
		{
			Handle->Position += amount;
		}

		public unsafe FastBufferWriter(int size, global::Unity.Collections.Allocator allocator, int maxSize = -1)
		{
			Handle = (global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter.WriterHandle*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter.WriterHandle) + size, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter.WriterHandle>(), allocator);
			Handle->BufferPointer = (byte*)Handle + sizeof(global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter.WriterHandle);
			Handle->Position = 0;
			Handle->Length = 0;
			Handle->Capacity = size;
			Handle->Allocator = allocator;
			Handle->MaxCapacity = ((maxSize < size) ? size : maxSize);
			Handle->BufferGrew = false;
		}

		public unsafe void Dispose()
		{
			if (Handle->BufferGrew)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(Handle->BufferPointer, Handle->Allocator);
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(Handle, Handle->Allocator);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void Seek(int where)
		{
			where = global::System.Math.Min(where, Handle->Capacity);
			if (Handle->Position > Handle->Length && where < Handle->Position)
			{
				Handle->Length = Handle->Position;
			}
			Handle->Position = where;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void Truncate(int where = -1)
		{
			if (where == -1)
			{
				where = Position;
			}
			if (Handle->Position > where)
			{
				Handle->Position = where;
			}
			if (Handle->Length > where)
			{
				Handle->Length = where;
			}
		}

		public global::Unity.Multiplayer.Tools.NetStats.BitWriter EnterBitwiseContext()
		{
			return new global::Unity.Multiplayer.Tools.NetStats.BitWriter(this);
		}

		internal unsafe void Grow(int additionalSizeRequired)
		{
			int num;
			for (num = Handle->Capacity * 2; num < Position + additionalSizeRequired; num *= 2)
			{
			}
			int num2 = global::System.Math.Min(num, Handle->MaxCapacity);
			byte* ptr = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(num2, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<byte>(), Handle->Allocator);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr, Handle->BufferPointer, Length);
			if (Handle->BufferGrew)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(Handle->BufferPointer, Handle->Allocator);
			}
			Handle->BufferGrew = true;
			Handle->BufferPointer = ptr;
			Handle->Capacity = num2;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe bool TryBeginWrite(int bytes)
		{
			if (Handle->Position + bytes > Handle->Capacity)
			{
				if (Handle->Position + bytes > Handle->MaxCapacity)
				{
					return false;
				}
				if (Handle->Capacity >= Handle->MaxCapacity)
				{
					return false;
				}
				Grow(bytes);
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe bool TryBeginWriteValue<T>(in T value) where T : unmanaged
		{
			int num = sizeof(T);
			if (Handle->Position + num > Handle->Capacity)
			{
				if (Handle->Position + num > Handle->MaxCapacity)
				{
					return false;
				}
				if (Handle->Capacity >= Handle->MaxCapacity)
				{
					return false;
				}
				Grow(num);
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe bool TryBeginWriteInternal(int bytes)
		{
			if (Handle->Position + bytes > Handle->Capacity)
			{
				if (Handle->Position + bytes > Handle->MaxCapacity)
				{
					return false;
				}
				if (Handle->Capacity >= Handle->MaxCapacity)
				{
					return false;
				}
				Grow(bytes);
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

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int GetWriteSize(string s, bool oneByteChars = false)
		{
			return 4 + s.Length * (oneByteChars ? 1 : 2);
		}

		public void WriteNetworkSerializable<T>(in T value) where T : global::Unity.Multiplayer.Tools.NetStats.INetworkSerializable
		{
			global::Unity.Multiplayer.Tools.NetStats.BufferSerializer<global::Unity.Multiplayer.Tools.NetStats.BufferSerializerWriter> serializer = new global::Unity.Multiplayer.Tools.NetStats.BufferSerializer<global::Unity.Multiplayer.Tools.NetStats.BufferSerializerWriter>(new global::Unity.Multiplayer.Tools.NetStats.BufferSerializerWriter(this));
			value.NetworkSerialize(serializer);
		}

		public void WriteNetworkSerializable<T>(global::Unity.Multiplayer.Tools.NetStats.INetworkSerializable[] array, int count = -1, int offset = 0) where T : global::Unity.Multiplayer.Tools.NetStats.INetworkSerializable
		{
			WriteValueSafe<int>((count != -1) ? count : (array.Length - offset));
			for (int i = 0; i < array.Length; i++)
			{
				global::Unity.Multiplayer.Tools.NetStats.INetworkSerializable value = array[i];
				WriteNetworkSerializable(in value);
			}
		}

		public unsafe void WriteValue(string s, bool oneByteChars = false)
		{
			WriteValue<uint>((uint)s.Length);
			int length = s.Length;
			if (oneByteChars)
			{
				for (int i = 0; i < length; i++)
				{
					WriteByte((byte)s[i]);
				}
			}
			else
			{
				fixed (char* value = s)
				{
					WriteBytes((byte*)value, length * 2);
				}
			}
		}

		public unsafe void WriteValueSafe(string s, bool oneByteChars = false)
		{
			int writeSize = GetWriteSize(s, oneByteChars);
			if (!TryBeginWriteInternal(writeSize))
			{
				throw new global::System.OverflowException("Writing past the end of the buffer");
			}
			WriteValue<uint>((uint)s.Length);
			int length = s.Length;
			if (oneByteChars)
			{
				for (int i = 0; i < length; i++)
				{
					WriteByte((byte)s[i]);
				}
			}
			else
			{
				fixed (char* value = s)
				{
					WriteBytes((byte*)value, length * 2);
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetWriteSize<T>(T[] array, int count = -1, int offset = 0) where T : unmanaged
		{
			int num = ((count != -1) ? count : (array.Length - offset)) * sizeof(T);
			return 4 + num;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteValue<T>(T[] array, int count = -1, int offset = 0) where T : unmanaged
		{
			int value = ((count != -1) ? count : (array.Length - offset));
			int size = value * sizeof(T);
			WriteValue(in value);
			fixed (T* ptr = array)
			{
				byte* value2 = (byte*)ptr + (nint)offset * (nint)sizeof(T);
				WriteBytes(value2, size);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteValueSafe<T>(T[] array, int count = -1, int offset = 0) where T : unmanaged
		{
			int value = ((count != -1) ? count : (array.Length - offset));
			int num = value * sizeof(T);
			if (!TryBeginWriteInternal(num + 4))
			{
				throw new global::System.OverflowException("Writing past the end of the buffer");
			}
			WriteValue(in value);
			fixed (T* ptr = array)
			{
				byte* value2 = (byte*)ptr + (nint)offset * (nint)sizeof(T);
				WriteBytes(value2, num);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WritePartialValue<T>(T value, int bytesToWrite, int offsetBytes = 0) where T : unmanaged
		{
			byte* source = (byte*)(&value) + offsetBytes;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Handle->BufferPointer + Handle->Position, source, bytesToWrite);
			Handle->Position += bytesToWrite;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteByte(byte value)
		{
			Handle->BufferPointer[Handle->Position++] = value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteByteSafe(byte value)
		{
			if (!TryBeginWriteInternal(1))
			{
				throw new global::System.OverflowException("Writing past the end of the buffer");
			}
			Handle->BufferPointer[Handle->Position++] = value;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteBytes(byte* value, int size, int offset = 0)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Handle->BufferPointer + Handle->Position, value + offset, size);
			Handle->Position += size;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteBytesSafe(byte* value, int size, int offset = 0)
		{
			if (!TryBeginWriteInternal(size))
			{
				throw new global::System.OverflowException("Writing past the end of the buffer");
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Handle->BufferPointer + Handle->Position, value + offset, size);
			Handle->Position += size;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteBytes(byte[] value, int size = -1, int offset = 0)
		{
			fixed (byte* value2 = value)
			{
				WriteBytes(value2, (size == -1) ? value.Length : size, offset);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteBytesSafe(byte[] value, int size = -1, int offset = 0)
		{
			fixed (byte* value2 = value)
			{
				WriteBytesSafe(value2, (size == -1) ? value.Length : size, offset);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void CopyTo(global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter other)
		{
			other.WriteBytes(Handle->BufferPointer, Handle->Position);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void CopyFrom(global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter other)
		{
			WriteBytes(other.Handle->BufferPointer, other.Handle->Position);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetWriteSize<T>(in T value) where T : unmanaged
		{
			return sizeof(T);
		}

		public unsafe static int GetWriteSize<T>() where T : unmanaged
		{
			return sizeof(T);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteValue<T>(in T value) where T : unmanaged
		{
			int num = sizeof(T);
			fixed (T* source = &value)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Handle->BufferPointer + Handle->Position, source, num);
			}
			Handle->Position += num;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteValueSafe<T>(in T value) where T : unmanaged
		{
			int num = sizeof(T);
			if (!TryBeginWriteInternal(num))
			{
				throw new global::System.OverflowException("Writing past the end of the buffer");
			}
			fixed (T* source = &value)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(Handle->BufferPointer + Handle->Position, source, num);
			}
			Handle->Position += num;
		}

		public unsafe global::Unity.Collections.NativeArray<byte> ToNativeArray(global::Unity.Collections.Allocator allocator)
		{
			global::Unity.Collections.NativeArray<byte> nativeArray = new global::Unity.Collections.NativeArray<byte>(Length, allocator, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray), GetUnsafePtr(), nativeArray.Length);
			return nativeArray;
		}
	}
}
