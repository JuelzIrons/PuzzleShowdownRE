namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::Unity.Burst.BurstCompile]
	public struct NullPipelineStage : global::Unity.Networking.Transport.INetworkPipelineStage
	{
		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate> ReceiveFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate>(Receive);

		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate> SendFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate>(Send);

		private unsafe static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate> InitializeConnectionFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate>(InitializeConnection);

		public int StaticSize => 0;

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate))]
		private static int Send(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
			return 0;
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate))]
		private static void Receive(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundRecvBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate))]
		private unsafe static void InitializeConnection(byte* staticInstanceBuffer, int staticInstanceBufferLength, byte* sendProcessBuffer, int sendProcessBufferLength, byte* recvProcessBuffer, int recvProcessBufferLength, byte* sharedProcessBuffer, int sharedProcessBufferLength)
		{
		}

		public unsafe global::Unity.Networking.Transport.NetworkPipelineStage StaticInitialize(byte* staticInstanceBuffer, int staticInstanceBufferLength, global::Unity.Networking.Transport.NetworkSettings netParams)
		{
			return new global::Unity.Networking.Transport.NetworkPipelineStage(ReceiveFunctionPointer, SendFunctionPointer, InitializeConnectionFunctionPointer, 0, 0, 0, 0);
		}
	}
}
