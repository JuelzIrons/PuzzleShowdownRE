namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	[global::Unity.VisualScripting.UnitTitle("Equality Comparison")]
	[global::Unity.VisualScripting.UnitSurtitle("Equality")]
	[global::Unity.VisualScripting.UnitShortTitle("Comparison")]
	[global::Unity.VisualScripting.UnitOrder(4)]
	[global::System.Obsolete("Use the Comparison node instead.")]
	public sealed class EqualityComparison : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A = B")]
		public global::Unity.VisualScripting.ValueOutput equal { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A ≠ B")]
		public global::Unity.VisualScripting.ValueOutput notEqual { get; private set; }

		protected override void Definition()
		{
			a = ValueInput<object>("a").AllowsNull();
			b = ValueInput<object>("b").AllowsNull();
			equal = ValueOutput("equal", Equal).Predictable();
			notEqual = ValueOutput("notEqual", NotEqual).Predictable();
			Requirement(a, equal);
			Requirement(b, equal);
			Requirement(a, notEqual);
			Requirement(b, notEqual);
		}

		private bool Equal(global::Unity.VisualScripting.Flow flow)
		{
			return global::Unity.VisualScripting.OperatorUtility.Equal(flow.GetValue(a), flow.GetValue(b));
		}

		private bool NotEqual(global::Unity.VisualScripting.Flow flow)
		{
			return global::Unity.VisualScripting.OperatorUtility.NotEqual(flow.GetValue(a), flow.GetValue(b));
		}
	}
}
