namespace TMPro
{
	[global::System.Serializable]
	public struct MultipleSubstitutionRecord
	{
		[global::UnityEngine.SerializeField]
		private uint m_TargetGlyphID;

		[global::UnityEngine.SerializeField]
		private uint[] m_SubstituteGlyphIDs;

		public uint targetGlyphID
		{
			get
			{
				return m_TargetGlyphID;
			}
			set
			{
				m_TargetGlyphID = value;
			}
		}

		public uint[] substituteGlyphIDs
		{
			get
			{
				return m_SubstituteGlyphIDs;
			}
			set
			{
				m_SubstituteGlyphIDs = value;
			}
		}
	}
}
