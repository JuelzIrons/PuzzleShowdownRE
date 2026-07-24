namespace Newtonsoft.Json.Utilities
{
	internal class DictionaryWrapper<TKey, TValue> : global::System.Collections.Generic.IDictionary<TKey, TValue>, global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>, global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>, global::System.Collections.IEnumerable, global::Newtonsoft.Json.Utilities.IWrappedDictionary, global::System.Collections.IDictionary, global::System.Collections.ICollection
	{
		private readonly struct DictionaryEnumerator<TEnumeratorKey, TEnumeratorValue> : global::System.Collections.IDictionaryEnumerator, global::System.Collections.IEnumerator
		{
			private readonly global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;

			public global::System.Collections.DictionaryEntry Entry => (global::System.Collections.DictionaryEntry)Current;

			public object Key => Entry.Key;

			public object? Value => Entry.Value;

			public object Current => new global::System.Collections.DictionaryEntry(_e.Current.Key, _e.Current.Value);

			public DictionaryEnumerator(global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(e, "e");
				_e = e;
			}

			public bool MoveNext()
			{
				return _e.MoveNext();
			}

			public void Reset()
			{
				_e.Reset();
			}
		}

		private readonly global::System.Collections.IDictionary? _dictionary;

		private readonly global::System.Collections.Generic.IDictionary<TKey, TValue>? _genericDictionary;

		private readonly global::System.Collections.Generic.IReadOnlyDictionary<TKey, TValue>? _readOnlyDictionary;

		private object? _syncRoot;

		internal global::System.Collections.Generic.IDictionary<TKey, TValue> GenericDictionary => _genericDictionary;

		public global::System.Collections.Generic.ICollection<TKey> Keys
		{
			get
			{
				if (_dictionary != null)
				{
					return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Cast<TKey>(_dictionary.Keys));
				}
				if (_readOnlyDictionary != null)
				{
					return global::System.Linq.Enumerable.ToList(_readOnlyDictionary.Keys);
				}
				return GenericDictionary.Keys;
			}
		}

		public global::System.Collections.Generic.ICollection<TValue> Values
		{
			get
			{
				if (_dictionary != null)
				{
					return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Cast<TValue>(_dictionary.Values));
				}
				if (_readOnlyDictionary != null)
				{
					return global::System.Linq.Enumerable.ToList(_readOnlyDictionary.Values);
				}
				return GenericDictionary.Values;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				if (_dictionary != null)
				{
					return (TValue)_dictionary[key];
				}
				if (_readOnlyDictionary != null)
				{
					return _readOnlyDictionary[key];
				}
				return GenericDictionary[key];
			}
			set
			{
				if (_dictionary != null)
				{
					_dictionary[key] = value;
					return;
				}
				if (_readOnlyDictionary != null)
				{
					throw new global::System.NotSupportedException();
				}
				GenericDictionary[key] = value;
			}
		}

		public int Count
		{
			get
			{
				if (_dictionary != null)
				{
					return _dictionary.Count;
				}
				if (_readOnlyDictionary != null)
				{
					return _readOnlyDictionary.Count;
				}
				return GenericDictionary.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				if (_dictionary != null)
				{
					return _dictionary.IsReadOnly;
				}
				if (_readOnlyDictionary != null)
				{
					return true;
				}
				return GenericDictionary.IsReadOnly;
			}
		}

		object? global::System.Collections.IDictionary.this[object key]
		{
			get
			{
				if (_dictionary != null)
				{
					return _dictionary[key];
				}
				if (_readOnlyDictionary != null)
				{
					return _readOnlyDictionary[(TKey)key];
				}
				return GenericDictionary[(TKey)key];
			}
			set
			{
				if (_dictionary != null)
				{
					_dictionary[key] = value;
					return;
				}
				if (_readOnlyDictionary != null)
				{
					throw new global::System.NotSupportedException();
				}
				GenericDictionary[(TKey)key] = (TValue)value;
			}
		}

		bool global::System.Collections.IDictionary.IsFixedSize
		{
			get
			{
				if (_genericDictionary != null)
				{
					return false;
				}
				if (_readOnlyDictionary != null)
				{
					return true;
				}
				return _dictionary.IsFixedSize;
			}
		}

		global::System.Collections.ICollection global::System.Collections.IDictionary.Keys
		{
			get
			{
				if (_genericDictionary != null)
				{
					return global::System.Linq.Enumerable.ToList(_genericDictionary.Keys);
				}
				if (_readOnlyDictionary != null)
				{
					return global::System.Linq.Enumerable.ToList(_readOnlyDictionary.Keys);
				}
				return _dictionary.Keys;
			}
		}

		global::System.Collections.ICollection global::System.Collections.IDictionary.Values
		{
			get
			{
				if (_genericDictionary != null)
				{
					return global::System.Linq.Enumerable.ToList(_genericDictionary.Values);
				}
				if (_readOnlyDictionary != null)
				{
					return global::System.Linq.Enumerable.ToList(_readOnlyDictionary.Values);
				}
				return _dictionary.Values;
			}
		}

		bool global::System.Collections.ICollection.IsSynchronized
		{
			get
			{
				if (_dictionary != null)
				{
					return _dictionary.IsSynchronized;
				}
				return false;
			}
		}

		object global::System.Collections.ICollection.SyncRoot
		{
			get
			{
				if (_syncRoot == null)
				{
					global::System.Threading.Interlocked.CompareExchange(ref _syncRoot, new object(), null);
				}
				return _syncRoot;
			}
		}

		public object UnderlyingDictionary
		{
			get
			{
				if (_dictionary != null)
				{
					return _dictionary;
				}
				if (_readOnlyDictionary != null)
				{
					return _readOnlyDictionary;
				}
				return GenericDictionary;
			}
		}

		public DictionaryWrapper(global::System.Collections.IDictionary dictionary)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			_dictionary = dictionary;
		}

		public DictionaryWrapper(global::System.Collections.Generic.IDictionary<TKey, TValue> dictionary)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			_genericDictionary = dictionary;
		}

		public DictionaryWrapper(global::System.Collections.Generic.IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			_readOnlyDictionary = dictionary;
		}

		public void Add(TKey key, TValue value)
		{
			if (_dictionary != null)
			{
				_dictionary.Add(key, value);
				return;
			}
			if (_genericDictionary != null)
			{
				_genericDictionary.Add(key, value);
				return;
			}
			throw new global::System.NotSupportedException();
		}

		public bool ContainsKey(TKey key)
		{
			if (_dictionary != null)
			{
				return _dictionary.Contains(key);
			}
			if (_readOnlyDictionary != null)
			{
				return _readOnlyDictionary.ContainsKey(key);
			}
			return GenericDictionary.ContainsKey(key);
		}

		public bool Remove(TKey key)
		{
			if (_dictionary != null)
			{
				if (_dictionary.Contains(key))
				{
					_dictionary.Remove(key);
					return true;
				}
				return false;
			}
			if (_readOnlyDictionary != null)
			{
				throw new global::System.NotSupportedException();
			}
			return GenericDictionary.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue? value)
		{
			if (_dictionary != null)
			{
				if (!_dictionary.Contains(key))
				{
					value = default(TValue);
					return false;
				}
				value = (TValue)_dictionary[key];
				return true;
			}
			if (_readOnlyDictionary != null)
			{
				throw new global::System.NotSupportedException();
			}
			return GenericDictionary.TryGetValue(key, out value);
		}

		public void Add(global::System.Collections.Generic.KeyValuePair<TKey, TValue> item)
		{
			if (_dictionary != null)
			{
				((global::System.Collections.IList)_dictionary).Add(item);
				return;
			}
			if (_readOnlyDictionary != null)
			{
				throw new global::System.NotSupportedException();
			}
			_genericDictionary?.Add(item);
		}

		public void Clear()
		{
			if (_dictionary != null)
			{
				_dictionary.Clear();
				return;
			}
			if (_readOnlyDictionary != null)
			{
				throw new global::System.NotSupportedException();
			}
			GenericDictionary.Clear();
		}

		public bool Contains(global::System.Collections.Generic.KeyValuePair<TKey, TValue> item)
		{
			if (_dictionary != null)
			{
				return ((global::System.Collections.IList)_dictionary).Contains(item);
			}
			if (_readOnlyDictionary != null)
			{
				return global::System.Linq.Enumerable.Contains(_readOnlyDictionary, item);
			}
			return GenericDictionary.Contains(item);
		}

		public void CopyTo(global::System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (_dictionary != null)
			{
				foreach (global::System.Collections.DictionaryEntry item in _dictionary)
				{
					array[arrayIndex++] = new global::System.Collections.Generic.KeyValuePair<TKey, TValue>((TKey)item.Key, (TValue)item.Value);
				}
				return;
			}
			if (_readOnlyDictionary != null)
			{
				throw new global::System.NotSupportedException();
			}
			GenericDictionary.CopyTo(array, arrayIndex);
		}

		public bool Remove(global::System.Collections.Generic.KeyValuePair<TKey, TValue> item)
		{
			if (_dictionary != null)
			{
				if (_dictionary.Contains(item.Key))
				{
					if (object.Equals(_dictionary[item.Key], item.Value))
					{
						_dictionary.Remove(item.Key);
						return true;
					}
					return false;
				}
				return true;
			}
			if (_readOnlyDictionary != null)
			{
				throw new global::System.NotSupportedException();
			}
			return GenericDictionary.Remove(item);
		}

		public global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			if (_dictionary != null)
			{
				return global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Cast<global::System.Collections.DictionaryEntry>(_dictionary), (global::System.Collections.DictionaryEntry de) => new global::System.Collections.Generic.KeyValuePair<TKey, TValue>((TKey)de.Key, (TValue)de.Value)).GetEnumerator();
			}
			if (_readOnlyDictionary != null)
			{
				return _readOnlyDictionary.GetEnumerator();
			}
			return GenericDictionary.GetEnumerator();
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		void global::System.Collections.IDictionary.Add(object key, object? value)
		{
			if (_dictionary != null)
			{
				_dictionary.Add(key, value);
				return;
			}
			if (_readOnlyDictionary != null)
			{
				throw new global::System.NotSupportedException();
			}
			GenericDictionary.Add((TKey)key, (TValue)value);
		}

		global::System.Collections.IDictionaryEnumerator global::System.Collections.IDictionary.GetEnumerator()
		{
			if (_dictionary != null)
			{
				return _dictionary.GetEnumerator();
			}
			if (_readOnlyDictionary != null)
			{
				return new global::Newtonsoft.Json.Utilities.DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(_readOnlyDictionary.GetEnumerator());
			}
			return new global::Newtonsoft.Json.Utilities.DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(GenericDictionary.GetEnumerator());
		}

		bool global::System.Collections.IDictionary.Contains(object key)
		{
			if (_genericDictionary != null)
			{
				return _genericDictionary.ContainsKey((TKey)key);
			}
			if (_readOnlyDictionary != null)
			{
				return _readOnlyDictionary.ContainsKey((TKey)key);
			}
			return _dictionary.Contains(key);
		}

		public void Remove(object key)
		{
			if (_dictionary != null)
			{
				_dictionary.Remove(key);
				return;
			}
			if (_readOnlyDictionary != null)
			{
				throw new global::System.NotSupportedException();
			}
			GenericDictionary.Remove((TKey)key);
		}

		void global::System.Collections.ICollection.CopyTo(global::System.Array array, int index)
		{
			if (_dictionary != null)
			{
				_dictionary.CopyTo(array, index);
				return;
			}
			if (_readOnlyDictionary != null)
			{
				throw new global::System.NotSupportedException();
			}
			GenericDictionary.CopyTo((global::System.Collections.Generic.KeyValuePair<TKey, TValue>[])array, index);
		}
	}
}
