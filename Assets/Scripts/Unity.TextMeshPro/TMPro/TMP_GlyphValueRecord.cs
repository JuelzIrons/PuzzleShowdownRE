namespace TMPro
{
	[global::System.Serializable]
	public struct TMP_GlyphValueRecord
	{
		[global::UnityEngine.SerializeField]
		internal float m_XPlacement;

		[global::UnityEngine.SerializeField]
		internal float m_YPlacement;

		[global::UnityEngine.SerializeField]
		internal float m_XAdvance;

		[global::UnityEngine.SerializeField]
		internal float m_YAdvance;

		public float xPlacement
		{
			get
			{
				return m_XPlacement;
			}
			set
			{
				m_XPlacement = value;
			}
		}

		public float yPlacement
		{
			get
			{
				return m_YPlacement;
			}
			set
			{
				m_YPlacement = value;
			}
		}

		public float xAdvance
		{
			get
			{
				return m_XAdvance;
			}
			set
			{
				m_XAdvance = value;
			}
		}

		public float yAdvance
		{
			get
			{
				return m_YAdvance;
			}
			set
			{
				m_YAdvance = value;
			}
		}

		public TMP_GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
		{
			m_XPlacement = xPlacement;
			m_YPlacement = yPlacement;
			m_XAdvance = xAdvance;
			m_YAdvance = yAdvance;
		}

		internal TMP_GlyphValueRecord(global::TMPro.GlyphValueRecord_Legacy valueRecord)
		{
			m_XPlacement = valueRecord.xPlacement;
			m_YPlacement = valueRecord.yPlacement;
			m_XAdvance = valueRecord.xAdvance;
			m_YAdvance = valueRecord.yAdvance;
		}

		internal TMP_GlyphValueRecord(global::UnityEngine.TextCore.LowLevel.GlyphValueRecord valueRecord)
		{
			m_XPlacement = valueRecord.xPlacement;
			m_YPlacement = valueRecord.yPlacement;
			m_XAdvance = valueRecord.xAdvance;
			m_YAdvance = valueRecord.yAdvance;
		}

		public static global::TMPro.TMP_GlyphValueRecord operator +(global::TMPro.TMP_GlyphValueRecord a, global::TMPro.TMP_GlyphValueRecord b)
		{
			global::TMPro.TMP_GlyphValueRecord result = default(global::TMPro.TMP_GlyphValueRecord);
			result.m_XPlacement = a.xPlacement + b.xPlacement;
			result.m_YPlacement = a.yPlacement + b.yPlacement;
			result.m_XAdvance = a.xAdvance + b.xAdvance;
			result.m_YAdvance = a.yAdvance + b.yAdvance;
			return result;
		}
	}
}
