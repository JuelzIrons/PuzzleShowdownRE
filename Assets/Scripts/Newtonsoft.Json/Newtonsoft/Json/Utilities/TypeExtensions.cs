namespace Newtonsoft.Json.Utilities
{
	internal static class TypeExtensions
	{
		public static global::System.Reflection.MethodInfo Method(this global::System.Delegate d)
		{
			return d.Method;
		}

		public static global::System.Reflection.MemberTypes MemberType(this global::System.Reflection.MemberInfo memberInfo)
		{
			return memberInfo.MemberType;
		}

		public static bool ContainsGenericParameters(this global::System.Type type)
		{
			return type.ContainsGenericParameters;
		}

		public static bool IsInterface(this global::System.Type type)
		{
			return type.IsInterface;
		}

		public static bool IsGenericType(this global::System.Type type)
		{
			return type.IsGenericType;
		}

		public static bool IsGenericTypeDefinition(this global::System.Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		public static global::System.Type? BaseType(this global::System.Type type)
		{
			return type.BaseType;
		}

		public static global::System.Reflection.Assembly Assembly(this global::System.Type type)
		{
			return type.Assembly;
		}

		public static bool IsEnum(this global::System.Type type)
		{
			return type.IsEnum;
		}

		public static bool IsClass(this global::System.Type type)
		{
			return type.IsClass;
		}

		public static bool IsSealed(this global::System.Type type)
		{
			return type.IsSealed;
		}

		public static bool IsAbstract(this global::System.Type type)
		{
			return type.IsAbstract;
		}

		public static bool IsVisible(this global::System.Type type)
		{
			return type.IsVisible;
		}

		public static bool IsValueType(this global::System.Type type)
		{
			return type.IsValueType;
		}

		public static bool IsPrimitive(this global::System.Type type)
		{
			return type.IsPrimitive;
		}

		public static bool AssignableToTypeName(this global::System.Type type, string fullTypeName, bool searchInterfaces, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::System.Type? match)
		{
			global::System.Type type2 = type;
			while (type2 != null)
			{
				if (string.Equals(type2.FullName, fullTypeName, global::System.StringComparison.Ordinal))
				{
					match = type2;
					return true;
				}
				type2 = type2.BaseType();
			}
			if (searchInterfaces)
			{
				global::System.Type[] interfaces = type.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (string.Equals(interfaces[i].Name, fullTypeName, global::System.StringComparison.Ordinal))
					{
						match = type;
						return true;
					}
				}
			}
			match = null;
			return false;
		}

		public static bool AssignableToTypeName(this global::System.Type type, string fullTypeName, bool searchInterfaces)
		{
			global::System.Type match;
			return type.AssignableToTypeName(fullTypeName, searchInterfaces, out match);
		}

		public static bool ImplementInterface(this global::System.Type type, global::System.Type interfaceType)
		{
			global::System.Type type2 = type;
			while (type2 != null)
			{
				foreach (global::System.Type item in (global::System.Collections.Generic.IEnumerable<global::System.Type>)type2.GetInterfaces())
				{
					if (item == interfaceType || (item != null && item.ImplementInterface(interfaceType)))
					{
						return true;
					}
				}
				type2 = type2.BaseType();
			}
			return false;
		}
	}
}
