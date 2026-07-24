namespace Unity.Services.Authentication.PlayerAccounts
{
	internal interface INetworkConfiguration
	{
		int Retries { get; }

		int Timeout { get; }
	}
}
