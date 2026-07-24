namespace TMPro
{
	[global::System.Serializable]
	[global::UnityEngine.ExcludeFromPreset]
	public class TMP_StyleSheet : global::UnityEngine.ScriptableObject
	{
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::TMPro.TMP_Style> m_StyleList = new global::System.Collections.Generic.List<global::TMPro.TMP_Style>(1);

		private global::System.Collections.Generic.Dictionary<int, global::TMPro.TMP_Style> m_StyleLookupDictionary;

		internal global::System.Collections.Generic.List<global::TMPro.TMP_Style> styles => m_StyleList;

		private void Reset()
		{
			LoadStyleDictionaryInternal();
		}

		public global::TMPro.TMP_Style GetStyle(int hashCode)
		{
			if (m_StyleLookupDictionary == null)
			{
				LoadStyleDictionaryInternal();
			}
			if (m_StyleLookupDictionary.TryGetValue(hashCode, out var value))
			{
				return value;
			}
			return null;
		}

		public global::TMPro.TMP_Style GetStyle(string name)
		{
			if (m_StyleLookupDictionary == null)
			{
				LoadStyleDictionaryInternal();
			}
			int hashCode = global::TMPro.TMP_TextParsingUtilities.GetHashCode(name);
			if (m_StyleLookupDictionary.TryGetValue(hashCode, out var value))
			{
				return value;
			}
			return null;
		}

		public void RefreshStyles()
		{
			LoadStyleDictionaryInternal();
		}

		private void LoadStyleDictionaryInternal()
		{
			if (m_StyleLookupDictionary == null)
			{
				m_StyleLookupDictionary = new global::System.Collections.Generic.Dictionary<int, global::TMPro.TMP_Style>();
			}
			else
			{
				m_StyleLookupDictionary.Clear();
			}
			for (int i = 0; i < m_StyleList.Count; i++)
			{
				m_StyleList[i].RefreshStyle();
				if (!m_StyleLookupDictionary.ContainsKey(m_StyleList[i].hashCode))
				{
					m_StyleLookupDictionary.Add(m_StyleList[i].hashCode, m_StyleList[i]);
				}
			}
			int hashCode = global::TMPro.TMP_TextParsingUtilities.GetHashCode("Normal");
			if (!m_StyleLookupDictionary.ContainsKey(hashCode))
			{
				global::TMPro.TMP_Style tMP_Style = new global::TMPro.TMP_Style("Normal", string.Empty, string.Empty);
				m_StyleList.Add(tMP_Style);
				m_StyleLookupDictionary.Add(hashCode, tMP_Style);
			}
		}
	}
}
