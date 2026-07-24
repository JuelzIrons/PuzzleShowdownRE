namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitTitle("Has Variable")]
	public sealed class IsVariableDefined : global::Unity.VisualScripting.UnifiedVariableUnit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Defined")]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.PortKey("isDefined")]
		public global::Unity.VisualScripting.ValueOutput isVariableDefined { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			isVariableDefined = ValueOutput("isDefined", IsDefined);
			Requirement(base.name, isVariableDefined);
			if (base.kind == global::Unity.VisualScripting.VariableKind.Object)
			{
				Requirement(base.@object, isVariableDefined);
			}
		}

		private bool IsDefined(global::Unity.VisualScripting.Flow flow)
		{
			string value = flow.GetValue<string>(base.name);
			return base.kind switch
			{
				global::Unity.VisualScripting.VariableKind.Flow => flow.variables.IsDefined(value), 
				global::Unity.VisualScripting.VariableKind.Graph => global::Unity.VisualScripting.Variables.Graph(flow.stack).IsDefined(value), 
				global::Unity.VisualScripting.VariableKind.Object => global::Unity.VisualScripting.Variables.Object(flow.GetValue<global::UnityEngine.GameObject>(base.@object)).IsDefined(value), 
				global::Unity.VisualScripting.VariableKind.Scene => global::Unity.VisualScripting.Variables.Scene(flow.stack.scene).IsDefined(value), 
				global::Unity.VisualScripting.VariableKind.Application => global::Unity.VisualScripting.Variables.Application.IsDefined(value), 
				global::Unity.VisualScripting.VariableKind.Saved => global::Unity.VisualScripting.Variables.Saved.IsDefined(value), 
				_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.VariableKind>(base.kind), 
			};
		}
	}
}
