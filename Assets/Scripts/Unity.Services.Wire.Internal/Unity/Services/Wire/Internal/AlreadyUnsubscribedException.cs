namespace Unity.Services.Wire.Internal
{
	public class AlreadyUnsubscribedException : global::Unity.Services.Core.RequestFailedException
	{
		public AlreadyUnsubscribedException(string alias)
			: base(23009, "Already unsubscribed from " + alias)
		{
		}
	}
}
