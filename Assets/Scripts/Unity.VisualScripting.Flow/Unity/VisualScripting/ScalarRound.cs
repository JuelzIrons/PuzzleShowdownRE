namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Round")]
	public sealed class ScalarRound : global::Unity.VisualScripting.Round<float, int>
	{
		protected override int Floor(float input)
		{
			return global::UnityEngine.Mathf.FloorToInt(input);
		}

		protected override int AwayFromZero(float input)
		{
			return global::UnityEngine.Mathf.RoundToInt(input);
		}

		protected override int Ceiling(float input)
		{
			return global::UnityEngine.Mathf.CeilToInt(input);
		}
	}
}
