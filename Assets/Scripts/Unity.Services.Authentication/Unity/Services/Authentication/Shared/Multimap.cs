namespace Unity.Services.Authentication.Shared
{
	internal class Multimap<TKey, TValue> : global::System.Collections.Generic.IDictionary<TKey, global::System.Collections.Generic.IList<TValue>>, global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, global::System.Collections.Generic.IList<TValue>>>, global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<TKey, global::System.Collections.Generic.IList<TValue>>>, global::System.Collections.IEnumerable
	{
		private readonly global::System.Collections.Generic.Dictionary<TKey, global::System.Collections.Generic.IList<TValue>> _dictionary;

		public global::System.Collections.Generic.IList<TValue> this[TKey key]
		{
			get
			{
				return _dictionary[key];
			}
			set
			{
				_dictionary[key] = value;
			}
		}

		public global::System.Collections.Generic.ICollection<TKey> Keys => _dictionary.Keys;

		public global::System.Collections.Generic.ICollection<global::System.Collections.Generic.IList<TValue>> Values => _dictionary.Values;

		public int Count => _dictionary.Count;

		public bool IsReadOnly => false;

		public Multimap()
		{
			_dictionary = new global::System.Collections.Generic.Dictionary<TKey, global::System.Collections.Generic.IList<TValue>>();
		}

		public Multimap(global::System.Collections.Generic.IEqualityComparer<TKey> comparer)
		{
			_dictionary = new global::System.Collections.Generic.Dictionary<TKey, global::System.Collections.Generic.IList<TValue>>(comparer);
		}

		public global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<TKey, global::System.Collections.Generic.IList<TValue>>> GetEnumerator()
		{
			return _dictionary.GetEnumerator();
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return _dictionary.GetEnumerator();
		}

		public void Add(global::System.Collections.Generic.KeyValuePair<TKey, global::System.Collections.Generic.IList<TValue>> item)
		{
			if (!TryAdd(item.Key, item.Value))
			{
				throw new global::System.InvalidOperationException("Could not add values to Multimap.");
			}
		}

		public void Add(global::Unity.Services.Authentication.Shared.Multimap<TKey, TValue> multimap)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<TKey, global::System.Collections.Generic.IList<TValue>> item in multimap)
			{
				if (!TryAdd(item.Key, item.Value))
				{
					throw new global::System.InvalidOperationException("Could not add values to Multimap.");
				}
			}
		}

		public void Clear()
		{
			_dictionary.Clear();
		}

		public bool Contains(global::System.Collections.Generic.KeyValuePair<TKey, global::System.Collections.Generic.IList<TValue>> item)
		{
			throw new global::System.NotImplementedException();
		}

		public void CopyTo(global::System.Collections.Generic.KeyValuePair<TKey, global::System.Collections.Generic.IList<TValue>>[] array, int arrayIndex)
		{
			throw new global::System.NotImplementedException();
		}

		public bool Remove(global::System.Collections.Generic.KeyValuePair<TKey, global::System.Collections.Generic.IList<TValue>> item)
		{
			throw new global::System.NotImplementedException();
		}

		public void Add(TKey key, global::System.Collections.Generic.IList<TValue> value)
		{
			if (value == null || value.Count <= 0)
			{
				return;
			}
			if (_dictionary.TryGetValue(key, out var value2))
			{
				foreach (TValue item in value)
				{
					value2.Add(item);
				}
				return;
			}
			value2 = new global::System.Collections.Generic.List<TValue>(value);
			if (!TryAdd(key, value2))
			{
				throw new global::System.InvalidOperationException("Could not add values to Multimap.");
			}
		}

		public bool ContainsKey(TKey key)
		{
			return _dictionary.ContainsKey(key);
		}

		public bool Remove(TKey key)
		{
			global::System.Collections.Generic.IList<TValue> value;
			return TryRemove(key, out value);
		}

		public bool TryGetValue(TKey key, out global::System.Collections.Generic.IList<TValue> value)
		{
			return _dictionary.TryGetValue(key, out value);
		}

		public void CopyTo(global::System.Array array, int index)
		{
			((global::System.Collections.ICollection)_dictionary).CopyTo(array, index);
		}

		public void Add(TKey key, TValue value)
		{
			if (value == null)
			{
				return;
			}
			if (_dictionary.TryGetValue(key, out var value2))
			{
				value2.Add(value);
				return;
			}
			value2 = new global::System.Collections.Generic.List<TValue> { value };
			if (TryAdd(key, value2))
			{
				return;
			}
			throw new global::System.InvalidOperationException("Could not add value to Multimap.");
		}

		private bool TryRemove(TKey key, out global::System.Collections.Generic.IList<TValue> value)
		{
			_dictionary.TryGetValue(key, out value);
			return _dictionary.Remove(key);
		}

		private bool TryAdd(TKey key, global::System.Collections.Generic.IList<TValue> value)
		{
			try
			{
				_dictionary.Add(key, value);
			}
			catch (global::System.ArgumentException)
			{
				return false;
			}
			return true;
		}
	}
}
