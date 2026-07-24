namespace Unity.Services.Authentication.Internal
{
	[global::UnityEngine.Scripting.RequireImplementors]
	public interface IPlayerId : global::Unity.Services.Core.Internal.IServiceComponent
	{
		string PlayerId { get; }

		event global::System.Action<string> PlayerIdChanged;
	}
}
