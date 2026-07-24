namespace TMPro
{
	[global::System.Serializable]
	[global::UnityEngine.ExcludeFromPreset]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/TextMeshPro/Settings.html")]
	public class TMP_Settings : global::UnityEngine.ScriptableObject
	{
		public class LineBreakingTable
		{
			public global::System.Collections.Generic.HashSet<uint> leadingCharacters;

			public global::System.Collections.Generic.HashSet<uint> followingCharacters;
		}

		private static global::TMPro.TMP_Settings s_Instance;

		[global::UnityEngine.SerializeField]
		internal string assetVersion;

		internal static string s_CurrentAssetVersion = "2";

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_enableWordWrapping")]
		[global::UnityEngine.SerializeField]
		private global::TMPro.TextWrappingModes m_TextWrappingMode;

		[global::UnityEngine.SerializeField]
		private bool m_enableKerning;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.TextCore.OTL_FeatureTag> m_ActiveFontFeatures = new global::System.Collections.Generic.List<global::UnityEngine.TextCore.OTL_FeatureTag> { (global::UnityEngine.TextCore.OTL_FeatureTag)0u };

		[global::UnityEngine.SerializeField]
		private bool m_enableExtraPadding;

		[global::UnityEngine.SerializeField]
		private bool m_enableTintAllSprites;

		[global::UnityEngine.SerializeField]
		private bool m_enableParseEscapeCharacters;

		[global::UnityEngine.SerializeField]
		private bool m_EnableRaycastTarget = true;

		[global::UnityEngine.SerializeField]
		private bool m_GetFontFeaturesAtRuntime = true;

		[global::UnityEngine.SerializeField]
		private int m_missingGlyphCharacter;

		[global::UnityEngine.SerializeField]
		private bool m_ClearDynamicDataOnBuild = true;

		[global::UnityEngine.SerializeField]
		private bool m_warningsDisabled;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_FontAsset m_defaultFontAsset;

		[global::UnityEngine.SerializeField]
		private string m_defaultFontAssetPath;

		[global::UnityEngine.SerializeField]
		private float m_defaultFontSize;

		[global::UnityEngine.SerializeField]
		private float m_defaultAutoSizeMinRatio;

		[global::UnityEngine.SerializeField]
		private float m_defaultAutoSizeMaxRatio;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector2 m_defaultTextMeshProTextContainerSize;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector2 m_defaultTextMeshProUITextContainerSize;

		[global::UnityEngine.SerializeField]
		private bool m_autoSizeTextContainer;

		[global::UnityEngine.SerializeField]
		private bool m_IsTextObjectScaleStatic;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::TMPro.TMP_FontAsset> m_fallbackFontAssets;

		[global::UnityEngine.SerializeField]
		private bool m_matchMaterialPreset;

		[global::UnityEngine.SerializeField]
		private bool m_HideSubTextObjects = true;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_SpriteAsset m_defaultSpriteAsset;

		[global::UnityEngine.SerializeField]
		private string m_defaultSpriteAssetPath;

		[global::UnityEngine.SerializeField]
		private bool m_enableEmojiSupport;

		[global::UnityEngine.SerializeField]
		private uint m_MissingCharacterSpriteUnicode;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::TMPro.TMP_Asset> m_EmojiFallbackTextAssets;

		[global::UnityEngine.SerializeField]
		private string m_defaultColorGradientPresetsPath;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_StyleSheet m_defaultStyleSheet;

		[global::UnityEngine.SerializeField]
		private string m_StyleSheetsResourcePath;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.TextAsset m_leadingCharacters;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.TextAsset m_followingCharacters;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_Settings.LineBreakingTable m_linebreakingRules;

		[global::UnityEngine.SerializeField]
		private bool m_UseModernHangulLineBreakingRules;

		public static string version => "1.4.0";

		public static global::TMPro.TextWrappingModes textWrappingMode => instance.m_TextWrappingMode;

		[global::System.Obsolete("The \"enableKerning\" property has been deprecated. Use the \"fontFeatures\" property to control what features are enabled by default on newly created text components.")]
		public static bool enableKerning
		{
			get
			{
				if (instance.m_ActiveFontFeatures != null)
				{
					return instance.m_ActiveFontFeatures.Contains(global::UnityEngine.TextCore.OTL_FeatureTag.kern);
				}
				return instance.m_enableKerning;
			}
		}

		public static global::System.Collections.Generic.List<global::UnityEngine.TextCore.OTL_FeatureTag> fontFeatures => instance.m_ActiveFontFeatures;

		public static bool enableExtraPadding => instance.m_enableExtraPadding;

		public static bool enableTintAllSprites => instance.m_enableTintAllSprites;

		public static bool enableParseEscapeCharacters => instance.m_enableParseEscapeCharacters;

		public static bool enableRaycastTarget => instance.m_EnableRaycastTarget;

		public static bool getFontFeaturesAtRuntime => instance.m_GetFontFeaturesAtRuntime;

		public static int missingGlyphCharacter
		{
			get
			{
				return instance.m_missingGlyphCharacter;
			}
			set
			{
				instance.m_missingGlyphCharacter = value;
			}
		}

		public static bool clearDynamicDataOnBuild => instance.m_ClearDynamicDataOnBuild;

		public static bool warningsDisabled => instance.m_warningsDisabled;

		public static global::TMPro.TMP_FontAsset defaultFontAsset
		{
			get
			{
				return instance.m_defaultFontAsset;
			}
			set
			{
				instance.m_defaultFontAsset = value;
			}
		}

		public static string defaultFontAssetPath => instance.m_defaultFontAssetPath;

		public static float defaultFontSize => instance.m_defaultFontSize;

		public static float defaultTextAutoSizingMinRatio => instance.m_defaultAutoSizeMinRatio;

		public static float defaultTextAutoSizingMaxRatio => instance.m_defaultAutoSizeMaxRatio;

		public static global::UnityEngine.Vector2 defaultTextMeshProTextContainerSize => instance.m_defaultTextMeshProTextContainerSize;

		public static global::UnityEngine.Vector2 defaultTextMeshProUITextContainerSize => instance.m_defaultTextMeshProUITextContainerSize;

		public static bool autoSizeTextContainer => instance.m_autoSizeTextContainer;

		public static bool isTextObjectScaleStatic
		{
			get
			{
				return instance.m_IsTextObjectScaleStatic;
			}
			set
			{
				instance.m_IsTextObjectScaleStatic = value;
			}
		}

		public static global::System.Collections.Generic.List<global::TMPro.TMP_FontAsset> fallbackFontAssets
		{
			get
			{
				return instance.m_fallbackFontAssets;
			}
			set
			{
				instance.m_fallbackFontAssets = value;
			}
		}

		public static bool matchMaterialPreset => instance.m_matchMaterialPreset;

		public static bool hideSubTextObjects => instance.m_HideSubTextObjects;

		public static global::TMPro.TMP_SpriteAsset defaultSpriteAsset
		{
			get
			{
				return instance.m_defaultSpriteAsset;
			}
			set
			{
				instance.m_defaultSpriteAsset = value;
			}
		}

		public static string defaultSpriteAssetPath => instance.m_defaultSpriteAssetPath;

		public static bool enableEmojiSupport
		{
			get
			{
				return instance.m_enableEmojiSupport;
			}
			set
			{
				instance.m_enableEmojiSupport = value;
			}
		}

		public static uint missingCharacterSpriteUnicode
		{
			get
			{
				return instance.m_MissingCharacterSpriteUnicode;
			}
			set
			{
				instance.m_MissingCharacterSpriteUnicode = value;
			}
		}

		public static global::System.Collections.Generic.List<global::TMPro.TMP_Asset> emojiFallbackTextAssets
		{
			get
			{
				return instance.m_EmojiFallbackTextAssets;
			}
			set
			{
				instance.m_EmojiFallbackTextAssets = value;
			}
		}

		public static string defaultColorGradientPresetsPath => instance.m_defaultColorGradientPresetsPath;

		public static global::TMPro.TMP_StyleSheet defaultStyleSheet
		{
			get
			{
				return instance.m_defaultStyleSheet;
			}
			set
			{
				instance.m_defaultStyleSheet = value;
			}
		}

		public static string styleSheetsResourcePath => instance.m_StyleSheetsResourcePath;

		public static global::UnityEngine.TextAsset leadingCharacters => instance.m_leadingCharacters;

		public static global::UnityEngine.TextAsset followingCharacters => instance.m_followingCharacters;

		public static global::TMPro.TMP_Settings.LineBreakingTable linebreakingRules
		{
			get
			{
				if (instance.m_linebreakingRules == null)
				{
					LoadLinebreakingRules();
				}
				return instance.m_linebreakingRules;
			}
		}

		public static bool useModernHangulLineBreakingRules
		{
			get
			{
				return instance.m_UseModernHangulLineBreakingRules;
			}
			set
			{
				instance.m_UseModernHangulLineBreakingRules = value;
			}
		}

		public static global::TMPro.TMP_Settings instance
		{
			get
			{
				if (isTMPSettingsNull)
				{
					s_Instance = global::UnityEngine.Resources.Load<global::TMPro.TMP_Settings>("TMP Settings");
					if (!isTMPSettingsNull && s_Instance.m_ActiveFontFeatures.Count == 1 && s_Instance.m_ActiveFontFeatures[0] == (global::UnityEngine.TextCore.OTL_FeatureTag)0u)
					{
						s_Instance.m_ActiveFontFeatures.Clear();
						if (s_Instance.m_enableKerning)
						{
							s_Instance.m_ActiveFontFeatures.Add(global::UnityEngine.TextCore.OTL_FeatureTag.kern);
						}
					}
				}
				return s_Instance;
			}
		}

		internal static bool isTMPSettingsNull => s_Instance == null;

		internal void SetAssetVersion()
		{
			assetVersion = s_CurrentAssetVersion;
		}

		public static global::TMPro.TMP_Settings LoadDefaultSettings()
		{
			if (s_Instance == null)
			{
				global::TMPro.TMP_Settings tMP_Settings = global::UnityEngine.Resources.Load<global::TMPro.TMP_Settings>("TMP Settings");
				if (tMP_Settings != null)
				{
					s_Instance = tMP_Settings;
				}
			}
			return s_Instance;
		}

		public static global::TMPro.TMP_Settings GetSettings()
		{
			if (instance == null)
			{
				return null;
			}
			return instance;
		}

		public static global::TMPro.TMP_FontAsset GetFontAsset()
		{
			if (instance == null)
			{
				return null;
			}
			return instance.m_defaultFontAsset;
		}

		public static global::TMPro.TMP_SpriteAsset GetSpriteAsset()
		{
			if (instance == null)
			{
				return null;
			}
			return instance.m_defaultSpriteAsset;
		}

		public static global::TMPro.TMP_StyleSheet GetStyleSheet()
		{
			if (instance == null)
			{
				return null;
			}
			return instance.m_defaultStyleSheet;
		}

		public static void LoadLinebreakingRules()
		{
			if (!(instance == null))
			{
				if (s_Instance.m_linebreakingRules == null)
				{
					s_Instance.m_linebreakingRules = new global::TMPro.TMP_Settings.LineBreakingTable();
				}
				s_Instance.m_linebreakingRules.leadingCharacters = GetCharacters(s_Instance.m_leadingCharacters);
				s_Instance.m_linebreakingRules.followingCharacters = GetCharacters(s_Instance.m_followingCharacters);
			}
		}

		private static global::System.Collections.Generic.HashSet<uint> GetCharacters(global::UnityEngine.TextAsset file)
		{
			global::System.Collections.Generic.HashSet<uint> hashSet = new global::System.Collections.Generic.HashSet<uint>();
			string text = file.text;
			for (int i = 0; i < text.Length; i++)
			{
				hashSet.Add(text[i]);
			}
			return hashSet;
		}
	}
}
