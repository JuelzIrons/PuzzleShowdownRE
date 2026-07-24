namespace Unity.Services.Multiplayer
{
	internal class MatchmakerManager : global::Unity.Services.Multiplayer.IMatchmakerManager
	{
		private const string k_UnknownRegion = "unknown-region";

		private readonly global::Unity.Services.Multiplayer.ISessionManager m_SessionManager;

		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private readonly global::Unity.Services.Matchmaker.IMatchmakerService m_MatchmakerService;

		private readonly global::Unity.Services.Authentication.Internal.IPlayerId m_PlayerId;

		private readonly global::Unity.Services.Authentication.Internal.IAccessToken m_AccessToken;

		private readonly global::Unity.Services.Authentication.Internal.IAccessTokenObserver m_AccessTokenObserver;

		private readonly global::Unity.Services.Multiplayer.QosCalculator m_QosCalculator;

		public MatchmakerManager(global::Unity.Services.Multiplayer.ISessionManager sessionManager, global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::Unity.Services.Qos.IQosService qosService, global::Unity.Services.Matchmaker.IMatchmakerService matchmakerService, global::Unity.Services.Authentication.Internal.IPlayerId playerId, global::Unity.Services.Authentication.Internal.IAccessToken accessToken, global::Unity.Services.Authentication.Internal.IAccessTokenObserver accessTokenObserver)
		{
			m_SessionManager = sessionManager;
			m_ActionScheduler = actionScheduler;
			m_MatchmakerService = matchmakerService;
			m_PlayerId = playerId;
			m_AccessToken = accessToken;
			m_AccessTokenObserver = accessTokenObserver;
			m_QosCalculator = new global::Unity.Services.Multiplayer.QosCalculator(qosService);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Multiplayer.ISession> StartAsync(global::Unity.Services.Multiplayer.MatchmakerOptions matchOptions, global::Unity.Services.Multiplayer.SessionOptions sessionOptions, global::System.Threading.CancellationToken token = default(global::System.Threading.CancellationToken))
		{
			global::Unity.Services.Matchmaker.Models.Player item = await PreparePlayerAsync(m_PlayerId.PlayerId, global::System.Linq.Enumerable.ToDictionary(matchOptions.PlayerProperties?, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.PlayerProperty> x) => x.Key, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.PlayerProperty> x) => x.Value.Value), matchOptions.QueueName);
			try
			{
				global::Unity.Services.Matchmaker.CreateTicketOptions options = new global::Unity.Services.Matchmaker.CreateTicketOptions(matchOptions.QueueName, matchOptions.TicketAttributes);
				global::Unity.Services.Matchmaker.Models.CreateTicketResponse createTicketResponse = await m_MatchmakerService.CreateTicketAsync(new global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Player> { item }, options);
				global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Multiplayer.ISession> completionSource = new global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Multiplayer.ISession>();
				global::Unity.Services.Multiplayer.Matchmaker matchmaker = new global::Unity.Services.Multiplayer.Matchmaker(createTicketResponse.Id, sessionOptions, m_SessionManager, m_ActionScheduler, m_MatchmakerService, m_PlayerId, m_AccessToken, m_AccessTokenObserver, completionSource);
				token.Register(CancelMatchmaking);
				return await completionSource.Task;
				async void CancelMatchmaking()
				{
					matchmaker.CancelPolling();
					try
					{
						await matchmaker.CancelAsync();
					}
					catch (global::Unity.Services.Multiplayer.SessionException ex3)
					{
						_ = ex3.Error;
						_ = 15;
					}
					catch (global::System.Exception ex4)
					{
						throw new global::Unity.Services.Multiplayer.SessionException(ex4.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
					}
					completionSource.TrySetCanceled();
				}
			}
			catch (global::System.Threading.Tasks.TaskCanceledException ex)
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex.Message, global::Unity.Services.Multiplayer.SessionError.MatchmakerCancelled);
			}
			catch (global::System.Exception ex2) when (!(ex2 is global::Unity.Services.Multiplayer.SessionException))
			{
				throw new global::Unity.Services.Multiplayer.SessionException(ex2.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
			}
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Models.Player> PreparePlayerAsync(string playerId, object customData, string queueName)
		{
			return new global::Unity.Services.Matchmaker.Models.Player(playerId, customData, await m_QosCalculator.GetQosResultsAsync(queueName));
		}
	}
}
