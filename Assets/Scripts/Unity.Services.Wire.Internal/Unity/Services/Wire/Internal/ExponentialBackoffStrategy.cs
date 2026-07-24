namespace Unity.Services.Wire.Internal
{
	internal class ExponentialBackoffStrategy : global::Unity.Services.Wire.Internal.IBackoffStrategy
	{
		private int m_Attempt;

		private float m_Factor;

		private const float k_Max = 30f;

		private const float k_Min = 0.1f;

		public ExponentialBackoffStrategy()
		{
			m_Attempt = 0;
			m_Factor = 2f;
		}

		private float GetDuration(int attempt)
		{
			float num = 0.1f * (float)global::System.Math.Pow(m_Factor, attempt);
			if (!(num < 0.1f))
			{
				if (!(num > 30f))
				{
					return num;
				}
				return 30f;
			}
			return 0.1f;
		}

		public float GetNext()
		{
			float duration = GetDuration(m_Attempt);
			m_Attempt++;
			return duration;
		}

		public void Reset()
		{
			m_Attempt = 0;
		}
	}
}
