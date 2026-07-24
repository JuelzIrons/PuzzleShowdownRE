namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Graphs/Graph Nodes")]
	public abstract class SetGraph<TGraph, TMacro, TMachine> : global::Unity.VisualScripting.Unit where TGraph : class, global::Unity.VisualScripting.IGraph, new() where TMacro : global::Unity.VisualScripting.Macro<TGraph> where TMachine : global::Unity.VisualScripting.Machine<TGraph, TMacro>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; protected set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.NullMeansSelf]
		public global::Unity.VisualScripting.ValueInput target { get; protected set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Graph")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput graphInput { get; protected set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Graph")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput graphOutput { get; protected set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; protected set; }

		protected abstract bool isGameObject { get; }

		private global::System.Type targetType
		{
			get
			{
				if (!isGameObject)
				{
					return typeof(TMachine);
				}
				return typeof(global::UnityEngine.GameObject);
			}
		}

		protected override void Definition()
		{
			enter = ControlInput("enter", SetMacro);
			target = ValueInput(targetType, "target").NullMeansSelf();
			target.SetDefaultValue(targetType.PseudoDefault());
			graphInput = ValueInput<TMacro>("graphInput", null);
			graphOutput = ValueOutput<TMacro>("graphOutput");
			exit = ControlOutput("exit");
			Requirement(graphInput, enter);
			Assignment(enter, graphOutput);
			Succession(enter, exit);
		}

		private global::Unity.VisualScripting.ControlOutput SetMacro(global::Unity.VisualScripting.Flow flow)
		{
			TMacro value = flow.GetValue<TMacro>(graphInput);
			object value2 = flow.GetValue(target, targetType);
			if (value2 is global::UnityEngine.GameObject gameObject)
			{
				gameObject.GetComponent<TMachine>().nest.SwitchToMacro(value);
			}
			else
			{
				((TMachine)value2).nest.SwitchToMacro(value);
			}
			flow.SetValue(graphOutput, value);
			return exit;
		}
	}
}
