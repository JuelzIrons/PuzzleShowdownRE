namespace Unity.VisualScripting
{
	public class GraphDebugData : global::Unity.VisualScripting.IGraphDebugData
	{
		protected global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElementDebugData> elementsData { get; } = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElementDebugData>();

		protected global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraphParentElement, global::Unity.VisualScripting.IGraphDebugData> childrenGraphsData { get; } = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraphParentElement, global::Unity.VisualScripting.IGraphDebugData>();

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IGraphElementDebugData> global::Unity.VisualScripting.IGraphDebugData.elementsData => elementsData.Values;

		public GraphDebugData(global::Unity.VisualScripting.IGraph definition)
		{
		}

		public global::Unity.VisualScripting.IGraphElementDebugData GetOrCreateElementData(global::Unity.VisualScripting.IGraphElementWithDebugData element)
		{
			if (!elementsData.TryGetValue(element, out var value))
			{
				value = element.CreateDebugData();
				elementsData.Add(element, value);
			}
			return value;
		}

		public global::Unity.VisualScripting.IGraphDebugData GetOrCreateChildGraphData(global::Unity.VisualScripting.IGraphParentElement element)
		{
			if (!childrenGraphsData.TryGetValue(element, out var value))
			{
				value = new global::Unity.VisualScripting.GraphDebugData(element.childGraph);
				childrenGraphsData.Add(element, value);
			}
			return value;
		}
	}
}
