namespace Unity.VisualScripting.FullSerializer
{
	public class fsIEnumerableConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			if (!typeof(global::System.Collections.IEnumerable).IsAssignableFrom(type))
			{
				return false;
			}
			return GetAddMethod(type) != null;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return global::Unity.VisualScripting.FullSerializer.fsMetaType.Get(Serializer.Config, storageType).CreateInstance();
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance_, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			global::System.Collections.IEnumerable enumerable = (global::System.Collections.IEnumerable)instance_;
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::System.Type elementType = GetElementType(storageType);
			serialized = global::Unity.VisualScripting.FullSerializer.fsData.CreateList(HintSize(enumerable));
			global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> asList = serialized.AsList;
			foreach (object item in enumerable)
			{
				global::Unity.VisualScripting.FullSerializer.fsData data;
				global::Unity.VisualScripting.FullSerializer.fsResult result = Serializer.TrySerialize(elementType, item, out data);
				success.AddMessages(result);
				if (!result.Failed)
				{
					asList.Add(data);
				}
			}
			if (IsStack(enumerable.GetType()))
			{
				asList.Reverse();
			}
			return success;
		}

		private bool IsStack(global::System.Type type)
		{
			if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(type).IsGenericType)
			{
				return global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(type).GetGenericTypeDefinition() == typeof(global::System.Collections.Generic.Stack<>);
			}
			return false;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance_, global::System.Type storageType)
		{
			global::System.Collections.IEnumerable enumerable = (global::System.Collections.IEnumerable)instance_;
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += CheckType(data, global::Unity.VisualScripting.FullSerializer.fsDataType.Array));
			if (fsResult2.Failed)
			{
				return success;
			}
			global::System.Type elementType = GetElementType(storageType);
			global::System.Reflection.MethodInfo addMethod = GetAddMethod(storageType);
			TryClear(storageType, enumerable);
			global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsData> asList = data.AsList;
			for (int i = 0; i < asList.Count; i++)
			{
				global::Unity.VisualScripting.FullSerializer.fsData data2 = asList[i];
				object result = null;
				global::Unity.VisualScripting.FullSerializer.fsResult result2 = Serializer.TryDeserialize(data2, elementType, ref result);
				success.AddMessages(result2);
				if (result2.Succeeded)
				{
					addMethod.Invoke(enumerable, new object[1] { result });
				}
			}
			return success;
		}

		private static int HintSize(global::System.Collections.IEnumerable collection)
		{
			if (collection is global::System.Collections.ICollection)
			{
				return ((global::System.Collections.ICollection)collection).Count;
			}
			return 0;
		}

		private static global::System.Type GetElementType(global::System.Type objectType)
		{
			if (objectType.HasElementType)
			{
				return objectType.GetElementType();
			}
			global::System.Type type = global::Unity.VisualScripting.FullSerializer.fsReflectionUtility.GetInterface(objectType, typeof(global::System.Collections.Generic.IEnumerable<>));
			if (type != null)
			{
				return type.GetGenericArguments()[0];
			}
			return typeof(object);
		}

		private static void TryClear(global::System.Type type, object instance)
		{
			global::System.Reflection.MethodInfo flattenedMethod = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetFlattenedMethod(type, "Clear");
			if (flattenedMethod != null)
			{
				flattenedMethod.Invoke(instance, null);
			}
		}

		private static int TryGetExistingSize(global::System.Type type, object instance)
		{
			global::System.Reflection.PropertyInfo flattenedProperty = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetFlattenedProperty(type, "Count");
			if (flattenedProperty != null)
			{
				return (int)flattenedProperty.GetGetMethod().Invoke(instance, null);
			}
			return 0;
		}

		private static global::System.Reflection.MethodInfo GetAddMethod(global::System.Type type)
		{
			global::System.Type type2 = global::Unity.VisualScripting.FullSerializer.fsReflectionUtility.GetInterface(type, typeof(global::System.Collections.Generic.ICollection<>));
			if (type2 != null)
			{
				global::System.Reflection.MethodInfo declaredMethod = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetDeclaredMethod(type2, "Add");
				if (declaredMethod != null)
				{
					return declaredMethod;
				}
			}
			return global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetFlattenedMethod(type, "Add") ?? global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetFlattenedMethod(type, "Push") ?? global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetFlattenedMethod(type, "Enqueue");
		}
	}
}
