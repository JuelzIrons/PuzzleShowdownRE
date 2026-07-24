namespace Unity.Services.Multiplayer
{
	public class DirectNetworkOptions
	{
		public global::Unity.Services.Multiplayer.ListenIPAddress ListenIp { get; }

		public global::Unity.Services.Multiplayer.PublishIPAddress PublishIp { get; }

		public ushort Port { get; }

		public DirectNetworkOptions()
			: this(global::Unity.Services.Multiplayer.ListenIPAddress.LoopbackIpv4, global::Unity.Services.Multiplayer.PublishIPAddress.LoopbackIpv4, 0)
		{
		}

		public DirectNetworkOptions(ushort port)
			: this(global::Unity.Services.Multiplayer.ListenIPAddress.LoopbackIpv4, global::Unity.Services.Multiplayer.PublishIPAddress.LoopbackIpv4, port)
		{
		}

		public DirectNetworkOptions(global::Unity.Services.Multiplayer.ListenIPAddress listenIp, global::Unity.Services.Multiplayer.PublishIPAddress publishIp, ushort port)
		{
			ListenIp = listenIp;
			PublishIp = publishIp;
			Port = port;
		}
	}
}
