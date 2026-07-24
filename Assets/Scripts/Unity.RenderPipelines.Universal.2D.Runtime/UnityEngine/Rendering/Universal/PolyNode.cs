namespace UnityEngine.Rendering.Universal
{
	internal class PolyNode
	{
		internal global::UnityEngine.Rendering.Universal.PolyNode m_Parent;

		internal global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> m_polygon = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>();

		internal int m_Index;

		internal global::UnityEngine.Rendering.Universal.JoinTypes m_jointype;

		internal global::UnityEngine.Rendering.Universal.EndTypes m_endtype;

		internal global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.PolyNode> m_Childs = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.PolyNode>();

		public int ChildCount => m_Childs.Count;

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> Contour => m_polygon;

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.PolyNode> Childs => m_Childs;

		public global::UnityEngine.Rendering.Universal.PolyNode Parent => m_Parent;

		public bool IsHole => IsHoleNode();

		public bool IsOpen { get; set; }

		private bool IsHoleNode()
		{
			bool flag = true;
			for (global::UnityEngine.Rendering.Universal.PolyNode parent = m_Parent; parent != null; parent = parent.m_Parent)
			{
				flag = !flag;
			}
			return flag;
		}

		internal void AddChild(global::UnityEngine.Rendering.Universal.PolyNode Child)
		{
			int count = m_Childs.Count;
			m_Childs.Add(Child);
			Child.m_Parent = this;
			Child.m_Index = count;
		}

		public global::UnityEngine.Rendering.Universal.PolyNode GetNext()
		{
			if (m_Childs.Count > 0)
			{
				return m_Childs[0];
			}
			return GetNextSiblingUp();
		}

		internal global::UnityEngine.Rendering.Universal.PolyNode GetNextSiblingUp()
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
