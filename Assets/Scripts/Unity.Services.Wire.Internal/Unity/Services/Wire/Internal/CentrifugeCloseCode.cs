namespace Unity.Services.Wire.Internal
{
	public enum CentrifugeCloseCode
	{
		WebsocketNotSet = 0,
		WebsocketNormal = 1000,
		WebsocketAway = 1001,
		WebsocketProtocolError = 1002,
		WebsocketUnsupportedData = 1003,
		WebsocketUndefined = 1004,
		WebsocketNoStatus = 1005,
		WebsocketAbnormal = 1006,
		WebsocketInvalidData = 1007,
		WebsocketPolicyViolation = 1008,
		WebsocketTooBig = 1009,
		WebsocketMandatoryExtension = 1010,
		WebsocketServerError = 1011,
		WebsocketTlsHandshakeFailure = 1015,
		Normal = 3000,
		Shutdown = 3001,
		InvalidToken = 3002,
		BadRequest = 3003,
		InternalServerError = 3004,
		Expired = 3005,
		SubscriptionExpired = 3006,
		Stale = 3007,
		Slow = 3008,
		WriteError = 3009,
		InsufficientState = 3010,
		ForceReconnect = 3011,
		ForceNoReconnect = 3012,
		ConnectionLimit = 3013,
		ChannelLimit = 3014
	}
}
