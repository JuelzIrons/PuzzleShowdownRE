namespace ClipperLib
{
	internal class PolyNode
	{
		internal global::ClipperLib.PolyNode m_Parent;

		internal global::System.Collections.Generic.List<global::ClipperLib.IntPoint> m_polygon = new global::System.Collections.Generic.List<global::ClipperLib.IntPoint>();

		internal int m_Index;

		internal global::ClipperLib.JoinType m_jointype;

		internal global::ClipperLib.EndType m_endtype;

		internal global::System.Collections.Generic.List<global::ClipperLib.PolyNode> m_Childs = new global::System.Collections.Generic.List<global::ClipperLib.PolyNode>();

		public int ChildCount => m_Childs.Count;

		public global::System.Collections.Generic.List<global::ClipperLib.IntPoint> Contour => m_polygon;

		public global::System.Collections.Generic.List<global::ClipperLib.PolyNode> Childs => m_Childs;

		public global::ClipperLib.PolyNode Parent => m_Parent;

		public bool IsHole => IsHoleNode();

		public bool IsOpen { get; set; }

		private bool IsHoleNode()
		{
			bool flag = true;
			for (global::ClipperLib.PolyNode parent = m_Parent; parent != null; parent = parent.m_Parent)
			{
				flag = !flag;
			}
			return flag;
		}

		internal void AddChild(global::ClipperLib.PolyNode Child)
		{
			int count = m_Childs.Count;
			m_Childs.Add(Child);
			Child.m_Parent = this;
			Child.m_Index = count;
		}

		public global::ClipperLib.PolyNode GetNext()
		{
			if (m_Childs.Count > 0)
			{
				return m_Childs[0];
			}
			return GetNextSiblingUp();
		}

		internal global::ClipperLib.PolyNode GetNextSiblingUp()
		{
			if (m_Parent == null)
			{
				return null;
			}
			if (m_Index == m_Parent.m_Childs.Count - 1)
			{
				return m_Parent.GetNextSiblingUp();
			}
			return m_Parent.m_Childs[m_Index + 1];
		}
	}
}
