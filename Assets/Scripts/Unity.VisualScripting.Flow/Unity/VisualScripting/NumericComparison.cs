namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	[global::Unity.VisualScripting.UnitTitle("Numeric Comparison")]
	[global::Unity.VisualScripting.UnitSurtitle("Numeric")]
	[global::Unity.VisualScripting.UnitShortTitle("Comparison")]
	[global::Unity.VisualScripting.UnitOrder(99)]
	[global::System.Obsolete("Use the Comparison node with Numeric enabled instead.")]
	public sealed class NumericComparison : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A < B")]
		public global::Unity.VisualScripting.ValueOutput aLessThanB { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A ≤ B")]
		public global::Unity.VisualScripting.ValueOutput aLessThanOrEqualToB { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A = B")]
		public global::Unity.VisualScripting.ValueOutput aEqualToB { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A ≥ B")]
		public global::Unity.VisualScripting.ValueOutput aGreaterThanOrEqualToB { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A > B")]
		public global::Unity.VisualScripting.ValueOutput aGreatherThanB { get; private set; }

		protected override void Definition()
		{
			a = ValueInput<float>("a");
			b = ValueInput("b", 0f);
			aLessThanB = ValueOutput("aLessThanB", Less).Predictable();
			aLessThanOrEqualToB = ValueOutput("aLessThanOrEqualToB", LessOrEqual).Predictable();
			aEqualToB = ValueOutput("aEqualToB", Equal).Predictable();
			aGreaterThanOrEqualToB = ValueOutput("aGreaterThanOrEqualToB", GreaterOrEqual).Predictable();
			aGreatherThanB = ValueOutput("aGreatherThanB", Greater).Predictable();
			Requirement(a, aLessThanB);
			Requirement(b, aLessThanB);
			Requirement(a, aLessThanOrEqualToB);
			Requirement(b, aLessThanOrEqualToB);
			Requirement(a, aEqualToB);
			Requirement(b, aEqualToB);
			Requirement(a, aGreaterThanOrEqualToB);
			Requirement(b, aGreaterThanOrEqualToB);
			Requirement(a, aGreatherThanB);
			Requirement(b, aGreatherThanB);
		}

		private bool Less(global::Unity.VisualScripting.Flow flow)
		{
			return flow.GetValue<float>(a) < flow.GetValue<float>(b);
		}

		private bool LessOrEqual(global::Unity.VisualScripting.Flow flow)
		{
			float value = flow.GetValue<float>(a);
			float value2 = flow.GetValue<float>(b);
			if (!(value < value2))
			{
				return global::UnityEngine.Mathf.Approximately(value, value2);
			}
			return true;
		}

		private bool Equal(global::Unity.VisualScripting.Flow flow)
		{
			return global::UnityEngine.Mathf.Approximately(flow.GetValue<float>(a), flow.GetValue<float>(b));
		}

		private bool GreaterOrEqual(global::Unity.VisualScripting.Flow flow)
		{
			float value = flow.GetValue<float>(a);
			float value2 = flow.GetValue<float>(b);
			if (!(value > value2))
			{
				return global::UnityEngine.Mathf.Approximately(value, value2);
			}
			return true;
		}

		private bool Greater(global::Unity.VisualScripting.Flow flow)
		{
			return flow.GetValue<float>(a) < flow.GetValue<float>(b);
		}
	}
}
