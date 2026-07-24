namespace Unity.Services.Wire.Internal
{
	internal class NoChannelPublicationException : global::System.Exception
	{
		public NoChannelPublicationException(string originalData)
			: base("can't parse publication's channel: " + originalData)
		{
		}
	}
}
