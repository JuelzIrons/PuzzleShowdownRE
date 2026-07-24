namespace UnityEngine.Rendering
{
	public abstract class CameraHistoryItem : global::UnityEngine.Rendering.ContextItem
	{
		private global::UnityEngine.Rendering.BufferedRTHandleSystem m_owner;

		private uint m_TypeId = uint.MaxValue;

		protected global::UnityEngine.Rendering.BufferedRTHandleSystem storage => m_owner;

		public virtual void OnCreate(global::UnityEngine.Rendering.BufferedRTHandleSystem owner, uint typeId)
		{
			m_owner = owner;
			m_TypeId = typeId;
		}

		protected int MakeId(uint index)
		{
			return (int)(((m_TypeId & 0xFFFF) << 16) | (index & 0xFFFF));
		}

		protected global::UnityEngine.Rendering.RTHandle AllocHistoryFrameRT(int id, int count, ref global::UnityEngine.RenderTextureDescriptor desc, string name = "")
		{
			return AllocHistoryFrameRT(id, count, ref desc, global::UnityEngine.FilterMode.Bilinear, name);
		}

		protected global::UnityEngine.Rendering.RTHandle AllocHistoryFrameRT(int id, int count, ref global::UnityEngine.RenderTextureDescriptor desc, global::UnityEngine.FilterMode filterMode, string name = "")
		{
			m_owner.AllocBuffer(id, count, ref desc, filterMode, global::UnityEngine.TextureWrapMode.Clamp, isShadowMap: false, 0, 0f, name);
			return GetCurrentFrameRT(0);
		}

		protected void ReleaseHistoryFrameRT(int id)
		{
			m_owner.ReleaseBuffer(id);
		}

		protected global::UnityEngine.Rendering.RTHandle GetPreviousFrameRT(int id)
		{
			return m_owner.GetFrameRT(id, 1);
		}

		protected global::UnityEngine.Rendering.RTHandle GetCurrentFrameRT(int id)
		{
			return m_owner.GetFrameRT(id, 0);
		}
	}
}
