namespace Steamworks
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4)]
	public struct CSteamID : global::System.IEquatable<global::Steamworks.CSteamID>, global::System.IComparable<global::Steamworks.CSteamID>
	{
		public static readonly global::Steamworks.CSteamID Nil = default(global::Steamworks.CSteamID);

		public static readonly global::Steamworks.CSteamID OutofDateGS = new global::Steamworks.CSteamID(new global::Steamworks.AccountID_t(0u), 0u, global::Steamworks.EUniverse.k_EUniverseInvalid, global::Steamworks.EAccountType.k_EAccountTypeInvalid);

		public static readonly global::Steamworks.CSteamID LanModeGS = new global::Steamworks.CSteamID(new global::Steamworks.AccountID_t(0u), 0u, global::Steamworks.EUniverse.k_EUniversePublic, global::Steamworks.EAccountType.k_EAccountTypeInvalid);

		public static readonly global::Steamworks.CSteamID NotInitYetGS = new global::Steamworks.CSteamID(new global::Steamworks.AccountID_t(1u), 0u, global::Steamworks.EUniverse.k_EUniverseInvalid, global::Steamworks.EAccountType.k_EAccountTypeInvalid);

		public static readonly global::Steamworks.CSteamID NonSteamGS = new global::Steamworks.CSteamID(new global::Steamworks.AccountID_t(2u), 0u, global::Steamworks.EUniverse.k_EUniverseInvalid, global::Steamworks.EAccountType.k_EAccountTypeInvalid);

		public ulong m_SteamID;

		public CSteamID(global::Steamworks.AccountID_t unAccountID, global::Steamworks.EUniverse eUniverse, global::Steamworks.EAccountType eAccountType)
		{
			m_SteamID = 0uL;
			Set(unAccountID, eUniverse, eAccountType);
		}

		public CSteamID(global::Steamworks.AccountID_t unAccountID, uint unAccountInstance, global::Steamworks.EUniverse eUniverse, global::Steamworks.EAccountType eAccountType)
		{
			m_SteamID = 0uL;
			InstancedSet(unAccountID, unAccountInstance, eUniverse, eAccountType);
		}

		public CSteamID(ulong ulSteamID)
		{
			m_SteamID = ulSteamID;
		}

		public void Set(global::Steamworks.AccountID_t unAccountID, global::Steamworks.EUniverse eUniverse, global::Steamworks.EAccountType eAccountType)
		{
			SetAccountID(unAccountID);
			SetEUniverse(eUniverse);
			SetEAccountType(eAccountType);
			if (eAccountType == global::Steamworks.EAccountType.k_EAccountTypeClan || eAccountType == global::Steamworks.EAccountType.k_EAccountTypeGameServer)
			{
				SetAccountInstance(0u);
			}
			else
			{
				SetAccountInstance(1u);
			}
		}

		public void InstancedSet(global::Steamworks.AccountID_t unAccountID, uint unInstance, global::Steamworks.EUniverse eUniverse, global::Steamworks.EAccountType eAccountType)
		{
			SetAccountID(unAccountID);
			SetEUniverse(eUniverse);
			SetEAccountType(eAccountType);
			SetAccountInstance(unInstance);
		}

		public void Clear()
		{
			m_SteamID = 0uL;
		}

		public void CreateBlankAnonLogon(global::Steamworks.EUniverse eUniverse)
		{
			SetAccountID(new global::Steamworks.AccountID_t(0u));
			SetEUniverse(eUniverse);
			SetEAccountType(global::Steamworks.EAccountType.k_EAccountTypeAnonGameServer);
			SetAccountInstance(0u);
		}

		public void CreateBlankAnonUserLogon(global::Steamworks.EUniverse eUniverse)
		{
			SetAccountID(new global::Steamworks.AccountID_t(0u));
			SetEUniverse(eUniverse);
			SetEAccountType(global::Steamworks.EAccountType.k_EAccountTypeAnonUser);
			SetAccountInstance(0u);
		}

		public bool BBlankAnonAccount()
		{
			if (GetAccountID() == new global::Steamworks.AccountID_t(0u) && BAnonAccount())
			{
				return GetUnAccountInstance() == 0;
			}
			return false;
		}

		public bool BGameServerAccount()
		{
			if (GetEAccountType() != global::Steamworks.EAccountType.k_EAccountTypeGameServer)
			{
				return GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeAnonGameServer;
			}
			return true;
		}

		public bool BPersistentGameServerAccount()
		{
			return GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeGameServer;
		}

		public bool BAnonGameServerAccount()
		{
			return GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeAnonGameServer;
		}

		public bool BContentServerAccount()
		{
			return GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeContentServer;
		}

		public bool BClanAccount()
		{
			return GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeClan;
		}

		public bool BChatAccount()
		{
			return GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeChat;
		}

		public bool IsLobby()
		{
			if (GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeChat)
			{
				return (GetUnAccountInstance() & 0x40000) != 0;
			}
			return false;
		}

		public bool BIndividualAccount()
		{
			if (GetEAccountType() != global::Steamworks.EAccountType.k_EAccountTypeIndividual)
			{
				return GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeConsoleUser;
			}
			return true;
		}

		public bool BAnonAccount()
		{
			if (GetEAccountType() != global::Steamworks.EAccountType.k_EAccountTypeAnonUser)
			{
				return GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeAnonGameServer;
			}
			return true;
		}

		public bool BAnonUserAccount()
		{
			return GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeAnonUser;
		}

		public bool BConsoleUserAccount()
		{
			return GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeConsoleUser;
		}

		public void SetAccountID(global::Steamworks.AccountID_t other)
		{
			m_SteamID = (m_SteamID & 0xFFFFFFFF00000000uL) | ((ulong)(uint)other & 0xFFFFFFFFuL);
		}

		public void SetAccountInstance(uint other)
		{
			m_SteamID = (m_SteamID & 0xFFF00000FFFFFFFFuL) | (((ulong)other & 0xFFFFFuL) << 32);
		}

		public void SetEAccountType(global::Steamworks.EAccountType other)
		{
			m_SteamID = (m_SteamID & 0xFF0FFFFFFFFFFFFFuL) | (((ulong)other & 0xFuL) << 52);
		}

		public void SetEUniverse(global::Steamworks.EUniverse other)
		{
			m_SteamID = (m_SteamID & 0xFFFFFFFFFFFFFFL) | (((ulong)other & 0xFFuL) << 56);
		}

		public global::Steamworks.AccountID_t GetAccountID()
		{
			return new global::Steamworks.AccountID_t((uint)(m_SteamID & 0xFFFFFFFFu));
		}

		public uint GetUnAccountInstance()
		{
			return (uint)((m_SteamID >> 32) & 0xFFFFF);
		}

		public global::Steamworks.EAccountType GetEAccountType()
		{
			return (global::Steamworks.EAccountType)((m_SteamID >> 52) & 0xF);
		}

		public global::Steamworks.EUniverse GetEUniverse()
		{
			return (global::Steamworks.EUniverse)((m_SteamID >> 56) & 0xFF);
		}

		public bool IsValid()
		{
			if (GetEAccountType() <= global::Steamworks.EAccountType.k_EAccountTypeInvalid || GetEAccountType() >= global::Steamworks.EAccountType.k_EAccountTypeMax)
			{
				return false;
			}
			if (GetEUniverse() <= global::Steamworks.EUniverse.k_EUniverseInvalid || GetEUniverse() >= global::Steamworks.EUniverse.k_EUniverseMax)
			{
				return false;
			}
			if (GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeIndividual && (GetAccountID() == new global::Steamworks.AccountID_t(0u) || GetUnAccountInstance() > 1))
			{
				return false;
			}
			if (GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeClan && (GetAccountID() == new global::Steamworks.AccountID_t(0u) || GetUnAccountInstance() != 0))
			{
				return false;
			}
			if (GetEAccountType() == global::Steamworks.EAccountType.k_EAccountTypeGameServer && GetAccountID() == new global::Steamworks.AccountID_t(0u))
			{
				return false;
			}
			return true;
		}

		public override string ToString()
		{
			return m_SteamID.ToString();
		}

		public override bool Equals(object other)
		{
			if (other is global::Steamworks.CSteamID)
			{
				return this == (global::Steamworks.CSteamID)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_SteamID.GetHashCode();
		}

		public static bool operator ==(global::Steamworks.CSteamID x, global::Steamworks.CSteamID y)
		{
			return x.m_SteamID == y.m_SteamID;
		}

		public static bool operator !=(global::Steamworks.CSteamID x, global::Steamworks.CSteamID y)
		{
			return !(x == y);
		}

		public static explicit operator global::Steamworks.CSteamID(ulong value)
		{
			return new global::Steamworks.CSteamID(value);
		}

		public static explicit operator ulong(global::Steamworks.CSteamID that)
		{
			return that.m_SteamID;
		}

		public bool Equals(global::Steamworks.CSteamID other)
		{
			return m_SteamID == other.m_SteamID;
		}

		public int CompareTo(global::Steamworks.CSteamID other)
		{
			return m_SteamID.CompareTo(other.m_SteamID);
		}
	}
}
