namespace Unity.Services.Wire.Internal
{
	public class AlreadySubscribedException : global::Unity.Services.Core.RequestFailedException
	{
		public AlreadySubscribedException(string alias)
			: base(23008, "Already subscribed to " + alias + ".")
		{
		}
	}
}
