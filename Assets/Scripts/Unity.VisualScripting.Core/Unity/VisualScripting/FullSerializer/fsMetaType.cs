namespace Unity.VisualScripting.FullSerializer
{
	public class fsMetaType
	{
		public global::System.Type ReflectedType;

		private bool _hasEmittedAotData;

		private bool? _hasDefaultConstructorCache;

		private bool _isDefaultConstructorPublic;

		private static global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.FullSerializer.fsConfig, global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsMetaType>> _configMetaTypes = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.FullSerializer.fsConfig, global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsMetaType>>();

		public global::Unity.VisualScripting.FullSerializer.fsMetaProperty[] Properties { get; private set; }

		public bool HasDefaultConstructor
		{
			get
			{
				if (!_hasDefaultConstructorCache.HasValue)
				{
					if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(ReflectedType).IsArray)
					{
						_hasDefaultConstructorCache = true;
						_isDefaultConstructorPublic = true;
					}
					else if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(ReflectedType).IsValueType)
					{
						_hasDefaultConstructorCache = true;
						_isDefaultConstructorPublic = true;
					}
					else
					{
						global::System.Reflection.ConstructorInfo declaredConstructor = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetDeclaredConstructor(ReflectedType, global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.EmptyTypes);
						_hasDefaultConstructorCache = declaredConstructor != null;
						if (declaredConstructor != null)
						{
							_isDefaultConstructorPublic = declaredConstructor.IsPublic;
						}
					}
				}
				return _hasDefaultConstructorCache.Value;
			}
		}

		private fsMetaType(global::Unity.VisualScripting.FullSerializer.fsConfig config, global::System.Type reflectedType)
		{
			ReflectedType = reflectedType;
			global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsMetaProperty> list = new global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsMetaProperty>();
			CollectProperties(config, list, reflectedType);
			Properties = list.ToArray();
		}

		public bool EmitAotData()
		{
			if (!_hasEmittedAotData)
			{
				_hasEmittedAotData = true;
				for (int i = 0; i < Properties.Length; i++)
				{
					if (!Properties[i].IsPublic)
					{
						return false;
					}
					if (Properties[i].IsReadOnly)
					{
						return false;
					}
				}
				if (!HasDefaultConstructor)
				{
					return false;
				}
				global::Unity.VisualScripting.FullSerializer.fsAotCompilationManager.AddAotCompilation(ReflectedType, Properties, _isDefaultConstructorPublic);
				return true;
			}
			return false;
		}

		public object CreateInstance()
		{
			if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(ReflectedType).IsInterface || global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(ReflectedType).IsAbstract)
			{
				throw new global::System.Exception("Cannot create an instance of an interface or abstract type for " + ReflectedType);
			}
			if (typeof(global::UnityEngine.ScriptableObject).IsAssignableFrom(ReflectedType))
			{
				return global::UnityEngine.ScriptableObject.CreateInstance(ReflectedType);
			}
			if (typeof(string) == ReflectedType)
			{
				return string.Empty;
			}
			if (!HasDefaultConstructor)
			{
				return global::System.Runtime.Serialization.FormatterServices.GetSafeUninitializedObject(ReflectedType);
			}
			if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(ReflectedType).IsArray)
			{
				return global::System.Array.CreateInstance(ReflectedType.GetElementType(), 0);
			}
			try
			{
				return global::System.Activator.CreateInstance(ReflectedType, nonPublic: true);
			}
			catch (global::System.MissingMethodException innerException)
			{
				throw new global::System.InvalidOperationException("Unable to create instance of " + ReflectedType?.ToString() + "; there is no default constructor", innerException);
			}
			catch (global::System.Reflection.TargetInvocationException innerException2)
			{
				throw new global::System.InvalidOperationException("Constructor of " + ReflectedType?.ToString() + " threw an exception when creating an instance", innerException2);
			}
			catch (global::System.MemberAccessException innerException3)
			{
				throw new global::System.InvalidOperationException("Unable to access constructor of " + ReflectedType, innerException3);
			}
		}

		public static global::Unity.VisualScripting.FullSerializer.fsMetaType Get(global::Unity.VisualScripting.FullSerializer.fsConfig config, global::System.Type type)
		{
			global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsMetaType> value;
			lock (typeof(global::Unity.VisualScripting.FullSerializer.fsMetaType))
			{
				if (!_configMetaTypes.TryGetValue(config, out value))
				{
					global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsMetaType> dictionary = (_configMetaTypes[config] = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsMetaType>());
					value = dictionary;
				}
			}
			if (!value.TryGetValue(type, out var value2))
			{
				value2 = (value[type] = new global::Unity.VisualScripting.FullSerializer.fsMetaType(config, type));
			}
			return value2;
		}

		public static void ClearCache()
		{
			lock (typeof(global::Unity.VisualScripting.FullSerializer.fsMetaType))
			{
				_configMetaTypes = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.FullSerializer.fsConfig, global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.fsMetaType>>();
			}
		}

		private static void CollectProperties(global::Unity.VisualScripting.FullSerializer.fsConfig config, global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.fsMetaProperty> properties, global::System.Type reflectedType)
		{
			bool flag = config.DefaultMemberSerialization == global::Unity.VisualScripting.FullSerializer.fsMemberSerialization.OptIn;
			bool flag2 = config.DefaultMemberSerialization == global::Unity.VisualScripting.FullSerializer.fsMemberSerialization.OptOut;
			global::Unity.VisualScripting.FullSerializer.fsObjectAttribute attribute = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetAttribute<global::Unity.VisualScripting.FullSerializer.fsObjectAttribute>(reflectedType);
			if (attribute != null)
			{
				flag = attribute.MemberSerialization == global::Unity.VisualScripting.FullSerializer.fsMemberSerialization.OptIn;
				flag2 = attribute.MemberSerialization == global::Unity.VisualScripting.FullSerializer.fsMemberSerialization.OptOut;
			}
			global::System.Reflection.MemberInfo[] declaredMembers = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetDeclaredMembers(reflectedType);
			global::System.Reflection.MemberInfo[] array = declaredMembers;
			foreach (global::System.Reflection.MemberInfo member in array)
			{
				if (global::System.Linq.Enumerable.Any(config.IgnoreSerializeAttributes, (global::System.Type t) => global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.HasAttribute(member, t)))
				{
					continue;
				}
				global::System.Reflection.PropertyInfo propertyInfo = member as global::System.Reflection.PropertyInfo;
				global::System.Reflection.FieldInfo fieldInfo = member as global::System.Reflection.FieldInfo;
				if ((propertyInfo == null && fieldInfo == null) || (propertyInfo != null && !config.EnablePropertySerialization) || (flag && !global::System.Linq.Enumerable.Any(config.SerializeAttributes, (global::System.Type t) => global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.HasAttribute(member, t))) || (flag2 && global::System.Linq.Enumerable.Any(config.IgnoreSerializeAttributes, (global::System.Type t) => global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.HasAttribute(member, t))))
				{
					continue;
				}
				if (propertyInfo != null)
				{
					if (CanSerializeProperty(config, propertyInfo, declaredMembers, flag2))
					{
						properties.Add(new global::Unity.VisualScripting.FullSerializer.fsMetaProperty(config, propertyInfo));
					}
				}
				else if (fieldInfo != null && CanSerializeField(config, fieldInfo, flag2))
				{
					properties.Add(new global::Unity.VisualScripting.FullSerializer.fsMetaProperty(config, fieldInfo));
				}
			}
			if (global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(reflectedType).BaseType != null)
			{
				CollectProperties(config, properties, global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.Resolve(reflectedType).BaseType);
			}
		}

		private static bool IsAutoProperty(global::System.Reflection.PropertyInfo property, global::System.Reflection.MemberInfo[] members)
		{
			if (property.CanWrite && property.CanRead)
			{
				return global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.HasAttribute(property.GetGetMethod(), typeof(global::System.Runtime.CompilerServices.CompilerGeneratedAttribute), shouldCache: false);
			}
			return false;
		}

		private static bool CanSerializeProperty(global::Unity.VisualScripting.FullSerializer.fsConfig config, global::System.Reflection.PropertyInfo property, global::System.Reflection.MemberInfo[] members, bool annotationFreeValue)
		{
			if (typeof(global::System.Delegate).IsAssignableFrom(property.PropertyType))
			{
				return false;
			}
			global::System.Reflection.MethodInfo getMethod = property.GetGetMethod(nonPublic: false);
			global::System.Reflection.MethodInfo setMethod = property.GetSetMethod(nonPublic: false);
			if ((getMethod != null && getMethod.IsStatic) || (setMethod != null && setMethod.IsStatic))
			{
				return false;
			}
			if (property.GetIndexParameters().Length != 0)
			{
				return false;
			}
			if (global::System.Linq.Enumerable.Any(config.SerializeAttributes, (global::System.Type t) => global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.HasAttribute(property, t)))
			{
				return true;
			}
			if (!property.CanRead || !property.CanWrite)
			{
				return false;
			}
			if (getMethod != null && (config.SerializeNonPublicSetProperties || setMethod != null) && (config.SerializeNonAutoProperties || IsAutoProperty(property, members)))
			{
				return true;
			}
			return annotationFreeValue;
		}

		private static bool CanSerializeField(global::Unity.VisualScripting.FullSerializer.fsConfig config, global::System.Reflection.FieldInfo field, bool annotationFreeValue)
		{
			if (typeof(global::System.Delegate).IsAssignableFrom(field.FieldType))
			{
				return false;
			}
			if (global::System.Attribute.IsDefined(field, typeof(global::System.Runtime.CompilerServices.CompilerGeneratedAttribute), inherit: false))
			{
				return false;
			}
			if (field.IsStatic)
			{
				return false;
			}
			if (global::System.Linq.Enumerable.Any(config.SerializeAttributes, (global::System.Type t) => global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.HasAttribute(field, t)))
			{
				return true;
			}
			if (!annotationFreeValue && !field.IsPublic)
			{
				return false;
			}
			return true;
		}
	}
}
