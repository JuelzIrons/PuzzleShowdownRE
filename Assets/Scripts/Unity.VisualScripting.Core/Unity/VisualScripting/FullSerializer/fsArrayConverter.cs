namespace Unity.VisualScripting.FullSerializer
{
	public class fsArrayConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			return type.IsArray;
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
			global::System.Collections.IList list = (global::System.Array)instance;
			global::System.Type elementType = storageType.GetElementType();
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			serialized = global::Unity.VisualScripting.FullSerializer.fsData.CreateList(list.Count);
			global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> asList = serialized.AsList;
			for (int i = 0; i < list.Count; i++)
			{
				object instance2 = list[i];
				global::Unity.VisualScripting.FullSerializer.fsData data;
				global::Unity.VisualScripting.FullSerializer.fsResult result = Serializer.TrySerialize(elementType, instance2, out data);
				success.AddMessages(result);
				if (!result.Failed)
				{
					asList.Add(data);
				}
			}
			return success;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += CheckType(data, global::Unity.VisualScripting.FullSerializer.fsDataType.Array));
			if (fsResult2.Failed)
			{
				return success;
			}
			global::System.Type elementType = storageType.GetElementType();
			global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> asList = data.AsList;
			global::System.Collections.ArrayList arrayList = new global::System.Collections.ArrayList(asList.Count);
			int count = arrayList.Count;
			for (int i = 0; i < asList.Count; i++)
			{
				global::Unity.VisualScripting.FullSerializer.fsData data2 = asList[i];
				object result = null;
				if (i < count)
				{
					result = arrayList[i];
				}
				global::Unity.VisualScripting.FullSerializer.fsResult result2 = Serializer.TryDeserialize(data2, elementType, ref result);
				success.AddMessages(result2);
				if (!result2.Failed)
				{
					if (i < count)
					{
						arrayList[i] = result;
					}
					else
					{
						arrayList.Add(result);
					}
				}
			}
			instance = arrayList.ToArray(elementType);
			return success;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return global::Unity.VisualScripting.FullSerializer.fsMetaType.Get(Serializer.Config, storageType).CreateInstance();
		}
	}
}
