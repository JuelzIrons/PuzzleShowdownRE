namespace Steamworks
{
	public static class SteamMusic
	{
		public static bool BIsEnabled()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMusic_BIsEnabled(global::Steamworks.CSteamAPIContext.GetSteamMusic());
		}

		public static bool BIsPlaying()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMusic_BIsPlaying(global::Steamworks.CSteamAPIContext.GetSteamMusic());
		}

		public static global::Steamworks.AudioPlayback_Status GetPlaybackStatus()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMusic_GetPlaybackStatus(global::Steamworks.CSteamAPIContext.GetSteamMusic());
		}

		public static void Play()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMusic_Play(global::Steamworks.CSteamAPIContext.GetSteamMusic());
		}

		public static void Pause()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMusic_Pause(global::Steamworks.CSteamAPIContext.GetSteamMusic());
		}

		public static void PlayPrevious()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMusic_PlayPrevious(global::Steamworks.CSteamAPIContext.GetSteamMusic());
		}

		public static void PlayNext()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMusic_PlayNext(global::Steamworks.CSteamAPIContext.GetSteamMusic());
		}

		public static void SetVolume(float flVolume)
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			global::Steamworks.NativeMethods.ISteamMusic_SetVolume(global::Steamworks.CSteamAPIContext.GetSteamMusic(), flVolume);
		}

		public static float GetVolume()
		{
			global::Steamworks.InteropHelp.TestIfAvailableClient();
			return global::Steamworks.NativeMethods.ISteamMusic_GetVolume(global::Steamworks.CSteamAPIContext.GetSteamMusic());
		}
	}
}
