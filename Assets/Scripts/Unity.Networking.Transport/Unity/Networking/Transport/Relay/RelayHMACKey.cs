namespace Unity.Networking.Transport.Relay
{
	public struct RelayHMACKey
	{
		public const int k_Length = 64;

		public unsafe fixed byte Value[64];

		public unsafe static global::Unity.Networking.Transport.Relay.RelayHMACKey FromBytePointer(byte* data, int length)
		{
			if (length != 64)
			{
				global::UnityEngine.Debug.LogError($"Provided byte array length is invalid, must be {64} but got {length}.");
				return default(global::Unity.Networking.Transport.Relay.RelayHMACKey);
			}
			global::Unity.Networking.Transport.Relay.RelayHMACKey result = default(global::Unity.Networking.Transport.Relay.RelayHMACKey);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(result.Value, data, length);
			return result;
		}

		public unsafe static global::Unity.Networking.Transport.Relay.RelayHMACKey FromByteArray(byte[] data)
		{
			fixed (byte* data2 = data)
			{
				return FromBytePointer(data2, data.Length);
			}
		}
	}
}
