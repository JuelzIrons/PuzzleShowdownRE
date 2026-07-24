namespace Unity.VectorGraphics
{
	public class Stroke
	{
		private global::Unity.VectorGraphics.Matrix2D m_FillTransform = global::Unity.VectorGraphics.Matrix2D.identity;

		public global::UnityEngine.Color Color
		{
			get
			{
				if (!(Fill is global::Unity.VectorGraphics.SolidFill { Color: var color }))
				{
					return default(global::UnityEngine.Color);
				}
				return color;
			}
			set
			{
				Fill = new global::Unity.VectorGraphics.SolidFill
				{
					Color = value
				};
			}
		}

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

		public float HalfThickness { get; set; }

		public float[] Pattern { get; set; }

		public float PatternOffset { get; set; }

		public float TippedCornerLimit { get; set; }
	}
}
