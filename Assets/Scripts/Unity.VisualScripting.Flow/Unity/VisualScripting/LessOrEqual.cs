namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	[global::Unity.VisualScripting.UnitOrder(10)]
	public sealed class LessOrEqual : global::Unity.VisualScripting.BinaryComparisonUnit
	{
		[global::Unity.VisualScripting.PortLabel("A ≤ B")]
		public override global::Unity.VisualScripting.ValueOutput comparison => base.comparison;

		protected override bool NumericComparison(float a, float b)
		{
			if (!(a < b))
			{
				return global::UnityEngine.Mathf.Approximately(a, b);
			}
			return true;
		}

		protected override bool GenericComparison(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.LessThanOrEqual(a, b);
		}
	}
}
