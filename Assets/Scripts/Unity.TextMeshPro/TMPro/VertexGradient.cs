namespace TMPro
{
	[global::System.Serializable]
	public struct VertexGradient
	{
		public global::UnityEngine.Color topLeft;

		public global::UnityEngine.Color topRight;

		public global::UnityEngine.Color bottomLeft;

		public global::UnityEngine.Color bottomRight;

		public VertexGradient(global::UnityEngine.Color color)
		{
			topLeft = color;
			topRight = color;
			bottomLeft = color;
			bottomRight = color;
		}

		public VertexGradient(global::UnityEngine.Color color0, global::UnityEngine.Color color1, global::UnityEngine.Color color2, global::UnityEngine.Color color3)
		{
			topLeft = color0;
			topRight = color1;
			bottomLeft = color2;
			bottomRight = color3;
		}
	}
}
