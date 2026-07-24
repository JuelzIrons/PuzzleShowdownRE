namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.FlowGraph))]
	[global::UnityEngine.CreateAssetMenu(menuName = "Visual Scripting/Script Graph", fileName = "New Script Graph", order = 81)]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.visualscripting@latest/index.html?subfolder=/manual/vs-script-graphs-intro.html")]
	public sealed class ScriptGraphAsset : global::Unity.VisualScripting.Macro<global::Unity.VisualScripting.FlowGraph>
	{
		[global::UnityEngine.ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}

		public override global::Unity.VisualScripting.FlowGraph DefaultGraph()
		{
			return global::Unity.VisualScripting.FlowGraph.WithInputOutput();
		}
	}
}
