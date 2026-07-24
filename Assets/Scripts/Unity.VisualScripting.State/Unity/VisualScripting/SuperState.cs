namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.StateGraph))]
	public sealed class SuperState : global::Unity.VisualScripting.NesterState<global::Unity.VisualScripting.StateGraph, global::Unity.VisualScripting.StateGraphAsset>, global::Unity.VisualScripting.IGraphEventListener
	{
		public SuperState()
		{
		}

		public SuperState(global::Unity.VisualScripting.StateGraphAsset macro)
			: base(macro)
		{
		}

		public static global::Unity.VisualScripting.SuperState WithStart()
		{
			global::Unity.VisualScripting.SuperState superState = new global::Unity.VisualScripting.SuperState();
			superState.nest.source = global::Unity.VisualScripting.GraphSource.Embed;
			superState.nest.embed = global::Unity.VisualScripting.StateGraph.WithStart();
			return superState;
		}

		protected override void OnEnterImplementation(global::Unity.VisualScripting.Flow flow)
		{
			if (flow.stack.TryEnterParentElement(this))
			{
				base.nest.graph.Start(flow);
				flow.stack.ExitParentElement();
			}
		}

		protected override void OnExitImplementation(global::Unity.VisualScripting.Flow flow)
		{
			if (flow.stack.TryEnterParentElement(this))
			{
				base.nest.graph.Stop(flow);
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
			return pointer.GetElementData<global::Unity.VisualScripting.State.Data>(this).isActive;
		}

		public override global::Unity.VisualScripting.StateGraph DefaultGraph()
		{
			return global::Unity.VisualScripting.StateGraph.WithStart();
		}
	}
}
