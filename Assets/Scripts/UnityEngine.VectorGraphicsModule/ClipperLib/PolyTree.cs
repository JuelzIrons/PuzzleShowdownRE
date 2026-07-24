namespace ClipperLib
{
	internal class PolyTree : global::ClipperLib.PolyNode
	{
		internal global::System.Collections.Generic.List<global::ClipperLib.PolyNode> m_AllPolys = new global::System.Collections.Generic.List<global::ClipperLib.PolyNode>();

		public int Total
		{
			get
			{
				int num = m_AllPolys.Count;
				if (num > 0 && m_Childs[0] != m_AllPolys[0])
				{
					num--;
				}
				return num;
			}
		}

		public void Clear()
		{
			for (int i = 0; i < m_AllPolys.Count; i++)
			{
				m_AllPolys[i] = null;
			}
			m_AllPolys.Clear();
			m_Childs.Clear();
		}

		public global::ClipperLib.PolyNode GetFirst()
		{
			if (m_Childs.Count > 0)
			{
				return m_Childs[0];
			}
			return null;
		}
	}
}
