namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	[global::Unity.VisualScripting.UnitOrder(5)]
	public sealed class Equal : global::Unity.VisualScripting.BinaryComparisonUnit
	{
		protected override string outputKey => "equal";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A = B")]
		[global::Unity.VisualScripting.PortKey("equal")]
		public override global::Unity.VisualScripting.ValueOutput comparison => base.comparison;

		public Equal()
		{
			base.numeric = false;
		}

		protected override bool NumericComparison(float a, float b)
		{
			return global::UnityEngine.Mathf.Approximately(a, b);
		}

		protected override bool GenericComparison(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.Equal(a, b);
		}
	}
}
