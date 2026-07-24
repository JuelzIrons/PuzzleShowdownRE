namespace Unity.Services.Authentication.Internal
{
	[global::UnityEngine.Scripting.RequireImplementors]
	public interface IAccessTokenObserver : global::Unity.Services.Core.Internal.IServiceComponent
	{
		event global::System.Action<string> AccessTokenChanged;
	}
}
