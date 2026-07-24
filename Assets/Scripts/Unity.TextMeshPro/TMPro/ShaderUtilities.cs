namespace TMPro
{
	public static class ShaderUtilities
	{
		public static int ID_MainTex;

		public static int ID_FaceTex;

		public static int ID_FaceColor;

		public static int ID_FaceDilate;

		public static int ID_Shininess;

		public static int ID_OutlineOffset1;

		public static int ID_OutlineOffset2;

		public static int ID_OutlineOffset3;

		public static int ID_OutlineMode;

		public static int ID_IsoPerimeter;

		public static int ID_Softness;

		public static int ID_UnderlayColor;

		public static int ID_UnderlayOffsetX;

		public static int ID_UnderlayOffsetY;

		public static int ID_UnderlayDilate;

		public static int ID_UnderlaySoftness;

		public static int ID_UnderlayOffset;

		public static int ID_UnderlayIsoPerimeter;

		public static int ID_WeightNormal;

		public static int ID_WeightBold;

		public static int ID_OutlineTex;

		public static int ID_OutlineWidth;

		public static int ID_OutlineSoftness;

		public static int ID_OutlineColor;

		public static int ID_Outline2Color;

		public static int ID_Outline2Width;

		public static int ID_Padding;

		public static int ID_GradientScale;

		public static int ID_ScaleX;

		public static int ID_ScaleY;

		public static int ID_PerspectiveFilter;

		public static int ID_Sharpness;

		public static int ID_TextureWidth;

		public static int ID_TextureHeight;

		public static int ID_BevelAmount;

		public static int ID_GlowColor;

		public static int ID_GlowOffset;

		public static int ID_GlowPower;

		public static int ID_GlowOuter;

		public static int ID_GlowInner;

		public static int ID_LightAngle;

		public static int ID_EnvMap;

		public static int ID_EnvMatrix;

		public static int ID_EnvMatrixRotation;

		public static int ID_MaskCoord;

		public static int ID_ClipRect;

		public static int ID_MaskSoftnessX;

		public static int ID_MaskSoftnessY;

		public static int ID_VertexOffsetX;

		public static int ID_VertexOffsetY;

		public static int ID_UseClipRect;

		public static int ID_StencilID;

		public static int ID_StencilOp;

		public static int ID_StencilComp;

		public static int ID_StencilReadMask;

		public static int ID_StencilWriteMask;

		public static int ID_ShaderFlags;

		public static int ID_ScaleRatio_A;

		public static int ID_ScaleRatio_B;

		public static int ID_ScaleRatio_C;

		public static string Keyword_Bevel;

		public static string Keyword_Glow;

		public static string Keyword_Underlay;

		public static string Keyword_Ratios;

		public static string Keyword_MASK_SOFT;

		public static string Keyword_MASK_HARD;

		public static string Keyword_MASK_TEX;

		public static string Keyword_Outline;

		public static string ShaderTag_ZTestMode;

		public static string ShaderTag_CullMode;

		private static float m_clamp;

		public static bool isInitialized;

		private static global::UnityEngine.Shader k_ShaderRef_MobileSDF;

		private static global::UnityEngine.Shader k_ShaderRef_MobileBitmap;

		internal static global::UnityEngine.Shader ShaderRef_MobileSDF
		{
			get
			{
				if (k_ShaderRef_MobileSDF == null)
				{
					k_ShaderRef_MobileSDF = global::UnityEngine.Shader.Find("TextMeshPro/Mobile/Distance Field");
				}
				return k_ShaderRef_MobileSDF;
			}
		}

		internal static global::UnityEngine.Shader ShaderRef_MobileBitmap
		{
			get
			{
				if (k_ShaderRef_MobileBitmap == null)
				{
					k_ShaderRef_MobileBitmap = global::UnityEngine.Shader.Find("TextMeshPro/Mobile/Bitmap");
				}
				return k_ShaderRef_MobileBitmap;
			}
		}

		static ShaderUtilities()
		{
			Keyword_Bevel = "BEVEL_ON";
			Keyword_Glow = "GLOW_ON";
			Keyword_Underlay = "UNDERLAY_ON";
			Keyword_Ratios = "RATIOS_OFF";
			Keyword_MASK_SOFT = "MASK_SOFT";
			Keyword_MASK_HARD = "MASK_HARD";
			Keyword_MASK_TEX = "MASK_TEX";
			Keyword_Outline = "OUTLINE_ON";
			ShaderTag_ZTestMode = "unity_GUIZTestMode";
			ShaderTag_CullMode = "_CullMode";
			m_clamp = 1f;
			isInitialized = false;
			GetShaderPropertyIDs();
		}

		public static void GetShaderPropertyIDs()
		{
			if (!isInitialized)
			{
				isInitialized = true;
				ID_MainTex = global::UnityEngine.Shader.PropertyToID("_MainTex");
				ID_FaceTex = global::UnityEngine.Shader.PropertyToID("_FaceTex");
				ID_FaceColor = global::UnityEngine.Shader.PropertyToID("_FaceColor");
				ID_FaceDilate = global::UnityEngine.Shader.PropertyToID("_FaceDilate");
				ID_Shininess = global::UnityEngine.Shader.PropertyToID("_FaceShininess");
				ID_OutlineOffset1 = global::UnityEngine.Shader.PropertyToID("_OutlineOffset1");
				ID_OutlineOffset2 = global::UnityEngine.Shader.PropertyToID("_OutlineOffset2");
				ID_OutlineOffset3 = global::UnityEngine.Shader.PropertyToID("_OutlineOffset3");
				ID_OutlineMode = global::UnityEngine.Shader.PropertyToID("_OutlineMode");
				ID_IsoPerimeter = global::UnityEngine.Shader.PropertyToID("_IsoPerimeter");
				ID_Softness = global::UnityEngine.Shader.PropertyToID("_Softness");
				ID_UnderlayColor = global::UnityEngine.Shader.PropertyToID("_UnderlayColor");
				ID_UnderlayOffsetX = global::UnityEngine.Shader.PropertyToID("_UnderlayOffsetX");
				ID_UnderlayOffsetY = global::UnityEngine.Shader.PropertyToID("_UnderlayOffsetY");
				ID_UnderlayDilate = global::UnityEngine.Shader.PropertyToID("_UnderlayDilate");
				ID_UnderlaySoftness = global::UnityEngine.Shader.PropertyToID("_UnderlaySoftness");
				ID_UnderlayOffset = global::UnityEngine.Shader.PropertyToID("_UnderlayOffset");
				ID_UnderlayIsoPerimeter = global::UnityEngine.Shader.PropertyToID("_UnderlayIsoPerimeter");
				ID_WeightNormal = global::UnityEngine.Shader.PropertyToID("_WeightNormal");
				ID_WeightBold = global::UnityEngine.Shader.PropertyToID("_WeightBold");
				ID_OutlineTex = global::UnityEngine.Shader.PropertyToID("_OutlineTex");
				ID_OutlineWidth = global::UnityEngine.Shader.PropertyToID("_OutlineWidth");
				ID_OutlineSoftness = global::UnityEngine.Shader.PropertyToID("_OutlineSoftness");
				ID_OutlineColor = global::UnityEngine.Shader.PropertyToID("_OutlineColor");
				ID_Outline2Color = global::UnityEngine.Shader.PropertyToID("_Outline2Color");
				ID_Outline2Width = global::UnityEngine.Shader.PropertyToID("_Outline2Width");
				ID_Padding = global::UnityEngine.Shader.PropertyToID("_Padding");
				ID_GradientScale = global::UnityEngine.Shader.PropertyToID("_GradientScale");
				ID_ScaleX = global::UnityEngine.Shader.PropertyToID("_ScaleX");
				ID_ScaleY = global::UnityEngine.Shader.PropertyToID("_ScaleY");
				ID_PerspectiveFilter = global::UnityEngine.Shader.PropertyToID("_PerspectiveFilter");
				ID_Sharpness = global::UnityEngine.Shader.PropertyToID("_Sharpness");
				ID_TextureWidth = global::UnityEngine.Shader.PropertyToID("_TextureWidth");
				ID_TextureHeight = global::UnityEngine.Shader.PropertyToID("_TextureHeight");
				ID_BevelAmount = global::UnityEngine.Shader.PropertyToID("_Bevel");
				ID_LightAngle = global::UnityEngine.Shader.PropertyToID("_LightAngle");
				ID_EnvMap = global::UnityEngine.Shader.PropertyToID("_Cube");
				ID_EnvMatrix = global::UnityEngine.Shader.PropertyToID("_EnvMatrix");
				ID_EnvMatrixRotation = global::UnityEngine.Shader.PropertyToID("_EnvMatrixRotation");
				ID_GlowColor = global::UnityEngine.Shader.PropertyToID("_GlowColor");
				ID_GlowOffset = global::UnityEngine.Shader.PropertyToID("_GlowOffset");
				ID_GlowPower = global::UnityEngine.Shader.PropertyToID("_GlowPower");
				ID_GlowOuter = global::UnityEngine.Shader.PropertyToID("_GlowOuter");
				ID_GlowInner = global::UnityEngine.Shader.PropertyToID("_GlowInner");
				ID_MaskCoord = global::UnityEngine.Shader.PropertyToID("_MaskCoord");
				ID_ClipRect = global::UnityEngine.Shader.PropertyToID("_ClipRect");
				ID_UseClipRect = global::UnityEngine.Shader.PropertyToID("_UseClipRect");
				ID_MaskSoftnessX = global::UnityEngine.Shader.PropertyToID("_MaskSoftnessX");
				ID_MaskSoftnessY = global::UnityEngine.Shader.PropertyToID("_MaskSoftnessY");
				ID_VertexOffsetX = global::UnityEngine.Shader.PropertyToID("_VertexOffsetX");
				ID_VertexOffsetY = global::UnityEngine.Shader.PropertyToID("_VertexOffsetY");
				ID_StencilID = global::UnityEngine.Shader.PropertyToID("_Stencil");
				ID_StencilOp = global::UnityEngine.Shader.PropertyToID("_StencilOp");
				ID_StencilComp = global::UnityEngine.Shader.PropertyToID("_StencilComp");
				ID_StencilReadMask = global::UnityEngine.Shader.PropertyToID("_StencilReadMask");
				ID_StencilWriteMask = global::UnityEngine.Shader.PropertyToID("_StencilWriteMask");
				ID_ShaderFlags = global::UnityEngine.Shader.PropertyToID("_ShaderFlags");
				ID_ScaleRatio_A = global::UnityEngine.Shader.PropertyToID("_ScaleRatioA");
				ID_ScaleRatio_B = global::UnityEngine.Shader.PropertyToID("_ScaleRatioB");
				ID_ScaleRatio_C = global::UnityEngine.Shader.PropertyToID("_ScaleRatioC");
				if (k_ShaderRef_MobileSDF == null)
				{
					k_ShaderRef_MobileSDF = global::UnityEngine.Shader.Find("TextMeshPro/Mobile/Distance Field");
				}
				if (k_ShaderRef_MobileBitmap == null)
				{
					k_ShaderRef_MobileBitmap = global::UnityEngine.Shader.Find("TextMeshPro/Mobile/Bitmap");
				}
			}
		}

		public static void UpdateShaderRatios(global::UnityEngine.Material mat)
		{
			float num = 1f;
			float num2 = 1f;
			float num3 = 1f;
			bool flag = !global::System.Linq.Enumerable.Contains(mat.shaderKeywords, Keyword_Ratios);
			if (mat.HasProperty(ID_GradientScale) && mat.HasProperty(ID_FaceDilate))
			{
				float num4 = mat.GetFloat(ID_GradientScale);
				float num5 = mat.GetFloat(ID_FaceDilate);
				float num6 = mat.GetFloat(ID_OutlineWidth);
				float num7 = mat.GetFloat(ID_OutlineSoftness);
				float num8 = global::UnityEngine.Mathf.Max(mat.GetFloat(ID_WeightNormal), mat.GetFloat(ID_WeightBold)) / 4f;
				float num9 = global::UnityEngine.Mathf.Max(1f, num8 + num5 + num6 + num7);
				num = (flag ? ((num4 - m_clamp) / (num4 * num9)) : 1f);
				mat.SetFloat(ID_ScaleRatio_A, num);
				if (mat.HasProperty(ID_GlowOffset))
				{
					float num10 = mat.GetFloat(ID_GlowOffset);
					float num11 = mat.GetFloat(ID_GlowOuter);
					float num12 = (num8 + num5) * (num4 - m_clamp);
					num9 = global::UnityEngine.Mathf.Max(1f, num10 + num11);
					num2 = (flag ? (global::UnityEngine.Mathf.Max(0f, num4 - m_clamp - num12) / (num4 * num9)) : 1f);
					mat.SetFloat(ID_ScaleRatio_B, num2);
				}
				if (mat.HasProperty(ID_UnderlayOffsetX))
				{
					float f = mat.GetFloat(ID_UnderlayOffsetX);
					float f2 = mat.GetFloat(ID_UnderlayOffsetY);
					float num13 = mat.GetFloat(ID_UnderlayDilate);
					float num14 = mat.GetFloat(ID_UnderlaySoftness);
					float num15 = (num8 + num5) * (num4 - m_clamp);
					num9 = global::UnityEngine.Mathf.Max(1f, global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(f), global::UnityEngine.Mathf.Abs(f2)) + num13 + num14);
					num3 = (flag ? (global::UnityEngine.Mathf.Max(0f, num4 - m_clamp - num15) / (num4 * num9)) : 1f);
					mat.SetFloat(ID_ScaleRatio_C, num3);
				}
			}
		}

		public static global::UnityEngine.Vector4 GetFontExtent(global::UnityEngine.Material material)
		{
			return global::UnityEngine.Vector4.zero;
		}

		public static bool IsMaskingEnabled(global::UnityEngine.Material material)
		{
			if (material == null || !material.HasProperty(ID_ClipRect))
			{
				return false;
			}
			if (global::System.Linq.Enumerable.Contains(material.shaderKeywords, Keyword_MASK_SOFT) || global::System.Linq.Enumerable.Contains(material.shaderKeywords, Keyword_MASK_HARD) || global::System.Linq.Enumerable.Contains(material.shaderKeywords, Keyword_MASK_TEX))
			{
				return true;
			}
			return false;
		}

		public static float GetPadding(global::UnityEngine.Material material, bool enableExtraPadding, bool isBold)
		{
			if (!isInitialized)
			{
				GetShaderPropertyIDs();
			}
			if (material == null)
			{
				return 0f;
			}
			int num = (enableExtraPadding ? 4 : 0);
			if (!material.HasProperty(ID_GradientScale))
			{
				if (material.HasProperty(ID_Padding))
				{
					num += (int)material.GetFloat(ID_Padding);
				}
				return (float)num + 1f;
			}
			if (material.HasProperty(ID_IsoPerimeter))
			{
				return ComputePaddingForProperties(material) + 0.25f + (float)num;
			}
			global::UnityEngine.Vector4 zero = global::UnityEngine.Vector4.zero;
			global::UnityEngine.Vector4 zero2 = global::UnityEngine.Vector4.zero;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			float num8 = 0f;
			float num9 = 0f;
			float num10 = 0f;
			float num11 = 0f;
			UpdateShaderRatios(material);
			string[] shaderKeywords = material.shaderKeywords;
			if (material.HasProperty(ID_ScaleRatio_A))
			{
				num5 = material.GetFloat(ID_ScaleRatio_A);
			}
			if (material.HasProperty(ID_FaceDilate))
			{
				num2 = material.GetFloat(ID_FaceDilate) * num5;
			}
			if (material.HasProperty(ID_OutlineSoftness))
			{
				num3 = material.GetFloat(ID_OutlineSoftness) * num5;
			}
			if (material.HasProperty(ID_OutlineWidth))
			{
				num4 = material.GetFloat(ID_OutlineWidth) * num5;
			}
			num11 = num4 + num3 + num2;
			if (material.HasProperty(ID_GlowOffset) && global::System.Linq.Enumerable.Contains(shaderKeywords, Keyword_Glow))
			{
				if (material.HasProperty(ID_ScaleRatio_B))
				{
					num6 = material.GetFloat(ID_ScaleRatio_B);
				}
				num8 = material.GetFloat(ID_GlowOffset) * num6;
				num9 = material.GetFloat(ID_GlowOuter) * num6;
			}
			num11 = global::UnityEngine.Mathf.Max(num11, num2 + num8 + num9);
			if (material.HasProperty(ID_UnderlaySoftness) && global::System.Linq.Enumerable.Contains(shaderKeywords, Keyword_Underlay))
			{
				if (material.HasProperty(ID_ScaleRatio_C))
				{
					num7 = material.GetFloat(ID_ScaleRatio_C);
				}
				float num12 = 0f;
				float num13 = 0f;
				float num14 = 0f;
				float num15 = 0f;
				if (material.HasProperty(ID_UnderlayOffset))
				{
					global::UnityEngine.Vector2 vector = material.GetVector(ID_UnderlayOffset);
					num12 = vector.x;
					num13 = vector.y;
					num14 = material.GetFloat(ID_UnderlayDilate);
					num15 = material.GetFloat(ID_UnderlaySoftness);
				}
				else if (material.HasProperty(ID_UnderlayOffsetX))
				{
					num12 = material.GetFloat(ID_UnderlayOffsetX) * num7;
					num13 = material.GetFloat(ID_UnderlayOffsetY) * num7;
					num14 = material.GetFloat(ID_UnderlayDilate) * num7;
					num15 = material.GetFloat(ID_UnderlaySoftness) * num7;
				}
				zero.x = global::UnityEngine.Mathf.Max(zero.x, num2 + num14 + num15 - num12);
				zero.y = global::UnityEngine.Mathf.Max(zero.y, num2 + num14 + num15 - num13);
				zero.z = global::UnityEngine.Mathf.Max(zero.z, num2 + num14 + num15 + num12);
				zero.w = global::UnityEngine.Mathf.Max(zero.w, num2 + num14 + num15 + num13);
			}
			zero.x = global::UnityEngine.Mathf.Max(zero.x, num11);
			zero.y = global::UnityEngine.Mathf.Max(zero.y, num11);
			zero.z = global::UnityEngine.Mathf.Max(zero.z, num11);
			zero.w = global::UnityEngine.Mathf.Max(zero.w, num11);
			zero.x += num;
			zero.y += num;
			zero.z += num;
			zero.w += num;
			zero.x = global::UnityEngine.Mathf.Min(zero.x, 1f);
			zero.y = global::UnityEngine.Mathf.Min(zero.y, 1f);
			zero.z = global::UnityEngine.Mathf.Min(zero.z, 1f);
			zero.w = global::UnityEngine.Mathf.Min(zero.w, 1f);
			zero2.x = ((zero2.x < zero.x) ? zero.x : zero2.x);
			zero2.y = ((zero2.y < zero.y) ? zero.y : zero2.y);
			zero2.z = ((zero2.z < zero.z) ? zero.z : zero2.z);
			zero2.w = ((zero2.w < zero.w) ? zero.w : zero2.w);
			num10 = material.GetFloat(ID_GradientScale);
			zero *= num10;
			num11 = global::UnityEngine.Mathf.Max(zero.x, zero.y);
			num11 = global::UnityEngine.Mathf.Max(zero.z, num11);
			num11 = global::UnityEngine.Mathf.Max(zero.w, num11);
			return num11 + 1.25f;
		}

		private static float ComputePaddingForProperties(global::UnityEngine.Material mat)
		{
			global::UnityEngine.Vector4 vector = mat.GetVector(ID_IsoPerimeter);
			global::UnityEngine.Vector2 vector2 = mat.GetVector(ID_OutlineOffset1);
			global::UnityEngine.Vector2 vector3 = mat.GetVector(ID_OutlineOffset2);
			global::UnityEngine.Vector2 vector4 = mat.GetVector(ID_OutlineOffset3);
			bool num = mat.GetFloat(ID_OutlineMode) != 0f;
			global::UnityEngine.Vector4 vector5 = mat.GetVector(ID_Softness);
			float num2 = mat.GetFloat(ID_GradientScale);
			float a = global::UnityEngine.Mathf.Max(0f, vector.x + vector5.x * 0.5f);
			if (!num)
			{
				a = global::UnityEngine.Mathf.Max(a, vector.y + vector5.y * 0.5f + global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(vector2.x), global::UnityEngine.Mathf.Abs(vector2.y)));
				a = global::UnityEngine.Mathf.Max(a, vector.z + vector5.z * 0.5f + global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(vector3.x), global::UnityEngine.Mathf.Abs(vector3.y)));
				a = global::UnityEngine.Mathf.Max(a, vector.w + vector5.w * 0.5f + global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(vector4.x), global::UnityEngine.Mathf.Abs(vector4.y)));
			}
			else
			{
				float num3 = global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(vector2.x), global::UnityEngine.Mathf.Abs(vector2.y));
				float num4 = global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(vector3.x), global::UnityEngine.Mathf.Abs(vector3.y));
				a = global::UnityEngine.Mathf.Max(a, vector.y + vector5.y * 0.5f + num3);
				a = global::UnityEngine.Mathf.Max(a, vector.z + vector5.z * 0.5f + num4);
				float num5 = global::UnityEngine.Mathf.Max(num3, num4);
				a += global::UnityEngine.Mathf.Max(0f, vector.w + vector5.w * 0.5f - global::UnityEngine.Mathf.Max(0f, a - num5));
			}
			global::UnityEngine.Vector2 vector6 = mat.GetVector(ID_UnderlayOffset);
			float num6 = mat.GetFloat(ID_UnderlayDilate);
			float num7 = mat.GetFloat(ID_UnderlaySoftness);
			a = global::UnityEngine.Mathf.Max(a, num6 + num7 * 0.5f + global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(vector6.x), global::UnityEngine.Mathf.Abs(vector6.y)));
			return a * num2;
		}

		public static float GetPadding(global::UnityEngine.Material[] materials, bool enableExtraPadding, bool isBold)
		{
			if (!isInitialized)
			{
				GetShaderPropertyIDs();
			}
			if (materials == null)
			{
				return 0f;
			}
			int num = (enableExtraPadding ? 4 : 0);
			if (materials[0].HasProperty(ID_Padding))
			{
				return (float)num + materials[0].GetFloat(ID_Padding);
			}
			global::UnityEngine.Vector4 zero = global::UnityEngine.Vector4.zero;
			global::UnityEngine.Vector4 zero2 = global::UnityEngine.Vector4.zero;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			float num8 = 0f;
			float num9 = 0f;
			float num10 = 0f;
			for (int i = 0; i < materials.Length; i++)
			{
				UpdateShaderRatios(materials[i]);
				string[] shaderKeywords = materials[i].shaderKeywords;
				if (materials[i].HasProperty(ID_ScaleRatio_A))
				{
					num5 = materials[i].GetFloat(ID_ScaleRatio_A);
				}
				if (materials[i].HasProperty(ID_FaceDilate))
				{
					num2 = materials[i].GetFloat(ID_FaceDilate) * num5;
				}
				if (materials[i].HasProperty(ID_OutlineSoftness))
				{
					num3 = materials[i].GetFloat(ID_OutlineSoftness) * num5;
				}
				if (materials[i].HasProperty(ID_OutlineWidth))
				{
					num4 = materials[i].GetFloat(ID_OutlineWidth) * num5;
				}
				num10 = num4 + num3 + num2;
				if (materials[i].HasProperty(ID_GlowOffset) && global::System.Linq.Enumerable.Contains(shaderKeywords, Keyword_Glow))
				{
					if (materials[i].HasProperty(ID_ScaleRatio_B))
					{
						num6 = materials[i].GetFloat(ID_ScaleRatio_B);
					}
					num8 = materials[i].GetFloat(ID_GlowOffset) * num6;
					num9 = materials[i].GetFloat(ID_GlowOuter) * num6;
				}
				num10 = global::UnityEngine.Mathf.Max(num10, num2 + num8 + num9);
				if (materials[i].HasProperty(ID_UnderlaySoftness) && global::System.Linq.Enumerable.Contains(shaderKeywords, Keyword_Underlay))
				{
					if (materials[i].HasProperty(ID_ScaleRatio_C))
					{
						num7 = materials[i].GetFloat(ID_ScaleRatio_C);
					}
					float num11 = materials[i].GetFloat(ID_UnderlayOffsetX) * num7;
					float num12 = materials[i].GetFloat(ID_UnderlayOffsetY) * num7;
					float num13 = materials[i].GetFloat(ID_UnderlayDilate) * num7;
					float num14 = materials[i].GetFloat(ID_UnderlaySoftness) * num7;
					zero.x = global::UnityEngine.Mathf.Max(zero.x, num2 + num13 + num14 - num11);
					zero.y = global::UnityEngine.Mathf.Max(zero.y, num2 + num13 + num14 - num12);
					zero.z = global::UnityEngine.Mathf.Max(zero.z, num2 + num13 + num14 + num11);
					zero.w = global::UnityEngine.Mathf.Max(zero.w, num2 + num13 + num14 + num12);
				}
				zero.x = global::UnityEngine.Mathf.Max(zero.x, num10);
				zero.y = global::UnityEngine.Mathf.Max(zero.y, num10);
				zero.z = global::UnityEngine.Mathf.Max(zero.z, num10);
				zero.w = global::UnityEngine.Mathf.Max(zero.w, num10);
				zero.x += num;
				zero.y += num;
				zero.z += num;
				zero.w += num;
				zero.x = global::UnityEngine.Mathf.Min(zero.x, 1f);
				zero.y = global::UnityEngine.Mathf.Min(zero.y, 1f);
				zero.z = global::UnityEngine.Mathf.Min(zero.z, 1f);
				zero.w = global::UnityEngine.Mathf.Min(zero.w, 1f);
				zero2.x = ((zero2.x < zero.x) ? zero.x : zero2.x);
				zero2.y = ((zero2.y < zero.y) ? zero.y : zero2.y);
				zero2.z = ((zero2.z < zero.z) ? zero.z : zero2.z);
				zero2.w = ((zero2.w < zero.w) ? zero.w : zero2.w);
			}
			float num15 = materials[0].GetFloat(ID_GradientScale);
			zero *= num15;
			num10 = global::UnityEngine.Mathf.Max(zero.x, zero.y);
			num10 = global::UnityEngine.Mathf.Max(zero.z, num10);
			num10 = global::UnityEngine.Mathf.Max(zero.w, num10);
			return num10 + 0.25f;
		}
	}
}
