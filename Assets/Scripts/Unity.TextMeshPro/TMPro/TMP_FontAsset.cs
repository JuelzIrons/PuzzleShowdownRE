namespace TMPro
{
	[global::System.Serializable]
	[global::UnityEngine.ExcludeFromPreset]
	public class TMP_FontAsset : global::TMPro.TMP_Asset
	{
		[global::UnityEngine.SerializeField]
		internal string m_SourceFontFileGUID;

		[global::UnityEngine.SerializeField]
		internal global::TMPro.FontAssetCreationSettings m_CreationSettings;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Font m_SourceFontFile;

		[global::UnityEngine.SerializeField]
		private string m_SourceFontFilePath;

		[global::UnityEngine.SerializeField]
		private global::TMPro.AtlasPopulationMode m_AtlasPopulationMode;

		[global::UnityEngine.SerializeField]
		internal bool InternalDynamicOS;

		private int m_FamilyNameHashCode;

		private int m_StyleNameHashCode;

		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::UnityEngine.TextCore.Glyph> m_GlyphTable = new global::System.Collections.Generic.List<global::UnityEngine.TextCore.Glyph>();

		internal global::System.Collections.Generic.Dictionary<uint, global::UnityEngine.TextCore.Glyph> m_GlyphLookupDictionary;

		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::TMPro.TMP_Character> m_CharacterTable = new global::System.Collections.Generic.List<global::TMPro.TMP_Character>();

		internal global::System.Collections.Generic.Dictionary<uint, global::TMPro.TMP_Character> m_CharacterLookupDictionary;

		internal global::UnityEngine.Texture2D m_AtlasTexture;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Texture2D[] m_AtlasTextures;

		[global::UnityEngine.SerializeField]
		internal int m_AtlasTextureIndex;

		[global::UnityEngine.SerializeField]
		private bool m_IsMultiAtlasTexturesEnabled;

		[global::UnityEngine.SerializeField]
		private bool m_GetFontFeatures = true;

		[global::UnityEngine.SerializeField]
		private bool m_ClearDynamicDataOnBuild;

		[global::UnityEngine.SerializeField]
		internal int m_AtlasWidth;

		[global::UnityEngine.SerializeField]
		internal int m_AtlasHeight;

		[global::UnityEngine.SerializeField]
		internal int m_AtlasPadding;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.TextCore.LowLevel.GlyphRenderMode m_AtlasRenderMode;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.TextCore.GlyphRect> m_UsedGlyphRects;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.TextCore.GlyphRect> m_FreeGlyphRects;

		[global::UnityEngine.SerializeField]
		internal global::TMPro.TMP_FontFeatureTable m_FontFeatureTable = new global::TMPro.TMP_FontFeatureTable();

		[global::UnityEngine.SerializeField]
		internal bool m_ShouldReimportFontFeatures;

		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::TMPro.TMP_FontAsset> m_FallbackFontAssetTable;

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_FontWeightPair[] m_FontWeightTable = new global::TMPro.TMP_FontWeightPair[10];

		[global::UnityEngine.SerializeField]
		private global::TMPro.TMP_FontWeightPair[] fontWeights;

		public float normalStyle;

		public float normalSpacingOffset;

		public float boldStyle = 0.75f;

		public float boldSpacing = 7f;

		public byte italicStyle = 35;

		public byte tabSize = 10;

		internal bool IsFontAssetLookupTablesDirty;

		[global::UnityEngine.SerializeField]
		private global::TMPro.FaceInfo_Legacy m_fontInfo;

		[global::UnityEngine.SerializeField]
		internal global::System.Collections.Generic.List<global::TMPro.TMP_Glyph> m_glyphInfoList;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_kerningInfo")]
		internal global::TMPro.KerningTable m_KerningTable = new global::TMPro.KerningTable();

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::TMPro.TMP_FontAsset> fallbackFontAssets;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Texture2D atlas;

		private static readonly global::System.Collections.Generic.List<global::System.WeakReference<global::TMPro.TMP_FontAsset>> s_CallbackInstances = new global::System.Collections.Generic.List<global::System.WeakReference<global::TMPro.TMP_FontAsset>>();

		private static global::Unity.Profiling.ProfilerMarker k_ReadFontAssetDefinitionMarker = new global::Unity.Profiling.ProfilerMarker("TMP.ReadFontAssetDefinition");

		private static global::Unity.Profiling.ProfilerMarker k_AddSynthesizedCharactersMarker = new global::Unity.Profiling.ProfilerMarker("TMP.AddSynthesizedCharacters");

		private static global::Unity.Profiling.ProfilerMarker k_TryAddGlyphMarker = new global::Unity.Profiling.ProfilerMarker("TMP.TryAddGlyph");

		private static global::Unity.Profiling.ProfilerMarker k_TryAddCharacterMarker = new global::Unity.Profiling.ProfilerMarker("TMP.TryAddCharacter");

		private static global::Unity.Profiling.ProfilerMarker k_TryAddCharactersMarker = new global::Unity.Profiling.ProfilerMarker("TMP.TryAddCharacters");

		private static global::Unity.Profiling.ProfilerMarker k_UpdateLigatureSubstitutionRecordsMarker = new global::Unity.Profiling.ProfilerMarker("TMP.UpdateLigatureSubstitutionRecords");

		private static global::Unity.Profiling.ProfilerMarker k_UpdateGlyphAdjustmentRecordsMarker = new global::Unity.Profiling.ProfilerMarker("TMP.UpdateGlyphAdjustmentRecords");

		private static global::Unity.Profiling.ProfilerMarker k_UpdateDiacriticalMarkAdjustmentRecordsMarker = new global::Unity.Profiling.ProfilerMarker("TMP.UpdateDiacriticalAdjustmentRecords");

		private static global::Unity.Profiling.ProfilerMarker k_ClearFontAssetDataMarker = new global::Unity.Profiling.ProfilerMarker("TMP.ClearFontAssetData");

		private static global::Unity.Profiling.ProfilerMarker k_UpdateFontAssetDataMarker = new global::Unity.Profiling.ProfilerMarker("TMP.UpdateFontAssetData");

		private static string s_DefaultMaterialSuffix = " Atlas Material";

		private static global::System.Collections.Generic.HashSet<int> k_SearchedFontAssetLookup;

		private static global::System.Collections.Generic.List<global::TMPro.TMP_FontAsset> k_FontAssets_FontFeaturesUpdateQueue = new global::System.Collections.Generic.List<global::TMPro.TMP_FontAsset>();

		private static global::System.Collections.Generic.HashSet<int> k_FontAssets_FontFeaturesUpdateQueueLookup = new global::System.Collections.Generic.HashSet<int>();

		private static global::System.Collections.Generic.List<global::UnityEngine.Texture2D> k_FontAssets_AtlasTexturesUpdateQueue = new global::System.Collections.Generic.List<global::UnityEngine.Texture2D>();

		private static global::System.Collections.Generic.HashSet<int> k_FontAssets_AtlasTexturesUpdateQueueLookup = new global::System.Collections.Generic.HashSet<int>();

		private global::System.Collections.Generic.List<global::UnityEngine.TextCore.Glyph> m_GlyphsToRender = new global::System.Collections.Generic.List<global::UnityEngine.TextCore.Glyph>();

		private global::System.Collections.Generic.List<global::UnityEngine.TextCore.Glyph> m_GlyphsRendered = new global::System.Collections.Generic.List<global::UnityEngine.TextCore.Glyph>();

		private global::System.Collections.Generic.List<uint> m_GlyphIndexList = new global::System.Collections.Generic.List<uint>();

		private global::System.Collections.Generic.List<uint> m_GlyphIndexListNewlyAdded = new global::System.Collections.Generic.List<uint>();

		internal global::System.Collections.Generic.List<uint> m_GlyphsToAdd = new global::System.Collections.Generic.List<uint>();

		internal global::System.Collections.Generic.HashSet<uint> m_GlyphsToAddLookup = new global::System.Collections.Generic.HashSet<uint>();

		internal global::System.Collections.Generic.List<global::TMPro.TMP_Character> m_CharactersToAdd = new global::System.Collections.Generic.List<global::TMPro.TMP_Character>();

		internal global::System.Collections.Generic.HashSet<uint> m_CharactersToAddLookup = new global::System.Collections.Generic.HashSet<uint>();

		internal global::System.Collections.Generic.List<uint> s_MissingCharacterList = new global::System.Collections.Generic.List<uint>();

		internal global::System.Collections.Generic.HashSet<uint> m_MissingUnicodesFromFontFile = new global::System.Collections.Generic.HashSet<uint>();

		internal static uint[] k_GlyphIndexArray;

		public global::TMPro.FontAssetCreationSettings creationSettings
		{
			get
			{
				return m_CreationSettings;
			}
			set
			{
				m_CreationSettings = value;
			}
		}

		public global::UnityEngine.Font sourceFontFile
		{
			get
			{
				return m_SourceFontFile;
			}
			internal set
			{
				m_SourceFontFile = value;
			}
		}

		public global::TMPro.AtlasPopulationMode atlasPopulationMode
		{
			get
			{
				return m_AtlasPopulationMode;
			}
			set
			{
				m_AtlasPopulationMode = value;
			}
		}

		internal int familyNameHashCode
		{
			get
			{
				if (m_FamilyNameHashCode == 0)
				{
					m_FamilyNameHashCode = global::TMPro.TMP_TextUtilities.GetHashCode(m_FaceInfo.familyName);
				}
				return m_FamilyNameHashCode;
			}
			set
			{
				m_FamilyNameHashCode = value;
			}
		}

		internal int styleNameHashCode
		{
			get
			{
				if (m_StyleNameHashCode == 0)
				{
					m_StyleNameHashCode = global::TMPro.TMP_TextUtilities.GetHashCode(m_FaceInfo.styleName);
				}
				return m_StyleNameHashCode;
			}
			set
			{
				m_StyleNameHashCode = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.TextCore.Glyph> glyphTable
		{
			get
			{
				return m_GlyphTable;
			}
			internal set
			{
				m_GlyphTable = value;
			}
		}

		public global::System.Collections.Generic.Dictionary<uint, global::UnityEngine.TextCore.Glyph> glyphLookupTable
		{
			get
			{
				if (m_GlyphLookupDictionary == null)
				{
					ReadFontAssetDefinition();
				}
				return m_GlyphLookupDictionary;
			}
		}

		public global::System.Collections.Generic.List<global::TMPro.TMP_Character> characterTable
		{
			get
			{
				return m_CharacterTable;
			}
			internal set
			{
				m_CharacterTable = value;
			}
		}

		public global::System.Collections.Generic.Dictionary<uint, global::TMPro.TMP_Character> characterLookupTable
		{
			get
			{
				if (m_CharacterLookupDictionary == null)
				{
					ReadFontAssetDefinition();
				}
				return m_CharacterLookupDictionary;
			}
		}

		public global::UnityEngine.Texture2D atlasTexture
		{
			get
			{
				if (m_AtlasTexture == null)
				{
					m_AtlasTexture = atlasTextures[0];
				}
				return m_AtlasTexture;
			}
		}

		public global::UnityEngine.Texture2D[] atlasTextures
		{
			get
			{
				_ = m_AtlasTextures;
				return m_AtlasTextures;
			}
			set
			{
				m_AtlasTextures = value;
			}
		}

		public int atlasTextureCount => m_AtlasTextureIndex + 1;

		public bool isMultiAtlasTexturesEnabled
		{
			get
			{
				return m_IsMultiAtlasTexturesEnabled;
			}
			set
			{
				m_IsMultiAtlasTexturesEnabled = value;
			}
		}

		public bool getFontFeatures
		{
			get
			{
				return m_GetFontFeatures;
			}
			set
			{
				m_GetFontFeatures = value;
			}
		}

		internal bool clearDynamicDataOnBuild
		{
			get
			{
				return m_ClearDynamicDataOnBuild;
			}
			set
			{
				m_ClearDynamicDataOnBuild = value;
			}
		}

		public int atlasWidth
		{
			get
			{
				return m_AtlasWidth;
			}
			internal set
			{
				m_AtlasWidth = value;
			}
		}

		public int atlasHeight
		{
			get
			{
				return m_AtlasHeight;
			}
			internal set
			{
				m_AtlasHeight = value;
			}
		}

		public int atlasPadding
		{
			get
			{
				return m_AtlasPadding;
			}
			internal set
			{
				m_AtlasPadding = value;
			}
		}

		public global::UnityEngine.TextCore.LowLevel.GlyphRenderMode atlasRenderMode
		{
			get
			{
				return m_AtlasRenderMode;
			}
			internal set
			{
				m_AtlasRenderMode = value;
			}
		}

		internal global::System.Collections.Generic.List<global::UnityEngine.TextCore.GlyphRect> usedGlyphRects
		{
			get
			{
				return m_UsedGlyphRects;
			}
			set
			{
				m_UsedGlyphRects = value;
			}
		}

		internal global::System.Collections.Generic.List<global::UnityEngine.TextCore.GlyphRect> freeGlyphRects
		{
			get
			{
				return m_FreeGlyphRects;
			}
			set
			{
				m_FreeGlyphRects = value;
			}
		}

		public global::TMPro.TMP_FontFeatureTable fontFeatureTable
		{
			get
			{
				return m_FontFeatureTable;
			}
			internal set
			{
				m_FontFeatureTable = value;
			}
		}

		public global::System.Collections.Generic.List<global::TMPro.TMP_FontAsset> fallbackFontAssetTable
		{
			get
			{
				return m_FallbackFontAssetTable;
			}
			set
			{
				m_FallbackFontAssetTable = value;
			}
		}

		public global::TMPro.TMP_FontWeightPair[] fontWeightTable
		{
			get
			{
				return m_FontWeightTable;
			}
			internal set
			{
				m_FontWeightTable = value;
			}
		}

		[global::System.Obsolete("The fontInfo property and underlying type is now obsolete. Please use the faceInfo property and FaceInfo type instead.")]
		public global::TMPro.FaceInfo_Legacy fontInfo => m_fontInfo;

		public static global::TMPro.TMP_FontAsset CreateFontAsset(string familyName, string styleName, int pointSize = 90)
		{
			if (global::UnityEngine.TextCore.LowLevel.FontEngine.TryGetSystemFontReference(familyName, styleName, out var fontRef))
			{
				return CreateFontAsset(fontRef.filePath, fontRef.faceIndex, pointSize, 9, global::UnityEngine.TextCore.LowLevel.GlyphRenderMode.SDFAA, 1024, 1024, global::TMPro.AtlasPopulationMode.DynamicOS);
			}
			global::UnityEngine.Debug.Log("Unable to find a font file with the specified Family Name [" + familyName + "] and Style [" + styleName + "].");
			return null;
		}

		public static global::TMPro.TMP_FontAsset CreateFontAsset(string fontFilePath, int faceIndex, int samplingPointSize, int atlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, int atlasWidth, int atlasHeight)
		{
			return CreateFontAsset(fontFilePath, faceIndex, samplingPointSize, atlasPadding, renderMode, atlasWidth, atlasHeight, global::TMPro.AtlasPopulationMode.Dynamic);
		}

		private static global::TMPro.TMP_FontAsset CreateFontAsset(string fontFilePath, int faceIndex, int samplingPointSize, int atlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, global::TMPro.AtlasPopulationMode atlasPopulationMode, bool enableMultiAtlasSupport = true)
		{
			if (global::UnityEngine.TextCore.LowLevel.FontEngine.LoadFontFace(fontFilePath, samplingPointSize, faceIndex) != global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
			{
				global::UnityEngine.Debug.Log("Unable to load font face from [" + fontFilePath + "].");
				return null;
			}
			global::TMPro.TMP_FontAsset tMP_FontAsset = CreateFontAssetInstance(null, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
			tMP_FontAsset.m_SourceFontFilePath = fontFilePath;
			return tMP_FontAsset;
		}

		public static global::TMPro.TMP_FontAsset CreateFontAsset(global::UnityEngine.Font font)
		{
			return CreateFontAsset(font, 90, 9, global::UnityEngine.TextCore.LowLevel.GlyphRenderMode.SDFAA, 1024, 1024);
		}

		public static global::TMPro.TMP_FontAsset CreateFontAsset(global::UnityEngine.Font font, int samplingPointSize, int atlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, global::TMPro.AtlasPopulationMode atlasPopulationMode = global::TMPro.AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			return CreateFontAsset(font, 0, samplingPointSize, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
		}

		private static global::TMPro.TMP_FontAsset CreateFontAsset(global::UnityEngine.Font font, int faceIndex, int samplingPointSize, int atlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, global::TMPro.AtlasPopulationMode atlasPopulationMode = global::TMPro.AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			if (global::UnityEngine.TextCore.LowLevel.FontEngine.LoadFontFace(font, samplingPointSize, faceIndex) != global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
			{
				global::UnityEngine.Debug.LogWarning("Unable to load font face for [" + font.name + "]. Make sure \"Include Font Data\" is enabled in the Font Import Settings.", font);
				return null;
			}
			return CreateFontAssetInstance(font, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
		}

		private static global::TMPro.TMP_FontAsset CreateFontAssetInstance(global::UnityEngine.Font font, int atlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, global::TMPro.AtlasPopulationMode atlasPopulationMode, bool enableMultiAtlasSupport)
		{
			global::TMPro.TMP_FontAsset tMP_FontAsset = global::UnityEngine.ScriptableObject.CreateInstance<global::TMPro.TMP_FontAsset>();
			tMP_FontAsset.m_Version = "1.1.0";
			tMP_FontAsset.faceInfo = global::UnityEngine.TextCore.LowLevel.FontEngine.GetFaceInfo();
			if (atlasPopulationMode == global::TMPro.AtlasPopulationMode.Dynamic && font != null)
			{
				tMP_FontAsset.sourceFontFile = font;
			}
			tMP_FontAsset.atlasPopulationMode = atlasPopulationMode;
			tMP_FontAsset.clearDynamicDataOnBuild = global::TMPro.TMP_Settings.clearDynamicDataOnBuild;
			tMP_FontAsset.atlasWidth = atlasWidth;
			tMP_FontAsset.atlasHeight = atlasHeight;
			tMP_FontAsset.atlasPadding = atlasPadding;
			tMP_FontAsset.atlasRenderMode = renderMode;
			tMP_FontAsset.atlasTextures = new global::UnityEngine.Texture2D[1];
			global::UnityEngine.TextureFormat textureFormat = (((renderMode & (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)65536) != (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)65536) ? global::UnityEngine.TextureFormat.Alpha8 : global::UnityEngine.TextureFormat.RGBA32);
			global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(1, 1, textureFormat, mipChain: false);
			tMP_FontAsset.atlasTextures[0] = texture2D;
			tMP_FontAsset.isMultiAtlasTexturesEnabled = enableMultiAtlasSupport;
			int num;
			if ((renderMode & (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)16) == (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)16)
			{
				global::UnityEngine.Material material = null;
				num = 0;
				material = ((textureFormat != global::UnityEngine.TextureFormat.Alpha8) ? new global::UnityEngine.Material(global::UnityEngine.Shader.Find("TextMeshPro/Sprite")) : new global::UnityEngine.Material(global::TMPro.ShaderUtilities.ShaderRef_MobileBitmap));
				material.SetTexture(global::TMPro.ShaderUtilities.ID_MainTex, texture2D);
				material.SetFloat(global::TMPro.ShaderUtilities.ID_TextureWidth, atlasWidth);
				material.SetFloat(global::TMPro.ShaderUtilities.ID_TextureHeight, atlasHeight);
				tMP_FontAsset.material = material;
			}
			else
			{
				num = 1;
				global::UnityEngine.Material material2 = new global::UnityEngine.Material(global::TMPro.ShaderUtilities.ShaderRef_MobileSDF);
				material2.SetTexture(global::TMPro.ShaderUtilities.ID_MainTex, texture2D);
				material2.SetFloat(global::TMPro.ShaderUtilities.ID_TextureWidth, atlasWidth);
				material2.SetFloat(global::TMPro.ShaderUtilities.ID_TextureHeight, atlasHeight);
				material2.SetFloat(global::TMPro.ShaderUtilities.ID_GradientScale, atlasPadding + num);
				material2.SetFloat(global::TMPro.ShaderUtilities.ID_WeightNormal, tMP_FontAsset.normalStyle);
				material2.SetFloat(global::TMPro.ShaderUtilities.ID_WeightBold, tMP_FontAsset.boldStyle);
				tMP_FontAsset.material = material2;
			}
			tMP_FontAsset.freeGlyphRects = new global::System.Collections.Generic.List<global::UnityEngine.TextCore.GlyphRect>(8)
			{
				new global::UnityEngine.TextCore.GlyphRect(0, 0, atlasWidth - num, atlasHeight - num)
			};
			tMP_FontAsset.usedGlyphRects = new global::System.Collections.Generic.List<global::UnityEngine.TextCore.GlyphRect>(8);
			tMP_FontAsset.ReadFontAssetDefinition();
			return tMP_FontAsset;
		}

		private void RegisterCallbackInstance(global::TMPro.TMP_FontAsset instance)
		{
			for (int i = 0; i < s_CallbackInstances.Count; i++)
			{
				if (s_CallbackInstances[i].TryGetTarget(out var target) && target == instance)
				{
					return;
				}
			}
			for (int j = 0; j < s_CallbackInstances.Count; j++)
			{
				if (!s_CallbackInstances[j].TryGetTarget(out var _))
				{
					s_CallbackInstances[j] = new global::System.WeakReference<global::TMPro.TMP_FontAsset>(instance);
					return;
				}
			}
			s_CallbackInstances.Add(new global::System.WeakReference<global::TMPro.TMP_FontAsset>(this));
		}

		private void OnDestroy()
		{
			DestroyAtlasTextures();
			global::UnityEngine.Object.DestroyImmediate(m_Material);
		}

		public void ReadFontAssetDefinition()
		{
			InitializeDictionaryLookupTables();
			AddSynthesizedCharactersAndFaceMetrics();
			if (m_FaceInfo.capLine == 0f && m_CharacterLookupDictionary.ContainsKey(88u))
			{
				uint glyphIndex = m_CharacterLookupDictionary[88u].glyphIndex;
				m_FaceInfo.capLine = m_GlyphLookupDictionary[glyphIndex].metrics.horizontalBearingY;
			}
			if (m_FaceInfo.meanLine == 0f && m_CharacterLookupDictionary.ContainsKey(120u))
			{
				uint glyphIndex2 = m_CharacterLookupDictionary[120u].glyphIndex;
				m_FaceInfo.meanLine = m_GlyphLookupDictionary[glyphIndex2].metrics.horizontalBearingY;
			}
			if (m_FaceInfo.scale == 0f)
			{
				m_FaceInfo.scale = 1f;
			}
			if (m_FaceInfo.strikethroughOffset == 0f)
			{
				m_FaceInfo.strikethroughOffset = m_FaceInfo.capLine / 2.5f;
			}
			if (m_AtlasPadding == 0 && base.material.HasProperty(global::TMPro.ShaderUtilities.ID_GradientScale))
			{
				m_AtlasPadding = (int)base.material.GetFloat(global::TMPro.ShaderUtilities.ID_GradientScale) - 1;
			}
			if (m_FaceInfo.unitsPerEM == 0 && atlasPopulationMode != global::TMPro.AtlasPopulationMode.Static)
			{
				if (!global::Unity.Jobs.LowLevel.Unsafe.JobsUtility.IsExecutingJob)
				{
					m_FaceInfo.unitsPerEM = global::UnityEngine.TextCore.LowLevel.FontEngine.GetFaceInfo().unitsPerEM;
					global::UnityEngine.Debug.Log("Font Asset [" + base.name + "] Units Per EM set to " + m_FaceInfo.unitsPerEM + ". Please commit the newly serialized value.");
				}
				else
				{
					global::UnityEngine.Debug.LogError("Font Asset [" + base.name + "] is missing Units Per EM. Please select the 'Reset FaceInfo' menu item on Font Asset [" + base.name + "] to ensure proper serialization.");
				}
			}
			base.hashCode = global::TMPro.TMP_TextUtilities.GetHashCode(base.name);
			familyNameHashCode = global::TMPro.TMP_TextUtilities.GetHashCode(m_FaceInfo.familyName);
			styleNameHashCode = global::TMPro.TMP_TextUtilities.GetHashCode(m_FaceInfo.styleName);
			base.materialHashCode = global::TMPro.TMP_TextUtilities.GetSimpleHashCode(base.name + s_DefaultMaterialSuffix);
			global::TMPro.TMP_ResourceManager.AddFontAsset(this);
			IsFontAssetLookupTablesDirty = false;
			RegisterCallbackInstance(this);
		}

		internal void InitializeDictionaryLookupTables()
		{
			InitializeGlyphLookupDictionary();
			InitializeCharacterLookupDictionary();
			if ((m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.Dynamic || m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.DynamicOS) && m_ShouldReimportFontFeatures)
			{
				ImportFontFeatures();
			}
			InitializeLigatureSubstitutionLookupDictionary();
			InitializeGlyphPaidAdjustmentRecordsLookupDictionary();
			InitializeMarkToBaseAdjustmentRecordsLookupDictionary();
			InitializeMarkToMarkAdjustmentRecordsLookupDictionary();
		}

		internal void InitializeGlyphLookupDictionary()
		{
			if (m_GlyphLookupDictionary == null)
			{
				m_GlyphLookupDictionary = new global::System.Collections.Generic.Dictionary<uint, global::UnityEngine.TextCore.Glyph>();
			}
			else
			{
				m_GlyphLookupDictionary.Clear();
			}
			if (m_GlyphIndexList == null)
			{
				m_GlyphIndexList = new global::System.Collections.Generic.List<uint>();
			}
			else
			{
				m_GlyphIndexList.Clear();
			}
			if (m_GlyphIndexListNewlyAdded == null)
			{
				m_GlyphIndexListNewlyAdded = new global::System.Collections.Generic.List<uint>();
			}
			else
			{
				m_GlyphIndexListNewlyAdded.Clear();
			}
			int count = m_GlyphTable.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.TextCore.Glyph glyph = m_GlyphTable[i];
				uint index = glyph.index;
				if (!m_GlyphLookupDictionary.ContainsKey(index))
				{
					m_GlyphLookupDictionary.Add(index, glyph);
					m_GlyphIndexList.Add(index);
				}
			}
		}

		internal void InitializeCharacterLookupDictionary()
		{
			if (m_CharacterLookupDictionary == null)
			{
				m_CharacterLookupDictionary = new global::System.Collections.Generic.Dictionary<uint, global::TMPro.TMP_Character>();
			}
			else
			{
				m_CharacterLookupDictionary.Clear();
			}
			for (int i = 0; i < m_CharacterTable.Count; i++)
			{
				global::TMPro.TMP_Character tMP_Character = m_CharacterTable[i];
				uint unicode = tMP_Character.unicode;
				uint glyphIndex = tMP_Character.glyphIndex;
				if (!m_CharacterLookupDictionary.ContainsKey(unicode))
				{
					m_CharacterLookupDictionary.Add(unicode, tMP_Character);
					tMP_Character.textAsset = this;
					tMP_Character.glyph = m_GlyphLookupDictionary[glyphIndex];
				}
			}
			if (m_MissingUnicodesFromFontFile != null)
			{
				m_MissingUnicodesFromFontFile.Clear();
			}
		}

		internal void ClearFallbackCharacterTable()
		{
			global::System.Collections.Generic.List<uint> list = new global::System.Collections.Generic.List<uint>();
			foreach (global::System.Collections.Generic.KeyValuePair<uint, global::TMPro.TMP_Character> item in m_CharacterLookupDictionary)
			{
				if (item.Value.textAsset != this)
				{
					list.Add(item.Key);
				}
			}
			foreach (uint item2 in list)
			{
				m_CharacterLookupDictionary.Remove(item2);
			}
		}

		internal void InitializeLigatureSubstitutionLookupDictionary()
		{
			if (m_FontFeatureTable.m_LigatureSubstitutionRecordLookup == null)
			{
				m_FontFeatureTable.m_LigatureSubstitutionRecordLookup = new global::System.Collections.Generic.Dictionary<uint, global::System.Collections.Generic.List<global::TMPro.LigatureSubstitutionRecord>>();
			}
			else
			{
				m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Clear();
			}
			global::System.Collections.Generic.List<global::TMPro.LigatureSubstitutionRecord> ligatureSubstitutionRecords = m_FontFeatureTable.m_LigatureSubstitutionRecords;
			if (ligatureSubstitutionRecords == null)
			{
				return;
			}
			for (int i = 0; i < ligatureSubstitutionRecords.Count; i++)
			{
				global::TMPro.LigatureSubstitutionRecord item = ligatureSubstitutionRecords[i];
				if (item.componentGlyphIDs != null && item.componentGlyphIDs.Length != 0)
				{
					uint key = item.componentGlyphIDs[0];
					if (!m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.ContainsKey(key))
					{
						m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Add(key, new global::System.Collections.Generic.List<global::TMPro.LigatureSubstitutionRecord> { item });
					}
					else
					{
						m_FontFeatureTable.m_LigatureSubstitutionRecordLookup[key].Add(item);
					}
				}
			}
		}

		internal void InitializeGlyphPaidAdjustmentRecordsLookupDictionary()
		{
			if (m_KerningTable != null && m_KerningTable.kerningPairs != null && m_KerningTable.kerningPairs.Count > 0)
			{
				UpgradeGlyphAdjustmentTableToFontFeatureTable();
			}
			if (m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup == null)
			{
				m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup = new global::System.Collections.Generic.Dictionary<uint, global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord>();
			}
			else
			{
				m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Clear();
			}
			global::System.Collections.Generic.List<global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords = m_FontFeatureTable.m_GlyphPairAdjustmentRecords;
			if (glyphPairAdjustmentRecords == null)
			{
				return;
			}
			for (int i = 0; i < glyphPairAdjustmentRecords.Count; i++)
			{
				global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord value = glyphPairAdjustmentRecords[i];
				uint key = (value.secondAdjustmentRecord.glyphIndex << 16) | value.firstAdjustmentRecord.glyphIndex;
				if (!m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(key))
				{
					m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(key, value);
				}
			}
		}

		internal void InitializeMarkToBaseAdjustmentRecordsLookupDictionary()
		{
			if (m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup == null)
			{
				m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup = new global::System.Collections.Generic.Dictionary<uint, global::TMPro.MarkToBaseAdjustmentRecord>();
			}
			else
			{
				m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.Clear();
			}
			global::System.Collections.Generic.List<global::TMPro.MarkToBaseAdjustmentRecord> markToBaseAdjustmentRecords = m_FontFeatureTable.m_MarkToBaseAdjustmentRecords;
			if (markToBaseAdjustmentRecords == null)
			{
				return;
			}
			for (int i = 0; i < markToBaseAdjustmentRecords.Count; i++)
			{
				global::TMPro.MarkToBaseAdjustmentRecord value = markToBaseAdjustmentRecords[i];
				uint key = (value.markGlyphID << 16) | value.baseGlyphID;
				if (!m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.ContainsKey(key))
				{
					m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.Add(key, value);
				}
			}
		}

		internal void InitializeMarkToMarkAdjustmentRecordsLookupDictionary()
		{
			if (m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup == null)
			{
				m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup = new global::System.Collections.Generic.Dictionary<uint, global::TMPro.MarkToMarkAdjustmentRecord>();
			}
			else
			{
				m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.Clear();
			}
			global::System.Collections.Generic.List<global::TMPro.MarkToMarkAdjustmentRecord> markToMarkAdjustmentRecords = m_FontFeatureTable.m_MarkToMarkAdjustmentRecords;
			if (markToMarkAdjustmentRecords == null)
			{
				return;
			}
			for (int i = 0; i < markToMarkAdjustmentRecords.Count; i++)
			{
				global::TMPro.MarkToMarkAdjustmentRecord value = markToMarkAdjustmentRecords[i];
				uint key = (value.combiningMarkGlyphID << 16) | value.baseMarkGlyphID;
				if (!m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.ContainsKey(key))
				{
					m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.Add(key, value);
				}
			}
		}

		internal void AddSynthesizedCharactersAndFaceMetrics()
		{
			bool flag = false;
			if (m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.Dynamic || m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.DynamicOS)
			{
				flag = LoadFontFace() == global::UnityEngine.TextCore.LowLevel.FontEngineError.Success;
				if (!flag && !InternalDynamicOS && global::TMPro.TMP_Settings.warningsDisabled)
				{
					global::UnityEngine.Debug.LogWarning("Unable to load font face for [" + base.name + "] font asset.", this);
				}
			}
			AddSynthesizedCharacter(3u, flag, addImmediately: true);
			AddSynthesizedCharacter(9u, flag, addImmediately: true);
			AddSynthesizedCharacter(10u, flag);
			AddSynthesizedCharacter(11u, flag);
			AddSynthesizedCharacter(13u, flag);
			AddSynthesizedCharacter(1564u, flag);
			AddSynthesizedCharacter(8203u, flag);
			AddSynthesizedCharacter(8206u, flag);
			AddSynthesizedCharacter(8207u, flag);
			AddSynthesizedCharacter(8232u, flag);
			AddSynthesizedCharacter(8233u, flag);
			AddSynthesizedCharacter(8288u, flag);
		}

		private void AddSynthesizedCharacter(uint unicode, bool isFontFaceLoaded, bool addImmediately = false)
		{
			if (m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return;
			}
			if (isFontFaceLoaded && global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(unicode) != 0)
			{
				if (addImmediately)
				{
					global::UnityEngine.TextCore.LowLevel.GlyphLoadFlags flags = (((m_AtlasRenderMode & (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)4) == (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)4) ? (global::UnityEngine.TextCore.LowLevel.GlyphLoadFlags.LOAD_NO_HINTING | global::UnityEngine.TextCore.LowLevel.GlyphLoadFlags.LOAD_NO_BITMAP) : global::UnityEngine.TextCore.LowLevel.GlyphLoadFlags.LOAD_NO_BITMAP);
					if (global::UnityEngine.TextCore.LowLevel.FontEngine.TryGetGlyphWithUnicodeValue(unicode, flags, out var glyph))
					{
						m_CharacterLookupDictionary.Add(unicode, new global::TMPro.TMP_Character(unicode, this, glyph));
					}
				}
			}
			else
			{
				global::UnityEngine.TextCore.Glyph glyph = new global::UnityEngine.TextCore.Glyph(0u, new global::UnityEngine.TextCore.GlyphMetrics(0f, 0f, 0f, 0f, 0f), global::UnityEngine.TextCore.GlyphRect.zero, 1f, 0);
				m_CharacterLookupDictionary.Add(unicode, new global::TMPro.TMP_Character(unicode, this, glyph));
			}
		}

		internal void AddCharacterToLookupCache(uint unicode, global::TMPro.TMP_Character character, global::TMPro.FontStyles fontStyle = global::TMPro.FontStyles.Normal, global::TMPro.FontWeight fontWeight = global::TMPro.FontWeight.Regular, bool isAlternativeTypeface = false)
		{
			uint key = unicode;
			if (fontStyle != global::TMPro.FontStyles.Normal || fontWeight != global::TMPro.FontWeight.Regular)
			{
				key = (((uint)((isAlternativeTypeface ? 128 : 0) | ((int)fontStyle << 4)) | ((uint)fontWeight / 100u)) << 24) | unicode;
			}
			m_CharacterLookupDictionary.TryAdd(key, character);
		}

		internal global::UnityEngine.TextCore.LowLevel.FontEngineError LoadFontFace()
		{
			if (m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.Dynamic)
			{
				if (global::UnityEngine.TextCore.LowLevel.FontEngine.LoadFontFace(m_SourceFontFile, m_FaceInfo.pointSize, m_FaceInfo.faceIndex) == global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
				{
					return global::UnityEngine.TextCore.LowLevel.FontEngineError.Success;
				}
				if (!string.IsNullOrEmpty(m_SourceFontFilePath))
				{
					return global::UnityEngine.TextCore.LowLevel.FontEngine.LoadFontFace(m_SourceFontFilePath, m_FaceInfo.pointSize, m_FaceInfo.faceIndex);
				}
				return global::UnityEngine.TextCore.LowLevel.FontEngineError.Invalid_Face;
			}
			return global::UnityEngine.TextCore.LowLevel.FontEngine.LoadFontFace(m_FaceInfo.familyName, m_FaceInfo.styleName, m_FaceInfo.pointSize);
		}

		internal void SortCharacterTable()
		{
			if (m_CharacterTable != null && m_CharacterTable.Count > 0)
			{
				m_CharacterTable = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.OrderBy(m_CharacterTable, (global::TMPro.TMP_Character c) => c.unicode));
			}
		}

		internal void SortGlyphTable()
		{
			if (m_GlyphTable != null && m_GlyphTable.Count > 0)
			{
				m_GlyphTable = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.OrderBy(m_GlyphTable, (global::UnityEngine.TextCore.Glyph c) => c.index));
			}
		}

		internal void SortFontFeatureTable()
		{
			m_FontFeatureTable.SortGlyphPairAdjustmentRecords();
			m_FontFeatureTable.SortMarkToBaseAdjustmentRecords();
			m_FontFeatureTable.SortMarkToMarkAdjustmentRecords();
		}

		internal void SortAllTables()
		{
			SortGlyphTable();
			SortCharacterTable();
			SortFontFeatureTable();
		}

		public bool HasCharacter(int character)
		{
			if (characterLookupTable == null)
			{
				return false;
			}
			return m_CharacterLookupDictionary.ContainsKey((uint)character);
		}

		public bool HasCharacter(char character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			if (characterLookupTable == null)
			{
				return false;
			}
			if (m_CharacterLookupDictionary.ContainsKey(character))
			{
				return true;
			}
			if (tryAddCharacter && (m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.Dynamic || m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.DynamicOS) && TryAddCharacterInternal(character, out var _))
			{
				return true;
			}
			if (searchFallbacks)
			{
				if (k_SearchedFontAssetLookup == null)
				{
					k_SearchedFontAssetLookup = new global::System.Collections.Generic.HashSet<int>();
				}
				else
				{
					k_SearchedFontAssetLookup.Clear();
				}
				k_SearchedFontAssetLookup.Add(GetInstanceID());
				if (fallbackFontAssetTable != null && fallbackFontAssetTable.Count > 0)
				{
					for (int i = 0; i < fallbackFontAssetTable.Count && fallbackFontAssetTable[i] != null; i++)
					{
						global::TMPro.TMP_FontAsset tMP_FontAsset = fallbackFontAssetTable[i];
						int item = tMP_FontAsset.GetInstanceID();
						if (k_SearchedFontAssetLookup.Add(item) && tMP_FontAsset.HasCharacter_Internal(character, searchFallbacks: true, tryAddCharacter))
						{
							return true;
						}
					}
				}
				if (global::TMPro.TMP_Settings.fallbackFontAssets != null && global::TMPro.TMP_Settings.fallbackFontAssets.Count > 0)
				{
					for (int j = 0; j < global::TMPro.TMP_Settings.fallbackFontAssets.Count && global::TMPro.TMP_Settings.fallbackFontAssets[j] != null; j++)
					{
						global::TMPro.TMP_FontAsset tMP_FontAsset2 = global::TMPro.TMP_Settings.fallbackFontAssets[j];
						int item2 = tMP_FontAsset2.GetInstanceID();
						if (k_SearchedFontAssetLookup.Add(item2) && tMP_FontAsset2.HasCharacter_Internal(character, searchFallbacks: true, tryAddCharacter))
						{
							return true;
						}
					}
				}
				if (global::TMPro.TMP_Settings.defaultFontAsset != null)
				{
					global::TMPro.TMP_FontAsset defaultFontAsset = global::TMPro.TMP_Settings.defaultFontAsset;
					int item3 = defaultFontAsset.GetInstanceID();
					if (k_SearchedFontAssetLookup.Add(item3) && defaultFontAsset.HasCharacter_Internal(character, searchFallbacks: true, tryAddCharacter))
					{
						return true;
					}
				}
			}
			return false;
		}

		private bool HasCharacter_Internal(uint character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			if (m_CharacterLookupDictionary == null)
			{
				ReadFontAssetDefinition();
				if (m_CharacterLookupDictionary == null)
				{
					return false;
				}
			}
			if (m_CharacterLookupDictionary.ContainsKey(character))
			{
				return true;
			}
			if (tryAddCharacter && (atlasPopulationMode == global::TMPro.AtlasPopulationMode.Dynamic || m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.DynamicOS) && TryAddCharacterInternal(character, out var _))
			{
				return true;
			}
			if (searchFallbacks)
			{
				if (fallbackFontAssetTable == null || fallbackFontAssetTable.Count == 0)
				{
					return false;
				}
				for (int i = 0; i < fallbackFontAssetTable.Count && fallbackFontAssetTable[i] != null; i++)
				{
					global::TMPro.TMP_FontAsset tMP_FontAsset = fallbackFontAssetTable[i];
					int item = tMP_FontAsset.GetInstanceID();
					if (k_SearchedFontAssetLookup.Add(item) && tMP_FontAsset.HasCharacter_Internal(character, searchFallbacks: true, tryAddCharacter))
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool HasCharacters(string text, out global::System.Collections.Generic.List<char> missingCharacters)
		{
			if (characterLookupTable == null)
			{
				missingCharacters = null;
				return false;
			}
			missingCharacters = new global::System.Collections.Generic.List<char>();
			for (int i = 0; i < text.Length; i++)
			{
				uint codePoint = global::TMPro.TMP_FontAssetUtilities.GetCodePoint(text, ref i);
				if (!m_CharacterLookupDictionary.ContainsKey(codePoint))
				{
					missingCharacters.Add((char)codePoint);
				}
			}
			if (missingCharacters.Count == 0)
			{
				return true;
			}
			return false;
		}

		public bool HasCharacters(string text, out uint[] missingCharacters, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			missingCharacters = null;
			if (characterLookupTable == null)
			{
				return false;
			}
			s_MissingCharacterList.Clear();
			for (int i = 0; i < text.Length; i++)
			{
				bool flag = true;
				uint codePoint = global::TMPro.TMP_FontAssetUtilities.GetCodePoint(text, ref i);
				if (m_CharacterLookupDictionary.ContainsKey(codePoint) || (tryAddCharacter && (atlasPopulationMode == global::TMPro.AtlasPopulationMode.Dynamic || m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.DynamicOS) && TryAddCharacterInternal(codePoint, out var _)))
				{
					continue;
				}
				if (searchFallbacks)
				{
					if (k_SearchedFontAssetLookup == null)
					{
						k_SearchedFontAssetLookup = new global::System.Collections.Generic.HashSet<int>();
					}
					else
					{
						k_SearchedFontAssetLookup.Clear();
					}
					k_SearchedFontAssetLookup.Add(GetInstanceID());
					if (fallbackFontAssetTable != null && fallbackFontAssetTable.Count > 0)
					{
						for (int j = 0; j < fallbackFontAssetTable.Count && fallbackFontAssetTable[j] != null; j++)
						{
							global::TMPro.TMP_FontAsset tMP_FontAsset = fallbackFontAssetTable[j];
							int item = tMP_FontAsset.GetInstanceID();
							if (k_SearchedFontAssetLookup.Add(item) && tMP_FontAsset.HasCharacter_Internal(codePoint, searchFallbacks: true, tryAddCharacter))
							{
								flag = false;
								break;
							}
						}
					}
					if (flag && global::TMPro.TMP_Settings.fallbackFontAssets != null && global::TMPro.TMP_Settings.fallbackFontAssets.Count > 0)
					{
						for (int k = 0; k < global::TMPro.TMP_Settings.fallbackFontAssets.Count && global::TMPro.TMP_Settings.fallbackFontAssets[k] != null; k++)
						{
							global::TMPro.TMP_FontAsset tMP_FontAsset2 = global::TMPro.TMP_Settings.fallbackFontAssets[k];
							int item2 = tMP_FontAsset2.GetInstanceID();
							if (k_SearchedFontAssetLookup.Add(item2) && tMP_FontAsset2.HasCharacter_Internal(codePoint, searchFallbacks: true, tryAddCharacter))
							{
								flag = false;
								break;
							}
						}
					}
					if (flag && global::TMPro.TMP_Settings.defaultFontAsset != null)
					{
						global::TMPro.TMP_FontAsset defaultFontAsset = global::TMPro.TMP_Settings.defaultFontAsset;
						int item3 = defaultFontAsset.GetInstanceID();
						if (k_SearchedFontAssetLookup.Add(item3) && defaultFontAsset.HasCharacter_Internal(codePoint, searchFallbacks: true, tryAddCharacter))
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					s_MissingCharacterList.Add(codePoint);
				}
			}
			if (s_MissingCharacterList.Count > 0)
			{
				missingCharacters = s_MissingCharacterList.ToArray();
				return false;
			}
			return true;
		}

		public bool HasCharacters(string text)
		{
			if (characterLookupTable == null)
			{
				return false;
			}
			for (int i = 0; i < text.Length; i++)
			{
				uint codePoint = global::TMPro.TMP_FontAssetUtilities.GetCodePoint(text, ref i);
				if (!m_CharacterLookupDictionary.ContainsKey(codePoint))
				{
					return false;
				}
			}
			return true;
		}

		public static string GetCharacters(global::TMPro.TMP_FontAsset fontAsset)
		{
			string text = string.Empty;
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				text += (char)fontAsset.characterTable[i].unicode;
			}
			return text;
		}

		public static int[] GetCharactersArray(global::TMPro.TMP_FontAsset fontAsset)
		{
			int[] array = new int[fontAsset.characterTable.Count];
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				array[i] = (int)fontAsset.characterTable[i].unicode;
			}
			return array;
		}

		internal uint GetGlyphIndex(uint unicode)
		{
			if (m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return m_CharacterLookupDictionary[unicode].glyphIndex;
			}
			if (LoadFontFace() != global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
			{
				return 0u;
			}
			return global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(unicode);
		}

		internal uint GetGlyphVariantIndex(uint unicode, uint variantSelectorUnicode)
		{
			if (LoadFontFace() != global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
			{
				return 0u;
			}
			return global::UnityEngine.TextCore.LowLevel.FontEngine.GetVariantGlyphIndex(unicode, variantSelectorUnicode);
		}

		internal static void RegisterFontAssetForFontFeatureUpdate(global::TMPro.TMP_FontAsset fontAsset)
		{
			int item = fontAsset.instanceID;
			if (k_FontAssets_FontFeaturesUpdateQueueLookup.Add(item))
			{
				k_FontAssets_FontFeaturesUpdateQueue.Add(fontAsset);
			}
		}

		internal static void UpdateFontFeaturesForFontAssetsInQueue()
		{
			int count = k_FontAssets_FontFeaturesUpdateQueue.Count;
			for (int i = 0; i < count; i++)
			{
				k_FontAssets_FontFeaturesUpdateQueue[i].UpdateGPOSFontFeaturesForNewlyAddedGlyphs();
			}
			if (count > 0)
			{
				k_FontAssets_FontFeaturesUpdateQueue.Clear();
				k_FontAssets_FontFeaturesUpdateQueueLookup.Clear();
			}
		}

		internal static void RegisterAtlasTextureForApply(global::UnityEngine.Texture2D texture)
		{
			int item = texture.GetInstanceID();
			if (k_FontAssets_AtlasTexturesUpdateQueueLookup.Add(item))
			{
				k_FontAssets_AtlasTexturesUpdateQueue.Add(texture);
			}
		}

		internal static void UpdateAtlasTexturesInQueue()
		{
			int count = k_FontAssets_AtlasTexturesUpdateQueueLookup.Count;
			for (int i = 0; i < count; i++)
			{
				k_FontAssets_AtlasTexturesUpdateQueue[i].Apply(updateMipmaps: false, makeNoLongerReadable: false);
			}
			if (count > 0)
			{
				k_FontAssets_AtlasTexturesUpdateQueue.Clear();
				k_FontAssets_AtlasTexturesUpdateQueueLookup.Clear();
			}
		}

		internal static void UpdateFontAssetsInUpdateQueue()
		{
			UpdateAtlasTexturesInQueue();
			UpdateFontFeaturesForFontAssetsInQueue();
		}

		public bool TryAddCharacters(uint[] unicodes, bool includeFontFeatures = false)
		{
			uint[] missingUnicodes;
			return TryAddCharacters(unicodes, out missingUnicodes, includeFontFeatures);
		}

		public bool TryAddCharacters(uint[] unicodes, out uint[] missingUnicodes, bool includeFontFeatures = false)
		{
			if (unicodes == null || unicodes.Length == 0 || m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.Static)
			{
				if (m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.Static)
				{
					global::UnityEngine.Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					global::UnityEngine.Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided Unicode list is Null or Empty.", this);
				}
				missingUnicodes = null;
				return false;
			}
			if (LoadFontFace() != global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
			{
				missingUnicodes = global::System.Linq.Enumerable.ToArray(unicodes);
				return false;
			}
			if (m_CharacterLookupDictionary == null || m_GlyphLookupDictionary == null)
			{
				ReadFontAssetDefinition();
			}
			m_GlyphsToAdd.Clear();
			m_GlyphsToAddLookup.Clear();
			m_CharactersToAdd.Clear();
			m_CharactersToAddLookup.Clear();
			s_MissingCharacterList.Clear();
			bool flag = false;
			int num = unicodes.Length;
			for (int i = 0; i < num; i++)
			{
				uint codePoint = global::TMPro.TMP_FontAssetUtilities.GetCodePoint(unicodes, ref i);
				if (m_CharacterLookupDictionary.ContainsKey(codePoint))
				{
					continue;
				}
				uint glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(codePoint);
				if (glyphIndex == 0)
				{
					switch (codePoint)
					{
					case 160u:
						glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(32u);
						break;
					case 173u:
					case 8209u:
						glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(45u);
						break;
					}
					if (glyphIndex == 0)
					{
						s_MissingCharacterList.Add(codePoint);
						flag = true;
						continue;
					}
				}
				global::TMPro.TMP_Character tMP_Character = new global::TMPro.TMP_Character(codePoint, glyphIndex);
				if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
				{
					tMP_Character.glyph = m_GlyphLookupDictionary[glyphIndex];
					tMP_Character.textAsset = this;
					m_CharacterTable.Add(tMP_Character);
					m_CharacterLookupDictionary.Add(codePoint, tMP_Character);
					continue;
				}
				if (m_GlyphsToAddLookup.Add(glyphIndex))
				{
					m_GlyphsToAdd.Add(glyphIndex);
				}
				if (m_CharactersToAddLookup.Add(codePoint))
				{
					m_CharactersToAdd.Add(tMP_Character);
				}
			}
			if (m_GlyphsToAdd.Count == 0)
			{
				missingUnicodes = unicodes;
				return false;
			}
			if (m_AtlasTextures[m_AtlasTextureIndex].width <= 1 || m_AtlasTextures[m_AtlasTextureIndex].height <= 1)
			{
				m_AtlasTextures[m_AtlasTextureIndex].Reinitialize(m_AtlasWidth, m_AtlasHeight);
				global::UnityEngine.TextCore.LowLevel.FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			}
			global::UnityEngine.TextCore.Glyph[] glyphs;
			bool flag2 = global::UnityEngine.TextCore.LowLevel.FontEngine.TryAddGlyphsToTexture(m_GlyphsToAdd, m_AtlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyphs);
			for (int j = 0; j < glyphs.Length && glyphs[j] != null; j++)
			{
				global::UnityEngine.TextCore.Glyph glyph = glyphs[j];
				uint index = glyph.index;
				glyph.atlasIndex = m_AtlasTextureIndex;
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(index, glyph);
				m_GlyphIndexListNewlyAdded.Add(index);
				m_GlyphIndexList.Add(index);
			}
			m_GlyphsToAdd.Clear();
			for (int k = 0; k < m_CharactersToAdd.Count; k++)
			{
				global::TMPro.TMP_Character tMP_Character2 = m_CharactersToAdd[k];
				if (!m_GlyphLookupDictionary.TryGetValue(tMP_Character2.glyphIndex, out var value))
				{
					m_GlyphsToAdd.Add(tMP_Character2.glyphIndex);
					continue;
				}
				tMP_Character2.glyph = value;
				tMP_Character2.textAsset = this;
				m_CharacterTable.Add(tMP_Character2);
				m_CharacterLookupDictionary.Add(tMP_Character2.unicode, tMP_Character2);
				m_CharactersToAdd.RemoveAt(k);
				k--;
			}
			if (m_IsMultiAtlasTexturesEnabled && !flag2)
			{
				while (!flag2)
				{
					flag2 = TryAddGlyphsToNewAtlasTexture();
				}
			}
			if (includeFontFeatures)
			{
				UpdateFontFeaturesForNewlyAddedGlyphs();
			}
			for (int l = 0; l < m_CharactersToAdd.Count; l++)
			{
				global::TMPro.TMP_Character tMP_Character3 = m_CharactersToAdd[l];
				s_MissingCharacterList.Add(tMP_Character3.unicode);
			}
			missingUnicodes = null;
			if (s_MissingCharacterList.Count > 0)
			{
				missingUnicodes = s_MissingCharacterList.ToArray();
			}
			if (flag2)
			{
				return !flag;
			}
			return false;
		}

		public bool TryAddCharacters(string characters, bool includeFontFeatures = false)
		{
			string missingCharacters;
			return TryAddCharacters(characters, out missingCharacters, includeFontFeatures);
		}

		public bool TryAddCharacters(string characters, out string missingCharacters, bool includeFontFeatures = false)
		{
			if (string.IsNullOrEmpty(characters) || m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.Static)
			{
				if (m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.Static)
				{
					global::UnityEngine.Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					global::UnityEngine.Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided character list is Null or Empty.", this);
				}
				missingCharacters = characters;
				return false;
			}
			if (LoadFontFace() != global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
			{
				missingCharacters = characters;
				return false;
			}
			if (m_CharacterLookupDictionary == null || m_GlyphLookupDictionary == null)
			{
				ReadFontAssetDefinition();
			}
			m_GlyphsToAdd.Clear();
			m_GlyphsToAddLookup.Clear();
			m_CharactersToAdd.Clear();
			m_CharactersToAddLookup.Clear();
			s_MissingCharacterList.Clear();
			bool flag = false;
			int length = characters.Length;
			for (int i = 0; i < length; i++)
			{
				uint num = characters[i];
				if (m_CharacterLookupDictionary.ContainsKey(num))
				{
					continue;
				}
				uint glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(num);
				if (glyphIndex == 0)
				{
					switch (num)
					{
					case 160u:
						glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(32u);
						break;
					case 173u:
					case 8209u:
						glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(45u);
						break;
					}
					if (glyphIndex == 0)
					{
						s_MissingCharacterList.Add(num);
						flag = true;
						continue;
					}
				}
				global::TMPro.TMP_Character tMP_Character = new global::TMPro.TMP_Character(num, glyphIndex);
				if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
				{
					tMP_Character.glyph = m_GlyphLookupDictionary[glyphIndex];
					tMP_Character.textAsset = this;
					m_CharacterTable.Add(tMP_Character);
					m_CharacterLookupDictionary.Add(num, tMP_Character);
					continue;
				}
				if (m_GlyphsToAddLookup.Add(glyphIndex))
				{
					m_GlyphsToAdd.Add(glyphIndex);
				}
				if (m_CharactersToAddLookup.Add(num))
				{
					m_CharactersToAdd.Add(tMP_Character);
				}
			}
			if (m_GlyphsToAdd.Count == 0)
			{
				missingCharacters = characters;
				return false;
			}
			if (m_AtlasTextures[m_AtlasTextureIndex].width <= 1 || m_AtlasTextures[m_AtlasTextureIndex].height <= 1)
			{
				m_AtlasTextures[m_AtlasTextureIndex].Reinitialize(m_AtlasWidth, m_AtlasHeight);
				global::UnityEngine.TextCore.LowLevel.FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			}
			global::UnityEngine.TextCore.Glyph[] glyphs;
			bool flag2 = global::UnityEngine.TextCore.LowLevel.FontEngine.TryAddGlyphsToTexture(m_GlyphsToAdd, m_AtlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyphs);
			for (int j = 0; j < glyphs.Length && glyphs[j] != null; j++)
			{
				global::UnityEngine.TextCore.Glyph glyph = glyphs[j];
				uint index = glyph.index;
				glyph.atlasIndex = m_AtlasTextureIndex;
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(index, glyph);
				m_GlyphIndexListNewlyAdded.Add(index);
				m_GlyphIndexList.Add(index);
			}
			m_GlyphsToAdd.Clear();
			for (int k = 0; k < m_CharactersToAdd.Count; k++)
			{
				global::TMPro.TMP_Character tMP_Character2 = m_CharactersToAdd[k];
				if (!m_GlyphLookupDictionary.TryGetValue(tMP_Character2.glyphIndex, out var value))
				{
					m_GlyphsToAdd.Add(tMP_Character2.glyphIndex);
					continue;
				}
				tMP_Character2.glyph = value;
				tMP_Character2.textAsset = this;
				m_CharacterTable.Add(tMP_Character2);
				m_CharacterLookupDictionary.Add(tMP_Character2.unicode, tMP_Character2);
				m_CharactersToAdd.RemoveAt(k);
				k--;
			}
			if (m_IsMultiAtlasTexturesEnabled && !flag2)
			{
				while (!flag2)
				{
					flag2 = TryAddGlyphsToNewAtlasTexture();
				}
			}
			if (includeFontFeatures)
			{
				UpdateFontFeaturesForNewlyAddedGlyphs();
			}
			missingCharacters = string.Empty;
			for (int l = 0; l < m_CharactersToAdd.Count; l++)
			{
				global::TMPro.TMP_Character tMP_Character3 = m_CharactersToAdd[l];
				s_MissingCharacterList.Add(tMP_Character3.unicode);
			}
			if (s_MissingCharacterList.Count > 0)
			{
				missingCharacters = s_MissingCharacterList.UintToString();
			}
			if (flag2)
			{
				return !flag;
			}
			return false;
		}

		internal bool AddGlyphInternal(uint glyphIndex)
		{
			global::UnityEngine.TextCore.Glyph glyph;
			return TryAddGlyphInternal(glyphIndex, out glyph);
		}

		internal bool TryAddGlyphInternal(uint glyphIndex, out global::UnityEngine.TextCore.Glyph glyph)
		{
			glyph = null;
			if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				glyph = m_GlyphLookupDictionary[glyphIndex];
				return true;
			}
			if (m_AtlasPopulationMode == global::TMPro.AtlasPopulationMode.Static)
			{
				return false;
			}
			if (LoadFontFace() != global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
			{
				return false;
			}
			if (!m_AtlasTextures[m_AtlasTextureIndex].isReadable)
			{
				global::UnityEngine.Debug.LogWarning("Unable to add the requested glyph to font asset [" + base.name + "]'s atlas texture. Please make the texture [" + m_AtlasTextures[m_AtlasTextureIndex].name + "] readable.", m_AtlasTextures[m_AtlasTextureIndex]);
				return false;
			}
			if (m_AtlasTextures[m_AtlasTextureIndex].width <= 1 || m_AtlasTextures[m_AtlasTextureIndex].height <= 1)
			{
				m_AtlasTextures[m_AtlasTextureIndex].Reinitialize(m_AtlasWidth, m_AtlasHeight);
				global::UnityEngine.TextCore.LowLevel.FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			}
			if (global::UnityEngine.TextCore.LowLevel.FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyph))
			{
				glyph.atlasIndex = m_AtlasTextureIndex;
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				m_GlyphIndexList.Add(glyphIndex);
				m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				if (m_GetFontFeatures && global::TMPro.TMP_Settings.getFontFeaturesAtRuntime)
				{
					UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
					RegisterFontAssetForFontFeatureUpdate(this);
				}
				return true;
			}
			if (m_IsMultiAtlasTexturesEnabled && m_UsedGlyphRects.Count > 0)
			{
				SetupNewAtlasTexture();
				if (global::UnityEngine.TextCore.LowLevel.FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyph))
				{
					glyph.atlasIndex = m_AtlasTextureIndex;
					m_GlyphTable.Add(glyph);
					m_GlyphLookupDictionary.Add(glyphIndex, glyph);
					m_GlyphIndexList.Add(glyphIndex);
					m_GlyphIndexListNewlyAdded.Add(glyphIndex);
					if (m_GetFontFeatures && global::TMPro.TMP_Settings.getFontFeaturesAtRuntime)
					{
						UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
						RegisterFontAssetForFontFeatureUpdate(this);
					}
					return true;
				}
			}
			return false;
		}

		internal bool TryAddCharacterInternal(uint unicode, out global::TMPro.TMP_Character character)
		{
			character = null;
			if (m_MissingUnicodesFromFontFile.Contains(unicode))
			{
				return false;
			}
			if (LoadFontFace() != global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
			{
				return false;
			}
			uint glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(unicode);
			if (glyphIndex == 0)
			{
				switch (unicode)
				{
				case 160u:
					glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(32u);
					break;
				case 173u:
				case 8209u:
					glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(45u);
					break;
				}
				if (glyphIndex == 0)
				{
					m_MissingUnicodesFromFontFile.Add(unicode);
					return false;
				}
			}
			if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				character = new global::TMPro.TMP_Character(unicode, this, m_GlyphLookupDictionary[glyphIndex]);
				m_CharacterTable.Add(character);
				m_CharacterLookupDictionary.Add(unicode, character);
				return true;
			}
			global::UnityEngine.TextCore.Glyph glyph = null;
			if (!m_AtlasTextures[m_AtlasTextureIndex].isReadable)
			{
				global::UnityEngine.Debug.LogWarning("Unable to add the requested character to font asset [" + base.name + "]'s atlas texture. Please make the texture [" + m_AtlasTextures[m_AtlasTextureIndex].name + "] readable.", m_AtlasTextures[m_AtlasTextureIndex]);
				return false;
			}
			if (m_AtlasTextures[m_AtlasTextureIndex].width <= 1 || m_AtlasTextures[m_AtlasTextureIndex].height <= 1)
			{
				m_AtlasTextures[m_AtlasTextureIndex].Reinitialize(m_AtlasWidth, m_AtlasHeight);
				global::UnityEngine.TextCore.LowLevel.FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			}
			if (global::UnityEngine.TextCore.LowLevel.FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyph))
			{
				glyph.atlasIndex = m_AtlasTextureIndex;
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				character = new global::TMPro.TMP_Character(unicode, this, glyph);
				m_CharacterTable.Add(character);
				m_CharacterLookupDictionary.Add(unicode, character);
				m_GlyphIndexList.Add(glyphIndex);
				m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				if (m_GetFontFeatures && global::TMPro.TMP_Settings.getFontFeaturesAtRuntime)
				{
					UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
					RegisterFontAssetForFontFeatureUpdate(this);
				}
				return true;
			}
			if (m_IsMultiAtlasTexturesEnabled && m_UsedGlyphRects.Count > 0)
			{
				SetupNewAtlasTexture();
				if (global::UnityEngine.TextCore.LowLevel.FontEngine.TryAddGlyphToTexture(glyphIndex, m_AtlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyph))
				{
					glyph.atlasIndex = m_AtlasTextureIndex;
					m_GlyphTable.Add(glyph);
					m_GlyphLookupDictionary.Add(glyphIndex, glyph);
					character = new global::TMPro.TMP_Character(unicode, this, glyph);
					m_CharacterTable.Add(character);
					m_CharacterLookupDictionary.Add(unicode, character);
					m_GlyphIndexList.Add(glyphIndex);
					m_GlyphIndexListNewlyAdded.Add(glyphIndex);
					if (m_GetFontFeatures && global::TMPro.TMP_Settings.getFontFeaturesAtRuntime)
					{
						UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
						RegisterFontAssetForFontFeatureUpdate(this);
					}
					return true;
				}
			}
			return false;
		}

		internal bool TryGetCharacter_and_QueueRenderToTexture(uint unicode, out global::TMPro.TMP_Character character)
		{
			character = null;
			if (m_MissingUnicodesFromFontFile.Contains(unicode))
			{
				return false;
			}
			if (LoadFontFace() != global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
			{
				return false;
			}
			uint glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(unicode);
			if (glyphIndex == 0)
			{
				switch (unicode)
				{
				case 160u:
					glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(32u);
					break;
				case 173u:
				case 8209u:
					glyphIndex = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphIndex(45u);
					break;
				}
				if (glyphIndex == 0)
				{
					m_MissingUnicodesFromFontFile.Add(unicode);
					return false;
				}
			}
			if (m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				character = new global::TMPro.TMP_Character(unicode, this, m_GlyphLookupDictionary[glyphIndex]);
				m_CharacterTable.Add(character);
				m_CharacterLookupDictionary.Add(unicode, character);
				return true;
			}
			global::UnityEngine.TextCore.LowLevel.GlyphLoadFlags flags = ((((global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)4 & m_AtlasRenderMode) == (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)4) ? (global::UnityEngine.TextCore.LowLevel.GlyphLoadFlags.LOAD_NO_HINTING | global::UnityEngine.TextCore.LowLevel.GlyphLoadFlags.LOAD_NO_BITMAP) : global::UnityEngine.TextCore.LowLevel.GlyphLoadFlags.LOAD_NO_BITMAP);
			global::UnityEngine.TextCore.Glyph glyph = null;
			if (global::UnityEngine.TextCore.LowLevel.FontEngine.TryGetGlyphWithIndexValue(glyphIndex, flags, out glyph))
			{
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				character = new global::TMPro.TMP_Character(unicode, this, glyph);
				m_CharacterTable.Add(character);
				m_CharacterLookupDictionary.Add(unicode, character);
				m_GlyphIndexList.Add(glyphIndex);
				m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				if (m_GetFontFeatures && global::TMPro.TMP_Settings.getFontFeaturesAtRuntime)
				{
					UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
					RegisterFontAssetForFontFeatureUpdate(this);
				}
				m_GlyphsToRender.Add(glyph);
				return true;
			}
			return false;
		}

		internal void TryAddGlyphsToAtlasTextures()
		{
		}

		private bool TryAddGlyphsToNewAtlasTexture()
		{
			SetupNewAtlasTexture();
			global::UnityEngine.TextCore.Glyph[] glyphs;
			bool result = global::UnityEngine.TextCore.LowLevel.FontEngine.TryAddGlyphsToTexture(m_GlyphsToAdd, m_AtlasPadding, global::UnityEngine.TextCore.LowLevel.GlyphPackingMode.BestShortSideFit, m_FreeGlyphRects, m_UsedGlyphRects, m_AtlasRenderMode, m_AtlasTextures[m_AtlasTextureIndex], out glyphs);
			for (int i = 0; i < glyphs.Length && glyphs[i] != null; i++)
			{
				global::UnityEngine.TextCore.Glyph glyph = glyphs[i];
				uint index = glyph.index;
				glyph.atlasIndex = m_AtlasTextureIndex;
				m_GlyphTable.Add(glyph);
				m_GlyphLookupDictionary.Add(index, glyph);
				m_GlyphIndexListNewlyAdded.Add(index);
				m_GlyphIndexList.Add(index);
			}
			m_GlyphsToAdd.Clear();
			for (int j = 0; j < m_CharactersToAdd.Count; j++)
			{
				global::TMPro.TMP_Character tMP_Character = m_CharactersToAdd[j];
				if (!m_GlyphLookupDictionary.TryGetValue(tMP_Character.glyphIndex, out var value))
				{
					m_GlyphsToAdd.Add(tMP_Character.glyphIndex);
					continue;
				}
				tMP_Character.glyph = value;
				tMP_Character.textAsset = this;
				m_CharacterTable.Add(tMP_Character);
				m_CharacterLookupDictionary.Add(tMP_Character.unicode, tMP_Character);
				m_CharactersToAdd.RemoveAt(j);
				j--;
			}
			return result;
		}

		private void SetupNewAtlasTexture()
		{
			m_AtlasTextureIndex++;
			if (m_AtlasTextures.Length == m_AtlasTextureIndex)
			{
				global::System.Array.Resize(ref m_AtlasTextures, m_AtlasTextures.Length * 2);
			}
			global::UnityEngine.TextureFormat textureFormat = (((m_AtlasRenderMode & (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)65536) != (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)65536) ? global::UnityEngine.TextureFormat.Alpha8 : global::UnityEngine.TextureFormat.RGBA32);
			m_AtlasTextures[m_AtlasTextureIndex] = new global::UnityEngine.Texture2D(m_AtlasWidth, m_AtlasHeight, textureFormat, mipChain: false);
			global::UnityEngine.TextCore.LowLevel.FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
			int num = (((m_AtlasRenderMode & (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)16) != (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)16) ? 1 : 0);
			m_FreeGlyphRects.Clear();
			m_FreeGlyphRects.Add(new global::UnityEngine.TextCore.GlyphRect(0, 0, m_AtlasWidth - num, m_AtlasHeight - num));
			m_UsedGlyphRects.Clear();
		}

		internal void UpdateAtlasTexture()
		{
			if (m_GlyphsToRender.Count != 0)
			{
				if (m_AtlasTextures[m_AtlasTextureIndex].width <= 1 || m_AtlasTextures[m_AtlasTextureIndex].height <= 1)
				{
					m_AtlasTextures[m_AtlasTextureIndex].Reinitialize(m_AtlasWidth, m_AtlasHeight);
					global::UnityEngine.TextCore.LowLevel.FontEngine.ResetAtlasTexture(m_AtlasTextures[m_AtlasTextureIndex]);
				}
				m_AtlasTextures[m_AtlasTextureIndex].Apply(updateMipmaps: false, makeNoLongerReadable: false);
			}
		}

		private void UpdateFontFeaturesForNewlyAddedGlyphs()
		{
			UpdateLigatureSubstitutionRecords();
			UpdateGlyphAdjustmentRecords();
			UpdateDiacriticalMarkAdjustmentRecords();
			m_GlyphIndexListNewlyAdded.Clear();
		}

		private void UpdateGPOSFontFeaturesForNewlyAddedGlyphs()
		{
			UpdateGlyphAdjustmentRecords();
			UpdateDiacriticalMarkAdjustmentRecords();
			m_GlyphIndexListNewlyAdded.Clear();
		}

		internal void ImportFontFeatures()
		{
			if (LoadFontFace() == global::UnityEngine.TextCore.LowLevel.FontEngineError.Success)
			{
				global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord[] allPairAdjustmentRecords = global::UnityEngine.TextCore.LowLevel.FontEngine.GetAllPairAdjustmentRecords();
				if (allPairAdjustmentRecords != null)
				{
					AddPairAdjustmentRecords(allPairAdjustmentRecords);
				}
				global::UnityEngine.TextCore.LowLevel.MarkToBaseAdjustmentRecord[] allMarkToBaseAdjustmentRecords = global::UnityEngine.TextCore.LowLevel.FontEngine.GetAllMarkToBaseAdjustmentRecords();
				if (allMarkToBaseAdjustmentRecords != null)
				{
					AddMarkToBaseAdjustmentRecords(allMarkToBaseAdjustmentRecords);
				}
				global::UnityEngine.TextCore.LowLevel.MarkToMarkAdjustmentRecord[] allMarkToMarkAdjustmentRecords = global::UnityEngine.TextCore.LowLevel.FontEngine.GetAllMarkToMarkAdjustmentRecords();
				if (allMarkToMarkAdjustmentRecords != null)
				{
					AddMarkToMarkAdjustmentRecords(allMarkToMarkAdjustmentRecords);
				}
				global::UnityEngine.TextCore.LowLevel.LigatureSubstitutionRecord[] allLigatureSubstitutionRecords = global::UnityEngine.TextCore.LowLevel.FontEngine.GetAllLigatureSubstitutionRecords();
				if (allLigatureSubstitutionRecords != null)
				{
					AddLigatureSubstitutionRecords(allLigatureSubstitutionRecords);
				}
				m_ShouldReimportFontFeatures = false;
			}
		}

		private void UpdateGSUBFontFeaturesForNewGlyphIndex(uint glyphIndex)
		{
			global::UnityEngine.TextCore.LowLevel.LigatureSubstitutionRecord[] ligatureSubstitutionRecords = global::UnityEngine.TextCore.LowLevel.FontEngine.GetLigatureSubstitutionRecords(glyphIndex);
			if (ligatureSubstitutionRecords != null)
			{
				AddLigatureSubstitutionRecords(ligatureSubstitutionRecords);
			}
		}

		internal void UpdateLigatureSubstitutionRecords()
		{
			global::UnityEngine.TextCore.LowLevel.LigatureSubstitutionRecord[] ligatureSubstitutionRecords = global::UnityEngine.TextCore.LowLevel.FontEngine.GetLigatureSubstitutionRecords(m_GlyphIndexListNewlyAdded);
			if (ligatureSubstitutionRecords != null)
			{
				AddLigatureSubstitutionRecords(ligatureSubstitutionRecords);
			}
		}

		private void AddLigatureSubstitutionRecords(global::UnityEngine.TextCore.LowLevel.LigatureSubstitutionRecord[] records)
		{
			for (int i = 0; i < records.Length; i++)
			{
				global::UnityEngine.TextCore.LowLevel.LigatureSubstitutionRecord ligatureSubstitutionRecord = records[i];
				if (records[i].componentGlyphIDs == null || records[i].ligatureGlyphID == 0)
				{
					break;
				}
				uint key = ligatureSubstitutionRecord.componentGlyphIDs[0];
				global::TMPro.LigatureSubstitutionRecord ligatureSubstitutionRecord2 = new global::TMPro.LigatureSubstitutionRecord
				{
					componentGlyphIDs = ligatureSubstitutionRecord.componentGlyphIDs,
					ligatureGlyphID = ligatureSubstitutionRecord.ligatureGlyphID
				};
				if (m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.TryGetValue(key, out var value))
				{
					foreach (global::TMPro.LigatureSubstitutionRecord item in value)
					{
						if (ligatureSubstitutionRecord2 == item)
						{
							return;
						}
					}
					m_FontFeatureTable.m_LigatureSubstitutionRecordLookup[key].Add(ligatureSubstitutionRecord2);
				}
				else
				{
					m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Add(key, new global::System.Collections.Generic.List<global::TMPro.LigatureSubstitutionRecord> { ligatureSubstitutionRecord2 });
				}
				m_FontFeatureTable.m_LigatureSubstitutionRecords.Add(ligatureSubstitutionRecord2);
			}
		}

		internal void UpdateGlyphAdjustmentRecords()
		{
			global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord[] pairAdjustmentRecords = global::UnityEngine.TextCore.LowLevel.FontEngine.GetPairAdjustmentRecords(m_GlyphIndexListNewlyAdded);
			if (pairAdjustmentRecords != null)
			{
				AddPairAdjustmentRecords(pairAdjustmentRecords);
			}
		}

		private void AddPairAdjustmentRecords(global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord[] records)
		{
			float num = m_FaceInfo.pointSize / (float)m_FaceInfo.unitsPerEM;
			for (int i = 0; i < records.Length; i++)
			{
				global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord glyphPairAdjustmentRecord = records[i];
				global::UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord firstAdjustmentRecord = glyphPairAdjustmentRecord.firstAdjustmentRecord;
				global::UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord secondAdjustmentRecord = glyphPairAdjustmentRecord.secondAdjustmentRecord;
				uint glyphIndex = firstAdjustmentRecord.glyphIndex;
				uint glyphIndex2 = secondAdjustmentRecord.glyphIndex;
				if (glyphIndex == 0 && glyphIndex2 == 0)
				{
					break;
				}
				uint key = (glyphIndex2 << 16) | glyphIndex;
				if (!m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(key))
				{
					global::UnityEngine.TextCore.LowLevel.GlyphValueRecord glyphValueRecord = firstAdjustmentRecord.glyphValueRecord;
					glyphValueRecord.xAdvance *= num;
					glyphPairAdjustmentRecord.firstAdjustmentRecord = new global::UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord(glyphIndex, glyphValueRecord);
					m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(glyphPairAdjustmentRecord);
					m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(key, glyphPairAdjustmentRecord);
				}
			}
		}

		internal void UpdateGlyphAdjustmentRecords(uint[] glyphIndexes)
		{
			global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord[] glyphPairAdjustmentTable = global::UnityEngine.TextCore.LowLevel.FontEngine.GetGlyphPairAdjustmentTable(glyphIndexes);
			if (glyphPairAdjustmentTable == null || glyphPairAdjustmentTable.Length == 0)
			{
				return;
			}
			if (m_FontFeatureTable == null)
			{
				m_FontFeatureTable = new global::TMPro.TMP_FontFeatureTable();
			}
			for (int i = 0; i < glyphPairAdjustmentTable.Length && glyphPairAdjustmentTable[i].firstAdjustmentRecord.glyphIndex != 0; i++)
			{
				uint key = (glyphPairAdjustmentTable[i].secondAdjustmentRecord.glyphIndex << 16) | glyphPairAdjustmentTable[i].firstAdjustmentRecord.glyphIndex;
				if (!m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(key))
				{
					global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord glyphPairAdjustmentRecord = glyphPairAdjustmentTable[i];
					m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(glyphPairAdjustmentRecord);
					m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(key, glyphPairAdjustmentRecord);
				}
			}
		}

		internal void UpdateDiacriticalMarkAdjustmentRecords()
		{
			global::UnityEngine.TextCore.LowLevel.MarkToBaseAdjustmentRecord[] markToBaseAdjustmentRecords = global::UnityEngine.TextCore.LowLevel.FontEngine.GetMarkToBaseAdjustmentRecords(m_GlyphIndexListNewlyAdded);
			if (markToBaseAdjustmentRecords != null)
			{
				AddMarkToBaseAdjustmentRecords(markToBaseAdjustmentRecords);
			}
			global::UnityEngine.TextCore.LowLevel.MarkToMarkAdjustmentRecord[] markToMarkAdjustmentRecords = global::UnityEngine.TextCore.LowLevel.FontEngine.GetMarkToMarkAdjustmentRecords(m_GlyphIndexListNewlyAdded);
			if (markToMarkAdjustmentRecords != null)
			{
				AddMarkToMarkAdjustmentRecords(markToMarkAdjustmentRecords);
			}
		}

		private void AddMarkToBaseAdjustmentRecords(global::UnityEngine.TextCore.LowLevel.MarkToBaseAdjustmentRecord[] records)
		{
			float num = m_FaceInfo.pointSize / (float)m_FaceInfo.unitsPerEM;
			for (int i = 0; i < records.Length; i++)
			{
				global::UnityEngine.TextCore.LowLevel.MarkToBaseAdjustmentRecord markToBaseAdjustmentRecord = records[i];
				if (records[i].baseGlyphID == 0 || records[i].markGlyphID == 0)
				{
					break;
				}
				uint key = (markToBaseAdjustmentRecord.markGlyphID << 16) | markToBaseAdjustmentRecord.baseGlyphID;
				if (!m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.ContainsKey(key))
				{
					global::TMPro.MarkToBaseAdjustmentRecord markToBaseAdjustmentRecord2 = new global::TMPro.MarkToBaseAdjustmentRecord
					{
						baseGlyphID = markToBaseAdjustmentRecord.baseGlyphID,
						baseGlyphAnchorPoint = new global::TMPro.GlyphAnchorPoint
						{
							xCoordinate = markToBaseAdjustmentRecord.baseGlyphAnchorPoint.xCoordinate * num,
							yCoordinate = markToBaseAdjustmentRecord.baseGlyphAnchorPoint.yCoordinate * num
						},
						markGlyphID = markToBaseAdjustmentRecord.markGlyphID,
						markPositionAdjustment = new global::TMPro.MarkPositionAdjustment
						{
							xPositionAdjustment = markToBaseAdjustmentRecord.markPositionAdjustment.xPositionAdjustment * num,
							yPositionAdjustment = markToBaseAdjustmentRecord.markPositionAdjustment.yPositionAdjustment * num
						}
					};
					m_FontFeatureTable.MarkToBaseAdjustmentRecords.Add(markToBaseAdjustmentRecord2);
					m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.Add(key, markToBaseAdjustmentRecord2);
				}
			}
		}

		private void AddMarkToMarkAdjustmentRecords(global::UnityEngine.TextCore.LowLevel.MarkToMarkAdjustmentRecord[] records)
		{
			float num = m_FaceInfo.pointSize / (float)m_FaceInfo.unitsPerEM;
			for (int i = 0; i < records.Length; i++)
			{
				global::UnityEngine.TextCore.LowLevel.MarkToMarkAdjustmentRecord markToMarkAdjustmentRecord = records[i];
				if (records[i].baseMarkGlyphID == 0 || records[i].combiningMarkGlyphID == 0)
				{
					break;
				}
				uint key = (markToMarkAdjustmentRecord.combiningMarkGlyphID << 16) | markToMarkAdjustmentRecord.baseMarkGlyphID;
				if (!m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.ContainsKey(key))
				{
					global::TMPro.MarkToMarkAdjustmentRecord markToMarkAdjustmentRecord2 = new global::TMPro.MarkToMarkAdjustmentRecord
					{
						baseMarkGlyphID = markToMarkAdjustmentRecord.baseMarkGlyphID,
						baseMarkGlyphAnchorPoint = new global::TMPro.GlyphAnchorPoint
						{
							xCoordinate = markToMarkAdjustmentRecord.baseMarkGlyphAnchorPoint.xCoordinate * num,
							yCoordinate = markToMarkAdjustmentRecord.baseMarkGlyphAnchorPoint.yCoordinate * num
						},
						combiningMarkGlyphID = markToMarkAdjustmentRecord.combiningMarkGlyphID,
						combiningMarkPositionAdjustment = new global::TMPro.MarkPositionAdjustment
						{
							xPositionAdjustment = markToMarkAdjustmentRecord.combiningMarkPositionAdjustment.xPositionAdjustment * num,
							yPositionAdjustment = markToMarkAdjustmentRecord.combiningMarkPositionAdjustment.yPositionAdjustment * num
						}
					};
					m_FontFeatureTable.MarkToMarkAdjustmentRecords.Add(markToMarkAdjustmentRecord2);
					m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.Add(key, markToMarkAdjustmentRecord2);
				}
			}
		}

		private void CopyListDataToArray<T>(global::System.Collections.Generic.List<T> srcList, ref T[] dstArray)
		{
			int count = srcList.Count;
			if (dstArray == null)
			{
				dstArray = new T[count];
			}
			else
			{
				global::System.Array.Resize(ref dstArray, count);
			}
			for (int i = 0; i < count; i++)
			{
				dstArray[i] = srcList[i];
			}
		}

		internal void UpdateFontAssetData()
		{
			uint[] array = new uint[m_CharacterTable.Count];
			for (int i = 0; i < m_CharacterTable.Count; i++)
			{
				array[i] = m_CharacterTable[i].unicode;
			}
			ClearCharacterAndGlyphTables();
			ClearFontFeaturesTables();
			ClearAtlasTextures(setAtlasSizeToZero: true);
			ReadFontAssetDefinition();
			if (array.Length != 0)
			{
				TryAddCharacters(array, m_GetFontFeatures && global::TMPro.TMP_Settings.getFontFeaturesAtRuntime);
			}
		}

		public void ClearFontAssetData(bool setAtlasSizeToZero = false)
		{
			ClearCharacterAndGlyphTables();
			ClearFontFeaturesTables();
			ClearAtlasTextures(setAtlasSizeToZero);
			ReadFontAssetDefinition();
			for (int i = 0; i < s_CallbackInstances.Count; i++)
			{
				if (s_CallbackInstances[i].TryGetTarget(out var target) && target != this)
				{
					target.ClearFallbackCharacterTable();
				}
			}
			global::TMPro.TMPro_EventManager.ON_FONT_PROPERTY_CHANGED(isChanged: true, this);
		}

		internal void ClearCharacterAndGlyphTablesInternal()
		{
			ClearCharacterAndGlyphTables();
			ClearAtlasTextures(setAtlasSizeToZero: true);
			ReadFontAssetDefinition();
		}

		internal void ClearFontFeaturesInternal()
		{
			ClearFontFeaturesTables();
			ReadFontAssetDefinition();
		}

		private void ClearCharacterAndGlyphTables()
		{
			if (m_GlyphTable != null)
			{
				m_GlyphTable.Clear();
			}
			if (m_CharacterTable != null)
			{
				m_CharacterTable.Clear();
			}
			if (m_UsedGlyphRects != null)
			{
				m_UsedGlyphRects.Clear();
			}
			if (m_FreeGlyphRects != null)
			{
				int num = (((m_AtlasRenderMode & (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)16) != (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)16) ? 1 : 0);
				m_FreeGlyphRects.Clear();
				m_FreeGlyphRects.Add(new global::UnityEngine.TextCore.GlyphRect(0, 0, m_AtlasWidth - num, m_AtlasHeight - num));
			}
			if (m_GlyphsToRender != null)
			{
				m_GlyphsToRender.Clear();
			}
			if (m_GlyphsRendered != null)
			{
				m_GlyphsRendered.Clear();
			}
		}

		private void ClearFontFeaturesTables()
		{
			if (m_FontFeatureTable != null && m_FontFeatureTable.m_LigatureSubstitutionRecords != null)
			{
				m_FontFeatureTable.m_LigatureSubstitutionRecords.Clear();
			}
			if (m_FontFeatureTable != null && m_FontFeatureTable.m_GlyphPairAdjustmentRecords != null)
			{
				m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Clear();
			}
			if (m_FontFeatureTable != null && m_FontFeatureTable.m_MarkToBaseAdjustmentRecords != null)
			{
				m_FontFeatureTable.m_MarkToBaseAdjustmentRecords.Clear();
			}
			if (m_FontFeatureTable != null && m_FontFeatureTable.m_MarkToMarkAdjustmentRecords != null)
			{
				m_FontFeatureTable.m_MarkToMarkAdjustmentRecords.Clear();
			}
		}

		internal void ClearAtlasTextures(bool setAtlasSizeToZero = false)
		{
			m_AtlasTextureIndex = 0;
			if (m_AtlasTextures == null)
			{
				return;
			}
			global::UnityEngine.Texture2D texture2D = null;
			for (int i = 1; i < m_AtlasTextures.Length; i++)
			{
				texture2D = m_AtlasTextures[i];
				if (!(texture2D == null))
				{
					global::UnityEngine.Object.DestroyImmediate(texture2D, allowDestroyingAssets: true);
				}
			}
			global::System.Array.Resize(ref m_AtlasTextures, 1);
			texture2D = (m_AtlasTexture = m_AtlasTextures[0]);
			_ = texture2D.isReadable;
			global::UnityEngine.TextureFormat format = (((m_AtlasRenderMode & (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)65536) != (global::UnityEngine.TextCore.LowLevel.GlyphRenderMode)65536) ? global::UnityEngine.TextureFormat.Alpha8 : global::UnityEngine.TextureFormat.RGBA32);
			if (setAtlasSizeToZero)
			{
				texture2D.Reinitialize(1, 1, format, hasMipMap: false);
			}
			else if (texture2D.width != m_AtlasWidth || texture2D.height != m_AtlasHeight)
			{
				texture2D.Reinitialize(m_AtlasWidth, m_AtlasHeight, format, hasMipMap: false);
			}
			global::UnityEngine.TextCore.LowLevel.FontEngine.ResetAtlasTexture(texture2D);
			texture2D.Apply();
		}

		private void DestroyAtlasTextures()
		{
			if (m_AtlasTextures == null)
			{
				return;
			}
			for (int i = 0; i < m_AtlasTextures.Length; i++)
			{
				global::UnityEngine.Texture2D texture2D = m_AtlasTextures[i];
				if (texture2D != null)
				{
					global::UnityEngine.Object.DestroyImmediate(texture2D);
				}
			}
		}

		private void UpgradeGlyphAdjustmentTableToFontFeatureTable()
		{
			global::UnityEngine.Debug.Log("Upgrading font asset [" + base.name + "] Glyph Adjustment Table.", this);
			if (m_FontFeatureTable == null)
			{
				m_FontFeatureTable = new global::TMPro.TMP_FontFeatureTable();
			}
			int count = m_KerningTable.kerningPairs.Count;
			m_FontFeatureTable.m_GlyphPairAdjustmentRecords = new global::System.Collections.Generic.List<global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord>(count);
			for (int i = 0; i < count; i++)
			{
				global::TMPro.KerningPair kerningPair = m_KerningTable.kerningPairs[i];
				uint glyphIndex = 0u;
				if (m_CharacterLookupDictionary.TryGetValue(kerningPair.firstGlyph, out var value))
				{
					glyphIndex = value.glyphIndex;
				}
				uint glyphIndex2 = 0u;
				if (m_CharacterLookupDictionary.TryGetValue(kerningPair.secondGlyph, out var value2))
				{
					glyphIndex2 = value2.glyphIndex;
				}
				global::UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord firstAdjustmentRecord = new global::UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord(glyphIndex, new global::UnityEngine.TextCore.LowLevel.GlyphValueRecord(kerningPair.firstGlyphAdjustments.xPlacement, kerningPair.firstGlyphAdjustments.yPlacement, kerningPair.firstGlyphAdjustments.xAdvance, kerningPair.firstGlyphAdjustments.yAdvance));
				global::UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord secondAdjustmentRecord = new global::UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord(glyphIndex2, new global::UnityEngine.TextCore.LowLevel.GlyphValueRecord(kerningPair.secondGlyphAdjustments.xPlacement, kerningPair.secondGlyphAdjustments.yPlacement, kerningPair.secondGlyphAdjustments.xAdvance, kerningPair.secondGlyphAdjustments.yAdvance));
				global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord item = new global::UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord(firstAdjustmentRecord, secondAdjustmentRecord);
				m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(item);
			}
			m_KerningTable.kerningPairs = null;
			m_KerningTable = null;
		}
	}
}
