namespace Newtonsoft.Json.Linq
{
	internal class JPropertyKeyedCollection : global::System.Collections.ObjectModel.Collection<global::Newtonsoft.Json.Linq.JToken>
	{
		private static readonly global::System.Collections.Generic.IEqualityComparer<string> Comparer = global::System.StringComparer.Ordinal;

		private global::System.Collections.Generic.Dictionary<string, global::Newtonsoft.Json.Linq.JToken>? _dictionary;

		public global::Newtonsoft.Json.Linq.JToken this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new global::System.ArgumentNullException("key");
				}
				if (_dictionary != null)
				{
					return _dictionary[key];
				}
				throw new global::System.Collections.Generic.KeyNotFoundException();
			}
		}

		public global::System.Collections.Generic.ICollection<string> Keys
		{
			get
			{
				EnsureDictionary();
				return _dictionary.Keys;
			}
		}

		public global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken> Values
		{
			get
			{
				EnsureDictionary();
				return _dictionary.Values;
			}
		}

		public JPropertyKeyedCollection()
			: base((global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken>)new global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JToken>())
		{
		}

		private void AddKey(string key, global::Newtonsoft.Json.Linq.JToken item)
		{
			EnsureDictionary();
			_dictionary[key] = item;
		}

		protected void ChangeItemKey(global::Newtonsoft.Json.Linq.JToken item, string newKey)
		{
			if (!ContainsItem(item))
			{
				throw new global::System.ArgumentException("The specified item does not exist in this KeyedCollection.");
			}
			string keyForItem = GetKeyForItem(item);
			if (!Comparer.Equals(keyForItem, newKey))
			{
				if (newKey != null)
				{
					AddKey(newKey, item);
				}
				if (keyForItem != null)
				{
					RemoveKey(keyForItem);
				}
			}
		}

		protected override void ClearItems()
		{
			base.ClearItems();
			_dictionary?.Clear();
		}

		public bool Contains(string key)
		{
			if (key == null)
			{
				throw new global::System.ArgumentNullException("key");
			}
			if (_dictionary != null)
			{
				return _dictionary.ContainsKey(key);
			}
			return false;
		}

		private bool ContainsItem(global::Newtonsoft.Json.Linq.JToken item)
		{
			if (_dictionary == null)
			{
				return false;
			}
			string keyForItem = GetKeyForItem(item);
			global::Newtonsoft.Json.Linq.JToken value;
			return _dictionary.TryGetValue(keyForItem, out value);
		}

		private void EnsureDictionary()
		{
			if (_dictionary == null)
			{
				_dictionary = new global::System.Collections.Generic.Dictionary<string, global::Newtonsoft.Json.Linq.JToken>(Comparer);
			}
		}

		private string GetKeyForItem(global::Newtonsoft.Json.Linq.JToken item)
		{
			return ((global::Newtonsoft.Json.Linq.JProperty)item).Name;
		}

		protected override void InsertItem(int index, global::Newtonsoft.Json.Linq.JToken item)
		{
			AddKey(GetKeyForItem(item), item);
			base.InsertItem(index, item);
		}

		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new global::System.ArgumentNullException("key");
			}
			if (_dictionary != null)
			{
				if (_dictionary.TryGetValue(key, out global::Newtonsoft.Json.Linq.JToken value))
				{
					return Remove(value);
				}
				return false;
			}
			return false;
		}

		protected override void RemoveItem(int index)
		{
			string keyForItem = GetKeyForItem(base.Items[index]);
			RemoveKey(keyForItem);
			base.RemoveItem(index);
		}

		private void RemoveKey(string key)
		{
			_dictionary?.Remove(key);
		}

		protected override void SetItem(int index, global::Newtonsoft.Json.Linq.JToken item)
		{
			string keyForItem = GetKeyForItem(item);
			string keyForItem2 = GetKeyForItem(base.Items[index]);
			if (Comparer.Equals(keyForItem2, keyForItem))
			{
				if (_dictionary != null)
				{
					_dictionary[keyForItem] = item;
				}
			}
			else
			{
				AddKey(keyForItem, item);
				if (keyForItem2 != null)
				{
					RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		public bool TryGetValue(string key, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out global::Newtonsoft.Json.Linq.JToken? value)
		{
			if (_dictionary == null)
			{
				value = null;
				return false;
			}
			return _dictionary.TryGetValue(key, out value);
		}

		public int IndexOfReference(global::Newtonsoft.Json.Linq.JToken t)
		{
			return global::Newtonsoft.Json.Utilities.CollectionUtils.IndexOfReference((global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JToken>)base.Items, t);
		}

		public bool Compare(global::Newtonsoft.Json.Linq.JPropertyKeyedCollection other)
		{
			if (this == other)
			{
				return true;
			}
			global::System.Collections.Generic.Dictionary<string, global::Newtonsoft.Json.Linq.JToken> dictionary = _dictionary;
			global::System.Collections.Generic.Dictionary<string, global::Newtonsoft.Json.Linq.JToken> dictionary2 = other._dictionary;
			if (dictionary == null && dictionary2 == null)
			{
				return true;
			}
			if (dictionary == null)
			{
				return dictionary2.Count == 0;
			}
			if (dictionary2 == null)
			{
				return dictionary.Count == 0;
			}
			if (dictionary.Count != dictionary2.Count)
			{
				return false;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Linq.JToken> item in dictionary)
			{
				if (!dictionary2.TryGetValue(item.Key, out var value))
				{
					return false;
				}
				global::Newtonsoft.Json.Linq.JProperty jProperty = (global::Newtonsoft.Json.Linq.JProperty)item.Value;
				global::Newtonsoft.Json.Linq.JProperty jProperty2 = (global::Newtonsoft.Json.Linq.JProperty)value;
				if (jProperty.Value == null)
				{
					return jProperty2.Value == null;
				}
				if (!jProperty.Value.DeepEquals(jProperty2.Value))
				{
					return false;
				}
			}
			return true;
		}
	}
}
