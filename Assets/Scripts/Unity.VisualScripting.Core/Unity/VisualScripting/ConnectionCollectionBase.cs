namespace Unity.VisualScripting
{
	public class ConnectionCollectionBase<TConnection, TSource, TDestination, TCollection> : global::Unity.VisualScripting.IConnectionCollection<TConnection, TSource, TDestination>, global::System.Collections.Generic.ICollection<TConnection>, global::System.Collections.Generic.IEnumerable<TConnection>, global::System.Collections.IEnumerable where TConnection : global::Unity.VisualScripting.IConnection<TSource, TDestination> where TCollection : global::System.Collections.Generic.ICollection<TConnection>
	{
		private readonly global::System.Collections.Generic.Dictionary<TDestination, global::System.Collections.Generic.List<TConnection>> byDestination;

		private readonly global::System.Collections.Generic.Dictionary<TSource, global::System.Collections.Generic.List<TConnection>> bySource;

		protected readonly TCollection collection;

		public global::System.Collections.Generic.IEnumerable<TConnection> this[TSource source] => WithSource(source);

		public global::System.Collections.Generic.IEnumerable<TConnection> this[TDestination destination] => WithDestination(destination);

		public int Count => collection.Count;

		public bool IsReadOnly => false;

		public ConnectionCollectionBase(TCollection collection)
		{
			this.collection = collection;
			bySource = new global::System.Collections.Generic.Dictionary<TSource, global::System.Collections.Generic.List<TConnection>>();
			byDestination = new global::System.Collections.Generic.Dictionary<TDestination, global::System.Collections.Generic.List<TConnection>>();
		}

		public global::System.Collections.Generic.IEnumerator<TConnection> GetEnumerator()
		{
			return collection.GetEnumerator();
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public global::System.Collections.Generic.IEnumerable<TConnection> WithSource(TSource source)
		{
			return WithSourceNoAlloc(source);
		}

		public global::System.Collections.Generic.List<TConnection> WithSourceNoAlloc(TSource source)
		{
			global::Unity.VisualScripting.Ensure.That("source").IsNotNull(source);
			if (bySource.TryGetValue(source, out var value))
			{
				return value;
			}
			return global::Unity.VisualScripting.Empty<TConnection>.list;
		}

		public TConnection SingleOrDefaultWithSource(TSource source)
		{
			global::Unity.VisualScripting.Ensure.That("source").IsNotNull(source);
			if (bySource.TryGetValue(source, out var value))
			{
				if (value.Count == 1)
				{
					return value[0];
				}
				if (value.Count == 0)
				{
					return default(TConnection);
				}
				throw new global::System.InvalidOperationException();
			}
			return default(TConnection);
		}

		public global::System.Collections.Generic.IEnumerable<TConnection> WithDestination(TDestination destination)
		{
			return WithDestinationNoAlloc(destination);
		}

		public global::System.Collections.Generic.List<TConnection> WithDestinationNoAlloc(TDestination destination)
		{
			global::Unity.VisualScripting.Ensure.That("destination").IsNotNull(destination);
			if (byDestination.TryGetValue(destination, out var value))
			{
				return value;
			}
			return global::Unity.VisualScripting.Empty<TConnection>.list;
		}

		public TConnection SingleOrDefaultWithDestination(TDestination destination)
		{
			global::Unity.VisualScripting.Ensure.That("destination").IsNotNull(destination);
			if (byDestination.TryGetValue(destination, out var value))
			{
				if (value.Count == 1)
				{
					return value[0];
				}
				if (value.Count == 0)
				{
					return default(TConnection);
				}
				throw new global::System.InvalidOperationException();
			}
			return default(TConnection);
		}

		public void Add(TConnection item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			if (item.source == null)
			{
				throw new global::System.ArgumentNullException("item.source");
			}
			if (item.destination == null)
			{
				throw new global::System.ArgumentNullException("item.destination");
			}
			BeforeAdd(item);
			collection.Add(item);
			AddToDictionaries(item);
			AfterAdd(item);
		}

		public void Clear()
		{
			collection.Clear();
			bySource.Clear();
			byDestination.Clear();
		}

		public bool Contains(TConnection item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			return collection.Contains(item);
		}

		public void CopyTo(TConnection[] array, int arrayIndex)
		{
			collection.CopyTo(array, arrayIndex);
		}

		public bool Remove(TConnection item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			if (item.source == null)
			{
				throw new global::System.ArgumentNullException("item.source");
			}
			if (item.destination == null)
			{
				throw new global::System.ArgumentNullException("item.destination");
			}
			if (!collection.Contains(item))
			{
				return false;
			}
			BeforeRemove(item);
			collection.Remove(item);
			RemoveFromDictionaries(item);
			AfterRemove(item);
			return true;
		}

		protected virtual void BeforeAdd(TConnection item)
		{
		}

		protected virtual void AfterAdd(TConnection item)
		{
		}

		protected virtual void BeforeRemove(TConnection item)
		{
		}

		protected virtual void AfterRemove(TConnection item)
		{
		}

		private void AddToDictionaries(TConnection item)
		{
			if (!bySource.ContainsKey(item.source))
			{
				bySource.Add(item.source, new global::System.Collections.Generic.List<TConnection>());
			}
			bySource[item.source].Add(item);
			if (!byDestination.ContainsKey(item.destination))
			{
				byDestination.Add(item.destination, new global::System.Collections.Generic.List<TConnection>());
			}
			byDestination[item.destination].Add(item);
		}

		private void RemoveFromDictionaries(TConnection item)
		{
			bySource[item.source].Remove(item);
			byDestination[item.destination].Remove(item);
		}
	}
}
