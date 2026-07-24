namespace Unity.VisualScripting
{
	public sealed class FlowGraphData : global::Unity.VisualScripting.GraphData<global::Unity.VisualScripting.FlowGraph>, global::Unity.VisualScripting.IGraphDataWithVariables, global::Unity.VisualScripting.IGraphData, global::Unity.VisualScripting.IGraphEventListenerData
	{
		public global::Unity.VisualScripting.VariableDeclarations variables { get; }

		public bool isListening { get; set; }

		public FlowGraphData(global::Unity.VisualScripting.FlowGraph definition)
			: base(definition)
		{
			variables = definition.variables.CloneViaFakeSerialization();
		}
	}
}
