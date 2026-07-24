namespace Unity.VisualScripting.FullSerializer
{
	public class fsForwardConverter : global::Unity.VisualScripting.FullSerializer.fsConverter
	{
		private string _memberName;

		public fsForwardConverter(global::Unity.VisualScripting.FullSerializer.fsForwardAttribute attribute)
		{
			_memberName = attribute.MemberName;
		}

		public override bool CanProcess(global::System.Type type)
		{
			throw new global::System.NotSupportedException("Please use the [fsForward(...)] attribute.");
		}

		private global::Unity.VisualScripting.FullSerializer.fsResult GetProperty(object instance, out global::Unity.VisualScripting.FullSerializer.fsMetaProperty property)
		{
			global::Unity.VisualScripting.FullSerializer.fsMetaProperty[] properties = global::Unity.VisualScripting.FullSerializer.fsMetaType.Get(Serializer.Config, instance.GetType()).Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i].MemberName == _memberName)
				{
					property = properties[i];
					return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
				}
			}
			property = null;
			return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("No property named \"" + _memberName + "\" on " + global::Unity.VisualScripting.FullSerializer.Internal.fsTypeExtensions.CSharpName(instance.GetType()));
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TrySerialize(object instance, out global::Unity.VisualScripting.FullSerializer.fsData serialized, global::System.Type storageType)
		{
			serialized = global::Unity.VisualScripting.FullSerializer.fsData.Null;
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::Unity.VisualScripting.FullSerializer.fsMetaProperty property;
			global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += GetProperty(instance, out property));
			if (fsResult2.Failed)
			{
				return success;
			}
			object instance2 = property.Read(instance);
			return Serializer.TrySerialize(property.StorageType, instance2, out serialized);
		}

		public override global::Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(global::Unity.VisualScripting.FullSerializer.fsData data, ref object instance, global::System.Type storageType)
		{
			global::Unity.VisualScripting.FullSerializer.fsResult success = global::Unity.VisualScripting.FullSerializer.fsResult.Success;
			global::Unity.VisualScripting.FullSerializer.fsMetaProperty property;
			global::Unity.VisualScripting.FullSerializer.fsResult fsResult2 = (success += GetProperty(instance, out property));
			if (fsResult2.Failed)
			{
				return success;
			}
			object result = null;
			if ((success += Serializer.TryDeserialize(data, property.StorageType, ref result)).Failed)
			{
				return success;
			}
			property.Write(instance, result);
			return success;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return global::Unity.VisualScripting.FullSerializer.fsMetaType.Get(Serializer.Config, storageType).CreateInstance();
		}
	}
}
