namespace Unity.VisualScripting
{
	public interface IGraphDebugData
	{
		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IGraphElementDebugData> elementsData { get; }

		global::Unity.VisualScripting.IGraphElementDebugData GetOrCreateElementData(global::Unity.VisualScripting.IGraphElementWithDebugData element);

		global::Unity.VisualScripting.IGraphDebugData GetOrCreateChildGraphData(global::Unity.VisualScripting.IGraphParentElement element);
	}
}
