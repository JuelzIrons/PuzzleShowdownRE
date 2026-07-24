namespace TMPro
{
	[global::System.Serializable]
	public struct LigatureSubstitutionRecord
	{
		[global::UnityEngine.SerializeField]
		private uint[] m_ComponentGlyphIDs;

		[global::UnityEngine.SerializeField]
		private uint m_LigatureGlyphID;

		public uint[] componentGlyphIDs
		{
			get
			{
				return m_ComponentGlyphIDs;
			}
			set
			{
				m_ComponentGlyphIDs = value;
			}
		}

		public uint ligatureGlyphID
		{
			get
			{
				return m_LigatureGlyphID;
			}
			set
			{
				m_LigatureGlyphID = value;
			}
		}

		public static bool operator ==(global::TMPro.LigatureSubstitutionRecord lhs, global::TMPro.LigatureSubstitutionRecord rhs)
		{
			if (lhs.ligatureGlyphID != rhs.m_LigatureGlyphID)
			{
				return false;
			}
			int num = lhs.m_ComponentGlyphIDs.Length;
			if (num != rhs.m_ComponentGlyphIDs.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (lhs.m_ComponentGlyphIDs[i] != rhs.m_ComponentGlyphIDs[i])
				{
					return false;
				}
			}
			return true;
		}

		public static bool operator !=(global::TMPro.LigatureSubstitutionRecord lhs, global::TMPro.LigatureSubstitutionRecord rhs)
		{
			return !(lhs == rhs);
		}
	}
}
