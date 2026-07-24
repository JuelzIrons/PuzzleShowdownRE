namespace Unity.Services.DistributedAuthority.Internal
{
	internal class DefaultClock : global::Unity.Services.DistributedAuthority.Internal.IClock
	{
		public global::System.DateTimeOffset UtcNow()
		{
			return global::System.DateTimeOffset.UtcNow;
		}
	}
}
