namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Subtract")]
	public sealed class ScalarSubtract : global::Unity.VisualScripting.Subtract<float>
	{
		protected override float defaultMinuend => 1f;

		protected override float defaultSubtrahend => 1f;

		public override float Operation(float a, float b)
		{
			return a - b;
		}
	}
}
