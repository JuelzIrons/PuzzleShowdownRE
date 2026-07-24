namespace Unity.VisualScripting
{
	public static class ConversionUtility
	{
		public enum ConversionType
		{
			Impossible = 0,
			Identity = 1,
			Upcast = 2,
			Downcast = 3,
			NumericImplicit = 4,
			NumericExplicit = 5,
			UserDefinedImplicit = 6,
			UserDefinedExplicit = 7,
			UserDefinedThenNumericImplicit = 8,
			UserDefinedThenNumericExplicit = 9,
			UnityHierarchy = 10,
			EnumerableToArray = 11,
			EnumerableToList = 12,
			ToString = 13
		}

		private struct ConversionQuery : global::System.IEquatable<global::Unity.VisualScripting.ConversionUtility.ConversionQuery>
		{
			public readonly global::System.Type source;

			public readonly global::System.Type destination;

			public ConversionQuery(global::System.Type source, global::System.Type destination)
			{
				this.source = source;
				this.destination = destination;
			}

			public bool Equals(global::Unity.VisualScripting.ConversionUtility.ConversionQuery other)
			{
				if (source == other.source)
				{
					return destination == other.destination;
				}
				return false;
			}

			public override bool Equals(object obj)
			{
				if (!(obj is global::Unity.VisualScripting.ConversionUtility.ConversionQuery))
				{
					return false;
				}
				return Equals((global::Unity.VisualScripting.ConversionUtility.ConversionQuery)obj);
			}

			public override int GetHashCode()
			{
				return global::Unity.VisualScripting.HashUtility.GetHashCode(source, destination);
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct ConversionQueryComparer : global::System.Collections.Generic.IEqualityComparer<global::Unity.VisualScripting.ConversionUtility.ConversionQuery>
		{
			public bool Equals(global::Unity.VisualScripting.ConversionUtility.ConversionQuery x, global::Unity.VisualScripting.ConversionUtility.ConversionQuery y)
			{
				return x.Equals(y);
			}

			public int GetHashCode(global::Unity.VisualScripting.ConversionUtility.ConversionQuery obj)
			{
				return obj.GetHashCode();
			}
		}

		private const global::System.Reflection.BindingFlags UserDefinedBindingFlags = global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public;

		private static readonly global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.ConversionUtility.ConversionQuery, global::Unity.VisualScripting.ConversionUtility.ConversionType> conversionTypesCache = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.ConversionUtility.ConversionQuery, global::Unity.VisualScripting.ConversionUtility.ConversionType>(default(global::Unity.VisualScripting.ConversionUtility.ConversionQueryComparer));

		private static readonly global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.ConversionUtility.ConversionQuery, global::System.Reflection.MethodInfo[]> userConversionMethodsCache = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.ConversionUtility.ConversionQuery, global::System.Reflection.MethodInfo[]>(default(global::Unity.VisualScripting.ConversionUtility.ConversionQueryComparer));

		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.HashSet<global::System.Type>> implicitNumericConversions = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.HashSet<global::System.Type>>
		{
			{
				typeof(sbyte),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(byte),
					typeof(int),
					typeof(long),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(byte),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(short),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(int),
					typeof(long),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(ushort),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(int),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(long),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(uint),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(long),
					typeof(ulong),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(long),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(char),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			},
			{
				typeof(float),
				new global::System.Collections.Generic.HashSet<global::System.Type> { typeof(double) }
			},
			{
				typeof(ulong),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(float),
					typeof(double),
					typeof(decimal)
				}
			}
		};

		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.HashSet<global::System.Type>> explicitNumericConversions = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.HashSet<global::System.Type>>
		{
			{
				typeof(sbyte),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(byte),
					typeof(ushort),
					typeof(uint),
					typeof(ulong),
					typeof(char)
				}
			},
			{
				typeof(byte),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(sbyte),
					typeof(char)
				}
			},
			{
				typeof(short),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(ushort),
					typeof(uint),
					typeof(ulong),
					typeof(char)
				}
			},
			{
				typeof(ushort),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(char)
				}
			},
			{
				typeof(int),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(uint),
					typeof(ulong),
					typeof(char)
				}
			},
			{
				typeof(uint),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(char)
				}
			},
			{
				typeof(long),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(ulong),
					typeof(char)
				}
			},
			{
				typeof(ulong),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(char)
				}
			},
			{
				typeof(char),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short)
				}
			},
			{
				typeof(float),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(char),
					typeof(decimal)
				}
			},
			{
				typeof(double),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(char),
					typeof(float),
					typeof(decimal)
				}
			},
			{
				typeof(decimal),
				new global::System.Collections.Generic.HashSet<global::System.Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(char),
					typeof(float),
					typeof(double)
				}
			}
		};

		private static bool RespectsIdentity(global::System.Type source, global::System.Type destination)
		{
			return source == destination;
		}

		private static bool IsUpcast(global::System.Type source, global::System.Type destination)
		{
			return destination.IsAssignableFrom(source);
		}

		private static bool IsDowncast(global::System.Type source, global::System.Type destination)
		{
			return source.IsAssignableFrom(destination);
		}

		private static bool ExpectsString(global::System.Type source, global::System.Type destination)
		{
			return destination == typeof(string);
		}

		public static bool HasImplicitNumericConversion(global::System.Type source, global::System.Type destination)
		{
			if (implicitNumericConversions.ContainsKey(source))
			{
				return implicitNumericConversions[source].Contains(destination);
			}
			return false;
		}

		public static bool HasExplicitNumericConversion(global::System.Type source, global::System.Type destination)
		{
			if (explicitNumericConversions.ContainsKey(source))
			{
				return explicitNumericConversions[source].Contains(destination);
			}
			return false;
		}

		public static bool HasNumericConversion(global::System.Type source, global::System.Type destination)
		{
			if (!HasImplicitNumericConversion(source, destination))
			{
				return HasExplicitNumericConversion(source, destination);
			}
			return true;
		}

		private static global::System.Collections.Generic.IEnumerable<global::System.Reflection.MethodInfo> FindUserDefinedConversionMethods(global::Unity.VisualScripting.ConversionUtility.ConversionQuery query)
		{
			global::System.Type source = query.source;
			global::System.Type destination = query.destination;
			global::System.Collections.Generic.IEnumerable<global::System.Reflection.MethodInfo> first = global::System.Linq.Enumerable.Where(source.GetMethods(global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public), (global::System.Reflection.MethodInfo m) => m.IsUserDefinedConversion());
			global::System.Collections.Generic.IEnumerable<global::System.Reflection.MethodInfo> second = global::System.Linq.Enumerable.Where(destination.GetMethods(global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public), (global::System.Reflection.MethodInfo m) => m.IsUserDefinedConversion());
			return global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.Concat(first, second), (global::System.Reflection.MethodInfo m) => m.GetParameters()[0].ParameterType.IsAssignableFrom(source) || source.IsAssignableFrom(m.GetParameters()[0].ParameterType));
		}

		private static global::System.Reflection.MethodInfo[] GetUserDefinedConversionMethods(global::System.Type source, global::System.Type destination)
		{
			global::Unity.VisualScripting.ConversionUtility.ConversionQuery conversionQuery = new global::Unity.VisualScripting.ConversionUtility.ConversionQuery(source, destination);
			if (!userConversionMethodsCache.ContainsKey(conversionQuery))
			{
				userConversionMethodsCache.Add(conversionQuery, global::System.Linq.Enumerable.ToArray(FindUserDefinedConversionMethods(conversionQuery)));
			}
			return userConversionMethodsCache[conversionQuery];
		}

		private static global::Unity.VisualScripting.ConversionUtility.ConversionType GetUserDefinedConversionType(global::System.Type source, global::System.Type destination)
		{
			global::System.Reflection.MethodInfo[] userDefinedConversionMethods = GetUserDefinedConversionMethods(source, destination);
			global::System.Reflection.MethodInfo methodInfo = global::System.Linq.Enumerable.FirstOrDefault(userDefinedConversionMethods, (global::System.Reflection.MethodInfo m) => m.ReturnType == destination);
			if (methodInfo != null)
			{
				if (methodInfo.Name == "op_Implicit")
				{
					return global::Unity.VisualScripting.ConversionUtility.ConversionType.UserDefinedImplicit;
				}
				if (methodInfo.Name == "op_Explicit")
				{
					return global::Unity.VisualScripting.ConversionUtility.ConversionType.UserDefinedExplicit;
				}
			}
			else if (destination.IsPrimitive && destination != typeof(global::System.IntPtr) && destination != typeof(global::System.UIntPtr))
			{
				methodInfo = global::System.Linq.Enumerable.FirstOrDefault(userDefinedConversionMethods, (global::System.Reflection.MethodInfo m) => HasImplicitNumericConversion(m.ReturnType, destination));
				if (methodInfo != null)
				{
					if (methodInfo.Name == "op_Implicit")
					{
						return global::Unity.VisualScripting.ConversionUtility.ConversionType.UserDefinedThenNumericImplicit;
					}
					if (methodInfo.Name == "op_Explicit")
					{
						return global::Unity.VisualScripting.ConversionUtility.ConversionType.UserDefinedThenNumericExplicit;
					}
				}
				else
				{
					methodInfo = global::System.Linq.Enumerable.FirstOrDefault(userDefinedConversionMethods, (global::System.Reflection.MethodInfo m) => HasExplicitNumericConversion(m.ReturnType, destination));
					if (methodInfo != null)
					{
						return global::Unity.VisualScripting.ConversionUtility.ConversionType.UserDefinedThenNumericExplicit;
					}
				}
			}
			return global::Unity.VisualScripting.ConversionUtility.ConversionType.Impossible;
		}

		private static bool HasEnumerableToArrayConversion(global::System.Type source, global::System.Type destination)
		{
			if (source != typeof(string) && typeof(global::System.Collections.IEnumerable).IsAssignableFrom(source) && destination.IsArray)
			{
				return destination.GetArrayRank() == 1;
			}
			return false;
		}

		private static bool HasEnumerableToListConversion(global::System.Type source, global::System.Type destination)
		{
			if (source != typeof(string) && typeof(global::System.Collections.IEnumerable).IsAssignableFrom(source) && destination.IsGenericType)
			{
				return destination.GetGenericTypeDefinition() == typeof(global::System.Collections.Generic.List<>);
			}
			return false;
		}

		private static bool HasUnityHierarchyConversion(global::System.Type source, global::System.Type destination)
		{
			if (destination == typeof(global::UnityEngine.GameObject))
			{
				return typeof(global::UnityEngine.Component).IsAssignableFrom(source);
			}
			if (typeof(global::UnityEngine.Component).IsAssignableFrom(destination) || destination.IsInterface)
			{
				if (!(source == typeof(global::UnityEngine.GameObject)))
				{
					return typeof(global::UnityEngine.Component).IsAssignableFrom(source);
				}
				return true;
			}
			return false;
		}

		private static bool IsValidConversion(global::Unity.VisualScripting.ConversionUtility.ConversionType conversionType, bool guaranteed)
		{
			if (conversionType == global::Unity.VisualScripting.ConversionUtility.ConversionType.Impossible)
			{
				return false;
			}
			if (guaranteed && conversionType == global::Unity.VisualScripting.ConversionUtility.ConversionType.Downcast)
			{
				return false;
			}
			return true;
		}

		public static bool CanConvert(object value, global::System.Type type, bool guaranteed)
		{
			return IsValidConversion(GetRequiredConversion(value, type), guaranteed);
		}

		public static bool CanConvert(global::System.Type source, global::System.Type destination, bool guaranteed)
		{
			return IsValidConversion(GetRequiredConversion(source, destination), guaranteed);
		}

		public static object Convert(object value, global::System.Type type)
		{
			return Convert(value, type, GetRequiredConversion(value, type));
		}

		public static T Convert<T>(object value)
		{
			return (T)Convert(value, typeof(T));
		}

		public static bool TryConvert(object value, global::System.Type type, out object result, bool guaranteed)
		{
			global::Unity.VisualScripting.ConversionUtility.ConversionType requiredConversion = GetRequiredConversion(value, type);
			if (IsValidConversion(requiredConversion, guaranteed))
			{
				result = Convert(value, type, requiredConversion);
				return true;
			}
			result = value;
			return false;
		}

		public static bool TryConvert<T>(object value, out T result, bool guaranteed)
		{
			if (TryConvert(value, typeof(T), out var result2, guaranteed))
			{
				result = (T)result2;
				return true;
			}
			result = default(T);
			return false;
		}

		public static bool IsConvertibleTo(this global::System.Type source, global::System.Type destination, bool guaranteed)
		{
			return CanConvert(source, destination, guaranteed);
		}

		public static bool IsConvertibleTo(this object source, global::System.Type type, bool guaranteed)
		{
			return CanConvert(source, type, guaranteed);
		}

		public static bool IsConvertibleTo<T>(this object source, bool guaranteed)
		{
			return CanConvert(source, typeof(T), guaranteed);
		}

		public static object ConvertTo(this object source, global::System.Type type)
		{
			return Convert(source, type);
		}

		public static T ConvertTo<T>(this object source)
		{
			return (T)Convert(source, typeof(T));
		}

		public static global::Unity.VisualScripting.ConversionUtility.ConversionType GetRequiredConversion(global::System.Type source, global::System.Type destination)
		{
			global::Unity.VisualScripting.ConversionUtility.ConversionQuery conversionQuery = new global::Unity.VisualScripting.ConversionUtility.ConversionQuery(source, destination);
			if (!conversionTypesCache.TryGetValue(conversionQuery, out var value))
			{
				value = DetermineConversionType(conversionQuery);
				conversionTypesCache.Add(conversionQuery, value);
			}
			return value;
		}

		private static global::Unity.VisualScripting.ConversionUtility.ConversionType DetermineConversionType(global::Unity.VisualScripting.ConversionUtility.ConversionQuery query)
		{
			global::System.Type source = query.source;
			global::System.Type destination = query.destination;
			if (source == null)
			{
				if (destination.IsNullable())
				{
					return global::Unity.VisualScripting.ConversionUtility.ConversionType.Identity;
				}
				return global::Unity.VisualScripting.ConversionUtility.ConversionType.Impossible;
			}
			global::Unity.VisualScripting.Ensure.That("destination").IsNotNull(destination);
			if (RespectsIdentity(source, destination))
			{
				return global::Unity.VisualScripting.ConversionUtility.ConversionType.Identity;
			}
			if (IsUpcast(source, destination))
			{
				return global::Unity.VisualScripting.ConversionUtility.ConversionType.Upcast;
			}
			if (IsDowncast(source, destination))
			{
				return global::Unity.VisualScripting.ConversionUtility.ConversionType.Downcast;
			}
			if (HasImplicitNumericConversion(source, destination))
			{
				return global::Unity.VisualScripting.ConversionUtility.ConversionType.NumericImplicit;
			}
			if (HasExplicitNumericConversion(source, destination))
			{
				return global::Unity.VisualScripting.ConversionUtility.ConversionType.NumericExplicit;
			}
			if (HasUnityHierarchyConversion(source, destination))
			{
				return global::Unity.VisualScripting.ConversionUtility.ConversionType.UnityHierarchy;
			}
			if (HasEnumerableToArrayConversion(source, destination))
			{
				return global::Unity.VisualScripting.ConversionUtility.ConversionType.EnumerableToArray;
			}
			if (HasEnumerableToListConversion(source, destination))
			{
				return global::Unity.VisualScripting.ConversionUtility.ConversionType.EnumerableToList;
			}
			global::Unity.VisualScripting.ConversionUtility.ConversionType userDefinedConversionType = GetUserDefinedConversionType(source, destination);
			if (userDefinedConversionType != global::Unity.VisualScripting.ConversionUtility.ConversionType.Impossible)
			{
				return userDefinedConversionType;
			}
			return global::Unity.VisualScripting.ConversionUtility.ConversionType.Impossible;
		}

		public static global::Unity.VisualScripting.ConversionUtility.ConversionType GetRequiredConversion(object value, global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			return GetRequiredConversion(value?.GetType(), type);
		}

		private static object NumericConversion(object value, global::System.Type type)
		{
			return global::System.Convert.ChangeType(value, type);
		}

		private static object UserDefinedConversion(global::Unity.VisualScripting.ConversionUtility.ConversionType conversion, object value, global::System.Type type)
		{
			global::System.Reflection.MethodInfo[] userDefinedConversionMethods = GetUserDefinedConversionMethods(value.GetType(), type);
			bool flag = conversion == global::Unity.VisualScripting.ConversionUtility.ConversionType.UserDefinedThenNumericImplicit || conversion == global::Unity.VisualScripting.ConversionUtility.ConversionType.UserDefinedThenNumericExplicit;
			global::System.Reflection.MethodInfo methodInfo = null;
			if (flag)
			{
				global::System.Reflection.MethodInfo[] array = userDefinedConversionMethods;
				foreach (global::System.Reflection.MethodInfo methodInfo2 in array)
				{
					if (HasNumericConversion(methodInfo2.ReturnType, type))
					{
						methodInfo = methodInfo2;
						break;
					}
				}
			}
			else
			{
				global::System.Reflection.MethodInfo[] array = userDefinedConversionMethods;
				foreach (global::System.Reflection.MethodInfo methodInfo3 in array)
				{
					if (methodInfo3.ReturnType == type)
					{
						methodInfo = methodInfo3;
						break;
					}
				}
			}
			object obj = methodInfo.InvokeOptimized(null, value);
			if (flag)
			{
				obj = NumericConversion(obj, type);
			}
			return obj;
		}

		private static object EnumerableToArrayConversion(object value, global::System.Type arrayType)
		{
			global::System.Type elementType = arrayType.GetElementType();
			object[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.Cast<object>((global::System.Collections.IEnumerable)value), elementType.IsAssignableFrom));
			global::System.Array array2 = global::System.Array.CreateInstance(elementType, array.Length);
			array.CopyTo(array2, 0);
			return array2;
		}

		private static object EnumerableToListConversion(object value, global::System.Type listType)
		{
			global::System.Type type = listType.GetGenericArguments()[0];
			object[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.Cast<object>((global::System.Collections.IEnumerable)value), type.IsAssignableFrom));
			global::System.Collections.IList list = (global::System.Collections.IList)global::System.Activator.CreateInstance(listType);
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}

		private static object UnityHierarchyConversion(object value, global::System.Type type)
		{
			if (value.IsUnityNull())
			{
				return null;
			}
			if (type == typeof(global::UnityEngine.GameObject) && value is global::UnityEngine.Component)
			{
				return ((global::UnityEngine.Component)value).gameObject;
			}
			if (typeof(global::UnityEngine.Component).IsAssignableFrom(type) || type.IsInterface)
			{
				if (value is global::UnityEngine.Component)
				{
					return ((global::UnityEngine.Component)value).GetComponent(type);
				}
				if (value is global::UnityEngine.GameObject)
				{
					return ((global::UnityEngine.GameObject)value).GetComponent(type);
				}
			}
			throw new global::Unity.VisualScripting.InvalidConversionException();
		}

		private static object Convert(object value, global::System.Type type, global::Unity.VisualScripting.ConversionUtility.ConversionType conversionType)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			if (conversionType == global::Unity.VisualScripting.ConversionUtility.ConversionType.Impossible)
			{
				throw new global::Unity.VisualScripting.InvalidConversionException(string.Format("Cannot convert from '{0}' to '{1}'.", value?.GetType().ToString() ?? "null", type));
			}
			try
			{
				switch (conversionType)
				{
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.Identity:
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.Upcast:
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.Downcast:
					return value;
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.ToString:
					return value.ToString();
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.NumericImplicit:
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.NumericExplicit:
					return NumericConversion(value, type);
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.UserDefinedImplicit:
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.UserDefinedExplicit:
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.UserDefinedThenNumericImplicit:
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.UserDefinedThenNumericExplicit:
					return UserDefinedConversion(conversionType, value, type);
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.EnumerableToArray:
					return EnumerableToArrayConversion(value, type);
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.EnumerableToList:
					return EnumerableToListConversion(value, type);
				case global::Unity.VisualScripting.ConversionUtility.ConversionType.UnityHierarchy:
					return UnityHierarchyConversion(value, type);
				default:
					throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.ConversionUtility.ConversionType>(conversionType);
				}
			}
			catch (global::System.Exception innerException)
			{
				throw new global::Unity.VisualScripting.InvalidConversionException(string.Format("Failed to convert from '{0}' to '{1}' via {2}.", value?.GetType().ToString() ?? "null", type, conversionType), innerException);
			}
		}
	}
}
