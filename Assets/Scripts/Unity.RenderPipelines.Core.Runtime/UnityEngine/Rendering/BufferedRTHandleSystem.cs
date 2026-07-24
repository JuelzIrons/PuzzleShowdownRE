namespace UnityEngine.Rendering
{
	public class BufferedRTHandleSystem : global::System.IDisposable
	{
		private global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.RTHandle[]> m_RTHandles = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.RTHandle[]>();

		private global::UnityEngine.Rendering.RTHandleSystem m_RTHandleSystem = new global::UnityEngine.Rendering.RTHandleSystem();

		private bool m_DisposedValue;

		public int maxWidth => m_RTHandleSystem.GetMaxWidth();

		public int maxHeight => m_RTHandleSystem.GetMaxHeight();

		public global::UnityEngine.Rendering.RTHandleProperties rtHandleProperties => m_RTHandleSystem.rtHandleProperties;

		public global::UnityEngine.Rendering.RTHandle GetFrameRT(int bufferId, int frameIndex)
		{
			if (!m_RTHandles.ContainsKey(bufferId))
			{
				return null;
			}
			return m_RTHandles[bufferId][frameIndex];
		}

		public void ClearBuffers(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::UnityEngine.Rendering.RTHandle[]> rTHandle in m_RTHandles)
			{
				for (int i = 0; i < rTHandle.Value.Length; i++)
				{
					global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, rTHandle.Value[i], global::UnityEngine.Rendering.ClearFlag.Color, global::UnityEngine.Color.black);
				}
			}
		}

		public void AllocBuffer(int bufferId, global::System.Func<global::UnityEngine.Rendering.RTHandleSystem, int, global::UnityEngine.Rendering.RTHandle> allocator, int bufferCount)
		{
			global::UnityEngine.Rendering.RTHandle[] array = new global::UnityEngine.Rendering.RTHandle[bufferCount];
			m_RTHandles.Add(bufferId, array);
			array[0] = allocator(m_RTHandleSystem, 0);
			int i = 1;
			for (int num = array.Length; i < num; i++)
			{
				array[i] = allocator(m_RTHandleSystem, i);
				m_RTHandleSystem.SwitchResizeMode(array[i], global::UnityEngine.Rendering.RTHandleSystem.ResizeMode.OnDemand);
			}
		}

		public void AllocBuffer(int bufferId, int bufferCount, ref global::UnityEngine.RenderTextureDescriptor descriptor, global::UnityEngine.FilterMode filterMode = global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode wrapMode = global::UnityEngine.TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			global::UnityEngine.Rendering.RTHandle[] array = new global::UnityEngine.Rendering.RTHandle[bufferCount];
			m_RTHandles.Add(bufferId, array);
			global::UnityEngine.Rendering.RTHandleAllocInfo rTHandleAllocInfo = global::UnityEngine.Rendering.RTHandles.GetRTHandleAllocInfo(in descriptor, filterMode, wrapMode, anisoLevel, mipMapBias, name);
			rTHandleAllocInfo.isShadowMap = isShadowMap;
			array[0] = m_RTHandleSystem.Alloc(descriptor.width, descriptor.height, rTHandleAllocInfo);
			int i = 1;
			for (int num = array.Length; i < num; i++)
			{
				array[i] = m_RTHandleSystem.Alloc(descriptor.width, descriptor.height, rTHandleAllocInfo);
				m_RTHandleSystem.SwitchResizeMode(array[i], global::UnityEngine.Rendering.RTHandleSystem.ResizeMode.OnDemand);
			}
		}

		public void ReleaseBuffer(int bufferId)
		{
			if (m_RTHandles.TryGetValue(bufferId, out var value))
			{
				global::UnityEngine.Rendering.RTHandle[] array = value;
				foreach (global::UnityEngine.Rendering.RTHandle rth in array)
				{
					m_RTHandleSystem.Release(rth);
				}
			}
			m_RTHandles.Remove(bufferId);
		}

		public void SwapAndSetReferenceSize(int width, int height)
		{
			Swap();
			m_RTHandleSystem.SetReferenceSize(width, height);
		}

		public void ResetReferenceSize(int width, int height)
		{
			m_RTHandleSystem.ResetReferenceSize(width, height);
		}

		public int GetNumFramesAllocated(int bufferId)
		{
			if (!m_RTHandles.ContainsKey(bufferId))
			{
				return 0;
			}
			return m_RTHandles[bufferId].Length;
		}

		public global::UnityEngine.Vector2 CalculateRatioAgainstMaxSize(int width, int height)
		{
			return m_RTHandleSystem.CalculateRatioAgainstMaxSize(new global::UnityEngine.Vector2Int(width, height));
		}

		private void Swap()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::UnityEngine.Rendering.RTHandle[]> rTHandle2 in m_RTHandles)
			{
				if (rTHandle2.Value.Length > 1)
				{
					global::UnityEngine.Rendering.RTHandle rTHandle = rTHandle2.Value[rTHandle2.Value.Length - 1];
					int i = 0;
					for (int num = rTHandle2.Value.Length - 1; i < num; i++)
					{
						rTHandle2.Value[i + 1] = rTHandle2.Value[i];
					}
					rTHandle2.Value[0] = rTHandle;
					m_RTHandleSystem.SwitchResizeMode(rTHandle2.Value[0], global::UnityEngine.Rendering.RTHandleSystem.ResizeMode.Auto);
					m_RTHandleSystem.SwitchResizeMode(rTHandle2.Value[1], global::UnityEngine.Rendering.RTHandleSystem.ResizeMode.OnDemand);
				}
				else
				{
					m_RTHandleSystem.SwitchResizeMode(rTHandle2.Value[0], global::UnityEngine.Rendering.RTHandleSystem.ResizeMode.Auto);
				}
			}
		}

		private void Dispose(bool disposing)
		{
			if (!m_DisposedValue)
			{
				if (disposing)
				{
					ReleaseAll();
					m_RTHandleSystem.Dispose();
					m_RTHandleSystem = null;
				}
				m_DisposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		public void ReleaseAll()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::UnityEngine.Rendering.RTHandle[]> rTHandle in m_RTHandles)
			{
				int i = 0;
				for (int num = rTHandle.Value.Length; i < num; i++)
				{
					m_RTHandleSystem.Release(rTHandle.Value[i]);
				}
			}
			m_RTHandles.Clear();
		}
	}
}
