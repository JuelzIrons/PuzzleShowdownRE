namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitShortTitle("Set Variable")]
	public sealed class SetVariable : global::Unity.VisualScripting.UnifiedVariableUnit
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

		protected override void Definition()
		{
			base.Definition();
			assign = ControlInput("assign", Assign);
			input = ValueInput<object>("input").AllowsNull();
			output = ValueOutput<object>("output");
			assigned = ControlOutput("assigned");
			Requirement(base.name, assign);
			Requirement(input, assign);
			Assignment(assign, output);
			Succession(assign, assigned);
			if (base.kind == global::Unity.VisualScripting.VariableKind.Object)
			{
				Requirement(base.@object, assign);
			}
		}

		private global::Unity.VisualScripting.ControlOutput Assign(global::Unity.VisualScripting.Flow flow)
		{
			string value = flow.GetValue<string>(base.name);
			object value2 = flow.GetValue(input);
			switch (base.kind)
			{
			case global::Unity.VisualScripting.VariableKind.Flow:
				flow.variables.Set(value, value2);
				break;
			case global::Unity.VisualScripting.VariableKind.Graph:
				global::Unity.VisualScripting.Variables.Graph(flow.stack).Set(value, value2);
				break;
			case global::Unity.VisualScripting.VariableKind.Object:
				global::Unity.VisualScripting.Variables.Object(flow.GetValue<global::UnityEngine.GameObject>(base.@object)).Set(value, value2);
				break;
			case global::Unity.VisualScripting.VariableKind.Scene:
				global::Unity.VisualScripting.Variables.Scene(flow.stack.scene).Set(value, value2);
				break;
			case global::Unity.VisualScripting.VariableKind.Application:
				global::Unity.VisualScripting.Variables.Application.Set(value, value2);
				break;
			case global::Unity.VisualScripting.VariableKind.Saved:
				global::Unity.VisualScripting.Variables.Saved.Set(value, value2);
				break;
			default:
				throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.VariableKind>(base.kind);
			}
			flow.SetValue(output, value2);
			return assigned;
		}
	}
}
