namespace Unity.Services.Vivox.Internal
{
	public interface IVivoxTokenProviderInternal
	{
		global::System.Threading.Tasks.Task<string> GetTokenAsync(string issuer = null, global::System.TimeSpan? expiration = null, string userUri = null, string action = null, string conferenceUri = null, string fromUserUri = null, string realm = null);
	}
}
