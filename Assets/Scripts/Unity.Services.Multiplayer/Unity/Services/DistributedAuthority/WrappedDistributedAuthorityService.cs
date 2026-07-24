namespace Unity.Services.DistributedAuthority
{
	internal class WrappedDistributedAuthorityService : global::Unity.Services.DistributedAuthority.SDK.IDistributedAuthoritySDKConfiguration, global::Unity.Services.DistributedAuthority.IDistributedAuthorityService
	{
		private const string QosRelayServiceName = "relay";

		private readonly global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicyProvider m_RetryPolicyProvider;

		private readonly global::Unity.Services.DistributedAuthority.Internal.IClock m_Clock;

		private readonly global::Unity.Services.DistributedAuthority.IInternalDaLobbyService m_LobbyService;

		private readonly global::Unity.Services.Relay.IRelayService m_RelayService;

		private readonly global::Unity.Services.Qos.Internal.IQosResults m_QosResults;

		private readonly global::Unity.Services.DistributedAuthority.Apis.DistributedAuthority.IDistributedAuthorityApiClient m_ApiClient;

		private global::Unity.Services.DistributedAuthority.Configuration Configuration { get; }

		internal WrappedDistributedAuthorityService(global::Unity.Services.DistributedAuthority.Apis.DistributedAuthority.IDistributedAuthorityApiClient apiClient, global::Unity.Services.DistributedAuthority.ErrorMitigation.IRetryPolicyProvider retryPolicyProvider, global::Unity.Services.DistributedAuthority.Internal.IClock clock, global::Unity.Services.DistributedAuthority.Configuration configuration, global::Unity.Services.DistributedAuthority.IInternalDaLobbyService internalLobbyService, global::Unity.Services.Relay.IRelayService relayService, global::Unity.Services.Qos.Internal.IQosResults qosResults)
		{
			m_ApiClient = apiClient;
			m_RetryPolicyProvider = retryPolicyProvider;
			m_Clock = clock;
			Configuration = configuration;
			m_LobbyService = internalLobbyService;
			m_RelayService = relayService;
			m_QosResults = qosResults;
		}

		public void SetBasePath(string basePath)
		{
			Configuration.BasePath = basePath;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.DistributedAuthority.Models.Session> CreateSessionForLobbyIdAsync(string lobbyId, string region = null)
		{
			if (string.IsNullOrEmpty(lobbyId))
			{
				throw new global::System.ArgumentException("Lobby ID cannot be null or empty.", "lobbyId");
			}
			if (string.IsNullOrEmpty(region) && m_QosResults != null && m_RelayService != null)
			{
				try
				{
					global::System.Collections.Generic.List<string> regions = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(await m_RelayService.ListRegionsAsync(), (global::Unity.Services.Relay.Models.Region r) => r.Id));
					global::System.Collections.Generic.IList<global::Unity.Services.Qos.Internal.QosResult> list = await m_QosResults.GetSortedQosResultsAsync("relay", regions);
					if (global::System.Linq.Enumerable.Any(list))
					{
						region = list[0].Region;
					}
					else
					{
						global::Unity.Services.Multiplayer.Logger.LogWarning("No Qos region selected. Will use default.");
					}
				}
				catch (global::System.Exception ex)
				{
					global::Unity.Services.Multiplayer.Logger.LogWarning("Could not do Qos region selection. Will use default." + global::System.Environment.NewLine + "QoS failed due to [" + ex.GetType().Name + "]. Reason: " + ex.Message);
				}
			}
			global::Unity.Services.DistributedAuthority.DistributedAuthority.CreateSessionRequest request = new global::Unity.Services.DistributedAuthority.DistributedAuthority.CreateSessionRequest(new global::Unity.Services.DistributedAuthority.Models.Session(lobbyId, region));
			try
			{
				return (await m_ApiClient.CreateSessionAsync(request)).Result;
			}
			catch (global::Unity.Services.DistributedAuthority.Http.HttpException<global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody> ex2)
			{
				throw new global::Unity.Services.DistributedAuthority.Exceptions.DistributedAuthorityServiceException(ex2.ActualError.GetExceptionReason(), ex2.ActualError.GetExceptionMessage(), ex2);
			}
			catch (global::Unity.Services.DistributedAuthority.Http.HttpException ex3)
			{
				if (ex3.Response.IsHttpError)
				{
					throw new global::Unity.Services.DistributedAuthority.Exceptions.DistributedAuthorityServiceException(ex3.Response.GetExceptionReason(), ex3.Response.ErrorMessage, ex3);
				}
				if (ex3.Response.IsNetworkError)
				{
					throw new global::Unity.Services.DistributedAuthority.Exceptions.DistributedAuthorityServiceException(global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason.NetworkError, ex3.Response.ErrorMessage);
				}
				throw new global::Unity.Services.Core.RequestFailedException(45999, "Something went wrong.", ex3);
			}
		}

		public async global::System.Threading.Tasks.Task<string> JoinSessionForLobbyIdAsync(string lobbyId, int timeoutSeconds = 120)
		{
			if (string.IsNullOrEmpty(lobbyId))
			{
				throw new global::System.ArgumentException("Lobby Id cannot be null or empty.", "lobbyId");
			}
			if (timeoutSeconds <= 0)
			{
				throw new global::System.ArgumentException("Timeout must be greater than 0.", "timeoutSeconds");
			}
			try
			{
				string text = await PollLobbyForJoinCodeAsync(lobbyId, timeoutSeconds);
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				throw new global::Unity.Services.DistributedAuthority.Exceptions.DistributedAuthorityServiceException(global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason.Unknown, "No join code returned from lobby.");
			}
			catch (global::Unity.Services.DistributedAuthority.Http.HttpException<global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody> ex)
			{
				throw new global::Unity.Services.DistributedAuthority.Exceptions.DistributedAuthorityServiceException(ex.ActualError.GetExceptionReason(), ex.ActualError.GetExceptionMessage(), ex);
			}
			catch (global::Unity.Services.DistributedAuthority.Http.HttpException ex2)
			{
				if (ex2.Response.IsHttpError)
				{
					throw new global::Unity.Services.DistributedAuthority.Exceptions.DistributedAuthorityServiceException(ex2.Response.GetExceptionReason(), ex2.Response.ErrorMessage, ex2);
				}
				if (ex2.Response.IsNetworkError)
				{
					throw new global::Unity.Services.DistributedAuthority.Exceptions.DistributedAuthorityServiceException(global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason.NetworkError, ex2.Response.ErrorMessage);
				}
				throw new global::Unity.Services.Core.RequestFailedException(45999, "Something went wrong.", ex2);
			}
		}

		private async global::System.Threading.Tasks.Task<string> PollLobbyForJoinCodeAsync(string lobbyId, int timeoutSeconds)
		{
			if (TryGetJoinCode(await m_LobbyService.JoinLobbyByIdAsync(lobbyId), out var joinCode))
			{
				return joinCode;
			}
			global::System.DateTimeOffset start = m_Clock.UtcNow();
			global::System.TimeSpan timeout = global::System.TimeSpan.FromSeconds(timeoutSeconds);
			TryGetJoinCode(await m_RetryPolicyProvider.ForOperation(async () => await m_LobbyService.GetLobbyAsync(lobbyId)).WithRetryCondition((global::Unity.Services.Lobbies.Models.Lobby l) => global::System.Threading.Tasks.Task.FromResult(ShouldRetry(l, start, timeout))).WithJitterMagnitude(0.5f)
				.WithMaxDelayTime(2f)
				.HandleException((global::Unity.Services.Lobbies.LobbyServiceException ex) => ex.Reason == global::Unity.Services.Lobbies.LobbyExceptionReason.RateLimited)
				.RunAsync(), out joinCode);
			return joinCode;
		}

		private bool ShouldRetry(global::Unity.Services.Lobbies.Models.Lobby lobby, global::System.DateTimeOffset start, global::System.TimeSpan timeout)
		{
			if (TryGetJoinCode(lobby, out var joinCode))
			{
				if (!string.IsNullOrEmpty(joinCode))
				{
					return false;
				}
				return m_Clock.UtcNow().Subtract(start) < timeout;
			}
			return true;
		}

		private bool TryGetJoinCode(global::Unity.Services.Lobbies.Models.Lobby lobby, out string joinCode)
		{
			global::Unity.Services.Lobbies.Models.DataObject value = null;
			joinCode = string.Empty;
			lobby.Data?.TryGetValue("_session_network", out value);
			if (string.IsNullOrEmpty(value?.Value))
			{
				return false;
			}
			global::Unity.Services.Multiplayer.NetworkMetadata networkMetadata = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Unity.Services.Multiplayer.NetworkMetadata>(value.Value);
			if (string.IsNullOrEmpty(networkMetadata.RelayJoinCode))
			{
				return false;
			}
			joinCode = networkMetadata.RelayJoinCode;
			return true;
		}
	}
}
