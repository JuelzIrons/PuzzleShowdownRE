namespace Unity.VectorGraphics
{
	internal class SVGStyleSheet
	{
		private global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, global::Unity.VectorGraphics.SVGPropertySheet>> m_Selectors = new global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, global::Unity.VectorGraphics.SVGPropertySheet>>();

		public global::Unity.VectorGraphics.SVGPropertySheet this[string key]
		{
			get
			{
				int num = m_Selectors.FindIndex((global::System.Collections.Generic.KeyValuePair<string, global::Unity.VectorGraphics.SVGPropertySheet> x) => x.Key == key);
				if (num != -1)
				{
					return m_Selectors[num].Value;
				}
				return null;
			}
			set
			{
				global::System.Collections.Generic.KeyValuePair<string, global::Unity.VectorGraphics.SVGPropertySheet> keyValuePair = new global::System.Collections.Generic.KeyValuePair<string, global::Unity.VectorGraphics.SVGPropertySheet>(key, value);
				int num = m_Selectors.FindIndex((global::System.Collections.Generic.KeyValuePair<string, global::Unity.VectorGraphics.SVGPropertySheet> x) => x.Key == key);
				if (num != -1)
				{
					m_Selectors[num] = keyValuePair;
				}
				m_Selectors.Add(keyValuePair);
			}
		}

		public global::System.Collections.Generic.IEnumerable<string> selectors
		{
			get
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.VectorGraphics.SVGPropertySheet> selector in m_Selectors)
				{
					yield return selector.Key;
				}
			}
		}

		public int Count => m_Selectors.Count;

		public void Clear()
		{
			m_Selectors.Clear();
		}
	}
}
