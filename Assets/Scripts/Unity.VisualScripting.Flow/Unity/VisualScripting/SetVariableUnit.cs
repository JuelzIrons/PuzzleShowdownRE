namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitShortTitle("Set Variable")]
	public abstract class SetVariableUnit : global::Unity.VisualScripting.VariableUnit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput assign { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("New Value")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput input { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput assigned { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Value")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput output { get; private set; }

		protected SetVariableUnit()
		{
		}

		protected SetVariableUnit(string defaultName)
			: base(defaultName)
		{
		}

		protected override void Definition()
		{
			base.Definition();
			assign = ControlInput("assign", Assign);
			input = ValueInput<object>("input");
			output = ValueOutput<object>("output");
			assigned = ControlOutput("assigned");
			Requirement(input, assign);
			Requirement(base.name, assign);
			Assignment(assign, output);
			Succession(assign, assigned);
		}

		protected virtual global::Unity.VisualScripting.ControlOutput Assign(global::Unity.VisualScripting.Flow flow)
		{
			object value = flow.GetValue<object>(input);
			string value2 = flow.GetValue<string>(base.name);
			GetDeclarations(flow).Set(value2, value);
			flow.SetValue(output, value);
			return assigned;
		}
	}
}
