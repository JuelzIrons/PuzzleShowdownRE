namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	public abstract class NetworkScenarioTask : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkScenario
	{
		private readonly global::System.Threading.CancellationTokenSource m_Cancellation = new global::System.Threading.CancellationTokenSource();

		public override void Start(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi networkEventsApi)
		{
			global::Unity.Multiplayer.Tools.Common.TaskExtensions.Forget(Run(networkEventsApi, m_Cancellation.Token));
		}

		public override void Dispose()
		{
			if (!m_Cancellation.IsCancellationRequested)
			{
				m_Cancellation.Cancel();
			}
			m_Cancellation?.Dispose();
		}

		protected abstract global::System.Threading.Tasks.Task Run(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi networkEventsApi, global::System.Threading.CancellationToken cancellationToken);
	}
}
