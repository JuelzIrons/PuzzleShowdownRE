namespace Unity.VisualScripting
{
	public abstract class State : global::Unity.VisualScripting.GraphElement<global::Unity.VisualScripting.StateGraph>, global::Unity.VisualScripting.IState, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable, global::Unity.VisualScripting.IGraphElementWithData
	{
		public class Data : global::Unity.VisualScripting.IGraphElementData
		{
			public bool isActive;

			public bool hasEntered;
		}

		public class DebugData : global::Unity.VisualScripting.IStateDebugData, global::Unity.VisualScripting.IGraphElementDebugData
		{
			public int lastEnterFrame { get; set; }

			public float lastExitTime { get; set; }

			public global::System.Exception runtimeException { get; set; }
		}

		public const float DefaultWidth = 170f;

		[global::Unity.VisualScripting.Serialize]
		public bool isStart { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public virtual bool canBeSource => true;

		[global::Unity.VisualScripting.DoNotSerialize]
		public virtual bool canBeDestination => true;

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IStateTransition> outgoingTransitions => base.graph?.transitions.WithSource(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.IStateTransition>();

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IStateTransition> incomingTransitions => base.graph?.transitions.WithDestination(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.IStateTransition>();

		protected global::System.Collections.Generic.List<global::Unity.VisualScripting.IStateTransition> outgoingTransitionsNoAlloc => base.graph?.transitions.WithSourceNoAlloc(this) ?? global::Unity.VisualScripting.Empty<global::Unity.VisualScripting.IStateTransition>.list;

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IStateTransition> transitions => global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IStateTransition>(new global::System.Collections.IEnumerable[2] { outgoingTransitions, incomingTransitions });

		[global::Unity.VisualScripting.Serialize]
		public global::UnityEngine.Vector2 position { get; set; }

		[global::Unity.VisualScripting.Serialize]
		public float width { get; set; } = 170f;

		global::Unity.VisualScripting.StateGraph global::Unity.VisualScripting.IState.graph => base.graph;

		public global::Unity.VisualScripting.IGraphElementData CreateData()
		{
			return new global::Unity.VisualScripting.State.Data();
		}

		public global::Unity.VisualScripting.IGraphElementDebugData CreateDebugData()
		{
			return new global::Unity.VisualScripting.State.DebugData();
		}

		public override void BeforeRemove()
		{
			base.BeforeRemove();
			Disconnect();
		}

		public override void Instantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			base.Instantiate(instance);
			global::Unity.VisualScripting.State.Data elementData = instance.GetElementData<global::Unity.VisualScripting.State.Data>(this);
			if (this is global::Unity.VisualScripting.IGraphEventListener listener && elementData.isActive)
			{
				listener.StartListening(instance);
			}
			else if (isStart && !elementData.hasEntered && base.graph.IsListening(instance))
			{
				using (global::Unity.VisualScripting.Flow flow = global::Unity.VisualScripting.Flow.New(instance))
				{
					OnEnter(flow, global::Unity.VisualScripting.StateEnterReason.Start);
				}
			}
		}

		public override void Uninstantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			if (this is global::Unity.VisualScripting.IGraphEventListener listener)
			{
				listener.StopListening(instance);
			}
			base.Uninstantiate(instance);
		}

		protected void CopyFrom(global::Unity.VisualScripting.State source)
		{
			CopyFrom((global::Unity.VisualScripting.GraphElement<global::Unity.VisualScripting.StateGraph>)source);
			isStart = source.isStart;
			width = source.width;
		}

		public void Disconnect()
		{
			global::Unity.VisualScripting.IStateTransition[] array = global::System.Linq.Enumerable.ToArray(transitions);
			foreach (global::Unity.VisualScripting.IStateTransition item in array)
			{
				base.graph.transitions.Remove(item);
			}
		}

		public virtual void OnEnter(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.StateEnterReason reason)
		{
			global::Unity.VisualScripting.State.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.State.Data>(this);
			if (elementData.isActive)
			{
				return;
			}
			elementData.isActive = true;
			elementData.hasEntered = true;
			foreach (global::Unity.VisualScripting.IStateTransition item in outgoingTransitionsNoAlloc)
			{
				(item as global::Unity.VisualScripting.IGraphEventListener)?.StartListening(flow.stack);
			}
			if (flow.enableDebug)
			{
				flow.stack.GetElementDebugData<global::Unity.VisualScripting.State.DebugData>(this).lastEnterFrame = global::Unity.VisualScripting.EditorTimeBinding.frame;
			}
			OnEnterImplementation(flow);
			foreach (global::Unity.VisualScripting.IStateTransition item2 in outgoingTransitionsNoAlloc)
			{
				try
				{
					item2.OnEnter(flow);
				}
				catch (global::System.Exception ex)
				{
					item2.HandleException(flow.stack, ex);
					throw;
				}
			}
		}

		public virtual void OnExit(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.StateExitReason reason)
		{
			global::Unity.VisualScripting.State.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.State.Data>(this);
			if (!elementData.isActive)
			{
				return;
			}
			OnExitImplementation(flow);
			elementData.isActive = false;
			if (flow.enableDebug)
			{
				flow.stack.GetElementDebugData<global::Unity.VisualScripting.State.DebugData>(this).lastExitTime = global::Unity.VisualScripting.EditorTimeBinding.time;
			}
			foreach (global::Unity.VisualScripting.IStateTransition item in outgoingTransitionsNoAlloc)
			{
				try
				{
					item.OnExit(flow);
				}
				catch (global::System.Exception ex)
				{
					item.HandleException(flow.stack, ex);
					throw;
				}
			}
		}

		protected virtual void OnEnterImplementation(global::Unity.VisualScripting.Flow flow)
		{
		}

		protected virtual void UpdateImplementation(global::Unity.VisualScripting.Flow flow)
		{
		}

		protected virtual void FixedUpdateImplementation(global::Unity.VisualScripting.Flow flow)
		{
		}

		protected virtual void LateUpdateImplementation(global::Unity.VisualScripting.Flow flow)
		{
		}

		protected virtual void OnExitImplementation(global::Unity.VisualScripting.Flow flow)
		{
		}

		public virtual void OnBranchTo(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.IState destination)
		{
		}

		public override global::Unity.VisualScripting.AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			global::Unity.VisualScripting.AnalyticsIdentifier obj = new global::Unity.VisualScripting.AnalyticsIdentifier
			{
				Identifier = GetType().FullName,
				Namespace = GetType().Namespace
			};
			obj.Hashcode = obj.Identifier.GetHashCode();
			return obj;
		}
	}
}
