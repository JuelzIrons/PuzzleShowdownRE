namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.StateGraph))]
	[global::Unity.VisualScripting.UnitCategory("Nesting")]
	public sealed class StateUnit : global::Unity.VisualScripting.NesterUnit<global::Unity.VisualScripting.StateGraph, global::Unity.VisualScripting.StateGraphAsset>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlInput start { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlInput stop { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput started { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput stopped { get; private set; }

		public StateUnit()
		{
		}

		public StateUnit(global::Unity.VisualScripting.StateGraphAsset macro)
			: base(macro)
		{
		}

		public static global::Unity.VisualScripting.StateUnit WithStart()
		{
			global::Unity.VisualScripting.StateUnit stateUnit = new global::Unity.VisualScripting.StateUnit();
			stateUnit.nest.source = global::Unity.VisualScripting.GraphSource.Embed;
			stateUnit.nest.embed = global::Unity.VisualScripting.StateGraph.WithStart();
			return stateUnit;
		}

		protected override void Definition()
		{
			start = ControlInput("start", Start);
			stop = ControlInput("stop", Stop);
			started = ControlOutput("started");
			stopped = ControlOutput("stopped");
			Succession(start, started);
			Succession(stop, stopped);
		}

		private global::Unity.VisualScripting.ControlOutput Start(global::Unity.VisualScripting.Flow flow)
		{
			flow.stack.EnterParentElement(this);
			base.nest.graph.Start(flow);
			flow.stack.ExitParentElement();
			return started;
		}

		private global::Unity.VisualScripting.ControlOutput Stop(global::Unity.VisualScripting.Flow flow)
		{
			flow.stack.EnterParentElement(this);
			base.nest.graph.Stop(flow);
			flow.stack.ExitParentElement();
			return stopped;
		}

		public override global::Unity.VisualScripting.StateGraph DefaultGraph()
		{
			return global::Unity.VisualScripting.StateGraph.WithStart();
		}
	}
}
