namespace TMPro
{
	[global::System.Serializable]
	public struct GlyphValueRecord_Legacy
	{
		public float xPlacement;

		public float yPlacement;

		public float xAdvance;

		public float yAdvance;

		internal GlyphValueRecord_Legacy(global::UnityEngine.TextCore.LowLevel.GlyphValueRecord valueRecord)
		{
			xPlacement = valueRecord.xPlacement;
			yPlacement = valueRecord.yPlacement;
			xAdvance = valueRecord.xAdvance;
			yAdvance = valueRecord.yAdvance;
		}

		public static global::TMPro.GlyphValueRecord_Legacy operator +(global::TMPro.GlyphValueRecord_Legacy a, global::TMPro.GlyphValueRecord_Legacy b)
		{
			global::TMPro.GlyphValueRecord_Legacy result = default(global::TMPro.GlyphValueRecord_Legacy);
			result.xPlacement = a.xPlacement + b.xPlacement;
			result.yPlacement = a.yPlacement + b.yPlacement;
			result.xAdvance = a.xAdvance + b.xAdvance;
			result.yAdvance = a.yAdvance + b.yAdvance;
			return result;
		}
	}
}
