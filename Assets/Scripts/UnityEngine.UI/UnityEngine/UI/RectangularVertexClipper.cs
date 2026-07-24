namespace UnityEngine.UI
{
	internal class RectangularVertexClipper
	{
		private readonly global::UnityEngine.Vector3[] m_WorldCorners = new global::UnityEngine.Vector3[4];

		private readonly global::UnityEngine.Vector3[] m_CanvasCorners = new global::UnityEngine.Vector3[4];

		public global::UnityEngine.Rect GetCanvasRect(global::UnityEngine.RectTransform t, global::UnityEngine.Canvas c)
		{
			if (c == null)
			{
				return default(global::UnityEngine.Rect);
			}
			t.GetWorldCorners(m_WorldCorners);
			global::UnityEngine.Transform component = c.GetComponent<global::UnityEngine.Transform>();
			for (int i = 0; i < 4; i++)
			{
				m_CanvasCorners[i] = component.InverseTransformPoint(m_WorldCorners[i]);
			}
			return new global::UnityEngine.Rect(m_CanvasCorners[0].x, m_CanvasCorners[0].y, m_CanvasCorners[2].x - m_CanvasCorners[0].x, m_CanvasCorners[2].y - m_CanvasCorners[0].y);
		}
	}
}
