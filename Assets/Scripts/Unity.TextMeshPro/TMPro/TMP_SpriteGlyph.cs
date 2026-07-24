namespace TMPro
{
	[global::System.Serializable]
	public class TMP_SpriteGlyph : global::UnityEngine.TextCore.Glyph
	{
		public global::UnityEngine.Sprite sprite;

		public TMP_SpriteGlyph()
		{
		}

		public TMP_SpriteGlyph(uint index, global::UnityEngine.TextCore.GlyphMetrics metrics, global::UnityEngine.TextCore.GlyphRect glyphRect, float scale, int atlasIndex)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
		}

		public TMP_SpriteGlyph(uint index, global::UnityEngine.TextCore.GlyphMetrics metrics, global::UnityEngine.TextCore.GlyphRect glyphRect, float scale, int atlasIndex, global::UnityEngine.Sprite sprite)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
			this.sprite = sprite;
		}
	}
}
