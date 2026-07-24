namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.IncludeInSettings(false)]
	public sealed class VariablesAsset : global::Unity.VisualScripting.LudiqScriptableObject
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.InspectorWide(true)]
		public global::Unity.VisualScripting.VariableDeclarations declarations { get; internal set; } = new global::Unity.VisualScripting.VariableDeclarations();

		[global::UnityEngine.ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}
	}
}
