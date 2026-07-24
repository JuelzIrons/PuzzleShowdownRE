namespace UnityEngine.Rendering.Universal
{
	public sealed class TaaHistory : global::UnityEngine.Rendering.CameraHistoryItem
	{
		private int[] m_TaaAccumulationTextureIds = new int[2];

		private int[] m_TaaAccumulationVersions = new int[2];

		private static readonly string[] m_TaaAccumulationNames = new string[2] { "TaaAccumulationTex0", "TaaAccumulationTex1" };

		private global::UnityEngine.RenderTextureDescriptor m_Descriptor;

		private global::UnityEngine.Hash128 m_DescKey;

		public override void OnCreate(global::UnityEngine.Rendering.BufferedRTHandleSystem owner, uint typeId)
		{
			base.OnCreate(owner, typeId);
			m_TaaAccumulationTextureIds[0] = MakeId(0u);
			m_TaaAccumulationTextureIds[1] = MakeId(1u);
		}

		public override void Reset()
		{
			for (int i = 0; i < m_TaaAccumulationTextureIds.Length; i++)
			{
				ReleaseHistoryFrameRT(m_TaaAccumulationTextureIds[i]);
				m_TaaAccumulationVersions[i] = -1;
			}
			m_Descriptor.width = 0;
			m_Descriptor.height = 0;
			m_Descriptor.graphicsFormat = global::UnityEngine.Experimental.Rendering.GraphicsFormat.None;
			m_DescKey = global::UnityEngine.Hash128.Compute(0);
		}

		public global::UnityEngine.Rendering.RTHandle GetAccumulationTexture(int eyeIndex = 0)
		{
			return GetCurrentFrameRT(m_TaaAccumulationTextureIds[eyeIndex]);
		}

		public int GetAccumulationVersion(int eyeIndex = 0)
		{
			return m_TaaAccumulationVersions[eyeIndex];
		}

		internal void SetAccumulationVersion(int eyeIndex, int version)
		{
			m_TaaAccumulationVersions[eyeIndex] = version;
		}

		private bool IsValid()
		{
			return GetAccumulationTexture() != null;
		}

		private bool IsDirty(ref global::UnityEngine.RenderTextureDescriptor desc)
		{
			return m_DescKey != global::UnityEngine.Hash128.Compute(ref desc);
		}

		private void Alloc(ref global::UnityEngine.RenderTextureDescriptor desc, bool xrMultipassEnabled)
		{
			AllocHistoryFrameRT(m_TaaAccumulationTextureIds[0], 1, ref desc, m_TaaAccumulationNames[0]);
			if (xrMultipassEnabled)
			{
				AllocHistoryFrameRT(m_TaaAccumulationTextureIds[1], 1, ref desc, m_TaaAccumulationNames[1]);
			}
			m_Descriptor = desc;
			m_DescKey = global::UnityEngine.Hash128.Compute(ref desc);
		}

		internal bool Update(ref global::UnityEngine.RenderTextureDescriptor cameraDesc, bool xrMultipassEnabled = false)
		{
			if (cameraDesc.width > 0 && cameraDesc.height > 0 && cameraDesc.graphicsFormat != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None)
			{
				global::UnityEngine.RenderTextureDescriptor desc = global::UnityEngine.Rendering.Universal.TemporalAA.TemporalAADescFromCameraDesc(ref cameraDesc);
				if (IsDirty(ref desc))
				{
					Reset();
				}
				if (!IsValid())
				{
					Alloc(ref desc, xrMultipassEnabled);
					return true;
				}
			}
			return false;
		}
	}
}
