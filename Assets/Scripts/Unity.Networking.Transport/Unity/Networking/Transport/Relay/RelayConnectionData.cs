namespace Unity.Networking.Transport.Relay
{
	public struct RelayConnectionData
	{
		public const int k_Length = 255;

		public unsafe fixed byte Value[255];

		public unsafe static global::Unity.Networking.Transport.Relay.RelayConnectionData FromBytePointer(byte* dataPtr, int length)
		{
			if (length > 255)
			{
				global::UnityEngine.Debug.LogError($"Provided byte array length is invalid, must be less or equal to {255} but got {length}.");
				return default(global::Unity.Networking.Transport.Relay.RelayConnectionData);
			}
			global::Unity.Networking.Transport.Relay.RelayConnectionData result = default(global::Unity.Networking.Transport.Relay.RelayConnectionData);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(result.Value, dataPtr, length);
			return result;
		}

		public unsafe static global::Unity.Networking.Transport.Relay.RelayConnectionData FromByteArray(byte[] data)
		{
			fixed (byte* dataPtr = data)
			{
				return FromBytePointer(dataPtr, data.Length);
			}
		}
	}
}
