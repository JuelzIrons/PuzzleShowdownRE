namespace Unity.Services.Multiplayer
{
	internal class DaHandler : global::Unity.Services.Multiplayer.IDaHandler
	{
		private global::Unity.Services.Relay.Models.JoinAllocation m_JoinAllocation;

		private readonly global::Unity.Services.Relay.IRelayService m_RelayService;

		private readonly global::Unity.Services.DistributedAuthority.IDistributedAuthorityService m_DaService;

		private readonly global::Unity.Services.Authentication.Internal.IPlayerId m_PlayerId;

		public string RelayJoinCode { get; internal set; }

		public string Region { get; internal set; }

		public global::System.Guid AllocationId
		{
			get
			{
				if (m_JoinAllocation == null)
				{
					return global::System.Guid.Empty;
				}
				return m_JoinAllocation.AllocationId;
			}
		}

		public DaHandler(global::Unity.Services.Relay.IRelayService relayService, global::Unity.Services.DistributedAuthority.IDistributedAuthorityService daService, global::Unity.Services.Authentication.Internal.IPlayerId playerId)
		{
			m_RelayService = relayService;
			m_DaService = daService;
			m_PlayerId = playerId;
		}

		public async global::System.Threading.Tasks.Task CreateAndJoinSessionAsync(string lobbyId, string region)
		{
			ValidateNoAllocation();
			try
			{
				await m_DaService.CreateSessionForLobbyIdAsync(lobbyId, region);
				await JoinSessionAsync(await m_DaService.JoinSessionForLobbyIdAsync(lobbyId));
			}
			catch (global::Unity.Services.DistributedAuthority.Exceptions.DistributedAuthorityServiceException ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogError(ex.Message);
				throw new global::Unity.Services.Multiplayer.SessionException("Failed to create and join session", global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task JoinSessionAsync(string relayJoinCode)
		{
			ValidateNoAllocation();
			try
			{
				RelayJoinCode = relayJoinCode;
				m_JoinAllocation = await m_RelayService.JoinAllocationAsync(RelayJoinCode);
				Region = m_JoinAllocation.Region;
			}
			catch (global::Unity.Services.DistributedAuthority.Exceptions.DistributedAuthorityServiceException ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogError(ex.Message);
				throw new global::Unity.Services.Multiplayer.SessionException("Failed to join session", global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
			catch (global::System.Exception ex2)
			{
				global::Unity.Services.Multiplayer.Logger.LogError(ex2.Message);
				throw new global::Unity.Services.Multiplayer.SessionException("Failed to join session", global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public global::Unity.Networking.Transport.Relay.RelayServerData GetRelayServerData(global::Unity.Services.Multiplayer.RelayProtocol connectionType)
		{
			ValidateAllocationExists();
			try
			{
				return global::Unity.Services.Relay.Models.AllocationUtils.ToRelayServerData(m_JoinAllocation, connectionType);
			}
			catch (global::System.Exception ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogError(ex.Message);
				throw new global::Unity.Services.Multiplayer.SessionException("Failed to get relay server data", global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public global::Unity.Services.DistributedAuthority.ConnectPayload GetConnectPayload()
		{
			if (string.IsNullOrEmpty(m_PlayerId.PlayerId))
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Player must be authenticated to get connection payload", global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
			return new global::Unity.Services.DistributedAuthority.ConnectPayload(m_PlayerId.PlayerId);
		}

		private void ValidateNoAllocation()
		{
			if (m_JoinAllocation != null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("There is already an allocation", global::Unity.Services.Multiplayer.SessionError.AllocationAlreadyExists);
			}
		}

		private void ValidateAllocationExists()
		{
			if (m_JoinAllocation == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("There is no allocation", global::Unity.Services.Multiplayer.SessionError.AllocationNotFound);
			}
		}
	}
}
