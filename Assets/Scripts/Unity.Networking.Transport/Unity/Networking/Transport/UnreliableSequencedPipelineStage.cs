namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::Unity.Burst.BurstCompile]
	public struct UnreliableSequencedPipelineStage : global::Unity.Networking.Transport.INetworkPipelineStage
	{
		public struct SequenceId
		{
			internal ushort Value;

			public global::Unity.Collections.FixedString64Bytes ToFixedString()
			{
				return $"USPS.SequenceId[{Value}]";
			}

			public override string ToString()
			{
				return ToFixedString().ToString();
			}
		}

		public struct Statistics
		{
			public ulong NumPacketsSent;

			public ulong NumPacketsReceived;

			public ulong NumPacketsCulledOutOfOrder;

			public ulong NumPacketsDroppedNeverArrived;

			public double NetworkPacketLossPercent
			{
				get
				{
					if (NumPacketsReceived == 0L)
					{
						return 0.0;
					}
					return (double)NumPacketsDroppedNeverArrived / (double)(NumPacketsReceived + NumPacketsDroppedNeverArrived);
				}
			}

			public double OutOfOrderPacketLossPercent
			{
				get
				{
					if (NumPacketsReceived == 0L)
					{
						return 0.0;
					}
					return (double)NumPacketsCulledOutOfOrder / (double)(NumPacketsReceived + NumPacketsDroppedNeverArrived);
				}
			}

			public double CombinedPacketLossPercent
			{
				get
				{
					if (NumPacketsReceived == 0L)
					{
						return 0.0;
					}
					return (double)(NumPacketsDroppedNeverArrived + NumPacketsCulledOutOfOrder) / (double)(NumPacketsReceived + NumPacketsDroppedNeverArrived);
				}
			}

			public global::Unity.Collections.FixedString512Bytes ToFixedString()
			{
				return $"USPS.Stats[sent: {NumPacketsSent}, recv:{NumPacketsReceived}, dropped:{NumPacketsDroppedNeverArrived} ({(int)(NetworkPacketLossPercent * 100.0)}%), outOfOrder:{NumPacketsCulledOutOfOrder} ({(int)(OutOfOrderPacketLossPercent * 100.0)}%), combined:{(int)(CombinedPacketLossPercent * 100.0)}%]";
			}

			public override string ToString()
			{
				return ToFixedString().ToString();
			}
		}

		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate> ReceiveFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate>(Receive);

		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate> SendFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate>(Send);

		private unsafe static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate> InitializeConnectionFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate>(InitializeConnection);

		public int StaticSize => 0;

		public unsafe global::Unity.Networking.Transport.NetworkPipelineStage StaticInitialize(byte* staticInstanceBuffer, int staticInstanceBufferLength, global::Unity.Networking.Transport.NetworkSettings settings)
		{
			return new global::Unity.Networking.Transport.NetworkPipelineStage(ReceiveFunctionPointer, SendFunctionPointer, InitializeConnectionFunctionPointer, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.SequenceId>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.SequenceId>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<ushort>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.Statistics>());
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate))]
		private unsafe static void Receive(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundRecvBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
			global::Unity.Collections.NativeArray<byte> array = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(inboundBuffer.buffer, inboundBuffer.bufferLength, global::Unity.Collections.Allocator.Invalid);
			ushort num = new global::Unity.Collections.DataStreamReader(array).ReadUShort();
			global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.SequenceId* internalProcessBuffer = (global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.SequenceId*)ctx.internalProcessBuffer;
			global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.Statistics* internalSharedProcessBuffer = (global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.Statistics*)ctx.internalSharedProcessBuffer;
			internalSharedProcessBuffer->NumPacketsReceived++;
			if (global::Unity.Networking.Transport.Utilities.SequenceHelpers.GreaterThan16(num, internalProcessBuffer->Value))
			{
				internalSharedProcessBuffer->NumPacketsDroppedNeverArrived += (ulong)((long)global::Unity.Networking.Transport.Utilities.SequenceHelpers.AbsDistance(num, internalProcessBuffer->Value) - 1L);
				internalProcessBuffer->Value = num;
				inboundBuffer = inboundBuffer.Slice(2);
			}
			else
			{
				inboundBuffer = default(global::Unity.Networking.Transport.InboundRecvBuffer);
				internalSharedProcessBuffer->NumPacketsCulledOutOfOrder++;
				internalSharedProcessBuffer->NumPacketsDroppedNeverArrived--;
			}
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate))]
		private unsafe static int Send(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
			global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.SequenceId* internalProcessBuffer = (global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.SequenceId*)ctx.internalProcessBuffer;
			global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.Statistics* internalSharedProcessBuffer = (global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.Statistics*)ctx.internalSharedProcessBuffer;
			ctx.header.WriteUShort(internalProcessBuffer->Value);
			ushort* value = &internalProcessBuffer->Value;
			(*value)++;
			internalSharedProcessBuffer->NumPacketsSent++;
			return 0;
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate))]
		private unsafe static void InitializeConnection(byte* staticInstanceBuffer, int staticInstanceBufferLength, byte* sendProcessBuffer, int sendProcessBufferLength, byte* recvProcessBuffer, int recvProcessBufferLength, byte* sharedProcessBuffer, int sharedProcessBufferLength)
		{
			if (recvProcessBufferLength == global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.SequenceId>() && sendProcessBufferLength == global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.SequenceId>() && sharedProcessBufferLength == global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.Statistics>())
			{
				*(global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.SequenceId*)recvProcessBuffer = new global::Unity.Networking.Transport.UnreliableSequencedPipelineStage.SequenceId
				{
					Value = ushort.MaxValue
				};
			}
		}
	}
}
