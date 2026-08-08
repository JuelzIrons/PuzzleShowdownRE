namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(2803)]
	public struct SteamInputConfigurationLoaded_t
	{
		public const int k_iCallback = 2803;

		public global::Steamworks.AppId_t m_unAppID;

		public global::Steamworks.InputHandle_t m_ulDeviceHandle;

		public global::Steamworks.CSteamID m_ulMappingCreator;

		public uint m_unMajorRevision;

		public uint m_unMinorRevision;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bUsesSteamInputAPI;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bUsesGamepadAPI;
	}
}
