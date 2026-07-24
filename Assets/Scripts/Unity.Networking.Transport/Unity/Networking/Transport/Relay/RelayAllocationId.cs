namespace Unity.Networking.Transport.Relay
{
	public struct RelayAllocationId : global::System.IEquatable<global::Unity.Networking.Transport.Relay.RelayAllocationId>, global::System.IComparable<global::Unity.Networking.Transport.Relay.RelayAllocationId>
	{
		public const int k_Length = 16;

		public unsafe fixed byte Value[16];

		public unsafe static global::Unity.Networking.Transport.Relay.RelayAllocationId FromBytePointer(byte* dataPtr, int length)
		{
			if (length != 16)
			{
				global::UnityEngine.Debug.LogError($"Provided byte array length is invalid, must be {16} but got {length}.");
				return default(global::Unity.Networking.Transport.Relay.RelayAllocationId);
			}
			global::Unity.Networking.Transport.Relay.RelayAllocationId result = default(global::Unity.Networking.Transport.Relay.RelayAllocationId);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(result.Value, dataPtr, 16L);
			return result;
		}

		public unsafe static global::Unity.Networking.Transport.Relay.RelayAllocationId FromByteArray(byte[] data)
		{
			fixed (byte* dataPtr = data)
			{
				return FromBytePointer(dataPtr, data.Length);
			}
		}

		internal unsafe global::Unity.Networking.Transport.NetworkEndpoint ToNetworkEndpoint()
		{
			global::Unity.Networking.Transport.NetworkEndpoint result = default(global::Unity.Networking.Transport.NetworkEndpoint);
			*(global::Unity.Networking.Transport.Relay.RelayAllocationId*)(&result) = this;
			return result;
		}

		public static bool operator ==(global::Unity.Networking.Transport.Relay.RelayAllocationId lhs, global::Unity.Networking.Transport.Relay.RelayAllocationId rhs)
		{
			return lhs.Compare(rhs) == 0;
		}

		public static bool operator !=(global::Unity.Networking.Transport.Relay.RelayAllocationId lhs, global::Unity.Networking.Transport.Relay.RelayAllocationId rhs)
		{
			return lhs.Compare(rhs) != 0;
		}

		public bool Equals(global::Unity.Networking.Transport.Relay.RelayAllocationId other)
		{
			return Compare(other) == 0;
		}

		public int CompareTo(global::Unity.Networking.Transport.Relay.RelayAllocationId other)
		{
			return Compare(other);
		}

		public override bool Equals(object other)
		{
			if (other != null)
			{
				return this == (global::Unity.Networking.Transport.Relay.RelayAllocationId)other;
			}
			return false;
		}

		public unsafe override int GetHashCode()
		{
			fixed (byte* value = Value)
			{
				int num = 0;
				for (int i = 0; i < 16; i++)
				{
					num = (num * 31) ^ value[i];
				}
				return num;
			}
		}

		private unsafe int Compare(global::Unity.Networking.Transport.Relay.RelayAllocationId other)
		{
			fixed (byte* value = Value)
			{
				void* ptr = value;
				return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(ptr, other.Value, 16L);
			}
		}
	}
}
