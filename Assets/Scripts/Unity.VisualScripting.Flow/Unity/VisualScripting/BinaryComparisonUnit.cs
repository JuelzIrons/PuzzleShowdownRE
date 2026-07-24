namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	public abstract class BinaryComparisonUnit : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public virtual global::Unity.VisualScripting.ValueOutput comparison { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.InspectorToggleLeft]
		public bool numeric { get; set; } = true;

		protected virtual string outputKey => "comparison";

		protected override void Definition()
		{
			if (numeric)
			{
				a = ValueInput<float>("a");
				b = ValueInput("b", 0f);
				comparison = ValueOutput(outputKey, NumericComparison).Predictable();
			}
			else
			{
				a = ValueInput<object>("a").AllowsNull();
				b = ValueInput<object>("b").AllowsNull();
				comparison = ValueOutput(outputKey, GenericComparison).Predictable();
			}
			Requirement(a, comparison);
			Requirement(b, comparison);
		}

		private bool NumericComparison(global::Unity.VisualScripting.Flow flow)
		{
			return NumericComparison(flow.GetValue<float>(a), flow.GetValue<float>(b));
		}

		private bool GenericComparison(global::Unity.VisualScripting.Flow flow)
		{
			return GenericComparison(flow.GetValue(a), flow.GetValue(b));
		}

		protected abstract bool NumericComparison(float a, float b);

		protected abstract bool GenericComparison(object a, object b);
	}
}
