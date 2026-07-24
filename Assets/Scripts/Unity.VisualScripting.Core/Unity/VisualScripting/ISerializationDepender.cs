namespace Unity.VisualScripting
{
	public interface ISerializationDepender : global::UnityEngine.ISerializationCallbackReceiver
	{
		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ISerializationDependency> deserializationDependencies { get; }

		void OnAfterDependenciesDeserialized();
	}
}
