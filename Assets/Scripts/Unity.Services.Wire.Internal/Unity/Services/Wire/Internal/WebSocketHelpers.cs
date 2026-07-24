namespace Unity.Services.Wire.Internal
{
	internal static class WebSocketHelpers
	{
		public enum WebSocketErrorCode
		{
			InstanceNotFound = -1,
			AlreadyConnecting = -2,
			NotConnected = -3,
			AlreadyClosing = -4,
			AlreadyClosed = -5,
			NotOpen = -6,
			InvalidCloseParameters = -7
		}

		public static global::Unity.Services.Wire.Internal.WebSocketCloseCode ParseCloseCodeEnum(int closeCode)
		{
			if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Wire.Internal.WebSocketCloseCode), closeCode))
			{
				return (global::Unity.Services.Wire.Internal.WebSocketCloseCode)closeCode;
			}
			return global::Unity.Services.Wire.Internal.WebSocketCloseCode.Undefined;
		}

		public static global::Unity.Services.Wire.Internal.WebSocketException GetErrorMessageFromCode(global::Unity.Services.Wire.Internal.WebSocketHelpers.WebSocketErrorCode errorCode, global::System.Exception inner)
		{
			return errorCode switch
			{
				global::Unity.Services.Wire.Internal.WebSocketHelpers.WebSocketErrorCode.InstanceNotFound => new global::Unity.Services.Wire.Internal.WebSocketUnexpectedException("WebSocket instance not found.", inner), 
				global::Unity.Services.Wire.Internal.WebSocketHelpers.WebSocketErrorCode.AlreadyConnecting => new global::Unity.Services.Wire.Internal.WebSocketInvalidStateException("WebSocket is already connected or in connecting state.", inner), 
				global::Unity.Services.Wire.Internal.WebSocketHelpers.WebSocketErrorCode.NotConnected => new global::Unity.Services.Wire.Internal.WebSocketInvalidStateException("WebSocket is not connected.", inner), 
				global::Unity.Services.Wire.Internal.WebSocketHelpers.WebSocketErrorCode.AlreadyClosing => new global::Unity.Services.Wire.Internal.WebSocketInvalidStateException("WebSocket is already closing.", inner), 
				global::Unity.Services.Wire.Internal.WebSocketHelpers.WebSocketErrorCode.AlreadyClosed => new global::Unity.Services.Wire.Internal.WebSocketInvalidStateException("WebSocket is already closed.", inner), 
				global::Unity.Services.Wire.Internal.WebSocketHelpers.WebSocketErrorCode.NotOpen => new global::Unity.Services.Wire.Internal.WebSocketInvalidStateException("WebSocket is not in open state.", inner), 
				global::Unity.Services.Wire.Internal.WebSocketHelpers.WebSocketErrorCode.InvalidCloseParameters => new global::Unity.Services.Wire.Internal.WebSocketInvalidArgumentException("Cannot close WebSocket. An invalid code was specified or reason is too long.", inner), 
				_ => new global::Unity.Services.Wire.Internal.WebSocketUnexpectedException("Unknown error.", inner), 
			};
		}
	}
}
