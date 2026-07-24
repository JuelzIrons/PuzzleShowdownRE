namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::Unity.Burst.BurstCompile]
	public struct FragmentationPipelineStage : global::Unity.Networking.Transport.INetworkPipelineStage
	{
		private struct FragContext
		{
			public int startIndex;

			public int endIndex;

			public int sequence;

			public bool packetError;
		}

		private struct FragSharedContext
		{
			public int PayloadCapacity;
		}

		[global::System.Flags]
		private enum FragFlags
		{
			First = 0x8000,
			Last = 0x4000,
			SeqMask = 0x3FFF
		}

		private const int FragHeaderCapacity = 2;

		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate> ReceiveFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate>(Receive);

		private static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate> SendFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate>(Send);

		private unsafe static global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate> InitializeConnectionFunctionPointer = new global::Unity.Networking.Transport.TransportFunctionPointer<global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate>(InitializeConnection);

		public int StaticSize => global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.FragmentationPipelineStage.FragSharedContext>();

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.SendDelegate))]
		private unsafe static int Send(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
			global::Unity.Networking.Transport.FragmentationPipelineStage.FragContext* internalProcessBuffer = (global::Unity.Networking.Transport.FragmentationPipelineStage.FragContext*)ctx.internalProcessBuffer;
			byte* ptr = ctx.internalProcessBuffer + sizeof(global::Unity.Networking.Transport.FragmentationPipelineStage.FragContext);
			global::Unity.Networking.Transport.FragmentationPipelineStage.FragSharedContext* staticInstanceBuffer = (global::Unity.Networking.Transport.FragmentationPipelineStage.FragSharedContext*)ctx.staticInstanceBuffer;
			global::Unity.Networking.Transport.FragmentationPipelineStage.FragFlags fragFlags = global::Unity.Networking.Transport.FragmentationPipelineStage.FragFlags.First;
			int num = ctx.maxMessageSize - systemHeaderSize - inboundBuffer.headerPadding;
			int num2 = num - ctx.accumulatedHeaderCapacity;
			if (internalProcessBuffer->endIndex > internalProcessBuffer->startIndex)
			{
				if (inboundBuffer.buffer != null)
				{
					return -3;
				}
				fragFlags &= ~global::Unity.Networking.Transport.FragmentationPipelineStage.FragFlags.First;
				int num3 = internalProcessBuffer->endIndex - internalProcessBuffer->startIndex;
				if (num3 > num)
				{
					num3 = num;
				}
				inboundBuffer.bufferWithHeaders = (inboundBuffer.buffer = ptr + internalProcessBuffer->startIndex) - inboundBuffer.headerPadding;
				inboundBuffer.bufferLength = num3;
				inboundBuffer.bufferWithHeadersLength = num3 + inboundBuffer.headerPadding;
				internalProcessBuffer->startIndex += num3;
			}
			else if (inboundBuffer.bufferLength > num2)
			{
				int payloadCapacity = staticInstanceBuffer->PayloadCapacity;
				int num4 = inboundBuffer.bufferLength - num2;
				byte* source = inboundBuffer.buffer + num2;
				if (num4 + inboundBuffer.headerPadding > payloadCapacity)
				{
					return -4;
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr + inboundBuffer.headerPadding, source, num4);
				internalProcessBuffer->startIndex = inboundBuffer.headerPadding;
				internalProcessBuffer->endIndex = num4 + inboundBuffer.headerPadding;
				inboundBuffer.bufferWithHeadersLength -= num4;
				inboundBuffer.bufferLength -= num4;
			}
			if (internalProcessBuffer->endIndex > internalProcessBuffer->startIndex)
			{
				requests |= global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Resume;
			}
			else
			{
				fragFlags |= global::Unity.Networking.Transport.FragmentationPipelineStage.FragFlags.Last;
			}
			int num5 = (internalProcessBuffer->sequence++ & 0x3FFF) | (int)fragFlags;
			ctx.header.WriteShort((short)num5);
			return 0;
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.ReceiveDelegate))]
		private unsafe static void Receive(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundRecvBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
		{
			global::Unity.Networking.Transport.FragmentationPipelineStage.FragContext* internalProcessBuffer = (global::Unity.Networking.Transport.FragmentationPipelineStage.FragContext*)ctx.internalProcessBuffer;
			byte* ptr = ctx.internalProcessBuffer + sizeof(global::Unity.Networking.Transport.FragmentationPipelineStage.FragContext);
			global::Unity.Networking.Transport.FragmentationPipelineStage.FragSharedContext* staticInstanceBuffer = (global::Unity.Networking.Transport.FragmentationPipelineStage.FragSharedContext*)ctx.staticInstanceBuffer;
			global::Unity.Collections.NativeArray<byte> array = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(inboundBuffer.buffer, inboundBuffer.bufferLength, global::Unity.Collections.Allocator.Invalid);
			short num = new global::Unity.Collections.DataStreamReader(array).ReadShort();
			int num2 = num & 0x3FFF;
			global::Unity.Networking.Transport.FragmentationPipelineStage.FragFlags fragFlags = (global::Unity.Networking.Transport.FragmentationPipelineStage.FragFlags)(num & -16384);
			inboundBuffer = inboundBuffer.Slice(2);
			int num3 = internalProcessBuffer->sequence;
			bool num4 = (fragFlags & global::Unity.Networking.Transport.FragmentationPipelineStage.FragFlags.First) != 0;
			bool flag = (fragFlags & global::Unity.Networking.Transport.FragmentationPipelineStage.FragFlags.Last) != 0;
			if (num4)
			{
				num3 = num2;
				internalProcessBuffer->packetError = false;
				internalProcessBuffer->endIndex = 0;
			}
			if (num2 != num3)
			{
				internalProcessBuffer->packetError = true;
				internalProcessBuffer->endIndex = 0;
			}
			if (!internalProcessBuffer->packetError)
			{
				if (!flag || internalProcessBuffer->endIndex > 0)
				{
					if (internalProcessBuffer->endIndex + inboundBuffer.bufferLength > staticInstanceBuffer->PayloadCapacity)
					{
						global::UnityEngine.Debug.LogError("Fragmentation capacity exceeded");
						return;
					}
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr + internalProcessBuffer->endIndex, inboundBuffer.buffer, inboundBuffer.bufferLength);
					internalProcessBuffer->endIndex += inboundBuffer.bufferLength;
				}
				if (flag && internalProcessBuffer->endIndex > 0)
				{
					inboundBuffer = new global::Unity.Networking.Transport.InboundRecvBuffer
					{
						buffer = ptr,
						bufferLength = internalProcessBuffer->endIndex
					};
				}
			}
			if (!flag || internalProcessBuffer->packetError)
			{
				inboundBuffer = default(global::Unity.Networking.Transport.InboundRecvBuffer);
			}
			internalProcessBuffer->sequence = (num2 + 1) & 0x3FFF;
		}

		[global::Unity.Burst.BurstCompile(DisableDirectCall = true)]
		[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Networking.Transport.NetworkPipelineStage.InitializeConnectionDelegate))]
		private unsafe static void InitializeConnection(byte* staticInstanceBuffer, int staticInstanceBufferLength, byte* sendProcessBuffer, int sendProcessBufferLength, byte* recvProcessBuffer, int recvProcessBufferLength, byte* sharedProcessBuffer, int sharedProcessBufferLength)
		{
		}

		public unsafe global::Unity.Networking.Transport.NetworkPipelineStage StaticInitialize(byte* staticInstanceBuffer, int staticInstanceBufferLength, global::Unity.Networking.Transport.NetworkSettings settings)
		{
			((global::Unity.Networking.Transport.FragmentationPipelineStage.FragSharedContext*)staticInstanceBuffer)->PayloadCapacity = global::Unity.Networking.Transport.Utilities.FragmentationStageParameterExtensions.GetFragmentationStageParameters(ref settings).PayloadCapacity;
			return new global::Unity.Networking.Transport.NetworkPipelineStage(ReceiveFunctionPointer, SendFunctionPointer, InitializeConnectionFunctionPointer, sizeof(global::Unity.Networking.Transport.FragmentationPipelineStage.FragContext) + ((global::Unity.Networking.Transport.FragmentationPipelineStage.FragSharedContext*)staticInstanceBuffer)->PayloadCapacity, sizeof(global::Unity.Networking.Transport.FragmentationPipelineStage.FragContext) + ((global::Unity.Networking.Transport.FragmentationPipelineStage.FragSharedContext*)staticInstanceBuffer)->PayloadCapacity, 2, 0, ((global::Unity.Networking.Transport.FragmentationPipelineStage.FragSharedContext*)staticInstanceBuffer)->PayloadCapacity);
		}
	}
}
