namespace Unity.Collections
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "Unity.Networking.Transport", "Unity.Networking.Transport", null)]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public struct DataStreamWriter
	{
		private struct StreamData
		{
			public unsafe byte* buffer;

			public int length;

			public int capacity;

			public ulong bitBuffer;

			public int bitIndex;

			public int failedWrites;
		}

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		private global::Unity.Collections.DataStreamWriter.StreamData m_Data;

		public global::System.IntPtr m_SendHandleData;

		public unsafe static bool IsLittleEndian
		{
			get
			{
				uint num = 1u;
				byte* ptr = (byte*)(&num);
				return *ptr == 1;
			}
		}

		public unsafe readonly bool IsCreated
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Data.buffer != null;
			}
		}

		public readonly bool HasFailedWrites
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Data.failedWrites > 0;
			}
		}

		public readonly int Capacity
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Data.capacity;
			}
		}

		public int Length
		{
			get
			{
				SyncBitData();
				return m_Data.length + (m_Data.bitIndex + 7 >> 3);
			}
		}

		public int LengthInBits
		{
			get
			{
				SyncBitData();
				return m_Data.length * 8 + m_Data.bitIndex;
			}
		}

		public DataStreamWriter(int length, global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			Initialize(out this, global::Unity.Collections.CollectionHelper.CreateNativeArray<byte>(length, allocator));
		}

		public DataStreamWriter(global::Unity.Collections.NativeArray<byte> data)
		{
			Initialize(out this, data);
		}

		public unsafe DataStreamWriter(byte* data, int length)
		{
			global::Unity.Collections.NativeArray<byte> data2 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(data, length, global::Unity.Collections.Allocator.Invalid);
			Initialize(out this, data2);
		}

		public unsafe global::Unity.Collections.NativeArray<byte> AsNativeArray()
		{
			return global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(m_Data.buffer, Length, global::Unity.Collections.Allocator.Invalid);
		}

		private unsafe static void Initialize(out global::Unity.Collections.DataStreamWriter self, global::Unity.Collections.NativeArray<byte> data)
		{
			self.m_SendHandleData = global::System.IntPtr.Zero;
			self.m_Data.capacity = data.Length;
			self.m_Data.length = 0;
			self.m_Data.buffer = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(data);
			self.m_Data.bitBuffer = 0uL;
			self.m_Data.bitIndex = 0;
			self.m_Data.failedWrites = 0;
		}

		private static short ByteSwap(short val)
		{
			return (short)(((val & 0xFF) << 8) | ((val >> 8) & 0xFF));
		}

		private static int ByteSwap(int val)
		{
			return ((val & 0xFF) << 24) | ((val & 0xFF00) << 8) | ((val >> 8) & 0xFF00) | ((val >> 24) & 0xFF);
		}

		private unsafe void SyncBitData()
		{
			int num = m_Data.bitIndex;
			if (num > 0)
			{
				ulong num2 = m_Data.bitBuffer;
				int num3 = 0;
				while (num > 0)
				{
					m_Data.buffer[m_Data.length + num3] = (byte)num2;
					num -= 8;
					num2 >>= 8;
					num3++;
				}
			}
		}

		public unsafe void Flush()
		{
			while (m_Data.bitIndex > 0)
			{
				m_Data.buffer[m_Data.length++] = (byte)m_Data.bitBuffer;
				m_Data.bitIndex -= 8;
				m_Data.bitBuffer >>= 8;
			}
			m_Data.bitIndex = 0;
		}

		private unsafe bool WriteBytesInternal(byte* data, int bytes)
		{
			if (m_Data.length + (m_Data.bitIndex + 7 >> 3) + bytes > m_Data.capacity)
			{
				m_Data.failedWrites++;
				return false;
			}
			Flush();
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(m_Data.buffer + m_Data.length, data, bytes);
			m_Data.length += bytes;
			return true;
		}

		public unsafe bool WriteByte(byte value)
		{
			return WriteBytesInternal(&value, 1);
		}

		public unsafe bool WriteBytes(global::Unity.Collections.NativeArray<byte> value)
		{
			return WriteBytesInternal((byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(value), value.Length);
		}

		public unsafe bool WriteBytes(global::System.Span<byte> value)
		{
			fixed (byte* data = value)
			{
				return WriteBytesInternal(data, value.Length);
			}
		}

		public unsafe bool WriteShort(short value)
		{
			return WriteBytesInternal((byte*)(&value), 2);
		}

		public unsafe bool WriteUShort(ushort value)
		{
			return WriteBytesInternal((byte*)(&value), 2);
		}

		public unsafe bool WriteInt(int value)
		{
			return WriteBytesInternal((byte*)(&value), 4);
		}

		public unsafe bool WriteUInt(uint value)
		{
			return WriteBytesInternal((byte*)(&value), 4);
		}

		public unsafe bool WriteLong(long value)
		{
			return WriteBytesInternal((byte*)(&value), 8);
		}

		public unsafe bool WriteULong(ulong value)
		{
			return WriteBytesInternal((byte*)(&value), 8);
		}

		public unsafe bool WriteShortNetworkByteOrder(short value)
		{
			short num = (IsLittleEndian ? ByteSwap(value) : value);
			return WriteBytesInternal((byte*)(&num), 2);
		}

		public bool WriteUShortNetworkByteOrder(ushort value)
		{
			return WriteShortNetworkByteOrder((short)value);
		}

		public unsafe bool WriteIntNetworkByteOrder(int value)
		{
			int num = (IsLittleEndian ? ByteSwap(value) : value);
			return WriteBytesInternal((byte*)(&num), 4);
		}

		public bool WriteUIntNetworkByteOrder(uint value)
		{
			return WriteIntNetworkByteOrder((int)value);
		}

		public bool WriteFloat(float value)
		{
			global::Unity.Collections.UIntFloat uIntFloat = new global::Unity.Collections.UIntFloat
			{
				floatValue = value
			};
			return WriteInt((int)uIntFloat.intValue);
		}

		public bool WriteDouble(double value)
		{
			global::Unity.Collections.UIntFloat uIntFloat = new global::Unity.Collections.UIntFloat
			{
				doubleValue = value
			};
			return WriteLong((long)uIntFloat.longValue);
		}

		private unsafe void FlushBits()
		{
			while (m_Data.bitIndex >= 8)
			{
				m_Data.buffer[m_Data.length++] = (byte)m_Data.bitBuffer;
				m_Data.bitIndex -= 8;
				m_Data.bitBuffer >>= 8;
			}
		}

		private void WriteRawBitsInternal(uint value, int numbits)
		{
			m_Data.bitBuffer |= (ulong)value << m_Data.bitIndex;
			m_Data.bitIndex += numbits;
		}

		public bool WriteRawBits(uint value, int numbits)
		{
			if (m_Data.length + (m_Data.bitIndex + numbits + 7 >> 3) > m_Data.capacity)
			{
				m_Data.failedWrites++;
				return false;
			}
			WriteRawBitsInternal(value, numbits);
			FlushBits();
			return true;
		}

		public unsafe bool WritePackedUInt(uint value, in global::Unity.Collections.StreamCompressionModel model)
		{
			int num = model.CalculateBucket(value);
			uint num2 = model.bucketOffsets[num];
			int num3 = model.bucketSizes[num];
			ushort num4 = model.encodeTable[num];
			if (m_Data.length + (m_Data.bitIndex + (num4 & 0xFF) + num3 + 7 >> 3) > m_Data.capacity)
			{
				m_Data.failedWrites++;
				return false;
			}
			WriteRawBitsInternal((uint)(num4 >> 8), num4 & 0xFF);
			WriteRawBitsInternal(value - num2, num3);
			FlushBits();
			return true;
		}

		public unsafe bool WritePackedULong(ulong value, in global::Unity.Collections.StreamCompressionModel model)
		{
			uint* ptr = (uint*)(&value);
			return WritePackedUInt(*ptr, in model) & WritePackedUInt(ptr[1], in model);
		}

		public bool WritePackedInt(int value, in global::Unity.Collections.StreamCompressionModel model)
		{
			uint value2 = (uint)((value >> 31) ^ (value << 1));
			return WritePackedUInt(value2, in model);
		}

		public bool WritePackedLong(long value, in global::Unity.Collections.StreamCompressionModel model)
		{
			ulong value2 = (ulong)((value >> 63) ^ (value << 1));
			return WritePackedULong(value2, in model);
		}

		public bool WritePackedFloat(float value, in global::Unity.Collections.StreamCompressionModel model)
		{
			return WritePackedFloatDelta(value, 0f, in model);
		}

		public bool WritePackedDouble(double value, in global::Unity.Collections.StreamCompressionModel model)
		{
			return WritePackedDoubleDelta(value, 0.0, in model);
		}

		public bool WritePackedUIntDelta(uint value, uint baseline, in global::Unity.Collections.StreamCompressionModel model)
		{
			int value2 = (int)(baseline - value);
			return WritePackedInt(value2, in model);
		}

		public bool WritePackedIntDelta(int value, int baseline, in global::Unity.Collections.StreamCompressionModel model)
		{
			int value2 = baseline - value;
			return WritePackedInt(value2, in model);
		}

		public bool WritePackedLongDelta(long value, long baseline, in global::Unity.Collections.StreamCompressionModel model)
		{
			long value2 = baseline - value;
			return WritePackedLong(value2, in model);
		}

		public bool WritePackedULongDelta(ulong value, ulong baseline, in global::Unity.Collections.StreamCompressionModel model)
		{
			long value2 = (long)(baseline - value);
			return WritePackedLong(value2, in model);
		}

		public bool WritePackedFloatDelta(float value, float baseline, in global::Unity.Collections.StreamCompressionModel model)
		{
			int num = 0;
			if (value != baseline)
			{
				num = 32;
			}
			if (m_Data.length + (m_Data.bitIndex + 1 + num + 7 >> 3) > m_Data.capacity)
			{
				m_Data.failedWrites++;
				return false;
			}
			if (num == 0)
			{
				WriteRawBitsInternal(0u, 1);
			}
			else
			{
				WriteRawBitsInternal(1u, 1);
				global::Unity.Collections.UIntFloat uIntFloat = new global::Unity.Collections.UIntFloat
				{
					floatValue = value
				};
				WriteRawBitsInternal(uIntFloat.intValue, num);
			}
			FlushBits();
			return true;
		}

		public unsafe bool WritePackedDoubleDelta(double value, double baseline, in global::Unity.Collections.StreamCompressionModel model)
		{
			int num = 0;
			if (value != baseline)
			{
				num = 64;
			}
			if (m_Data.length + (m_Data.bitIndex + 1 + num + 7 >> 3) > m_Data.capacity)
			{
				m_Data.failedWrites++;
				return false;
			}
			if (num == 0)
			{
				WriteRawBitsInternal(0u, 1);
			}
			else
			{
				WriteRawBitsInternal(1u, 1);
				global::Unity.Collections.UIntFloat uIntFloat = new global::Unity.Collections.UIntFloat
				{
					doubleValue = value
				};
				uint* ptr = (uint*)(&uIntFloat.longValue);
				WriteRawBitsInternal(*ptr, 32);
				FlushBits();
				WriteRawBitsInternal(ptr[1], 32);
			}
			FlushBits();
			return true;
		}

		public unsafe bool WriteFixedString32(global::Unity.Collections.FixedString32Bytes str)
		{
			int bytes = *(ushort*)(&str) + 2;
			byte* data = (byte*)(&str);
			return WriteBytesInternal(data, bytes);
		}

		public unsafe bool WriteFixedString64(global::Unity.Collections.FixedString64Bytes str)
		{
			int bytes = *(ushort*)(&str) + 2;
			byte* data = (byte*)(&str);
			return WriteBytesInternal(data, bytes);
		}

		public unsafe bool WriteFixedString128(global::Unity.Collections.FixedString128Bytes str)
		{
			int bytes = *(ushort*)(&str) + 2;
			byte* data = (byte*)(&str);
			return WriteBytesInternal(data, bytes);
		}

		public unsafe bool WriteFixedString512(global::Unity.Collections.FixedString512Bytes str)
		{
			int bytes = *(ushort*)(&str) + 2;
			byte* data = (byte*)(&str);
			return WriteBytesInternal(data, bytes);
		}

		public unsafe bool WriteFixedString4096(global::Unity.Collections.FixedString4096Bytes str)
		{
			int bytes = *(ushort*)(&str) + 2;
			byte* data = (byte*)(&str);
			return WriteBytesInternal(data, bytes);
		}

		public unsafe bool WritePackedFixedString32Delta(global::Unity.Collections.FixedString32Bytes str, global::Unity.Collections.FixedString32Bytes baseline, in global::Unity.Collections.StreamCompressionModel model)
		{
			ushort length = *(ushort*)(&str);
			byte* data = (byte*)(&str) + 2;
			return WritePackedFixedStringDelta(data, length, (byte*)(&baseline) + 2, *(ushort*)(&baseline), in model);
		}

		public unsafe bool WritePackedFixedString64Delta(global::Unity.Collections.FixedString64Bytes str, global::Unity.Collections.FixedString64Bytes baseline, in global::Unity.Collections.StreamCompressionModel model)
		{
			ushort length = *(ushort*)(&str);
			byte* data = (byte*)(&str) + 2;
			return WritePackedFixedStringDelta(data, length, (byte*)(&baseline) + 2, *(ushort*)(&baseline), in model);
		}

		public unsafe bool WritePackedFixedString128Delta(global::Unity.Collections.FixedString128Bytes str, global::Unity.Collections.FixedString128Bytes baseline, in global::Unity.Collections.StreamCompressionModel model)
		{
			ushort length = *(ushort*)(&str);
			byte* data = (byte*)(&str) + 2;
			return WritePackedFixedStringDelta(data, length, (byte*)(&baseline) + 2, *(ushort*)(&baseline), in model);
		}

		public unsafe bool WritePackedFixedString512Delta(global::Unity.Collections.FixedString512Bytes str, global::Unity.Collections.FixedString512Bytes baseline, in global::Unity.Collections.StreamCompressionModel model)
		{
			ushort length = *(ushort*)(&str);
			byte* data = (byte*)(&str) + 2;
			return WritePackedFixedStringDelta(data, length, (byte*)(&baseline) + 2, *(ushort*)(&baseline), in model);
		}

		public unsafe bool WritePackedFixedString4096Delta(global::Unity.Collections.FixedString4096Bytes str, global::Unity.Collections.FixedString4096Bytes baseline, in global::Unity.Collections.StreamCompressionModel model)
		{
			ushort length = *(ushort*)(&str);
			byte* data = (byte*)(&str) + 2;
			return WritePackedFixedStringDelta(data, length, (byte*)(&baseline) + 2, *(ushort*)(&baseline), in model);
		}

		private unsafe bool WritePackedFixedStringDelta(byte* data, uint length, byte* baseData, uint baseLength, in global::Unity.Collections.StreamCompressionModel model)
		{
			global::Unity.Collections.DataStreamWriter.StreamData data2 = m_Data;
			if (!WritePackedUIntDelta(length, baseLength, in model))
			{
				return false;
			}
			bool flag = false;
			if (length <= baseLength)
			{
				for (uint num = 0u; num < length; num++)
				{
					flag |= !WritePackedUIntDelta(data[num], baseData[num], in model);
				}
			}
			else
			{
				for (uint num2 = 0u; num2 < baseLength; num2++)
				{
					flag |= !WritePackedUIntDelta(data[num2], baseData[num2], in model);
				}
				for (uint num3 = baseLength; num3 < length; num3++)
				{
					flag |= !WritePackedUInt(data[num3], in model);
				}
			}
			if (flag)
			{
				m_Data = data2;
				m_Data.failedWrites++;
			}
			return !flag;
		}

		public void Clear()
		{
			m_Data.length = 0;
			m_Data.bitIndex = 0;
			m_Data.bitBuffer = 0uL;
			m_Data.failedWrites = 0;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private readonly void CheckRead()
		{
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckAllocator(global::Unity.Collections.AllocatorManager.AllocatorHandle allocator)
		{
			if (allocator.ToAllocator != global::Unity.Collections.Allocator.Temp)
			{
				throw new global::System.InvalidOperationException("DataStreamWriters can only be created with temp memory");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckBits(uint value, int numBits)
		{
			if (numBits < 0 || numBits > 32)
			{
				throw new global::System.ArgumentOutOfRangeException($"Invalid number of bits specified: {numBits}! Valid range is (0, 32) inclusive.");
			}
			ulong num = (ulong)(1L << numBits);
			if (value >= num)
			{
				throw new global::System.ArgumentOutOfRangeException($"Value {value} does not fit in the specified number of bits: {numBits}! Range (inclusive) is (0, {num - 1})!");
			}
		}
	}
}
