namespace UnityEngine.Rendering.Universal
{
	internal class LightCookieManager : global::System.IDisposable
	{
		private static class ShaderProperty
		{
			public static readonly int mainLightTexture = global::UnityEngine.Shader.PropertyToID("_MainLightCookieTexture");

			public static readonly int mainLightWorldToLight = global::UnityEngine.Shader.PropertyToID("_MainLightWorldToLight");

			public static readonly int mainLightCookieTextureFormat = global::UnityEngine.Shader.PropertyToID("_MainLightCookieTextureFormat");

			public static readonly int additionalLightsCookieAtlasTexture = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsCookieAtlasTexture");

			public static readonly int additionalLightsCookieAtlasTextureFormat = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsCookieAtlasTextureFormat");

			public static readonly int additionalLightsCookieEnableBits = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsCookieEnableBits");

			public static readonly int additionalLightsCookieAtlasUVRectBuffer = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsCookieAtlasUVRectBuffer");

			public static readonly int additionalLightsCookieAtlasUVRects = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsCookieAtlasUVRects");

			public static readonly int additionalLightsWorldToLightBuffer = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsWorldToLightBuffer");

			public static readonly int additionalLightsLightTypeBuffer = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsLightTypeBuffer");

			public static readonly int additionalLightsWorldToLights = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsWorldToLights");

			public static readonly int additionalLightsLightTypes = global::UnityEngine.Shader.PropertyToID("_AdditionalLightsLightTypes");
		}

		private enum LightCookieShaderFormat
		{
			None = -1,
			RGB = 0,
			Alpha = 1,
			Red = 2
		}

		public struct Settings
		{
			public struct AtlasSettings
			{
				public global::UnityEngine.Vector2Int resolution;

				public global::UnityEngine.Experimental.Rendering.GraphicsFormat format;

				public bool isPow2
				{
					get
					{
						if (global::UnityEngine.Mathf.IsPowerOfTwo(resolution.x))
						{
							return global::UnityEngine.Mathf.IsPowerOfTwo(resolution.y);
						}
						return false;
					}
				}

				public bool isSquare => resolution.x == resolution.y;
			}

			public global::UnityEngine.Rendering.Universal.LightCookieManager.Settings.AtlasSettings atlas;

			public int maxAdditionalLights;

			public float cubeOctahedralSizeScale;

			public bool useStructuredBuffer;

			public static global::UnityEngine.Rendering.Universal.LightCookieManager.Settings Create()
			{
				global::UnityEngine.Rendering.Universal.LightCookieManager.Settings result = default(global::UnityEngine.Rendering.Universal.LightCookieManager.Settings);
				result.atlas.resolution = new global::UnityEngine.Vector2Int(1024, 1024);
				result.atlas.format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB;
				result.maxAdditionalLights = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights;
				result.cubeOctahedralSizeScale = 2.5f;
				result.useStructuredBuffer = global::UnityEngine.Rendering.Universal.RenderingUtils.useStructuredBuffer;
				return result;
			}
		}

		private struct LightCookieMapping
		{
			public ushort visibleLightIndex;

			public ushort lightBufferIndex;

			public global::UnityEngine.Light light;

			public static global::System.Func<global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping, global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping, int> s_CompareByCookieSize = delegate(global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping a, global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping b)
			{
				global::UnityEngine.Texture cookie = a.light.cookie;
				global::UnityEngine.Texture cookie2 = b.light.cookie;
				int num = cookie.width * cookie.height;
				int num2 = cookie2.width * cookie2.height - num;
				if (num2 == 0)
				{
					int instanceID = cookie.GetInstanceID();
					int instanceID2 = cookie2.GetInstanceID();
					return instanceID - instanceID2;
				}
				return num2;
			};

			public static global::System.Func<global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping, global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping, int> s_CompareByBufferIndex = (global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping a, global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping b) => a.lightBufferIndex - b.lightBufferIndex;
		}

		private readonly struct WorkSlice<T>
		{
			private readonly T[] m_Data;

			private readonly int m_Start;

			private readonly int m_Length;

			public T this[int index]
			{
				get
				{
					return m_Data[m_Start + index];
				}
				set
				{
					m_Data[m_Start + index] = value;
				}
			}

			public int length => m_Length;

			public int capacity => m_Data.Length;

			public WorkSlice(T[] src, int srcLen = -1)
				: this(src, 0, srcLen)
			{
			}

			public WorkSlice(T[] src, int srcStart, int srcLen = -1)
			{
				m_Data = src;
				m_Start = srcStart;
				m_Length = ((srcLen < 0) ? src.Length : global::System.Math.Min(srcLen, src.Length));
			}

			public void Sort(global::System.Func<T, T, int> compare)
			{
				if (m_Length > 1)
				{
					global::UnityEngine.Rendering.Universal.Sorting.QuickSort(m_Data, m_Start, m_Start + m_Length - 1, compare);
				}
			}
		}

		private class WorkMemory
		{
			public global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping[] lightMappings;

			public global::UnityEngine.Vector4[] uvRects;

			public void Resize(int size)
			{
				if (!(size <= lightMappings?.Length))
				{
					size = global::System.Math.Max(size, (size + 15) / 16 * 16);
					lightMappings = new global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping[size];
					uvRects = new global::UnityEngine.Vector4[size];
				}
			}
		}

		private class LightCookieShaderData : global::System.IDisposable
		{
			private int m_Size;

			private bool m_UseStructuredBuffer;

			private global::UnityEngine.Matrix4x4[] m_WorldToLightCpuData;

			private global::UnityEngine.Vector4[] m_AtlasUVRectCpuData;

			private float[] m_LightTypeCpuData;

			private global::UnityEngine.Rendering.Universal.ShaderBitArray m_CookieEnableBitsCpuData;

			private global::UnityEngine.ComputeBuffer m_WorldToLightBuffer;

			private global::UnityEngine.ComputeBuffer m_AtlasUVRectBuffer;

			private global::UnityEngine.ComputeBuffer m_LightTypeBuffer;

			public global::UnityEngine.Matrix4x4[] worldToLights => m_WorldToLightCpuData;

			public global::UnityEngine.Rendering.Universal.ShaderBitArray cookieEnableBits => m_CookieEnableBitsCpuData;

			public global::UnityEngine.Vector4[] atlasUVRects => m_AtlasUVRectCpuData;

			public float[] lightTypes => m_LightTypeCpuData;

			public bool isUploaded { get; set; }

			public LightCookieShaderData(int size, bool useStructuredBuffer)
			{
				m_UseStructuredBuffer = useStructuredBuffer;
				Resize(size);
			}

			public void Dispose()
			{
				if (m_UseStructuredBuffer)
				{
					m_WorldToLightBuffer?.Dispose();
					m_AtlasUVRectBuffer?.Dispose();
					m_LightTypeBuffer?.Dispose();
				}
			}

			public void Resize(int size)
			{
				if (size > m_Size)
				{
					if (m_Size > 0)
					{
						Dispose();
					}
					m_WorldToLightCpuData = new global::UnityEngine.Matrix4x4[size];
					m_AtlasUVRectCpuData = new global::UnityEngine.Vector4[size];
					m_LightTypeCpuData = new float[size];
					m_CookieEnableBitsCpuData.Resize(size);
					if (m_UseStructuredBuffer)
					{
						m_WorldToLightBuffer = new global::UnityEngine.ComputeBuffer(size, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Matrix4x4>());
						m_AtlasUVRectBuffer = new global::UnityEngine.ComputeBuffer(size, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Vector4>());
						m_LightTypeBuffer = new global::UnityEngine.ComputeBuffer(size, global::System.Runtime.InteropServices.Marshal.SizeOf<float>());
					}
					m_Size = size;
				}
			}

			public void Upload(global::UnityEngine.Rendering.CommandBuffer cmd)
			{
				if (m_UseStructuredBuffer)
				{
					m_WorldToLightBuffer.SetData(m_WorldToLightCpuData);
					m_AtlasUVRectBuffer.SetData(m_AtlasUVRectCpuData);
					m_LightTypeBuffer.SetData(m_LightTypeCpuData);
					cmd.SetGlobalBuffer(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.additionalLightsWorldToLightBuffer, m_WorldToLightBuffer);
					cmd.SetGlobalBuffer(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.additionalLightsCookieAtlasUVRectBuffer, m_AtlasUVRectBuffer);
					cmd.SetGlobalBuffer(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.additionalLightsLightTypeBuffer, m_LightTypeBuffer);
				}
				else
				{
					cmd.SetGlobalMatrixArray(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.additionalLightsWorldToLights, m_WorldToLightCpuData);
					cmd.SetGlobalVectorArray(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.additionalLightsCookieAtlasUVRects, m_AtlasUVRectCpuData);
					cmd.SetGlobalFloatArray(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.additionalLightsLightTypes, m_LightTypeCpuData);
				}
				cmd.SetGlobalFloatArray(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.additionalLightsCookieEnableBits, m_CookieEnableBitsCpuData.data);
				isUploaded = true;
			}

			public void Clear(global::UnityEngine.Rendering.CommandBuffer cmd)
			{
				if (isUploaded)
				{
					m_CookieEnableBitsCpuData.Clear();
					cmd.SetGlobalFloatArray(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.additionalLightsCookieEnableBits, m_CookieEnableBitsCpuData.data);
					isUploaded = false;
				}
			}
		}

		private static readonly global::UnityEngine.Matrix4x4 s_DirLightProj = global::UnityEngine.Matrix4x4.Ortho(-0.5f, 0.5f, -0.5f, 0.5f, -0.5f, 0.5f);

		private global::UnityEngine.Rendering.Texture2DAtlas m_AdditionalLightsCookieAtlas;

		private global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieShaderData m_AdditionalLightsCookieShaderData;

		private readonly global::UnityEngine.Rendering.Universal.LightCookieManager.Settings m_Settings;

		private global::UnityEngine.Rendering.Universal.LightCookieManager.WorkMemory m_WorkMem;

		private int[] m_VisibleLightIndexToShaderDataIndex;

		private const int k_MaxCookieSizeDivisor = 16;

		private int m_CookieSizeDivisor = 1;

		private uint m_PrevCookieRequestPixelCount = uint.MaxValue;

		private int m_PrevWarnFrame = -1;

		internal bool IsKeywordLightCookieEnabled { get; private set; }

		internal global::UnityEngine.Rendering.RTHandle AdditionalLightsCookieAtlasTexture => m_AdditionalLightsCookieAtlas?.AtlasTexture;

		public LightCookieManager(ref global::UnityEngine.Rendering.Universal.LightCookieManager.Settings settings)
		{
			m_Settings = settings;
			m_WorkMem = new global::UnityEngine.Rendering.Universal.LightCookieManager.WorkMemory();
		}

		private void InitAdditionalLights(int size)
		{
			m_AdditionalLightsCookieAtlas = new global::UnityEngine.Rendering.Texture2DAtlas(m_Settings.atlas.resolution.x, m_Settings.atlas.resolution.y, m_Settings.atlas.format, global::UnityEngine.FilterMode.Bilinear, powerOfTwoPadding: false, "Universal Light Cookie Atlas", useMipMap: false);
			m_AdditionalLightsCookieShaderData = new global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieShaderData(size, m_Settings.useStructuredBuffer);
			m_VisibleLightIndexToShaderDataIndex = new int[m_Settings.maxAdditionalLights + 1];
			m_CookieSizeDivisor = 1;
			m_PrevCookieRequestPixelCount = uint.MaxValue;
		}

		public bool isInitialized()
		{
			if (m_AdditionalLightsCookieAtlas != null)
			{
				return m_AdditionalLightsCookieShaderData != null;
			}
			return false;
		}

		public void Dispose()
		{
			m_AdditionalLightsCookieAtlas?.Release();
			m_AdditionalLightsCookieShaderData?.Dispose();
		}

		public int GetLightCookieShaderDataIndex(int visibleLightIndex)
		{
			if (!isInitialized())
			{
				return -1;
			}
			return m_VisibleLightIndexToShaderDataIndex[visibleLightIndex];
		}

		public void Setup(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(global::UnityEngine.Rendering.Universal.URPProfileId.LightCookies)))
			{
				bool flag = lightData.mainLightIndex >= 0;
				if (flag)
				{
					global::UnityEngine.Rendering.VisibleLight visibleMainLight = lightData.visibleLights[lightData.mainLightIndex];
					flag = SetupMainLight(cmd, ref visibleMainLight);
				}
				bool flag2 = lightData.additionalLightsCount > 0;
				if (flag2)
				{
					flag2 = SetupAdditionalLights(cmd, lightData);
				}
				if (!flag2)
				{
					if (m_VisibleLightIndexToShaderDataIndex != null && m_AdditionalLightsCookieShaderData.isUploaded)
					{
						int num = m_VisibleLightIndexToShaderDataIndex.Length;
						for (int i = 0; i < num; i++)
						{
							m_VisibleLightIndexToShaderDataIndex[i] = -1;
						}
					}
					m_AdditionalLightsCookieShaderData?.Clear(cmd);
				}
				IsKeywordLightCookieEnabled = flag || flag2;
				cmd.SetKeyword(in global::UnityEngine.Rendering.Universal.ShaderGlobalKeywords.LightCookies, IsKeywordLightCookieEnabled);
			}
		}

		private bool SetupMainLight(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.VisibleLight visibleMainLight)
		{
			global::UnityEngine.Light light = visibleMainLight.light;
			global::UnityEngine.Texture cookie = light.cookie;
			bool num = cookie != null;
			if (num)
			{
				global::UnityEngine.Matrix4x4 uvTransform = global::UnityEngine.Matrix4x4.identity;
				float value = (float)GetLightCookieShaderFormat(cookie.graphicsFormat);
				if (light.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData>(out var component))
				{
					GetLightUVScaleOffset(ref component, ref uvTransform);
				}
				global::UnityEngine.Matrix4x4 value2 = s_DirLightProj * uvTransform * visibleMainLight.localToWorldMatrix.inverse;
				cmd.SetGlobalTexture(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.mainLightTexture, cookie);
				cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.mainLightWorldToLight, value2);
				cmd.SetGlobalFloat(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.mainLightCookieTextureFormat, value);
				return num;
			}
			cmd.SetGlobalTexture(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.mainLightTexture, global::UnityEngine.Texture2D.whiteTexture);
			cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.mainLightWorldToLight, global::UnityEngine.Matrix4x4.identity);
			cmd.SetGlobalFloat(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.mainLightCookieTextureFormat, -1f);
			return num;
		}

		private global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieShaderFormat GetLightCookieShaderFormat(global::UnityEngine.Experimental.Rendering.GraphicsFormat cookieFormat)
		{
			switch (cookieFormat)
			{
			default:
				return global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieShaderFormat.RGB;
			case (global::UnityEngine.Experimental.Rendering.GraphicsFormat)54:
			case (global::UnityEngine.Experimental.Rendering.GraphicsFormat)55:
				return global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieShaderFormat.Alpha;
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_SRGB:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_SNorm:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UInt:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_SInt:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_UNorm:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SNorm:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_UInt:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SInt:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_UInt:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SInt:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R_BC4_UNorm:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R_BC4_SNorm:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R_EAC_UNorm:
			case global::UnityEngine.Experimental.Rendering.GraphicsFormat.R_EAC_SNorm:
				return global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieShaderFormat.Red;
			}
		}

		private void GetLightUVScaleOffset(ref global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData additionalLightData, ref global::UnityEngine.Matrix4x4 uvTransform)
		{
			global::UnityEngine.Vector2 vector = global::UnityEngine.Vector2.one / additionalLightData.lightCookieSize;
			global::UnityEngine.Vector2 lightCookieOffset = additionalLightData.lightCookieOffset;
			if (global::UnityEngine.Mathf.Abs(vector.x) < global::Unity.Mathematics.half.MinValue)
			{
				vector.x = global::UnityEngine.Mathf.Sign(vector.x) * global::Unity.Mathematics.half.MinValue;
			}
			if (global::UnityEngine.Mathf.Abs(vector.y) < global::Unity.Mathematics.half.MinValue)
			{
				vector.y = global::UnityEngine.Mathf.Sign(vector.y) * global::Unity.Mathematics.half.MinValue;
			}
			uvTransform = global::UnityEngine.Matrix4x4.Scale(new global::UnityEngine.Vector3(vector.x, vector.y, 1f));
			uvTransform.SetColumn(3, new global::UnityEngine.Vector4((0f - lightCookieOffset.x) * vector.x, (0f - lightCookieOffset.y) * vector.y, 0f, 1f));
		}

		private bool SetupAdditionalLights(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalLightData lightData)
		{
			int size = global::System.Math.Min(m_Settings.maxAdditionalLights, lightData.visibleLights.Length);
			m_WorkMem.Resize(size);
			int num = FilterAndValidateAdditionalLights(lightData, m_WorkMem.lightMappings);
			if (num <= 0)
			{
				return false;
			}
			if (!isInitialized())
			{
				InitAdditionalLights(num);
			}
			global::UnityEngine.Rendering.Universal.LightCookieManager.WorkSlice<global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping> validLightMappings = new global::UnityEngine.Rendering.Universal.LightCookieManager.WorkSlice<global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping>(m_WorkMem.lightMappings, num);
			int srcLen = UpdateAdditionalLightsAtlas(cmd, ref validLightMappings, m_WorkMem.uvRects);
			global::UnityEngine.Rendering.Universal.LightCookieManager.WorkSlice<global::UnityEngine.Vector4> validUvRects = new global::UnityEngine.Rendering.Universal.LightCookieManager.WorkSlice<global::UnityEngine.Vector4>(m_WorkMem.uvRects, srcLen);
			UploadAdditionalLights(cmd, lightData, ref validLightMappings, ref validUvRects);
			return validUvRects.length > 0;
		}

		private int FilterAndValidateAdditionalLights(global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping[] validLightMappings)
		{
			int mainLightIndex = lightData.mainLightIndex;
			int num = 0;
			int num2 = 0;
			int length = lightData.visibleLights.Length;
			global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping lightCookieMapping = default(global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping);
			for (int i = 0; i < length; i++)
			{
				if (i == mainLightIndex)
				{
					num--;
					continue;
				}
				ref global::UnityEngine.Rendering.VisibleLight reference = ref lightData.visibleLights.UnsafeElementAtMutable(i);
				global::UnityEngine.Light light = reference.light;
				if (light.cookie == null)
				{
					continue;
				}
				global::UnityEngine.LightType lightType = reference.lightType;
				if (lightType != global::UnityEngine.LightType.Spot && lightType != global::UnityEngine.LightType.Point && lightType != global::UnityEngine.LightType.Directional)
				{
					global::UnityEngine.Debug.LogWarning("Additional " + lightType.ToString() + " light called '" + light.name + "' has a light cookie which will not be visible.", light);
					continue;
				}
				lightCookieMapping.visibleLightIndex = (ushort)i;
				lightCookieMapping.lightBufferIndex = (ushort)(i + num);
				lightCookieMapping.light = light;
				if (lightCookieMapping.lightBufferIndex >= validLightMappings.Length || num2 + 1 >= validLightMappings.Length)
				{
					if (length > m_Settings.maxAdditionalLights && global::UnityEngine.Time.frameCount - m_PrevWarnFrame > 3600)
					{
						m_PrevWarnFrame = global::UnityEngine.Time.frameCount;
						global::UnityEngine.Debug.LogWarning("Max light cookies (" + validLightMappings.Length + ") reached. Some visible lights (" + (length - i - 1) + ") might skip light cookie rendering.");
					}
					break;
				}
				validLightMappings[num2++] = lightCookieMapping;
			}
			return num2;
		}

		private int UpdateAdditionalLightsAtlas(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.LightCookieManager.WorkSlice<global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping> validLightMappings, global::UnityEngine.Vector4[] textureAtlasUVRects)
		{
			validLightMappings.Sort(global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping.s_CompareByCookieSize);
			uint num = ComputeCookieRequestPixelCount(ref validLightMappings);
			global::UnityEngine.Vector2Int referenceSize = m_AdditionalLightsCookieAtlas.AtlasTexture.referenceSize;
			float requestAtlasRatio = (float)num / (float)(referenceSize.x * referenceSize.y);
			int num2 = ApproximateCookieSizeDivisor(requestAtlasRatio);
			if (num2 < m_CookieSizeDivisor && num < m_PrevCookieRequestPixelCount)
			{
				m_AdditionalLightsCookieAtlas.ResetAllocator();
				m_CookieSizeDivisor = num2;
			}
			int num3 = 0;
			while (num3 <= 0)
			{
				num3 = FetchUVRects(cmd, ref validLightMappings, textureAtlasUVRects, m_CookieSizeDivisor);
				if (num3 <= 0)
				{
					m_AdditionalLightsCookieAtlas.ResetAllocator();
					m_CookieSizeDivisor = global::UnityEngine.Mathf.Max(m_CookieSizeDivisor + 1, num2);
					m_PrevCookieRequestPixelCount = num;
				}
			}
			return num3;
		}

		private int FetchUVRects(global::UnityEngine.Rendering.CommandBuffer cmd, ref global::UnityEngine.Rendering.Universal.LightCookieManager.WorkSlice<global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping> validLightMappings, global::UnityEngine.Vector4[] textureAtlasUVRects, int cookieSizeDivisor)
		{
			int result = 0;
			for (int i = 0; i < validLightMappings.length; i++)
			{
				global::UnityEngine.Texture cookie = validLightMappings[i].light.cookie;
				global::UnityEngine.Vector4 zero = global::UnityEngine.Vector4.zero;
				zero = ((cookie.dimension != global::UnityEngine.Rendering.TextureDimension.Cube) ? Fetch2D(cmd, cookie, cookieSizeDivisor) : FetchCube(cmd, cookie, cookieSizeDivisor));
				if (!(zero != global::UnityEngine.Vector4.zero))
				{
					if (cookieSizeDivisor > 16)
					{
						global::UnityEngine.Debug.LogWarning("Light cookies atlas is extremely full! Some of the light cookies were discarded. Increase light cookie atlas space or reduce the amount of unique light cookies.");
						return result;
					}
					return 0;
				}
				if (!global::UnityEngine.SystemInfo.graphicsUVStartsAtTop)
				{
					zero.w = 1f - zero.w - zero.y;
				}
				textureAtlasUVRects[result++] = zero;
			}
			return result;
		}

		private uint ComputeCookieRequestPixelCount(ref global::UnityEngine.Rendering.Universal.LightCookieManager.WorkSlice<global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping> validLightMappings)
		{
			uint num = 0u;
			int num2 = 0;
			for (int i = 0; i < validLightMappings.length; i++)
			{
				global::UnityEngine.Texture cookie = validLightMappings[i].light.cookie;
				int instanceID = cookie.GetInstanceID();
				if (instanceID != num2)
				{
					num2 = instanceID;
					int num3 = cookie.width * cookie.height;
					num += (uint)num3;
				}
			}
			return num;
		}

		private int ApproximateCookieSizeDivisor(float requestAtlasRatio)
		{
			return (int)global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Ceil(global::UnityEngine.Mathf.Sqrt(requestAtlasRatio)), 1f);
		}

		private global::UnityEngine.Vector4 Fetch2D(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture cookie, int cookieSizeDivisor = 1)
		{
			global::UnityEngine.Vector4 scaleOffset = global::UnityEngine.Vector4.zero;
			int num = global::UnityEngine.Mathf.Max(cookie.width / cookieSizeDivisor, 4);
			int num2 = global::UnityEngine.Mathf.Max(cookie.height / cookieSizeDivisor, 4);
			global::UnityEngine.Vector2 cookieSize = new global::UnityEngine.Vector2(num, num2);
			if (m_AdditionalLightsCookieAtlas.IsCached(out scaleOffset, cookie))
			{
				m_AdditionalLightsCookieAtlas.UpdateTexture(cmd, cookie, ref scaleOffset);
			}
			else
			{
				m_AdditionalLightsCookieAtlas.AllocateTexture(cmd, ref scaleOffset, cookie, num, num2);
			}
			AdjustUVRect(ref scaleOffset, cookie, ref cookieSize);
			return scaleOffset;
		}

		private global::UnityEngine.Vector4 FetchCube(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Texture cookie, int cookieSizeDivisor = 1)
		{
			global::UnityEngine.Vector4 scaleOffset = global::UnityEngine.Vector4.zero;
			int num = global::UnityEngine.Mathf.Max(ComputeOctahedralCookieSize(cookie) / cookieSizeDivisor, 4);
			if (m_AdditionalLightsCookieAtlas.IsCached(out scaleOffset, cookie))
			{
				m_AdditionalLightsCookieAtlas.UpdateTexture(cmd, cookie, ref scaleOffset);
			}
			else
			{
				m_AdditionalLightsCookieAtlas.AllocateTexture(cmd, ref scaleOffset, cookie, num, num);
			}
			global::UnityEngine.Vector2 cookieSize = global::UnityEngine.Vector2.one * num;
			AdjustUVRect(ref scaleOffset, cookie, ref cookieSize);
			return scaleOffset;
		}

		private int ComputeOctahedralCookieSize(global::UnityEngine.Texture cookie)
		{
			int num = global::System.Math.Max(cookie.width, cookie.height);
			if (m_Settings.atlas.isPow2)
			{
				return num * global::UnityEngine.Mathf.NextPowerOfTwo((int)m_Settings.cubeOctahedralSizeScale);
			}
			return (int)((float)num * m_Settings.cubeOctahedralSizeScale + 0.5f);
		}

		private void AdjustUVRect(ref global::UnityEngine.Vector4 uvScaleOffset, global::UnityEngine.Texture cookie, ref global::UnityEngine.Vector2 cookieSize)
		{
			if (uvScaleOffset != global::UnityEngine.Vector4.zero)
			{
				ShrinkUVRect(ref uvScaleOffset, 0.5f, ref cookieSize);
			}
		}

		private void ShrinkUVRect(ref global::UnityEngine.Vector4 uvScaleOffset, float amountPixels, ref global::UnityEngine.Vector2 cookieSize)
		{
			global::UnityEngine.Vector2 vector = global::UnityEngine.Vector2.one * amountPixels / cookieSize;
			global::UnityEngine.Vector2 vector2 = (cookieSize - global::UnityEngine.Vector2.one * (amountPixels * 2f)) / cookieSize;
			uvScaleOffset.z += uvScaleOffset.x * vector.x;
			uvScaleOffset.w += uvScaleOffset.y * vector.y;
			uvScaleOffset.x *= vector2.x;
			uvScaleOffset.y *= vector2.y;
		}

		private void UploadAdditionalLights(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Universal.UniversalLightData lightData, ref global::UnityEngine.Rendering.Universal.LightCookieManager.WorkSlice<global::UnityEngine.Rendering.Universal.LightCookieManager.LightCookieMapping> validLightMappings, ref global::UnityEngine.Rendering.Universal.LightCookieManager.WorkSlice<global::UnityEngine.Vector4> validUvRects)
		{
			cmd.SetGlobalTexture(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.additionalLightsCookieAtlasTexture, m_AdditionalLightsCookieAtlas.AtlasTexture);
			cmd.SetGlobalFloat(global::UnityEngine.Rendering.Universal.LightCookieManager.ShaderProperty.additionalLightsCookieAtlasTextureFormat, (float)GetLightCookieShaderFormat(m_AdditionalLightsCookieAtlas.AtlasTexture.rt.graphicsFormat));
			if (m_VisibleLightIndexToShaderDataIndex.Length < lightData.visibleLights.Length)
			{
				m_VisibleLightIndexToShaderDataIndex = new int[lightData.visibleLights.Length];
			}
			int num = global::System.Math.Min(m_VisibleLightIndexToShaderDataIndex.Length, lightData.visibleLights.Length);
			for (int i = 0; i < num; i++)
			{
				m_VisibleLightIndexToShaderDataIndex[i] = -1;
			}
			m_AdditionalLightsCookieShaderData.Resize(m_Settings.maxAdditionalLights);
			global::UnityEngine.Matrix4x4[] worldToLights = m_AdditionalLightsCookieShaderData.worldToLights;
			global::UnityEngine.Rendering.Universal.ShaderBitArray cookieEnableBits = m_AdditionalLightsCookieShaderData.cookieEnableBits;
			global::UnityEngine.Vector4[] atlasUVRects = m_AdditionalLightsCookieShaderData.atlasUVRects;
			float[] lightTypes = m_AdditionalLightsCookieShaderData.lightTypes;
			global::System.Array.Clear(atlasUVRects, 0, atlasUVRects.Length);
			cookieEnableBits.Clear();
			for (int j = 0; j < validUvRects.length; j++)
			{
				int visibleLightIndex = validLightMappings[j].visibleLightIndex;
				int lightBufferIndex = validLightMappings[j].lightBufferIndex;
				m_VisibleLightIndexToShaderDataIndex[visibleLightIndex] = lightBufferIndex;
				ref global::UnityEngine.Rendering.VisibleLight reference = ref lightData.visibleLights.UnsafeElementAtMutable(visibleLightIndex);
				lightTypes[lightBufferIndex] = (float)reference.lightType;
				worldToLights[lightBufferIndex] = reference.localToWorldMatrix.inverse;
				atlasUVRects[lightBufferIndex] = validUvRects[j];
				cookieEnableBits[lightBufferIndex] = true;
				if (reference.lightType == global::UnityEngine.LightType.Spot)
				{
					float spotAngle = reference.spotAngle;
					float range = reference.range;
					global::UnityEngine.Matrix4x4 matrix4x = global::UnityEngine.Matrix4x4.Perspective(spotAngle, 1f, 0.001f, range);
					matrix4x.SetColumn(2, matrix4x.GetColumn(2) * -1f);
					worldToLights[lightBufferIndex] = matrix4x * worldToLights[lightBufferIndex];
				}
				else if (reference.lightType == global::UnityEngine.LightType.Directional)
				{
					reference.light.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData>(out var component);
					global::UnityEngine.Matrix4x4 uvTransform = global::UnityEngine.Matrix4x4.identity;
					GetLightUVScaleOffset(ref component, ref uvTransform);
					global::UnityEngine.Matrix4x4 matrix4x2 = s_DirLightProj * uvTransform * reference.localToWorldMatrix.inverse;
					worldToLights[lightBufferIndex] = matrix4x2;
				}
			}
			m_AdditionalLightsCookieShaderData.Upload(cmd);
		}
	}
}
