namespace Unity.Services.Multiplayer
{
	internal class Matchmaker : global::Unity.Services.Multiplayer.ISessionMatchmaking
	{
		private const int k_PollingDelaySeconds = 1;

		private const int k_MatchmakingResultsRetryCount = 3;

		private const string k_AssignmentTimeoutMessage = "Matchmaking took longer than the timeout value configured for the pool.";

		private const string k_AssignmentFailedMessage = "Unknown failure while matchmaking.";

		private global::Unity.Services.Matchmaker.Models.CustomAssignment m_CustomAssignment;

		private global::Unity.Services.Matchmaker.Models.IpPortAssignment m_IpPortAssignment;

		private global::Unity.Services.Matchmaker.Models.MatchIdAssignment m_MatchIdAssignment;

		private global::Unity.Services.Matchmaker.Models.MultiplayAssignment m_MultiplayAssignment;

		private readonly global::Unity.Services.Multiplayer.SessionOptions m_SessionOptions;

		private long? m_PollingActionId;

		private readonly global::Unity.Services.Multiplayer.ISessionManager m_SessionManager;

		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private readonly global::Unity.Services.Matchmaker.IMatchmakerService m_MatchmakerService;

		private readonly global::Unity.Services.Authentication.Internal.IPlayerId m_PlayerId;

		private readonly global::Unity.Services.Authentication.Internal.IAccessToken m_AccessToken;

		private readonly global::Unity.Services.Authentication.Internal.IAccessTokenObserver m_AccessTokenObserver;

		private readonly global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Multiplayer.ISession> m_SessionCompletionSource;

		private const int k_MultiplaySessionJoinTimeoutSeconds = 10;

		public global::Unity.Services.Multiplayer.MatchmakerState State { get; internal set; }

		public global::Unity.Services.Multiplayer.MatchmakerAssignmentType AssignmentType { get; internal set; }

		public string TicketId { get; internal set; }

		private bool IsAuthorized => m_AccessToken.AccessToken != null;

		public event global::System.Action<global::Unity.Services.Multiplayer.MatchmakerState> StateChanged;

		public event global::System.Action MatchFound;

		public event global::System.Action MatchFailed;

		public event global::System.Action<global::Unity.Services.Multiplayer.ISession> MatchJoined;

		public event global::System.Action MatchJoinFailed;

		internal Matchmaker(string ticketId, global::Unity.Services.Multiplayer.SessionOptions sessionOptions, global::Unity.Services.Multiplayer.ISessionManager sessionManager, global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::Unity.Services.Matchmaker.IMatchmakerService matchmaker, global::Unity.Services.Authentication.Internal.IPlayerId playerId, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Authentication.Internal.IAccessTokenObserver accessTokenObserver, global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Multiplayer.ISession> completionSource)
		{
			TicketId = ticketId;
			m_SessionOptions = sessionOptions;
			m_SessionManager = sessionManager;
			m_ActionScheduler = actionScheduler;
			m_MatchmakerService = matchmaker;
			m_PlayerId = playerId;
			m_AccessToken = accessToken;
			m_AccessTokenObserver = accessTokenObserver;
			m_SessionCompletionSource = completionSource;
			m_PlayerId.PlayerIdChanged += OnPlayerIdChanged;
			m_AccessTokenObserver.AccessTokenChanged += OnAccessTokenChanged;
			SetState(global::Unity.Services.Multiplayer.MatchmakerState.InProgress);
			SchedulePolling(0);
			global::UnityEngine.Application.exitCancellationToken.Register(Cleanup);
		}

		private void Cleanup()
		{
			if (State != global::Unity.Services.Multiplayer.MatchmakerState.InProgress)
			{
				return;
			}
			global::Unity.Services.Multiplayer.Logger.Log("Cleanup matchmaker - cancel polling, delete ticket.");
			try
			{
				CancelPolling();
				CancelAsync();
			}
			catch (global::System.Exception)
			{
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> JoinAsync()
		{
			ValidateAuthorization();
			ValidateTicketId();
			ValidateValidAssignment();
			if (State != global::Unity.Services.Multiplayer.MatchmakerState.MatchFound && State != global::Unity.Services.Multiplayer.MatchmakerState.JoinFailed)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Invalid Matchmaker State to join.", global::Unity.Services.Multiplayer.SessionError.InvalidMatchmakerState);
			}
			switch (AssignmentType)
			{
			case global::Unity.Services.Multiplayer.MatchmakerAssignmentType.Custom:
				return await JoinCustomAssignmentAsync();
			case global::Unity.Services.Multiplayer.MatchmakerAssignmentType.IpPort:
				return await JoinIpPortAssignmentAsync();
			case global::Unity.Services.Multiplayer.MatchmakerAssignmentType.MatchId:
				return await JoinMatchIdAssignmentAsync();
			case global::Unity.Services.Multiplayer.MatchmakerAssignmentType.Multiplay:
				return await JoinMultiplayAssignmentAsync();
			default:
				SetState(global::Unity.Services.Multiplayer.MatchmakerState.JoinFailed);
				OnMatchJoinFailed();
				throw new global::Unity.Services.Multiplayer.SessionException("Invalid assignment", global::Unity.Services.Multiplayer.SessionError.InvalidMatchmakerAssignment);
			}
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> JoinCustomAssignmentAsync()
		{
			try
			{
				global::Unity.Services.Multiplayer.SessionHandler sessionHandler = await m_SessionManager.JoinByIdAsync(m_CustomAssignment.MatchId, new global::Unity.Services.Multiplayer.JoinSessionOptions
				{
					Options = m_SessionOptions.Options,
					Password = m_SessionOptions.Password,
					Type = m_SessionOptions.Type,
					PlayerProperties = m_SessionOptions.PlayerProperties
				});
				SetState(global::Unity.Services.Multiplayer.MatchmakerState.Joined);
				OnMatchJoined(sessionHandler);
				return sessionHandler;
			}
			catch (global::Unity.Services.Multiplayer.SessionException exception)
			{
				SetState(global::Unity.Services.Multiplayer.MatchmakerState.JoinFailed);
				OnMatchJoinFailed();
				m_SessionCompletionSource.TrySetException(exception);
				throw;
			}
			catch (global::System.Exception ex)
			{
				SetState(global::Unity.Services.Multiplayer.MatchmakerState.JoinFailed);
				OnMatchJoinFailed();
				global::Unity.Services.Multiplayer.SessionException exception2 = new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
				m_SessionCompletionSource.TrySetException(exception2);
				throw;
			}
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> JoinIpPortAssignmentAsync()
		{
			try
			{
				global::Unity.Services.Multiplayer.SessionHandler sessionHandler = await m_SessionManager.JoinByIdAsync(m_IpPortAssignment.MatchId, new global::Unity.Services.Multiplayer.JoinSessionOptions
				{
					Options = m_SessionOptions.Options,
					Password = m_SessionOptions.Password,
					Type = m_SessionOptions.Type,
					PlayerProperties = m_SessionOptions.PlayerProperties
				});
				SetState(global::Unity.Services.Multiplayer.MatchmakerState.Joined);
				OnMatchJoined(sessionHandler);
				return sessionHandler;
			}
			catch (global::Unity.Services.Multiplayer.SessionException exception)
			{
				SetState(global::Unity.Services.Multiplayer.MatchmakerState.JoinFailed);
				OnMatchJoinFailed();
				m_SessionCompletionSource.TrySetException(exception);
				throw;
			}
			catch (global::System.Exception ex)
			{
				SetState(global::Unity.Services.Multiplayer.MatchmakerState.JoinFailed);
				OnMatchJoinFailed();
				global::Unity.Services.Multiplayer.SessionException exception2 = new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
				m_SessionCompletionSource.TrySetException(exception2);
				throw;
			}
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> JoinMatchIdAssignmentAsync()
		{
			_ = 1;
			try
			{
				await ValidateMaxPlayersAsync(m_MatchIdAssignment.MatchId);
				global::Unity.Services.Multiplayer.SessionHandler sessionHandler = await m_SessionManager.CreateOrJoinAsync(m_MatchIdAssignment.MatchId, m_SessionOptions);
				SetState(global::Unity.Services.Multiplayer.MatchmakerState.Joined);
				OnMatchJoined(sessionHandler);
				return sessionHandler;
			}
			catch (global::Unity.Services.Multiplayer.SessionException exception)
			{
				SetState(global::Unity.Services.Multiplayer.MatchmakerState.JoinFailed);
				OnMatchJoinFailed();
				m_SessionCompletionSource.TrySetException(exception);
				throw;
			}
			catch (global::System.Exception ex)
			{
				SetState(global::Unity.Services.Multiplayer.MatchmakerState.JoinFailed);
				OnMatchJoinFailed();
				global::Unity.Services.Multiplayer.SessionException exception2 = new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
				m_SessionCompletionSource.TrySetException(exception2);
				throw;
			}
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> JoinMultiplayAssignmentAsync()
		{
			global::System.Diagnostics.Stopwatch stopwatch = new global::System.Diagnostics.Stopwatch();
			stopwatch.Start();
			while (stopwatch.Elapsed < global::System.TimeSpan.FromSeconds(10.0))
			{
				try
				{
					global::Unity.Services.Multiplayer.SessionHandler sessionHandler = await m_SessionManager.JoinByIdAsync(m_MultiplayAssignment.MatchId, new global::Unity.Services.Multiplayer.JoinSessionOptions
					{
						Options = m_SessionOptions.Options,
						Password = m_SessionOptions.Password,
						Type = m_SessionOptions.Type,
						PlayerProperties = m_SessionOptions.PlayerProperties
					});
					SetState(global::Unity.Services.Multiplayer.MatchmakerState.Joined);
					OnMatchJoined(sessionHandler);
					return sessionHandler;
				}
				catch (global::Unity.Services.Multiplayer.SessionException ex)
				{
					if (ex.Error != global::Unity.Services.Multiplayer.SessionError.SessionNotFound)
					{
						SetState(global::Unity.Services.Multiplayer.MatchmakerState.JoinFailed);
						OnMatchJoinFailed();
						throw;
					}
					await WaitForSeconds(1.0);
				}
				catch (global::System.Exception)
				{
					SetState(global::Unity.Services.Multiplayer.MatchmakerState.JoinFailed);
					OnMatchJoinFailed();
					throw;
				}
			}
			SetState(global::Unity.Services.Multiplayer.MatchmakerState.JoinFailed);
			OnMatchJoinFailed();
			throw new global::Unity.Services.Multiplayer.SessionException("Failed to join Multiplay session", global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentFailed);
		}

		private async global::System.Threading.Tasks.Task WaitForSeconds(double seconds)
		{
			global::System.Threading.Tasks.TaskCompletionSource<object> tcs = new global::System.Threading.Tasks.TaskCompletionSource<object>();
			m_ActionScheduler.ScheduleAction(delegate
			{
				tcs.SetResult(null);
			}, seconds);
			await tcs.Task;
		}

		public async global::System.Threading.Tasks.Task CancelAsync()
		{
			ValidateAuthorization();
			ValidateTicketId();
			if (State != global::Unity.Services.Multiplayer.MatchmakerState.InProgress)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Invalid Matchmaker State to cancel.", global::Unity.Services.Multiplayer.SessionError.InvalidMatchmakerState);
			}
			try
			{
				await m_MatchmakerService.DeleteTicketAsync(TicketId);
			}
			catch (global::System.Exception)
			{
			}
			TicketId = null;
			SetState(global::Unity.Services.Multiplayer.MatchmakerState.Canceled);
		}

		public void Reset()
		{
			SetState(global::Unity.Services.Multiplayer.MatchmakerState.None);
		}

		internal void SetState(global::Unity.Services.Multiplayer.MatchmakerState state)
		{
			State = state;
			global::Unity.Services.Multiplayer.MatchmakerState state2 = State;
			if (state2 == global::Unity.Services.Multiplayer.MatchmakerState.Canceled || state2 == global::Unity.Services.Multiplayer.MatchmakerState.None)
			{
				m_SessionCompletionSource?.TrySetCanceled();
			}
			this.StateChanged?.Invoke(State);
		}

		internal void SetCustomAssignment(global::Unity.Services.Matchmaker.Models.CustomAssignment assignment)
		{
			AssignmentType = global::Unity.Services.Multiplayer.MatchmakerAssignmentType.Custom;
			m_CustomAssignment = assignment;
			SetState(global::Unity.Services.Multiplayer.MatchmakerState.MatchFound);
			OnMatchFound();
		}

		internal void SetIpPortAssignment(global::Unity.Services.Matchmaker.Models.IpPortAssignment assignment)
		{
			AssignmentType = global::Unity.Services.Multiplayer.MatchmakerAssignmentType.IpPort;
			m_IpPortAssignment = assignment;
			SetState(global::Unity.Services.Multiplayer.MatchmakerState.MatchFound);
			OnMatchFound();
		}

		internal void SetMatchIdAssignment(global::Unity.Services.Matchmaker.Models.MatchIdAssignment assignment)
		{
			AssignmentType = global::Unity.Services.Multiplayer.MatchmakerAssignmentType.MatchId;
			m_MatchIdAssignment = assignment;
			SetState(global::Unity.Services.Multiplayer.MatchmakerState.MatchFound);
			OnMatchFound();
		}

		internal void SetMultiplayAssignment(global::Unity.Services.Matchmaker.Models.MultiplayAssignment assignment)
		{
			AssignmentType = global::Unity.Services.Multiplayer.MatchmakerAssignmentType.Multiplay;
			m_MultiplayAssignment = assignment;
			SetState(global::Unity.Services.Multiplayer.MatchmakerState.MatchFound);
			OnMatchFound();
		}

		private async void OnMatchFound()
		{
			this.MatchFound?.Invoke();
			global::Unity.Services.Multiplayer.ISession result = await JoinAsync();
			m_SessionCompletionSource?.TrySetResult(result);
		}

		private void OnMatchJoined(global::Unity.Services.Multiplayer.ISession session)
		{
			this.MatchJoined?.Invoke(session);
		}

		private void OnMatchJoinFailed()
		{
			this.MatchJoinFailed?.Invoke();
		}

		internal void SetMatchFailure(string message, global::Unity.Services.Multiplayer.SessionError reason)
		{
			SetState(global::Unity.Services.Multiplayer.MatchmakerState.MatchFailed);
			this.MatchFailed?.Invoke();
			throw new global::Unity.Services.Multiplayer.SessionException(message, reason);
		}

		internal void SchedulePolling(int seconds)
		{
			if (!m_PollingActionId.HasValue)
			{
				m_PollingActionId = m_ActionScheduler.ScheduleAction(RunScheduledPolling, seconds);
			}
		}

		internal void CancelPolling()
		{
			if (m_PollingActionId.HasValue)
			{
				m_ActionScheduler.CancelAction(m_PollingActionId.Value);
				m_PollingActionId = null;
			}
		}

		internal async void RunScheduledPolling()
		{
			m_PollingActionId = null;
			try
			{
				await PollTicketStatusAsync();
			}
			catch (global::System.Exception exception)
			{
				m_SessionCompletionSource.TrySetException(exception);
			}
			if (State == global::Unity.Services.Multiplayer.MatchmakerState.InProgress)
			{
				SchedulePolling(1);
			}
		}

		private async global::System.Threading.Tasks.Task PollTicketStatusAsync()
		{
			try
			{
				global::Unity.Services.Matchmaker.Models.TicketStatusResponse ticketStatusResponse = await m_MatchmakerService.GetTicketAsync(TicketId);
				if (ticketStatusResponse.Type == typeof(global::Unity.Services.Matchmaker.Models.NoneAssignment) && ticketStatusResponse.Value is global::Unity.Services.Matchmaker.Models.NoneAssignment noneAssignment)
				{
					switch (noneAssignment.Status)
					{
					case global::Unity.Services.Matchmaker.Models.NoneAssignment.StatusOptions.Found:
						SetMatchFailure("Assignment should have changed.", global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentFailed);
						break;
					case global::Unity.Services.Matchmaker.Models.NoneAssignment.StatusOptions.InProgress:
						break;
					case global::Unity.Services.Matchmaker.Models.NoneAssignment.StatusOptions.Failed:
					{
						string message2 = string.Format("Ticket {0}: {1}", noneAssignment.Status, string.IsNullOrEmpty(noneAssignment.Message) ? "Unknown failure while matchmaking." : noneAssignment.Message);
						SetMatchFailure(message2, global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentFailed);
						break;
					}
					case global::Unity.Services.Matchmaker.Models.NoneAssignment.StatusOptions.Timeout:
					{
						string message = string.Format("Ticket {0}: {1}", noneAssignment.Status, string.IsNullOrEmpty(noneAssignment.Message) ? "Matchmaking took longer than the timeout value configured for the pool." : noneAssignment.Message);
						SetMatchFailure(message, global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentTimeout);
						break;
					}
					}
					return;
				}
				if (ticketStatusResponse.Type == typeof(global::Unity.Services.Matchmaker.Models.CustomAssignment) && ticketStatusResponse.Value is global::Unity.Services.Matchmaker.Models.CustomAssignment customAssignment)
				{
					switch (customAssignment.Status)
					{
					case global::Unity.Services.Matchmaker.Models.CustomAssignment.StatusOptions.Found:
						SetCustomAssignment(customAssignment);
						break;
					case global::Unity.Services.Matchmaker.Models.CustomAssignment.StatusOptions.InProgress:
						break;
					case global::Unity.Services.Matchmaker.Models.CustomAssignment.StatusOptions.Failed:
					{
						string message4 = string.Format("Ticket {0}: {1}", customAssignment.Status, string.IsNullOrEmpty(customAssignment.Message) ? "Unknown failure while matchmaking." : customAssignment.Message);
						SetMatchFailure(message4, global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentFailed);
						break;
					}
					case global::Unity.Services.Matchmaker.Models.CustomAssignment.StatusOptions.Timeout:
					{
						string message3 = string.Format("Ticket {0}: {1}", customAssignment.Status, string.IsNullOrEmpty(customAssignment.Message) ? "Matchmaking took longer than the timeout value configured for the pool." : customAssignment.Message);
						SetMatchFailure(message3, global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentTimeout);
						break;
					}
					}
					return;
				}
				if (ticketStatusResponse.Type == typeof(global::Unity.Services.Matchmaker.Models.IpPortAssignment) && ticketStatusResponse.Value is global::Unity.Services.Matchmaker.Models.IpPortAssignment ipPortAssignment)
				{
					switch (ipPortAssignment.Status)
					{
					case global::Unity.Services.Matchmaker.Models.IpPortAssignment.StatusOptions.Found:
						SetIpPortAssignment(ipPortAssignment);
						break;
					case global::Unity.Services.Matchmaker.Models.IpPortAssignment.StatusOptions.InProgress:
						break;
					case global::Unity.Services.Matchmaker.Models.IpPortAssignment.StatusOptions.Failed:
					{
						string message6 = string.Format("Ticket {0}: {1}", ipPortAssignment.Status, string.IsNullOrEmpty(ipPortAssignment.Message) ? "Unknown failure while matchmaking." : ipPortAssignment.Message);
						SetMatchFailure(message6, global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentFailed);
						break;
					}
					case global::Unity.Services.Matchmaker.Models.IpPortAssignment.StatusOptions.Timeout:
					{
						string message5 = string.Format("Ticket {0}: {1}", ipPortAssignment.Status, string.IsNullOrEmpty(ipPortAssignment.Message) ? "Matchmaking took longer than the timeout value configured for the pool." : ipPortAssignment.Message);
						SetMatchFailure(message5, global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentTimeout);
						break;
					}
					}
					return;
				}
				if (ticketStatusResponse.Type == typeof(global::Unity.Services.Matchmaker.Models.MultiplayAssignment) && ticketStatusResponse.Value is global::Unity.Services.Matchmaker.Models.MultiplayAssignment multiplayAssignment)
				{
					switch (multiplayAssignment.Status)
					{
					case global::Unity.Services.Matchmaker.Models.MultiplayAssignment.StatusOptions.Found:
						SetMultiplayAssignment(multiplayAssignment);
						break;
					case global::Unity.Services.Matchmaker.Models.MultiplayAssignment.StatusOptions.InProgress:
						break;
					case global::Unity.Services.Matchmaker.Models.MultiplayAssignment.StatusOptions.Failed:
					{
						string message8 = string.Format("Ticket {0}: {1}", multiplayAssignment.Status, string.IsNullOrEmpty(multiplayAssignment.Message) ? "Unknown failure while matchmaking." : multiplayAssignment.Message);
						SetMatchFailure(message8, global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentFailed);
						break;
					}
					case global::Unity.Services.Matchmaker.Models.MultiplayAssignment.StatusOptions.Timeout:
					{
						string message7 = string.Format("Ticket {0}: {1}", multiplayAssignment.Status, string.IsNullOrEmpty(multiplayAssignment.Message) ? "Matchmaking took longer than the timeout value configured for the pool." : multiplayAssignment.Message);
						SetMatchFailure(message7, global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentTimeout);
						break;
					}
					}
					return;
				}
				if (ticketStatusResponse.Type == typeof(global::Unity.Services.Matchmaker.Models.MatchIdAssignment) && ticketStatusResponse.Value is global::Unity.Services.Matchmaker.Models.MatchIdAssignment matchIdAssignment)
				{
					switch (matchIdAssignment.Status)
					{
					case global::Unity.Services.Matchmaker.Models.MatchIdAssignment.StatusOptions.Found:
						SetMatchIdAssignment(matchIdAssignment);
						break;
					case global::Unity.Services.Matchmaker.Models.MatchIdAssignment.StatusOptions.InProgress:
						break;
					case global::Unity.Services.Matchmaker.Models.MatchIdAssignment.StatusOptions.Failed:
					{
						string message10 = string.Format("Ticket {0}: {1}", matchIdAssignment.Status, string.IsNullOrEmpty(matchIdAssignment.Message) ? "Unknown failure while matchmaking." : matchIdAssignment.Message);
						SetMatchFailure(message10, global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentFailed);
						break;
					}
					case global::Unity.Services.Matchmaker.Models.MatchIdAssignment.StatusOptions.Timeout:
					{
						string message9 = string.Format("Ticket {0}: {1}", matchIdAssignment.Status, string.IsNullOrEmpty(matchIdAssignment.Message) ? "Matchmaking took longer than the timeout value configured for the pool." : matchIdAssignment.Message);
						SetMatchFailure(message9, global::Unity.Services.Multiplayer.SessionError.MatchmakerAssignmentTimeout);
						break;
					}
					}
					return;
				}
				throw new global::Unity.Services.Multiplayer.SessionException("GetTicketAsync returned an invalid assignment type. This operation is not supported.", global::Unity.Services.Multiplayer.SessionError.InvalidMatchmakerAssignment);
			}
			catch (global::Unity.Services.Matchmaker.MatchmakerServiceException exception)
			{
				throw ConvertException(exception);
			}
		}

		private void ValidateAuthorization()
		{
			if (!IsAuthorized)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Player is not authorized", global::Unity.Services.Multiplayer.SessionError.NotAuthorized);
			}
		}

		private void ValidateTicketId()
		{
			if (string.IsNullOrEmpty(TicketId))
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Invalid matchmaker ticket", global::Unity.Services.Multiplayer.SessionError.InvalidMatchmakerTicket);
			}
		}

		private void ValidateValidAssignment()
		{
			if (AssignmentType == global::Unity.Services.Multiplayer.MatchmakerAssignmentType.None)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("No assignment found", global::Unity.Services.Multiplayer.SessionError.InvalidMatchmakerAssignment);
			}
		}

		private void OnPlayerIdChanged(string obj)
		{
			Reset();
		}

		private void OnAccessTokenChanged(string accessToken)
		{
			if (accessToken == null)
			{
				Reset();
			}
		}

		private async global::System.Threading.Tasks.Task ValidateMaxPlayersAsync(string matchId)
		{
			int retryCount = 1;
			global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults matchmakingResults = null;
			while (matchmakingResults == null && retryCount <= 3)
			{
				try
				{
					matchmakingResults = await m_MatchmakerService.GetMatchmakingResultsAsync(matchId);
				}
				catch (global::Unity.Services.Matchmaker.MatchmakerServiceException ex) when (retryCount < 3 && ex.Reason == global::Unity.Services.Matchmaker.MatchmakerExceptionReason.EntityNotFound)
				{
					await WaitForSeconds(1.0);
				}
				catch (global::System.Exception ex2)
				{
					global::Unity.Services.Multiplayer.Logger.LogError("Error when trying to fetch matchmaking results: " + ex2.Message);
					break;
				}
				retryCount++;
			}
			if (matchmakingResults != null)
			{
				if (m_SessionOptions.MaxPlayers < matchmakingResults.MatchProperties.MaxPlayers)
				{
					global::Unity.Services.Multiplayer.SessionException ex3 = new global::Unity.Services.Multiplayer.SessionException(string.Format("{0} in {1} ({2}) is less than the MaxPlayers configured in Matchmaker rules ({3}).", "MaxPlayers", "SessionOptions", m_SessionOptions.MaxPlayers, matchmakingResults.MatchProperties.MaxPlayers), global::Unity.Services.Multiplayer.SessionError.InvalidParameter);
					m_SessionCompletionSource?.TrySetException(ex3);
					throw ex3;
				}
				_ = m_SessionOptions.MaxPlayers;
				_ = matchmakingResults.MatchProperties.MaxPlayers;
			}
		}

		private global::Unity.Services.Multiplayer.SessionException ConvertException(global::Unity.Services.Matchmaker.MatchmakerServiceException exception)
		{
			return new global::Unity.Services.Multiplayer.SessionException(exception.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
		}
	}
}
