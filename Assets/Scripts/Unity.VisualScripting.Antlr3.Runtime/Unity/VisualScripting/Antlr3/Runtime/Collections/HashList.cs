namespace Unity.VisualScripting.Antlr3.Runtime.Collections
{
	public sealed class HashList : global::System.Collections.IDictionary, global::System.Collections.ICollection, global::System.Collections.IEnumerable
	{
		private sealed class HashListEnumerator : global::System.Collections.IDictionaryEnumerator, global::System.Collections.IEnumerator
		{
			internal enum EnumerationMode
			{
				Key = 0,
				Value = 1,
				Entry = 2
			}

			private global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList _hashList;

			private global::System.Collections.Generic.List<object> _orderList;

			private global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator.EnumerationMode _mode;

			private int _index;

			private int _version;

			private object _key;

			private object _value;

			public object Key
			{
				get
				{
					if (_key == null)
					{
						throw new global::System.InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return _key;
				}
			}

			public object Value
			{
				get
				{
					if (_key == null)
					{
						throw new global::System.InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return _value;
				}
			}

			public global::System.Collections.DictionaryEntry Entry
			{
				get
				{
					if (_key == null)
					{
						throw new global::System.InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new global::System.Collections.DictionaryEntry(_key, _value);
				}
			}

			public object Current
			{
				get
				{
					if (_key == null)
					{
						throw new global::System.InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					if (_mode == global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator.EnumerationMode.Key)
					{
						return _key;
					}
					if (_mode == global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator.EnumerationMode.Value)
					{
						return _value;
					}
					return new global::System.Collections.DictionaryEntry(_key, _value);
				}
			}

			internal HashListEnumerator()
			{
				_index = 0;
				_key = null;
				_value = null;
			}

			internal HashListEnumerator(global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList hashList, global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator.EnumerationMode mode)
			{
				_hashList = hashList;
				_mode = mode;
				_version = hashList._version;
				_orderList = hashList._insertionOrderList;
				_index = 0;
				_key = null;
				_value = null;
			}

			public void Reset()
			{
				if (_version != _hashList._version)
				{
					throw new global::System.InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				_index = 0;
				_key = null;
				_value = null;
			}

			public bool MoveNext()
			{
				if (_version != _hashList._version)
				{
					throw new global::System.InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (_index < _orderList.Count)
				{
					_key = _orderList[_index];
					_value = _hashList[_key];
					_index++;
					return true;
				}
				_key = null;
				return false;
			}
		}

		private sealed class KeyCollection : global::System.Collections.ICollection, global::System.Collections.IEnumerable
		{
			private global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList _hashList;

			public bool IsSynchronized => _hashList.IsSynchronized;

			public int Count => _hashList.Count;

			public object SyncRoot => _hashList.SyncRoot;

			internal KeyCollection()
			{
			}

			internal KeyCollection(global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList hashList)
			{
				_hashList = hashList;
			}

			public override string ToString()
			{
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
				stringBuilder.Append("[");
				global::System.Collections.Generic.List<object> insertionOrderList = _hashList._insertionOrderList;
				for (int i = 0; i < insertionOrderList.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(insertionOrderList[i]);
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}

			public override bool Equals(object o)
			{
				if (o is global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.KeyCollection)
				{
					global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.KeyCollection keyCollection = (global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.KeyCollection)o;
					if (Count == 0 && keyCollection.Count == 0)
					{
						return true;
					}
					if (Count == keyCollection.Count)
					{
						for (int i = 0; i < Count; i++)
						{
							if (_hashList._insertionOrderList[i] == keyCollection._hashList._insertionOrderList[i] || _hashList._insertionOrderList[i].Equals(keyCollection._hashList._insertionOrderList[i]))
							{
								return true;
							}
						}
					}
				}
				return false;
			}

			public override int GetHashCode()
			{
				return _hashList._insertionOrderList.GetHashCode();
			}

			public void CopyTo(global::System.Array array, int index)
			{
				_hashList.CopyKeysTo(array, index);
			}

			public global::System.Collections.IEnumerator GetEnumerator()
			{
				return new global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator(_hashList, global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator.EnumerationMode.Key);
			}
		}

		private sealed class ValueCollection : global::System.Collections.ICollection, global::System.Collections.IEnumerable
		{
			private global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList _hashList;

			public bool IsSynchronized => _hashList.IsSynchronized;

			public int Count => _hashList.Count;

			public object SyncRoot => _hashList.SyncRoot;

			internal ValueCollection()
			{
			}

			internal ValueCollection(global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList hashList)
			{
				_hashList = hashList;
			}

			public override string ToString()
			{
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
				stringBuilder.Append("[");
				global::System.Collections.IEnumerator enumerator = new global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator(_hashList, global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator.EnumerationMode.Value);
				if (enumerator.MoveNext())
				{
					stringBuilder.Append((enumerator.Current == null) ? "null" : enumerator.Current);
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(", ");
						stringBuilder.Append((enumerator.Current == null) ? "null" : enumerator.Current);
					}
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}

			public void CopyTo(global::System.Array array, int index)
			{
				_hashList.CopyValuesTo(array, index);
			}

			public global::System.Collections.IEnumerator GetEnumerator()
			{
				return new global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator(_hashList, global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator.EnumerationMode.Value);
			}
		}

		private global::System.Collections.Hashtable _dictionary = new global::System.Collections.Hashtable();

		private global::System.Collections.Generic.List<object> _insertionOrderList = new global::System.Collections.Generic.List<object>();

		private int _version;

		public bool IsReadOnly => _dictionary.IsReadOnly;

		public object this[object key]
		{
			get
			{
				return _dictionary[key];
			}
			set
			{
				bool flag = !_dictionary.Contains(key);
				_dictionary[key] = value;
				if (flag)
				{
					_insertionOrderList.Add(key);
				}
				_version++;
			}
		}

		public global::System.Collections.ICollection Values => new global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.ValueCollection(this);

		public global::System.Collections.ICollection Keys => new global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.KeyCollection(this);

		public bool IsFixedSize => _dictionary.IsFixedSize;

		public bool IsSynchronized => _dictionary.IsSynchronized;

		public int Count => _dictionary.Count;

		public object SyncRoot => _dictionary.SyncRoot;

		public HashList()
			: this(-1)
		{
		}

		public HashList(int capacity)
		{
			if (capacity < 0)
			{
				_dictionary = new global::System.Collections.Hashtable();
				_insertionOrderList = new global::System.Collections.Generic.List<object>();
			}
			else
			{
				_dictionary = new global::System.Collections.Hashtable(capacity);
				_insertionOrderList = new global::System.Collections.Generic.List<object>(capacity);
			}
			_version = 0;
		}

		public global::System.Collections.IDictionaryEnumerator GetEnumerator()
		{
			return new global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator(this, global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator.EnumerationMode.Entry);
		}

		public void Remove(object key)
		{
			_dictionary.Remove(key);
			_insertionOrderList.Remove(key);
			_version++;
		}

		public bool Contains(object key)
		{
			return _dictionary.Contains(key);
		}

		public void Clear()
		{
			_dictionary.Clear();
			_insertionOrderList.Clear();
			_version++;
		}

		public void Add(object key, object value)
		{
			_dictionary.Add(key, value);
			_insertionOrderList.Add(key);
			_version++;
		}

		public void CopyTo(global::System.Array array, int index)
		{
			int count = _insertionOrderList.Count;
			for (int i = 0; i < count; i++)
			{
				global::System.Collections.DictionaryEntry dictionaryEntry = new global::System.Collections.DictionaryEntry(_insertionOrderList[i], _dictionary[_insertionOrderList[i]]);
				array.SetValue(dictionaryEntry, index++);
			}
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return new global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator(this, global::Unity.VisualScripting.Antlr3.Runtime.Collections.HashList.HashListEnumerator.EnumerationMode.Entry);
		}

		private void CopyKeysTo(global::System.Array array, int index)
		{
			int count = _insertionOrderList.Count;
			for (int i = 0; i < count; i++)
			{
				array.SetValue(_insertionOrderList[i], index++);
			}
		}

		private void CopyValuesTo(global::System.Array array, int index)
		{
			int count = _insertionOrderList.Count;
			for (int i = 0; i < count; i++)
			{
				array.SetValue(_dictionary[_insertionOrderList[i]], index++);
			}
		}
	}
}
