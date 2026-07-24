namespace Unity.VisualScripting
{
	public class UnitCategoryConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter
	{
		public override global::System.Type ModelType => typeof(global::Unity.VisualScripting.UnitCategory);

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return new object();
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			serialized = new global::Unity.VisualScripting.FullSerializer.fsData(((global::Unity.VisualScripting.UnitCategory)instance).fullName);
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			if (!data.IsString)
			{
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("Expected string in " + data);
			}
			instance = new global::Unity.VisualScripting.UnitCategory(data.AsString);
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}
	}
}
