namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Add")]
	public sealed class ScalarSum : global::Unity.VisualScripting.Sum<float>, global::Unity.VisualScripting.IDefaultValue<float>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public float defaultValue => 1f;

		public override float Operation(float a, float b)
		{
			return a + b;
		}

		public override float Operation(global::System.Collections.Generic.IEnumerable<float> values)
		{
			return global::System.Linq.Enumerable.Sum(values);
		}
	}
}
