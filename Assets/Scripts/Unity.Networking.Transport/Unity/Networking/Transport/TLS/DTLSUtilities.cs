namespace Unity.Networking.Transport.TLS
{
	internal static class DTLSUtilities
	{
		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe static bool IsClientHello(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor)
		{
			byte* ptr = (byte*)packetProcessor.GetUnsafePayloadPtr() + packetProcessor.Offset;
			if (packetProcessor.Length >= 25 && *ptr == 22)
			{
				return ptr[13] == 1;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal unsafe static bool IsServerHello(ref global::Unity.Networking.Transport.PacketProcessor packetProcessor)
		{
			byte* ptr = (byte*)packetProcessor.GetUnsafePayloadPtr() + packetProcessor.Offset;
			if (packetProcessor.Length >= 25 && *ptr == 22)
			{
				return ptr[13] == 2;
			}
			return false;
		}
	}
}
