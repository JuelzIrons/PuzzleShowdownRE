namespace Unity.VisualScripting
{
	public interface ISerializationDependency : global::UnityEngine.ISerializationCallbackReceiver
	{
		internal bool IsDeserialized { get; set; }
	}
}
