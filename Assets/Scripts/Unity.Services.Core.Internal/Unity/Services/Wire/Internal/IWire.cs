namespace Unity.Services.Wire.Internal
{
	[global::UnityEngine.Scripting.RequireImplementors]
	public interface IWire : global::Unity.Services.Core.Internal.IServiceComponent
	{
		global::Unity.Services.Wire.Internal.IChannel CreateChannel(global::Unity.Services.Wire.Internal.IChannelTokenProvider tokenProvider);
	}
}
