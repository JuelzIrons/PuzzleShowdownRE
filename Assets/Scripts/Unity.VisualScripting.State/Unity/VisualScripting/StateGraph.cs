namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	public sealed class StateGraph : global::Unity.VisualScripting.Graph, global::Unity.VisualScripting.IGraphEventListener
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.IState> states { get; internal set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.GraphConnectionCollection<global::Unity.VisualScripting.IStateTransition, global::Unity.VisualScripting.IState, global::Unity.VisualScripting.IState> transitions { get; internal set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.GraphGroup> groups { get; internal set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.StickyNote> sticky { get; private set; }

		public StateGraph()
		{
			states = new global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.IState>(this);
			transitions = new global::Unity.VisualScripting.GraphConnectionCollection<global::Unity.VisualScripting.IStateTransition, global::Unity.VisualScripting.IState, global::Unity.VisualScripting.IState>(this);
			groups = new global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.GraphGroup>(this);
			sticky = new global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.StickyNote>(this);
			base.elements.Include(states);
			base.elements.Include(transitions);
			base.elements.Include(groups);
			base.elements.Include(sticky);
		}

		public override global::Unity.VisualScripting.IGraphData CreateData()
		{
			return new global::Unity.VisualScripting.StateGraphData(this);
		}

		public void StartListening(global::Unity.VisualScripting.GraphStack stack)
		{
			stack.GetGraphData<global::Unity.VisualScripting.StateGraphData>().isListening = true;
			global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.IState> activeStatesNoAlloc = GetActiveStatesNoAlloc(stack);
			foreach (global::Unity.VisualScripting.IState item in activeStatesNoAlloc)
			{
				(item as global::Unity.VisualScripting.IGraphEventListener)?.StartListening(stack);
			}
			activeStatesNoAlloc.Free();
		}

		public void StopListening(global::Unity.VisualScripting.GraphStack stack)
		{
			global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.IState> activeStatesNoAlloc = GetActiveStatesNoAlloc(stack);
			foreach (global::Unity.VisualScripting.IState item in activeStatesNoAlloc)
			{
				(item as global::Unity.VisualScripting.IGraphEventListener)?.StopListening(stack);
			}
			activeStatesNoAlloc.Free();
			stack.GetGraphData<global::Unity.VisualScripting.StateGraphData>().isListening = false;
		}

		public bool IsListening(global::Unity.VisualScripting.GraphPointer pointer)
		{
			return pointer.GetGraphData<global::Unity.VisualScripting.StateGraphData>().isListening;
		}

		private global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.IState> GetActiveStatesNoAlloc(global::Unity.VisualScripting.GraphPointer pointer)
		{
			global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.IState> hashSet = global::Unity.VisualScripting.HashSetPool<global::Unity.VisualScripting.IState>.New();
			foreach (global::Unity.VisualScripting.IState state in states)
			{
				if (pointer.GetElementData<global::Unity.VisualScripting.State.Data>(state).isActive)
				{
					hashSet.Add(state);
				}
			}
			return hashSet;
		}

		public void Start(global::Unity.VisualScripting.Flow flow)
		{
			flow.stack.GetGraphData<global::Unity.VisualScripting.StateGraphData>().isListening = true;
			foreach (global::Unity.VisualScripting.IState item in global::System.Linq.Enumerable.Where(states, (global::Unity.VisualScripting.IState s) => s.isStart))
			{
				try
				{
					item.OnEnter(flow, global::Unity.VisualScripting.StateEnterReason.Start);
				}
				catch (global::System.Exception ex)
				{
					item.HandleException(flow.stack, ex);
					throw;
				}
			}
		}

		public void Stop(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.IState> activeStatesNoAlloc = GetActiveStatesNoAlloc(flow.stack);
			foreach (global::Unity.VisualScripting.IState item in activeStatesNoAlloc)
			{
				try
				{
					item.OnExit(flow, global::Unity.VisualScripting.StateExitReason.Stop);
				}
				catch (global::System.Exception ex)
				{
					item.HandleException(flow.stack, ex);
					throw;
				}
			}
			activeStatesNoAlloc.Free();
			flow.stack.GetGraphData<global::Unity.VisualScripting.StateGraphData>().isListening = false;
		}

		public static global::Unity.VisualScripting.StateGraph WithStart()
		{
			global::Unity.VisualScripting.StateGraph stateGraph = new global::Unity.VisualScripting.StateGraph();
			global::Unity.VisualScripting.FlowState flowState = global::Unity.VisualScripting.FlowState.WithEnterUpdateExit();
			flowState.isStart = true;
			flowState.nest.embed.title = "Start";
			flowState.position = new global::UnityEngine.Vector2(-86f, -15f);
			stateGraph.states.Add(flowState);
			return stateGraph;
		}
	}
}
