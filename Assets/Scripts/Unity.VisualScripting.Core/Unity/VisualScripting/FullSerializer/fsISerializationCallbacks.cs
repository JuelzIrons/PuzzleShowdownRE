namespace Unity.VisualScripting.FullSerializer
{
	public interface fsISerializationCallbacks
	{
		void OnBeforeSerialize(global::System.Type storageType);

		void OnAfterSerialize(global::System.Type storageType, ref global::Unity.VisualScripting.FullSerializer.fsData data);

		void OnBeforeDeserialize(global::System.Type storageType, ref global::Unity.VisualScripting.FullSerializer.fsData data);

		void OnAfterDeserialize(global::System.Type storageType);
	}
}
