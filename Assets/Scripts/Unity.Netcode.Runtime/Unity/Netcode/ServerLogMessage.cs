namespace Unity.Netcode
{
	internal struct ServerLogMessage : global::Unity.Netcode.INetworkMessage
	{
		public ulong SenderId;

		public global::Unity.Netcode.NetworkLog.LogType LogType;

		public string Message;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			writer.WriteValueSafe(in LogType, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
			global::Unity.Netcode.BytePacker.WriteValuePacked(writer, Message);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, SenderId);
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			if ((networkManager.IsServer || networkManager.LocalClient.IsSessionOwner) && networkManager.NetworkConfig.EnableNetworkLogs)
			{
				reader.ReadValueSafe(out LogType, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
				global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out Message);
				global::Unity.Netcode.ByteUnpacker.ReadValuePacked(reader, out SenderId);
				if (networkManager.DAHost && networkManager.CurrentSessionOwner != networkManager.LocalClientId)
				{
					global::Unity.Netcode.ServerLogMessage message = this;
					int num = networkManager.ConnectionManager.SendMessage(ref message, global::Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced, networkManager.CurrentSessionOwner);
					networkManager.NetworkMetrics.TrackServerLogSent(networkManager.CurrentSessionOwner, (uint)LogType, num);
					return false;
				}
				return true;
			}
			return false;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager obj = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			ulong num = (obj.DistributedAuthorityMode ? SenderId : context.SenderId);
			obj.NetworkMetrics.TrackServerLogReceived(num, (uint)LogType, context.MessageSize);
			switch (LogType)
			{
			case global::Unity.Netcode.NetworkLog.LogType.Info:
				global::Unity.Netcode.NetworkLog.LogInfoServerLocal(Message, num);
				break;
			case global::Unity.Netcode.NetworkLog.LogType.Warning:
				global::Unity.Netcode.NetworkLog.LogWarningServerLocal(Message, num);
				break;
			case global::Unity.Netcode.NetworkLog.LogType.Error:
				global::Unity.Netcode.NetworkLog.LogErrorServerLocal(Message, num);
				break;
			}
		}
	}
}
