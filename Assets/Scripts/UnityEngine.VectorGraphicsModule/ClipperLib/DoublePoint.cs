namespace ClipperLib
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

		public DoublePoint(global::ClipperLib.DoublePoint dp)
		{
			X = dp.X;
			Y = dp.Y;
		}

		public DoublePoint(global::ClipperLib.IntPoint ip)
		{
			X = ip.X;
			Y = ip.Y;
		}
	}
}
