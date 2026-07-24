namespace Unity.VisualScripting.FullSerializer
{
	public abstract class fsObjectProcessor
	{
		public virtual bool CanProcess(global::System.Type type)
		{
			throw new global::System.NotImplementedException();
		}

		public virtual void OnBeforeSerialize(global::System.Type storageType, object instance)
		{
		}

		public virtual void OnAfterSerialize(global::System.Type storageType, object instance, ref global::Unity.VisualScripting.FullSerializer.fsData data)
		{
		}

		public virtual void OnBeforeDeserialize(global::System.Type storageType, ref global::Unity.VisualScripting.FullSerializer.fsData data)
		{
		}

		public virtual void OnBeforeDeserializeAfterInstanceCreation(global::System.Type storageType, object instance, ref global::Unity.VisualScripting.FullSerializer.fsData data)
		{
		}

		public virtual void OnAfterDeserialize(global::System.Type storageType, object instance)
		{
		}
	}
}
