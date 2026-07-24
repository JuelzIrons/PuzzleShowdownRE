namespace Unity.VectorGraphics
{
	public class Shape
	{
		private global::Unity.VectorGraphics.Matrix2D m_FillTransform = global::Unity.VectorGraphics.Matrix2D.identity;

		public global::Unity.VectorGraphics.BezierContour[] Contours { get; set; }

		public global::Unity.VectorGraphics.IFill Fill { get; set; }

		public global::Unity.VectorGraphics.Matrix2D FillTransform
		{
			get
			{
				return m_FillTransform;
			}
			set
			{
				m_FillTransform = value;
			}
		}

		public global::Unity.VectorGraphics.PathProperties PathProps { get; set; }

		public bool IsConvex { get; set; }
	}
}
