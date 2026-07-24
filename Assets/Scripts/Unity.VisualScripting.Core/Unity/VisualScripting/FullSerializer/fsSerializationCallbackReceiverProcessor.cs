namespace Unity.VisualScripting.FullSerializer
{
	public class fsSerializationCallbackReceiverProcessor : global::Unity.VisualScripting.FullSerializer.fsObjectProcessor
	{
		public override bool CanProcess(global::System.Type type)
		{
			return typeof(global::UnityEngine.ISerializationCallbackReceiver).IsAssignableFrom(type);
		}

		public override void OnBeforeSerialize(global::System.Type storageType, object instance)
		{
			if (instance != null && !(instance is global::UnityEngine.Object))
			{
				((global::UnityEngine.ISerializationCallbackReceiver)instance).OnBeforeSerialize();
			}
		}

		public override void OnAfterDeserialize(global::System.Type storageType, object instance)
		{
			if (instance != null && !(instance is global::UnityEngine.Object))
			{
				((global::UnityEngine.ISerializationCallbackReceiver)instance).OnAfterDeserialize();
			}
		}
	}
}
