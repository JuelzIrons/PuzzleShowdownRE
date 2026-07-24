namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	public sealed class FlowStateTransition : global::Unity.VisualScripting.NesterStateTransition<global::Unity.VisualScripting.FlowGraph, global::Unity.VisualScripting.ScriptGraphAsset>, global::Unity.VisualScripting.IGraphEventListener
	{
		public FlowStateTransition()
		{
		}

		public FlowStateTransition(global::Unity.VisualScripting.IState source, global::Unity.VisualScripting.IState destination)
			: base(source, destination)
		{
			if (!source.canBeSource)
			{
				throw new global::System.InvalidOperationException("Source state cannot emit transitions.");
			}
			if (!destination.canBeDestination)
			{
				throw new global::System.InvalidOperationException("Destination state cannot receive transitions.");
			}
		}

		public static global::Unity.VisualScripting.FlowStateTransition WithDefaultTrigger(global::Unity.VisualScripting.IState source, global::Unity.VisualScripting.IState destination)
		{
			global::Unity.VisualScripting.FlowStateTransition flowStateTransition = new global::Unity.VisualScripting.FlowStateTransition(source, destination);
			flowStateTransition.nest.source = global::Unity.VisualScripting.GraphSource.Embed;
			flowStateTransition.nest.embed = GraphWithDefaultTrigger();
			return flowStateTransition;
		}

		public static global::Unity.VisualScripting.FlowGraph GraphWithDefaultTrigger()
		{
			return new global::Unity.VisualScripting.FlowGraph
			{
				units = { (global::Unity.VisualScripting.IUnit)new global::Unity.VisualScripting.TriggerStateTransition
				{
					position = new global::UnityEngine.Vector2(100f, -50f)
				} }
			};
		}

		public override void OnEnter(global::Unity.VisualScripting.Flow flow)
		{
			if (flow.stack.TryEnterParentElement(this))
			{
				flow.stack.TriggerEventHandler((global::Unity.VisualScripting.EventHook hook) => hook == "OnEnterState", default(global::Unity.VisualScripting.EmptyEventArgs), (global::Unity.VisualScripting.IGraphParentElement parent) => parent is global::Unity.VisualScripting.SubgraphUnit, force: false);
				flow.stack.ExitParentElement();
			}
		}

		public override void OnExit(global::Unity.VisualScripting.Flow flow)
		{
			if (flow.stack.TryEnterParentElement(this))
			{
				flow.stack.TriggerEventHandler((global::Unity.VisualScripting.EventHook hook) => hook == "OnExitState", default(global::Unity.VisualScripting.EmptyEventArgs), (global::Unity.VisualScripting.IGraphParentElement parent) => parent is global::Unity.VisualScripting.SubgraphUnit, force: false);
				base.nest.graph.StopListening(flow.stack);
				flow.stack.ExitParentElement();
			}
		}

		public void StartListening(global::Unity.VisualScripting.GraphStack stack)
		{
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StartListening(stack);
				stack.ExitParentElement();
			}
		}

		public void StopListening(global::Unity.VisualScripting.GraphStack stack)
		{
			if (stack.TryEnterParentElement(this))
			{
				base.nest.graph.StopListening(stack);
				stack.ExitParentElement();
			}
		}

		public bool IsListening(global::Unity.VisualScripting.GraphPointer pointer)
		{
			return pointer.GetElementData<global::Unity.VisualScripting.State.Data>(base.source).isActive;
		}

		public override global::Unity.VisualScripting.FlowGraph DefaultGraph()
		{
			return GraphWithDefaultTrigger();
		}
	}
}
