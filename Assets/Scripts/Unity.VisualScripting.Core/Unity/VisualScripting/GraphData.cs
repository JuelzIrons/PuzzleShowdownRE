namespace Unity.VisualScripting
{
	public class GraphData<TGraph> : global::Unity.VisualScripting.IGraphData where TGraph : class, global::Unity.VisualScripting.IGraph
	{
		protected TGraph definition { get; }

		protected global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraphElementWithData, global::Unity.VisualScripting.IGraphElementData> elementsData { get; } = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraphElementWithData, global::Unity.VisualScripting.IGraphElementData>();

		protected global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraphParentElement, global::Unity.VisualScripting.IGraphData> childrenGraphsData { get; } = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.IGraphParentElement, global::Unity.VisualScripting.IGraphData>();

		protected global::System.Collections.Generic.Dictionary<global::System.Guid, global::Unity.VisualScripting.IGraphElementData> phantomElementsData { get; } = new global::System.Collections.Generic.Dictionary<global::System.Guid, global::Unity.VisualScripting.IGraphElementData>();

		protected global::System.Collections.Generic.Dictionary<global::System.Guid, global::Unity.VisualScripting.IGraphData> phantomChildrenGraphsData { get; } = new global::System.Collections.Generic.Dictionary<global::System.Guid, global::Unity.VisualScripting.IGraphData>();

		public GraphData(TGraph definition)
		{
			this.definition = definition;
		}

		public bool TryGetElementData(global::Unity.VisualScripting.IGraphElementWithData element, out global::Unity.VisualScripting.IGraphElementData data)
		{
			return elementsData.TryGetValue(element, out data);
		}

		public bool TryGetChildGraphData(global::Unity.VisualScripting.IGraphParentElement element, out global::Unity.VisualScripting.IGraphData data)
		{
			return childrenGraphsData.TryGetValue(element, out data);
		}

		public global::Unity.VisualScripting.IGraphElementData CreateElementData(global::Unity.VisualScripting.IGraphElementWithData element)
		{
			if (elementsData.ContainsKey(element))
			{
				throw new global::System.InvalidOperationException($"Graph data already contains element data for {element}.");
			}
			if (phantomElementsData.TryGetValue(element.guid, out var value))
			{
				phantomElementsData.Remove(element.guid);
			}
			else
			{
				value = element.CreateData();
			}
			elementsData.Add(element, value);
			return value;
		}

		public void FreeElementData(global::Unity.VisualScripting.IGraphElementWithData element)
		{
			if (elementsData.TryGetValue(element, out var value))
			{
				elementsData.Remove(element);
				phantomElementsData.Add(element.guid, value);
			}
			else
			{
				global::UnityEngine.Debug.LogWarning($"Graph data does not contain element data to free for {element}.");
			}
		}

		public global::Unity.VisualScripting.IGraphData CreateChildGraphData(global::Unity.VisualScripting.IGraphParentElement element)
		{
			if (childrenGraphsData.ContainsKey(element))
			{
				throw new global::System.InvalidOperationException($"Graph data already contains child graph data for {element}.");
			}
			if (phantomChildrenGraphsData.TryGetValue(element.guid, out var value))
			{
				phantomChildrenGraphsData.Remove(element.guid);
			}
			else
			{
				value = element.childGraph.CreateData();
			}
			childrenGraphsData.Add(element, value);
			return value;
		}

		public void FreeChildGraphData(global::Unity.VisualScripting.IGraphParentElement element)
		{
			if (childrenGraphsData.TryGetValue(element, out var value))
			{
				childrenGraphsData.Remove(element);
				phantomChildrenGraphsData.Add(element.guid, value);
			}
			else
			{
				global::UnityEngine.Debug.LogWarning($"Graph data does not contain child graph data to free for {element}.");
			}
		}
	}
}
