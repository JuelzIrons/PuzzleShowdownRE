namespace Unity.Netcode
{
	internal struct DestroyObjectMessage : global::Unity.Netcode.INetworkMessage, global::Unity.Netcode.INetworkSerializeByMemcpy
	{
		private const int k_OptimizeDestroyObjectMessage = 1;

		private const int k_AllowDestroyGameInPlaced = 2;

		private const string k_Name = "DestroyObjectMessage";

		public ulong NetworkObjectId;

		internal int DeferredDespawnTick;

		internal ulong TargetClientId;

		internal bool IsDistributedAuthority;

		private const byte k_IsTargetedDestroy = 1;

		private const byte k_IsDeferredDespawn = 2;

		private const byte k_DestroyGameObject = 4;

		internal bool IsTargetedDestroy;

		public bool DestroyGameObject;

		public int Version => 2;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			bool flag = DeferredDespawnTick > 0;
			byte b = 0;
			if (IsTargetedDestroy)
			{
				b |= 1;
			}
			if (flag)
			{
				b |= 2;
			}
			if (DestroyGameObject)
			{
				b |= 4;
			}
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, NetworkObjectId);
			if (IsDistributedAuthority)
			{
				writer.WriteByteSafe(b);
				if (IsTargetedDestroy)
				{
					global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, TargetClientId);
				}
				if (targetVersion < 1 || flag)
				{
					global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, DeferredDespawnTick);
				}
			}
			else if (targetVersion >= 2)
			{
				writer.WriteByteSafe(b);
			}
			if (targetVersion < 1)
			{
				writer.WriteValueSafe(in DestroyGameObject, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			}
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if (!networkManager.IsClient)
			{
				return false;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out NetworkObjectId);
			if (networkManager.DistributedAuthorityMode)
			{
				reader.ReadByteSafe(out var value);
				IsTargetedDestroy = (value & 1) != 0;
				bool flag = (value & 2) != 0;
				DestroyGameObject = (value & 4) != 0;
				if (IsTargetedDestroy)
				{
					global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out TargetClientId);
				}
				if (receivedMessageVersion < 1 || flag)
				{
					global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out DeferredDespawnTick);
				}
			}
			else if (receivedMessageVersion >= 2)
			{
				reader.ReadByteSafe(out var value2);
				DestroyGameObject = (value2 & 4) != 0;
			}
			if (receivedMessageVersion < 1)
			{
				reader.ReadValueSafe(out bool value3, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				DestroyGameObject = value3;
			}
			if (networkManager.SpawnManager.SpawnedObjects.ContainsKey(NetworkObjectId))
			{
				return true;
			}
			if (!networkManager.DistributedAuthorityMode || (networkManager.DistributedAuthorityMode && !IsTargetedDestroy))
			{
				networkManager.DeferredMessageManager.DeferMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnSpawn, NetworkObjectId, reader, ref context, "DestroyObjectMessage");
			}
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			networkManager.SpawnManager.SpawnedObjects.TryGetValue(NetworkObjectId, out var value);
			if (networkManager.DAHost)
			{
				HandleDAHostForwardMessage(context.SenderId, ref networkManager, value);
				if ((bool)value && DeferredDespawnTick > 0 && (!IsTargetedDestroy || (IsTargetedDestroy && TargetClientId == 0L)))
				{
					HandleDeferredDespawn(ref networkManager, ref value);
					return;
				}
			}
			if (!value)
			{
				if (networkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogWarning(string.Format("[{0}] Received destroy object message for NetworkObjectId ({1}) on Client-{2}, but that {3} does not exist!", "DestroyObjectMessage", NetworkObjectId, networkManager.LocalClientId, "NetworkObject"));
				}
				return;
			}
			if (networkManager.DistributedAuthorityMode)
			{
				if (DeferredDespawnTick > 0 && !networkManager.DAHost)
				{
					HandleDeferredDespawn(ref networkManager, ref value);
					return;
				}
				if (IsTargetedDestroy && TargetClientId != networkManager.LocalClientId)
				{
					value.Observers.Remove(TargetClientId);
					return;
				}
			}
			networkManager.SpawnManager.OnDespawnNonAuthorityObject(value, DestroyGameObject);
			networkManager.NetworkMetrics.TrackObjectDestroyReceived(context.SenderId, value, context.MessageSize);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void HandleDAHostForwardMessage(ulong senderId, ref global::Unity.Netcode.NetworkManager networkManager, global::Unity.Netcode.NetworkObject networkObject)
		{
			global::Unity.Netcode.DestroyObjectMessage message = new global::Unity.Netcode.DestroyObjectMessage
			{
				NetworkObjectId = NetworkObjectId,
				DestroyGameObject = DestroyGameObject,
				IsDistributedAuthority = true,
				IsTargetedDestroy = IsTargetedDestroy,
				TargetClientId = TargetClientId,
				DeferredDespawnTick = DeferredDespawnTick
			};
			ulong num = ((networkObject == null) ? senderId : networkObject.OwnerClientId);
			global::System.Collections.Generic.List<ulong> obj = ((networkObject == null) ? networkManager.ConnectionManager.ConnectedClientIds : global::System.Linq.Enumerable.ToList(networkObject.Observers));
			global::Unity.Netcode.NetworkDelivery defaultDelivery = global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.DestroyObjectMessage>.DefaultDelivery;
			foreach (ulong item in obj)
			{
				if (item != networkManager.LocalClientId && item != num)
				{
					networkManager.ConnectionManager.SendMessage(ref message, defaultDelivery, item);
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void HandleDeferredDespawn(ref global::Unity.Netcode.NetworkManager networkManager, ref global::Unity.Netcode.NetworkObject networkObject)
		{
			networkObject.DeferredDespawnTick = DeferredDespawnTick;
			bool hasDeferredDespawnCheck = networkObject.OnDeferredDespawnComplete != null;
			networkManager.SpawnManager.DeferDespawnNetworkObject(NetworkObjectId, DeferredDespawnTick, hasDeferredDespawnCheck, DestroyGameObject);
		}
	}
}
