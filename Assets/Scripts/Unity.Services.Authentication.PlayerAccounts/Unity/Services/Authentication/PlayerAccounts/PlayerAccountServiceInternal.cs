namespace Unity.Services.Authentication.PlayerAccounts
{
	internal class PlayerAccountServiceInternal : global::Unity.Services.Authentication.PlayerAccounts.IPlayerAccountService
	{
		private const string k_AccountPortalUrl = "https://player-account.unity.com";

		private const string k_AuthUrl = "https://player-login.unity.com/v1/oauth2/auth";

		private const string k_TokenUrl = "https://player-login.unity.com/v1/oauth2/token";

		private const string k_CodeChallengeMethod = "S256";

		private readonly global::Unity.Services.Core.Configuration.Internal.ICloudProjectId m_CloudProjectId;

		private readonly global::Unity.Services.Authentication.PlayerAccounts.IBrowserUtils m_BrowserUtils;

		private readonly global::Unity.Services.Authentication.PlayerAccounts.IJwtDecoder m_JwtDecoder;

		private readonly global::Unity.Services.Authentication.PlayerAccounts.INetworkHandler m_NetworkingClient;

		private readonly global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings m_Settings;

		public string AccountPortalUrl => "https://player-account.unity.com";

		public bool IsSignedIn
		{
			get
			{
				if (SignInState != global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState.Authorized)
				{
					return SignInState == global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState.Refreshing;
				}
				return true;
			}
		}

		public string AccessToken { get; internal set; }

		public string IdToken { get; internal set; }

		public string RefreshToken { get; internal set; }

		public global::Unity.Services.Authentication.PlayerAccounts.IdToken IdTokenClaims { get; internal set; }

		internal global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState SignInState { get; set; }

		internal string RedirectUri { get; set; }

		internal string CodeVerifier { get; set; }

		internal string ClientId => m_Settings?.ClientId;

		public event global::System.Action SignedIn;

		public event global::System.Action SignedOut;

		public event global::System.Action<global::Unity.Services.Core.RequestFailedException> SignInFailed;

		internal PlayerAccountServiceInternal(global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings settings, global::Unity.Services.Core.Configuration.Internal.ICloudProjectId cloudProjectId, global::Unity.Services.Authentication.PlayerAccounts.IJwtDecoder jwtDecoder, global::Unity.Services.Authentication.PlayerAccounts.INetworkHandler networkingClient)
		{
			m_Settings = settings;
			m_CloudProjectId = cloudProjectId;
			m_BrowserUtils = global::Unity.Services.Authentication.PlayerAccounts.BrowserUtils.CreateBrowserUtils(m_CloudProjectId, m_Settings, OnAuthCodeReceived);
			m_JwtDecoder = jwtDecoder;
			m_NetworkingClient = networkingClient;
			SignInState = global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState.SignedOut;
			global::UnityEngine.Application.deepLinkActivated += OnDeepLinkActivated;
		}

		public async global::System.Threading.Tasks.Task StartSignInAsync(bool isSigningUp = false)
		{
			if (SignInState == global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState.Authorized || SignInState == global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState.Refreshing)
			{
				throw global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10101, "Player is already signed in.");
			}
			if (string.IsNullOrEmpty(ClientId))
			{
				throw global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10102, "The Client Id is not configured.");
			}
			SignInState = global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState.SigningIn;
			try
			{
				if (!m_BrowserUtils.Bind())
				{
					throw global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10101, "Platform binding failed");
				}
				await m_BrowserUtils.LaunchUrlAsync(BuildAuthorizationRequestUrl(isSigningUp));
			}
			catch (global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException exception)
			{
				SendSignInFailedEvent(exception, forceSignOut: true);
				throw;
			}
			catch (global::Unity.Services.Core.RequestFailedException ex)
			{
				SendSignInFailedEvent(new global::Unity.Services.Core.RequestFailedException(ex.ErrorCode, "Error opening system browser for OAuth 2.0 authorization request."), forceSignOut: true);
			}
		}

		public global::System.Threading.Tasks.Task RefreshTokenAsync()
		{
			if (!IsSignedIn)
			{
				throw global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10101, "Player is not signed in.");
			}
			string refreshToken = RefreshToken;
			if (string.IsNullOrEmpty(refreshToken))
			{
				throw global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10107, "Refresh token is null or empty.");
			}
			SignInState = global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState.Refreshing;
			string refreshRequest = "client_id=" + ClientId + "&refresh_token=" + refreshToken + "&grant_type=refresh_token";
			if (!string.IsNullOrEmpty(m_Settings.Scope))
			{
				refreshRequest = refreshRequest + "&scope=" + m_Settings.Scope;
			}
			return HandleSignInRequestAsync(() => m_NetworkingClient.PostAsync<global::Unity.Services.Authentication.PlayerAccounts.SignInResponse>("https://player-login.unity.com/v1/oauth2/token", refreshRequest));
		}

		public void SignOut()
		{
			AccessToken = null;
			global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState signInState = SignInState;
			SignInState = global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState.SignedOut;
			if (signInState != global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState.SigningIn)
			{
				this.SignedOut?.Invoke();
			}
		}

		private string BuildAuthorizationRequestUrl(bool isSigningUp)
		{
			global::Unity.Services.Authentication.PlayerAccounts.CodeChallengeGenerator codeChallengeGenerator = new global::Unity.Services.Authentication.PlayerAccounts.CodeChallengeGenerator();
			CodeVerifier = codeChallengeGenerator.GenerateCode();
			string text = codeChallengeGenerator.GenerateStateString();
			string text2 = global::Unity.Services.Authentication.PlayerAccounts.CodeChallengeGenerator.S256EncodeChallenge(CodeVerifier);
			RedirectUri = m_BrowserUtils?.GetRedirectUri();
			string text3 = "https://player-login.unity.com/v1/oauth2/auth?response_type=code&redirect_uri=" + global::System.Uri.EscapeDataString(RedirectUri) + "&response_mode=query&client_id=" + ClientId + "&state=" + text + "&code_challenge=" + text2 + "&code_challenge_method=S256";
			if (isSigningUp)
			{
				text3 += "&action=sign-up";
			}
			if (!string.IsNullOrEmpty(m_Settings.Scope))
			{
				text3 = text3 + "&scope=" + m_Settings.Scope;
			}
			global::Unity.Services.Authentication.PlayerAccounts.Logger.Log("AuthorizationRequest URL: " + text3);
			return text3;
		}

		private void OnDeepLinkActivated(string url)
		{
			global::System.Uri uri = new global::System.Uri(url);
			if (!(uri.Scheme != m_Settings.DeepLinkUriScheme))
			{
				global::System.Collections.Generic.Dictionary<string, string> dictionary = global::Unity.Services.Authentication.PlayerAccounts.UriHelper.ParseQueryString(uri.Query.Trim());
				global::System.Collections.Generic.Dictionary<string, string> dictionary2 = global::Unity.Services.Authentication.PlayerAccounts.UriHelper.ParseQueryString(uri.Fragment.Trim());
				dictionary.TryGetValue("code", out var value);
				dictionary.TryGetValue("error", out var value2);
				if (string.IsNullOrEmpty(value))
				{
					dictionary2.TryGetValue("code", out value);
				}
				if (string.IsNullOrEmpty(value2))
				{
					dictionary2.TryGetValue("error", out value2);
				}
				if (!string.IsNullOrEmpty(value2))
				{
					throw global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsExceptionHandler.HandleError(value2);
				}
				OnAuthCodeReceived(value);
			}
		}

		private void OnAuthCodeReceived(string code)
		{
			SignInRequestAsync(code, CodeVerifier, RedirectUri);
		}

		private global::System.Threading.Tasks.Task SignInRequestAsync(string code, string codeVerifier, string redirectUri)
		{
			string signInRequestBody = "code=" + code + "&redirect_uri=" + global::System.Uri.EscapeDataString(redirectUri) + "&client_id=" + ClientId + "&code_verifier=" + codeVerifier + "&grant_type=authorization_code";
			return HandleSignInRequestAsync(() => m_NetworkingClient.PostAsync<global::Unity.Services.Authentication.PlayerAccounts.SignInResponse>("https://player-login.unity.com/v1/oauth2/token", signInRequestBody));
		}

		private async global::System.Threading.Tasks.Task HandleSignInRequestAsync(global::System.Func<global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.PlayerAccounts.SignInResponse>> signInRequest)
		{
			try
			{
				SignInState = global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState.SigningIn;
				CompleteSignIn(await signInRequest());
			}
			catch (global::Unity.Services.Core.RequestFailedException exception)
			{
				SendSignInFailedEvent(exception, forceSignOut: true);
				throw;
			}
			catch (global::Unity.Services.Authentication.PlayerAccounts.WebRequestException ex)
			{
				global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsErrorResponse playerAccountsErrorResponse = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsErrorResponse>(ex.Message);
				global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException ex2 = global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsExceptionHandler.HandleError(playerAccountsErrorResponse?.Error, playerAccountsErrorResponse?.Description, ex);
				global::Unity.Services.Authentication.PlayerAccounts.Logger.LogException(ex2);
				throw ex2;
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

		internal void CompleteSignIn(global::Unity.Services.Authentication.PlayerAccounts.SignInResponse signInResponse)
		{
			AccessToken = signInResponse?.AccessToken;
			IdToken = signInResponse?.IdToken;
			if (IdToken != null)
			{
				IdTokenClaims = m_JwtDecoder.Decode<global::Unity.Services.Authentication.PlayerAccounts.IdToken>(IdToken);
			}
			RefreshToken = signInResponse?.RefreshToken;
			SignInState = global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountState.Authorized;
			this.SignedIn?.Invoke();
		}
	}
}
