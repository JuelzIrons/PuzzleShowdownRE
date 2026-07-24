namespace Unity.Networking.Transport
{
	internal struct UnderlyingConnectionList : global::Unity.Networking.Transport.IUnderlyingConnectionList
	{
		private global::Unity.Networking.Transport.ConnectionList Connections;

		public UnderlyingConnectionList(ref global::Unity.Networking.Transport.ConnectionList connections)
		{
			Connections = connections;
		}

		public bool TryConnect(ref global::Unity.Networking.Transport.NetworkEndpoint endpoint, ref global::Unity.Networking.Transport.ConnectionId underlyingConnection)
		{
			if (underlyingConnection != default(global::Unity.Networking.Transport.ConnectionId))
			{
				if (Connections.GetConnectionState(underlyingConnection) == global::Unity.Networking.Transport.NetworkConnection.State.Connected)
				{
					return true;
				}
			}
			else
			{
				underlyingConnection = Connections.StartConnecting(ref endpoint);
			}
			return false;
		}

		public void Disconnect(ref global::Unity.Networking.Transport.ConnectionId connectionId)
		{
			global::Unity.Networking.Transport.NetworkConnection.State connectionState = Connections.GetConnectionState(connectionId);
			if (connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting && connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
			{
				Connections.StartDisconnecting(ref connectionId);
			}
		}

		public global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection> QueryIncomingDisconnections(global::Unity.Collections.Allocator allocator)
		{
			return Connections.QueryIncomingDisconnections(allocator);
		}
	}
}
