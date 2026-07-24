namespace Steamworks
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	public struct SteamDatagramRelayAuthTicket
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
		private struct ExtraField
		{
			private enum EType
			{
				k_EType_String = 0,
				k_EType_Int = 1,
				k_EType_Fixed64 = 2
			}

			[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 128)]
			public struct FixedBytes128
			{
			}

			[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
			private struct OptionValue
			{
				[global::System.Runtime.InteropServices.FieldOffset(0)]
				public global::Steamworks.SteamDatagramRelayAuthTicket.ExtraField.FixedBytes128 m_szStringValue;

				[global::System.Runtime.InteropServices.FieldOffset(0)]
				private long m_nIntValue;

				[global::System.Runtime.InteropServices.FieldOffset(0)]
				private ulong m_nFixed64Value;
			}

			private global::Steamworks.SteamDatagramRelayAuthTicket.ExtraField.EType m_eType;

			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 28)]
			private byte[] m_szName;

			private global::Steamworks.SteamDatagramRelayAuthTicket.ExtraField.OptionValue m_val;
		}

		private global::Steamworks.SteamNetworkingIdentity m_identityGameserver;

		private global::Steamworks.SteamNetworkingIdentity m_identityAuthorizedClient;

		private uint m_unPublicIP;

		private global::Steamworks.RTime32 m_rtimeTicketExpiry;

		private global::Steamworks.SteamDatagramHostedAddress m_routing;

		private uint m_nAppID;

		private int m_nRestrictToVirtualPort;

		private const int k_nMaxExtraFields = 16;

		private int m_nExtraFields;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16)]
		private global::Steamworks.SteamDatagramRelayAuthTicket.ExtraField[] m_vecExtraFields;

		public void Clear()
		{
		}
	}
}
