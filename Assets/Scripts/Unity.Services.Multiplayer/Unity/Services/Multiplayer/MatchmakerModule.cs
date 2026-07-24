namespace Unity.Services.Multiplayer
{
	internal class MatchmakerModule : global::Unity.Services.Multiplayer.IModule
	{
		private readonly global::Unity.Services.Multiplayer.ISession m_Session;

		private bool m_Enabled;

		private bool m_Initialized;

		private bool m_StartBackfillOnInit;

		private bool m_AutomaticallyRemovePlayersFromBackfill = true;

		private bool m_AutomaticallyStartBackfillingWhenPlayerIsMissing = true;

		private bool m_BackfillIsActive;

		private int m_PlayerConnectionTimeout = 30;

		private int m_BackfillingLoopInterval = 1;

		private global::Unity.Services.Matchmaker.Models.BackfillTicket m_LocalBackfillTicket;

		private global::Unity.Services.Matchmaker.Models.StoredMatchProperties m_MatchProperties;

		private readonly global::System.Collections.Generic.Dictionary<string, global::System.DateTime> m_PlayerWaitingConnection = new global::System.Collections.Generic.Dictionary<string, global::System.DateTime>();

		private long? m_ApproveActionID;

		private bool m_LocalDataDirty;

		private string m_Connection;

		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private readonly global::Unity.Services.Matchmaker.IMatchmakerService m_MatchmakerService;

		private const string k_TeamIdProperty = "TeamId";

		private const string k_TeamNameProperty = "TeamName";

		public global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults MatchmakingResults { get; private set; }

		public MatchmakerModule(global::Unity.Services.Multiplayer.ISession session, global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::Unity.Services.Matchmaker.IMatchmakerService matchmakerService)
		{
			m_ActionScheduler = actionScheduler;
			m_MatchmakerService = matchmakerService;
			m_Session = session;
		}

		public async global::System.Threading.Tasks.Task InitializeAsync()
		{
			if (m_Enabled)
			{
				if (!(await FetchMatchmakingResults()))
				{
					throw new global::Unity.Services.Multiplayer.SessionException("Error while fetching Matchmaking Results", global::Unity.Services.Multiplayer.SessionError.InvalidMatchmakerResults);
				}
				if (m_Session.MaxPlayers < MatchmakingResults.MatchProperties.MaxPlayers)
				{
					throw new global::Unity.Services.Multiplayer.SessionException($"MaxPlayers in Session ({m_Session.MaxPlayers}) is less than the MaxPlayers configured in Matchmaker rules ({MatchmakingResults.MatchProperties.MaxPlayers}).", global::Unity.Services.Multiplayer.SessionError.InvalidCreateSessionOptions);
				}
				m_Initialized = true;
				if (m_Session.IsServer)
				{
					InitializeBackfilling();
				}
			}
		}

		public async global::System.Threading.Tasks.Task LeaveAsync()
		{
			if (m_Initialized && m_LocalBackfillTicket != null)
			{
				await StopBackfillingAsync();
			}
		}

		internal void Enable()
		{
			m_Enabled = true;
		}

		public async global::System.Threading.Tasks.Task StartBackfillingAsync()
		{
			if (!m_Initialized)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("This session cannot be backfilled as it was not created by matchmaking.", global::Unity.Services.Multiplayer.SessionError.InvalidOperation);
			}
			if (!m_ApproveActionID.HasValue && ShouldBackfill())
			{
				global::Unity.Services.Matchmaker.CreateBackfillTicketOptions options = new global::Unity.Services.Matchmaker.CreateBackfillTicketOptions(MatchmakingResults.QueueName, m_Connection, null, new global::Unity.Services.Matchmaker.Models.BackfillTicketProperties(global::Unity.Services.Matchmaker.Models.ConversionExtensions.ToMatchProperties(m_MatchProperties)), matchId: m_Session.Id, poolId: MatchmakingResults.PoolId).WithCustomConnection();
				string backfillTicketId;
				try
				{
					backfillTicketId = await m_MatchmakerService.CreateBackfillTicketAsync(options);
				}
				catch (global::System.Exception ex)
				{
					global::Unity.Services.Multiplayer.Logger.LogError("Error while creating backfill ticket: " + ex.Message);
					return;
				}
				global::Unity.Services.Matchmaker.Models.BackfillTicket backfillTicket = await ApproveBackfillTicket(backfillTicketId);
				if (backfillTicket != null)
				{
					m_LocalBackfillTicket = backfillTicket;
				}
				m_BackfillIsActive = true;
				ScheduleApproveBackfillLoop();
			}
		}

		public async global::System.Threading.Tasks.Task StopBackfillingAsync()
		{
			if (!m_BackfillIsActive || !m_Initialized)
			{
				return;
			}
			m_BackfillIsActive = false;
			if (m_ApproveActionID.HasValue)
			{
				m_ActionScheduler.CancelAction(m_ApproveActionID.Value);
				m_ApproveActionID = null;
			}
			if (m_LocalBackfillTicket == null)
			{
				return;
			}
			try
			{
				await m_MatchmakerService.DeleteBackfillTicketAsync(m_LocalBackfillTicket.Id);
			}
			catch (global::System.Exception ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Error while deleling backfill ticket: " + ex.Message);
				throw;
			}
		}

		private int GetBackfillPlayerCount()
		{
			return m_LocalBackfillTicket?.Properties.MatchProperties.Players.Count ?? 0;
		}

		private bool IsBackfillFull()
		{
			return GetBackfillPlayerCount() >= m_Session.MaxPlayers;
		}

		private bool ShouldBackfill()
		{
			if (!m_Session.IsLocked)
			{
				return !IsBackfillFull();
			}
			return false;
		}

		private void OnPlayerJoined(string playerId)
		{
			if (!LocalMatchPropertiesAreValid())
			{
				throw new global::Unity.Services.Multiplayer.SessionException("State of the local match is invalid. Cannot add player.", global::Unity.Services.Multiplayer.SessionError.InvalidLocalMatchProperties);
			}
			if (m_MatchProperties.Players.Exists((global::Unity.Services.Matchmaker.Models.Player p) => p.Id == playerId))
			{
				return;
			}
			bool flag = false;
			if (m_LocalBackfillTicket != null)
			{
				if (!LocalBackfillTicketIsValid())
				{
					throw new global::Unity.Services.Multiplayer.SessionException("State of the backfill ticket is invalid. Cannot add player.", global::Unity.Services.Multiplayer.SessionError.InvalidBackfillTicket);
				}
				if (FindPlayerByIdInBackfillTicket(playerId, out var player))
				{
					flag = true;
					m_PlayerWaitingConnection.Remove(playerId);
					if (!m_MatchProperties.Players.Exists((global::Unity.Services.Matchmaker.Models.Player p) => p.Id == playerId))
					{
						m_MatchProperties.Players.Add(player);
					}
					if (!FindPlayerTeamInBackfill(playerId, out var foundTeam))
					{
						throw new global::Unity.Services.Multiplayer.SessionException("Could not find team with id " + foundTeam.TeamId + " of the player in the backfill ticket.", global::Unity.Services.Multiplayer.SessionError.InvalidBackfillTicket);
					}
					if (FindTeamByIdInLocalMatchProperties(foundTeam.TeamId, out var team))
					{
						if (!team.PlayerIds.Contains(playerId))
						{
							team.PlayerIds.Add(playerId);
						}
					}
					else
					{
						global::Unity.Services.Matchmaker.Models.Team item = new global::Unity.Services.Matchmaker.Models.Team(foundTeam.TeamName, foundTeam.TeamId, new global::System.Collections.Generic.List<string> { playerId });
						m_MatchProperties.Teams.Add(item);
					}
				}
			}
			if (flag)
			{
				return;
			}
			global::Unity.Services.Multiplayer.IReadOnlyPlayer readOnlyPlayer = global::System.Linq.Enumerable.FirstOrDefault(m_Session.Players, (global::Unity.Services.Multiplayer.IReadOnlyPlayer p) => p.Id == playerId);
			if (readOnlyPlayer == null)
			{
				return;
			}
			if (!readOnlyPlayer.Properties.TryGetValue("TeamId", out var value))
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Player does not have TeamId property. Cannot add player to the backfill ticket.", global::Unity.Services.Multiplayer.SessionError.PlayerMissingTeamProperties);
			}
			string value2 = value.Value;
			if (!FindTeamByIdInLocalMatchProperties(value2, out var team2))
			{
				team2 = new global::Unity.Services.Matchmaker.Models.Team(value2, value2, new global::System.Collections.Generic.List<string>());
				m_MatchProperties.Teams.Add(team2);
			}
			global::Unity.Services.Matchmaker.Models.Player player2 = new global::Unity.Services.Matchmaker.Models.Player(readOnlyPlayer.Id, readOnlyPlayer.Properties);
			team2.PlayerIds.Add(playerId);
			m_MatchProperties.Players.Add(player2);
			if (m_LocalBackfillTicket == null)
			{
				return;
			}
			if (!BackfillTicketIsValid())
			{
				throw new global::Unity.Services.Multiplayer.SessionException("State of the backfill ticket is invalid. Cannot add player.", global::Unity.Services.Multiplayer.SessionError.InvalidBackfillTicket);
			}
			if (!FindPlayerByIdInBackfillTicket(player2.Id, out var _))
			{
				m_LocalBackfillTicket.Properties.MatchProperties.Players.Add(player2);
				m_LocalDataDirty = true;
			}
			if (FindTeamByIdInBackfillTicket(value2, out var team3))
			{
				if (!team3.PlayerIds.Contains(playerId))
				{
					team3.PlayerIds.Add(playerId);
					m_LocalDataDirty = true;
				}
			}
			else
			{
				readOnlyPlayer.Properties.TryGetValue("TeamId", out var value3);
				team3 = new global::Unity.Services.Matchmaker.Models.Team(value3?.Value, value2, new global::System.Collections.Generic.List<string> { playerId });
				m_LocalBackfillTicket.Properties.MatchProperties.Teams.Add(team3);
				m_LocalDataDirty = true;
			}
		}

		private void OnPlayerLeft(string playerId)
		{
			if (!m_AutomaticallyRemovePlayersFromBackfill)
			{
				return;
			}
			if (!LocalMatchPropertiesAreValid())
			{
				throw new global::Unity.Services.Multiplayer.SessionException("State of the local match properties are invalid. Cannot remove player.", global::Unity.Services.Multiplayer.SessionError.InvalidLocalMatchProperties);
			}
			global::Unity.Services.Matchmaker.Models.Player player = global::System.Linq.Enumerable.FirstOrDefault(m_MatchProperties.Players, (global::Unity.Services.Matchmaker.Models.Player p) => p.Id == playerId);
			if (player != null && m_MatchProperties.Players.Remove(player) && FindPlayerTeamInLocalMatchProperties(playerId, out var foundTeam))
			{
				foundTeam.PlayerIds.Remove(playerId);
			}
			if (m_LocalBackfillTicket != null)
			{
				RemovePlayerFromBackfill(playerId);
			}
			if (ShouldBackfill() && !m_BackfillIsActive && m_AutomaticallyStartBackfillingWhenPlayerIsMissing)
			{
				m_ActionScheduler.ScheduleAction(async delegate
				{
					await StartBackfillingAsync();
				});
			}
		}

		private void RemovePlayerFromBackfill(string playerId)
		{
			if (!BackfillTicketIsValid())
			{
				throw new global::Unity.Services.Multiplayer.SessionException("State of the backfill ticket is invalid. Cannot add player.", global::Unity.Services.Multiplayer.SessionError.InvalidBackfillTicket);
			}
			if (FindPlayerByIdInBackfillTicket(playerId, out var player))
			{
				if (m_LocalBackfillTicket.Properties.MatchProperties.Players.Remove(player))
				{
					m_LocalDataDirty = true;
				}
				if (FindPlayerTeamInBackfill(playerId, out var foundTeam))
				{
					foundTeam.PlayerIds.Remove(playerId);
					m_LocalDataDirty = true;
				}
			}
		}

		private bool FindPlayerByIdInBackfillTicket(string userID, out global::Unity.Services.Matchmaker.Models.Player player)
		{
			player = global::System.Linq.Enumerable.FirstOrDefault(m_LocalBackfillTicket.Properties.MatchProperties.Players, (global::Unity.Services.Matchmaker.Models.Player p) => p.Id.Equals(userID));
			return player != null;
		}

		private bool FindTeamByIdInLocalMatchProperties(string teamId, out global::Unity.Services.Matchmaker.Models.Team team)
		{
			team = global::System.Linq.Enumerable.FirstOrDefault(m_MatchProperties.Teams, (global::Unity.Services.Matchmaker.Models.Team t) => t.TeamId == teamId);
			return team != null;
		}

		private bool FindTeamByIdInBackfillTicket(string teamId, out global::Unity.Services.Matchmaker.Models.Team team)
		{
			team = global::System.Linq.Enumerable.FirstOrDefault(m_LocalBackfillTicket.Properties.MatchProperties.Teams, (global::Unity.Services.Matchmaker.Models.Team t) => t.TeamId == teamId);
			return team != null;
		}

		private bool FindPlayerTeamInBackfill(string playerId, out global::Unity.Services.Matchmaker.Models.Team foundTeam)
		{
			return FindPlayerTeamFromMatchProperties(playerId, m_LocalBackfillTicket.Properties.MatchProperties, out foundTeam);
		}

		private bool FindPlayerTeamInLocalMatchProperties(string playerId, out global::Unity.Services.Matchmaker.Models.Team foundTeam)
		{
			return FindPlayerTeamFromMatchProperties(playerId, global::Unity.Services.Matchmaker.Models.ConversionExtensions.ToMatchProperties(m_MatchProperties), out foundTeam);
		}

		private bool FindPlayerTeamFromMatchProperties(string userID, global::Unity.Services.Matchmaker.Models.MatchProperties matchProperties, out global::Unity.Services.Matchmaker.Models.Team foundTeam)
		{
			foundTeam = null;
			foreach (global::Unity.Services.Matchmaker.Models.Team team in matchProperties.Teams)
			{
				if (team.PlayerIds.Contains(userID))
				{
					foundTeam = team;
					return true;
				}
			}
			return false;
		}

		private void InitializeBackfilling()
		{
			if (m_BackfillingLoopInterval < 1)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Backfilling loop interval must be greater than 0.", global::Unity.Services.Multiplayer.SessionError.InvalidOperation);
			}
			m_Session.PlayerHasLeft += OnPlayerLeft;
			m_Session.PlayerJoined += OnPlayerJoined;
			if (!m_StartBackfillOnInit)
			{
				return;
			}
			if (MatchmakingResults.MatchProperties.BackfillTicketId != null)
			{
				m_ApproveActionID = m_ActionScheduler.ScheduleAction(async delegate
				{
					global::Unity.Services.Matchmaker.Models.BackfillTicket backfillTicket = await ApproveBackfillTicket(MatchmakingResults.MatchProperties.BackfillTicketId);
					if (backfillTicket != null)
					{
						m_LocalBackfillTicket = backfillTicket;
						m_Connection = m_LocalBackfillTicket.Connection;
						m_ApproveActionID = null;
						ScheduleApproveBackfillLoop();
						m_BackfillIsActive = true;
					}
				});
			}
			else if (ShouldBackfill() && !m_BackfillIsActive)
			{
				m_ActionScheduler.ScheduleAction(async delegate
				{
					await StartBackfillingAsync();
				});
			}
		}

		private void ScheduleApproveBackfillLoop()
		{
			if (!m_ApproveActionID.HasValue)
			{
				m_ApproveActionID = m_ActionScheduler.ScheduleAction(ApproveBackfillLoop, m_BackfillingLoopInterval);
			}
		}

		private async void ApproveBackfillLoop()
		{
			m_ApproveActionID = null;
			if (!m_BackfillIsActive)
			{
				return;
			}
			if (!ShouldBackfill())
			{
				await StopBackfillingAsync();
				return;
			}
			ValidateAndRemovePlayersPendingConnection();
			if (m_LocalDataDirty)
			{
				try
				{
					if (!BackfillTicketIsValid())
					{
						global::Unity.Services.Multiplayer.Logger.LogError("Backfill ticket is invalid - Cannot update backfill ticket.");
						return;
					}
					await m_MatchmakerService.UpdateBackfillTicketAsync(m_LocalBackfillTicket.Id, m_LocalBackfillTicket);
					m_LocalDataDirty = false;
				}
				catch (global::System.Exception ex)
				{
					global::Unity.Services.Multiplayer.Logger.LogError("Error updating backfill ticket: " + ex.Message);
				}
			}
			else
			{
				try
				{
					if (m_LocalBackfillTicket == null)
					{
						global::Unity.Services.Multiplayer.Logger.LogError("Local backfill ticket is null. Backfilling needs to be started first.");
						return;
					}
					global::Unity.Services.Matchmaker.Models.BackfillTicket backfillTicket = await ApproveBackfillTicket(m_LocalBackfillTicket.Id);
					if (backfillTicket != null)
					{
						m_LocalBackfillTicket = backfillTicket;
					}
					AddNewPlayersToPendingPlayers();
				}
				catch (global::System.Exception ex2)
				{
					global::Unity.Services.Multiplayer.Logger.LogError("Error approving backfill ticket: " + ex2.Message);
				}
			}
			if (!ShouldBackfill())
			{
				await StopBackfillingAsync();
			}
			else
			{
				ScheduleApproveBackfillLoop();
			}
		}

		private void AddNewPlayersToPendingPlayers()
		{
			if (m_PlayerConnectionTimeout == 0 || !BackfillTicketIsValid() || !LocalMatchPropertiesAreValid())
			{
				return;
			}
			foreach (global::Unity.Services.Matchmaker.Models.Player backfillPlayer in m_LocalBackfillTicket.Properties.MatchProperties.Players)
			{
				if (!m_MatchProperties.Players.Exists((global::Unity.Services.Matchmaker.Models.Player p) => p.Id == backfillPlayer.Id) && !m_PlayerWaitingConnection.ContainsKey(backfillPlayer.Id))
				{
					m_PlayerWaitingConnection[backfillPlayer.Id] = global::System.DateTime.Now;
				}
			}
		}

		private void ValidateAndRemovePlayersPendingConnection()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.DateTime> item in m_PlayerWaitingConnection)
			{
				if (global::System.DateTime.Now > item.Value.AddSeconds(m_PlayerConnectionTimeout))
				{
					RemovePlayerFromBackfill(item.Key);
				}
			}
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Models.BackfillTicket> ApproveBackfillTicket(string backfillTicketId)
		{
			try
			{
				return await m_MatchmakerService.ApproveBackfillTicketAsync(backfillTicketId);
			}
			catch (global::System.Exception ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Error while approving backfill ticket: " + ex.Message);
				return null;
			}
		}

		private bool BackfillTicketIsValid()
		{
			if (m_LocalBackfillTicket == null)
			{
				return false;
			}
			if (m_LocalBackfillTicket.Properties != null && m_LocalBackfillTicket.Properties.MatchProperties != null && m_LocalBackfillTicket.Properties.MatchProperties.Players != null && m_LocalBackfillTicket.Properties.MatchProperties.Teams != null)
			{
				return m_LocalBackfillTicket.Properties.MatchProperties.Teams.Count > 0;
			}
			return false;
		}

		private async global::System.Threading.Tasks.Task<bool> FetchMatchmakingResults()
		{
			try
			{
				MatchmakingResults = await m_MatchmakerService.GetMatchmakingResultsAsync(m_Session.Id);
			}
			catch (global::System.Exception ex)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Error while fetching matchmaking results from allocation payload: " + ex.Message);
				return false;
			}
			if (MatchmakingResults == null)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Matchmaking results are null");
				return false;
			}
			if (MatchmakingResults.MatchProperties == null || MatchmakingResults.MatchProperties.Teams == null || MatchmakingResults.MatchProperties.Players == null)
			{
				global::Unity.Services.Multiplayer.Logger.LogError("Match properties on matchmaking results are invalid");
				return false;
			}
			m_MatchProperties = MatchmakingResults.MatchProperties;
			return true;
		}

		internal void SetBackfillingConfiguration(global::Unity.Services.Multiplayer.BackfillingConfiguration options)
		{
			m_StartBackfillOnInit = options.Enable;
			m_AutomaticallyRemovePlayersFromBackfill = options.AutomaticallyRemovePlayers;
			m_AutomaticallyStartBackfillingWhenPlayerIsMissing = options.AutoStart;
			m_BackfillingLoopInterval = options.BackfillingLoopInterval;
			m_PlayerConnectionTimeout = options.PlayerConnectionTimeout;
		}

		private bool LocalBackfillTicketIsValid()
		{
			if (m_LocalBackfillTicket.Properties != null && m_LocalBackfillTicket.Properties.MatchProperties != null && m_LocalBackfillTicket.Properties.MatchProperties.Players != null)
			{
				return m_LocalBackfillTicket.Properties.MatchProperties.Teams != null;
			}
			return false;
		}

		private bool LocalMatchPropertiesAreValid()
		{
			if (m_MatchProperties.Players != null && m_MatchProperties.Teams != null)
			{
				return m_MatchProperties.Players != null;
			}
			return false;
		}
	}
}
