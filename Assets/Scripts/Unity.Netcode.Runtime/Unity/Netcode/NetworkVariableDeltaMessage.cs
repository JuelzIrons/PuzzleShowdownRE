namespace Unity.Netcode
{
	internal struct NetworkVariableDeltaMessage : global::Unity.Netcode.INetworkMessage
	{
		private const int k_ServerDeltaForwardingAndNetworkDelivery = 1;

		public ulong NetworkObjectId;

		public ushort NetworkBehaviourIndex;

		public global::System.Collections.Generic.HashSet<int> DeliveryMappedNetworkVariableIndex;

		public ulong TargetClientId;

		public global::Unity.Netcode.NetworkBehaviour NetworkBehaviour;

		public global::Unity.Netcode.NetworkDelivery NetworkDelivery;

		private global::Unity.Netcode.FastBufferReader m_ReceivedNetworkVariableData;

		private bool m_ForwardingMessage;

		private int m_ReceivedMessageVersion;

		private const string k_Name = "NetworkVariableDeltaMessage";

		private global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<int>> m_ForwardUpdates;

		private global::System.Collections.Generic.List<int> m_UpdatedNetworkVariables;

		public int Version => 1;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void WriteNetworkVariable(ref global::Unity.Netcode.FastBufferWriter writer, ref global::Unity.Netcode.NetworkVariableBase networkVariable, bool ensureNetworkVariableLengthSafety, int nonfragmentedSize, int fragmentedSize)
		{
			if (ensureNetworkVariableLengthSafety)
			{
				global::Unity.Netcode.FastBufferWriter writer2 = new global::Unity.Netcode.FastBufferWriter(nonfragmentedSize, global::Unity.Collections.Allocator.Temp, fragmentedSize);
				networkVariable.WriteDelta(writer2);
				global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, writer2.Length);
				if (!writer.TryBeginWrite(writer2.Length))
				{
					throw new global::System.OverflowException("Not enough space in the buffer to write NetworkVariableDeltaMessage");
				}
				writer2.CopyTo(writer);
			}
			else
			{
				networkVariable.WriteDelta(writer);
			}
		}

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			if (!writer.TryBeginWrite(global::Unity.Netcode.FastBufferWriter.GetWriteSize(in NetworkObjectId, default(global::Unity.Netcode.FastBufferWriter.ForStructs)) + global::Unity.Netcode.FastBufferWriter.GetWriteSize(in NetworkBehaviourIndex, default(global::Unity.Netcode.FastBufferWriter.ForStructs))))
			{
				throw new global::System.OverflowException("Not enough space in the buffer to write NetworkVariableDeltaMessage");
			}
			global::Unity.Netcode.NetworkObject networkObject = NetworkBehaviour.NetworkObject;
			global::Unity.Netcode.NetworkManager networkManagerOwner = networkObject.NetworkManagerOwner;
			string networkBehaviourName = NetworkBehaviour.__getTypeName();
			int nonFragmentedMessageMaxSize = networkManagerOwner.MessageManager.NonFragmentedMessageMaxSize;
			int fragmentedMessageMaxSize = networkManagerOwner.MessageManager.FragmentedMessageMaxSize;
			bool ensureNetworkVariableLengthSafety = networkManagerOwner.NetworkConfig.EnsureNetworkVariableLengthSafety;
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, NetworkObjectId);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, NetworkBehaviourIndex);
			if (targetVersion >= 1)
			{
				writer.WriteValueSafe(in NetworkDelivery, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
				if (m_ForwardingMessage)
				{
					for (int i = 0; i < NetworkBehaviour.NetworkVariableFields.Count; i++)
					{
						int length = writer.Length;
						global::Unity.Netcode.NetworkVariableBase networkVariable = NetworkBehaviour.NetworkVariableFields[i];
						bool value = m_ForwardUpdates[TargetClientId].Contains(i);
						if (ensureNetworkVariableLengthSafety)
						{
							if (!value)
							{
								global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, 0);
							}
						}
						else
						{
							writer.WriteValueSafe(in value, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
						}
						if (value)
						{
							WriteNetworkVariable(ref writer, ref networkVariable, ensureNetworkVariableLengthSafety, nonFragmentedMessageMaxSize, fragmentedMessageMaxSize);
							networkManagerOwner.NetworkMetrics.TrackNetworkVariableDeltaSent(TargetClientId, networkObject, networkVariable.Name, networkBehaviourName, writer.Length - length);
						}
					}
					return;
				}
			}
			for (int j = 0; j < NetworkBehaviour.NetworkVariableFields.Count; j++)
			{
				if (!DeliveryMappedNetworkVariableIndex.Contains(j))
				{
					if (ensureNetworkVariableLengthSafety)
					{
						global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, (ushort)0);
					}
					else
					{
						writer.WriteValueSafe<bool>(false, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					}
					continue;
				}
				int length2 = writer.Length;
				global::Unity.Netcode.NetworkVariableBase networkVariable2 = NetworkBehaviour.NetworkVariableFields[j];
				bool value2 = networkVariable2.IsDirty() && networkVariable2.CanClientRead(TargetClientId) && (networkManagerOwner.IsServer || (networkVariable2.CanWrite() && networkVariable2.CanSend()));
				if (networkVariable2.WritePerm == global::Unity.Netcode.NetworkVariableWritePermission.Owner && networkObject.OwnerClientId == TargetClientId)
				{
					value2 = false;
				}
				if (networkManagerOwner.SpawnManager.ObjectsToShowToClient.TryGetValue(TargetClientId, out var value3) && value3.Contains(networkObject))
				{
					value2 = false;
				}
				if (ensureNetworkVariableLengthSafety)
				{
					if (!value2)
					{
						global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, 0);
					}
				}
				else
				{
					writer.WriteValueSafe(in value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
				if (value2)
				{
					WriteNetworkVariable(ref writer, ref networkVariable2, ensureNetworkVariableLengthSafety, nonFragmentedMessageMaxSize, fragmentedMessageMaxSize);
					networkManagerOwner.NetworkMetrics.TrackNetworkVariableDeltaSent(TargetClientId, networkObject, networkVariable2.Name, networkBehaviourName, writer.Length - length2);
				}
			}
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			m_ReceivedMessageVersion = receivedMessageVersion;
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out NetworkObjectId);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out NetworkBehaviourIndex);
			if (receivedMessageVersion >= 1)
			{
				reader.ReadValueSafe(out NetworkDelivery, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
			}
			m_ReceivedNetworkVariableData = reader;
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (!networkManager.SpawnManager.SpawnedObjects.TryGetValue(NetworkObjectId, out var value))
			{
				networkManager.DeferredMessageManager.DeferMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnSpawn, NetworkObjectId, m_ReceivedNetworkVariableData, ref context, "NetworkVariableDeltaMessage");
				return;
			}
			bool ensureNetworkVariableLengthSafety = networkManager.NetworkConfig.EnsureNetworkVariableLengthSafety;
			global::Unity.Netcode.NetworkBehaviour networkBehaviourAtOrderIndex = value.GetNetworkBehaviourAtOrderIndex(NetworkBehaviourIndex);
			bool flag = m_ReceivedMessageVersion >= 1 && networkManager.IsServer;
			bool keepDirtyDelta = m_ReceivedMessageVersion < 1 && networkManager.IsServer;
			m_UpdatedNetworkVariables = new global::System.Collections.Generic.List<int>();
			if (networkBehaviourAtOrderIndex == null)
			{
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogWarning(string.Format("Network variable delta message received for a non-existent behaviour. {0}: {1}, {2}: {3}", "NetworkObjectId", NetworkObjectId, "NetworkBehaviourIndex", NetworkBehaviourIndex));
				}
				return;
			}
			if (flag)
			{
				m_ForwardUpdates = new global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<int>>();
				foreach (ulong connectedClientId in networkManager.ConnectionManager.ConnectedClientIds)
				{
					if (connectedClientId != context.SenderId && connectedClientId != networkManager.LocalClientId && value.Observers.Contains(connectedClientId))
					{
						m_ForwardUpdates.Add(connectedClientId, new global::System.Collections.Generic.List<int>());
					}
				}
			}
			for (int i = 0; i < networkBehaviourAtOrderIndex.NetworkVariableFields.Count; i++)
			{
				int value2 = 0;
				global::Unity.Netcode.NetworkVariableBase networkVariableBase = networkBehaviourAtOrderIndex.NetworkVariableFields[i];
				if (ensureNetworkVariableLengthSafety)
				{
					global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(m_ReceivedNetworkVariableData, out value2);
					if (value2 == 0)
					{
						continue;
					}
				}
				else
				{
					m_ReceivedNetworkVariableData.ReadValueSafe(out bool value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
					if (!value3)
					{
						continue;
					}
				}
				if (networkManager.IsServer && !networkVariableBase.CanClientWrite(context.SenderId))
				{
					if (ensureNetworkVariableLengthSafety)
					{
						if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
						{
							global::Unity.Netcode.NetworkLog.LogWarning(string.Format("Client wrote to {0} without permission. => {1}: {2} - {3}(): {4} - VariableIndex: {5}", typeof(global::Unity.Netcode.NetworkVariable<>).Name, "NetworkObjectId", NetworkObjectId, "GetNetworkBehaviourOrderIndex", value.GetNetworkBehaviourOrderIndex(networkBehaviourAtOrderIndex), i));
							global::Unity.Netcode.NetworkLog.LogError("[" + networkVariableBase.GetType().Name + "]");
						}
						m_ReceivedNetworkVariableData.Seek(m_ReceivedNetworkVariableData.Position + value2);
						continue;
					}
					if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::Unity.Netcode.NetworkLog.LogError(string.Format("Client wrote to {0} without permission. No more variables can be read. This is critical. => {1}: {2} - {3}(): {4} - VariableIndex: {5}", typeof(global::Unity.Netcode.NetworkVariable<>).Name, "NetworkObjectId", NetworkObjectId, "GetNetworkBehaviourOrderIndex", value.GetNetworkBehaviourOrderIndex(networkBehaviourAtOrderIndex), i));
						global::Unity.Netcode.NetworkLog.LogError("[" + networkVariableBase.GetType().Name + "]");
					}
					return;
				}
				int position = m_ReceivedNetworkVariableData.Position;
				if (ensureNetworkVariableLengthSafety)
				{
					int num = m_ReceivedNetworkVariableData.Length - position;
					if (value2 > num)
					{
						global::UnityEngine.Debug.LogError($"[{networkBehaviourAtOrderIndex.name}][Delta State Read Error] Expecting to read {value2} but only {num} remains!");
						return;
					}
				}
				try
				{
					networkVariableBase.ReadDelta(m_ReceivedNetworkVariableData, keepDirtyDelta);
					m_UpdatedNetworkVariables.Add(i);
				}
				catch (global::System.Exception exception)
				{
					global::UnityEngine.Debug.LogException(exception);
					return;
				}
				if (ensureNetworkVariableLengthSafety)
				{
					int num2 = m_ReceivedNetworkVariableData.Position - position;
					if (num2 != value2)
					{
						if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
						{
							global::Unity.Netcode.NetworkLog.LogWarning(string.Format("[{0}: {1} - {2}][Delta State Read] NetworkVariable read {3} bytes but was expected to read {4} bytes!", "NetworkObjectId", NetworkObjectId, "GetNetworkBehaviourOrderIndex", num2, value2));
						}
						m_ReceivedNetworkVariableData.Seek(position + value2);
					}
				}
				if (flag)
				{
					foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::System.Collections.Generic.List<int>> forwardUpdate in m_ForwardUpdates)
					{
						if (networkVariableBase.CanClientRead(forwardUpdate.Key) && (!networkManager.SpawnManager.ObjectsToShowToClient.ContainsKey(forwardUpdate.Key) || !networkManager.SpawnManager.ObjectsToShowToClient[forwardUpdate.Key].Contains(value)))
						{
							forwardUpdate.Value.Add(i);
						}
					}
				}
				networkManager.NetworkMetrics.TrackNetworkVariableDeltaReceived(context.SenderId, value, networkVariableBase.Name, networkBehaviourAtOrderIndex.__getTypeName(), context.MessageSize);
			}
			if (flag)
			{
				global::Unity.Netcode.NetworkVariableDeltaMessage message = new global::Unity.Netcode.NetworkVariableDeltaMessage
				{
					NetworkBehaviour = networkBehaviourAtOrderIndex,
					NetworkBehaviourIndex = NetworkBehaviourIndex,
					NetworkObjectId = NetworkObjectId,
					m_ForwardingMessage = true,
					m_ForwardUpdates = m_ForwardUpdates
				};
				foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::System.Collections.Generic.List<int>> forwardUpdate2 in m_ForwardUpdates)
				{
					if (forwardUpdate2.Value.Count > 0)
					{
						message.TargetClientId = forwardUpdate2.Key;
						networkManager.ConnectionManager.SendMessage(ref message, NetworkDelivery, forwardUpdate2.Key);
					}
				}
			}
			foreach (int updatedNetworkVariable in m_UpdatedNetworkVariables)
			{
				networkBehaviourAtOrderIndex.NetworkVariableFields[updatedNetworkVariable].PostDeltaRead();
			}
		}
	}
}
