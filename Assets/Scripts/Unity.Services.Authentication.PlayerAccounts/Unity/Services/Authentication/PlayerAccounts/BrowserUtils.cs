namespace Unity.Services.Authentication.PlayerAccounts
{
	internal static class BrowserUtils
	{
		internal static global::Unity.Services.Authentication.PlayerAccounts.IBrowserUtils CreateBrowserUtils(global::Unity.Services.Core.Configuration.Internal.ICloudProjectId cloudProjectId, global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings settings, global::System.Action<string> onAuthCodeReceived)
		{
			global::Unity.Services.Authentication.PlayerAccounts.StandaloneBrowserUtils standaloneBrowserUtils = new global::Unity.Services.Authentication.PlayerAccounts.StandaloneBrowserUtils();
			standaloneBrowserUtils.AuthCodeReceivedEvent += onAuthCodeReceived;
			return standaloneBrowserUtils;
		}
	}
}
