namespace UnityEngine.Rendering
{
	internal sealed class SerializedDictionaryDebugView<K, V>
	{
		private global::System.Collections.Generic.IDictionary<K, V> dict;

		[global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.RootHidden)]
		public global::System.Collections.Generic.KeyValuePair<K, V>[] Items
		{
			get
			{
				global::System.Collections.Generic.KeyValuePair<K, V>[] array = new global::System.Collections.Generic.KeyValuePair<K, V>[dict.Count];
				dict.CopyTo(array, 0);
				return array;
			}
		}

		public SerializedDictionaryDebugView(global::System.Collections.Generic.IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new global::System.ArgumentNullException(dictionary.ToString());
			}
			dict = dictionary;
		}
	}
}
