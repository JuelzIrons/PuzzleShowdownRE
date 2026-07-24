namespace Unity.Services.Authentication.PlayerAccounts
{
	internal class NetworkConfiguration : global::Unity.Services.Authentication.PlayerAccounts.INetworkConfiguration
	{
		private const int k_DefaultRetries = 2;

		private const int k_DefaultTimeout = 5;

		public int Retries { get; set; } = 2;

		public int Timeout { get; set; } = 5;
	}
}
