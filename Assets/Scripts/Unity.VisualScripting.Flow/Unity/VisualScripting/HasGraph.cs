namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Graphs/Graph Nodes")]
	public abstract class HasGraph<TGraph, TMacro, TMachine> : global::Unity.VisualScripting.Unit where TGraph : class, global::Unity.VisualScripting.IGraph, new() where TMacro : global::Unity.VisualScripting.Macro<TGraph> where TMachine : global::Unity.VisualScripting.Machine<TGraph, TMacro>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.NullMeansSelf]
		public global::Unity.VisualScripting.ValueInput target { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Graph")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput graphInput { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Has Graph")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput hasGraphOutput { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

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
			enter = ControlInput("enter", TriggerHasGraph);
			target = ValueInput(targetType, "target").NullMeansSelf();
			target.SetDefaultValue(targetType.PseudoDefault());
			graphInput = ValueInput<TMacro>("graphInput", null);
			hasGraphOutput = ValueOutput("hasGraphOutput", OutputHasGraph);
			exit = ControlOutput("exit");
			Requirement(graphInput, enter);
			Assignment(enter, hasGraphOutput);
			Succession(enter, exit);
		}

		private global::Unity.VisualScripting.ControlOutput TriggerHasGraph(global::Unity.VisualScripting.Flow flow)
		{
			flow.SetValue(hasGraphOutput, OutputHasGraph(flow));
			return exit;
		}

		private bool OutputHasGraph(global::Unity.VisualScripting.Flow flow)
		{
			TMacro macro = flow.GetValue<TMacro>(graphInput);
			if (flow.GetValue(target, targetType) is global::UnityEngine.GameObject gameObject)
			{
				if (gameObject != null)
				{
					TMachine[] components = gameObject.GetComponents<TMachine>();
					macro = flow.GetValue<TMacro>(graphInput);
					return global::System.Linq.Enumerable.Any(global::System.Linq.Enumerable.Where(components, (TMachine currentMachine) => currentMachine != null), (TMachine currentMachine) => currentMachine.graph != null && currentMachine.graph.Equals(macro.graph));
				}
			}
			else
			{
				TMachine value = flow.GetValue<TMachine>(target);
				if (value.graph != null && value.graph.Equals(macro.graph))
				{
					return true;
				}
			}
			return false;
		}
	}
}
