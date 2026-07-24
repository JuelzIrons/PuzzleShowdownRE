namespace Unity.VisualScripting
{
	public interface IMachine : global::Unity.VisualScripting.IGraphRoot, global::Unity.VisualScripting.IGraphParent, global::Unity.VisualScripting.IGraphNester, global::Unity.VisualScripting.IAotStubbable
	{
		global::Unity.VisualScripting.IGraphData graphData { get; set; }

		global::UnityEngine.GameObject threadSafeGameObject { get; }
	}
}
