namespace UnityEngine.Rendering.Universal
{
	internal struct IntRect
	{
		public long left;

		public long top;

		public long right;

		public long bottom;

		public IntRect(long l, long t, long r, long b)
		{
			left = l;
			top = t;
			right = r;
			bottom = b;
		}

		public IntRect(global::UnityEngine.Rendering.Universal.IntRect ir)
		{
			left = ir.left;
			top = ir.top;
			right = ir.right;
			bottom = ir.bottom;
		}
	}
}
