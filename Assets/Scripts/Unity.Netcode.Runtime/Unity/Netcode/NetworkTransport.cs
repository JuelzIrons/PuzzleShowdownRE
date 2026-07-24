namespace Unity.Netcode
{
	public abstract class NetworkTransport : global::UnityEngine.MonoBehaviour
	{
		public delegate void TransportEventDelegate(global::Unity.Netcode.NetworkEvent eventType, ulong clientId, global::System.ArraySegment<byte> payload, float receiveTime);

		public enum DisconnectEvents
		{
			TransportShutdown = 0,
			Disconnected = 1,
			ProtocolTimeout = 2,
			MaxConnectionAttempts = 3,
			ClosedByRemote = 4,
			ClosedRemoteConnection = 5,
			AuthenticationFailure = 6,
			ProtocolError = 7
		}

		internal global::Unity.Netcode.INetworkMetrics NetworkMetrics;

		public abstract ulong ServerClientId { get; }

		public virtual bool IsSupported => true;

		public global::Unity.Netcode.NetworkTransport.DisconnectEvents DisconnectEvent { get; private set; }

		public string DisconnectEventMessage { get; private set; }

		public event global::Unity.Netcode.NetworkTransport.TransportEventDelegate OnTransportEvent;

		protected void InvokeOnTransportEvent(global::Unity.Netcode.NetworkEvent eventType, ulong clientId, global::System.ArraySegment<byte> payload, float receiveTime)
		{
			this.OnTransportEvent?.Invoke(eventType, clientId, payload, receiveTime);
		}

		public abstract void Send(ulong clientId, global::System.ArraySegment<byte> payload, global::Unity.Netcode.NetworkDelivery networkDelivery);

		public abstract global::Unity.Netcode.NetworkEvent PollEvent(out ulong clientId, out global::System.ArraySegment<byte> payload, out float receiveTime);

		public abstract bool StartClient();

		public abstract bool StartServer();

		public abstract void DisconnectRemoteClient(ulong clientId);

		public abstract void DisconnectLocalClient();

		public abstract ulong GetCurrentRtt(ulong clientId);

		public abstract void Shutdown();

		public abstract void Initialize(global::Unity.Netcode.NetworkManager networkManager = null);

		protected virtual void OnEarlyUpdate()
		{
		}

		internal void EarlyUpdate()
		{
			OnEarlyUpdate();
		}

		protected virtual void OnPostLateUpdate()
		{
		}

		internal void PostLateUpdate()
		{
			OnPostLateUpdate();
		}

		protected virtual global::Unity.Netcode.NetworkTopologyTypes OnCurrentTopology()
		{
			return global::Unity.Netcode.NetworkTopologyTypes.ClientServer;
		}

		internal global::Unity.Netcode.NetworkTopologyTypes CurrentTopology()
		{
			return OnCurrentTopology();
		}

		protected void SetDisconnectEvent(global::Unity.Netcode.NetworkTransport.DisconnectEvents disconnectEvent, string message = null)
		{
			DisconnectEvent = disconnectEvent;
			DisconnectEventMessage = string.Empty;
			if (message != null)
			{
				DisconnectEventMessage = message;
			}
			else
			{
				DisconnectEventMessage = GetDisconnectEventMessage(disconnectEvent);
			}
		}

		protected virtual string GetDisconnectEventMessage(global::Unity.Netcode.NetworkTransport.DisconnectEvents disconnectEvent)
		{
			return string.Empty;
		}

		internal void ClosingRemoteConnection()
		{
			SetDisconnectEvent(global::Unity.Netcode.NetworkTransport.DisconnectEvents.ClosedRemoteConnection);
		}

		internal void ShuttingDown()
		{
			SetDisconnectEvent(global::Unity.Netcode.NetworkTransport.DisconnectEvents.TransportShutdown);
		}
	}
}
