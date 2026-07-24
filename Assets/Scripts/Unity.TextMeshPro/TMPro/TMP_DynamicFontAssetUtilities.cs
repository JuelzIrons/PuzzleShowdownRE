namespace TMPro
{
	internal class TMP_DynamicFontAssetUtilities
	{
		public struct FontReference
		{
			public string familyName;

			public string styleName;

			public int faceIndex;

			public string filePath;

			public ulong hashCode;

			public FontReference(string fontFilePath, string faceNameAndStyle, int index)
			{
				familyName = null;
				styleName = null;
				faceIndex = index;
				uint num = 0u;
				uint num2 = 0u;
				filePath = fontFilePath;
				int length = faceNameAndStyle.Length;
				char[] array = new char[length];
				int num3 = 0;
				int length2 = 0;
				for (int i = 0; i < length; i++)
				{
					char c = faceNameAndStyle[i];
					switch (num3)
					{
					case 0:
						if (i + 2 < length && c == ' ' && faceNameAndStyle[i + 1] == '-' && faceNameAndStyle[i + 2] == ' ')
						{
							num3 = 1;
							familyName = new string(array, 0, length2);
							i += 2;
							length2 = 0;
						}
						else
						{
							num = ((num << 5) + num) ^ global::TMPro.TMP_TextUtilities.ToUpperFast(c);
							array[length2++] = c;
						}
						break;
					case 1:
						num2 = ((num2 << 5) + num2) ^ global::TMPro.TMP_TextUtilities.ToUpperFast(c);
						array[length2++] = c;
						if (i + 1 == length)
						{
							styleName = new string(array, 0, length2);
						}
						break;
					}
				}
				hashCode = ((ulong)num2 << 32) | num;
			}
		}

		private static global::TMPro.TMP_DynamicFontAssetUtilities s_Instance = new global::TMPro.TMP_DynamicFontAssetUtilities();

		private global::System.Collections.Generic.Dictionary<ulong, global::TMPro.TMP_DynamicFontAssetUtilities.FontReference> s_SystemFontLookup;

		private string[] s_SystemFontPaths;

		private uint s_RegularStyleNameHashCode = 1291372090u;

		private void InitializeSystemFontReferenceCache()
		{
			if (s_SystemFontLookup == null)
			{
				s_SystemFontLookup = new global::System.Collections.Generic.Dictionary<ulong, global::TMPro.TMP_DynamicFontAssetUtilities.FontReference>();
			}
			else
			{
				s_SystemFontLookup.Clear();
			}
			if (s_SystemFontPaths == null)
			{
				s_SystemFontPaths = global::UnityEngine.Font.GetPathsToOSFonts();
			}
			for (int i = 0; i < s_SystemFontPaths.Length; i++)
			{
				global::UnityEngine.TextCore.LowLevel.FontEngineError fontEngineError = global::UnityEngine.TextCore.LowLevel.FontEngine.LoadFontFace(s_SystemFontPaths[i]);
				if (fontEngineError != global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
				{
					global::UnityEngine.Debug.LogWarning("Error [" + fontEngineError.ToString() + "] trying to load the font at path [" + s_SystemFontPaths[i] + "].");
					continue;
				}
				string[] fontFaces = global::UnityEngine.TextCore.LowLevel.FontEngine.GetFontFaces();
				for (int j = 0; j < fontFaces.Length; j++)
				{
					global::TMPro.TMP_DynamicFontAssetUtilities.FontReference value = new global::TMPro.TMP_DynamicFontAssetUtilities.FontReference(s_SystemFontPaths[i], fontFaces[j], j);
					if (!s_SystemFontLookup.ContainsKey(value.hashCode))
					{
						s_SystemFontLookup.Add(value.hashCode, value);
						global::UnityEngine.Debug.Log("[" + i + "] Family Name [" + value.familyName + "]   Style Name [" + value.styleName + "]   Index [" + value.faceIndex + "]   HashCode [" + value.hashCode + "]    Path [" + value.filePath + "].");
					}
				}
				global::UnityEngine.TextCore.LowLevel.FontEngine.UnloadFontFace();
			}
		}

		public static bool TryGetSystemFontReference(string familyName, out global::TMPro.TMP_DynamicFontAssetUtilities.FontReference fontRef)
		{
			return s_Instance.TryGetSystemFontReferenceInternal(familyName, null, out fontRef);
		}

		public static bool TryGetSystemFontReference(string familyName, string styleName, out global::TMPro.TMP_DynamicFontAssetUtilities.FontReference fontRef)
		{
			return s_Instance.TryGetSystemFontReferenceInternal(familyName, styleName, out fontRef);
		}

		private bool TryGetSystemFontReferenceInternal(string familyName, string styleName, out global::TMPro.TMP_DynamicFontAssetUtilities.FontReference fontRef)
		{
			if (s_SystemFontLookup == null)
			{
				InitializeSystemFontReferenceCache();
			}
			fontRef = default(global::TMPro.TMP_DynamicFontAssetUtilities.FontReference);
			uint hashCodeCaseInSensitive = global::TMPro.TMP_TextUtilities.GetHashCodeCaseInSensitive(familyName);
			uint num = (string.IsNullOrEmpty(styleName) ? s_RegularStyleNameHashCode : global::TMPro.TMP_TextUtilities.GetHashCodeCaseInSensitive(styleName));
			ulong key = ((ulong)num << 32) | hashCodeCaseInSensitive;
			if (s_SystemFontLookup.ContainsKey(key))
			{
				fontRef = s_SystemFontLookup[key];
				return true;
			}
			if (num != s_RegularStyleNameHashCode)
			{
				return false;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::TMPro.TMP_DynamicFontAssetUtilities.FontReference> item in s_SystemFontLookup)
			{
				if (item.Value.familyName == familyName)
				{
					fontRef = item.Value;
					return true;
				}
			}
			return false;
		}
	}
}
