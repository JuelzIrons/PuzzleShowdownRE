namespace Unity.Services.DistributedAuthority.Exceptions
{
	internal class DistributedAuthorityServiceException : global::Unity.Services.Core.RequestFailedException
	{
		public global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason Reason { get; private set; }

		public global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody ApiError
		{
			get
			{
				global::Unity.Services.DistributedAuthority.Http.HttpException<global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody> ex = base.InnerException as global::Unity.Services.DistributedAuthority.Http.HttpException<global::Unity.Services.DistributedAuthority.Models.ErrorResponseBody>;
				if (ex?.ActualError == null)
				{
					return null;
				}
				return ex.ActualError;
			}
		}

		public DistributedAuthorityServiceException(global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason reason, string message, global::System.Exception innerException)
			: base((int)reason, message, innerException)
		{
			Reason = reason;
		}

		public DistributedAuthorityServiceException(global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason reason, string message)
			: base((int)reason, message)
		{
			Reason = reason;
		}

		public DistributedAuthorityServiceException(long errorCode, string message)
			: base((int)errorCode, message)
		{
			if (global::System.Enum.IsDefined(typeof(global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason), errorCode))
			{
				Reason = (global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason)errorCode;
			}
			else
			{
				Reason = global::Unity.Services.DistributedAuthority.DistributedAuthorityExceptionReason.Unknown;
			}
		}

		public DistributedAuthorityServiceException(global::System.Exception innerException)
			: base(45999, "Unknown Distributed Authority Service Exception", innerException)
		{
		}
	}
}
