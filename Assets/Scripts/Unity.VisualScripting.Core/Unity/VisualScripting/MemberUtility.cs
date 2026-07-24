namespace Unity.VisualScripting
{
	public static class MemberUtility
	{
		private static readonly global::System.Lazy<global::Unity.VisualScripting.ExtensionMethodCache> ExtensionMethodsCache;

		private static readonly global::System.Lazy<global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Reflection.MethodInfo[]>> InheritedExtensionMethodsCache;

		private static readonly global::System.Lazy<global::System.Collections.Generic.HashSet<global::System.Reflection.MethodInfo>> GenericExtensionMethods;

		static MemberUtility()
		{
			ExtensionMethodsCache = new global::System.Lazy<global::Unity.VisualScripting.ExtensionMethodCache>(() => new global::Unity.VisualScripting.ExtensionMethodCache(), isThreadSafe: true);
			InheritedExtensionMethodsCache = new global::System.Lazy<global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Reflection.MethodInfo[]>>(() => new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Reflection.MethodInfo[]>(), isThreadSafe: true);
			GenericExtensionMethods = new global::System.Lazy<global::System.Collections.Generic.HashSet<global::System.Reflection.MethodInfo>>(() => new global::System.Collections.Generic.HashSet<global::System.Reflection.MethodInfo>(), isThreadSafe: true);
		}

		public static bool IsOperator(this global::System.Reflection.MethodInfo method)
		{
			if (method.IsSpecialName)
			{
				return global::Unity.VisualScripting.OperatorUtility.operatorNames.ContainsKey(method.Name);
			}
			return false;
		}

		public static bool IsUserDefinedConversion(this global::System.Reflection.MethodInfo method)
		{
			if (method.IsSpecialName)
			{
				if (!(method.Name == "op_Implicit"))
				{
					return method.Name == "op_Explicit";
				}
				return true;
			}
			return false;
		}

		public static global::System.Reflection.MethodInfo MakeGenericMethodVia(this global::System.Reflection.MethodInfo openConstructedMethod, params global::System.Type[] closedConstructedParameterTypes)
		{
			global::Unity.VisualScripting.Ensure.That("openConstructedMethod").IsNotNull(openConstructedMethod);
			global::Unity.VisualScripting.Ensure.That("closedConstructedParameterTypes").IsNotNull(closedConstructedParameterTypes);
			if (!openConstructedMethod.ContainsGenericParameters)
			{
				return openConstructedMethod;
			}
			global::System.Type[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(openConstructedMethod.GetParameters(), (global::System.Reflection.ParameterInfo p) => p.ParameterType));
			if (array.Length != closedConstructedParameterTypes.Length)
			{
				throw new global::System.ArgumentOutOfRangeException("closedConstructedParameterTypes");
			}
			global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Type> resolvedGenericParameters = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Type>();
			for (int num = 0; num < array.Length; num++)
			{
				global::System.Type openConstructedType = array[num];
				global::System.Type closedConstructedType = closedConstructedParameterTypes[num];
				openConstructedType.MakeGenericTypeVia(closedConstructedType, resolvedGenericParameters);
			}
			global::System.Type[] typeArguments = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(openConstructedMethod.GetGenericArguments(), (global::System.Type openConstructedGenericArgument) => resolvedGenericParameters.ContainsKey(openConstructedGenericArgument) ? resolvedGenericParameters[openConstructedGenericArgument] : openConstructedGenericArgument));
			return openConstructedMethod.MakeGenericMethod(typeArguments);
		}

		public static bool IsGenericExtension(this global::System.Reflection.MethodInfo methodInfo)
		{
			return GenericExtensionMethods.Value.Contains(methodInfo);
		}

		private static global::System.Collections.Generic.IEnumerable<global::System.Reflection.MethodInfo> GetInheritedExtensionMethods(global::System.Type thisArgumentType)
		{
			global::System.Reflection.MethodInfo[] cache = ExtensionMethodsCache.Value.Cache;
			global::System.Reflection.MethodInfo[] array = cache;
			foreach (global::System.Reflection.MethodInfo methodInfo in array)
			{
				if (!methodInfo.GetParameters()[0].ParameterType.CanMakeGenericTypeVia(thisArgumentType))
				{
					continue;
				}
				if (methodInfo.ContainsGenericParameters)
				{
					global::System.Collections.Generic.IEnumerable<global::System.Type> source = global::System.Linq.Enumerable.Concat(thisArgumentType.Yield(), global::System.Linq.Enumerable.Select(methodInfo.GetParametersWithoutThis(), (global::System.Reflection.ParameterInfo p) => p.ParameterType));
					global::System.Reflection.MethodInfo methodInfo2 = methodInfo.MakeGenericMethodVia(global::System.Linq.Enumerable.ToArray(source));
					GenericExtensionMethods.Value.Add(methodInfo2);
					yield return methodInfo2;
				}
				else
				{
					yield return methodInfo;
				}
			}
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Reflection.MethodInfo> GetExtensionMethods(this global::System.Type thisArgumentType, bool inherited = true)
		{
			if (inherited)
			{
				lock (InheritedExtensionMethodsCache)
				{
					if (!InheritedExtensionMethodsCache.Value.TryGetValue(thisArgumentType, out var value))
					{
						value = global::System.Linq.Enumerable.ToArray(GetInheritedExtensionMethods(thisArgumentType));
						InheritedExtensionMethodsCache.Value.Add(thisArgumentType, value);
					}
					return value;
				}
			}
			return global::System.Linq.Enumerable.Where(ExtensionMethodsCache.Value.Cache, (global::System.Reflection.MethodInfo method) => method.GetParameters()[0].ParameterType == thisArgumentType);
		}

		public static bool IsExtension(this global::System.Reflection.MethodInfo methodInfo)
		{
			return methodInfo.HasAttribute<global::System.Runtime.CompilerServices.ExtensionAttribute>(inherit: false);
		}

		public static bool IsExtensionMethod(this global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo is global::System.Reflection.MethodInfo methodInfo)
			{
				return methodInfo.IsExtension();
			}
			return false;
		}

		public static global::System.Delegate CreateDelegate(this global::System.Reflection.MethodInfo methodInfo, global::System.Type delegateType)
		{
			return global::System.Delegate.CreateDelegate(delegateType, methodInfo);
		}

		public static bool IsAccessor(this global::System.Reflection.MemberInfo memberInfo)
		{
			if (!(memberInfo is global::System.Reflection.FieldInfo))
			{
				return memberInfo is global::System.Reflection.PropertyInfo;
			}
			return true;
		}

		public static global::System.Type GetAccessorType(this global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo is global::System.Reflection.FieldInfo)
			{
				return ((global::System.Reflection.FieldInfo)memberInfo).FieldType;
			}
			if (memberInfo is global::System.Reflection.PropertyInfo)
			{
				return ((global::System.Reflection.PropertyInfo)memberInfo).PropertyType;
			}
			return null;
		}

		public static bool IsPubliclyGettable(this global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo is global::System.Reflection.FieldInfo)
			{
				return ((global::System.Reflection.FieldInfo)memberInfo).IsPublic;
			}
			if (memberInfo is global::System.Reflection.PropertyInfo)
			{
				global::System.Reflection.PropertyInfo propertyInfo = (global::System.Reflection.PropertyInfo)memberInfo;
				if (propertyInfo.CanRead)
				{
					return propertyInfo.GetGetMethod(nonPublic: false) != null;
				}
				return false;
			}
			if (memberInfo is global::System.Reflection.MethodInfo)
			{
				return ((global::System.Reflection.MethodInfo)memberInfo).IsPublic;
			}
			if (memberInfo is global::System.Reflection.ConstructorInfo)
			{
				return ((global::System.Reflection.ConstructorInfo)memberInfo).IsPublic;
			}
			throw new global::System.NotSupportedException();
		}

		private static global::System.Type ExtendedDeclaringType(this global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo is global::System.Reflection.MethodInfo methodInfo && methodInfo.IsExtension())
			{
				return methodInfo.GetParameters()[0].ParameterType;
			}
			return memberInfo.DeclaringType;
		}

		public static global::System.Type ExtendedDeclaringType(this global::System.Reflection.MemberInfo memberInfo, bool invokeAsExtension)
		{
			if (invokeAsExtension)
			{
				return memberInfo.ExtendedDeclaringType();
			}
			return memberInfo.DeclaringType;
		}

		public static bool IsStatic(this global::System.Reflection.PropertyInfo propertyInfo)
		{
			global::System.Reflection.MethodInfo getMethod = propertyInfo.GetGetMethod(nonPublic: true);
			if ((object)getMethod == null || !getMethod.IsStatic)
			{
				return propertyInfo.GetSetMethod(nonPublic: true)?.IsStatic ?? false;
			}
			return true;
		}

		public static bool IsStatic(this global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo is global::System.Reflection.FieldInfo)
			{
				return ((global::System.Reflection.FieldInfo)memberInfo).IsStatic;
			}
			if (memberInfo is global::System.Reflection.PropertyInfo)
			{
				return ((global::System.Reflection.PropertyInfo)memberInfo).IsStatic();
			}
			if (memberInfo is global::System.Reflection.MethodBase)
			{
				return ((global::System.Reflection.MethodBase)memberInfo).IsStatic;
			}
			throw new global::System.NotSupportedException();
		}

		private static global::System.Collections.Generic.IEnumerable<global::System.Reflection.ParameterInfo> GetParametersWithoutThis(this global::System.Reflection.MethodBase methodBase)
		{
			return global::System.Linq.Enumerable.Skip(methodBase.GetParameters(), methodBase.IsExtensionMethod() ? 1 : 0);
		}

		public static bool IsInvokedAsExtension(this global::System.Reflection.MethodBase methodBase, global::System.Type targetType)
		{
			if (methodBase.IsExtensionMethod())
			{
				return methodBase.DeclaringType != targetType;
			}
			return false;
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Reflection.ParameterInfo> GetInvocationParameters(this global::System.Reflection.MethodBase methodBase, bool invokeAsExtension)
		{
			if (invokeAsExtension)
			{
				return methodBase.GetParametersWithoutThis();
			}
			return methodBase.GetParameters();
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Reflection.ParameterInfo> GetInvocationParameters(this global::System.Reflection.MethodBase methodBase, global::System.Type targetType)
		{
			return methodBase.GetInvocationParameters(methodBase.IsInvokedAsExtension(targetType));
		}

		public static global::System.Type UnderlyingParameterType(this global::System.Reflection.ParameterInfo parameterInfo)
		{
			if (parameterInfo.ParameterType.IsByRef)
			{
				return parameterInfo.ParameterType.GetElementType();
			}
			return parameterInfo.ParameterType;
		}

		public static bool HasDefaultValue(this global::System.Reflection.ParameterInfo parameterInfo)
		{
			return (parameterInfo.Attributes & global::System.Reflection.ParameterAttributes.HasDefault) == global::System.Reflection.ParameterAttributes.HasDefault;
		}

		public static object DefaultValue(this global::System.Reflection.ParameterInfo parameterInfo)
		{
			if (parameterInfo.HasDefaultValue())
			{
				object obj = parameterInfo.DefaultValue;
				if (obj == null && parameterInfo.ParameterType.IsValueType)
				{
					obj = parameterInfo.ParameterType.Default();
				}
				return obj;
			}
			return parameterInfo.UnderlyingParameterType().Default();
		}

		public static object PseudoDefaultValue(this global::System.Reflection.ParameterInfo parameterInfo)
		{
			if (parameterInfo.HasDefaultValue())
			{
				object obj = parameterInfo.DefaultValue;
				if (obj == null && parameterInfo.ParameterType.IsValueType)
				{
					obj = parameterInfo.ParameterType.PseudoDefault();
				}
				return obj;
			}
			return parameterInfo.UnderlyingParameterType().PseudoDefault();
		}

		public static bool AllowsNull(this global::System.Reflection.ParameterInfo parameterInfo)
		{
			global::System.Type parameterType = parameterInfo.ParameterType;
			if (!parameterType.IsReferenceType() || !parameterInfo.HasAttribute<global::Unity.VisualScripting.AllowsNullAttribute>())
			{
				return global::System.Nullable.GetUnderlyingType(parameterType) != null;
			}
			return true;
		}

		public static bool HasOutModifier(this global::System.Reflection.ParameterInfo parameterInfo)
		{
			global::Unity.VisualScripting.Ensure.That("parameterInfo").IsNotNull(parameterInfo);
			if (parameterInfo.IsOut)
			{
				return parameterInfo.ParameterType.IsByRef;
			}
			return false;
		}

		public static bool CanWrite(this global::System.Reflection.FieldInfo fieldInfo)
		{
			if (!fieldInfo.IsInitOnly)
			{
				return !fieldInfo.IsLiteral;
			}
			return false;
		}

		public static global::Unity.VisualScripting.Member ToManipulator(this global::System.Reflection.MemberInfo memberInfo)
		{
			return memberInfo.ToManipulator(memberInfo.DeclaringType);
		}

		public static global::Unity.VisualScripting.Member ToManipulator(this global::System.Reflection.MemberInfo memberInfo, global::System.Type targetType)
		{
			if (memberInfo is global::System.Reflection.FieldInfo fieldInfo)
			{
				return fieldInfo.ToManipulator(targetType);
			}
			if (memberInfo is global::System.Reflection.PropertyInfo propertyInfo)
			{
				return propertyInfo.ToManipulator(targetType);
			}
			if (memberInfo is global::System.Reflection.MethodInfo methodInfo)
			{
				return methodInfo.ToManipulator(targetType);
			}
			if (memberInfo is global::System.Reflection.ConstructorInfo constructorInfo)
			{
				return constructorInfo.ToManipulator(targetType);
			}
			throw new global::System.InvalidOperationException();
		}

		public static global::Unity.VisualScripting.Member ToManipulator(this global::System.Reflection.FieldInfo fieldInfo, global::System.Type targetType)
		{
			return new global::Unity.VisualScripting.Member(targetType, fieldInfo);
		}

		public static global::Unity.VisualScripting.Member ToManipulator(this global::System.Reflection.PropertyInfo propertyInfo, global::System.Type targetType)
		{
			return new global::Unity.VisualScripting.Member(targetType, propertyInfo);
		}

		public static global::Unity.VisualScripting.Member ToManipulator(this global::System.Reflection.MethodInfo methodInfo, global::System.Type targetType)
		{
			return new global::Unity.VisualScripting.Member(targetType, methodInfo);
		}

		public static global::Unity.VisualScripting.Member ToManipulator(this global::System.Reflection.ConstructorInfo constructorInfo, global::System.Type targetType)
		{
			return new global::Unity.VisualScripting.Member(targetType, constructorInfo);
		}

		public static global::System.Reflection.ConstructorInfo GetConstructorAccepting(this global::System.Type type, global::System.Type[] paramTypes, bool nonPublic)
		{
			global::System.Reflection.BindingFlags bindingFlags = global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public;
			if (nonPublic)
			{
				bindingFlags |= global::System.Reflection.BindingFlags.NonPublic;
			}
			return global::System.Linq.Enumerable.FirstOrDefault(type.GetConstructors(bindingFlags), delegate(global::System.Reflection.ConstructorInfo constructor)
			{
				global::System.Reflection.ParameterInfo[] parameters = constructor.GetParameters();
				if (parameters.Length != paramTypes.Length)
				{
					return false;
				}
				for (int i = 0; i < parameters.Length; i++)
				{
					if (paramTypes[i] == null)
					{
						if (!parameters[i].ParameterType.IsNullable())
						{
							return false;
						}
					}
					else if (!parameters[i].ParameterType.IsAssignableFrom(paramTypes[i]))
					{
						return false;
					}
				}
				return true;
			});
		}

		public static global::System.Reflection.ConstructorInfo GetConstructorAccepting(this global::System.Type type, params global::System.Type[] paramTypes)
		{
			return type.GetConstructorAccepting(paramTypes, nonPublic: true);
		}

		public static global::System.Reflection.ConstructorInfo GetPublicConstructorAccepting(this global::System.Type type, params global::System.Type[] paramTypes)
		{
			return type.GetConstructorAccepting(paramTypes, nonPublic: false);
		}

		public static global::System.Reflection.ConstructorInfo GetDefaultConstructor(this global::System.Type type)
		{
			return type.GetConstructorAccepting();
		}

		public static global::System.Reflection.ConstructorInfo GetPublicDefaultConstructor(this global::System.Type type)
		{
			return type.GetPublicConstructorAccepting();
		}

		public static global::System.Reflection.MemberInfo[] GetExtendedMember(this global::System.Type type, string name, global::System.Reflection.MemberTypes types, global::System.Reflection.BindingFlags flags)
		{
			global::System.Collections.Generic.List<global::System.Reflection.MemberInfo> list = global::System.Linq.Enumerable.ToList(type.GetMember(name, types, flags));
			if (types.HasFlag(global::System.Reflection.MemberTypes.Method))
			{
				list.AddRange(global::System.Linq.Enumerable.Cast<global::System.Reflection.MemberInfo>(global::System.Linq.Enumerable.Where(type.GetExtensionMethods(), (global::System.Reflection.MethodInfo extension) => extension.Name == name)));
			}
			return list.ToArray();
		}

		public static global::System.Reflection.MemberInfo[] GetExtendedMembers(this global::System.Type type, global::System.Reflection.BindingFlags flags)
		{
			global::System.Collections.Generic.HashSet<global::System.Reflection.MemberInfo> hashSet = type.GetMembers(flags).ToHashSet();
			foreach (global::System.Reflection.MethodInfo extensionMethod in type.GetExtensionMethods())
			{
				hashSet.Add(extensionMethod);
			}
			return global::System.Linq.Enumerable.ToArray(hashSet);
		}

		private static bool NameMatches(this global::System.Reflection.MemberInfo member, string name)
		{
			return member.Name == name;
		}

		private static bool ParametersMatch(this global::System.Reflection.MethodBase methodBase, global::System.Collections.Generic.IEnumerable<global::System.Type> parameterTypes, bool invokeAsExtension)
		{
			global::Unity.VisualScripting.Ensure.That("parameterTypes").IsNotNull(parameterTypes);
			return global::System.Linq.Enumerable.SequenceEqual(global::System.Linq.Enumerable.Select(methodBase.GetInvocationParameters(invokeAsExtension), (global::System.Reflection.ParameterInfo paramInfo) => paramInfo.ParameterType), parameterTypes);
		}

		private static bool GenericArgumentsMatch(this global::System.Reflection.MethodInfo method, global::System.Collections.Generic.IEnumerable<global::System.Type> genericArgumentTypes)
		{
			global::Unity.VisualScripting.Ensure.That("genericArgumentTypes").IsNotNull(genericArgumentTypes);
			if (method.ContainsGenericParameters)
			{
				return false;
			}
			return global::System.Linq.Enumerable.SequenceEqual(method.GetGenericArguments(), genericArgumentTypes);
		}

		public static bool SignatureMatches(this global::System.Reflection.FieldInfo field, string name)
		{
			return field.NameMatches(name);
		}

		public static bool SignatureMatches(this global::System.Reflection.PropertyInfo property, string name)
		{
			return property.NameMatches(name);
		}

		public static bool SignatureMatches(this global::System.Reflection.ConstructorInfo constructor, string name, global::System.Collections.Generic.IEnumerable<global::System.Type> parameterTypes)
		{
			if (constructor.NameMatches(name))
			{
				return constructor.ParametersMatch(parameterTypes, invokeAsExtension: false);
			}
			return false;
		}

		public static bool SignatureMatches(this global::System.Reflection.MethodInfo method, string name, global::System.Collections.Generic.IEnumerable<global::System.Type> parameterTypes, bool invokeAsExtension)
		{
			if (method.NameMatches(name) && method.ParametersMatch(parameterTypes, invokeAsExtension))
			{
				return !method.ContainsGenericParameters;
			}
			return false;
		}

		public static bool SignatureMatches(this global::System.Reflection.MethodInfo method, string name, global::System.Collections.Generic.IEnumerable<global::System.Type> parameterTypes, global::System.Collections.Generic.IEnumerable<global::System.Type> genericArgumentTypes, bool invokeAsExtension)
		{
			if (method.NameMatches(name) && method.ParametersMatch(parameterTypes, invokeAsExtension))
			{
				return method.GenericArgumentsMatch(genericArgumentTypes);
			}
			return false;
		}

		public static global::System.Reflection.FieldInfo GetFieldUnambiguous(this global::System.Type type, string name, global::System.Reflection.BindingFlags flags)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			global::Unity.VisualScripting.Ensure.That("name").IsNotNull(name);
			flags |= global::System.Reflection.BindingFlags.DeclaredOnly;
			while (type != null)
			{
				global::System.Reflection.FieldInfo field = type.GetField(name, flags);
				if (field != null)
				{
					return field;
				}
				type = type.BaseType;
			}
			return null;
		}

		public static global::System.Reflection.PropertyInfo GetPropertyUnambiguous(this global::System.Type type, string name, global::System.Reflection.BindingFlags flags)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			global::Unity.VisualScripting.Ensure.That("name").IsNotNull(name);
			flags |= global::System.Reflection.BindingFlags.DeclaredOnly;
			while (type != null)
			{
				global::System.Reflection.PropertyInfo property = type.GetProperty(name, flags);
				if (property != null)
				{
					return property;
				}
				type = type.BaseType;
			}
			return null;
		}

		public static global::System.Reflection.MethodInfo GetMethodUnambiguous(this global::System.Type type, string name, global::System.Reflection.BindingFlags flags)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			global::Unity.VisualScripting.Ensure.That("name").IsNotNull(name);
			flags |= global::System.Reflection.BindingFlags.DeclaredOnly;
			while (type != null)
			{
				global::System.Reflection.MethodInfo method = type.GetMethod(name, flags);
				if (method != null)
				{
					return method;
				}
				type = type.BaseType;
			}
			return null;
		}

		private static TMemberInfo DisambiguateHierarchy<TMemberInfo>(this global::System.Collections.Generic.IEnumerable<TMemberInfo> members, global::System.Type type) where TMemberInfo : global::System.Reflection.MemberInfo
		{
			while (type != null)
			{
				foreach (TMemberInfo member in members)
				{
					global::System.Reflection.MethodInfo methodInfo = member as global::System.Reflection.MethodInfo;
					bool invokeAsExtension = methodInfo != null && methodInfo.IsInvokedAsExtension(type);
					if (member.ExtendedDeclaringType(invokeAsExtension) == type)
					{
						return member;
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		public static global::System.Reflection.FieldInfo Disambiguate(this global::System.Collections.Generic.IEnumerable<global::System.Reflection.FieldInfo> fields, global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("fields").IsNotNull(fields);
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			return fields.DisambiguateHierarchy(type);
		}

		public static global::System.Reflection.PropertyInfo Disambiguate(this global::System.Collections.Generic.IEnumerable<global::System.Reflection.PropertyInfo> properties, global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("properties").IsNotNull(properties);
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			return properties.DisambiguateHierarchy(type);
		}

		public static global::System.Reflection.ConstructorInfo Disambiguate(this global::System.Collections.Generic.IEnumerable<global::System.Reflection.ConstructorInfo> constructors, global::System.Type type, global::System.Collections.Generic.IEnumerable<global::System.Type> parameterTypes)
		{
			global::Unity.VisualScripting.Ensure.That("constructors").IsNotNull(constructors);
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			global::Unity.VisualScripting.Ensure.That("parameterTypes").IsNotNull(parameterTypes);
			return global::System.Linq.Enumerable.Where(constructors, (global::System.Reflection.ConstructorInfo m) => m.ParametersMatch(parameterTypes, invokeAsExtension: false) && !m.ContainsGenericParameters).DisambiguateHierarchy(type);
		}

		public static global::System.Reflection.MethodInfo Disambiguate(this global::System.Collections.Generic.IEnumerable<global::System.Reflection.MethodInfo> methods, global::System.Type type, global::System.Collections.Generic.IEnumerable<global::System.Type> parameterTypes)
		{
			global::Unity.VisualScripting.Ensure.That("methods").IsNotNull(methods);
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			global::Unity.VisualScripting.Ensure.That("parameterTypes").IsNotNull(parameterTypes);
			return global::System.Linq.Enumerable.Where(methods, (global::System.Reflection.MethodInfo m) => m.ParametersMatch(parameterTypes, m.IsInvokedAsExtension(type)) && !m.ContainsGenericParameters).DisambiguateHierarchy(type);
		}

		public static global::System.Reflection.MethodInfo Disambiguate(this global::System.Collections.Generic.IEnumerable<global::System.Reflection.MethodInfo> methods, global::System.Type type, global::System.Collections.Generic.IEnumerable<global::System.Type> parameterTypes, global::System.Collections.Generic.IEnumerable<global::System.Type> genericArgumentTypes)
		{
			global::Unity.VisualScripting.Ensure.That("methods").IsNotNull(methods);
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			global::Unity.VisualScripting.Ensure.That("parameterTypes").IsNotNull(parameterTypes);
			global::Unity.VisualScripting.Ensure.That("genericArgumentTypes").IsNotNull(genericArgumentTypes);
			return global::System.Linq.Enumerable.Where(methods, (global::System.Reflection.MethodInfo m) => m.ParametersMatch(parameterTypes, m.IsInvokedAsExtension(type)) && m.GenericArgumentsMatch(genericArgumentTypes)).DisambiguateHierarchy(type);
		}
	}
}
