namespace UnityEngine.Rendering.Universal
{
	internal static class Light2DLookupTexture
	{
		internal static readonly string k_LightLookupProperty = "_LightLookup";

		internal static readonly string k_FalloffLookupProperty = "_FalloffLookup";

		internal static readonly int k_LightLookupID = global::UnityEngine.Shader.PropertyToID(k_LightLookupProperty);

		internal static readonly int k_FalloffLookupID = global::UnityEngine.Shader.PropertyToID(k_FalloffLookupProperty);

		private static global::UnityEngine.Texture2D s_PointLightLookupTexture;

		private static global::UnityEngine.Texture2D s_FalloffLookupTexture;

		private static global::UnityEngine.Rendering.RTHandle m_LightLookupRTHandle = null;

		private static global::UnityEngine.Rendering.RTHandle m_FalloffRTHandle = null;

		public static global::UnityEngine.Rendering.RTHandle GetLightLookupTexture_Rendergraph()
		{
			if (s_PointLightLookupTexture == null || m_LightLookupRTHandle == null)
			{
				global::UnityEngine.Texture lightLookupTexture = GetLightLookupTexture();
				m_LightLookupRTHandle?.Release();
				m_LightLookupRTHandle = global::UnityEngine.Rendering.RTHandles.Alloc(lightLookupTexture);
			}
			return m_LightLookupRTHandle;
		}

		public static global::UnityEngine.Rendering.RTHandle GetFallOffLookupTexture_Rendergraph()
		{
			if (s_FalloffLookupTexture == null || m_FalloffRTHandle == null)
			{
				global::UnityEngine.Texture falloffLookupTexture = GetFalloffLookupTexture();
				m_FalloffRTHandle?.Release();
				m_FalloffRTHandle = global::UnityEngine.Rendering.RTHandles.Alloc(falloffLookupTexture);
			}
			return m_FalloffRTHandle;
		}

		public static void Release()
		{
			m_FalloffRTHandle?.Release();
			m_LightLookupRTHandle?.Release();
			m_FalloffRTHandle = null;
			m_LightLookupRTHandle = null;
		}

		public static global::UnityEngine.Texture GetLightLookupTexture()
		{
			if (s_PointLightLookupTexture == null)
			{
				s_PointLightLookupTexture = CreatePointLightLookupTexture();
			}
			return s_PointLightLookupTexture;
		}

		public static global::UnityEngine.Texture GetFalloffLookupTexture()
		{
			if (s_FalloffLookupTexture == null)
			{
				s_FalloffLookupTexture = CreateFalloffLookupTexture();
			}
			return s_FalloffLookupTexture;
		}

		private static global::UnityEngine.Texture2D CreatePointLightLookupTexture()
		{
			global::UnityEngine.Experimental.Rendering.GraphicsFormat format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
			if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.SetPixels))
			{
				format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_SFloat;
			}
			else if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32G32B32A32_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.SetPixels))
			{
				format = global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32G32B32A32_SFloat;
			}
			global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(256, 256, format, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
			texture2D.name = k_LightLookupProperty;
			texture2D.filterMode = global::UnityEngine.FilterMode.Bilinear;
			texture2D.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(128f, 128f);
			for (int i = 0; i < 256; i++)
			{
				for (int j = 0; j < 256; j++)
				{
					global::UnityEngine.Vector2 vector2 = new global::UnityEngine.Vector2(j, i);
					float num = global::UnityEngine.Vector2.Distance(vector2, vector);
					global::UnityEngine.Vector2 vector3 = vector2 - vector;
					global::UnityEngine.Vector2 vector4 = vector - vector2;
					vector4.Normalize();
					float r = ((j != 255 && i != 255) ? global::UnityEngine.Mathf.Clamp(1f - 2f * num / 256f, 0f, 1f) : 0f);
					float num2 = global::UnityEngine.Mathf.Acos(global::UnityEngine.Vector2.Dot(global::UnityEngine.Vector2.down, vector3.normalized)) / global::System.MathF.PI;
					float g = global::UnityEngine.Mathf.Clamp(1f - num2, 0f, 1f);
					float x = vector4.x;
					float y = vector4.y;
					global::UnityEngine.Color color = new global::UnityEngine.Color(r, g, x, y);
					texture2D.SetPixel(j, i, color);
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		private static global::UnityEngine.Texture2D CreateFalloffLookupTexture()
		{
			global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(2048, 128, global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_SRGB, global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
			texture2D.name = k_FalloffLookupProperty;
			texture2D.filterMode = global::UnityEngine.FilterMode.Bilinear;
			texture2D.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
			for (int i = 0; i < 192; i++)
			{
				float num = (float)(i + 32) / 256f;
				float p = global::UnityEngine.Mathf.Log(0f - num + 1f) / global::UnityEngine.Mathf.Log(num);
				for (int j = 0; j < 2048; j++)
				{
					float r = global::UnityEngine.Mathf.Pow((float)j / 2048f, p);
					global::UnityEngine.Color color = new global::UnityEngine.Color(r, 0f, 0f, 1f);
					if (i >= 32 && i < 160)
					{
						texture2D.SetPixel(j, i - 32, color);
					}
				}
			}
			texture2D.Apply();
			return texture2D;
		}
	}
}
