namespace Unity.Services.Authentication
{
	internal class AuthenticationServiceInternal : global::Unity.Services.Authentication.IAuthenticationService
	{
		private const string k_IdProviderNameRegex = "^oidc-[a-z0-9-_\\.]{1,15}$";

		private const string k_ProfileRegex = "^[a-zA-Z0-9_-]{1,30}$";

		private global::Unity.Services.Authentication.PlayerInfo m_PlayerInfo;

		private readonly global::Unity.Services.Authentication.IProfile m_Profile;

		private readonly global::Unity.Services.Authentication.IJwtDecoder m_JwtDecoder;

		private readonly global::Unity.Services.Authentication.IAuthenticationCache m_Cache;

		private readonly global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_Scheduler;

		private readonly global::Unity.Services.Authentication.IAuthenticationMetrics m_Metrics;

		private const string k_SteamIdentityRegex = "^[a-zA-Z0-9]{5,30}$";

		private global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification> m_Notifications;

		internal string CodeLinkSessionId { get; set; }

		internal string CodeVerifier { get; set; }

		public bool IsSignedIn
		{
			get
			{
				if (State != global::Unity.Services.Authentication.AuthenticationState.Authorized && State != global::Unity.Services.Authentication.AuthenticationState.Refreshing)
				{
					return State == global::Unity.Services.Authentication.AuthenticationState.Expired;
				}
				return true;
			}
		}

		public bool IsAuthorized
		{
			get
			{
				if (State != global::Unity.Services.Authentication.AuthenticationState.Authorized)
				{
					return State == global::Unity.Services.Authentication.AuthenticationState.Refreshing;
				}
				return true;
			}
		}

		public bool IsExpired => State == global::Unity.Services.Authentication.AuthenticationState.Expired;

		public bool SessionTokenExists => !string.IsNullOrEmpty(SessionTokenComponent.SessionToken);

		public string SessionToken => SessionTokenComponent.SessionToken;

		public string Profile => m_Profile.Current;

		public string AccessToken => AccessTokenComponent.AccessToken;

		public string PlayerId => PlayerIdComponent.PlayerId;

		public global::Unity.Services.Authentication.PlayerInfo PlayerInfo
		{
			get
			{
				return m_PlayerInfo;
			}
			internal set
			{
				if (m_PlayerInfo != value)
				{
					m_PlayerInfo = value;
					this.PlayerInfoChanged?.Invoke(value);
				}
			}
		}

		[global::JetBrains.Annotations.CanBeNull]
		public string LastNotificationDate { get; private set; }

		internal long? ExpirationActionId { get; set; }

		internal long? RefreshActionId { get; set; }

		internal global::Unity.Services.Authentication.AccessTokenComponent AccessTokenComponent { get; }

		internal global::Unity.Services.Authentication.EnvironmentIdComponent EnvironmentIdComponent { get; }

		internal global::Unity.Services.Authentication.PlayerIdComponent PlayerIdComponent { get; }

		internal global::Unity.Services.Authentication.PlayerNameComponent PlayerNameComponent { get; }

		internal global::Unity.Services.Authentication.SessionTokenComponent SessionTokenComponent { get; }

		internal global::Unity.Services.Core.Environments.Internal.IEnvironments EnvironmentComponent { get; }

		internal global::Unity.Services.Authentication.AuthenticationState State { get; set; }

		internal global::Unity.Services.Authentication.IAuthenticationSettings Settings { get; }

		internal global::Unity.Services.Authentication.IAuthenticationNetworkClient NetworkClient { get; set; }

		internal global::Unity.Services.Authentication.Generated.IPlayerNamesApi PlayerNamesApi { get; set; }

		internal global::Unity.Services.Authentication.IAuthenticationExceptionHandler ExceptionHandler { get; set; }

		public global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification> Notifications => m_Notifications;

		public string PlayerName => PlayerNameComponent.PlayerName;

		public event global::System.Action<global::Unity.Services.Core.RequestFailedException> SignInFailed;

		public event global::System.Action SignedIn;

		public event global::System.Action SignedOut;

		public event global::System.Action Expired;

		public event global::System.Action<global::Unity.Services.Authentication.SignInCodeInfo> SignInCodeReceived;

		public event global::System.Action SignInCodeExpired;

		public event global::System.Action<global::Unity.Services.Core.RequestFailedException> UpdatePasswordFailed;

		public event global::System.Action<global::Unity.Services.Authentication.PlayerInfo> PlayerInfoChanged;

		public event global::System.Action<string> PlayerNameChanged
		{
			add
			{
				PlayerNameComponent.PlayerNameChanged += value;
			}
			remove
			{
				PlayerNameComponent.PlayerNameChanged -= value;
			}
		}

		public event global::System.Action<string> PlayerIdChanged
		{
			add
			{
				PlayerIdComponent.PlayerIdChanged += value;
			}
			remove
			{
				PlayerIdComponent.PlayerIdChanged -= value;
			}
		}

		internal event global::System.Action<global::Unity.Services.Authentication.AuthenticationState, global::Unity.Services.Authentication.AuthenticationState> StateChanged;

		public global::System.Threading.Tasks.Task SignInWithOpenIdConnectAsync(string idProviderName, string idToken, global::Unity.Services.Authentication.SignInOptions options = null)
		{
			if (!ValidateOpenIdConnectIdProviderName(idProviderName))
			{
				throw ExceptionHandler.BuildInvalidIdProviderNameException();
			}
			return SignInWithExternalTokenAsync(idProviderName, new global::Unity.Services.Authentication.SignInWithExternalTokenRequest
			{
				IdProvider = idProviderName,
				Token = idToken,
				SignInOnly = (options != null && !options.CreateAccount)
			});
		}

		public global::System.Threading.Tasks.Task LinkWithOpenIdConnectAsync(string idProviderName, string idToken, global::Unity.Services.Authentication.LinkOptions options = null)
		{
			if (!ValidateOpenIdConnectIdProviderName(idProviderName))
			{
				throw ExceptionHandler.BuildInvalidIdProviderNameException();
			}
			return LinkWithExternalTokenAsync(idProviderName, new global::Unity.Services.Authentication.LinkWithExternalTokenRequest
			{
				IdProvider = idProviderName,
				Token = idToken,
				ForceLink = (options?.ForceLink ?? false)
			});
		}

		public global::System.Threading.Tasks.Task UnlinkOpenIdConnectAsync(string idProviderName)
		{
			if (!ValidateOpenIdConnectIdProviderName(idProviderName))
			{
				throw ExceptionHandler.BuildInvalidIdProviderNameException();
			}
			return UnlinkExternalTokenAsync(idProviderName);
		}

		public void ProcessAuthenticationTokens(string accessToken, string sessionToken = null)
		{
			if (State == global::Unity.Services.Authentication.AuthenticationState.SignedOut || State == global::Unity.Services.Authentication.AuthenticationState.Expired)
			{
				try
				{
					ValidateAccessToken(accessToken);
				}
				catch (global::Unity.Services.Core.RequestFailedException)
				{
					throw;
				}
				catch (global::System.Exception ex2)
				{
					throw global::Unity.Services.Authentication.AuthenticationException.Create(0, "Failed validating access token: " + ex2.Message);
				}
				CompleteSignIn(accessToken, sessionToken);
				return;
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		private void ValidateAccessToken(string accessToken)
		{
			if (string.IsNullOrEmpty(accessToken))
			{
				throw global::Unity.Services.Authentication.AuthenticationException.Create(51, "Empty or null access token.");
			}
			string text = global::System.Linq.Enumerable.FirstOrDefault((m_JwtDecoder.Decode<global::Unity.Services.Authentication.AccessToken>(accessToken) ?? throw global::Unity.Services.Authentication.AuthenticationException.Create(51, "Failed to decode and verify access token.")).Audience, (string s) => s.StartsWith("envName:"))?.Replace("envName:", "");
			if (EnvironmentComponent.Current != text)
			{
				throw global::Unity.Services.Authentication.AuthenticationException.Create(global::Unity.Services.Authentication.AuthenticationErrorCodes.EnvironmentMismatch, "The configured environment(" + EnvironmentComponent.Current + ") and the access token one(" + (text ?? "null") + ") don't match.");
			}
		}

		private bool ValidateOpenIdConnectIdProviderName(string idProviderName)
		{
			if (!string.IsNullOrEmpty(idProviderName))
			{
				return global::System.Text.RegularExpressions.Regex.Match(idProviderName, "^oidc-[a-z0-9-_\\.]{1,15}$").Success;
			}
			return false;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInCodeInfo> GenerateSignInCodeAsync(string identifier = null)
		{
			if (State != global::Unity.Services.Authentication.AuthenticationState.SignedOut && State != global::Unity.Services.Authentication.AuthenticationState.Expired)
			{
				global::Unity.Services.Core.RequestFailedException ex = ExceptionHandler.BuildClientInvalidStateException(State);
				SendSignInFailedEvent(ex, forceSignOut: false);
				throw ex;
			}
			global::Unity.Services.Authentication.CodeChallengeGenerator codeChallengeGenerator = new global::Unity.Services.Authentication.CodeChallengeGenerator();
			string codeVerifier = codeChallengeGenerator.GenerateCode();
			string codeChallenge = global::Unity.Services.Authentication.CodeChallengeGenerator.S256EncodeChallenge(codeVerifier);
			global::Unity.Services.Authentication.GenerateSignInCodeRequest request = new global::Unity.Services.Authentication.GenerateSignInCodeRequest
			{
				Identifier = identifier,
				CodeChallenge = codeChallenge
			};
			global::Unity.Services.Authentication.GenerateCodeResponse generateCodeResponse = await NetworkClient.GenerateSignInCodeAsync(request);
			global::Unity.Services.Authentication.SignInCodeInfo signInCodeInfo = new global::Unity.Services.Authentication.SignInCodeInfo
			{
				SignInCode = generateCodeResponse.SignInCode,
				Expiration = generateCodeResponse.Expiration
			};
			CodeLinkSessionId = generateCodeResponse.CodeLinkSessionId;
			CodeVerifier = codeVerifier;
			this.SignInCodeReceived?.Invoke(signInCodeInfo);
			return signInCodeInfo;
		}

		public async global::System.Threading.Tasks.Task SignInWithCodeAsync(bool usePolling = false, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (CodeVerifier == null || CodeLinkSessionId == null)
			{
				throw ExceptionHandler.BuildUnknownException("SignInWithCodeAsync failed: No sign-in code has been generated. Ensure GenerateSignInCode has been called and completed successfully before attempting to sign in");
			}
			global::Unity.Services.Authentication.SignInWithCodeRequest request = new global::Unity.Services.Authentication.SignInWithCodeRequest
			{
				CodeVerifier = CodeVerifier,
				CodeLinkSessionId = CodeLinkSessionId
			};
			try
			{
				global::Unity.Services.Authentication.SignInResponse signInResponse;
				if (usePolling)
				{
					signInResponse = await PollForCodeConfirmationAsync(request, cancellationToken);
				}
				else
				{
					signInResponse = await NetworkClient.SignInWithCodeAsync(request);
					if (string.IsNullOrEmpty(signInResponse.IdToken))
					{
						global::Unity.Services.Authentication.Logger.LogWarning("Sign In Code has not been confirmed.");
						return;
					}
				}
				CodeLinkSessionId = null;
				CodeVerifier = null;
				await HandleSignInRequestAsync(() => global::System.Threading.Tasks.Task.FromResult(signInResponse));
			}
			catch (global::Unity.Services.Authentication.WebRequestException)
			{
				CodeLinkSessionId = null;
				CodeVerifier = null;
				throw ExceptionHandler.BuildUnknownException("The sign-in code was not confirmed.");
			}
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> PollForCodeConfirmationAsync(global::Unity.Services.Authentication.SignInWithCodeRequest request, global::System.Threading.CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				await DelayWithScheduler(Settings.CodeConfirmationDelay);
				try
				{
					global::Unity.Services.Authentication.SignInResponse signInResponse = await NetworkClient.SignInWithCodeAsync(request);
					if (!string.IsNullOrEmpty(signInResponse.IdToken))
					{
						return signInResponse;
					}
				}
				catch (global::Unity.Services.Authentication.WebRequestException ex)
				{
					if (ex.ResponseCode == 404)
					{
						this.SignInCodeExpired?.Invoke();
						throw ExceptionHandler.BuildUnknownException("The sign-in code has expired.");
					}
				}
			}
			global::Unity.Services.Core.RequestFailedException ex2 = ExceptionHandler.BuildUnknownException("The operation was canceled or timed out while waiting for code confirmation.");
			SendSignInFailedEvent(ex2, forceSignOut: true);
			throw ex2;
		}

		private global::System.Threading.Tasks.Task DelayWithScheduler(double delaySeconds)
		{
			global::System.Threading.Tasks.TaskCompletionSource<bool> tcs = new global::System.Threading.Tasks.TaskCompletionSource<bool>();
			m_Scheduler.ScheduleAction(delegate
			{
				tcs.SetResult(result: true);
			}, delaySeconds);
			return tcs.Task;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInCodeInfo> GetSignInCodeInfoAsync(string code)
		{
			if (IsAuthorized)
			{
				if (string.IsNullOrEmpty(code))
				{
					throw ExceptionHandler.BuildUnknownException("Code cannot be null or empty");
				}
				try
				{
					global::Unity.Services.Authentication.CodeLinkInfoRequest request = new global::Unity.Services.Authentication.CodeLinkInfoRequest
					{
						SignInCode = code
					};
					global::Unity.Services.Authentication.CodeLinkInfoResponse codeLinkInfoResponse = await NetworkClient.GetCodeIdentifierAsync(request);
					return new global::Unity.Services.Authentication.SignInCodeInfo
					{
						SignInCode = code,
						Identifier = codeLinkInfoResponse.Identifier,
						Expiration = codeLinkInfoResponse.Expiration
					};
				}
				catch (global::Unity.Services.Authentication.WebRequestException exception)
				{
					throw ExceptionHandler.ConvertException(exception);
				}
				catch (global::System.Exception ex)
				{
					throw ExceptionHandler.BuildUnknownException(ex.Message);
				}
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		public async global::System.Threading.Tasks.Task ConfirmCodeAsync(string code, string idProvider = null, string externalToken = null)
		{
			if (string.IsNullOrEmpty(code))
			{
				throw ExceptionHandler.BuildUnknownException("Code cannot be null or empty");
			}
			if (IsAuthorized)
			{
				try
				{
					string sessionToken = (SessionTokenExists ? SessionTokenComponent.SessionToken : null);
					global::Unity.Services.Authentication.ConfirmSignInCodeRequest request = new global::Unity.Services.Authentication.ConfirmSignInCodeRequest
					{
						SignInCode = code,
						IdProvider = idProvider,
						SessionToken = sessionToken,
						ExternalToken = externalToken
					};
					await NetworkClient.ConfirmCodeAsync(request);
					return;
				}
				catch (global::Unity.Services.Authentication.WebRequestException exception)
				{
					throw ExceptionHandler.ConvertException(exception);
				}
				catch (global::System.Exception ex)
				{
					throw ExceptionHandler.BuildUnknownException(ex.Message);
				}
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		internal AuthenticationServiceInternal(global::Unity.Services.Authentication.IAuthenticationSettings settings, global::Unity.Services.Authentication.IAuthenticationNetworkClient networkClient, global::Unity.Services.Authentication.Generated.IPlayerNamesApi playerNamesApi, global::Unity.Services.Authentication.IProfile profile, global::Unity.Services.Authentication.IJwtDecoder jwtDecoder, global::Unity.Services.Authentication.IAuthenticationCache cache, global::Unity.Services.Core.Scheduler.Internal.IActionScheduler scheduler, global::Unity.Services.Authentication.IAuthenticationMetrics metrics, global::Unity.Services.Authentication.AccessTokenComponent accessToken, global::Unity.Services.Authentication.EnvironmentIdComponent environmentId, global::Unity.Services.Authentication.PlayerIdComponent playerId, global::Unity.Services.Authentication.PlayerNameComponent playerName, global::Unity.Services.Authentication.SessionTokenComponent sessionToken, global::Unity.Services.Core.Environments.Internal.IEnvironments environment)
		{
			Settings = settings;
			NetworkClient = networkClient;
			PlayerNamesApi = playerNamesApi;
			m_Profile = profile;
			m_JwtDecoder = jwtDecoder;
			m_Cache = cache;
			m_Scheduler = scheduler;
			m_Metrics = metrics;
			ExceptionHandler = new global::Unity.Services.Authentication.AuthenticationExceptionHandler(m_Metrics);
			AccessTokenComponent = accessToken;
			EnvironmentIdComponent = environmentId;
			PlayerIdComponent = playerId;
			PlayerNameComponent = playerName;
			SessionTokenComponent = sessionToken;
			EnvironmentComponent = environment;
			State = global::Unity.Services.Authentication.AuthenticationState.SignedOut;
			MigrateCache();
			PlayerIdComponent.PlayerIdChanged += OnPlayerIdChanged;
			Expired += delegate
			{
				m_Metrics.SendExpiredSessionMetric();
			};
		}

		private void OnPlayerIdChanged(string playerId)
		{
			PlayerNameComponent.Clear();
		}

		public global::System.Threading.Tasks.Task SignInAnonymouslyAsync(global::Unity.Services.Authentication.SignInOptions options = null)
		{
			if (State == global::Unity.Services.Authentication.AuthenticationState.SignedOut || State == global::Unity.Services.Authentication.AuthenticationState.Expired)
			{
				if (SessionTokenExists)
				{
					string sessionToken = SessionTokenComponent.SessionToken;
					if (string.IsNullOrEmpty(sessionToken))
					{
						SessionTokenComponent.Clear();
						global::Unity.Services.Core.RequestFailedException exception = ExceptionHandler.BuildClientSessionTokenNotExistsException();
						SendSignInFailedEvent(exception, forceSignOut: true);
						return global::System.Threading.Tasks.Task.FromException(exception);
					}
					return HandleSignInRequestAsync(() => NetworkClient.SignInWithSessionTokenAsync(sessionToken));
				}
				if (options == null || options.CreateAccount)
				{
					return HandleSignInRequestAsync(NetworkClient.SignInAnonymouslyAsync);
				}
				SessionTokenComponent.Clear();
				global::Unity.Services.Core.RequestFailedException exception2 = ExceptionHandler.BuildClientSessionTokenNotExistsException();
				SendSignInFailedEvent(exception2, forceSignOut: true);
				return global::System.Threading.Tasks.Task.FromException(exception2);
			}
			global::Unity.Services.Core.RequestFailedException exception3 = ExceptionHandler.BuildClientInvalidStateException(State);
			SendSignInFailedEvent(exception3, forceSignOut: false);
			return global::System.Threading.Tasks.Task.FromException(exception3);
		}

		public async global::System.Threading.Tasks.Task DeleteAccountAsync()
		{
			if (IsAuthorized)
			{
				try
				{
					await NetworkClient.DeleteAccountAsync(PlayerId);
					SignOut(clearCredentials: true);
					return;
				}
				catch (global::Unity.Services.Authentication.WebRequestException exception)
				{
					throw ExceptionHandler.ConvertException(exception);
				}
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		public void SignOut(bool clearCredentials = false)
		{
			AccessTokenComponent.Clear();
			PlayerInfo = null;
			m_Notifications = null;
			if (clearCredentials)
			{
				SessionTokenComponent.Clear();
				PlayerIdComponent.Clear();
				PlayerNameComponent.Clear();
			}
			CancelScheduledRefresh();
			CancelScheduledExpiration();
			ChangeState(global::Unity.Services.Authentication.AuthenticationState.SignedOut);
		}

		public void SwitchProfile(string profile)
		{
			if (State == global::Unity.Services.Authentication.AuthenticationState.SignedOut)
			{
				if (!string.IsNullOrEmpty(profile) && global::System.Text.RegularExpressions.Regex.Match(profile, "^[a-zA-Z0-9_-]{1,30}$").Success)
				{
					m_Profile.Current = profile;
					PlayerIdComponent.Refresh();
					SessionTokenComponent.Refresh();
					PlayerNameComponent.Refresh();
					return;
				}
				throw ExceptionHandler.BuildClientInvalidProfileException();
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		public void ClearSessionToken()
		{
			if (State == global::Unity.Services.Authentication.AuthenticationState.SignedOut)
			{
				SessionTokenComponent.Clear();
				return;
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.PlayerInfo> GetPlayerInfoAsync()
		{
			if (IsAuthorized)
			{
				try
				{
					PlayerInfo = new global::Unity.Services.Authentication.PlayerInfo(await NetworkClient.GetPlayerInfoAsync(PlayerId));
					return PlayerInfo;
				}
				catch (global::Unity.Services.Authentication.WebRequestException exception)
				{
					throw ExceptionHandler.ConvertException(exception);
				}
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		internal global::System.Threading.Tasks.Task RefreshAccessTokenAsync()
		{
			if (IsSignedIn)
			{
				if (State == global::Unity.Services.Authentication.AuthenticationState.Expired)
				{
					return global::System.Threading.Tasks.Task.CompletedTask;
				}
				string sessionToken = SessionTokenComponent.SessionToken;
				if (string.IsNullOrEmpty(sessionToken))
				{
					return global::System.Threading.Tasks.Task.CompletedTask;
				}
				return StartRefreshAsync(sessionToken);
			}
			return global::System.Threading.Tasks.Task.CompletedTask;
		}

		internal async global::System.Threading.Tasks.Task HandleSignInRequestAsync(global::System.Func<global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse>> signInRequest, bool enableRefresh = true)
		{
			try
			{
				ChangeState(global::Unity.Services.Authentication.AuthenticationState.SigningIn);
				CompleteSignIn(await signInRequest(), enableRefresh);
			}
			catch (global::Unity.Services.Core.RequestFailedException exception)
			{
				SendSignInFailedEvent(exception, forceSignOut: true);
				throw;
			}
			catch (global::Unity.Services.Authentication.WebRequestException exception2)
			{
				global::Unity.Services.Core.RequestFailedException ex = ExceptionHandler.ConvertException(exception2);
				if (ex.ErrorCode == global::Unity.Services.Authentication.AuthenticationErrorCodes.InvalidSessionToken)
				{
					SessionTokenComponent.Clear();
					global::Unity.Services.Authentication.Logger.Log("The session token is invalid and has been cleared. The associated account is no longer accessible through this login method.");
				}
				SendSignInFailedEvent(ex, forceSignOut: true);
				throw ex;
			}
		}

		internal async global::System.Threading.Tasks.Task StartRefreshAsync(string sessionToken)
		{
			ChangeState(global::Unity.Services.Authentication.AuthenticationState.Refreshing);
			try
			{
				CompleteSignIn(await NetworkClient.SignInWithSessionTokenAsync(sessionToken));
			}
			catch (global::Unity.Services.Core.RequestFailedException)
			{
				global::Unity.Services.Authentication.Logger.LogWarning("The access token is not valid. Retry and refresh again.");
				if (State != global::Unity.Services.Authentication.AuthenticationState.Expired)
				{
					Expire();
				}
			}
			catch (global::Unity.Services.Authentication.WebRequestException)
			{
				if (State == global::Unity.Services.Authentication.AuthenticationState.Refreshing)
				{
					global::Unity.Services.Authentication.Logger.LogWarning("Failed to refresh access token due to network error or internal server error, will retry later.");
					ChangeState(global::Unity.Services.Authentication.AuthenticationState.Authorized);
					ScheduleRefresh(Settings.RefreshAttemptFrequency);
				}
			}
		}

		internal void CompleteSignIn(global::Unity.Services.Authentication.SignInResponse response, bool enableRefresh = true)
		{
			CompleteSignIn(response.IdToken, response.SessionToken, enableRefresh, response.User, response.LastNotificationDate);
		}

		private void CompleteSignIn(string accessToken, string sessionToken, bool enableRefresh = true, global::Unity.Services.Authentication.User user = null, string lastNotificationDate = null)
		{
			try
			{
				global::Unity.Services.Authentication.AccessToken accessToken2 = m_JwtDecoder.Decode<global::Unity.Services.Authentication.AccessToken>(accessToken);
				if (accessToken2 == null)
				{
					throw global::Unity.Services.Authentication.AuthenticationException.Create(51, "Failed to decode and verify access token.");
				}
				AccessTokenComponent.AccessToken = accessToken;
				if (accessToken2.Audience != null)
				{
					EnvironmentIdComponent.EnvironmentId = global::System.Linq.Enumerable.FirstOrDefault(accessToken2.Audience, (string s) => s.StartsWith("envId:"))?.Substring(6);
				}
				PlayerInfo = ((user != null) ? new global::Unity.Services.Authentication.PlayerInfo(user) : new global::Unity.Services.Authentication.PlayerInfo(accessToken2.Subject));
				PlayerIdComponent.PlayerId = accessToken2.Subject;
				SessionTokenComponent.SessionToken = sessionToken;
				long num = accessToken2.Expiration - accessToken2.IssuedAt;
				long num2 = num - Settings.AccessTokenRefreshBuffer;
				long num3 = num - Settings.AccessTokenExpiryBuffer;
				if (enableRefresh && sessionToken != null && num2 > 0 && num2 < num3)
				{
					ScheduleRefresh(num2);
				}
				if (num3 > 0)
				{
					ScheduleExpiration(num3);
				}
				LastNotificationDate = lastNotificationDate;
				ChangeState(global::Unity.Services.Authentication.AuthenticationState.Authorized);
			}
			catch (global::Unity.Services.Authentication.AuthenticationException)
			{
				throw;
			}
			catch (global::System.Exception innerException)
			{
				throw global::Unity.Services.Authentication.AuthenticationException.Create(0, "Unknown error completing sign-in.", innerException);
			}
		}

		internal void ScheduleRefresh(double delay)
		{
			if (delay >= 0.0)
			{
				CancelScheduledRefresh();
				RefreshActionId = m_Scheduler.ScheduleAction(ExecuteScheduledRefresh, delay);
				AccessTokenComponent.RefreshTime = global::System.DateTime.UtcNow.AddSeconds(delay);
			}
			else
			{
				global::Unity.Services.Authentication.Logger.LogError($"Schedule delay for refresh is invalid ({delay}).");
			}
		}

		internal void ScheduleExpiration(double delay)
		{
			if (delay >= 0.0)
			{
				CancelScheduledExpiration();
				ExpirationActionId = m_Scheduler.ScheduleAction(ExecuteScheduledExpiration, delay);
				AccessTokenComponent.ExpiryTime = global::System.DateTime.UtcNow.AddSeconds(delay);
			}
			else
			{
				global::Unity.Services.Authentication.Logger.LogError($"Schedule delay for expiration is invalid ({delay}).");
			}
		}

		internal void ExecuteScheduledRefresh()
		{
			RefreshActionId = null;
			AccessTokenComponent.RefreshTime = null;
			RefreshAccessTokenAsync();
		}

		internal void ExecuteScheduledExpiration()
		{
			ExpirationActionId = null;
			AccessTokenComponent.ExpiryTime = null;
			Expire();
		}

		internal void CancelScheduledRefresh()
		{
			if (RefreshActionId.HasValue)
			{
				m_Scheduler.CancelAction(RefreshActionId.Value);
				RefreshActionId = null;
				AccessTokenComponent.RefreshTime = null;
			}
		}

		internal void CancelScheduledExpiration()
		{
			if (ExpirationActionId.HasValue)
			{
				m_Scheduler.CancelAction(ExpirationActionId.Value);
				ExpirationActionId = null;
				AccessTokenComponent.ExpiryTime = null;
			}
		}

		internal void Expire()
		{
			AccessTokenComponent.Clear();
			CancelScheduledRefresh();
			CancelScheduledExpiration();
			ChangeState(global::Unity.Services.Authentication.AuthenticationState.Expired);
		}

		internal void MigrateCache()
		{
			try
			{
				SessionTokenComponent.Migrate();
			}
			catch (global::System.Exception exception)
			{
				global::Unity.Services.Authentication.Logger.LogException(exception);
			}
		}

		private void ChangeState(global::Unity.Services.Authentication.AuthenticationState newState)
		{
			if (State != newState)
			{
				global::Unity.Services.Authentication.AuthenticationState state = State;
				State = newState;
				HandleStateChanged(state, newState);
			}
		}

		private void HandleStateChanged(global::Unity.Services.Authentication.AuthenticationState oldState, global::Unity.Services.Authentication.AuthenticationState newState)
		{
			this.StateChanged?.Invoke(oldState, newState);
			switch (newState)
			{
			case global::Unity.Services.Authentication.AuthenticationState.Authorized:
				if (oldState != global::Unity.Services.Authentication.AuthenticationState.Refreshing)
				{
					this.SignedIn?.Invoke();
				}
				break;
			case global::Unity.Services.Authentication.AuthenticationState.SignedOut:
				if (oldState != global::Unity.Services.Authentication.AuthenticationState.SigningIn)
				{
					this.SignedOut?.Invoke();
				}
				break;
			case global::Unity.Services.Authentication.AuthenticationState.Expired:
				this.Expired?.Invoke();
				break;
			case global::Unity.Services.Authentication.AuthenticationState.SigningIn:
			case global::Unity.Services.Authentication.AuthenticationState.Refreshing:
				break;
			}
		}

		private void SendSignInFailedEvent(global::Unity.Services.Core.RequestFailedException exception, bool forceSignOut)
		{
			this.SignInFailed?.Invoke(exception);
			if (forceSignOut)
			{
				SignOut();
			}
		}

		public global::System.Threading.Tasks.Task SignInWithAppleAsync(string idToken, global::Unity.Services.Authentication.SignInOptions options = null)
		{
			return SignInWithExternalTokenAsync("apple.com", new global::Unity.Services.Authentication.SignInWithExternalTokenRequest
			{
				IdProvider = "apple.com",
				Token = idToken,
				SignInOnly = (options != null && !options.CreateAccount)
			});
		}

		public global::System.Threading.Tasks.Task LinkWithAppleAsync(string idToken, global::Unity.Services.Authentication.LinkOptions options = null)
		{
			return LinkWithExternalTokenAsync("apple.com", new global::Unity.Services.Authentication.LinkWithExternalTokenRequest
			{
				IdProvider = "apple.com",
				Token = idToken,
				ForceLink = (options?.ForceLink ?? false)
			});
		}

		public global::System.Threading.Tasks.Task UnlinkAppleAsync()
		{
			return UnlinkExternalTokenAsync("apple.com");
		}

		public global::System.Threading.Tasks.Task SignInWithAppleGameCenterAsync(string signature, string teamPlayerId, string publicKeyURL, string salt, ulong timestamp, global::Unity.Services.Authentication.SignInOptions options = null)
		{
			return SignInWithExternalTokenAsync("apple-game-center", new global::Unity.Services.Authentication.SignInWithAppleGameCenterRequest
			{
				IdProvider = "apple-game-center",
				Token = signature,
				AppleGameCenterConfig = new global::Unity.Services.Authentication.AppleGameCenterConfig
				{
					TeamPlayerId = teamPlayerId,
					PublicKeyURL = publicKeyURL,
					Salt = salt,
					Timestamp = timestamp
				},
				SignInOnly = (options != null && !options.CreateAccount)
			});
		}

		public global::System.Threading.Tasks.Task LinkWithAppleGameCenterAsync(string signature, string teamPlayerId, string publicKeyURL, string salt, ulong timestamp, global::Unity.Services.Authentication.LinkOptions options = null)
		{
			return LinkWithExternalTokenAsync("apple-game-center", new global::Unity.Services.Authentication.LinkWithAppleGameCenterRequest
			{
				IdProvider = "apple-game-center",
				Token = signature,
				AppleGameCenterConfig = new global::Unity.Services.Authentication.AppleGameCenterConfig
				{
					TeamPlayerId = teamPlayerId,
					PublicKeyURL = publicKeyURL,
					Salt = salt,
					Timestamp = timestamp
				},
				ForceLink = (options?.ForceLink ?? false)
			});
		}

		public global::System.Threading.Tasks.Task UnlinkAppleGameCenterAsync()
		{
			return UnlinkExternalTokenAsync("apple-game-center");
		}

		public global::System.Threading.Tasks.Task SignInWithFacebookAsync(string accessToken, global::Unity.Services.Authentication.SignInOptions options = null)
		{
			return SignInWithExternalTokenAsync("facebook.com", new global::Unity.Services.Authentication.SignInWithExternalTokenRequest
			{
				IdProvider = "facebook.com",
				Token = accessToken,
				SignInOnly = (options != null && !options.CreateAccount)
			});
		}

		public global::System.Threading.Tasks.Task LinkWithFacebookAsync(string accessToken, global::Unity.Services.Authentication.LinkOptions options = null)
		{
			return LinkWithExternalTokenAsync("facebook.com", new global::Unity.Services.Authentication.LinkWithExternalTokenRequest
			{
				IdProvider = "facebook.com",
				Token = accessToken,
				ForceLink = (options?.ForceLink ?? false)
			});
		}

		public global::System.Threading.Tasks.Task UnlinkFacebookAsync()
		{
			return UnlinkExternalTokenAsync("facebook.com");
		}

		public global::System.Threading.Tasks.Task SignInWithGoogleAsync(string idToken, global::Unity.Services.Authentication.SignInOptions options = null)
		{
			return SignInWithExternalTokenAsync("google.com", new global::Unity.Services.Authentication.SignInWithExternalTokenRequest
			{
				IdProvider = "google.com",
				Token = idToken,
				SignInOnly = (options != null && !options.CreateAccount)
			});
		}

		public global::System.Threading.Tasks.Task LinkWithGoogleAsync(string idToken, global::Unity.Services.Authentication.LinkOptions options = null)
		{
			return LinkWithExternalTokenAsync("google.com", new global::Unity.Services.Authentication.LinkWithExternalTokenRequest
			{
				IdProvider = "google.com",
				Token = idToken,
				ForceLink = (options?.ForceLink ?? false)
			});
		}

		public global::System.Threading.Tasks.Task UnlinkGoogleAsync()
		{
			return UnlinkExternalTokenAsync("google.com");
		}

		public global::System.Threading.Tasks.Task SignInWithGooglePlayGamesAsync(string authCode, global::Unity.Services.Authentication.SignInOptions options = null)
		{
			return SignInWithExternalTokenAsync("google-play-games", new global::Unity.Services.Authentication.SignInWithExternalTokenRequest
			{
				IdProvider = "google-play-games",
				Token = authCode,
				SignInOnly = (options != null && !options.CreateAccount)
			});
		}

		public global::System.Threading.Tasks.Task LinkWithGooglePlayGamesAsync(string authCode, global::Unity.Services.Authentication.LinkOptions options = null)
		{
			return LinkWithExternalTokenAsync("google-play-games", new global::Unity.Services.Authentication.LinkWithExternalTokenRequest
			{
				IdProvider = "google-play-games",
				Token = authCode,
				ForceLink = (options?.ForceLink ?? false)
			});
		}

		public global::System.Threading.Tasks.Task UnlinkGooglePlayGamesAsync()
		{
			return UnlinkExternalTokenAsync("google-play-games");
		}

		public global::System.Threading.Tasks.Task SignInWithOculusAsync(string nonce, string userId, global::Unity.Services.Authentication.SignInOptions options = null)
		{
			return SignInWithExternalTokenAsync("oculus", new global::Unity.Services.Authentication.SignInWithOculusRequest
			{
				IdProvider = "oculus",
				Token = nonce,
				OculusConfig = new global::Unity.Services.Authentication.OculusConfig
				{
					UserId = userId
				},
				SignInOnly = (options != null && !options.CreateAccount)
			});
		}

		public global::System.Threading.Tasks.Task LinkWithOculusAsync(string nonce, string userId, global::Unity.Services.Authentication.LinkOptions options = null)
		{
			return LinkWithExternalTokenAsync("oculus", new global::Unity.Services.Authentication.LinkWithOculusRequest
			{
				IdProvider = "oculus",
				Token = nonce,
				OculusConfig = new global::Unity.Services.Authentication.OculusConfig
				{
					UserId = userId
				},
				ForceLink = (options?.ForceLink ?? false)
			});
		}

		public global::System.Threading.Tasks.Task UnlinkOculusAsync()
		{
			return UnlinkExternalTokenAsync("oculus");
		}

		[global::System.Obsolete("This method is deprecated as of version 2.7.1. Please use the SignInWithSteamAsync method with the 'identity' parameter for better security.")]
		public global::System.Threading.Tasks.Task SignInWithSteamAsync(string sessionTicket, global::Unity.Services.Authentication.SignInOptions options = null)
		{
			return SignInWithExternalTokenAsync("steampowered.com", new global::Unity.Services.Authentication.SignInWithSteamRequest
			{
				IdProvider = "steampowered.com",
				Token = sessionTicket,
				SignInOnly = (options != null && !options.CreateAccount)
			});
		}

		[global::System.Obsolete("This method is deprecated as of version 2.7.1. Please use the LinkWithSteamAsync method with the 'identity' parameter for better security.")]
		public global::System.Threading.Tasks.Task LinkWithSteamAsync(string sessionTicket, global::Unity.Services.Authentication.LinkOptions options = null)
		{
			return LinkWithExternalTokenAsync("steampowered.com", new global::Unity.Services.Authentication.LinkWithSteamRequest
			{
				IdProvider = "steampowered.com",
				Token = sessionTicket,
				ForceLink = (options?.ForceLink ?? false)
			});
		}

		public global::System.Threading.Tasks.Task SignInWithSteamAsync(string sessionTicket, string identity, global::Unity.Services.Authentication.SignInOptions options = null)
		{
			ValidateSteamIdentity(identity);
			return SignInWithExternalTokenAsync("steampowered.com", new global::Unity.Services.Authentication.SignInWithSteamRequest
			{
				IdProvider = "steampowered.com",
				Token = sessionTicket,
				SteamConfig = new global::Unity.Services.Authentication.SteamConfig
				{
					identity = identity
				},
				SignInOnly = (options != null && !options.CreateAccount)
			});
		}

		public global::System.Threading.Tasks.Task LinkWithSteamAsync(string sessionTicket, string identity, global::Unity.Services.Authentication.LinkOptions options = null)
		{
			ValidateSteamIdentity(identity);
			return LinkWithExternalTokenAsync("steampowered.com", new global::Unity.Services.Authentication.LinkWithSteamRequest
			{
				IdProvider = "steampowered.com",
				Token = sessionTicket,
				SteamConfig = new global::Unity.Services.Authentication.SteamConfig
				{
					identity = identity
				},
				ForceLink = (options?.ForceLink ?? false)
			});
		}

		public global::System.Threading.Tasks.Task SignInWithSteamAsync(string sessionTicket, string identity, string appId, global::Unity.Services.Authentication.SignInOptions options = null)
		{
			ValidateSteamIdentity(identity);
			return SignInWithExternalTokenAsync("steampowered.com", new global::Unity.Services.Authentication.SignInWithSteamRequest
			{
				IdProvider = "steampowered.com",
				Token = sessionTicket,
				SteamConfig = new global::Unity.Services.Authentication.SteamConfig
				{
					identity = identity,
					appId = appId
				},
				SignInOnly = (options != null && !options.CreateAccount)
			});
		}

		public global::System.Threading.Tasks.Task LinkWithSteamAsync(string sessionTicket, string identity, string appId, global::Unity.Services.Authentication.LinkOptions options = null)
		{
			ValidateSteamIdentity(identity);
			return LinkWithExternalTokenAsync("steampowered.com", new global::Unity.Services.Authentication.LinkWithSteamRequest
			{
				IdProvider = "steampowered.com",
				Token = sessionTicket,
				SteamConfig = new global::Unity.Services.Authentication.SteamConfig
				{
					identity = identity,
					appId = appId
				},
				ForceLink = (options?.ForceLink ?? false)
			});
		}

		private void ValidateSteamIdentity(string identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw ExceptionHandler.BuildUnknownException("Identity cannot be null or empty.");
			}
			if (!global::System.Text.RegularExpressions.Regex.IsMatch(identity, "^[a-zA-Z0-9]{5,30}$"))
			{
				throw ExceptionHandler.BuildUnknownException("The provided identity must only contain alphanumeric characters and be between 5 and 30 characters in length.");
			}
		}

		public global::System.Threading.Tasks.Task UnlinkSteamAsync()
		{
			return UnlinkExternalTokenAsync("steampowered.com");
		}

		public global::System.Threading.Tasks.Task SignInWithUnityAsync(string token, global::Unity.Services.Authentication.SignInOptions options = null)
		{
			return SignInWithExternalTokenAsync("unity", new global::Unity.Services.Authentication.SignInWithExternalTokenRequest
			{
				IdProvider = "unity",
				Token = token,
				SignInOnly = (options != null && !options.CreateAccount)
			});
		}

		public global::System.Threading.Tasks.Task LinkWithUnityAsync(string token, global::Unity.Services.Authentication.LinkOptions options = null)
		{
			return LinkWithExternalTokenAsync("unity", new global::Unity.Services.Authentication.LinkWithExternalTokenRequest
			{
				IdProvider = "unity",
				Token = token,
				ForceLink = (options?.ForceLink ?? false)
			});
		}

		public global::System.Threading.Tasks.Task UnlinkUnityAsync()
		{
			return UnlinkExternalTokenAsync("unity");
		}

		internal global::System.Threading.Tasks.Task SignInWithExternalTokenAsync(string idProvider, global::Unity.Services.Authentication.SignInWithExternalTokenRequest request, bool enableRefresh = true)
		{
			if (State == global::Unity.Services.Authentication.AuthenticationState.SignedOut || State == global::Unity.Services.Authentication.AuthenticationState.Expired)
			{
				return HandleSignInRequestAsync(() => NetworkClient.SignInWithExternalTokenAsync(idProvider, request), enableRefresh);
			}
			global::Unity.Services.Core.RequestFailedException exception = ExceptionHandler.BuildClientInvalidStateException(State);
			SendSignInFailedEvent(exception, forceSignOut: false);
			return global::System.Threading.Tasks.Task.FromException(exception);
		}

		internal async global::System.Threading.Tasks.Task LinkWithExternalTokenAsync(string idProvider, global::Unity.Services.Authentication.LinkWithExternalTokenRequest request)
		{
			if (IsAuthorized)
			{
				try
				{
					global::Unity.Services.Authentication.LinkResponse linkResponse = await NetworkClient.LinkWithExternalTokenAsync(idProvider, request);
					PlayerInfo?.AddExternalIdentity(global::System.Linq.Enumerable.FirstOrDefault(linkResponse.User?.ExternalIds?, (global::Unity.Services.Authentication.ExternalIdentity x) => x.ProviderId == request.IdProvider));
					return;
				}
				catch (global::Unity.Services.Authentication.WebRequestException exception)
				{
					throw ExceptionHandler.ConvertException(exception);
				}
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		internal async global::System.Threading.Tasks.Task UnlinkExternalTokenAsync(string idProvider)
		{
			if (IsAuthorized)
			{
				string text = PlayerInfo?.GetIdentityId(idProvider);
				if (text == null)
				{
					throw ExceptionHandler.BuildClientUnlinkExternalIdNotFoundException();
				}
				try
				{
					await NetworkClient.UnlinkExternalTokenAsync(idProvider, new global::Unity.Services.Authentication.UnlinkRequest
					{
						IdProvider = idProvider,
						ExternalId = text
					});
					PlayerInfo.RemoveIdentity(idProvider);
					return;
				}
				catch (global::Unity.Services.Authentication.WebRequestException exception)
				{
					throw ExceptionHandler.ConvertException(exception);
				}
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification>> GetNotificationsAsync()
		{
			if (!IsAuthorized)
			{
				throw ExceptionHandler.BuildClientInvalidStateException(State);
			}
			try
			{
				m_Notifications = (await NetworkClient.GetNotificationsAsync(PlayerId)).ToNotificationList();
				return m_Notifications;
			}
			catch (global::Unity.Services.Authentication.WebRequestException exception)
			{
				throw ExceptionHandler.ConvertException(exception);
			}
		}

		public async global::System.Threading.Tasks.Task<string> GetPlayerNameAsync(bool autoGenerate = true)
		{
			if (IsAuthorized)
			{
				try
				{
					PlayerNamesApi.Configuration.AccessToken = AccessTokenComponent.AccessToken;
					global::Unity.Services.Authentication.Generated.Player data = (await PlayerNamesApi.GetNameAsync(PlayerId, autoGenerate)).Data;
					PlayerNameComponent.PlayerName = data.Name;
					return data.Name;
				}
				catch (global::Unity.Services.Authentication.Shared.ApiException ex)
				{
					if (ex.Response.StatusCode == 404)
					{
						PlayerNameComponent.Clear();
						return null;
					}
					throw ExceptionHandler.ConvertException(ex);
				}
				catch (global::System.Exception ex2)
				{
					throw ExceptionHandler.BuildUnknownException(ex2.Message);
				}
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		public async global::System.Threading.Tasks.Task<string> UpdatePlayerNameAsync(string playerName)
		{
			if (IsAuthorized)
			{
				if (string.IsNullOrWhiteSpace(playerName) || global::System.Linq.Enumerable.Any(playerName, char.IsWhiteSpace))
				{
					throw ExceptionHandler.BuildInvalidPlayerNameException();
				}
				try
				{
					PlayerNamesApi.Configuration.AccessToken = AccessTokenComponent.AccessToken;
					string text = (await PlayerNamesApi.UpdateNameAsync(PlayerId, new global::Unity.Services.Authentication.Generated.UpdateNameRequest(playerName))).Data?.Name;
					if (string.IsNullOrWhiteSpace(text))
					{
						throw ExceptionHandler.BuildUnknownException("Invalid player name response");
					}
					PlayerNameComponent.PlayerName = text;
					return text;
				}
				catch (global::Unity.Services.Authentication.Shared.ApiException exception)
				{
					throw ExceptionHandler.ConvertException(exception);
				}
				catch (global::System.Exception ex)
				{
					throw ExceptionHandler.BuildUnknownException(ex.Message);
				}
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		public global::System.Threading.Tasks.Task SignInWithUsernamePasswordAsync(string username, string password)
		{
			return SignInWithUsernamePasswordRequestAsync(BuildUsernamePasswordRequest(username, password));
		}

		public global::System.Threading.Tasks.Task SignUpWithUsernamePasswordAsync(string username, string password)
		{
			return SignUpWithUsernamePasswordRequestAsync(BuildUsernamePasswordRequest(username, password));
		}

		public global::System.Threading.Tasks.Task AddUsernamePasswordAsync(string username, string password)
		{
			return AddUsernamePasswordRequestAsync(BuildUsernamePasswordRequest(username, password));
		}

		public global::System.Threading.Tasks.Task UpdatePasswordAsync(string currentPassword, string newPassword)
		{
			if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword))
			{
				throw ExceptionHandler.BuildInvalidCredentialsException();
			}
			return UpdatePasswordRequestAsync(new global::Unity.Services.Authentication.UpdatePasswordRequest
			{
				Password = currentPassword,
				NewPassword = newPassword
			});
		}

		internal global::System.Threading.Tasks.Task SignInWithUsernamePasswordRequestAsync(global::Unity.Services.Authentication.UsernamePasswordRequest request, bool enableRefresh = true)
		{
			if (State == global::Unity.Services.Authentication.AuthenticationState.SignedOut || State == global::Unity.Services.Authentication.AuthenticationState.Expired)
			{
				return HandleSignInRequestAsync(() => NetworkClient.SignInWithUsernamePasswordAsync(request), enableRefresh);
			}
			global::Unity.Services.Core.RequestFailedException exception = ExceptionHandler.BuildClientInvalidStateException(State);
			SendSignInFailedEvent(exception, forceSignOut: false);
			return global::System.Threading.Tasks.Task.FromException(exception);
		}

		internal global::System.Threading.Tasks.Task SignUpWithUsernamePasswordRequestAsync(global::Unity.Services.Authentication.UsernamePasswordRequest request, bool enableRefresh = true)
		{
			if (State == global::Unity.Services.Authentication.AuthenticationState.SignedOut || State == global::Unity.Services.Authentication.AuthenticationState.Expired)
			{
				return HandleSignInRequestAsync(() => NetworkClient.SignUpWithUsernamePasswordAsync(request), enableRefresh);
			}
			global::Unity.Services.Core.RequestFailedException exception = ExceptionHandler.BuildClientInvalidStateException(State);
			SendSignInFailedEvent(exception, forceSignOut: false);
			return global::System.Threading.Tasks.Task.FromException(exception);
		}

		internal async global::System.Threading.Tasks.Task AddUsernamePasswordRequestAsync(global::Unity.Services.Authentication.UsernamePasswordRequest request)
		{
			if (IsAuthorized)
			{
				try
				{
					global::Unity.Services.Authentication.SignInResponse signInResponse = await NetworkClient.AddUsernamePasswordAsync(request);
					PlayerInfo.Username = signInResponse.User?.Username;
					return;
				}
				catch (global::Unity.Services.Authentication.WebRequestException exception)
				{
					throw ExceptionHandler.ConvertException(exception);
				}
				catch (global::System.Exception ex)
				{
					throw ExceptionHandler.BuildUnknownException(ex.Message);
				}
			}
			throw ExceptionHandler.BuildClientInvalidStateException(State);
		}

		internal global::System.Threading.Tasks.Task UpdatePasswordRequestAsync(global::Unity.Services.Authentication.UpdatePasswordRequest request, bool enableRefresh = true)
		{
			if (IsAuthorized)
			{
				return HandleUpdatePasswordRequestAsync(() => NetworkClient.UpdatePasswordAsync(request), enableRefresh);
			}
			return global::System.Threading.Tasks.Task.FromException(ExceptionHandler.BuildClientInvalidStateException(State));
		}

		internal async global::System.Threading.Tasks.Task HandleUpdatePasswordRequestAsync(global::System.Func<global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse>> updatePasswordRequest, bool enableRefresh = true)
		{
			try
			{
				CompleteSignIn(await updatePasswordRequest(), enableRefresh);
			}
			catch (global::Unity.Services.Core.RequestFailedException exception)
			{
				SendUpdatePasswordFailedEvent(exception, forceSignOut: false);
				throw;
			}
			catch (global::Unity.Services.Authentication.WebRequestException exception2)
			{
				global::Unity.Services.Core.RequestFailedException ex = ExceptionHandler.ConvertException(exception2);
				if (ex.ErrorCode == global::Unity.Services.Authentication.AuthenticationErrorCodes.InvalidSessionToken)
				{
					SessionTokenComponent.Clear();
					global::Unity.Services.Authentication.Logger.Log("The session token is invalid and has been cleared. The associated account is no longer accessible through this login method.");
				}
				SendUpdatePasswordFailedEvent(ex, forceSignOut: false);
				throw ex;
			}
		}

		private global::Unity.Services.Authentication.UsernamePasswordRequest BuildUsernamePasswordRequest(string username, string password)
		{
			if (!ValidateCredentials(username, password))
			{
				throw ExceptionHandler.BuildInvalidCredentialsException();
			}
			return new global::Unity.Services.Authentication.UsernamePasswordRequest
			{
				Username = username,
				Password = password
			};
		}

		private void SendUpdatePasswordFailedEvent(global::Unity.Services.Core.RequestFailedException exception, bool forceSignOut)
		{
			this.UpdatePasswordFailed?.Invoke(exception);
			if (forceSignOut)
			{
				SignOut();
			}
		}

		private bool ValidateCredentials(string username, string password)
		{
			if (!string.IsNullOrEmpty(username))
			{
				return !string.IsNullOrEmpty(password);
			}
			return false;
		}
	}
}
