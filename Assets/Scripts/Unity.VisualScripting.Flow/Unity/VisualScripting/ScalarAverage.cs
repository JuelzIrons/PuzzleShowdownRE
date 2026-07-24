namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Average")]
	public sealed class ScalarAverage : global::Unity.VisualScripting.Average<float>
	{
		public override float Operation(float a, float b)
		{
			return (a + b) / 2f;
		}

		public override float Operation(global::System.Collections.Generic.IEnumerable<float> values)
		{
			return global::System.Linq.Enumerable.Average(values);
		}
	}
}
