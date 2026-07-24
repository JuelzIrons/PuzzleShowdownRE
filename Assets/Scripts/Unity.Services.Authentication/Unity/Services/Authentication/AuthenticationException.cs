namespace Unity.Services.Authentication
{
	public sealed class AuthenticationException : global::Unity.Services.Core.RequestFailedException
	{
		public global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification> Notifications { get; }

		private AuthenticationException(int errorCode, string message, global::System.Exception innerException = null, global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification> notifications = null)
			: base(errorCode, message, innerException)
		{
			Notifications = notifications;
		}

		public static global::Unity.Services.Core.RequestFailedException Create(int errorCode, string message, global::System.Exception innerException = null)
		{
			return Create(errorCode, message, null, innerException);
		}

		internal static global::Unity.Services.Core.RequestFailedException Create(int errorCode, string message, global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification> notifications, global::System.Exception innerException = null)
		{
			if (errorCode < global::Unity.Services.Authentication.AuthenticationErrorCodes.MinValue)
			{
				return new global::Unity.Services.Core.RequestFailedException(errorCode, message, innerException);
			}
			return new global::Unity.Services.Authentication.AuthenticationException(errorCode, message, innerException, notifications);
		}
	}
}
