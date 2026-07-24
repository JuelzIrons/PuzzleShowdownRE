namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Logic")]
	[global::Unity.VisualScripting.UnitTitle("Comparison")]
	[global::Unity.VisualScripting.UnitShortTitle("Comparison")]
	[global::Unity.VisualScripting.UnitOrder(99)]
	public sealed class Comparison : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput a { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput b { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		public bool numeric { get; set; } = true;

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
		[global::Unity.VisualScripting.PortLabel("A ≠ B")]
		public global::Unity.VisualScripting.ValueOutput aNotEqualToB { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A ≥ B")]
		public global::Unity.VisualScripting.ValueOutput aGreaterThanOrEqualToB { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("A > B")]
		public global::Unity.VisualScripting.ValueOutput aGreatherThanB { get; private set; }

		protected override void Definition()
		{
			if (numeric)
			{
				a = ValueInput<float>("a");
				b = ValueInput("b", 0f);
				aLessThanB = ValueOutput("aLessThanB", (global::Unity.VisualScripting.Flow flow) => NumericLess(flow.GetValue<float>(a), flow.GetValue<float>(b))).Predictable();
				aLessThanOrEqualToB = ValueOutput("aLessThanOrEqualToB", (global::Unity.VisualScripting.Flow flow) => NumericLessOrEqual(flow.GetValue<float>(a), flow.GetValue<float>(b))).Predictable();
				aEqualToB = ValueOutput("aEqualToB", (global::Unity.VisualScripting.Flow flow) => NumericEqual(flow.GetValue<float>(a), flow.GetValue<float>(b))).Predictable();
				aNotEqualToB = ValueOutput("aNotEqualToB", (global::Unity.VisualScripting.Flow flow) => NumericNotEqual(flow.GetValue<float>(a), flow.GetValue<float>(b))).Predictable();
				aGreaterThanOrEqualToB = ValueOutput("aGreaterThanOrEqualToB", (global::Unity.VisualScripting.Flow flow) => NumericGreaterOrEqual(flow.GetValue<float>(a), flow.GetValue<float>(b))).Predictable();
				aGreatherThanB = ValueOutput("aGreatherThanB", (global::Unity.VisualScripting.Flow flow) => NumericGreater(flow.GetValue<float>(a), flow.GetValue<float>(b))).Predictable();
			}
			else
			{
				a = ValueInput<object>("a").AllowsNull();
				b = ValueInput<object>("b").AllowsNull();
				aLessThanB = ValueOutput("aLessThanB", (global::Unity.VisualScripting.Flow flow) => GenericLess(flow.GetValue(a), flow.GetValue(b)));
				aLessThanOrEqualToB = ValueOutput("aLessThanOrEqualToB", (global::Unity.VisualScripting.Flow flow) => GenericLessOrEqual(flow.GetValue(a), flow.GetValue(b)));
				aEqualToB = ValueOutput("aEqualToB", (global::Unity.VisualScripting.Flow flow) => GenericEqual(flow.GetValue(a), flow.GetValue(b)));
				aNotEqualToB = ValueOutput("aNotEqualToB", (global::Unity.VisualScripting.Flow flow) => GenericNotEqual(flow.GetValue(a), flow.GetValue(b)));
				aGreaterThanOrEqualToB = ValueOutput("aGreaterThanOrEqualToB", (global::Unity.VisualScripting.Flow flow) => GenericGreaterOrEqual(flow.GetValue(a), flow.GetValue(b)));
				aGreatherThanB = ValueOutput("aGreatherThanB", (global::Unity.VisualScripting.Flow flow) => GenericGreater(flow.GetValue(a), flow.GetValue(b)));
			}
			Requirement(a, aLessThanB);
			Requirement(b, aLessThanB);
			Requirement(a, aLessThanOrEqualToB);
			Requirement(b, aLessThanOrEqualToB);
			Requirement(a, aEqualToB);
			Requirement(b, aEqualToB);
			Requirement(a, aNotEqualToB);
			Requirement(b, aNotEqualToB);
			Requirement(a, aGreaterThanOrEqualToB);
			Requirement(b, aGreaterThanOrEqualToB);
			Requirement(a, aGreatherThanB);
			Requirement(b, aGreatherThanB);
		}

		private bool NumericLess(float a, float b)
		{
			return a < b;
		}

		private bool NumericLessOrEqual(float a, float b)
		{
			if (!(a < b))
			{
				return global::UnityEngine.Mathf.Approximately(a, b);
			}
			return true;
		}

		private bool NumericEqual(float a, float b)
		{
			return global::UnityEngine.Mathf.Approximately(a, b);
		}

		private bool NumericNotEqual(float a, float b)
		{
			return !global::UnityEngine.Mathf.Approximately(a, b);
		}

		private bool NumericGreaterOrEqual(float a, float b)
		{
			if (!(a > b))
			{
				return global::UnityEngine.Mathf.Approximately(a, b);
			}
			return true;
		}

		private bool NumericGreater(float a, float b)
		{
			return a > b;
		}

		private bool GenericLess(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.LessThan(a, b);
		}

		private bool GenericLessOrEqual(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.LessThanOrEqual(a, b);
		}

		private bool GenericEqual(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.Equal(a, b);
		}

		private bool GenericNotEqual(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.NotEqual(a, b);
		}

		private bool GenericGreaterOrEqual(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.GreaterThanOrEqual(a, b);
		}

		private bool GenericGreater(object a, object b)
		{
			return global::Unity.VisualScripting.OperatorUtility.GreaterThan(a, b);
		}
	}
}
