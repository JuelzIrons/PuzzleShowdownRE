namespace Unity.Services.Authentication.Internal
{
	[global::UnityEngine.Scripting.RequireImplementors]
	internal interface IPlayerName
	{
		string PlayerName { get; }

		event global::System.Action<string> PlayerNameChanged;
	}
}
