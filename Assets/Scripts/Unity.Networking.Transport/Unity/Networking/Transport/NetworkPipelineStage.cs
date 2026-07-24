namespace Unity.Networking.Transport
{
	public struct NetworkPipelineStage
	{
		[global::System.Flags]
		public enum Requests
		{
			None = 0,
			Resume = 1,
			Update = 2,
			SendUpdate = 4,
			Error = 8
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public delegate void ReceiveDelegate(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundRecvBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeadersSize);

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public delegate int SendDelegate(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeadersSize);

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		public unsafe delegate void InitializeConnectionDelegate(byte* staticInstanceBuffer, int staticInstanceBufferLength, byte* sendProcessBuffer, int sendProcessBufferLength, byte* recvProcessBuffer, int recvProcessBufferLength, byte* sharedProcessBuffer, int sharedProcessBufferLength);

		public global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate> Receive;

		public global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate> Send;

		public global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate> InitializeConnection;

		public readonly int ReceiveCapacity;

		public readonly int SendCapacity;

		public readonly int HeaderCapacity;

		public readonly int SharedStateCapacity;

		public readonly int PayloadCapacity;

		internal int StaticStateStart;

		internal int StaticStateCapacity;

		public NetworkPipelineStage(global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate> Receive, global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate> Send, global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate> InitializeConnection, int ReceiveCapacity, int SendCapacity, int HeaderCapacity, int SharedStateCapacity, int PayloadCapacity = 0)
		{
			this.Receive = Receive;
			this.Send = Send;
			this.InitializeConnection = InitializeConnection;
			this.ReceiveCapacity = ReceiveCapacity;
			this.SendCapacity = SendCapacity;
			this.HeaderCapacity = HeaderCapacity;
			this.SharedStateCapacity = SharedStateCapacity;
			this.PayloadCapacity = PayloadCapacity;
			StaticStateStart = (StaticStateCapacity = 0);
		}
	}
}
