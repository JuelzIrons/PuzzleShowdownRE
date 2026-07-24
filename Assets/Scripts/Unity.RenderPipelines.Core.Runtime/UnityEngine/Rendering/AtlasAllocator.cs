namespace UnityEngine.Rendering
{
	internal class AtlasAllocator
	{
		private class AtlasNode
		{
			public global::UnityEngine.Rendering.AtlasAllocator.AtlasNode m_RightChild;

			public global::UnityEngine.Rendering.AtlasAllocator.AtlasNode m_BottomChild;

			public global::UnityEngine.Vector4 m_Rect = new global::UnityEngine.Vector4(0f, 0f, 0f, 0f);

			public global::UnityEngine.Rendering.AtlasAllocator.AtlasNode Allocate(ref global::UnityEngine.Rendering.ObjectPool<global::UnityEngine.Rendering.AtlasAllocator.AtlasNode> pool, int width, int height, bool powerOfTwoPadding)
			{
				if (m_RightChild != null)
				{
					global::UnityEngine.Rendering.AtlasAllocator.AtlasNode atlasNode = m_RightChild.Allocate(ref pool, width, height, powerOfTwoPadding);
					if (atlasNode == null)
					{
						atlasNode = m_BottomChild.Allocate(ref pool, width, height, powerOfTwoPadding);
					}
					return atlasNode;
				}
				int num = 0;
				int num2 = 0;
				if (powerOfTwoPadding)
				{
					num = (int)m_Rect.x % width;
					num2 = (int)m_Rect.y % height;
				}
				if ((float)width <= m_Rect.x - (float)num && (float)height <= m_Rect.y - (float)num2)
				{
					m_RightChild = pool.Get();
					m_BottomChild = pool.Get();
					m_Rect.z += num;
					m_Rect.w += num2;
					m_Rect.x -= num;
					m_Rect.y -= num2;
					if (width > height)
					{
						m_RightChild.m_Rect.z = m_Rect.z + (float)width;
						m_RightChild.m_Rect.w = m_Rect.w;
						m_RightChild.m_Rect.x = m_Rect.x - (float)width;
						m_RightChild.m_Rect.y = height;
						m_BottomChild.m_Rect.z = m_Rect.z;
						m_BottomChild.m_Rect.w = m_Rect.w + (float)height;
						m_BottomChild.m_Rect.x = m_Rect.x;
						m_BottomChild.m_Rect.y = m_Rect.y - (float)height;
					}
					else
					{
						m_RightChild.m_Rect.z = m_Rect.z + (float)width;
						m_RightChild.m_Rect.w = m_Rect.w;
						m_RightChild.m_Rect.x = m_Rect.x - (float)width;
						m_RightChild.m_Rect.y = m_Rect.y;
						m_BottomChild.m_Rect.z = m_Rect.z;
						m_BottomChild.m_Rect.w = m_Rect.w + (float)height;
						m_BottomChild.m_Rect.x = width;
						m_BottomChild.m_Rect.y = m_Rect.y - (float)height;
					}
					m_Rect.x = width;
					m_Rect.y = height;
					return this;
				}
				return null;
			}

			public void Release(ref global::UnityEngine.Rendering.ObjectPool<global::UnityEngine.Rendering.AtlasAllocator.AtlasNode> pool)
			{
				if (m_RightChild != null)
				{
					m_RightChild.Release(ref pool);
					m_BottomChild.Release(ref pool);
					pool.Release(m_RightChild);
					pool.Release(m_BottomChild);
				}
				m_RightChild = null;
				m_BottomChild = null;
				m_Rect = global::UnityEngine.Vector4.zero;
			}
		}

		private global::UnityEngine.Rendering.AtlasAllocator.AtlasNode m_Root;

		private int m_Width;

		private int m_Height;

		private bool powerOfTwoPadding;

		private global::UnityEngine.Rendering.ObjectPool<global::UnityEngine.Rendering.AtlasAllocator.AtlasNode> m_NodePool;

		public AtlasAllocator(int width, int height, bool potPadding)
		{
			m_Root = new global::UnityEngine.Rendering.AtlasAllocator.AtlasNode();
			m_Root.m_Rect.Set(width, height, 0f, 0f);
			m_Width = width;
			m_Height = height;
			powerOfTwoPadding = potPadding;
			m_NodePool = new global::UnityEngine.Rendering.ObjectPool<global::UnityEngine.Rendering.AtlasAllocator.AtlasNode>(delegate
			{
			}, delegate
			{
			});
		}

		public bool Allocate(ref global::UnityEngine.Vector4 result, int width, int height)
		{
			global::UnityEngine.Rendering.AtlasAllocator.AtlasNode atlasNode = m_Root.Allocate(ref m_NodePool, width, height, powerOfTwoPadding);
			if (atlasNode != null)
			{
				result = atlasNode.m_Rect;
				return true;
			}
			result = global::UnityEngine.Vector4.zero;
			return false;
		}

		public void Reset()
		{
			m_Root.Release(ref m_NodePool);
			m_Root.m_Rect.Set(m_Width, m_Height, 0f, 0f);
		}
	}
}
