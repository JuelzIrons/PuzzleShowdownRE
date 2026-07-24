namespace Unity.VisualScripting.FullSerializer
{
	public class fsGuidConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		public override bool CanProcess(global::System.Type type)
		{
			return type == typeof(global::System.Guid);
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
			serialized = new global::Unity.VisualScripting.FullSerializer.fsData(((global::System.Guid)instance/*cast due to .constrained prefix*/).ToString());
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			if (data.IsString)
			{
				instance = new global::System.Guid(data.AsString);
				return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			}
			return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("fsGuidConverter encountered an unknown JSON data type");
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return default(global::System.Guid);
		}
	}
}
