namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::Unity.Burst.BurstCompile]
	public struct SimulatorPipelineStage : global::Unity.Networking.Transport.INetworkPipelineStage
	{
		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate> ReceiveFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate>(Receive);

		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate> SendFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate>(Send);

		private unsafe static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate> InitializeConnectionFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate>(InitializeConnection);

		public int StaticSize => global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters>();

		public unsafe global::Unity.Networking.Transport.NetworkPipelineStage StaticInitialize(byte* staticInstanceBuffer, int staticInstanceBufferLength, global::Unity.Networking.Transport.NetworkSettings settings)
		{
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters simulatorStageParameters = global::Unity.Networking.Transport.Utilities.SimulatorStageParameterExtensions.GetSimulatorStageParameters(ref settings);
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters>();
			if (num != staticInstanceBufferLength)
			{
				throw new global::System.InvalidOperationException($"simulatorParamsSizeOf {num}");
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(staticInstanceBuffer, &simulatorStageParameters, num);
			return new global::Unity.Networking.Transport.NetworkPipelineStage(ReceiveFunctionPointer, SendFunctionPointer, InitializeConnectionFunctionPointer, simulatorStageParameters.MaxPacketCount * (simulatorStageParameters.MaxPacketSize + global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket>()), simulatorStageParameters.MaxPacketCount * (simulatorStageParameters.MaxPacketSize + global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket>()), 0, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context>());
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate))]
		private unsafe static void InitializeConnection(byte* staticInstanceBuffer, int staticInstanceBufferLength, byte* sendProcessBuffer, int sendProcessBufferLength, byte* recvProcessBuffer, int recvProcessBufferLength, byte* sharedProcessBuffer, int sharedProcessBufferLength)
		{
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters param = default(global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(&param, staticInstanceBuffer, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters>());
			if (staticInstanceBufferLength == global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters>() && sharedProcessBufferLength == global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context>())
			{
				global::Unity.Networking.Transport.Utilities.SimulatorUtility.InitializeContext(param, sharedProcessBuffer);
			}
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate))]
		private unsafe static int Send(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context*)ctx.internalSharedProcessBuffer;
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters param = *(global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters*)ctx.staticInstanceBuffer;
			if (param.Mode == global::Unity.Networking.Transport.Utilities.ApplyMode.ReceivedPacketsOnly || param.Mode == global::Unity.Networking.Transport.Utilities.ApplyMode.Off)
			{
				return 0;
			}
			if (inboundBuffer.headerPadding + inboundBuffer.bufferLength > param.MaxPacketSize)
			{
				global::UnityEngine.Debug.LogWarning($"Incoming packet too large for SimulatorPipeline internal storage buffer. Passing through. [buffer={inboundBuffer.headerPadding + inboundBuffer.bufferLength} MaxPacketSize={param.MaxPacketSize}]");
				return -4;
			}
			long timestamp = ctx.timestamp;
			if (inboundBuffer.buffer != null)
			{
				internalSharedProcessBuffer->PacketCount++;
				if (global::Unity.Networking.Transport.Utilities.SimulatorUtility.ShouldDropPacket(internalSharedProcessBuffer, param, timestamp))
				{
					inboundBuffer = default(global::Unity.Networking.Transport.InboundSendBuffer);
					return 0;
				}
				if (param.FuzzFactor > 0)
				{
					global::Unity.Networking.Transport.Utilities.SimulatorUtility.FuzzPacket(internalSharedProcessBuffer, ref param, ref inboundBuffer);
				}
				if (global::Unity.Networking.Transport.Utilities.SimulatorUtility.ShouldDuplicatePacket(internalSharedProcessBuffer, ref param) && global::Unity.Networking.Transport.Utilities.SimulatorUtility.TryDelayPacket(ref ctx, ref param, ref inboundBuffer, ref requests, timestamp))
				{
					internalSharedProcessBuffer->PacketCount++;
				}
				if (global::Unity.Networking.Transport.Utilities.SimulatorUtility.TrySkipDelayingPacket(ref param, ref requests, internalSharedProcessBuffer) || !global::Unity.Networking.Transport.Utilities.SimulatorUtility.TryDelayPacket(ref ctx, ref param, ref inboundBuffer, ref requests, timestamp))
				{
					return 0;
				}
			}
			global::Unity.Networking.Transport.InboundSendBuffer delayedPacket = default(global::Unity.Networking.Transport.InboundSendBuffer);
			if (global::Unity.Networking.Transport.Utilities.SimulatorUtility.GetDelayedPacket(ref ctx, ref delayedPacket, ref requests, timestamp))
			{
				inboundBuffer = delayedPacket;
				return 0;
			}
			inboundBuffer = default(global::Unity.Networking.Transport.InboundSendBuffer);
			return 0;
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate))]
		private unsafe static void Receive(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundRecvBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context*)ctx.internalSharedProcessBuffer;
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters param = *(global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters*)ctx.staticInstanceBuffer;
			if (param.Mode == global::Unity.Networking.Transport.Utilities.ApplyMode.SentPacketsOnly || param.Mode == global::Unity.Networking.Transport.Utilities.ApplyMode.Off)
			{
				return;
			}
			if (inboundBuffer.bufferLength > param.MaxPacketSize)
			{
				global::UnityEngine.Debug.LogWarning($"Incoming packet too large for SimulatorPipeline internal storage buffer. Passing through. [buffer={inboundBuffer.bufferLength} MaxPacketSize={param.MaxPacketSize}]");
				return;
			}
			long timestamp = ctx.timestamp;
			if (inboundBuffer.buffer != null)
			{
				internalSharedProcessBuffer->PacketCount++;
				if (global::Unity.Networking.Transport.Utilities.SimulatorUtility.ShouldDropPacket(internalSharedProcessBuffer, param, timestamp))
				{
					inboundBuffer = default(global::Unity.Networking.Transport.InboundRecvBuffer);
					return;
				}
				global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer2 = new global::Unity.Networking.Transport.InboundSendBuffer
				{
					bufferWithHeaders = inboundBuffer.buffer,
					bufferWithHeadersLength = inboundBuffer.bufferLength,
					buffer = inboundBuffer.buffer,
					bufferLength = inboundBuffer.bufferLength,
					headerPadding = 0
				};
				if (global::Unity.Networking.Transport.Utilities.SimulatorUtility.ShouldDuplicatePacket(internalSharedProcessBuffer, ref param) && global::Unity.Networking.Transport.Utilities.SimulatorUtility.TryDelayPacket(ref ctx, ref param, ref inboundBuffer2, ref requests, timestamp))
				{
					internalSharedProcessBuffer->PacketCount++;
				}
				if (global::Unity.Networking.Transport.Utilities.SimulatorUtility.TrySkipDelayingPacket(ref param, ref requests, internalSharedProcessBuffer) || !global::Unity.Networking.Transport.Utilities.SimulatorUtility.TryDelayPacket(ref ctx, ref param, ref inboundBuffer2, ref requests, timestamp))
				{
					return;
				}
			}
			global::Unity.Networking.Transport.InboundSendBuffer delayedPacket = default(global::Unity.Networking.Transport.InboundSendBuffer);
			if (global::Unity.Networking.Transport.Utilities.SimulatorUtility.GetDelayedPacket(ref ctx, ref delayedPacket, ref requests, timestamp))
			{
				inboundBuffer.buffer = delayedPacket.bufferWithHeaders;
				inboundBuffer.bufferLength = delayedPacket.bufferWithHeadersLength;
			}
			else
			{
				inboundBuffer = default(global::Unity.Networking.Transport.InboundRecvBuffer);
			}
		}
	}
}
