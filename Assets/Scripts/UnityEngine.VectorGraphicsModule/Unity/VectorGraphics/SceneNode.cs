namespace Unity.VectorGraphics
{
	public class SceneNode
	{
		private global::Unity.VectorGraphics.Matrix2D m_Transform = global::Unity.VectorGraphics.Matrix2D.identity;

		public global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode> Children { get; set; }

		public global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape> Shapes { get; set; }

		public global::Unity.VectorGraphics.Matrix2D Transform
		{
			get
			{
				return m_Transform;
			}
			set
			{
				m_Transform = value;
			}
		}

		public global::Unity.VectorGraphics.SceneNode Clipper { get; set; }
	}
}
