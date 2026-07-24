namespace UnityEngine.Rendering
{
	public class RTHandle
	{
		internal global::UnityEngine.Rendering.RTHandleSystem m_Owner;

		internal global::UnityEngine.RenderTexture m_RT;

		internal global::UnityEngine.Texture m_ExternalTexture;

		internal global::UnityEngine.Rendering.RenderTargetIdentifier m_NameID;

		internal bool m_EnableMSAA;

		internal bool m_EnableRandomWrite;

		internal bool m_EnableHWDynamicScale;

		internal bool m_RTHasOwnership = true;

		internal string m_Name;

		internal bool m_UseCustomHandleScales;

		internal global::UnityEngine.Rendering.RTHandleProperties m_CustomHandleProperties;

		internal global::UnityEngine.Rendering.ScaleFunc scaleFunc;

		public global::UnityEngine.Vector2 scaleFactor { get; internal set; }

		public bool useScaling { get; internal set; }

		public global::UnityEngine.Vector2Int referenceSize { get; internal set; }

		public global::UnityEngine.Rendering.RTHandleProperties rtHandleProperties
		{
			get
			{
				if (!m_UseCustomHandleScales)
				{
					return m_Owner.rtHandleProperties;
				}
				return m_CustomHandleProperties;
			}
		}

		public global::UnityEngine.RenderTexture rt => m_RT;

		public global::UnityEngine.Texture externalTexture => m_ExternalTexture;

		public global::UnityEngine.Rendering.RenderTargetIdentifier nameID => m_NameID;

		public string name => m_Name;

		public bool isMSAAEnabled => m_EnableMSAA;

		public void SetCustomHandleProperties(in global::UnityEngine.Rendering.RTHandleProperties properties)
		{
			m_UseCustomHandleScales = true;
			m_CustomHandleProperties = properties;
		}

		public void ClearCustomHandleProperties()
		{
			m_UseCustomHandleScales = false;
		}

		internal RTHandle(global::UnityEngine.Rendering.RTHandleSystem owner)
		{
			m_Owner = owner;
		}

		public static implicit operator global::UnityEngine.Rendering.RenderTargetIdentifier(global::UnityEngine.Rendering.RTHandle handle)
		{
			return handle?.nameID ?? default(global::UnityEngine.Rendering.RenderTargetIdentifier);
		}

		public static implicit operator global::UnityEngine.Texture(global::UnityEngine.Rendering.RTHandle handle)
		{
			if (handle == null)
			{
				return null;
			}
			if (!(handle.rt != null))
			{
				return handle.m_ExternalTexture;
			}
			return handle.rt;
		}

		public static implicit operator global::UnityEngine.RenderTexture(global::UnityEngine.Rendering.RTHandle handle)
		{
			return handle?.rt;
		}

		internal void SetRenderTexture(global::UnityEngine.RenderTexture rt, bool transferOwnership = true)
		{
			m_RT = rt;
			m_ExternalTexture = null;
			m_RTHasOwnership = transferOwnership;
			m_NameID = new global::UnityEngine.Rendering.RenderTargetIdentifier(rt);
		}

		internal void SetTexture(global::UnityEngine.Texture tex)
		{
			m_RT = null;
			m_ExternalTexture = tex;
			m_NameID = new global::UnityEngine.Rendering.RenderTargetIdentifier(tex);
		}

		internal void SetTexture(global::UnityEngine.Rendering.RenderTargetIdentifier tex)
		{
			m_RT = null;
			m_ExternalTexture = null;
			m_NameID = tex;
		}

		public int GetInstanceID()
		{
			if (m_RT != null)
			{
				return m_RT.GetInstanceID();
			}
			if (m_ExternalTexture != null)
			{
				return m_ExternalTexture.GetInstanceID();
			}
			return m_NameID.GetHashCode();
		}

		public void Release()
		{
			m_Owner.Remove(this);
			if (m_RTHasOwnership)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(m_RT);
			}
			m_NameID = global::UnityEngine.Rendering.BuiltinRenderTextureType.None;
			m_RT = null;
			m_ExternalTexture = null;
			m_RTHasOwnership = true;
		}

		public global::UnityEngine.Vector2Int GetScaledSize(global::UnityEngine.Vector2Int refSize)
		{
			if (!useScaling)
			{
				return refSize;
			}
			if (scaleFunc != null)
			{
				return scaleFunc(refSize);
			}
			return new global::UnityEngine.Vector2Int(global::UnityEngine.Mathf.RoundToInt(scaleFactor.x * (float)refSize.x), global::UnityEngine.Mathf.RoundToInt(scaleFactor.y * (float)refSize.y));
		}

		public global::UnityEngine.Vector2Int GetScaledSize()
		{
			if (!useScaling)
			{
				return referenceSize;
			}
			if (scaleFunc != null)
			{
				return scaleFunc(referenceSize);
			}
			return new global::UnityEngine.Vector2Int(global::UnityEngine.Mathf.RoundToInt(scaleFactor.x * (float)referenceSize.x), global::UnityEngine.Mathf.RoundToInt(scaleFactor.y * (float)referenceSize.y));
		}

		public void SwitchToFastMemory(global::UnityEngine.Rendering.CommandBuffer cmd, float residencyFraction = 1f, global::UnityEngine.Rendering.FastMemoryFlags flags = global::UnityEngine.Rendering.FastMemoryFlags.SpillTop, bool copyContents = false)
		{
			residencyFraction = global::UnityEngine.Mathf.Clamp01(residencyFraction);
			cmd.SwitchIntoFastMemory(m_RT, flags, residencyFraction, copyContents);
		}

		public void CopyToFastMemory(global::UnityEngine.Rendering.CommandBuffer cmd, float residencyFraction = 1f, global::UnityEngine.Rendering.FastMemoryFlags flags = global::UnityEngine.Rendering.FastMemoryFlags.SpillTop)
		{
			SwitchToFastMemory(cmd, residencyFraction, flags, copyContents: true);
		}

		public void SwitchOutFastMemory(global::UnityEngine.Rendering.CommandBuffer cmd, bool copyContents = true)
		{
			cmd.SwitchOutOfFastMemory(m_RT, copyContents);
		}
	}
}
