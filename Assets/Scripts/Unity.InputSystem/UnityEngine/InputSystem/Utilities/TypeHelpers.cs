namespace UnityEngine.InputSystem.Utilities
{
	internal static class TypeHelpers
	{
		public static TObject As<TObject>(this object obj)
		{
			if (obj == null)
			{
				return default(TObject);
			}
			return (TObject)obj;
		}

		public static bool IsInt(this global::System.TypeCode type)
		{
			return type switch
			{
				global::System.TypeCode.Byte => true, 
				global::System.TypeCode.SByte => true, 
				global::System.TypeCode.Int16 => true, 
				global::System.TypeCode.Int32 => true, 
				global::System.TypeCode.Int64 => true, 
				global::System.TypeCode.UInt16 => true, 
				global::System.TypeCode.UInt32 => true, 
				global::System.TypeCode.UInt64 => true, 
				_ => false, 
			};
		}

		public static global::System.Type GetValueType(global::System.Reflection.MemberInfo member)
		{
			global::System.Reflection.FieldInfo fieldInfo = member as global::System.Reflection.FieldInfo;
			if (fieldInfo != null)
			{
				return fieldInfo.FieldType;
			}
			global::System.Reflection.PropertyInfo propertyInfo = member as global::System.Reflection.PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.PropertyType;
			}
			global::System.Reflection.MethodInfo methodInfo = member as global::System.Reflection.MethodInfo;
			if (methodInfo != null)
			{
				return methodInfo.ReturnType;
			}
			return null;
		}

		public static string GetNiceTypeName(this global::System.Type type)
		{
			if (type.IsPrimitive)
			{
				if (type == typeof(int))
				{
					return "int";
				}
				if (type == typeof(float))
				{
					return "float";
				}
				if (type == typeof(char))
				{
					return "char";
				}
				if (type == typeof(byte))
				{
					return "byte";
				}
				if (type == typeof(short))
				{
					return "short";
				}
				if (type == typeof(long))
				{
					return "long";
				}
				if (type == typeof(double))
				{
					return "double";
				}
				if (type == typeof(uint))
				{
					return "uint";
				}
				if (type == typeof(sbyte))
				{
					return "sbyte";
				}
				if (type == typeof(ushort))
				{
					return "ushort";
				}
				if (type == typeof(ulong))
				{
					return "ulong";
				}
			}
			return type.Name;
		}

		public static global::System.Type GetGenericTypeArgumentFromHierarchy(global::System.Type type, global::System.Type genericTypeDefinition, int argumentIndex)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			if (genericTypeDefinition == null)
			{
				throw new global::System.ArgumentNullException("genericTypeDefinition");
			}
			if (argumentIndex < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("argumentIndex");
			}
			if (genericTypeDefinition.IsInterface)
			{
				while (true)
				{
					global::System.Type[] interfaces = type.GetInterfaces();
					bool flag = false;
					global::System.Type[] array = interfaces;
					foreach (global::System.Type type2 in array)
					{
						if (type2.IsConstructedGenericType && type2.GetGenericTypeDefinition() == genericTypeDefinition)
						{
							type = type2;
							flag = true;
							break;
						}
						global::System.Type genericTypeArgumentFromHierarchy = GetGenericTypeArgumentFromHierarchy(type2, genericTypeDefinition, argumentIndex);
						if (genericTypeArgumentFromHierarchy != null)
						{
							return genericTypeArgumentFromHierarchy;
						}
					}
					if (flag)
					{
						break;
					}
					type = type.BaseType;
					if (type == null || type == typeof(object))
					{
						return null;
					}
				}
			}
			else
			{
				while (!type.IsConstructedGenericType || type.GetGenericTypeDefinition() != genericTypeDefinition)
				{
					type = type.BaseType;
					if (type == typeof(object))
					{
						return null;
					}
				}
			}
			return type.GenericTypeArguments[argumentIndex];
		}
	}
}
