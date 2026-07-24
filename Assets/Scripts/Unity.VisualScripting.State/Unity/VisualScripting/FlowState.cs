namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.FlowGraph))]
	[global::System.ComponentModel.DisplayName("Script State")]
	public sealed class FlowState : global::Unity.VisualScripting.NesterState<global::Unity.VisualScripting.FlowGraph, global::Unity.VisualScripting.ScriptGraphAsset>, global::Unity.VisualScripting.IGraphEventListener
	{
		public FlowState()
		{
		}

		public FlowState(global::Unity.VisualScripting.ScriptGraphAsset macro)
			: base(macro)
		{
		}

		protected override void OnEnterImplementation(global::Unity.VisualScripting.Flow flow)
		{
			if (flow.stack.TryEnterParentElement(this))
			{
				base.nest.graph.StartListening(flow.stack);
				flow.stack.TriggerEventHandler((global::Unity.VisualScripting.EventHook hook) => hook == "OnEnterState", default(global::Unity.VisualScripting.EmptyEventArgs), (global::Unity.VisualScripting.IGraphParentElement parent) => parent is global::Unity.VisualScripting.SubgraphUnit, force: false);
				flow.stack.ExitParentElement();
			}
		}

		protected override void OnExitImplementation(global::Unity.VisualScripting.Flow flow)
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
			return pointer.GetElementData<global::Unity.VisualScripting.State.Data>(this).isActive;
		}

		public override global::Unity.VisualScripting.FlowGraph DefaultGraph()
		{
			return GraphWithEnterUpdateExit();
		}

		public static global::Unity.VisualScripting.FlowState WithEnterUpdateExit()
		{
			global::Unity.VisualScripting.FlowState flowState = new global::Unity.VisualScripting.FlowState();
			flowState.nest.source = global::Unity.VisualScripting.GraphSource.Embed;
			flowState.nest.embed = GraphWithEnterUpdateExit();
			return flowState;
		}

		public static global::Unity.VisualScripting.FlowGraph GraphWithEnterUpdateExit()
		{
			return new global::Unity.VisualScripting.FlowGraph
			{
				units = 
				{
					(global::Unity.VisualScripting.IUnit)new global::Unity.VisualScripting.OnEnterState
					{
						position = new global::UnityEngine.Vector2(-205f, -215f)
					},
					(global::Unity.VisualScripting.IUnit)new global::Unity.VisualScripting.Update
					{
						position = new global::UnityEngine.Vector2(-161f, -38f)
					},
					(global::Unity.VisualScripting.IUnit)new global::Unity.VisualScripting.OnExitState
					{
						position = new global::UnityEngine.Vector2(-205f, 145f)
					}
				}
			};
		}
	}
}
