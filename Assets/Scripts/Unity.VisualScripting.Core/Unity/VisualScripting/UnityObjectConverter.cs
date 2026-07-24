namespace Unity.VisualScripting
{
	public class UnityObjectConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		private global::System.Collections.Generic.List<global::UnityEngine.Object> objectReferences => Serializer.Context.Get<global::System.Collections.Generic.List<global::UnityEngine.Object>>();

		public override bool CanProcess(global::System.Type type)
		{
			return typeof(global::UnityEngine.Object).IsAssignableFrom(type);
		}

		public override bool RequestCycleSupport(global::System.Type storageType)
		{
			return false;
		}

		public override bool RequestInheritanceSupport(global::System.Type storageType)
		{
			return false;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			global::UnityEngine.Object item = (global::UnityEngine.Object)instance;
			int count = objectReferences.Count;
			serialized = new global::Unity.VisualScripting.FullSerializer.fsData(count);
			objectReferences.Add(item);
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData storage, ref object instance, global::System.Type storageType)
		{
			int num = (int)storage.AsInt64;
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			if (num >= 0 && num < objectReferences.Count)
			{
				global::UnityEngine.Object obj = (global::UnityEngine.Object)(instance = objectReferences[num]);
				if (instance != null && !storageType.IsInstanceOfType(instance))
				{
					if (obj.GetHashCode() != 0)
					{
						success.AddMessage($"Object reference at index #{num} does not match target type ({instance.GetType()} != {storageType}). Defaulting to null.");
					}
					instance = null;
				}
			}
			else
			{
				success.AddMessage($"No object reference provided at index #{num}. Defaulting to null.");
				instance = null;
			}
			return success;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return storageType;
		}
	}
}
