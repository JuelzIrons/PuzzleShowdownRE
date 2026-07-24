namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	[global::Unity.VisualScripting.UnitOrder(6)]
	public sealed class NotEqual : global::Unity.VisualScripting.BinaryComparisonUnit
	{
		protected override string outputKey => "notEqual";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A ≠ B")]
		[global::Unity.VisualScripting.PortKey("notEqual")]
		public override global::Unity.VisualScripting.ValueOutput comparison => base.comparison;

		public NotEqual()
		{
			base.numeric = false;
		}

		protected override bool NumericComparison(float a, float b)
		{
			return !global::UnityEngine.Mathf.Approximately(a, b);
		}

		protected override bool GenericComparison(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.NotEqual(a, b);
		}
	}
}
