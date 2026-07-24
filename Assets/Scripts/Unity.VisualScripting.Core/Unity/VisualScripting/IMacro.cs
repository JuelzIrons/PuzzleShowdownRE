namespace Unity.VisualScripting
{
	public interface IMacro : global::Unity.VisualScripting.IGraphRoot, global::Unity.VisualScripting.IGraphParent, global::Unity.VisualScripting.ISerializationDependency, global::UnityEngine.ISerializationCallbackReceiver, global::Unity.VisualScripting.IAotStubbable
	{
		global::Unity.VisualScripting.IGraph graph { get; set; }
	}
}
