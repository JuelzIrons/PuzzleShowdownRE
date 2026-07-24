namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal struct MantissaAndExponent
	{
		public float Mantissa { get; set; }

		public int Exponent { get; set; }

		public float GetValue(float exponentBase)
		{
			return Mantissa * global::System.MathF.Pow(exponentBase, Exponent);
		}
	}
}
