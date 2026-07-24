namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Divide")]
	public sealed class ScalarDivide : global::Unity.VisualScripting.Divide<float>
	{
		protected override float defaultDividend => 1f;

		protected override float defaultDivisor => 1f;

		public override float Operation(float a, float b)
		{
			return a / b;
		}
	}
}
