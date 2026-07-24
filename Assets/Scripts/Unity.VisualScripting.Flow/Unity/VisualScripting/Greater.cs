namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	[global::Unity.VisualScripting.UnitOrder(11)]
	public sealed class Greater : global::Unity.VisualScripting.BinaryComparisonUnit
	{
		[global::Unity.VisualScripting.PortLabel("A > B")]
		public override global::Unity.VisualScripting.ValueOutput comparison => base.comparison;

		protected override bool NumericComparison(float a, float b)
		{
			return a > b;
		}

		protected override bool GenericComparison(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.GreaterThan(a, b);
		}
	}
}
