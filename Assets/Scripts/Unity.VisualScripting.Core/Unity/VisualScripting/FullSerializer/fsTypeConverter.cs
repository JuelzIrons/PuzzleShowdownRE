namespace Unity.VisualScripting.FullSerializer
{
	public class fsTypeConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			return typeof(global::System.Type).IsAssignableFrom(type);
		}

		public override bool RequestCycleSupport(global::System.Type type)
		{
			return false;
		}

		public override bool RequestInheritanceSupport(global::System.Type type)
		{
			return false;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			global::System.Type type = (global::System.Type)instance;
			serialized = new global::Unity.VisualScripting.FullSerializer.fsData(global::Unity.VisualScripting.RuntimeCodebase.SerializeType(type));
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			if (!data.IsString)
			{
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Type converter requires a string");
			}
			if (global::Unity.VisualScripting.RuntimeCodebase.TryDeserializeType(data.AsString, out var type))
			{
				instance = type;
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Unable to find type: '" + (data.AsString ?? "(null)") + "'.");
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return storageType;
		}
	}
}
