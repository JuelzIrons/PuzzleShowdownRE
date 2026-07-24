namespace Unity.VisualScripting
{
	public sealed class GetVariable : global::Unity.VisualScripting.UnifiedVariableUnit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput value { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput fallback { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.InspectorLabel("Fallback")]
		public bool specifyFallback { get; set; }

		protected override void Definition()
		{
			base.Definition();
			value = ValueOutput("value", Get).PredictableIf(IsDefined);
			Requirement(base.name, value);
			if (base.kind == global::Unity.VisualScripting.VariableKind.Object)
			{
				Requirement(base.@object, value);
			}
			if (specifyFallback)
			{
				fallback = ValueInput<object>("fallback");
				Requirement(fallback, value);
			}
		}

		private bool IsDefined(global::Unity.VisualScripting.Flow flow)
		{
			string variable = flow.GetValue<string>(base.name);
			if (string.IsNullOrEmpty(variable))
			{
				return false;
			}
			global::UnityEngine.GameObject gameObject = null;
			if (base.kind == global::Unity.VisualScripting.VariableKind.Object)
			{
				gameObject = flow.GetValue<global::UnityEngine.GameObject>(base.@object);
				if (gameObject == null)
				{
					return false;
				}
			}
			global::UnityEngine.SceneManagement.Scene? scene = flow.stack.scene;
			if (base.kind == global::Unity.VisualScripting.VariableKind.Scene && (!scene.HasValue || !scene.Value.IsValid() || !scene.Value.isLoaded || !global::Unity.VisualScripting.Variables.ExistInScene(scene)))
			{
				return false;
			}
			return base.kind switch
			{
				global::Unity.VisualScripting.VariableKind.Flow => flow.variables.IsDefined(variable), 
				global::Unity.VisualScripting.VariableKind.Graph => global::Unity.VisualScripting.Variables.Graph(flow.stack).IsDefined(variable), 
				global::Unity.VisualScripting.VariableKind.Object => global::Unity.VisualScripting.Variables.Object(gameObject).IsDefined(variable), 
				global::Unity.VisualScripting.VariableKind.Scene => global::Unity.VisualScripting.Variables.Scene(scene.Value).IsDefined(variable), 
				global::Unity.VisualScripting.VariableKind.Application => global::Unity.VisualScripting.Variables.Application.IsDefined(variable), 
				global::Unity.VisualScripting.VariableKind.Saved => global::Unity.VisualScripting.Variables.Saved.IsDefined(variable), 
				_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.VariableKind>(base.kind), 
			};
		}

		private object Get(global::Unity.VisualScripting.Flow flow)
		{
			string variable = flow.GetValue<string>(base.name);
			global::Unity.VisualScripting.VariableDeclarations variableDeclarations = base.kind switch
			{
				global::Unity.VisualScripting.VariableKind.Flow => flow.variables, 
				global::Unity.VisualScripting.VariableKind.Graph => global::Unity.VisualScripting.Variables.Graph(flow.stack), 
				global::Unity.VisualScripting.VariableKind.Object => global::Unity.VisualScripting.Variables.Object(flow.GetValue<global::UnityEngine.GameObject>(base.@object)), 
				global::Unity.VisualScripting.VariableKind.Scene => global::Unity.VisualScripting.Variables.Scene(flow.stack.scene), 
				global::Unity.VisualScripting.VariableKind.Application => global::Unity.VisualScripting.Variables.Application, 
				global::Unity.VisualScripting.VariableKind.Saved => global::Unity.VisualScripting.Variables.Saved, 
				_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.VariableKind>(base.kind), 
			};
			if (specifyFallback && !variableDeclarations.IsDefined(variable))
			{
				return flow.GetValue(fallback);
			}
			return variableDeclarations.Get(variable);
		}
	}
}
