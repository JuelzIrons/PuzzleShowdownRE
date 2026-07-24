namespace Unity.Services.Authentication.Internal
{
	[global::UnityEngine.Scripting.RequireImplementors]
	public interface IAccessToken : global::Unity.Services.Core.Internal.IServiceComponent
	{
		string AccessToken { get; }
	}
}
