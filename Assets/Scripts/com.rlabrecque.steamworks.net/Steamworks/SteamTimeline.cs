namespace Steamworks
{
	public static class SteamTimeline
	{
		public static void SetTimelineTooltip(string pchDescription, float flTimeDelta)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchDescription);
			global::Steamworks.NativeMethods.ISteamTimeline_SetTimelineTooltip(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), pchDescription2, flTimeDelta);
		}

		public static void ClearTimelineTooltip(float flTimeDelta)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamTimeline_ClearTimelineTooltip(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), flTimeDelta);
		}

		public static void SetTimelineGameMode(global::Steamworks.ETimelineGameMode eMode)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamTimeline_SetTimelineGameMode(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), eMode);
		}

		public static global::Steamworks.TimelineEventHandle_t AddInstantaneousTimelineEvent(string pchTitle, string pchDescription, string pchIcon, uint unIconPriority, float flStartOffsetSeconds = 0f, global::Steamworks.ETimelineEventClipPriority ePossibleClip = global::Steamworks.ETimelineEventClipPriority.k_ETimelineEventClipPriority_None)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchTitle2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchTitle);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchDescription);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchIcon2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchIcon);
			return (global::Steamworks.TimelineEventHandle_t)global::Steamworks.NativeMethods.ISteamTimeline_AddInstantaneousTimelineEvent(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), pchTitle2, pchDescription2, pchIcon2, unIconPriority, flStartOffsetSeconds, ePossibleClip);
		}

		public static global::Steamworks.TimelineEventHandle_t AddRangeTimelineEvent(string pchTitle, string pchDescription, string pchIcon, uint unIconPriority, float flStartOffsetSeconds = 0f, float flDuration = 0f, global::Steamworks.ETimelineEventClipPriority ePossibleClip = global::Steamworks.ETimelineEventClipPriority.k_ETimelineEventClipPriority_None)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchTitle2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchTitle);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchDescription);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchIcon2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchIcon);
			return (global::Steamworks.TimelineEventHandle_t)global::Steamworks.NativeMethods.ISteamTimeline_AddRangeTimelineEvent(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), pchTitle2, pchDescription2, pchIcon2, unIconPriority, flStartOffsetSeconds, flDuration, ePossibleClip);
		}

		public static global::Steamworks.TimelineEventHandle_t StartRangeTimelineEvent(string pchTitle, string pchDescription, string pchIcon, uint unPriority, float flStartOffsetSeconds, global::Steamworks.ETimelineEventClipPriority ePossibleClip)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchTitle2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchTitle);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchDescription);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchIcon2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchIcon);
			return (global::Steamworks.TimelineEventHandle_t)global::Steamworks.NativeMethods.ISteamTimeline_StartRangeTimelineEvent(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), pchTitle2, pchDescription2, pchIcon2, unPriority, flStartOffsetSeconds, ePossibleClip);
		}

		public static void UpdateRangeTimelineEvent(global::Steamworks.TimelineEventHandle_t ulEvent, string pchTitle, string pchDescription, string pchIcon, uint unPriority, global::Steamworks.ETimelineEventClipPriority ePossibleClip)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchTitle2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchTitle);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchDescription2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchDescription);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchIcon2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchIcon);
			global::Steamworks.NativeMethods.ISteamTimeline_UpdateRangeTimelineEvent(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), ulEvent, pchTitle2, pchDescription2, pchIcon2, unPriority, ePossibleClip);
		}

		public static void EndRangeTimelineEvent(global::Steamworks.TimelineEventHandle_t ulEvent, float flEndOffsetSeconds)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamTimeline_EndRangeTimelineEvent(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), ulEvent, flEndOffsetSeconds);
		}

		public static void RemoveTimelineEvent(global::Steamworks.TimelineEventHandle_t ulEvent)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamTimeline_RemoveTimelineEvent(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), ulEvent);
		}

		public static global::Steamworks.SteamAPICall_t DoesEventRecordingExist(global::Steamworks.TimelineEventHandle_t ulEvent)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamTimeline_DoesEventRecordingExist(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), ulEvent);
		}

		public static void StartGamePhase()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamTimeline_StartGamePhase(global::Steamworks.CSteamAPIContext.GetSteamTimeline());
		}

		public static void EndGamePhase()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamTimeline_EndGamePhase(global::Steamworks.CSteamAPIContext.GetSteamTimeline());
		}

		public static void SetGamePhaseID(string pchPhaseID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPhaseID2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPhaseID);
			global::Steamworks.NativeMethods.ISteamTimeline_SetGamePhaseID(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), pchPhaseID2);
		}

		public static global::Steamworks.SteamAPICall_t DoesGamePhaseRecordingExist(string pchPhaseID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPhaseID2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPhaseID);
			return (global::Steamworks.SteamAPICall_t)global::Steamworks.NativeMethods.ISteamTimeline_DoesGamePhaseRecordingExist(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), pchPhaseID2);
		}

		public static void AddGamePhaseTag(string pchTagName, string pchTagIcon, string pchTagGroup, uint unPriority)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchTagName2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchTagName);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchTagIcon2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchTagIcon);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchTagGroup2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchTagGroup);
			global::Steamworks.NativeMethods.ISteamTimeline_AddGamePhaseTag(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), pchTagName2, pchTagIcon2, pchTagGroup2, unPriority);
		}

		public static void SetGamePhaseAttribute(string pchAttributeGroup, string pchAttributeValue, uint unPriority)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchAttributeGroup2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchAttributeGroup);
			using global::Steamworks.InteropHelp.UTF8StringHandle pchAttributeValue2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchAttributeValue);
			global::Steamworks.NativeMethods.ISteamTimeline_SetGamePhaseAttribute(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), pchAttributeGroup2, pchAttributeValue2, unPriority);
		}

		public static void OpenOverlayToGamePhase(string pchPhaseID)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			using global::Steamworks.InteropHelp.UTF8StringHandle pchPhaseID2 = new global::Steamworks.InteropHelp.UTF8StringHandle(pchPhaseID);
			global::Steamworks.NativeMethods.ISteamTimeline_OpenOverlayToGamePhase(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), pchPhaseID2);
		}

		public static void OpenOverlayToTimelineEvent(global::Steamworks.TimelineEventHandle_t ulEvent)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamTimeline_OpenOverlayToTimelineEvent(global::Steamworks.CSteamAPIContext.GetSteamTimeline(), ulEvent);
		}
	}
}
