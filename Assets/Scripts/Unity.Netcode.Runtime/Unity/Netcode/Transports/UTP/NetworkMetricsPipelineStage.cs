namespace Unity.Netcode.Transports.UTP
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::Unity.Burst.BurstCompile]
	internal struct NetworkMetricsPipelineStage : global::Unity.Networking.Transport.INetworkPipelineStage
	{
		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate> s_ReceiveFunction = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate>(Receive);

		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate> s_SendFunction = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate>(Send);

		private unsafe static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate> s_InitializeConnectionFunction = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate>(InitializeConnection);

		public int StaticSize => 0;

		public unsafe global::Unity.Networking.Transport.NetworkPipelineStage StaticInitialize(byte* staticInstanceBuffer, int staticInstanceBufferLength, global::Unity.Networking.Transport.NetworkSettings settings)
		{
			return new global::Unity.Networking.Transport.NetworkPipelineStage(s_ReceiveFunction, s_SendFunction, s_InitializeConnectionFunction, 0, 0, 0, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Netcode.Transports.UTP.NetworkMetricsContext>());
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate))]
		private unsafe static void Receive(ref global::Unity.Networking.Transport.NetworkPipelineContext networkPipelineContext, ref global::Unity.Networking.Transport.InboundRecvBuffer inboundReceiveBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
			global::Unity.Netcode.Transports.UTP.NetworkMetricsContext* internalSharedProcessBuffer = (global::Unity.Netcode.Transports.UTP.NetworkMetricsContext*)networkPipelineContext.internalSharedProcessBuffer;
			internalSharedProcessBuffer->PacketReceivedCount++;
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate))]
		private unsafe static int Send(ref global::Unity.Networking.Transport.NetworkPipelineContext networkPipelineContext, ref global::Unity.Networking.Transport.InboundSendBuffer inboundSendBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
			global::Unity.Netcode.Transports.UTP.NetworkMetricsContext* internalSharedProcessBuffer = (global::Unity.Netcode.Transports.UTP.NetworkMetricsContext*)networkPipelineContext.internalSharedProcessBuffer;
			internalSharedProcessBuffer->PacketSentCount++;
			return 0;
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate))]
		private unsafe static void InitializeConnection(byte* staticInstanceBuffer, int staticInstanceBufferLength, byte* sendProcessBuffer, int sendProcessBufferLength, byte* receiveProcessBuffer, int receiveProcessBufferLength, byte* sharedProcessBuffer, int sharedProcessBufferLength)
		{
			((global::Unity.Netcode.Transports.UTP.NetworkMetricsContext*)sharedProcessBuffer)->PacketSentCount = 0u;
			((global::Unity.Netcode.Transports.UTP.NetworkMetricsContext*)sharedProcessBuffer)->PacketReceivedCount = 0u;
		}
	}
}
