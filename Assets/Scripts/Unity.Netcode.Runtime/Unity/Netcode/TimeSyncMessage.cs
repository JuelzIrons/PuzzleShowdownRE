namespace Unity.Netcode
{
	internal struct TimeSyncMessage : global::Unity.Netcode.INetworkMessage, global::Unity.Netcode.INetworkSerializeByMemcpy
	{
		public int Tick;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, Tick);
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			if (!((global::Unity.Netcode.NetworkManager)context.SystemOwner).IsClient)
			{
				return false;
			}
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out Tick);
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			global::Unity.Netcode.NetworkTime networkTime = new global::Unity.Netcode.NetworkTime(networkManager.NetworkTickSystem.TickRate, Tick);
			networkManager.NetworkTimeSystem.SyncCount++;
			networkManager.NetworkTimeSystem.Sync(networkTime.Time, (double)networkManager.NetworkConfig.NetworkTransport.GetCurrentRtt(context.SenderId) / 1000.0);
		}
	}
}
