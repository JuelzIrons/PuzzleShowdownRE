namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	public class MinMaxRangeAttribute : global::UnityEngine.PropertyAttribute
	{
		public readonly float Min;

		public readonly float Max;

		public readonly bool RoundToInt;

		public MinMaxRangeAttribute(float min, float max, bool roundToInt = false)
		{
			Min = min;
			Max = max;
			RoundToInt = roundToInt;
		}
	}
}
