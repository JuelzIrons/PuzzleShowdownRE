namespace Unity.VectorGraphics
{
	public class GradientFill : global::Unity.VectorGraphics.IFill
	{
		private float m_Opacity = 1f;

		public global::Unity.VectorGraphics.GradientFillType Type { get; set; }

		public global::Unity.VectorGraphics.GradientStop[] Stops { get; set; }

		public global::Unity.VectorGraphics.FillMode Mode { get; set; }

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

		public global::Unity.VectorGraphics.AddressMode Addressing { get; set; }

		public global::UnityEngine.Vector2 RadialFocus { get; set; }
	}
}
