namespace Unity.Multiplayer.Tools.Common
{
	internal class ContinuousExponentialMovingAverage
	{
		private const double k_DefaultInitialTime = double.NegativeInfinity;

		public static readonly double k_ln2 = global::System.Math.Log(2.0);

		public static readonly float k_ln2F = global::System.MathF.Log(2f);

		public double DecayConstant { get; private set; }

		public double LastValue { get; private set; }

		public double LastTime { get; private set; }

		public static global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage CreateWithHalfLife(double halfLife)
		{
			return new global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage(GetDecayConstantForHalfLife(halfLife));
		}

		public static double GetDecayConstantForHalfLife(double halfLife)
		{
			return k_ln2 / halfLife;
		}

		public static float GetDecayConstantForHalfLife(float halfLife)
		{
			if (!(halfLife > 0f))
			{
				return -1f;
			}
			return k_ln2F / halfLife;
		}

		public ContinuousExponentialMovingAverage(double decayConstant, double value = 0.0, double time = double.NegativeInfinity)
		{
			if (decayConstant < 0.0)
			{
				throw new global::System.ArgumentException($"ContinuousExponentialMovingAverage decay constant {decayConstant} should be >= 0; " + "otherwise it will grow exponentially over time.");
			}
			DecayConstant = decayConstant;
			LastValue = value;
			LastTime = time;
		}

		public void Reset()
		{
			DecayConstant = 0.0;
			LastValue = 0.0;
			LastTime = double.NegativeInfinity;
		}

		public void ClearValueAndTime()
		{
			LastValue = 0.0;
			LastTime = double.NegativeInfinity;
		}

		public void AddSampleForGauge(double sample, double time)
		{
			double num = global::System.Math.Exp((0.0 - (time - LastTime)) * DecayConstant);
			double num2 = 1.0 - num;
			LastValue += num2 * (sample - LastValue);
			LastTime = time;
		}

		public void AddSampleForCounter(double sample, double time)
		{
			double num = time - LastTime;
			double num2 = sample / num;
			double num3 = global::System.Math.Exp((0.0 - num) * DecayConstant);
			double num4 = 1.0 - num3;
			LastValue += num4 * (num2 - LastValue);
			LastTime = time;
		}

		public double GetGaugeValue()
		{
			return LastValue;
		}

		public double GetCounterValue(double time)
		{
			double num = global::System.Math.Exp((0.0 - (time - LastTime)) * DecayConstant);
			return LastValue * num;
		}
	}
}
