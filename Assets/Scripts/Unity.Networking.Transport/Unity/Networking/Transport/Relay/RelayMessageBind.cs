namespace Unity.Networking.Transport.Relay
{
	internal static class RelayMessageBind
	{
		private const byte k_ConnectionDataLength = byte.MaxValue;

		private const byte k_HMACLength = 32;

		public const int Length = 295;

		public unsafe static void Write(global::Unity.Collections.DataStreamWriter writer, byte acceptMode, ushort nonce, byte* connectionDataPtr, byte* hmac)
		{
			global::Unity.Networking.Transport.Relay.RelayMessageHeader relayMessageHeader = global::Unity.Networking.Transport.Relay.RelayMessageHeader.Create(global::Unity.Networking.Transport.Relay.RelayMessageType.Bind);
			global::Unity.Collections.LowLevel.Unsafe.DataStreamExtensions.WriteBytesUnsafe(ref writer, (byte*)(&relayMessageHeader), 4);
			writer.WriteByte(acceptMode);
			writer.WriteUShort(nonce);
			writer.WriteByte(byte.MaxValue);
			global::Unity.Collections.LowLevel.Unsafe.DataStreamExtensions.WriteBytesUnsafe(ref writer, connectionDataPtr, 255);
			global::Unity.Collections.LowLevel.Unsafe.DataStreamExtensions.WriteBytesUnsafe(ref writer, hmac, 32);
		}

		public unsafe static void Write(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor, ref global::Unity.Networking.Transport.Relay.RelayServerData serverData)
		{
			global::Unity.Networking.Transport.Relay.RelayMessageHeader.Write(ref packetProcessor, global::Unity.Networking.Transport.Relay.RelayMessageType.Bind);
			packetProcessor.AppendToPayload((byte)0);
			packetProcessor.AppendToPayload(serverData.Nonce);
			packetProcessor.AppendToPayload(byte.MaxValue);
			packetProcessor.AppendToPayload(serverData.ConnectionData);
			fixed (byte* hMAC = serverData.HMAC)
			{
				packetProcessor.AppendToPayload(hMAC, 32);
			}
		}
	}
}
