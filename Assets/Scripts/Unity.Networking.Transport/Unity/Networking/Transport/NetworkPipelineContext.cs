namespace Unity.Networking.Transport
{
	public struct NetworkPipelineContext
	{
		public unsafe byte* staticInstanceBuffer;

		public unsafe byte* internalSharedProcessBuffer;

		public unsafe byte* internalProcessBuffer;

		public global::Unity.Collections.DataStreamWriter header;

		public long timestamp;

		public int staticInstanceBufferLength;

		public int internalSharedProcessBufferLength;

		public int internalProcessBufferLength;

		public int accumulatedHeaderCapacity;

		public int maxMessageSize;
	}
}
