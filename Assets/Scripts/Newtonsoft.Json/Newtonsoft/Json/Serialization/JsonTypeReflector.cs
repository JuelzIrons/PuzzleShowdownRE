namespace Newtonsoft.Json.Serialization
{
	internal static class JsonTypeReflector
	{
		private static bool? _dynamicCodeGeneration;

		private static bool? _fullyTrusted;

		public const string IdPropertyName = "$id";

		public const string RefPropertyName = "$ref";

		public const string TypePropertyName = "$type";

		public const string ValuePropertyName = "$value";

		public const string ArrayValuesPropertyName = "$values";

		public const string ShouldSerializePrefix = "ShouldSerialize";

		public const string SpecifiedPostfix = "Specified";

		public const string ConcurrentDictionaryTypeName = "System.Collections.Concurrent.ConcurrentDictionary`2";

		private static readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::System.Func<object[]?, object>> CreatorCache = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::System.Func<object[], object>>(GetCreator);

		private static readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::System.Type?> AssociatedMetadataTypesCache = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::System.Type>(GetAssociateMetadataTypeFromAttribute);

		private static global::Newtonsoft.Json.Utilities.ReflectionObject? _metadataTypeAttributeReflectionObject;

		public static bool DynamicCodeGeneration
		{
			[global::System.Security.SecuritySafeCritical]
			get
			{
				if (!_dynamicCodeGeneration.HasValue)
				{
					_dynamicCodeGeneration = false;
				}
				return _dynamicCodeGeneration == true;
			}
		}

		public static bool FullyTrusted
		{
			get
			{
				if (!_fullyTrusted.HasValue)
				{
					global::System.AppDomain currentDomain = global::System.AppDomain.CurrentDomain;
					_fullyTrusted = currentDomain.IsHomogenous && currentDomain.IsFullyTrusted;
				}
				return _fullyTrusted == true;
			}
		}

		public static global::Newtonsoft.Json.Utilities.ReflectionDelegateFactory ReflectionDelegateFactory => global::Newtonsoft.Json.Utilities.LateBoundReflectionDelegateFactory.Instance;

		public static T? GetCachedAttribute<T>(object attributeProvider) where T : global::System.Attribute
		{
			return global::Newtonsoft.Json.Serialization.CachedAttributeGetter<T>.GetAttribute(attributeProvider);
		}

		public static bool CanTypeDescriptorConvertString(global::System.Type type, out global::System.ComponentModel.TypeConverter typeConverter)
		{
			typeConverter = global::System.ComponentModel.TypeDescriptor.GetConverter(type);
			if (typeConverter != null)
			{
				global::System.Type type2 = typeConverter.GetType();
				if (!string.Equals(type2.FullName, "System.ComponentModel.ComponentConverter", global::System.StringComparison.Ordinal) && !string.Equals(type2.FullName, "System.ComponentModel.ReferenceConverter", global::System.StringComparison.Ordinal) && !string.Equals(type2.FullName, "System.Windows.Forms.Design.DataSourceConverter", global::System.StringComparison.Ordinal) && type2 != typeof(global::System.ComponentModel.TypeConverter))
				{
					return typeConverter.CanConvertTo(typeof(string));
				}
			}
			return false;
		}

		public static global::System.Runtime.Serialization.DataContractAttribute? GetDataContractAttribute(global::System.Type type)
		{
			global::System.Type type2 = type;
			while (type2 != null)
			{
				global::System.Runtime.Serialization.DataContractAttribute attribute = global::Newtonsoft.Json.Serialization.CachedAttributeGetter<global::System.Runtime.Serialization.DataContractAttribute>.GetAttribute(type2);
				if (attribute != null)
				{
					return attribute;
				}
				type2 = global::Newtonsoft.Json.Utilities.TypeExtensions.BaseType(type2);
			}
			return null;
		}

		public static global::System.Runtime.Serialization.DataMemberAttribute? GetDataMemberAttribute(global::System.Reflection.MemberInfo memberInfo)
		{
			if (global::Newtonsoft.Json.Utilities.TypeExtensions.MemberType(memberInfo) == global::System.Reflection.MemberTypes.Field)
			{
				return global::Newtonsoft.Json.Serialization.CachedAttributeGetter<global::System.Runtime.Serialization.DataMemberAttribute>.GetAttribute(memberInfo);
			}
			global::System.Reflection.PropertyInfo propertyInfo = (global::System.Reflection.PropertyInfo)memberInfo;
			global::System.Runtime.Serialization.DataMemberAttribute attribute = global::Newtonsoft.Json.Serialization.CachedAttributeGetter<global::System.Runtime.Serialization.DataMemberAttribute>.GetAttribute(propertyInfo);
			if (attribute == null && global::Newtonsoft.Json.Utilities.ReflectionUtils.IsVirtual(propertyInfo))
			{
				global::System.Type type = propertyInfo.DeclaringType;
				while (attribute == null && type != null)
				{
					global::System.Reflection.PropertyInfo propertyInfo2 = (global::System.Reflection.PropertyInfo)global::Newtonsoft.Json.Utilities.ReflectionUtils.GetMemberInfoFromType(type, propertyInfo);
					if (propertyInfo2 != null && global::Newtonsoft.Json.Utilities.ReflectionUtils.IsVirtual(propertyInfo2))
					{
						attribute = global::Newtonsoft.Json.Serialization.CachedAttributeGetter<global::System.Runtime.Serialization.DataMemberAttribute>.GetAttribute(propertyInfo2);
					}
					type = global::Newtonsoft.Json.Utilities.TypeExtensions.BaseType(type);
				}
			}
			return attribute;
		}

		public static global::Newtonsoft.Json.MemberSerialization GetObjectMemberSerialization(global::System.Type objectType, bool ignoreSerializableAttribute)
		{
			global::Newtonsoft.Json.JsonObjectAttribute cachedAttribute = GetCachedAttribute<global::Newtonsoft.Json.JsonObjectAttribute>(objectType);
			if (cachedAttribute != null)
			{
				return cachedAttribute.MemberSerialization;
			}
			if (GetDataContractAttribute(objectType) != null)
			{
				return global::Newtonsoft.Json.MemberSerialization.OptIn;
			}
			if (!ignoreSerializableAttribute && IsSerializable(objectType))
			{
				return global::Newtonsoft.Json.MemberSerialization.Fields;
			}
			return global::Newtonsoft.Json.MemberSerialization.OptOut;
		}

		public static global::Newtonsoft.Json.JsonConverter? GetJsonConverter(object attributeProvider)
		{
			global::Newtonsoft.Json.JsonConverterAttribute cachedAttribute = GetCachedAttribute<global::Newtonsoft.Json.JsonConverterAttribute>(attributeProvider);
			if (cachedAttribute != null)
			{
				global::System.Func<object[], object> func = CreatorCache.Get(cachedAttribute.ConverterType);
				if (func != null)
				{
					return (global::Newtonsoft.Json.JsonConverter)func(cachedAttribute.ConverterParameters);
				}
			}
			return null;
		}

		public static global::Newtonsoft.Json.JsonConverter CreateJsonConverterInstance(global::System.Type converterType, object[]? args)
		{
			return (global::Newtonsoft.Json.JsonConverter)CreatorCache.Get(converterType)(args);
		}

		public static global::Newtonsoft.Json.Serialization.NamingStrategy CreateNamingStrategyInstance(global::System.Type namingStrategyType, object[]? args)
		{
			return (global::Newtonsoft.Json.Serialization.NamingStrategy)CreatorCache.Get(namingStrategyType)(args);
		}

		public static global::Newtonsoft.Json.Serialization.NamingStrategy? GetContainerNamingStrategy(global::Newtonsoft.Json.JsonContainerAttribute containerAttribute)
		{
			if (containerAttribute.NamingStrategyInstance == null)
			{
				if (containerAttribute.NamingStrategyType == null)
				{
					return null;
				}
				containerAttribute.NamingStrategyInstance = CreateNamingStrategyInstance(containerAttribute.NamingStrategyType, containerAttribute.NamingStrategyParameters);
			}
			return containerAttribute.NamingStrategyInstance;
		}

		private static global::System.Func<object[]?, object> GetCreator(global::System.Type type)
		{
			global::System.Func<object> defaultConstructor = (global::Newtonsoft.Json.Utilities.ReflectionUtils.HasDefaultConstructor(type, nonPublic: false) ? ReflectionDelegateFactory.CreateDefaultConstructor<object>(type) : null);
			return delegate(object[]? parameters)
			{
				try
				{
					if (parameters != null)
					{
						global::System.Type[] types = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(parameters, delegate(object param)
						{
							if (param == null)
							{
								throw new global::System.InvalidOperationException("Cannot pass a null parameter to the constructor.");
							}
							return param.GetType();
						}));
						global::System.Reflection.ConstructorInfo constructor = type.GetConstructor(types);
						if (!(constructor != null))
						{
							throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("No matching parameterized constructor found for '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, type));
						}
						return ReflectionDelegateFactory.CreateParameterizedConstructor(constructor)(parameters);
					}
					if (defaultConstructor == null)
					{
						throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("No parameterless constructor defined for '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, type));
					}
					return defaultConstructor();
				}
				catch (global::System.Exception innerException)
				{
					throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error creating '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, type), innerException);
				}
			};
		}

		private static global::System.Type? GetAssociatedMetadataType(global::System.Type type)
		{
			return AssociatedMetadataTypesCache.Get(type);
		}

		private static global::System.Type? GetAssociateMetadataTypeFromAttribute(global::System.Type type)
		{
			global::System.Attribute[] attributes = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttributes(type, null, inherit: true);
			foreach (global::System.Attribute attribute in attributes)
			{
				global::System.Type type2 = attribute.GetType();
				if (string.Equals(type2.FullName, "System.ComponentModel.DataAnnotations.MetadataTypeAttribute", global::System.StringComparison.Ordinal))
				{
					if (_metadataTypeAttributeReflectionObject == null)
					{
						_metadataTypeAttributeReflectionObject = global::Newtonsoft.Json.Utilities.ReflectionObject.Create(type2, "MetadataClassType");
					}
					return (global::System.Type)_metadataTypeAttributeReflectionObject.GetValue(attribute, "MetadataClassType");
				}
			}
			return null;
		}

		private static T? GetAttribute<T>(global::System.Type type) where T : global::System.Attribute
		{
			global::System.Type associatedMetadataType = GetAssociatedMetadataType(type);
			T attribute;
			if (associatedMetadataType != null)
			{
				attribute = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttribute<T>(associatedMetadataType, inherit: true);
				if (attribute != null)
				{
					return attribute;
				}
			}
			attribute = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttribute<T>(type, inherit: true);
			if (attribute != null)
			{
				return attribute;
			}
			global::System.Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				attribute = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttribute<T>(interfaces[i], inherit: true);
				if (attribute != null)
				{
					return attribute;
				}
			}
			return null;
		}

		private static T? GetAttribute<T>(global::System.Reflection.MemberInfo memberInfo) where T : global::System.Attribute
		{
			global::System.Type associatedMetadataType = GetAssociatedMetadataType(memberInfo.DeclaringType);
			T attribute;
			if (associatedMetadataType != null)
			{
				global::System.Reflection.MemberInfo memberInfoFromType = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetMemberInfoFromType(associatedMetadataType, memberInfo);
				if (memberInfoFromType != null)
				{
					attribute = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttribute<T>(memberInfoFromType, inherit: true);
					if (attribute != null)
					{
						return attribute;
					}
				}
			}
			attribute = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttribute<T>(memberInfo, inherit: true);
			if (attribute != null)
			{
				return attribute;
			}
			if (memberInfo.DeclaringType != null)
			{
				global::System.Type[] interfaces = memberInfo.DeclaringType.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					global::System.Reflection.MemberInfo memberInfoFromType2 = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetMemberInfoFromType(interfaces[i], memberInfo);
					if (memberInfoFromType2 != null)
					{
						attribute = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttribute<T>(memberInfoFromType2, inherit: true);
						if (attribute != null)
						{
							return attribute;
						}
					}
				}
			}
			return null;
		}

		public static bool IsNonSerializable(object provider)
		{
			return global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttribute<global::System.NonSerializedAttribute>(provider, inherit: false) != null;
		}

		public static bool IsSerializable(object provider)
		{
			return global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttribute<global::System.SerializableAttribute>(provider, inherit: false) != null;
		}

		public static T? GetAttribute<T>(object provider) where T : global::System.Attribute
		{
			if (provider is global::System.Type type)
			{
				return GetAttribute<T>(type);
			}
			if (provider is global::System.Reflection.MemberInfo memberInfo)
			{
				return GetAttribute<T>(memberInfo);
			}
			return global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttribute<T>(provider, inherit: true);
		}
	}
}
