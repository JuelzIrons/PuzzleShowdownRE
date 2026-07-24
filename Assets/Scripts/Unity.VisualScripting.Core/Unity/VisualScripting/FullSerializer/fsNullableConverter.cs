namespace Unity.VisualScripting.FullSerializer
{
	public class fsNullableConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(type).IsGenericType)
			{
				return type.GetGenericTypeDefinition() == typeof(global::System.Nullable<>);
			}
			return false;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			return Serializer.TrySerialize(global::System.Nullable.GetUnderlyingType(storageType), instance, out serialized);
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			return Serializer.TryDeserialize(data, global::System.Nullable.GetUnderlyingType(storageType), ref instance);
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return storageType;
		}
	}
}
