namespace Unity.VisualScripting.FullSerializer
{
	public class fsSerializationCallbackProcessor : global::Unity.VisualScripting.FullSerializer.fsObjectProcessor
	{
		public override bool CanProcess(global::System.Type type)
		{
			return typeof(global::Unity.VisualScripting.FullSerializer.fsISerializationCallbacks).IsAssignableFrom(type);
		}

		public override void OnBeforeSerialize(global::System.Type storageType, object instance)
		{
			if (instance != null)
			{
				((global::Unity.VisualScripting.FullSerializer.fsISerializationCallbacks)instance).OnBeforeSerialize(storageType);
			}
		}

		public override void OnAfterSerialize(global::System.Type storageType, object instance, ref global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (instance != null)
			{
				((global::Unity.VisualScripting.FullSerializer.fsISerializationCallbacks)instance).OnAfterSerialize(storageType, ref data);
			}
		}

		public override void OnBeforeDeserializeAfterInstanceCreation(global::System.Type storageType, object instance, ref global::Unity.VisualScripting.FullSerializer.fsData data)
		{
			if (!(instance is global::Unity.VisualScripting.FullSerializer.fsISerializationCallbacks))
			{
				throw new global::System.InvalidCastException("Please ensure the converter for " + storageType?.ToString() + " actually returns an instance of it, not an instance of " + instance.GetType());
			}
			((global::Unity.VisualScripting.FullSerializer.fsISerializationCallbacks)instance).OnBeforeDeserialize(storageType, ref data);
		}

		public override void OnAfterDeserialize(global::System.Type storageType, object instance)
		{
			if (instance != null)
			{
				((global::Unity.VisualScripting.FullSerializer.fsISerializationCallbacks)instance).OnAfterDeserialize(storageType);
			}
		}
	}
}
