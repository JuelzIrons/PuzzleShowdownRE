namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public class TextureCurve : global::System.IDisposable
	{
		private const int k_Precision = 128;

		private const float k_Step = 1f / 128f;

		[global::UnityEngine.SerializeField]
		private bool m_Loop;

		[global::UnityEngine.SerializeField]
		private float m_ZeroValue;

		[global::UnityEngine.SerializeField]
		private float m_Range;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AnimationCurve m_Curve;

		private global::UnityEngine.AnimationCurve m_LoopingCurve;

		private global::UnityEngine.Texture2D m_Texture;

		private bool m_IsCurveDirty;

		private bool m_IsTextureDirty;

		[field: global::UnityEngine.SerializeField]
		public int length { get; private set; }

		public global::UnityEngine.Keyframe this[int index] => m_Curve[index];

		public TextureCurve(global::UnityEngine.AnimationCurve baseCurve, float zeroValue, bool loop, in global::UnityEngine.Vector2 bounds)
			: this(baseCurve.keys, zeroValue, loop, in bounds)
		{
		}

		public TextureCurve(global::UnityEngine.Keyframe[] keys, float zeroValue, bool loop, in global::UnityEngine.Vector2 bounds)
		{
			m_Curve = new global::UnityEngine.AnimationCurve(keys);
			m_ZeroValue = zeroValue;
			m_Loop = loop;
			m_Range = bounds.magnitude;
			length = keys.Length;
			SetDirty();
		}

		public void Dispose()
		{
			Release();
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
			m_IsCurveDirty = true;
			m_IsTextureDirty = true;
		}

		private static global::UnityEngine.Experimental.Rendering.GraphicsFormat GetTextureFormat()
		{
			if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.SetPixels))
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat;
			}
			if (global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.SetPixels))
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8_UNorm;
			}
			return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm;
		}

		public global::UnityEngine.Texture2D GetTexture()
		{
			if (m_Texture == null)
			{
				m_Texture = new global::UnityEngine.Texture2D(128, 1, GetTextureFormat(), global::UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
				m_Texture.name = "CurveTexture";
				m_Texture.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
				m_Texture.filterMode = global::UnityEngine.FilterMode.Bilinear;
				m_Texture.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
				m_Texture.anisoLevel = 0;
				m_IsTextureDirty = true;
			}
			if (m_IsTextureDirty)
			{
				global::UnityEngine.Color[] array = new global::UnityEngine.Color[128];
				for (int i = 0; i < array.Length; i++)
				{
					array[i].r = Evaluate((float)i * (1f / 128f));
				}
				m_Texture.SetPixels(array);
				m_Texture.Apply(updateMipmaps: false, makeNoLongerReadable: false);
				m_IsTextureDirty = false;
			}
			return m_Texture;
		}

		public float Evaluate(float time)
		{
			if (m_IsCurveDirty)
			{
				length = m_Curve.length;
			}
			if (length == 0)
			{
				return m_ZeroValue;
			}
			if (!m_Loop || length == 1)
			{
				return m_Curve.Evaluate(time);
			}
			if (m_IsCurveDirty)
			{
				if (m_LoopingCurve == null)
				{
					m_LoopingCurve = new global::UnityEngine.AnimationCurve();
				}
				global::UnityEngine.Keyframe key = m_Curve[length - 1];
				key.time -= m_Range;
				global::UnityEngine.Keyframe key2 = m_Curve[0];
				key2.time += m_Range;
				m_LoopingCurve.keys = m_Curve.keys;
				m_LoopingCurve.AddKey(key);
				m_LoopingCurve.AddKey(key2);
				m_IsCurveDirty = false;
			}
			return m_LoopingCurve.Evaluate(time);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int AddKey(float time, float value)
		{
			int num = m_Curve.AddKey(time, value);
			if (num > -1)
			{
				SetDirty();
			}
			return num;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int MoveKey(int index, in global::UnityEngine.Keyframe key)
		{
			int result = m_Curve.MoveKey(index, key);
			SetDirty();
			return result;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void RemoveKey(int index)
		{
			m_Curve.RemoveKey(index);
			SetDirty();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SmoothTangents(int index, float weight)
		{
			m_Curve.SmoothTangents(index, weight);
			SetDirty();
		}
	}
}
