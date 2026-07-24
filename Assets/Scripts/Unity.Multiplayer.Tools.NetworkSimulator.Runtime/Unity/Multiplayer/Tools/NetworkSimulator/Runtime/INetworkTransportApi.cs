namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	internal interface INetworkTransportApi
	{
		bool IsAvailable { get; }

		bool IsConnected { get; }

		void SimulateDisconnect();

		void SimulateReconnect();

		void UpdateNetworkParameters(global::Unity.Multiplayer.Tools.Adapters.NetworkParameters networkParameters);
	}
}
