namespace Unity.Networking.Transport
{
	public struct ReceiveJobArguments
	{
		public global::Unity.Networking.Transport.PacketsQueue ReceiveQueue;

		public global::Unity.Networking.Transport.OperationResult ReceiveResult;

		public long Time;

		internal global::Unity.Networking.Transport.NetworkDriverReceiver DriverReceiver;

		internal global::Unity.Networking.Transport.NetworkEventQueue EventQueue;

		internal global::Unity.Networking.Transport.NetworkPipelineProcessor PipelineProcessor;

		internal global::Unity.Collections.NativeHashMap<global::Unity.Networking.Transport.ConnectionId, global::Unity.Networking.Transport.ConnectionPayload> ConnectionPayloads;
	}
}
