namespace Unity.VisualScripting
{
	public class NamespaceConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter
	{
		public override global::System.Type ModelType => typeof(global::Unity.VisualScripting.Namespace);

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return new object();
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			serialized = new global::Unity.VisualScripting.FullSerializer.fsData(((global::Unity.VisualScripting.Namespace)instance).FullName);
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			if (!data.IsString)
			{
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Expected string in " + data);
			}
			instance = global::Unity.VisualScripting.Namespace.FromFullName(data.AsString);
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}
	}
}
