namespace Unity.VectorGraphics
{
	public class PatternFill : global::Unity.VectorGraphics.IFill
	{
		private float m_Opacity = 1f;

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

		public global::Unity.VectorGraphics.SceneNode Pattern { get; set; }

		public global::UnityEngine.Rect Rect { get; set; }
	}
}
