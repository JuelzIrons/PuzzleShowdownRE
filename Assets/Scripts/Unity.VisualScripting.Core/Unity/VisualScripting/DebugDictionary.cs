namespace Unity.VisualScripting
{
	public class DebugDictionary<TKey, TValue> : global::System.Collections.Generic.IDictionary<TKey, TValue>, global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>, global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>, global::System.Collections.IEnumerable, global::System.Collections.IDictionary, global::System.Collections.ICollection
	{
		private readonly global::System.Collections.Generic.Dictionary<TKey, TValue> dictionary = new global::System.Collections.Generic.Dictionary<TKey, TValue>();

		public TValue this[TKey key]
		{
			get
			{
				return dictionary[key];
			}
			set
			{
				Debug($"Set: {key} => {value}");
				dictionary[key] = value;
			}
		}

		object global::System.Collections.IDictionary.this[object key]
		{
			get
			{
				return this[(TKey)key];
			}
			set
			{
				this[(TKey)key] = (TValue)value;
			}
		}

		public string label { get; set; } = "Dictionary";

		public bool debug { get; set; }

		public int Count => dictionary.Count;

		object global::System.Collections.ICollection.SyncRoot => ((global::System.Collections.ICollection)dictionary).SyncRoot;

		bool global::System.Collections.ICollection.IsSynchronized => ((global::System.Collections.ICollection)dictionary).IsSynchronized;

		global::System.Collections.ICollection global::System.Collections.IDictionary.Values => ((global::System.Collections.IDictionary)dictionary).Values;

		bool global::System.Collections.IDictionary.IsReadOnly => ((global::System.Collections.IDictionary)dictionary).IsReadOnly;

		bool global::System.Collections.IDictionary.IsFixedSize => ((global::System.Collections.IDictionary)dictionary).IsFixedSize;

		bool global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>.IsReadOnly => ((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>)dictionary).IsReadOnly;

		public global::System.Collections.Generic.ICollection<TKey> Keys => dictionary.Keys;

		global::System.Collections.ICollection global::System.Collections.IDictionary.Keys => ((global::System.Collections.IDictionary)dictionary).Keys;

		public global::System.Collections.Generic.ICollection<TValue> Values => dictionary.Values;

		void global::System.Collections.ICollection.CopyTo(global::System.Array array, int index)
		{
			((global::System.Collections.ICollection)dictionary).CopyTo(array, index);
		}

		private void Debug(string message)
		{
			if (debug)
			{
				if (!string.IsNullOrEmpty(label))
				{
					message = "[" + label + "] " + message;
				}
				global::UnityEngine.Debug.Log(message + "\n");
			}
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return ((global::System.Collections.IEnumerable)dictionary).GetEnumerator();
		}

		void global::System.Collections.IDictionary.Remove(object key)
		{
			Remove((TKey)key);
		}

		bool global::System.Collections.IDictionary.Contains(object key)
		{
			return ContainsKey((TKey)key);
		}

		void global::System.Collections.IDictionary.Add(object key, object value)
		{
			Add((TKey)key, (TValue)value);
		}

		public void Clear()
		{
			Debug("Clear");
			dictionary.Clear();
		}

		global::System.Collections.IDictionaryEnumerator global::System.Collections.IDictionary.GetEnumerator()
		{
			return ((global::System.Collections.IDictionary)dictionary).GetEnumerator();
		}

		public bool Contains(global::System.Collections.Generic.KeyValuePair<TKey, TValue> item)
		{
			return global::System.Linq.Enumerable.Contains(dictionary, item);
		}

		void global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>.Add(global::System.Collections.Generic.KeyValuePair<TKey, TValue> item)
		{
			((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>)dictionary).Add(item);
		}

		void global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>.CopyTo(global::System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>)dictionary).CopyTo(array, arrayIndex);
		}

		bool global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>.Remove(global::System.Collections.Generic.KeyValuePair<TKey, TValue> item)
		{
			return ((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>)dictionary).Remove(item);
		}

		public global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return dictionary.GetEnumerator();
		}

		public bool ContainsKey(TKey key)
		{
			return dictionary.ContainsKey(key);
		}

		public void Add(TKey key, TValue value)
		{
			Debug($"Add: {key} => {value}");
			dictionary.Add(key, value);
		}

		public bool Remove(TKey key)
		{
			Debug($"Remove: {key}");
			return dictionary.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return dictionary.TryGetValue(key, out value);
		}
	}
}
