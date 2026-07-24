namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.FlowGraph))]
	[global::Unity.VisualScripting.UnitCategory("Graphs/Graph Nodes")]
	public sealed class HasScriptGraph : global::Unity.VisualScripting.HasGraph<global::Unity.VisualScripting.FlowGraph, global::Unity.VisualScripting.ScriptGraphAsset, global::Unity.VisualScripting.ScriptMachine>
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable]
		[global::JetBrains.Annotations.UsedImplicitly]
		public global::Unity.VisualScripting.ScriptGraphContainerType containerType { get; set; }

		protected override bool isGameObject => containerType == global::Unity.VisualScripting.ScriptGraphContainerType.GameObject;
	}
}
