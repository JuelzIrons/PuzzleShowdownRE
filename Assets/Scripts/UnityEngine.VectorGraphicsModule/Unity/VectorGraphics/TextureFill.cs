namespace Unity.VectorGraphics
{
	public class TextureFill : global::Unity.VectorGraphics.IFill
	{
		private float m_Opacity = 1f;

		public global::UnityEngine.Texture2D Texture { get; set; }

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
	}
}
