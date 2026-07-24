namespace Newtonsoft.Json.Serialization
{
	public class DefaultContractResolver : global::Newtonsoft.Json.Serialization.IContractResolver
	{
		internal class EnumerableDictionaryWrapper<TEnumeratorKey, TEnumeratorValue> : global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<object, object>>, global::System.Collections.IEnumerable
		{
			private readonly global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;

			public EnumerableDictionaryWrapper(global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(e, "e");
				_e = e;
			}

			public global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<object, object>> GetEnumerator()
			{
				foreach (global::System.Collections.Generic.KeyValuePair<TEnumeratorKey, TEnumeratorValue> item in _e)
				{
					yield return new global::System.Collections.Generic.KeyValuePair<object, object>(item.Key, item.Value);
				}
			}

			global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		private static readonly global::Newtonsoft.Json.Serialization.IContractResolver _instance = new global::Newtonsoft.Json.Serialization.DefaultContractResolver();

		private static readonly string[] BlacklistedTypeNames = new string[3] { "System.IO.DriveInfo", "System.IO.FileInfo", "System.IO.DirectoryInfo" };

		private static readonly global::Newtonsoft.Json.JsonConverter[] BuiltInConverters = new global::Newtonsoft.Json.JsonConverter[10]
		{
			new global::Newtonsoft.Json.Converters.EntityKeyMemberConverter(),
			new global::Newtonsoft.Json.Converters.ExpandoObjectConverter(),
			new global::Newtonsoft.Json.Converters.XmlNodeConverter(),
			new global::Newtonsoft.Json.Converters.BinaryConverter(),
			new global::Newtonsoft.Json.Converters.DataSetConverter(),
			new global::Newtonsoft.Json.Converters.DataTableConverter(),
			new global::Newtonsoft.Json.Converters.DiscriminatedUnionConverter(),
			new global::Newtonsoft.Json.Converters.KeyValuePairConverter(),
			new global::Newtonsoft.Json.Converters.BsonObjectIdConverter(),
			new global::Newtonsoft.Json.Converters.RegexConverter()
		};

		private readonly global::Newtonsoft.Json.DefaultJsonNameTable _nameTable = new global::Newtonsoft.Json.DefaultJsonNameTable();

		private readonly global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::Newtonsoft.Json.Serialization.JsonContract> _contractCache;

		internal static global::Newtonsoft.Json.Serialization.IContractResolver Instance => _instance;

		public bool DynamicCodeGeneration => global::Newtonsoft.Json.Serialization.JsonTypeReflector.DynamicCodeGeneration;

		[global::System.Obsolete("DefaultMembersSearchFlags is obsolete. To modify the members serialized inherit from DefaultContractResolver and override the GetSerializableMembers method instead.")]
		public global::System.Reflection.BindingFlags DefaultMembersSearchFlags { get; set; }

		public bool SerializeCompilerGeneratedMembers { get; set; }

		public bool IgnoreSerializableInterface { get; set; }

		public bool IgnoreSerializableAttribute { get; set; }

		public bool IgnoreIsSpecifiedMembers { get; set; }

		public bool IgnoreShouldSerializeMembers { get; set; }

		public global::Newtonsoft.Json.Serialization.NamingStrategy? NamingStrategy { get; set; }

		public DefaultContractResolver()
		{
			IgnoreSerializableAttribute = true;
			DefaultMembersSearchFlags = global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public;
			_contractCache = new global::Newtonsoft.Json.Utilities.ThreadSafeStore<global::System.Type, global::Newtonsoft.Json.Serialization.JsonContract>(CreateContract);
		}

		public virtual global::Newtonsoft.Json.Serialization.JsonContract ResolveContract(global::System.Type type)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(type, "type");
			return _contractCache.Get(type);
		}

		private static bool FilterMembers(global::System.Reflection.MemberInfo member)
		{
			if (member is global::System.Reflection.PropertyInfo propertyInfo)
			{
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsIndexedProperty(propertyInfo))
				{
					return false;
				}
				return !global::Newtonsoft.Json.Utilities.ReflectionUtils.IsByRefLikeType(propertyInfo.PropertyType);
			}
			if (member is global::System.Reflection.FieldInfo fieldInfo)
			{
				return !global::Newtonsoft.Json.Utilities.ReflectionUtils.IsByRefLikeType(fieldInfo.FieldType);
			}
			return true;
		}

		protected virtual global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> GetSerializableMembers(global::System.Type objectType)
		{
			bool ignoreSerializableAttribute = IgnoreSerializableAttribute;
			global::Newtonsoft.Json.MemberSerialization objectMemberSerialization = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetObjectMemberSerialization(objectType, ignoreSerializableAttribute);
			global::System.Collections.Generic.IEnumerable<global::System.Reflection.MemberInfo> enumerable = global::System.Linq.Enumerable.Where(global::Newtonsoft.Json.Utilities.ReflectionUtils.GetFieldsAndProperties(objectType, global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic), (global::System.Reflection.MemberInfo m) => !(m is global::System.Reflection.PropertyInfo property) || !global::Newtonsoft.Json.Utilities.ReflectionUtils.IsIndexedProperty(property));
			global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> list = new global::System.Collections.Generic.List<global::System.Reflection.MemberInfo>();
			if (objectMemberSerialization != global::Newtonsoft.Json.MemberSerialization.Fields)
			{
				global::System.Runtime.Serialization.DataContractAttribute dataContractAttribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetDataContractAttribute(objectType);
				global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> list2 = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(global::Newtonsoft.Json.Utilities.ReflectionUtils.GetFieldsAndProperties(objectType, DefaultMembersSearchFlags), FilterMembers));
				foreach (global::System.Reflection.MemberInfo item in enumerable)
				{
					if (SerializeCompilerGeneratedMembers || !item.IsDefined(typeof(global::System.Runtime.CompilerServices.CompilerGeneratedAttribute), inherit: true))
					{
						if (list2.Contains(item))
						{
							list.Add(item);
						}
						else if (global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::Newtonsoft.Json.JsonPropertyAttribute>(item) != null)
						{
							list.Add(item);
						}
						else if (global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::Newtonsoft.Json.JsonRequiredAttribute>(item) != null)
						{
							list.Add(item);
						}
						else if (dataContractAttribute != null && global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::System.Runtime.Serialization.DataMemberAttribute>(item) != null)
						{
							list.Add(item);
						}
						else if (objectMemberSerialization == global::Newtonsoft.Json.MemberSerialization.Fields && global::Newtonsoft.Json.Utilities.TypeExtensions.MemberType(item) == global::System.Reflection.MemberTypes.Field)
						{
							list.Add(item);
						}
					}
				}
				if (global::Newtonsoft.Json.Utilities.TypeExtensions.AssignableToTypeName(objectType, "System.Data.Objects.DataClasses.EntityObject", searchInterfaces: false, out global::System.Type _))
				{
					list = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(list, ShouldSerializeEntityMember));
				}
				if (typeof(global::System.Exception).IsAssignableFrom(objectType))
				{
					list = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(list, (global::System.Reflection.MemberInfo m) => !string.Equals(m.Name, "TargetSite", global::System.StringComparison.Ordinal)));
				}
			}
			else
			{
				foreach (global::System.Reflection.MemberInfo item2 in enumerable)
				{
					if (item2 is global::System.Reflection.FieldInfo { IsStatic: false })
					{
						list.Add(item2);
					}
				}
			}
			return list;
		}

		private bool ShouldSerializeEntityMember(global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo is global::System.Reflection.PropertyInfo propertyInfo && global::Newtonsoft.Json.Utilities.TypeExtensions.IsGenericType(propertyInfo.PropertyType) && propertyInfo.PropertyType.GetGenericTypeDefinition().FullName == "System.Data.Objects.DataClasses.EntityReference`1")
			{
				return false;
			}
			return true;
		}

		protected virtual global::Newtonsoft.Json.Serialization.JsonObjectContract CreateObjectContract(global::System.Type objectType)
		{
			global::Newtonsoft.Json.Serialization.JsonObjectContract jsonObjectContract = new global::Newtonsoft.Json.Serialization.JsonObjectContract(objectType);
			InitializeContract(jsonObjectContract);
			bool ignoreSerializableAttribute = IgnoreSerializableAttribute;
			jsonObjectContract.MemberSerialization = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetObjectMemberSerialization(jsonObjectContract.NonNullableUnderlyingType, ignoreSerializableAttribute);
			global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(jsonObjectContract.Properties, CreateProperties(jsonObjectContract.NonNullableUnderlyingType, jsonObjectContract.MemberSerialization));
			global::System.Func<string, string> func = null;
			global::Newtonsoft.Json.JsonObjectAttribute cachedAttribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetCachedAttribute<global::Newtonsoft.Json.JsonObjectAttribute>(jsonObjectContract.NonNullableUnderlyingType);
			if (cachedAttribute != null)
			{
				jsonObjectContract.ItemRequired = cachedAttribute._itemRequired;
				jsonObjectContract.ItemNullValueHandling = cachedAttribute._itemNullValueHandling;
				jsonObjectContract.MissingMemberHandling = cachedAttribute._missingMemberHandling;
				if (cachedAttribute.NamingStrategyType != null)
				{
					global::Newtonsoft.Json.Serialization.NamingStrategy namingStrategy = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetContainerNamingStrategy(cachedAttribute);
					func = (string s) => namingStrategy.GetDictionaryKey(s);
				}
			}
			if (func == null)
			{
				func = ResolveExtensionDataName;
			}
			jsonObjectContract.ExtensionDataNameResolver = func;
			if (jsonObjectContract.IsInstantiable)
			{
				global::System.Reflection.ConstructorInfo attributeConstructor = GetAttributeConstructor(jsonObjectContract.NonNullableUnderlyingType);
				if (attributeConstructor != null)
				{
					jsonObjectContract.OverrideCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(attributeConstructor);
					global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(jsonObjectContract.CreatorParameters, CreateConstructorParameters(attributeConstructor, jsonObjectContract.Properties));
				}
				else if (jsonObjectContract.MemberSerialization == global::Newtonsoft.Json.MemberSerialization.Fields)
				{
					if (global::Newtonsoft.Json.Serialization.JsonTypeReflector.FullyTrusted)
					{
						jsonObjectContract.DefaultCreator = jsonObjectContract.GetUninitializedObject;
					}
				}
				else if (jsonObjectContract.DefaultCreator == null || jsonObjectContract.DefaultCreatorNonPublic)
				{
					global::System.Reflection.ConstructorInfo parameterizedConstructor = GetParameterizedConstructor(jsonObjectContract.NonNullableUnderlyingType);
					if (parameterizedConstructor != null)
					{
						jsonObjectContract.ParameterizedCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(parameterizedConstructor);
						global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(jsonObjectContract.CreatorParameters, CreateConstructorParameters(parameterizedConstructor, jsonObjectContract.Properties));
					}
				}
				else if (global::Newtonsoft.Json.Utilities.TypeExtensions.IsValueType(jsonObjectContract.NonNullableUnderlyingType))
				{
					global::System.Reflection.ConstructorInfo immutableConstructor = GetImmutableConstructor(jsonObjectContract.NonNullableUnderlyingType, jsonObjectContract.Properties);
					if (immutableConstructor != null)
					{
						jsonObjectContract.OverrideCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(immutableConstructor);
						global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(jsonObjectContract.CreatorParameters, CreateConstructorParameters(immutableConstructor, jsonObjectContract.Properties));
					}
				}
			}
			global::System.Reflection.MemberInfo extensionDataMemberForType = GetExtensionDataMemberForType(jsonObjectContract.NonNullableUnderlyingType);
			if (extensionDataMemberForType != null)
			{
				SetExtensionDataDelegates(jsonObjectContract, extensionDataMemberForType);
			}
			if (global::System.Array.IndexOf<string>(BlacklistedTypeNames, objectType.FullName) != -1)
			{
				jsonObjectContract.OnSerializingCallbacks.Add(ThrowUnableToSerializeError);
			}
			return jsonObjectContract;
		}

		private static void ThrowUnableToSerializeError(object o, global::System.Runtime.Serialization.StreamingContext context)
		{
			throw new global::Newtonsoft.Json.JsonSerializationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unable to serialize instance of '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, o.GetType()));
		}

		private global::System.Reflection.MemberInfo? GetExtensionDataMemberForType(global::System.Type type)
		{
			return global::System.Linq.Enumerable.LastOrDefault(global::System.Linq.Enumerable.SelectMany(GetClassHierarchyForType(type), delegate(global::System.Type baseType)
			{
				global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> list = new global::System.Collections.Generic.List<global::System.Reflection.MemberInfo>();
				global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(list, baseType.GetProperties(global::System.Reflection.BindingFlags.DeclaredOnly | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic));
				global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(list, baseType.GetFields(global::System.Reflection.BindingFlags.DeclaredOnly | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic));
				return list;
			}), delegate(global::System.Reflection.MemberInfo m)
			{
				global::System.Reflection.MemberTypes memberTypes = global::Newtonsoft.Json.Utilities.TypeExtensions.MemberType(m);
				if (memberTypes != global::System.Reflection.MemberTypes.Property && memberTypes != global::System.Reflection.MemberTypes.Field)
				{
					return false;
				}
				if (!m.IsDefined(typeof(global::Newtonsoft.Json.JsonExtensionDataAttribute), inherit: false))
				{
					return false;
				}
				if (!global::Newtonsoft.Json.Utilities.ReflectionUtils.CanReadMemberValue(m, nonPublic: true))
				{
					throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid extension data attribute on '{0}'. Member '{1}' must have a getter.", global::System.Globalization.CultureInfo.InvariantCulture, GetClrTypeFullName(m.DeclaringType), m.Name));
				}
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.ImplementsGenericDefinition(global::Newtonsoft.Json.Utilities.ReflectionUtils.GetMemberUnderlyingType(m), typeof(global::System.Collections.Generic.IDictionary<, >), out global::System.Type implementingType))
				{
					global::System.Type obj = implementingType.GetGenericArguments()[0];
					global::System.Type type2 = implementingType.GetGenericArguments()[1];
					if (obj.IsAssignableFrom(typeof(string)) && type2.IsAssignableFrom(typeof(global::Newtonsoft.Json.Linq.JToken)))
					{
						return true;
					}
				}
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid extension data attribute on '{0}'. Member '{1}' type must implement IDictionary<string, JToken>.", global::System.Globalization.CultureInfo.InvariantCulture, GetClrTypeFullName(m.DeclaringType), m.Name));
			});
		}

		private static void SetExtensionDataDelegates(global::Newtonsoft.Json.Serialization.JsonObjectContract contract, global::System.Reflection.MemberInfo member)
		{
			global::Newtonsoft.Json.JsonExtensionDataAttribute attribute = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetAttribute<global::Newtonsoft.Json.JsonExtensionDataAttribute>(member);
			if (attribute == null)
			{
				return;
			}
			global::System.Type memberUnderlyingType = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetMemberUnderlyingType(member);
			global::Newtonsoft.Json.Utilities.ReflectionUtils.ImplementsGenericDefinition(memberUnderlyingType, typeof(global::System.Collections.Generic.IDictionary<, >), out global::System.Type implementingType);
			global::System.Type type = implementingType.GetGenericArguments()[0];
			global::System.Type type2 = implementingType.GetGenericArguments()[1];
			global::System.Type type3 = ((!global::Newtonsoft.Json.Utilities.ReflectionUtils.IsGenericDefinition(memberUnderlyingType, typeof(global::System.Collections.Generic.IDictionary<, >))) ? memberUnderlyingType : typeof(global::System.Collections.Generic.Dictionary<, >).MakeGenericType(type, type2));
			global::System.Func<object, object?> getExtensionDataDictionary = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(member);
			if (attribute.ReadData)
			{
				global::System.Action<object, object?> setExtensionDataDictionary = (global::Newtonsoft.Json.Utilities.ReflectionUtils.CanSetMemberValue(member, nonPublic: true, canSetReadOnly: false) ? global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateSet<object>(member) : null);
				global::System.Func<object> createExtensionDataDictionary = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type3);
				global::System.Reflection.MethodInfo methodInfo = memberUnderlyingType.GetProperty("Item", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public, null, type2, new global::System.Type[1] { type }, null)?.GetSetMethod();
				if (methodInfo == null)
				{
					methodInfo = implementingType.GetProperty("Item", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public, null, type2, new global::System.Type[1] { type }, null)?.GetSetMethod();
				}
				global::Newtonsoft.Json.Utilities.MethodCall<object, object?> setExtensionDataDictionaryValue = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
				global::Newtonsoft.Json.Serialization.ExtensionDataSetter extensionDataSetter = delegate(object o, string key, object? value)
				{
					object obj = getExtensionDataDictionary(o);
					if (obj == null)
					{
						if (setExtensionDataDictionary == null)
						{
							throw new global::Newtonsoft.Json.JsonSerializationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot set value onto extension data member '{0}'. The extension data collection is null and it cannot be set.", global::System.Globalization.CultureInfo.InvariantCulture, member.Name));
						}
						obj = createExtensionDataDictionary();
						setExtensionDataDictionary(o, obj);
					}
					setExtensionDataDictionaryValue(obj, key, value);
				};
				contract.ExtensionDataSetter = extensionDataSetter;
			}
			if (attribute.WriteData)
			{
				global::System.Reflection.ConstructorInfo method = global::System.Linq.Enumerable.First(typeof(global::Newtonsoft.Json.Serialization.DefaultContractResolver.EnumerableDictionaryWrapper<, >).MakeGenericType(type, type2).GetConstructors());
				global::Newtonsoft.Json.Serialization.ObjectConstructor<object> createEnumerableWrapper = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(method);
				global::Newtonsoft.Json.Serialization.ExtensionDataGetter extensionDataGetter = delegate(object o)
				{
					object obj = getExtensionDataDictionary(o);
					return (obj == null) ? null : ((global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<object, object>>)createEnumerableWrapper(obj));
				};
				contract.ExtensionDataGetter = extensionDataGetter;
			}
			contract.ExtensionDataValueType = type2;
		}

		private global::System.Reflection.ConstructorInfo? GetAttributeConstructor(global::System.Type objectType)
		{
			global::System.Collections.Generic.IEnumerator<global::System.Reflection.ConstructorInfo> enumerator = global::System.Linq.Enumerable.Where(objectType.GetConstructors(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic), (global::System.Reflection.ConstructorInfo c) => c.IsDefined(typeof(global::Newtonsoft.Json.JsonConstructorAttribute), inherit: true)).GetEnumerator();
			if (enumerator.MoveNext())
			{
				global::System.Reflection.ConstructorInfo current = enumerator.Current;
				if (enumerator.MoveNext())
				{
					throw new global::Newtonsoft.Json.JsonException("Multiple constructors with the JsonConstructorAttribute.");
				}
				return current;
			}
			if (objectType == typeof(global::System.Version))
			{
				return objectType.GetConstructor(new global::System.Type[4]
				{
					typeof(int),
					typeof(int),
					typeof(int),
					typeof(int)
				});
			}
			return null;
		}

		private global::System.Reflection.ConstructorInfo? GetImmutableConstructor(global::System.Type objectType, global::Newtonsoft.Json.Serialization.JsonPropertyCollection memberProperties)
		{
			global::System.Collections.Generic.IEnumerator<global::System.Reflection.ConstructorInfo> enumerator = ((global::System.Collections.Generic.IEnumerable<global::System.Reflection.ConstructorInfo>)objectType.GetConstructors()).GetEnumerator();
			if (enumerator.MoveNext())
			{
				global::System.Reflection.ConstructorInfo current = enumerator.Current;
				if (!enumerator.MoveNext())
				{
					global::System.Reflection.ParameterInfo[] parameters = current.GetParameters();
					if (parameters.Length != 0)
					{
						global::System.Reflection.ParameterInfo[] array = parameters;
						foreach (global::System.Reflection.ParameterInfo parameterInfo in array)
						{
							global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty = MatchProperty(memberProperties, parameterInfo.Name, parameterInfo.ParameterType);
							if (jsonProperty == null || jsonProperty.Writable)
							{
								return null;
							}
						}
						return current;
					}
				}
			}
			return null;
		}

		private global::System.Reflection.ConstructorInfo? GetParameterizedConstructor(global::System.Type objectType)
		{
			global::System.Reflection.ConstructorInfo[] constructors = objectType.GetConstructors(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
			if (constructors.Length == 1)
			{
				return constructors[0];
			}
			return null;
		}

		protected virtual global::System.Collections.Generic.IList<global::Newtonsoft.Json.Serialization.JsonProperty> CreateConstructorParameters(global::System.Reflection.ConstructorInfo constructor, global::Newtonsoft.Json.Serialization.JsonPropertyCollection memberProperties)
		{
			global::System.Reflection.ParameterInfo[] parameters = constructor.GetParameters();
			global::Newtonsoft.Json.Serialization.JsonPropertyCollection jsonPropertyCollection = new global::Newtonsoft.Json.Serialization.JsonPropertyCollection(constructor.DeclaringType);
			global::System.Reflection.ParameterInfo[] array = parameters;
			foreach (global::System.Reflection.ParameterInfo parameterInfo in array)
			{
				if (parameterInfo.Name == null)
				{
					continue;
				}
				global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty = MatchProperty(memberProperties, parameterInfo.Name, parameterInfo.ParameterType);
				if (jsonProperty != null || parameterInfo.Name != null)
				{
					global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty2 = CreatePropertyFromConstructorParameter(jsonProperty, parameterInfo);
					if (jsonProperty2 != null)
					{
						jsonPropertyCollection.AddProperty(jsonProperty2);
					}
				}
			}
			return jsonPropertyCollection;
		}

		private global::Newtonsoft.Json.Serialization.JsonProperty? MatchProperty(global::Newtonsoft.Json.Serialization.JsonPropertyCollection properties, string name, global::System.Type type)
		{
			if (name == null)
			{
				return null;
			}
			global::Newtonsoft.Json.Serialization.JsonProperty closestMatchProperty = properties.GetClosestMatchProperty(name);
			if (closestMatchProperty == null || closestMatchProperty.PropertyType != type)
			{
				return null;
			}
			return closestMatchProperty;
		}

		protected virtual global::Newtonsoft.Json.Serialization.JsonProperty CreatePropertyFromConstructorParameter(global::Newtonsoft.Json.Serialization.JsonProperty? matchingMemberProperty, global::System.Reflection.ParameterInfo parameterInfo)
		{
			global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty = new global::Newtonsoft.Json.Serialization.JsonProperty();
			jsonProperty.PropertyType = parameterInfo.ParameterType;
			jsonProperty.AttributeProvider = new global::Newtonsoft.Json.Serialization.ReflectionAttributeProvider(parameterInfo);
			SetPropertySettingsFromAttributes(jsonProperty, parameterInfo, parameterInfo.Name, parameterInfo.Member.DeclaringType, global::Newtonsoft.Json.MemberSerialization.OptOut, out var _);
			jsonProperty.Readable = false;
			jsonProperty.Writable = true;
			if (matchingMemberProperty != null)
			{
				jsonProperty.PropertyName = ((jsonProperty.PropertyName != parameterInfo.Name) ? jsonProperty.PropertyName : matchingMemberProperty.PropertyName);
				jsonProperty.Converter = jsonProperty.Converter ?? matchingMemberProperty.Converter;
				if (!jsonProperty._hasExplicitDefaultValue && matchingMemberProperty._hasExplicitDefaultValue)
				{
					jsonProperty.DefaultValue = matchingMemberProperty.DefaultValue;
				}
				jsonProperty._required = jsonProperty._required ?? matchingMemberProperty._required;
				jsonProperty.IsReference = jsonProperty.IsReference ?? matchingMemberProperty.IsReference;
				jsonProperty.NullValueHandling = jsonProperty.NullValueHandling ?? matchingMemberProperty.NullValueHandling;
				jsonProperty.DefaultValueHandling = jsonProperty.DefaultValueHandling ?? matchingMemberProperty.DefaultValueHandling;
				jsonProperty.ReferenceLoopHandling = jsonProperty.ReferenceLoopHandling ?? matchingMemberProperty.ReferenceLoopHandling;
				jsonProperty.ObjectCreationHandling = jsonProperty.ObjectCreationHandling ?? matchingMemberProperty.ObjectCreationHandling;
				jsonProperty.TypeNameHandling = jsonProperty.TypeNameHandling ?? matchingMemberProperty.TypeNameHandling;
			}
			return jsonProperty;
		}

		protected virtual global::Newtonsoft.Json.JsonConverter? ResolveContractConverter(global::System.Type objectType)
		{
			return global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetJsonConverter(objectType);
		}

		private global::System.Func<object> GetDefaultCreator(global::System.Type createdType)
		{
			return global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(createdType);
		}

		private void InitializeContract(global::Newtonsoft.Json.Serialization.JsonContract contract)
		{
			global::Newtonsoft.Json.JsonContainerAttribute cachedAttribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetCachedAttribute<global::Newtonsoft.Json.JsonContainerAttribute>(contract.NonNullableUnderlyingType);
			if (cachedAttribute != null)
			{
				contract.IsReference = cachedAttribute._isReference;
			}
			else
			{
				global::System.Runtime.Serialization.DataContractAttribute dataContractAttribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetDataContractAttribute(contract.NonNullableUnderlyingType);
				if (dataContractAttribute != null && dataContractAttribute.IsReference)
				{
					contract.IsReference = true;
				}
			}
			contract.Converter = ResolveContractConverter(contract.NonNullableUnderlyingType);
			contract.InternalConverter = global::Newtonsoft.Json.JsonSerializer.GetMatchingConverter(BuiltInConverters, contract.NonNullableUnderlyingType);
			if (contract.IsInstantiable && (global::Newtonsoft.Json.Utilities.ReflectionUtils.HasDefaultConstructor(contract.CreatedType, nonPublic: true) || global::Newtonsoft.Json.Utilities.TypeExtensions.IsValueType(contract.CreatedType)))
			{
				contract.DefaultCreator = GetDefaultCreator(contract.CreatedType);
				contract.DefaultCreatorNonPublic = !global::Newtonsoft.Json.Utilities.TypeExtensions.IsValueType(contract.CreatedType) && global::Newtonsoft.Json.Utilities.ReflectionUtils.GetDefaultConstructor(contract.CreatedType) == null;
			}
			ResolveCallbackMethods(contract, contract.NonNullableUnderlyingType);
		}

		private void ResolveCallbackMethods(global::Newtonsoft.Json.Serialization.JsonContract contract, global::System.Type t)
		{
			GetCallbackMethodsForType(t, out global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback> onSerializing, out global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback> onSerialized, out global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback> onDeserializing, out global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback> onDeserialized, out global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationErrorCallback> onError);
			if (onSerializing != null)
			{
				global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(contract.OnSerializingCallbacks, onSerializing);
			}
			if (onSerialized != null)
			{
				global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(contract.OnSerializedCallbacks, onSerialized);
			}
			if (onDeserializing != null)
			{
				global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(contract.OnDeserializingCallbacks, onDeserializing);
			}
			if (onDeserialized != null)
			{
				global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(contract.OnDeserializedCallbacks, onDeserialized);
			}
			if (onError != null)
			{
				global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(contract.OnErrorCallbacks, onError);
			}
		}

		private void GetCallbackMethodsForType(global::System.Type type, out global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>? onSerializing, out global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>? onSerialized, out global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>? onDeserializing, out global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>? onDeserialized, out global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationErrorCallback>? onError)
		{
			onSerializing = null;
			onSerialized = null;
			onDeserializing = null;
			onDeserialized = null;
			onError = null;
			foreach (global::System.Type item in GetClassHierarchyForType(type))
			{
				global::System.Reflection.MethodInfo currentCallback = null;
				global::System.Reflection.MethodInfo currentCallback2 = null;
				global::System.Reflection.MethodInfo currentCallback3 = null;
				global::System.Reflection.MethodInfo currentCallback4 = null;
				global::System.Reflection.MethodInfo currentCallback5 = null;
				bool flag = ShouldSkipSerializing(item);
				bool flag2 = ShouldSkipDeserialized(item);
				global::System.Reflection.MethodInfo[] methods = item.GetMethods(global::System.Reflection.BindingFlags.DeclaredOnly | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
				foreach (global::System.Reflection.MethodInfo methodInfo in methods)
				{
					if (!methodInfo.ContainsGenericParameters)
					{
						global::System.Type prevAttributeType = null;
						global::System.Reflection.ParameterInfo[] parameters = methodInfo.GetParameters();
						if (!flag && IsValidCallback(methodInfo, parameters, typeof(global::System.Runtime.Serialization.OnSerializingAttribute), currentCallback, ref prevAttributeType))
						{
							onSerializing = onSerializing ?? new global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>();
							onSerializing.Add(global::Newtonsoft.Json.Serialization.JsonContract.CreateSerializationCallback(methodInfo));
							currentCallback = methodInfo;
						}
						if (IsValidCallback(methodInfo, parameters, typeof(global::System.Runtime.Serialization.OnSerializedAttribute), currentCallback2, ref prevAttributeType))
						{
							onSerialized = onSerialized ?? new global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>();
							onSerialized.Add(global::Newtonsoft.Json.Serialization.JsonContract.CreateSerializationCallback(methodInfo));
							currentCallback2 = methodInfo;
						}
						if (IsValidCallback(methodInfo, parameters, typeof(global::System.Runtime.Serialization.OnDeserializingAttribute), currentCallback3, ref prevAttributeType))
						{
							onDeserializing = onDeserializing ?? new global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>();
							onDeserializing.Add(global::Newtonsoft.Json.Serialization.JsonContract.CreateSerializationCallback(methodInfo));
							currentCallback3 = methodInfo;
						}
						if (!flag2 && IsValidCallback(methodInfo, parameters, typeof(global::System.Runtime.Serialization.OnDeserializedAttribute), currentCallback4, ref prevAttributeType))
						{
							onDeserialized = onDeserialized ?? new global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationCallback>();
							onDeserialized.Add(global::Newtonsoft.Json.Serialization.JsonContract.CreateSerializationCallback(methodInfo));
							currentCallback4 = methodInfo;
						}
						if (IsValidCallback(methodInfo, parameters, typeof(global::Newtonsoft.Json.Serialization.OnErrorAttribute), currentCallback5, ref prevAttributeType))
						{
							onError = onError ?? new global::System.Collections.Generic.List<global::Newtonsoft.Json.Serialization.SerializationErrorCallback>();
							onError.Add(global::Newtonsoft.Json.Serialization.JsonContract.CreateSerializationErrorCallback(methodInfo));
							currentCallback5 = methodInfo;
						}
					}
				}
			}
		}

		private static bool IsConcurrentOrObservableCollection(global::System.Type t)
		{
			if (global::Newtonsoft.Json.Utilities.TypeExtensions.IsGenericType(t))
			{
				switch (t.GetGenericTypeDefinition().FullName)
				{
				case "System.Collections.Concurrent.ConcurrentQueue`1":
				case "System.Collections.Concurrent.ConcurrentStack`1":
				case "System.Collections.Concurrent.ConcurrentBag`1":
				case "System.Collections.Concurrent.ConcurrentDictionary`2":
				case "System.Collections.ObjectModel.ObservableCollection`1":
					return true;
				}
			}
			return false;
		}

		private static bool ShouldSkipDeserialized(global::System.Type t)
		{
			if (IsConcurrentOrObservableCollection(t))
			{
				return true;
			}
			if (t.Name == "FSharpSet`1" || t.Name == "FSharpMap`2")
			{
				return true;
			}
			return false;
		}

		private static bool ShouldSkipSerializing(global::System.Type t)
		{
			if (IsConcurrentOrObservableCollection(t))
			{
				return true;
			}
			if (t.Name == "FSharpSet`1" || t.Name == "FSharpMap`2")
			{
				return true;
			}
			return false;
		}

		private global::System.Collections.Generic.List<global::System.Type> GetClassHierarchyForType(global::System.Type type)
		{
			global::System.Collections.Generic.List<global::System.Type> list = new global::System.Collections.Generic.List<global::System.Type>();
			global::System.Type type2 = type;
			while (type2 != null && type2 != typeof(object))
			{
				list.Add(type2);
				type2 = global::Newtonsoft.Json.Utilities.TypeExtensions.BaseType(type2);
			}
			list.Reverse();
			return list;
		}

		protected virtual global::Newtonsoft.Json.Serialization.JsonDictionaryContract CreateDictionaryContract(global::System.Type objectType)
		{
			global::Newtonsoft.Json.Serialization.JsonDictionaryContract jsonDictionaryContract = new global::Newtonsoft.Json.Serialization.JsonDictionaryContract(objectType);
			InitializeContract(jsonDictionaryContract);
			global::Newtonsoft.Json.JsonContainerAttribute attribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::Newtonsoft.Json.JsonContainerAttribute>(objectType);
			if (attribute?.NamingStrategyType != null)
			{
				global::Newtonsoft.Json.Serialization.NamingStrategy namingStrategy = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetContainerNamingStrategy(attribute);
				jsonDictionaryContract.DictionaryKeyResolver = (string s) => namingStrategy.GetDictionaryKey(s);
			}
			else
			{
				jsonDictionaryContract.DictionaryKeyResolver = ResolveDictionaryKey;
			}
			global::System.Reflection.ConstructorInfo attributeConstructor = GetAttributeConstructor(jsonDictionaryContract.NonNullableUnderlyingType);
			if (attributeConstructor != null)
			{
				global::System.Reflection.ParameterInfo[] parameters = attributeConstructor.GetParameters();
				global::System.Type type = ((jsonDictionaryContract.DictionaryKeyType != null && jsonDictionaryContract.DictionaryValueType != null) ? typeof(global::System.Collections.Generic.IEnumerable<>).MakeGenericType(typeof(global::System.Collections.Generic.KeyValuePair<, >).MakeGenericType(jsonDictionaryContract.DictionaryKeyType, jsonDictionaryContract.DictionaryValueType)) : typeof(global::System.Collections.IDictionary));
				if (parameters.Length == 0)
				{
					jsonDictionaryContract.HasParameterizedCreator = false;
				}
				else
				{
					if (parameters.Length != 1 || !type.IsAssignableFrom(parameters[0].ParameterType))
					{
						throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Constructor for '{0}' must have no parameters or a single parameter that implements '{1}'.", global::System.Globalization.CultureInfo.InvariantCulture, jsonDictionaryContract.UnderlyingType, type));
					}
					jsonDictionaryContract.HasParameterizedCreator = true;
				}
				jsonDictionaryContract.OverrideCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(attributeConstructor);
			}
			return jsonDictionaryContract;
		}

		protected virtual global::Newtonsoft.Json.Serialization.JsonArrayContract CreateArrayContract(global::System.Type objectType)
		{
			global::Newtonsoft.Json.Serialization.JsonArrayContract jsonArrayContract = new global::Newtonsoft.Json.Serialization.JsonArrayContract(objectType);
			InitializeContract(jsonArrayContract);
			global::System.Reflection.ConstructorInfo attributeConstructor = GetAttributeConstructor(jsonArrayContract.NonNullableUnderlyingType);
			if (attributeConstructor != null)
			{
				global::System.Reflection.ParameterInfo[] parameters = attributeConstructor.GetParameters();
				global::System.Type type = ((jsonArrayContract.CollectionItemType != null) ? typeof(global::System.Collections.Generic.IEnumerable<>).MakeGenericType(jsonArrayContract.CollectionItemType) : typeof(global::System.Collections.IEnumerable));
				if (parameters.Length == 0)
				{
					jsonArrayContract.HasParameterizedCreator = false;
				}
				else
				{
					if (parameters.Length != 1 || !type.IsAssignableFrom(parameters[0].ParameterType))
					{
						throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Constructor for '{0}' must have no parameters or a single parameter that implements '{1}'.", global::System.Globalization.CultureInfo.InvariantCulture, jsonArrayContract.UnderlyingType, type));
					}
					jsonArrayContract.HasParameterizedCreator = true;
				}
				jsonArrayContract.OverrideCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(attributeConstructor);
			}
			return jsonArrayContract;
		}

		protected virtual global::Newtonsoft.Json.Serialization.JsonPrimitiveContract CreatePrimitiveContract(global::System.Type objectType)
		{
			global::Newtonsoft.Json.Serialization.JsonPrimitiveContract jsonPrimitiveContract = new global::Newtonsoft.Json.Serialization.JsonPrimitiveContract(objectType);
			InitializeContract(jsonPrimitiveContract);
			return jsonPrimitiveContract;
		}

		protected virtual global::Newtonsoft.Json.Serialization.JsonLinqContract CreateLinqContract(global::System.Type objectType)
		{
			global::Newtonsoft.Json.Serialization.JsonLinqContract jsonLinqContract = new global::Newtonsoft.Json.Serialization.JsonLinqContract(objectType);
			InitializeContract(jsonLinqContract);
			return jsonLinqContract;
		}

		protected virtual global::Newtonsoft.Json.Serialization.JsonISerializableContract CreateISerializableContract(global::System.Type objectType)
		{
			global::Newtonsoft.Json.Serialization.JsonISerializableContract jsonISerializableContract = new global::Newtonsoft.Json.Serialization.JsonISerializableContract(objectType);
			InitializeContract(jsonISerializableContract);
			if (jsonISerializableContract.IsInstantiable)
			{
				global::System.Reflection.ConstructorInfo constructor = jsonISerializableContract.NonNullableUnderlyingType.GetConstructor(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic, null, new global::System.Type[2]
				{
					typeof(global::System.Runtime.Serialization.SerializationInfo),
					typeof(global::System.Runtime.Serialization.StreamingContext)
				}, null);
				if (constructor != null)
				{
					global::Newtonsoft.Json.Serialization.ObjectConstructor<object> iSerializableCreator = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
					jsonISerializableContract.ISerializableCreator = iSerializableCreator;
				}
			}
			return jsonISerializableContract;
		}

		protected virtual global::Newtonsoft.Json.Serialization.JsonDynamicContract CreateDynamicContract(global::System.Type objectType)
		{
			global::Newtonsoft.Json.Serialization.JsonDynamicContract jsonDynamicContract = new global::Newtonsoft.Json.Serialization.JsonDynamicContract(objectType);
			InitializeContract(jsonDynamicContract);
			global::Newtonsoft.Json.JsonContainerAttribute attribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::Newtonsoft.Json.JsonContainerAttribute>(objectType);
			if (attribute?.NamingStrategyType != null)
			{
				global::Newtonsoft.Json.Serialization.NamingStrategy namingStrategy = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetContainerNamingStrategy(attribute);
				jsonDynamicContract.PropertyNameResolver = (string s) => namingStrategy.GetDictionaryKey(s);
			}
			else
			{
				jsonDynamicContract.PropertyNameResolver = ResolveDictionaryKey;
			}
			global::Newtonsoft.Json.Utilities.CollectionUtils.AddRange(jsonDynamicContract.Properties, CreateProperties(objectType, global::Newtonsoft.Json.MemberSerialization.OptOut));
			return jsonDynamicContract;
		}

		protected virtual global::Newtonsoft.Json.Serialization.JsonStringContract CreateStringContract(global::System.Type objectType)
		{
			global::Newtonsoft.Json.Serialization.JsonStringContract jsonStringContract = new global::Newtonsoft.Json.Serialization.JsonStringContract(objectType);
			InitializeContract(jsonStringContract);
			return jsonStringContract;
		}

		protected virtual global::Newtonsoft.Json.Serialization.JsonContract CreateContract(global::System.Type objectType)
		{
			global::System.Type t = global::Newtonsoft.Json.Utilities.ReflectionUtils.EnsureNotByRefType(objectType);
			if (IsJsonPrimitiveType(t))
			{
				return CreatePrimitiveContract(objectType);
			}
			t = global::Newtonsoft.Json.Utilities.ReflectionUtils.EnsureNotNullableType(t);
			global::Newtonsoft.Json.JsonContainerAttribute cachedAttribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetCachedAttribute<global::Newtonsoft.Json.JsonContainerAttribute>(t);
			if (cachedAttribute is global::Newtonsoft.Json.JsonObjectAttribute)
			{
				return CreateObjectContract(objectType);
			}
			if (cachedAttribute is global::Newtonsoft.Json.JsonArrayAttribute)
			{
				return CreateArrayContract(objectType);
			}
			if (cachedAttribute is global::Newtonsoft.Json.JsonDictionaryAttribute)
			{
				return CreateDictionaryContract(objectType);
			}
			if (t == typeof(global::Newtonsoft.Json.Linq.JToken) || t.IsSubclassOf(typeof(global::Newtonsoft.Json.Linq.JToken)))
			{
				return CreateLinqContract(objectType);
			}
			if (global::Newtonsoft.Json.Utilities.CollectionUtils.IsDictionaryType(t))
			{
				return CreateDictionaryContract(objectType);
			}
			if (typeof(global::System.Collections.IEnumerable).IsAssignableFrom(t))
			{
				return CreateArrayContract(objectType);
			}
			if (CanConvertToString(t))
			{
				return CreateStringContract(objectType);
			}
			if (!IgnoreSerializableInterface && typeof(global::System.Runtime.Serialization.ISerializable).IsAssignableFrom(t) && global::Newtonsoft.Json.Serialization.JsonTypeReflector.IsSerializable(t))
			{
				return CreateISerializableContract(objectType);
			}
			if (typeof(global::System.Dynamic.IDynamicMetaObjectProvider).IsAssignableFrom(t))
			{
				return CreateDynamicContract(objectType);
			}
			if (IsIConvertible(t))
			{
				return CreatePrimitiveContract(t);
			}
			return CreateObjectContract(objectType);
		}

		internal static bool IsJsonPrimitiveType(global::System.Type t)
		{
			global::Newtonsoft.Json.Utilities.PrimitiveTypeCode typeCode = global::Newtonsoft.Json.Utilities.ConvertUtils.GetTypeCode(t);
			if (typeCode != global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Empty)
			{
				return typeCode != global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Object;
			}
			return false;
		}

		internal static bool IsIConvertible(global::System.Type t)
		{
			if (typeof(global::System.IConvertible).IsAssignableFrom(t) || (global::Newtonsoft.Json.Utilities.ReflectionUtils.IsNullableType(t) && typeof(global::System.IConvertible).IsAssignableFrom(global::System.Nullable.GetUnderlyingType(t))))
			{
				return !typeof(global::Newtonsoft.Json.Linq.JToken).IsAssignableFrom(t);
			}
			return false;
		}

		internal static bool CanConvertToString(global::System.Type type)
		{
			if (global::Newtonsoft.Json.Serialization.JsonTypeReflector.CanTypeDescriptorConvertString(type, out global::System.ComponentModel.TypeConverter _))
			{
				return true;
			}
			if (type == typeof(global::System.Type) || type.IsSubclassOf(typeof(global::System.Type)))
			{
				return true;
			}
			return false;
		}

		private static bool IsValidCallback(global::System.Reflection.MethodInfo method, global::System.Reflection.ParameterInfo[] parameters, global::System.Type attributeType, global::System.Reflection.MethodInfo? currentCallback, ref global::System.Type? prevAttributeType)
		{
			if (!method.IsDefined(attributeType, inherit: false))
			{
				return false;
			}
			if (currentCallback != null)
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid attribute. Both '{0}' and '{1}' in type '{2}' have '{3}'.", global::System.Globalization.CultureInfo.InvariantCulture, method, currentCallback, GetClrTypeFullName(method.DeclaringType), attributeType));
			}
			if (prevAttributeType != null)
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Invalid Callback. Method '{3}' in type '{2}' has both '{0}' and '{1}'.", global::System.Globalization.CultureInfo.InvariantCulture, prevAttributeType, attributeType, GetClrTypeFullName(method.DeclaringType), method));
			}
			if (method.IsVirtual)
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Virtual Method '{0}' of type '{1}' cannot be marked with '{2}' attribute.", global::System.Globalization.CultureInfo.InvariantCulture, method, GetClrTypeFullName(method.DeclaringType), attributeType));
			}
			if (method.ReturnType != typeof(void))
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Serialization Callback '{1}' in type '{0}' must return void.", global::System.Globalization.CultureInfo.InvariantCulture, GetClrTypeFullName(method.DeclaringType), method));
			}
			if (attributeType == typeof(global::Newtonsoft.Json.Serialization.OnErrorAttribute))
			{
				if (parameters == null || parameters.Length != 2 || parameters[0].ParameterType != typeof(global::System.Runtime.Serialization.StreamingContext) || parameters[1].ParameterType != typeof(global::Newtonsoft.Json.Serialization.ErrorContext))
				{
					throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Serialization Error Callback '{1}' in type '{0}' must have two parameters of type '{2}' and '{3}'.", global::System.Globalization.CultureInfo.InvariantCulture, GetClrTypeFullName(method.DeclaringType), method, typeof(global::System.Runtime.Serialization.StreamingContext), typeof(global::Newtonsoft.Json.Serialization.ErrorContext)));
				}
			}
			else if (parameters == null || parameters.Length != 1 || parameters[0].ParameterType != typeof(global::System.Runtime.Serialization.StreamingContext))
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Serialization Callback '{1}' in type '{0}' must have a single parameter of type '{2}'.", global::System.Globalization.CultureInfo.InvariantCulture, GetClrTypeFullName(method.DeclaringType), method, typeof(global::System.Runtime.Serialization.StreamingContext)));
			}
			prevAttributeType = attributeType;
			return true;
		}

		internal static string GetClrTypeFullName(global::System.Type type)
		{
			if (global::Newtonsoft.Json.Utilities.TypeExtensions.IsGenericTypeDefinition(type) || !global::Newtonsoft.Json.Utilities.TypeExtensions.ContainsGenericParameters(type))
			{
				return type.FullName;
			}
			return global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("{0}.{1}", global::System.Globalization.CultureInfo.InvariantCulture, type.Namespace, type.Name);
		}

		protected virtual global::System.Collections.Generic.IList<global::Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(global::System.Type type, global::Newtonsoft.Json.MemberSerialization memberSerialization)
		{
			global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> obj = GetSerializableMembers(type) ?? throw new global::Newtonsoft.Json.JsonSerializationException("Null collection of serializable members returned.");
			global::Newtonsoft.Json.DefaultJsonNameTable nameTable = GetNameTable();
			global::Newtonsoft.Json.Serialization.JsonPropertyCollection jsonPropertyCollection = new global::Newtonsoft.Json.Serialization.JsonPropertyCollection(type);
			foreach (global::System.Reflection.MemberInfo item in obj)
			{
				global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty = CreateProperty(item, memberSerialization);
				if (jsonProperty != null)
				{
					lock (nameTable)
					{
						jsonProperty.PropertyName = nameTable.Add(jsonProperty.PropertyName);
					}
					jsonPropertyCollection.AddProperty(jsonProperty);
				}
			}
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.OrderBy(jsonPropertyCollection, (global::Newtonsoft.Json.Serialization.JsonProperty p) => p.Order ?? (-1)));
		}

		internal virtual global::Newtonsoft.Json.DefaultJsonNameTable GetNameTable()
		{
			return _nameTable;
		}

		protected virtual global::Newtonsoft.Json.Serialization.IValueProvider CreateMemberValueProvider(global::System.Reflection.MemberInfo member)
		{
			return new global::Newtonsoft.Json.Serialization.ReflectionValueProvider(member);
		}

		protected virtual global::Newtonsoft.Json.Serialization.JsonProperty CreateProperty(global::System.Reflection.MemberInfo member, global::Newtonsoft.Json.MemberSerialization memberSerialization)
		{
			global::Newtonsoft.Json.Serialization.JsonProperty jsonProperty = new global::Newtonsoft.Json.Serialization.JsonProperty();
			jsonProperty.PropertyType = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetMemberUnderlyingType(member);
			jsonProperty.DeclaringType = member.DeclaringType;
			jsonProperty.ValueProvider = CreateMemberValueProvider(member);
			jsonProperty.AttributeProvider = new global::Newtonsoft.Json.Serialization.ReflectionAttributeProvider(member);
			SetPropertySettingsFromAttributes(jsonProperty, member, member.Name, member.DeclaringType, memberSerialization, out var allowNonPublicAccess);
			if (memberSerialization != global::Newtonsoft.Json.MemberSerialization.Fields)
			{
				jsonProperty.Readable = global::Newtonsoft.Json.Utilities.ReflectionUtils.CanReadMemberValue(member, allowNonPublicAccess);
				jsonProperty.Writable = global::Newtonsoft.Json.Utilities.ReflectionUtils.CanSetMemberValue(member, allowNonPublicAccess, jsonProperty.HasMemberAttribute);
			}
			else
			{
				jsonProperty.Readable = true;
				jsonProperty.Writable = true;
			}
			if (!IgnoreShouldSerializeMembers)
			{
				jsonProperty.ShouldSerialize = CreateShouldSerializeTest(member);
			}
			if (!IgnoreIsSpecifiedMembers)
			{
				SetIsSpecifiedActions(jsonProperty, member, allowNonPublicAccess);
			}
			return jsonProperty;
		}

		private void SetPropertySettingsFromAttributes(global::Newtonsoft.Json.Serialization.JsonProperty property, object attributeProvider, string name, global::System.Type declaringType, global::Newtonsoft.Json.MemberSerialization memberSerialization, out bool allowNonPublicAccess)
		{
			global::System.Runtime.Serialization.DataContractAttribute? dataContractAttribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetDataContractAttribute(declaringType);
			global::System.Reflection.MemberInfo memberInfo = attributeProvider as global::System.Reflection.MemberInfo;
			global::System.Runtime.Serialization.DataMemberAttribute dataMemberAttribute = ((dataContractAttribute == null || !(memberInfo != null)) ? null : global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetDataMemberAttribute(memberInfo));
			global::Newtonsoft.Json.JsonPropertyAttribute attribute = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::Newtonsoft.Json.JsonPropertyAttribute>(attributeProvider);
			global::Newtonsoft.Json.JsonRequiredAttribute? attribute2 = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::Newtonsoft.Json.JsonRequiredAttribute>(attributeProvider);
			string text;
			bool hasSpecifiedName;
			if (attribute != null && attribute.PropertyName != null)
			{
				text = attribute.PropertyName;
				hasSpecifiedName = true;
			}
			else if (dataMemberAttribute != null && dataMemberAttribute.Name != null)
			{
				text = dataMemberAttribute.Name;
				hasSpecifiedName = true;
			}
			else
			{
				text = name;
				hasSpecifiedName = false;
			}
			global::Newtonsoft.Json.JsonContainerAttribute attribute3 = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::Newtonsoft.Json.JsonContainerAttribute>(declaringType);
			global::Newtonsoft.Json.Serialization.NamingStrategy namingStrategy = ((attribute?.NamingStrategyType != null) ? global::Newtonsoft.Json.Serialization.JsonTypeReflector.CreateNamingStrategyInstance(attribute.NamingStrategyType, attribute.NamingStrategyParameters) : ((!(attribute3?.NamingStrategyType != null)) ? NamingStrategy : global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetContainerNamingStrategy(attribute3)));
			if (namingStrategy != null)
			{
				property.PropertyName = namingStrategy.GetPropertyName(text, hasSpecifiedName);
			}
			else
			{
				property.PropertyName = ResolvePropertyName(text);
			}
			property.UnderlyingName = name;
			bool flag = false;
			if (attribute != null)
			{
				property._required = attribute._required;
				property.Order = attribute._order;
				property.DefaultValueHandling = attribute._defaultValueHandling;
				flag = true;
				property.NullValueHandling = attribute._nullValueHandling;
				property.ReferenceLoopHandling = attribute._referenceLoopHandling;
				property.ObjectCreationHandling = attribute._objectCreationHandling;
				property.TypeNameHandling = attribute._typeNameHandling;
				property.IsReference = attribute._isReference;
				property.ItemIsReference = attribute._itemIsReference;
				property.ItemConverter = ((attribute.ItemConverterType != null) ? global::Newtonsoft.Json.Serialization.JsonTypeReflector.CreateJsonConverterInstance(attribute.ItemConverterType, attribute.ItemConverterParameters) : null);
				property.ItemReferenceLoopHandling = attribute._itemReferenceLoopHandling;
				property.ItemTypeNameHandling = attribute._itemTypeNameHandling;
			}
			else
			{
				property.NullValueHandling = null;
				property.ReferenceLoopHandling = null;
				property.ObjectCreationHandling = null;
				property.TypeNameHandling = null;
				property.IsReference = null;
				property.ItemIsReference = null;
				property.ItemConverter = null;
				property.ItemReferenceLoopHandling = null;
				property.ItemTypeNameHandling = null;
				if (dataMemberAttribute != null)
				{
					property._required = (dataMemberAttribute.IsRequired ? global::Newtonsoft.Json.Required.AllowNull : global::Newtonsoft.Json.Required.Default);
					property.Order = ((dataMemberAttribute.Order != -1) ? new int?(dataMemberAttribute.Order) : ((int?)null));
					property.DefaultValueHandling = ((!dataMemberAttribute.EmitDefaultValue) ? new global::Newtonsoft.Json.DefaultValueHandling?(global::Newtonsoft.Json.DefaultValueHandling.Ignore) : ((global::Newtonsoft.Json.DefaultValueHandling?)null));
					flag = true;
				}
			}
			if (attribute2 != null)
			{
				property._required = global::Newtonsoft.Json.Required.Always;
				flag = true;
			}
			property.HasMemberAttribute = flag;
			bool flag2 = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::Newtonsoft.Json.JsonIgnoreAttribute>(attributeProvider) != null || global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::Newtonsoft.Json.JsonExtensionDataAttribute>(attributeProvider) != null || global::Newtonsoft.Json.Serialization.JsonTypeReflector.IsNonSerializable(attributeProvider);
			if (memberSerialization != global::Newtonsoft.Json.MemberSerialization.OptIn)
			{
				bool flag3 = false;
				flag3 = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::System.Runtime.Serialization.IgnoreDataMemberAttribute>(attributeProvider) != null;
				property.Ignored = flag2 || flag3;
			}
			else
			{
				property.Ignored = flag2 || !flag;
			}
			property.Converter = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetJsonConverter(attributeProvider);
			global::System.ComponentModel.DefaultValueAttribute attribute4 = global::Newtonsoft.Json.Serialization.JsonTypeReflector.GetAttribute<global::System.ComponentModel.DefaultValueAttribute>(attributeProvider);
			if (attribute4 != null)
			{
				property.DefaultValue = attribute4.Value;
			}
			allowNonPublicAccess = false;
			if ((DefaultMembersSearchFlags & global::System.Reflection.BindingFlags.NonPublic) == global::System.Reflection.BindingFlags.NonPublic)
			{
				allowNonPublicAccess = true;
			}
			if (flag)
			{
				allowNonPublicAccess = true;
			}
			if (memberSerialization == global::Newtonsoft.Json.MemberSerialization.Fields)
			{
				allowNonPublicAccess = true;
			}
		}

		private global::System.Predicate<object>? CreateShouldSerializeTest(global::System.Reflection.MemberInfo member)
		{
			global::System.Reflection.MethodInfo method = member.DeclaringType.GetMethod("ShouldSerialize" + member.Name, global::Newtonsoft.Json.Utilities.ReflectionUtils.EmptyTypes);
			if (method == null || method.ReturnType != typeof(bool))
			{
				return null;
			}
			global::Newtonsoft.Json.Utilities.MethodCall<object, object?> shouldSerializeCall = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return (object o) => (bool)shouldSerializeCall(o);
		}

		private void SetIsSpecifiedActions(global::Newtonsoft.Json.Serialization.JsonProperty property, global::System.Reflection.MemberInfo member, bool allowNonPublicAccess)
		{
			global::System.Reflection.MemberInfo memberInfo = member.DeclaringType.GetProperty(member.Name + "Specified", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
			if (memberInfo == null)
			{
				memberInfo = member.DeclaringType.GetField(member.Name + "Specified", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
			}
			if (!(memberInfo == null) && !(global::Newtonsoft.Json.Utilities.ReflectionUtils.GetMemberUnderlyingType(memberInfo) != typeof(bool)))
			{
				global::System.Func<object, object> specifiedPropertyGet = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(memberInfo);
				property.GetIsSpecified = (object o) => (bool)specifiedPropertyGet(o);
				if (global::Newtonsoft.Json.Utilities.ReflectionUtils.CanSetMemberValue(memberInfo, allowNonPublicAccess, canSetReadOnly: false))
				{
					property.SetIsSpecified = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateSet<object>(memberInfo);
				}
			}
		}

		protected virtual string ResolvePropertyName(string propertyName)
		{
			if (NamingStrategy != null)
			{
				return NamingStrategy.GetPropertyName(propertyName, hasSpecifiedName: false);
			}
			return propertyName;
		}

		protected virtual string ResolveExtensionDataName(string extensionDataName)
		{
			if (NamingStrategy != null)
			{
				return NamingStrategy.GetExtensionDataName(extensionDataName);
			}
			return extensionDataName;
		}

		protected virtual string ResolveDictionaryKey(string dictionaryKey)
		{
			if (NamingStrategy != null)
			{
				return NamingStrategy.GetDictionaryKey(dictionaryKey);
			}
			return ResolvePropertyName(dictionaryKey);
		}

		public string GetResolvedPropertyName(string propertyName)
		{
			return ResolvePropertyName(propertyName);
		}
	}
}
