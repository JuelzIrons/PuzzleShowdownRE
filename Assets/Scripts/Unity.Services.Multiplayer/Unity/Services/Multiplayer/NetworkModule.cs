namespace Unity.Services.Multiplayer
{
	internal class NetworkModule : global::Unity.Services.Multiplayer.IHostSessionNetwork, global::Unity.Services.Multiplayer.IClientSessionNetwork, global::Unity.Services.Multiplayer.IModule
	{
		public const string SessionPropertyKey = "_session_network";

		private const string k_EnclosingTypeName = "NetworkModule";

		private static readonly string[] AvailableMethodNames = new string[3] { "StartDirectNetworkAsync", "StartRelayNetworkAsync", "StartDistributedAuthorityNetworkAsync" };

		private readonly global::Unity.Services.Multiplayer.IDaBuilder m_DaBuilder;

		private readonly global::Unity.Services.Multiplayer.INetworkBuilder m_NetworkBuilder;

		private readonly global::Unity.Services.Multiplayer.IRelayBuilder m_RelayBuilder;

		private readonly global::Unity.Services.Multiplayer.ISession m_Session;

		private global::Unity.Services.Multiplayer.IDaHandler m_DaHandler;

		private global::Unity.Services.Multiplayer.IRelayHandler m_RelayHandler;

		private global::Unity.Services.Multiplayer.INetworkHandler m_NetworkHandler;

		private global::Unity.Services.Multiplayer.SessionProperty m_Property;

		private global::Unity.Services.Multiplayer.NetworkMetadata m_NetworkMetadata;

		private global::Unity.Services.Multiplayer.NetworkState m_State;

		private bool m_IsMigrating;

		global::Unity.Services.Multiplayer.NetworkInfo global::Unity.Services.Multiplayer.IHostSessionNetwork.NetworkInfo => NetworkInfo;

		internal global::Unity.Services.Multiplayer.NetworkInfo NetworkInfo { get; set; }

		internal global::Unity.Services.Multiplayer.NetworkMetadata NetworkMetadata => m_NetworkMetadata;

		internal global::Unity.Services.Multiplayer.NetworkOptions NetworkOptions { get; set; }

		internal global::Unity.Services.Multiplayer.IDaHandler DaHandler => m_DaHandler;

		internal global::Unity.Services.Multiplayer.IRelayHandler RelayHandler => m_RelayHandler;

		internal global::Unity.Services.Multiplayer.IHostMigrationHandler HostMigrationHandler { get; set; }

		public global::Unity.Services.Multiplayer.IClientSessionNetwork ClientNetwork => this;

		public global::Unity.Services.Multiplayer.IHostSessionNetwork HostNetwork => this;

		public global::Unity.Services.Multiplayer.NetworkState State
		{
			get
			{
				return m_State;
			}
			private set
			{
				if (m_State != value && !m_IsMigrating)
				{
					m_State = value;
					this.StateChanged?.Invoke(value);
				}
			}
		}

		public global::Unity.Services.Multiplayer.INetworkHandler NetworkHandler
		{
			get
			{
				return m_NetworkHandler;
			}
			set
			{
				if (State != global::Unity.Services.Multiplayer.NetworkState.Stopped)
				{
					throw new global::Unity.Services.Multiplayer.SessionException("Trying to set the network handler when the network is being setup or has been setup.", global::Unity.Services.Multiplayer.SessionError.InvalidOperation);
				}
				m_NetworkHandler = value;
			}
		}

		public event global::System.Action<global::Unity.Services.Multiplayer.NetworkState> StateChanged;

		public event global::System.Action<global::Unity.Services.Multiplayer.SessionError> StartFailed;

		public event global::System.Action<global::Unity.Services.Multiplayer.SessionError> StopFailed;

		public event global::System.Action<global::Unity.Services.Multiplayer.SessionError> MigrationFailed;

		private static string Enumerate(in global::System.ReadOnlySpan<string> names)
		{
			global::System.ReadOnlySpan<string> readOnlySpan = names;
			string text = string.Join(", ", readOnlySpan.Slice(0, readOnlySpan.Length - 1).ToArray());
			readOnlySpan = names;
			return text + " or " + readOnlySpan[readOnlySpan.Length - 1];
		}

		internal NetworkModule(global::Unity.Services.Multiplayer.ISession session, global::Unity.Services.Multiplayer.INetworkBuilder networkBuilder, global::Unity.Services.Multiplayer.IDaBuilder daBuilder, global::Unity.Services.Multiplayer.IRelayBuilder relayBuilder)
		{
			m_Session = session;
			m_NetworkBuilder = networkBuilder;
			m_DaBuilder = daBuilder;
			m_RelayBuilder = relayBuilder;
			m_Session.Changed += OnSessionChanged;
			m_Session.SessionPropertiesChanged += OnSessionChanged;
			m_Session.SessionHostChanged += OnSessionHostChanged;
			HostMigrationHandler = null;
		}

		global::System.Threading.Tasks.Task global::Unity.Services.Multiplayer.IModule.InitializeAsync()
		{
			State = global::Unity.Services.Multiplayer.NetworkState.Stopped;
			if (NetworkInfo != null && m_Session.IsHost)
			{
				return StartNetworkWithOptionsAsync();
			}
			return ValidateNetworkPropertyAsync();
		}

		async global::System.Threading.Tasks.Task global::Unity.Services.Multiplayer.IModule.LeaveAsync()
		{
			await ResetAsync();
		}

		private async void OnSessionChanged()
		{
			if (State == global::Unity.Services.Multiplayer.NetworkState.Started && !m_Session.IsMember && !m_Session.IsServer)
			{
				await ResetAsync();
			}
			else if (!m_Session.IsHost)
			{
				await ValidateNetworkPropertyAsync();
			}
		}

		private async void OnSessionHostChanged(string newHostId)
		{
			await m_Session.ReconnectAsync();
			if (m_NetworkMetadata != null && HostMigrationHandler != null && m_Session.IsHost)
			{
				await MigrateHostNetworkAsync();
			}
		}

		private async global::System.Threading.Tasks.Task MigrateHostNetworkAsync()
		{
			if (HostMigrationHandler == null || m_NetworkMetadata == null || m_NetworkMetadata.Network == global::Unity.Services.Multiplayer.NetworkType.DistributedAuthority)
			{
				return;
			}
			try
			{
				StartMigration();
				global::Unity.Services.Multiplayer.NetworkMetadata previousMetadata = m_NetworkMetadata;
				switch (previousMetadata.Network)
				{
				case global::Unity.Services.Multiplayer.NetworkType.Direct:
					await ResetAsync();
					await ApplyMigrationAsync();
					await StartDirectNetworkAsync(new global::Unity.Services.Multiplayer.DirectNetworkOptions(global::Unity.Services.Multiplayer.ListenIPAddress.LoopbackIpv4, new global::Unity.Services.Multiplayer.PublishIPAddress(previousMetadata.Endpoint), 0));
					break;
				case global::Unity.Services.Multiplayer.NetworkType.Relay:
					await ResetAsync();
					await ApplyMigrationAsync();
					await StartRelayNetworkAsync(new global::Unity.Services.Multiplayer.RelayNetworkOptions(GetRelayProtocol(), previousMetadata.RelayRegion, !string.IsNullOrEmpty(previousMetadata.RelayRegion)));
					break;
				}
				await m_Session.AsHost().SavePropertiesAsync();
				CompleteMigration();
			}
			catch (global::Unity.Services.Multiplayer.SessionException ex)
			{
				FailMigration(ex.Error);
			}
			catch (global::System.Exception exception)
			{
				global::Unity.Services.Multiplayer.Logger.LogException(exception);
				FailMigration(global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
			}
		}

		private async global::System.Threading.Tasks.Task MigrateClientNetworkAsync(global::Unity.Services.Multiplayer.NetworkMetadata metadata)
		{
			StartMigration();
			try
			{
				await ResetAsync();
				await JoinNetworkAsync(metadata);
				CompleteMigration();
			}
			catch (global::Unity.Services.Multiplayer.SessionException ex)
			{
				FailMigration(ex.Error);
			}
			catch (global::System.Exception)
			{
				FailMigration(global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
			}
		}

		private async global::System.Threading.Tasks.Task StartNetworkWithOptionsAsync()
		{
			_ = 7;
			try
			{
				ValidateStartNetwork();
				State = global::Unity.Services.Multiplayer.NetworkState.Starting;
				string allocationId = null;
				global::Unity.Services.Multiplayer.NetworkRole networkRole = ((m_Session.CurrentPlayer == null) ? global::Unity.Services.Multiplayer.NetworkRole.Server : global::Unity.Services.Multiplayer.NetworkRole.Host);
				switch (NetworkInfo.Network)
				{
				case global::Unity.Services.Multiplayer.NetworkType.DistributedAuthority:
				{
					if (m_DaHandler == null)
					{
						m_DaHandler = m_DaBuilder.Build();
					}
					await m_DaHandler.CreateAndJoinSessionAsync(m_Session.Id, NetworkInfo.RelayOptions.Region);
					allocationId = m_DaHandler.AllocationId.ToString();
					global::Unity.Services.DistributedAuthority.ConnectPayload connectPayload = m_DaHandler.GetConnectPayload();
					await SetDistributedAuthorityConnectHash(connectPayload);
					global::Unity.Services.Multiplayer.NetworkConfiguration networkConfiguration3 = new global::Unity.Services.Multiplayer.NetworkConfiguration(networkRole, global::Unity.Services.Multiplayer.NetworkType.DistributedAuthority, m_DaHandler.GetRelayServerData(GetRelayProtocol()), connectPayload.SerializeToNativeArray());
					await StartNetworkHandlerAsync(networkConfiguration3);
					m_NetworkMetadata = new global::Unity.Services.Multiplayer.NetworkMetadata
					{
						Network = global::Unity.Services.Multiplayer.NetworkType.DistributedAuthority,
						RelayJoinCode = m_DaHandler.RelayJoinCode,
						RelayRegion = (NetworkInfo.RelayOptions.PreserveRegion ? m_DaHandler.Region : null),
						HostId = null
					};
					break;
				}
				case global::Unity.Services.Multiplayer.NetworkType.Relay:
				{
					if (m_RelayHandler == null)
					{
						m_RelayHandler = m_RelayBuilder.Build();
					}
					await m_RelayHandler.CreateAllocationAsync(m_Session.MaxPlayers, NetworkInfo.RelayOptions.Region);
					await m_RelayHandler.FetchJoinCodeAsync();
					global::Unity.Networking.Transport.Relay.RelayServerData relayServerData = m_RelayHandler.GetRelayServerData(GetRelayProtocol());
					global::Unity.Services.Multiplayer.NetworkConfiguration networkConfiguration2 = new global::Unity.Services.Multiplayer.NetworkConfiguration(networkRole, global::Unity.Services.Multiplayer.NetworkType.Relay, relayServerData, null);
					await StartNetworkHandlerAsync(networkConfiguration2);
					m_NetworkMetadata = new global::Unity.Services.Multiplayer.NetworkMetadata
					{
						Network = global::Unity.Services.Multiplayer.NetworkType.Relay,
						RelayJoinCode = m_RelayHandler.RelayJoinCode,
						RelayRegion = (NetworkInfo.RelayOptions.PreserveRegion ? m_RelayHandler.Region : null),
						HostId = m_Session.Host
					};
					allocationId = m_RelayHandler.AllocationId.ToString();
					break;
				}
				case global::Unity.Services.Multiplayer.NetworkType.Direct:
				{
					global::Unity.Services.Multiplayer.NetworkConfiguration networkConfiguration = new global::Unity.Services.Multiplayer.NetworkConfiguration(networkRole, NetworkInfo.PublishAddress, NetworkInfo.ListenAddress);
					await StartNetworkHandlerAsync(networkConfiguration);
					if (networkConfiguration.Role != global::Unity.Services.Multiplayer.NetworkRole.Client && networkConfiguration.DirectNetworkPublishAddress.Port == 0)
					{
						global::Unity.Services.Multiplayer.Logger.LogCallWarning("NetworkModule", string.Format("Port 0 on publish address {0} was not updated by network handler (hint: call {1}.{2}())", networkConfiguration.DirectNetworkPublishAddress, "NetworkConfiguration", "UpdatePublishPort"), "StartNetworkWithOptionsAsync");
					}
					m_NetworkMetadata = new global::Unity.Services.Multiplayer.NetworkMetadata
					{
						Network = global::Unity.Services.Multiplayer.NetworkType.Direct,
						Endpoint = new global::Unity.Services.Multiplayer.NetworkEndpointAddress(networkConfiguration.DirectNetworkPublishAddress),
						HostId = m_Session.Host
					};
					break;
				}
				}
				if (!string.IsNullOrEmpty(allocationId) && m_Session.CurrentPlayer != null)
				{
					m_Session.CurrentPlayer.SetAllocationId(allocationId);
				}
				HostMigrationHandler?.Start();
				global::Unity.Services.Multiplayer.SessionProperty property = new global::Unity.Services.Multiplayer.SessionProperty(global::Unity.Services.Multiplayer.NetworkMetadata.Serialize(m_NetworkMetadata), global::Unity.Services.Multiplayer.VisibilityPropertyOptions.Member);
				m_Session.AsHost().SetProperty("_session_network", property);
				await m_Session.AsHost().SavePropertiesAsync();
				State = global::Unity.Services.Multiplayer.NetworkState.Started;
			}
			catch (global::Unity.Services.Multiplayer.SessionException ex)
			{
				State = global::Unity.Services.Multiplayer.NetworkState.Stopped;
				this.StartFailed?.Invoke(ex.Error);
				throw;
			}
			catch (global::System.Exception arg)
			{
				global::Unity.Services.Multiplayer.SessionError sessionError = global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed;
				State = global::Unity.Services.Multiplayer.NetworkState.Stopped;
				this.StartFailed?.Invoke(sessionError);
				throw new global::Unity.Services.Multiplayer.SessionException($"Starting network failed with unexpected exception.\n{arg}", sessionError);
			}
		}

		private async global::System.Threading.Tasks.Task JoinNetworkAsync(global::Unity.Services.Multiplayer.NetworkMetadata metadata)
		{
			if (State == global::Unity.Services.Multiplayer.NetworkState.Starting)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Trying to connect when already connecting.", global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
			}
			if (State == global::Unity.Services.Multiplayer.NetworkState.Started)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Trying to connect when already connected.", global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
			}
			try
			{
				State = global::Unity.Services.Multiplayer.NetworkState.Starting;
				m_NetworkMetadata = metadata;
				string allocationId = null;
				if (m_RelayHandler == null)
				{
					m_RelayHandler = m_RelayBuilder.Build();
				}
				global::Unity.Services.Multiplayer.NetworkConfiguration networkConfiguration;
				switch (m_NetworkMetadata.Network)
				{
				case global::Unity.Services.Multiplayer.NetworkType.Direct:
					networkConfiguration = new global::Unity.Services.Multiplayer.NetworkConfiguration(global::Unity.Services.Multiplayer.NetworkRole.Client, new global::Unity.Services.Multiplayer.PublishIPAddress(m_NetworkMetadata.Endpoint), global::Unity.Services.Multiplayer.ListenIPAddress.LoopbackIpv4.WithPort(m_NetworkMetadata.Endpoint.Port));
					break;
				case global::Unity.Services.Multiplayer.NetworkType.Relay:
				{
					await m_RelayHandler.JoinAllocationAsync(m_NetworkMetadata.RelayJoinCode);
					global::Unity.Networking.Transport.Relay.RelayServerData relayServerData = m_RelayHandler.GetRelayServerData(GetRelayProtocol());
					networkConfiguration = new global::Unity.Services.Multiplayer.NetworkConfiguration(global::Unity.Services.Multiplayer.NetworkRole.Client, global::Unity.Services.Multiplayer.NetworkType.Relay, relayServerData, null);
					allocationId = m_RelayHandler.AllocationId.ToString();
					break;
				}
				case global::Unity.Services.Multiplayer.NetworkType.DistributedAuthority:
				{
					if (m_DaHandler == null)
					{
						m_DaHandler = m_DaBuilder.Build();
					}
					await m_DaHandler.JoinSessionAsync(m_NetworkMetadata.RelayJoinCode);
					allocationId = m_DaHandler.AllocationId.ToString();
					global::Unity.Services.DistributedAuthority.ConnectPayload connectPayload = m_DaHandler.GetConnectPayload();
					await SetDistributedAuthorityConnectHash(connectPayload);
					networkConfiguration = new global::Unity.Services.Multiplayer.NetworkConfiguration(global::Unity.Services.Multiplayer.NetworkRole.Client, global::Unity.Services.Multiplayer.NetworkType.DistributedAuthority, m_DaHandler.GetRelayServerData(GetRelayProtocol()), connectPayload.SerializeToNativeArray());
					break;
				}
				default:
					throw new global::System.ArgumentException($"Invalid transport type {m_NetworkMetadata.Network}");
				}
				await StartNetworkHandlerAsync(networkConfiguration);
				if (!string.IsNullOrEmpty(allocationId))
				{
					m_Session.CurrentPlayer.SetAllocationId(allocationId);
				}
				State = global::Unity.Services.Multiplayer.NetworkState.Started;
			}
			catch (global::Unity.Services.Multiplayer.SessionException ex)
			{
				State = global::Unity.Services.Multiplayer.NetworkState.Stopped;
				this.StartFailed?.Invoke(ex.Error);
				throw;
			}
			catch (global::System.Exception)
			{
				global::Unity.Services.Multiplayer.SessionError sessionError = global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed;
				State = global::Unity.Services.Multiplayer.NetworkState.Stopped;
				this.StartFailed?.Invoke(sessionError);
				throw new global::Unity.Services.Multiplayer.SessionException("Joining network failed.", sessionError);
			}
		}

		private bool IsNetworkSetup()
		{
			if (m_Session.Properties != null)
			{
				return m_Session.Properties.ContainsKey("_session_network");
			}
			return false;
		}

		private global::System.Threading.Tasks.Task ValidateNetworkPropertyAsync()
		{
			if (m_Session.IsHost)
			{
				return global::System.Threading.Tasks.Task.CompletedTask;
			}
			if (State == global::Unity.Services.Multiplayer.NetworkState.Started && !IsNetworkSetup())
			{
				return StopNetworkAsync();
			}
			if (m_Session.Properties.TryGetValue("_session_network", out var value) && m_Property?.Value != value.Value)
			{
				m_Property = value;
				return ProcessNetworkMetadataAsync(m_Property.Value);
			}
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		private async global::System.Threading.Tasks.Task ProcessNetworkMetadataAsync(string metadata)
		{
			_ = 1;
			try
			{
				global::Unity.Services.Multiplayer.NetworkMetadata networkMetadata = m_NetworkMetadata;
				global::Unity.Services.Multiplayer.NetworkMetadata networkMetadata2 = global::Unity.Services.Multiplayer.NetworkMetadata.Deserialize(metadata);
				if (State != global::Unity.Services.Multiplayer.NetworkState.Starting && State != global::Unity.Services.Multiplayer.NetworkState.Started && !m_Session.IsHost)
				{
					if (networkMetadata2.HostId == null || networkMetadata2.HostId.Equals(m_Session.Host))
					{
						await JoinNetworkAsync(networkMetadata2);
					}
				}
				else if (networkMetadata != null && State == global::Unity.Services.Multiplayer.NetworkState.Started && networkMetadata.HostId != networkMetadata2.HostId)
				{
					await MigrateClientNetworkAsync(networkMetadata2);
				}
			}
			catch (global::System.Exception)
			{
				State = global::Unity.Services.Multiplayer.NetworkState.Stopped;
				throw new global::Unity.Services.Multiplayer.SessionException("Unexpected exception processing network metadata", global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
			}
		}

		private global::System.Threading.Tasks.Task StartNetworkHandlerAsync(global::Unity.Services.Multiplayer.NetworkConfiguration networkConfiguration)
		{
			if (m_NetworkHandler == null)
			{
				m_NetworkHandler = m_NetworkBuilder.Build();
			}
			return NetworkHandler.StartAsync(networkConfiguration);
		}

		private void StartMigration()
		{
			State = global::Unity.Services.Multiplayer.NetworkState.Migrating;
			m_IsMigrating = true;
		}

		private void FailMigration(global::Unity.Services.Multiplayer.SessionError error)
		{
			m_IsMigrating = false;
			State = global::Unity.Services.Multiplayer.NetworkState.Stopped;
			this.MigrationFailed?.Invoke(error);
		}

		private void CompleteMigration()
		{
			m_IsMigrating = false;
			State = global::Unity.Services.Multiplayer.NetworkState.Started;
			m_Session.OnSessionMigrated();
		}

		private async global::System.Threading.Tasks.Task ApplyMigrationAsync()
		{
			if (HostMigrationHandler != null)
			{
				await HostMigrationHandler.ApplyMigrationDataAsync();
			}
		}

		private async global::System.Threading.Tasks.Task SetDistributedAuthorityConnectHash(global::Unity.Services.DistributedAuthority.ConnectPayload connectPayload)
		{
			m_Session.CurrentPlayer.SetProperty("_distributed_authority_connect_hash", new global::Unity.Services.Multiplayer.PlayerProperty(connectPayload.Base64SecretHash(), global::Unity.Services.Multiplayer.VisibilityPropertyOptions.Private));
			await m_Session.SaveCurrentPlayerDataAsync();
		}

		private void ValidateStartNetwork()
		{
			if (!m_Session.IsHost)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Trying to setup the network but the player isn't the host.", global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
			}
			ValidateNetworkStateForStartNetwork();
		}

		private void ValidateNetworkStateForStartNetwork()
		{
			if (State == global::Unity.Services.Multiplayer.NetworkState.Starting)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Trying to setup the network when already in progress.", global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
			}
			if (State == global::Unity.Services.Multiplayer.NetworkState.Started)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Trying to start the network when already started.", global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
			}
		}

		internal global::Unity.Services.Multiplayer.RelayProtocol GetRelayProtocol()
		{
			return NetworkOptions?.RelayProtocol ?? NetworkInfo?.RelayOptions?.Protocol ?? global::Unity.Services.Multiplayer.RelayProtocol.DTLS;
		}

		public async global::System.Threading.Tasks.Task StartDirectNetworkAsync(global::Unity.Services.Multiplayer.DirectNetworkOptions networkOptions)
		{
			if (networkOptions == null)
			{
				throw new global::System.ArgumentNullException("networkOptions", "Network options cannot be null.");
			}
			NetworkInfo = global::Unity.Services.Multiplayer.NetworkInfo.BuildDirect(networkOptions);
			await StartNetworkWithOptionsAsync();
		}

		public async global::System.Threading.Tasks.Task StartRelayNetworkAsync(global::Unity.Services.Multiplayer.RelayNetworkOptions networkOptions)
		{
			if (networkOptions == null)
			{
				throw new global::System.ArgumentNullException("networkOptions", "Network options cannot be null.");
			}
			NetworkInfo = global::Unity.Services.Multiplayer.NetworkInfo.BuildRelay(networkOptions);
			await StartNetworkWithOptionsAsync();
		}

		public async global::System.Threading.Tasks.Task StartDistributedAuthorityNetworkAsync(global::Unity.Services.Multiplayer.RelayNetworkOptions networkOptions)
		{
			if (networkOptions == null)
			{
				throw new global::System.ArgumentNullException("networkOptions", "Network options cannot be null.");
			}
			NetworkInfo = global::Unity.Services.Multiplayer.NetworkInfo.BuildDistributed(networkOptions);
			await StartNetworkWithOptionsAsync();
		}

		global::System.Threading.Tasks.Task global::Unity.Services.Multiplayer.IClientSessionNetwork.StopNetworkAsync()
		{
			return StopNetworkAsync();
		}

		public async global::System.Threading.Tasks.Task StopNetworkAsync()
		{
			if (State != global::Unity.Services.Multiplayer.NetworkState.Started)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Trying to stop the network when it is not started.", global::Unity.Services.Multiplayer.SessionError.InvalidOperation);
			}
			try
			{
				State = global::Unity.Services.Multiplayer.NetworkState.Stopping;
				await ResetAsync();
				if (m_Session.IsHost)
				{
					global::Unity.Services.Multiplayer.IHostSession hostSession = m_Session.AsHost();
					hostSession.SetProperty("_session_network", null);
					await hostSession.SavePropertiesAsync();
				}
				State = global::Unity.Services.Multiplayer.NetworkState.Stopped;
			}
			catch (global::System.Exception)
			{
				State = global::Unity.Services.Multiplayer.NetworkState.Started;
				this.StopFailed?.Invoke(global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
				throw new global::Unity.Services.Multiplayer.SessionException("Unexpected error while stopping network.", global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
			}
		}

		private async global::System.Threading.Tasks.Task ResetAsync()
		{
			HostMigrationHandler?.Stop();
			if (NetworkHandler != null)
			{
				await NetworkHandler.StopAsync();
			}
			State = global::Unity.Services.Multiplayer.NetworkState.Stopped;
			m_RelayHandler?.Disconnect();
			m_NetworkMetadata = null;
		}

		global::System.Threading.Tasks.Task global::Unity.Services.Multiplayer.IClientSessionNetwork.StartNetworkAsync()
		{
			ValidateNetworkStateForStartNetwork();
			if (NetworkInfo != null)
			{
				if (!m_Session.IsHost)
				{
					return ValidateNetworkPropertyAsync();
				}
				return StartNetworkWithOptionsAsync();
			}
			if (m_Session.IsHost)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Trying to start network on host session but there is no network metadata. Use " + Enumerate((global::System.ReadOnlySpan<string>)AvailableMethodNames) + " instead.", global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
			}
			throw new global::Unity.Services.Multiplayer.SessionException("Trying to start network as client but there is no network metadata.", global::Unity.Services.Multiplayer.SessionError.NetworkSetupFailed);
		}
	}
}
