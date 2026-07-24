namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Minimum")]
	public sealed class ScalarMinimum : global::Unity.VisualScripting.Minimum<float>
	{
		public override float Operation(float a, float b)
		{
			return global::UnityEngine.Mathf.Min(a, b);
		}

		public override float Operation(global::System.Collections.Generic.IEnumerable<float> values)
		{
			return global::System.Linq.Enumerable.Min(values);
		}
	}
}
