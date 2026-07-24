namespace Unity.VisualScripting
{
	public class NonNullableDictionary<TKey, TValue> : global::System.Collections.Generic.IDictionary<TKey, TValue>, global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>, global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>, global::System.Collections.IEnumerable, global::System.Collections.IDictionary, global::System.Collections.ICollection
	{
		private readonly global::System.Collections.Generic.Dictionary<TKey, TValue> dictionary;

		public TValue this[TKey key]
		{
			get
			{
				return dictionary[key];
			}
			set
			{
				dictionary[key] = value;
			}
		}

		object global::System.Collections.IDictionary.this[object key]
		{
			get
			{
				return ((global::System.Collections.IDictionary)dictionary)[key];
			}
			set
			{
				((global::System.Collections.IDictionary)dictionary)[key] = value;
			}
		}

		public int Count => dictionary.Count;

		public bool IsSynchronized => ((global::System.Collections.ICollection)dictionary).IsSynchronized;

		public object SyncRoot => ((global::System.Collections.ICollection)dictionary).SyncRoot;

		public bool IsReadOnly => false;

		public global::System.Collections.Generic.ICollection<TKey> Keys => dictionary.Keys;

		global::System.Collections.ICollection global::System.Collections.IDictionary.Values => ((global::System.Collections.IDictionary)dictionary).Values;

		global::System.Collections.ICollection global::System.Collections.IDictionary.Keys => ((global::System.Collections.IDictionary)dictionary).Keys;

		public global::System.Collections.Generic.ICollection<TValue> Values => dictionary.Values;

		public bool IsFixedSize => ((global::System.Collections.IDictionary)dictionary).IsFixedSize;

		public NonNullableDictionary()
		{
			dictionary = new global::System.Collections.Generic.Dictionary<TKey, TValue>();
		}

		public NonNullableDictionary(int capacity)
		{
			dictionary = new global::System.Collections.Generic.Dictionary<TKey, TValue>(capacity);
		}

		public NonNullableDictionary(global::System.Collections.Generic.IEqualityComparer<TKey> comparer)
		{
			dictionary = new global::System.Collections.Generic.Dictionary<TKey, TValue>(comparer);
		}

		public NonNullableDictionary(global::System.Collections.Generic.IDictionary<TKey, TValue> dictionary)
		{
			this.dictionary = new global::System.Collections.Generic.Dictionary<TKey, TValue>(dictionary);
		}

		public NonNullableDictionary(int capacity, global::System.Collections.Generic.IEqualityComparer<TKey> comparer)
		{
			dictionary = new global::System.Collections.Generic.Dictionary<TKey, TValue>(capacity, comparer);
		}

		public NonNullableDictionary(global::System.Collections.Generic.IDictionary<TKey, TValue> dictionary, global::System.Collections.Generic.IEqualityComparer<TKey> comparer)
		{
			this.dictionary = new global::System.Collections.Generic.Dictionary<TKey, TValue>(dictionary, comparer);
		}

		public void CopyTo(global::System.Array array, int index)
		{
			((global::System.Collections.ICollection)dictionary).CopyTo(array, index);
		}

		void global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>.Add(global::System.Collections.Generic.KeyValuePair<TKey, TValue> item)
		{
			((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>)dictionary).Add(item);
		}

		public void Add(TKey key, TValue value)
		{
			if (value == null)
			{
				throw new global::System.ArgumentNullException("value");
			}
			dictionary.Add(key, value);
		}

		public void Add(object key, object value)
		{
			((global::System.Collections.IDictionary)dictionary).Add(key, value);
		}

		public void Clear()
		{
			dictionary.Clear();
		}

		public bool Contains(object key)
		{
			return ((global::System.Collections.IDictionary)dictionary).Contains(key);
		}

		global::System.Collections.IDictionaryEnumerator global::System.Collections.IDictionary.GetEnumerator()
		{
			return ((global::System.Collections.IDictionary)dictionary).GetEnumerator();
		}

		public void Remove(object key)
		{
			((global::System.Collections.IDictionary)dictionary).Remove(key);
		}

		bool global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>.Contains(global::System.Collections.Generic.KeyValuePair<TKey, TValue> item)
		{
			return ((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>)dictionary).Contains(item);
		}

		public bool ContainsKey(TKey key)
		{
			return dictionary.ContainsKey(key);
		}

		void global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>.CopyTo(global::System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>)dictionary).CopyTo(array, arrayIndex);
		}

		public global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return dictionary.GetEnumerator();
		}

		bool global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>.Remove(global::System.Collections.Generic.KeyValuePair<TKey, TValue> item)
		{
			return ((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>)dictionary).Remove(item);
		}

		public bool Remove(TKey key)
		{
			return dictionary.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return dictionary.TryGetValue(key, out value);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return dictionary.GetEnumerator();
		}
	}
}
