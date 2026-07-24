namespace Unity.VisualScripting.FullSerializer
{
	public abstract class fsBaseConverter
	{
		public global::Unity.VisualScripting.FullSerializer.fsSerializer Serializer;

		public virtual object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			if (RequestCycleSupport(storageType))
			{
				throw new global::System.InvalidOperationException("Please override CreateInstance for " + GetType()?.ToString() + "; the object graph for " + storageType?.ToString() + " can contain potentially contain cycles, so separated instance creation is needed");
			}
			return storageType;
		}

		public virtual bool RequestCycleSupport(global::System.Type storageType)
		{
			if (storageType == typeof(string))
			{
				return false;
			}
			if (!global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(storageType).IsClass)
			{
				return global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(storageType).IsInterface;
			}
			return true;
		}

		public virtual bool RequestInheritanceSupport(global::System.Type storageType)
		{
			return !global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(storageType).IsSealed;
		}

		public abstract global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType);

		public abstract global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType);

		protected global::Unity.VisualScripting.FullSerializer.fsResult FailExpectedType(global::Unity.VisualScripting.FullSerializer.fsData data, params global::Unity.VisualScripting.FullSerializer.fsDataType[] types)
		{
			return global::Unity.VisualScripting.FullSerializer.fsResult.Fail(GetType().Name + " expected one of " + string.Join(", ", global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(types, (global::Unity.VisualScripting.FullSerializer.fsDataType t) => t.ToString()))) + " but got " + data.Type.ToString() + " in " + data);
		}

		protected global::Unity.VisualScripting.FullSerializer.fsResult CheckType(global::Unity.VisualScripting.FullSerializer.fsData data, global::Unity.VisualScripting.FullSerializer.fsDataType type)
		{
			if (data.Type != type)
			{
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail(GetType().Name + " expected " + type.ToString() + " but got " + data.Type.ToString() + " in " + data);
			}
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		protected global::Unity.VisualScripting.FullSerializer.fsResult CheckKey(global::Unity.VisualScripting.FullSerializer.fsData data, string key, out global::Unity.VisualScripting.FullSerializer.fsData subitem)
		{
			return CheckKey(data.AsDictionary, key, out subitem);
		}

		protected global::Unity.VisualScripting.FullSerializer.fsResult CheckKey(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, string key, out global::Unity.VisualScripting.FullSerializer.fsData subitem)
		{
			if (!data.TryGetValue(key, out subitem))
			{
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail(GetType().Name + " requires a <" + key + "> key in the data " + data);
			}
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		protected global::Unity.VisualScripting.FullSerializer.fsResult SerializeMember<T>(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, global::System.Type overrideConverterType, string name, T value)
		{
			global::Unity.VisualScripting.FullSerializer.fsData data2;
			global::Unity.VisualScripting.FullSerializer.fsResult result = Serializer.TrySerialize(typeof(T), overrideConverterType, value, out data2);
			if (result.Succeeded)
			{
				data[name] = data2;
			}
			return result;
		}

		protected global::Unity.VisualScripting.FullSerializer.fsResult DeserializeMember<T>(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, global::System.Type overrideConverterType, string name, out T value)
		{
			if (!data.TryGetValue(name, out var value2))
			{
				value = default(T);
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Unable to find member \"" + name + "\"");
			}
			object result = null;
			global::Unity.VisualScripting.FullSerializer.fsResult result2 = Serializer.TryDeserialize(value2, typeof(T), overrideConverterType, ref result);
			value = (T)result;
			return result2;
		}
	}
}
