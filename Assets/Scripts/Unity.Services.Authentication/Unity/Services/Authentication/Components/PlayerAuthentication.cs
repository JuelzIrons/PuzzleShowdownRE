namespace Unity.Services.Authentication.Components
{
	[global::UnityEngine.AddComponentMenu("Services/Player Authentication")]
	public class PlayerAuthentication : global::Unity.Services.Core.Components.ServicesBehaviour
	{
		[global::UnityEngine.Header("On Initialization")]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Option to set a custom profile to scope persisted credentials and get different players.")]
		public bool SetCustomProfile;

		[global::UnityEngine.SerializeField]
		[global::Unity.Services.Core.Internal.Visibility("SetCustomProfile", true)]
		[global::UnityEngine.Tooltip("The profile is a local scope for persisted player credentials that you can use to get different players.")]
		public string Profile;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Option to sign in anonymously automatically after services initialization.")]
		public bool SignInAnonymously;

		[global::UnityEngine.Header("On Sign In")]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Fetches the player info upon sign in. This provides the player creation time, username, etc.")]
		public bool FetchPlayerInfo;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Fetches the player name upon sign in.")]
		public bool FetchPlayerName;

		[global::UnityEngine.SerializeField]
		[global::Unity.Services.Core.Internal.Visibility("FetchPlayerName", true)]
		[global::UnityEngine.Tooltip("Pass in the option to autogenerate the name if none exist.")]
		public bool GenerateName;

		[global::UnityEngine.Header("Events")]
		[global::UnityEngine.SerializeField]
		public global::Unity.Services.Authentication.Components.PlayerAuthenticationEvents Events = new global::Unity.Services.Authentication.Components.PlayerAuthenticationEvents();

		internal bool IsSetupDone;

		internal bool IsInfoFetched;

		internal bool IsNameFetched;

		public global::Unity.Services.Authentication.IAuthenticationService AuthenticationService { get; internal set; }

		internal PlayerAuthentication()
		{
		}

		protected override void OnServicesReady()
		{
		}

		protected override async void OnServicesInitialized()
		{
			if (AuthenticationService == null)
			{
				try
				{
					SetAuthenticationService();
					await SetupAsync();
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogError(ex.ToString());
				}
			}
		}

		protected override void Cleanup()
		{
			if (AuthenticationService != null)
			{
				AuthenticationService.SignInFailed -= OnSignInFailed;
				AuthenticationService.SignedOut -= OnSignedOut;
				AuthenticationService.Expired -= OnExpired;
				AuthenticationService.SignInCodeReceived -= OnSignInCodeReceived;
				AuthenticationService.SignInCodeExpired -= OnSignInCodeExpired;
			}
		}

		internal virtual void SetAuthenticationService()
		{
			AuthenticationService = global::Unity.Services.Core.UnityServicesExtensions.GetAuthenticationService(base.Services);
		}

		internal async global::System.Threading.Tasks.Task SetupAsync()
		{
			AuthenticationService.SignedIn -= OnSignedIn;
			AuthenticationService.SignedIn += OnSignedIn;
			AuthenticationService.SignInFailed -= OnSignInFailed;
			AuthenticationService.SignInFailed += OnSignInFailed;
			AuthenticationService.SignedOut -= OnSignedOut;
			AuthenticationService.SignedOut += OnSignedOut;
			AuthenticationService.Expired -= OnExpired;
			AuthenticationService.Expired += OnExpired;
			AuthenticationService.SignInCodeReceived -= OnSignInCodeReceived;
			AuthenticationService.SignInCodeReceived += OnSignInCodeReceived;
			AuthenticationService.SignInCodeExpired -= OnSignInCodeExpired;
			AuthenticationService.SignInCodeExpired += OnSignInCodeExpired;
			if (!AuthenticationService.IsSignedIn)
			{
				if (SetCustomProfile)
				{
					AuthenticationService.SwitchProfile(Profile);
				}
				if (SignInAnonymously)
				{
					await SignInAnonymouslyAsync();
				}
			}
			IsSetupDone = true;
		}

		private async global::System.Threading.Tasks.Task SignInAnonymouslyAsync()
		{
			try
			{
				await AuthenticationService.SignInAnonymouslyAsync();
			}
			catch (global::System.Exception arg)
			{
				global::UnityEngine.Debug.LogError($"Authentication Failed!\n{arg}");
			}
		}

		private async global::System.Threading.Tasks.Task FetchPlayerInfoAsync()
		{
			try
			{
				await AuthenticationService.GetPlayerInfoAsync();
				IsInfoFetched = true;
			}
			catch (global::System.Exception arg)
			{
				global::UnityEngine.Debug.LogError($"Fetch Player Info Failed!\n{arg}");
			}
		}

		private async global::System.Threading.Tasks.Task FetchPlayerNameAsync()
		{
			try
			{
				await AuthenticationService.GetPlayerNameAsync(GenerateName);
				IsNameFetched = true;
			}
			catch (global::System.Exception arg)
			{
				global::UnityEngine.Debug.LogError($"Fetch Player Name Failed!\n{arg}");
			}
		}

		private async void OnSignedIn()
		{
			if (FetchPlayerInfo && !IsInfoFetched)
			{
				await FetchPlayerInfoAsync();
			}
			if (FetchPlayerName && !IsNameFetched)
			{
				await FetchPlayerNameAsync();
			}
			Events?.SignedIn?.Invoke();
		}

		private void OnSignInFailed(global::System.Exception exception)
		{
			Events?.SignInFailed?.Invoke(exception);
		}

		private void OnSignedOut()
		{
			ResetAutomation();
			Events?.SignedOut?.Invoke();
		}

		private void OnExpired()
		{
			Events?.Expired?.Invoke();
		}

		private void OnSignInCodeReceived(global::Unity.Services.Authentication.SignInCodeInfo info)
		{
			Events?.SignInCodeReceived?.Invoke(info);
		}

		private void OnSignInCodeExpired()
		{
			Events?.SignInCodeExpired?.Invoke();
		}

		private void ResetAutomation()
		{
			IsInfoFetched = false;
			IsNameFetched = false;
		}
	}
}
