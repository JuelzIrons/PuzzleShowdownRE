namespace Unity.VisualScripting
{
	public interface IStateTransition : global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable, global::Unity.VisualScripting.IConnection<global::Unity.VisualScripting.IState, global::Unity.VisualScripting.IState>
	{
		void Branch(global::Unity.VisualScripting.Flow flow);

		void OnEnter(global::Unity.VisualScripting.Flow flow);

		void OnExit(global::Unity.VisualScripting.Flow flow);
	}
}
