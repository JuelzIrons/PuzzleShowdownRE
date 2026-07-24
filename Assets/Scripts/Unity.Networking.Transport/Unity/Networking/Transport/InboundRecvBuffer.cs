namespace Unity.Networking.Transport
{
	public struct InboundRecvBuffer
	{
		public unsafe byte* buffer;

		public int bufferLength;

		public unsafe global::Unity.Networking.Transport.InboundRecvBuffer Slice(int offset)
		{
			global::Unity.Networking.Transport.InboundRecvBuffer result = default(global::Unity.Networking.Transport.InboundRecvBuffer);
			result.buffer = buffer + offset;
			result.bufferLength = bufferLength - offset;
			return result;
		}
	}
}
