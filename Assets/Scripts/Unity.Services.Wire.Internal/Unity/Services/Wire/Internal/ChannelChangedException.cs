namespace Unity.Services.Wire.Internal
{
	public class ChannelChangedException : global::Unity.Services.Core.RequestFailedException
	{
		public ChannelChangedException(string newAlias, string oldAlias)
			: base(23005, "The token retriever is not consistent, the alias has changed: " + oldAlias + "->" + newAlias + ".")
		{
		}
	}
}
