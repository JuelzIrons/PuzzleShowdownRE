namespace Unity.Networking.Transport.Utilities
{
	internal struct ReliableAckMask : global::System.IEquatable<global::Unity.Networking.Transport.Utilities.ReliableAckMask>
	{
		public const int AcksPerByte = 8;

		private const int k_ByteCount = 255;

		private const int k_BitCount = 2040;

		private const int k_ChunkCount = 32;

		private unsafe fixed ulong m_Chunks[32];

		public static readonly global::Unity.Networking.Transport.Utilities.ReliableAckMask AllAcked;

		unsafe static ReliableAckMask()
		{
			for (int i = 0; i < 32; i++)
			{
				AllAcked.m_Chunks[i] = ulong.MaxValue;
			}
		}

		public unsafe static global::Unity.Networking.Transport.Utilities.ReliableAckMask FromBytes(global::System.ReadOnlySpan<byte> bytes)
		{
			global::Unity.Networking.Transport.Utilities.ReliableAckMask allAcked = AllAcked;
			int num = global::Unity.Mathematics.math.min(bytes.Length, 255);
			fixed (byte* source = bytes)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(allAcked.m_Chunks, source, num);
			}
			return allAcked;
		}

		public unsafe void Ack(int distance)
		{
			int num = distance / 64;
			int num2 = distance % 64;
			ref ulong reference = ref m_Chunks[num];
			reference |= (ulong)(1L << num2);
		}

		public unsafe bool IsAcked(int distance)
		{
			int num = distance / 64;
			int num2 = distance % 64;
			return (m_Chunks[num] & (ulong)(1L << num2)) != 0;
		}

		public unsafe void Shift(int count)
		{
			while (count >= 64)
			{
				for (int num = 31; num > 0; num--)
				{
					m_Chunks[num] = m_Chunks[num - 1];
				}
				m_Chunks[0] = 0uL;
				count -= 64;
			}
			if (count != 0)
			{
				for (int num2 = 31; num2 > 0; num2--)
				{
					ulong num3 = m_Chunks[num2] << count;
					ulong num4 = m_Chunks[num2 - 1] >> 64 - count;
					m_Chunks[num2] = num3 | num4;
				}
				m_Chunks[0] <<= count;
			}
		}

		public unsafe void Merge(global::Unity.Networking.Transport.Utilities.ReliableAckMask other)
		{
			for (int i = 0; i < 32; i++)
			{
				ref ulong reference = ref m_Chunks[i];
				reference |= other.m_Chunks[i];
			}
		}

		public unsafe void WriteTo(ref global::Unity.Collections.DataStreamWriter writer, int numBytes)
		{
			fixed (ulong* chunks = m_Chunks)
			{
				global::Unity.Collections.LowLevel.Unsafe.DataStreamExtensions.WriteBytesUnsafe(ref writer, (byte*)chunks, numBytes);
			}
		}

		public unsafe int MinimumWriteSize()
		{
			ref ulong reference = ref m_Chunks[31];
			reference |= 0xFF00000000000000uL;
			for (int num = 31; num >= 0; num--)
			{
				if (m_Chunks[num] != ulong.MaxValue)
				{
					int num2 = 8 - global::Unity.Mathematics.math.lzcnt(~m_Chunks[num]) / 8;
					return num * 8 + num2;
				}
			}
			return 1;
		}

		public unsafe bool Equals(global::Unity.Networking.Transport.Utilities.ReliableAckMask other)
		{
			fixed (ulong* chunks = m_Chunks)
			{
				return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(chunks, other.m_Chunks, 255L) == 0;
			}
		}

		public unsafe override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < 31; i++)
			{
				num ^= m_Chunks[i].GetHashCode();
			}
			return num ^ (m_Chunks[31] & 0xFFFFFFFFFFFFFFL).GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				return Equals((global::Unity.Networking.Transport.Utilities.ReliableAckMask)obj);
			}
			return false;
		}

		public static bool operator ==(global::Unity.Networking.Transport.Utilities.ReliableAckMask left, global::Unity.Networking.Transport.Utilities.ReliableAckMask right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::Unity.Networking.Transport.Utilities.ReliableAckMask left, global::Unity.Networking.Transport.Utilities.ReliableAckMask right)
		{
			return !left.Equals(right);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckBytes(global::System.ReadOnlySpan<byte> bytes)
		{
			if (bytes.Length > 255)
			{
				throw new global::System.ArgumentException("Byte array is too large to read a mask from.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckDistance(int distance)
		{
			if (distance < 0 || distance >= 2040)
			{
				throw new global::System.ArgumentOutOfRangeException("Distance must be in the range [0, 63].");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckShiftCount(int count)
		{
			if (count < 0 || count > 2040)
			{
				throw new global::System.ArgumentOutOfRangeException("Shift count must be in the range [0, 64].");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckWriteToCount(int count)
		{
			if (count < 0 || count > 255)
			{
				throw new global::System.ArgumentOutOfRangeException("Invalid write length for reliable mask.");
			}
		}
	}
}
