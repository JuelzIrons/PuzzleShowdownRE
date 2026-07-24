namespace Unity.Services.Relay
{
	public class RelayServiceException : global::Unity.Services.Core.RequestFailedException
	{
		public global::Unity.Services.Relay.RelayExceptionReason Reason { get; private set; }

		public global::Unity.Services.Relay.Models.ErrorResponseBody ApiError => (base.InnerException as global::Unity.Services.Relay.Http.HttpException<global::Unity.Services.Relay.Models.ErrorResponseBody>)?.ActualError;

		public RelayServiceException(global::Unity.Services.Relay.RelayExceptionReason reason, string message, global::System.Exception innerException)
			: base((int)reason, message, innerException)
		{
			Reason = reason;
		}

		public RelayServiceException(global::Unity.Services.Relay.RelayExceptionReason reason, string message)
			: base((int)reason, message)
		{
			Reason = reason;
		}

		public RelayServiceException(long errorCode, string message)
			: base((int)errorCode, message)
		{
			if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Relay.RelayExceptionReason), errorCode))
			{
				Reason = (global::Unity.Services.Relay.RelayExceptionReason)errorCode;
			}
			else
			{
				Reason = global::Unity.Services.Relay.RelayExceptionReason.Unknown;
			}
		}

		public RelayServiceException(global::System.Exception innerException)
			: base(15999, "Unknown Relay Service Exception", innerException)
		{
		}
	}
}
