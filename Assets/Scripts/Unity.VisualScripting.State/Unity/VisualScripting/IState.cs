namespace Unity.VisualScripting
{
	public interface IState : global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable, global::Unity.VisualScripting.IGraphElementWithData
	{
		new global::Unity.VisualScripting.StateGraph graph { get; }

		bool isStart { get; set; }

		bool canBeSource { get; }

		bool canBeDestination { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IStateTransition> outgoingTransitions { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IStateTransition> incomingTransitions { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IStateTransition> transitions { get; }

		global::UnityEngine.Vector2 position { get; set; }

		float width { get; set; }

		void OnBranchTo(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.IState destination);

		void OnEnter(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.StateEnterReason reason);

		void OnExit(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.StateExitReason reason);
	}
}
