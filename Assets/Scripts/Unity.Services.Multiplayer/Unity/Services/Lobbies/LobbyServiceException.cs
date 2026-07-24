namespace Unity.Services.Lobbies
{
	public class LobbyServiceException : global::Unity.Services.Core.RequestFailedException
	{
		public global::Unity.Services.Lobbies.LobbyExceptionReason Reason { get; private set; }

		public global::Unity.Services.Lobbies.Models.ErrorStatus ApiError => (base.InnerException as global::Unity.Services.Lobbies.Http.HttpException<global::Unity.Services.Lobbies.Models.ErrorStatus>)?.ActualError;

		public LobbyServiceException(global::Unity.Services.Lobbies.LobbyExceptionReason reason, string message, global::System.Exception innerException)
			: base((int)reason, message, innerException)
		{
			Reason = reason;
		}

		public LobbyServiceException(global::Unity.Services.Lobbies.LobbyExceptionReason reason, string message)
			: base((int)reason, message)
		{
			Reason = reason;
		}

		public LobbyServiceException(long errorCode, string message)
			: base((int)errorCode, message)
		{
			if (global::System.Enum.IsDefined(typeof(global::Unity.Services.Lobbies.LobbyExceptionReason), (int)errorCode))
			{
				Reason = (global::Unity.Services.Lobbies.LobbyExceptionReason)errorCode;
			}
			else
			{
				Reason = global::Unity.Services.Lobbies.LobbyExceptionReason.Unknown;
			}
		}

		public LobbyServiceException(global::System.Exception innerException)
			: base(16999, "Unknown Lobby Service Exception", innerException)
		{
		}
	}
}
