namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	[global::Steamworks.CallbackIdentity(1023)]
	public struct FileDetailsResult_t
	{
		public const int k_iCallback = 1023;

		public global::Steamworks.EResult m_eResult;

		public ulong m_ulFileSize;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 20)]
		public byte[] m_FileSHA;

		public uint m_unFlags;
	}
}
