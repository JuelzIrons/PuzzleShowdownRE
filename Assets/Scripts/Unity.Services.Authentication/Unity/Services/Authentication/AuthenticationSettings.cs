namespace Unity.Services.Authentication
{
	internal class AuthenticationSettings : global::Unity.Services.Authentication.IAuthenticationSettings
	{
		private const int k_AccessTokenRefreshBuffer = 300;

		private const int k_AccessTokenExpiryBuffer = 15;

		private const int k_RefreshAttemptFrequency = 30;

		private const int k_CodeConfirmationDelay = 5;

		public int AccessTokenRefreshBuffer { get; internal set; }

		public int AccessTokenExpiryBuffer { get; internal set; }

		public int RefreshAttemptFrequency { get; internal set; }

		public int CodeConfirmationAttempts { get; internal set; }

		public int CodeConfirmationDelay { get; internal set; }

		internal AuthenticationSettings()
		{
			AccessTokenRefreshBuffer = 300;
			AccessTokenExpiryBuffer = 15;
			RefreshAttemptFrequency = 30;
			CodeConfirmationDelay = 5;
		}

		internal void Reset()
		{
			AccessTokenRefreshBuffer = 300;
			AccessTokenExpiryBuffer = 15;
			RefreshAttemptFrequency = 30;
			CodeConfirmationDelay = 5;
		}
	}
}
