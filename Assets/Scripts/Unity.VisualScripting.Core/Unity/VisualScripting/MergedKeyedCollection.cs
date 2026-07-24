namespace Unity.VisualScripting
{
	public class MergedKeyedCollection<TKey, TItem> : global::Unity.VisualScripting.IMergedCollection<TItem>, global::System.Collections.Generic.ICollection<TItem>, global::System.Collections.Generic.IEnumerable<TItem>, global::System.Collections.IEnumerable
	{
		public struct Enumerator : global::System.Collections.Generic.IEnumerator<TItem>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.IKeyedCollection<TKey, TItem>>.Enumerator collectionsEnumerator;

			private TItem currentItem;

			private global::Unity.VisualScripting.IKeyedCollection<TKey, TItem> currentCollection;

			private int indexInCurrentCollection;

			private bool exceeded;

			public TItem Current => currentItem;

			object global::System.Collections.IEnumerator.Current
			{
				get
				{
					if (exceeded)
					{
						throw new global::System.InvalidOperationException();
					}
					return Current;
				}
			}

			public Enumerator(global::Unity.VisualScripting.MergedKeyedCollection<TKey, TItem> merged)
			{
				this = default(global::Unity.VisualScripting.MergedKeyedCollection<TKey, TItem>.Enumerator);
				collectionsEnumerator = merged.collections.GetEnumerator();
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				if (currentCollection == null)
				{
					if (!collectionsEnumerator.MoveNext())
					{
						currentItem = default(TItem);
						exceeded = true;
						return false;
					}
					currentCollection = collectionsEnumerator.Current.Value;
					if (currentCollection == null)
					{
						throw new global::System.InvalidOperationException("Merged sub collection is null.");
					}
				}
				if (indexInCurrentCollection < currentCollection.Count)
				{
					currentItem = currentCollection[indexInCurrentCollection];
					indexInCurrentCollection++;
					return true;
				}
				while (collectionsEnumerator.MoveNext())
				{
					currentCollection = collectionsEnumerator.Current.Value;
					indexInCurrentCollection = 0;
					if (currentCollection == null)
					{
						throw new global::System.InvalidOperationException("Merged sub collection is null.");
					}
					if (indexInCurrentCollection < currentCollection.Count)
					{
						currentItem = currentCollection[indexInCurrentCollection];
						indexInCurrentCollection++;
						return true;
					}
				}
				currentItem = default(TItem);
				exceeded = true;
				return false;
			}

			void global::System.Collections.IEnumerator.Reset()
			{
				throw new global::System.InvalidOperationException();
			}
		}

		protected readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.IKeyedCollection<TKey, TItem>> collections;

		protected readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.IKeyedCollection<TKey, TItem>> collectionsLookup;

		public TItem this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new global::System.ArgumentNullException("key");
				}
				foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::Unity.VisualScripting.IKeyedCollection<TKey, TItem>> collection in collections)
				{
					if (collection.Value.Contains(key))
					{
						return collection.Value[key];
					}
				}
				throw new global::System.Collections.Generic.KeyNotFoundException();
			}
		}

		public int Count
		{
			get
			{
				int num = 0;
				foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::Unity.VisualScripting.IKeyedCollection<TKey, TItem>> collection in collections)
				{
					num += collection.Value.Count;
				}
				return num;
			}
		}

		public bool IsReadOnly => false;

		public MergedKeyedCollection()
		{
			collections = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.IKeyedCollection<TKey, TItem>>();
			collectionsLookup = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.IKeyedCollection<TKey, TItem>>();
		}

		public bool Includes<TSubItem>() where TSubItem : TItem
		{
			return Includes(typeof(TSubItem));
		}

		public bool Includes(global::System.Type elementType)
		{
			return GetCollectionForType(elementType, throwOnFail: false) != null;
		}

		public global::Unity.VisualScripting.IKeyedCollection<TKey, TSubItem> ForType<TSubItem>() where TSubItem : TItem
		{
			return ((global::Unity.VisualScripting.VariantKeyedCollection<TItem, TSubItem, TKey>)GetCollectionForType(typeof(TSubItem))).implementation;
		}

		public virtual void Include<TSubItem>(global::Unity.VisualScripting.IKeyedCollection<TKey, TSubItem> collection) where TSubItem : TItem
		{
			global::System.Type typeFromHandle = typeof(TSubItem);
			global::Unity.VisualScripting.VariantKeyedCollection<TItem, TSubItem, TKey> value = new global::Unity.VisualScripting.VariantKeyedCollection<TItem, TSubItem, TKey>(collection);
			collections.Add(typeFromHandle, value);
			collectionsLookup.Add(typeFromHandle, value);
		}

		protected global::Unity.VisualScripting.IKeyedCollection<TKey, TItem> GetCollectionForItem(TItem item)
		{
			global::Unity.VisualScripting.Ensure.That("item").IsNotNull(item);
			return GetCollectionForType(item.GetType());
		}

		protected global::Unity.VisualScripting.IKeyedCollection<TKey, TItem> GetCollectionForType(global::System.Type type, bool throwOnFail = true)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			if (collectionsLookup.TryGetValue(type, out var value))
			{
				return value;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::Unity.VisualScripting.IKeyedCollection<TKey, TItem>> collection in collections)
			{
				if (collection.Key.IsAssignableFrom(type))
				{
					value = collection.Value;
					collectionsLookup.Add(type, value);
					return value;
				}
			}
			if (throwOnFail)
			{
				throw new global::System.InvalidOperationException($"No sub-collection available for type '{type}'.");
			}
			return null;
		}

		protected global::Unity.VisualScripting.IKeyedCollection<TKey, TItem> GetCollectionForKey(TKey key, bool throwOnFail = true)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::Unity.VisualScripting.IKeyedCollection<TKey, TItem>> collection in collections)
			{
				if (collection.Value.Contains(key))
				{
					return collection.Value;
				}
			}
			if (throwOnFail)
			{
				throw new global::System.InvalidOperationException($"No sub-collection available for key '{key}'.");
			}
			return null;
		}

		public bool TryGetValue(TKey key, out TItem value)
		{
			global::Unity.VisualScripting.IKeyedCollection<TKey, TItem> collectionForKey = GetCollectionForKey(key, throwOnFail: false);
			value = default(TItem);
			return collectionForKey?.TryGetValue(key, out value) ?? false;
		}

		public virtual void Add(TItem item)
		{
			GetCollectionForItem(item).Add(item);
		}

		public void Clear()
		{
			foreach (global::Unity.VisualScripting.IKeyedCollection<TKey, TItem> value in collections.Values)
			{
				value.Clear();
			}
		}

		public bool Contains(TItem item)
		{
			return GetCollectionForItem(item).Contains(item);
		}

		public bool Remove(TItem item)
		{
			return GetCollectionForItem(item).Remove(item);
		}

		public void CopyTo(TItem[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new global::System.ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length - arrayIndex < Count)
			{
				throw new global::System.ArgumentException();
			}
			int num = 0;
			foreach (global::Unity.VisualScripting.IKeyedCollection<TKey, TItem> value in collections.Values)
			{
				value.CopyTo(array, arrayIndex + num);
				num += value.Count;
			}
		}

		public bool Contains(TKey key)
		{
			return GetCollectionForKey(key, throwOnFail: false) != null;
		}

		public bool Remove(TKey key)
		{
			return GetCollectionForKey(key).Remove(key);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		global::System.Collections.Generic.IEnumerator<TItem> global::System.Collections.Generic.IEnumerable<TItem>.GetEnumerator()
		{
			return GetEnumerator();
		}

		public global::Unity.VisualScripting.MergedKeyedCollection<TKey, TItem>.Enumerator GetEnumerator()
		{
			return new global::Unity.VisualScripting.MergedKeyedCollection<TKey, TItem>.Enumerator(this);
		}
	}
}
