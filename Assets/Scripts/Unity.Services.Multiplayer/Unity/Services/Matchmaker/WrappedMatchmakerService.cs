namespace Unity.Services.Matchmaker
{
	internal class WrappedMatchmakerService : global::Unity.Services.Matchmaker.IMatchmakerSdkConfiguration, global::Unity.Services.Matchmaker.IMatchmakerService
	{
		private const string CloudEnvironmentKey = "com.unity.services.core.cloud-environment";

		internal readonly global::Unity.Services.Matchmaker.IMatchmakerServiceSdk m_MatchmakerService;

		internal readonly global::Unity.Services.Core.Configuration.Internal.ICloudProjectId m_CloudProjectId;

		private readonly global::Unity.Services.Matchmaker.Overrides.IABRemoteConfig m_abRemoteConfig;

		private readonly global::Unity.Services.Matchmaker.Overrides.IABAnalytics m_abAnalytics;

		internal WrappedMatchmakerService(global::Unity.Services.Core.Configuration.Internal.ICloudProjectId cloudProjectId, global::Unity.Services.Core.Configuration.Internal.IProjectConfiguration projectConfiguration, global::Unity.Services.Core.Device.Internal.IInstallationId installationId, global::Unity.Services.Authentication.Internal.IEnvironmentId environmentIdProvider, global::Unity.Services.Matchmaker.IMatchmakerServiceSdk matchmakerService, global::Unity.Services.Matchmaker.Overrides.IABRemoteConfig abRemoteConfig = null, global::Unity.Services.Matchmaker.Overrides.IABAnalytics abAnalytics = null)
		{
			m_CloudProjectId = cloudProjectId;
			m_MatchmakerService = matchmakerService;
			string cloudProjectId2 = m_CloudProjectId.GetCloudProjectId();
			string environmentId = environmentIdProvider?.EnvironmentId ?? "";
			if (abAnalytics == null)
			{
				if (global::Unity.Services.Core.Internal.CoreRegistry.Instance.TryGetServiceComponent<global::Unity.Services.Core.Analytics.Internal.IAnalyticsStandardEventComponent>(out var component))
				{
					m_abAnalytics = ((component != null) ? new global::Unity.Services.Matchmaker.Overrides.ABAnalytics(cloudProjectId2, environmentId, component) : null);
				}
			}
			else
			{
				m_abAnalytics = abAnalytics;
			}
			if (m_abAnalytics != null)
			{
				if (abRemoteConfig == null)
				{
					string installationId2 = installationId?.GetOrCreateIdentifier();
					string cloudEnvironment = projectConfiguration?.GetString("com.unity.services.core.cloud-environment");
					m_abRemoteConfig = new global::Unity.Services.Matchmaker.Overrides.ABRemoteConfig(new global::Unity.Services.Matchmaker.Http.HttpClient(), installationId2, cloudEnvironment, cloudProjectId2, environmentId);
				}
				else
				{
					m_abRemoteConfig = abRemoteConfig;
				}
			}
		}

		public void SetBasePath(string basePath)
		{
			m_MatchmakerService.Configuration.BasePath = basePath;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Models.CreateTicketResponse> CreateTicketAsync(global::System.Collections.Generic.List<global::Unity.Services.Matchmaker.Models.Player> players, global::Unity.Services.Matchmaker.CreateTicketOptions options)
		{
			EnsureSignedIn();
			if (players == null || players.Count < 1)
			{
				throw new global::System.ArgumentNullException("players", "Cannot create a matchmaking ticket without at least 1 player to add to the queue!");
			}
			string queueName = options?.QueueName;
			global::System.Collections.Generic.Dictionary<string, object> attributes = options?.Attributes;
			if (m_abRemoteConfig != null)
			{
				await m_abRemoteConfig.RefreshGameOverridesAsync();
			}
			global::Unity.Services.Matchmaker.Models.CreateTicketRequest createTicketRequestParameter = new global::Unity.Services.Matchmaker.Models.CreateTicketRequest(players, queueName, attributes, m_abRemoteConfig?.Overrides);
			global::Unity.Services.Matchmaker.Tickets.CreateTicketRequest request = new global::Unity.Services.Matchmaker.Tickets.CreateTicketRequest(null, createTicketRequestParameter);
			global::Unity.Services.Matchmaker.Models.CreateTicketResponse result = (await TryCatchRequest((global::System.Func<global::Unity.Services.Matchmaker.Tickets.CreateTicketRequest, global::Unity.Services.Matchmaker.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.CreateTicketResponse>>>)m_MatchmakerService.TicketsApi.CreateTicketAsync, request)).Result;
			if (result.AbTestingResult != null && result.AbTestingResult.IsAbTesting)
			{
				m_abAnalytics?.SubmitUserAssignmentConfirmedEvent(result.AbTestingResult.VariantId, m_abRemoteConfig?.AssignmentId);
			}
			return result;
		}

		public async global::System.Threading.Tasks.Task DeleteTicketAsync(string ticketId)
		{
			EnsureSignedIn();
			if (string.IsNullOrWhiteSpace(ticketId))
			{
				throw new global::System.ArgumentNullException("ticketId", "Argument should be non-null, non-empty & not only whitespaces.");
			}
			global::Unity.Services.Matchmaker.Tickets.DeleteTicketRequest request = new global::Unity.Services.Matchmaker.Tickets.DeleteTicketRequest(ticketId);
			await TryCatchRequest(m_MatchmakerService.TicketsApi.DeleteTicketAsync, request);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Models.TicketStatusResponse> GetTicketAsync(string ticketId)
		{
			EnsureSignedIn();
			if (string.IsNullOrWhiteSpace(ticketId))
			{
				throw new global::System.ArgumentNullException("ticketId", "Argument should be non-null, non-empty & not only whitespaces.");
			}
			global::Unity.Services.Matchmaker.Tickets.GetTicketStatusRequest request = new global::Unity.Services.Matchmaker.Tickets.GetTicketStatusRequest(ticketId);
			return (await TryCatchRequest((global::System.Func<global::Unity.Services.Matchmaker.Tickets.GetTicketStatusRequest, global::Unity.Services.Matchmaker.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.TicketStatusResponse>>>)m_MatchmakerService.TicketsApi.GetTicketStatusAsync, request)).Result;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Models.BackfillTicket> ApproveBackfillTicketAsync(string backfillTicketId)
		{
			global::Unity.Services.Matchmaker.Backfill.ApproveBackfillTicketRequest request = new global::Unity.Services.Matchmaker.Backfill.ApproveBackfillTicketRequest(backfillTicketId);
			return (await TryCatchRequest((global::System.Func<global::Unity.Services.Matchmaker.Backfill.ApproveBackfillTicketRequest, string, global::Unity.Services.Matchmaker.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.LegacyBackfillTicket>>>)m_MatchmakerService.BackfillApi.ApproveBackfillTicketAsync, request)).Result.GetCompatibilityModel();
		}

		public async global::System.Threading.Tasks.Task<string> CreateBackfillTicketAsync(global::Unity.Services.Matchmaker.CreateBackfillTicketOptions options)
		{
			if (options == null)
			{
				throw new global::System.ArgumentNullException("options must not be null.");
			}
			if (string.IsNullOrWhiteSpace(options.Connection) && options.ConnectionDetails == null)
			{
				throw new global::Unity.Services.Multiplayer.SessionException("Either Connection or ConnectionDetails must be provided to create a backfill ticket.", global::Unity.Services.Multiplayer.SessionError.InvalidBackfillTicketOptions);
			}
			if (!string.IsNullOrWhiteSpace(options.Connection) && options.ConnectionDetails != null)
			{
				global::Unity.Services.Multiplayer.Logger.LogWarning(string.Format("When using both {0} and {1}, {2} will take precedence.", "Connection", options.ConnectionDetails, "ConnectionDetails"));
			}
			global::Unity.Services.Matchmaker.Backfill.CreateBackfillTicketRequest request = new global::Unity.Services.Matchmaker.Backfill.CreateBackfillTicketRequest(options.GetLegacyModel());
			return (await TryCatchRequest((global::System.Func<global::Unity.Services.Matchmaker.Backfill.CreateBackfillTicketRequest, string, global::Unity.Services.Matchmaker.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.CreateBackfillTicketResponse>>>)m_MatchmakerService.BackfillApi.CreateBackfillTicketAsync, request)).Result.Id;
		}

		public async global::System.Threading.Tasks.Task DeleteBackfillTicketAsync(string backfillTicketId)
		{
			global::Unity.Services.Matchmaker.Backfill.DeleteBackfillTicketRequest request = new global::Unity.Services.Matchmaker.Backfill.DeleteBackfillTicketRequest(backfillTicketId);
			await TryCatchRequest(m_MatchmakerService.BackfillApi.DeleteBackfillTicketAsync, request);
		}

		public async global::System.Threading.Tasks.Task UpdateBackfillTicketAsync(string backfillTicketId, global::Unity.Services.Matchmaker.Models.BackfillTicket ticket)
		{
			global::Unity.Services.Matchmaker.Backfill.UpdateBackfillTicketRequest request = new global::Unity.Services.Matchmaker.Backfill.UpdateBackfillTicketRequest(backfillTicketId, ticket.GetLegacyModel());
			await TryCatchRequest(m_MatchmakerService.BackfillApi.UpdateBackfillTicketAsync, request);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults> GetMatchmakingResultsAsync(string matchId)
		{
			EnsureSignedIn();
			if (string.IsNullOrWhiteSpace(matchId))
			{
				throw new global::System.ArgumentNullException("matchId", "Argument should be non-null, non-empty & not only whitespaces.");
			}
			global::Unity.Services.Matchmaker.Matches.GetMatchmakingResultsRequest request = new global::Unity.Services.Matchmaker.Matches.GetMatchmakingResultsRequest(matchId, m_CloudProjectId.GetCloudProjectId());
			return (await TryCatchRequest((global::System.Func<global::Unity.Services.Matchmaker.Matches.GetMatchmakingResultsRequest, global::Unity.Services.Matchmaker.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<global::Unity.Services.Matchmaker.Models.StoredMatchmakingResults>>>)m_MatchmakerService.MatchesApi.GetMatchmakingResultsAsync, request)).Result;
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response> TryCatchRequest<TRequest>(global::System.Func<TRequest, global::Unity.Services.Matchmaker.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response>> func, TRequest request)
		{
			global::Unity.Services.Matchmaker.Response response = null;
			try
			{
				response = await func(request, m_MatchmakerService.Configuration);
			}
			catch (global::Unity.Services.Matchmaker.Http.HttpException ex)
			{
				int num = (int)ex.Response.StatusCode;
				global::Unity.Services.Matchmaker.MatchmakerExceptionReason reason = global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unknown;
				if (ex.Response.IsNetworkError)
				{
					reason = global::Unity.Services.Matchmaker.MatchmakerExceptionReason.NetworkError;
				}
				else if (ex.Response.IsHttpError && num < 1000)
				{
					num += 21000;
					if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Matchmaker.MatchmakerExceptionReason), num))
					{
						reason = (global::Unity.Services.Matchmaker.MatchmakerExceptionReason)num;
					}
				}
				ResolveErrorWrapping(reason, ex);
			}
			catch (global::System.Exception exception)
			{
				ResolveErrorWrapping(global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unknown, exception);
			}
			return response;
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response> TryCatchRequest<TRequest>(global::System.Func<TRequest, string, global::Unity.Services.Matchmaker.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response>> func, TRequest request)
		{
			if (string.IsNullOrEmpty(m_MatchmakerService.ServerAccessToken?.AccessToken))
			{
				throw new global::Unity.Services.Matchmaker.MatchmakerServiceException(global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unauthorized, "Backfill operations require a server access token.");
			}
			global::Unity.Services.Matchmaker.Response response = null;
			try
			{
				response = await func(request, m_MatchmakerService.ServerAccessToken.AccessToken, m_MatchmakerService.Configuration);
			}
			catch (global::Unity.Services.Matchmaker.Http.HttpException ex)
			{
				int num = (int)ex.Response.StatusCode;
				global::Unity.Services.Matchmaker.MatchmakerExceptionReason reason = global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unknown;
				if (ex.Response.IsNetworkError)
				{
					reason = global::Unity.Services.Matchmaker.MatchmakerExceptionReason.NetworkError;
				}
				else if (ex.Response.IsHttpError && num < 1000)
				{
					num += 21000;
					if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Matchmaker.MatchmakerExceptionReason), num))
					{
						reason = (global::Unity.Services.Matchmaker.MatchmakerExceptionReason)num;
					}
				}
				ResolveErrorWrapping(reason, ex);
			}
			catch (global::System.Exception exception)
			{
				ResolveErrorWrapping(global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unknown, exception);
			}
			return response;
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<TReturn>> TryCatchRequest<TRequest, TReturn>(global::System.Func<TRequest, global::Unity.Services.Matchmaker.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<TReturn>>> func, TRequest request)
		{
			global::Unity.Services.Matchmaker.Response<TReturn> response = null;
			try
			{
				response = await func(request, m_MatchmakerService.Configuration);
			}
			catch (global::Unity.Services.Matchmaker.Http.HttpException ex)
			{
				int num = (int)ex.Response.StatusCode;
				global::Unity.Services.Matchmaker.MatchmakerExceptionReason reason = global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unknown;
				if (ex.Response.IsNetworkError)
				{
					reason = global::Unity.Services.Matchmaker.MatchmakerExceptionReason.NetworkError;
				}
				else if (ex.Response.IsHttpError && num < 1000)
				{
					num += 21000;
					if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Matchmaker.MatchmakerExceptionReason), num))
					{
						reason = (global::Unity.Services.Matchmaker.MatchmakerExceptionReason)num;
					}
				}
				ResolveErrorWrapping(reason, ex);
			}
			catch (global::System.Exception exception)
			{
				ResolveErrorWrapping(global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unknown, exception);
			}
			return response;
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<TReturn>> TryCatchRequest<TRequest, TReturn>(global::System.Func<TRequest, string, global::Unity.Services.Matchmaker.Configuration, global::System.Threading.Tasks.Task<global::Unity.Services.Matchmaker.Response<TReturn>>> func, TRequest request)
		{
			if (string.IsNullOrEmpty(m_MatchmakerService.ServerAccessToken?.AccessToken))
			{
				throw new global::Unity.Services.Matchmaker.MatchmakerServiceException(global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unauthorized, "Backfill operations require a server access token.");
			}
			global::Unity.Services.Matchmaker.Response<TReturn> response = null;
			try
			{
				response = await func(request, m_MatchmakerService.ServerAccessToken.AccessToken, m_MatchmakerService.Configuration);
			}
			catch (global::Unity.Services.Matchmaker.Http.HttpException ex)
			{
				int num = (int)ex.Response.StatusCode;
				global::Unity.Services.Matchmaker.MatchmakerExceptionReason reason = global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unknown;
				if (ex.Response.IsNetworkError)
				{
					reason = global::Unity.Services.Matchmaker.MatchmakerExceptionReason.NetworkError;
				}
				else if (ex.Response.IsHttpError && num < 1000)
				{
					num += 21000;
					if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Matchmaker.MatchmakerExceptionReason), num))
					{
						reason = (global::Unity.Services.Matchmaker.MatchmakerExceptionReason)num;
					}
				}
				ResolveErrorWrapping(reason, ex);
			}
			catch (global::System.Exception exception)
			{
				ResolveErrorWrapping(global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unknown, exception);
			}
			return response;
		}

		private void ResolveErrorWrapping(global::Unity.Services.Matchmaker.MatchmakerExceptionReason reason, global::System.Exception exception = null)
		{
			if (reason == global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unknown)
			{
				global::Unity.Services.Multiplayer.Logger.LogError($"{(global::System.Enum.GetName(typeof(global::Unity.Services.Matchmaker.MatchmakerExceptionReason), reason))} ({(int)reason}). Message: Something went wrong.");
				throw new global::Unity.Services.Matchmaker.MatchmakerServiceException(reason, "Something went wrong.", exception);
			}
			if (exception is global::Unity.Services.Matchmaker.Http.HttpException<global::Unity.Services.Matchmaker.Models.ProblemDetails> { ActualError: var actualError } ex)
			{
				if (actualError != null)
				{
					global::Unity.Services.Matchmaker.Http.JsonObject jsonObject = actualError.Errors.GetAs<global::Unity.Services.Matchmaker.Http.JsonObject>();
					string text = ((jsonObject == null || jsonObject.obj == null) ? actualError.Detail : (global::System.Environment.NewLine + global::Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject)));
					global::Unity.Services.Multiplayer.Logger.LogError($"{(global::System.Enum.GetName(typeof(global::Unity.Services.Matchmaker.MatchmakerExceptionReason), reason))} ({(int)reason}) " + global::System.Environment.NewLine + " Title: " + ex.ActualError.Title + " " + global::System.Environment.NewLine + " Errors: " + text + global::System.Environment.NewLine);
				}
				throw new global::Unity.Services.Matchmaker.MatchmakerServiceException(reason, ex.Response.ErrorMessage, ex);
			}
			global::Unity.Services.Multiplayer.Logger.LogError($"{(global::System.Enum.GetName(typeof(global::Unity.Services.Matchmaker.MatchmakerExceptionReason), reason))} ({(int)reason}). Message: {exception.Message}");
			throw new global::Unity.Services.Matchmaker.MatchmakerServiceException(reason, exception.Message, exception);
		}

		private void EnsureSignedIn()
		{
			if (m_MatchmakerService.AccessToken.AccessToken == null)
			{
				throw new global::Unity.Services.Matchmaker.MatchmakerServiceException(global::Unity.Services.Matchmaker.MatchmakerExceptionReason.Unauthorized, "You are not signed in to the Authentication Service. Please sign in.");
			}
		}
	}
}
