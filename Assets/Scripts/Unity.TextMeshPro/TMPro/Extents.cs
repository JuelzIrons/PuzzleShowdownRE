namespace TMPro
{
	public struct Extents
	{
		internal static global::TMPro.Extents zero = new global::TMPro.Extents(global::UnityEngine.Vector2.zero, global::UnityEngine.Vector2.zero);

		internal static global::TMPro.Extents uninitialized = new global::TMPro.Extents(new global::UnityEngine.Vector2(32767f, 32767f), new global::UnityEngine.Vector2(-32767f, -32767f));

		public global::UnityEngine.Vector2 min;

		public global::UnityEngine.Vector2 max;

		public Extents(global::UnityEngine.Vector2 min, global::UnityEngine.Vector2 max)
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
