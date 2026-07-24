namespace Steamworks
{
	public static class SteamInput
	{
		public static bool Init(bool bExplicitlyCallRunFrame)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_Init(global::Steamworks.CSteamAPIContext.GetSteamInput(), bExplicitlyCallRunFrame);
		}

		public static bool Shutdown()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_Shutdown(global::Steamworks.CSteamAPIContext.GetSteamInput());
		}

		public static bool SetInputActionManifestFilePath(string pchInputActionManifestAbsolutePath)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchInputActionManifestAbsolutePath2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchInputActionManifestAbsolutePath);
			return global::Steamworks.NativeMethods.ISteamInput_SetInputActionManifestFilePath(global::Steamworks.CSteamAPIContext.GetSteamInput(), pchInputActionManifestAbsolutePath2);
		}

		public static void RunFrame(bool bReservedValue = true)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_RunFrame(global::Steamworks.CSteamAPIContext.GetSteamInput(), bReservedValue);
		}

		public static bool BWaitForData(bool bWaitForever, uint unTimeout)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_BWaitForData(global::Steamworks.CSteamAPIContext.GetSteamInput(), bWaitForever, unTimeout);
		}

		public static bool BNewDataAvailable()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_BNewDataAvailable(global::Steamworks.CSteamAPIContext.GetSteamInput());
		}

		public static int GetConnectedControllers(global::Steamworks.InputHandle_t[] handlesOut)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			if (handlesOut != null && handlesOut.Length != 16)
			{
				throw new global::System.ArgumentException("handlesOut must be the same size as Constants.STEAM_INPUT_MAX_COUNT!");
			}
			return global::Steamworks.NativeMethods.ISteamInput_GetConnectedControllers(global::Steamworks.CSteamAPIContext.GetSteamInput(), handlesOut);
		}

		public static void EnableDeviceCallbacks()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_EnableDeviceCallbacks(global::Steamworks.CSteamAPIContext.GetSteamInput());
		}

		public static void EnableActionEventCallbacks(global::Steamworks.SteamInputActionEventCallbackPointer pCallback)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_EnableActionEventCallbacks(global::Steamworks.CSteamAPIContext.GetSteamInput(), pCallback);
		}

		public static global::Steamworks.InputActionSetHandle_t GetActionSetHandle(string pszActionSetName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszActionSetName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszActionSetName);
			return (global::Steamworks.InputActionSetHandle_t)global::Steamworks.NativeMethods.ISteamInput_GetActionSetHandle(global::Steamworks.CSteamAPIContext.GetSteamInput(), pszActionSetName2);
		}

		public static void ActivateActionSet(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputActionSetHandle_t actionSetHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_ActivateActionSet(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, actionSetHandle);
		}

		public static global::Steamworks.InputActionSetHandle_t GetCurrentActionSet(global::Steamworks.InputHandle_t inputHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.InputActionSetHandle_t)global::Steamworks.NativeMethods.ISteamInput_GetCurrentActionSet(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		public static void ActivateActionSetLayer(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputActionSetHandle_t actionSetLayerHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_ActivateActionSetLayer(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, actionSetLayerHandle);
		}

		public static void DeactivateActionSetLayer(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputActionSetHandle_t actionSetLayerHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_DeactivateActionSetLayer(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, actionSetLayerHandle);
		}

		public static void DeactivateAllActionSetLayers(global::Steamworks.InputHandle_t inputHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_DeactivateAllActionSetLayers(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		public static int GetActiveActionSetLayers(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputActionSetHandle_t[] handlesOut)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			if (handlesOut != null && handlesOut.Length != 16)
			{
				throw new global::System.ArgumentException("handlesOut must be the same size as Constants.STEAM_INPUT_MAX_ACTIVE_LAYERS!");
			}
			return global::Steamworks.NativeMethods.ISteamInput_GetActiveActionSetLayers(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, handlesOut);
		}

		public static global::Steamworks.InputDigitalActionHandle_t GetDigitalActionHandle(string pszActionName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszActionName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszActionName);
			return (global::Steamworks.InputDigitalActionHandle_t)global::Steamworks.NativeMethods.ISteamInput_GetDigitalActionHandle(global::Steamworks.CSteamAPIContext.GetSteamInput(), pszActionName2);
		}

		public static global::Steamworks.InputDigitalActionData_t GetDigitalActionData(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputDigitalActionHandle_t digitalActionHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_GetDigitalActionData(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, digitalActionHandle);
		}

		public static int GetDigitalActionOrigins(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputActionSetHandle_t actionSetHandle, global::Steamworks.InputDigitalActionHandle_t digitalActionHandle, global::Steamworks.EInputActionOrigin[] originsOut)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			if (originsOut != null && originsOut.Length != 8)
			{
				throw new global::System.ArgumentException("originsOut must be the same size as Constants.STEAM_INPUT_MAX_ORIGINS!");
			}
			return global::Steamworks.NativeMethods.ISteamInput_GetDigitalActionOrigins(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, actionSetHandle, digitalActionHandle, originsOut);
		}

		public static string GetStringForDigitalActionName(global::Steamworks.InputDigitalActionHandle_t eActionHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamInput_GetStringForDigitalActionName(global::Steamworks.CSteamAPIContext.GetSteamInput(), eActionHandle));
		}

		public static global::Steamworks.InputAnalogActionHandle_t GetAnalogActionHandle(string pszActionName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pszActionName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pszActionName);
			return (global::Steamworks.InputAnalogActionHandle_t)global::Steamworks.NativeMethods.ISteamInput_GetAnalogActionHandle(global::Steamworks.CSteamAPIContext.GetSteamInput(), pszActionName2);
		}

		public static global::Steamworks.InputAnalogActionData_t GetAnalogActionData(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputAnalogActionHandle_t analogActionHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_GetAnalogActionData(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, analogActionHandle);
		}

		public static int GetAnalogActionOrigins(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputActionSetHandle_t actionSetHandle, global::Steamworks.InputAnalogActionHandle_t analogActionHandle, global::Steamworks.EInputActionOrigin[] originsOut)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			if (originsOut != null && originsOut.Length != 8)
			{
				throw new global::System.ArgumentException("originsOut must be the same size as Constants.STEAM_INPUT_MAX_ORIGINS!");
			}
			return global::Steamworks.NativeMethods.ISteamInput_GetAnalogActionOrigins(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, actionSetHandle, analogActionHandle, originsOut);
		}

		public static string GetGlyphPNGForActionOrigin(global::Steamworks.EInputActionOrigin eOrigin, global::Steamworks.ESteamInputGlyphSize eSize, uint unFlags)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamInput_GetGlyphPNGForActionOrigin(global::Steamworks.CSteamAPIContext.GetSteamInput(), eOrigin, eSize, unFlags));
		}

		public static string GetGlyphSVGForActionOrigin(global::Steamworks.EInputActionOrigin eOrigin, uint unFlags)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamInput_GetGlyphSVGForActionOrigin(global::Steamworks.CSteamAPIContext.GetSteamInput(), eOrigin, unFlags));
		}

		public static string GetGlyphForActionOrigin_Legacy(global::Steamworks.EInputActionOrigin eOrigin)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamInput_GetGlyphForActionOrigin_Legacy(global::Steamworks.CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		public static string GetStringForActionOrigin(global::Steamworks.EInputActionOrigin eOrigin)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamInput_GetStringForActionOrigin(global::Steamworks.CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		public static string GetStringForAnalogActionName(global::Steamworks.InputAnalogActionHandle_t eActionHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamInput_GetStringForAnalogActionName(global::Steamworks.CSteamAPIContext.GetSteamInput(), eActionHandle));
		}

		public static void StopAnalogActionMomentum(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.InputAnalogActionHandle_t eAction)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_StopAnalogActionMomentum(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, eAction);
		}

		public static global::Steamworks.InputMotionData_t GetMotionData(global::Steamworks.InputHandle_t inputHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_GetMotionData(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		public static void TriggerVibration(global::Steamworks.InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_TriggerVibration(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, usLeftSpeed, usRightSpeed);
		}

		public static void TriggerVibrationExtended(global::Steamworks.InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed, ushort usLeftTriggerSpeed, ushort usRightTriggerSpeed)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_TriggerVibrationExtended(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, usLeftSpeed, usRightSpeed, usLeftTriggerSpeed, usRightTriggerSpeed);
		}

		public static void TriggerSimpleHapticEvent(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.EControllerHapticLocation eHapticLocation, byte nIntensity, char nGainDB, byte nOtherIntensity, char nOtherGainDB)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_TriggerSimpleHapticEvent(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, eHapticLocation, nIntensity, nGainDB, nOtherIntensity, nOtherGainDB);
		}

		public static void SetLEDColor(global::Steamworks.InputHandle_t inputHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_SetLEDColor(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, nColorR, nColorG, nColorB, nFlags);
		}

		public static void Legacy_TriggerHapticPulse(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.ESteamControllerPad eTargetPad, ushort usDurationMicroSec)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_Legacy_TriggerHapticPulse(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, eTargetPad, usDurationMicroSec);
		}

		public static void Legacy_TriggerRepeatedHapticPulse(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.ESteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_Legacy_TriggerRepeatedHapticPulse(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags);
		}

		public static bool ShowBindingPanel(global::Steamworks.InputHandle_t inputHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_ShowBindingPanel(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		public static global::Steamworks.ESteamInputType GetInputTypeForHandle(global::Steamworks.InputHandle_t inputHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_GetInputTypeForHandle(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		public static global::Steamworks.InputHandle_t GetControllerForGamepadIndex(int nIndex)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.InputHandle_t)global::Steamworks.NativeMethods.ISteamInput_GetControllerForGamepadIndex(global::Steamworks.CSteamAPIContext.GetSteamInput(), nIndex);
		}

		public static int GetGamepadIndexForController(global::Steamworks.InputHandle_t ulinputHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_GetGamepadIndexForController(global::Steamworks.CSteamAPIContext.GetSteamInput(), ulinputHandle);
		}

		public static string GetStringForXboxOrigin(global::Steamworks.EXboxOrigin eOrigin)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamInput_GetStringForXboxOrigin(global::Steamworks.CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		public static string GetGlyphForXboxOrigin(global::Steamworks.EXboxOrigin eOrigin)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.InteropHelp.PtrToStringUTF8(global::Steamworks.NativeMethods.ISteamInput_GetGlyphForXboxOrigin(global::Steamworks.CSteamAPIContext.GetSteamInput(), eOrigin));
		}

		public static global::Steamworks.EInputActionOrigin GetActionOriginFromXboxOrigin(global::Steamworks.InputHandle_t inputHandle, global::Steamworks.EXboxOrigin eOrigin)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_GetActionOriginFromXboxOrigin(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, eOrigin);
		}

		public static global::Steamworks.EInputActionOrigin TranslateActionOrigin(global::Steamworks.ESteamInputType eDestinationInputType, global::Steamworks.EInputActionOrigin eSourceOrigin)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_TranslateActionOrigin(global::Steamworks.CSteamAPIContext.GetSteamInput(), eDestinationInputType, eSourceOrigin);
		}

		public static bool GetDeviceBindingRevision(global::Steamworks.InputHandle_t inputHandle, out int pMajor, out int pMinor)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_GetDeviceBindingRevision(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, out pMajor, out pMinor);
		}

		public static uint GetRemotePlaySessionID(global::Steamworks.InputHandle_t inputHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_GetRemotePlaySessionID(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle);
		}

		public static ushort GetSessionInputConfigurationSettings()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamInput_GetSessionInputConfigurationSettings(global::Steamworks.CSteamAPIContext.GetSteamInput());
		}

		public static void SetDualSenseTriggerEffect(global::Steamworks.InputHandle_t inputHandle, global::System.IntPtr pParam)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamInput_SetDualSenseTriggerEffect(global::Steamworks.CSteamAPIContext.GetSteamInput(), inputHandle, pParam);
		}
	}
}
