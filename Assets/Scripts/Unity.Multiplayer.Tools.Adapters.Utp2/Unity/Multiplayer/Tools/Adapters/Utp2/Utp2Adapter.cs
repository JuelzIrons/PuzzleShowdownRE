namespace Unity.Multiplayer.Tools.Adapters.Utp2
{
	internal class Utp2Adapter : global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter, global::Unity.Multiplayer.Tools.Adapters.INetworkAvailability, global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent, global::Unity.Multiplayer.Tools.Adapters.ISimulateDisconnectAndReconnect, global::Unity.Multiplayer.Tools.Adapters.ISimulateDisconnect, global::Unity.Multiplayer.Tools.Adapters.ISimulateReconnect, global::Unity.Multiplayer.Tools.Adapters.IHandleNetworkParameters
	{
		private readonly global::Unity.Networking.Transport.NetworkDriver m_NetworkDriver;

		private global::Unity.Multiplayer.Tools.Adapters.NetworkParameters m_CurrentNetworkParameters;

		private bool m_IsDisabled;

		public global::Unity.Multiplayer.Tools.Adapters.AdapterMetadata Metadata { get; } = new global::Unity.Multiplayer.Tools.Adapters.AdapterMetadata
		{
			PackageInfo = new global::Unity.Multiplayer.Tools.Adapters.PackageInfo
			{
				PackageName = "com.unity.transport",
				Version = new global::Unity.Multiplayer.Tools.Adapters.PackageVersion
				{
					Major = 2,
					Minor = 0,
					Patch = 0,
					PreRelease = ""
				}
			}
		};

		public bool IsConnected
		{
			get
			{
				if (m_NetworkDriver.IsCreated)
				{
					return !m_IsDisabled;
				}
				return false;
			}
		}

		public global::Unity.Multiplayer.Tools.Adapters.NetworkParameters NetworkParameters
		{
			get
			{
				if (m_NetworkDriver.CurrentSettings.TryGet<global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters>(out var parameter))
				{
					global::Unity.Multiplayer.Tools.Adapters.NetworkParameters currentNetworkParameters = new global::Unity.Multiplayer.Tools.Adapters.NetworkParameters
					{
						PacketDelayMilliseconds = parameter.PacketDelayMs,
						PacketDelayRangeMilliseconds = parameter.PacketJitterMs,
						PacketLossIntervalMilliseconds = parameter.PacketDropInterval,
						PacketLossPercent = parameter.PacketDropPercentage
					};
					m_CurrentNetworkParameters = currentNetworkParameters;
				}
				return m_CurrentNetworkParameters;
			}
			set
			{
				if (m_NetworkDriver.CurrentSettings.TryGet<global::Unity.Networking.Transport.Utilities.SimulatorUtility.Parameters>(out var parameter))
				{
					m_CurrentNetworkParameters = value;
					parameter.PacketDelayMs = m_CurrentNetworkParameters.PacketDelayMilliseconds;
					parameter.PacketJitterMs = m_CurrentNetworkParameters.PacketDelayRangeMilliseconds;
					parameter.PacketDropInterval = m_CurrentNetworkParameters.PacketLossIntervalMilliseconds;
					parameter.PacketDropPercentage = m_CurrentNetworkParameters.PacketLossPercent;
					global::Unity.Networking.Transport.Utilities.SimulatorStageParameterExtensions.ModifySimulatorStageParameters(m_NetworkDriver, parameter);
				}
			}
		}

		public Utp2Adapter(global::Unity.Networking.Transport.NetworkDriver networkDriver)
		{
			m_NetworkDriver = networkDriver;
			m_CurrentNetworkParameters = new global::Unity.Multiplayer.Tools.Adapters.NetworkParameters();
		}

		public T GetComponent<T>() where T : class, global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent
		{
			return this as T;
		}

		public void SimulateDisconnect()
		{
			m_IsDisabled = true;
			global::Unity.Networking.Transport.NetworkSimulatorParameterExtensions.ModifyNetworkSimulatorParameters(m_NetworkDriver, new global::Unity.Networking.Transport.NetworkSimulatorParameter
			{
				ReceivePacketLossPercent = 100f,
				SendPacketLossPercent = 100f
			});
		}

		public void SimulateReconnect()
		{
			m_IsDisabled = false;
			global::Unity.Networking.Transport.NetworkSimulatorParameterExtensions.ModifyNetworkSimulatorParameters(m_NetworkDriver, new global::Unity.Networking.Transport.NetworkSimulatorParameter
			{
				ReceivePacketLossPercent = 0f,
				SendPacketLossPercent = 0f
			});
		}
	}
}
