namespace Unity.Networking.Transport.Utilities
{
	public static class SimulatorUtility
	{
		[global::System.Serializable]
		public struct Parameters : global::Unity.Networking.Transport.INetworkParameter
		{
			public int MaxPacketCount;

			public int MaxPacketSize;

			public uint RandomSeed;

			public global::Unity.Networking.Transport.Utilities.ApplyMode Mode;

			public int PacketDelayMs;

			public int PacketJitterMs;

			public int PacketDropInterval;

			public int PacketDropPercentage;

			public int PacketDuplicationPercentage;

			public int FuzzFactor;

			public int FuzzOffset;

			public bool Validate()
			{
				return true;
			}
		}

		internal struct Context
		{
			public global::Unity.Mathematics.Random Random;

			public int PacketCount;

			public int ReadyPackets;

			public int WaitingPackets;
		}

		internal struct DelayedPacket
		{
			public int processBufferOffset;

			public ushort packetSize;

			public ushort packetHeaderPadding;

			public long delayUntil;
		}

		internal unsafe static void InitializeContext(global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters param, byte* sharedProcessBuffer)
		{
			((global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context*)sharedProcessBuffer)->Random = default(global::Unity.Mathematics.Random);
			if (param.RandomSeed != 0)
			{
				((global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context*)sharedProcessBuffer)->Random.InitState(param.RandomSeed);
			}
			else
			{
				((global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context*)sharedProcessBuffer)->Random.InitState();
			}
		}

		private unsafe static bool GetEmptyDataSlot(global::Unity.Networking.Transport.NetworkPipelineContext ctx, byte* processBufferPtr, ref int packetPayloadOffset, ref int packetDataOffset)
		{
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters staticInstanceBuffer = *(global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters*)ctx.staticInstanceBuffer;
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket>();
			int num2 = staticInstanceBuffer.MaxPacketCount * num;
			bool result = false;
			for (int i = 0; i < staticInstanceBuffer.MaxPacketCount; i++)
			{
				packetDataOffset = num * i;
				global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket* ptr = (global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket*)(processBufferPtr + packetDataOffset);
				if (ptr->delayUntil == 0L)
				{
					result = true;
					packetPayloadOffset = num2 + staticInstanceBuffer.MaxPacketSize * i;
					break;
				}
			}
			return result;
		}

		internal unsafe static bool GetDelayedPacket(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundSendBuffer delayedPacket, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, long currentTimestamp)
		{
			requests = global::Unity.Networking.Transport.NetworkPipelineStage.Requests.None;
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters staticInstanceBuffer = *(global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters*)ctx.staticInstanceBuffer;
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket>();
			byte* internalProcessBuffer = ctx.internalProcessBuffer;
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context*)ctx.internalSharedProcessBuffer;
			int num2 = -1;
			long num3 = long.MaxValue;
			int num4 = 0;
			int num5 = 0;
			for (int i = 0; i < staticInstanceBuffer.MaxPacketCount; i++)
			{
				global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket* ptr = (global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket*)(internalProcessBuffer + num * i);
				if ((int)ptr->delayUntil == 0)
				{
					continue;
				}
				num5++;
				if (ptr->delayUntil <= currentTimestamp)
				{
					num4++;
					if (num3 > ptr->delayUntil)
					{
						num2 = i;
						num3 = ptr->delayUntil;
					}
				}
			}
			internalSharedProcessBuffer->WaitingPackets = num5;
			if (num4 > 1)
			{
				requests |= global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Resume;
			}
			else if (num5 > 0)
			{
				requests |= global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Update;
			}
			if (num2 >= 0)
			{
				global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket* ptr2 = (global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket*)(internalProcessBuffer + num * num2);
				ptr2->delayUntil = 0L;
				delayedPacket.bufferWithHeaders = ctx.internalProcessBuffer + ptr2->processBufferOffset;
				delayedPacket.bufferWithHeadersLength = ptr2->packetSize;
				delayedPacket.headerPadding = ptr2->packetHeaderPadding;
				delayedPacket.SetBufferFromBufferWithHeaders();
				return true;
			}
			return false;
		}

		internal unsafe static void FuzzPacket(global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context* ctx, ref global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters param, ref global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer)
		{
			int fuzzFactor = param.FuzzFactor;
			int fuzzOffset = param.FuzzOffset;
			if (ctx->Random.NextInt(0, 100) > fuzzFactor)
			{
				return;
			}
			int bufferLength = inboundBuffer.bufferLength;
			for (int i = fuzzOffset; i < bufferLength; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (fuzzFactor > ctx->Random.NextInt(0, 100))
					{
						byte* num = inboundBuffer.buffer + i;
						*num ^= (byte)(1 << j);
					}
				}
			}
		}

		internal unsafe static bool TryDelayPacket(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters param, ref global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, long timestamp)
		{
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context*)ctx.internalSharedProcessBuffer;
			int packetPayloadOffset = 0;
			int packetDataOffset = 0;
			byte* internalProcessBuffer = ctx.internalProcessBuffer;
			if (!GetEmptyDataSlot(ctx, internalProcessBuffer, ref packetPayloadOffset, ref packetDataOffset))
			{
				global::UnityEngine.Debug.LogWarning($"Simulator has no space left in the delayed packets queue ({param.MaxPacketCount} packets already in queue). Letting packet go through. Increase MaxPacketCount during driver construction.");
				return false;
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ctx.internalProcessBuffer + packetPayloadOffset + inboundBuffer.headerPadding, inboundBuffer.buffer, inboundBuffer.bufferLength);
			int num = global::Unity.Mathematics.math.max(0, param.PacketDelayMs + internalSharedProcessBuffer->Random.NextInt(param.PacketJitterMs * 2) - param.PacketJitterMs);
			global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket delayedPacket = default(global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket);
			delayedPacket.delayUntil = timestamp + num;
			delayedPacket.processBufferOffset = packetPayloadOffset;
			delayedPacket.packetSize = (ushort)(inboundBuffer.headerPadding + inboundBuffer.bufferLength);
			delayedPacket.packetHeaderPadding = (ushort)inboundBuffer.headerPadding;
			byte* source = (byte*)(&delayedPacket);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(internalProcessBuffer + packetDataOffset, source, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.SimulatorUtility.DelayedPacket>());
			requests |= global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Update;
			return true;
		}

		internal unsafe static bool TrySkipDelayingPacket(ref global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters param, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context* simCtx)
		{
			if (param.PacketDelayMs == 0 && param.PacketJitterMs == 0)
			{
				if (simCtx->WaitingPackets > 0)
				{
					requests |= global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Update;
				}
				return true;
			}
			return false;
		}

		internal unsafe static bool ShouldDropPacket(global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context* ctx, global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters param, long timestamp)
		{
			if (param.PacketDropInterval > 0 && (ctx->PacketCount - 1) % param.PacketDropInterval == 0)
			{
				return true;
			}
			if (param.PacketDropPercentage > 0 && ctx->Random.NextInt(0, 100) < param.PacketDropPercentage)
			{
				return true;
			}
			return false;
		}

		internal unsafe static bool ShouldDuplicatePacket(global::Unity.Networking.Transport.Utilities.SimulatorUtility.Context* ctx, ref global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters param)
		{
			if (param.PacketDuplicationPercentage > 0 && ctx->Random.NextInt(0, 100) < param.PacketDuplicationPercentage)
			{
				return true;
			}
			return false;
		}
	}
}
