namespace Unity.Netcode
{
	internal struct NetworkTransformMessage : global::Unity.Netcode.INetworkMessage
	{
		private const string k_Name = "NetworkTransformMessage";

		internal global::Unity.Netcode.Components.NetworkTransform NetworkTransform;

		internal global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState State;

		private global::Unity.Netcode.FastBufferReader m_CurrentReader;

		internal int BytesWritten;

		public int Version => 0;

		private unsafe void CopyPayload(ref global::Unity.Netcode.FastBufferWriter writer)
		{
			writer.WriteBytesSafe(m_CurrentReader.GetUnsafePtrAtCurrentPosition(), m_CurrentReader.Length - m_CurrentReader.Position);
		}

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			if (m_CurrentReader.IsInitialized)
			{
				CopyPayload(ref writer);
				return;
			}
			int position = writer.Position;
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, NetworkTransform.NetworkObjectId);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, (int)NetworkTransform.NetworkBehaviourId);
			writer.WriteNetworkSerializable<global::Unity.Netcode.Components.NetworkTransform.NetworkTransformState>(NetworkTransform.LocalAuthoritativeNetworkState);
			BytesWritten = writer.Position - position;
		}

		public unsafe bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			global::Unity.Netcode.NetworkManager networkManager = context.SystemOwner as global::Unity.Netcode.NetworkManager;
			if (networkManager == null)
			{
				global::UnityEngine.Debug.LogError("[NetworkTransformMessage] System owner context was not of type NetworkManager!");
				return false;
			}
			if (networkManager.ShutdownInProgress)
			{
				return false;
			}
			int position = reader.Position;
			ulong value = 0uL;
			int value2 = 0;
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out value);
			bool flag = networkManager.SpawnManager.SpawnedObjects.ContainsKey(value);
			if (!flag && !networkManager.DAHost)
			{
				networkManager.DeferredMessageManager.DeferMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnSpawn, value, reader, ref context, "NetworkTransformMessage");
				return false;
			}
			global::Unity.Netcode.NetworkObject networkObject = null;
			bool flag2 = false;
			bool flag3 = false;
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out value2);
			if (flag)
			{
				networkObject = networkManager.SpawnManager.SpawnedObjects[value];
				if (networkObject.ChildNetworkBehaviours.Count <= value2 || networkObject.ChildNetworkBehaviours[value2] == null)
				{
					global::UnityEngine.Debug.LogError(string.Format("[{0}][Invalid] Targeted {1}, {2} ({3}), does not exist! Make sure you are not spawning {4}s with disabled {5}s that have {6} components on them.", "NetworkTransformMessage", "NetworkTransform", "NetworkBehaviourId", value2, "NetworkObject", "GameObject", "NetworkBehaviour"));
					return false;
				}
				global::Unity.Netcode.Components.NetworkTransform networkTransform = networkObject.ChildNetworkBehaviours[value2] as global::Unity.Netcode.Components.NetworkTransform;
				if (networkTransform == null)
				{
					global::UnityEngine.Debug.LogError(string.Format("[{0}][Invalid] Targeted {1}, {2} ({3}), does not exist! Make sure you are not spawning {4}s with disabled {5}s that have {6} components on them.", "NetworkTransformMessage", "NetworkTransform", "NetworkBehaviourId", value2, "NetworkObject", "GameObject", "NetworkBehaviour"));
					return false;
				}
				NetworkTransform = networkTransform;
				flag2 = NetworkTransform.IsServerAuthoritative();
				flag3 = !flag2 && networkManager.IsServer;
				reader.ReadNetworkSerializableInPlace(ref NetworkTransform.InboundState);
				NetworkTransform.InboundState.LastSerializedSize = reader.Position - position;
			}
			else
			{
				flag3 = networkManager.DAHost;
				if (!flag3)
				{
					global::UnityEngine.Debug.LogError(string.Format("[{0}][Invalid] Target NetworkObject ({1}) does not exist!", "NetworkTransformMessage", value));
					return false;
				}
				reader.ReadNetworkSerializableInPlace(ref State);
			}
			if (flag3)
			{
				int count = networkObject.Observers.Count;
				ulong* ptr = stackalloc ulong[count];
				if (networkManager.DistributedAuthorityMode && networkManager.DAHost)
				{
					int num = 0;
					foreach (ulong observer in networkObject.Observers)
					{
						ptr[num] = observer;
						if (num >= count)
						{
							global::UnityEngine.Debug.LogError("[NetworkTransformMessage] Exceeded total number of observers!");
						}
						num++;
					}
				}
				ulong num2 = 0uL;
				if (networkObject != null)
				{
					num2 = networkObject.OwnerClientId;
					if (num2 == 0L)
					{
						return true;
					}
				}
				else if (networkManager.DAHost)
				{
					num2 = context.SenderId;
				}
				global::Unity.Netcode.NetworkDelivery delivery = ((!((NetworkTransform != null) ? NetworkTransform.InboundState : State).IsReliableStateUpdate()) ? global::Unity.Netcode.NetworkDelivery.UnreliableSequenced : global::Unity.Netcode.NetworkDelivery.ReliableSequenced);
				if (networkManager.ConnectionManager.ConnectedClientsList.Count > ((!networkManager.IsHost) ? 1 : 2))
				{
					int num3 = (networkManager.DistributedAuthorityMode ? count : networkManager.ConnectionManager.ConnectedClientsList.Count);
					if (num3 == 0)
					{
						return true;
					}
					global::Unity.Netcode.NetworkTransformMessage message = this;
					message.m_CurrentReader = new global::Unity.Netcode.FastBufferReader(reader, global::Unity.Collections.Allocator.None);
					message.m_CurrentReader.Seek(position);
					for (int i = 0; i < num3; i++)
					{
						ulong num4 = (networkManager.DistributedAuthorityMode ? ptr[i] : networkManager.ConnectionManager.ConnectedClientsList[i].ClientId);
						if (num4 != 0L && (flag2 || num4 != num2) && (networkManager.DistributedAuthorityMode || networkObject.Observers.Contains(num4)))
						{
							networkManager.MessageManager.SendMessage(ref message, delivery, num4);
						}
					}
					message.m_CurrentReader.Dispose();
				}
			}
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			if (!(context.SystemOwner as global::Unity.Netcode.NetworkManager).DAHost || !(NetworkTransform == null))
			{
				if (NetworkTransform == null)
				{
					global::UnityEngine.Debug.LogError("[NetworkTransformMessage][Dropped] Reciever NetworkTransform was not set!");
				}
				else
				{
					NetworkTransform.TransformStateUpdate();
				}
			}
		}
	}
}
