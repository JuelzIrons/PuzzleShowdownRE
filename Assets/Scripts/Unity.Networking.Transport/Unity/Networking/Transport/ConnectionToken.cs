namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
	internal struct ConnectionToken : global::System.IEquatable<global::Unity.Networking.Transport.ConnectionToken>, global::System.IComparable<global::Unity.Networking.Transport.ConnectionToken>
	{
		public const int k_Length = 8;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public unsafe fixed byte Value[8];

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private long m_ValueLongWorkaround;

		public static bool operator ==(global::Unity.Networking.Transport.ConnectionToken lhs, global::Unity.Networking.Transport.ConnectionToken rhs)
		{
			return lhs.Compare(rhs) == 0;
		}

		public static bool operator !=(global::Unity.Networking.Transport.ConnectionToken lhs, global::Unity.Networking.Transport.ConnectionToken rhs)
		{
			return lhs.Compare(rhs) != 0;
		}

		public bool Equals(global::Unity.Networking.Transport.ConnectionToken other)
		{
			return Compare(other) == 0;
		}

		public int CompareTo(global::Unity.Networking.Transport.ConnectionToken other)
		{
			return Compare(other);
		}

		public override bool Equals(object other)
		{
			if (other != null)
			{
				return this == (global::Unity.Networking.Transport.ConnectionToken)other;
			}
			return false;
		}

		public unsafe override int GetHashCode()
		{
			fixed (byte* value = Value)
			{
				int num = 0;
				for (int i = 0; i < 8; i++)
				{
					num = (num * 31) ^ value[i];
				}
				return num;
			}
		}

		public unsafe override string ToString()
		{
			fixed (byte* value = Value)
			{
				return $"0x{*(long*)value:x16}";
			}
		}

		private unsafe int Compare(global::Unity.Networking.Transport.ConnectionToken other)
		{
			fixed (byte* value = Value)
			{
				void* ptr = value;
				return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr, other.Value, 8L);
			}
		}
	}
}
