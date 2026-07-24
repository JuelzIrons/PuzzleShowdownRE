namespace Unity.Services.Authentication
{
	internal class AuthenticationExceptionHandler : global::Unity.Services.Authentication.IAuthenticationExceptionHandler
	{
		private global::Unity.Services.Authentication.IAuthenticationMetrics Metrics { get; }

		public AuthenticationExceptionHandler(global::Unity.Services.Authentication.IAuthenticationMetrics metrics)
		{
			Metrics = metrics;
		}

		public global::Unity.Services.Core.RequestFailedException BuildClientInvalidStateException(global::Unity.Services.Authentication.AuthenticationState state)
		{
			string message = string.Empty;
			switch (state)
			{
			case global::Unity.Services.Authentication.AuthenticationState.SignedOut:
				message = "Invalid state for this operation. The player is signed out.";
				break;
			case global::Unity.Services.Authentication.AuthenticationState.SigningIn:
				message = "Invalid state for this operation. The player is already signing in.";
				break;
			case global::Unity.Services.Authentication.AuthenticationState.Authorized:
			case global::Unity.Services.Authentication.AuthenticationState.Refreshing:
				message = "Invalid state for this operation. The player is already signed in.";
				break;
			case global::Unity.Services.Authentication.AuthenticationState.Expired:
				message = "Invalid state for this operation. The player session has expired.";
				break;
			}
			Metrics.SendClientInvalidStateExceptionMetric();
			return global::Unity.Services.Authentication.AuthenticationException.Create(global::Unity.Services.Authentication.AuthenticationErrorCodes.ClientInvalidUserState, message);
		}

		public global::Unity.Services.Core.RequestFailedException BuildClientInvalidProfileException()
		{
			return global::Unity.Services.Authentication.AuthenticationException.Create(global::Unity.Services.Authentication.AuthenticationErrorCodes.ClientInvalidProfile, "Invalid profile name. The profile may only contain alphanumeric values, '-', '_', and must be no longer than 30 characters.");
		}

		public global::Unity.Services.Core.RequestFailedException BuildClientUnlinkExternalIdNotFoundException()
		{
			Metrics.SendUnlinkExternalIdNotFoundExceptionMetric();
			return global::Unity.Services.Authentication.AuthenticationException.Create(global::Unity.Services.Authentication.AuthenticationErrorCodes.ClientUnlinkExternalIdNotFound, "No external id was found to unlink from the provider. Use GetPlayerInfoAsync to load the linked external ids.");
		}

		public global::Unity.Services.Core.RequestFailedException BuildClientSessionTokenNotExistsException()
		{
			Metrics.SendClientSessionTokenNotExistsExceptionMetric();
			return global::Unity.Services.Authentication.AuthenticationException.Create(global::Unity.Services.Authentication.AuthenticationErrorCodes.ClientNoActiveSession, "There is no cached session token.");
		}

		public global::Unity.Services.Core.RequestFailedException BuildUnknownException(string error)
		{
			return global::Unity.Services.Authentication.AuthenticationException.Create(0, error);
		}

		public global::Unity.Services.Core.RequestFailedException BuildInvalidIdProviderNameException()
		{
			return global::Unity.Services.Authentication.AuthenticationException.Create(global::Unity.Services.Authentication.AuthenticationErrorCodes.InvalidParameters, "Invalid IdProviderName. The Id Provider name should start with 'oidc-' and have between 6 and 20 characters (including 'oidc-')");
		}

		public global::Unity.Services.Core.RequestFailedException BuildInvalidPlayerNameException()
		{
			return global::Unity.Services.Authentication.AuthenticationException.Create(global::Unity.Services.Authentication.AuthenticationErrorCodes.InvalidParameters, "Invalid Player Name. Player names cannot be empty or contain spaces.");
		}

		public global::Unity.Services.Core.RequestFailedException BuildInvalidCredentialsException()
		{
			return global::Unity.Services.Authentication.AuthenticationException.Create(global::Unity.Services.Authentication.AuthenticationErrorCodes.InvalidParameters, "Username and/or Password are not in the correct format");
		}

		public global::Unity.Services.Core.RequestFailedException ConvertException(global::Unity.Services.Authentication.WebRequestException exception)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			string value = $"Request failed: {exception.ResponseCode}, {exception.Message}";
			stringBuilder.Append(value);
			if (exception.ResponseHeaders != null && exception.ResponseHeaders.TryGetValue("x-request-id", out var value2))
			{
				stringBuilder.Append(", request-id: " + value2);
			}
			global::Unity.Services.Authentication.Logger.Log(stringBuilder.ToString());
			if (exception.NetworkError)
			{
				Metrics.SendNetworkErrorMetric();
				return global::Unity.Services.Authentication.AuthenticationException.Create(1, "Network Error: " + exception.Message, exception);
			}
			try
			{
				global::Unity.Services.Authentication.AuthenticationErrorResponse authenticationErrorResponse = global::Unity.Services.Authentication.IsolatedJsonConvert.DeserializeObject<global::Unity.Services.Authentication.AuthenticationErrorResponse>(exception.Message, global::Unity.Services.Authentication.SerializerSettings.DefaultSerializerSettings);
				return global::Unity.Services.Authentication.AuthenticationException.Create(MapErrorCodes(authenticationErrorResponse.Title), notifications: ParseNotifications(authenticationErrorResponse.Details), message: authenticationErrorResponse.Detail, innerException: exception);
			}
			catch (global::Newtonsoft.Json.JsonException innerException)
			{
				return global::Unity.Services.Authentication.AuthenticationException.Create(0, "Failed to deserialize server response.", innerException);
			}
			catch (global::System.Exception)
			{
				return global::Unity.Services.Authentication.AuthenticationException.Create(0, "Unknown error deserializing server response. ", exception);
			}
		}

		public global::Unity.Services.Core.RequestFailedException ConvertException(global::Unity.Services.Authentication.Shared.ApiException exception)
		{
			return exception?.Type switch
			{
				global::Unity.Services.Authentication.Shared.ApiExceptionType.InvalidParameters => global::Unity.Services.Authentication.AuthenticationException.Create(global::Unity.Services.Authentication.AuthenticationErrorCodes.InvalidParameters, exception.Message), 
				global::Unity.Services.Authentication.Shared.ApiExceptionType.Deserialization => global::Unity.Services.Authentication.AuthenticationException.Create(0, exception.Message), 
				global::Unity.Services.Authentication.Shared.ApiExceptionType.Network => CreateNetworkException(exception), 
				global::Unity.Services.Authentication.Shared.ApiExceptionType.Http => CreateHttpException(exception), 
				_ => CreateUnknownException(exception), 
			};
		}

		private static global::Unity.Services.Core.RequestFailedException CreateNetworkException(global::Unity.Services.Authentication.Shared.ApiException exception)
		{
			return exception?.Response?.StatusCode switch
			{
				503 => global::Unity.Services.Authentication.AuthenticationException.Create(3, exception.Message), 
				504 => global::Unity.Services.Authentication.AuthenticationException.Create(2, exception.Message), 
				_ => global::Unity.Services.Authentication.AuthenticationException.Create(1, exception.Message), 
			};
		}

		private static global::Unity.Services.Core.RequestFailedException CreateHttpException(global::Unity.Services.Authentication.Shared.ApiException exception)
		{
			return exception?.Response?.StatusCode switch
			{
				400 => global::Unity.Services.Authentication.AuthenticationException.Create(55, exception.Message), 
				401 => global::Unity.Services.Authentication.AuthenticationException.Create(51, exception.Message), 
				403 => global::Unity.Services.Authentication.AuthenticationException.Create(53, exception.Message), 
				404 => global::Unity.Services.Authentication.AuthenticationException.Create(54, exception.Message), 
				408 => global::Unity.Services.Authentication.AuthenticationException.Create(2, exception.Message), 
				409 => global::Unity.Services.Authentication.AuthenticationException.Create(58, exception.Message), 
				429 => global::Unity.Services.Authentication.AuthenticationException.Create(50, exception.Message), 
				_ => global::Unity.Services.Authentication.AuthenticationException.Create(55, exception.Message), 
			};
		}

		private static global::Unity.Services.Core.RequestFailedException CreateUnknownException(global::System.Exception exception)
		{
			return global::Unity.Services.Authentication.AuthenticationException.Create(0, "Unknown Error: " + exception.Message);
		}

		private int MapErrorCodes(string serverErrorTitle)
		{
			switch (serverErrorTitle)
			{
			case "BANNED_USER":
			case "PERMANENTLY_BANNED_USER":
				return global::Unity.Services.Authentication.AuthenticationErrorCodes.BannedUser;
			case "ENTITY_EXISTS":
				return global::Unity.Services.Authentication.AuthenticationErrorCodes.AccountAlreadyLinked;
			case "LINKED_ACCOUNT_LIMIT_EXCEEDED":
				return global::Unity.Services.Authentication.AuthenticationErrorCodes.AccountLinkLimitExceeded;
			case "INVALID_PARAMETERS":
				return global::Unity.Services.Authentication.AuthenticationErrorCodes.InvalidParameters;
			case "INVALID_SESSION_TOKEN":
				return global::Unity.Services.Authentication.AuthenticationErrorCodes.InvalidSessionToken;
			case "PERMISSION_DENIED":
				return global::Unity.Services.Authentication.AuthenticationErrorCodes.InvalidParameters;
			case "UNAUTHORIZED_REQUEST":
				return 51;
			default:
				return 0;
			}
		}

		private static global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification> ParseNotifications(global::System.Collections.Generic.List<object> details)
		{
			if (details != null && details.Count > 0)
			{
				foreach (object detail in details)
				{
					try
					{
						return global::Unity.Services.Authentication.IsolatedJsonConvert.DeserializeObject<global::Unity.Services.Authentication.GetNotificationsResponse>((detail as global::Newtonsoft.Json.Linq.JObject)?.ToString()).ToNotificationList();
					}
					catch
					{
					}
				}
			}
			return null;
		}
	}
}
