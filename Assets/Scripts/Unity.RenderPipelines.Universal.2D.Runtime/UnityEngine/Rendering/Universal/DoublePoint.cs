namespace UnityEngine.Rendering.Universal
{
	internal struct DoublePoint
	{
		public double X;

		public double Y;

		public DoublePoint(double x = 0.0, double y = 0.0)
		{
			X = x;
			Y = y;
		}

		public DoublePoint(global::UnityEngine.Rendering.Universal.DoublePoint dp)
		{
			X = dp.X;
			Y = dp.Y;
		}

		public DoublePoint(global::UnityEngine.Rendering.Universal.IntPoint ip)
		{
			X = ip.X;
			Y = ip.Y;
		}
	}
}
