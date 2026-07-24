namespace Unity.Multiplayer.Tools.Adapters
{
	internal interface IGetConnectedClients : global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent
	{
		global::System.Collections.Generic.IReadOnlyList<global::Unity.Multiplayer.Tools.Adapters.ClientId> ConnectedClients { get; }

		event global::System.Action<global::Unity.Multiplayer.Tools.Adapters.ClientId> ClientConnectionEvent;

		event global::System.Action<global::Unity.Multiplayer.Tools.Adapters.ClientId> ClientDisconnectionEvent;
	}
}
