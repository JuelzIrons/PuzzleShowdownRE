namespace Unity.VisualScripting.FullSerializer.Internal
{
	public static class fsPortableReflection
	{
		private struct AttributeQuery
		{
			public global::System.Reflection.MemberInfo MemberInfo;

			public global::System.Type AttributeType;
		}

		private class AttributeQueryComparator : global::System.Collections.Generic.IEqualityComparer<global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery>
		{
			public bool Equals(global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery x, global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery y)
			{
				if (x.MemberInfo == y.MemberInfo)
				{
					return x.AttributeType == y.AttributeType;
				}
				return false;
			}

			public int GetHashCode(global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery obj)
			{
				return obj.MemberInfo.GetHashCode() + 17 * obj.AttributeType.GetHashCode();
			}
		}

		public static global::System.Type[] EmptyTypes = new global::System.Type[0];

		private static global::System.Collections.Generic.IDictionary<global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery, global::System.Attribute> _cachedAttributeQueries = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery, global::System.Attribute>(new global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQueryComparator());

		private static global::System.Reflection.BindingFlags DeclaredFlags = global::System.Reflection.BindingFlags.DeclaredOnly | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic;

		public static bool HasAttribute<TAttribute>(global::System.Reflection.MemberInfo element)
		{
			return HasAttribute(element, typeof(TAttribute));
		}

		public static bool HasAttribute<TAttribute>(global::System.Reflection.MemberInfo element, bool shouldCache)
		{
			return HasAttribute(element, typeof(TAttribute), shouldCache);
		}

		public static bool HasAttribute(global::System.Reflection.MemberInfo element, global::System.Type attributeType)
		{
			return HasAttribute(element, attributeType, shouldCache: true);
		}

		public static bool HasAttribute(global::System.Reflection.MemberInfo element, global::System.Type attributeType, bool shouldCache)
		{
			return global::System.Attribute.IsDefined(element, attributeType, inherit: true);
		}

		public static global::System.Attribute GetAttribute(global::System.Reflection.MemberInfo element, global::System.Type attributeType, bool shouldCache)
		{
			global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery key = new global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery
			{
				MemberInfo = element,
				AttributeType = attributeType
			};
			if (!_cachedAttributeQueries.TryGetValue(key, out var value))
			{
				global::System.Attribute[] array = global::System.Linq.Enumerable.ToArray(global::System.Attribute.GetCustomAttributes(element, attributeType, inherit: true));
				if (array.Length != 0)
				{
					value = array[0];
				}
				if (shouldCache)
				{
					_cachedAttributeQueries[key] = value;
				}
			}
			return value;
		}

		public static TAttribute GetAttribute<TAttribute>(global::System.Reflection.MemberInfo element, bool shouldCache) where TAttribute : global::System.Attribute
		{
			return (TAttribute)GetAttribute(element, typeof(TAttribute), shouldCache);
		}

		public static TAttribute GetAttribute<TAttribute>(global::System.Reflection.MemberInfo element) where TAttribute : global::System.Attribute
		{
			return GetAttribute<TAttribute>(element, shouldCache: true);
		}

		public static global::System.Reflection.PropertyInfo GetDeclaredProperty(this global::System.Type type, string propertyName)
		{
			global::System.Reflection.PropertyInfo[] declaredProperties = type.GetDeclaredProperties();
			for (int i = 0; i < declaredProperties.Length; i++)
			{
				if (declaredProperties[i].Name == propertyName)
				{
					return declaredProperties[i];
				}
			}
			return null;
		}

		public static global::System.Reflection.MethodInfo GetDeclaredMethod(this global::System.Type type, string methodName)
		{
			global::System.Reflection.MethodInfo[] declaredMethods = type.GetDeclaredMethods();
			for (int i = 0; i < declaredMethods.Length; i++)
			{
				if (declaredMethods[i].Name == methodName)
				{
					return declaredMethods[i];
				}
			}
			return null;
		}

		public static global::System.Reflection.ConstructorInfo GetDeclaredConstructor(this global::System.Type type, global::System.Type[] parameters)
		{
			global::System.Reflection.ConstructorInfo[] declaredConstructors = type.GetDeclaredConstructors();
			foreach (global::System.Reflection.ConstructorInfo constructorInfo in declaredConstructors)
			{
				global::System.Reflection.ParameterInfo[] parameters2 = constructorInfo.GetParameters();
				if (parameters.Length == parameters2.Length)
				{
					for (int j = 0; j < parameters2.Length; j++)
					{
						_ = parameters2[j].ParameterType != parameters[j];
					}
					return constructorInfo;
				}
			}
			return null;
		}

		public static global::System.Reflection.ConstructorInfo[] GetDeclaredConstructors(this global::System.Type type)
		{
			return type.GetConstructors(DeclaredFlags & ~global::System.Reflection.BindingFlags.Static);
		}

		public static global::System.Reflection.MemberInfo[] GetFlattenedMember(this global::System.Type type, string memberName)
		{
			global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> list = new global::System.Collections.Generic.List<global::System.Reflection.MemberInfo>();
			while (type != null)
			{
				global::System.Reflection.MemberInfo[] declaredMembers = type.GetDeclaredMembers();
				for (int i = 0; i < declaredMembers.Length; i++)
				{
					if (declaredMembers[i].Name == memberName)
					{
						list.Add(declaredMembers[i]);
					}
				}
				type = type.Resolve().BaseType;
			}
			return list.ToArray();
		}

		public static global::System.Reflection.MethodInfo GetFlattenedMethod(this global::System.Type type, string methodName)
		{
			while (type != null)
			{
				global::System.Reflection.MethodInfo[] declaredMethods = type.GetDeclaredMethods();
				for (int i = 0; i < declaredMethods.Length; i++)
				{
					if (declaredMethods[i].Name == methodName)
					{
						return declaredMethods[i];
					}
				}
				type = type.Resolve().BaseType;
			}
			return null;
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Reflection.MethodInfo> GetFlattenedMethods(this global::System.Type type, string methodName)
		{
			while (type != null)
			{
				global::System.Reflection.MethodInfo[] methods = type.GetDeclaredMethods();
				int i = 0;
				while (i < methods.Length)
				{
					if (methods[i].Name == methodName)
					{
						yield return methods[i];
					}
					int num = i + 1;
					i = num;
				}
				type = type.Resolve().BaseType;
			}
		}

		public static global::System.Reflection.PropertyInfo GetFlattenedProperty(this global::System.Type type, string propertyName)
		{
			while (type != null)
			{
				global::System.Reflection.PropertyInfo[] declaredProperties = type.GetDeclaredProperties();
				for (int i = 0; i < declaredProperties.Length; i++)
				{
					if (declaredProperties[i].Name == propertyName)
					{
						return declaredProperties[i];
					}
				}
				type = type.Resolve().BaseType;
			}
			return null;
		}

		public static global::System.Reflection.MemberInfo GetDeclaredMember(this global::System.Type type, string memberName)
		{
			global::System.Reflection.MemberInfo[] declaredMembers = type.GetDeclaredMembers();
			for (int i = 0; i < declaredMembers.Length; i++)
			{
				if (declaredMembers[i].Name == memberName)
				{
					return declaredMembers[i];
				}
			}
			return null;
		}

		public static global::System.Reflection.MethodInfo[] GetDeclaredMethods(this global::System.Type type)
		{
			return type.GetMethods(DeclaredFlags);
		}

		public static global::System.Reflection.PropertyInfo[] GetDeclaredProperties(this global::System.Type type)
		{
			return type.GetProperties(DeclaredFlags);
		}

		public static global::System.Reflection.FieldInfo[] GetDeclaredFields(this global::System.Type type)
		{
			return type.GetFields(DeclaredFlags);
		}

		public static global::System.Reflection.MemberInfo[] GetDeclaredMembers(this global::System.Type type)
		{
			return type.GetMembers(DeclaredFlags);
		}

		public static global::System.Reflection.MemberInfo AsMemberInfo(global::System.Type type)
		{
			return type;
		}

		public static bool IsType(global::System.Reflection.MemberInfo member)
		{
			return member is global::System.Type;
		}

		public static global::System.Type AsType(global::System.Reflection.MemberInfo member)
		{
			return (global::System.Type)member;
		}

		public static global::System.Type Resolve(this global::System.Type type)
		{
			return type;
		}
	}
}
