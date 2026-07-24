namespace Unity.VisualScripting
{
	public interface IKeyedCollection<TKey, TItem> : global::System.Collections.Generic.ICollection<TItem>, global::System.Collections.Generic.IEnumerable<TItem>, global::System.Collections.IEnumerable
	{
		TItem this[TKey key] { get; }

		TItem this[int index] { get; }

		bool TryGetValue(TKey key, out TItem value);

		bool Contains(TKey key);

		bool Remove(TKey key);
	}
}
