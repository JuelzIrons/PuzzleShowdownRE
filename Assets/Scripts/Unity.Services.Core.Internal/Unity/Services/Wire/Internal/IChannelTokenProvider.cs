namespace Unity.Services.Wire.Internal
{
	public interface IChannelTokenProvider
	{
		global::System.Threading.Tasks.Task<global::Unity.Services.Wire.Internal.ChannelToken> GetTokenAsync();
	}
}
