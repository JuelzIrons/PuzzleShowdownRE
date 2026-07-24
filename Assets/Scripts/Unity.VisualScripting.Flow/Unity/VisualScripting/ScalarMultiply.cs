namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Multiply")]
	public sealed class ScalarMultiply : global::Unity.VisualScripting.Multiply<float>
	{
		protected override float defaultB => 1f;

		public override float Operation(float a, float b)
		{
			return a * b;
		}
	}
}
