namespace Unity.Services.Multiplayer
{
	internal class RelayHandler : global::Unity.Services.Multiplayer.IRelayHandler
	{
		private global::Unity.Services.Relay.Models.Allocation m_Allocation;

		private global::Unity.Services.Relay.Models.JoinAllocation m_JoinAllocation;

		private readonly global::Unity.Services.Relay.IRelayService m_RelayService;

		public global::Unity.Services.Multiplayer.RelayState State { get; internal set; }

		public global::System.Guid AllocationId { get; internal set; }

		public string RelayJoinCode { get; internal set; }

		public string Region { get; internal set; }

		public RelayHandler(global::Unity.Services.Relay.IRelayService relayService)
		{
			m_RelayService = relayService;
		}

		public async global::System.Threading.Tasks.Task CreateAllocationAsync(int maxPlayers, string region = null)
		{
			ValidateNoAllocation();
			try
			{
				m_Allocation = await m_RelayService.CreateAllocationAsync(maxPlayers, region);
				AllocationId = m_Allocation.AllocationId;
				Region = m_Allocation.Region;
				State = global::Unity.Services.Multiplayer.RelayState.Created;
			}
			catch (global::Unity.Services.Relay.RelayServiceException ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogError(ex.Message);
				throw new global::Unity.Services.Multiplayer.SessionException("Failed to create allocation", global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task JoinAllocationAsync(string joinCode)
		{
			ValidateNoAllocation();
			try
			{
				m_JoinAllocation = await m_RelayService.JoinAllocationAsync(joinCode);
				AllocationId = m_JoinAllocation.AllocationId;
				Region = m_JoinAllocation.Region;
				State = global::Unity.Services.Multiplayer.RelayState.Joined;
			}
			catch (global::Unity.Services.Relay.RelayServiceException ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogError(ex.Message);
				throw new global::Unity.Services.Multiplayer.SessionException("Failed to join allocation", global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public async global::System.Threading.Tasks.Task FetchJoinCodeAsync()
		{
			ValidateAllocationExists();
			try
			{
				RelayJoinCode = await m_RelayService.GetJoinCodeAsync(AllocationId);
			}
			catch (global::Unity.Services.Relay.RelayServiceException ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogError(ex.Message);
				throw new global::Unity.Services.Multiplayer.SessionException("Failed to fetch relay join code", global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		public void Disconnect()
		{
			m_Allocation = null;
			m_JoinAllocation = null;
			AllocationId = global::System.Guid.Empty;
			Region = null;
			RelayJoinCode = null;
			State = global::Unity.Services.Multiplayer.RelayState.None;
		}

		public global::Unity.Networking.Transport.Relay.RelayServerData GetRelayServerData(global::Unity.Services.Multiplayer.RelayProtocol relayProtocol)
		{
			ValidateAllocationExists();
			try
			{
				switch (State)
				{
				case global::Unity.Services.Multiplayer.RelayState.Created:
					return global::Unity.Services.Relay.Models.AllocationUtils.ToRelayServerData(m_Allocation, relayProtocol);
				case global::Unity.Services.Multiplayer.RelayState.Joined:
					return global::Unity.Services.Relay.Models.AllocationUtils.ToRelayServerData(m_JoinAllocation, relayProtocol);
				}
			}
			catch (global::System.Exception ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogError(ex.Message);
				throw new global::Unity.Services.Multiplayer.SessionException("Failed to get relay server data", global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
			throw new global::Unity.Services.Multiplayer.SessionException("Invalid relay state", global::Unity.Services.Multiplayer.SessionError.Unknown);
		}

		private void ValidateNoAllocation()
		{
			if (m_JoinAllocation != null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("There is already a join allocation", global::Unity.Services.Multiplayer.SessionError.AllocationAlreadyExists);
			}
			if (m_Allocation != null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("There is already an allocation", global::Unity.Services.Multiplayer.SessionError.AllocationAlreadyExists);
			}
		}

		private void ValidateAllocationExists()
		{
			if (m_Allocation == null && m_JoinAllocation == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("There is no allocation", global::Unity.Services.Multiplayer.SessionError.AllocationNotFound);
			}
		}
	}
}
