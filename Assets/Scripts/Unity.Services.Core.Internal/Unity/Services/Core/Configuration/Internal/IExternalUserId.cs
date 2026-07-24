namespace Unity.Services.Core.Configuration.Internal
{
	[global::UnityEngine.Scripting.RequireImplementors]
	public interface IExternalUserId : global::Unity.Services.Core.Internal.IServiceComponent
	{
		string UserId { get; }

		event global::System.Action<string> UserIdChanged;
	}
}
