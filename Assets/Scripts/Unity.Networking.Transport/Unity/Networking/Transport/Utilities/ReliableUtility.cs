namespace Unity.Networking.Transport.Utilities
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public struct ReliableUtility
	{
		public struct Statistics
		{
			public int PacketsReceived;

			public int PacketsSent;

			public int PacketsDropped;

			public int PacketsOutOfOrder;

			public int PacketsDuplicated;

			public int PacketsStale;

			public int PacketsResent;
		}

		public struct RTTInfo
		{
			public int LastRtt;

			public float SmoothedRtt;

			public float SmoothedVariance;

			public int ResendTimeout;
		}

		internal enum PacketType : byte
		{
			Payload = 0,
			Ack = 1
		}

		internal struct SequenceBufferContext
		{
			public long Sequence;

			public long Acked;

			public global::Unity.Networking.Transport.Utilities.ReliableAckMask AckMask;

			public global::Unity.Networking.Transport.Utilities.ReliableAckMask LastAckMask;

			internal long NumberOfOverflowsDetected;

			internal ushort LastReceivedOverflowCycle => (ushort)(NumberOfOverflowsDetected & 3);
		}

		public struct SharedContext
		{
			internal global::Unity.Networking.Transport.Utilities.ReliableUtility.SequenceBufferContext SentPackets;

			internal global::Unity.Networking.Transport.Utilities.ReliableUtility.SequenceBufferContext ReceivedPackets;

			internal int DuplicatesSinceLastAck;

			public int WindowSize;

			public int MinimumResendTime;

			public int MaximumResendTime;

			public global::Unity.Networking.Transport.Utilities.ReliableUtility.Statistics stats;

			public global::Unity.Networking.Transport.Utilities.ReliableUtility.RTTInfo RttInfo;

			internal int TimerDataOffset;

			internal int TimerDataStride;

			internal int RemoteTimerDataOffset;

			internal int RemoteTimerDataStride;
		}

		internal struct Context
		{
			public int Capacity;

			public long Resume;

			public long Delivered;

			public int IndexStride;

			public int IndexPtrOffset;

			public int DataStride;

			public int DataPtrOffset;
		}

		[global::System.Serializable]
		public struct Parameters : global::Unity.Networking.Transport.INetworkParameter
		{
			public int WindowSize;

			public int MinimumResendTime;

			public int MaximumResendTime;

			public bool Validate()
			{
				bool result = true;
				if (WindowSize < 0 || WindowSize > 2040)
				{
					result = false;
					global::UnityEngine.Debug.LogError(string.Format("{0} value ({1}) must be between 0 and {2}.", "WindowSize", WindowSize, 2040));
				}
				if (MinimumResendTime <= 0)
				{
					result = false;
					global::UnityEngine.Debug.LogError(string.Format("{0} value ({1}) must be positive.", "MinimumResendTime", MinimumResendTime));
				}
				if (MaximumResendTime <= 0)
				{
					result = false;
					global::UnityEngine.Debug.LogError(string.Format("{0} value ({1}) must be positive.", "MaximumResendTime", MaximumResendTime));
				}
				if (MaximumResendTime <= MinimumResendTime)
				{
					result = false;
					global::UnityEngine.Debug.LogError("MaximumResendTime must be greater than MinimumResendTime.");
				}
				return result;
			}
		}

		internal struct PacketHeader
		{
			public byte Type;

			public byte AckMaskLength;

			public ushort ProcessingTime;

			public ushort SequenceId;

			public ushort AckedSequenceId;
		}

		internal struct PacketInformation
		{
			public long SequenceId;

			public ushort Size;

			public ushort HeaderPadding;

			public long SendTime;
		}

		internal struct PacketTimers
		{
			public ushort ProcessingTime;

			public ushort Padding;

			public long SequenceId;

			public long SentTime;

			public long ReceiveTime;
		}

		internal const long NullEntry = -1L;

		public const int DefaultMinimumResendTime = 64;

		public const int DefaultMaximumResendTime = 200;

		[global::System.Obsolete("Renamed to DefaultMaximumResendTime. (UnityUpgradable) -> DefaultMaximumResendTime")]
		public const int MaximumResendTime = 200;

		internal const int MaxDuplicatesSinceLastAck = 3;

		internal const int MaxWindowSize = 2040;

		private static int AlignedSizeOf<T>() where T : struct
		{
			return (global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>() + 7) & -8;
		}

		internal static int MaxPacketHeaderWireSize(int windowSize)
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader>();
			int num2 = ((windowSize <= 32) ? 4 : ((windowSize <= 64) ? 8 : (windowSize / 8)));
			return num + num2;
		}

		internal static int SharedCapacityNeeded(global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters param)
		{
			int num = AlignedSizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers>() * param.WindowSize * 2;
			return AlignedSizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext>() + num;
		}

		internal static int ProcessCapacityNeeded(global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters param)
		{
			int num = AlignedSizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketInformation>();
			int num2 = 1472;
			num *= param.WindowSize;
			num2 *= param.WindowSize;
			return AlignedSizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.Context>() + num + num2;
		}

		internal unsafe static global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext InitializeContext(byte* sharedBuffer, int sharedBufferLength, byte* sendBuffer, int sendBufferLength, byte* recvBuffer, int recvBufferLength, global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters param)
		{
			InitializeProcessContext(sendBuffer, sendBufferLength, param);
			InitializeProcessContext(recvBuffer, recvBufferLength, param);
			*(global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer = new global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext
			{
				WindowSize = param.WindowSize,
				SentPackets = new global::Unity.Networking.Transport.Utilities.ReliableUtility.SequenceBufferContext
				{
					Acked = -1L
				},
				MinimumResendTime = param.MinimumResendTime,
				MaximumResendTime = param.MaximumResendTime,
				ReceivedPackets = new global::Unity.Networking.Transport.Utilities.ReliableUtility.SequenceBufferContext
				{
					Sequence = -1L,
					AckMask = global::Unity.Networking.Transport.Utilities.ReliableAckMask.AllAcked
				},
				RttInfo = new global::Unity.Networking.Transport.Utilities.ReliableUtility.RTTInfo
				{
					SmoothedVariance = 5f,
					SmoothedRtt = 50f,
					ResendTimeout = 50,
					LastRtt = 50
				},
				TimerDataOffset = AlignedSizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext>(),
				TimerDataStride = AlignedSizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers>(),
				RemoteTimerDataOffset = AlignedSizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext>() + AlignedSizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers>() * param.WindowSize,
				RemoteTimerDataStride = AlignedSizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers>()
			};
			return *(global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer;
		}

		internal unsafe static int InitializeProcessContext(byte* buffer, int bufferLength, global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters param)
		{
			int num = ProcessCapacityNeeded(param);
			if (bufferLength != num)
			{
				throw new global::System.InvalidOperationException("Insufficient memory to initialize reliable pipeline.");
			}
			((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)buffer)->Capacity = param.WindowSize;
			((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)buffer)->IndexStride = AlignedSizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketInformation>();
			((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)buffer)->IndexPtrOffset = AlignedSizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.Context>();
			((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)buffer)->DataStride = 1472;
			((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)buffer)->DataPtrOffset = ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)buffer)->IndexPtrOffset + ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)buffer)->IndexStride * ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)buffer)->Capacity;
			((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)buffer)->Resume = -1L;
			((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)buffer)->Delivered = -1L;
			Release(buffer, 0L, param.WindowSize);
			return 0;
		}

		internal unsafe static void SetPacket(byte* self, long sequence, global::Unity.Networking.Transport.InboundRecvBuffer data)
		{
			SetPacket(self, sequence, data.buffer, data.bufferLength);
		}

		internal unsafe static void SetPacket(byte* self, long sequence, void* data, int length)
		{
			if (length <= ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->DataStride)
			{
				_ = sequence % ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->Capacity;
				global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketInformation* packetInformation = GetPacketInformation(self, sequence);
				packetInformation->SequenceId = sequence;
				packetInformation->Size = (ushort)length;
				packetInformation->HeaderPadding = 0;
				packetInformation->SendTime = -1L;
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(GetPacketPtr((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self, sequence), data, length);
			}
		}

		internal unsafe static void SetPacket(byte* self, long sequence, global::Unity.Networking.Transport.InboundSendBuffer data, long timestamp)
		{
			int num = data.bufferLength + data.headerPadding;
			if (num <= ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->DataStride)
			{
				_ = sequence % ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->Capacity;
				global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketInformation* packetInformation = GetPacketInformation(self, sequence);
				packetInformation->SequenceId = sequence;
				packetInformation->Size = (ushort)num;
				packetInformation->HeaderPadding = (ushort)data.headerPadding;
				packetInformation->SendTime = timestamp;
				if (data.bufferLength > 0)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(GetPacketPtr((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self, sequence) + data.headerPadding, data.buffer, data.bufferLength);
				}
			}
		}

		internal unsafe static global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketInformation* GetPacketInformation(byte* self, long sequence)
		{
			long num = sequence % ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->Capacity;
			return (global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketInformation*)(self + ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->IndexPtrOffset + num * ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->IndexStride);
		}

		internal unsafe static byte* GetPacketPtr(global::Unity.Networking.Transport.Utilities.ReliableUtility.Context* ctx, long sequence)
		{
			long num = sequence % ctx->Capacity;
			long num2 = ctx->DataPtrOffset + num * ctx->DataStride;
			return (byte*)ctx + num2;
		}

		internal unsafe static bool TryAquire(byte* self, long sequence)
		{
			long index = sequence % ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->Capacity;
			if (GetIndex(self, index) == -1)
			{
				SetIndex(self, index, sequence);
				return true;
			}
			return false;
		}

		internal unsafe static void Release(byte* self, long sequence)
		{
			Release(self, sequence, 1);
		}

		internal unsafe static void Release(byte* self, long start_sequence, int count)
		{
			for (int i = 0; i < count; i++)
			{
				SetIndex(self, (start_sequence + i) % ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->Capacity, -1L);
			}
		}

		private unsafe static void SetIndex(byte* self, long index, long sequence)
		{
			long* ptr = (long*)(self + ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->IndexPtrOffset + index * ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->IndexStride);
			*ptr = sequence;
		}

		private unsafe static long GetIndex(byte* self, long index)
		{
			long* ptr = (long*)(self + ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->IndexPtrOffset + index * ((global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)self)->IndexStride);
			return *ptr;
		}

		internal unsafe static void ReleaseAcknowledgedPackets(global::Unity.Networking.Transport.NetworkPipelineContext context)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)context.internalSharedProcessBuffer;
			global::Unity.Networking.Transport.Utilities.ReliableAckMask ackMask = internalSharedProcessBuffer->SentPackets.AckMask;
			long acked = internalSharedProcessBuffer->SentPackets.Acked;
			for (int i = 0; i < internalSharedProcessBuffer->WindowSize; i++)
			{
				global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketInformation* packetInformation = GetPacketInformation(context.internalProcessBuffer, i);
				if (packetInformation->SequenceId >= 0 && packetInformation->SequenceId <= acked)
				{
					long num = global::Unity.Mathematics.math.abs(acked - packetInformation->SequenceId);
					if (num >= internalSharedProcessBuffer->WindowSize || ackMask.IsAcked((int)num))
					{
						Release(context.internalProcessBuffer, packetInformation->SequenceId);
						packetInformation->SendTime = -1L;
					}
				}
			}
		}

		internal unsafe static long GetNextSendResumeSequence(global::Unity.Networking.Transport.NetworkPipelineContext context)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)context.internalSharedProcessBuffer;
			long num = -1L;
			for (int i = 0; i < internalSharedProcessBuffer->WindowSize; i++)
			{
				global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketInformation* packetInformation = GetPacketInformation(context.internalProcessBuffer, i);
				if (packetInformation->SequenceId >= 0)
				{
					int num2 = CurrentResendTime(context.internalSharedProcessBuffer);
					if (context.timestamp > packetInformation->SendTime + num2 && (num == -1 || packetInformation->SequenceId < num))
					{
						num = packetInformation->SequenceId;
					}
				}
			}
			return num;
		}

		internal unsafe static bool NeedResumeReceive(global::Unity.Networking.Transport.NetworkPipelineContext context)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.Context* internalProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)context.internalProcessBuffer;
			long num = internalProcessBuffer->Delivered + 1;
			return GetPacketInformation(context.internalProcessBuffer, num)->SequenceId == num;
		}

		internal unsafe static global::Unity.Networking.Transport.InboundRecvBuffer ResumeReceive(global::Unity.Networking.Transport.NetworkPipelineContext context)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)context.internalSharedProcessBuffer;
			global::Unity.Networking.Transport.Utilities.ReliableUtility.Context* internalProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)context.internalProcessBuffer;
			long num = internalProcessBuffer->Delivered + 1;
			global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketInformation* packetInformation = GetPacketInformation(context.internalProcessBuffer, num);
			if (packetInformation->SequenceId == num)
			{
				long num2 = internalProcessBuffer->DataPtrOffset + num % internalProcessBuffer->Capacity * internalProcessBuffer->DataStride;
				global::Unity.Networking.Transport.InboundRecvBuffer result = new global::Unity.Networking.Transport.InboundRecvBuffer
				{
					buffer = context.internalProcessBuffer + num2,
					bufferLength = packetInformation->Size
				};
				internalProcessBuffer->Delivered = num;
				return result;
			}
			return default(global::Unity.Networking.Transport.InboundRecvBuffer);
		}

		internal unsafe static global::Unity.Networking.Transport.InboundSendBuffer ResumeSend(global::Unity.Networking.Transport.NetworkPipelineContext context)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)context.internalSharedProcessBuffer;
			global::Unity.Networking.Transport.Utilities.ReliableUtility.Context* internalProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)context.internalProcessBuffer;
			long resume = internalProcessBuffer->Resume;
			global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketInformation* packetInformation = GetPacketInformation(context.internalProcessBuffer, resume);
			packetInformation->SendTime = context.timestamp;
			long num = internalProcessBuffer->DataPtrOffset + resume % internalProcessBuffer->Capacity * internalProcessBuffer->DataStride;
			global::Unity.Networking.Transport.InboundSendBuffer result = default(global::Unity.Networking.Transport.InboundSendBuffer);
			result.bufferWithHeaders = context.internalProcessBuffer + num;
			result.bufferWithHeadersLength = packetInformation->Size;
			result.headerPadding = packetInformation->HeaderPadding;
			result.SetBufferFromBufferWithHeaders();
			internalSharedProcessBuffer->stats.PacketsResent++;
			return result;
		}

		internal unsafe static long Write(global::Unity.Networking.Transport.NetworkPipelineContext context, global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)context.internalSharedProcessBuffer;
			long sequence = internalSharedProcessBuffer->SentPackets.Sequence;
			if (!TryAquire(context.internalProcessBuffer, sequence))
			{
				return -5L;
			}
			internalSharedProcessBuffer->stats.PacketsSent++;
			internalSharedProcessBuffer->SentPackets.Sequence++;
			SetPacket(context.internalProcessBuffer, sequence, inboundBuffer, context.timestamp);
			StoreTimestamp(context.internalSharedProcessBuffer, sequence, context.timestamp);
			return sequence;
		}

		internal unsafe static int ReadHeader(global::System.ReadOnlySpan<byte> buffer, out global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader header, out global::Unity.Networking.Transport.Utilities.ReliableAckMask mask, int windowSize)
		{
			header = default(global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader);
			mask = default(global::Unity.Networking.Transport.Utilities.ReliableAckMask);
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader>();
			if (buffer.Length < num)
			{
				global::UnityEngine.Debug.LogError("Reliable pipeline: received an invalid header (too small).");
				return 0;
			}
			fixed (byte* source = buffer)
			{
				global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader packetHeader = default(global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(&packetHeader, source, num);
				header = packetHeader;
			}
			int num2 = header.AckMaskLength;
			if ((num2 == 0 && windowSize > 64) || (num2 != 0 && windowSize <= 64))
			{
				global::UnityEngine.Debug.LogError("Reliable pipeline: received an invalid header (ack mask length doesn't match window size).");
				return 0;
			}
			if (num2 == 0)
			{
				num2 = ((windowSize <= 32) ? 4 : 8);
			}
			if (buffer.Length < num + num2)
			{
				global::UnityEngine.Debug.LogError("Reliable pipeline: received an invalid header (too small).");
				return 0;
			}
			mask = global::Unity.Networking.Transport.Utilities.ReliableAckMask.FromBytes(buffer.Slice(num, num2));
			return num + num2;
		}

		internal unsafe static void WriteHeader(ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketType type, long sequence = -1L)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)ctx.internalSharedProcessBuffer;
			ushort num = (ushort)internalSharedProcessBuffer->ReceivedPackets.Sequence;
			global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader packetHeader = new global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader
			{
				Type = (byte)type,
				ProcessingTime = CalculateProcessingTime(ctx.internalSharedProcessBuffer, num, ctx.timestamp),
				SequenceId = (ushort)((type == global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketType.Ack) ? 0 : sequence),
				AckedSequenceId = num
			};
			int num2 = 0;
			if (internalSharedProcessBuffer->WindowSize <= 64)
			{
				num2 = ((internalSharedProcessBuffer->WindowSize <= 32) ? 4 : 8);
			}
			else
			{
				num2 = ((internalSharedProcessBuffer->SentPackets.Acked != -1) ? internalSharedProcessBuffer->ReceivedPackets.AckMask.MinimumWriteSize() : (internalSharedProcessBuffer->WindowSize / 8));
				packetHeader.AckMaskLength = (byte)num2;
			}
			global::Unity.Collections.LowLevel.Unsafe.DataStreamExtensions.WriteBytesUnsafe(ref ctx.header, (byte*)(&packetHeader), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader>());
			internalSharedProcessBuffer->ReceivedPackets.AckMask.WriteTo(ref ctx.header, num2);
		}

		internal unsafe static void UpdateContextAfterPacketSend(global::Unity.Networking.Transport.NetworkPipelineContext ctx)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)ctx.internalSharedProcessBuffer;
			internalSharedProcessBuffer->ReceivedPackets.Acked = internalSharedProcessBuffer->ReceivedPackets.Sequence;
			internalSharedProcessBuffer->ReceivedPackets.LastAckMask = internalSharedProcessBuffer->ReceivedPackets.AckMask;
			internalSharedProcessBuffer->DuplicatesSinceLastAck = 0;
		}

		internal unsafe static void StoreTimestamp(byte* sharedBuffer, long sequenceId, long timestamp)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers* localPacketTimer = GetLocalPacketTimer(sharedBuffer, sequenceId);
			localPacketTimer->SequenceId = sequenceId;
			localPacketTimer->SentTime = timestamp;
			localPacketTimer->ProcessingTime = 0;
			localPacketTimer->ReceiveTime = 0L;
		}

		internal unsafe static void StoreReceiveTimestamp(byte* sharedBuffer, long sequenceId, long timestamp, ushort processingTime)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.RTTInfo rttInfo = ((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->RttInfo;
			global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers* localPacketTimer = GetLocalPacketTimer(sharedBuffer, sequenceId);
			if (localPacketTimer != null && localPacketTimer->SequenceId == sequenceId && localPacketTimer->ReceiveTime <= 0)
			{
				localPacketTimer->ReceiveTime = timestamp;
				localPacketTimer->ProcessingTime = processingTime;
				rttInfo.LastRtt = (int)global::System.Math.Max(localPacketTimer->ReceiveTime - localPacketTimer->SentTime - localPacketTimer->ProcessingTime, 1L);
				float num = (float)rttInfo.LastRtt - rttInfo.SmoothedRtt;
				rttInfo.SmoothedRtt += num / 8f;
				rttInfo.SmoothedVariance += (global::Unity.Mathematics.math.abs(num) - rttInfo.SmoothedVariance) / 4f;
				rttInfo.ResendTimeout = (int)(rttInfo.SmoothedRtt + 4f * rttInfo.SmoothedVariance);
				((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->RttInfo = rttInfo;
			}
		}

		internal unsafe static void StoreRemoteReceiveTimestamp(byte* sharedBuffer, long sequenceId, long timestamp)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers* remotePacketTimer = GetRemotePacketTimer(sharedBuffer, sequenceId);
			remotePacketTimer->SequenceId = sequenceId;
			remotePacketTimer->ReceiveTime = timestamp;
		}

		private unsafe static int CurrentResendTime(byte* sharedBuffer)
		{
			if (((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->RttInfo.ResendTimeout > ((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->MaximumResendTime)
			{
				return ((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->MaximumResendTime;
			}
			return global::System.Math.Max(((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->RttInfo.ResendTimeout, ((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->MinimumResendTime);
		}

		internal unsafe static ushort CalculateProcessingTime(byte* sharedBuffer, long sequenceId, long timestamp)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers* remotePacketTimer = GetRemotePacketTimer(sharedBuffer, sequenceId);
			if (remotePacketTimer != null && remotePacketTimer->SequenceId == sequenceId)
			{
				return global::System.Math.Min((ushort)(timestamp - remotePacketTimer->ReceiveTime), ushort.MaxValue);
			}
			return 0;
		}

		internal unsafe static global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers* GetLocalPacketTimer(byte* sharedBuffer, long sequenceId)
		{
			long num = sequenceId % ((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->WindowSize;
			return (global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers*)((long)sharedBuffer + (long)((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->TimerDataOffset + ((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->TimerDataStride * num);
		}

		internal unsafe static global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers* GetRemotePacketTimer(byte* sharedBuffer, long sequenceId)
		{
			long num = sequenceId % ((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->WindowSize;
			return (global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketTimers*)((long)sharedBuffer + (long)((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->RemoteTimerDataOffset + ((global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)sharedBuffer)->RemoteTimerDataStride * num);
		}

		internal static long GetSequenceId64Bits(ref global::Unity.Networking.Transport.Utilities.ReliableUtility.SequenceBufferContext context, ushort sequenceId16Bits)
		{
			ushort num = (ushort)(sequenceId16Bits >> 14);
			ushort num2 = (ushort)(sequenceId16Bits & 0x3FFF);
			if (num == context.LastReceivedOverflowCycle + 1 || (num == 0 && context.LastReceivedOverflowCycle == 3))
			{
				context.NumberOfOverflowsDetected++;
			}
			long num3;
			if (num == context.LastReceivedOverflowCycle)
			{
				num3 = context.NumberOfOverflowsDetected;
			}
			else
			{
				if (num != context.LastReceivedOverflowCycle - 1 && (num != 3 || context.LastReceivedOverflowCycle != 0))
				{
					return -1L;
				}
				num3 = context.NumberOfOverflowsDetected - 1;
			}
			return (num3 << 14) | num2;
		}

		internal unsafe static long Read(global::Unity.Networking.Transport.NetworkPipelineContext context, global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader header, global::Unity.Networking.Transport.Utilities.ReliableAckMask mask)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)context.internalSharedProcessBuffer;
			internalSharedProcessBuffer->stats.PacketsReceived++;
			long sequenceId64Bits = GetSequenceId64Bits(ref internalSharedProcessBuffer->ReceivedPackets, header.SequenceId);
			if (sequenceId64Bits == -1)
			{
				internalSharedProcessBuffer->stats.PacketsStale++;
				return -1L;
			}
			bool flag = sequenceId64Bits > internalSharedProcessBuffer->ReceivedPackets.Sequence;
			int num = (int)global::Unity.Mathematics.math.abs(sequenceId64Bits - internalSharedProcessBuffer->ReceivedPackets.Sequence);
			if (!flag && num >= internalSharedProcessBuffer->WindowSize)
			{
				internalSharedProcessBuffer->stats.PacketsStale++;
				return -1L;
			}
			if (flag && num > internalSharedProcessBuffer->WindowSize)
			{
				return -1L;
			}
			if (flag)
			{
				internalSharedProcessBuffer->ReceivedPackets.Sequence = sequenceId64Bits;
				internalSharedProcessBuffer->ReceivedPackets.AckMask.Shift(num);
				internalSharedProcessBuffer->ReceivedPackets.AckMask.Ack(0);
				for (int i = 0; i < num; i++)
				{
					if (!internalSharedProcessBuffer->ReceivedPackets.AckMask.IsAcked(i))
					{
						internalSharedProcessBuffer->stats.PacketsDropped++;
					}
				}
			}
			else
			{
				if (internalSharedProcessBuffer->ReceivedPackets.AckMask.IsAcked(num))
				{
					ReadAckPacket(context, header, mask);
					internalSharedProcessBuffer->stats.PacketsDuplicated++;
					internalSharedProcessBuffer->DuplicatesSinceLastAck++;
					return -1L;
				}
				internalSharedProcessBuffer->ReceivedPackets.AckMask.Ack(num);
				internalSharedProcessBuffer->stats.PacketsOutOfOrder++;
			}
			StoreRemoteReceiveTimestamp(context.internalSharedProcessBuffer, sequenceId64Bits, context.timestamp);
			ReadAckPacket(context, header, mask);
			return sequenceId64Bits;
		}

		internal unsafe static void ReadAckPacket(global::Unity.Networking.Transport.NetworkPipelineContext context, global::Unity.Networking.Transport.Utilities.ReliableUtility.PacketHeader header, global::Unity.Networking.Transport.Utilities.ReliableAckMask mask)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)context.internalSharedProcessBuffer;
			StoreReceiveTimestamp(context.internalSharedProcessBuffer, header.AckedSequenceId, context.timestamp, header.ProcessingTime);
			long sequenceId64Bits = GetSequenceId64Bits(ref internalSharedProcessBuffer->SentPackets, header.AckedSequenceId);
			if (sequenceId64Bits != -1 && internalSharedProcessBuffer->SentPackets.Acked <= sequenceId64Bits)
			{
				if (internalSharedProcessBuffer->SentPackets.Acked == sequenceId64Bits)
				{
					internalSharedProcessBuffer->SentPackets.AckMask.Merge(mask);
					return;
				}
				internalSharedProcessBuffer->SentPackets.Acked = sequenceId64Bits;
				internalSharedProcessBuffer->SentPackets.AckMask = mask;
			}
		}

		internal unsafe static bool ShouldSendAck(global::Unity.Networking.Transport.NetworkPipelineContext ctx)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.Context* internalProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.Context*)ctx.internalProcessBuffer;
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* internalSharedProcessBuffer = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)ctx.internalSharedProcessBuffer;
			if (internalSharedProcessBuffer->ReceivedPackets.Acked >= internalSharedProcessBuffer->ReceivedPackets.Sequence && !(internalSharedProcessBuffer->ReceivedPackets.AckMask != internalSharedProcessBuffer->ReceivedPackets.LastAckMask))
			{
				return internalSharedProcessBuffer->DuplicatesSinceLastAck >= 3;
			}
			return true;
		}

		public unsafe static void SetMinimumResendTime(int value, global::Unity.Networking.Transport.NetworkDriver driver, global::Unity.Networking.Transport.NetworkPipeline pipeline, global::Unity.Networking.Transport.NetworkConnection connection)
		{
			global::Unity.Networking.Transport.NetworkPipelineStageId stageId = global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Networking.Transport.ReliableSequencedPipelineStage>();
			driver.GetPipelineBuffers(pipeline, stageId, connection, out var _, out var _, out var sharedBuffer);
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* unsafePtr = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(sharedBuffer);
			unsafePtr->MinimumResendTime = value;
		}

		public unsafe static void SetMaximumResendTime(int value, global::Unity.Networking.Transport.NetworkDriver driver, global::Unity.Networking.Transport.NetworkPipeline pipeline, global::Unity.Networking.Transport.NetworkConnection connection)
		{
			global::Unity.Networking.Transport.NetworkPipelineStageId stageId = global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Networking.Transport.ReliableSequencedPipelineStage>();
			driver.GetPipelineBuffers(pipeline, stageId, connection, out var _, out var _, out var sharedBuffer);
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* unsafePtr = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(sharedBuffer);
			unsafePtr->MaximumResendTime = value;
		}
	}
}
