namespace Unity.VectorGraphics
{
	public class SolidFill : global::Unity.VectorGraphics.IFill
	{
		private float m_Opacity = 1f;

		public global::UnityEngine.Color Color { get; set; }

		public float Opacity
		{
			get
			{
				return m_Opacity;
			}
			set
			{
				m_Opacity = value;
			}
		}

		public global::Unity.VectorGraphics.FillMode Mode { get; set; }
	}
}
