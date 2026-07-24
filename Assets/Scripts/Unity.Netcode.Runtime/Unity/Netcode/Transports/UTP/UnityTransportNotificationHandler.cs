namespace Unity.Netcode.Transports.UTP
{
	internal class UnityTransportNotificationHandler
	{
		private const int k_ClosedRemoteConnection = 128;

		private const int k_TransportShutdown = 129;

		internal const string DisconnectedMessage = "Gracefully disconnected.";

		internal const string TimeoutMessage = "Connection closed due to timed out.";

		internal const string MaxConnectionAttemptsMessage = "Connection closed due to maximum connection attempts reached.";

		internal const string ClosedByRemoteMessage = "Connection was closed by remote endpoint.";

		internal const string AuthenticationFailureMessage = "Connection closed due to authentication failure.";

		internal const string ProtocolErrorMessage = "Gracefully disconnected.";

		internal const string ClosedRemoteConnectionMessage = "Local transport closed the remote endpoint connection.";

		internal const string TransportShutdownMessage = "The transport was shutdown.";

		private global::System.Collections.Generic.Dictionary<int, global::Unity.Netcode.NetworkTransport.DisconnectEvents> m_DisconnectEventMap = new global::System.Collections.Generic.Dictionary<int, global::Unity.Netcode.NetworkTransport.DisconnectEvents>();

		private global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkTransport.DisconnectEvents, string> m_DisconnectEventMessageMap = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkTransport.DisconnectEvents, string>();

		internal global::Unity.Netcode.NetworkTransport.DisconnectEvents GetDisconnectEvent(int disconnectEventId)
		{
			if (!m_DisconnectEventMap.ContainsKey(disconnectEventId))
			{
				return global::Unity.Netcode.NetworkTransport.DisconnectEvents.Disconnected;
			}
			return m_DisconnectEventMap[disconnectEventId];
		}

		public string GetDisconnectEventMessage(global::Unity.Netcode.NetworkTransport.DisconnectEvents disconnectEvent)
		{
			if (!m_DisconnectEventMessageMap.ContainsKey(disconnectEvent))
			{
				return string.Empty;
			}
			return m_DisconnectEventMessageMap[disconnectEvent];
		}

		private void AddDisconnectEventMap(global::Unity.Netcode.NetworkTransport.DisconnectEvents disconnectEvent, int disconnectReason, string message)
		{
			m_DisconnectEventMap.Add(disconnectReason, disconnectEvent);
			m_DisconnectEventMessageMap.Add(disconnectEvent, message);
		}

		private void AddDisconnectEventMap(global::Unity.Netcode.NetworkTransport.DisconnectEvents disconnectEvent, global::Unity.Networking.Transport.Error.DisconnectReason disconnectReason, string message)
		{
			AddDisconnectEventMap(disconnectEvent, (int)disconnectReason, message);
		}

		public UnityTransportNotificationHandler()
		{
			AddDisconnectEventMap(global::Unity.Netcode.NetworkTransport.DisconnectEvents.Disconnected, global::Unity.Networking.Transport.Error.DisconnectReason.Default, "Gracefully disconnected.");
			AddDisconnectEventMap(global::Unity.Netcode.NetworkTransport.DisconnectEvents.ProtocolTimeout, global::Unity.Networking.Transport.Error.DisconnectReason.Timeout, "Connection closed due to timed out.");
			AddDisconnectEventMap(global::Unity.Netcode.NetworkTransport.DisconnectEvents.MaxConnectionAttempts, global::Unity.Networking.Transport.Error.DisconnectReason.MaxConnectionAttempts, "Connection closed due to maximum connection attempts reached.");
			AddDisconnectEventMap(global::Unity.Netcode.NetworkTransport.DisconnectEvents.ClosedByRemote, global::Unity.Networking.Transport.Error.DisconnectReason.ClosedByRemote, "Connection was closed by remote endpoint.");
			AddDisconnectEventMap(global::Unity.Netcode.NetworkTransport.DisconnectEvents.AuthenticationFailure, global::Unity.Networking.Transport.Error.DisconnectReason.AuthenticationFailure, "Connection closed due to authentication failure.");
			AddDisconnectEventMap(global::Unity.Netcode.NetworkTransport.DisconnectEvents.ProtocolError, global::Unity.Networking.Transport.Error.DisconnectReason.ProtocolError, "Gracefully disconnected.");
			AddDisconnectEventMap(global::Unity.Netcode.NetworkTransport.DisconnectEvents.ClosedRemoteConnection, 128, "Local transport closed the remote endpoint connection.");
			AddDisconnectEventMap(global::Unity.Netcode.NetworkTransport.DisconnectEvents.TransportShutdown, 129, "The transport was shutdown.");
		}
	}
}
