namespace Unity.Services.DistributedAuthority.Internal
{
	internal interface IClock
	{
		global::System.DateTimeOffset UtcNow();
	}
}
