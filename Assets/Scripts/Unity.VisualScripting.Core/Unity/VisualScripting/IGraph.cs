namespace Unity.VisualScripting
{
	public interface IGraph : global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.ISerializationDepender, global::UnityEngine.ISerializationCallbackReceiver
	{
		global::UnityEngine.Vector2 pan { get; set; }

		float zoom { get; set; }

		global::Unity.VisualScripting.MergedGraphElementCollection elements { get; }

		string title { get; }

		string summary { get; }

		global::Unity.VisualScripting.IGraphData CreateData();

		global::Unity.VisualScripting.IGraphDebugData CreateDebugData();

		void Instantiate(global::Unity.VisualScripting.GraphReference instance);

		void Uninstantiate(global::Unity.VisualScripting.GraphReference instance);
	}
}
