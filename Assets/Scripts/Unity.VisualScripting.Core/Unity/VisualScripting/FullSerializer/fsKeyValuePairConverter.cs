namespace Unity.VisualScripting.FullSerializer
{
	public class fsKeyValuePairConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(type).IsGenericType)
			{
				return type.GetGenericTypeDefinition() == typeof(global::System.Collections.Generic.KeyValuePair<, >);
			}
			return false;
		}

		public override bool RequestCycleSupport(global::System.Type storageType)
		{
			return false;
		}

		public override bool RequestInheritanceSupport(global::System.Type storageType)
		{
			return false;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::Unity.VisualScripting.FullSerializer.fsData subitem;
			global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += CheckKey(data, "Key", out subitem));
			if (fsResult2.Failed)
			{
				return success;
			}
			if ((success += CheckKey(data, "Value", out var subitem2)).Failed)
			{
				return success;
			}
			global::System.Type[] genericArguments = storageType.GetGenericArguments();
			global::System.Type storageType2 = genericArguments[0];
			global::System.Type storageType3 = genericArguments[1];
			object result = null;
			object result2 = null;
			success.AddMessages(Serializer.TryDeserialize(subitem, storageType2, ref result));
			success.AddMessages(Serializer.TryDeserialize(subitem2, storageType3, ref result2));
			instance = global::System.Activator.CreateInstance(storageType, result, result2);
			return success;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			global::System.Reflection.PropertyInfo declaredProperty = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetDeclaredProperty(storageType, "Key");
			global::System.Reflection.PropertyInfo declaredProperty2 = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetDeclaredProperty(storageType, "Value");
			object value = declaredProperty.GetValue(instance, null);
			object value2 = declaredProperty2.GetValue(instance, null);
			global::System.Type[] genericArguments = storageType.GetGenericArguments();
			global::System.Type storageType2 = genericArguments[0];
			global::System.Type storageType3 = genericArguments[1];
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			success.AddMessages(Serializer.TrySerialize(storageType2, value, out var data));
			success.AddMessages(Serializer.TrySerialize(storageType3, value2, out var data2));
			serialized = global::Unity.VisualScripting.FullSerializer.fsData.CreateDictionary();
			if (data != null)
			{
				serialized.AsDictionary["Key"] = data;
			}
			if (data2 != null)
			{
				serialized.AsDictionary["Value"] = data2;
			}
			return success;
		}
	}
}
