namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	[global::UnityEngine.AddComponentMenu("Netcode/Network Simulator")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.multiplayer.tools@latest/?subfolder=/manual/network-simulator.html")]
	public class NetworkSimulator : global::UnityEngine.MonoBehaviour, global::System.ComponentModel.INotifyPropertyChanged, global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi
	{
		internal enum ScenarioPlaybackState
		{
			Initial = 0,
			Running = 1,
			Paused = 2
		}

		internal delegate void ScenarioChangedHandler(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkScenario newScenario);

		internal delegate void ScenarioPlaybackStateChangedHandler(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulator.ScenarioPlaybackState newState);

		[global::UnityEngine.SerializeField]
		internal global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPresetAsset m_PresetAsset;

		[global::UnityEngine.SerializeReference]
		[global::UnityEngine.HideInInspector]
		internal global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset m_PresetReference = new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPreset();

		[global::UnityEngine.SerializeReference]
		internal global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkScenario m_Scenario;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		internal bool m_IsScenarioSettingsFolded;

		[global::UnityEngine.SerializeField]
		public bool AutoRunScenario;

		private readonly global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkTransportApi m_NetworkTransportApi = new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkTransportApi();

		private global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi m_NetworkEventsApi;

		private global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset m_CachedPreset;

		private bool m_CachedScenarioIsPaused;

		private global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulator.ScenarioPlaybackState m_ScenarioPlaybackState;

		internal bool UsedEditorGUI;

		private string m_LastPresetName;

		internal global::System.ComponentModel.PropertyChangedEventHandler m_PropertyChanged;

		internal global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkEventsApi NetworkEventsApi
		{
			get
			{
				if (m_NetworkEventsApi != null)
				{
					return m_NetworkEventsApi;
				}
				m_NetworkEventsApi = new global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkEventsApi(this, m_NetworkTransportApi);
				return m_NetworkEventsApi;
			}
		}

		public bool IsConnected => NetworkEventsApi.IsConnected;

		public bool IsAvailable => NetworkEventsApi.IsAvailable;

		public global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset ConnectionPreset
		{
			get
			{
				if (!(m_PresetAsset != null))
				{
					return m_PresetReference;
				}
				return m_PresetAsset;
			}
			set
			{
				if (value is global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPresetAsset presetAsset)
				{
					m_PresetAsset = presetAsset;
					m_PresetReference = null;
				}
				else
				{
					m_PresetReference = value;
					m_PresetAsset = null;
				}
				UpdateLiveParameters();
				OnPropertyChanged("ConnectionPreset");
			}
		}

		public global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkScenario Scenario
		{
			get
			{
				return m_Scenario;
			}
			set
			{
				if (object.Equals(m_Scenario, value))
				{
					return;
				}
				global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkScenario scenario = Scenario;
				if (scenario != null)
				{
					scenario.PauseStateChangedEvent -= OnPauseStateChangedEvent;
				}
				m_Scenario = value;
				if (m_Scenario != null)
				{
					m_Scenario.PauseStateChangedEvent += OnPauseStateChangedEvent;
				}
				scenarioPlaybackState = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulator.ScenarioPlaybackState.Initial;
				if (global::UnityEngine.Application.isPlaying)
				{
					scenario?.Dispose();
					if (m_Scenario != null)
					{
						bool autoRun = AutoRunScenario && scenario != null && !scenario.IsPaused;
						m_Scenario.InitializeScenario(NetworkEventsApi, autoRun);
					}
				}
				this.ScenarioChangedEvent(m_Scenario);
				OnPropertyChanged("Scenario");
			}
		}

		internal global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulator.ScenarioPlaybackState scenarioPlaybackState
		{
			get
			{
				return m_ScenarioPlaybackState;
			}
			private set
			{
				if (m_ScenarioPlaybackState != value)
				{
					m_ScenarioPlaybackState = value;
					this.ScenarioPlaybackStateChangedEvent(scenarioPlaybackState);
				}
			}
		}

		public global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset CurrentPreset => NetworkEventsApi.CurrentPreset;

		event global::System.ComponentModel.PropertyChangedEventHandler global::System.ComponentModel.INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				m_PropertyChanged = (global::System.ComponentModel.PropertyChangedEventHandler)global::System.Delegate.Combine(m_PropertyChanged, value);
			}
			remove
			{
				m_PropertyChanged = (global::System.ComponentModel.PropertyChangedEventHandler)global::System.Delegate.Remove(m_PropertyChanged, value);
			}
		}

		internal event global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulator.ScenarioChangedHandler ScenarioChangedEvent = delegate
		{
		};

		internal event global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulator.ScenarioPlaybackStateChangedHandler ScenarioPlaybackStateChangedEvent = delegate
		{
		};

		private void OnPauseStateChangedEvent(bool isPaused)
		{
			scenarioPlaybackState = ((!isPaused) ? global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulator.ScenarioPlaybackState.Running : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulator.ScenarioPlaybackState.Paused);
		}

		internal void UpdateLiveParameters(bool forceUpdate = false)
		{
			if (forceUpdate || base.enabled)
			{
				Analytic(forceUpdate);
				m_NetworkTransportApi.UpdateNetworkParameters(new global::Unity.Multiplayer.Tools.Adapters.NetworkParameters
				{
					PacketDelayMilliseconds = (ConnectionPreset?.PacketDelayMs ?? 0),
					PacketDelayRangeMilliseconds = (ConnectionPreset?.PacketJitterMs ?? 0),
					PacketLossIntervalMilliseconds = (ConnectionPreset?.PacketLossInterval ?? 0),
					PacketLossPercent = (ConnectionPreset?.PacketLossPercent ?? 0)
				});
			}
		}

		private void Analytic(bool forceUpdate)
		{
		}

		private void SetUsedEditorReset()
		{
		}

		private void OnEnable()
		{
			SetUsedEditorReset();
			if (m_CachedPreset != null)
			{
				ConnectionPreset = m_CachedPreset;
			}
			if (Scenario != null)
			{
				Scenario.PauseStateChangedEvent += OnPauseStateChangedEvent;
				Scenario.IsPaused = m_CachedScenarioIsPaused;
			}
			UpdateLiveParameters();
			global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.OnAdapterAdded += NetworkAdapterAdded;
		}

		private void NetworkAdapterAdded(global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter obj)
		{
			UpdateLiveParameters();
		}

		private void Start()
		{
			scenarioPlaybackState = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulator.ScenarioPlaybackState.Initial;
			Scenario?.InitializeScenario(NetworkEventsApi, AutoRunScenario);
		}

		private void OnDisable()
		{
			m_CachedPreset = ConnectionPreset;
			ConnectionPreset = global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkSimulatorPresets.None;
			UpdateLiveParameters(forceUpdate: true);
			if (Scenario != null)
			{
				Scenario.PauseStateChangedEvent -= OnPauseStateChangedEvent;
			}
			global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.OnAdapterAdded -= NetworkAdapterAdded;
		}

		private void OnDestroy()
		{
			Scenario?.Dispose();
		}

		private void Update()
		{
			if (Scenario is global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkScenarioBehaviour networkScenarioBehaviour)
			{
				networkScenarioBehaviour.UpdateScenario(global::UnityEngine.Time.deltaTime);
			}
		}

		private void OnPropertyChanged([global::System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
		{
			m_PropertyChanged?.Invoke(this, new global::System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}

		public void Disconnect()
		{
			NetworkEventsApi.Disconnect();
		}

		public void Reconnect()
		{
			NetworkEventsApi.Reconnect();
		}

		public void TriggerLagSpike(global::System.TimeSpan duration)
		{
			NetworkEventsApi.TriggerLagSpike(duration);
		}

		public global::System.Threading.Tasks.Task TriggerLagSpikeAsync(global::System.TimeSpan duration)
		{
			return NetworkEventsApi.TriggerLagSpikeAsync(duration);
		}

		public void ChangeConnectionPreset(global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.INetworkSimulatorPreset preset)
		{
			NetworkEventsApi.ChangeConnectionPreset(preset);
		}
	}
}
