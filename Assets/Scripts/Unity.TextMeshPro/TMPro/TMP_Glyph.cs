namespace TMPro
{
	[global::System.Serializable]
	public class TMP_Glyph : global::TMPro.TMP_TextElement_Legacy
	{
		public static global::TMPro.TMP_Glyph Clone(global::TMPro.TMP_Glyph source)
		{
			return new global::TMPro.TMP_Glyph
			{
				id = source.id,
				x = source.x,
				y = source.y,
				width = source.width,
				height = source.height,
				xOffset = source.xOffset,
				yOffset = source.yOffset,
				xAdvance = source.xAdvance,
				scale = source.scale
			};
		}
	}
}
