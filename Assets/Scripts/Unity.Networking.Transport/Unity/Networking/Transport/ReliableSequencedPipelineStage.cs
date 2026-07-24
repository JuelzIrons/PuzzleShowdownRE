namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::Unity.Burst.BurstCompile]
	public struct ReliableSequencedPipelineStage : global::Unity.Networking.Transport.INetworkPipelineStage
	{
		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate> ReceiveFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate>(Receive);

		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate> SendFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate>(Send);

		private unsafe static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate> InitializeConnectionFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate>(InitializeConnection);

		public int StaticSize => global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters>();

		public unsafe global::Unity.Networking.Transport.NetworkPipelineStage StaticInitialize(byte* staticInstanceBuffer, int staticInstanceBufferLength, global::Unity.Networking.Transport.NetworkSettings settings)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters reliableStageParameters = global::Unity.Networking.Transport.Utilities.ReliableStageParameterExtensions.GetReliableStageParameters(ref settings);
			reliableStageParameters.WindowSize = (reliableStageParameters.WindowSize + 7) & -8;
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(staticInstanceBuffer, &reliableStageParameters, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters>());
			return new global::Unity.Networking.Transport.NetworkPipelineStage(ReceiveFunctionPointer, SendFunctionPointer, InitializeConnectionFunctionPointer, global::Unity.Networking.Transport.Utilities.ReliableUtility.ProcessCapacityNeeded(reliableStageParameters), global::Unity.Networking.Transport.Utilities.ReliableUtility.ProcessCapacityNeeded(reliableStageParameters), global::Unity.Networking.Transport.Utilities.ReliableUtility.MaxPacketHeaderWireSize(reliableStageParameters.WindowSize), global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedCapacityNeeded(reliableStageParameters));
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate))]
		private unsafe static void Receive(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundRecvBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
			requests = global::Unity.Networking.Transport.NetworkPipelineStage.Requests.SendUpdate;
			global::Unity.Networking.Transport.Utilities.ReliableUtility.Context* internalProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)ctx.internalProcessBuffer;
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)ctx.internalSharedProcessBuffer;
			if (inboundBuffer.buffer != null)
			{
				global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader header;
				global::Unity.Networking.Transport.Utilities.ReliableAckMask mask;
				int num = global::Unity.Networking.Transport.Utilities.ReliableUtility.ReadHeader(new global::System.ReadOnlySpan<byte>(inboundBuffer.buffer, inboundBuffer.bufferLength), out header, out mask, internalSharedProcessBuffer->WindowSize);
				if (num <= 0)
				{
					inboundBuffer = default(global::Unity.Networking.Transport.InboundRecvBuffer);
					return;
				}
				global::Unity.Networking.Transport.InboundRecvBuffer inboundRecvBuffer = inboundBuffer.Slice(num);
				inboundBuffer = default(global::Unity.Networking.Transport.InboundRecvBuffer);
				if (header.Type == 1)
				{
					global::Unity.Networking.Transport.Utilities.ReliableUtility.ReadAckPacket(ctx, header, mask);
				}
				else
				{
					long num2 = global::Unity.Networking.Transport.Utilities.ReliableUtility.Read(ctx, header, mask);
					if (num2 >= 0)
					{
						ushort num3 = (ushort)(internalProcessBuffer->Delivered + 1);
						if (num2 == num3)
						{
							internalProcessBuffer->Delivered = num2;
							inboundBuffer = inboundRecvBuffer;
						}
						else
						{
							global::Unity.Networking.Transport.Utilities.ReliableUtility.SetPacket(ctx.internalProcessBuffer, num2, inboundRecvBuffer);
						}
					}
				}
			}
			if (inboundBuffer.buffer == null)
			{
				inboundBuffer = global::Unity.Networking.Transport.Utilities.ReliableUtility.ResumeReceive(ctx);
			}
			if (global::Unity.Networking.Transport.Utilities.ReliableUtility.NeedResumeReceive(ctx))
			{
				requests |= global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Resume;
			}
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate))]
		private unsafe static int Send(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
			requests = global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Update;
			global::Unity.Networking.Transport.Utilities.ReliableUtility.Context* internalProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)ctx.internalProcessBuffer;
			global::Unity.Networking.Transport.Utilities.ReliableUtility.ReleaseAcknowledgedPackets(ctx);
			if (inboundBuffer.buffer != null)
			{
				long num = global::Unity.Networking.Transport.Utilities.ReliableUtility.Write(ctx, inboundBuffer);
				if (num < 0)
				{
					inboundBuffer = default(global::Unity.Networking.Transport.InboundSendBuffer);
					requests |= global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Error;
					return -5;
				}
				global::Unity.Networking.Transport.Utilities.ReliableUtility.WriteHeader(ref ctx, global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketType.Payload, num);
				global::Unity.Networking.Transport.Utilities.ReliableUtility.UpdateContextAfterPacketSend(ctx);
				return 0;
			}
			if (internalProcessBuffer->Resume != -1)
			{
				inboundBuffer = global::Unity.Networking.Transport.Utilities.ReliableUtility.ResumeSend(ctx);
				global::Unity.Networking.Transport.Utilities.ReliableUtility.WriteHeader(ref ctx, global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketType.Payload, internalProcessBuffer->Resume);
				global::Unity.Networking.Transport.Utilities.ReliableUtility.UpdateContextAfterPacketSend(ctx);
				internalProcessBuffer->Resume = global::Unity.Networking.Transport.Utilities.ReliableUtility.GetNextSendResumeSequence(ctx);
				if (internalProcessBuffer->Resume != -1)
				{
					requests |= global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Resume;
				}
				return 0;
			}
			internalProcessBuffer->Resume = global::Unity.Networking.Transport.Utilities.ReliableUtility.GetNextSendResumeSequence(ctx);
			if (internalProcessBuffer->Resume != -1)
			{
				requests |= global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Resume;
			}
			if (global::Unity.Networking.Transport.Utilities.ReliableUtility.ShouldSendAck(ctx))
			{
				global::Unity.Networking.Transport.Utilities.ReliableUtility.WriteHeader(ref ctx, global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketType.Ack, -1L);
				global::Unity.Networking.Transport.Utilities.ReliableUtility.UpdateContextAfterPacketSend(ctx);
				inboundBuffer.bufferWithHeadersLength = inboundBuffer.headerPadding + 1;
				inboundBuffer.bufferWithHeaders = (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(inboundBuffer.bufferWithHeadersLength, 8, global::Unity.Collections.Allocator.Temp);
				inboundBuffer.SetBufferFromBufferWithHeaders();
				return 0;
			}
			return 0;
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate))]
		private unsafe static void InitializeConnection(byte* staticInstanceBuffer, int staticInstanceBufferLength, byte* sendProcessBuffer, int sendProcessBufferLength, byte* recvProcessBuffer, int recvProcessBufferLength, byte* sharedProcessBuffer, int sharedProcessBufferLength)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters param = default(global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(&param, staticInstanceBuffer, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters>());
			if (sharedProcessBufferLength == global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedCapacityNeeded(param) && sendProcessBufferLength + recvProcessBufferLength >= global::Unity.Networking.Transport.Utilities.ReliableUtility.ProcessCapacityNeeded(param) * 2)
			{
				global::Unity.Networking.Transport.Utilities.ReliableUtility.InitializeContext(sharedProcessBuffer, sharedProcessBufferLength, sendProcessBuffer, sendProcessBufferLength, recvProcessBuffer, recvProcessBufferLength, param);
			}
		}
	}
}
