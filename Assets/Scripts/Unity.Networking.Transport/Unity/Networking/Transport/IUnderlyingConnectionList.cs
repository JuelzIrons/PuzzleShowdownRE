namespace Unity.Networking.Transport
{
	internal interface IUnderlyingConnectionList
	{
		bool TryConnect(ref global::Unity.Networking.Transport.NetworkEndpoint endpoint, ref global::Unity.Networking.Transport.ConnectionId underlyingConnection);

		void Disconnect(ref global::Unity.Networking.Transport.ConnectionId connectionId);

		global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection> QueryIncomingDisconnections(global::Unity.Collections.Allocator allocator);
	}
}
