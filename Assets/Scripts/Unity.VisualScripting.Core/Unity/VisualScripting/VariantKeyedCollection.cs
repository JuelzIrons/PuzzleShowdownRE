namespace Unity.VisualScripting
{
	public class VariantKeyedCollection<TBase, TImplementation, TKey> : global::Unity.VisualScripting.VariantCollection<TBase, TImplementation>, global::Unity.VisualScripting.IKeyedCollection<TKey, TBase>, global::System.Collections.Generic.ICollection<TBase>, global::System.Collections.Generic.IEnumerable<TBase>, global::System.Collections.IEnumerable where TImplementation : TBase
	{
		public TBase this[TKey key] => (TBase)(object)implementation[key];

		public new global::Unity.VisualScripting.IKeyedCollection<TKey, TImplementation> implementation { get; private set; }

		TBase global::Unity.VisualScripting.IKeyedCollection<TKey, TBase>.this[int index] => (TBase)(object)implementation[index];

		public VariantKeyedCollection(global::Unity.VisualScripting.IKeyedCollection<TKey, TImplementation> implementation)
			: base((global::System.Collections.Generic.ICollection<TImplementation>)implementation)
		{
			this.implementation = implementation;
		}

		public bool TryGetValue(TKey key, out TBase value)
		{
			TImplementation value2;
			bool result = implementation.TryGetValue(key, out value2);
			value = (TBase)(object)value2;
			return result;
		}

		public bool Contains(TKey key)
		{
			return implementation.Contains(key);
		}

		public bool Remove(TKey key)
		{
			return implementation.Remove(key);
		}
	}
}
