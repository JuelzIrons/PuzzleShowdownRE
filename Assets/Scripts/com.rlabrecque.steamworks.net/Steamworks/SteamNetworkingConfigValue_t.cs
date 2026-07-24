namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamNetworkingConfigValue_t
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		public struct OptionValue
		{
			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public int m_int32;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public long m_int64;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public float m_float;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public global::System.IntPtr m_string;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public global::System.IntPtr m_functionPtr;
		}

		public global::Steamworks.ESteamNetworkingConfigValue m_eValue;

		public global::Steamworks.ESteamNetworkingConfigDataType m_eDataType;

		public global::Steamworks.SteamNetworkingConfigValue_t.OptionValue m_val;
	}
}
