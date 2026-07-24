namespace Unity.VisualScripting
{
	public interface IUnitConnection : global::Unity.VisualScripting.IConnection<global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitInputPort>, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		new global::Unity.VisualScripting.FlowGraph graph { get; }
	}
}
