namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal static class GraphScalingUtils
	{
		private static readonly float[] k_RoundNumbers = new float[9] { 1f, 1.5f, 2f, 3f, 4f, 5f, 6f, 8f, 10f };

		public static global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent NextLargestRoundNumber(float value)
		{
			if (value == 0f)
			{
				return default(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent);
			}
			int num = global::System.MathF.Sign(value);
			value = global::System.MathF.Abs(value);
			float num2 = global::System.MathF.Floor(global::System.MathF.Log10(value));
			float num3 = global::System.MathF.Pow(10f, num2);
			float num4 = value / num3;
			float num5 = k_RoundNumbers[0];
			for (int i = 0; i < k_RoundNumbers.Length; i++)
			{
				float num6 = k_RoundNumbers[i];
				if (num6 >= num4)
				{
					num5 = num6;
					break;
				}
			}
			if (num5 == 10f)
			{
				num5 = 1f;
				num2 += 1f;
			}
			return new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MantissaAndExponent
			{
				Mantissa = (float)num * num5,
				Exponent = (int)num2
			};
		}
	}
}
