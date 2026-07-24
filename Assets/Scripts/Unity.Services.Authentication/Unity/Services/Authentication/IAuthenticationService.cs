namespace Unity.Services.Authentication
{
	public interface IAuthenticationService
	{
		bool IsSignedIn { get; }

		bool IsAuthorized { get; }

		bool IsExpired { get; }

		string AccessToken { get; }

		string PlayerId { get; }

		string PlayerName { get; }

		string Profile { get; }

		bool SessionTokenExists { get; }

		string SessionToken { get; }

		string LastNotificationDate { get; }

		global::Unity.Services.Authentication.PlayerInfo PlayerInfo { get; }

		global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification> Notifications { get; }

		event global::System.Action SignedIn;

		event global::System.Action SignedOut;

		event global::System.Action Expired;

		event global::System.Action<global::Unity.Services.Authentication.SignInCodeInfo> SignInCodeReceived;

		event global::System.Action SignInCodeExpired;

		event global::System.Action<global::Unity.Services.Core.RequestFailedException> SignInFailed;

		event global::System.Action<string> PlayerNameChanged;

		event global::System.Action<string> PlayerIdChanged;

		event global::System.Action<global::Unity.Services.Authentication.PlayerInfo> PlayerInfoChanged;

		global::System.Threading.Tasks.Task SignInAnonymouslyAsync(global::Unity.Services.Authentication.SignInOptions options = null);

		global::System.Threading.Tasks.Task SignInWithAppleAsync(string idToken, global::Unity.Services.Authentication.SignInOptions options = null);

		global::System.Threading.Tasks.Task LinkWithAppleAsync(string idToken, global::Unity.Services.Authentication.LinkOptions options = null);

		global::System.Threading.Tasks.Task UnlinkAppleAsync();

		global::System.Threading.Tasks.Task SignInWithAppleGameCenterAsync(string signature, string teamPlayerId, string publicKeyURL, string salt, ulong timestamp, global::Unity.Services.Authentication.SignInOptions options = null);

		global::System.Threading.Tasks.Task LinkWithAppleGameCenterAsync(string signature, string teamPlayerId, string publicKeyURL, string salt, ulong timestamp, global::Unity.Services.Authentication.LinkOptions options = null);

		global::System.Threading.Tasks.Task UnlinkAppleGameCenterAsync();

		global::System.Threading.Tasks.Task SignInWithGoogleAsync(string idToken, global::Unity.Services.Authentication.SignInOptions options = null);

		global::System.Threading.Tasks.Task LinkWithGoogleAsync(string idToken, global::Unity.Services.Authentication.LinkOptions options = null);

		global::System.Threading.Tasks.Task UnlinkGoogleAsync();

		global::System.Threading.Tasks.Task SignInWithGooglePlayGamesAsync(string authCode, global::Unity.Services.Authentication.SignInOptions options = null);

		global::System.Threading.Tasks.Task LinkWithGooglePlayGamesAsync(string authCode, global::Unity.Services.Authentication.LinkOptions options = null);

		global::System.Threading.Tasks.Task UnlinkGooglePlayGamesAsync();

		global::System.Threading.Tasks.Task SignInWithFacebookAsync(string accessToken, global::Unity.Services.Authentication.SignInOptions options = null);

		global::System.Threading.Tasks.Task LinkWithFacebookAsync(string accessToken, global::Unity.Services.Authentication.LinkOptions options = null);

		global::System.Threading.Tasks.Task UnlinkFacebookAsync();

		global::System.Threading.Tasks.Task SignInWithSteamAsync(string sessionTicket, string identity, global::Unity.Services.Authentication.SignInOptions options = null);

		[global::System.Obsolete("This method is deprecated as of version 2.7.1. Please use the SignInWithSteamAsync method with the 'identity' parameter for better security.")]
		global::System.Threading.Tasks.Task SignInWithSteamAsync(string sessionTicket, global::Unity.Services.Authentication.SignInOptions options = null);

		global::System.Threading.Tasks.Task LinkWithSteamAsync(string sessionTicket, string identity, global::Unity.Services.Authentication.LinkOptions options = null);

		[global::System.Obsolete("This method is deprecated as of version 2.7.1. Please use the LinkWithSteamAsync method with the 'identity' parameter for better security.")]
		global::System.Threading.Tasks.Task LinkWithSteamAsync(string sessionTicket, global::Unity.Services.Authentication.LinkOptions options = null);

		global::System.Threading.Tasks.Task SignInWithSteamAsync(string sessionTicket, string identity, string appId, global::Unity.Services.Authentication.SignInOptions options = null);

		global::System.Threading.Tasks.Task LinkWithSteamAsync(string sessionTicket, string identity, string appId, global::Unity.Services.Authentication.LinkOptions options = null);

		global::System.Threading.Tasks.Task UnlinkSteamAsync();

		global::System.Threading.Tasks.Task SignInWithOculusAsync(string nonce, string userId, global::Unity.Services.Authentication.SignInOptions options = null);

		global::System.Threading.Tasks.Task LinkWithOculusAsync(string nonce, string userId, global::Unity.Services.Authentication.LinkOptions options = null);

		global::System.Threading.Tasks.Task UnlinkOculusAsync();

		global::System.Threading.Tasks.Task SignInWithOpenIdConnectAsync(string idProviderName, string idToken, global::Unity.Services.Authentication.SignInOptions options = null);

		global::System.Threading.Tasks.Task LinkWithOpenIdConnectAsync(string idProviderName, string idToken, global::Unity.Services.Authentication.LinkOptions options = null);

		global::System.Threading.Tasks.Task UnlinkOpenIdConnectAsync(string idProviderName);

		global::System.Threading.Tasks.Task SignInWithUnityAsync(string token, global::Unity.Services.Authentication.SignInOptions options = null);

		global::System.Threading.Tasks.Task LinkWithUnityAsync(string token, global::Unity.Services.Authentication.LinkOptions options = null);

		global::System.Threading.Tasks.Task UnlinkUnityAsync();

		global::System.Threading.Tasks.Task SignInWithUsernamePasswordAsync(string username, string password);

		global::System.Threading.Tasks.Task SignUpWithUsernamePasswordAsync(string username, string password);

		global::System.Threading.Tasks.Task AddUsernamePasswordAsync(string username, string password);

		global::System.Threading.Tasks.Task UpdatePasswordAsync(string currentPassword, string newPassword);

		global::System.Threading.Tasks.Task DeleteAccountAsync();

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.PlayerInfo> GetPlayerInfoAsync();

		global::System.Threading.Tasks.Task<string> GetPlayerNameAsync(bool autoGenerate = true);

		global::System.Threading.Tasks.Task<string> UpdatePlayerNameAsync(string name);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInCodeInfo> GenerateSignInCodeAsync(string identifier = null);

		global::System.Threading.Tasks.Task SignInWithCodeAsync(bool usePolling = false, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken));

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInCodeInfo> GetSignInCodeInfoAsync(string code);

		global::System.Threading.Tasks.Task ConfirmCodeAsync(string code, string idProvider = null, string externalToken = null);

		void ProcessAuthenticationTokens(string accessToken, string sessionToken = null);

		void SignOut(bool clearCredentials = false);

		void SwitchProfile(string profile);

		void ClearSessionToken();

		global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification>> GetNotificationsAsync();
	}
}
