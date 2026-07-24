namespace Newtonsoft.Json.Utilities
{
	internal static class ReflectionUtils
	{
		public static readonly global::System.Type[] EmptyTypes;

		static ReflectionUtils()
		{
			EmptyTypes = global::System.Type.EmptyTypes;
		}

		public static bool IsVirtual(this global::System.Reflection.PropertyInfo propertyInfo)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			global::System.Reflection.MethodInfo getMethod = propertyInfo.GetGetMethod(nonPublic: true);
			if (getMethod != null && getMethod.IsVirtual)
			{
				return true;
			}
			getMethod = propertyInfo.GetSetMethod(nonPublic: true);
			if (getMethod != null && getMethod.IsVirtual)
			{
				return true;
			}
			return false;
		}

		public static global::System.Reflection.MethodInfo? GetBaseDefinition(this global::System.Reflection.PropertyInfo propertyInfo)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			global::System.Reflection.MethodInfo getMethod = propertyInfo.GetGetMethod(nonPublic: true);
			if (getMethod != null)
			{
				return getMethod.GetBaseDefinition();
			}
			return propertyInfo.GetSetMethod(nonPublic: true)?.GetBaseDefinition();
		}

		public static bool IsPublic(global::System.Reflection.PropertyInfo property)
		{
			global::System.Reflection.MethodInfo getMethod = property.GetGetMethod();
			if (getMethod != null && getMethod.IsPublic)
			{
				return true;
			}
			global::System.Reflection.MethodInfo setMethod = property.GetSetMethod();
			if (setMethod != null && setMethod.IsPublic)
			{
				return true;
			}
			return false;
		}

		public static global::System.Type? GetObjectType(object? v)
		{
			return v?.GetType();
		}

		public static string GetTypeName(global::System.Type t, global::Newtonsoft.Json.TypeNameAssemblyFormatHandling assemblyFormat, global::Newtonsoft.Json.Serialization.ISerializationBinder? binder)
		{
			string fullyQualifiedTypeName = GetFullyQualifiedTypeName(t, binder);
			return assemblyFormat switch
			{
				global::Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple => RemoveAssemblyDetails(fullyQualifiedTypeName), 
				global::Newtonsoft.Json.TypeNameAssemblyFormatHandling.Full => fullyQualifiedTypeName, 
				_ => throw new global::System.ArgumentOutOfRangeException(), 
			};
		}

		private static string GetFullyQualifiedTypeName(global::System.Type t, global::Newtonsoft.Json.Serialization.ISerializationBinder? binder)
		{
			if (binder != null)
			{
				binder.BindToName(t, out string assemblyName, out string typeName);
				return typeName + ((assemblyName == null) ? "" : (", " + assemblyName));
			}
			return t.AssemblyQualifiedName;
		}

		private static string RemoveAssemblyDetails(string fullyQualifiedTypeName)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (char c in fullyQualifiedTypeName)
			{
				switch (c)
				{
				case '[':
					flag = false;
					flag2 = false;
					flag3 = true;
					stringBuilder.Append(c);
					break;
				case ']':
					flag = false;
					flag2 = false;
					flag3 = false;
					stringBuilder.Append(c);
					break;
				case ',':
					if (flag3)
					{
						stringBuilder.Append(c);
					}
					else if (!flag)
					{
						flag = true;
						stringBuilder.Append(c);
					}
					else
					{
						flag2 = true;
					}
					break;
				default:
					flag3 = false;
					if (!flag2)
					{
						stringBuilder.Append(c);
					}
					break;
				}
			}
			return stringBuilder.ToString();
		}

		public static bool HasDefaultConstructor(global::System.Type t, bool nonPublic)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(t, "t");
			if (t.IsValueType())
			{
				return true;
			}
			return GetDefaultConstructor(t, nonPublic) != null;
		}

		public static global::System.Reflection.ConstructorInfo? GetDefaultConstructor(global::System.Type t)
		{
			return GetDefaultConstructor(t, nonPublic: false);
		}

		public static global::System.Reflection.ConstructorInfo? GetDefaultConstructor(global::System.Type t, bool nonPublic)
		{
			global::System.Reflection.BindingFlags bindingFlags = global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public;
			if (nonPublic)
			{
				bindingFlags |= global::System.Reflection.BindingFlags.NonPublic;
			}
			return global::System.Linq.Enumerable.SingleOrDefault(t.GetConstructors(bindingFlags), (global::System.Reflection.ConstructorInfo c) => !global::System.Linq.Enumerable.Any(c.GetParameters()));
		}

		public static bool IsNullable(global::System.Type t)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(t, "t");
			if (t.IsValueType())
			{
				return IsNullableType(t);
			}
			return true;
		}

		public static bool IsNullableType(global::System.Type t)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(t, "t");
			if (t.IsGenericType())
			{
				return t.GetGenericTypeDefinition() == typeof(global::System.Nullable<>);
			}
			return false;
		}

		public static global::System.Type EnsureNotNullableType(global::System.Type t)
		{
			if (!IsNullableType(t))
			{
				return t;
			}
			return global::System.Nullable.GetUnderlyingType(t);
		}

		public static global::System.Type EnsureNotByRefType(global::System.Type t)
		{
			if (!t.IsByRef || !t.HasElementType)
			{
				return t;
			}
			return t.GetElementType();
		}

		public static bool IsGenericDefinition(global::System.Type type, global::System.Type genericInterfaceDefinition)
		{
			if (!type.IsGenericType())
			{
				return false;
			}
			return type.GetGenericTypeDefinition() == genericInterfaceDefinition;
		}

		public static bool ImplementsGenericDefinition(global::System.Type type, global::System.Type genericInterfaceDefinition)
		{
			global::System.Type implementingType;
			return ImplementsGenericDefinition(type, genericInterfaceDefinition, out implementingType);
		}

		public static bool ImplementsGenericDefinition(global::System.Type type, global::System.Type genericInterfaceDefinition, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::System.Type? implementingType)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(type, "type");
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(genericInterfaceDefinition, "genericInterfaceDefinition");
			if (!genericInterfaceDefinition.IsInterface() || !genericInterfaceDefinition.IsGenericTypeDefinition())
			{
				throw new global::System.ArgumentNullException("'{0}' is not a generic interface definition.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, genericInterfaceDefinition));
			}
			if (type.IsInterface() && type.IsGenericType())
			{
				global::System.Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericInterfaceDefinition == genericTypeDefinition)
				{
					implementingType = type;
					return true;
				}
			}
			global::System.Type[] interfaces = type.GetInterfaces();
			foreach (global::System.Type type2 in interfaces)
			{
				if (type2.IsGenericType())
				{
					global::System.Type genericTypeDefinition2 = type2.GetGenericTypeDefinition();
					if (genericInterfaceDefinition == genericTypeDefinition2)
					{
						implementingType = type2;
						return true;
					}
				}
			}
			implementingType = null;
			return false;
		}

		public static bool InheritsGenericDefinition(global::System.Type type, global::System.Type genericClassDefinition)
		{
			global::System.Type implementingType;
			return InheritsGenericDefinition(type, genericClassDefinition, out implementingType);
		}

		public static bool InheritsGenericDefinition(global::System.Type type, global::System.Type genericClassDefinition, out global::System.Type? implementingType)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(type, "type");
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(genericClassDefinition, "genericClassDefinition");
			if (!genericClassDefinition.IsClass() || !genericClassDefinition.IsGenericTypeDefinition())
			{
				throw new global::System.ArgumentNullException("'{0}' is not a generic class definition.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, genericClassDefinition));
			}
			return InheritsGenericDefinitionInternal(type, genericClassDefinition, out implementingType);
		}

		private static bool InheritsGenericDefinitionInternal(global::System.Type type, global::System.Type genericClassDefinition, out global::System.Type? implementingType)
		{
			global::System.Type type2 = type;
			do
			{
				if (type2.IsGenericType() && genericClassDefinition == type2.GetGenericTypeDefinition())
				{
					implementingType = type2;
					return true;
				}
				type2 = type2.BaseType();
			}
			while (type2 != null);
			implementingType = null;
			return false;
		}

		public static global::System.Type? GetCollectionItemType(global::System.Type type)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			if (ImplementsGenericDefinition(type, typeof(global::System.Collections.Generic.IEnumerable<>), out global::System.Type implementingType))
			{
				if (implementingType.IsGenericTypeDefinition())
				{
					throw new global::System.Exception("Type {0} is not a collection.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, type));
				}
				return implementingType.GetGenericArguments()[0];
			}
			if (typeof(global::System.Collections.IEnumerable).IsAssignableFrom(type))
			{
				return null;
			}
			throw new global::System.Exception("Type {0} is not a collection.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, type));
		}

		public static void GetDictionaryKeyValueTypes(global::System.Type dictionaryType, out global::System.Type? keyType, out global::System.Type? valueType)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(dictionaryType, "dictionaryType");
			if (ImplementsGenericDefinition(dictionaryType, typeof(global::System.Collections.Generic.IDictionary<, >), out global::System.Type implementingType))
			{
				if (implementingType.IsGenericTypeDefinition())
				{
					throw new global::System.Exception("Type {0} is not a dictionary.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, dictionaryType));
				}
				global::System.Type[] genericArguments = implementingType.GetGenericArguments();
				keyType = genericArguments[0];
				valueType = genericArguments[1];
			}
			else
			{
				if (!typeof(global::System.Collections.IDictionary).IsAssignableFrom(dictionaryType))
				{
					throw new global::System.Exception("Type {0} is not a dictionary.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, dictionaryType));
				}
				keyType = null;
				valueType = null;
			}
		}

		public static global::System.Type GetMemberUnderlyingType(global::System.Reflection.MemberInfo member)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(member, "member");
			return member.MemberType() switch
			{
				global::System.Reflection.MemberTypes.Field => ((global::System.Reflection.FieldInfo)member).FieldType, 
				global::System.Reflection.MemberTypes.Property => ((global::System.Reflection.PropertyInfo)member).PropertyType, 
				global::System.Reflection.MemberTypes.Event => ((global::System.Reflection.EventInfo)member).EventHandlerType, 
				global::System.Reflection.MemberTypes.Method => ((global::System.Reflection.MethodInfo)member).ReturnType, 
				_ => throw new global::System.ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo, EventInfo or MethodInfo", "member"), 
			};
		}

		public static bool IsByRefLikeType(global::System.Type type)
		{
			if (!type.IsValueType())
			{
				return false;
			}
			global::System.Attribute[] attributes = GetAttributes(type, null, inherit: false);
			for (int i = 0; i < attributes.Length; i++)
			{
				if (string.Equals(attributes[i].GetType().FullName, "System.Runtime.CompilerServices.IsByRefLikeAttribute", global::System.StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsIndexedProperty(global::System.Reflection.PropertyInfo property)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(property, "property");
			return property.GetIndexParameters().Length != 0;
		}

		public static object? GetMemberValue(global::System.Reflection.MemberInfo member, object target)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(member, "member");
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(target, "target");
			switch (member.MemberType())
			{
			case global::System.Reflection.MemberTypes.Field:
				return ((global::System.Reflection.FieldInfo)member).GetValue(target);
			case global::System.Reflection.MemberTypes.Property:
				try
				{
					return ((global::System.Reflection.PropertyInfo)member).GetValue(target, null);
				}
				catch (global::System.Reflection.TargetParameterCountException innerException)
				{
					throw new global::System.ArgumentException("MemberInfo '{0}' has index parameters".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, member.Name), innerException);
				}
			default:
				throw new global::System.ArgumentException("MemberInfo '{0}' is not of type FieldInfo or PropertyInfo".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, member.Name), "member");
			}
		}

		public static void SetMemberValue(global::System.Reflection.MemberInfo member, object target, object? value)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(member, "member");
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(target, "target");
			switch (member.MemberType())
			{
			case global::System.Reflection.MemberTypes.Field:
				((global::System.Reflection.FieldInfo)member).SetValue(target, value);
				break;
			case global::System.Reflection.MemberTypes.Property:
				((global::System.Reflection.PropertyInfo)member).SetValue(target, value, null);
				break;
			default:
				throw new global::System.ArgumentException("MemberInfo '{0}' must be of type FieldInfo or PropertyInfo".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, member.Name), "member");
			}
		}

		public static bool CanReadMemberValue(global::System.Reflection.MemberInfo member, bool nonPublic)
		{
			switch (member.MemberType())
			{
			case global::System.Reflection.MemberTypes.Field:
			{
				global::System.Reflection.FieldInfo fieldInfo = (global::System.Reflection.FieldInfo)member;
				if (nonPublic)
				{
					return true;
				}
				if (fieldInfo.IsPublic)
				{
					return true;
				}
				return false;
			}
			case global::System.Reflection.MemberTypes.Property:
			{
				global::System.Reflection.PropertyInfo propertyInfo = (global::System.Reflection.PropertyInfo)member;
				if (!propertyInfo.CanRead)
				{
					return false;
				}
				if (nonPublic)
				{
					return true;
				}
				return propertyInfo.GetGetMethod(nonPublic) != null;
			}
			default:
				return false;
			}
		}

		public static bool CanSetMemberValue(global::System.Reflection.MemberInfo member, bool nonPublic, bool canSetReadOnly)
		{
			switch (member.MemberType())
			{
			case global::System.Reflection.MemberTypes.Field:
			{
				global::System.Reflection.FieldInfo fieldInfo = (global::System.Reflection.FieldInfo)member;
				if (fieldInfo.IsLiteral)
				{
					return false;
				}
				if (fieldInfo.IsInitOnly && !canSetReadOnly)
				{
					return false;
				}
				if (nonPublic)
				{
					return true;
				}
				if (fieldInfo.IsPublic)
				{
					return true;
				}
				return false;
			}
			case global::System.Reflection.MemberTypes.Property:
			{
				global::System.Reflection.PropertyInfo propertyInfo = (global::System.Reflection.PropertyInfo)member;
				if (!propertyInfo.CanWrite)
				{
					return false;
				}
				if (nonPublic)
				{
					return true;
				}
				return propertyInfo.GetSetMethod(nonPublic) != null;
			}
			default:
				return false;
			}
		}

		public static global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> GetFieldsAndProperties(global::System.Type type, global::System.Reflection.BindingFlags bindingAttr)
		{
			global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> list = new global::System.Collections.Generic.List<global::System.Reflection.MemberInfo>();
			list.AddRange(GetFields(type, bindingAttr));
			list.AddRange(GetProperties(type, bindingAttr));
			global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> list2 = new global::System.Collections.Generic.List<global::System.Reflection.MemberInfo>(list.Count);
			foreach (global::System.Linq.IGrouping<string, global::System.Reflection.MemberInfo> item in global::System.Linq.Enumerable.GroupBy(list, (global::System.Reflection.MemberInfo m) => m.Name))
			{
				if (global::System.Linq.Enumerable.Count(item) == 1)
				{
					list2.Add(global::System.Linq.Enumerable.First(item));
					continue;
				}
				global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> list3 = new global::System.Collections.Generic.List<global::System.Reflection.MemberInfo>();
				foreach (global::System.Reflection.MemberInfo memberInfo in item)
				{
					if (list3.Count == 0)
					{
						list3.Add(memberInfo);
					}
					else if ((!IsOverridenGenericMember(memberInfo, bindingAttr) || memberInfo.Name == "Item") && !global::System.Linq.Enumerable.Any(list3, (global::System.Reflection.MemberInfo m) => m.DeclaringType == memberInfo.DeclaringType))
					{
						list3.Add(memberInfo);
					}
				}
				list2.AddRange(list3);
			}
			return list2;
		}

		private static bool IsOverridenGenericMember(global::System.Reflection.MemberInfo memberInfo, global::System.Reflection.BindingFlags bindingAttr)
		{
			if (memberInfo.MemberType() != global::System.Reflection.MemberTypes.Property)
			{
				return false;
			}
			global::System.Reflection.PropertyInfo propertyInfo = (global::System.Reflection.PropertyInfo)memberInfo;
			if (!propertyInfo.IsVirtual())
			{
				return false;
			}
			global::System.Type declaringType = propertyInfo.DeclaringType;
			if (!declaringType.IsGenericType())
			{
				return false;
			}
			global::System.Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
			if (genericTypeDefinition == null)
			{
				return false;
			}
			global::System.Reflection.MemberInfo[] member = genericTypeDefinition.GetMember(propertyInfo.Name, bindingAttr);
			if (member.Length == 0)
			{
				return false;
			}
			if (!GetMemberUnderlyingType(member[0]).IsGenericParameter)
			{
				return false;
			}
			return true;
		}

		public static T? GetAttribute<T>(object attributeProvider) where T : global::System.Attribute
		{
			return GetAttribute<T>(attributeProvider, inherit: true);
		}

		public static T? GetAttribute<T>(object attributeProvider, bool inherit) where T : global::System.Attribute
		{
			T[] attributes = GetAttributes<T>(attributeProvider, inherit);
			if (attributes == null)
			{
				return null;
			}
			return global::System.Linq.Enumerable.FirstOrDefault(attributes);
		}

		public static T[] GetAttributes<T>(object attributeProvider, bool inherit) where T : global::System.Attribute
		{
			global::System.Attribute[] attributes = GetAttributes(attributeProvider, typeof(T), inherit);
			if (attributes is T[] result)
			{
				return result;
			}
			return global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Cast<T>(attributes));
		}

		public static global::System.Attribute[] GetAttributes(object attributeProvider, global::System.Type? attributeType, bool inherit)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			if (!(attributeProvider is global::System.Type type))
			{
				if (!(attributeProvider is global::System.Reflection.Assembly element))
				{
					if (!(attributeProvider is global::System.Reflection.MemberInfo element2))
					{
						if (!(attributeProvider is global::System.Reflection.Module element3))
						{
							if (attributeProvider is global::System.Reflection.ParameterInfo element4)
							{
								if (!(attributeType != null))
								{
									return global::System.Attribute.GetCustomAttributes(element4, inherit);
								}
								return global::System.Attribute.GetCustomAttributes(element4, attributeType, inherit);
							}
							global::System.Reflection.ICustomAttributeProvider customAttributeProvider = (global::System.Reflection.ICustomAttributeProvider)attributeProvider;
							return (global::System.Attribute[])((attributeType != null) ? customAttributeProvider.GetCustomAttributes(attributeType, inherit) : customAttributeProvider.GetCustomAttributes(inherit));
						}
						if (!(attributeType != null))
						{
							return global::System.Attribute.GetCustomAttributes(element3, inherit);
						}
						return global::System.Attribute.GetCustomAttributes(element3, attributeType, inherit);
					}
					if (!(attributeType != null))
					{
						return global::System.Attribute.GetCustomAttributes(element2, inherit);
					}
					return global::System.Attribute.GetCustomAttributes(element2, attributeType, inherit);
				}
				if (!(attributeType != null))
				{
					return global::System.Attribute.GetCustomAttributes(element);
				}
				return global::System.Attribute.GetCustomAttributes(element, attributeType);
			}
			return global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Cast<global::System.Attribute>((attributeType != null) ? type.GetCustomAttributes(attributeType, inherit) : type.GetCustomAttributes(inherit)));
		}

		public static global::Newtonsoft.Json.Utilities.StructMultiKey<string?, string> SplitFullyQualifiedTypeName(string fullyQualifiedTypeName)
		{
			int? assemblyDelimiterIndex = GetAssemblyDelimiterIndex(fullyQualifiedTypeName);
			string v;
			string v2;
			if (assemblyDelimiterIndex.HasValue)
			{
				v = fullyQualifiedTypeName.Trim(0, assemblyDelimiterIndex.GetValueOrDefault());
				v2 = fullyQualifiedTypeName.Trim(assemblyDelimiterIndex.GetValueOrDefault() + 1, fullyQualifiedTypeName.Length - assemblyDelimiterIndex.GetValueOrDefault() - 1);
			}
			else
			{
				v = fullyQualifiedTypeName;
				v2 = null;
			}
			return new global::Newtonsoft.Json.Utilities.StructMultiKey<string, string>(v2, v);
		}

		private static int? GetAssemblyDelimiterIndex(string fullyQualifiedTypeName)
		{
			int num = 0;
			for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
			{
				switch (fullyQualifiedTypeName[i])
				{
				case '[':
					num++;
					break;
				case ']':
					num--;
					break;
				case ',':
					if (num == 0)
					{
						return i;
					}
					break;
				}
			}
			return null;
		}

		public static global::System.Reflection.MemberInfo? GetMemberInfoFromType(global::System.Type targetType, global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo.MemberType() == global::System.Reflection.MemberTypes.Property)
			{
				global::System.Reflection.PropertyInfo propertyInfo = (global::System.Reflection.PropertyInfo)memberInfo;
				global::System.Type[] types = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(propertyInfo.GetIndexParameters(), (global::System.Reflection.ParameterInfo p) => p.ParameterType));
				return targetType.GetProperty(propertyInfo.Name, global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic, null, propertyInfo.PropertyType, types, null);
			}
			return global::System.Linq.Enumerable.SingleOrDefault(targetType.GetMember(memberInfo.Name, memberInfo.MemberType(), global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic));
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Reflection.FieldInfo> GetFields(global::System.Type targetType, global::System.Reflection.BindingFlags bindingAttr)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(targetType, "targetType");
			global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> list = new global::System.Collections.Generic.List<global::System.Reflection.MemberInfo>(targetType.GetFields(bindingAttr));
			GetChildPrivateFields(list, targetType, bindingAttr);
			return global::System.Linq.Enumerable.Cast<global::System.Reflection.FieldInfo>(list);
		}

		private static void GetChildPrivateFields(global::System.Collections.Generic.IList<global::System.Reflection.MemberInfo> initialFields, global::System.Type type, global::System.Reflection.BindingFlags bindingAttr)
		{
			global::System.Type type2 = type;
			if ((bindingAttr & global::System.Reflection.BindingFlags.NonPublic) == 0)
			{
				return;
			}
			global::System.Reflection.BindingFlags bindingAttr2 = bindingAttr.RemoveFlag(global::System.Reflection.BindingFlags.Public);
			while ((type2 = type2.BaseType()) != null)
			{
				global::System.Collections.Generic.IEnumerable<global::System.Reflection.FieldInfo> collection = global::System.Linq.Enumerable.Where(type2.GetFields(bindingAttr2), (global::System.Reflection.FieldInfo f) => f.IsPrivate);
				initialFields.AddRange(collection);
			}
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Reflection.PropertyInfo> GetProperties(global::System.Type targetType, global::System.Reflection.BindingFlags bindingAttr)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(targetType, "targetType");
			global::System.Collections.Generic.List<global::System.Reflection.PropertyInfo> list = new global::System.Collections.Generic.List<global::System.Reflection.PropertyInfo>(targetType.GetProperties(bindingAttr));
			if (targetType.IsInterface())
			{
				global::System.Type[] interfaces = targetType.GetInterfaces();
				foreach (global::System.Type type in interfaces)
				{
					list.AddRange(type.GetProperties(bindingAttr));
				}
			}
			GetChildPrivateProperties(list, targetType, bindingAttr);
			for (int j = 0; j < list.Count; j++)
			{
				global::System.Reflection.PropertyInfo propertyInfo = list[j];
				if (propertyInfo.DeclaringType != targetType)
				{
					global::System.Reflection.PropertyInfo value = (global::System.Reflection.PropertyInfo)GetMemberInfoFromType(propertyInfo.DeclaringType, propertyInfo);
					list[j] = value;
				}
			}
			return list;
		}

		public static global::System.Reflection.BindingFlags RemoveFlag(this global::System.Reflection.BindingFlags bindingAttr, global::System.Reflection.BindingFlags flag)
		{
			if ((bindingAttr & flag) != flag)
			{
				return bindingAttr;
			}
			return bindingAttr ^ flag;
		}

		private static void GetChildPrivateProperties(global::System.Collections.Generic.IList<global::System.Reflection.PropertyInfo> initialProperties, global::System.Type type, global::System.Reflection.BindingFlags bindingAttr)
		{
			global::System.Type type2 = type;
			while ((type2 = type2.BaseType()) != null)
			{
				global::System.Reflection.PropertyInfo[] properties = type2.GetProperties(bindingAttr);
				foreach (global::System.Reflection.PropertyInfo propertyInfo in properties)
				{
					global::System.Reflection.PropertyInfo subTypeProperty = propertyInfo;
					if (!subTypeProperty.IsVirtual())
					{
						if (!IsPublic(subTypeProperty))
						{
							int num = initialProperties.IndexOf((global::System.Reflection.PropertyInfo p) => p.Name == subTypeProperty.Name);
							if (num == -1)
							{
								initialProperties.Add(subTypeProperty);
							}
							else if (!IsPublic(initialProperties[num]))
							{
								initialProperties[num] = subTypeProperty;
							}
						}
						else if (initialProperties.IndexOf((global::System.Reflection.PropertyInfo p) => p.Name == subTypeProperty.Name && p.DeclaringType == subTypeProperty.DeclaringType) == -1)
						{
							initialProperties.Add(subTypeProperty);
						}
					}
					else
					{
						global::System.Type subTypePropertyDeclaringType = subTypeProperty.GetBaseDefinition()?.DeclaringType ?? subTypeProperty.DeclaringType;
						if (initialProperties.IndexOf((global::System.Reflection.PropertyInfo p) => p.Name == subTypeProperty.Name && p.IsVirtual() && (p.GetBaseDefinition()?.DeclaringType ?? p.DeclaringType).IsAssignableFrom(subTypePropertyDeclaringType)) == -1)
						{
							initialProperties.Add(subTypeProperty);
						}
					}
				}
			}
		}

		public static bool IsMethodOverridden(global::System.Type currentType, global::System.Type methodDeclaringType, string method)
		{
			return global::System.Linq.Enumerable.Any(currentType.GetMethods(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic), (global::System.Reflection.MethodInfo info) => info.Name == method && info.DeclaringType != methodDeclaringType && info.GetBaseDefinition().DeclaringType == methodDeclaringType);
		}

		public static object? GetDefaultValue(global::System.Type type)
		{
			if (!type.IsValueType())
			{
				return null;
			}
			switch (global::Newtonsoft.Json.Utilities.ConvertUtils.GetTypeCode(type))
			{
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Boolean:
				return false;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Char:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.SByte:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int16:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt16:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int32:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Byte:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt32:
				return 0;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Int64:
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.UInt64:
				return 0L;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Single:
				return 0f;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Double:
				return 0.0;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Decimal:
				return 0m;
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTime:
				return default(global::System.DateTime);
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.BigInteger:
				return default(global::System.Numerics.BigInteger);
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.Guid:
				return default(global::System.Guid);
			case global::Newtonsoft.Json.Utilities.PrimitiveTypeCode.DateTimeOffset:
				return default(global::System.DateTimeOffset);
			default:
				if (IsNullable(type))
				{
					return null;
				}
				return global::System.Activator.CreateInstance(type);
			}
		}
	}
}
