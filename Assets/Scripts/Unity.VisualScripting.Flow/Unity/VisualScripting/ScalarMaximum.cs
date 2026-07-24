namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Math/Scalar")]
	[global::Unity.VisualScripting.UnitTitle("Maximum")]
	public sealed class ScalarMaximum : global::Unity.VisualScripting.Maximum<float>
	{
		public override float Operation(float a, float b)
		{
			return global::UnityEngine.Mathf.Max(a, b);
		}

		public override float Operation(global::System.Collections.Generic.IEnumerable<float> values)
		{
			return global::System.Linq.Enumerable.Max(values);
		}
	}
}
