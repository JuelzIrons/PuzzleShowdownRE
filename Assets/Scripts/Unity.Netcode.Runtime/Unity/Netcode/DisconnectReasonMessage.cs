namespace Unity.Netcode
{
	internal struct DisconnectReasonMessage : global::Unity.Netcode.INetworkMessage
	{
		public string Reason;

		public int Version => 0;

		public void Serialize(global::Unity.Netcode.FastBufferWriter writer, int targetVersion)
		{
			string s = Reason ?? string.Empty;
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(writer, Version);
			if (writer.TryBeginWrite(global::Unity.Netcode.FastBufferWriter.GetWriteSize(s)))
			{
				writer.WriteValue(s);
				return;
			}
			writer.WriteValueSafe(string.Empty);
			global::Unity.Netcode.NetworkLog.LogWarning("Disconnect reason didn't fit. Disconnected without sending a reason. Consider shortening the reason string.");
		}

		public bool Deserialize(global::Unity.Netcode.FastBufferReader reader, ref global::Unity.Netcode.NetworkContext context, int receivedMessageVersion)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out receivedMessageVersion);
			reader.ReadValueSafe(out Reason, false);
			return true;
		}

		public void Handle(ref global::Unity.Netcode.NetworkContext context)
		{
			global::Unity.Netcode.NetworkManager networkManager = (global::Unity.Netcode.NetworkManager)context.SystemOwner;
			networkManager.ConnectionManager.ServerDisconnectReason = Reason;
			if (networkManager.NetworkConfig.UseCMBService)
			{
				networkManager.StartCoroutine(HandleDisconnectAfterReason(networkManager));
			}
		}

		private global::System.Collections.IEnumerator HandleDisconnectAfterReason(global::Unity.Netcode.NetworkManager networkManager)
		{
			yield return new global::UnityEngine.WaitForFixedUpdate();
			global::Unity.Netcode.NetworkConnectionManager connectionManager = networkManager.ConnectionManager;
			connectionManager.DisconnectEventHandler(connectionManager.LocalClientTransportId);
		}
	}
}
