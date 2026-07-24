namespace Unity.Services.Vivox.Internal
{
	[global::UnityEngine.Scripting.RequireImplementors]
	public interface IVivox : global::Unity.Services.Core.Internal.IServiceComponent
	{
		void RegisterTokenProvider(global::Unity.Services.Vivox.Internal.IVivoxTokenProviderInternal tokenProvider);
	}
}
