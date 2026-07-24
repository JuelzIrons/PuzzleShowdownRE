namespace Unity.Services.Wire.Protocol.Internal
{
	internal enum CentrifugeErrorCode
	{
		ErrorInternal = 100,
		ErrorUnauthorized = 101,
		ErrorUnknownChannel = 102,
		ErrorPermissionDenied = 103,
		ErrorMethodNotFound = 104,
		ErrorAlreadySubscribed = 105,
		ErrorLimitExceeded = 106,
		ErrorBadRequest = 107,
		ErrorNotAvailable = 108,
		ErrorTokenExpired = 109,
		ErrorExpired = 110,
		ErrorTooManyRequests = 111,
		ErrorUnrecoverablePosition = 112
	}
}
