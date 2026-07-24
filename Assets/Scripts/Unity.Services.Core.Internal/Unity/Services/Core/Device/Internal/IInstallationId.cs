namespace Unity.Services.Core.Device.Internal
{
	public interface IInstallationId : global::Unity.Services.Core.Internal.IServiceComponent
	{
		string GetOrCreateIdentifier();
	}
}
