namespace Unity.VisualScripting
{
	public abstract class StateTransition : global::Unity.VisualScripting.GraphElement<global::Unity.VisualScripting.StateGraph>, global::Unity.VisualScripting.IStateTransition, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable, global::Unity.VisualScripting.IConnection<global::Unity.VisualScripting.IState, global::Unity.VisualScripting.IState>
	{
		public class DebugData : global::Unity.VisualScripting.IStateTransitionDebugData, global::Unity.VisualScripting.IGraphElementDebugData
		{
			public global::System.Exception runtimeException { get; set; }

			public int lastBranchFrame { get; set; }

			public float lastBranchTime { get; set; }
		}

		public override int dependencyOrder => 1;

		[global::Unity.VisualScripting.Serialize]
		public global::Unity.VisualScripting.IState source { get; internal set; }

		[global::Unity.VisualScripting.Serialize]
		public global::Unity.VisualScripting.IState destination { get; internal set; }

		protected StateTransition()
		{
		}

		protected StateTransition(global::Unity.VisualScripting.IState source, global::Unity.VisualScripting.IState destination)
		{
			global::Unity.VisualScripting.Ensure.That("source").IsNotNull(source);
			global::Unity.VisualScripting.Ensure.That("destination").IsNotNull(destination);
			if (source.graph != destination.graph)
			{
				throw new global::System.NotSupportedException("Cannot create transitions across state graphs.");
			}
			this.source = source;
			this.destination = destination;
		}

		public global::Unity.VisualScripting.IGraphElementDebugData CreateDebugData()
		{
			return new global::Unity.VisualScripting.StateTransition.DebugData();
		}

		public override void Instantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			base.Instantiate(instance);
			if (this is global::Unity.VisualScripting.IGraphEventListener listener && instance.GetElementData<global::Unity.VisualScripting.State.Data>(source).isActive)
			{
				listener.StartListening(instance);
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

		public void Branch(global::Unity.VisualScripting.Flow flow)
		{
			if (flow.enableDebug)
			{
				global::Unity.VisualScripting.StateTransition.DebugData elementDebugData = flow.stack.GetElementDebugData<global::Unity.VisualScripting.StateTransition.DebugData>(this);
				elementDebugData.lastBranchFrame = global::Unity.VisualScripting.EditorTimeBinding.frame;
				elementDebugData.lastBranchTime = global::Unity.VisualScripting.EditorTimeBinding.time;
			}
			try
			{
				source.OnExit(flow, global::Unity.VisualScripting.StateExitReason.Branch);
			}
			catch (global::System.Exception ex)
			{
				source.HandleException(flow.stack, ex);
				throw;
			}
			source.OnBranchTo(flow, destination);
			try
			{
				destination.OnEnter(flow, global::Unity.VisualScripting.StateEnterReason.Branch);
			}
			catch (global::System.Exception ex2)
			{
				destination.HandleException(flow.stack, ex2);
				throw;
			}
		}

		public abstract void OnEnter(global::Unity.VisualScripting.Flow flow);

		public abstract void OnExit(global::Unity.VisualScripting.Flow flow);

		public override global::Unity.VisualScripting.AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			return null;
		}
	}
}
