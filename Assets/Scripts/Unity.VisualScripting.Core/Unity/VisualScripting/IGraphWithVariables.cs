namespace Unity.VisualScripting
{
	public interface IGraphWithVariables : global::Unity.VisualScripting.IGraph, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.ISerializationDepender, global::UnityEngine.ISerializationCallbackReceiver
	{
		global::Unity.VisualScripting.VariableDeclarations variables { get; }

		global::System.Collections.Generic.IEnumerable<string> GetDynamicVariableNames(global::Unity.VisualScripting.VariableKind kind, global::Unity.VisualScripting.GraphReference reference);
	}
}
