namespace Unity.Netcode
{
	internal struct CreateObjectMessage : global::Unity.Netcode.INetworkMessage
	{
		private const string k_Name = "CreateObjectMessage";

		public global::Unity.Netcode.NetworkObject.SerializedObject ObjectInfo;

		private global::Unity.Netcode.FastBufferReader m_ReceivedNetworkVariableData;

		internal ulong[] ObserverIds;

		internal ulong[] NewObserverIds;

		internal ulong NetworkObjectId;

		private const byte k_IncludesSerializedObject = 1;

		private const byte k_UpdateObservers = 2;

		private const byte k_UpdateNewObservers = 4;

		internal bool IncludesSerializedObject;

		internal bool UpdateObservers;

		internal bool UpdateNewObservers;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			byte b = 0;
			if (IncludesSerializedObject)
			{
				b |= 1;
			}
			if (UpdateObservers)
			{
				b |= 2;
			}
			if (UpdateNewObservers)
			{
				b |= 4;
			}
			writer.WriteByteSafe(b);
			if (UpdateObservers)
			{
				global::Unity.Netcode.BytePacker.WriteValuePacked(writer, ObserverIds.Length);
				ulong[] observerIds = ObserverIds;
				foreach (ulong value in observerIds)
				{
					global::Unity.Netcode.BytePacker.WriteValuePacked(writer, value);
				}
			}
			if (UpdateNewObservers)
			{
				global::Unity.Netcode.BytePacker.WriteValuePacked(writer, NewObserverIds.Length);
				ulong[] observerIds = NewObserverIds;
				foreach (ulong value2 in observerIds)
				{
					global::Unity.Netcode.BytePacker.WriteValuePacked(writer, value2);
				}
			}
			if (IncludesSerializedObject)
			{
				ObjectInfo.Serialize(writer);
			}
			else
			{
				global::Unity.Netcode.BytePacker.WriteValuePacked(writer, NetworkObjectId);
			}
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (!networkManager.IsClient)
			{
				return false;
			}
			reader.ReadByteSafe(out var value);
			IncludesSerializedObject = (value & 1) != 0;
			UpdateObservers = (value & 2) != 0;
			UpdateNewObservers = (value & 4) != 0;
			if (UpdateObservers)
			{
				int value2 = 0;
				global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out value2);
				ObserverIds = new ulong[value2];
				ulong value3 = 0uL;
				for (int i = 0; i < value2; i++)
				{
					global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out value3);
					ObserverIds[i] = value3;
				}
			}
			if (UpdateNewObservers)
			{
				int value4 = 0;
				global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out value4);
				NewObserverIds = new ulong[value4];
				ulong value5 = 0uL;
				for (int j = 0; j < value4; j++)
				{
					global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out value5);
					NewObserverIds[j] = value5;
				}
			}
			if (IncludesSerializedObject)
			{
				ObjectInfo.Deserialize(reader);
			}
			else
			{
				global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out NetworkObjectId);
			}
			if (!networkManager.NetworkConfig.ForceSamePrefabs && !networkManager.SpawnManager.HasPrefab(ObjectInfo))
			{
				networkManager.DeferredMessageManager.DeferMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnAddPrefab, ObjectInfo.Hash, reader, ref context, "CreateObjectMessage");
				return false;
			}
			m_ReceivedNetworkVariableData = reader;
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (networkManager.SceneManager.ShouldDeferCreateObject())
			{
				networkManager.SceneManager.DeferCreateObject(context.SenderId, context.MessageSize, ObjectInfo, m_ReceivedNetworkVariableData, ObserverIds, NewObserverIds);
				return;
			}
			if (networkManager.DistributedAuthorityMode && !IncludesSerializedObject && UpdateObservers)
			{
				ObjectInfo = new global::Unity.Netcode.NetworkObject.SerializedObject
				{
					NetworkObjectId = NetworkObjectId
				};
			}
			CreateObject(ref networkManager, context.SenderId, context.MessageSize, ObjectInfo, m_ReceivedNetworkVariableData, ObserverIds, NewObserverIds);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static void CreateObject(ref global::Unity.Netcode.NetworkManager networkManager, ref global::Unity.Netcode.NetworkSceneManager.DeferredObjectCreation deferredObjectCreation)
		{
			ulong senderId = deferredObjectCreation.SenderId;
			ulong[] observerIds = deferredObjectCreation.ObserverIds;
			ulong[] newObserverIds = deferredObjectCreation.NewObserverIds;
			uint messageSize = deferredObjectCreation.MessageSize;
			global::Unity.Netcode.NetworkObject.SerializedObject serializedObject = deferredObjectCreation.SerializedObject;
			global::Unity.Netcode.FastBufferReader fastBufferReader = deferredObjectCreation.FastBufferReader;
			CreateObject(ref networkManager, senderId, messageSize, serializedObject, fastBufferReader, observerIds, newObserverIds);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		internal static void CreateObject(ref global::Unity.Netcode.NetworkManager networkManager, ulong senderId, uint messageSize, global::Unity.Netcode.NetworkObject.SerializedObject serializedObject, global::Unity.Netcode.FastBufferReader networkVariableData, ulong[] observerIds, ulong[] newObserverIds)
		{
			global::Unity.Netcode.NetworkObject networkObject = null;
			try
			{
				if (!networkManager.DistributedAuthorityMode)
				{
					networkObject = global::Unity.Netcode.NetworkObject.Deserialize(in serializedObject, networkVariableData, networkManager);
				}
				else
				{
					bool flag = observerIds != null && observerIds.Length != 0;
					bool flag2 = newObserverIds != null && newObserverIds.Length != 0;
					if (networkManager.SpawnManager.SpawnedObjects.ContainsKey(serializedObject.NetworkObjectId))
					{
						networkObject = networkManager.SpawnManager.SpawnedObjects[serializedObject.NetworkObjectId];
						if (flag2 && global::System.Linq.Enumerable.Contains(newObserverIds, networkManager.LocalClientId))
						{
							global::Unity.Netcode.NetworkLog.LogErrorServer(string.Format("[{0}][Duplicate-Broadcast] Detected duplicated object creation for {1}!", "CreateObjectMessage", serializedObject.NetworkObjectId));
						}
						else if (networkManager.CMBServiceConnection && networkManager.LocalClientId == networkObject.OwnerClientId)
						{
							global::Unity.Netcode.NetworkLog.LogWarning(string.Format("[{0}][Client-{1}][Duplicate-CreateObjectMessage][Client Is Owner] Detected duplicated object creation for {2}-{3}!", "CreateObjectMessage", networkManager.LocalClientId, networkObject.name, serializedObject.NetworkObjectId));
						}
					}
					else
					{
						networkObject = global::Unity.Netcode.NetworkObject.Deserialize(in serializedObject, networkVariableData, networkManager, invokedByMessage: true);
					}
					global::System.Collections.Generic.IReadOnlyList<ulong> readOnlyList;
					if (!flag || networkObject.IsPlayerObject)
					{
						readOnlyList = networkManager.ConnectedClientsIds;
					}
					else
					{
						global::System.Collections.Generic.IReadOnlyList<ulong> readOnlyList2 = observerIds;
						readOnlyList = readOnlyList2;
					}
					global::System.Collections.Generic.IReadOnlyList<ulong> readOnlyList3 = readOnlyList;
					for (int i = 0; i < readOnlyList3.Count; i++)
					{
						networkObject.AddObserver(readOnlyList3[i]);
					}
					if (networkManager.DAHost)
					{
						if (networkObject.IsPlayerObject && flag2 && readOnlyList3.Count != observerIds.Length)
						{
							observerIds = global::System.Linq.Enumerable.ToArray(readOnlyList3);
						}
						global::Unity.Netcode.CreateObjectMessage createObjectMessage = new global::Unity.Netcode.CreateObjectMessage
						{
							ObjectInfo = serializedObject,
							m_ReceivedNetworkVariableData = networkVariableData,
							ObserverIds = (flag ? observerIds : null),
							NetworkObjectId = networkObject.NetworkObjectId,
							IncludesSerializedObject = true
						};
						foreach (ulong item in readOnlyList3)
						{
							if ((flag || networkObject.Observers.Contains(item)) && item != networkObject.OwnerClientId && item != 0L)
							{
								createObjectMessage.IncludesSerializedObject = flag2 && global::System.Linq.Enumerable.Contains(newObserverIds, item);
								networkManager.SpawnManager.SendSpawnCallForObject(item, networkObject);
							}
						}
					}
				}
				if (networkObject != null)
				{
					networkManager.NetworkMetrics.TrackObjectSpawnReceived(senderId, networkObject, messageSize);
				}
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
		}
	}
}
