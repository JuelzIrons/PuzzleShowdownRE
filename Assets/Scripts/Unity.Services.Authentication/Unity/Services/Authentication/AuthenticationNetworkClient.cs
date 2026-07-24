namespace Unity.Services.Authentication
{
	internal class AuthenticationNetworkClient : global::Unity.Services.Authentication.IAuthenticationNetworkClient
	{
		private const string k_PlayerIdReplacement = "{PlayerId}";

		private const string k_AnonymousUrlStem = "/v1/authentication/anonymous";

		private const string k_SessionTokenUrlStem = "/v1/authentication/session-token";

		private const string k_ExternalTokenUrlStem = "/v1/authentication/external-token";

		private const string k_LinkExternalTokenUrlStem = "/v1/authentication/link";

		private const string k_UnlinkExternalTokenUrlStem = "/v1/authentication/unlink";

		private const string k_UsersUrlStem = "/v1/users";

		private const string k_UsernamePasswordSignInUrlStem = "/v1/authentication/usernamepassword/sign-in";

		private const string k_UsernamePasswordSignUpUrlStem = "/v1/authentication/usernamepassword/sign-up";

		private const string k_UpdatePasswordUrlStem = "/v1/authentication/usernamepassword/update-password";

		private const string k_GenerateSignInCodeUrlStem = "/v1/authentication/code-link/generate";

		private const string k_ConfirmSignInCodeUrlStem = "/v1/authentication/code-link/confirm";

		private const string k_GetCodeIdentifierUrlStem = "/v1/authentication/code-link/info";

		private const string k_CodeSignInUrlStem = "/v1/authentication/code-link/sign-in";

		private const string k_GetNotificationsStem = "/v1/users/{PlayerId}/notifications";

		private readonly string m_AnonymousUrl;

		private readonly string m_SessionTokenUrl;

		private readonly string m_ExternalTokenUrl;

		private readonly string m_LinkExternalTokenUrl;

		private readonly string m_UnlinkExternalTokenUrl;

		private readonly string m_UsersUrl;

		private readonly string m_UsernamePasswordSignInUrl;

		private readonly string m_UsernamePasswordSignUpUrl;

		private readonly string m_UpdatePasswordUrl;

		private readonly string m_GenerateSignInCodeUrl;

		private readonly string m_ConfirmSignInCodeUrl;

		private readonly string m_CodeSignInUrl;

		private readonly string m_GetCodeIdentifierUrl;

		private readonly string m_GetNotificationsUrl;

		private readonly global::System.Collections.Generic.Dictionary<string, string> m_CommonHeaders;

		internal global::Unity.Services.Authentication.AccessTokenComponent AccessTokenComponent { get; }

		internal global::Unity.Services.Core.Configuration.Internal.ICloudProjectId CloudProjectIdComponent { get; }

		internal global::Unity.Services.Core.Environments.Internal.IEnvironments EnvironmentComponent { get; }

		internal global::Unity.Services.Authentication.INetworkHandler NetworkHandler { get; }

		private string AccessToken => AccessTokenComponent.AccessToken;

		private string EnvironmentName => EnvironmentComponent.Current;

		internal AuthenticationNetworkClient(string host, global::Unity.Services.Core.Configuration.Internal.ICloudProjectId cloudProjectId, global::Unity.Services.Core.Environments.Internal.IEnvironments environment, global::Unity.Services.Authentication.INetworkHandler networkHandler, global::Unity.Services.Authentication.AccessTokenComponent accessToken)
		{
			AccessTokenComponent = accessToken;
			CloudProjectIdComponent = cloudProjectId;
			EnvironmentComponent = environment;
			NetworkHandler = networkHandler;
			m_AnonymousUrl = host + "/v1/authentication/anonymous";
			m_SessionTokenUrl = host + "/v1/authentication/session-token";
			m_ExternalTokenUrl = host + "/v1/authentication/external-token";
			m_LinkExternalTokenUrl = host + "/v1/authentication/link";
			m_UnlinkExternalTokenUrl = host + "/v1/authentication/unlink";
			m_UsersUrl = host + "/v1/users";
			m_UsernamePasswordSignInUrl = host + "/v1/authentication/usernamepassword/sign-in";
			m_UsernamePasswordSignUpUrl = host + "/v1/authentication/usernamepassword/sign-up";
			m_UpdatePasswordUrl = host + "/v1/authentication/usernamepassword/update-password";
			m_GenerateSignInCodeUrl = host + "/v1/authentication/code-link/generate";
			m_ConfirmSignInCodeUrl = host + "/v1/authentication/code-link/confirm";
			m_GetCodeIdentifierUrl = host + "/v1/authentication/code-link/info";
			m_CodeSignInUrl = host + "/v1/authentication/code-link/sign-in";
			m_GetNotificationsUrl = host + "/v1/users/{PlayerId}/notifications";
			m_CommonHeaders = new global::System.Collections.Generic.Dictionary<string, string>
			{
				["ProjectId"] = CloudProjectIdComponent.GetCloudProjectId(),
				["Error-Version"] = "v1"
			};
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignInAnonymouslyAsync()
		{
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.SignInResponse>(m_AnonymousUrl, WithEnvironmentAndRelease(GetCommonHeaders()));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignInWithSessionTokenAsync(string token)
		{
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.SignInResponse>(m_SessionTokenUrl, new global::Unity.Services.Authentication.SessionTokenRequest
			{
				SessionToken = token
			}, WithEnvironmentAndRelease(GetCommonHeaders()));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignInWithExternalTokenAsync(string idProvider, global::Unity.Services.Authentication.SignInWithExternalTokenRequest externalToken)
		{
			string url = m_ExternalTokenUrl + "/" + idProvider;
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.SignInResponse>(url, externalToken, WithEnvironmentAndRelease(GetCommonHeaders()));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.LinkResponse> LinkWithExternalTokenAsync(string idProvider, global::Unity.Services.Authentication.LinkWithExternalTokenRequest externalToken)
		{
			string url = m_LinkExternalTokenUrl + "/" + idProvider;
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.LinkResponse>(url, externalToken, WithEnvironmentAndRelease(WithAccessToken(GetCommonHeaders())));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.UnlinkResponse> UnlinkExternalTokenAsync(string idProvider, global::Unity.Services.Authentication.UnlinkRequest request)
		{
			string url = m_UnlinkExternalTokenUrl + "/" + idProvider;
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.UnlinkResponse>(url, request, WithEnvironmentAndRelease(WithAccessToken(GetCommonHeaders())));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.PlayerInfoResponse> GetPlayerInfoAsync(string playerId)
		{
			return NetworkHandler.GetAsync<global::Unity.Services.Authentication.PlayerInfoResponse>(CreateUserRequestUrl(playerId), WithAccessToken(GetCommonHeaders()));
		}

		public global::System.Threading.Tasks.Task DeleteAccountAsync(string playerId)
		{
			return NetworkHandler.DeleteAsync(CreateUserRequestUrl(playerId), WithEnvironmentAndRelease(WithAccessToken(GetCommonHeaders())));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignInWithUsernamePasswordAsync(global::Unity.Services.Authentication.UsernamePasswordRequest credentials)
		{
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.SignInResponse>(m_UsernamePasswordSignInUrl, credentials, WithEnvironmentAndRelease(GetCommonHeaders()));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignUpWithUsernamePasswordAsync(global::Unity.Services.Authentication.UsernamePasswordRequest credentials)
		{
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.SignInResponse>(m_UsernamePasswordSignUpUrl, credentials, WithEnvironmentAndRelease(GetCommonHeaders()));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> AddUsernamePasswordAsync(global::Unity.Services.Authentication.UsernamePasswordRequest credentials)
		{
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.SignInResponse>(m_UsernamePasswordSignUpUrl, credentials, WithEnvironmentAndRelease(WithAccessToken(GetCommonHeaders())));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> UpdatePasswordAsync(global::Unity.Services.Authentication.UpdatePasswordRequest credentials)
		{
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.SignInResponse>(m_UpdatePasswordUrl, credentials, WithEnvironmentAndRelease(WithAccessToken(GetCommonHeaders())));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.GenerateCodeResponse> GenerateSignInCodeAsync(global::Unity.Services.Authentication.GenerateSignInCodeRequest request)
		{
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.GenerateCodeResponse>(m_GenerateSignInCodeUrl, request, WithEnvironmentAndRelease(GetCommonHeaders()));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.CodeLinkConfirmResponse> ConfirmCodeAsync(global::Unity.Services.Authentication.ConfirmSignInCodeRequest request)
		{
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.CodeLinkConfirmResponse>(m_ConfirmSignInCodeUrl, request, WithEnvironmentAndRelease(WithAccessToken(GetCommonHeaders())));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignInWithCodeAsync(global::Unity.Services.Authentication.SignInWithCodeRequest request)
		{
			string url = m_CodeSignInUrl + "/" + request.CodeLinkSessionId;
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.SignInResponse>(url, request, WithEnvironmentAndRelease(GetCommonHeaders()));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.CodeLinkInfoResponse> GetCodeIdentifierAsync(global::Unity.Services.Authentication.CodeLinkInfoRequest request)
		{
			return NetworkHandler.PostAsync<global::Unity.Services.Authentication.CodeLinkInfoResponse>(m_GetCodeIdentifierUrl, request, WithEnvironmentAndRelease(WithAccessToken(GetCommonHeaders())));
		}

		public global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.GetNotificationsResponse> GetNotificationsAsync(string playerId)
		{
			return NetworkHandler.GetAsync<global::Unity.Services.Authentication.GetNotificationsResponse>(m_GetNotificationsUrl.Replace("{PlayerId}", playerId), WithEnvironmentAndRelease(WithAccessToken(GetCommonHeaders())));
		}

		private string CreateUserRequestUrl(string user)
		{
			return m_UsersUrl + "/" + user;
		}

		private global::System.Collections.Generic.Dictionary<string, string> WithAccessToken(global::System.Collections.Generic.Dictionary<string, string> headers)
		{
			headers["Authorization"] = "Bearer " + AccessToken;
			return headers;
		}

		private global::System.Collections.Generic.Dictionary<string, string> WithEnvironmentAndRelease(global::System.Collections.Generic.Dictionary<string, string> headers)
		{
			string environmentName = EnvironmentName;
			if (!string.IsNullOrEmpty(environmentName))
			{
				headers["UnityEnvironment"] = environmentName;
			}
			return headers;
		}

		private global::System.Collections.Generic.Dictionary<string, string> GetCommonHeaders()
		{
			return new global::System.Collections.Generic.Dictionary<string, string>(m_CommonHeaders);
		}
	}
}
