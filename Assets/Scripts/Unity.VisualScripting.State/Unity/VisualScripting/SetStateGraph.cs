namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.StateGraph))]
	public class SetStateGraph : global::Unity.VisualScripting.SetGraph<global::Unity.VisualScripting.StateGraph, global::Unity.VisualScripting.StateGraphAsset, global::Unity.VisualScripting.StateMachine>
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable]
		[global::JetBrains.Annotations.UsedImplicitly]
		public global::Unity.VisualScripting.StateGraphContainerType containerType { get; set; }

		protected override bool isGameObject => containerType == global::Unity.VisualScripting.StateGraphContainerType.GameObject;
	}
}
