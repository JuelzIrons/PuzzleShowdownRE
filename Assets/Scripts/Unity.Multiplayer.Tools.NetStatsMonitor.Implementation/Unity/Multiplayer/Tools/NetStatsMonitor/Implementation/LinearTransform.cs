namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal struct LinearTransform
	{
		public float A { get; set; }

		public float B { get; set; }

		public bool IsIdentity
		{
			get
			{
				if (A == 1f)
				{
					return B == 0f;
				}
				return false;
			}
		}

		public static global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform Identity => new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.LinearTransform
		{
			A = 1f,
			B = 0f
		};

		public float Apply(float x)
		{
			return A * x + B;
		}
	}
}
