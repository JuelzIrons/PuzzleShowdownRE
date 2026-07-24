namespace Unity.VisualScripting
{
	public interface IGraphParent
	{
		global::Unity.VisualScripting.IGraph childGraph { get; }

		bool isSerializationRoot { get; }

		global::UnityEngine.Object serializedObject { get; }

		global::Unity.VisualScripting.IGraph DefaultGraph();
	}
}
