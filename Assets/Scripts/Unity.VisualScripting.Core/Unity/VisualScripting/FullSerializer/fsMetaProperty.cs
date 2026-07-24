namespace Unity.VisualScripting.FullSerializer
{
	public class fsMetaProperty
	{
		internal global::System.Reflection.MemberInfo _memberInfo;

		public global::System.Type StorageType { get; private set; }

		public global::System.Type OverrideConverterType { get; private set; }

		public bool CanRead { get; private set; }

		public bool CanWrite { get; private set; }

		public string JsonName { get; private set; }

		public string MemberName { get; private set; }

		public bool IsPublic { get; private set; }

		public bool IsReadOnly { get; private set; }

		internal fsMetaProperty(global::Unity.VisualScripting.FullSerializer.fsConfig config, global::System.Reflection.FieldInfo field)
		{
			_memberInfo = field;
			StorageType = field.FieldType;
			MemberName = field.Name;
			IsPublic = field.IsPublic;
			IsReadOnly = field.IsInitOnly;
			CanRead = true;
			CanWrite = true;
			CommonInitialize(config);
		}

		internal fsMetaProperty(global::Unity.VisualScripting.FullSerializer.fsConfig config, global::System.Reflection.PropertyInfo property)
		{
			_memberInfo = property;
			StorageType = property.PropertyType;
			MemberName = property.Name;
			IsPublic = property.GetGetMethod() != null && property.GetGetMethod().IsPublic && property.GetSetMethod() != null && property.GetSetMethod().IsPublic;
			IsReadOnly = false;
			CanRead = property.CanRead;
			CanWrite = property.CanWrite;
			CommonInitialize(config);
		}

		private void CommonInitialize(global::Unity.VisualScripting.FullSerializer.fsConfig config)
		{
			global::Unity.VisualScripting.FullSerializer.fsPropertyAttribute attribute = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetAttribute<global::Unity.VisualScripting.FullSerializer.fsPropertyAttribute>(_memberInfo);
			if (attribute != null)
			{
				JsonName = attribute.Name;
				OverrideConverterType = attribute.Converter;
			}
			if (string.IsNullOrEmpty(JsonName))
			{
				JsonName = config.GetJsonNameFromMemberName(MemberName, _memberInfo);
			}
		}

		public void Write(object context, object value)
		{
			global::System.Reflection.FieldInfo fieldInfo = _memberInfo as global::System.Reflection.FieldInfo;
			global::System.Reflection.PropertyInfo propertyInfo = _memberInfo as global::System.Reflection.PropertyInfo;
			if (fieldInfo != null)
			{
				if (global::Unity.VisualScripting.PlatformUtility.supportsJit)
				{
					fieldInfo.SetValueOptimized(context, value);
				}
				else
				{
					fieldInfo.SetValue(context, value);
				}
			}
			else
			{
				if (!(propertyInfo != null))
				{
					return;
				}
				if (global::Unity.VisualScripting.PlatformUtility.supportsJit)
				{
					if (propertyInfo.CanWrite)
					{
						propertyInfo.SetValueOptimized(context, value);
					}
					return;
				}
				global::System.Reflection.MethodInfo setMethod = propertyInfo.GetSetMethod(nonPublic: true);
				if (setMethod != null)
				{
					setMethod.Invoke(context, new object[1] { value });
				}
			}
		}

		public object Read(object context)
		{
			if (_memberInfo is global::System.Reflection.PropertyInfo)
			{
				return ((global::System.Reflection.PropertyInfo)_memberInfo).GetValue(context, null);
			}
			return ((global::System.Reflection.FieldInfo)_memberInfo).GetValue(context);
		}
	}
}
