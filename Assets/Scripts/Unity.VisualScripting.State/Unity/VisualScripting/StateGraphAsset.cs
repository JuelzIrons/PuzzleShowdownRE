namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.StateGraph))]
	[global::UnityEngine.CreateAssetMenu(menuName = "Visual Scripting/State Graph", fileName = "New State Graph", order = 81)]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.visualscripting@latest/index.html?subfolder=/manual/vs-state-graphs-intro.html")]
	public sealed class StateGraphAsset : global::Unity.VisualScripting.Macro<global::Unity.VisualScripting.StateGraph>
	{
		[global::UnityEngine.ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}

		public override global::Unity.VisualScripting.StateGraph DefaultGraph()
		{
			return global::Unity.VisualScripting.StateGraph.WithStart();
		}
	}
}
