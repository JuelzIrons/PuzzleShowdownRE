namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(714)]
	public struct GamepadTextInputDismissed_t
	{
		public const int k_iCallback = 714;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bSubmitted;

		public uint m_unSubmittedText;

		public global::Steamworks.AppId_t m_unAppID;
	}
}
