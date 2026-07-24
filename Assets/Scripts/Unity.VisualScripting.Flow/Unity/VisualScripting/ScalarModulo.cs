namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Modulo")]
	public sealed class ScalarModulo : global::Unity.VisualScripting.Modulo<float>
	{
		protected override float defaultDividend => 1f;

		protected override float defaultDivisor => 1f;

		public override float Operation(float a, float b)
		{
			return a % b;
		}
	}
}
