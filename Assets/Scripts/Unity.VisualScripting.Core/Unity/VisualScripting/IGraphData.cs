namespace Unity.VisualScripting
{
	public interface IGraphData
	{
		bool TryGetElementData(global::Unity.VisualScripting.IGraphElementWithData element, out global::Unity.VisualScripting.IGraphElementData data);

		bool TryGetChildGraphData(global::Unity.VisualScripting.IGraphParentElement element, out global::Unity.VisualScripting.IGraphData data);

		global::Unity.VisualScripting.IGraphElementData CreateElementData(global::Unity.VisualScripting.IGraphElementWithData element);

		void FreeElementData(global::Unity.VisualScripting.IGraphElementWithData element);

		global::Unity.VisualScripting.IGraphData CreateChildGraphData(global::Unity.VisualScripting.IGraphParentElement element);

		void FreeChildGraphData(global::Unity.VisualScripting.IGraphParentElement element);
	}
}
