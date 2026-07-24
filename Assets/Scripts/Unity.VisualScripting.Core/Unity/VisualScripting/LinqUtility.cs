namespace Unity.VisualScripting
{
	public static class LinqUtility
	{
		public static global::System.Collections.Generic.IEnumerable<T> Concat<T>(params global::System.Collections.IEnumerable[] enumerables)
		{
			foreach (global::System.Collections.IEnumerable item in enumerables.NotNull())
			{
				foreach (T item2 in global::System.Linq.Enumerable.OfType<T>(item))
				{
					yield return item2;
				}
			}
		}

		public static global::System.Collections.Generic.IEnumerable<T> DistinctBy<T, TKey>(this global::System.Collections.Generic.IEnumerable<T> items, global::System.Func<T, TKey> property)
		{
			return global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.GroupBy(items, property), (global::System.Linq.IGrouping<TKey, T> x) => global::System.Linq.Enumerable.First(x));
		}

		public static global::System.Collections.Generic.IEnumerable<T> NotNull<T>(this global::System.Collections.Generic.IEnumerable<T> enumerable)
		{
			return global::System.Linq.Enumerable.Where(enumerable, (T i) => i != null);
		}

		public static global::System.Collections.Generic.IEnumerable<T> Yield<T>(this T t)
		{
			yield return t;
		}

		public static global::System.Collections.Generic.HashSet<T> ToHashSet<T>(this global::System.Collections.Generic.IEnumerable<T> enumerable)
		{
			return new global::System.Collections.Generic.HashSet<T>(enumerable);
		}

		public static void AddRange<T>(this global::System.Collections.Generic.ICollection<T> collection, global::System.Collections.Generic.IEnumerable<T> items)
		{
			foreach (T item in items)
			{
				collection.Add(item);
			}
		}

		public static void AddRange(this global::System.Collections.IList list, global::System.Collections.IEnumerable items)
		{
			foreach (object item in items)
			{
				list.Add(item);
			}
		}

		public static global::System.Collections.Generic.ICollection<T> AsReadOnlyCollection<T>(this global::System.Collections.Generic.IEnumerable<T> enumerable)
		{
			if (enumerable is global::System.Collections.Generic.ICollection<T>)
			{
				return (global::System.Collections.Generic.ICollection<T>)enumerable;
			}
			return global::System.Linq.Enumerable.ToList(enumerable).AsReadOnly();
		}

		public static global::System.Collections.Generic.IList<T> AsReadOnlyList<T>(this global::System.Collections.Generic.IEnumerable<T> enumerable)
		{
			if (enumerable is global::System.Collections.Generic.IList<T>)
			{
				return (global::System.Collections.Generic.IList<T>)enumerable;
			}
			return global::System.Linq.Enumerable.ToList(enumerable).AsReadOnly();
		}

		public static global::System.Collections.Generic.IEnumerable<T> Flatten<T>(this global::System.Collections.Generic.IEnumerable<T> source, global::System.Func<T, global::System.Collections.Generic.IEnumerable<T>> childrenSelector)
		{
			global::System.Collections.Generic.IEnumerable<T> enumerable = source;
			foreach (T item in source)
			{
				enumerable = global::System.Linq.Enumerable.Concat(enumerable, childrenSelector(item).Flatten(childrenSelector));
			}
			return enumerable;
		}

		public static global::System.Collections.Generic.IEnumerable<T> IntersectAll<T>(this global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.IEnumerable<T>> groups)
		{
			global::System.Collections.Generic.HashSet<T> hashSet = null;
			foreach (global::System.Collections.Generic.IEnumerable<T> group in groups)
			{
				if (hashSet == null)
				{
					hashSet = new global::System.Collections.Generic.HashSet<T>(group);
				}
				else
				{
					hashSet.IntersectWith(group);
				}
			}
			if (hashSet != null)
			{
				return global::System.Linq.Enumerable.AsEnumerable(hashSet);
			}
			return global::System.Linq.Enumerable.Empty<T>();
		}

		public static global::System.Collections.Generic.IEnumerable<T> OrderByDependencies<T>(this global::System.Collections.Generic.IEnumerable<T> source, global::System.Func<T, global::System.Collections.Generic.IEnumerable<T>> getDependencies, bool throwOnCycle = true)
		{
			global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>();
			global::System.Collections.Generic.HashSet<T> hashSet = global::Unity.VisualScripting.HashSetPool<T>.New();
			foreach (T item in source)
			{
				OrderByDependenciesVisit(item, hashSet, list, getDependencies, throwOnCycle);
			}
			global::Unity.VisualScripting.HashSetPool<T>.Free(hashSet);
			return list;
		}

		private static void OrderByDependenciesVisit<T>(T item, global::System.Collections.Generic.HashSet<T> visited, global::System.Collections.Generic.List<T> sorted, global::System.Func<T, global::System.Collections.Generic.IEnumerable<T>> getDependencies, bool throwOnCycle)
		{
			if (!visited.Contains(item))
			{
				visited.Add(item);
				foreach (T item2 in getDependencies(item))
				{
					OrderByDependenciesVisit(item2, visited, sorted, getDependencies, throwOnCycle);
				}
				sorted.Add(item);
			}
			else if (throwOnCycle && !sorted.Contains(item))
			{
				throw new global::System.InvalidOperationException("Cyclic dependency.");
			}
		}

		public static global::System.Collections.Generic.IEnumerable<T> OrderByDependers<T>(this global::System.Collections.Generic.IEnumerable<T> source, global::System.Func<T, global::System.Collections.Generic.IEnumerable<T>> getDependers, bool throwOnCycle = true)
		{
			global::System.Collections.Generic.Dictionary<T, global::System.Collections.Generic.HashSet<T>> dependencies = new global::System.Collections.Generic.Dictionary<T, global::System.Collections.Generic.HashSet<T>>();
			foreach (T item in source)
			{
				foreach (T item2 in getDependers(item))
				{
					if (!dependencies.ContainsKey(item2))
					{
						dependencies.Add(item2, new global::System.Collections.Generic.HashSet<T>());
					}
					dependencies[item2].Add(item);
				}
			}
			return source.OrderByDependencies((T depender) => dependencies.ContainsKey(depender) ? dependencies[depender] : global::System.Linq.Enumerable.Empty<T>(), throwOnCycle);
		}

		public static global::System.Collections.Generic.IEnumerable<T> Catch<T>(this global::System.Collections.Generic.IEnumerable<T> source, global::System.Action<global::System.Exception> @catch)
		{
			global::Unity.VisualScripting.Ensure.That("source").IsNotNull(source);
			using global::System.Collections.Generic.IEnumerator<T> enumerator = source.GetEnumerator();
			bool success;
			do
			{
				try
				{
					success = enumerator.MoveNext();
				}
				catch (global::System.OperationCanceledException)
				{
					yield break;
				}
				catch (global::System.Exception obj)
				{
					@catch?.Invoke(obj);
					success = false;
				}
				if (success)
				{
					yield return enumerator.Current;
				}
			}
			while (success);
		}

		public static global::System.Collections.Generic.IEnumerable<T> Catch<T>(this global::System.Collections.Generic.IEnumerable<T> source, global::System.Collections.Generic.ICollection<global::System.Exception> exceptions)
		{
			global::Unity.VisualScripting.Ensure.That("exceptions").IsNotNull(exceptions);
			return source.Catch(exceptions.Add);
		}

		public static global::System.Collections.Generic.IEnumerable<T> CatchAsLogError<T>(this global::System.Collections.Generic.IEnumerable<T> source, string message)
		{
			return source.Catch(delegate(global::System.Exception ex)
			{
				global::UnityEngine.Debug.LogError(message + "\n" + ex.ToString());
			});
		}

		public static global::System.Collections.Generic.IEnumerable<T> CatchAsLogWarning<T>(this global::System.Collections.Generic.IEnumerable<T> source, string message)
		{
			return source.Catch(delegate(global::System.Exception ex)
			{
				global::UnityEngine.Debug.LogWarning(message + "\n" + ex.ToString());
			});
		}
	}
}
