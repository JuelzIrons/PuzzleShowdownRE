namespace Unity.VisualScripting
{
	public static class TypeUtility
	{
		private static readonly global::System.Collections.Generic.HashSet<global::System.Type> _numericTypes = new global::System.Collections.Generic.HashSet<global::System.Type>
		{
			typeof(byte),
			typeof(sbyte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal)
		};

		private static readonly global::System.Collections.Generic.HashSet<global::System.Type> _numericConstructTypes = new global::System.Collections.Generic.HashSet<global::System.Type>
		{
			typeof(global::UnityEngine.Vector2),
			typeof(global::UnityEngine.Vector3),
			typeof(global::UnityEngine.Vector4),
			typeof(global::UnityEngine.Quaternion),
			typeof(global::UnityEngine.Matrix4x4),
			typeof(global::UnityEngine.Rect)
		};

		private static readonly global::System.Collections.Generic.HashSet<global::System.Type> typesWithShortStrings = new global::System.Collections.Generic.HashSet<global::System.Type>
		{
			typeof(string),
			typeof(global::UnityEngine.Vector2),
			typeof(global::UnityEngine.Vector3),
			typeof(global::UnityEngine.Vector4)
		};

		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, object> defaultPrimitives = new global::System.Collections.Generic.Dictionary<global::System.Type, object>
		{
			{
				typeof(int),
				0
			},
			{
				typeof(uint),
				0u
			},
			{
				typeof(long),
				0L
			},
			{
				typeof(ulong),
				0uL
			},
			{
				typeof(short),
				(short)0
			},
			{
				typeof(ushort),
				(ushort)0
			},
			{
				typeof(byte),
				(byte)0
			},
			{
				typeof(sbyte),
				(sbyte)0
			},
			{
				typeof(float),
				0f
			},
			{
				typeof(double),
				0.0
			},
			{
				typeof(decimal),
				0m
			},
			{
				typeof(global::UnityEngine.Vector2),
				default(global::UnityEngine.Vector2)
			},
			{
				typeof(global::UnityEngine.Vector3),
				default(global::UnityEngine.Vector3)
			},
			{
				typeof(global::UnityEngine.Vector4),
				default(global::UnityEngine.Vector4)
			}
		};

		public static bool IsBasic(this global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			if (type == typeof(string) || type == typeof(decimal))
			{
				return true;
			}
			if (type.IsEnum)
			{
				return true;
			}
			if (type.IsPrimitive)
			{
				if (type == typeof(global::System.IntPtr) || type == typeof(global::System.UIntPtr))
				{
					return false;
				}
				return true;
			}
			return false;
		}

		public static bool IsNumeric(this global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			return _numericTypes.Contains(type);
		}

		public static bool IsNumericConstruct(this global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			return _numericConstructTypes.Contains(type);
		}

		public static global::Unity.VisualScripting.Namespace Namespace(this global::System.Type type)
		{
			return global::Unity.VisualScripting.Namespace.FromFullName(type.Namespace);
		}

		public static global::System.Func<object> Instantiator(this global::System.Type type, bool nonPublic = true)
		{
			global::System.Func<object[], object> instantiator = type.Instantiator(nonPublic, global::Unity.VisualScripting.Empty<global::System.Type>.array);
			if (instantiator != null)
			{
				return () => instantiator(global::Unity.VisualScripting.Empty<object>.array);
			}
			return null;
		}

		public static global::System.Func<object[], object> Instantiator(this global::System.Type type, bool nonPublic = true, params global::System.Type[] parameterTypes)
		{
			if (typeof(global::UnityEngine.Object).IsAssignableFrom(type))
			{
				return null;
			}
			if ((type.IsValueType || type.IsBasic()) && parameterTypes.Length == 0)
			{
				return (object[] args) => type.PseudoDefault();
			}
			global::System.Reflection.ConstructorInfo constructor = type.GetConstructorAccepting(parameterTypes, nonPublic);
			if (constructor != null)
			{
				return (object[] args) => constructor.Invoke(args);
			}
			return null;
		}

		public static object TryInstantiate(this global::System.Type type, bool nonPublic = true, params object[] args)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			return type.Instantiator(nonPublic, global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(args, (object arg) => arg.GetType())))?.Invoke(args);
		}

		public static object Instantiate(this global::System.Type type, bool nonPublic = true, params object[] args)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			global::System.Type[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(args, (object arg) => arg.GetType()));
			return (type.Instantiator(nonPublic, array) ?? throw new global::System.ArgumentException(string.Format("Type {0} cannot be{1} instantiated with the provided parameter types: {2}", type, nonPublic ? "" : " publicly", array.ToCommaSeparatedString())))(args);
		}

		public static object Default(this global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			if (type.IsReferenceType())
			{
				return null;
			}
			if (!defaultPrimitives.TryGetValue(type, out var value))
			{
				return global::System.Activator.CreateInstance(type);
			}
			return value;
		}

		public static object PseudoDefault(this global::System.Type type)
		{
			if (type == typeof(global::UnityEngine.Color))
			{
				return global::UnityEngine.Color.white;
			}
			if (type == typeof(string))
			{
				return string.Empty;
			}
			if (type.IsEnum)
			{
				global::System.Array values = global::System.Enum.GetValues(type);
				if (values.Length == 0)
				{
					global::UnityEngine.Debug.LogWarning($"Empty enum: {type}\nThis may cause problems with serialization.");
					return global::System.Activator.CreateInstance(type);
				}
				global::System.ComponentModel.DefaultValueAttribute attribute = type.GetAttribute<global::System.ComponentModel.DefaultValueAttribute>();
				if (attribute != null)
				{
					return attribute.Value;
				}
				return values.GetValue(0);
			}
			return type.Default();
		}

		public static bool IsStatic(this global::System.Type type)
		{
			if (type.IsAbstract)
			{
				return type.IsSealed;
			}
			return false;
		}

		public static bool IsAbstract(this global::System.Type type)
		{
			if (type.IsAbstract)
			{
				return !type.IsSealed;
			}
			return false;
		}

		public static bool IsConcrete(this global::System.Type type)
		{
			if (!type.IsAbstract && !type.IsInterface)
			{
				return !type.ContainsGenericParameters;
			}
			return false;
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Type> GetInterfaces(this global::System.Type type, bool includeInherited)
		{
			if (includeInherited || type.BaseType == null)
			{
				return type.GetInterfaces();
			}
			return global::System.Linq.Enumerable.Except(type.GetInterfaces(), type.BaseType.GetInterfaces());
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Type> BaseTypeAndInterfaces(this global::System.Type type, bool inheritedInterfaces = true)
		{
			global::System.Collections.Generic.IEnumerable<global::System.Type> first = global::System.Linq.Enumerable.Empty<global::System.Type>();
			if (type.BaseType != null)
			{
				first = global::System.Linq.Enumerable.Concat(first, type.BaseType.Yield());
			}
			return global::System.Linq.Enumerable.Concat(first, type.GetInterfaces(inheritedInterfaces));
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Type> Hierarchy(this global::System.Type type)
		{
			global::System.Type baseType = type.BaseType;
			while (baseType != null)
			{
				yield return baseType;
				foreach (global::System.Type @interface in baseType.GetInterfaces(includeInherited: false))
				{
					yield return @interface;
				}
				baseType = baseType.BaseType;
			}
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Type> AndBaseTypeAndInterfaces(this global::System.Type type)
		{
			return global::System.Linq.Enumerable.Concat(type.Yield(), type.BaseTypeAndInterfaces());
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Type> AndInterfaces(this global::System.Type type)
		{
			return global::System.Linq.Enumerable.Concat(type.Yield(), type.GetInterfaces());
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Type> AndHierarchy(this global::System.Type type)
		{
			return global::System.Linq.Enumerable.Concat(type.Yield(), type.Hierarchy());
		}

		public static global::System.Type GetListElementType(global::System.Type listType, bool allowNonGeneric)
		{
			if (listType == null)
			{
				throw new global::System.ArgumentNullException("listType");
			}
			if (listType.IsArray)
			{
				return listType.GetElementType();
			}
			if (typeof(global::System.Collections.IList).IsAssignableFrom(listType))
			{
				global::System.Type type = global::System.Linq.Enumerable.FirstOrDefault(listType.AndInterfaces(), (global::System.Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(global::System.Collections.Generic.IList<>));
				if (type == null)
				{
					if (allowNonGeneric)
					{
						return typeof(object);
					}
					return null;
				}
				return type.GetGenericArguments()[0];
			}
			return null;
		}

		public static global::System.Type GetEnumerableElementType(global::System.Type enumerableType, bool allowNonGeneric)
		{
			if (enumerableType == null)
			{
				throw new global::System.ArgumentNullException("enumerableType");
			}
			if (typeof(global::System.Collections.IEnumerable).IsAssignableFrom(enumerableType))
			{
				global::System.Type type = global::System.Linq.Enumerable.FirstOrDefault(enumerableType.AndInterfaces(), (global::System.Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(global::System.Collections.Generic.IEnumerable<>));
				if (type == null)
				{
					if (allowNonGeneric)
					{
						return typeof(object);
					}
					return null;
				}
				return type.GetGenericArguments()[0];
			}
			return null;
		}

		public static global::System.Type GetDictionaryItemType(global::System.Type dictionaryType, bool allowNonGeneric, int genericArgumentIndex)
		{
			if (dictionaryType == null)
			{
				throw new global::System.ArgumentNullException("dictionaryType");
			}
			if (typeof(global::System.Collections.IDictionary).IsAssignableFrom(dictionaryType))
			{
				global::System.Type type = global::System.Linq.Enumerable.FirstOrDefault(dictionaryType.AndInterfaces(), (global::System.Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(global::System.Collections.Generic.IDictionary<, >));
				if (type == null)
				{
					if (allowNonGeneric)
					{
						return typeof(object);
					}
					return null;
				}
				return type.GetGenericArguments()[genericArgumentIndex];
			}
			return null;
		}

		public static global::System.Type GetDictionaryKeyType(global::System.Type dictionaryType, bool allowNonGeneric)
		{
			return GetDictionaryItemType(dictionaryType, allowNonGeneric, 0);
		}

		public static global::System.Type GetDictionaryValueType(global::System.Type dictionaryType, bool allowNonGeneric)
		{
			return GetDictionaryItemType(dictionaryType, allowNonGeneric, 1);
		}

		public static bool IsNullable(this global::System.Type type)
		{
			if (!type.IsReferenceType())
			{
				return global::System.Nullable.GetUnderlyingType(type) != null;
			}
			return true;
		}

		public static bool IsReferenceType(this global::System.Type type)
		{
			return !type.IsValueType;
		}

		public static bool IsStruct(this global::System.Type type)
		{
			if (type.IsValueType && !type.IsPrimitive)
			{
				return !type.IsEnum;
			}
			return false;
		}

		public static bool IsAssignableFrom(this global::System.Type type, object value)
		{
			if (value == null)
			{
				return type.IsNullable();
			}
			return type.IsInstanceOfType(value);
		}

		public static bool CanMakeGenericTypeVia(this global::System.Type openConstructedType, global::System.Type closedConstructedType)
		{
			global::Unity.VisualScripting.Ensure.That("openConstructedType").IsNotNull(openConstructedType);
			global::Unity.VisualScripting.Ensure.That("closedConstructedType").IsNotNull(closedConstructedType);
			if (openConstructedType == closedConstructedType)
			{
				return true;
			}
			if (openConstructedType.IsGenericParameter)
			{
				global::System.Reflection.GenericParameterAttributes genericParameterAttributes = openConstructedType.GenericParameterAttributes;
				if (genericParameterAttributes != global::System.Reflection.GenericParameterAttributes.None)
				{
					if (genericParameterAttributes.HasFlag(global::System.Reflection.GenericParameterAttributes.NotNullableValueTypeConstraint) && !closedConstructedType.IsValueType)
					{
						return false;
					}
					if (genericParameterAttributes.HasFlag(global::System.Reflection.GenericParameterAttributes.ReferenceTypeConstraint) && closedConstructedType.IsValueType)
					{
						return false;
					}
					if (genericParameterAttributes.HasFlag(global::System.Reflection.GenericParameterAttributes.DefaultConstructorConstraint) && closedConstructedType.GetConstructor(global::System.Type.EmptyTypes) == null)
					{
						return false;
					}
				}
				global::System.Type[] genericParameterConstraints = openConstructedType.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(closedConstructedType))
					{
						return false;
					}
				}
				return true;
			}
			if (openConstructedType.ContainsGenericParameters)
			{
				if (openConstructedType.IsGenericType)
				{
					global::System.Type genericTypeDefinition = openConstructedType.GetGenericTypeDefinition();
					foreach (global::System.Type item in closedConstructedType.AndBaseTypeAndInterfaces())
					{
						if (!item.IsGenericType || !(item.GetGenericTypeDefinition() == genericTypeDefinition))
						{
							continue;
						}
						global::System.Type[] genericArguments = item.GetGenericArguments();
						global::System.Type[] genericArguments2 = openConstructedType.GetGenericArguments();
						for (int j = 0; j < genericArguments2.Length; j++)
						{
							if (!genericArguments2[j].CanMakeGenericTypeVia(genericArguments[j]))
							{
								return false;
							}
						}
						return true;
					}
					return false;
				}
				if (openConstructedType.IsArray)
				{
					if (!closedConstructedType.IsArray || closedConstructedType.GetArrayRank() != openConstructedType.GetArrayRank())
					{
						return false;
					}
					global::System.Type elementType = openConstructedType.GetElementType();
					global::System.Type elementType2 = closedConstructedType.GetElementType();
					return elementType.CanMakeGenericTypeVia(elementType2);
				}
				if (openConstructedType.IsByRef)
				{
					if (!closedConstructedType.IsByRef)
					{
						return false;
					}
					global::System.Type elementType3 = openConstructedType.GetElementType();
					global::System.Type elementType4 = closedConstructedType.GetElementType();
					return elementType3.CanMakeGenericTypeVia(elementType4);
				}
				throw new global::System.NotImplementedException();
			}
			return openConstructedType.IsAssignableFrom(closedConstructedType);
		}

		public static global::System.Type MakeGenericTypeVia(this global::System.Type openConstructedType, global::System.Type closedConstructedType, global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Type> resolvedGenericParameters, bool safe = true)
		{
			global::Unity.VisualScripting.Ensure.That("openConstructedType").IsNotNull(openConstructedType);
			global::Unity.VisualScripting.Ensure.That("closedConstructedType").IsNotNull(closedConstructedType);
			global::Unity.VisualScripting.Ensure.That("resolvedGenericParameters").IsNotNull(resolvedGenericParameters);
			if (safe && !openConstructedType.CanMakeGenericTypeVia(closedConstructedType))
			{
				throw new global::Unity.VisualScripting.GenericClosingException(openConstructedType, closedConstructedType);
			}
			if (openConstructedType == closedConstructedType)
			{
				return openConstructedType;
			}
			if (openConstructedType.IsGenericParameter)
			{
				if (!closedConstructedType.ContainsGenericParameters)
				{
					if (resolvedGenericParameters.ContainsKey(openConstructedType))
					{
						if (resolvedGenericParameters[openConstructedType] != closedConstructedType)
						{
							throw new global::System.InvalidOperationException("Nested generic parameters resolve to different values.");
						}
					}
					else
					{
						resolvedGenericParameters.Add(openConstructedType, closedConstructedType);
					}
				}
				return closedConstructedType;
			}
			if (openConstructedType.ContainsGenericParameters)
			{
				if (openConstructedType.IsGenericType)
				{
					global::System.Type genericTypeDefinition = openConstructedType.GetGenericTypeDefinition();
					global::System.Type[] genericArguments = openConstructedType.GetGenericArguments();
					foreach (global::System.Type item in closedConstructedType.AndBaseTypeAndInterfaces())
					{
						if (item.IsGenericType && item.GetGenericTypeDefinition() == genericTypeDefinition)
						{
							global::System.Type[] genericArguments2 = item.GetGenericArguments();
							global::System.Type[] array = new global::System.Type[genericArguments.Length];
							for (int i = 0; i < genericArguments.Length; i++)
							{
								array[i] = genericArguments[i].MakeGenericTypeVia(genericArguments2[i], resolvedGenericParameters, safe: false);
							}
							return genericTypeDefinition.MakeGenericType(array);
						}
					}
					throw new global::Unity.VisualScripting.GenericClosingException(openConstructedType, closedConstructedType);
				}
				if (openConstructedType.IsArray)
				{
					int arrayRank = openConstructedType.GetArrayRank();
					if (!closedConstructedType.IsArray || closedConstructedType.GetArrayRank() != arrayRank)
					{
						throw new global::Unity.VisualScripting.GenericClosingException(openConstructedType, closedConstructedType);
					}
					global::System.Type elementType = openConstructedType.GetElementType();
					global::System.Type elementType2 = closedConstructedType.GetElementType();
					return elementType.MakeGenericTypeVia(elementType2, resolvedGenericParameters, safe: false).MakeArrayType(arrayRank);
				}
				if (openConstructedType.IsByRef)
				{
					if (!closedConstructedType.IsByRef)
					{
						throw new global::Unity.VisualScripting.GenericClosingException(openConstructedType, closedConstructedType);
					}
					global::System.Type elementType3 = openConstructedType.GetElementType();
					global::System.Type elementType4 = closedConstructedType.GetElementType();
					return elementType3.MakeGenericTypeVia(elementType4, resolvedGenericParameters, safe: false).MakeByRefType();
				}
				throw new global::System.NotImplementedException();
			}
			return openConstructedType;
		}

		public static string ToShortString(this object o, int maxLength = 20)
		{
			global::System.Type type = o?.GetType();
			if (type == null || o.IsUnityNull())
			{
				return "Null";
			}
			if (type == typeof(float))
			{
				return ((float)o).ToString("0.##");
			}
			if (type == typeof(double))
			{
				return ((double)o).ToString("0.##");
			}
			if (type == typeof(decimal))
			{
				return ((decimal)o).ToString("0.##");
			}
			if (type.IsBasic() || typesWithShortStrings.Contains(type))
			{
				return o.ToString().Truncate(maxLength);
			}
			if (typeof(global::UnityEngine.Object).IsAssignableFrom(type))
			{
				return ((global::UnityEngine.Object)o).name.Truncate(maxLength);
			}
			return null;
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Type> GetTypesSafely(this global::System.Reflection.Assembly assembly)
		{
			global::System.Type[] array;
			try
			{
				array = assembly.GetTypes();
			}
			catch (global::System.Reflection.ReflectionTypeLoadException ex) when (global::System.Linq.Enumerable.Any(ex.Types, (global::System.Type t) => t != null))
			{
				array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(ex.Types, (global::System.Type t) => t != null));
			}
			catch (global::System.Exception arg)
			{
				global::UnityEngine.Debug.LogWarning($"Failed to load types in assembly '{assembly}'.\n{arg}");
				yield break;
			}
			global::System.Type[] array2 = array;
			foreach (global::System.Type type in array2)
			{
				if (!(type == typeof(void)))
				{
					yield return type;
				}
			}
		}
	}
}
