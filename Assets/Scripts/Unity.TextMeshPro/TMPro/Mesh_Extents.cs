namespace TMPro
{
	[global::System.Serializable]
	public struct Mesh_Extents
	{
		public global::UnityEngine.Vector2 min;

		public global::UnityEngine.Vector2 max;

		public Mesh_Extents(global::UnityEngine.Vector2 min, global::UnityEngine.Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		public override string ToString()
		{
			return "Min (" + min.x.ToString("f2") + ", " + min.y.ToString("f2") + ")   Max (" + max.x.ToString("f2") + ", " + max.y.ToString("f2") + ")";
		}
	}
}
