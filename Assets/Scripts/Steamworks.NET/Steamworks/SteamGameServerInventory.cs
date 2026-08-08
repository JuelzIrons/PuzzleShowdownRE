namespace Steamworks
{
	public static class SteamGameServerInventory
	{
		public static global::Steamworks.EResult GetResultStatus(global::Steamworks.SteamInventoryResult_t resultHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_GetResultStatus(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), resultHandle);
		}

		public static bool GetResultItems(global::Steamworks.SteamInventoryResult_t resultHandle, global::Steamworks.SteamItemDetails_t[] pOutItemsArray, ref uint punOutItemsArraySize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			if (pOutItemsArray != null && pOutItemsArray.Length != punOutItemsArraySize)
			{
				throw new global::System.ArgumentException("pOutItemsArray must be the same size as punOutItemsArraySize!");
			}
			return global::Steamworks.NativeMethods.ISteamInventory_GetResultItems(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, pOutItemsArray, ref punOutItemsArraySize);
		}

		public static bool GetResultItemProperty(global::Steamworks.SteamInventoryResult_t resultHandle, uint unItemIndex, string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)punValueBufferSizeOut);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPropertyName);
			bool flag = global::Steamworks.NativeMethods.ISteamInventory_GetResultItemProperty(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, unItemIndex, pchPropertyName2, intPtr, ref punValueBufferSizeOut);
			pchValueBuffer = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static uint GetResultTimestamp(global::Steamworks.SteamInventoryResult_t resultHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_GetResultTimestamp(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), resultHandle);
		}

		public static bool CheckResultSteamID(global::Steamworks.SteamInventoryResult_t resultHandle, global::Steamworks.CSteamID steamIDExpected)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_CheckResultSteamID(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, steamIDExpected);
		}

		public static void DestroyResult(global::Steamworks.SteamInventoryResult_t resultHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamInventory_DestroyResult(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), resultHandle);
		}

		public static bool GetAllItems(out global::Steamworks.SteamInventoryResult_t pResultHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_GetAllItems(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle);
		}

		public static bool GetItemsByID(out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemInstanceID_t[] pInstanceIDs, uint unCountInstanceIDs)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_GetItemsByID(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pInstanceIDs, unCountInstanceIDs);
		}

		public static bool SerializeResult(global::Steamworks.SteamInventoryResult_t resultHandle, byte[] pOutBuffer, out uint punOutBufferSize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_SerializeResult(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), resultHandle, pOutBuffer, out punOutBufferSize);
		}

		public static bool DeserializeResult(out global::Steamworks.SteamInventoryResult_t pOutResultHandle, byte[] pBuffer, uint unBufferSize, bool bRESERVED_MUST_BE_FALSE = false)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_DeserializeResult(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pOutResultHandle, pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE);
		}

		public static bool GenerateItems(out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemDef_t[] pArrayItemDefs, uint[] punArrayQuantity, uint unArrayLength)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_GenerateItems(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pArrayItemDefs, punArrayQuantity, unArrayLength);
		}

		public static bool GrantPromoItems(out global::Steamworks.SteamInventoryResult_t pResultHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_GrantPromoItems(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle);
		}

		public static bool AddPromoItem(out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemDef_t itemDef)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_AddPromoItem(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, itemDef);
		}

		public static bool AddPromoItems(out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemDef_t[] pArrayItemDefs, uint unArrayLength)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_AddPromoItems(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pArrayItemDefs, unArrayLength);
		}

		public static bool ConsumeItem(out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemInstanceID_t itemConsume, uint unQuantity)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_ConsumeItem(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, itemConsume, unQuantity);
		}

		public static bool ExchangeItems(out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemDef_t[] pArrayGenerate, uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, global::Steamworks.SteamItemInstanceID_t[] pArrayDestroy, uint[] punArrayDestroyQuantity, uint unArrayDestroyLength)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_ExchangeItems(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pArrayGenerate, punArrayGenerateQuantity, unArrayGenerateLength, pArrayDestroy, punArrayDestroyQuantity, unArrayDestroyLength);
		}

		public static bool TransferItemQuantity(out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemInstanceID_t itemIdSource, uint unQuantity, global::Steamworks.SteamItemInstanceID_t itemIdDest)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_TransferItemQuantity(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, itemIdSource, unQuantity, itemIdDest);
		}

		public static void SendItemDropHeartbeat()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::Steamworks.NativeMethods.ISteamInventory_SendItemDropHeartbeat(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory());
		}

		public static bool TriggerItemDrop(out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.SteamItemDef_t dropListDefinition)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_TriggerItemDrop(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, dropListDefinition);
		}

		public static bool TradeItems(out global::Steamworks.SteamInventoryResult_t pResultHandle, global::Steamworks.CSteamID steamIDTradePartner, global::Steamworks.SteamItemInstanceID_t[] pArrayGive, uint[] pArrayGiveQuantity, uint nArrayGiveLength, global::Steamworks.SteamItemInstanceID_t[] pArrayGet, uint[] pArrayGetQuantity, uint nArrayGetLength)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_TradeItems(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, steamIDTradePartner, pArrayGive, pArrayGiveQuantity, nArrayGiveLength, pArrayGet, pArrayGetQuantity, nArrayGetLength);
		}

		public static bool LoadItemDefinitions()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_LoadItemDefinitions(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory());
		}

		public static bool GetItemDefinitionIDs(global::Steamworks.SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			if (pItemDefIDs != null && pItemDefIDs.Length != punItemDefIDsArraySize)
			{
				throw new global::System.ArgumentException("pItemDefIDs must be the same size as punItemDefIDsArraySize!");
			}
			return global::Steamworks.NativeMethods.ISteamInventory_GetItemDefinitionIDs(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), pItemDefIDs, ref punItemDefIDsArraySize);
		}

		public static bool GetItemDefinitionProperty(global::Steamworks.SteamItemDef_t iDefinition, string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)punValueBufferSizeOut);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPropertyName);
			bool flag = global::Steamworks.NativeMethods.ISteamInventory_GetItemDefinitionProperty(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), iDefinition, pchPropertyName2, intPtr, ref punValueBufferSizeOut);
			pchValueBuffer = (flag ? global::Steamworks.InteropHelp.PtrToStringUTF8(intPtr) : null);
			global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		public static global::Steamworks.SteamAPICall_t RequestEligiblePromoItemDefinitionsIDs(global::Steamworks.CSteamID steamID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamInventory_RequestEligiblePromoItemDefinitionsIDs(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), steamID);
		}

		public static bool GetEligiblePromoItemDefinitionIDs(global::Steamworks.CSteamID steamID, global::Steamworks.SteamItemDef_t[] pItemDefIDs, ref uint punItemDefIDsArraySize)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			if (pItemDefIDs != null && pItemDefIDs.Length != punItemDefIDsArraySize)
			{
				throw new global::System.ArgumentException("pItemDefIDs must be the same size as punItemDefIDsArraySize!");
			}
			return global::Steamworks.NativeMethods.ISteamInventory_GetEligiblePromoItemDefinitionIDs(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), steamID, pItemDefIDs, ref punItemDefIDsArraySize);
		}

		public static global::Steamworks.SteamAPICall_t StartPurchase(global::Steamworks.SteamItemDef_t[] pArrayItemDefs, uint[] punArrayQuantity, uint unArrayLength)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamInventory_StartPurchase(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), pArrayItemDefs, punArrayQuantity, unArrayLength);
		}

		public static global::Steamworks.SteamAPICall_t RequestPrices()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamInventory_RequestPrices(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory());
		}

		public static uint GetNumItemsWithPrices()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_GetNumItemsWithPrices(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory());
		}

		public static bool GetItemsWithPrices(global::Steamworks.SteamItemDef_t[] pArrayItemDefs, ulong[] pCurrentPrices, ulong[] pBasePrices, uint unArrayLength)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			if (pArrayItemDefs != null && pArrayItemDefs.Length != unArrayLength)
			{
				throw new global::System.ArgumentException("pArrayItemDefs must be the same size as unArrayLength!");
			}
			if (pCurrentPrices != null && pCurrentPrices.Length != unArrayLength)
			{
				throw new global::System.ArgumentException("pCurrentPrices must be the same size as unArrayLength!");
			}
			if (pBasePrices != null && pBasePrices.Length != unArrayLength)
			{
				throw new global::System.ArgumentException("pBasePrices must be the same size as unArrayLength!");
			}
			return global::Steamworks.NativeMethods.ISteamInventory_GetItemsWithPrices(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), pArrayItemDefs, pCurrentPrices, pBasePrices, unArrayLength);
		}

		public static bool GetItemPrice(global::Steamworks.SteamItemDef_t iDefinition, out ulong pCurrentPrice, out ulong pBasePrice)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_GetItemPrice(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), iDefinition, out pCurrentPrice, out pBasePrice);
		}

		public static global::Steamworks.SteamInventoryUpdateHandle_t StartUpdateProperties()
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return (global::Steamworks.SteamInventoryUpdateHandle_t)global::Steamworks.NativeMethods.ISteamInventory_StartUpdateProperties(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory());
		}

		public static bool RemoveProperty(global::Steamworks.SteamInventoryUpdateHandle_t handle, global::Steamworks.SteamItemInstanceID_t nItemID, string pchPropertyName)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPropertyName);
			return global::Steamworks.NativeMethods.ISteamInventory_RemoveProperty(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, pchPropertyName2);
		}

		public static bool SetProperty(global::Steamworks.SteamInventoryUpdateHandle_t handle, global::Steamworks.SteamItemInstanceID_t nItemID, string pchPropertyName, string pchPropertyValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPropertyName);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyValue2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPropertyValue);
			return global::Steamworks.NativeMethods.ISteamInventory_SetPropertyString(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, pchPropertyName2, pchPropertyValue2);
		}

		public static bool SetProperty(global::Steamworks.SteamInventoryUpdateHandle_t handle, global::Steamworks.SteamItemInstanceID_t nItemID, string pchPropertyName, bool bValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPropertyName);
			return global::Steamworks.NativeMethods.ISteamInventory_SetPropertyBool(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, pchPropertyName2, bValue);
		}

		public static bool SetProperty(global::Steamworks.SteamInventoryUpdateHandle_t handle, global::Steamworks.SteamItemInstanceID_t nItemID, string pchPropertyName, long nValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPropertyName);
			return global::Steamworks.NativeMethods.ISteamInventory_SetPropertyInt64(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, pchPropertyName2, nValue);
		}

		public static bool SetProperty(global::Steamworks.SteamInventoryUpdateHandle_t handle, global::Steamworks.SteamItemInstanceID_t nItemID, string pchPropertyName, float flValue)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPropertyName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPropertyName);
			return global::Steamworks.NativeMethods.ISteamInventory_SetPropertyFloat(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), handle, nItemID, pchPropertyName2, flValue);
		}

		public static bool SubmitUpdateProperties(global::Steamworks.SteamInventoryUpdateHandle_t handle, out global::Steamworks.SteamInventoryResult_t pResultHandle)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			return global::Steamworks.NativeMethods.ISteamInventory_SubmitUpdateProperties(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), handle, out pResultHandle);
		}

		public static bool InspectItem(out global::Steamworks.SteamInventoryResult_t pResultHandle, string pchItemToken)
		{
			global::Steamworks.InteropHelp.TestIfAvailableGameServer();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchItemToken2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchItemToken);
			return global::Steamworks.NativeMethods.ISteamInventory_InspectItem(global::Steamworks.CSteamGameServerAPIContext.GetSteamInventory(), out pResultHandle, pchItemToken2);
		}
	}
}
