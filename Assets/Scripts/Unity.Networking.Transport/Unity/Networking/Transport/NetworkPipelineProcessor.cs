namespace Unity.Networking.Transport
{
	internal struct NetworkPipelineProcessor : global::System.IDisposable
	{
		internal struct UpdatePipeline : global::System.IEquatable<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline>
		{
			public global::Unity.Networking.Transport.NetworkPipeline pipeline;

			public int stage;

			public global::Unity.Networking.Transport.NetworkConnection connection;

			public override int GetHashCode()
			{
				return (((pipeline.Id << 8) ^ stage) << 16) ^ connection.GetHashCode();
			}

			public bool Equals(global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline other)
			{
				if (pipeline.Id == other.pipeline.Id && stage == other.stage)
				{
					return connection == other.connection;
				}
				return false;
			}

			public override bool Equals(object other)
			{
				return Equals((global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline)other);
			}

			public static bool operator ==(global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline lhs, global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline rhs)
			{
				return lhs.Equals(rhs);
			}

			public static bool operator !=(global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline lhs, global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline rhs)
			{
				return !lhs.Equals(rhs);
			}
		}

		public struct Concurrent
		{
			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkPipelineStage> m_StageStructs;

			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Collections.NativeList<byte> m_StaticInstanceBuffer;

			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkPipelineProcessor.PipelineImpl> m_Pipelines;

			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Collections.NativeList<int> m_PipelineStagesIndices;

			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Collections.NativeList<int> m_AccumulatedHeaderCapacity;

			internal global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline>.ParallelWriter m_SendStageNeedsUpdateWrite;

			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Collections.NativeArray<int> sizePerConnection;

			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Collections.NativeList<byte> sharedBuffer;

			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Collections.NativeList<byte> sendBuffer;

			[global::Unity.Collections.ReadOnly]
			internal global::Unity.Collections.NativeArray<long> m_timestamp;

			internal int m_MaxPacketHeaderSize;

			public int SendHeaderCapacity(global::Unity.Networking.Transport.NetworkPipeline pipeline)
			{
				return m_Pipelines[pipeline.Id - 1].headerCapacity;
			}

			public int PayloadCapacity(global::Unity.Networking.Transport.NetworkPipeline pipeline)
			{
				if (pipeline.Id > 0)
				{
					return m_Pipelines[pipeline.Id - 1].payloadCapacity;
				}
				return 0;
			}

			public unsafe int Send(global::Unity.Networking.Transport.NetworkDriver.Concurrent driver, global::Unity.Networking.Transport.NetworkPipeline pipeline, global::Unity.Networking.Transport.NetworkConnection connection, global::Unity.Networking.Transport.NetworkInterfaceSendHandle sendHandle, int headerSize)
			{
				if (sendHandle.data == global::System.IntPtr.Zero)
				{
					return -8;
				}
				int internalId = connection.InternalId;
				int* unsafeReadOnlyPtr = (int*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(sendBuffer.AsArray());
				unsafeReadOnlyPtr += internalId * sizePerConnection[0] / 4;
				if (global::System.Threading.Interlocked.CompareExchange(ref *unsafeReadOnlyPtr, 1, 0) != 0)
				{
					driver.AbortSend(sendHandle);
					return -7;
				}
				global::Unity.Collections.NativeParallelHashSet<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline> currentUpdates = new global::Unity.Collections.NativeParallelHashSet<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline>(128, global::Unity.Collections.Allocator.Temp);
				int result = ProcessPipelineSend(driver, 0, pipeline, connection, sendHandle, headerSize, currentUpdates);
				global::System.Threading.Interlocked.Exchange(ref *unsafeReadOnlyPtr, 0);
				foreach (global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline item in currentUpdates)
				{
					m_SendStageNeedsUpdateWrite.Enqueue(item);
				}
				return result;
			}

			internal unsafe int ProcessPipelineSend(global::Unity.Networking.Transport.NetworkDriver.Concurrent driver, int startStage, global::Unity.Networking.Transport.NetworkPipeline pipeline, global::Unity.Networking.Transport.NetworkConnection connection, global::Unity.Networking.Transport.NetworkInterfaceSendHandle sendHandle, int headerSize, global::Unity.Collections.NativeParallelHashSet<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline> currentUpdates)
			{
				int num = headerSize;
				int num2 = sendHandle.size;
				global::Unity.Networking.Transport.NetworkPipelineContext ctx = new global::Unity.Networking.Transport.NetworkPipelineContext
				{
					maxMessageSize = driver.GetMaxSupportedMessageSize(connection),
					timestamp = m_timestamp[0]
				};
				global::Unity.Networking.Transport.NetworkPipelineProcessor.PipelineImpl p = m_Pipelines[pipeline.Id - 1];
				int internalId = connection.InternalId;
				bool flag = sendHandle.data == global::System.IntPtr.Zero;
				int num3 = 0;
				global::Unity.Collections.NativeList<int> resumeQ = new global::Unity.Collections.NativeList<int>(16, global::Unity.Collections.Allocator.Temp);
				int num4 = 0;
				global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer = default(global::Unity.Networking.Transport.InboundSendBuffer);
				if (!flag)
				{
					inboundBuffer.bufferWithHeaders = (byte*)(void*)sendHandle.data + num;
					inboundBuffer.bufferWithHeadersLength = sendHandle.size - num;
					inboundBuffer.buffer = inboundBuffer.bufferWithHeaders + p.headerCapacity;
					inboundBuffer.bufferLength = inboundBuffer.bufferWithHeadersLength - p.headerCapacity;
				}
				while (true)
				{
					headerSize = p.headerCapacity;
					int num5 = p.sendBufferOffset + sizePerConnection[0] * internalId;
					int num6 = p.sharedBufferOffset + sizePerConnection[2] * internalId;
					if (startStage > 0)
					{
						if (inboundBuffer.bufferWithHeadersLength > 0)
						{
							global::UnityEngine.Debug.LogError("Can't start from a stage with a buffer");
							return -3;
						}
						for (int i = 0; i < startStage; i++)
						{
							num5 += (m_StageStructs[m_PipelineStagesIndices[p.FirstStageIndex + i]].SendCapacity + 7) & -8;
							num6 += (m_StageStructs[m_PipelineStagesIndices[p.FirstStageIndex + i]].SharedStateCapacity + 7) & -8;
							headerSize -= m_StageStructs[m_PipelineStagesIndices[p.FirstStageIndex + i]].HeaderCapacity;
						}
					}
					for (int j = startStage; j < p.NumStages; j++)
					{
						int headerCapacity = m_StageStructs[m_PipelineStagesIndices[p.FirstStageIndex + j]].HeaderCapacity;
						inboundBuffer.headerPadding = headerSize;
						headerSize -= headerCapacity;
						if (headerCapacity > 0 && inboundBuffer.bufferWithHeadersLength > 0)
						{
							global::Unity.Collections.NativeArray<byte> data = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(inboundBuffer.bufferWithHeaders + headerSize, headerCapacity, global::Unity.Collections.Allocator.Invalid);
							ctx.header = new global::Unity.Collections.DataStreamWriter(data);
						}
						else
						{
							ctx.header = new global::Unity.Collections.DataStreamWriter(headerCapacity, global::Unity.Collections.Allocator.Temp);
						}
						global::Unity.Networking.Transport.InboundSendBuffer inboundSendBuffer = inboundBuffer;
						global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests = global::Unity.Networking.Transport.NetworkPipelineStage.Requests.None;
						int num7 = ProcessSendStage(j, num5, num6, p, ref resumeQ, ref ctx, ref inboundBuffer, ref requests, m_MaxPacketHeaderSize);
						if ((requests & global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Update) != global::Unity.Networking.Transport.NetworkPipelineStage.Requests.None)
						{
							currentUpdates.Add(new global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline
							{
								connection = connection,
								stage = j,
								pipeline = pipeline
							});
						}
						if (inboundBuffer.bufferWithHeadersLength == 0)
						{
							if ((requests & global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Error) != global::Unity.Networking.Transport.NetworkPipelineStage.Requests.None && !flag)
							{
								num2 = num7;
								num3 = num7;
							}
							break;
						}
						if (inboundBuffer.buffer != inboundSendBuffer.buffer)
						{
							global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(inboundBuffer.bufferWithHeaders + headerSize, global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(ctx.header.AsNativeArray()), ctx.header.Length);
						}
						if (ctx.header.Length < headerCapacity)
						{
							int num8 = headerCapacity - ctx.header.Length;
							global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(inboundBuffer.buffer - num8, inboundBuffer.buffer, inboundBuffer.bufferLength);
							inboundBuffer.bufferWithHeadersLength -= num8;
						}
						inboundBuffer.buffer = inboundBuffer.bufferWithHeaders + headerSize;
						inboundBuffer.bufferLength = ctx.header.Length + inboundBuffer.bufferLength;
						num5 += (ctx.internalProcessBufferLength + 7) & -8;
						num6 += (ctx.internalSharedProcessBufferLength + 7) & -8;
					}
					if (inboundBuffer.bufferLength != 0)
					{
						global::Unity.Collections.DataStreamWriter writer;
						if (sendHandle.data != global::System.IntPtr.Zero && inboundBuffer.bufferWithHeaders == (byte*)(void*)sendHandle.data + num)
						{
							if (inboundBuffer.buffer != inboundBuffer.bufferWithHeaders)
							{
								global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemMove(inboundBuffer.bufferWithHeaders, inboundBuffer.buffer, inboundBuffer.bufferLength);
								inboundBuffer.buffer = inboundBuffer.bufferWithHeaders;
							}
							int size = num + inboundBuffer.bufferLength;
							sendHandle.size = size;
							if ((num2 = driver.CompleteSend(connection, ref sendHandle)) < 0)
							{
								global::UnityEngine.Debug.LogWarning($"Sending from within pipeline failed with the following error code: {num2}");
							}
							else
							{
								driver.PrependPipelineByte(sendHandle, (byte)pipeline.Id);
							}
							sendHandle = default(global::Unity.Networking.Transport.NetworkInterfaceSendHandle);
						}
						else if (driver.BeginSend(connection, out writer) == 0)
						{
							global::Unity.Collections.LowLevel.Unsafe.DataStreamExtensions.WriteBytesUnsafe(ref writer, inboundBuffer.buffer, inboundBuffer.bufferLength);
							num2 = driver.ExtractPendingSendFromWriter(writer, out var pendingSend);
							if (num2 == 0)
							{
								pendingSend.SendHandle.size = num + writer.Length;
								num2 = driver.CompleteSend(connection, ref pendingSend.SendHandle);
								if (num2 >= 0)
								{
									driver.PrependPipelineByte(pendingSend.SendHandle, (byte)pipeline.Id);
								}
							}
							if (num2 < 0)
							{
								global::UnityEngine.Debug.LogWarning($"Sending from within pipeline failed with the following error code: {num2}");
							}
						}
					}
					if (num4 >= resumeQ.Length)
					{
						break;
					}
					startStage = resumeQ[num4++];
					inboundBuffer = default(global::Unity.Networking.Transport.InboundSendBuffer);
				}
				if (sendHandle.data != global::System.IntPtr.Zero)
				{
					driver.AbortSend(sendHandle);
				}
				if (num3 >= 0)
				{
					return num2;
				}
				return num3;
			}

			private unsafe int ProcessSendStage(int startStage, int internalBufferOffset, int internalSharedBufferOffset, global::Unity.Networking.Transport.NetworkPipelineProcessor.PipelineImpl p, ref global::Unity.Collections.NativeList<int> resumeQ, ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundSendBuffer inboundBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests, int systemHeaderSize)
			{
				int index = p.FirstStageIndex + startStage;
				global::Unity.Networking.Transport.NetworkPipelineStage networkPipelineStage = m_StageStructs[m_PipelineStagesIndices[index]];
				ctx.accumulatedHeaderCapacity = m_AccumulatedHeaderCapacity[index];
				ctx.staticInstanceBuffer = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafeReadOnlyPtr(m_StaticInstanceBuffer) + networkPipelineStage.StaticStateStart;
				ctx.staticInstanceBufferLength = networkPipelineStage.StaticStateCapacity;
				ctx.internalProcessBuffer = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafeReadOnlyPtr(sendBuffer) + internalBufferOffset;
				ctx.internalProcessBufferLength = networkPipelineStage.SendCapacity;
				ctx.internalSharedProcessBuffer = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafeReadOnlyPtr(sharedBuffer) + internalSharedBufferOffset;
				ctx.internalSharedProcessBufferLength = networkPipelineStage.SharedStateCapacity;
				requests = global::Unity.Networking.Transport.NetworkPipelineStage.Requests.None;
				int result = ((delegate* unmanaged[Cdecl]<ref global::Unity.Networking.Transport.NetworkPipelineContext, ref global::Unity.Networking.Transport.InboundSendBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests, int, int>)(void*)networkPipelineStage.Send.Ptr.Value)(ref ctx, ref inboundBuffer, ref requests, systemHeaderSize);
				if ((requests & global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Resume) != global::Unity.Networking.Transport.NetworkPipelineStage.Requests.None)
				{
					resumeQ.Add(in startStage);
				}
				return result;
			}
		}

		internal struct PipelineImpl
		{
			public int FirstStageIndex;

			public int NumStages;

			public int receiveBufferOffset;

			public int sendBufferOffset;

			public int sharedBufferOffset;

			public int headerCapacity;

			public int payloadCapacity;
		}

		public const int Alignment = 8;

		public const int AlignmentMinusOne = 7;

		private global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkPipelineStage> m_StageStructs;

		private global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkPipelineStageId> m_StageIds;

		private global::Unity.Collections.NativeList<int> m_PipelineStagesIndices;

		private global::Unity.Collections.NativeList<int> m_AccumulatedHeaderCapacity;

		private global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkPipelineProcessor.PipelineImpl> m_Pipelines;

		private global::Unity.Collections.NativeList<byte> m_StaticInstanceBuffer;

		private global::Unity.Collections.NativeList<byte> m_ReceiveBuffer;

		private global::Unity.Collections.NativeList<byte> m_SendBuffer;

		private global::Unity.Collections.NativeList<byte> m_SharedBuffer;

		private global::Unity.Collections.NativeParallelHashSet<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline> m_ReceiveStageNeedsUpdate;

		private global::Unity.Collections.NativeParallelHashSet<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline> m_SendStageNeedsUpdate;

		private global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline> m_SendStageNeedsUpdateRead;

		private global::Unity.Collections.NativeArray<int> sizePerConnection;

		private global::Unity.Collections.NativeArray<long> m_timestamp;

		private int m_MaxPacketHeaderSize;

		private const int SendSizeOffset = 0;

		private const int RecveiveSizeOffset = 1;

		private const int SharedSizeOffset = 2;

		internal int PipelineCount => m_Pipelines.Length;

		public long Timestamp
		{
			get
			{
				return m_timestamp[0];
			}
			internal set
			{
				m_timestamp[0] = value;
			}
		}

		public int PayloadCapacity(global::Unity.Networking.Transport.NetworkPipeline pipeline)
		{
			if (pipeline.Id > 0)
			{
				return m_Pipelines[pipeline.Id - 1].payloadCapacity;
			}
			return 0;
		}

		public global::Unity.Networking.Transport.NetworkPipelineProcessor.Concurrent ToConcurrent()
		{
			return new global::Unity.Networking.Transport.NetworkPipelineProcessor.Concurrent
			{
				m_StageStructs = m_StageStructs,
				m_StaticInstanceBuffer = m_StaticInstanceBuffer,
				m_Pipelines = m_Pipelines,
				m_PipelineStagesIndices = m_PipelineStagesIndices,
				m_AccumulatedHeaderCapacity = m_AccumulatedHeaderCapacity,
				m_SendStageNeedsUpdateWrite = m_SendStageNeedsUpdateRead.AsParallelWriter(),
				sizePerConnection = sizePerConnection,
				sendBuffer = m_SendBuffer,
				sharedBuffer = m_SharedBuffer,
				m_timestamp = m_timestamp,
				m_MaxPacketHeaderSize = m_MaxPacketHeaderSize
			};
		}

		public NetworkPipelineProcessor(global::Unity.Networking.Transport.NetworkSettings settings, int maxPacketHeaderSize)
		{
			m_StaticInstanceBuffer = new global::Unity.Collections.NativeList<byte>(0, global::Unity.Collections.Allocator.Persistent);
			m_StageStructs = new global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkPipelineStage>(8, global::Unity.Collections.Allocator.Persistent);
			m_StageIds = new global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkPipelineStageId>(8, global::Unity.Collections.Allocator.Persistent);
			m_PipelineStagesIndices = new global::Unity.Collections.NativeList<int>(16, global::Unity.Collections.Allocator.Persistent);
			m_AccumulatedHeaderCapacity = new global::Unity.Collections.NativeList<int>(16, global::Unity.Collections.Allocator.Persistent);
			m_Pipelines = new global::Unity.Collections.NativeList<global::Unity.Networking.Transport.NetworkPipelineProcessor.PipelineImpl>(16, global::Unity.Collections.Allocator.Persistent);
			m_ReceiveBuffer = new global::Unity.Collections.NativeList<byte>(0, global::Unity.Collections.Allocator.Persistent);
			m_SendBuffer = new global::Unity.Collections.NativeList<byte>(0, global::Unity.Collections.Allocator.Persistent);
			m_SharedBuffer = new global::Unity.Collections.NativeList<byte>(0, global::Unity.Collections.Allocator.Persistent);
			sizePerConnection = new global::Unity.Collections.NativeArray<int>(3, global::Unity.Collections.Allocator.Persistent);
			sizePerConnection[0] = 8;
			m_ReceiveStageNeedsUpdate = new global::Unity.Collections.NativeParallelHashSet<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline>(128, global::Unity.Collections.Allocator.Persistent);
			m_SendStageNeedsUpdate = new global::Unity.Collections.NativeParallelHashSet<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline>(128, global::Unity.Collections.Allocator.Persistent);
			m_SendStageNeedsUpdateRead = new global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline>(global::Unity.Collections.Allocator.Persistent);
			m_timestamp = new global::Unity.Collections.NativeArray<long>(1, global::Unity.Collections.Allocator.Persistent);
			m_MaxPacketHeaderSize = maxPacketHeaderSize;
			RegisterPipelineStage(default(global::Unity.Networking.Transport.NullPipelineStage), settings);
			RegisterPipelineStage(default(global::Unity.Networking.Transport.FragmentationPipelineStage), settings);
			RegisterPipelineStage(default(global::Unity.Networking.Transport.ReliableSequencedPipelineStage), settings);
			RegisterPipelineStage(default(global::Unity.Networking.Transport.UnreliableSequencedPipelineStage), settings);
			RegisterPipelineStage(default(global::Unity.Networking.Transport.SimulatorPipelineStage), settings);
			RegisterPipelineStage(default(global::Unity.Networking.Transport.SimulatorPipelineStageInSend), settings);
		}

		public void Dispose()
		{
			m_PipelineStagesIndices.Dispose();
			m_AccumulatedHeaderCapacity.Dispose();
			m_ReceiveBuffer.Dispose();
			m_SendBuffer.Dispose();
			m_SharedBuffer.Dispose();
			m_Pipelines.Dispose();
			sizePerConnection.Dispose();
			m_ReceiveStageNeedsUpdate.Dispose();
			m_SendStageNeedsUpdate.Dispose();
			m_SendStageNeedsUpdateRead.Dispose();
			m_timestamp.Dispose();
			m_StageStructs.Dispose();
			m_StageIds.Dispose();
			m_StaticInstanceBuffer.Dispose();
		}

		public unsafe void RegisterPipelineStage<T>(T stage, global::Unity.Networking.Transport.NetworkSettings settings) where T : unmanaged, global::Unity.Networking.Transport.INetworkPipelineStage
		{
			int length = m_StaticInstanceBuffer.Length;
			if (stage.StaticSize > 0)
			{
				int num = length + stage.StaticSize;
				num = (num + 15) & -16;
				m_StaticInstanceBuffer.ResizeUninitialized(num);
			}
			int num2 = length;
			byte* staticInstanceBuffer = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_StaticInstanceBuffer) + num2;
			global::Unity.Networking.Transport.NetworkPipelineStage value = stage.StaticInitialize(staticInstanceBuffer, stage.StaticSize, settings);
			value.StaticStateStart = num2;
			value.StaticStateCapacity = stage.StaticSize;
			m_StageStructs.Add(in value);
			m_StageIds.Add(global::Unity.Networking.Transport.NetworkPipelineStageId.Get<T>());
		}

		public unsafe void InitializeConnection(global::Unity.Networking.Transport.NetworkConnection con)
		{
			int num = (con.InternalId + 1) * sizePerConnection[1];
			int num2 = (con.InternalId + 1) * sizePerConnection[0];
			int num3 = (con.InternalId + 1) * sizePerConnection[2];
			if (m_ReceiveBuffer.Length < num)
			{
				m_ReceiveBuffer.ResizeUninitialized(num);
			}
			if (m_SendBuffer.Length < num2)
			{
				m_SendBuffer.ResizeUninitialized(num2);
			}
			if (m_SharedBuffer.Length < num3)
			{
				m_SharedBuffer.ResizeUninitialized(num3);
			}
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_ReceiveBuffer) + con.InternalId * sizePerConnection[1], sizePerConnection[1]);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_SendBuffer) + con.InternalId * sizePerConnection[0], sizePerConnection[0]);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_SharedBuffer) + con.InternalId * sizePerConnection[2], sizePerConnection[2]);
			InitializeStages(con.InternalId);
		}

		private unsafe void InitializeStages(int networkId)
		{
			for (int i = 0; i < m_Pipelines.Length; i++)
			{
				global::Unity.Networking.Transport.NetworkPipelineProcessor.PipelineImpl pipelineImpl = m_Pipelines[i];
				int num = pipelineImpl.receiveBufferOffset + sizePerConnection[1] * networkId;
				int num2 = pipelineImpl.sendBufferOffset + sizePerConnection[0] * networkId;
				int num3 = pipelineImpl.sharedBufferOffset + sizePerConnection[2] * networkId;
				for (int j = pipelineImpl.FirstStageIndex; j < pipelineImpl.FirstStageIndex + pipelineImpl.NumStages; j++)
				{
					global::Unity.Networking.Transport.NetworkPipelineStage networkPipelineStage = m_StageStructs[m_PipelineStagesIndices[j]];
					byte* ptr = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_SendBuffer) + num2;
					int sendCapacity = networkPipelineStage.SendCapacity;
					byte* ptr2 = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_ReceiveBuffer) + num;
					int receiveCapacity = networkPipelineStage.ReceiveCapacity;
					byte* ptr3 = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_SharedBuffer) + num3;
					int sharedStateCapacity = networkPipelineStage.SharedStateCapacity;
					byte* ptr4 = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_StaticInstanceBuffer) + networkPipelineStage.StaticStateStart;
					int staticStateCapacity = networkPipelineStage.StaticStateCapacity;
					((delegate* unmanaged[Cdecl]<byte*, int, byte*, int, byte*, int, byte*, int, void>)(void*)networkPipelineStage.InitializeConnection.Ptr.Value)(ptr4, staticStateCapacity, ptr, sendCapacity, ptr2, receiveCapacity, ptr3, sharedStateCapacity);
					num2 += (sendCapacity + 7) & -8;
					num += (receiveCapacity + 7) & -8;
					num3 += (sharedStateCapacity + 7) & -8;
				}
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ValidateStages(global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineStageId> stages)
		{
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < stages.Length; i++)
			{
				if (stages[i] == global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Networking.Transport.ReliableSequencedPipelineStage>())
				{
					num = i;
				}
				if (stages[i] == global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Networking.Transport.FragmentationPipelineStage>())
				{
					num2 = i;
				}
			}
			if (num >= 0 && num2 >= 0 && num2 > num)
			{
				throw new global::System.InvalidOperationException("Cannot create pipeline with ReliableSequenced followed by Fragmentation stage. Should reverse their order.");
			}
		}

		public global::Unity.Networking.Transport.NetworkPipeline CreatePipeline(global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineStageId> stages)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int value = 0;
			int num4 = 0;
			global::Unity.Networking.Transport.NetworkPipelineProcessor.PipelineImpl value2 = new global::Unity.Networking.Transport.NetworkPipelineProcessor.PipelineImpl
			{
				FirstStageIndex = m_PipelineStagesIndices.Length,
				NumStages = stages.Length
			};
			for (int i = 0; i < stages.Length; i++)
			{
				int value3 = global::Unity.Collections.NativeListExtensions.IndexOf(m_StageIds, stages[i]);
				m_PipelineStagesIndices.Add(in value3);
				m_AccumulatedHeaderCapacity.Add(in value);
				num += (m_StageStructs[value3].ReceiveCapacity + 7) & -8;
				num3 += (m_StageStructs[value3].SendCapacity + 7) & -8;
				value += m_StageStructs[value3].HeaderCapacity;
				num2 += (m_StageStructs[value3].SharedStateCapacity + 7) & -8;
				if (num4 == 0)
				{
					num4 = m_StageStructs[value3].PayloadCapacity;
				}
			}
			value2.receiveBufferOffset = sizePerConnection[1];
			sizePerConnection[1] = sizePerConnection[1] + num;
			value2.sendBufferOffset = sizePerConnection[0];
			sizePerConnection[0] = sizePerConnection[0] + num3;
			value2.sharedBufferOffset = sizePerConnection[2];
			sizePerConnection[2] = sizePerConnection[2] + num2;
			value2.headerCapacity = value;
			value2.payloadCapacity = num4;
			m_Pipelines.Add(in value2);
			return new global::Unity.Networking.Transport.NetworkPipeline
			{
				Id = m_Pipelines.Length
			};
		}

		public void GetPipelineBuffers(global::Unity.Networking.Transport.NetworkPipeline pipelineId, global::Unity.Networking.Transport.NetworkPipelineStageId stageId, global::Unity.Networking.Transport.NetworkConnection connection, out global::Unity.Collections.NativeArray<byte> readProcessingBuffer, out global::Unity.Collections.NativeArray<byte> writeProcessingBuffer, out global::Unity.Collections.NativeArray<byte> sharedBuffer)
		{
			if (pipelineId.Id - 1 < 0 || pipelineId.Id - 1 >= m_Pipelines.Length)
			{
				writeProcessingBuffer = default(global::Unity.Collections.NativeArray<byte>);
				readProcessingBuffer = default(global::Unity.Collections.NativeArray<byte>);
				sharedBuffer = default(global::Unity.Collections.NativeArray<byte>);
				return;
			}
			global::Unity.Networking.Transport.NetworkPipelineProcessor.PipelineImpl pipelineImpl = m_Pipelines[pipelineId.Id - 1];
			int num = pipelineImpl.receiveBufferOffset + sizePerConnection[1] * connection.InternalId;
			int num2 = pipelineImpl.sendBufferOffset + sizePerConnection[0] * connection.InternalId;
			int num3 = pipelineImpl.sharedBufferOffset + sizePerConnection[2] * connection.InternalId;
			bool flag = true;
			int i;
			for (i = pipelineImpl.FirstStageIndex; i < pipelineImpl.FirstStageIndex + pipelineImpl.NumStages; i++)
			{
				if (m_StageIds[m_PipelineStagesIndices[i]] == stageId)
				{
					flag = true;
					break;
				}
				num2 += (m_StageStructs[m_PipelineStagesIndices[i]].SendCapacity + 7) & -8;
				num += (m_StageStructs[m_PipelineStagesIndices[i]].ReceiveCapacity + 7) & -8;
				num3 += (m_StageStructs[m_PipelineStagesIndices[i]].SharedStateCapacity + 7) & -8;
			}
			if (!flag)
			{
				writeProcessingBuffer = default(global::Unity.Collections.NativeArray<byte>);
				readProcessingBuffer = default(global::Unity.Collections.NativeArray<byte>);
				sharedBuffer = default(global::Unity.Collections.NativeArray<byte>);
			}
			else
			{
				writeProcessingBuffer = m_SendBuffer.AsArray().GetSubArray(num2, m_StageStructs[m_PipelineStagesIndices[i]].SendCapacity);
				readProcessingBuffer = m_ReceiveBuffer.AsArray().GetSubArray(num, m_StageStructs[m_PipelineStagesIndices[i]].ReceiveCapacity);
				sharedBuffer = m_SharedBuffer.AsArray().GetSubArray(num3, m_StageStructs[m_PipelineStagesIndices[i]].SharedStateCapacity);
			}
		}

		internal unsafe T* GetWriteablePipelineParameter<T>(global::Unity.Networking.Transport.NetworkPipelineStageId stageId) where T : unmanaged, global::Unity.Networking.Transport.INetworkParameter
		{
			int num = global::Unity.Collections.NativeListExtensions.IndexOf(m_StageIds, stageId);
			if (num == -1)
			{
				return null;
			}
			global::Unity.Networking.Transport.NetworkPipelineStage networkPipelineStage = m_StageStructs[num];
			byte* result = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_StaticInstanceBuffer) + networkPipelineStage.StaticStateStart;
			if (networkPipelineStage.StaticStateCapacity != sizeof(T))
			{
				return null;
			}
			return (T*)result;
		}

		internal unsafe void UpdateSend(global::Unity.Networking.Transport.NetworkDriver.Concurrent driver)
		{
			int* unsafePtr = (int*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_SendBuffer.AsArray());
			for (int i = 0; i < m_SendBuffer.Length; i += sizePerConnection[0])
			{
				unsafePtr[i / 4] = 0;
			}
			global::Unity.Networking.Transport.NetworkPipelineProcessor.Concurrent self = ToConcurrent();
			global::Unity.Collections.NativeParallelHashSet<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline> currentUpdates = new global::Unity.Collections.NativeParallelHashSet<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline>(128, global::Unity.Collections.Allocator.Temp);
			global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline item;
			while (m_SendStageNeedsUpdateRead.TryDequeue(out item))
			{
				ProcessSendUpdate(ref self, ref driver, item, currentUpdates, m_MaxPacketHeaderSize);
			}
			foreach (global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline item2 in m_SendStageNeedsUpdate)
			{
				ProcessSendUpdate(ref self, ref driver, item2, currentUpdates, m_MaxPacketHeaderSize);
			}
			m_SendStageNeedsUpdate.Clear();
			foreach (global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline item3 in currentUpdates)
			{
				m_SendStageNeedsUpdateRead.Enqueue(item3);
			}
		}

		private static void ProcessSendUpdate(ref global::Unity.Networking.Transport.NetworkPipelineProcessor.Concurrent self, ref global::Unity.Networking.Transport.NetworkDriver.Concurrent driver, global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline update, global::Unity.Collections.NativeParallelHashSet<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline> currentUpdates, int headerSize)
		{
			if (driver.GetConnectionState(update.connection) == global::Unity.Networking.Transport.NetworkConnection.State.Connected)
			{
				int num = self.ProcessPipelineSend(driver, update.stage, update.pipeline, update.connection, default(global::Unity.Networking.Transport.NetworkInterfaceSendHandle), headerSize, currentUpdates);
				if (num < 0)
				{
					global::UnityEngine.Debug.LogWarning($"Sending from within pipeline failed with the following error code: {num}");
				}
			}
		}

		public void UpdateReceive(ref global::Unity.Networking.Transport.NetworkDriver driver)
		{
			global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline> nativeArray = m_ReceiveStageNeedsUpdate.ToNativeArray(global::Unity.Collections.Allocator.Temp);
			int length = nativeArray.Length;
			m_ReceiveStageNeedsUpdate.Clear();
			for (int i = 0; i < length; i++)
			{
				global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline updatePipeline = nativeArray[i];
				if (driver.GetConnectionState(updatePipeline.connection) == global::Unity.Networking.Transport.NetworkConnection.State.Connected)
				{
					global::Unity.Networking.Transport.NetworkDriverReceiver receiver = driver.Receiver;
					global::Unity.Networking.Transport.NetworkEventQueue eventQueue = driver.EventQueue;
					ProcessReceiveStagesFrom(ref receiver, ref eventQueue, updatePipeline.stage, updatePipeline.pipeline, updatePipeline.connection, default(global::Unity.Networking.Transport.InboundRecvBuffer));
				}
			}
		}

		public unsafe void Receive(byte pipelineId, ref global::Unity.Networking.Transport.NetworkDriverReceiver receiver, ref global::Unity.Networking.Transport.NetworkEventQueue eventQueue, ref global::Unity.Networking.Transport.NetworkConnection connection, byte* buffer, int bufferLength)
		{
			if (pipelineId == 0 || pipelineId > m_Pipelines.Length)
			{
				global::UnityEngine.Debug.LogError($"Received a packet with an invalid pipeline ({pipelineId}, should be between 1 and {m_Pipelines.Length}). Possible mismatch between pipeline definitions on each end of the connection.");
				return;
			}
			int startStage = m_Pipelines[pipelineId - 1].NumStages - 1;
			global::Unity.Networking.Transport.InboundRecvBuffer buffer2 = default(global::Unity.Networking.Transport.InboundRecvBuffer);
			buffer2.buffer = buffer;
			buffer2.bufferLength = bufferLength;
			ProcessReceiveStagesFrom(ref receiver, ref eventQueue, startStage, new global::Unity.Networking.Transport.NetworkPipeline
			{
				Id = pipelineId
			}, connection, buffer2);
		}

		private unsafe void ProcessReceiveStagesFrom(ref global::Unity.Networking.Transport.NetworkDriverReceiver receiver, ref global::Unity.Networking.Transport.NetworkEventQueue eventQueue, int startStage, global::Unity.Networking.Transport.NetworkPipeline pipeline, global::Unity.Networking.Transport.NetworkConnection connection, global::Unity.Networking.Transport.InboundRecvBuffer buffer)
		{
			global::Unity.Networking.Transport.NetworkPipelineProcessor.PipelineImpl pipelineImpl = m_Pipelines[pipeline.Id - 1];
			int internalId = connection.InternalId;
			global::Unity.Collections.NativeList<int> resumeQ = new global::Unity.Collections.NativeList<int>(16, global::Unity.Collections.Allocator.Temp);
			int num = 0;
			int maxPacketHeaderSize = m_MaxPacketHeaderSize;
			global::Unity.Networking.Transport.InboundRecvBuffer inboundBuffer = buffer;
			global::Unity.Networking.Transport.NetworkPipelineContext ctx = new global::Unity.Networking.Transport.NetworkPipelineContext
			{
				timestamp = Timestamp,
				header = default(global::Unity.Collections.DataStreamWriter)
			};
			while (true)
			{
				bool needsUpdate = false;
				bool needsSendUpdate = false;
				int num2 = pipelineImpl.receiveBufferOffset + sizePerConnection[1] * internalId;
				int num3 = pipelineImpl.sharedBufferOffset + sizePerConnection[2] * internalId;
				for (int i = 0; i < startStage; i++)
				{
					num2 += (m_StageStructs[m_PipelineStagesIndices[pipelineImpl.FirstStageIndex + i]].ReceiveCapacity + 7) & -8;
					num3 += (m_StageStructs[m_PipelineStagesIndices[pipelineImpl.FirstStageIndex + i]].SharedStateCapacity + 7) & -8;
				}
				for (int num4 = startStage; num4 >= 0; num4--)
				{
					ProcessReceiveStage(num4, pipeline, num2, num3, ref ctx, ref inboundBuffer, ref resumeQ, ref needsUpdate, ref needsSendUpdate, maxPacketHeaderSize);
					if (needsUpdate)
					{
						m_ReceiveStageNeedsUpdate.Add(new global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline
						{
							connection = connection,
							stage = num4,
							pipeline = pipeline
						});
					}
					if (needsSendUpdate)
					{
						m_SendStageNeedsUpdate.Add(new global::Unity.Networking.Transport.NetworkPipelineProcessor.UpdatePipeline
						{
							connection = connection,
							stage = num4,
							pipeline = pipeline
						});
					}
					if (inboundBuffer.buffer == null)
					{
						break;
					}
					if (num4 > 0)
					{
						num2 -= (m_StageStructs[m_PipelineStagesIndices[pipelineImpl.FirstStageIndex + num4 - 1]].ReceiveCapacity + 7) & -8;
						num3 -= (m_StageStructs[m_PipelineStagesIndices[pipelineImpl.FirstStageIndex + num4 - 1]].SharedStateCapacity + 7) & -8;
					}
					needsUpdate = false;
				}
				if (inboundBuffer.buffer != null)
				{
					receiver.PushDataEvent(connection, pipeline.Id, inboundBuffer.buffer, inboundBuffer.bufferLength, ref eventQueue);
				}
				if (num >= resumeQ.Length)
				{
					break;
				}
				startStage = resumeQ[num++];
				inboundBuffer = default(global::Unity.Networking.Transport.InboundRecvBuffer);
			}
		}

		private unsafe void ProcessReceiveStage(int stage, global::Unity.Networking.Transport.NetworkPipeline pipeline, int internalBufferOffset, int internalSharedBufferOffset, ref global::Unity.Networking.Transport.NetworkPipelineContext ctx, ref global::Unity.Networking.Transport.InboundRecvBuffer inboundBuffer, ref global::Unity.Collections.NativeList<int> resumeQ, ref bool needsUpdate, ref bool needsSendUpdate, int systemHeadersSize)
		{
			global::Unity.Networking.Transport.NetworkPipelineProcessor.PipelineImpl pipelineImpl = m_Pipelines[pipeline.Id - 1];
			int index = m_PipelineStagesIndices[pipelineImpl.FirstStageIndex + stage];
			global::Unity.Networking.Transport.NetworkPipelineStage networkPipelineStage = m_StageStructs[index];
			ctx.staticInstanceBuffer = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_StaticInstanceBuffer) + networkPipelineStage.StaticStateStart;
			ctx.staticInstanceBufferLength = networkPipelineStage.StaticStateCapacity;
			ctx.internalProcessBuffer = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_ReceiveBuffer) + internalBufferOffset;
			ctx.internalProcessBufferLength = networkPipelineStage.ReceiveCapacity;
			ctx.internalSharedProcessBuffer = global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_SharedBuffer) + internalSharedBufferOffset;
			ctx.internalSharedProcessBufferLength = networkPipelineStage.SharedStateCapacity;
			global::Unity.Networking.Transport.NetworkPipelineStage.Requests requests = global::Unity.Networking.Transport.NetworkPipelineStage.Requests.None;
			((delegate* unmanaged[Cdecl]<ref global::Unity.Networking.Transport.NetworkPipelineContext, ref global::Unity.Networking.Transport.InboundRecvBuffer, ref global::Unity.Networking.Transport.NetworkPipelineStage.Requests, int, void>)(void*)networkPipelineStage.Receive.Ptr.Value)(ref ctx, ref inboundBuffer, ref requests, systemHeadersSize);
			if ((requests & global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Resume) != global::Unity.Networking.Transport.NetworkPipelineStage.Requests.None)
			{
				resumeQ.Add(in stage);
			}
			needsUpdate = (requests & global::Unity.Networking.Transport.NetworkPipelineStage.Requests.Update) != 0;
			needsSendUpdate = (requests & global::Unity.Networking.Transport.NetworkPipelineStage.Requests.SendUpdate) != 0;
		}
	}
}
