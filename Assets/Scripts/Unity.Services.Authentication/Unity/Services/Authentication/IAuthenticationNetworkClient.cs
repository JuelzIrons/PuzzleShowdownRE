namespace Unity.Services.Authentication
{
	internal interface IAuthenticationNetworkClient
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignInAnonymouslyAsync();

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignInWithSessionTokenAsync(string token);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignInWithExternalTokenAsync(string idProvider, global::Unity.Services.Authentication.SignInWithExternalTokenRequest externalToken);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.LinkResponse> LinkWithExternalTokenAsync(string idProvider, global::Unity.Services.Authentication.LinkWithExternalTokenRequest externalToken);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.UnlinkResponse> UnlinkExternalTokenAsync(string idProvider, global::Unity.Services.Authentication.UnlinkRequest request);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.PlayerInfoResponse> GetPlayerInfoAsync(string playerId);

		global::System.Threading.Tasks.Task DeleteAccountAsync(string playerId);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignInWithUsernamePasswordAsync(global::Unity.Services.Authentication.UsernamePasswordRequest credentials);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignUpWithUsernamePasswordAsync(global::Unity.Services.Authentication.UsernamePasswordRequest credentials);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> AddUsernamePasswordAsync(global::Unity.Services.Authentication.UsernamePasswordRequest credentials);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> UpdatePasswordAsync(global::Unity.Services.Authentication.UpdatePasswordRequest credentials);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.GenerateCodeResponse> GenerateSignInCodeAsync(global::Unity.Services.Authentication.GenerateSignInCodeRequest request);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.CodeLinkConfirmResponse> ConfirmCodeAsync(global::Unity.Services.Authentication.ConfirmSignInCodeRequest request);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.SignInResponse> SignInWithCodeAsync(global::Unity.Services.Authentication.SignInWithCodeRequest request);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.CodeLinkInfoResponse> GetCodeIdentifierAsync(global::Unity.Services.Authentication.CodeLinkInfoRequest request);

		global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.GetNotificationsResponse> GetNotificationsAsync(string playerId);
	}
}
