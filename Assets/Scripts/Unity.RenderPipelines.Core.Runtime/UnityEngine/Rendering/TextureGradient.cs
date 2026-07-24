namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public class TextureGradient : global::System.IDisposable
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Gradient m_Gradient;

		private global::UnityEngine.Texture2D m_Texture;

		private int m_RequestedTextureSize = -1;

		private bool m_IsTextureDirty;

		private bool m_Precise;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		public global::UnityEngine.GradientMode mode = global::UnityEngine.GradientMode.PerceptualBlend;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		public global::UnityEngine.ColorSpace colorSpace = global::UnityEngine.ColorSpace.Uninitialized;

		[field: global::UnityEngine.SerializeField]
		[field: global::UnityEngine.HideInInspector]
		public int textureSize { get; private set; }

		[global::UnityEngine.HideInInspector]
		public global::UnityEngine.GradientColorKey[] colorKeys => m_Gradient?.colorKeys;

		[global::UnityEngine.HideInInspector]
		public global::UnityEngine.GradientAlphaKey[] alphaKeys => m_Gradient?.alphaKeys;

		public TextureGradient(global::UnityEngine.Gradient baseCurve)
			: this(baseCurve.colorKeys, baseCurve.alphaKeys)
		{
			mode = baseCurve.mode;
			colorSpace = baseCurve.colorSpace;
			m_Gradient.mode = baseCurve.mode;
			m_Gradient.colorSpace = baseCurve.colorSpace;
		}

		public TextureGradient(global::UnityEngine.GradientColorKey[] colorKeys, global::UnityEngine.GradientAlphaKey[] alphaKeys, global::UnityEngine.GradientMode mode = global::UnityEngine.GradientMode.PerceptualBlend, global::UnityEngine.ColorSpace colorSpace = global::UnityEngine.ColorSpace.Uninitialized, int requestedTextureSize = -1, bool precise = false)
		{
			Rebuild(colorKeys, alphaKeys, mode, colorSpace, requestedTextureSize, precise);
		}

		private void Rebuild(global::UnityEngine.GradientColorKey[] colorKeys, global::UnityEngine.GradientAlphaKey[] alphaKeys, global::UnityEngine.GradientMode mode, global::UnityEngine.ColorSpace colorSpace, int requestedTextureSize, bool precise)
		{
			m_Gradient = new global::UnityEngine.Gradient();
			m_Gradient.mode = mode;
			m_Gradient.colorSpace = colorSpace;
			m_Gradient.SetKeys(colorKeys, alphaKeys);
			m_Precise = precise;
			m_RequestedTextureSize = requestedTextureSize;
			if (requestedTextureSize > 0)
			{
				textureSize = requestedTextureSize;
			}
			else
			{
				float num = 1f;
				float[] array = new float[colorKeys.Length + alphaKeys.Length];
				for (int i = 0; i < colorKeys.Length; i++)
				{
					array[i] = colorKeys[i].time;
				}
				for (int j = 0; j < alphaKeys.Length; j++)
				{
					array[colorKeys.Length + j] = alphaKeys[j].time;
				}
				global::System.Array.Sort(array);
				for (int k = 1; k < array.Length; k++)
				{
					int num2 = global::System.Math.Max(k - 1, 0);
					int num3 = global::System.Math.Min(k, array.Length - 1);
					float num4 = global::UnityEngine.Mathf.Abs(array[num2] - array[num3]);
					if (num4 > 0f && num4 < num)
					{
						num = num4;
					}
				}
				float num5 = ((!precise && mode != global::UnityEngine.GradientMode.Fixed) ? 2f : 4f);
				float f = num5 * global::UnityEngine.Mathf.Ceil(1f / num + 1f);
				textureSize = global::UnityEngine.Mathf.RoundToInt(f);
				textureSize = global::System.Math.Min(textureSize, 1024);
			}
			SetDirty();
		}

		public void Dispose()
		{
		}

		public void Release()
		{
			if (m_Texture != null)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(m_Texture);
			}
			m_Texture = null;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetDirty()
		{
			m_IsTextureDirty = true;
		}

		private static global::UnityEngine.Experimental.Rendering.GraphicsFormat GetTextureFormat()
		{
			return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
		}

		public global::UnityEngine.Texture2D GetTexture()
		{
			float num = 1f / (float)(textureSize - 1);
			if (m_Texture != null && m_Texture.width != textureSize)
			{
				global::UnityEngine.Object.DestroyImmediate(m_Texture);
				m_Texture = null;
			}
			if (m_Texture == null)
			{
				m_Texture = new global::UnityEngine.Texture2D(textureSize, 1, GetTextureFormat(), global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
				m_Texture.name = "GradientTexture";
				m_Texture.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
				m_Texture.filterMode = global::UnityEngine.FilterMode.Bilinear;
				m_Texture.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
				m_Texture.anisoLevel = 0;
				m_IsTextureDirty = true;
			}
			if (m_IsTextureDirty)
			{
				global::UnityEngine.Color[] array = new global::UnityEngine.Color[textureSize];
				for (int i = 0; i < textureSize; i++)
				{
					array[i] = Evaluate((float)i * num);
				}
				m_Texture.SetPixels(array);
				m_Texture.Apply(updateMipmaps: false, makeNoLongerReadable: false);
				m_IsTextureDirty = false;
				m_Texture.IncrementUpdateCount();
			}
			return m_Texture;
		}

		public global::UnityEngine.Color Evaluate(float time)
		{
			if (textureSize <= 0)
			{
				return global::UnityEngine.Color.black;
			}
			return m_Gradient.Evaluate(time);
		}

		public void SetKeys(global::UnityEngine.GradientColorKey[] colorKeys, global::UnityEngine.GradientAlphaKey[] alphaKeys, global::UnityEngine.GradientMode mode, global::UnityEngine.ColorSpace colorSpace)
		{
			m_Gradient.SetKeys(colorKeys, alphaKeys);
			m_Gradient.mode = mode;
			m_Gradient.colorSpace = colorSpace;
			Rebuild(colorKeys, alphaKeys, mode, colorSpace, m_RequestedTextureSize, m_Precise);
		}
	}
}
