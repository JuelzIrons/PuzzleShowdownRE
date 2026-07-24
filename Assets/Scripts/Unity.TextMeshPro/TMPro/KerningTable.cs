namespace TMPro
{
	[global::System.Serializable]
	public class KerningTable
	{
		public global::System.Collections.Generic.List<global::TMPro.KerningPair> kerningPairs;

		public KerningTable()
		{
			kerningPairs = new global::System.Collections.Generic.List<global::TMPro.KerningPair>();
		}

		public void AddKerningPair()
		{
			if (kerningPairs.Count == 0)
			{
				kerningPairs.Add(new global::TMPro.KerningPair(0u, 0u, 0f));
				return;
			}
			uint firstGlyph = global::System.Linq.Enumerable.Last(kerningPairs).firstGlyph;
			uint secondGlyph = global::System.Linq.Enumerable.Last(kerningPairs).secondGlyph;
			float xOffset = global::System.Linq.Enumerable.Last(kerningPairs).xOffset;
			kerningPairs.Add(new global::TMPro.KerningPair(firstGlyph, secondGlyph, xOffset));
		}

		public int AddKerningPair(uint first, uint second, float offset)
		{
			if (kerningPairs.FindIndex((global::TMPro.KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
			{
				kerningPairs.Add(new global::TMPro.KerningPair(first, second, offset));
				return 0;
			}
			return -1;
		}

		public int AddGlyphPairAdjustmentRecord(uint first, global::TMPro.GlyphValueRecord_Legacy firstAdjustments, uint second, global::TMPro.GlyphValueRecord_Legacy secondAdjustments)
		{
			if (kerningPairs.FindIndex((global::TMPro.KerningPair item) => item.firstGlyph == first && item.secondGlyph == second) == -1)
			{
				kerningPairs.Add(new global::TMPro.KerningPair(first, firstAdjustments, second, secondAdjustments));
				return 0;
			}
			return -1;
		}

		public void RemoveKerningPair(int left, int right)
		{
			int num = kerningPairs.FindIndex((global::TMPro.KerningPair item) => item.firstGlyph == left && item.secondGlyph == right);
			if (num != -1)
			{
				kerningPairs.RemoveAt(num);
			}
		}

		public void RemoveKerningPair(int index)
		{
			kerningPairs.RemoveAt(index);
		}

		public void SortKerningPairs()
		{
			if (kerningPairs.Count > 0)
			{
				kerningPairs = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.ThenBy(global::System.Linq.Enumerable.OrderBy(kerningPairs, (global::TMPro.KerningPair s) => s.firstGlyph), (global::TMPro.KerningPair s) => s.secondGlyph));
			}
		}
	}
}
