namespace UnityEngine.Rendering.Universal
{
	internal abstract class SingleHistoryBase : global::UnityEngine.Rendering.CameraHistoryItem
	{
		private int m_Id;

		private global::UnityEngine.RenderTextureDescriptor m_Descriptor;

		private global::UnityEngine.Hash128 m_DescKey;

		public override void OnCreate(global::UnityEngine.Rendering.BufferedRTHandleSystem owner, uint typeId)
		{
			base.OnCreate(owner, typeId);
			m_Id = MakeId(0u);
		}

		public global::UnityEngine.Rendering.RTHandle GetTexture(int frameIndex = 0)
		{
			if ((uint)frameIndex >= GetHistoryFrameCount())
			{
				return null;
			}
			return base.storage.GetFrameRT(m_Id, frameIndex);
		}

		public global::UnityEngine.Rendering.RTHandle GetCurrentTexture()
		{
			return GetCurrentFrameRT(m_Id);
		}

		public global::UnityEngine.Rendering.RTHandle GetPreviousTexture()
		{
			return GetTexture(1);
		}

		internal bool IsAllocated()
		{
			return GetTexture() != null;
		}

		internal bool IsDirty(ref global::UnityEngine.RenderTextureDescriptor desc)
		{
			return m_DescKey != global::UnityEngine.Hash128.Compute(ref desc);
		}

		private void Alloc(ref global::UnityEngine.RenderTextureDescriptor desc)
		{
			AllocHistoryFrameRT(m_Id, GetHistoryFrameCount(), ref desc, GetHistoryName());
			m_Descriptor = desc;
			m_DescKey = global::UnityEngine.Hash128.Compute(ref desc);
		}

		public override void Reset()
		{
			ReleaseHistoryFrameRT(m_Id);
		}

		internal bool Update(ref global::UnityEngine.RenderTextureDescriptor cameraDesc)
		{
			if (cameraDesc.width > 0 && cameraDesc.height > 0 && cameraDesc.graphicsFormat != global::UnityEngine.Experimental.Rendering.GraphicsFormat.None)
			{
				global::UnityEngine.RenderTextureDescriptor desc = GetHistoryDescriptor(ref cameraDesc);
				if (IsDirty(ref desc))
				{
					Reset();
				}
				if (!IsAllocated())
				{
					Alloc(ref desc);
					return true;
				}
			}
			return false;
		}

		protected abstract int GetHistoryFrameCount();

		protected abstract string GetHistoryName();

		protected abstract global::UnityEngine.RenderTextureDescriptor GetHistoryDescriptor(ref global::UnityEngine.RenderTextureDescriptor cameraDesc);
	}
}
