namespace Unity.Services.Authentication.PlayerAccounts
{
	public static class PlayerAccountsErrorCodes
	{
		public const int UnknownError = 10100;

		public const int InvalidState = 10101;

		public const int MissingClientId = 10102;

		public const int InvalidClient = 10103;

		public const int InvalidScope = 10104;

		public const int InvalidRequest = 10105;

		public const int InvalidGrant = 10106;

		public const int MissingRefreshToken = 10107;

		public const int UnauthorizedClient = 10108;

		public const int UnsupportedGrantType = 10109;

		public const int UnsupportedResponseType = 10110;
	}
}
