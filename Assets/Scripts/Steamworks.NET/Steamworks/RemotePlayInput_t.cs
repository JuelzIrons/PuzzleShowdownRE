namespace Steamworks
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	public struct RemotePlayInput_t
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 56)]
		public struct OptionValue
		{
			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public global::Steamworks.RemotePlayInputMouseMotion_t m_MouseMotion;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public global::Steamworks.ERemotePlayMouseButton m_eMouseButton;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public global::Steamworks.RemotePlayInputMouseWheel_t m_MouseWheel;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public global::Steamworks.RemotePlayInputKey_t m_Key;
		}

		public global::Steamworks.RemotePlaySessionID_t m_unSessionID;

		public global::Steamworks.ERemotePlayInputType m_eType;

		public global::Steamworks.RemotePlayInput_t.OptionValue m_val;
	}
}
