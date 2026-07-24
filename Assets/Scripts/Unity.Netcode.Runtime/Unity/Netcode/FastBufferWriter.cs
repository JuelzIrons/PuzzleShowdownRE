namespace Unity.Netcode
{
	public struct FastBufferWriter : global::System.IDisposable
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

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public struct ForPrimitives
		{
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public struct ForEnums
		{
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public struct ForStructs
		{
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public struct ForNetworkSerializable
		{
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public struct ForFixedStrings
		{
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		public struct ForGeneric
		{
		}

		internal unsafe global::Unity.Netcode.FastBufferWriter.WriterHandle* Handle;

		private static byte[] s_ByteArrayCache = new byte[65535];

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

		public unsafe bool IsInitialized => Handle != null;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void CommitBitwiseWrites(int amount)
		{
			Handle->Position += amount;
		}

		public unsafe FastBufferWriter(int size, global::Unity.Collections.Allocator allocator, int maxSize = -1)
		{
			Handle = (global::Unity.Netcode.FastBufferWriter.WriterHandle*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(sizeof(global::Unity.Netcode.FastBufferWriter.WriterHandle) + size, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<global::Unity.Netcode.FastBufferWriter.WriterHandle>(), allocator);
			Handle->BufferPointer = (byte*)Handle + sizeof(global::Unity.Netcode.FastBufferWriter.WriterHandle);
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
			Handle = null;
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

		public global::Unity.Netcode.BitWriter EnterBitwiseContext()
		{
			return new global::Unity.Netcode.BitWriter(this);
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

		internal unsafe global::System.ArraySegment<byte> ToTempByteArray()
		{
			int length = Length;
			if (length > s_ByteArrayCache.Length)
			{
				return new global::System.ArraySegment<byte>(ToArray(), 0, length);
			}
			fixed (byte* destination = s_ByteArrayCache)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, Handle->BufferPointer, length);
			}
			return new global::System.ArraySegment<byte>(s_ByteArrayCache, 0, length);
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
			return SizeOfLengthField() + s.Length * (oneByteChars ? 1 : 2);
		}

		public void WriteNetworkSerializable<T>(in T value) where T : global::Unity.Netcode.INetworkSerializable
		{
			global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerWriter> serializer = new global::Unity.Netcode.BufferSerializer<global::Unity.Netcode.BufferSerializerWriter>(new global::Unity.Netcode.BufferSerializerWriter(this));
			value.NetworkSerialize(serializer);
		}

		public void WriteNetworkSerializable<T>(T[] array, int count = -1, int offset = 0) where T : global::Unity.Netcode.INetworkSerializable
		{
			int length = ((count != -1) ? count : (array.Length - offset));
			WriteLengthSafe(length);
			for (int i = 0; i < array.Length; i++)
			{
				T value = array[i];
				WriteNetworkSerializable(in value);
			}
		}

		public void WriteNetworkSerializable<T>(global::Unity.Collections.NativeArray<T> array, int count = -1, int offset = 0) where T : unmanaged, global::Unity.Netcode.INetworkSerializable
		{
			int length = ((count != -1) ? count : (array.Length - offset));
			WriteLengthSafe(length);
			foreach (T item in array)
			{
				WriteNetworkSerializable<T>(item);
			}
		}

		public unsafe void WriteValue(string s, bool oneByteChars = false)
		{
			WriteLength((uint)s.Length);
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
			WriteLength((uint)s.Length);
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
			return SizeOfLengthField() + num;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetWriteSize<T>(global::Unity.Collections.NativeArray<T> array, int count = -1, int offset = 0) where T : unmanaged
		{
			int num = ((count != -1) ? count : (array.Length - offset)) * sizeof(T);
			return SizeOfLengthField() + num;
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
				throw new global::System.OverflowException($"Writing past the end of the buffer, size is {size} bytes but remaining capacity is {Handle->Capacity - Handle->Position} bytes");
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
		public unsafe void WriteBytes(global::Unity.Collections.NativeArray<byte> value, int size = -1, int offset = 0)
		{
			byte* unsafePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(value);
			WriteBytes(unsafePtr, (size == -1) ? value.Length : size, offset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteBytes(global::Unity.Collections.NativeList<byte> value, int size = -1, int offset = 0)
		{
			byte* unsafePtr = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(value);
			WriteBytes(unsafePtr, (size == -1) ? value.Length : size, offset);
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
		public unsafe void WriteBytesSafe(global::Unity.Collections.NativeArray<byte> value, int size = -1, int offset = 0)
		{
			byte* unsafePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(value);
			WriteBytesSafe(unsafePtr, (size == -1) ? value.Length : size, offset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteBytesSafe(global::Unity.Collections.NativeList<byte> value, int size = -1, int offset = 0)
		{
			byte* unsafePtr = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(value);
			WriteBytesSafe(unsafePtr, (size == -1) ? value.Length : size, offset);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void CopyTo(global::Unity.Netcode.FastBufferWriter other)
		{
			other.WriteBytes(Handle->BufferPointer, Handle->Position);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void CopyFrom(global::Unity.Netcode.FastBufferWriter other)
		{
			WriteBytes(other.Handle->BufferPointer, other.Handle->Position);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetWriteSize<T>(in T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged
		{
			return sizeof(T);
		}

		public static int GetWriteSize<T>(in T value) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			return SizeOfLengthField() + value.Length;
		}

		public static int GetWriteSize<T>(in global::Unity.Collections.NativeArray<T> value) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			int num = SizeOfLengthField();
			foreach (T item in value)
			{
				num += SizeOfLengthField() + item.Length;
			}
			return num;
		}

		public unsafe static int GetWriteSize<T>() where T : unmanaged
		{
			return sizeof(T);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void WriteUnmanaged<T>(in T value) where T : unmanaged
		{
			fixed (T* ptr = &value)
			{
				byte* value2 = (byte*)ptr;
				WriteBytes(value2, sizeof(T));
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void WriteUnmanagedSafe<T>(in T value) where T : unmanaged
		{
			fixed (T* ptr = &value)
			{
				byte* value2 = (byte*)ptr;
				WriteBytesSafe(value2, sizeof(T));
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static int SizeOfLengthField()
		{
			return 4;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void WriteLengthSafe(uint length)
		{
			WriteUnmanagedSafe(in length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void WriteLength(uint length)
		{
			WriteUnmanaged(in length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void WriteLengthSafe(int length)
		{
			if (length < 0)
			{
				throw new global::System.InvalidCastException("Cannot write negative length");
			}
			WriteLengthSafe((uint)length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void WriteLength(int length)
		{
			WriteLength((uint)length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void WriteUnmanaged<T>(T[] value) where T : unmanaged
		{
			WriteLength(value.Length);
			fixed (T* ptr = value)
			{
				byte* value2 = (byte*)ptr;
				WriteBytes(value2, sizeof(T) * value.Length);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void WriteUnmanagedSafe<T>(T[] value) where T : unmanaged
		{
			WriteLengthSafe(value.Length);
			fixed (T* ptr = value)
			{
				byte* value2 = (byte*)ptr;
				WriteBytesSafe(value2, sizeof(T) * value.Length);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void WriteUnmanaged<T>(global::Unity.Collections.NativeArray<T> value) where T : unmanaged
		{
			WriteLength(value.Length);
			T* unsafePtr = (T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(value);
			byte* value2 = (byte*)unsafePtr;
			WriteBytes(value2, sizeof(T) * value.Length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe void WriteUnmanagedSafe<T>(global::Unity.Collections.NativeArray<T> value) where T : unmanaged
		{
			WriteLengthSafe(value.Length);
			T* unsafePtr = (T*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(value);
			byte* value2 = (byte*)unsafePtr;
			WriteBytesSafe(value2, sizeof(T) * value.Length);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue<T>(in T value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable
		{
			WriteNetworkSerializable(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue<T>(T[] value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable
		{
			WriteNetworkSerializable(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(in T value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable
		{
			WriteNetworkSerializable(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(T[] value, global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable unused = default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable)) where T : global::Unity.Netcode.INetworkSerializable
		{
			WriteNetworkSerializable(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue<T>(in T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue<T>(T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue<T>(global::Unity.Collections.NativeArray<T> value, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			if (typeof(global::Unity.Netcode.INetworkSerializable).IsAssignableFrom(typeof(T)))
			{
				global::Unity.Netcode.NetworkVariableSerialization<global::Unity.Collections.NativeArray<T>>.Serializer.Write(this, ref value);
			}
			else
			{
				WriteUnmanaged(value);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(in T value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(T[] value, global::Unity.Netcode.FastBufferWriter.ForStructs unused = default(global::Unity.Netcode.FastBufferWriter.ForStructs)) where T : unmanaged, global::Unity.Netcode.INetworkSerializeByMemcpy
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(global::Unity.Collections.NativeArray<T> value, global::Unity.Netcode.FastBufferWriter.ForGeneric unused = default(global::Unity.Netcode.FastBufferWriter.ForGeneric)) where T : unmanaged
		{
			if (typeof(global::Unity.Netcode.INetworkSerializable).IsAssignableFrom(typeof(T)))
			{
				global::Unity.Netcode.NetworkVariableSerialization<global::Unity.Collections.NativeArray<T>>.Serializer.Write(this, ref value);
			}
			else
			{
				WriteUnmanagedSafe(value);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue<T>(in T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue<T>(T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(in T value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(T[] value, global::Unity.Netcode.FastBufferWriter.ForPrimitives unused = default(global::Unity.Netcode.FastBufferWriter.ForPrimitives)) where T : unmanaged, global::System.IComparable, global::System.IConvertible, global::System.IComparable<T>, global::System.IEquatable<T>
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue<T>(in T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue<T>(T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(in T value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(T[] value, global::Unity.Netcode.FastBufferWriter.ForEnums unused = default(global::Unity.Netcode.FastBufferWriter.ForEnums)) where T : unmanaged, global::System.Enum
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(in global::UnityEngine.Vector2 value)
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(global::UnityEngine.Vector2[] value)
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(in global::UnityEngine.Vector3 value)
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(global::UnityEngine.Vector3[] value)
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(in global::UnityEngine.Vector2Int value)
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(global::UnityEngine.Vector2Int[] value)
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(in global::UnityEngine.Vector3Int value)
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(global::UnityEngine.Vector3Int[] value)
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(in global::UnityEngine.Vector4 value)
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(global::UnityEngine.Vector4[] value)
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(in global::UnityEngine.Quaternion value)
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(global::UnityEngine.Quaternion[] value)
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(in global::UnityEngine.Pose value)
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(global::UnityEngine.Pose[] value)
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(in global::UnityEngine.Color value)
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(global::UnityEngine.Color[] value)
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(in global::UnityEngine.Color32 value)
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(global::UnityEngine.Color32[] value)
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(in global::UnityEngine.Ray value)
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(global::UnityEngine.Ray[] value)
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(in global::UnityEngine.Ray2D value)
		{
			WriteUnmanaged(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue(global::UnityEngine.Ray2D[] value)
		{
			WriteUnmanaged(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(in global::UnityEngine.Vector2 value)
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(global::UnityEngine.Vector2[] value)
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(in global::UnityEngine.Vector3 value)
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(global::UnityEngine.Vector3[] value)
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(in global::UnityEngine.Vector2Int value)
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(global::UnityEngine.Vector2Int[] value)
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(in global::UnityEngine.Vector3Int value)
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(global::UnityEngine.Vector3Int[] value)
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(in global::UnityEngine.Vector4 value)
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(global::UnityEngine.Vector4[] value)
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(in global::UnityEngine.Quaternion value)
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(global::UnityEngine.Quaternion[] value)
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(in global::UnityEngine.Pose value)
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(global::UnityEngine.Pose[] value)
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(in global::UnityEngine.Color value)
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(global::UnityEngine.Color[] value)
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(in global::UnityEngine.Color32 value)
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(global::UnityEngine.Color32[] value)
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(in global::UnityEngine.Ray value)
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(global::UnityEngine.Ray[] value)
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(in global::UnityEngine.Ray2D value)
		{
			WriteUnmanagedSafe(in value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe(global::UnityEngine.Ray2D[] value)
		{
			WriteUnmanagedSafe(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public unsafe void WriteValue<T>(in T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			WriteLength(value.Length);
			fixed (T* ptr = &value)
			{
				WriteBytes(ptr->GetUnsafePtr(), value.Length);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue<T>(T[] value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			WriteLength(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				T value2 = value[i];
				WriteValue(in value2, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValue<T>(in global::Unity.Collections.NativeArray<T> value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			WriteLength(value.Length);
			foreach (T item in value)
			{
				WriteValue<T>(item, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(in T value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if (!TryBeginWriteInternal(SizeOfLengthField() + value.Length))
			{
				throw new global::System.OverflowException("Writing past the end of the buffer");
			}
			WriteValue(in value, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(T[] value, global::Unity.Netcode.FastBufferWriter.ForFixedStrings unused = default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings)) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if (!TryBeginWriteInternal(GetWriteSize(value)))
			{
				throw new global::System.OverflowException("Writing past the end of the buffer");
			}
			WriteLength(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				T value2 = value[i];
				WriteValue(in value2, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void WriteValueSafe<T>(in global::Unity.Collections.NativeArray<T> value) where T : unmanaged, global::Unity.Collections.INativeList<byte>, global::Unity.Collections.IUTF8Bytes
		{
			if (!TryBeginWriteInternal(GetWriteSize(in value)))
			{
				throw new global::System.OverflowException("Writing past the end of the buffer");
			}
			WriteLength(value.Length);
			foreach (T item in value)
			{
				WriteValue<T>(item, default(global::Unity.Netcode.FastBufferWriter.ForFixedStrings));
			}
		}
	}
}
