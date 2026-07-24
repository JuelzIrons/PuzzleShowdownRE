namespace Newtonsoft.Json.Utilities
{
	internal static class CollectionUtils
	{
		private static class EmptyArrayContainer<T>
		{
			public static readonly T[] Empty = new T[0];
		}

		public static bool IsNullOrEmpty<T>(global::System.Collections.Generic.ICollection<T> collection)
		{
			if (collection != null)
			{
				return collection.Count == 0;
			}
			return true;
		}

		public static void AddRange<T>(this global::System.Collections.Generic.IList<T> initial, global::System.Collections.Generic.IEnumerable<T> collection)
		{
			if (initial == null)
			{
				throw new global::System.ArgumentNullException("initial");
			}
			if (collection == null)
			{
				return;
			}
			foreach (T item in collection)
			{
				initial.Add(item);
			}
		}

		public static bool IsDictionaryType(global::System.Type type)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(type, "type");
			if (typeof(global::System.Collections.IDictionary).IsAssignableFrom(type))
			{
				return true;
			}
			if (global::Newtonsoft.Json.Utilities.ReflectionUtils.ImplementsGenericDefinition(type, typeof(global::System.Collections.Generic.IDictionary<, >)))
			{
				return true;
			}
			if (global::Newtonsoft.Json.Utilities.ReflectionUtils.ImplementsGenericDefinition(type, typeof(global::System.Collections.Generic.IReadOnlyDictionary<, >)))
			{
				return true;
			}
			return false;
		}

		public static global::System.Reflection.ConstructorInfo? ResolveEnumerableCollectionConstructor(global::System.Type collectionType, global::System.Type collectionItemType)
		{
			global::System.Type constructorArgumentType = typeof(global::System.Collections.Generic.IList<>).MakeGenericType(collectionItemType);
			return ResolveEnumerableCollectionConstructor(collectionType, collectionItemType, constructorArgumentType);
		}

		public static global::System.Reflection.ConstructorInfo? ResolveEnumerableCollectionConstructor(global::System.Type collectionType, global::System.Type collectionItemType, global::System.Type constructorArgumentType)
		{
			global::System.Type type = typeof(global::System.Collections.Generic.IEnumerable<>).MakeGenericType(collectionItemType);
			global::System.Reflection.ConstructorInfo constructorInfo = null;
			global::System.Reflection.ConstructorInfo[] constructors = collectionType.GetConstructors(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
			foreach (global::System.Reflection.ConstructorInfo constructorInfo2 in constructors)
			{
				global::System.Collections.Generic.IList<global::System.Reflection.ParameterInfo> parameters = constructorInfo2.GetParameters();
				if (parameters.Count == 1)
				{
					global::System.Type parameterType = parameters[0].ParameterType;
					if (type == parameterType)
					{
						constructorInfo = constructorInfo2;
						break;
					}
					if (constructorInfo == null && parameterType.IsAssignableFrom(constructorArgumentType))
					{
						constructorInfo = constructorInfo2;
					}
				}
			}
			return constructorInfo;
		}

		public static bool AddDistinct<T>(this global::System.Collections.Generic.IList<T> list, T value)
		{
			return list.AddDistinct(value, global::System.Collections.Generic.EqualityComparer<T>.Default);
		}

		public static bool AddDistinct<T>(this global::System.Collections.Generic.IList<T> list, T value, global::System.Collections.Generic.IEqualityComparer<T> comparer)
		{
			if (list.ContainsValue(value, comparer))
			{
				return false;
			}
			list.Add(value);
			return true;
		}

		public static bool ContainsValue<TSource>(this global::System.Collections.Generic.IEnumerable<TSource> source, TSource value, global::System.Collections.Generic.IEqualityComparer<TSource> comparer)
		{
			if (comparer == null)
			{
				comparer = global::System.Collections.Generic.EqualityComparer<TSource>.Default;
			}
			if (source == null)
			{
				throw new global::System.ArgumentNullException("source");
			}
			foreach (TSource item in source)
			{
				if (comparer.Equals(item, value))
				{
					return true;
				}
			}
			return false;
		}

		public static bool AddRangeDistinct<T>(this global::System.Collections.Generic.IList<T> list, global::System.Collections.Generic.IEnumerable<T> values, global::System.Collections.Generic.IEqualityComparer<T> comparer)
		{
			bool result = true;
			foreach (T value in values)
			{
				if (!list.AddDistinct(value, comparer))
				{
					result = false;
				}
			}
			return result;
		}

		public static int IndexOf<T>(this global::System.Collections.Generic.IEnumerable<T> collection, global::System.Func<T, bool> predicate)
		{
			int num = 0;
			foreach (T item in collection)
			{
				if (predicate(item))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		public static bool Contains<T>(this global::System.Collections.Generic.List<T> list, T value, global::System.Collections.IEqualityComparer comparer)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (comparer.Equals(value, list[i]))
				{
					return true;
				}
			}
			return false;
		}

		public static int IndexOfReference<T>(this global::System.Collections.Generic.List<T> list, T item)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if ((object)item == (object)list[i])
				{
					return i;
				}
			}
			return -1;
		}

		public static void FastReverse<T>(this global::System.Collections.Generic.List<T> list)
		{
			int num = 0;
			int num2 = list.Count - 1;
			while (num < num2)
			{
				T value = list[num];
				list[num] = list[num2];
				list[num2] = value;
				num++;
				num2--;
			}
		}

		private static global::System.Collections.Generic.IList<int> GetDimensions(global::System.Collections.IList values, int dimensionsCount)
		{
			global::System.Collections.Generic.IList<int> list = new global::System.Collections.Generic.List<int>();
			global::System.Collections.IList list2 = values;
			while (true)
			{
				list.Add(list2.Count);
				if (list.Count == dimensionsCount || list2.Count == 0 || !(list2[0] is global::System.Collections.IList list3))
				{
					break;
				}
				list2 = list3;
			}
			return list;
		}

		private static void CopyFromJaggedToMultidimensionalArray(global::System.Collections.IList values, global::System.Array multidimensionalArray, int[] indices)
		{
			int num = indices.Length;
			if (num == multidimensionalArray.Rank)
			{
				multidimensionalArray.SetValue(JaggedArrayGetValue(values, indices), indices);
				return;
			}
			int length = multidimensionalArray.GetLength(num);
			if (((global::System.Collections.IList)JaggedArrayGetValue(values, indices)).Count != length)
			{
				throw new global::System.Exception("Cannot deserialize non-cubical array as multidimensional array.");
			}
			int[] array = new int[num + 1];
			for (int i = 0; i < num; i++)
			{
				array[i] = indices[i];
			}
			for (int j = 0; j < multidimensionalArray.GetLength(num); j++)
			{
				array[num] = j;
				CopyFromJaggedToMultidimensionalArray(values, multidimensionalArray, array);
			}
		}

		private static object JaggedArrayGetValue(global::System.Collections.IList values, int[] indices)
		{
			global::System.Collections.IList list = values;
			for (int i = 0; i < indices.Length; i++)
			{
				int index = indices[i];
				if (i == indices.Length - 1)
				{
					return list[index];
				}
				list = (global::System.Collections.IList)list[index];
			}
			return list;
		}

		public static global::System.Array ToMultidimensionalArray(global::System.Collections.IList values, global::System.Type type, int rank)
		{
			global::System.Collections.Generic.IList<int> dimensions = GetDimensions(values, rank);
			while (dimensions.Count < rank)
			{
				dimensions.Add(0);
			}
			global::System.Array array = global::System.Array.CreateInstance(type, global::System.Linq.Enumerable.ToArray(dimensions));
			CopyFromJaggedToMultidimensionalArray(values, array, ArrayEmpty<int>());
			return array;
		}

		public static T[] ArrayEmpty<T>()
		{
			return global::Newtonsoft.Json.Utilities.CollectionUtils.EmptyArrayContainer<T>.Empty;
		}
	}
}
