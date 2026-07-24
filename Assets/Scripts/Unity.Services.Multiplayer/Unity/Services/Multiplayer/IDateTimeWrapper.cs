namespace Unity.Services.Multiplayer
{
	internal interface IDateTimeWrapper
	{
		global::System.DateTime UtcNow { get; }

		double SecondsSinceUnixEpoch();
	}
}
