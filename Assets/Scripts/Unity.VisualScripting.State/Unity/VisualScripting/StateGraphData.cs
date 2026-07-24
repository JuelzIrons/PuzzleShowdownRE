namespace Unity.VisualScripting
{
	public sealed class StateGraphData : global::Unity.VisualScripting.GraphData<global::Unity.VisualScripting.StateGraph>, global::Unity.VisualScripting.IGraphEventListenerData, global::Unity.VisualScripting.IGraphData
	{
		public bool isListening { get; set; }

		public StateGraphData(global::Unity.VisualScripting.StateGraph definition)
			: base(definition)
		{
		}
	}
}
