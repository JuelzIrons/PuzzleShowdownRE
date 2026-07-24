namespace Unity.Services.Authentication.PlayerAccounts
{
	public interface IPlayerAccountService
	{
		string AccessToken { get; }

		string IdToken { get; }

		string AccountPortalUrl { get; }

		global::Unity.Services.Authentication.PlayerAccounts.IdToken IdTokenClaims { get; }

		bool IsSignedIn { get; }

		event global::System.Action SignedIn;

		event global::System.Action SignedOut;

		event global::System.Action<global::Unity.Services.Core.RequestFailedException> SignInFailed;

		global::System.Threading.Tasks.Task StartSignInAsync(bool isSigningUp = false);

		global::System.Threading.Tasks.Task RefreshTokenAsync();

		void SignOut();
	}
}
