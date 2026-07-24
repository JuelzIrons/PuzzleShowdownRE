namespace Unity.Services.Wire.Internal
{
	public class ConnectionFailedException : global::Unity.Services.Core.RequestFailedException
	{
		public ConnectionFailedException(string reason)
			: base(23003, "Connection failed: " + reason + ".")
		{
		}
	}
}
