namespace Unity.Services.Authentication.Internal
{
	[global::UnityEngine.Scripting.RequireImplementors]
	public interface IEnvironmentId : global::Unity.Services.Core.Internal.IServiceComponent
	{
		string EnvironmentId { get; }
	}
}
