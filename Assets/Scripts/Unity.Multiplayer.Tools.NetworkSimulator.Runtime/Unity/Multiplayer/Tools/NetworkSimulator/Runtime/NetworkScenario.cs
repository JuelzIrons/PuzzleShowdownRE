namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	[global::System.Serializable]
	public abstract class NetworkScenario
	{
		internal delegate void PauseStateChangedHandler(bool isPaused);

		private global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi m_NetworkEventsApi;

		private bool m_Initialized;

		private bool m_HasStarted;

		private bool m_IsPaused = true;

		internal bool IsInitialized => m_Initialized;

		internal bool HasStarted => m_HasStarted;

		public bool IsPaused
		{
			get
			{
				return m_IsPaused;
			}
			set
			{
				if (m_Initialized && (m_IsPaused != value || !m_HasStarted) && !(m_IsPaused && value))
				{
					m_IsPaused = value;
					if (m_IsPaused)
					{
						OnPause();
					}
					else if (!m_HasStarted)
					{
						m_HasStarted = true;
						Start(m_NetworkEventsApi);
					}
					else
					{
						OnResume();
					}
					this.PauseStateChangedEvent(m_IsPaused);
				}
			}
		}

		internal event global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkScenario.PauseStateChangedHandler PauseStateChangedEvent = delegate
		{
		};

		internal void InitializeScenario(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi networkEventsApi, bool autoRun)
		{
			if (!m_HasStarted)
			{
				Analytic(autoRun);
				m_NetworkEventsApi = networkEventsApi;
				m_Initialized = true;
				if (autoRun)
				{
					m_HasStarted = true;
					Start(networkEventsApi);
					IsPaused = false;
				}
			}
		}

		private void Analytic(bool autoRun)
		{
		}

		public abstract void Start(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi networkEventsApi);

		public virtual void Dispose()
		{
		}

		protected virtual void OnPause()
		{
		}

		protected virtual void OnResume()
		{
		}
	}
}
