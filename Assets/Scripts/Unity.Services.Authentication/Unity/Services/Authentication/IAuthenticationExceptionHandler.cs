namespace Unity.Services.Authentication
{
	internal interface IAuthenticationExceptionHandler
	{
		global::Unity.Services.Core.RequestFailedException BuildClientInvalidStateException(global::Unity.Services.Authentication.AuthenticationState state);

		global::Unity.Services.Core.RequestFailedException BuildClientInvalidProfileException();

		global::Unity.Services.Core.RequestFailedException BuildClientUnlinkExternalIdNotFoundException();

		global::Unity.Services.Core.RequestFailedException BuildClientSessionTokenNotExistsException();

		global::Unity.Services.Core.RequestFailedException BuildUnknownException(string error);

		global::Unity.Services.Core.RequestFailedException BuildInvalidIdProviderNameException();

		global::Unity.Services.Core.RequestFailedException BuildInvalidPlayerNameException();

		global::Unity.Services.Core.RequestFailedException BuildInvalidCredentialsException();

		global::Unity.Services.Core.RequestFailedException ConvertException(global::Unity.Services.Authentication.WebRequestException exception);

		global::Unity.Services.Core.RequestFailedException ConvertException(global::Unity.Services.Authentication.Shared.ApiException exception);
	}
}
