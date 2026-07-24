namespace Unity.VisualScripting.FullSerializer
{
	public class fsDictionaryConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			return typeof(global::System.Collections.IDictionary).IsAssignableFrom(type);
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return global::Unity.VisualScripting.FullSerializer.fsMetaType.Get(Serializer.Config, storageType).CreateInstance();
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance_, global::System.Type storageType)
		{
			global::System.Collections.IDictionary dictionary = (global::System.Collections.IDictionary)instance_;
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			GetKeyValueTypes(dictionary.GetType(), out var keyStorageType, out var valueStorageType);
			global::Unity.VisualScripting.FullSerializer.fsResult result5;
			if (data.IsList)
			{
				global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> asList = data.AsList;
				for (int i = 0; i < asList.Count; i++)
				{
					global::Unity.VisualScripting.FullSerializer.fsData data2 = asList[i];
					if ((success += CheckType(data2, global::Unity.VisualScripting.FullSerializer.fsDataType.Object)).Failed)
					{
						return success;
					}
					if ((success += CheckKey(data2, "Key", out var subitem)).Failed)
					{
						return success;
					}
					if ((success += CheckKey(data2, "Value", out var subitem2)).Failed)
					{
						return success;
					}
					object result = null;
					object result2 = null;
					if ((success += Serializer.TryDeserialize(subitem, keyStorageType, ref result)).Failed)
					{
						return success;
					}
					if ((success += Serializer.TryDeserialize(subitem2, valueStorageType, ref result2)).Failed)
					{
						return success;
					}
					AddItemToDictionary(dictionary, result, result2);
				}
			}
			else
			{
				if (!data.IsDictionary)
				{
					return FailExpectedType(data, global::Unity.VisualScripting.FullSerializer.fsDataType.Array, global::Unity.VisualScripting.FullSerializer.fsDataType.Object);
				}
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.VisualScripting.FullSerializer.fsData> item in data.AsDictionary)
				{
					if (global::Unity.VisualScripting.FullSerializer.fsSerializer.IsReservedKeyword(item.Key))
					{
						continue;
					}
					global::Unity.VisualScripting.FullSerializer.fsData data3 = new global::Unity.VisualScripting.FullSerializer.fsData(item.Key);
					global::Unity.VisualScripting.FullSerializer.fsData value = item.Value;
					object result3 = null;
					object result4 = null;
					result5 = (success += Serializer.TryDeserialize(data3, keyStorageType, ref result3));
					if (result5.Failed)
					{
						result5 = success;
					}
					else
					{
						global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += Serializer.TryDeserialize(value, valueStorageType, ref result4));
						if (!fsResult2.Failed)
						{
							AddItemToDictionary(dictionary, result3, result4);
							continue;
						}
						result5 = success;
					}
					goto IL_01fb;
				}
			}
			return success;
			IL_01fb:
			return result5;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance_, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			serialized = global::Unity.VisualScripting.FullSerializer.fsData.Null;
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::System.Collections.IDictionary obj = (global::System.Collections.IDictionary)instance_;
			GetKeyValueTypes(obj.GetType(), out var keyStorageType, out var valueStorageType);
			global::System.Collections.IDictionaryEnumerator enumerator = obj.GetEnumerator();
			bool flag = true;
			global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> list = new global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData>(obj.Count);
			global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> list2 = new global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData>(obj.Count);
			while (enumerator.MoveNext())
			{
				if ((success += Serializer.TrySerialize(keyStorageType, enumerator.Key, out var data)).Failed)
				{
					return success;
				}
				if ((success += Serializer.TrySerialize(valueStorageType, enumerator.Value, out var data2)).Failed)
				{
					return success;
				}
				list.Add(data);
				list2.Add(data2);
				flag &= data.IsString;
			}
			if (flag)
			{
				serialized = global::Unity.VisualScripting.FullSerializer.fsData.CreateDictionary();
				global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> asDictionary = serialized.AsDictionary;
				for (int i = 0; i < list.Count; i++)
				{
					global::Unity.VisualScripting.FullSerializer.fsData fsData2 = list[i];
					global::Unity.VisualScripting.FullSerializer.fsData value = list2[i];
					asDictionary[fsData2.AsString] = value;
				}
			}
			else
			{
				serialized = global::Unity.VisualScripting.FullSerializer.fsData.CreateList(list.Count);
				global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> asList = serialized.AsList;
				for (int j = 0; j < list.Count; j++)
				{
					global::Unity.VisualScripting.FullSerializer.fsData value2 = list[j];
					global::Unity.VisualScripting.FullSerializer.fsData value3 = list2[j];
					global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> dictionary = new global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData>();
					dictionary["Key"] = value2;
					dictionary["Value"] = value3;
					asList.Add(new global::Unity.VisualScripting.FullSerializer.fsData(dictionary));
				}
			}
			return success;
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult AddItemToDictionary(global::System.Collections.IDictionary dictionary, object key, object value)
		{
			if (key == null || value == null)
			{
				global::System.Type type = global::Unity.VisualScripting.FullSerializer.fsReflectionUtility.GetInterface(dictionary.GetType(), typeof(global::System.Collections.Generic.ICollection<>));
				if (type == null)
				{
					return global::Unity.VisualScripting.FullSerializer.fsResult.Warn(dictionary.GetType()?.ToString() + " does not extend ICollection");
				}
				object obj = global::System.Activator.CreateInstance(type.GetGenericArguments()[0], key, value);
				global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetFlattenedMethod(type, "Add").Invoke(dictionary, new object[1] { obj });
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			dictionary[key] = value;
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		private static void GetKeyValueTypes(global::System.Type dictionaryType, out global::System.Type keyStorageType, out global::System.Type valueStorageType)
		{
			global::System.Type type = global::Unity.VisualScripting.FullSerializer.fsReflectionUtility.GetInterface(dictionaryType, typeof(global::System.Collections.Generic.IDictionary<, >));
			if (type != null)
			{
				global::System.Type[] genericArguments = type.GetGenericArguments();
				keyStorageType = genericArguments[0];
				valueStorageType = genericArguments[1];
			}
			else
			{
				keyStorageType = typeof(object);
				valueStorageType = typeof(object);
			}
		}
	}
}
