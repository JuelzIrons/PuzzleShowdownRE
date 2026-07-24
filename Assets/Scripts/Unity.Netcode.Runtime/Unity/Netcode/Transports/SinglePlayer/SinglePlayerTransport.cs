namespace Unity.Netcode.Transports.SinglePlayer
{
	[global::UnityEngine.AddComponentMenu("Netcode/Single Player Transport")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@latest/?subfolder=/api/Unity.Netcode.Transports.SinglePlayer.SinglePlayerTransport.html")]
	public class SinglePlayerTransport : global::Unity.Netcode.NetworkTransport
	{
		private struct MessageData
		{
			public ulong FromClientId;

			public global::System.ArraySegment<byte> Payload;

			public global::Unity.Netcode.NetworkEvent Event;

			public float AvailableTime;
		}

		internal static string NotStartingAsHostErrorMessage = "When using SinglePlayerTransport, you must start a hosted session so both client and server are available locally.";

		private static global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.Queue<global::Unity.Netcode.Transports.SinglePlayer.SinglePlayerTransport.MessageData>> s_MessageQueue = new global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.Queue<global::Unity.Netcode.Transports.SinglePlayer.SinglePlayerTransport.MessageData>>();

		private ulong m_TransportId;

		private global::Unity.Netcode.NetworkManager m_NetworkManager;

		public override ulong ServerClientId { get; }

		public override void Send(ulong clientId, global::System.ArraySegment<byte> payload, global::Unity.Netcode.NetworkDelivery networkDelivery)
		{
			byte[] array = new byte[payload.Array.Length];
			global::System.Array.Copy(payload.Array, array, payload.Array.Length);
			s_MessageQueue[clientId].Enqueue(new global::Unity.Netcode.Transports.SinglePlayer.SinglePlayerTransport.MessageData
			{
				FromClientId = m_TransportId,
				Payload = new global::System.ArraySegment<byte>(array, payload.Offset, payload.Count),
				Event = global::Unity.Netcode.NetworkEvent.Data,
				AvailableTime = (float)m_NetworkManager.LocalTime.FixedTime
			});
		}

		public override global::Unity.Netcode.NetworkEvent PollEvent(out ulong clientId, out global::System.ArraySegment<byte> payload, out float receiveTime)
		{
			if (s_MessageQueue[m_TransportId].Count > 0)
			{
				global::Unity.Netcode.Transports.SinglePlayer.SinglePlayerTransport.MessageData messageData = s_MessageQueue[m_TransportId].Peek();
				if ((double)messageData.AvailableTime > m_NetworkManager.LocalTime.FixedTime)
				{
					clientId = 0uL;
					payload = default(global::System.ArraySegment<byte>);
					receiveTime = 0f;
					return global::Unity.Netcode.NetworkEvent.Nothing;
				}
				s_MessageQueue[m_TransportId].Dequeue();
				clientId = messageData.FromClientId;
				payload = messageData.Payload;
				receiveTime = m_NetworkManager.LocalTime.TimeAsFloat;
				if (m_NetworkManager.IsServer && messageData.Event == global::Unity.Netcode.NetworkEvent.Connect)
				{
					s_MessageQueue[messageData.FromClientId].Enqueue(new global::Unity.Netcode.Transports.SinglePlayer.SinglePlayerTransport.MessageData
					{
						Event = global::Unity.Netcode.NetworkEvent.Connect,
						FromClientId = ServerClientId,
						Payload = default(global::System.ArraySegment<byte>)
					});
				}
				return messageData.Event;
			}
			clientId = 0uL;
			payload = default(global::System.ArraySegment<byte>);
			receiveTime = 0f;
			return global::Unity.Netcode.NetworkEvent.Nothing;
		}

		public override bool StartClient()
		{
			global::Unity.Netcode.NetworkLog.LogError(NotStartingAsHostErrorMessage);
			return false;
		}

		public override bool StartServer()
		{
			s_MessageQueue[ServerClientId] = new global::System.Collections.Generic.Queue<global::Unity.Netcode.Transports.SinglePlayer.SinglePlayerTransport.MessageData>();
			if (!m_NetworkManager.LocalClient.IsHost && m_NetworkManager.LocalClient.IsServer)
			{
				global::Unity.Netcode.NetworkLog.LogError(NotStartingAsHostErrorMessage);
				return false;
			}
			return true;
		}

		public override void DisconnectRemoteClient(ulong clientId)
		{
			s_MessageQueue[clientId].Enqueue(new global::Unity.Netcode.Transports.SinglePlayer.SinglePlayerTransport.MessageData
			{
				Event = global::Unity.Netcode.NetworkEvent.Disconnect,
				FromClientId = m_TransportId,
				Payload = default(global::System.ArraySegment<byte>)
			});
		}

		public override void DisconnectLocalClient()
		{
			s_MessageQueue[ServerClientId].Enqueue(new global::Unity.Netcode.Transports.SinglePlayer.SinglePlayerTransport.MessageData
			{
				Event = global::Unity.Netcode.NetworkEvent.Disconnect,
				FromClientId = m_TransportId,
				Payload = default(global::System.ArraySegment<byte>)
			});
		}

		public override ulong GetCurrentRtt(ulong clientId)
		{
			return 0uL;
		}

		public override void Shutdown()
		{
			s_MessageQueue.Clear();
			m_TransportId = 0uL;
		}

		protected override global::Unity.Netcode.NetworkTopologyTypes OnCurrentTopology()
		{
			if (!(m_NetworkManager != null))
			{
				return global::Unity.Netcode.NetworkTopologyTypes.ClientServer;
			}
			return m_NetworkManager.NetworkConfig.NetworkTopology;
		}

		public override void Initialize(global::Unity.Netcode.NetworkManager networkManager = null)
		{
			s_MessageQueue.Clear();
			m_NetworkManager = networkManager;
		}
	}
}
