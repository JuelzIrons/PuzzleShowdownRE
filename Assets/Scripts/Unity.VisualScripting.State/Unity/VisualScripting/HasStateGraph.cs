namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.StateGraph))]
	[global::Unity.VisualScripting.UnitCategory("Graphs/Graph Nodes")]
	public sealed class HasStateGraph : global::Unity.VisualScripting.HasGraph<global::Unity.VisualScripting.StateGraph, global::Unity.VisualScripting.StateGraphAsset, global::Unity.VisualScripting.StateMachine>
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable]
		[global::JetBrains.Annotations.UsedImplicitly]
		public global::Unity.VisualScripting.StateGraphContainerType containerType { get; set; }

		protected override bool isGameObject => containerType == global::Unity.VisualScripting.StateGraphContainerType.GameObject;
	}
}
