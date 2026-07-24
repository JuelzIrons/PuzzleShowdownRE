namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(167)]
	public struct DurationControl_t
	{
		public const int k_iCallback = 167;

		public global::Steamworks.EResult m_eResult;

		public global::Steamworks.AppId_t m_appid;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bApplicable;

		public int m_csecsLast5h;

		public global::Steamworks.EDurationControlProgress m_progress;

		public global::Steamworks.EDurationControlNotification m_notification;

		public int m_csecsToday;

		public int m_csecsRemaining;
	}
}
