namespace Unity.Netcode
{
	internal class NetworkMessageManager : global::System.IDisposable
	{
		private struct ReceiveQueueItem
		{
			public global::Unity.Netcode.FastBufferReader Reader;

			public global::Unity.Netcode.NetworkMessageHeader Header;

			public ulong SenderId;

			public float Timestamp;

			public int MessageHeaderSerializedSize;
		}

		private struct SendQueueItem
		{
			public global::Unity.Netcode.NetworkBatchHeader BatchHeader;

			public global::Unity.Netcode.FastBufferWriter Writer;

			public readonly global::Unity.Netcode.NetworkDelivery NetworkDelivery;

			public SendQueueItem(global::Unity.Netcode.NetworkDelivery delivery, int writerSize, global::Unity.Collections.Allocator writerAllocator, int maxWriterSize = -1)
			{
				Writer = new global::Unity.Netcode.FastBufferWriter(writerSize, writerAllocator, maxWriterSize);
				NetworkDelivery = delivery;
				BatchHeader = new global::Unity.Netcode.NetworkBatchHeader
				{
					Magic = 4448
				};
			}
		}

		internal delegate void MessageHandler(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, global::Unity.Netcode.NetworkMessageManager manager);

		internal delegate int VersionGetter();

		internal struct MessageWithHandler
		{
			public global::System.Type MessageType;

			public global::Unity.Netcode.NetworkMessageManager.MessageHandler Handler;

			public global::Unity.Netcode.NetworkMessageManager.VersionGetter GetVersion;
		}

		private struct PointerListWrapper<T> : global::System.Collections.Generic.IReadOnlyList<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable, global::System.Collections.Generic.IReadOnlyCollection<T> where T : unmanaged
		{
			private unsafe T* m_Value;

			private int m_Length;

			public int Count
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Length;
				}
			}

			public unsafe T this[int index]
			{
				[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
				get
				{
					return m_Value[index];
				}
			}

			internal unsafe PointerListWrapper(T* ptr, int length)
			{
				m_Value = ptr;
				m_Length = length;
			}

			public global::System.Collections.Generic.IEnumerator<T> GetEnumerator()
			{
				throw new global::System.NotImplementedException();
			}

			global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		public bool StopProcessing;

		private static global::System.Type s_ConnectionApprovedType = typeof(global::Unity.Netcode.ConnectionApprovedMessage);

		private static global::System.Type s_ConnectionRequestType = typeof(global::Unity.Netcode.ConnectionRequestMessage);

		private static global::System.Type s_DisconnectReasonType = typeof(global::Unity.Netcode.DisconnectReasonMessage);

		private global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkMessageManager.ReceiveQueueItem> m_IncomingMessageQueue = new global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkMessageManager.ReceiveQueueItem>(16, global::Unity.Collections.Allocator.Persistent);

		private global::Unity.Netcode.NetworkMessageManager.MessageHandler[] m_MessageHandlers = new global::Unity.Netcode.NetworkMessageManager.MessageHandler[4];

		private global::System.Type[] m_ReverseTypeMap = new global::System.Type[4];

		private global::System.Collections.Generic.Dictionary<global::System.Type, uint> m_MessageTypes = new global::System.Collections.Generic.Dictionary<global::System.Type, uint>();

		private global::System.Collections.Generic.Dictionary<ulong, global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkMessageManager.SendQueueItem>> m_SendQueues = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkMessageManager.SendQueueItem>>();

		private global::System.Collections.Generic.HashSet<ulong> m_DisconnectedClients = new global::System.Collections.Generic.HashSet<ulong>();

		private global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.Dictionary<global::System.Type, int>> m_PerClientMessageVersions = new global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.Dictionary<global::System.Type, int>>();

		private global::System.Collections.Generic.Dictionary<uint, global::System.Type> m_MessagesByHash = new global::System.Collections.Generic.Dictionary<uint, global::System.Type>();

		private global::System.Collections.Generic.Dictionary<global::System.Type, int> m_LocalVersions = new global::System.Collections.Generic.Dictionary<global::System.Type, int>();

		private global::System.Collections.Generic.List<global::Unity.Netcode.INetworkHooks> m_Hooks = new global::System.Collections.Generic.List<global::Unity.Netcode.INetworkHooks>();

		private uint m_HighMessageType;

		private object m_Owner;

		private global::Unity.Netcode.INetworkMessageSender m_Sender;

		private bool m_Disposed;

		private ulong m_LocalClientId;

		public const int DefaultNonFragmentedMessageMaxSize = 1296;

		public int NonFragmentedMessageMaxSize = 1296;

		public int FragmentedMessageMaxSize = int.MaxValue;

		public global::System.Collections.Generic.Dictionary<ulong, int> PeerMTUSizes = new global::System.Collections.Generic.Dictionary<ulong, int>();

		internal static bool EnableMessageOrderConsoleLog = false;

		internal global::System.Type[] MessageTypes => m_ReverseTypeMap;

		internal global::Unity.Netcode.NetworkMessageManager.MessageHandler[] MessageHandlers => m_MessageHandlers;

		internal uint MessageHandlerCount => m_HighMessageType;

		internal uint GetMessageType(global::System.Type t)
		{
			return m_MessageTypes[t];
		}

		internal object GetOwner()
		{
			return m_Owner;
		}

		internal void SetLocalClientId(ulong id)
		{
			m_LocalClientId = id;
		}

		public NetworkMessageManager(global::Unity.Netcode.INetworkMessageSender sender, object owner, global::Unity.Netcode.INetworkMessageProvider provider = null)
		{
			try
			{
				m_Sender = sender;
				m_Owner = owner;
				if (provider == null)
				{
					provider = default(global::Unity.Netcode.ILPPMessageProvider);
				}
				foreach (global::Unity.Netcode.NetworkMessageManager.MessageWithHandler message in provider.GetMessages())
				{
					RegisterMessageType(message);
				}
			}
			catch (global::System.Exception)
			{
				Dispose();
				throw;
			}
		}

		public void Dispose()
		{
			if (m_Disposed)
			{
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkMessageManager.SendQueueItem>> sendQueue in m_SendQueues)
			{
				ClientDisconnected(sendQueue.Key);
			}
			CleanupDisconnectedClients();
			for (int i = 0; i < m_IncomingMessageQueue.Length; i++)
			{
				m_IncomingMessageQueue.ElementAt(i).Reader.Dispose();
			}
			m_IncomingMessageQueue.Dispose();
			m_Disposed = true;
		}

		~NetworkMessageManager()
		{
			Dispose();
		}

		public void Hook(global::Unity.Netcode.INetworkHooks hooks)
		{
			m_Hooks.Add(hooks);
		}

		public void Unhook(global::Unity.Netcode.INetworkHooks hooks)
		{
			m_Hooks.Remove(hooks);
		}

		private void RegisterMessageType(global::Unity.Netcode.NetworkMessageManager.MessageWithHandler messageWithHandler)
		{
			if (m_HighMessageType == m_MessageHandlers.Length)
			{
				global::System.Array.Resize(ref m_MessageHandlers, 2 * m_MessageHandlers.Length);
				global::System.Array.Resize(ref m_ReverseTypeMap, 2 * m_ReverseTypeMap.Length);
			}
			m_MessageHandlers[m_HighMessageType] = messageWithHandler.Handler;
			m_ReverseTypeMap[m_HighMessageType] = messageWithHandler.MessageType;
			m_MessagesByHash[messageWithHandler.MessageType.FullName.Hash32()] = messageWithHandler.MessageType;
			m_MessageTypes[messageWithHandler.MessageType] = m_HighMessageType++;
			m_LocalVersions[messageWithHandler.MessageType] = messageWithHandler.GetVersion();
		}

		public int GetLocalVersion(global::System.Type messageType)
		{
			return m_LocalVersions[messageType];
		}

		internal static string ByteArrayToString(byte[] ba, int offset, int count)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(ba.Length * 2);
			for (int i = offset; i < offset + count; i++)
			{
				stringBuilder.AppendFormat("{0:x2} ", ba[i]);
			}
			return stringBuilder.ToString();
		}

		internal unsafe void HandleIncomingData(ulong clientId, global::System.ArraySegment<byte> data, float receiveTime)
		{
			fixed (byte* array = data.Array)
			{
				global::Unity.Netcode.FastBufferReader reader = new global::Unity.Netcode.FastBufferReader(array + data.Offset, global::Unity.Collections.Allocator.None, data.Count);
				if (!reader.TryBeginRead(sizeof(global::Unity.Netcode.NetworkBatchHeader)))
				{
					global::Unity.Netcode.NetworkLog.LogError("Received a packet too small to contain a BatchHeader. Ignoring it.");
					return;
				}
				reader.ReadValue(out global::Unity.Netcode.NetworkBatchHeader value, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
				if (value.Magic != 4448)
				{
					global::Unity.Netcode.NetworkLog.LogError($"Received a packet with an invalid Magic Value. Please report this to the Netcode for GameObjects team at https://github.com/Unity-Technologies/com.unity.netcode.gameobjects/issues and include the following data: Offset: {data.Offset}, Size: {data.Count}, Full receive array: {ByteArrayToString(data.Array, 0, data.Array.Length)}");
					return;
				}
				if (value.BatchSize != data.Count)
				{
					global::Unity.Netcode.NetworkLog.LogError($"Received a packet with an invalid Batch Size Value. Please report this to the Netcode for GameObjects team at https://github.com/Unity-Technologies/com.unity.netcode.gameobjects/issues and include the following data: Offset: {data.Offset}, Size: {data.Count}, Expected Size: {value.BatchSize}, Full receive array: {ByteArrayToString(data.Array, 0, data.Array.Length)}");
					return;
				}
				ulong num = global::Unity.Netcode.XXHash.Hash64(reader.GetUnsafePtrAtCurrentPosition(), reader.Length - reader.Position);
				if (num != value.BatchHash)
				{
					global::Unity.Netcode.NetworkLog.LogError($"Received a packet with an invalid Hash Value. Please report this to the Netcode for GameObjects team at https://github.com/Unity-Technologies/com.unity.netcode.gameobjects/issues and include the following data: Received Hash: {value.BatchHash}, Calculated Hash: {num}, Offset: {data.Offset}, Size: {data.Count}, Full receive array: {ByteArrayToString(data.Array, 0, data.Array.Length)}");
					return;
				}
				for (int i = 0; i < m_Hooks.Count; i++)
				{
					m_Hooks[i].OnBeforeReceiveBatch(clientId, value.BatchCount, reader.Length);
				}
				for (int j = 0; j < value.BatchCount; j++)
				{
					global::Unity.Netcode.NetworkMessageHeader header = default(global::Unity.Netcode.NetworkMessageHeader);
					int position = reader.Position;
					try
					{
						global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out header.MessageType);
						global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out header.MessageSize);
					}
					catch (global::System.OverflowException)
					{
						global::Unity.Netcode.NetworkLog.LogError("Received a batch that didn't have enough data for all of its batches, ending early!");
						throw;
					}
					int messageHeaderSerializedSize = reader.Position - position;
					if (!reader.TryBeginRead((int)header.MessageSize))
					{
						global::Unity.Netcode.NetworkLog.LogError("Received a message that claimed a size larger than the packet, ending early!");
						return;
					}
					m_IncomingMessageQueue.Add(new global::Unity.Netcode.NetworkMessageManager.ReceiveQueueItem
					{
						Header = header,
						SenderId = clientId,
						Timestamp = receiveTime,
						Reader = new global::Unity.Netcode.FastBufferReader(reader.GetUnsafePtrAtCurrentPosition(), global::Unity.Collections.Allocator.TempJob, (int)header.MessageSize),
						MessageHeaderSerializedSize = messageHeaderSerializedSize
					});
					reader.Seek(reader.Position + (int)header.MessageSize);
				}
				for (int k = 0; k < m_Hooks.Count; k++)
				{
					m_Hooks[k].OnAfterReceiveBatch(clientId, value.BatchCount, reader.Length);
				}
			}
		}

		private bool CanReceive(ulong clientId, global::System.Type messageType, global::Unity.Netcode.FastBufferReader messageContent, ref global::Unity.Netcode.NetworkContext context)
		{
			for (int i = 0; i < m_Hooks.Count; i++)
			{
				if (!m_Hooks[i].OnVerifyCanReceive(clientId, messageType, messageContent, ref context))
				{
					return false;
				}
			}
			return true;
		}

		internal global::System.Type GetMessageForHash(uint messageHash)
		{
			if (!m_MessagesByHash.ContainsKey(messageHash))
			{
				return null;
			}
			return m_MessagesByHash[messageHash];
		}

		internal void SetVersion(ulong clientId, uint messageHash, int version)
		{
			if (m_MessagesByHash.ContainsKey(messageHash))
			{
				global::System.Type key = m_MessagesByHash[messageHash];
				if (!m_PerClientMessageVersions.ContainsKey(clientId))
				{
					m_PerClientMessageVersions[clientId] = new global::System.Collections.Generic.Dictionary<global::System.Type, int>();
				}
				m_PerClientMessageVersions[clientId][key] = version;
			}
		}

		internal void SetServerMessageOrder(global::Unity.Collections.NativeArray<uint> messagesInIdOrder)
		{
			global::Unity.Netcode.NetworkMessageManager.MessageHandler[] messageHandlers = m_MessageHandlers;
			global::System.Collections.Generic.Dictionary<global::System.Type, uint> messageTypes = m_MessageTypes;
			m_ReverseTypeMap = new global::System.Type[messagesInIdOrder.Length];
			m_MessageHandlers = new global::Unity.Netcode.NetworkMessageManager.MessageHandler[messagesInIdOrder.Length];
			m_MessageTypes = new global::System.Collections.Generic.Dictionary<global::System.Type, uint>();
			for (int i = 0; i < messagesInIdOrder.Length; i++)
			{
				if (m_MessagesByHash.ContainsKey(messagesInIdOrder[i]))
				{
					global::System.Type type = m_MessagesByHash[messagesInIdOrder[i]];
					uint num = messageTypes[type];
					global::Unity.Netcode.NetworkMessageManager.MessageHandler messageHandler = messageHandlers[num];
					uint num2 = (uint)i;
					m_MessageTypes[type] = num2;
					m_MessageHandlers[num2] = messageHandler;
					m_ReverseTypeMap[num2] = type;
				}
			}
		}

		public void HandleMessage(in global::Unity.Netcode.NetworkMessageHeader header, global::Unity.Netcode.FastBufferReader reader, ulong senderId, float timestamp, int serializedHeaderSize)
		{
			using (reader)
			{
				if (header.MessageType >= m_HighMessageType)
				{
					global::UnityEngine.Debug.LogWarning($"Received a message with invalid message type value {header.MessageType}");
					return;
				}
				global::Unity.Netcode.NetworkContext context = new global::Unity.Netcode.NetworkContext
				{
					SystemOwner = m_Owner,
					SenderId = senderId,
					Timestamp = timestamp,
					Header = header,
					SerializedHeaderSize = serializedHeaderSize,
					MessageSize = header.MessageSize
				};
				global::System.Type messageType = m_ReverseTypeMap[header.MessageType];
				if (!CanReceive(senderId, messageType, reader, ref context))
				{
					return;
				}
				global::Unity.Netcode.NetworkMessageManager.MessageHandler messageHandler = m_MessageHandlers[header.MessageType];
				for (int i = 0; i < m_Hooks.Count; i++)
				{
					m_Hooks[i].OnBeforeReceiveMessage(senderId, messageType, reader.Length + global::Unity.Netcode.FastBufferWriter.GetWriteSize<global::Unity.Netcode.NetworkMessageHeader>());
				}
				if (messageHandler == null)
				{
					global::UnityEngine.Debug.LogException(new global::Unity.Netcode.HandlerNotRegisteredException(header.MessageType.ToString()));
				}
				else
				{
					try
					{
						messageHandler(reader, ref context, this);
					}
					catch (global::System.Exception exception)
					{
						global::UnityEngine.Debug.LogException(exception);
					}
				}
				for (int j = 0; j < m_Hooks.Count; j++)
				{
					m_Hooks[j].OnAfterReceiveMessage(senderId, messageType, reader.Length + global::Unity.Netcode.FastBufferWriter.GetWriteSize<global::Unity.Netcode.NetworkMessageHeader>());
				}
			}
		}

		internal void ProcessIncomingMessageQueue()
		{
			if (StopProcessing)
			{
				return;
			}
			for (int i = 0; i < m_IncomingMessageQueue.Length; i++)
			{
				ref global::Unity.Netcode.NetworkMessageManager.ReceiveQueueItem reference = ref m_IncomingMessageQueue.ElementAt(i);
				HandleMessage(in reference.Header, reference.Reader, reference.SenderId, reference.Timestamp, reference.MessageHeaderSerializedSize);
				if (m_Disposed)
				{
					return;
				}
			}
			m_IncomingMessageQueue.Clear();
		}

		internal void ClientConnected(ulong clientId)
		{
			if (!m_SendQueues.ContainsKey(clientId))
			{
				m_SendQueues[clientId] = new global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkMessageManager.SendQueueItem>(16, global::Unity.Collections.Allocator.Persistent);
			}
		}

		internal void ClientDisconnected(ulong clientId)
		{
			m_DisconnectedClients.Add(clientId);
		}

		private void CleanupDisconnectedClient(ulong clientId)
		{
			if (m_SendQueues.ContainsKey(clientId))
			{
				global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkMessageManager.SendQueueItem> nativeList = m_SendQueues[clientId];
				for (int i = 0; i < nativeList.Length; i++)
				{
					nativeList.ElementAt(i).Writer.Dispose();
				}
				nativeList.Dispose();
				m_SendQueues.Remove(clientId);
				m_PerClientMessageVersions.Remove(clientId);
				PeerMTUSizes.Remove(clientId);
			}
		}

		internal void CleanupDisconnectedClients()
		{
			if (m_DisconnectedClients.Count == 0)
			{
				return;
			}
			foreach (ulong disconnectedClient in m_DisconnectedClients)
			{
				CleanupDisconnectedClient(disconnectedClient);
			}
			m_DisconnectedClients.Clear();
		}

		public static int CreateMessageAndGetVersion<T>() where T : global::Unity.Netcode.INetworkMessage, new()
		{
			return new T().Version;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal int GetMessageVersion(global::System.Type type, ulong clientId, bool forReceive = false)
		{
			if (!m_PerClientMessageVersions.TryGetValue(clientId, out var value))
			{
				global::Unity.Netcode.NetworkManager singleton = global::Unity.Netcode.NetworkManager.Singleton;
				if (singleton != null && singleton.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					if (forReceive)
					{
						global::Unity.Netcode.NetworkLog.LogWarning($"Trying to receive {type.Name} from client {clientId} which is not in a connected state.");
					}
					else
					{
						global::Unity.Netcode.NetworkLog.LogWarning($"Trying to send {type.Name} to client {clientId} which is not in a connected state.");
					}
				}
				return -1;
			}
			if (!value.TryGetValue(type, out var value2))
			{
				return -1;
			}
			return value2;
		}

		public static void ReceiveMessage<T>(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, global::Unity.Netcode.NetworkMessageManager manager) where T : global::Unity.Netcode.INetworkMessage, new()
		{
			global::System.Type typeFromHandle = typeof(T);
			T message = new T();
			int num = 0;
			if (typeFromHandle != s_ConnectionRequestType && typeFromHandle != s_ConnectionApprovedType && typeFromHandle != s_DisconnectReasonType && context.SenderId != manager.m_LocalClientId)
			{
				num = manager.GetMessageVersion(typeFromHandle, context.SenderId, forReceive: true);
				if (num < 0)
				{
					return;
				}
			}
			if (message.Deserialize(reader, ref context, num))
			{
				for (int i = 0; i < manager.m_Hooks.Count; i++)
				{
					manager.m_Hooks[i].OnBeforeHandleMessage(ref message, ref context);
				}
				message.Handle(ref context);
				for (int j = 0; j < manager.m_Hooks.Count; j++)
				{
					manager.m_Hooks[j].OnAfterHandleMessage(ref message, ref context);
				}
			}
		}

		private bool CanSend(ulong clientId, global::System.Type messageType, global::Unity.Netcode.NetworkDelivery delivery)
		{
			for (int i = 0; i < m_Hooks.Count; i++)
			{
				if (!m_Hooks[i].OnVerifyCanSend(clientId, messageType, delivery))
				{
					return false;
				}
			}
			return true;
		}

		internal int SendMessage<TMessageType, TClientIdListType>(ref TMessageType message, global::Unity.Netcode.NetworkDelivery delivery, in TClientIdListType clientIds) where TMessageType : global::Unity.Netcode.INetworkMessage where TClientIdListType : global::System.Collections.Generic.IReadOnlyList<ulong>
		{
			if (clientIds.Count == 0)
			{
				return 0;
			}
			int num = 0;
			global::Unity.Collections.NativeHashSet<int> nativeHashSet = new global::Unity.Collections.NativeHashSet<int>(clientIds.Count, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < clientIds.Count; i++)
			{
				int num2 = 0;
				if (typeof(TMessageType) != s_ConnectionRequestType)
				{
					num2 = GetMessageVersion(typeof(TMessageType), clientIds[i]);
					if (num2 < 0)
					{
						continue;
					}
				}
				if (!nativeHashSet.Contains(num2))
				{
					nativeHashSet.Add(num2);
					int num3 = ((delivery == global::Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced) ? FragmentedMessageMaxSize : NonFragmentedMessageMaxSize);
					using global::Unity.Netcode.FastBufferWriter tmpSerializer = new global::Unity.Netcode.FastBufferWriter(NonFragmentedMessageMaxSize - global::Unity.Netcode.FastBufferWriter.GetWriteSize<global::Unity.Netcode.NetworkMessageHeader>(), global::Unity.Collections.Allocator.Temp, num3 - global::Unity.Netcode.FastBufferWriter.GetWriteSize<global::Unity.Netcode.NetworkMessageHeader>());
					global::Unity.Netcode.FastBufferWriter writer = tmpSerializer;
					message.Serialize(writer, num2);
					int num4 = SendPreSerializedMessage(in tmpSerializer, num3, ref message, delivery, (global::System.Collections.Generic.IReadOnlyList<ulong>)clientIds, num2);
					num = ((num4 > num) ? num4 : num);
				}
			}
			nativeHashSet.Dispose();
			return num;
		}

		internal unsafe int SendPreSerializedMessage<TMessageType>(in global::Unity.Netcode.FastBufferWriter tmpSerializer, int maxSize, ref TMessageType message, global::Unity.Netcode.NetworkDelivery delivery, in global::System.Collections.Generic.IReadOnlyList<ulong> clientIds, int messageVersionFilter) where TMessageType : global::Unity.Netcode.INetworkMessage
		{
			using global::Unity.Netcode.FastBufferWriter writer = new global::Unity.Netcode.FastBufferWriter(global::Unity.Netcode.FastBufferWriter.GetWriteSize<global::Unity.Netcode.NetworkMessageHeader>(), global::Unity.Collections.Allocator.Temp);
			global::Unity.Netcode.NetworkMessageHeader networkMessageHeader = new global::Unity.Netcode.NetworkMessageHeader
			{
				MessageSize = (uint)tmpSerializer.Length,
				MessageType = m_MessageTypes[typeof(TMessageType)]
			};
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, networkMessageHeader.MessageType);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, networkMessageHeader.MessageSize);
			for (int i = 0; i < clientIds.Count; i++)
			{
				if (m_DisconnectedClients.Contains(clientIds[i]))
				{
					continue;
				}
				if (typeof(TMessageType) != s_ConnectionRequestType)
				{
					int messageVersion = GetMessageVersion(typeof(TMessageType), clientIds[i]);
					if (messageVersion < 0 || messageVersion != messageVersionFilter)
					{
						continue;
					}
				}
				ulong num = clientIds[i];
				if (!CanSend(num, typeof(TMessageType), delivery))
				{
					continue;
				}
				int writerSize = NonFragmentedMessageMaxSize;
				if (delivery != global::Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced)
				{
					if (PeerMTUSizes.TryGetValue(num, out var value))
					{
						maxSize = value;
					}
					writerSize = maxSize;
					if (tmpSerializer.Position >= maxSize)
					{
						global::UnityEngine.Debug.LogError($"MTU size for {num} is too small to contain a message of type {typeof(TMessageType).FullName}");
						continue;
					}
				}
				for (int j = 0; j < m_Hooks.Count; j++)
				{
					m_Hooks[j].OnBeforeSendMessage(num, ref message, delivery);
				}
				global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkMessageManager.SendQueueItem> nativeList = m_SendQueues[num];
				if (nativeList.Length == 0)
				{
					nativeList.Add(new global::Unity.Netcode.NetworkMessageManager.SendQueueItem(delivery, writerSize, global::Unity.Collections.Allocator.TempJob, maxSize));
					nativeList.ElementAt(0).Writer.Seek(sizeof(global::Unity.Netcode.NetworkBatchHeader));
				}
				else
				{
					ref global::Unity.Netcode.NetworkMessageManager.SendQueueItem reference = ref nativeList.ElementAt(nativeList.Length - 1);
					if (reference.NetworkDelivery != delivery || reference.Writer.MaxCapacity - reference.Writer.Position < tmpSerializer.Length + writer.Length)
					{
						nativeList.Add(new global::Unity.Netcode.NetworkMessageManager.SendQueueItem(delivery, writerSize, global::Unity.Collections.Allocator.TempJob, maxSize));
						nativeList.ElementAt(nativeList.Length - 1).Writer.Seek(sizeof(global::Unity.Netcode.NetworkBatchHeader));
					}
				}
				ref global::Unity.Netcode.NetworkMessageManager.SendQueueItem reference2 = ref nativeList.ElementAt(nativeList.Length - 1);
				if (!reference2.Writer.TryBeginWrite(tmpSerializer.Length + writer.Length))
				{
					global::UnityEngine.Debug.LogError($"Not enough space to write message, size={tmpSerializer.Length + writer.Length} space used={reference2.Writer.Position} total size={reference2.Writer.Capacity}");
					continue;
				}
				reference2.Writer.WriteBytes(writer.GetUnsafePtr(), writer.Length);
				reference2.Writer.WriteBytes(tmpSerializer.GetUnsafePtr(), tmpSerializer.Length);
				reference2.BatchHeader.BatchCount++;
				for (int k = 0; k < m_Hooks.Count; k++)
				{
					m_Hooks[k].OnAfterSendMessage(num, ref message, delivery, tmpSerializer.Length + writer.Length);
				}
			}
			return tmpSerializer.Length + writer.Length;
		}

		internal unsafe int SendPreSerializedMessage<TMessageType>(in global::Unity.Netcode.FastBufferWriter tmpSerializer, int maxSize, ref TMessageType message, global::Unity.Netcode.NetworkDelivery delivery, ulong clientId) where TMessageType : global::Unity.Netcode.INetworkMessage
		{
			int num = 0;
			if (typeof(TMessageType) != s_ConnectionRequestType)
			{
				num = GetMessageVersion(typeof(TMessageType), clientId);
				if (num < 0)
				{
					return 0;
				}
			}
			ulong* ptr = stackalloc ulong[1] { clientId };
			return SendPreSerializedMessage(in tmpSerializer, maxSize, ref message, delivery, (global::System.Collections.Generic.IReadOnlyList<ulong>)new global::Unity.Netcode.NetworkMessageManager.PointerListWrapper<ulong>(ptr, 1), num);
		}

		internal unsafe int SendMessage<T>(ref T message, global::Unity.Netcode.NetworkDelivery delivery, ulong* clientIds, int numClientIds) where T : global::Unity.Netcode.INetworkMessage
		{
			return SendMessage<T, global::Unity.Netcode.NetworkMessageManager.PointerListWrapper<ulong>>(ref message, delivery, new global::Unity.Netcode.NetworkMessageManager.PointerListWrapper<ulong>(clientIds, numClientIds));
		}

		internal unsafe int SendMessage<T>(ref T message, global::Unity.Netcode.NetworkDelivery delivery, ulong clientId) where T : global::Unity.Netcode.INetworkMessage
		{
			ulong* ptr = stackalloc ulong[1] { clientId };
			return SendMessage<T, global::Unity.Netcode.NetworkMessageManager.PointerListWrapper<ulong>>(ref message, delivery, new global::Unity.Netcode.NetworkMessageManager.PointerListWrapper<ulong>(ptr, 1));
		}

		internal unsafe int SendMessage<T>(ref T message, global::Unity.Netcode.NetworkDelivery delivery, in global::Unity.Collections.NativeArray<ulong> clientIds) where T : global::Unity.Netcode.INetworkMessage
		{
			return SendMessage<T, global::Unity.Netcode.NetworkMessageManager.PointerListWrapper<ulong>>(ref message, delivery, new global::Unity.Netcode.NetworkMessageManager.PointerListWrapper<ulong>((ulong*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(clientIds), clientIds.Length));
		}

		internal unsafe int SendMessage<T>(ref T message, global::Unity.Netcode.NetworkDelivery delivery, in global::Unity.Collections.NativeList<ulong> clientIds) where T : global::Unity.Netcode.INetworkMessage
		{
			return SendMessage<T, global::Unity.Netcode.NetworkMessageManager.PointerListWrapper<ulong>>(ref message, delivery, new global::Unity.Netcode.NetworkMessageManager.PointerListWrapper<ulong>(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(clientIds), clientIds.Length));
		}

		internal unsafe void ProcessSendQueues()
		{
			if (StopProcessing)
			{
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkMessageManager.SendQueueItem>> sendQueue in m_SendQueues)
			{
				ulong key = sendQueue.Key;
				global::Unity.Collections.NativeList<global::Unity.Netcode.NetworkMessageManager.SendQueueItem> value = sendQueue.Value;
				for (int i = 0; i < value.Length; i++)
				{
					ref global::Unity.Netcode.NetworkMessageManager.SendQueueItem reference = ref value.ElementAt(i);
					if (m_DisconnectedClients.Contains(key))
					{
						reference.Writer.Dispose();
						continue;
					}
					if (reference.BatchHeader.BatchCount == 0)
					{
						reference.Writer.Dispose();
						continue;
					}
					for (int j = 0; j < m_Hooks.Count; j++)
					{
						m_Hooks[j].OnBeforeSendBatch(key, reference.BatchHeader.BatchCount, reference.Writer.Length, reference.NetworkDelivery);
					}
					reference.Writer.Seek(0);
					int num = (reference.Writer.Length + 7) & -8;
					reference.Writer.TryBeginWrite(num);
					reference.BatchHeader.BatchHash = global::Unity.Netcode.XXHash.Hash64(reference.Writer.GetUnsafePtr() + sizeof(global::Unity.Netcode.NetworkBatchHeader), num - sizeof(global::Unity.Netcode.NetworkBatchHeader));
					reference.BatchHeader.BatchSize = num;
					reference.Writer.WriteValue(in reference.BatchHeader, default(global::Unity.Netcode.FastBufferWriter.ForStructs));
					reference.Writer.Seek(num);
					try
					{
						m_Sender.Send(key, reference.NetworkDelivery, reference.Writer);
						for (int k = 0; k < m_Hooks.Count; k++)
						{
							m_Hooks[k].OnAfterSendBatch(key, reference.BatchHeader.BatchCount, reference.Writer.Length, reference.NetworkDelivery);
						}
					}
					finally
					{
						reference.Writer.Dispose();
					}
				}
				value.Clear();
			}
		}
	}
}
