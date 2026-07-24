namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	internal class NoOpNetworkTransportApi : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkTransportApi, global::System.IDisposable
	{
		public bool IsAvailable => global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning<bool>(this, "IsAvailable");

		public bool IsConnected => global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning<bool>(this, "IsConnected");

		public void Dispose()
		{
			global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning(this, "Dispose");
		}

		public void SimulateDisconnect()
		{
			global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning(this, "SimulateDisconnect");
		}

		public void SimulateReconnect()
		{
			global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning(this, "SimulateReconnect");
		}

		public void UpdateNetworkParameters(global::Unity.Multiplayer.Tools.Adapters.NetworkParameters networkParameters)
		{
			global::Unity.Multiplayer.Tools.Common.RuntimeUtils.NoEffectWarning(this, "UpdateNetworkParameters");
		}
	}
}
