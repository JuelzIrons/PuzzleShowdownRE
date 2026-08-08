namespace Steamworks
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4, Size = 372)]
	public class gameserveritem_t
	{
		public global::Steamworks.servernetadr_t m_NetAdr;

		public int m_nPing;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bHadSuccessfulResponse;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bDoNotRefresh;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szGameDir;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szMap;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szGameDescription;

		public uint m_nAppID;

		public int m_nPlayers;

		public int m_nMaxPlayers;

		public int m_nBotPlayers;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bPassword;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.I1)]
		public bool m_bSecure;

		public uint m_ulTimeLastPlayed;

		public int m_nServerVersion;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szServerName;

		[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szGameTags;

		public global::Steamworks.CSteamID m_steamID;

		public string GetGameDir()
		{
			return global::System.Text.Encoding.UTF8.GetString(m_szGameDir, 0, global::System.Array.IndexOf(m_szGameDir, (byte)0));
		}

		public void SetGameDir(string dir)
		{
			m_szGameDir = global::System.Text.Encoding.UTF8.GetBytes(dir + "\0");
		}

		public string GetMap()
		{
			return global::System.Text.Encoding.UTF8.GetString(m_szMap, 0, global::System.Array.IndexOf(m_szMap, (byte)0));
		}

		public void SetMap(string map)
		{
			m_szMap = global::System.Text.Encoding.UTF8.GetBytes(map + "\0");
		}

		public string GetGameDescription()
		{
			return global::System.Text.Encoding.UTF8.GetString(m_szGameDescription, 0, global::System.Array.IndexOf(m_szGameDescription, (byte)0));
		}

		public void SetGameDescription(string desc)
		{
			m_szGameDescription = global::System.Text.Encoding.UTF8.GetBytes(desc + "\0");
		}

		public string GetServerName()
		{
			if (m_szServerName[0] == 0)
			{
				return m_NetAdr.GetConnectionAddressString();
			}
			return global::System.Text.Encoding.UTF8.GetString(m_szServerName, 0, global::System.Array.IndexOf(m_szServerName, (byte)0));
		}

		public void SetServerName(string name)
		{
			m_szServerName = global::System.Text.Encoding.UTF8.GetBytes(name + "\0");
		}

		public string GetGameTags()
		{
			return global::System.Text.Encoding.UTF8.GetString(m_szGameTags, 0, global::System.Array.IndexOf(m_szGameTags, (byte)0));
		}

		public void SetGameTags(string tags)
		{
			m_szGameTags = global::System.Text.Encoding.UTF8.GetBytes(tags + "\0");
		}
	}
}
