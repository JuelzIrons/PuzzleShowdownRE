namespace Steamworks
{
	[global::System.Serializable]
	public struct CGameID : global::System.IEquatable<global::Steamworks.CGameID>, global::System.IComparable<global::Steamworks.CGameID>
	{
		public enum EGameIDType
		{
			k_EGameIDTypeApp = 0,
			k_EGameIDTypeGameMod = 1,
			k_EGameIDTypeShortcut = 2,
			k_EGameIDTypeP2P = 3
		}

		public ulong m_GameID;

		public CGameID(ulong GameID)
		{
			m_GameID = GameID;
		}

		public CGameID(global::Steamworks.AppId_t nAppID)
		{
			m_GameID = 0uL;
			SetAppID(nAppID);
		}

		public CGameID(global::Steamworks.AppId_t nAppID, uint nModID)
		{
			m_GameID = 0uL;
			SetAppID(nAppID);
			SetType(global::Steamworks.CGameID.EGameIDType.k_EGameIDTypeGameMod);
			SetModID(nModID);
		}

		public bool IsSteamApp()
		{
			return Type() == global::Steamworks.CGameID.EGameIDType.k_EGameIDTypeApp;
		}

		public bool IsMod()
		{
			return Type() == global::Steamworks.CGameID.EGameIDType.k_EGameIDTypeGameMod;
		}

		public bool IsShortcut()
		{
			return Type() == global::Steamworks.CGameID.EGameIDType.k_EGameIDTypeShortcut;
		}

		public bool IsP2PFile()
		{
			return Type() == global::Steamworks.CGameID.EGameIDType.k_EGameIDTypeP2P;
		}

		public global::Steamworks.AppId_t AppID()
		{
			return new global::Steamworks.AppId_t((uint)(m_GameID & 0xFFFFFF));
		}

		public global::Steamworks.CGameID.EGameIDType Type()
		{
			return (global::Steamworks.CGameID.EGameIDType)((m_GameID >> 24) & 0xFF);
		}

		public uint ModID()
		{
			return (uint)((m_GameID >> 32) & 0xFFFFFFFFu);
		}

		public bool IsValid()
		{
			switch (Type())
			{
			case global::Steamworks.CGameID.EGameIDType.k_EGameIDTypeApp:
				return AppID() != global::Steamworks.AppId_t.Invalid;
			case global::Steamworks.CGameID.EGameIDType.k_EGameIDTypeGameMod:
				if (AppID() != global::Steamworks.AppId_t.Invalid)
				{
					return (ModID() & 0x80000000u) != 0;
				}
				return false;
			case global::Steamworks.CGameID.EGameIDType.k_EGameIDTypeShortcut:
				return (ModID() & 0x80000000u) != 0;
			case global::Steamworks.CGameID.EGameIDType.k_EGameIDTypeP2P:
				if (AppID() == global::Steamworks.AppId_t.Invalid)
				{
					return (ModID() & 0x80000000u) != 0;
				}
				return false;
			default:
				return false;
			}
		}

		public void Reset()
		{
			m_GameID = 0uL;
		}

		public void Set(ulong GameID)
		{
			m_GameID = GameID;
		}

		private void SetAppID(global::Steamworks.AppId_t other)
		{
			m_GameID = (m_GameID & 0xFFFFFFFFFF000000uL) | ((ulong)(uint)other & 0xFFFFFFuL);
		}

		private void SetType(global::Steamworks.CGameID.EGameIDType other)
		{
			m_GameID = (m_GameID & 0xFFFFFFFF00FFFFFFuL) | (((ulong)other & 0xFFuL) << 24);
		}

		private void SetModID(uint other)
		{
			m_GameID = (m_GameID & 0xFFFFFFFFu) | (((ulong)other & 0xFFFFFFFFuL) << 32);
		}

		public override string ToString()
		{
			return m_GameID.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.CGameID)
			{
				return this == (global::Steamworks.CGameID)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_GameID.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.CGameID x, global::Steamworks.CGameID y)
		{
			return x.m_GameID == y.m_GameID;
		}

		public static bool operator !=(global::Steamworks.CGameID x, global::Steamworks.CGameID y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.CGameID(ulong value)
		{
			return new global::Steamworks.CGameID(value);
		}

		public static explicit operator ulong(global::Steamworks.CGameID that)
		{
			return that.m_GameID;
		}

		public bool Equals(global::Steamworks.CGameID other)
		{
			return m_GameID == other.m_GameID;
		}

		public int CompareTo(global::Steamworks.CGameID other)
		{
			return m_GameID.CompareTo(other.m_GameID);
		}
	}
}
