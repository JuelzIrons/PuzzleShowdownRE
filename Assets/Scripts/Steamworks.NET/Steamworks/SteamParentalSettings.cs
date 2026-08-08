namespace Steamworks
{
	public static class SteamParentalSettings
	{
		public static bool BIsParentalLockEnabled()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamParentalSettings_BIsParentalLockEnabled(global::Steamworks.CSteamAPIContext.GetSteamParentalSettings());
		}

		public static bool BIsParentalLockLocked()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamParentalSettings_BIsParentalLockLocked(global::Steamworks.CSteamAPIContext.GetSteamParentalSettings());
		}

		public static bool BIsAppBlocked(global::Steamworks.AppId_t nAppID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamParentalSettings_BIsAppBlocked(global::Steamworks.CSteamAPIContext.GetSteamParentalSettings(), nAppID);
		}

		public static bool BIsAppInBlockList(global::Steamworks.AppId_t nAppID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamParentalSettings_BIsAppInBlockList(global::Steamworks.CSteamAPIContext.GetSteamParentalSettings(), nAppID);
		}

		public static bool BIsFeatureBlocked(global::Steamworks.EParentalFeature eFeature)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamParentalSettings_BIsFeatureBlocked(global::Steamworks.CSteamAPIContext.GetSteamParentalSettings(), eFeature);
		}

		public static bool BIsFeatureInBlockList(global::Steamworks.EParentalFeature eFeature)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamParentalSettings_BIsFeatureInBlockList(global::Steamworks.CSteamAPIContext.GetSteamParentalSettings(), eFeature);
		}
	}
}
