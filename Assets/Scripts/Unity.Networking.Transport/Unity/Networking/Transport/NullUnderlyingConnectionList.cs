namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct NullUnderlyingConnectionList : global::Unity.Networking.Transport.IUnderlyingConnectionList
	{
		public bool TryConnect(ref global::Unity.Networking.Transport.NetworkEndpoint endpoint, ref global::Unity.Networking.Transport.ConnectionId underlyingConnection)
		{
			underlyingConnection = default(global::Unity.Networking.Transport.ConnectionId);
			return true;
		}

		public void Disconnect(ref global::Unity.Networking.Transport.ConnectionId connectionId)
		{
		}

		public global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection> QueryIncomingDisconnections(global::Unity.Collections.Allocator allocator)
		{
			return default(global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection>);
		}
	}
}
