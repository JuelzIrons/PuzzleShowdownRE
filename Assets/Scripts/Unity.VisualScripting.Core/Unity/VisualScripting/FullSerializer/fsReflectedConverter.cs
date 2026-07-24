namespace Unity.VisualScripting.FullSerializer
{
	public class fsReflectedConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(type).IsArray || typeof(global::System.Collections.ICollection).IsAssignableFrom(type))
			{
				return false;
			}
			return true;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			serialized = global::Unity.VisualScripting.FullSerializer.fsData.CreateDictionary();
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::Unity.VisualScripting.FullSerializer.fsMetaType fsMetaType2 = global::Unity.VisualScripting.FullSerializer.fsMetaType.Get(Serializer.Config, instance.GetType());
			fsMetaType2.EmitAotData();
			for (int i = 0; i < fsMetaType2.Properties.Length; i++)
			{
				global::Unity.VisualScripting.FullSerializer.fsMetaProperty fsMetaProperty2 = fsMetaType2.Properties[i];
				if (fsMetaProperty2.CanRead)
				{
					global::Unity.VisualScripting.FullSerializer.fsData data;
					global::Unity.VisualScripting.FullSerializer.fsResult result = Serializer.TrySerialize(fsMetaProperty2.StorageType, fsMetaProperty2.OverrideConverterType, fsMetaProperty2.Read(instance), out data);
					success.AddMessages(result);
					if (!result.Failed)
					{
						serialized.AsDictionary[fsMetaProperty2.JsonName] = data;
					}
				}
			}
			return success;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += CheckType(data, global::Unity.VisualScripting.FullSerializer.fsDataType.Object));
			if (fsResult2.Failed)
			{
				return success;
			}
			global::Unity.VisualScripting.FullSerializer.fsMetaType fsMetaType2 = global::Unity.VisualScripting.FullSerializer.fsMetaType.Get(Serializer.Config, storageType);
			fsMetaType2.EmitAotData();
			for (int i = 0; i < fsMetaType2.Properties.Length; i++)
			{
				global::Unity.VisualScripting.FullSerializer.fsMetaProperty fsMetaProperty2 = fsMetaType2.Properties[i];
				if (fsMetaProperty2.CanWrite && data.AsDictionary.TryGetValue(fsMetaProperty2.JsonName, out var value))
				{
					object result = null;
					if (fsMetaProperty2.CanRead)
					{
						result = fsMetaProperty2.Read(instance);
					}
					global::Unity.VisualScripting.FullSerializer.fsResult result2 = Serializer.TryDeserialize(value, fsMetaProperty2.StorageType, fsMetaProperty2.OverrideConverterType, ref result);
					success.AddMessages(result2);
					if (!result2.Failed)
					{
						fsMetaProperty2.Write(instance, result);
					}
				}
			}
			return success;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return global::Unity.VisualScripting.FullSerializer.fsMetaType.Get(Serializer.Config, storageType).CreateInstance();
		}
	}
}
