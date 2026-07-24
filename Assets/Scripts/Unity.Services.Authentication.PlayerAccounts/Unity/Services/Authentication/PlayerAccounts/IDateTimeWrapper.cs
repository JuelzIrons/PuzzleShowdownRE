namespace Unity.Services.Authentication.PlayerAccounts
{
	internal interface IDateTimeWrapper
	{
		global::System.DateTime UtcNow { get; }

		double SecondsSinceUnixEpoch();
	}
}
