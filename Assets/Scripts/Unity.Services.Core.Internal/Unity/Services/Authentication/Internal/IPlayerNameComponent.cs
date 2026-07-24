namespace Unity.Services.Authentication.Internal
{
	[global::UnityEngine.Scripting.RequireImplementors]
	public interface IPlayerNameComponent : global::Unity.Services.Core.Internal.IServiceComponent
	{
		string PlayerName { get; }

		event global::System.Action<string> PlayerNameChanged;
	}
}
