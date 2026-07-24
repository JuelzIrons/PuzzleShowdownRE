namespace UnityEngine.U2D.Animation
{
	internal class BufferManager : global::UnityEngine.ScriptableObject
	{
		private static global::UnityEngine.U2D.Animation.BufferManager s_Instance;

		private global::System.Collections.Generic.Dictionary<int, global::UnityEngine.U2D.Animation.VertexBuffer> m_Buffers = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.U2D.Animation.VertexBuffer>();

		private global::System.Collections.Generic.Queue<global::UnityEngine.U2D.Animation.VertexBuffer> m_BuffersToDispose = new global::System.Collections.Generic.Queue<global::UnityEngine.U2D.Animation.VertexBuffer>();

		public int bufferCount
		{
			get
			{
				int num = 0;
				foreach (global::UnityEngine.U2D.Animation.VertexBuffer value in m_Buffers.Values)
				{
					num += value.bufferCount;
				}
				return num;
			}
		}

		public bool needDoubleBuffering { get; set; }

		public static global::UnityEngine.U2D.Animation.BufferManager instance
		{
			get
			{
				if (s_Instance == null)
				{
					global::UnityEngine.U2D.Animation.BufferManager[] array = global::UnityEngine.Resources.FindObjectsOfTypeAll<global::UnityEngine.U2D.Animation.BufferManager>();
					if (array.Length != 0)
					{
						s_Instance = array[0];
					}
					else
					{
						s_Instance = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.U2D.Animation.BufferManager>();
					}
					s_Instance.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
				}
				return s_Instance;
			}
		}

		private void OnEnable()
		{
			if (s_Instance == null)
			{
				s_Instance = this;
			}
			needDoubleBuffering = true;
			global::UnityEngine.Application.onBeforeRender += Update;
		}

		private void OnDisable()
		{
			if (s_Instance == this)
			{
				s_Instance = null;
			}
			ForceClearBuffers();
			global::UnityEngine.Application.onBeforeRender -= Update;
		}

		private void ForceClearBuffers()
		{
			foreach (global::UnityEngine.U2D.Animation.VertexBuffer value in m_Buffers.Values)
			{
				value.Dispose();
			}
			foreach (global::UnityEngine.U2D.Animation.VertexBuffer item in m_BuffersToDispose)
			{
				item.Dispose();
			}
			m_Buffers.Clear();
			m_BuffersToDispose.Clear();
		}

		public global::UnityEngine.U2D.Animation.NativeByteArray GetBuffer(int id, int bufferSize)
		{
			if (!m_Buffers.TryGetValue(id, out var value))
			{
				value = CreateBuffer(id, bufferSize);
			}
			return value?.GetBuffer(bufferSize);
		}

		private global::UnityEngine.U2D.Animation.VertexBuffer CreateBuffer(int id, int bufferSize)
		{
			if (bufferSize < 1)
			{
				global::UnityEngine.Debug.LogError("Cannot create a buffer smaller than 1 byte.");
				return null;
			}
			global::UnityEngine.U2D.Animation.VertexBuffer vertexBuffer = new global::UnityEngine.U2D.Animation.VertexBuffer(id, bufferSize, needDoubleBuffering);
			m_Buffers.Add(id, vertexBuffer);
			return vertexBuffer;
		}

		public void ReturnBuffer(int id)
		{
			if (m_Buffers.TryGetValue(id, out var value))
			{
				value.Deactivate();
				m_BuffersToDispose.Enqueue(value);
				m_Buffers.Remove(id);
			}
		}

		private void Update()
		{
			while (m_BuffersToDispose.Count > 0 && m_BuffersToDispose.Peek().IsSafeToDispose())
			{
				m_BuffersToDispose.Dequeue().Dispose();
			}
		}
	}
}
