namespace Unity.Services.Matchmaker
{
	public class MatchmakerServiceException : global::Unity.Services.Core.RequestFailedException
	{
		public global::Unity.Services.Matchmaker.MatchmakerExceptionReason Reason { get; private set; }

		public MatchmakerServiceException(global::Unity.Services.Matchmaker.MatchmakerExceptionReason reason, string message, global::System.Exception innerException = null)
			: base((int)reason, message, innerException)
		{
			Reason = reason;
		}

		public MatchmakerServiceException(global::System.Exception innerException)
			: base(21999, "Unknown Matchmaker Service Exception", innerException)
		{
		}
	}
}
