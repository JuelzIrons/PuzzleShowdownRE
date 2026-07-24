namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.IncludeInSettings(false)]
	public sealed class DictionaryAsset : global::Unity.VisualScripting.LudiqScriptableObject, global::System.Collections.Generic.IDictionary<string, object>, global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, object>>, global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<string, object>>, global::System.Collections.IEnumerable
	{
		public object this[string key]
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

		[global::Unity.VisualScripting.Serialize]
		public global::System.Collections.Generic.Dictionary<string, object> dictionary { get; private set; } = new global::System.Collections.Generic.Dictionary<string, object>();

		public int Count => dictionary.Count;

		public global::System.Collections.Generic.ICollection<string> Keys => dictionary.Keys;

		public global::System.Collections.Generic.ICollection<object> Values => dictionary.Values;

		bool global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, object>>.IsReadOnly => ((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, object>>)dictionary).IsReadOnly;

		protected override void OnAfterDeserialize()
		{
			base.OnAfterDeserialize();
			if (dictionary == null)
			{
				dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			}
		}

		public void Clear()
		{
			dictionary.Clear();
		}

		public bool ContainsKey(string key)
		{
			return dictionary.ContainsKey(key);
		}

		public void Add(string key, object value)
		{
			dictionary.Add(key, value);
		}

		public void Merge(global::Unity.VisualScripting.DictionaryAsset other, bool overwriteExisting = true)
		{
			foreach (string key in other.Keys)
			{
				if (overwriteExisting)
				{
					dictionary[key] = other[key];
				}
				else if (!dictionary.ContainsKey(key))
				{
					dictionary.Add(key, other[key]);
				}
			}
		}

		public bool Remove(string key)
		{
			return dictionary.Remove(key);
		}

		public bool TryGetValue(string key, out object value)
		{
			return dictionary.TryGetValue(key, out value);
		}

		public global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<string, object>> GetEnumerator()
		{
			return dictionary.GetEnumerator();
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return ((global::System.Collections.IEnumerable)dictionary).GetEnumerator();
		}

		void global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, object>>.Add(global::System.Collections.Generic.KeyValuePair<string, object> item)
		{
			((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, object>>)dictionary).Add(item);
		}

		bool global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, object>>.Contains(global::System.Collections.Generic.KeyValuePair<string, object> item)
		{
			return ((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, object>>)dictionary).Contains(item);
		}

		void global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, object>>.CopyTo(global::System.Collections.Generic.KeyValuePair<string, object>[] array, int arrayIndex)
		{
			((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, object>>)dictionary).CopyTo(array, arrayIndex);
		}

		bool global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, object>>.Remove(global::System.Collections.Generic.KeyValuePair<string, object> item)
		{
			return ((global::System.Collections.Generic.ICollection<global::System.Collections.Generic.KeyValuePair<string, object>>)dictionary).Remove(item);
		}

		[global::UnityEngine.ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}
	}
}
