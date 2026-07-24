namespace Steamworks
{
	public static class SteamParties
	{
		public static uint GetNumActiveBeacons()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamParties_GetNumActiveBeacons(global::Steamworks.CSteamAPIContext.GetSteamParties());
		}

		public static global::Steamworks.PartyBeaconID_t GetBeaconByIndex(uint unIndex)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.PartyBeaconID_t)global::Steamworks.NativeMethods.ISteamParties_GetBeaconByIndex(global::Steamworks.CSteamAPIContext.GetSteamParties(), unIndex);
		}

		public static bool GetBeaconDetails(global::Steamworks.PartyBeaconID_t ulBeaconID, out global::Steamworks.CSteamID pSteamIDBeaconOwner, out global::Steamworks.SteamPartyBeaconLocation_t pLocation, out string pchMetadata, int cchMetadata)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cchMetadata);
			bool flag = global::Steamworks.NativeMethods.ISteamParties_GetBeaconDetails(global::Steamworks.CSteamAPIContext.GetSteamParties(), ulBeaconID, out pSteamIDBeaconOwner, out pLocation, intPtr, cchMetadata);
			pchMetadata = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static global::Steamworks.SteamAPICall_t JoinParty(global::Steamworks.PartyBeaconID_t ulBeaconID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamParties_JoinParty(global::Steamworks.CSteamAPIContext.GetSteamParties(), ulBeaconID);
		}

		public static bool GetNumAvailableBeaconLocations(out uint puNumLocations)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamParties_GetNumAvailableBeaconLocations(global::Steamworks.CSteamAPIContext.GetSteamParties(), out puNumLocations);
		}

		public static bool GetAvailableBeaconLocations(global::Steamworks.SteamPartyBeaconLocation_t[] pLocationList, uint uMaxNumLocations)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamParties_GetAvailableBeaconLocations(global::Steamworks.CSteamAPIContext.GetSteamParties(), pLocationList, uMaxNumLocations);
		}

		public static global::Steamworks.SteamAPICall_t CreateBeacon(uint unOpenSlots, ref global::Steamworks.SteamPartyBeaconLocation_t pBeaconLocation, string pchConnectString, string pchMetadata)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchConnectString2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchConnectString);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchMetadata2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchMetadata);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamParties_CreateBeacon(global::Steamworks.CSteamAPIContext.GetSteamParties(), unOpenSlots, ref pBeaconLocation, pchConnectString2, pchMetadata2);
		}

		public static void OnReservationCompleted(global::Steamworks.PartyBeaconID_t ulBeacon, global::Steamworks.CSteamID steamIDUser)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamParties_OnReservationCompleted(global::Steamworks.CSteamAPIContext.GetSteamParties(), ulBeacon, steamIDUser);
		}

		public static void CancelReservation(global::Steamworks.PartyBeaconID_t ulBeacon, global::Steamworks.CSteamID steamIDUser)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamParties_CancelReservation(global::Steamworks.CSteamAPIContext.GetSteamParties(), ulBeacon, steamIDUser);
		}

		public static global::Steamworks.SteamAPICall_t ChangeNumOpenSlots(global::Steamworks.PartyBeaconID_t ulBeacon, uint unOpenSlots)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamParties_ChangeNumOpenSlots(global::Steamworks.CSteamAPIContext.GetSteamParties(), ulBeacon, unOpenSlots);
		}

		public static bool DestroyBeacon(global::Steamworks.PartyBeaconID_t ulBeacon)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamParties_DestroyBeacon(global::Steamworks.CSteamAPIContext.GetSteamParties(), ulBeacon);
		}

		public static bool GetBeaconLocationData(global::Steamworks.SteamPartyBeaconLocation_t BeaconLocation, global::Steamworks.ESteamPartyBeaconLocationData eData, out string pchDataStringOut, int cchDataStringOut)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(cchDataStringOut);
			bool flag = global::Steamworks.NativeMethods.ISteamParties_GetBeaconLocationData(global::Steamworks.CSteamAPIContext.GetSteamParties(), BeaconLocation, eData, intPtr, cchDataStringOut);
			pchDataStringOut = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}
	}
}
